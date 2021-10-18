using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string CommentDetails { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        
        
    }
}