namespace ExpenseTracker.DeviceConsentService.Api.Entities
{
    public class SyncSession
    {
        public Guid Id { get; set; }

        public Guid ConnectedSourceId { get; set; }

        public string Status { get; set; } = "IN_PROGRESS";

        public DateTime StartedAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public DateTime LastSyncedWaterMark { get; set; }

        public int RecordReadCount { get; set; }

        public int RecordProcessCount { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation Property
        public ConnectedSource ConnectedSource { get; set; } = null!;
    }
}
