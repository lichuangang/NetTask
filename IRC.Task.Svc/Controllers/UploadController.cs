using IRC.Task.Core.Common;
using QCommon;
using QCommon.Entity.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace IRC.Task.Svc.Controllers
{
    public class UploadController : ApiController
    {
        /// <summary>
        /// 文件上传
        /// </summary>
        [HttpPost]
        public ApiResult File()
        {
            HttpFileCollection fileColl = HttpContext.Current.Request.Files;
            List<string> files = new List<string>();
            foreach (var fileName in fileColl.AllKeys)
            {
                if (!Directory.Exists(ResourceFilePath.TempPath))
                {
                    Directory.CreateDirectory(ResourceFilePath.TempPath);
                }
                var orgFileName = fileColl[fileName].FileName;
                string fileFullName = Path.Combine(ResourceFilePath.TempPath, orgFileName);
                //文件存在，先删除
                if (System.IO.File.Exists(fileFullName))
                {
                    System.IO.File.Delete(fileFullName);
                }
                fileColl[fileName].SaveAs(fileFullName);
                files.Add(orgFileName);
            }
            return ApiResultFactory.CreateResult(files);
        }


        /// <summary>
        /// 图片
        /// </summary>
        [HttpPost]
        public ApiResult Image(int maxWidth = 0, int maxHeight = 0)
        {
            return null;
        }
    }
}
