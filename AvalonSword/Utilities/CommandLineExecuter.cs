using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Utilities
{
    public class CommandLineExecuter:IDisposable
    {
        public readonly Process P;

        public CommandLineExecuter()
        {
            P = new Process();
            P.StartInfo.FileName = "cmd.exe";
            P.StartInfo.UseShellExecute = false;
            P.StartInfo.CreateNoWindow = true;
            P.StartInfo.RedirectStandardError = true;
            P.StartInfo.RedirectStandardInput = true;
            P.StartInfo.RedirectStandardOutput = true;
            P.StandardInput.AutoFlush = true;
            P.Start();
            P.BeginOutputReadLine();
        }

        public void ExecuteCommand(IEnumerable<string> cmdList)
        {
            foreach (var cmd in cmdList)
            {
                ExecuteCommand(cmd);
            }
        }

        public void ExecuteCommand(string cmd)
        {
            P.StandardInput.WriteLine(cmd);
        }

        public void Close()
        {
            P?.Close();
        }

        public void Dispose()
        {
            Close();
        }

        public static string Execute(string cmd)
        {
            var p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + cmd;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;

            try
            {
                p.Start();
                var result = p.StandardOutput.ReadToEnd();
                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                p.Close();
            }
        }
    }
}
