using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MeGUI.core.util;

namespace MeGUI
{
    class MencoderMuxer : CommandlineMuxer
    {
        public MencoderMuxer(string executablePath)
        {
            this.executable = executablePath;
        }

        /// <summary>
        /// gets the framenumber from an mencoder status update line
        /// </summary>
        /// <param name="line">mencoder stdout line</param>
        /// <returns>the framenumber included in the line</returns>
        public ulong? getFrameNumber(string line)
        {
            try
            {
                int frameNumberStart = line.IndexOf("s", 4) + 1;
                int frameNumberEnd = line.IndexOf("f");
                string frameNumber = line.Substring(frameNumberStart, frameNumberEnd - frameNumberStart).Trim();
                return ulong.Parse(frameNumber);
            }
            catch (Exception e)
            {
                log.Append("Exception in getFrameNumber(" + line + ") " + e.Message);
                return null;
            }
        }

        protected override bool checkExitCode()
        {
            return true;
        }

        public override void ProcessLine(string line, StreamType stream)
        {
            if (line.StartsWith("Pos:")) // status update
                su.NbFramesDone = getFrameNumber(line);
            else if (line.IndexOf("error") != -1)
            {
                log.AppendLine(line);
                su.HasError = true;
            }
            else if (line.IndexOf("not an MEncoder option") != -1)
            {
                log.AppendLine("Error: Unrecognized commandline parameter detected.");
                su.Error = line;
                su.HasError = true;
            }
            else
                log.AppendLine(line);
        }
    }
}
