using KristMedv2.ApplicationLogic.DataModels;
using KristMedv2.ApplicationLogic.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KristMedv2.DataAccess
{
    public class MedicRepository : BaseRepository<Medic>, IMedicRepository
    {
        public MedicRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {

        }

        public Medic GetMedicByID(Guid Id)
        {

            return dbContext.Medics
                            .Where(medic => medic.Id == Id)
                            .FirstOrDefault();
        }

        public Medic GetMedicByUserId(Guid userId)
        {
            return dbContext.Medics
                             .Where(medic => medic.UserId == userId)
                             .SingleOrDefault();
        }

        public Medic GetMedicByLastName(string lastName)
        {
            return dbContext.Medics
                             .Where(medic => medic.LastName == lastName || lastName == null)
                             .SingleOrDefault();
        }

        public IEnumerable<Medic> GetAllMedics()
        {
            return dbContext.Medics
                             .AsEnumerable();
        }
    }
}
