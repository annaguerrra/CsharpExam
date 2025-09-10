using Microsoft.Net.Http.Headers;

namespace CSExam.Services.JWT;

public class UserToLoginDto
{
    public Guid ID { get; set; }
    public string Username { get; set; }
}