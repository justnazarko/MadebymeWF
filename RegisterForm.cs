using MadebymeWF.Data;
using MadebymeWF.Models;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;


namespace MadebymeWF
{
    public partial class RegisterForm : Form
    {
        private Logger logger = new Logger();
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUsername.Text == "Логін")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.Text = "Логін";
                txtUsername.ForeColor = Color.Gray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Пароль")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.Text = "Пароль";
                txtPassword.ForeColor = Color.Gray;
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            if (txtEmail.Text == "Email")
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Text = "Email";
                txtEmail.ForeColor = Color.Gray;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    string login = txtUsername.Text.Trim();
                    string password = txtPassword.Text.Trim();
                    string email = txtEmail.Text.Trim();

                    if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
                    {
                        MessageBox.Show("Всі поля повинні бути заповнені!");
                        logger.Write("Реєстрація", "Поля не заповнені.", true);
                        return;
                    }

                    if (!IsValidPassword(password))
                    {
                        MessageBox.Show("Пароль повинен містити як мінімум 8 символів, включаючи букви та цифри.");
                        logger.Write("Реєстрація", "Невірний формат пароля.", true);
                        return;
                    }

                    if (!IsValidEmail(email))
                    {
                        MessageBox.Show("Введіть правильний формат email.");
                        logger.Write("Реєстрація", "Невірний формат email.", true);
                        return;
                    }

                    if (context.Users.Any(u => u.Login == login))
                    {
                        MessageBox.Show("Користувач з таким логіном вже існує. Оберіть інший логін.");
                        logger.Write("Реєстрація", $"Користувач з логіном '{login}' вже існує.", true);
                        return;
                    }

                    User newUser = new User
                    {
                        Login = login,
                        Password = password,
                        Email = email,
                        Name = "Новий користувач"
                    };

                    context.Users.Add(newUser);
                    context.SaveChanges();

                    MessageBox.Show("Реєстрація успішна! Зараз вас буде перенаправлено на форму входу.");
                    logger.Write("Реєстрація", $"Користувач '{login}' успішно зареєстрований.");

                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при реєстрації: " + ex.Message);
                    logger.Write("Реєстрація", $"Помилка: {ex.Message}", true);
                }
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
        public bool IsValidPassword(string password)
        {
            return password.Length >= 8 &&
                   password.Any(char.IsLetter) &&
                   password.Any(char.IsDigit);
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
