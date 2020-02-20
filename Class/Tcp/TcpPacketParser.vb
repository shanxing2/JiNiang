Imports System.Runtime.InteropServices
Imports System.Runtime.Serialization
Imports System.Text
Imports ShanXingTech
Imports ShanXingTech.Text2
Imports 姬娘插件.Events

''' <summary>
''' TCP包解析器
''' </summary>
Public NotInheritable Class TcpPacketParser



#Region "枚举区"

#End Region

#Region "事件区"
	''' <summary>
	''' 解析了数据包（单个或者多个）事件
	''' </summary>
	Public Event Parsed As EventHandler(Of ParsedEventArgs)
#End Region

#Region "字段区"

#End Region

#Region "属性区"
	''' <summary>
	''' 需要组包
	''' </summary>
	''' <returns></returns>
	Public Property NeedCombinePacket As Boolean
#End Region

#Region "常量区"
    Public ReadOnly HeaderSize As Integer = 16
#End Region

#Region "构造函数"
	'Sub New()
	'    '
	'End Sub
#End Region

#Region "函数区"
	Private Function ToByte(Of T As Structure)(struct As T) As Byte()
        Dim ptr As IntPtr
        Dim bytes = Array.Empty(Of Byte)()
        Try
            Dim size As Integer = Marshal.SizeOf(Of T)(struct)
            ptr = Marshal.AllocHGlobal(size)
            Marshal.StructureToPtr(struct, ptr, True)
            bytes = New Byte(size - 1) {}

            ' 从内存空间拷贝到byte 数组
            ' #########################
            '   因为暂时不会手动控制结构成员的布局顺序，无法直接使用下行代码来实现结构到数组的转换
            '   bytes.ToHexString
            '   "6e000000100001000700000001000000"
            'Marshal.Copy(ptr, bytes, 0, size)
            ' #########################

            ' short——2 integer-4 see Structure Header
            ' bytes.ToHexString
            ' "0000006e001000010000000700000001"
            Dim protocolHeader = New Byte() {4, 2, 2, 4, 4}
            Dim offset As Integer = 0
            Dim bIndex As Integer = 0
            For Each ph In protocolHeader
                offset += ph
                For i = offset - 1 To offset - ph Step -1
                    bytes(bIndex) = Marshal.ReadByte(ptr, i)
                    bIndex += 1
                Next
            Next
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            ' 释放非托管内存
            Marshal.FreeHGlobal(ptr)
        End Try

        Return bytes
    End Function

    ''' <summary>
    ''' 封包
    ''' </summary>
    ''' <param name="body">数据体</param>
    ''' <param name="OpCode">操作码</param>
    ''' <returns></returns>
    Public Function Pack(ByVal body As String, ByVal opCode As OpCode) As Byte()
        ' 生成数据体部分数组
        Dim bodyBytes = Encoding.ASCII.GetBytes(body)
        ' 生成协议头部分数组
        Dim header = New Header With {
            .PacketSize = bodyBytes.Length + 16,
            .Size = 16,
            .Protover = 1,
            .OpCode = opCode,
            .Sequence = 1
        }

        Dim bytes = ToByte(header)
        Dim headerSize = bytes.Length - 1
        ' 将数据体部分数组复制到协议头数组后面以合并两个数组
        ReDim Preserve bytes(bodyBytes.Length + headerSize)
        bodyBytes.CopyTo(bytes, headerSize + 1)

        Return bytes
    End Function

    ''' <summary>
    '''  获取协议头
    ''' </summary>
    ''' <param name="receiveBytes">从服务器接收到的字节数组</param>
    ''' <returns></returns>
    Private Function GetHeader(ByVal receiveBytes As Byte()) As Header
        ' 可能会因为多条数据串在一起或者是乱序，导致无法解析而有异常
        If receiveBytes.Length < Me.HeaderSize Then
            Throw New UnPackFailedException(receiveBytes, "数据长度小于协议头长度")
        End If

#Region "解析协议头(一个字节2个长度)"
        '头部格式：
        '偏移量 长度	含义
        '0      4	封包总大小
        '4      2	头部长度
        '6      2	协议版本，目前是0 、1、2
        '8      4	操作码（封包类型）
        '12     4	sequence，可以取常数1
        Dim receiveDataSize As Integer
        Dim headerSize As Short
        Dim protover As Short
        Dim opCode As OpCode
        Dim sequence As Integer
        Try
            ' 前4个字节先组成一个16进制数，然后转换为10进制数
            'receiveDataSize = Convert.ToUInt32(receiveBytes(0).ToString("x2") & receiveBytes(1).ToString("x2") & receiveBytes(2).ToString("x2") & receiveBytes(3).ToString("x2"), 16)
            receiveDataSize = CInt($"&H{receiveBytes(0).ToString("x2")}{receiveBytes(1).ToString("x2")}{receiveBytes(2).ToString("x2")}{receiveBytes(3).ToString("x2")}")
            headerSize = CShort(receiveBytes(4).ToString("x2").Insert(0, "&H") & receiveBytes(5).ToString("x2"))
            If Me.HeaderSize < headerSize Then
                Throw New PopularityMixedException
            End If

            protover = CShort(receiveBytes(6).ToString("x2").Insert(0, "&H") & receiveBytes(7).ToString("x2"))
            System.Enum.TryParse(CStr(CInt($"&H{receiveBytes(8).ToString("x2")}{receiveBytes(9).ToString("x2")}{receiveBytes(10).ToString("x2")}{receiveBytes(11).ToString("x2")}")), opCode)
            sequence = CInt($"&H{receiveBytes(12).ToString("x2")}{receiveBytes(13).ToString("x2")}{receiveBytes(14).ToString("x2")}{receiveBytes(15).ToString("x2")}")
        Catch ex As PopularityMixedException
            Throw
        Catch ex As Exception
            Throw New UnPackFailedException(receiveBytes， "解析协议头失败")
        End Try

        Dim header = New Header With {
            .PacketSize = receiveDataSize,
            .Size = headerSize,
            .Protover = protover,
            .OpCode = opCode,
            .Sequence = sequence
        }
#End Region

        Return header
    End Function

    ''' <summary>
    ''' 获取子信息包包头
    ''' </summary>
    ''' <param name="receiveBytes"></param>
    ''' <param name="totalDeCompressedByteLength"></param>
    ''' <returns></returns>
    Private Function GetHeader(ByRef receiveBytes As Byte(), ByRef totalDeCompressedByteLength As Integer) As Header
        ' 可能会因为多条数据串在一起或者是乱序，导致无法解析而有异常
        If receiveBytes.Length < Me.HeaderSize Then
            Throw New UnPackFailedException(receiveBytes， "数据长度小于协议头长度")
        End If

#Region "解析协议头(一个字节2个长度)"
        '头部格式：
        '偏移量 长度	含义
        '0      4	封包总大小
        '4      2	头部长度
        '6      2	协议版本，目前是0 、1、2
        '8      4	操作码（封包类型）
        '12     4	sequence，可以取常数1
        Dim receiveDataSize As Integer
        Dim headerSize As Short
        Dim protover As Short
        Dim opCode As OpCode
        Dim sequence As Integer
        Dim offset As Integer = totalDeCompressedByteLength

        ' 最多向前多取一位
        Try
            ' 头大小不等于16
            If receiveBytes(offset + 4) <> 0 OrElse receiveBytes(offset + 5) <> Me.HeaderSize Then
                offset += 1
            End If

            ' 前4个字节先组成一个16进制数，然后转换为10进制数
            'receiveDataSize = Convert.ToUInt32(receiveBytes(0).ToString("x2") & receiveBytes(1).ToString("x2") & receiveBytes(2).ToString("x2") & receiveBytes(3).ToString("x2"), 16)
            receiveDataSize = CInt($"&H{receiveBytes(offset).ToString("x2")}{receiveBytes(offset + 1).ToString("x2")}{receiveBytes(offset + 2).ToString("x2")}{receiveBytes(offset + 3).ToString("x2")}")
            headerSize = CShort(receiveBytes(offset + 4).ToString("x2").Insert(0, "&H") & receiveBytes(offset + 5).ToString("x2"))
        Catch ex As Exception
            Throw New UnPackFailedException(receiveBytes， "解析协议头失败")
        End Try
        If headerSize = 0 OrElse
            Me.HeaderSize < headerSize Then
            Throw New UnPackFailedException(receiveBytes， "解析协议头失败")
        End If

        protover = CShort(receiveBytes(offset + 6).ToString("x2").Insert(0, "&H") & receiveBytes(offset + 7).ToString("x2"))
        System.Enum.TryParse(CStr(CInt($"&H{receiveBytes(offset + 8).ToString("x2")}{receiveBytes(offset + 9).ToString("x2")}{receiveBytes(offset + 10).ToString("x2")}{receiveBytes(offset + 11).ToString("x2")}")), opCode)
        sequence = CInt($"&H{receiveBytes(offset + 12).ToString("x2")}{receiveBytes(offset + 13).ToString("x2")}{receiveBytes(offset + 14).ToString("x2")}{receiveBytes(offset + 15).ToString("x2")}")


        Dim header = New Header With {
            .PacketSize = receiveDataSize,
            .Size = headerSize,
            .Protover = protover,
            .OpCode = opCode,
            .Sequence = sequence
        }
#End Region

        If offset > totalDeCompressedByteLength Then
            totalDeCompressedByteLength = offset
        End If

        Return header
    End Function

    ''' <summary>
    ''' 解包
    ''' </summary>
    ''' <param name="receiveBytes">从服务器接收到的字节数组</param>
    ''' <returns></returns>
    Public Async Function UnPackAsync(ByVal receiveBytes As Byte()) As Task(Of Integer)
        ' 处理乱包？
        'Dim removeCount = TryRemoveDataBetweenNullCharOld(receiveBytes)

        Dim receiveData = String.Empty
        ' 已经从整个封包中解包的字节数
        Dim totalUpPacketByteCount As Integer = 0

        Try
            Dim header As Header
            Dim subBuffer = Array.Empty(Of Byte)()
            NeedCombinePacket = False

            While totalUpPacketByteCount < receiveBytes.Length
                If Not FillSubBuffer(receiveBytes, subBuffer, totalUpPacketByteCount, header) Then
                    NeedCombinePacket = True
                    Exit While
                End If

                If header.PacketSize < 16 Then
