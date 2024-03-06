using System.Web;
using IdentityLearning.API.Extensions;
using IdentityLearning.API.Filters.AccessTokenBlacklistAuthorize;
using IdentityLearning.API.ModelBinders;
using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;
using IdentityLearning.Application.DTOs.Identity.Token.DTOs;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.Features.Sessions.Commands.DeleteUserSession;
using IdentityLearning.Application.Features.Sessions.Queries.GetUserSessions;
using IdentityLearning.Application.Features.Sessions.Queries.IsSessionContainsAccessToken;
using IdentityLearning.Application.Features.Token.Commands.RefreshAccessToken;
using IdentityLearning.Application.Features.Token.Queries.GetUserIdFromAccessToken;
using IdentityLearning.Application.Features.User.Commands.ConfirmDeleteUser;
using IdentityLearning.Application.Features.User.Commands.ConfirmEmail;
using IdentityLearning.Application.Features.User.Commands.DeleteUser;
using IdentityLearning.Application.Features.User.Commands.Login;
using IdentityLearning.Application.Features.User.Commands.Register;
using IdentityLearning.Application.Features.User.Commands.UpdatePassword;
using IdentityLearning.Application.Features.User.Commands.UpdateProfile;
using IdentityLearning.Application.Features.User.Queries.GetProfile;
using IdentityLearning.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IdentityLearning.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            var registerCommand = new RegisterCommand(registerDto);
            var registerResult = await _mediator.Send(registerCommand);

            return registerResult.IsSuccessfull
                ? Ok(registerResult.Body)
                : registerResult.ToErrorActionResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var loginCommand = new LoginCommand(loginDto);
            var loginResult = await _mediator.Send(loginCommand);

            return loginResult.IsSuccessfull
                ? Ok(loginResult.Body)
                : loginResult.ToErrorActionResult();
        }

        [ExtendedAuthorize]
        [HttpGet("sessions")]
        public async Task<IActionResult> GetSessions([ModelBinder<UserIdModelBinder>] long userId)
        {
            var getUserSessionsResult = await _mediator.Send(new GetUserSessionsQuery(userId));

            return getUserSessionsResult.IsSuccessfull
                ? Ok(getUserSessionsResult.Body)
                : getUserSessionsResult.ToErrorActionResult();
        }

        [ExtendedAuthorize]
        [HttpDelete("sessions/{sessionId:long}")]
        public async Task<IActionResult> DeleteSession(long sessionId, [ModelBinder<UserIdModelBinder>] long userId)
        {
            var userClaims = HttpContext.User.Claims.ToArray();

            var isSessionContainsAccessTokenDto = new IsSessionContainsAccessTokenDto()
            {
                AccessTokenClaims = userClaims,
                SessionId = sessionId
            };

            var isSessionContainsAccessTokenResult =
                await _mediator.Send(new IsSessionContainsAccessTokenQuery(isSessionContainsAccessTokenDto));
            if (isSessionContainsAccessTokenResult.IsSuccessfull == false)
            {
                return isSessionContainsAccessTokenResult.ToErrorActionResult();
            }

            if (isSessionContainsAccessTokenResult.Body)
            {
                return Result<bool>
                    .NotSuccessfull("User can't delete the session in which he is running", TotalErrorCode.Forbidden)
                    .ToErrorActionResult();
            }

            var deleteUserSessionDto = new DeleteUserSessionDto()
            {
                UserId = userId,
                SessionId = sessionId
            };

            var deleteUserSessionsResult = await _mediator.Send(new DeleteUserSessionCommand(deleteUserSessionDto));

            return deleteUserSessionsResult.IsSuccessfull
                ? NoContent()
                : deleteUserSessionsResult.ToErrorActionResult();
        }

        [ExtendedAuthorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileDto updateProfileDto, [ModelBinder<UserIdModelBinder>] long userId)
        {
            var updateProfileResult = await _mediator.Send(new UpdateProfileCommand(userId, updateProfileDto));

            return updateProfileResult.IsSuccessfull
                ? NoContent()
                : updateProfileResult.ToErrorActionResult();
        }

        [ExtendedAuthorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile([ModelBinder<UserIdModelBinder>] long userId)
        {
            var getProfileResult = await _mediator.Send(new GetProfileCommand(userId));

            return getProfileResult.IsSuccessfull
                ? Ok(getProfileResult.Body)
                : getProfileResult.ToErrorActionResult();
        }


        [HttpPut("refreshAccessToken")]
        public async Task<IActionResult> RefreshAccessToken(RefreshAccessTokenDto refreshAccessTokenDto)
        {
            var refreshAccessTokenCommand = new RefreshAccessTokenCommand(refreshAccessTokenDto);
            var refreshAccessTokenResult = await _mediator.Send(refreshAccessTokenCommand);

            return refreshAccessTokenResult.IsSuccessfull
                ? Ok(refreshAccessTokenResult.Body)
                : refreshAccessTokenResult.ToErrorActionResult();
        }

        [HttpPost("confirmEmail")]
        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] long userId, [FromQuery] string code)
        {
            var confirmEmailDto = new ConfirmEmailDto()
            {
                ConfirmationCode = code,
            };

            var confirmEmailResult = await _mediator.Send(new ConfirmEmailCommand(userId, confirmEmailDto));

            return confirmEmailResult.IsSuccessfull
                ? Ok("Email confirmed")
                : confirmEmailResult.ToErrorActionResult();
        }

        [ExtendedAuthorize]
        [HttpPut("updatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordDto updatePasswordDto, [ModelBinder<UserIdModelBinder>] long userId)
        {
            var updatePasswordCommand = new UpdatePasswordCommand(userId, updatePasswordDto);
            var updatePasswordResult = await _mediator.Send(updatePasswordCommand);

            return updatePasswordResult.IsSuccessfull
                ? NoContent()
                : updatePasswordResult.ToErrorActionResult();
        }

        [ExtendedAuthorize]
        [HttpPut("delete")]
        public async Task<IActionResult> DeleteAccount([ModelBinder<UserIdModelBinder>] long userId)
        {
            var deleteUserCommand = new DeleteUserCommand(userId);
            var deleteUserResult = await _mediator.Send(deleteUserCommand);

            return deleteUserResult.IsSuccessfull
                ? NoContent()
                : deleteUserResult.ToErrorActionResult();
        }

        [HttpGet("confirmDelete")]
        [HttpDelete("confirmDelete")]
        public async Task<IActionResult> ConfirmAccountDelete([FromQuery] long userId, [FromQuery] string code)
        {
            var confirmAccountDeleteCommand = new ConfirmDeleteUserCommand(code, userId);
            var confirmAccountDeleteResult = await _mediator.Send(confirmAccountDeleteCommand);

            return confirmAccountDeleteResult.IsSuccessfull
                ? NoContent()
                : confirmAccountDeleteResult.ToErrorActionResult();
        }
    }
}
