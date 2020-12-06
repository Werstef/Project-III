using KristMedv2.ApplicationLogic.Abstractions;
using KristMedv2.ApplicationLogic.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KristMedv2.DataAccess
{
    public class EquipmentTypeRepository : BaseRepository<EquipmentType>, IEquipmentTypeRepository
    {
        public EquipmentTypeRepository(KristMedv2DbContext dbContext) : base(dbContext)
        {

        }

        public EquipmentType GetEquipmentById(Guid Id)
        {
            return dbContext.EquipmentTypes
                            .Where(equipment => equipment.Id == Id)
                            .Include(equipment => equipment.Equipment_Treatments)
                            .SingleOrDefault();
        }
    }
}
