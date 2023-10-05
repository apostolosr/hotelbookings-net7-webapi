using System.ComponentModel.DataAnnotations;

namespace HotelBookings.Models.Bookings
{
    /// <summary>
    /// Model for the request of creating a hotel
    /// </summary>
    public class CreateBookingModel
    {
        /// <summary>
        /// Id of the hotel the booking is for
        /// </summary>
        [Required]
        public required int HotelId { get; set; }

        /// <summary>
        /// Name of the customer
        /// </summary>
        [Required]
        public required string CustomerName { get; set; }

        /// <summary>
        /// Number of PAX (people)
        /// </summary>
        [Range(1, 20, ErrorMessage = "There should be at least one person; can't exceed 20")]
        public int Pax { get; set; }
    }
}

