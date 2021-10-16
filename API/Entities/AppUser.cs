using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime Dob { get; set; }
        public ICollection<Post>  Posts { get; set; }
        public byte[]  PasswordHash { get; set; }
        public byte [] PasswordSalt { get; set; }
    }
}