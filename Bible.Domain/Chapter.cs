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

        public bool IsValidVerse(int verse)
        {
            return verse > this.Verses.Count;
        }
        public Verse GetVerse(int verse)
        {
            return Verses[verse];
        }
    }
}
