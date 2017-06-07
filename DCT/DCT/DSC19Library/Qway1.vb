Option Strict Off
Option Explicit On 


Public Module Qway1

    Function Bin2Str(ByRef bufbin() As Byte, ByRef start As Short, ByRef cnt As Object) As String
        Dim i As Short
        Dim bufstr As String

        'cnt = LenB(bufbin)
        'cnt = 3
        bufstr = ""
        'UPGRADE_WARNING: 未能解析对象 cnt 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
        For i = 0 To cnt - 1
            'bufstr = bufstr + Chr$(AscB(MidB(bufbin, start + i, 1)))
            bufstr = bufstr & Chr(bufbin(start + i))
        Next i

        Bin2Str = bufstr
    End Function

    Function BinCopy(ByRef dstbuf() As Byte, ByRef dst_ndx As Short, ByRef srcbuf() As Byte, ByRef src_ndx As Short, ByRef cnt As Short) As Short
        Dim i As Short

        For i = 0 To cnt - 1
            dstbuf(dst_ndx + i) = srcbuf(src_ndx + i)
        Next i

        BinCopy = 0
    End Function



    Function KbnClr(ByRef node As Short, ByRef port As Short, ByRef sub_node As Short) As Short
        Dim ret, cnt As Short
        Dim data() As Byte
        Dim tmpmsg As String
        Dim dsp_mode As Short
        Dim scroll_mode As Short
        Dim delay_msec As Integer
        Dim tmpmsg2 As String

        ReDim data(128)
        data(0) = &H40S 'sub cmd
        data(1) = sub_node

        tmpmsg2 = "9A011F"

        ret = Str2Bin(tmpmsg2, data, 2)

        ret = QwaySendTagDataB(CInt(node), CInt(port), data, ret)

        KbnClr = ret
    End Function
    Function KbnSegDownload(ByRef node As Short, ByRef port As Short, ByRef sub_node As Short, ByRef seg_no As Short, ByRef msg As String) As Short
        Dim ret, cnt As Short
        Dim data() As Byte
        Dim tmpmsg As String
        Dim dsp_mode As Short
        Dim scroll_mode As Short
        Dim delay_msec As Integer
        Dim tmpmsg2 As String

        cnt = Len(msg)
        ReDim data(100 + cnt * 4) 'may be chinese : *2, change to Hex format : *2
        data(0) = &H44S 'sub cmd
        data(1) = sub_node
        tmpmsg = str2hex(msg) 'change to Hex string (Double-Byte Code)

        dsp_mode = 1
        delay_msec = &H500S

        tmpmsg2 = "9A"
        tmpmsg2 = tmpmsg2 & Right(Str(100 + seg_no), 2)
        tmpmsg2 = tmpmsg2 & "03000000" & Right(Str(100 + dsp_mode), 2) & "0000" & tmpmsg & "00"
        If delay_msec Then
            tmpmsg2 = tmpmsg2 & "07" & Mid(Hex(&H10000 + delay_msec), 4, 2) & Mid(Hex(&H10000 + delay_msec), 2, 2)
        End If
        tmpmsg2 = tmpmsg2 & "1F"

        ret = Str2Bin(tmpmsg2, data, 2)

        ret = QwaySendTagDataB(CInt(node), CInt(port), data, ret)

        KbnSegDownload = ret
    End Function
    Function KbnSegDel(ByRef node As Short, ByRef port As Short, ByRef sub_node As Short, ByRef seg_no As Short) As Short
        Dim ret As Short
        Dim data() As Byte
        Dim tmpmsg2 As String

        ReDim data(100) 'may be chinese : *2, change to Hex format : *2
        data(0) = &H4BS 'sub cmd
        data(1) = sub_node

        tmpmsg2 = "9A"
        tmpmsg2 = tmpmsg2 & Right(Str(100 + seg_no), 2)
        tmpmsg2 = tmpmsg2 & "9B"

        ret = Str2Bin(tmpmsg2, data, 2)

        ret = QwaySendTagDataB(CInt(node), CInt(port), data, ret)

        KbnSegDel = ret
    End Function
    Function KbnSegDsp(ByRef node As Short, ByRef port As Short, ByRef sub_node As Short, ByRef seg_no As Short) As Short
        Dim ret As Short
        Dim data() As Byte
        Dim tmpmsg2 As String

        ReDim data(100) 'may be chinese : *2, change to Hex format : *2
        data(0) = &H47S 'sub cmd
        data(1) = sub_node

        tmpmsg2 = "9A"
        tmpmsg2 = tmpmsg2 & Right(Str(100 + seg_no), 2)
        tmpmsg2 = tmpmsg2 & "9B"

        ret = Str2Bin(tmpmsg2, data, 2)
        ret = QwaySendTagDataB(CInt(node), CInt(port), data, ret)

        KbnSegDsp = ret
    End Function


    Function KbnDspChn(ByRef node As Short, ByRef port As Short, ByRef sub_node As Short, ByRef msg As String) As Short
        Dim ret, cnt As Short
        Dim data() As Byte
        Dim tmpmsg As String
        Dim dsp_mode As Short
        Dim scroll_mode As Short
        Dim delay_msec As Integer
        Dim tmpmsg2 As String

        cnt = Len(msg)
        ReDim data(100 + cnt * 4) 'may be chinese : *2, change to Hex format : *2
        data(0) = &H40S 'sub cmd
        data(1) = sub_node
        tmpmsg = str2hex(msg) 'change to Hex string (Double-Byte Code)

        dsp_mode = 1
        delay_msec = &H500S
        scroll_mode = 1

        tmpmsg2 = "9A"
        tmpmsg2 = tmpmsg2 & "03000000" & Right(Str(100 + dsp_mode), 2) & "0000" & tmpmsg & "00"
        If delay_msec Then
            tmpmsg2 = tmpmsg2 & "07" & Mid(Hex(&H10000 + delay_msec), 4, 2) & Mid(Hex(&H10000 + delay_msec), 2, 2)
        End If
        If scroll_mode Then
            tmpmsg2 = tmpmsg2 & "0300000008000000"
        End If
        tmpmsg2 = tmpmsg2 & "1F"

        ret = Str2Bin(tmpmsg2, data, 2)

        ret = QwaySendTagDataB(CInt(node), CInt(port), data, ret)

        KbnDspChn = ret
    End Function


    Function LcmDspChn(ByRef node As Short, ByRef port As Short, ByRef sub_node As Short, ByRef msg As String) As Short
        Dim ret, cnt As Short
        Dim data() As Byte
        Dim tmpmsg As String

        cnt = Len(msg)
        ReDim data(2 + cnt * 4) 'may be chinese : *2, change to Hex format : *2
        data(0) = &H38S 'sub cmd
        data(1) = sub_node
        tmpmsg = str2hex(msg) 'change to Hex string (Double-Byte Code)
        ret = Str2Bin(tmpmsg, data, 2)
        ret = QwaySendDataB(CInt(node), CInt(port), data, ret)

        LcmDspChn = ret
    End Function

    Function LcmDspEng(ByRef node As Short, ByRef port As Short, ByRef sub_node As Short, ByRef msg As String) As Short
        Dim ret, cnt As Short
        Dim data() As Byte

        cnt = Len(msg)
        ReDim data(2 + cnt * 2)
        data(0) = &H39S 'sub cmd
        data(1) = sub_node
        ret = Str2Bin(msg, data, 2)
        ret = QwaySendDataB(CInt(node), CInt(port), data, ret)

        LcmDspEng = ret
    End Function

    Function Str2Bin(ByRef strdata As String, ByRef bufbin() As Byte, ByRef start As Short) As Integer
        '// change string to byte array
        Dim i As Short
        Dim strcnt, ndx, data As Integer
        Dim bufstr As String

        strcnt = Len(strdata)
        ndx = start
        For i = 1 To strcnt
            data = Asc(Mid(strdata, i, 1))
            bufbin(ndx) = (data Mod 256) And &HFFS
            ndx = ndx + 1
            If data >= 256 Then
                bufbin(ndx) = (data \ 256) And &HFFS
                ndx = ndx + 1
            End If
        Next i

        Str2Bin = ndx
    End Function

    Function QwayGetDataB(ByRef node As Integer, ByRef port As Integer, ByRef qwaydata() As Byte) As Short
        'id=1-4, node=1-250/255, port=1/2/255, ret>=0:ok,else:err
        Dim ret, retcnt As Integer
        Dim time_out As Short
        Dim qway_snd, qway_rcv As QWAY_CCB

        ret = AB_GW_RcvMsg(node, qway_rcv)

        If ret > 0 Then
            port = CShort(qway_rcv.ccbport And 1) + 1

            ret = BinCopy(qwaydata, 0, qway_rcv.ccbdata, 0, qway_rcv.ccblen - 6)
            QwayGetDataB = qway_rcv.ccblen - 6
            'qwaydata = Bin2Str(qway_rcv.ccbdata(), 1, qway_rcv.ccblen - 6)
            'MsgBox2 HexOp2(qwaydata, -1)
        Else
            QwayGetDataB = ret
        End If

    End Function

    Function QwaySendTagDataB(ByRef node As Integer, ByRef port As Integer, ByRef qwaydata() As Byte, ByRef data_len As Short) As Short
        'id=1-4, node=1-250/255, port=0:LCD,1/2/3(both):COM,
        'ret=0:ok,else:err,-2:node err

        Dim ret, retcnt As Integer
        Dim i, cnt As Short
        Dim qway_snd, qway_rcv As QWAY_CCB
        Dim databuf() As Byte
        Dim packsize As Short
        Dim subnode, subcmd As Short
        Dim data_len2 As Short

        If node < 0 Or node > 255 Then GoTo QwaySendDataB99
        If port < 0 Or port > 255 Then GoTo QwaySendDataB99

        Select Case port
            Case 0
                qway_snd.ccbport = &H40S
            Case 1
                qway_snd.ccbport = &H60S
            Case 2
                qway_snd.ccbport = &H61S
            Case Else
                qway_snd.ccbport = &HFFS
        End Select

        qway_snd.ccbdnode = node
        qway_snd.ccbsnode = 0
        qway_snd.ccbcmd = 0
        subcmd = qwaydata(0)
        subnode = qwaydata(1)
        data_len2 = data_len - 2

        packsize = 50
        For i = 1 To data_len2 Step packsize
            cnt = data_len2 - i + 1
            If cnt > packsize Then cnt = packsize

            ret = BinCopy(qway_snd.ccbdata, 2, qwaydata, i - 1 + 2, cnt)
            'ret = BinCopy(qway_snd.ccbdata(), 0, databuf(), i - 1, cnt)
            qway_snd.ccblen = 6 + cnt + 2
            qway_snd.ccbdata(0) = subcmd
            qway_snd.ccbdata(1) = subnode

            ret = AB_GW_SndMsg(node, qway_snd)

            QwaySendTagDataB = ret
            If QwaySendTagDataB < 0 Then
                Exit For
            End If
        Next i
        Exit Function