#Region "未知包"
                    ' 如果 PostSize 长度小于16，那可能是未知包 不需要再解析数据体

                    Dim sb = StringBuilderCache.Acquire(16)
                    For i = 0 To subBuffer.Length - 1
                        sb.Append(receiveBytes(i).ToString("x2"))
                    Next
                    receiveData = StringBuilderCache.GetStringAndReleaseBuilder(sb)

                    Dim tcpPacketData = New TcpPacketData With {
                        .Header = header,
                        .Body = receiveData
                    }
                    RaiseEvent Parsed(Nothing, New ParsedEventArgs(tcpPacketData))

                    Logger.WriteLine("接收到未知信息: " & receiveData)
#End Region
                ElseIf header.PacketSize = 16 Then
#Region "心跳包"
                    ' 如果长度为16，表示这是一个心跳包

                    receiveData = subBuffer.ToHexString(UpperLowerCase.Lower)

                    Dim tcpPacketData = New TcpPacketData With {
                        .Header = header,
                        .Body = receiveData
                    }
                    RaiseEvent Parsed(Nothing, New ParsedEventArgs(tcpPacketData))

                    'Debug.Print(Logger.MakeDebugString("接收到信息: " & receiveData & Environment.NewLine))
#End Region
                ElseIf header.PacketSize = 20 Then
