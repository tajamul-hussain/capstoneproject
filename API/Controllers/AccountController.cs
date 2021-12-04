using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.DTOS;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context,ITokenService tokenService)
        {
            _context = context;
            _tokenService=tokenService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)

        {
            if( await UserExsists(registerDto.Email)) return new BadRequestObjectResult("User Already Exsists try to login");
           
           
            using var hmac=new HMACSHA512();
            var user= new AppUser
            {
                Buname=registerDto.Buname,
                Role=registerDto.Role,
                UserName=registerDto.Username,
                Dob=registerDto.Dob,
                Sroleone=registerDto.Sroleone,
                Sroletwo=registerDto.Sroletwo,
                Email=registerDto.Email.ToLower(),
                PasswordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt=hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto{
                UserName=user.UserName,
                Email=user.Email,
                Role=user.Role,
                Sroleone=user.Sroleone,
                Sroletwo=user.Sroletwo,
                Buname=user.Buname,
                Token=_tokenService.CreateToken(user)
            };
            
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user=await _context.Users.FirstOrDefaultAsync(x=>x.Email==loginDto.Email);
            if(user==null) return new UnauthorizedObjectResult("Invalid user Email or Password");
            using var hmac=new HMACSHA512(user.PasswordSalt);
            var computedHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for(int i=0;i<computedHash.Length;i++)
            {
                if(computedHash[i]!=user.PasswordHash[i]){
                    return new UnauthorizedObjectResult("Invalid password Or Email");
                }
            }
            return new UserDto{
                // Email=user.Email,
                // UserName=user.UserName,
                // Id=user.Id,

                 UserName=user.UserName,
                Email=user.Email,
                Role=user.Role,
                Sroleone=user.Sroleone,
                Sroletwo=user.Sroletwo,
                Buname=user.Buname,





                Token=_tokenService.CreateToken(user),
                
            };

        }







        private async Task<bool> UserExsists(string email)
        {
            return  await _context.Users.AnyAsync(x=>x.Email==email.ToLower());
        }
    }


    
    













}