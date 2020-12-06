using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {
            

        }
        public Admin GetAdminByUserId(Guid userId)
        {
            return dbContext.Admins
                            .Where(admin => admin.UserId == userId)
                            .SingleOrDefault();
        }
    }
}
