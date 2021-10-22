using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOS
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string CommentDetails { get; set; }
        public int PostId { get; set; }
        public int AppUserId { get; set; }
        
    }
}