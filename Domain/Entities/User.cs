using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : BaseEntityId
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Login { get; private set; }
        public string PasswordHash { get; private set; }
        public string Telephone { get; private set; }
        public bool IsEnabled { get; private set; }
        public int UserTypeId { get; private set; }
        public UserType UserType { get; private set; }

        private User() { }

        public User(string name, string email,bool isEnabled, string login, string password, string telephone, UserType userType)
        {
            Name = name;
            Email = email;
            Login = login;
            Telephone = telephone;
            IsEnabled = isEnabled;
            UserType = userType ?? throw new ArgumentNullException(nameof(userType));
            setPassword(password);

        }

        public void setPassword(string password)
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool checkPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }
    }
}
