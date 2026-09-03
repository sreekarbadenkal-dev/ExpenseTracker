using ExpenseTracker.UserService.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.UserService.Api.DTOs.ResponseDTOs
{
    public class PrefereceResDto
    {
        [Required]
        public string Id { get; set; }  
        [Required]
        public string DefaultCurrency { get; set; } = "INR";
        [Required]
        public string TimeZone { get; set; } = "Asia/Kolkata";
        [Required]
        public string Language { get; set; } = "en-IN";
        [Required]
        public DateFormatOptions DateFormat { get; set; } = DateFormatOptions.DdMmYyyy;
    }
}
