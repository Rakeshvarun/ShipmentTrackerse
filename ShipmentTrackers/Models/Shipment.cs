using System.ComponentModel.DataAnnotations;

namespace ShipmentTrackers.Models
{
    public class Shipment
    {
        public int Id { get; set; }

        [Display(Name = "Tracking ID")]
        public string TrackingId { get; set; } = string.Empty;

        [Required, Display(Name = "Sender Name")]
        public string SenderName { get; set; } = string.Empty;

        [Required, Display(Name = "Receiver Name")]
        public string ReceiverName { get; set; } = string.Empty;

        [Required, Display(Name = "Origin City")]
        public string Origin { get; set; } = string.Empty;

        [Required, Display(Name = "Destination City")]
        public string Destination { get; set; } = string.Empty;

        [Display(Name = "Weight (kg)")]
        public double Weight { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending";

        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Updated At")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
