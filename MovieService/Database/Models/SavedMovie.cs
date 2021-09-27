using System;

namespace MovieService.Database.Models
{
    [Serializable]
    public class SavedMovie
    {
        public string MovieId { get; set; }
        public int UserId { get; set; }
        public bool Watched { get; set; }
        public Movie Movie { get; set; }
    }
}