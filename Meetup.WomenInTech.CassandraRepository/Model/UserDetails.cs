using Cassandra.NetCore.ORM.Attributes;
using Newtonsoft.Json;
using System;

namespace Meetup.WomenInTech.CassandraRepository.Model
{
    [CassandraTable("UserDetails")]
    public class UserDetails
    {
        [JsonProperty("id")]
        [PrimaryKey]
        public string Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}
