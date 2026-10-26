namespace ExpenseTracker.DeviceConsentService.Api.Entities
{
    public class Device
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string DeviceIdentifier { get; set; } = string.Empty;

        public string DeviceType { get; set; } = string.Empty;

        public string? DeviceModel { get; set; }

        public string? OsModel { get; set; }

        public string? AppVersion { get; set; }

        public string? PushNotificationToken { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime LastActiveAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        // Navigation Property
        public ICollection<ConnectedSource> ConnectedSources { get; set; } = new List<ConnectedSource>();
    }
}
