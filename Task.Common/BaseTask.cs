using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common
{
    /* ==============================================================================
     * 描述：BaseTask
     * 创建人：李传刚 2017/7/26 14:27:00
     * ==============================================================================
     */
    public abstract class BaseTask : MarshalByRefObject
    {
        public abstract void Process();

        #region 重写MarshalByRefObject成员

        public override object InitializeLifetimeService()
        {
            return null;
        }

        #endregion
    }

}
