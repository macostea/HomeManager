using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeManager.Common.Models;

namespace HomeManager.Common.Repository
{
    public class SensorRepository : IRepository<ISensor>
    {
        private readonly IDBContext db;

        public SensorRepository(IDBContext db)
        {
            this.db = db;
        }

        public Task<bool> Add(ISensor obj)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ISensor>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
