namespace Bible.Domain
{
    // Add words of Jesus in red decorator. Char position start and end.
    // Cross references
    // Personal notes
    // Sermon/study overlays
    public record BibleTextLine(string Book, int Chapter, int Verse, string VerseText);
}
