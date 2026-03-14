using FoodDelivery.Domain.Enums;
using NetTopologySuite.Geometries;
using System;

namespace FoodDelivery.Domain.Entities
{
    public sealed class Address
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Street { get; private set; } = string.Empty;
        public string PostalCode { get; private set; } = string.Empty;
        public string AppartmentNumber { get; private set; } = string.Empty;
        public AddressLabel Label { get; private set; }
        public Point Location { get; private set; } = null!;
        public bool IsDefault { get; private set; }
        public DateTime CreateAt { get; private set; }

        public User? User { get; private set; }

        private Address() { }

        public Address(Guid userId, string street, string postalCode, string appartmentNumber, double lat, double lng, AddressLabel label = AddressLabel.Home)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Street = street;
            PostalCode = postalCode;
            AppartmentNumber = appartmentNumber;
            Label = label;

            Location = new Point(lng, lat) { SRID = 4326 };
            IsDefault = false;
            CreateAt = DateTime.UtcNow;
        }

        public void SetDefault() => IsDefault = true;
        public void UnsetDefault() => IsDefault = false;

        public void Update(string? street = null, string? postalCode = null, string? appartmentNumber = null, double? lat = null, double? lng = null, AddressLabel? label = null)
        {
            if (!string.IsNullOrWhiteSpace(street)) Street = street;
            if (!string.IsNullOrWhiteSpace(postalCode)) PostalCode = postalCode;
            if (!string.IsNullOrWhiteSpace(appartmentNumber)) AppartmentNumber = appartmentNumber;
            if (label.HasValue) Label = label.Value;
            if (lat.HasValue && lng.HasValue)
                Location = new Point(lng.Value, lat.Value) { SRID = 4326 };
        }
    }
}
