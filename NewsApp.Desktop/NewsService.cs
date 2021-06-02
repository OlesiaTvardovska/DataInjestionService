using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using NewsApp.Desktop.Models;
using System.Threading.Tasks;

namespace NewsApp.Desktop
{
    public class NewsService
    {
        public async static Task<NewsList> GetUnmarkedNews()
        {
            var graphQLClient = new GraphQLHttpClient("http://localhost:58724/graphql", 
                new NewtonsoftJsonSerializer());
            
            var newsRequest = new GraphQLRequest
            {
                Query = @"
                    query{
                      news_list{
                        title,
                        url,
                        id
                        }
                    }
                "
            };

            var graphQLResponse = await graphQLClient.SendQueryAsync<NewsList>(newsRequest);
            return graphQLResponse.Data;
        }
    }
}
