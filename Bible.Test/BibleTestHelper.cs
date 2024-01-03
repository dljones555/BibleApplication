

namespace Bible.Test
{
    public static class BibleTestHelper
    {
        public static IBible CreateDefaultKingJamesBible()
        {
            var version = "kjv";
            var filename = "eng-kjv_vpl.txt";

            var filepath = $"./Resources/{filename}";

            using var sr = new StreamReader(filepath);
            var fileContents = sr.ReadToEnd();

            var btp = new KjvBibleTextParser(version, fileContents);
            var bible = btp.Parse();

            return bible;
        }
    }
}