#Region "人气值包"
                    ' 这表示是一个人气值包,直播页面上显示 -,表示轮播
                    ' 直接解析后面的人气值包的内容(后4个字节)，前面的心跳包不要了
                    ' 人气值包如果跟其他包一起发来，前后会分别有‘0’作为分隔符，如 ‘00 0000001400100001000000030000000100022acf 00（没有空格）’
                    receiveData = $"&H{subBuffer(16).ToString("x2")}{subBuffer(17).ToString("x2")}{subBuffer(18).ToString("x2")}{subBuffer(19).ToString("x2")}"

                    Dim tcpPacketData As New TcpPacketData With {
                        .Header = header,
                        .Body = receiveData
                    }
                    RaiseEvent Parsed(Nothing, New ParsedEventArgs(tcpPacketData))

                    Debug.Print(Logger.MakeDebugString("接收到信息:" & receiveData & " 十进制为 " & CInt(receiveData).ToStringOfCulture))
#End Region
                ElseIf header.PacketSize = 36 Then
#Region "心跳包+人气值包"
                    ' 第一次向服务器发送心跳包之后，服务器会返回
                    ' 直接解析后面的人气值包的内容(后20个字节)，前面的心跳包不要了
                    Await UnPackAsync(receiveBytes.SubArray(16, 20))
