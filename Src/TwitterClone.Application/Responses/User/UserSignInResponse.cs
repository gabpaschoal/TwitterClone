namespace TwitterClone.Application.Responses.User;

public class UserSignInResponse
{
    public string Name { get; set; }
    public string NickName { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpireAt { get; set; }
}
