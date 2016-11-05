namespace ABClient
{
    using System;
    using System.IO;
    using Properties;

    internal static class DataManager
    {
        private static string _path;

        internal static string FileMap;
        internal static string DirTemp;

        internal static void Init()
        {
            _path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            _path = Path.Combine(_path, AppVars.AppVersion.Product);
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            FileMap = Path.Combine(_path, Resources.NameFileMap);
            DirTemp = Path.Combine(_path, Resources.NameDirTemp);
            if (!Directory.Exists(DirTemp))
            {
                Directory.CreateDirectory(DirTemp);
            }
        }
    }
}