QwaySendDataB99:
        QwaySendTagDataB = -2
    End Function

    Function QwaySendDataB(ByRef node As Integer, ByRef port As Integer, ByRef qwaydata() As Byte, ByRef data_len As Short) As Short
        'id=1-4, node=1-250/255, port=0:LCD,1/2/3(both):COM,
        'ret=0:ok,else:err,-2:node err

        Dim ret, retcnt As Integer
        Dim i, cnt As Short
        Dim qway_snd, qway_rcv As QWAY_CCB
        Dim databuf() As Byte
        Dim packsize As Short

        If node < 0 Or node > 255 Then GoTo QwaySendDataB99
        If port < 0 Or port > 255 Then GoTo QwaySendDataB99

        Select Case port
            Case 0
                qway_snd.ccbport = &H40S
            Case 1
                qway_snd.ccbport = &H60S
            Case 2
                qway_snd.ccbport = &H61S
            Case Else
                qway_snd.ccbport = &HFFS
        End Select

        qway_snd.ccbdnode = node
        qway_snd.ccbsnode = 0
        qway_snd.ccbcmd = 0

        packsize = 50
        For i = 1 To data_len Step packsize
            cnt = data_len - i + 1
            If cnt > packsize Then cnt = packsize

            ret = BinCopy(qway_snd.ccbdata, 0, qwaydata, i - 1, cnt)
            'ret = BinCopy(qway_snd.ccbdata(), 0, databuf(), i - 1, cnt)
            qway_snd.ccblen = 6 + cnt

            ret = AB_GW_SndMsg(node, qway_snd)

            QwaySendDataB = ret
            If QwaySendDataB < 0 Then
                Exit For
            End If
        Next i
        Exit Function

QwaySendDataB99:
        QwaySendDataB = -2
    End Function
End Module


