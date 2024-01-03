namespace Bible.Domain
{
    public class ChapterSearchStrategy : ISearchStrategy
    {
        private IBible _bible;
        public ChapterSearchStrategy(IBible bible)
        {
            _bible = bible;
        }
        public IEnumerable<BibleTextLine> Search(string searchTerm)
        {
            var verses = new List<BibleTextLine>();

            var chapterParts = _bible.GetChapterParts(searchTerm);

            var book = _bible.GetBook(chapterParts.book);

            var chapter = book.GetChapter(chapterParts.chapter);

            foreach(var v in chapter.Verses)
            {
                verses.Add(new BibleTextLine(book.Title, chapter.Number, v.Number, v.Text));
            }

            return verses;
        }

        public string Validate(string searchTerm)
        {
            return string.Empty;
        }
    }
}
