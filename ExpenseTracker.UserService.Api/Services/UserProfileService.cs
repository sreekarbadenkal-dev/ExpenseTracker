using ExpenseTracker.UserService.Api.Data;
using ExpenseTracker.UserService.Api.DTOs.RequestDTOs;
using ExpenseTracker.UserService.Api.DTOs.ResponseDTOs;
using ExpenseTracker.UserService.Api.Entities;
using ExpenseTracker.UserService.Api.Exceptions;
using ExpenseTracker.UserService.Api.Services.Media;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.UserService.Api
{
    public class UserProfileService(UserServiceDbContext context, IMediaService mediaService)
    {
        private readonly UserServiceDbContext _context = context;
        private readonly IMediaService _mediaService = mediaService;

        public async Task<ProfileResDto> getProfile(Guid userId)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);

            if (profile == null)
            {

                throw new UserServiceException(
                    statusCode: StatusCodes.Status400BadRequest,
                    message: $"User with {userId} does not have a profile",
                    resultCode: 1
                    );
            }

            ProfileResDto profileResDto = new ProfileResDto
            {
                UserId = profile.UserId,
                UserName = profile.UserName,
                Description = profile.Description ?? "",
                ProfilePictureUrl = profile.ProfilePicture ?? "",
                DateOfBirth = profile.DateOfBirth.ToString() == null ? "" : profile.DateOfBirth.ToString(),
            };

            return profileResDto;
        }

        public async Task<string> UploadProfilePicture(ProfilePictureDto profilePicture)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(profile => profile.UserId == profilePicture.UserId);

            if (profile == null)
            {
                throw new UserServiceException(
                    statusCode: StatusCodes.Status400BadRequest,
                    message: $"Profile with userId {profilePicture.UserId} not found",
                    resultCode: 0
                    );
            }

            var uploadResult = await _mediaService.UploadImageAsync(profilePicture.File, "expense-tracker/profiles");

            profile.ProfilePicture = uploadResult.Url;
            await _context.SaveChangesAsync();

            return uploadResult.Url;
        }

        public async Task<bool> CreateProfile(ProfileReqDto req)
        {
            try
            {
                var profile = await _context.Profiles.FirstOrDefaultAsync(profile => profile.UserId == req.UserId);


                if (profile == null)
                {
                    profile = new UserProfile();
                    profile.UserId = req.UserId;    
                    _context.Profiles.Add(profile);
                }

                profile.UserName = req.UserName;
                profile.DateOfBirth = req.DateOfBirth;
                profile.Description = req.Description;

                if (req.Description != null)
                    profile.Description = req.Description;


                if (req.ProfilePicture != null)
                {
                    var result = await _mediaService.UploadImageAsync(req.ProfilePicture, "expense-tracker/profiles");
                    profile.ProfilePicture = result.Url;
                }


                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
    }
}