namespace MovieService.Database.Models
{
    public class Movie
    {
        /// <summary>
        ///     This id is same as ImDb movie id
        /// </summary>
        public string Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }
        public decimal ImdbRating { get; set; }
    }
}