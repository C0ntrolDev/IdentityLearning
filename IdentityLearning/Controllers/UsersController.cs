using IdentityLearning.API.Extensions;
using IdentityLearning.API.Filters.AccessTokenBlacklistAuthorize;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.Features.Acccount.Commands.DeleteUser;
using IdentityLearning.Application.Features.User.Commands.Create;
using IdentityLearning.Application.Features.User.Commands.DeleteAll;
using IdentityLearning.Application.Features.User.Commands.Update;
using IdentityLearning.Application.Features.User.Queries.Get;
using IdentityLearning.Application.Features.User.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityLearning.API.Controllers
{
    [ExtendedAuthorize(Roles = "Admin,Moderator")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto user)
        {
            var command = new CreateUserCommand(user);
            var createResult = await _mediator.Send(command);

            return createResult.IsSuccessfull
                ? NoContent()
                : createResult.ToErrorActionResult();

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAllUsersQuery();
            var getAllResult = await _mediator.Send(command);

            return getAllResult.IsSuccessfull
                ? Ok(getAllResult.Body)
                : getAllResult.ToErrorActionResult();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(long userId)
        {
            var command = new GetUserQuery(userId);
            var getResult = await _mediator.Send(command);

            return getResult.IsSuccessfull
                ? Ok(getResult.Body)
                : getResult.ToErrorActionResult();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Update(long userId, UserDto user)
        {
            var command = new UpdateUserCommand(userId, user);
            var updateResult = await _mediator.Send(command);

            return updateResult.IsSuccessfull
                ? NoContent()
                : updateResult.ToErrorActionResult();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(long userId)
        {
            var command = new DeleteUserCommand(userId);
            var deleteResult = await _mediator.Send(command);

            return deleteResult.IsSuccessfull
                ? NoContent()
                : deleteResult.ToErrorActionResult();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            var command = new DeleteAllUsersCommand();
            var deleteAllCommand = await _mediator.Send(command);

            return deleteAllCommand.IsSuccessfull
                ? NoContent()
                : deleteAllCommand.ToErrorActionResult();
        }
    }
}
