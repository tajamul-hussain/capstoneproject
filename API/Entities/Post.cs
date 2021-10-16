using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("Posts")]
    public class Post
    {
        public int Id { get; set; }
        public string Url { get; set; }
        
        public int Likes { get; set; }=0;
        public int PublicId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        
    }
}