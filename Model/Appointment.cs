using System;

namespace Model
{
    public class Appointment
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Duration { get; set; } //duration of the appointment - 30 or 60 minutes
        public string AppointmentWith { get; set; } //who is the appointment with? Yev, Olivier, or a callback?
        public double Time { get; set; }//to use to figure out where to put the person on the schedule
    }
}
