using QCommon.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Repositories
{
    /* ==============================================================================
     * 描述：IBaseRepository
     * 创建人：李传刚 2017/7/26 10:04:29
     * ==============================================================================
     */
    public interface IBaseRepository<T> where T : IdKey<int>
    {
        List<T> GetAll();

        T GetById(int id);

        int DeleteById(int id);
    }
}
