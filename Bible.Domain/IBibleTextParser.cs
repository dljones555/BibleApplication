namespace Bible.Domain
{
    public interface IBibleTextParser
    {
        Bible Parse();
        BibleTextLine ParseLine(string line);
        (string book, int chapter, int verse) ParseVerse(string verseInput);
    }
}