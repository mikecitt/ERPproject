using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrgovinskaRadnja.Domain.Core.Repositories;
using TrgovinskaRadnja.Domain.Core.Services;
using TrgovinskaRadnja.Domain.Dtos;

namespace TrgovinskaRadnja.Services.Services
{
    public class UserService : BaseService<SiteUserDto>, IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<SiteUserDto> GetAccountWithEmailAsync(string email)
        {
            return await _userRepository.GetUserWithEmailAsync(email);
        }

        public async Task<string> GetUserRoleAsString(int userId)
        {
            SiteUserDto userDto = await _userRepository.GetByIdAsync<SiteUserDto>(userId);
            return userDto.Role.ToString();
        }

        public async Task<SiteUserDto> GetUserWithEmailAsync(string email)
        {
            return await _userRepository.GetUserWithEmailAsync(email);
        }

        public async Task RegisterAsync(SiteUserDto userDto)
        {
            userDto.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);


            SiteUserDto user = await _userRepository.GetUserWithEmailAsync(userDto.Email);
            if (user != null)
            {
                throw new KeyNotFoundException();
            }

            await _userRepository.CreateAsync(userDto);
        }

    }
}
