using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task.Common;

namespace TaskDemo1
{
    public class TaskOne : BaseTask
    {
        public override void Process()
        {
            Console.WriteLine("开始执行：TaskOne");
            try
            {
                string path = @"C:\Users\Administrator\Desktop";
                //在运行目录生成10个文件
                for (int i = 0; i < 10; i++)
                {
                    string fileFullName = Path.Combine(path, i + ".txt");
                    if (File.Exists(fileFullName))
                    {
                        continue;
                    }
                    File.Create(fileFullName);
                    Console.WriteLine("创建文件：" + i + ".txt");
                    //暂停2秒再执行下一个循环
                    Thread.Sleep(2000);
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
