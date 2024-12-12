using MadebymeWF.Data;
using MadebymeWF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MadebymeWF
{
    public partial class MainScreen : Form
    {
        public List<PostViewModel> posts;
        private int currentPage = 0; 
        private const int itemsPerPage = 8;
        private ContextMenuStrip contextMenu;

        public MainScreen()
        {
            InitializeComponent();
            LoadPostsFromDatabase();
            LoadProductsToGrid();
            InitializeContextMenu();
        }
        private void RefreshData()
        {
            LoadPostsFromDatabase();
            LoadProductsToGrid();
            UpdateNavigationButtons();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Q)
            {
                RefreshScreen(); 
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void RefreshScreen()
        {
            try
            {
                LoadPostsFromDatabase();
                LoadProductsToGrid();
                UpdateNavigationButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка оновлення екрану: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void LoadPostsFromDatabase()
        {
            using (var context = new ApplicationDbContext())
            {
                posts = context.Posts
                .Select(p => new PostViewModel
                {
                    PostId = p.PostId,
                    Name = p.Name,
                    Description = p.Description,
                    PhotoPath = p.PhotoPath,
                    Rating = p.Rating,
                    SellerName = p.User.Name ?? "Unknown",
                    PayCard = p.PayCard,
                    Price = p.Price,
                    UserId = p.SellerRef
                })
                .ToList();

            }
        }

        public void LoadProductsToGrid()
        {
            if (productGrid != null)
            {
                productGrid.Controls.Clear(); 
            }
            else
            {
                MessageBox.Show("Елемент productGrid не ініціалізований.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int start = currentPage * itemsPerPage;
            int end = Math.Min(start + itemsPerPage, posts.Count);

            for (int i = start; i < end; i++)
            {
                PostViewModel post = posts[i];

                Panel productCard = new Panel
                {
                    Size = new Size(150, 200),
                    BackColor = (UserSession.IsUserLoggedIn() && UserSession.CurrentUser.UserId == post.UserId)
                   ? Color.FromArgb(255, 210, 210)
                   : Color.FromArgb(210, 255, 210),
                    Tag = post.PostId 
                };


                PictureBox pbPhoto = new PictureBox
                {
                    Dock = DockStyle.Top,
                    Height = 100,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Gray
                };

                try
                {
                    pbPhoto.Image = Image.FromFile(post.PhotoPath); 
                }
                catch (FileNotFoundException)
                {
                    pbPhoto.Image = Properties.Resources.DefaultImage; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка завантаження зображення: {ex.Message}");
                    pbPhoto.Image = Properties.Resources.DefaultImage;
                }

                Label lblName = new Label
                {
                    Text = post.Name,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblSeller = new Label
                {
                    Text = $"Продавець: {post.SellerName}",
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label lblPrice = new Label
                {
                    Text = $"Ціна: {post.Price:F2} грн",
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                productCard.Controls.Add(lblPrice);
                productCard.Controls.Add(lblSeller);
                productCard.Controls.Add(lblName);
                productCard.Controls.Add(pbPhoto);

                productCard.MouseDown += (s, e) => ShowContextMenu(e, post, productCard);

                productCard.Click += (s, e) => OpenViewPost(post);

                productGrid.Controls.Add(productCard);
            }

            UpdateNavigationButtons();
        }
        private void AnimateAndRemovePost(Panel productCard)
        {
            Timer animationTimer = new Timer { Interval = 30 };
            float scale = 1.0f;
            int alpha = 255;

            animationTimer.Tick += (s, e) =>
            {
                scale -= 0.05f;
                alpha -= 15; 

                if (scale <= 0 || alpha <= 0)
                {
                    animationTimer.Stop();
                    animationTimer.Dispose();
                    productGrid.Controls.Remove(productCard);
                    productCard.Dispose(); 
                }
                else
                {
                    productCard.Scale(new SizeF(scale, scale));
                    productCard.BackColor = Color.FromArgb(Math.Max(0, alpha), productCard.BackColor); // Змінюємо прозорість кольору
                }
            };

            animationTimer.Start();
        }

        private void InitializeContextMenu()
        {
            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem deleteItem = new ToolStripMenuItem("Видалити");
            deleteItem.Click += DeletePost_Click;
            contextMenu.Items.Add(deleteItem);
        }

        private void ShowContextMenu(MouseEventArgs e, PostViewModel post, Panel productCard)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (UserSession.IsUserLoggedIn() && UserSession.CurrentUser.UserId == post.UserId)
                {
                    contextMenu.Tag = post;
                    contextMenu.Show(productCard, e.Location); 
                }
            }
        }


        private void DeletePost_Click(object sender, EventArgs e)
        {
            var logger = new Logger();
            var post = contextMenu.Tag as PostViewModel;

            if (post != null)
            {
                DialogResult result = MessageBox.Show("Ви впевнені, що хочете видалити цей пост?", "Підтвердження видалення", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (var context = new ApplicationDbContext())
                        {
                            var postToDelete = context.Posts.FirstOrDefault(p => p.PostId == post.PostId);
                            if (postToDelete != null)
                            {
                                context.Posts.Remove(postToDelete);
                                context.SaveChanges();
                                logger.Write("DeletePost", $"Пост із ID {post.PostId} успішно видалено.");

                                var productCard = productGrid.Controls
                                .OfType<Panel>()
                                .FirstOrDefault(p => (int)p.Tag == post.PostId);

                                if (productCard != null)
                                {
                                    AnimateAndRemovePost(productCard);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Write("DeletePost", ex.Message, true);
                    }
                }
            }
        }

        private void OpenViewPost(PostViewModel post)
        {
            var viewPostForm = new ViewPostForm(post.PostId);
            viewPostForm.Show();
        }

        private void UpdateNavigationButtons()
        {
            btnPrev.Enabled = currentPage > 0;
            btnNext.Enabled = (currentPage + 1) * itemsPerPage < posts.Count;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if ((currentPage + 1) * itemsPerPage < posts.Count)
            {
                currentPage++;
                LoadProductsToGrid();
            }
        }
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                LoadProductsToGrid();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            using (var context = new ApplicationDbContext())
            {
                posts = context.Posts
                    .Include("User") 
                    .Where(p => p.Name.ToLower().Contains(searchText)) 
                    .Select(p => new PostViewModel
                    {
                        PostId = p.PostId,
                        Name = p.Name,
                        Description = p.Description,
                        PhotoPath = p.PhotoPath,
                        Rating = p.Rating,
                        SellerName = p.User != null ? p.User.Name : "Unknown",
                        PayCard = p.PayCard
                    })
                    .ToList();
            }

            currentPage = 0;
            LoadProductsToGrid();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Пошук")
            {
                txtSearch.Clear();
                txtSearch.ForeColor = Color.Black;
            }

        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Пошук";
                txtSearch.ForeColor = Color.Gray;
            }

        }

        private void productGrid_Paint(object sender, PaintEventArgs e)
        {
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            LoadPostsFromDatabase();
            LoadProductsToGrid();
            RefreshData();

        }

        private void btnAddAd_Click(object sender, EventArgs e)
        {
            if (UserSession.IsUserLoggedIn())
            {
                int userId = UserSession.CurrentUser.UserId;

                var postAdForm = new PostAdForm(userId);
                postAdForm.ShowDialog();
                LoadPostsFromDatabase();
                LoadProductsToGrid();
            }
            else
            {
                MessageBox.Show("User is not logged in.");
            }
        }

        private void MainScreen_Load_1(object sender, EventArgs e)
        {
            LoadPostsFromDatabase();
            LoadProductsToGrid();
        }
    }

    public class PostViewModel
    {
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public decimal? Rating { get; set; }
        public string SellerName { get; set; }
        public string PayCard { get; set; }
        public decimal? Price { get; set; }
        public int UserId { get; set; }  
    }

}
