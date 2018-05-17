using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Appointment
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Duration { get; set; } //duration of the appointment - 30 or 60 minutes
        public string AppointmentWith { get; set; } //who is the appointment with? Yev, Olivier, or a callback?
        public string Time { get; set; }//to use to figure out where to put the person on the schedule (ie.which block?) / needs to be a string, will be compared to labels on the schedule
        public int ColumnPos { get; set; }
        public int RowPos { get; set; }
    }
}
