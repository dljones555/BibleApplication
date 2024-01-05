using System.Collections.Generic;
using System.Text.RegularExpressions;

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

            var searchTermToLower = searchTerm.ToLower();

            Regex r = new Regex(@$"(\b{searchTerm}\b)", RegexOptions.IgnoreCase);
            //var result = List.Where(u => r.IsMatch(u.PartNumber) && u.PartNumber.Contains("12201"));

            var query = from book in _bible.Books
                        from chapter in book.Chapters
                        from verse in chapter.Verses
                        where r.IsMatch(verse.Text)
                        select new { book.Title, ChapterNumber = chapter.Number, VerseNumber = verse.Number, verse.Text };

            foreach (var v in query.ToList())
            {
                verses.Add(new BibleTextLine(v.Title, v.ChapterNumber, v.VerseNumber, v.Text));
            }

            return verses;
        }

        public string Validate(string searchTerm)
        {
            return string.Empty;
        }
    }
}
