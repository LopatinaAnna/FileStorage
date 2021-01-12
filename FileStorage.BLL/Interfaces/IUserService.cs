using FileStorage.BLL.DTO;
using FileStorage.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FileStorage.BLL.Interfaces
{
    /// <summary>
    /// IUserService interface
    /// </summary>
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDto userDto);

        Task<ClaimsIdentity> Authenticate(UserDto userDto);

        Task SetInitialData(UserDto adminDto, List<string> roles);

        Task<bool> ChangePassword(UserDto userDto, string newPassword);
    }
}