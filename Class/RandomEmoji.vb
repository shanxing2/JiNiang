Public Class RandomEmoji
#Region "字段区"
    Private Shared ReadOnly m_Angry As String()
    Private Shared ReadOnly m_Happy As String()
    Private Shared ReadOnly m_Shock As String()
    Private Shared ReadOnly m_Sad As String()
    Private Shared ReadOnly m_Helpless As String()
    Private Shared ReadOnly m_Rand As Random

#End Region

#Region "构造函数区"
    ''' <summary>
    ''' 类构造函数
    ''' 类之内的任意一个静态方法第一次调用时调用此构造函数
    ''' 而且程序生命周期内仅调用一次
    ''' </summary>
    Shared Sub New()
        m_Angry = {",,Ծ‸Ծ,,", "(╯‵□′)╯︵┻━┻", "(╬ﾟдﾟ)▄︻┻┳═一"}
        m_Happy = {"=‿=✧", "●ω●", "(/ ▽ \\)", "(=・ω・=)", "(●'◡'●)ﾉ♥", "<(▰˘◡˘▰)>", "(⁄ ⁄•⁄ω⁄•⁄ ⁄)", "(ง,,• ᴗ •,,)ง ✧", ">ㅂ<ﾉ ☆"}
        m_Shock = {"Σ( ° △ °|||)︴", "┌( ಠ_ಠ)┘", "(ﾟДﾟ≡ﾟдﾟ)!?", "∑(っ °Д °;)っ"}
        m_Sad = {"＞︿＜", "＞△＜", "●︿●", "(´；ω；`)"}
        m_Helpless = {"◐▽◑", "ʅ（´◔౪◔）ʃ", "_(:3 」∠)_", "_(┐「ε:)_", "(°▽°)ﾉ", "←◡←", "_(•̀ᴗ•́ 」∠ ❀)_", "_φ(･ω･` )", "╮(￣▽￣)╭"}

        m_Rand = New Random(Date.Now.Millisecond)
    End Sub
#End Region

#Region "函数区"
    ''' <summary>
    ''' 生气
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function Angry() As String
        Return RandomEmoji(m_Angry)
    End Function

    ''' <summary>
    ''' 开森
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function Happy() As String
        Return RandomEmoji(m_Happy)
    End Function

    ''' <summary>
    ''' 震惊
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function Shock() As String
        Return RandomEmoji(m_Shock)
    End Function

    ''' <summary>
    ''' 桑心
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function Sad() As String
        Return RandomEmoji(m_Sad)
    End Function

    ''' <summary>
    ''' 阔怜无助弱小
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function Helpless() As String
        Return RandomEmoji(m_Helpless)
    End Function

    Private Shared Function RandomEmoji(ByRef emojiArray As String()) As String
        Return emojiArray(m_Rand.Next(0, emojiArray.Length))
    End Function
#End Region
End Class
