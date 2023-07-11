using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotifyStudents
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window
    {
        public string NameSurname { get; private set; }
        public string Email { get; private set; }
        public string GroupName { get; private set; }

        public AddStudent()
        {
            InitializeComponent();
        }

        private void AddStudButton_Click(object sender, RoutedEventArgs e)
        {
            // Получение введенных данных
            NameSurname = NameSurnameTextBox.Text;
            Email = EmailTextBox.Text;
            GroupName = GroupTextBox.Text;

            // Закрытие окна
            this.Close();
        }
    }

}
