namespace Barbershop.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDateTime { get; set; }

        public int BarberId { get; set; }

        // child ref
        public List<Barber> Barber { get; set; }
    }
}
