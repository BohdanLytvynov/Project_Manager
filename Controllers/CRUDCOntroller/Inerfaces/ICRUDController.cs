using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.CRUDCOntroller.Inerfaces
{
    public interface ICRUDController<TEntity>
    {
        void Add(TEntity entity);

        void Update(Guid id, TEntity newEntity);
    }
}
