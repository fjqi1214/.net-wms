Option Strict Off
Option Explicit On 


Public Module GW21API
    '*++ BUILD Version: 0001
    '
    ' Qway95.bas          Copyright (c) 1996, ATOP Technologies,Inc
    '
    ' Abstract:
    '    For Win32 developers under Windows 95
    '    This include file defines all constants and types needed for
    '    accessing ATOP's Q-Way device, a RS-485 based fieldbus.
    '
    ' Revision History:
    '-*/

    '#define QWAY_MAX_DATA_LEN   64
    '' Qway communication control block (CCB) structure
    Public Structure QWAY_CCB
        Dim ccblen As Short
        Dim ccbport As Byte
        Dim ccbdnode As Byte
        Dim ccbsnode As Byte
        Dim ccbcmd As Byte
        <VBFixedArray(256)> Dim ccbdata() As Byte

        'UPGRADE_TODO: 必须调用“Initialize”来初始化此结构的实例。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1026"”
        Public Sub Initialize()
            ReDim ccbdata(256)
        End Sub
    End Structure

    Public Structure CAPS_CCB
        Dim ccblen As Short
        Dim ccbport As Byte
        Dim ccbdnode As Byte
        Dim ccbsnode As Byte
        Dim ccbcmd As Byte
        Dim ccbsubcmd As Byte
        Dim ccbsubnode As Byte
        <VBFixedArray(256)> Dim ccbdata() As Byte

        'UPGRADE_TODO: 必须调用“Initialize”来初始化此结构的实例。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1026"”
        Public Sub Initialize()
            ReDim ccbdata(256)
        End Sub
    End Structure

    Public Structure LB_DSPSTR
        <VBFixedArray(80)> Dim dspstr() As Byte

        'UPGRADE_TODO: 必须调用“Initialize”来初始化此结构的实例。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1026"”
        Public Sub Initialize()
            ReDim dspstr(80)
        End Sub
    End Structure

    '  // initial Gateway
    Public Declare Function AB_API_Open Lib "dapapi2" () As Short
    Public Declare Function AB_API_Close Lib "dapapi2" () As Short
    Public Declare Function AB_GW_Open Lib "dapapi2" (ByVal Gateway_ID As Integer) As Short
    Public Declare Function AB_GW_Close Lib "dapapi2" (ByVal Gateway_ID As Integer) As Short
    Public Declare Function AB_GW_Cnt Lib "dapapi2" () As Short
    Public Declare Function AB_GW_Conf Lib "dapapi2" (ByVal ndx As Integer, ByRef Gateway_ID As Integer, ByRef ip As Byte, ByRef port As Integer) As Short
    Public Declare Function AB_GW_Ndx2ID Lib "dapapi2" (ByVal ndx As Integer) As Short
    Public Declare Function AB_GW_ID2Ndx Lib "dapapi2" (ByVal Gateway_ID As Integer) As Short
    Public Declare Function AB_GW_InsConf Lib "dapapi2" (ByVal Gateway_ID As Integer, ByRef ip As Byte, ByVal port As Integer) As Short
    Public Declare Function AB_GW_UpdConf Lib "dapapi2" (ByVal Gateway_ID As Integer, ByRef ip As Byte, ByVal port As Integer) As Short
    Public Declare Function AB_GW_DelConf Lib "dapapi2" (ByVal Gateway_ID As Integer) As Short

    '  // Get/Send message from/to Gateway
    'UPGRADE_WARNING: 结构 QWAY_CCB 可能要求封送处理属性作为此声明语句中的参数传递。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1050"”
    Public Declare Function AB_GW_RcvMsg Lib "dapapi2" (ByVal Gateway_ID As Integer, ByRef ccb As QWAY_CCB) As Short
    'UPGRADE_WARNING: 结构 QWAY_CCB 可能要求封送处理属性作为此声明语句中的参数传递。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1050"”
    Public Declare Function AB_GW_SndMsg Lib "dapapi2" (ByVal Gateway_ID As Integer, ByRef ccb As QWAY_CCB) As Short
    Public Declare Function AB_GW_RcvReady Lib "dapapi2" (ByVal Gateway_ID As Integer) As Short
    Public Declare Function AB_GW_RcvData Lib "dapapi2" (ByVal Gateway_ID As Integer) As Short
    Public Declare Function AB_GW_RcvAddr Lib "dapapi2" () As Short
    Public Declare Function AB_GW_RcvPort Lib "dapapi2" () As Short
    Public Declare Function AB_GW_RcvTagNode Lib "dapapi2" () As Short
    Public Declare Function AB_GW_RcvTagCmd Lib "dapapi2" () As Short
    Public Declare Function AB_GW_RcvTagData Lib "dapapi2" (ByRef data As Byte) As Short
    Public Declare Function AB_GW_SetDefault Lib "dapapi2" (ByVal Gateway_ID As Short) As Short
    Public Declare Function AB_GW_GetDefault Lib "dapapi2" () As Short

    '  // Get Gateway status
    Public Declare Function AB_GW_Status Lib "dapapi2" (ByVal Gateway_ID As Integer) As Short
    Public Declare Function AB_GW_AllStatus Lib "dapapi2" (ByRef status As Byte) As Short
    Public Declare Function AB_GW_TagDiag Lib "dapapi2" (ByVal Gateway_ID As Integer, ByVal port As Integer) As Short
    Public Declare Function AB_GW_SetPollRang Lib "dapapi2" (ByVal Gateway_ID As Integer, ByVal port As Integer, ByVal poll_range As Integer) As Short


    '  // Send Message to picking tag
    Public Declare Function AB_LB_DspNum Lib "dapapi2" (ByVal node_addr As Integer, ByVal Disp_Int As Integer, ByVal DOT As Byte, ByVal interval As Integer) As Short
    Public Declare Function AB_LB_DspStr Lib "dapapi2" (ByVal node_addr As Integer, ByRef Disp_Str As Byte, ByVal DOT As Byte, ByVal interval As Integer) As Short
    Public Declare Function AB_LB_DspStr2 Lib "dapapi2" (ByVal tag_addr As Integer, ByRef Disp_Str As Byte, ByVal interval As Integer) As Short
    Public Declare Function AB_LB_DspOff Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_LB_DspAddr Lib "dapapi2" (ByVal node_addr As Integer) As Short
    Public Declare Function AB_LB_LedOn Lib "dapapi2" (ByVal node_addr As Integer, ByVal Lamp_STA As Byte, ByVal interval As Integer) As Short
    Public Declare Function AB_LB_BuzOn Lib "dapapi2" (ByVal node_addr As Integer, ByVal Buzzer_Type As Byte) As Short
    Public Declare Function AB_LB_SetMode Lib "dapapi2" (ByVal node_addr As Integer, ByVal Pick_Mode As Byte) As Short
    Public Declare Function AB_LB_Simulate Lib "dapapi2" (ByVal node_addr As Integer, ByVal Simulate_Mode As Byte) As Short
    Public Declare Function AB_LB_SetLock Lib "dapapi2" (ByVal node_addr As Integer, ByVal Lock_State As Byte, ByVal Lock_key As Byte) As Short

    '  //-----Tag
    'Added by Icyer
    Public Declare Function AB_Tag_RcvMsg Lib "dapapi2" (ByRef tag_addr As Integer, ByRef subcmd As Short, ByRef msg_type As Short, ByRef data As Byte, ByRef data_cnt As Short) As Short
    Public Function AB_Tag_RcvMsg(ByRef tag_addr As Integer, ByRef subcmd As Short, ByRef msg_type As Short, ByRef data() As Byte, ByRef data_cnt As Short) As Short
        On Error GoTo ErrHandle
        Dim strPath As String
        strPath = System.Configuration.ConfigurationSettings.AppSettings.Get("DCTCommandPath")
        Dim strFiles() As String
        strFiles = System.IO.Directory.GetFiles(strPath, "*.txt")
        If strFiles.Length = 0 Then
            tag_addr = 0
            data_cnt = 0
            AB_Tag_RcvMsg = 0
            Exit Function
        End If
        Dim sortList As New SortedList
        Dim strFile As String
        For Each strFile In strFiles
            sortList.Add(strFile, strFile)
        Next
        strFile = sortList.GetByIndex(0).ToString()
        Dim reader As New System.IO.StreamReader(strFile)
        Dim strLine As String
        strLine = reader.ReadLine()
        reader.Close()
        System.IO.File.Delete(strFile)
        Dim strTmp() As String
        strTmp = strLine.Split(":")
        tag_addr = Convert.ToInt32(strTmp(0))
        data_cnt = strTmp(1).Length
        If data_cnt > 0 Then
            Dim bytData(data_cnt) As Byte
            bytData = System.Text.UTF8Encoding.UTF8.GetBytes(strTmp(1))
            data = bytData
        End If
        AB_Tag_RcvMsg = data_cnt
        Exit Function
