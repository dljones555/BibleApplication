namespace Bible.Domain
{
    public interface IBible
    {
        List<Book> Books { get; set; }
        string Version { get; set; }
        public ISearchStrategy DetermineSearchStrategy(string searchTerm);
        Book GetBook(string book);
        (string book, int chapter) GetChapterParts(string verseInput);
        (string book, int chapter, int verse) GetVerseParts(string verseInput);
        bool IsValidChapterFormat(string verseInput);
        bool IsValidVerseFormat(string verseInput);
    }
}