using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Task.Core.Common
{
    /* ==============================================================================
     * 描述：ZipCompressExtention
     * 创建人：李传刚 2017/7/28 14:02:21
     * ==============================================================================
     */
    public static class ZipCompressExtention
    {
        #region 文件压缩方法

        /// <summary>
        /// 对单个文件进行Zip压缩
        /// </summary>
        /// <param name="file">待压缩的文件信息</param>
        /// <param name="zipFileName">压缩后的文件名,文件名必须为全路径,未传入时默认为待压缩文件.zip</param>
        /// <param name="compressionLevel">压缩品质级别（0~9）</param>
        /// <param name="password">压缩时设置的密码</param>
        /// <param name="deleteOriginalFile">压缩完后是否删除原文件</param>
        public static void ZipFileCompress(this FileInfo file, string zipFileName, int compressionLevel = 1, string password = null, bool deleteOriginalFile = false)
        {

            if (file == null)
                throw new ArgumentNullException("file");

            if (string.IsNullOrEmpty(zipFileName))
            {
                zipFileName = Path.Combine(Path.GetDirectoryName(file.FullName), Path.GetFileNameWithoutExtension(file.FullName) + ".zip");
            }

            if (Path.GetExtension(zipFileName).ToLower() != ".zip")
            {
                zipFileName = Path.GetFileNameWithoutExtension(zipFileName) + ".zip";
            }

            if (!Directory.Exists(Path.GetDirectoryName(zipFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(zipFileName));
            }

            ZipOutputStream outStream = null;
            FileStream readStream = null;
            bool success = false;

            try
            {

                outStream = new ZipOutputStream(File.Create(zipFileName));
                readStream = file.OpenRead();

                ZipEntry zipEntry = new ZipEntry(file.Name);
                zipEntry.DateTime = file.CreationTime > file.LastWriteTime ? file.LastWriteTime : file.CreationTime;
                outStream.SetLevel(compressionLevel);
                if (!string.IsNullOrEmpty(password))
                {
                    outStream.Password = password;
                }
                outStream.PutNextEntry(zipEntry);

                int size = 2048;
                byte[] data = new byte[size];
                while (true)
                {
                    size = readStream.Read(data, 0, size);
                    if (size <= 0) break;
                    outStream.Write(data, 0, size);
                }

                success = true;
            }
            finally
            {
                if (readStream != null)
                {
                    readStream.Close();
                    readStream.Dispose();

                    if (success && deleteOriginalFile)
                    {
                        file.Delete();
                    }
                }

                if (outStream != null)
                {
                    outStream.Finish();
                    outStream.Close();
                    outStream.Dispose();
                }
            }
        }

        /// <summary>
        /// 对单个文件进行Zip压缩
        /// </summary>
        /// <param name="file">待压缩的文件全路径</param>
        /// <param name="zipFileName">压缩后的文件名,文件名必须为全路径,未传入时默认为待压缩文件.zip</param>
        /// <param name="compressionLevel">压缩品质级别（0~9）</param>
        /// <param name="password">压缩时设置的密码</param>
        /// <param name="deleteOriginalFile">压缩完后是否删除原文件</param>
        public static void ZipFileCompress(string fileName, string zipFileName, int compressionLevel = 1, string password = null, bool deleteOriginalFile = false)
        {
            ZipFileCompress(new FileInfo(fileName), zipFileName, compressionLevel, password, deleteOriginalFile);
        }

        /// <summary>
        /// 对单个文件进行Zip压缩
        /// </summary>
        /// <param name="file">待压缩的多个文件信息列表</param>
        /// <param name="zipFileName">压缩后的文件名,文件名必须为全路径,未传入时默认为待压缩文件.zip</param>
        /// <param name="compressionLevel">压缩品质级别（0~9）</param>
        /// <param name="password">压缩时设置的密码</param>
        /// <param name="deleteOriginalFile">压缩完后是否删除原文件</param>
        public static void ZipFileCompress(this List<FileInfo> files, string zipFileName, int compressionLevel = 1, string password = null, bool deleteOriginalFile = false)
        {
            if (files == null || files.Count == 0)
                throw new ArgumentNullException("files");

            if (string.IsNullOrEmpty(zipFileName))
                throw new ArgumentNullException(zipFileName);

            if (Path.GetExtension(zipFileName).ToLower() != ".zip")
            {
                zipFileName = Path.GetFileNameWithoutExtension(zipFileName) + ".zip";
            }

            if (!Directory.Exists(Path.GetDirectoryName(zipFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(zipFileName));
            }

            ZipOutputStream outStream = null;
            FileStream readStream = null;
            bool success = true;

            try
            {
                outStream = new ZipOutputStream(File.Create(zipFileName));
                outStream.SetLevel(compressionLevel);
                if (!string.IsNullOrEmpty(password))
                {
                    outStream.Password = password;
                }

                foreach (var file in files)
                {
                    try
                    {
                        readStream = file.OpenRead();

                        ZipEntry zipEntry = new ZipEntry(file.Name);
                        zipEntry.DateTime = file.CreationTime > file.LastWriteTime ? file.LastWriteTime : file.CreationTime;
                        outStream.PutNextEntry(zipEntry);

                        int size = 2048;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = readStream.Read(data, 0, size);
                            if (size <= 0) break;
                            outStream.Write(data, 0, size);
                        }
                    }
                    catch (Exception ex)
                    {
                        success = false;
                    }
                    finally
                    {
                        if (readStream != null)
                        {
                            readStream.Close();
                            readStream.Dispose();
                        }
                    }
                }
            }
            finally
            {
                if (outStream != null)
                {
                    outStream.Finish();
                    outStream.Close();
                    outStream.Dispose();
                }

                //全部都压缩成功后才进行删除原文件
                if (success && deleteOriginalFile)
                {
                    foreach (var item in files)
                    {
                        try
                        {
                            item.Delete();
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
        }

        #endregion

        #region 目录压缩方法

        /// <summary>
        /// 对指定目录进行Zip压缩
        /// </summary>
        /// <param name="dire">待压缩的目录信息</param>
        /// <param name="zipFileName">压缩后的文件名,文件名必须为全路径,未传入时默认为待压缩目录.zip</param>
        /// <param name="compressionLevel">压缩品质级别（0~9）</param>
        /// <param name="password">压缩时设置的密码</param>
        /// <param name="deleteOriginalDire">压缩完后是否删除原文件</param>
        public static void ZipDirectoryCompress(this DirectoryInfo dire, string zipFileName, int compressionLevel = 1, string password = null, bool deleteOriginalDire = false)
        {
            if (!dire.Exists)
                throw new DirectoryNotFoundException(dire.FullName);

            if (string.IsNullOrEmpty(zipFileName))
            {
                var direName = !dire.FullName.EndsWith("\\") ? Path.GetDirectoryName(dire.FullName) : Path.GetDirectoryName(Path.GetDirectoryName(dire.FullName));
                zipFileName = direName + "\\" + dire.Name + ".zip";
            }

            if (Path.GetExtension(zipFileName).ToLower() != ".zip")
            {
                zipFileName += ".zip";
            }

            using (ZipOutputStream outputStream = new ZipOutputStream(File.Create(zipFileName)))
            {

                outputStream.SetLevel(compressionLevel);
                if (!string.IsNullOrEmpty(password))
                {
                    outputStream.Password = password;
                }

                Crc32 crc = new Crc32();

                var files = GetAllFiles(dire);
                foreach (var file in files)
                {

                    FileStream fs = File.OpenRead(file.Key);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);


                    ZipEntry entry = new ZipEntry(file.Key.Substring(dire.FullName.EndsWith("\\") ? dire.FullName.Length : dire.FullName.Length + 1));
                    entry.DateTime = file.Value;
                    entry.Size = fs.Length;
                    fs.Close();
                    fs.Dispose();


                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    outputStream.PutNextEntry(entry);
                    outputStream.Write(buffer, 0, buffer.Length);
                }

            }

            if (deleteOriginalDire)
            {
                try
                {
                    dire.Delete(true);
                }
                catch (Exception ex)
                {

                }
            }

        }

        /// <summary>
        /// 对指定目录进行Zip压缩
        /// </summary>
        /// <param name="dire">待压缩的目录名称</param>
        /// <param name="zipFileName">压缩后的文件名,文件名必须为全路径,未传入时默认为待压缩目录.zip</param>
        /// <param name="compressionLevel">压缩品质级别（0~9）</param>
        /// <param name="password">压缩时设置的密码</param>
        /// <param name="deleteOriginalDire">压缩完后是否删除原文件</param>
        public static void ZipDirectoryCompress(string dirPath, string zipFileName, int compressionLevel = 1, string password = null, bool deleteOriginalDire = false)
        {
            ZipDirectoryCompress(new DirectoryInfo(dirPath), zipFileName, compressionLevel, password, deleteOriginalDire);
        }

        #endregion

        #region 解压方法


        /// <summary>
        /// 对Zip压缩文件进行进行解压
        /// </summary>
        /// <param name="zipedFileName">已压缩包的文件名称</param>
        /// <param name="targetPath">解压缩目标路径,为空时默认同压缩包相同路径</param>
        /// <param name="password">解压密码</param>
        /// <param name="deleteOriginalZipedFile">解压成功后是否删除原压缩文件包</param>
        public static void ZipDecompress(string zipedFileName, string targetPath, string password = null, bool deleteOriginalZipedFile = false)
        {
            if (!File.Exists(zipedFileName))
                throw new FileNotFoundException(zipedFileName);

            if (string.IsNullOrEmpty(targetPath))
            {
                targetPath = Path.Combine(Path.GetDirectoryName(zipedFileName), Path.GetFileNameWithoutExtension(zipedFileName));
            }

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            int size = 2048;
            byte[] data = new byte[size];
            ZipEntry zEntry = null;

            using (ZipInputStream inputStream = new ZipInputStream(File.OpenRead(zipedFileName)))
            {
                if (!string.IsNullOrEmpty(password))
                {
                    inputStream.Password = password;
                }

                while ((zEntry = inputStream.GetNextEntry()) != null)
                {
                    //目录
                    if (zEntry.IsDirectory)
                    {
                        if (!Directory.Exists(targetPath + zEntry.Name))
                        {
                            Directory.CreateDirectory(targetPath + zEntry.Name);
                        }
                    }
                    //文件
                    else
                    {
                        if (!string.IsNullOrEmpty(zEntry.Name))
                        {
                            var fileFullName = Path.Combine(targetPath, zEntry.Name);
                            if (!Directory.Exists(Path.GetDirectoryName(fileFullName)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(fileFullName));
                            }

                            using (FileStream writerStream = File.Create(Path.Combine(targetPath, zEntry.Name)))
                            {
                                while (true)
                                {
                                    size = inputStream.Read(data, 0, data.Length);
                                    if (size <= 0) break;

                                    writerStream.Write(data, 0, size);
                                }
                                writerStream.Close();
                            }
                        }

                    }
                }

                inputStream.Close();
            }

            if (deleteOriginalZipedFile)
            {
                try
                {
                    File.Delete(zipedFileName);
                }
                catch (Exception ex)
                {

                }

            }
        }

        /// <summary>
        /// 对压缩的文件流进行解压
        /// </summary>
        /// <param name="inputStream">已压缩包的文件流</param>
        /// <param name="targetPath">解压缩目标路径,必须指明</param>
        /// <param name="password">解压密码</param>
        public static void ZipDecompress(Stream stream, string targetPath, string password = null)
        {

            if (!Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }

            int size = 2048;
            byte[] data = new byte[size];
            ZipEntry zEntry = null;

            using (ZipInputStream inputStream = new ZipInputStream(stream))
            {
                if (!string.IsNullOrEmpty(password))
                {
                    inputStream.Password = password;
                }

                while ((zEntry = inputStream.GetNextEntry()) != null)
                {
                    //目录
                    if (zEntry.IsDirectory)
                    {
                        if (!Directory.Exists(targetPath + zEntry.Name))
                        {
                            Directory.CreateDirectory(targetPath + zEntry.Name);
                        }
                    }
                    //文件
                    else
                    {
                        if (!string.IsNullOrEmpty(zEntry.Name))
                        {
                            var fileFullName = Path.Combine(targetPath, zEntry.Name);
                            if (!Directory.Exists(Path.GetDirectoryName(fileFullName)))
                            {
                                Directory.CreateDirectory(Path.GetDirectoryName(fileFullName));
                            }

                            using (FileStream writerStream = File.Create(Path.Combine(targetPath, zEntry.Name)))
                            {
                                while (true)
                                {
                                    size = inputStream.Read(data, 0, data.Length);
                                    if (size <= 0) break;

                                    writerStream.Write(data, 0, size);
                                }
                                writerStream.Close();
                            }
                        }

                    }
                }

                inputStream.Close();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取指定目录下所有的文件信息
        /// </summary>
        /// <param name="dirInfo"></param>
        /// <returns></returns>
        private static Dictionary<string, DateTime> GetAllFiles(DirectoryInfo dirInfo)
        {
            Dictionary<string, DateTime> rtnValue = new Dictionary<string, DateTime>();

            foreach (var item in dirInfo.GetFiles("*.*"))
            {
                rtnValue.Add(item.FullName, item.CreationTime > item.LastWriteTime ? item.CreationTime : item.LastWriteTime);
            }

            foreach (var item in dirInfo.GetDirectories())
            {
                var subDirctionary = GetAllFiles(item);
                if (subDirctionary != null && subDirctionary.Count > 0)
                {
                    rtnValue = (rtnValue.Union(subDirctionary)).ToDictionary(pair => pair.Key, pair => pair.Value);
                }
            }

            return rtnValue;
        }

        #endregion
    }
}
