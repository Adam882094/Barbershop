using System.ComponentModel.DataAnnotations;

namespace Barbershop.Models
{
    public class Barber
    {

        public int BarberId { get; set; }
        public string BarberName { get; set; }

        // parent ref
        public Appointment Appointment { get; set; }
    }
}
