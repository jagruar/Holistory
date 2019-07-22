namespace Holistory.Api.DataTranserObjects
{
    public class IdentificationDto
    {
        public string Id { get; set; }

        public string Token { get; set; }

        public IdentificationDto(string id, string token)
        {
            Id = id;
            Token = token;
        }
    }
}
