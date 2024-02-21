namespace Barbershop.Models
{
    public class Haircut
    {
        public int HaircutId { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }

        public List<Appointment>? Appointments { get; set; }


    }
}
