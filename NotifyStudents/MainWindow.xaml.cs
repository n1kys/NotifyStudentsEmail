using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace NotifyStudents
{
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        public string Login { get; set; }
        public string Password { get; set; }


        private bool IsEmailValid(string email)
        {
            string emailPattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$";
            return Regex.IsMatch(email, emailPattern);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login = LoginTextBox.Text;
            Password = PasswordTextBox.Password;

            if (!IsEmailValid(Login))
            {
                MessageBox.Show("Invalid email format");
                return;
            }
            else
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }

            // Дополнительная логика для аутентификации
        }
    }
}

