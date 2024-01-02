namespace Bible.Domain
{
    public class KeywordSearchStrategy : ISearchStrategy
    {
        private IBible _bible;
        public KeywordSearchStrategy(IBible bible)
        {
            _bible = bible;
        }
        public IEnumerable<BibleTextLine> Search(string searchTerm)
        {
            var verses = new List<BibleTextLine>();

            var query = from book in _bible.Books
                        from chapter in book.Chapters
                        from verse in chapter.Verses
                        where verse.Text.Contains(searchTerm)
                        select new { book.Title, ChapterNumber = chapter.Number, VerseNumber = verse.Number, verse.Text };

            foreach (var v in query.ToList())
            {
                verses.Add(new BibleTextLine(v.Title, v.ChapterNumber, v.VerseNumber, v.Text));
            }

            return verses;
        }

        public string Validate(string searchTerm)
        {
            throw new NotImplementedException();
        }
    }
}
