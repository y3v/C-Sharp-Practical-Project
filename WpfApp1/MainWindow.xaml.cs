using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public List<Appointment> appointments = new List<Appointment>();
        public List<TextBox> apptBox = new List<TextBox>();
        public List<EditButton> editBtns = new List<EditButton>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string month = DateTime.Today.Month.ToString();
            switch (month)
            {
                case "1":
                    month = "January";
                    break;
                case "2":
                    month = "February";
                    break;
                case "3":
                    month = "March";
                    break;
                case "4":
                    month = "April";
                    break;
                case "5":
                    month = "May";
                    break;
                case "6":
                    month = "June";
                    break;
                case "7":
                    month = "July";
                    break;
                case "8":
                    month = "August";
                    break;
                case "9":
                    month = "September";
                    break;
                case "10":
                    month = "October";
                    break;
                case "11":
                    month = "November";
                    break;
                case "12":
                    month = "December";
                    break;
            }

            txtDate.Text = month + " " + DateTime.Today.Day;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddAppointment appt = new AddAppointment();
            appt.Show();
        }

        public void AppointmentAdded()
        {
            Label[] labels = { lbl1, lbl2, lbl3, lbl4, lbl5, lbl6, lbl7, lbl8, lbl9, lbl10, lbl11, lbl12, lbl13, lbl14, lbl15, lbl16 };

            switch (appointments.Last().AppointmentWith)    // to determine which column the appointment is to be inserted into 
            {
                case "Callbacks":
                    appointments.Last().ColumnPos = 0;
                    break;
                case "Olivier":
                    appointments.Last().ColumnPos = 1;
                    break;
                case "Yev":
                    appointments.Last().ColumnPos = 2;
                    break;
            }

            for (int i = 0; i < labels.Length; i++) // to determine which row the appointment is to be inserted into
            {
                if (appointments.Last().Time == (String)labels[i].Content)
                {
                    appointments.Last().RowPos = i;
                }
            }

            //creation and design of the box
            apptBox.Add(new TextBox());
            apptBox.Last().Text = appointments.Last().FirstName + " " + appointments.Last().LastName;
            apptBox.Last().FontWeight = FontWeights.Bold;

            switch (appointments.Last().Duration)    // to determine duration -- determine the rowspan
            {
                case 30:
                    apptBox.Last().SetValue(Grid.RowSpanProperty, 1);
                    apptBox.Last().Background = Brushes.CadetBlue;
                    break;
                case 60:
                    apptBox.Last().SetValue(Grid.RowSpanProperty, 2);
                    apptBox.Last().Background = Brushes.DarkCyan;
                    break;
            }
            

            //set column and row position in the grid for where the appointment box will be inserted
            Grid.SetColumn(apptBox.Last(), appointments.Last().ColumnPos);
            Grid.SetRow(apptBox.Last(), appointments.Last().RowPos);
            apptBox.Last().IsReadOnly = true; // make sure information can not be edited in the main window
            grdSchedule.Children.Add(apptBox.Last());

            //--------create an edit button that appears at the top left corner of same grid blick--------
            editBtns.Add(new EditButton
            {
                Height = 20,
                Width = 20,
                Index = appointments.IndexOf(appointments.Last()),
                Content = new Image
                {
                    Source = new BitmapImage(new Uri("/resources/pencil-icon.jpg", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center
                }
            });
            //---------set the grid coordinates--------------
            Grid.SetColumn(editBtns.Last(), appointments.Last().ColumnPos);
            Grid.SetRow(editBtns.Last(), appointments.Last().RowPos);
            editBtns.Last().HorizontalAlignment = HorizontalAlignment.Right;
            editBtns.Last().VerticalAlignment = VerticalAlignment.Top;
            grdSchedule.Children.Add(editBtns.Last()); //add button to the grid

            //---------add click event to the event handler--------------
            editBtns.Last().Click += new RoutedEventHandler(editButton_Click);
        }

        void editButton_Click(object sender, RoutedEventArgs e)
        {
            /*the idea is to create a window that is almost identical to the original AddAppointment Window
             with the added difference that this new one will rep*/
            EditAppointment edit = new EditAppointment(appointments, ((EditButton)sender).Index);
            edit.Show();
        }

    }
}
