Option Strict Off
Option Explicit On 


Public Module HexIp


    Function HexIp0(ByRef ip_str As String, ByRef op_str As String) As Short
        Dim start, cnt As Short
        Dim tmpstr As String

        op_str = ""
        start = 1
        cnt = 0
        Do
            tmpstr = Mid(ip_str, start, 1)
            start = start + 1
            If tmpstr = "" Then Exit Do
            If tmpstr = "\" Then
                tmpstr = "&H"
                tmpstr = tmpstr & Mid(ip_str, start, 2)
                start = start + 2
                tmpstr = Chr(Val(tmpstr))
            End If
            op_str = op_str & tmpstr
            cnt = cnt + 1
        Loop
        HexIp0 = cnt
    End Function

    Function HexIpB(ByRef ip_str As String, ByRef op_str() As Byte, ByRef op_start As Short) As Short
        Dim start, cnt As Short
        Dim tmpstr As String
        Dim data As Short
        Dim data2 As Integer

        start = 1
        cnt = 0 + op_start
        Do
            tmpstr = Mid(ip_str, start, 1)
            start = start + 1
            If tmpstr = "" Then Exit Do
            If tmpstr = "\" Then
                tmpstr = "&H" & Mid(ip_str, start, 2)
                start = start + 2
                op_str(cnt) = Val(tmpstr)
                cnt = cnt + 1
            Else
                data2 = Asc(tmpstr)
                If data2 < 0 Then data2 = data2 + 65536
                If data2 < 256 Then
                    op_str(cnt) = (data2 Mod 256) And &HFFS
                    cnt = cnt + 1
                Else
                    op_str(cnt) = (data2 \ 256) And &HFFS
                    cnt = cnt + 1
                    op_str(cnt) = (data2 Mod 256) And &HFFS
                    cnt = cnt + 1
                End If
            End If
        Loop
        HexIpB = cnt

    End Function


    Function HexOp0(ByRef ip_str As Object, ByRef cnt As Short, ByRef op_str As String) As Short
        Dim start As Short
        Dim tmpstr As String
        Dim ch As Short

        op_str = ""
        For start = 1 To cnt
            'UPGRADE_WARNING: 未能解析对象 ip_str 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
            ch = Asc(Mid(ip_str, start, 1))
            'If ch = 0 Or ch = &H5C Or ch > &H80 Then
            If ch < &H20S Or ch = &H5CS Or ch >= &H80S Then
                tmpstr = Hex(ch)
                If Len(tmpstr) < 2 Then
                    op_str = op_str & "\0" & tmpstr
                Else
                    op_str = op_str & "\" & tmpstr
                End If
            Else
                op_str = op_str & Chr(ch)
            End If
        Next start
    End Function

    Function HexOp2(ByRef ip_str As Object, ByRef cnt As Short) As String
        Dim ret As Short
        Dim op_str As String

        If cnt < 0 Then cnt = Len(ip_str)

        ret = HexOp0(ip_str, cnt, op_str)
        HexOp2 = op_str
    End Function

    Function ldump(ByRef ip_str As Object, ByRef op_str As Object) As Short
        Dim start As Short
        Dim tmpstr As String

        'UPGRADE_WARNING: 未能解析对象 op_str 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
        op_str = ""
        start = 1
        Do
            'UPGRADE_WARNING: 未能解析对象 ip_str 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
            tmpstr = Mid(ip_str, start, 1)
            start = start + 1
            If tmpstr = "" Then Exit Do
            tmpstr = Hex(Asc(tmpstr)) & " "
            If Len(tmpstr) < 3 Then
                tmpstr = "0" & tmpstr
            End If
            'UPGRADE_WARNING: 未能解析对象 op_str 的默认属性。 单击以获得更多信息:“ms-help://MS.VSCC.2003/commoner/redir/redirect.htm?keyword="vbup1037"”
            op_str = op_str + tmpstr
        Loop

    End Function

    Function ldump2(ByRef ip_str As String, ByRef cnt As Short) As String
        Dim ret, start As Short
        Dim tmpstr, op_str As String

        op_str = ""
        If cnt < 0 Then cnt = Len(ip_str)
        start = 1
        Do
            tmpstr = Mid(ip_str, start, 1)
            start = start + 1
            If tmpstr = "" Then Exit Do
            tmpstr = Hex(Asc(tmpstr)) & " "
            If Len(tmpstr) < 3 Then
                tmpstr = "0" & tmpstr
            End If
            op_str = op_str & tmpstr

            If start > cnt Then Exit Do
        Loop

        ldump2 = op_str
    End Function

    Function ldump3(ByRef ip_str() As Byte, ByRef start As Short, ByRef cnt As Short) As String
        Dim ret, i As Short
        Dim tmpstr, op_str As String

        op_str = ""
        For i = 0 To cnt - 1
            tmpstr = Hex(ip_str(start + i)) & " "
            If Len(tmpstr) < 3 Then
                tmpstr = "0" & tmpstr
            End If
            op_str = op_str & tmpstr
        Next i

        ldump3 = op_str
    End Function

    Function str2hex(ByRef tmpstr As String) As String
        '// change string to Hex format string (Double-Byte Code)
        Dim cnt, i As Short
        Dim tmpstr2 As String
        Dim data As Short

        cnt = Len(tmpstr)
        tmpstr2 = ""
        For i = 1 To cnt
            data = Asc(Mid(tmpstr, i, 1)) 'double byte code
            If (data And &HFF00S) = 0 Then
                tmpstr2 = tmpstr2 & Mid(Hex(&H100S + data), 2)
            Else
                tmpstr2 = tmpstr2 & Mid(Hex(&H10000 + CShort(data And &HFFFF)), 2)
            End If
        Next i
        str2hex = tmpstr2
    End Function
End Module

