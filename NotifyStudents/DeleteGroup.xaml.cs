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
    /// Interaction logic for DeleteGroup.xaml
    /// </summary>
    public partial class DeleteGroup : Window
    {
        public string GroupName { get; set; }
        public DeleteGroup()
        {
            InitializeComponent();
        }

        private void DeleteCommandButton_Click(object sender, RoutedEventArgs e)
        {
            GroupName = DeleteTextBox.Text;
            this.Close();
        }
    }
}
