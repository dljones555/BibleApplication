namespace Bible.Domain
{
    public class Chapter
    {
        public int Number { get; set; }
        public string Book { get; }
        public List<Verse> Verses { get; set; }

        public Chapter(int number, string book)
        {
            Verses = new List<Verse>();
            Number = number;
            Book = book;
        }

        public bool IsValidVerse(int number)
        {
            return number > this.Verses.Count;
        }
        public Verse GetVerse(int number)
        {
            return Verses.Where(v => v.Number == number).Single();
        }
    }
}
