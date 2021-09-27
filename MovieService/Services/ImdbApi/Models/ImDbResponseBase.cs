namespace MovieService.Services.ImdbApi.Models
{
    public abstract class ImDbResponseBase : IImDbResponse
    {
        string IImDbResponse.ErrorMessage { get; set; }
    }

    public interface IImDbResponse
    {
        public string ErrorMessage { get; set; }
    }
}