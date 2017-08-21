using IRC.Task.Applications;
using IRC.Task.Core.Dtos;
using IRC.Task.Core.TaskDb;
using IRC.Task.QueryServices;
using QCommon;
using QCommon.Components;
using QCommon.Entity.Api;
using QCommon.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IRC.Task.Svc.Controllers
{
    public class TaskController : ApiController
    {
        ITaskRunService _taskRunService = ObjectContainer.Resolve<ITaskRunService>();
        TaskQueryService _taskQueryService = ObjectContainer.Resolve<TaskQueryService>();
        ITaskDbService _taskDbService = ObjectContainer.Resolve<ITaskDbService>();

        #region 任务管理
        [HttpGet]
        public ApiResult Run(int settingId)
        {
            _taskRunService.StartTask(settingId);
            return ApiResultFactory.CreateResult("成功");
        }

        [HttpGet]
        public ApiResult Stop(int settingId)
        {
            _taskRunService.StopTask(settingId);
            return ApiResultFactory.CreateResult("成功");
        }
        #endregion

        #region 程序集管理
        [HttpGet]
        public ApiResult GetTask(int page, int pageSize, string name = "")
        {
            var result = _taskQueryService.GetTaskPage(page, pageSize, name);
            return ApiResultFactory.CreateResult(result);
        }

        [HttpGet]
        public ApiResult Tasks()
        {
            var result = _taskQueryService.Tasks();
            return ApiResultFactory.CreateResult(result);
        }

        [HttpPost]
        public ApiResult SaveTask(TaskInfoDto task)
        {
            _taskDbService.SaveTask(task);
            return ApiResultFactory.CreateResult("成功");
        }

        [HttpPost]
        public ApiResult DeleteTask(int id)
        {
            _taskDbService.DelTask(id);
            return ApiResultFactory.CreateResult("成功");
        }
        #endregion

        #region 任务设置管理
        [HttpGet]
        public ApiResult GetSetting(int page, int pageSize, string name = "")
        {
            var result = _taskQueryService.GetSettingPage(page, pageSize, name);
            return ApiResultFactory.CreateResult(result);
        }

        [HttpPost]
        public ApiResult SaveSetting(TaskSetting setting)
        {
            _taskDbService.SaveSetting(setting);
            return ApiResultFactory.CreateResult("成功");
        }

        [HttpPost]
        public ApiResult DeleteSetting(int id)
        {
            _taskDbService.DelSetting(id);
            return ApiResultFactory.CreateResult("成功");
        }
        #endregion
    }
}
