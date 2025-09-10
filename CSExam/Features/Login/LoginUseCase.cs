using CSExam.Entites;
using CSExam.Services.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CSExam.Features.Login;

public class LoginUseCase(
    CSExamDbContext ctx,
    EFJWTService jwt
)
{
    public async Task<Result<LoginResponse>> Do(LoginPayload payload)
    {
        var user = await ctx.Users
            .FirstOrDefaultAsync(u => u.Username == payload.Login && u.Password == payload.Password);

        if (user is null)
            return Result<LoginResponse>.Fail("User not found");

        if (user.Password != payload.Password)
            return Result<LoginResponse>.Fail("Login or Password are incorrects");

        var token = jwt.CreateToken(new UserToLoginDto
        {
            ID = user.ID,
            Username = user.Username
        });

        return Result<LoginResponse>.Success(new LoginResponse(token));
    }
}