using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class MedicationTypeRepository : BaseRepository<MedicationType>, IMedicationTypeRepository
    {
        public MedicationTypeRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {


        }
        public MedicationType GetMedicationById(Guid Id)
        {
            return dbContext.MedicationTypes
                            .Where(medication => medication.Id == Id)
                            .Include(medication => medication.Medication_Treatments)
                            .SingleOrDefault();
        }
    }
}
