using POS.Application.Commons.Base;
using POS.Application.Dto.Request;
using POS.Application.Dto.Response;

namespace POS.Application.Interface;

public interface IUserApplication
{
    Task<BaseResponse<IEnumerable<UserSelectResponseDto>>> ListSelectUser();
    Task<BaseResponse<string>> GenerateToken(TokenRequestDto requestDto);
    Task<BaseResponse<UserResponseDto>> UserById(int id);
    Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto);
    Task<BaseResponse<bool>> EditUser(int userId, UserRequestDto requestDto);
    Task<BaseResponse<bool>> RemoveUser(int id);
}
