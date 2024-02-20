using System.ComponentModel.DataAnnotations;

namespace Barbershop.Models
{
    public class Barber
    {

        public int BarberId { get; set; }

        public string? BarberName { get; set; }

        // child ref
        public List<Appointment>? Appointments { get; set; }
       // public Appointment Appointment { get; set; }
    }
}
