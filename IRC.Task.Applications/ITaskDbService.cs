using IRC.Task.Core.Dtos;
using IRC.Task.Core.TaskDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Applications
{
    public interface ITaskDbService
    {
        void SaveTask(TaskInfoDto task);

        void SaveSetting(TaskSetting setting);

        void DelTask(int id);

        void DelSetting(int id);
    }
}
