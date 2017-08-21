using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Common.Utilities
{
    /* ==============================================================================
     * 描述：ILoggerFactory
     * 创建人：李传刚 2017/7/31 16:35:37
     * ==============================================================================
     */
    public interface ILoggerFactory
    {
        ILog Create(string name);

        ILog Create(Type type);
    }
}
