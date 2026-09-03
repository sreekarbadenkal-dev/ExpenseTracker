using ExpenseTracker.UserService.Api.Data;
using ExpenseTracker.UserService.Api.Entities;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.UserService.Api.DTOs.ResponseDTOs;
using ExpenseTracker.UserService.Api.DTOs.RequestDTOs;
using ExpenseTracker.UserService.Api.Exceptions;

namespace ExpenseTracker.UserService.Api.Services
{
    public class UserPreferenceService(UserServiceDbContext context)
    {
        private readonly UserServiceDbContext _context = context;

        public async Task<PrefereceResDto?> GetPreferenceAsync(Guid guid)
        {
            var preference = await _context.Preferences.FirstOrDefaultAsync(pref => pref.UserId == guid);

            if (preference == null)
            {
                return null;
            }

            return new PrefereceResDto
            {
                Id = preference.UserId.ToString(),
                DefaultCurrency = preference.DefaultCurrency,
                TimeZone = preference.TimeZone,
                Language = preference.Language,
                DateFormat = preference.DateFormat,
            };
        }

        public async Task<PrefereceResDto> UpdatePreferenceAsync(Guid guid, PreferenceReqDto req)
        {
            var profile = await _context.Profiles.AnyAsync(profile => profile.UserId == guid);

            if (!profile)
                throw new UserServiceException(
                    statusCode: StatusCodes.Status404NotFound,
                    message: $"Cannot Set a preference userId with {guid} does not exsist",
                    resultCode: 0
                    );

            var preference = await _context.Preferences.FirstOrDefaultAsync(pref=>pref.UserId == guid);

            if(preference == null)
            {
               
                    
                preference = new UserPreferences
                {
                    UserId = guid,
                    DefaultCurrency = req.DefaultCurrency,
                    TimeZone = req.TimeZone,    
                    Language = req.Language,    
                    DateFormat = req.DateFormat,    
                };

                _context.Preferences.Add(preference);
            }
            else
            {
                preference.DefaultCurrency = req.DefaultCurrency;
                preference.TimeZone = req.TimeZone;
                preference.Language = req.Language;
                preference.DateFormat = req.DateFormat;
            }

            await _context.SaveChangesAsync();

            return new PrefereceResDto
            {
                Id = preference.UserId.ToString(),
                DefaultCurrency = preference.DefaultCurrency,
                TimeZone = preference.TimeZone,
                Language = preference.Language,
                DateFormat = preference.DateFormat,
            };
        }
    }

}
