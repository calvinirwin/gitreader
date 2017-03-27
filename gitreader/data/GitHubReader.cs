using gitreader.data.git;
using LibGit2Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace gitreader.data
{
    public class GitHubReader : IGitData
    { 

        private const string  LOCAL_MASTER_DIR = @"C:/DevProjects_Git/";


        public string GetRepoData(string owner)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format("https://api.github.com/users/{0}/repos", owner));
            client.DefaultRequestHeaders.Add("User-Agent", "calvinirwin");
            var response = client.GetAsync("").Result;
            response.EnsureSuccessStatusCode();

            //string status = response.StatusCode + " " + response.ReasonPhrase + Environment.NewLine;
            string responseBodyAsText =  response.Content.ReadAsStringAsync().Result;
            GitRepo[] repos = JsonConvert.DeserializeObject<GitRepo[]>(responseBodyAsText);
            
            // todo: load the branches and the contributors


            return responseBodyAsText;
            
        }



        /// <summary>
        /// use this to test out getting commit data from the server
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public string GetCommitData(string owner, string repository)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format("https://api.github.com/repos/{0}/{1}/commits", owner, repository));
            client.DefaultRequestHeaders.Add("User-Agent", "calvinirwin");
            var response = client.GetAsync("").Result;
            response.EnsureSuccessStatusCode();
            string responseBodyAsText = response.Content.ReadAsStringAsync().Result;
            GitCommit[] commits = JsonConvert.DeserializeObject<GitCommit[]>(responseBodyAsText);

            


            return responseBodyAsText;

        }



        public string CloneRepos(string owner)
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format("https://api.github.com/users/{0}/repos", owner));
            client.DefaultRequestHeaders.Add("User-Agent", "calvinirwin");
            var response = client.GetAsync("").Result;
            response.EnsureSuccessStatusCode();

            //string status = response.StatusCode + " " + response.ReasonPhrase + Environment.NewLine;
            string responseBodyAsText = response.Content.ReadAsStringAsync().Result;
            GitRepo[] repos = JsonConvert.DeserializeObject<GitRepo[]>(responseBodyAsText);
            responseBodyAsText = "";

            foreach (GitRepo repo in repos.ToList<GitRepo>())
            {
                if (repo.Name != "gitreader")
                {
                    responseBodyAsText += "<hr />";
                    string repoLocation = String.Format(LOCAL_MASTER_DIR + "{0}", repo.Name);
                    if (!Directory.Exists(repoLocation))
                    {
                        Repository.Clone(String.Format("https://github.com/{0}/{1}.git", owner, repo.Name), repoLocation);
                        responseBodyAsText += "<strong>" + repo.Name + "<em>Status: Cloned</em></strong><br />";
                    }
                    else
                    {
                        using (var repoLocal = new Repository(repoLocation))
                        {
                            MergeResult mr = Commands.Pull(repoLocal, 
                                new LibGit2Sharp.Signature("testuser", "test@test.com", new DateTimeOffset(DateTime.Now)), 
                                new LibGit2Sharp.PullOptions());
                            responseBodyAsText += "<strong>" + repo.Name + "<em>Status: " + mr.Status + "</em></strong><br />";
                        }
                    }
                    responseBodyAsText += GetStats(repo.Name);
                }
            }
            return responseBodyAsText;
        }


        public string GetStats(string repoName)
        {
            string stats = string.Empty;
            using (var repo = new Repository(String.Format(LOCAL_MASTER_DIR + "{0}", repoName)))
            {
                var RFC2822Format = "ddd dd MMM HH:mm:ss yyyy K";

                foreach (Commit c in repo.Commits.Take(15))
                {
                    stats += "<br />";
                    stats += string.Format("commit {0}", c.Id);

                    if (c.Parents.Count() > 1)
                    {
                        stats += "<br />";
                        stats += String.Format("Merge: {0}", string.Join(" ", c.Parents.Select(p => p.Id.Sha.Substring(0, 7)).ToArray()));
                    }

                    stats += "<br />";
                    stats += String.Format("Author: {0} <{1}>", c.Author.Name, c.Author.Email);
                    stats += "<br />";
                    stats += String.Format("Date:   {0}", c.Author.When.ToString(RFC2822Format, CultureInfo.InvariantCulture));
                    stats += "<br />";
                    stats += String.Format(c.Message);
                    stats += "<br />";

                    //Console.WriteLine(string.Format("Author: {0} <{1}>", c.Author.Name, c.Author.Email));
                    //Console.WriteLine("Date:   {0}", c.Author.When.ToString(RFC2822Format, CultureInfo.InvariantCulture));
                    //Console.WriteLine();
                    //Console.WriteLine(c.Message);
                    //Console.WriteLine();
                }
            }
            return stats;

        }


    }
}