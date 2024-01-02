namespace Bible.Domain
{
    public class KjvBibleTextParser : IBibleTextParser
    {
        private string _text { get; set; }
        private string _version { get; set; }
        public KjvBibleTextParser(string version, string text)
        {
            _text = text;
            _version = version;
        }

        public Bible Parse()
        {
            var apocrypha = "TOB JDT ESG WIS SIR BAR PRA SUS BEL 1MA 2MA 1ES PRM 4ES";

            List<Book> books = new List<Book>();
            var lines = _text.Split('\n');
            Book? currentBook = default;
            Chapter? currentChapter = default;

            foreach (var line in lines)
            {
                bool isNewBook = false;

                // EOF for eng-kjv_vpl
                if (string.IsNullOrWhiteSpace(line))
                    break;

                var verse = ParseLine(line);

                if (apocrypha.Contains(verse.Book))
                    continue;

                if (verse.Book != currentBook?.Title)
                {
                    currentBook = new Book(verse.Book);
                    books.Add(currentBook);

                    isNewBook = true;
                }

                if (verse.Chapter != currentChapter?.Number || isNewBook)
                {
                    currentBook.Chapters.Add(new Chapter(verse.Chapter,currentBook.Title));
                }

                currentChapter = currentBook.Chapters.Last();

                var verseText = verse.VerseText;
                // Replace Unicode paragraph characer with newilne
                verseText = verseText.Replace("\u00b6", "\n");
                verseText = verseText.Trim();
                currentChapter.Verses.Add(new Verse(verse.Chapter, verse.Verse, verseText));

            }

            return new Bible(_version, books);
        }

        public BibleTextLine ParseLine(string line)
        {
            // GEN 1:1 In the beginning...
            // Split at first semicolon, returning two parts.
            var lineParts = line.Split(':', 2);

            // Split book and chapter from 1st part
            var bookAndChapter = lineParts[0].Split(' ');
            var book = bookAndChapter[0];
            var chapter = Convert.ToInt32(bookAndChapter[1]);

            // Parse verse number
            var verseNumEndPos = lineParts[1].IndexOf(' ');
            int verseNum = Convert.ToInt32(lineParts[1].Substring(0, verseNumEndPos));

            // Parse verse text
            verseNumEndPos++;
            var verseText = lineParts[1][verseNumEndPos..];

            return new BibleTextLine(book, chapter, verseNum, verseText);
        }

        public (string book, int chapter, int verse) ParseVerse(string verseInput)
        {
            // Split at first semicolon, returning two parts.
            var verseParts = verseInput.Split(':', 2);

            // Split book and chapter from 1st part
            var bookAndChapter = verseParts[0].Split(' ');
            var book = bookAndChapter[0];
            var chapter = Convert.ToInt32(bookAndChapter[1]);

            int verse = Convert.ToInt32(verseParts[1]);

            return (book, chapter, verse);
        }
    }
}
