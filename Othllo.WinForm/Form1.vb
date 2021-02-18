Option Strict Off
Imports Othello.Shared
Imports System.Reflection

Public Class Form1
    Dim boardState As BoardState
    Dim aiBlack As IAi
    Dim aiWhite As IAi

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.boardState = New BoardState()
        RefreshScreen()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Enabled = False

        aiBlack = CreateAiObject(Piece.Black)
        aiWhite = CreateAiObject(Piece.White)

        Me.boardState = New BoardState()

        RefreshScreen()

        Timer1.Interval = 500
        Timer1.Start()
    End Sub

    Private Function CreateAiObject(piece As Piece) As IAi
        'DLLをAssemblyにロードする
        Dim asm = Assembly.LoadFrom(GetAppPath() & "\AI\sampleAI.dll")

        'クラスをインスタンス化
        Return CType(asm.CreateInstance("sampleAI.AI",
                                    False,
                                    BindingFlags.CreateInstance,
                                    Nothing,
                                    New Object() {piece},
                                    Nothing,
                                    Nothing), IAi)
    End Function

    Private Function GetAppPath() As String
        Return IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()

        Dim ai = GetAi()
        Dim point = ai.HitPiece(Me.boardState.Board)

        Me.boardState.ExecuteTurn(point)

        RefreshScreen()

        If EndCheck() = False Then
            Timer1.Start()
        End If
    End Sub

    Private Function GetAi() As IAi
        Dim ai As IAi
        If Me.boardState.TurnPiece = Piece.Black Then
            ai = Me.aiBlack
        Else
            ai = Me.aiWhite
        End If

        Return ai
    End Function

    Private Function EndCheck() As Boolean
        If boardState.GetPossibleSetStoneCount(Piece.Black) = 0 And boardState.GetPossibleSetStoneCount(Piece.White) = 0 Then
            Dim blackCount As Integer = Me.boardState.PieceCount(Piece.Black)
            Dim whiteCount As Integer = Me.boardState.PieceCount(Piece.White)

            If blackCount = whiteCount Then
                MsgBox("引き分け")
            ElseIf blackCount > whiteCount Then
                MsgBox("黒の勝利です")
            ElseIf blackCount < whiteCount Then
                MsgBox("白の勝利です")
            End If

            Button1.Enabled = True

            Return True
        End If

        Return False
    End Function

    Private Sub RefreshScreen()
        If Me.boardState.TurnPiece = Piece.Black Then
            lblTop.Text = "黒の手番です"
        Else
            lblTop.Text = "白の手番です"
        End If

        For x As Integer = 0 To BoardState.MAX_POS_X - 1
            For y As Integer = 0 To BoardState.MAX_POS_Y - 1
                Dim s As String = ""
                Select Case Me.boardState.Board(x, y)
                    Case Piece.None
                        s = ""
                    Case Piece.Black
                        s = "●"
                    Case Piece.White
                        s = "○"
                End Select

                TableLayoutPanel1.Controls("pos" & x & y).Text = s
            Next
        Next

        lblBlackCount.Text = Me.boardState.PieceCount(Piece.Black)
        lblWhiteCount.Text = Me.boardState.PieceCount(Piece.White)
    End Sub
End Class
