using Application.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersControllerONHOLD : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersControllerONHOLD(IMediator mediator)
        {
            this.mediator = mediator;
        }


        // On hold => Not sure if needed for the moment
        //[HttpPost]
        //public async Task<IActionResult> Register([FromBody] RegisterViewModel registerVm)
        //{
        //    var result = await mediator.Send(new RegisterUserCommand())
        //}
    }
}