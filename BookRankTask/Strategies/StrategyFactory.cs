using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BookRankTask.Models.Enums;

namespace BookRankTask.Strategies
{
    public class StrategyFactory
    {
        //实例存储集合
        private static readonly Dictionary<BigTagType, IStrategy> _dictionary = new Dictionary<BigTagType, IStrategy>();
        static StrategyFactory()
        {
            //获取当前正在执行的程序集
            var assembly = Assembly.GetExecutingAssembly();
            //获取要操作的基类并且包含接口
            var types = assembly.GetTypes().Where(s => s.GetInterface("IStrategy") != null && !s.IsAbstract).ToArray();
            foreach (BigTagType item in Enum.GetValues(typeof(BigTagType)))
            {
                var type = types.First(o => o.Name.Contains(string.Format("{0}{1}", item, "Strategy")));
                //将要使用的操作的实例放到字典集合里（统一格式命名的操作类）
                _dictionary[item] = (IStrategy)Activator.CreateInstance(type, true);
            }
        }
        /// <summary>
        /// 获取要使用的实例.
        /// </summary>
        /// <param name="bigTagType"></param>
        /// <returns></returns>
        public static IStrategy GetStrategy(BigTagType bigTagType)
        {
            return _dictionary[bigTagType];
        }
    }
}
