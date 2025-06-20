using System.ComponentModel.DataAnnotations;

namespace LivriaBackend.commerce.Domain.Model.ValueObjects
{
    public record Shipping
    {
        [Required]
        [StringLength(255)]
        public string Address { get; init; }

        [Required]
        [StringLength(100)]
        public string City { get; init; } = "Lima Metropolitana"; 

        [Required]
        [StringLength(100)]
        public string District { get; init; }

        [StringLength(500)]
        public string Reference { get; init; } 

        private Shipping() { }

        public Shipping(string address, string district, string reference)
        {
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentNullException(nameof(address), "Address cannot be empty.");
            if (string.IsNullOrWhiteSpace(district)) throw new ArgumentNullException(nameof(district), "District cannot be empty.");

            Address = address;
            District = district;
            Reference = reference;
        }
    }
}