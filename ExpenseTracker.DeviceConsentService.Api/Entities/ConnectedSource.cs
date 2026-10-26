namespace ExpenseTracker.DeviceConsentService.Api.Entities
{
    public class ConnectedSource
    {
        public Guid Id { get; set; }

        public Guid DeviceId { get; set; }

        public string SourceType { get; set; } = string.Empty;

        public string? SourceIdentifier { get; set; }

        public string Status { get; set; } = "ACTIVE";

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        // Navigation Properties
        public Device Device { get; set; } = null!;

        public ICollection<Consent> Consents { get; set; } = new List<Consent>();

        public ICollection<SyncSession> SyncSessions { get; set; } = new List<SyncSession>();
    }
}
