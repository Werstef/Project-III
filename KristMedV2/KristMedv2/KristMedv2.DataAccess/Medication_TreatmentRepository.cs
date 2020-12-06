using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class Medication_TreatmentRepository : BaseRepository<Medication_Treatment>, IMedication_TreatmentRepository
    {
        public Medication_TreatmentRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {


        }
    }
}
