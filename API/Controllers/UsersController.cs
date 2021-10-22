using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOS;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;

        }




        [HttpGet]
        [Authorize]

        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            return Ok(usersToReturn);

        }

        [HttpGet]
        [Route("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            var userToReturn = _mapper.Map<MemberDto>(user);
            return Ok(userToReturn);
        }
        [HttpGet]
        [Route("id/{id}")]
        public async Task<ActionResult<MemberDto>> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var userToReturn = _mapper.Map<MemberDto>(user);
            return (userToReturn);
        }
        
     
      
        [HttpPost("add-photo/{username}")]
        public async Task<ActionResult<PostDto>> AddPost(IFormFile file,string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
           if(user==null)return BadRequest("no user");
            var result = await _photoService.AddPhotoAsync(file);
            if(result.Error!=null){
                return BadRequest(result.Error.Message);
            }
            
            var post=new Post{
                Url=result.SecureUrl.AbsoluteUri,
                PublicId=result.PublicId
            };
            if(user.Posts.Count==0)
            {
                post.IsMain=true;
            }
            user.Posts.Add(post);
            if(await _userRepository.SaveAllAsync()){
                return _mapper.Map<PostDto>(post);
            }
            return BadRequest("Problem in adding photo");

        }
        [HttpDelete]
        [Route("delete-photo/{username}/{photoId}")]
        public async Task<ActionResult> DeletePost(string username,int photoId)
        {
            var user= await _userRepository.GetUserByUsernameAsync(username);
            var post=user.Posts.FirstOrDefault(x=>x.Id==photoId);
            if(post==null) return NotFound();
            if(post.IsMain) return BadRequest("You cannnot delet main photo");
            if(post.PublicId!=null){
                var result= await _photoService.DeletePhotoAsync(post.PublicId);
                if(result.Error!=null) return BadRequest(result.Error.Message);
            }
            user.Posts.Remove(post);
            if(await _userRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to delete");
        }

    }
}