using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using API.DTOS;
using API.Interfaces;
using API.Entities;
using API.Data;

namespace API.Controllers
{
  
    [Route("[controller]")]
    public class RecognizeController : BaseApiController
    {

        private readonly IUserRepository _userRepository;
             private readonly DataContext _context;

          public RecognizeController(IUserRepository userRepository,DataContext context)
    {
            _userRepository = userRepository;
              _context = context;
        
    }



   

        [HttpPost]
       
        public async Task<ActionResult> Recognise(RecognizeDto recognizeDto)
        {
          
            var loggedInUserEmail =   HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var loggedInUser=_userRepository.GetUserByUseremailAsync(loggedInUserEmail);
            var loggedinUserId=loggedInUser.Id;
            var recognise=new Recognise(){
                LoggedId=loggedinUserId,
                SourceID=recognizeDto.SourceID,
                Points=recognizeDto.Points,
                Comments=recognizeDto.Comments
            };
            
           _context.Recognization.Add(recognise);
            await _context.SaveChangesAsync();
            return Ok(recognise);

        }

    }
}