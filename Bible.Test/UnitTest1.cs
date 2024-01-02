using Bible.Domain;

namespace Bible.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            const int bibleCanonBookCount = 66;

            var version = "kjv";
            var filename = "eng-kjv_vpl.txt";

            using var sr = new StreamReader(filename);
            var fileContents = sr.ReadToEnd();

            var btp = new KjvBibleTextParser(version, fileContents);
            var bible = btp.Parse();

            Assert.Equal(bibleCanonBookCount,bible.Books.Count);

        }
    }
}