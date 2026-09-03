
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ExpenseTracker.UserService.Api.Exceptions;
using Microsoft.Extensions.Options;

namespace ExpenseTracker.UserService.Api.Services.Media
{
    public class CloudinaryMediaService : IMediaService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryMediaService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary( account );
        }


        public async Task<MediaUploadResult> UploadImageAsync(IFormFile file, string folderName)
        {
            if (file.Length == 0)
            {
                throw new UserServiceException(
                    statusCode: StatusCodes.Status400BadRequest,
                    message: $"File is empty",
                    resultCode: 0
                    );
            }

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folderName,
                Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
            };


            var uploadResult = await _cloudinary.UploadAsync( uploadParams );

            if (uploadResult.Error != null)
            {
                throw new UserServiceException(
                    statusCode: StatusCodes.Status500InternalServerError,
                    message: $"Image could not be uploaded, please try again later"
                    );
            }

            return new MediaUploadResult
                {
                Url = uploadResult.SecureUrl.ToString(),
                publicId = uploadResult.PublicId
                };
        }

        public async Task<bool> DeleteImageAsync(string url, string publicId)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(publicId))
                return false;

            var deletedParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletedParams);

            if (result.Error != null)
            {
                return false;
            }

            return true;
        }
    }
}
