using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext:DbContext
    {
        public DataContext( DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Recognise> Recognization { get; set; }
        public DbSet<Post> Posts {get; set; }
        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
       
        protected override void OnModelCreating (ModelBuilder builder){
            base.OnModelCreating(builder);
            builder.Entity<UserLike>().HasKey(k=> new {k.SourceUserId,k.LikedUserId});



            builder.Entity<UserLike>().
            HasOne(s=>s.SourceUser).WithMany(l=>l.LikedUsers).
            HasForeignKey(s=>s.SourceUserId).OnDelete(DeleteBehavior.Cascade);



            
            builder.Entity<UserLike>().
            HasOne(s=>s.LikedUser).WithMany(l=>l.LikedByUsers).
            HasForeignKey(s=>s.LikedUserId).OnDelete(DeleteBehavior.Cascade);


        }
        
    }
}