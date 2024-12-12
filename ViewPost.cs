using MadebymeWF.Data;
using MadebymeWF.Models;
using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MadebymeWF
{
    public partial class ViewPostForm : Form
    {
        private readonly int _postId;

        public ViewPostForm(int postId)
        {
            _postId = postId;
            InitializeComponent();
            LoadPostDetails();
        }

        private void LoadPostDetails()
        {
            using (var context = new ApplicationDbContext())
            {
                // Завантаження поста з даними про продавця через SellerRef
                var post = context.Posts
                    .FirstOrDefault(p => p.PostId == _postId);

                if (post == null)
                {
                    MessageBox.Show("Пост не знайдено.");
                    Close();
                    return;
                }

                var seller = context.Users
                    .FirstOrDefault(u => u.UserId == post.SellerRef);

                PopulatePostDetails(post, seller);
            }
        }

        private void PopulatePostDetails(Post post, User seller)
        {
            // Завантаження фото
            if (!string.IsNullOrWhiteSpace(post.PhotoPath) && File.Exists(post.PhotoPath))
            {
                pbPhoto.Image = Image.FromFile(post.PhotoPath);
            }
            else
            {
                pbPhoto.Image = Properties.Resources.DefaultImage;
            }

            // Назва поста
            lblTitle.Text = post.Name;
            lblTitle.Font = new Font("Arial", 14, FontStyle.Bold);

            // Інформація про продавця
            if (seller != null)
            {
                lblName.Text = $"Продавець: {seller.Name}";
                lblContact.Text = $"Контакти: {seller.MobileNumber ?? "Немає інформації"}";
            }
            else
            {
                lblName.Text = "Продавець: Невідомо";
                lblContact.Text = "Контакти: Немає інформації";
            }

            // Ціна
            lblPrice.Text = $"Ціна: {post.Price:F2} грн";
            lblPrice.Font = new Font("Arial", 16, FontStyle.Bold);
            lblPrice.ForeColor = Color.Green;

            // Опис
            lblDescription.Text = $"Опис:\n{post.Description}";
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ViewPostForm_Load(object sender, EventArgs e)
        {
            // Можливо, для початкової ініціалізації
        }

        private void ViewPostForm_Load_1(object sender, EventArgs e)
        {
            // Будь-яка додаткова логіка, яку потрібно виконати під час завантаження форми
        }
    }
}