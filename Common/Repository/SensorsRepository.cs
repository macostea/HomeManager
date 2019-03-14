using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Domain.Entities;

namespace Common.Repository
{
    public class SensorsRepository<T>: IRepository<T> where T : EntityBase
    {
        private readonly DBController dbController;

        public SensorsRepository(DBController dbController)
        {
            this.dbController = dbController;
        }

        public async Task<bool> Add(T obj)
        {
            var res = await this.dbController.Connection.InsertAsync<T>(obj);
            return res != 0;
        }

        public async Task<bool> Delete(T entity)
        {
            var res = await this.dbController.Connection.DeleteAsync<T>(entity);
            return res;
        }

        public async Task<bool> Edit(T entity)
        {
            var res = await this.dbController.Connection.UpdateAsync<T>(entity);
            return res;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await this.dbController.Connection.GetAllAsync<T>();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(int id)
        {
            return await this.dbController.Connection.GetAsync<T>(id);
        }
    }
}
