using Bible.Domain;

namespace Bible.Test
{
    public class UnitTest1
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

        // verify verses. gen 1:1, john 3:16, rev 22:21 verse text

        // verify 3 types of strategies
    }
}