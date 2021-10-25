using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.DTOS;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers

{
   [Authorize]
    public class LikesController:BaseApiController
    {
        private readonly ILikesRepository _likesRepository;
        private readonly IUserRepository _userRepository;
        public LikesController(IUserRepository userRepository,ILikesRepository likesRepository)
        {
            _userRepository = userRepository;
            _likesRepository = likesRepository;
            
        }
        // [HttpPost]
        // [Route("{username}/{sourceusername}")]
        // public async Task<ActionResult> AddLike(string username,string sourceusername){
        //     var sourceUser= await _userRepository.GetUserByUsernameAsync(sourceusername);
        //     var sourceUserId=sourceUser.Id;
        //    var likedUser=await _userRepository.GetUserByUsernameAsync(username);
        //    var SourceUser=await _likesRepository.GetUserWithLikes(sourceUserId);
        //    if(likedUser==null) return NotFound("user Not found");
        //    if(SourceUser.UserName==username) return BadRequest("Cant like your self");
        //    var userLike=await _likesRepository.GetUserLike(sourceUserId,likedUser.Id);
        //    if(userLike!=null) return BadRequest("You already Liked the User");
        //    userLike=new UserLike(){
        //        SourceUserId=sourceUserId,
        //        LikedUserId=likedUser.Id
        //    };
        //    SourceUser.LikedUsers.Add(userLike);
        //    if(await _userRepository.SaveAllAsync()) return Ok();
        //    return BadRequest("Failed To Follow");
            
        // }
        // [HttpGet]
    
        // public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate,string sourceUsername)
        // {
        //     var sourceUser= await _userRepository.GetUserByUsernameAsync(sourceUsername);
        //     var id=sourceUser.Id;
        //     var UserId=id;
        //     var users= await _likesRepository.GetUserLikes(predicate,UserId);
        //     return Ok(users);
        // }

        //claims principle
        // [HttpPost]
        // [Route("{username}")]
        // public async Task<ActionResult> AddLike(string username)
        // {


        // }

        [HttpPost]
        [Route("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
                 
            var loggedInUserEmail= HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var sourceUser=_userRepository.GetUserByUseremailAsync(loggedInUserEmail);
            var sourceUserId=sourceUser.Result.Id;
            var likedUser= await _userRepository.GetUserByUsernameAsync(username);
            var SourceUser= await _likesRepository.GetUserWithLikes(sourceUserId);
            if(likedUser==null) return NotFound();

            if(SourceUser.UserName==username) return BadRequest("Cannot Follow yourself");

            var userLike=await _likesRepository.GetUserLike(sourceUserId,likedUser.Id);
            if(userLike!=null) return BadRequest("You Already Followed this user");
            userLike=new UserLike(){
                SourceUserId=sourceUserId,
                LikedUserId=likedUser.Id
            };
            SourceUser.LikedUsers.Add(userLike);
            if(await _userRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to Follow");   

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
        {
           var loggedInUserEmail=HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
           var loggedInUser= await _userRepository.GetUserByUseremailAsync(loggedInUserEmail);
           var users= await _likesRepository.GetUserLikes(predicate,loggedInUser.Id);
           return Ok(users);
        }




         
        
    }
}