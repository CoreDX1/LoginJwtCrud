using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using POS.Application.Commons.Base;
using POS.Application.Dto.Request;
using POS.Application.Dto.Response;
using POS.Application.Interface;
using POS.Application.Validators.User;
using POS.Domain.Entities;
using POS.Infrastructure.Persistences.Interface;
using POS.Utilities.Static;
using System.Security.Claims;
using BC = BCrypt.Net.BCrypt;
using POS.Application.Token;
using Microsoft.Extensions.Options;

namespace POS.Application.Services;

public class UserApplication : IUserApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserValidator _validator;
    private readonly JwtSettings _options;

    public UserApplication(
        IMapper mapper,
        UserValidator validator,
        IUnitOfWork unitOfWork,
        IOptions<JwtSettings> options
    )
    {
        _mapper = mapper;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _options = options.Value;
    }

    public async Task<BaseResponse<IEnumerable<UserSelectResponseDto>>> ListSelectUser()
    {
        var response = new BaseResponse<IEnumerable<UserSelectResponseDto>>();
        IEnumerable<User> user = await _unitOfWork.User.LIstSelectUser();
        if (user is null)
        {
            response.ISuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMTY;
        }
        else
        {
            response.ISuccess = true;
            response.Data = _mapper.Map<IEnumerable<UserSelectResponseDto>>(user);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var validateUser = await _validator.ValidateAsync(requestDto);
        if (!validateUser.IsValid)
        {
            response.ISuccess = false;
            response.Message = ReplyMessage.MESSAGE_VALIDATE;
            response.Errors = validateUser.Errors;
            return response;
        }
        User user = _mapper.Map<User>(requestDto);
        user.Password = BC.HashPassword(requestDto.Password);
        response.Data = await _unitOfWork.User.RegisterUser(user);
        if (!response.Data)
        {
            response.ISuccess = false;
            response.Message = ReplyMessage.MESSAGE_VALIDATE_EMAIL;
        }
        else
        {
            response.ISuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        return response;
    }

    public async Task<BaseResponse<UserResponseDto>> UserById(int id)
    {
        var response = new BaseResponse<UserResponseDto>();
        User user = await _unitOfWork.User.UserById(id);
        if (user is null)
        {
            response.ISuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMTY;
        }
        else
        {
            response.ISuccess = true;
            response.Message = ReplyMessage.MESSAGE_QUERY;
            response.Data = _mapper.Map<UserResponseDto>(user);
        }
        return response;
    }

    public async Task<BaseResponse<string>> GenerateToken(TokenRequestDto requestDto)
    {
        var response = new BaseResponse<string>();
        User count = await _unitOfWork.User.AccountUserName(requestDto.Name!);
        if (count is not null)
        {
            if (BC.Verify(requestDto.Password, count.Password))
            {
                response.ISuccess = true;
                response.Data = GenerateToken(count);
                response.Message = ReplyMessage.MESSAGE_TOKEN;
                return response;
            }
        }
        else
        {
            response.ISuccess = false;
            response.Message = ReplyMessage.MESSAGE_TOKEN_ERROR;
        }
        return response;
    }

    // * GENERAMOS EL TOKEN CON LA INFOMACIÓN DEL USUARIO //
    private string GenerateToken(User user)
    {
        // * CREAMOS EL HEADER //
        var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret!));
        var creadentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(creadentials);

        // * CREAMOS EL CLAIMS //
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Name),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.GivenName, user.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(
                JwtRegisteredClaimNames.Iat,
                Guid.NewGuid().ToString(),
                ClaimValueTypes.Integer64
            ),
        };

        // * CREAMOS EL PAYLOAD //
        var payload = new JwtPayload(
            issuer: _options.Issuer,
            audience: _options.Issuer,
            claims: claims,
            notBefore: DateTime.UtcNow,
            // * Exipa a las 8 horas //
            expires: DateTime.Now.AddHours(int.Parse(_options.Expires!))
        );

        // * GENERAMOS EL TOKEN //
        var token = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<BaseResponse<bool>> EditUser(int userId, UserRequestDto requestDto)
    {
        var response = new BaseResponse<bool>();
        var editUser = await UserById(userId);
        if (editUser.Data is null)
        {
            response.ISuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMTY;
            return response;
        }
        User user = _mapper.Map<User>(requestDto);
        user.UserId = userId;
        response.Data = await _unitOfWork.User.UpdateUser(user);
        if (!response.Data)
        {
            response.ISuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        else
        {
            response.ISuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        return response;
    }

    public async Task<BaseResponse<bool>> RemoveUser(int id)
    {
        var response = new BaseResponse<bool>();
        var userId = await UserById(id);
        if (userId.Data is null)
        {
            response.ISuccess = false;
            response.Message = ReplyMessage.MESSAGE_QUERY_EMTY;
            return response;
        }
        response.Data = await _unitOfWork.User.DeleteUser(id);
        if (!response.Data)
        {
            response.ISuccess = false;
            response.Message = ReplyMessage.MESSAGE_FAILED;
        }
        else
        {
            response.ISuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        return response;
    }
}
