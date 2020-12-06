using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class ContactMessageRepository : BaseRepository<ContactMessage>, IContactMessageRepository
    {
        public ContactMessageRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<ContactMessage> GetAllContactMessages()
        {
            return dbContext.ContactMessages
                             .Include(message => message.Client)
                             .AsEnumerable();
        }
    }
}
