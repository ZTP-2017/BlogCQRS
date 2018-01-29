using System.Threading.Tasks;
using Akka.Actor;
using Blog.ReadSide.Model;
using Dapper;
using MySql.Data.MySqlClient;

namespace Blog.ReadSide.Query
{
    public class ArticleHandler : ReceiveActor
    {
        private readonly string _connectionString = @"Server=localhost;database=blog_cqrs;uid=root;pwd=password;";

        public ArticleHandler()
        {
            ReceiveAsync<GetArticleDetails>(Handle);
            ReceiveAsync<GetSectionArticleListItems>(Handle);
        }

        private async Task Handle(GetArticleDetails query)
        {
            var sql = "SELECT * FROM ArticleDetails WHERE id = @ArticleId;";
            
            using (var connection = new MySqlConnection(_connectionString))
            {
                var result = await connection
                    .QuerySingleAsync<ArticleDetailsRecord>(
                        sql, new { ArticleId = query.Id });
                
                Sender.Tell(result, Self);
            }
        }
        
        private async Task Handle(GetSectionArticleListItems query)
        {
            var sql = "SELECT * FROM ArticleDetails WHERE SectionId = @SectionId;";
            
            using (var connection = new MySqlConnection(_connectionString))
            {
                var result = await connection
                    .QueryAsync<ArticleListItemRecord>(sql, new { SectionId = query.Id });
                
                Sender.Tell(result, Self);
            }
        }
    }
}