using System.ComponentModel.DataAnnotations;

namespace HotelBookings.Models.Bookings
{
    /// <summary>
    /// Model for the request of creating a hotel
    /// </summary>
    public class UpdateBookingModel
    {
        /* Probably no meaning in updating a hotel id for a booking already made
            therefore not including this, but keeping it for future reconsideration
        */
        // public required string HotelId { get; set; }

        /// <summary>
        /// Name of the customer
        /// </summary>
        [Required]
        public required string CustomerName { get; set; }

        /// <summary>
        /// Number of PAX (people)
        /// </summary>
        [Required]
        [Range(1, 20, ErrorMessage = "There should be at least one person; can't exceed 20")]
        public int Pax { get; set; }
    }
}

