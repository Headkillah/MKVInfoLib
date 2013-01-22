using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace MKV
{
    public class MKVFileInfo
    {

        public String resolution { get; set; }
        public double duration { get; set; }

        public MKVFileInfo(String mkvFileName)
        {
            // get mkvinfo from embedded resources
            byte[] binaryResource = Properties.Resources.mkvinfo;
            string tempExe = Path.Combine(Path.GetTempPath(), "mkvinfo.exe");

            // copy to temp folder
            using (FileStream exeFile = new FileStream(tempExe, FileMode.OpenOrCreate))
            {
                exeFile.Write(binaryResource, 0, binaryResource.Length);
            }

            // start process
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = tempExe,
                    Arguments = mkvFileName,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();

            String output = null;

            // get output
            while (!proc.StandardOutput.EndOfStream)
            {
                output += proc.StandardOutput.ReadLine();
            }

            proc.Close();

            // remove file from temp
            try
            {

            }
            catch (Exception e)
            {
                File.Delete(tempExe);
            }

            // parse all info we need
            ParseMetaData(output);
        }

        private void ParseMetaData(String output)
        {
            Regex rgx = null;

            // parse resolution
            rgx = new Regex(@"\+ Display (width|height): ([0-9]+)\|");
            this.resolution = rgx.Match(output).Groups[2].ToString() + "x" + rgx.Match(output).NextMatch().Groups[2].ToString();

            // parse duration
            rgx = new Regex(@"\+ Duration: ([0-9\.]+)s");
            this.duration = double.Parse(rgx.Match(output).Groups[1].ToString(), System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
