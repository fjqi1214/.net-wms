using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.Common.DCT.PC
{
    public enum PCClientStatus
    {
        TCPCreateError = -1,
        ClientNotExist = -2,
        Closed = 0,
        WaitingForConnect = 6,
        Connecting = 7,
        TimeOut = 8,
        TCPConnectError = 9
    }
}
