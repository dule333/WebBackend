using WebBackend.Dto;

namespace WebBackend.Interfaces
{
    public interface IUserService
    {
        RegisterDto ChangeUser(RegisterDto newUser);
        RegisterDto CreateUser(RegisterDto newUser);
        RegisterDto GetUser(int id);
        void VerifyPostal(int id, bool yes);
        bool Login(LoginDto user);
    }
}
