using System.Collections.Generic;

namespace API.DTOS
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
         public int Likes { get; set; }=0;
         public int AppUserId { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}