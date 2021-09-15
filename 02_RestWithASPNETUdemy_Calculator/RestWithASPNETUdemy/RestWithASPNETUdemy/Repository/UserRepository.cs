using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNETUdemy.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MySQLContext _context;
        public UserRepository(MySQLContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => (u.UserName.Equals(user.UserName)) && (u.Password.Equals(pass)));
        }
        public User ValidateCredentials(string username)
        {
            return _context.Users.SingleOrDefault(u => u.UserName.Equals(username));
        }

        public bool RevokeToken(string username)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName.Equals(username));
            if (user is null) return false;
            user.RefreshToken = null;
            _context.SaveChanges();
            return true;
        }

        public User RefreshUserInfo(User user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            
            return user;
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