#End Region
                Else
#Region "弹幕、消息、？包"
                    Dim unPacketCount = Await InternalUnPackAsync(subBuffer, header)
                    If unPacketCount <> header.PacketSize Then
                        totalUpPacketByteCount += unPacketCount
                        Exit While
                    End If
#End Region
                End If

                totalUpPacketByteCount += header.PacketSize
                Debug.Print(Logger.MakeDebugString("已解码字节长度：" & totalUpPacketByteCount.ToStringOfCulture))
            End While

        Catch ex As UnPackFailedException
            Logger.WriteLine(ex, receiveBytes.ToHexString,,,)
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        ' 尝试跳过分隔符
        If totalUpPacketByteCount + 1 = receiveBytes.Length AndAlso
         0 = receiveBytes(totalUpPacketByteCount) Then
            totalUpPacketByteCount += 1
        End If

        Debug.Print(Logger.MakeDebugString("（单包/多包）总解码字节长度：" & totalUpPacketByteCount.ToStringOfCulture))

        Return totalUpPacketByteCount
    End Function

    ''' <summary>
    ''' 填充子数组
    ''' </summary>
    ''' <param name="receiveBytes">当前接收到的整个信息包数组</param>
    ''' <param name="subBytes">欲填充的子数组</param>
    ''' <param name="totalUpPacketByteCount">已经从整个封包中解包的字节数</param>
    ''' <param name="header"></param>
    ''' <returns></returns>
    Private Function FillSubBuffer(ByRef receiveBytes As Byte(), ByRef subBytes As Byte(), ByRef totalUpPacketByteCount As Integer, ByRef header As Header) As Boolean
        ' 取不了下个包就返回
        If totalUpPacketByteCount >= receiveBytes.Length OrElse
            totalUpPacketByteCount + HeaderSize > receiveBytes.Length Then Return False

        ' 先获取协议头 再根据协议头获取数据体
        header = GetHeader(receiveBytes, totalUpPacketByteCount)

        ' 缺包处理 返回未能解压的数组
        If totalUpPacketByteCount + header.PacketSize > receiveBytes.Length Then
            Return False
        End If

        subBytes = receiveBytes.SubArray(totalUpPacketByteCount, header.PacketSize)

        Return True
    End Function

    ''' <summary>
    ''' 解包
    ''' </summary>
    ''' <param name="receiveBytes"></param>
    ''' <returns></returns>
    Private Async Function InternalUnPackAsync(ByVal receiveBytes As Byte()， ByVal header As Header) As Task(Of Integer)
#Region "zlib帮助信息"
        ' zlib https://tools.ietf.org/html/rfc1950
        '  CMF |  FLG
        '78 01 - No Compression/low
        '78 9C - Default Compression
        '78 DA - Best Compression 
        ' ZLIB/GZIP headers
        'Level | ZLIB  | GZIP 
        '1   | 78 01 | 1F 8B 
        '2   | 78 5E | 1F 8B 
        '3   | 78 5E | 1F 8B 
        '4   | 78 5E | 1F 8B 
        '5   | 78 5E | 1F 8B 
        '6   | 78 9C | 1F 8B 
        '7   | 78 DA | 1F 8B 
        '8   | 78 DA | 1F 8B 
        '9   | 78 DA | 1F 8B 
#End Region

        Dim receiveData = String.Empty
        Dim totalDeCompressedByteLength As Integer = 0

        Dim subCompressBytes As Byte() = New Byte(header.PacketSize - header.Size - 1) {}
        Array.Copy(receiveBytes, header.Size + totalDeCompressedByteLength, subCompressBytes, 0, header.PacketSize - header.Size)

        ' 解析里面所有包
        While totalDeCompressedByteLength < receiveBytes.Length
            ' Deflate 算法压缩之后的数据，第一个字节是 78h（120b），第二个字节是 DAh(218b)
            ' 不满足这个条件的话就是未压缩过的数据，可以直接用utf8解码
            Dim needDecompress = (subCompressBytes(0) = 120 AndAlso subCompressBytes(1) = 218)
            Debug.Print(Logger.MakeDebugString("需要解压 = " & needDecompress.ToString))
            If needDecompress Then
#Region "需要解压"
                Debug.Print(Logger.MakeDebugString("需要解压的子包字节长度：" & subCompressBytes.Length.ToStringOfCulture))
                ' 解压
                Dim deCompressBytes = Await subCompressBytes.DeCompressAsync
                ' 如果解压出来的字节数组小于等于16个字节，说明是心跳包，或者是其他未知包，不是弹幕数据包
                If deCompressBytes.Length = 0 Then
                    Throw New UnPackFailedException(subCompressBytes， "缺包")
                ElseIf deCompressBytes.Length > 0 AndAlso deCompressBytes.Length <= header.Size Then
                    Dim sb = StringBuilderCache.Acquire(16)
                    For bIndex = 0 To deCompressBytes.Length
                        sb.Append(receiveBytes(bIndex).ToString("x2"))
                    Next
                    receiveData = StringBuilderCache.GetStringAndReleaseBuilder(sb)

                    Dim tcpPacketData = New TcpPacketData With {
                        .Header = header,
                        .Body = receiveData
                    }
                    RaiseEvent Parsed(Nothing, New ParsedEventArgs(tcpPacketData))
                Else
                    ' 解包
                    For Each subTcpPacketData In ParsePacketDeCompressed(deCompressBytes)
                        RaiseEvent Parsed(Nothing, New ParsedEventArgs(subTcpPacketData))
                    Next
                End If
#End Region
            Else
#Region "不需要解压"
                ' 这种包的类型有 ROOM_RANK、SYS_MSG、人气值
                receiveData = Encoding.UTF8.GetString(subCompressBytes)
                receiveData = receiveData.TryRemoveNewLine()
                'receiveData = receiveData.TryUnescape()

                Dim subTcpPacketData As New TcpPacketData With {
                    .Header = header,
                    .Body = receiveData
                }
                RaiseEvent Parsed(Nothing, New ParsedEventArgs(subTcpPacketData))
#End Region
            End If

#Region "下一个包"
            totalDeCompressedByteLength += header.PacketSize

            Debug.Print(Logger.MakeDebugString("已解码字节长度：" & totalDeCompressedByteLength.ToStringOfCulture))

            If totalDeCompressedByteLength = receiveBytes.Length Then Exit While

            ' 先获取协议头 再根据协议头获取数据体
            header = GetHeader(receiveBytes， totalDeCompressedByteLength)

            ' 缺包处理 返回未能解压的数组
            If totalDeCompressedByteLength + header.PacketSize > receiveBytes.Length Then
                Throw New UnPackFailedException(subCompressBytes， "缺包")
            End If

            subCompressBytes = New Byte(header.PacketSize - header.Size - 1) {}
            Array.Copy(receiveBytes, header.Size + totalDeCompressedByteLength, subCompressBytes, 0, header.PacketSize - header.Size)
#End Region
        End While

        Return totalDeCompressedByteLength
    End Function

    ''' <summary>
    ''' 解析已经解压过的信息包，不能用于解析未解压过的信息包
    ''' </summary>
    ''' <param name="deCompressBytes">解压过的tcp包字节数组</param>
    ''' <returns></returns>
    Private Iterator Function ParsePacketDeCompressed(ByVal deCompressBytes As Byte()) As IEnumerable(Of TcpPacketData)
        Dim receiveData As String
        Dim index As Integer '= 0
        Dim deCompressCount As Integer '= 0
        Dim startIndex As Integer '= 0

        While startIndex < deCompressBytes.Length
            ' 缺包处理 返回未能解压的数组
            If startIndex + HeaderSize > deCompressBytes.Length Then
                Throw New UnPackFailedException(deCompressBytes.SubArray(startIndex, HeaderSize)， "缺包")
            End If

            'Dim header = GetHeader(deCompressBytes.SubArray(startIndex, HeaderSize))
            Dim header = GetHeader(deCompressBytes, startIndex)

            index = startIndex + header.Size
            deCompressCount = header.PacketSize - header.Size

            ' 此包可能是为压缩的单个包；也可能是一个压缩之后的包，里面有一个包，或者是多个包
            If index > deCompressBytes.Length OrElse deCompressCount > deCompressBytes.Length Then
                ' 由于数据乱序，可能会导致无法解压、解析协议头错误等，
                receiveData = Encoding.UTF8.GetString(deCompressBytes)
            Else
                receiveData = Encoding.UTF8.GetString(deCompressBytes, index, deCompressCount)
            End If

            receiveData = receiveData.TryRemoveNewLine()
            ' 由于json反序列化弹幕中包含的转义字符（比如 '"'）会异常，所以现在不进行转义 2018 06 15
            'receiveData = receiveData.TryUnescape()

            Dim subTcpPacketData = New TcpPacketData With {
                .Header = header,
                .Body = receiveData
            }

            startIndex += header.PacketSize
            Yield subTcpPacketData
        End While
    End Function
