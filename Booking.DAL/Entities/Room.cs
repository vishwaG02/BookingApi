namespace Booking.DAL.Entities
{
    public class Room : BaseEntityClass
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Location { get; set; }

        public int Capacity { get; set; }

        public string Amenities { get; set; }

        public TimeOnly AvailableFrom { get; set; }

        public TimeOnly AvailableTo { get; set; }

        public ICollection<Bookings> Bookings { get; set; } = new List<Bookings>();

    }
}
