using gitreader.data.git;
using LibGit2Sharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace gitreader.data
{
    public class GitHubReader : IGitData
    { 

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
            string responseBodyAsText ="";
            GitRepo[] repos = JsonConvert.DeserializeObject<GitRepo[]>(responseBodyAsText);

            foreach (GitRepo repo in repos.ToList<GitRepo>())
            {
                if (repo.Name != "gitreader")
                {
                    Repository.Clone(String.Format("https://github.com/{0}/{1}.git", owner, repo.Name), String.Format(@"C:/DevProjects/{0}", repo.Name));
                    responseBodyAsText += " Cloned : " + repo.FullName + " (" + repo.Name + ")";
                }
            }

            return responseBodyAsText;

        }

    }
}