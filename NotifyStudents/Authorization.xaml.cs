using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace NotifyStudents
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Group> groups = new List<Group>(); // Список групп
        private List<Student> allStudents = new List<Student>(); // Список всех студентов

        public MainWindow()
        {
            InitializeComponent();

        }

        public class Student
        {
            public string NameSurname { get; set; }
            public string Email { get; set; }
            public string GroupName { get; set; }
        }

        public class Group
        {
            public string Name { get; set; }
            public List<Student> Students { get; set; }

            public Group()
            {
                Students = new List<Student>();
            }
        }

        private void AddGroupToTreeView(string groupNumber)
        {
            // Проверяем, существует ли уже чекбокс с указанной группой
            bool checkBoxExists = listBox.Items.OfType<Border>()
                .Any(border => (border.Child as CheckBox)?.Content?.ToString() == groupNumber);

            if (!checkBoxExists)
            {
                // Создание нового чекбокса
                CheckBox checkBox = new CheckBox();
                checkBox.Content = groupNumber;
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;

                // Создание обертки для чекбокса с заданными отступами
                Border border = new Border();
                border.Margin = new Thickness(5);
                border.Child = checkBox;

                // Добавление обертки с чекбоксом в ListView
                listBox.Items.Add(border);
            }
        }


        private void AddStudentToTreeView(string groupName, string studentNameSurname, string email, TreeView tw)
        {
            // Поиск узла группы в TreeView
            TreeViewItem groupNode = null;
            foreach (TreeViewItem node in tw.Items)
            {
                if (node.Header.ToString() == groupName)
                {
                    groupNode = node;
                    break;
                }
            }

            if (groupNode == null)
            {
                // Создание нового узла группы
                groupNode = new TreeViewItem();
                groupNode.Header = groupName;

                // Добавление узла группы в TreeView
                tw.Items.Add(groupNode);
            }

            // Проверяем, есть ли уже узел студента в узле группы
            bool studentNodeExists = false;
            foreach (TreeViewItem studentNode in groupNode.Items)
            {
                if (studentNode.Header.ToString() == $"{studentNameSurname} - {email}")
                {
                    studentNodeExists = true;
                    break;
                }
            }

            if (!studentNodeExists)
            {
                // Создание нового узла студента
                TreeViewItem studentNode = new TreeViewItem();
                studentNode.Header = $"{studentNameSurname} - {email}";

                // Добавление узла студента в узел группы
                groupNode.Items.Add(studentNode);
            }
            AddGroupToTreeView(groupName);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStudent addStudent = new AddStudent();
            addStudent.ShowDialog();

            string nameSurname = addStudent.NameSurname;
            string email = addStudent.Email;
            string groupName = addStudent.GroupName;

            // Поиск группы по имени
            Group existingGroup = groups.FirstOrDefault(g => g.Name == groupName);

            if (existingGroup != null)
            {
                // Группа уже существует, добавляем студента в существующую группу
                existingGroup.Students.Add(new Student
                {
                    NameSurname = nameSurname,
                    Email = email,
                    GroupName = groupName
                });

            }
            else
            {
                // Создаем новую группу и добавляем студента в нее
                Group newGroup = new Group
                {
                    Name = groupName,
                    Students = new List<Student>
        {
            new Student
            {
                NameSurname = nameSurname,
                Email = email,
                GroupName = groupName
            }
        }
                };

                // Добавляем новую группу в список групп
                groups.Add(newGroup);
            }
            AddStudentToTreeView(groupName, nameSurname, email, treeView);
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный номер группы из CheckBox
            string selectedGroupNumber = ((CheckBox)sender).Content.ToString();

            // Обновляем список студентов в TreeView
            UpdateStudentTreeView(selectedGroupNumber, TreeOfStudents);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Очищаем список студентов в другом ListView
            TreeOfStudents.Items.Clear();
        }

        private void UpdateStudentTreeView(string groupNumber, TreeView tw)
        {
            // Очищаем TreeView
            tw.Items.Clear();

            // Поиск группы по номеру
            Group group = groups.FirstOrDefault(g => g.Name == groupNumber);

            if (group != null)
            {
                // Создание узла группы
                TreeViewItem groupNode = new TreeViewItem();
                groupNode.Header = group.Name;

                // Добавление узла группы в TreeView
                tw.Items.Add(groupNode);

                // Добавление студентов в узел группы
                foreach (Student student in group.Students)
                {
                    TreeViewItem studentNode = new TreeViewItem();
                    studentNode.Header = $"{student.NameSurname} - {student.Email}";

                    // Добавление узла студента в узел группы
                    groupNode.Items.Add(studentNode);
                }
            }
        }
        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Уверены, что хотите удалить данный элемент?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                if (treeView.SelectedItem is TreeViewItem selectedItem)
                {
                    if (selectedItem.Header is string studentInfo)
                    {
                        // Получение имени и электронной почты студента из заголовка
                        string[] parts = studentInfo.Split(new string[] { " - " }, StringSplitOptions.None);
                        string nameSurname = parts[0];
                        string email = parts[1];

                        // Поиск группы, содержащей студента
                        Group group = groups.FirstOrDefault(g => g.Students.Any(s => s.NameSurname == nameSurname && s.Email == email));

                        if (group != null)
                        {
                            // Поиск студента в группе и удаление его
                            Student existingStudent = group.Students.FirstOrDefault(s => s.NameSurname == nameSurname && s.Email == email);
                            if (existingStudent != null)
                            {
                                group.Students.Remove(existingStudent);
                            }
                        }

                        // Удаление выбранного студента из TreeView
                        if (selectedItem.Parent is TreeViewItem parent)
                        {
                            parent.Items.Remove(selectedItem);
                        }
                        else
                        {
                            treeView.Items.Remove(selectedItem);
                        }

                        // Обновление списка чекбоксов в listBox
                        UpdateCheckBoxList();
                    }
                }
            }
        }


        private void UpdateCheckBoxList()
        {
            // Очищаем listBox
            listBox.Items.Clear();

            // Перебираем группы
            foreach (Group group in groups)
            {
                // Создание нового чекбокса
                CheckBox checkBox = new CheckBox();
                checkBox.Content = group.Name;
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;

                // Проверяем, есть ли студенты в группе
                if (group.Students.Any())
                {
                    // Добавляем чекбокс в listBox
                    Border border = new Border();
                    border.Margin = new Thickness(5);
                    border.Child = checkBox;
                    listBox.Items.Add(border);
                }
            }
        }


        private void AddGroup_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
