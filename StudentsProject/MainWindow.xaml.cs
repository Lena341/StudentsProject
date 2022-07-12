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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudentsProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] semesters = {0,1,2,3,4,5,6,7,8};
        private string[] directions = { "Computer Engineering", "Network Engineering", "Software Engineering", "No direction" };
        StudentDBEntities dBEntities;
        
        public MainWindow()
        {
            InitializeComponent();
            this.Reset();
        }

        public void Reset()
        {
            firstName.Text = String.Empty; 
            lastName.Text = String.Empty;
            stdSemester.Items.Clear();
            foreach (int i in semesters)
            {
                stdSemester.Items.Add(i);
            }
            

            stdDirection.Items.Clear();
            foreach (string i in directions)
            {
                stdDirection.Items.Add(i);
            }
            stdDirection.Text = stdDirection.Items[0] as string;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to quit?", "Confirm",MessageBoxButton.OKCancel,MessageBoxImage.Question);
            e.Cancel = (result == MessageBoxResult.Cancel);
        }

        private void newStudent_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
            firstName.IsEnabled = true;
            lastName.IsEnabled = true;
            stdSemester.IsEnabled = true;
            stdDirection.IsEnabled = true;
            insertStudent.IsEnabled = true;
            updateStudent.IsEnabled = true;
            deleteStudent.IsEnabled = true;
            clearButton.IsEnabled = true;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void insertStudent_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student(); 
            student.Name = firstName.Text; 
            student.LastName = lastName.Text; 
            student.Semester = stdSemester.SelectedIndex;
            student.Direction = stdDirection.Text; 
            dBEntities.Students.Add(student); 
            dBEntities.SaveChanges();
            studentsList.ItemsSource = dBEntities.Students.ToList(); 
        }

        private void updateStudent_Click(object sender, RoutedEventArgs e)
        {
            var updateRow = dBEntities.Students.Where(w => w.Name == firstName.Text).FirstOrDefault();
            updateRow.Name = firstName.Text;
            updateRow.LastName = lastName.Text;
            updateRow.Semester = stdSemester.SelectedIndex; 
            updateRow.Direction = stdDirection.Text;
            dBEntities.SaveChanges();
            studentsList.ItemsSource = dBEntities.Students.ToList();
        }

        private void deleteStudent_Click(object sender, RoutedEventArgs e)
        {
            var deleteRow = dBEntities.Students.Where(i => i.LastName == lastName.Text).FirstOrDefault();
            dBEntities.Students.Remove(deleteRow);
            dBEntities.SaveChanges();
            studentsList.ItemsSource = dBEntities.Students.ToList();
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            firstName.Text = String.Empty;
            lastName.Text = String.Empty;
            stdSemester.Items.Clear();
            stdDirection.Items.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dBEntities = new StudentDBEntities(); 
            studentsList.ItemsSource = dBEntities.Students.ToList();
        }
}
