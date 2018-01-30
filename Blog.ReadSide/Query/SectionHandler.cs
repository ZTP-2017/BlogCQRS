using System.Threading.Tasks;
using Akka.Actor;
using Blog.ReadSide.Model;
using Dapper;
using MySql.Data.MySqlClient;

namespace Blog.ReadSide.Query
{
    public class SectionHandler : ReceiveActor
    {
        private readonly string _connectionString = @"Server=localhost;database=blog_read;uid=root;pwd=password;";

        public SectionHandler()
        {
            ReceiveAsync<GetSectionList>(Handle);
        }

        private async Task Handle(GetSectionList query)
        {
            var sql = "SELECT * FROM Section;";
            
            using (var connection = new MySqlConnection(_connectionString))
            {
                var result = await connection.QueryAsync<SectionDetailsRecord>(sql);
                
                Sender.Tell(result, Self);
            }
        }
    }
}