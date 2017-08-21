using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common.Utilities
{
    /* ==============================================================================
     * 描述：AppDomainObjectProxyFactory
     * 创建人：李传刚 2017/7/31 15:56:21
     * ==============================================================================
     */
    public class AppDomainObjectProxyFactory<T> : IDisposable where T : class
    {
        #region  字段

        private string assemblyFile;

        private AppDomain currentDomain;

        private TransparentAgent factory = null;

        private object lockObj = new object();

        #endregion

        #region 属性

        /// <summary>
        /// 获取新建的独立应用程序域
        /// </summary>
        public AppDomain CurrentDomain
        {
            get { return currentDomain; }
        }

        private T proxy = default(T);

        /// <summary>
        /// 获取最主要的远程代理对象
        /// </summary>
        public T Proxy
        {
            get
            {
                if (proxy == null)
                {
                    this.proxy = this.CreateProxy<T>();
                }

                return proxy;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 传入主程序集所在路径,程序集中必须包括T的实现
        /// </summary>
        /// <param name="assemblyFile"></param>
        public AppDomainObjectProxyFactory(string assemblyFile)
        {
            this.assemblyFile = assemblyFile;
            CreateIndependentAppDomain();
        }

        /// <summary>
        /// 创建新的应用程序域和透明代理对象
        /// </summary>
        private void CreateIndependentAppDomain()
        {
            if (currentDomain == null)
            {
                lock (lockObj)
                {
                    if (currentDomain == null)
                    {
                        AppDomainSetup setup = new AppDomainSetup();
                        setup.ApplicationName = "TaskHostLoader";
                        var binPath = Path.GetDirectoryName(this.assemblyFile);
                        setup.ApplicationBase = binPath;   //将应用程序域指定至加载程序集的路径下
                        setup.PrivateBinPath = binPath;

                        // 配置文件
                        var configFile = this.assemblyFile + ".config";
                        if (File.Exists(configFile))
                            setup.ConfigurationFile = configFile;

                        // 影复制路径
                        setup.CachePath = Path.GetTempPath();
                        setup.ShadowCopyFiles = "true";

                        setup.ShadowCopyDirectories = binPath;

                        //创建跨域对象 透明代理
                        currentDomain = AppDomain.CreateDomain("TaskHostLoaderDomain", null, setup);

                        string currentAssemblyName = Assembly.GetExecutingAssembly().GetName().FullName;
                        currentAssemblyName = this.GetType().Assembly.FullName;

                        //通过透明代理对象来进行反射其他远程对象的创建
                        this.factory = currentDomain.CreateInstanceAndUnwrap(currentAssemblyName, typeof(TransparentAgent).FullName) as TransparentAgent;
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Unload();
        }

        #endregion

        #region 创建对象的公开方法

        /// <summary>
        /// 根据主程序集创建该程序集中指定为A类型的对象
        /// </summary>
        /// <typeparam name="A">准备创建的对象所属类型</typeparam>
        /// <returns>返回创建成功的对象</returns>
        public A CreateProxy<A>() where A : class
        {
            return this.CreateProxy<A>(this.assemblyFile, null);
        }

        /// <summary>
        /// 根据主程序集创建该程序集中指定为A类型的对象
        /// </summary>
        /// <typeparam name="A">准备创建的对象所属类型</typeparam>
        /// <returns>返回创建成功的对象</returns>
        public A CreateProxy<A>(object[] args) where A : class
        {
            return this.CreateProxy<A>(this.assemblyFile, args);
        }

        /// <summary>
        /// 根据指定程序集来创建指定为A类型的对象
        /// </summary>
        /// <typeparam name="A">准备创建的对象所属类型,该类型必须包括在assemblyFile对应程序集中</typeparam>
        /// <param name="assemblyFile">包括A类型的程序集全路径</param>
        /// <returns>返回创建成功的对象</returns>
        public A CreateProxy<A>(string assemblyFile) where A : class
        {

            return factory.Create<A>(assemblyFile, null);
        }

        /// <summary>
        /// 根据指定程序集来创建指定为A类型的对象
        /// </summary>
        /// <typeparam name="A">准备创建的对象所属类型,该类型必须包括在assemblyFile对应程序集中</typeparam>
        /// <param name="assemblyFile">包括A类型的程序集全路径</param>
        /// <param name="args">创建A对象时构造函数所需的参数</param>
        /// <returns>返回创建成功的对象</returns>
        public A CreateProxy<A>(string assemblyFile, object[] args) where A : class
        {
            try
            {
                return factory.Create<A>(assemblyFile, args);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region  私有方法


        /// <summary>
        /// 卸载应用程序域
        /// </summary>
        private void Unload()
        {
            try
            {
                if (currentDomain != null)
                {
                    AppDomain.Unload(currentDomain);
                    currentDomain = null;
                }
            }
            catch (Exception)
            {

            }
        }

        #endregion
    }
}
