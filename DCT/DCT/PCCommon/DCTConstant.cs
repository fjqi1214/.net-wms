using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.Common.DCT.PC
{
    public class DCTMessageDirection
    {
        public static string ServerToClient = "ServerToClient";
        public static string ClientToServer = "ClientToServer";
    }

    public class DCTMessageType
    {
        public static string Command = "Command";
        public static string Message = "Message";
    }

    public class DCTMessageStatus
    {
        public static string New = "New";
        public static string Reading = "Reading";
        public static string Dealed = "Dealed";
    }
}
