using System.Text.Json.Serialization;

namespace Booking.DAL.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BookingStatus
    {
        Confirmed,
        Cancelled
    }

    public class Constants
    {
    }
}
