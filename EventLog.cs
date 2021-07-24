using System;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace Common
{
    public static class EventLog
    {
        static StreamWriter LogStream = null;
        static bool CloseLogAfterEveryWrite = false;
        static string LogFileName = null;

        static object logStreamLock = new object ();
        static int lineNumber = 1;

     //****************************************************************

        public static void Open (string fileName, bool closeAfterEvery) 
        {
            lock (logStreamLock)
            {
                try
                {
                    LogFileName = fileName;
                    LogStream = new StreamWriter (fileName);

                    CloseLogAfterEveryWrite = closeAfterEvery;

                    if (CloseLogAfterEveryWrite)
                    {
                        LogStream.Close ();
                        LogStream = null;
                    }

                    AppDomain.CurrentDomain.ProcessExit += AppExiting;
                }

                catch (Exception)
                {
                    LogStream = null;
                }
            }
        }

     //****************************************************************

        public static void Open (string fileName)
        {
            Open (fileName, true);            
        }

        //***********************************************************

        static readonly bool PrintThreadID = false;  // not all combinations handled
        static readonly bool PrintLineNumber = false;

        public static void Write (string line)
        {
            lock (logStreamLock)
            {
                try
                {
                    string fileText;

                    if (PrintThreadID)
                    {
                        int id = Thread.CurrentThread.ManagedThreadId;
                        fileText = string.Format ("{0}. Thread {1}: {2}", lineNumber++, id, line);
                    }

                    else if (PrintLineNumber)
                    {
                        fileText = string.Format ("{0} {1}", lineNumber++, line);
                    }

                    else
                        fileText = string.Format ("{0}", line);

                    if (LogFileName == null)
                    {
                        Console.Write (fileText);// (line);
                        return;
                    }

                    if (LogStream == null)
                    {
                        LogStream = new StreamWriter (LogFileName, true);
                    }

                    if (LogStream != null)
                    {
                        LogStream.Write (fileText);// (line);

                        if (CloseLogAfterEveryWrite)
                        {
                            LogStream.Close ();
                            LogStream = null;
                        }
                    }
                }

                catch (Exception)
                {
                    LogStream = null;
                }
            }
        }

        //***********************************************************

        public static void WriteLine (string line)
        {
            Write (line + "\r\n");
        }

        //***********************************************************

        static void AppExiting (object sender, EventArgs e)
        {
     //     WriteLine ("Closed on AppExit");
            Close ();
        }
    
        public static void Close ()
        {
            lock (logStreamLock)
            {
                if (LogStream != null)
                {
                    LogStream.Close ();
                    LogStream = null;
                }
            }
        }
    }
}
