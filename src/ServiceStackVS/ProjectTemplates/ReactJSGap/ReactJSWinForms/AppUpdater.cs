using System;
using System.IO;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Configuration;
using Squirrel;

namespace $safeprojectname$
{
    public static class AppUpdater
    {
        private static UpdateManager _updateManagerInstance;

        public static UpdateManager AppUpdateManager
        {
            get
            {
                if (_updateManagerInstance != null)
                {
                    return _updateManagerInstance;
                }

                var appSettings = new AppSettings();
                var updateManagerTask =
                    UpdateManager.GitHubUpdateManager(appSettings.GetString("UpdateManagerUrl"));
                updateManagerTask.Wait(TimeSpan.FromMinutes(1));
                _updateManagerInstance = updateManagerTask.Result;
                return _updateManagerInstance;
            }
        }

        public static void Dispose()
        {
            if (_updateManagerInstance != null)
            {
                _updateManagerInstance.Dispose();
            }
        }

        public static async Task<bool> CheckForUpdates(string deployUrl)
        {
#if DEBUG
            // Copy Squirrel.exe and rename to Update.exe for debugging, 
            // see https://github.com/Squirrel/Squirrel.Windows/blob/master/docs/using/debugging-updates.md#updateexe-not-found
            if (!File.Exists("..\\Update.exe"))
            {
                File.Copy(
                    "..\\..\\..\\..\\..\\packages\\squirrel.windows.1.2.5\\tools\\Squirrel.exe".MapHostAbsolutePath(),
                    "..\\Update.exe");
            }
#endif
            try
            {
                var updateInfo = await AppUpdateManager.CheckForUpdate();
                if (updateInfo.ReleasesToApply.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // Update failed. 
            }
            return false;
        }

        public static async Task<ReleaseEntry> ApplyUpdates(string deployUrl)
        {
            var result = await AppUpdateManager.UpdateApp();
            return result;
        }

        public static void CreateShortcutForThisExe()
        {
            AppUpdateManager.CreateShortcutForThisExe();
        }

        public static void RemoveShortcutForThisExe()
        {
            AppUpdateManager.RemoveShortcutForThisExe();
        }
    }
}
