using Meetup.WomenInTech.CassandraRepository.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.WomenInTech.CassandraRepository.Interface
{
    public interface IRepository
    {
        Task<List<UserDetails>> Get();

        Task Upsert(UserDetails userDetails);

        Task<UserDetails> GetById(string id);       
    }
}
