using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AppointmentManager
    {
        private List<Appointment> appointments = new List<Appointment>();

        public void InsertApptToCalendar(string firstname, string lastname, int duration, string withWho, string time)
        {
            appointments.Add(new Appointment() { FirstName = firstname, LastName = lastname, Duration = duration, AppointmentWith = withWho, Time = time });


        }


    }
}
