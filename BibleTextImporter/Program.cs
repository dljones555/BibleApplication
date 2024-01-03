using Bible.Domain;
using System.Text.RegularExpressions;

try
{

    // strategy pattern could work 
    // text search - i'm sure there are better ways to index. research. probably not running parallel asyn search on x books and
    //               returning results
    // book/chapter format
    // book/chapter/verse
    // in prepping data need to take out apocrypha books or make optional
    // unit tests. count books, total chapters. test a few key verses and edge cases.
    // those paragraph tags need to be there
    // there is other meta data in there like words of jesus in red
    // what does at scale bible architecture look like
    // 
    // bible app..
    // what are people searching for
    // what ever happened to tag and search clouds and geo maps of searches
    // data on what churches are studying
    // could do all kinds of cool stuff with big data and analytics
    // would be neat to see this stuff look more "alive" and "active"
    // people in my city are reading x translation, searching for
    // engagement by day
    // what does bible app, online, devos look like versus paper versus podcasts, youtube videos, etc.
    // time spent

    var version = "kjv";
    var filename = "eng-kjv_vpl.txt";

    using var sr = new StreamReader(filename);
    var fileContents = sr.ReadToEnd();

    var btp = new KjvBibleTextParser(version, fileContents);
    var bible = btp.Parse();

    var searchTerm = "";
    while (true)
    {
        Console.Write("Search the Bible: ");
        searchTerm = Console.ReadLine();

        if (String.IsNullOrWhiteSpace(searchTerm))
        {
            Console.WriteLine("Enter a bible verse or search keyword(s).");
            continue;
        }

        if (searchTerm.ToUpperInvariant().Equals("END"))
            break;

        var searchStrategy = bible.DetermineSearchStrategy(searchTerm);

        var error = searchStrategy.Validate(searchTerm);

        if (!string.IsNullOrWhiteSpace(error))
        {
            Console.WriteLine(error);
            continue;
        }

        var lines = searchStrategy.Search(searchTerm);

        foreach(var line in lines)
        {
            Console.WriteLine($"{line.Book} {line.Chapter}:{line.Verse} {line.VerseText}");
        }

        // allow for just chapter
        // easier in a controller
        // bool isValidChapter = !string.IsNullOrEmpty(bibleVerse) && bible.IsValidChapterFormat(bibleVerse);

    }
}
catch (IOException e)
{
    Console.WriteLine(e.Message);
}
