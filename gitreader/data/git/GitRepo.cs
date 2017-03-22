using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gitreader.data.git
{
    public class GitRepo
    {

        public int id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public GitUser Owner { get; set; }
        public IEnumerable<GitRepoContributor> Contributors { get; set; }

        public bool Private { get; set; }
        public bool Fork { get; set; }
        public string Description { get; set; }

        public string Url { get; set; }
        public string Html_url { get; set; }
        public string Language { get; set; }
        public string Default_branch { get; set; }
        public IEnumerable<GitBranch> Branches { get; set; }

        public class GitRepoContributor : GitUser
        {
            public int Contributions { get; set; }

        }

        public class GitBranch
        {
            public string Name { get;set;}
            public GitCommit Commit { get; set; }
        }


        public void GetContributors() { }

        public void GetBranches() { }


    }
}