using AutoMapper;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebBackend.Configuration;
using WebBackend.Dto;
using WebBackend.Infrastructure;
using WebBackend.Interfaces;
using WebBackend.Models;

namespace WebBackend.Services
{
    public class UserService : IUserService
    {
        private IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly DeliveryContext _deliveryContext;
        private MailSettings _settings;

        public UserService(IMapper mapper, DeliveryContext deliveryContext, IConfiguration config, IOptions<MailSettings> settings)
        {
            _mapper = mapper;
            _deliveryContext = deliveryContext;
            _configuration = config;
            _settings = settings.Value;
        }
        public RegisterDto ChangeUser(RegisterDto newUser)
        {
            User user = _deliveryContext.Users.Find(newUser.Id);

            user.Name = newUser.Name;
            user.Email = newUser.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
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
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _deliveryContext.Users.Add(user);
            _deliveryContext.SaveChanges();
            return _mapper.Map<RegisterDto>(user);
        }

        public RegisterDto GetUser(int id)
        {
            return _mapper.Map<RegisterDto>(_deliveryContext.Users.Find(id));
        }

        public List<PostalDto> GetUsers()
        {
            List<User> users = _mapper.Map<List<User>>(_deliveryContext.Users);
            return _mapper.Map<List<PostalDto>>(users.FindAll(x=>x.UserType == UserType.Postal));
        }

        public string Login(LoginDto user)
        {
            List<User> users = _mapper.Map<List<User>>(_deliveryContext.Users);
            User databaseEntry = users.Find(x=>x.Email.Equals(user.Email));
            if (databaseEntry == null)
            {
                return "";
            }
            List<Claim> claims = new List<Claim>();
            if (databaseEntry.UserType == UserType.User)
                claims.Add(new Claim(ClaimTypes.Role, "customer"));
            if (databaseEntry.UserType == UserType.Admin)
                claims.Add(new Claim(ClaimTypes.Role, "admin"));
            if (databaseEntry.UserType == UserType.Postal)
                claims.Add(new Claim(ClaimTypes.Role, "postal"));

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("SecretKey")));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:44351",
                claims: claims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials:signingCredentials
                );
            tokenOptions.Payload["id"] = databaseEntry.Id;
            string tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            if (BCrypt.Net.BCrypt.Verify(user.Password, databaseEntry.Password))
                return tokenString;
            return "";
        }

        public void VerifyPostal(int id, bool yes)
        {
            User user = _deliveryContext.Users.Find(id);
            if(yes)
            {
                user.VerificationProgress = VerificationType.Accepted;
                user.IsVerified = true;
                SendMail(user.Email, true);
                _deliveryContext.SaveChanges();
                return;
            }    
            user.VerificationProgress = VerificationType.Declined;
            SendMail(user.Email, false);
            _deliveryContext.SaveChanges();
        }

        private void SendMail(string email, bool yes)
        {
            var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress(_settings.DisplayName, _settings.From));
            mail.Sender = new MailboxAddress(_settings.DisplayName, _settings.From);

            mail.To.Add(MailboxAddress.Parse(email));

            var body = new BodyBuilder();
            mail.Subject = "Verification";
            body.HtmlBody = (yes) ? "Accepted" : "Declined";
            mail.Body = body.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_settings.Host, _settings.Port);
            smtp.Authenticate(_settings.UserName, _settings.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
