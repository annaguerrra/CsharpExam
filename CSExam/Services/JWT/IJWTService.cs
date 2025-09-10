namespace CSExam.Services.JWT;

public interface IJWTService
{
    public string CreateToken(UserToLoginDto data);
}