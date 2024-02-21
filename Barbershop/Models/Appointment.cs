namespace Barbershop.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDateTime { get; set; }

        //If doing a dropdown menu of set times this is uncessary
        //public DateTime AppointmentEndTime { get; set; }

        public int BarberId { get; set; }

        public int HaircutId { get; set; }

        public string? CustomerId { get; set; }

        //Parent Ref
        public Barber? Barber { get; set; }
        public Haircut? Haircut { get; set; }
    }
}
