using System.Threading.Tasks;
using QCommon.Cache;
using QCommon.Components;
using QCommon.Logging;
using QCommon.Utilities;

namespace TagBooksTask
{
    public class SecUtils
    {
        private static readonly ILogger _logger = ObjectContainer.Resolve<ILoggerFactory>().Create(typeof(SecUtils).FullName);
        private static ICacheProvider _cacheProvider = ObjectContainer.ResolveNamed<ICacheProvider>(CacheConsts.Redis);

        public static void ClearUserCache(string id)
        {
            Task.Factory.StartNew(() =>
            {
                string body = HttpUtils.Get("http://sec.ireadercity.com/General/NotiUpdate", new
                {
                    BizFunc = "GetIreaderUser",
                    Id = id
                });
                _logger.Info(body);

                var key = string.Format("GoodBooksWeb_BookQueryService_FindByBookId_{0}", id);
                _cacheProvider.Remove(key);
            });
        }
    }
}