#End Region

#Region "内部类"


    ''' <summary>
    ''' 解包失败异常
    ''' </summary>
    <Serializable>
    Private Class UnPackFailedException
        Inherits Exception

        Public ReadOnly Property UnPackFailedBuffer As Byte()
        Public Overrides ReadOnly Property Message As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="unPackFailedBuffer">解包失败的字节数组</param>
        Public Sub New(unPackFailedBuffer() As Byte)
            Me.UnPackFailedBuffer = unPackFailedBuffer
        End Sub

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="unPackFailedBuffer">解包失败的字节数组</param>
        ''' <param name="message">错误信息</param>
        Public Sub New(unPackFailedBuffer() As Byte, message As String)
            Me.UnPackFailedBuffer = unPackFailedBuffer
            Me.Message = message
        End Sub

        Public Overrides Sub GetObjectData(info As SerializationInfo, context As StreamingContext)
            MyBase.GetObjectData(info, context)
        End Sub
    End Class

    ''' <summary>
    ''' 人气值与弹幕混合
    ''' </summary>
    <Serializable>
    Private Class PopularityMixedException
        Inherits Exception
        ' 人气值包如果跟其他包一起发来，前后会分别有‘0’作为分隔符，如 ‘00 0000001400100001000000030000000100022acf 00（没有空格）’
    End Class
#End Region

End Class
