using QCommon.Components;
using QCommon.Entity;
using QCommon.ThirdParty.Dapper;
using QCommon.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Repositories
{
    /* ==============================================================================
     * 描述：TaskDbRepository
     * 创建人：李传刚 2017/7/26 9:45:22
     * ==============================================================================
     */
    public class TaskDbRepository<T> : BaseRepository, IBaseRepository<T> where T : IdKey<int>
    {
        public TaskDbRepository()
            : this(ObjectContainer.Resolve<IRepositoryContext>())
        {

        }

        public TaskDbRepository(IRepositoryContext context) : base(context) { }


        private static string TabName
        {
            get
            {
                return typeof(T).Name;
            }
        }

        private static string getAllSql = "SELECT * FROM {0}";

        private static string getByIdSql = "SELECT TOP 1 * FROM {0} WHERE Id = {1}";

        private static string deleteByIdSql = "DELETE FROM {0} WHERE Id = {1}";

        public List<T> GetAll()
        {
            var sql = string.Format(getAllSql, TabName);
            return QueryList<T>(sql, null).ToList();
        }

        public T GetById(int id)
        {
            var sql = string.Format(getByIdSql, TabName, id);
            return QueryList<T>(sql, null).FirstOrDefault();
        }

        public int DeleteById(int id)
        {
            var sql = string.Format(deleteByIdSql, TabName, id);
            return Execute(sql, null);
        }
    }
}
