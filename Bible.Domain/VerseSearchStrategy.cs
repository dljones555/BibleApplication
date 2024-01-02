using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bible.Domain
{
    public class VerseSearchStrategy : ISearchStrategy
    {
        private IBible _bible;
        public VerseSearchStrategy(IBible bible)
        {
            _bible = bible;
        }

        public string Validate(string searchTerm)
        {
            string error = string.Empty;

            var verseParts = _bible.GetVerseParts(searchTerm);
            var book = _bible.GetBook(verseParts.book);

            if (book is null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Invalid book abbreviation.");
                foreach (var b in _bible.Books)
                {
                    sb.Append(b.Title + " ");
                }
                sb.AppendLine();
                error = sb.ToString();
                return error;
            }

            if (book.IsValidChapter(verseParts.chapter))
            {
                error = $"{book.Title} has {book.Chapters.Count} chapters.";
                return error;
            }

            var chapter = book.GetChapter(verseParts.chapter);

            if (chapter.IsValidVerse(verseParts.verse))
            {
                error = $"{book.Title} {chapter.Number} has {chapter.Verses.Count} verses.";
                return error;
            }

            return error;
        }

        public IEnumerable<BibleTextLine> Search(string searchTerm)
        {
            var verses = new List<BibleTextLine>();
            var verseParts = _bible.GetVerseParts(searchTerm);

            var book = _bible.GetBook(verseParts.book);

            var chapter = book.GetChapter(verseParts.chapter);

            var verse = chapter.GetVerse(verseParts.verse);

            verses.Add(new BibleTextLine(book.Title, chapter.Number, verseParts.verse, verse.Text));

            return verses;
        }
    }
}
