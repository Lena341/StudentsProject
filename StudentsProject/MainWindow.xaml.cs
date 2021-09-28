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
            firstName.Text = String.Empty; //We set the Text as empty to make sure that the textboxes will be empty.
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
            this.Reset();//We call the Reset() method so that all the control elements in the form can be in their default state.
            //All the control elements are activated
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
            Student student = new Student(); //We create a new Student object
            student.Name = firstName.Text; //we get the content of the textBox and set it as the value of the Name
            student.LastName = lastName.Text; //Same for LastName
            student.Semester = stdSemester.SelectedIndex;//Because we use the SelectedIndex Selector the values from semesters array start from 0 and not from 1.
            student.Direction = stdDirection.Text; //Same for Direction
            dBEntities.Students.Add(student); //We add the student object to dbEntities
            dBEntities.SaveChanges();
            studentsList.ItemsSource = dBEntities.Students.ToList(); 
        }

        private void updateStudent_Click(object sender, RoutedEventArgs e)
        {
            //First we declare the variable updateRow. We use var so that .NET can determine the data type automatically
            //Then we use a lambda expression to create an anonymous function. The input parameter is w the expression follows after =>
            //We use a lambda expression because of the FirstOrDefault() method. Because it can be converted to a delegate type.
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
            dBEntities = new StudentDBEntities(); //The dBEntities field represents the database entities generated by the Microsoft Entity Framework.
            //It is an instance of the MoviesDBEntities class that was generated by the Entity Designer
            studentsList.ItemsSource = dBEntities.Students.ToList(); // or studentsList.ItemsSource = dBEntities.Students.ToList();
            //Here the dBEntities is used the retrieve data from the Students table.
            //ToList() is used to convert the set of students into a generic collection of Student objects (List<Students>)
        }


    }
}
