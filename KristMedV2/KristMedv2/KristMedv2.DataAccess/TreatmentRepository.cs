using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class TreatmentRepository : BaseRepository<Treatment>, ITreatmentRepository
    {
        public TreatmentRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {

        }

        public Treatment GetTreatmentByID(Guid Id)
        {

            var treatment_value = dbContext.Treatments
                            .Where(treatment => treatment.Id == Id)
                            .Include(treatment => treatment.Medic)
                            .Include(treatment => treatment.Medications_Treatment)
                            .Include(treatment => treatment.Client)
                            .Include(treatment => treatment.Equipments_Treatment)
                            .FirstOrDefault();
            return treatment_value;
        }
    }
}
