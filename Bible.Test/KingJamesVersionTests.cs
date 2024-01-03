using Bible.Domain;

namespace Bible.Test
{
    public class KingJamesVesionTests
    {
        [Fact]
        public void Load_KingJamesVesion_BooksCount_Returns_66()
        {
            const int expectedBibleCanonBookCount = 66;

            var bible = BibleTestHelper.CreateDefaultKingJamesBible();

            int actualBibleCanonBookCount = bible.Books.Count;

            Assert.Equal(expectedBibleCanonBookCount, actualBibleCanonBookCount);
        }

        [Fact]
        public void Load_KingJamesVesion_ChaptersCount_Returns_1189()
        {
            const int expectedChaptersTotalCount = 1189;

            var bible = BibleTestHelper.CreateDefaultKingJamesBible();

            int actualChaptersTotalCount = bible.Books.Sum(b => b.Chapters.Count);

            Assert.Equal(expectedChaptersTotalCount, actualChaptersTotalCount);
        }

        [Theory]
        [InlineData("GEN 1:1", "In the beginning God created the heaven and the earth.")]
        [InlineData("JOH 3:16", "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life.")]
        [InlineData("REV 22:21", "The grace of our Lord Jesus Christ [be] with you all. Amen.")]
        public void Search_Bible_By_VerseStrategy_Returns_VerseText(string verseNotation, string expectedVerse)
        {
            var bible = BibleTestHelper.CreateDefaultKingJamesBible();

            var strategy = bible.DetermineSearchStrategy(verseNotation);

            Assert.IsType<VerseSearchStrategy>(strategy);

            var actualVerse = strategy.Search(verseNotation);

            Assert.Single(actualVerse);

            Assert.Equal(expectedVerse, actualVerse.First().VerseText);
        }

        [Fact]
        public void Search_Bible_By_ChapterStrategy_Returns_Expected_Verse_Count()
        {
            var bible = BibleTestHelper.CreateDefaultKingJamesBible();

            const string chapter = "PSA 119";
            const int expectedVerseCount = 176;

            var strategy = bible.DetermineSearchStrategy(chapter);

            Assert.IsType<ChapterSearchStrategy>(strategy);

            var verses = strategy.Search(chapter);

            Assert.Equal(expectedVerseCount, verses.Count());
        }

        [Fact]

        public void Search_Bible_By_SearchStrategy_Returns_Expected_Verse_Count()
        {
            var bible = BibleTestHelper.CreateDefaultKingJamesBible();

            const string keyword = "Laodicea";
            const int expectedVerseCount = 6;

            var strategy = bible.DetermineSearchStrategy(keyword);

            Assert.IsType<KeywordSearchStrategy>(strategy);

            var verses = strategy.Search(keyword);

            Assert.Equal(expectedVerseCount, verses.Count());
        }
        // Write search test

        // Write negative tests / errors
    }
}