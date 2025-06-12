namespace Booking.BAL.ViewModel.Booking
{
    public class RoomAvailability
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public string Amenities { get; set; }
        public List<TimeBlock> AvailableBlocks { get; set; }
    }

    public class TimeBlock
    {
        public string Start { get; set; }
        public string End { get; set; }
    }
}
