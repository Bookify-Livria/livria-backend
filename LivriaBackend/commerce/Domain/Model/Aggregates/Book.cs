using System.Text.Json.Serialization;

namespace LivriaBackend.commerce.Domain.Model.Aggregates
{
    public class Book
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Genre { get; set; }
        public float Price { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public string Language { get; set; }
        public string Author { get; set; }

        [JsonConstructor]
        public Book(string title, DateTime publishDate, string genre, float price, string format, string description,
            string cover, string language, string author)
        {
            Title = title;
            PublishDate = publishDate;
            Genre = genre;
            Price = price;
            Format = format;
            Description = description;
            Cover = cover;
            Language = language;
            Author = author;
        }

        protected Book() { }
    }
}