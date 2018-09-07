using System;
using System.Collections.Generic;
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

        public bool Add(ISensor obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISensor> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
