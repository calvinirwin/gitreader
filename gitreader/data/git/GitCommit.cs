using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gitreader.data.git
{


    public class GitCommit
    {
        public string SHA { get; set; }
        public string Url { get; set; }
        public string Html_url { get; set; }
        public string Comments_url { get; set; }
        public GitUser Author { get; set; }
        public GitUser Committer { get; set; }
        public IEnumerable<GitCommit> Parents { get; set; }



        public class GitCommitDetails
        {
            public string Message { get; set; }
            public string Url { get; set; }
            public GitMember Author { get; set; }
            public GitMember Committer { get; set; }
        }

        public class GitMember
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public DateTime Date { get; set; }

        }

    }

}