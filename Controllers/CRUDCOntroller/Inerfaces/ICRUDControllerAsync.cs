using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.CRUDCOntroller.Inerfaces
{
    public interface ICRUDControllerAsync<TEntity>
    {
        Task AddAsync(TEntity entity);

        Task UpdateAsync(Guid id, TEntity newEntity, int bitmask);

        Task RemoveAsync(Guid id);

        Task GetAllAsync();

        Task GetAsync(Guid id);
       
        Task<Guid> GenerateIdAsync();
    }
}
