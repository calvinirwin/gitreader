using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gitreader.data
{
    public interface IGitData
    {
        string GetRepoData(string owner);

        //IEnumerable<GitRepo> GetRepoData(string owner);
    }
}
