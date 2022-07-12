using AutoMapper;
using System.Collections.Generic;
using WebBackend.Dto;
using WebBackend.Infrastructure;
using WebBackend.Interfaces;
using WebBackend.Models;

namespace WebBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DeliveryContext _deliveryContext;

        public UserService(IMapper mapper, DeliveryContext deliveryContext)
        {
            _mapper = mapper;
            _deliveryContext = deliveryContext;
        }
        public RegisterDto ChangeUser(RegisterDto newUser)
        {
            User user = _deliveryContext.Users.Find(newUser.Id);

            user.Name = newUser.Name;
            user.Email = newUser.Email;
            user.Password = newUser.Password;
            user.FullName = newUser.FullName;
            user.Address = newUser.Address;
            user.ImageURI = newUser.ImageURI;

            _deliveryContext.SaveChanges();
            return _mapper.Map<RegisterDto>(user);
        }

        public RegisterDto CreateUser(RegisterDto newUser)
        {
            User user = _mapper.Map<User>(newUser);
            if(user.UserType != UserType.Postal)
            {
                user.IsVerified = true;
                user.VerificationProgress = VerificationType.Accepted;
            }
            _deliveryContext.Users.Add(user);
            _deliveryContext.SaveChanges();
            return _mapper.Map<RegisterDto>(user);
        }

        public RegisterDto GetUser(int id)
        {
            return _mapper.Map<RegisterDto>(_deliveryContext.Users.Find(id));
        }

        public bool Login(LoginDto user)
        {
            List<User> users = _mapper.Map<List<User>>(_deliveryContext.Users);
            User databaseEntry = users.Find(x=>x.Email.Equals(user.Email));
            if(databaseEntry.Password.Equals(user.Password))
                return true;
            return false;
        }

        public void VerifyPostal(int id, bool yes)
        {
            User user = _deliveryContext.Users.Find(id);
            if(yes)
            {
                user.VerificationProgress = VerificationType.Accepted;
                user.IsVerified = true;
                _deliveryContext.SaveChanges();
                return;
            }    
            user.VerificationProgress = VerificationType.Declined;
            _deliveryContext.SaveChanges();
        }
    }
}
