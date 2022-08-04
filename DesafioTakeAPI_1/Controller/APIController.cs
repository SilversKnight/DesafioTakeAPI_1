using System;
using System.Collections.Generic;

using System.Web;
using System.Net;
using System.Text;
using System.Linq;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DesafioTakeAPI_1.Model;


namespace DesafioTakeAPI_1.API
{
    class APIController
    {
        private async Task<List<TakeRepository>> requestRepositoryList(ILambdaContext context)
        {
            context.Logger.Log("Realizando requisição de dados do repositório.\n");

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

            HttpClient httpClient = new HttpClient { BaseAddress = new Uri("https://api.github.com/orgs/takenet/") };
            httpClient.DefaultRequestHeaders.Add("User-Agent", "request");
            List<TakeRepository> listTakeRepos = new List<TakeRepository>();
            string responseString = "";

            try
            {
                var response = await httpClient.GetAsync("repos");
                responseString = await response.Content.ReadAsStringAsync();
                var listJson = JsonConvert.DeserializeObject<IList<GitHubRepository>>(responseString, settings);
                listJson = listJson.Where(r => r.language == "C#").OrderBy(r => r.created_at).Take(5).ToList();

                foreach (var repos in listJson)
                {
                    listTakeRepos.Add(
                        new TakeRepository
                        {
                            avatar_url = repos.owner.Property("avatar_url").Value.ToString(),
                            created_at = repos.created_at,
                            description = repos.description,
                            full_name = repos.full_name,
                            language = repos.language
                        }
                        );
                }

                context.Logger.Log("Requisição bem-sucedida\n");
            }
            catch (Exception e)
            {
                context.Logger.Log("Falha na requisição\n");
            }

            return listTakeRepos;
        }
        public APIGatewayProxyResponse returnRepositoryData(ILambdaContext context)
        {
            APIGatewayProxyResponse response;

            List<TakeRepository> body = requestRepositoryList(context).Result;

            try
            {
                response = formatStandardReturn(200,body);
                context.Logger.Log("Retornando JSON.\n");
            }
            catch (Exception e)
            {

                response = new APIGatewayProxyResponse
                {
                    StatusCode = 400,
                    Body = e.Message,
                    Headers = new Dictionary<string, string>
                    {
                        { "Content-Type", "application/json" },
                        { "Access-Control-Allow-Origin", "*" }
                    }
                };
            }

            return response;
        }

        private APIGatewayProxyResponse formatStandardReturn(int status, IList body)
        {

            return new APIGatewayProxyResponse
            {
                StatusCode = status,
                Body = JsonConvert.SerializeObject(body),
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "Access-Control-Allow-Origin", "*" }
                }
            };
        }
    }
}
