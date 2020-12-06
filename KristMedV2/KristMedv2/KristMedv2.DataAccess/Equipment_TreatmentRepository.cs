using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class Equipment_TreatmentRepository : BaseRepository<Equipment_Treatment>, IEquipment_TreatmentRepository
    {
        public Equipment_TreatmentRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {


        }
    }
}
