using KristMedv2.ApplicationLogic.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace KristMedv2.ApplicationLogic.Abstractions
{
    public interface IMedicRepository : IRepository<Medic>
    {
        public Medic GetMedicByID(Guid Id);

        public Medic GetMedicByLastName(string lastName);

        public Medic GetMedicByUserId(Guid userId);

        public IEnumerable<Medic> GetAllMedics();
    }
}
