using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceStack;
using Squirrel;

namespace $safeprojectname$
{
    public static class SquirrelHelpers
    {
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
                using (var mgr = new UpdateManager(deployUrl))
                {
                    var updateInfo = await mgr.CheckForUpdate();
                    if (updateInfo.ReleasesToApply.Count > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                // Update failed. 
            }
            return false;
        }

        public static async Task<ReleaseEntry> ApplyUpdates(string deployUrl)
        {
            using (var manager = new UpdateManager(deployUrl))
            {
                var result = await manager.UpdateApp();
                return result;
            }
        }
    }
}
