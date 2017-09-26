using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDI.Utility.Common
{
    public class DFHelper
    {
        private static void getsize(string strpath, ref int total)
        {
            DirectoryInfo dir = new DirectoryInfo(strpath);
            foreach (FileSystemInfo fsi in dir.GetFileSystemInfos())
            {
                if (fsi is FileInfo)
                {
                    FileInfo fi = fsi as FileInfo;
                    total += int.Parse(fi.Length.ToString());
                }
                else
                {
                    DirectoryInfo di = fsi as DirectoryInfo;
                    string newPath = di.FullName;
                    getsize(newPath, ref total);
                }
            }
        }
        //获得文件大小
        public static int GetSize(string strPath)
        {
            int totalSize = 0;
            getsize(strPath, ref totalSize);
            return totalSize;
        }
        public static string ShowSpace(string strPath)
        {
            int iSize = GetSize(strPath);
            string strSize = ShowSpace(iSize);
            return strSize;
        }
        //文件大小单位化
        public static string ShowSpace(int size)
        {
            string strSize;
            float fSize;
            if (size < 1024)
            {
                strSize = string.Format("{0}　{1}", size, "Btye");
                return strSize;
            }
            else
            {
                fSize = size / 1024;
                strSize = string.Format("{0}　{1}", fSize, "KB");
            }
            if (fSize > 1024)
            {
                fSize = fSize / 1024;
                strSize = string.Format("{0}　{1}", fSize, "MB");
            }
            if (fSize > 1024)
            {
                fSize = fSize / 1024;
                strSize = string.Format("{0}　{1}", fSize, "GB");
            }
            return strSize;
        }
    }
}
