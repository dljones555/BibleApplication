using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bible.Domain
{
    public interface ISearchStrategy
    {
        public IEnumerable<BibleTextLine> Search(string searchTerm);
        string Validate(string searchTerm);
    }
    
}
