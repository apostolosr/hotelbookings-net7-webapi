using System.ComponentModel.DataAnnotations;

namespace HotelBookings.Models.Hotels
{
    /// <summary>
    /// Model for the request of creating a hotel
    /// </summary>
    public class CreateHotelModel
    {
        /// <summary>
        /// Name of the hotel
        /// </summary>
        [Required]
        public required string Name { get; set; }

        /// <summary>
        /// Address of the hotel
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Rating of the hotel (0 to 5 stars)
        /// </summary>
        [Required]
        [Range(1, 5, ErrorMessage = "Star rating must be between 1 and 5")]
        public int StarRating { get; set; }
    }
}

