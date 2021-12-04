using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOS
{
    public class MemberDto
    {

         public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PostUrl { get; set; }
        public string Role { get; set; }
        public string Sroleone { get; set; }
        public string  Sroletwo { get; set; }
        public string Buname { get; set; }
       
        public DateTime Dob { get; set; }
        public ICollection<PostDto> Posts { get; set; }    
        
    }
}