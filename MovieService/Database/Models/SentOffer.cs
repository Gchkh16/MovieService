using System;

namespace MovieService.Database.Models
{
    [Serializable]
    public class SentOffer
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string MovieId { get; set; }
        public DateTime SentAtUtc { get; set; }
    }
}