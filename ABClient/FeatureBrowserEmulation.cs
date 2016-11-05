using System;
using System.Diagnostics;
using System.Security;
using Microsoft.Win32;

namespace ABClient
{
    public static class FeatureBrowserEmulation
    {
        internal static void ChangeMode()
        {
            const string key64 = @"Software\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            const string key32 = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
            string appname = Process.GetCurrentProcess().ProcessName + ".exe";

            // Webpages are displayed in IE9 Standards mode, regardless of the !DOCTYPE directive.
            const int browserEmulationMode = 11001;
            //const int browserEmulationMode = 8888;

            try
            {
                var browserEmulationKey =
                    Registry.CurrentUser.OpenSubKey(key64, RegistryKeyPermissionCheck.ReadWriteSubTree) ??
                    Registry.CurrentUser.CreateSubKey(key64);

                if (browserEmulationKey != null)
                {
                    browserEmulationKey.SetValue(appname, browserEmulationMode, RegistryValueKind.DWord);
                    browserEmulationKey.Close();
                }
            }
            catch
            {
            }

            try
            {
                var browserEmulationKey =
                    Registry.CurrentUser.OpenSubKey(key32, RegistryKeyPermissionCheck.ReadWriteSubTree) ??
                    Registry.CurrentUser.CreateSubKey(key32);

                if (browserEmulationKey != null)
                {
                    browserEmulationKey.SetValue(appname, browserEmulationMode, RegistryValueKind.DWord);
                    browserEmulationKey.Close();
                }
            }
            catch
            {
            }
        }
    }
}
