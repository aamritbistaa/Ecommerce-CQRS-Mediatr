using CQRSApplication.Command.AuthUserCommand;
using CQRSApplication.Context;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CQRSApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly CQRSDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public AuthenticationController(CQRSDbContext dbContext, IConfiguration configuration, IMediator mediator)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] CreateUserCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _mediator.Send(command, cancellationToken);
                Log.Information("User {@Id} created Successfully", response.Id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to create user with information {@request} \n {@message}", command, ex.Message);
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        public class LoginDetails
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDetails credentials, CancellationToken cancellationToken)
        {
            //Search User in the database
            //var userCredential = await _context.UserCredentials.FindAsync(credentials.Email);
            // var list = await _dbContext.UserCredentials.ToListAsync();

            // var data = from item in list
            //            where item.Email == credentials.Email
            //            select item;
            // var userCredential = data.FirstOrDefault();
            // //var userCredential = await _context.UserCredentials.FindAsync(credentials);
            // Todo: Convert password to hash

            var userCredential = await _dbContext.UserCredentials
                            .FirstOrDefaultAsync(uc => uc.Email == credentials.Email, cancellationToken);
            if (userCredential == null || userCredential.Password != credentials.Password)
            {
                Log.Warning("Invalid Credentials: {@credentials}", credentials);
                throw new Exception(message: "Invalid Credentials");
            }
            var userDetail = await _dbContext.Users
                            .FirstOrDefaultAsync(u => u.UserCredentialsId == userCredential.Id, cancellationToken);
            if (userDetail == null)
            {
                Log.Error("Unable to find user with user credential id: {@CredentialId}}", userCredential.Id);
                throw new Exception(message: "User does not exist");
            }
            //Create JWT token handler and get secret key
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);
            //preparing list of user claims
            //role, userId
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userCredential.UserName),
                new Claim("UserId", userDetail.Id.ToString()),
                new Claim(ClaimTypes.Role,userCredential.Role.ToString()),
            };
            //Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //create Token and set it to user
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userCredential.Token = tokenHandler.WriteToken(token);
            userCredential.IsActive = true;
            await _dbContext.SaveChangesAsync();

            Log.Information("Token Generated for {@UserId} with {@token} ", userDetail.Id, userCredential.Token);

            return Ok(new
            {
                token = userCredential.Token,
                userCredential.UserName,
                userDetail.Id,
            });
        }
    }
}
