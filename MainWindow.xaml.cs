using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.IO;

namespace Test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Mario Krajoski - Task 1
            InitializeComponent();
            //Add components -> tests in the combo box
            comboTestName.Items.Add(new ComboBoxItem() { Content = "MoCA", FontSize=15});
            comboTestName.Items.Add(new ComboBoxItem() { Content = "ADAS", FontSize = 15 });
            comboTestName.Items.Add(new ComboBoxItem() { Content = "EUROTEST", FontSize = 15 });
            comboTestName.Items.Add(new ComboBoxItem() { Content = "PHOTOTEST", FontSize = 15 });
            comboTestName.Items.Add(new ComboBoxItem() { Content = "ACE-III", FontSize = 15 });

        }

        private void finalButton_Click(object sender, RoutedEventArgs e)
        {
            string primaryText = text1.Text; //input data from primary text
            string secondaryText = text2.Text; //input data from secondary text
            string tertiaryText = text3.Text; //input data from tertiary text
            string start = text4.Text; //input data from the start field
            string stop = text5.Text; //input data from the stop field

            if (String.IsNullOrEmpty(primaryText) == true) //first condition: check if primary field is empty
                MessageBox.Show("Primary Text field is empty. Please enter valid data.","Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                //check if Primary field is of valid format which is integer or decimal
                if (int.TryParse(primaryText, out int _) == false && decimal.TryParse(primaryText, out decimal _) == false)
                    MessageBox.Show("Primary Text is not integer or decimal. Please enter valid data.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    //Check cases where Secondary Text and Tertiary Text are not empty
                    //Note: Secondary and Tertiary fields can be left blank
                    if(String.IsNullOrEmpty(secondaryText) == false)
                    {
                        if (int.TryParse(secondaryText, out int _) == false && decimal.TryParse(secondaryText, out decimal _) == false)
                            MessageBox.Show("Secondary Text is not integer or decimal. Please enter valid data.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if(String.IsNullOrEmpty (tertiaryText) == false)
                    {
                        if (int.TryParse(tertiaryText, out int _) == false && decimal.TryParse(tertiaryText, out decimal _) == false)
                            MessageBox.Show("Tertiary Text is not integer or decimal. Please enter valid data.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    //Check if Start and Stop fields are empty. If yes, show error message.
                    if (String.IsNullOrEmpty(start) == true || String.IsNullOrEmpty(stop) == true) {
                        MessageBox.Show("Start field and Stop field must not be empty. Please try again.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                    else {
                        //Check if start field and stop field are of desired format -> timetables with integers and the symbol :
                        string timePattern = @"^[0-9]{1,2}[:][0-5][0-9]$";
                        if (Regex.Match(start, timePattern).Success == false || Regex.Match(stop, timePattern).Success == false)
                            MessageBox.Show("You inserted invalid data. Please insert timetables in correct format." +
                                " Correct formats example: 08:30 or 8:30", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            //Put the two timetables as variables and compare them. 
                            //If the result from Start field is greater than the result from the end field, show error message. 
                            TimeSpan.TryParse(start, out TimeSpan rezultat1);
                            TimeSpan.TryParse(stop, out TimeSpan rezultat2);
                            if (rezultat1 > rezultat2)
                                MessageBox.Show("Error! The time for Start is bigger than the time for Stop. Please enter valid data.", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                            {
                                //Everything is okay till now. So write the contents in a .csv file which is created from here.
                                string filePath = "C:\\Users\\User3245\\Desktop\\braintrip\\Test1\\results.csv"; // Specify the file path
                                string[] rows = {
                                        "TestName; PrimaryScore; SecondaryScore; TertiaryScore; StartTime; StopTime",
                                        comboTestName.Text+"; "+primaryText+"; "+secondaryText+"; "+tertiaryText+"; "+start+"; "+stop+"\n"
                                    };

                                // Write rows to the CSV file. Append if there are already existing rows.
                                File.AppendAllLines(filePath, rows);
                                MessageBox.Show("You successfully inserted the test.","Successful insertion",MessageBoxButton.OK);
                            }
                        }
                    }
                }
            }
        }
    }
}