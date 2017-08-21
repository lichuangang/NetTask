using IRC.Task.Core.TaskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Repositories
{
    public interface ITaskInfoRepository : IBaseRepository<TaskInfo>
    {
        void Insert(TaskInfo task);
        void Update(TaskInfo task);
        int CountByName(string name, int id = 0);
    }
}
