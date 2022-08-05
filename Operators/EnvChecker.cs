using Microsoft.Win32;

namespace LangBox.Operators
{
    internal class EnvChecker
    {
        public const string NOTFOUND = "NOTFOUND";
        private static string vscPath = NOTFOUND;

        public static bool Check(string Name)
        {
            return Name switch
            {
                "C_CPP" => CheckGcc(),
                "Python" => CheckPip(),
                "Java" => CheckJava(),
                _ => false,
            };
        }

        private static bool CheckGcc()
        {
            CmdResult result = CmdRunner.CmdRun("gcc -v");
            Logger.Info(result.error);
            if (result.result!=null)
                return true;
            else
                return false;
        }
        private static bool CheckPip()
        {
            CmdResult result = CmdRunner.CmdRun("pip -V");
            Logger.Info(result.result);
            if (result.result!= null)
                return true;
            else
                return false;
        }

        private static bool CheckJava()
        {
            CmdResult result = CmdRunner.CmdRun("java -version");
            Logger.Info(result.result);
            if (result.result!= null)
                return true;
            else
                return false;
            
        }

        public static string GetCodePath()
        {
            // 已经找到过VScode
            if (vscPath != NOTFOUND)
                return vscPath;

            // 命令行验证是否在PATH路径
            CmdResult result = CmdRunner.CmdRun("code --help");
            if (result.result.Contains("To read output from another program, append"))
            {
                vscPath = string.Empty;
                return vscPath;
            }
            else
            {
                // 若不在，采用注册表查找
                RegistryKey machineKey =
                Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall");
                RegistryKey userKey =
                    Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
                string[] machineKeyList = machineKey.GetSubKeyNames();
                string[] userKeyList = userKey.GetSubKeyNames();

                foreach (string keyName in machineKeyList)
                {
                    RegistryKey key = machineKey.OpenSubKey(keyName);
                    object name = key.GetValue("DisplayName");
                    if (name != null && name.ToString().Contains("Microsoft Visual Studio Code"))
                    {
                        vscPath = key.GetValue("InstallLocation").ToString();
                        return vscPath;
                    }
                }

                foreach (string keyName in userKeyList)
                {
                    RegistryKey key = userKey.OpenSubKey(keyName);
                    object name = key.GetValue("DisplayName");
                    if (name != null && name.ToString().Contains("Microsoft Visual Studio Code"))
                    {
                        vscPath = key.GetValue("InstallLocation").ToString();
                        return vscPath;
                    }
                }
                return NOTFOUND;
            }
        }
    }
}