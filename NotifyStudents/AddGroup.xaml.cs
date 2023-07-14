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
    /// Interaction logic for AddGroup.xaml
    /// </summary>
    public partial class AddGroup : Window
    {
        public string GroupName { get; private set; }
        public AddGroup()
        {
            InitializeComponent();
        }

        private void AddCommandButton_Click(object sender, RoutedEventArgs e)
        {
            GroupName = AddTextBox.Text;
            this.Close();
        }
    }
}
