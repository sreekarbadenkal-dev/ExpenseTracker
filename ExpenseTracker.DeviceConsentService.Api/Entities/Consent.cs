namespace ExpenseTracker.DeviceConsentService.Api.Entities
{
    public class Consent
    {
        public Guid Id { get; set; }

        public Guid ConnectedSourceId { get; set; }

        public string PermissionScope { get; set; } = string.Empty;

        public string Status { get; set; } = "GRANTED";

        public DateTime GrantedAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public DateTime? ExpiredAt { get; set; }

        public string? ConsentMetadata { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        // Navigation Property
        public ConnectedSource ConnectedSource { get; set; } = null!;
    }
}
