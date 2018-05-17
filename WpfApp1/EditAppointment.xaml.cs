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
    /// Interaction logic for EditAppointment.xaml
    /// </summary>
    public partial class EditAppointment : Window
    {
        List<Appointment> appointments;
        Appointment appt;
        int index;
        int listItem;
        bool isValid, isAdded = false;
        string startFName;
        string startLName;
        int startDuration;
        string startWith;
        string startTime;

        public EditAppointment(List<Appointment> appointments, int index)
        {
            this.appointments = appointments;
            this.index = index;
            InitializeComponent();

            txtName2.Text = appointments[index].FirstName;
            txtSurname2.Text = appointments[index].LastName;

            //ComboBoxItem typeItem = (ComboBoxItem)timeSelect.SelectedItem;
            //appt.Time = typeItem.Content.ToString();

            GetComboBoxItem();
            GetRadioButtonInfo();
            startFName = appointments[index].FirstName;
            startLName = appointments[index].LastName;
            startDuration = appointments[index].Duration;
            startWith = appointments[index].AppointmentWith;
            startTime = appointments[index].Time;
        }

        private void GetComboBoxItem()
        {
            ComboBoxItem[] items = { Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9, Item10, Item11, Item12, Item13, Item14, Item15, Item16 };
            for (int i = 0; i < 16; i++)
            {
                if (appointments[index].Time == (string)items[i].Content)
                {
                    listItem = i;
                }
            }

            switch (listItem)
            {
                case 0:
                    timeSelect2.SelectedIndex = 0;
                    break;
                case 1:
                    timeSelect2.SelectedIndex = 1;
                    break;
                case 2:
                    timeSelect2.SelectedIndex = 2;
                    break;
                case 3:
                    timeSelect2.SelectedIndex = 3;
                    break;
                case 4:
                    timeSelect2.SelectedIndex = 4;
                    break;
                case 5:
                    timeSelect2.SelectedIndex = 5;
                    break;
                case 6:
                    timeSelect2.SelectedIndex = 6;
                    break;
                case 7:
                    timeSelect2.SelectedIndex = 7;
                    break;
                case 8:
                    timeSelect2.SelectedIndex = 8;
                    break;
                case 9:
                    timeSelect2.SelectedIndex = 9;
                    break;
                case 10:
                    timeSelect2.SelectedIndex = 10;
                    break;
                case 11:
                    timeSelect2.SelectedIndex = 11;
                    break;
                case 12:
                    timeSelect2.SelectedIndex = 12;
                    break;
                case 13:
                    timeSelect2.SelectedIndex = 13;
                    break;
                case 14:
                    timeSelect2.SelectedIndex = 14;
                    break;
                case 15:
                    timeSelect2.SelectedIndex = 15;
                    break;
            }
        }

        private void GetRadioButtonInfo()
        {
            if (appointments[index].AppointmentWith == "Yev")
            {
                radioYev2.IsChecked = true;
            }
            else if (appointments[index].AppointmentWith == "Olivier")
            {
                radioOlivier2.IsChecked = true;
            }
            else if (appointments[index].AppointmentWith == "Callback")
            {
                radioCallback2.IsChecked = true;
            }

            if (appointments[index].Duration == 30)
            {
                radioHalfHour2.IsChecked = true;
            }
            else if (appointments[index].Duration == 60)
            {
                radioHour2.IsChecked = true;
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            // --------------Add updated version-------------------
            appt = new Appointment();
            appt.FirstName = txtName2.Text;
            appt.LastName = txtSurname2.Text;
            if (radioYev2.IsChecked == true)
            {
                appt.AppointmentWith = "Yev";
            }
            else if (radioOlivier2.IsChecked == true)
            {
                appt.AppointmentWith = "Olivier";
            }
            else if (radioCallback2.IsChecked == true)
            {
                appt.AppointmentWith = "Callbacks";
            }

            if (radioHalfHour2.IsChecked == true)
            {
                appt.Duration = 30;
            }
            else if (radioHour2.IsChecked == true)
            {
                appt.Duration = 60;
            }
            
            try
            {
                ComboBoxItem typeItem = (ComboBoxItem)timeSelect2.SelectedItem;
                appt.Time = typeItem.Content.ToString();
                if (CheckIfBlockIsEmpty())
                {
                    throw new OverlapException();
                }
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
            if (isValid)
            {
                //-------------- Remove all old elements associated with the old block------------------
                ((MainWindow)Application.Current.MainWindow).apptBox[index].Visibility = Visibility.Hidden;
                ((MainWindow)Application.Current.MainWindow).editBtns[index].IsEnabled = false;
                ((MainWindow)Application.Current.MainWindow).editBtns[index].Visibility = Visibility.Hidden;
                ((MainWindow)Application.Current.MainWindow).apptBox.RemoveAt(index); // remove current appt from textbox list
                ((MainWindow)Application.Current.MainWindow).appointments.RemoveAt(index); // remove current appt from appointments list
                ((MainWindow)Application.Current.MainWindow).editBtns.RemoveAt(index); //remove button associated with grid position
                IndexShift();
                ((MainWindow)Application.Current.MainWindow).appointments.Add(appt);
                ((MainWindow)Application.Current.MainWindow).AppointmentAdded();
                isAdded = true;

                Close();
            }
        }

         private bool CheckIfBlockIsEmpty()
        {
            //-----------This checks if another appointment is already booked in this slot-----------
            bool ret = false;   // empty
            foreach(Appointment a in ((MainWindow)Application.Current.MainWindow).appointments)
            {
                //if (appt.FirstName != startFName 
                //    || appt.LastName != startLName 
                //    || appt.Duration != startDuration)
                //{
                //    break;
                //}

                if (a.AppointmentWith == appt.AppointmentWith && a.Time == appt.Time)
                {
                    if ((appt.Time != startTime || appt.AppointmentWith != startWith))
                    {
                        ret = true; // not empty
                    }
                }
            }
            return ret;
        }

        private void IndexShift()
        {
            foreach(EditButton e in ((MainWindow)Application.Current.MainWindow).editBtns)
            {
                if (e.Index >= index)
                {
                    e.Index--;
                }
            }
        }
    }
}
