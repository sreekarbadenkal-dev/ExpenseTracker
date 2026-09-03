using ExpenseTracker.UserService.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.UserService.Api.DTOs.RequestDTOs;

    public class PreferenceReqDto
    {
        [Required(ErrorMessage = "DefaultCurrency is required.")]
        public string DefaultCurrency { get; set; } = "INR";
        [Required(ErrorMessage = "TimeZone is required.")]
        public string TimeZone { get; set; } = "Asia/Kolkata";
        [Required(ErrorMessage = "Language is required.")]
        public string Language { get; set; } = "en-IN";
        [Required(ErrorMessage = "DateFormat is required.")]
        public DateFormatOptions DateFormat { get; set; } = DateFormatOptions.DdMmYyyy;
    }
