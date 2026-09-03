namespace ExpenseTracker.UserService.Api.Services.Media
{
    public class MediaUploadResult
    {
        public string Url { get; set; } = string.Empty;
        public string publicId { get; set; } = string.Empty;
    }
    public class CloudinarySettings
    {
        public string CloudName { get; set; } = string.Empty;
        public string ApiKey {  get; set; } = string.Empty;
        public string ApiSecret { get; set; } = string.Empty;
    }
    public interface IMediaService
    {
        Task<MediaUploadResult> UploadImageAsync(IFormFile file, string folderName);

        Task<bool> DeleteImageAsync(string url, string publicId);
    }
}
