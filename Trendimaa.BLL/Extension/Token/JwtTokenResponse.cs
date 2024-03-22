namespace Orde.BLL.Extension.Token
{
    public class JwtTokenResponse
    {
        public JwtTokenResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
