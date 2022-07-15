using System.Collections.Generic;
using WebBackend.Dto;

namespace WebBackend.Interfaces
{
    public interface IUserService
    {
        RegisterDto ChangeUser(RegisterDto newUser);
        RegisterDto CreateUser(RegisterDto newUser);
        RegisterDto GetUser(int id);
        List<PostalDto> GetUsers();
        void VerifyPostal(int id, bool yes);
        string Login(LoginDto user);
    }
}
