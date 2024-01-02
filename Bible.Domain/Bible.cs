using System.Text.RegularExpressions;

namespace Bible.Domain
{

    public class Bible : IBible
    {
        public string Version { get; set; }
        public List<Book> Books { get; set; }
        private Regex _verseFormatRegex { get; set; }
        private Regex _chapterFormatRegex { get; set; }
        public Bible(string version, List<Book> books)
        {
            Version = version;
            Books = books;

            _verseFormatRegex = new(@"^([a-zA-Z]{3})\s(\d+):(\d+)$");
            _chapterFormatRegex = new(@"^([a-zA-Z]{3})\s(\d+)$");
        }

        public ISearchStrategy DetermineSearchStrategy(string searchTerm)
        {
            ISearchStrategy searchStrategy;

            if (IsValidVerseFormat(searchTerm))
            {
                searchStrategy = new VerseSearchStrategy(this);
            }
            else if (IsValidChapterFormat(searchTerm))
            {
                searchStrategy = new ChapterSearchStrategy(this);
            }
            else
            {
                searchStrategy = new KeywordSearchStrategy(this);
            }

            return searchStrategy;
        }

        public bool IsValidVerseFormat(string verseInput)
        {
            return _verseFormatRegex.IsMatch(verseInput);
        }

        public (string book, int chapter, int verse) GetVerseParts(string verseInput)
        {
            var match = _verseFormatRegex.Match(verseInput);

            return (match.Groups[1].Value, Convert.ToInt32(match.Groups[2].Value), Convert.ToInt32(match.Groups[3].Value));
        }

        public bool IsValidChapterFormat(string verseInput)
        {
            return _chapterFormatRegex.IsMatch(verseInput);
        }

        public (string book, int chapter) GetChapterParts(string verseInput)
        {
            var match = _chapterFormatRegex.Match(verseInput);
            return (match.Groups[1].Value, Convert.ToInt32(match.Groups[2].Value));
        }

        public Book GetBook(string book)
        {
            var b = this.Books.Where(b => b.Title == book).FirstOrDefault();
            return b;
        }
    }
}
