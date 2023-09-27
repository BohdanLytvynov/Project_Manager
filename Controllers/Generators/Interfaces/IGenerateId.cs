using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers.Generators.Interfaces
{
    public interface IGenerateId<TDbContextType>
        where TDbContextType : DbContext
    {
        Guid Generate(TDbContextType dbContext);
    }
}
