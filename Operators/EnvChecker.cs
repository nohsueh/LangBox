﻿using Microsoft.Win32;

namespace LangBox.Operators
{
    internal class EnvChecker
    {
        public const string NOTFOUND = "NOTFOUND";
        private static string vscPath = NOTFOUND;

        public static bool Check(string Name)
        {
            if(Name.Equals("C_CPP"))
            {
                return CheckGcc();
            }
            if (Name.Equals("Python"))
            {
                return CheckPip();
            }
            if (Name.Equals("Java"))
            {
                return CheckJava();
            }

            return false;
        }

        public static bool CheckGcc()
        {
            CmdResult result = CmdRunner.CmdRun("gcc");
            if (result.error.Contains("no input files"))
                return true;
            else
                return false;
        }
        public static bool CheckPip()
        {
            CmdResult result = CmdRunner.CmdRun("pip -V");
            if (result.result.Contains("lib\\site-packages\\pip"))
                return true;
            else
                return false;
        }

        public static bool CheckJava()
        {
            CmdResult result = CmdRunner.CmdRun("java -version");
            if (result.error.Contains("java version"))
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