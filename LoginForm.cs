using MadebymeWF.Data;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;


namespace MadebymeWF
{
    public partial class LoginForm : Form
    {
        private Logger logger = new Logger();
        public LoginForm()
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (var context = new ApplicationDbContext())
            {
                try
                {
                    var user = context.Users
                        .Where(u => u.Login == login && u.Password == password)
                        .FirstOrDefault();

                    if (user != null)
                    {
                        UserSession.StartSession(user);
                        logger.Write("Вхід", $"Користувач '{login}' успішно увійшов.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Помилка при спробі авторизації: " + ex.Message);
                    logger.Write("Вхід", $"Помилка: {ex.Message}", true);
                    return;
                }
            }

            if (UserSession.IsUserLoggedIn())
            {
                MainScreen mainScreen = new MainScreen();
                mainScreen.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Невірний логін або пароль. Спробуйте ще раз.");
                logger.Write("Вхід", $"Невдала спроба входу для логіну '{login}'.", true);
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();

            registerForm.Show();

            this.Hide(); 
        }


        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
