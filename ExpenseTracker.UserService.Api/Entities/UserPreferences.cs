using Microsoft.VisualBasic;
using System.Runtime.Serialization;

namespace ExpenseTracker.UserService.Api.Entities
{
    public enum DateFormatOptions
    {
        DdMmYyyy=0,
        MmDdYyyy=1,
        YyyyDdMm=2,
    }
    public class UserPreferences
    {
        public Guid UserId { get; set; }
        public string DefaultCurrency {  get; set; }
        public string TimeZone { get; set; }
        public DateFormatOptions DateFormat { get; set; } = DateFormatOptions.DdMmYyyy;
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public string Language { get; set; }
        public UserProfile UserProfile { get; set; } = null!;
    }
}