ErrHandle:
        data_cnt = 0
        AB_Tag_RcvMsg = 1
    End Function
    Public Declare Function AB_Tag_Reset Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_Tag_ChgAddr Lib "dapapi2" (ByVal tag_addr As Integer, ByVal new_tag As Integer) As Short

    '  // Send message to special tag level devices
    Public Declare Function AB_CCD_Action Lib "dapapi2" (ByVal node_addr As Integer, ByVal Action As Integer) As Integer

    '  //-----MMI-19
    Public Declare Function AB_DCS_InputMode Lib "dapapi2" (ByVal tag_addr As Integer, ByVal input_mode As Byte) As Short
    Public Declare Function AB_DCS_BufSize Lib "dapapi2" (ByVal tag_addr As Integer, ByVal buf_size As Byte) As Short
    Declare Function AB_DCS_SetConf Lib "dapapi2" (ByVal tag_addr As Integer, ByVal enable_status As Byte, ByVal disable_status As Byte) As Short
    Public Declare Function AB_DCS_ReqConf Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DCS_GetVer Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DCS_Reset Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DCS_SetRows Lib "dapapi2" (ByVal tag_addr As Integer, ByVal rows As Byte) As Short
    Public Declare Function AB_DCS_SimulateKey Lib "dapapi2" (ByVal tag_addr As Integer, ByVal key_code As Byte) As Short
    Public Declare Function AB_DCS_Cls Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DCS_Buzzer Lib "dapapi2" (ByVal tag_addr As Integer, ByVal alarm_time As Byte, ByVal alarm_cnt As Byte) As Short
    Public Declare Function AB_DCS_ScrollUp Lib "dapapi2" (ByVal tag_addr As Integer, ByVal up_rows As Byte) As Short
    Public Declare Function AB_DCS_ScrollDown Lib "dapapi2" (ByVal tag_addr As Integer, ByVal down_rows As Byte) As Short
    Public Declare Function AB_DCS_ScrollHome Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DCS_ScrollEnd Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DCS_SetCursor Lib "dapapi2" (ByVal tag_addr As Integer, ByVal row As Byte, ByVal column As Byte) As Short
    Public Declare Function AB_DCS_DspStrE Lib "dapapi2" (ByVal tag_addr As Integer, ByRef dsp_str As Byte, ByVal dsp_cnt As Integer) As Short
    'Added by Icyer
    Public Declare Function AB_DCS_DspStrC Lib "dapapi2" (ByVal tag_addr As Integer, ByRef dsp_str As Byte, ByVal dsp_cnt As Integer) As Short
    Private OutputFileSequence As Long
    Public Function AB_DCS_DspStrC(ByVal tag_addr As Integer, ByRef dsp_str() As Byte, ByVal dsp_cnt As Integer) As Short
        Dim strPath As String
        strPath = System.Configuration.ConfigurationSettings.AppSettings.Get("DCTCommandPath")
        strPath = System.IO.Path.Combine(strPath, "Output")
        If System.IO.Directory.Exists(strPath) = False Then
            System.IO.Directory.CreateDirectory(strPath)
        End If
        strPath = System.IO.Path.Combine(strPath, tag_addr.ToString() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + DateTime.Now.Millisecond.ToString().PadLeft(3, "0"))
        Dim strPath0 As String
        OutputFileSequence = OutputFileSequence + 1
        strPath0 = OutputFileSequence.ToString().PadLeft(8, "0") + ".txt"
        strPath = strPath + strPath0
        Dim fs As New System.IO.FileStream(strPath, IO.FileMode.CreateNew)
        fs.Write(dsp_str, 0, dsp_cnt)
        fs.Close()
        AB_DCS_DspStrC = 1
    End Function

    '  //-----Convertor
    Public Declare Function AB_CNV_SendData Lib "dapapi2" (ByVal tag_addr As Integer, ByRef dsp_str As Byte, ByVal dsp_cnt As Integer) As Short
    Public Declare Function AB_CNV_SetTerminator Lib "dapapi2" (ByVal tag_addr As Integer, ByVal terminator As Integer) As Short

    '  //-----AP20A
    Public Declare Function AB_LB2_DspStr Lib "dapapi2" (ByVal tag_addr As Integer, ByRef dsp_str As Byte, ByVal interval As Integer) As Short
    Public Declare Function AB_LB2_SetRows Lib "dapapi2" (ByVal tag_addr As Integer, ByVal max_rows As Integer) As Short
    Public Declare Function AB_LB2_Download Lib "dapapi2" (ByVal tag_addr As Integer, ByVal row As Integer, ByRef dsp_str As Byte) As Short

    '  //-----Fixed CCD
    Public Declare Function AB_CCD_Rescan Lib "dapapi2" (ByVal tag_addr As Integer) As Short

    '  //-----DT200
    Public Declare Function AB_DT2_DspStr Lib "dapapi2" (ByVal tag_addr As Integer, ByRef dsp_str As Byte, ByVal dsp_cnt As Integer) As Short
    Public Declare Function AB_DT2_EnableCounter Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DT2_DisableCounter Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DT2_ReadCounter Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DT2_SetCounter Lib "dapapi2" (ByVal tag_addr As Integer, ByVal count As Integer) As Short

    '  // DIO
    Public Declare Function AB_DIO_ReadIoStatus Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_DIO_SetDO Lib "dapapi2" (ByVal tag_addr As Integer, ByVal channel As Integer, ByVal status As Integer) As Short
    Public Declare Function AB_DIO_SetDiRspMode Lib "dapapi2" (ByVal tag_addr As Integer, ByVal mode As Integer) As Short
    Public Declare Function AB_DIO_SetDiOpMode Lib "dapapi2" (ByVal tag_addr As Integer, ByVal mode As Integer) As Short


    '  // Send message to mini-KANBAN
    Public Declare Function AB_KBN_Clear Lib "dapapi2" (ByVal tag_addr As Integer) As Short
    Public Declare Function AB_KBN_DspMsg Lib "dapapi2" (ByVal tag_addr As Integer, ByRef msg As Byte, ByVal dsp_mode As Integer, ByVal scroll_mode As Integer) As Short
    Public Declare Function AB_KBN_SegDownload Lib "dapapi2" (ByVal tag_addr As Integer, ByVal seg_no As Integer, ByRef msg As Byte, ByVal dsp_mode As Integer, ByVal scroll_mode As Integer) As Short
    Public Declare Function AB_KBN_SegDel Lib "dapapi2" (ByVal tag_addr As Integer, ByVal seg_no As Integer) As Short
    Public Declare Function AB_KBN_SegDsp Lib "dapapi2" (ByVal tag_addr As Integer, ByRef seg_no As Byte, ByVal seg_cnt As Integer) As Short

    Public Structure GwPortStatus
        Dim enable_flag As Short 'enable flag
        Dim max_node As Short 'max polling node
        Dim subnode_status As String 'all subnode status string
        Dim timeout_flag As Short 'timeout flag
        Dim wait_flag As Short 'wait diagnosis response
    End Structure

    Public Structure GwConfType
        Dim gw_id As Short
        Dim ip As String
        Dim port As Short
        <VBFixedArray(2 - 1)> Dim portn() As GwPortStatus
        Dim reconnect_cnt As Short
        Dim status As Short

        'UPGRADE_TODO: 必须调用“Initialize”来初始化此结构的实例。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1026"”
        Public Sub Initialize()
            ReDim portn(2 - 1)
        End Sub
    End Structure

    Public GwCnt As Short
    Public GwConf() As GwConfType


    Public Function AB_ErrMsg(ByRef ret As Short) As String
        Dim tmpstr As Object
        Select Case ret
            '   Case -3
            '       tmpstr = "Parameter data is error !"
        Case -2
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "TCP is not created yet !"
            Case -1
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "DAP_ID out of range !"
            Case 0
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Closed"
            Case 1
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Open"
            Case 2
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Listening"
            Case 3
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Connection is Pending"
            Case 4
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Resolving the host name"
            Case 5
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Host is Resolved"
            Case 6
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Waiting to Connect"
            Case 7
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Connected ok "
            Case 8
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Connection is closing"
            Case 9
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "State error has occurred"
            Case 10
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Connection state is undetermined"
            Case Else
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = "Unknown Error Code"
        End Select

        'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
        AB_ErrMsg = tmpstr
    End Function


    Public Function AB_LoadConf() As Short
        Dim tmpstr As Object
        Dim cnt, ret As Short
        Dim i, j As Short
        Dim id, port As Integer
        Dim ip(20) As Byte

        GwCnt = AB_GW_Cnt()
        If GwCnt = 0 Then Exit Function
        ReDim GwConf(GwCnt - 1)

        For i = 0 To GwCnt - 1
            ret = AB_GW_Conf(i, id, ip(0), port)
            If ret >= 0 Then
                GwConf(i).gw_id = id
                GwConf(i).port = port
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                tmpstr = ""
                For j = 0 To 19
                    If ip(j) = 0 Then Exit For
                    'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                    tmpstr = tmpstr + Chr(ip(j))
                Next j
                'UPGRADE_WARNING: 未能解析对象 tmpstr 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
                GwConf(i).ip = tmpstr
            Else
                GwConf(i).gw_id = 0
                GwConf(i).port = 0
                GwConf(i).ip = ""
            End If
        Next i
        AB_LoadConf = GwCnt
    End Function
End Module

