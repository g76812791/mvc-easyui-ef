using System;
using System.IO;
using System.Web.Hosting;

namespace KCollector.Common
{
    public class ErrorLog
    {
        private readonly string _rootFolder;

        public string RootFolder
        {
            get { return _rootFolder; }
        }

        public ErrorLog()
        {
            _rootFolder = HostingEnvironment.ApplicationPhysicalPath;
        }

        protected virtual string GetFolder()
        {
            var now = DateTime.Now;
            var path1 = string.Format("logs/errors/{0}/{1}/", now.Year, now.Month);
            return Path.Combine(RootFolder, path1);
        }

        protected virtual string GetFilePath()
        {
            var now = DateTime.Now;
            var folder = GetFolder();
            var path1 = string.Format("{0}.log", now.Day);
            return Path.Combine(folder, path1);
        }

        protected virtual string GetLogContent(object message)
        {
            return message.ToString() + "\r\n";
        }

        public void Log(object message)
        {
            try
            {
                var folder = GetFolder();
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var file = GetFilePath();
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                }
                File.AppendAllText(file, GetLogContent(message));
            }
            catch (Exception)
            {

            }
        }
    }
}