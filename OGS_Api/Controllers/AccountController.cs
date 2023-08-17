namespace OGS_Api.Controllers
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using OGS_Api.Data;
    using OGS_Api.DTO;
    using OGS_Api.Repositories;
    using OGS_Api.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUsersRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly OgsContext _context;
        private readonly IPhotoService _photoService;
        public AccountController(IUsersRepository userRepository, IConfiguration configuration, IMapper mapper, OgsContext context, IPhotoService photoService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
            _context = context;
            _photoService = photoService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(UserModel userModel)
        {
            var user = await _userRepository.Authenticate(userModel.email, userModel.password);
            
            if(user == null){
                return Unauthorized("Email or Password is Invalid");
            }

            var res = new UserResModel();
            res.username = user.username;
            res.token = CreateJWT(user);
            res.id = user.id;
            

            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserModel userModel){

            if(await _userRepository.UserAlreadyExists(userModel.email)){
                return BadRequest("User is Already Exists in the Database Please Provide other Email Address");
            }

             _userRepository.Register(userModel.username, userModel.password, userModel.email, userModel.mobileno, userModel.address);
             return StatusCode(201);
        }

        private string CreateJWT(User user){

            var SecretKey = _configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var claims = new Claim[]{
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
            };

            var signingCredentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredentails
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet("userinfo/{userId}")]
        public async Task<IActionResult> UserInfo(int userId){
            var user = await _userRepository.UserInfoAsync(userId);
            // var userInfo = _mapper.Map<UserModel>(user);
            return Ok(user);
        }

        [HttpPost("photo/{userId}")]
        public async Task<IActionResult> AddUserPhoto([FromForm] UserFileModel fileModel, [FromRoute] int userId){

            var result = await _photoService.UploadImageAsync(fileModel.file);

            var user = await _userRepository.UserInfoAsync(userId);

            fileModel.imageUrl = result.SecureUrl.AbsoluteUri;
            fileModel.publicId = result.PublicId;

            var userImage = _mapper.Map<User>(fileModel);

            if(user != null){

                user.imageUrl = userImage.imageUrl;
                user.publicId = userImage.publicId;
            }

            await _context.SaveChangesAsync();
            return Ok(user);
        }


        [HttpGet]

        public async Task<IActionResult> GetlAllUsers(){
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        // [HttpPut("updatepassword/{userId}")]
        // public async Task<IActionResult> UserUpdatePassword(int userId, UserPasswordUpdateModel userModel){

        //     await _userRepository.UserUpdatePasswordAsync(userId, userModel);
        //     return StatusCode(200);
        // }

    }
}