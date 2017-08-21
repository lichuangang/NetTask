using IRC.Task.Core.TaskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Repositories
{
    public interface ITaskSettingRepository : IBaseRepository<TaskSetting>
    {
        void UpdateStatus(int setId, int status);
        void Update(TaskSetting setting);
        void Insert(TaskSetting setting);
        int CountByTaskInfoId(int id);
        List<TaskSetting> GetListByStatus(params int[] status);
    }
}
