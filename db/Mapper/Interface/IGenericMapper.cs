using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiveways.Insight.Model.Mapper
{
    public interface IGenericMapper<TEntity, TDTO>
    {
        Task<TDTO> ToDTOAsync(TEntity entity);

        Task<TEntity> ToEntityAsync(TDTO entity);
    }
}
