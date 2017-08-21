using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common.Utilities
{
    /* ==============================================================================
     * 描述：TransparentAgent
     * 创建人：李传刚 2017/7/31 15:56:55
     * ==============================================================================
     */
    public class TransparentAgent : MarshalByRefObject
    {
        private const BindingFlags bindFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance;

        public TransparentAgent()
        {

        }

        /// <summary>
        /// 通过透明代理来创建指定程序集中指定类的对象
        /// </summary>
        /// <typeparam name="T">可以为抽象类、接口、具体实现类</typeparam>
        /// <param name="assemblyFile">程序集全路径</param>
        /// <param name="args">构造函数所需的参数集</param>
        /// <returns>返回创建完的T对象</returns>
        public T Create<T>(string assemblyFile, object[] args) where T : class
        {
            Assembly ass = Assembly.LoadFrom(assemblyFile);

            foreach (Type tp in ass.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(tp))
                {
                    object obj = ass.CreateInstance(tp.FullName, false, bindFlags, null, args, System.Globalization.CultureInfo.CurrentCulture, null);
                    //如果一个DLL里存在多个同类型对象，默认返回第一个。所以从编程规范上，约束只能有一个类继承BaseTask；
                    return obj as T;
                }
            }

            return null;
        }
    }
}
