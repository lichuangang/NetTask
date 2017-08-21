using IRC.Task.Core.Common;
using IRC.Task.Core.Dtos;
using IRC.Task.Core.TaskDb;
using IRC.Task.Repositories;
using QCommon.Components;
using QCommon.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Applications.Impl
{
    /* ==============================================================================
     * 描述：TaskDbService
     * 创建人：李传刚 2017/7/28 13:49:46
     * ==============================================================================
     */
    [Component]
    public class TaskDbService : ITaskDbService
    {
        ITaskInfoRepository _taskInfoRsy = ObjectContainer.Resolve<ITaskInfoRepository>();
        ITaskSettingRepository _taskSettingRsy = ObjectContainer.Resolve<ITaskSettingRepository>();
        public void SaveTask(TaskInfoDto task)
        {
            if (_taskInfoRsy.CountByName(task.TaskName, task.Id) > 0)
            {
                throw new BusinessException("任务重名");
            }

            if (task.UpdateFile)
            {
                MoveFile(task, task.Id == 0 ? true : task.UpdateConfig);
            }

            if (task.Id == 0)
            {

                _taskInfoRsy.Insert(task);
            }
            else
            {
                _taskInfoRsy.Update(task);
            }
        }

        private void MoveFile(TaskInfo task, bool replaceConfig = true)
        {
            string zipPath = Path.Combine(ResourceFilePath.TempPath, task.FileName);

            if (File.Exists(zipPath))
            {
                ZipCompressExtention.ZipDecompress(zipPath, Path.Combine(ResourceFilePath.TempPath, task.TaskName));
            }

            var taskAssemblyPath = Path.Combine(ResourceFilePath.TempPath, task.TaskName, task.TaskAssemblyName);
            if (!File.Exists(taskAssemblyPath))
            {
                throw new BusinessException("程序集匹配有误，约定(文件夹，文件名，任务名称必需一致)。请重新上传程序集", "500");
            }
            //不更新config文件
            if (false == replaceConfig)
            {
                var configFullName = taskAssemblyPath + ".config";
                var orgConfigFullName = Path.Combine(ResourceFilePath.TaskRun, task.TaskName, task.TaskAssemblyName) + ".config";
                if (File.Exists(orgConfigFullName))
                {
                    File.Delete(configFullName);
                    File.Move(orgConfigFullName, configFullName);
                }
            }

            var runPath = Path.Combine(ResourceFilePath.TaskRun, task.TaskName);
            if (!Directory.Exists(ResourceFilePath.TaskRun))
            {
                //根目录创建
                Directory.CreateDirectory(ResourceFilePath.TaskRun);
            }
            if (Directory.Exists(runPath))
            {
                //如果原来有这个目录，先删除
                Directory.Delete(runPath, true);
            }

            Directory.Move(Path.Combine(ResourceFilePath.TempPath, task.TaskName), runPath);
        }

        public void SaveSetting(TaskSetting setting)
        {
            if (setting.Id != 0)
            {
                var orgSetting = _taskSettingRsy.GetById(setting.Id);
                if (orgSetting == null)
                {
                    throw new BusinessException("任务可能已经被删除,请刷新页面重新操作.");
                }

                if (orgSetting.Status != 0)
                {
                    throw new BusinessException("任务正在运行中,请先停止任务,再执行操作.");
                }

                _taskSettingRsy.Update(setting);
            }
            else
            {
                _taskSettingRsy.Insert(setting);
            }
        }

        public void DelTask(int id)
        {
            var info = _taskInfoRsy.GetById(id);
            if (_taskSettingRsy.CountByTaskInfoId(id) > 0)
            {
                throw new BusinessException("程序集被任务引用，请先删除任务。");
            }
            var runPath = Path.Combine(ResourceFilePath.TaskRun, info.TaskName);
            if (Directory.Exists(runPath))
            {
                //删除运行目录中的文件
                Directory.Delete(runPath, true);
            }
            _taskInfoRsy.DeleteById(id);
        }

        public void DelSetting(int id)
        {
            var info = _taskSettingRsy.GetById(id);
            if (info.Status != 0)
            {
                throw new BusinessException("当前任务还在运行中，请先停止任务再执行删除。");
            }
            _taskSettingRsy.DeleteById(id);
        }
    }
}
