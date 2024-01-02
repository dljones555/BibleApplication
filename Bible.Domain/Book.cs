namespace Bible.Domain
{
    public class Book
    {
        public string Title { get; set; }
        public List<Chapter> Chapters { get; set; }
        public Book(string title)
        {
            Title = title;
            Chapters = new List<Chapter>();
        }

        public bool IsValidChapter(int chapter)
        {
            return chapter > this.Chapters.Count;
        }

        public Chapter GetChapter(int chapter)
        {
            return this.Chapters.Where(c => c.Number == chapter).First();
        }
    }
}
