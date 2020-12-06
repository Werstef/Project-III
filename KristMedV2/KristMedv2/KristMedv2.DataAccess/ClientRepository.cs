using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository 
    {
        public ClientRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {

        }

        public Client GetClientByUserId(Guid userId)
        {
            return dbContext.Clients
                            .Where(client => client.UserId == userId)
                            .SingleOrDefault();
        }
    }
}
