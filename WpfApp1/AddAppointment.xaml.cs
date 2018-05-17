
using Model;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AddAppointment.xaml
    /// </summary>
    public partial class AddAppointment : Window
    {
        public Appointment appt;
        public bool isAdded = false;
        //static int count = 0;
        bool isValid;

        public AddAppointment()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            appt = new Appointment();
            appt.FirstName = txtName.Text;
            appt.LastName = txtSurname.Text;
            if (radioYev.IsChecked == true)
            {
                appt.AppointmentWith = "Yev";
            }
            else if (radioOlivier.IsChecked == true)
            {
                appt.AppointmentWith = "Olivier";
            }
            else if (radioCallback.IsChecked == true)
            {
                appt.AppointmentWith = "Callbacks";
            }

            if (radioHalfHour.IsChecked == true)
            {
                appt.Duration = 30;
            }
            else if (radioHour.IsChecked == true)
            {
                appt.Duration = 60;
            }

            try
            {
                ComboBoxItem typeItem = (ComboBoxItem)timeSelect.SelectedItem;
                appt.Time = typeItem.Content.ToString();
                if (CheckIfBlockIsEmpty())
                {
                    throw new OverlapException();
                }
                ((MainWindow)Application.Current.MainWindow).appointments.Add(appt);
                ((MainWindow)Application.Current.MainWindow).AppointmentAdded();
                isValid = true;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Please fill in all appropriate fields");
                isValid = false;
            }
            catch (OverlapException)
            {
                MessageBox.Show("An appointment is already booked for this time slot");
                isValid = false;
            }
            if (isValid) {
                isAdded = true;
                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private bool CheckIfBlockIsEmpty()
        {
            //-----------This checks if another appointment is already booked in this slot-----------
            bool ret = false;   // empty
            foreach(Appointment a in ((MainWindow)Application.Current.MainWindow).appointments)
            {
                if (a.AppointmentWith == appt.AppointmentWith && a.Time == appt.Time)
                {
                    ret = true; // not empty
                }
            }
            return ret;
        }
    }
}
