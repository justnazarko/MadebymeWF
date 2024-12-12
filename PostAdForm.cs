using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MadebymeWF.Data;
using MadebymeWF.Models;

namespace MadebymeWF
{
    public partial class PostAdForm : Form
    {
        private int _currentUserId; 

        public PostAdForm(int userId)
        {
            InitializeComponent();
            _currentUserId = userId;
        }

        private string _photoPath;

        private void btnUploadPhoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pbPhoto.Image = Image.FromFile(openFileDialog.FileName);
                    _photoPath = openFileDialog.FileName;
                }
            }
        }

        private void btnPostAd_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string description = txtDescription.Text.Trim();
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string price = txtPrice.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description) ||
                string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(price) || _photoPath == null)
            {
                MessageBox.Show("Будь ласка, заповніть всі обов'язкові поля та додайте фото!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var logger = new Logger();

            try
            {
                string userFolder = Path.Combine("photos", $"user_{_currentUserId}");
                Directory.CreateDirectory(userFolder);

                string photoFileName = $"{title.Replace(" ", "_")}.jpg";
                string destinationPath = Path.Combine(userFolder, photoFileName);
                File.Copy(_photoPath, destinationPath, overwrite: true);

                var context = new ApplicationDbContext();
                var post = new Post
                {
                    Name = title,
                    Description = description,
                    PhotoPath = destinationPath,
                    SellerRef = _currentUserId,
                };
                context.Posts.Add(post);
                context.SaveChanges();
                logger.Write("AddPost", $"Пост із назвою '{title}' успішно додано користувачем {_currentUserId}.");

                MessageBox.Show("Оголошення успішно розміщено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logger.Write("AddPost", ex.Message, true);
            }
        }
    

    private void txtField_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.ForeColor == Color.Gray)
            {
                textBox.Text = "";
                textBox.ForeColor = Color.Black;
            }
        }

        private void txtField_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.ForeColor = Color.Gray;

                if (textBox == txtTitle) textBox.Text = "Назва товару";
                else if (textBox == txtDescription) textBox.Text = "Опис товару";
                else if (textBox == txtName) textBox.Text = "Ім'я";
                else if (textBox == txtEmail) textBox.Text = "Електронна пошта";
                else if (textBox == txtPhone) textBox.Text = "Телефон";
                else if (textBox == txtAddress) textBox.Text = "Адреса";
                else if (textBox == txtPrice) textBox.Text = "Ціна";
            }
        }

        private void PostAdForm_Load(object sender, EventArgs e)
        {

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void pbPhoto_Click(object sender, EventArgs e)
        {

        }
    }
}
