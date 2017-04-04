Public Class GameForm

    Dim Grid(0 To 3, 0 To 3) As PictureBox
    Dim OldGrid(0 To 3, 0 To 3) As Long
    Dim NewGrid(0 To 3, 0 To 3) As Long
    Dim BestScore As Long
    Dim OldScore As Long
    Dim NewScore As Long
    Dim Downx As Long
    Dim Downy As Long
    Dim Upx As Long
    Dim Upy As Long
    Dim isAgain As Boolean
    Dim isDisplay As Boolean
    Dim isExit As Boolean
    Dim isExitConfirm As Boolean
    Dim isOver As Boolean

    Private Sub GameForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If isExitConfirm Then
            WriteData()
            e.Cancel = False
        Else
            If MovePanelTimer1.Enabled = False And MovePanelTimer2.Enabled = False Then
                If Panel.Top <> 75 Then
                    isExit = True
                    Panel.Image = My.Resources.Panel1
                    Panel.Visible = True
                    MovePanelTimer1.Enabled = True
                Else
                    isOver = False
                    MovePanelTimer2.Enabled = True
                End If
            End If
            e.Cancel = True
        End If
    End Sub

    Private Sub GameForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If MovePanelTimer1.Enabled = False And MovePanelTimer2.Enabled = False Then
            If Panel.Top <> 75 Then
                If RandomTimer.Enabled = False Then
                    Select Case e.KeyCode
                        Case Keys.Up
                            MoveUp()
                        Case Keys.Down
                            MoveDown()
                        Case Keys.Left
                            MoveLeft()
                        Case Keys.Right
                            MoveRight()
                        Case Keys.W
                            MoveUp()
                        Case Keys.S
                            MoveDown()
                        Case Keys.A
                            MoveLeft()
                        Case Keys.D
                            MoveRight()
                        Case Keys.NumPad8
                            MoveUp()
                        Case Keys.NumPad2
                            MoveDown()
                        Case Keys.NumPad4
                            MoveLeft()
                        Case Keys.NumPad6
                            MoveRight()
                    End Select
                End If
                If e.KeyCode = Keys.Escape Then Me.Close()
            Else
                If e.KeyCode = Keys.Escape Then
                    isOver = False
                    MovePanelTimer2.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub GameForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim j As Integer
        Dim isClean As Boolean
        Grid = {{Grid1, Grid2, Grid3, Grid4}, {Grid5, Grid6, Grid7, Grid8}, {Grid9, Grid10, Grid11, Grid12}, {Grid13, Grid14, Grid15, Grid16}}
        ReadData()
        Me.Text = "FLAT 2048 - 当前:" & OldScore
        isClean = True
        isDisplay = True
        isExit = True
        isExitConfirm = False
        isOver = False
        isAgain = False
        For i = 0 To 3
            For j = 0 To 3
                If OldGrid(i, j) <> 0 Then isClean = False
            Next
        Next
        If isClean Then
            RandomGrid()
            RandomGrid()
        End If
        RefreshGrid()
        SetOpacityTimer1.Enabled = True
    End Sub

    Private Sub GameForm_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown, Grid1.MouseDown, Grid2.MouseDown, Grid3.MouseDown, Grid4.MouseDown, Grid5.MouseDown, Grid6.MouseDown, Grid7.MouseDown, Grid8.MouseDown, Grid9.MouseDown, Grid10.MouseDown, Grid11.MouseDown, Grid12.MouseDown, Grid13.MouseDown, Grid14.MouseDown, Grid15.MouseDown, Grid16.MouseDown
        Downx = e.X
        Downy = e.Y
    End Sub

    Private Sub GameForm_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp, Grid1.MouseUp, Grid2.MouseUp, Grid3.MouseUp, Grid4.MouseUp, Grid5.MouseUp, Grid6.MouseUp, Grid7.MouseUp, Grid8.MouseUp, Grid9.MouseUp, Grid10.MouseUp, Grid11.MouseUp, Grid12.MouseUp, Grid13.MouseUp, Grid14.MouseUp, Grid15.MouseUp, Grid16.MouseUp
        Dim Deltax As Long
        Dim Deltay As Long
        Upx = e.X
        Upy = e.Y
        If RandomTimer.Enabled = False And MovePanelTimer1.Enabled = False And MovePanelTimer2.Enabled = False Then
            If Panel.Top <> 75 Then
                Deltax = Downx - Upx
                Deltay = Downy - Upy
                If Deltay >= Math.Abs(Deltax) + 10 Then MoveUp()
                If Deltay <= -(Math.Abs(Deltax) + 10) Then MoveDown()
                If Deltax >= Math.Abs(Deltay) + 10 Then MoveLeft()
                If Deltax <= -(Math.Abs(Deltay) + 10) Then MoveRight()
            Else
                isOver = False
                MovePanelTimer2.Enabled = True
            End If
        End If
    End Sub

    Private Sub MovePanelTimer1_Tick(sender As Object, e As EventArgs) Handles MovePanelTimer1.Tick
        If Panel.Top < 75 Then
            Panel.Top = Panel.Top + 15
        Else
            MovePanelTimer1.Enabled = False
        End If
    End Sub

    Private Sub MovePanelTimer2_Tick(sender As Object, e As EventArgs) Handles MovePanelTimer2.Tick
        If Panel.Top < 270 Then
            Panel.Top = Panel.Top + 15
        Else
            MovePanelTimer2.Enabled = False
            Panel.Visible = False
            Panel.Top = -120
            If isAgain = True Then
                ResetGame()
            End If
        End If
    End Sub

    Private Sub Panel_MouseClick(sender As Object, e As MouseEventArgs) Handles Panel.MouseClick
        If Panel.Top = 75 Then
            If isExit Then
                If (e.X >= 13 And e.X <= 93) And (e.Y >= 78 And e.Y <= 102) Then
                    isOver = False
                    MovePanelTimer2.Enabled = True
                End If
                If (e.X >= 107 And e.X <= 153) And (e.Y >= 78 And e.Y <= 102) Then
                    SetOpacityTimer2.Enabled = True
                End If
                If (e.X >= 167 And e.X <= 247) And (e.Y >= 78 And e.Y <= 102) Then
                    isAgain = True
                    MovePanelTimer2.Enabled = True
                End If
            Else
                If (e.X >= 52 And e.X <= 98) And (e.Y >= 78 And e.Y <= 102) Then
                    SetOpacityTimer2.Enabled = True
                End If
                If (e.X >= 128 And e.X <= 208) And (e.Y >= 78 And e.Y <= 102) Then
                    isAgain = True
                    MovePanelTimer2.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub PlusScoreTimer_Tick(sender As Object, e As EventArgs) Handles PlusScoreTimer.Tick
        Dim i As Long
        If OldScore <> NewScore Then
            RollScoreTimer.Enabled = False
            i = Len(CStr(NewScore - OldScore))
            If OldScore + i ^ i <= NewScore Then
                OldScore = OldScore + i ^ i
            Else
                OldScore = NewScore
            End If
        Else
            PlusScoreTimer.Enabled = False
            isDisplay = True
            RollScoreTimer.Enabled = True
        End If
        Me.Text = "FLAT 2048 - 当前:" & OldScore
    End Sub

    Private Sub RandomTimer_Tick(sender As Object, e As EventArgs) Handles RandomTimer.Tick
        RandomTimer.Enabled = False
        RandomGrid()
        RefreshGrid()
    End Sub

    Private Sub RollScoreTimer_Tick(sender As Object, e As EventArgs) Handles RollScoreTimer.Tick
        If isDisplay Then
            Me.Text = "FLAT 2048 - 最高:" & BestScore
        Else
            Me.Text = "FLAT 2048 - 当前:" & NewScore
        End If
        isDisplay = Not isDisplay
    End Sub

    Private Sub SetOpacityTimer1_Tick(sender As Object, e As EventArgs) Handles SetOpacityTimer1.Tick
        SetOpacityTimer2.Enabled = False
        If Me.Opacity < 0.9 Then
            Me.Opacity = Me.Opacity + 0.05
        Else
            SetOpacityTimer1.Enabled = False
            RollScoreTimer.Enabled = True
        End If
    End Sub

    Private Sub SetOpacityTimer2_Tick(sender As Object, e As EventArgs) Handles SetOpacityTimer2.Tick
        SetOpacityTimer1.Enabled = False
        If Me.Opacity > 0 Then
            Me.Opacity = Me.Opacity - 0.05
        Else
            SetOpacityTimer2.Enabled = False
            isExitConfirm = True
            Me.Close()
        End If
    End Sub

    Private Sub MoveDown()
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        UpdateNewGrid()
        For i = 0 To 3
            For j = 3 To 1 Step -1
                k = j
                Do
                    k = k - 1
                Loop Until NewGrid(k, i) <> 0 Or k = 0
                If NewGrid(j, i) = NewGrid(k, i) Then
                    NewGrid(k, i) = 0
                    NewGrid(j, i) = 2 * NewGrid(j, i)
                    NewScore = NewScore + NewGrid(j, i)
                End If
            Next
            For j = 3 To 1 Step -1
                If NewGrid(j, i) = 0 Then
                    k = j
                    Do
                        k = k - 1
                    Loop Until NewGrid(k, i) <> 0 Or k = 0
                    If NewGrid(k, i) <> 0 Then
                        NewGrid(j, i) = NewGrid(k, i)
                        NewGrid(k, i) = 0
                    End If
                End If
            Next
        Next
        MoveGrid()
    End Sub

    Private Sub MoveGrid()
        Dim i As Integer
        Dim j As Integer
        Dim isMove As Boolean
        isMove = False
        For i = 0 To 3
            For j = 0 To 3
                If NewGrid(i, j) <> OldGrid(i, j) Then isMove = True
            Next
        Next
        If isMove Then
            If NewScore > BestScore Then BestScore = NewScore
            PlusScoreTimer.Enabled = True
            UpdateOldGrid()
            RefreshGrid()
            RandomTimer.Enabled = True
        Else
            TestOver()
        End If
    End Sub

    Private Sub MoveLeft()
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        UpdateNewGrid()
        For i = 0 To 3
            For j = 0 To 2
                k = j
                Do
                    k = k + 1
                Loop Until NewGrid(i, k) <> 0 Or k = 3
                If NewGrid(i, j) = NewGrid(i, k) Then
                    NewGrid(i, k) = 0
                    NewGrid(i, j) = 2 * NewGrid(i, j)
                    NewScore = NewScore + NewGrid(i, j)
                End If
            Next
            For j = 0 To 2
                If NewGrid(i, j) = 0 Then
                    k = j
                    Do
                        k = k + 1
                    Loop Until NewGrid(i, k) <> 0 Or k = 3
                    If NewGrid(i, k) <> 0 Then
                        NewGrid(i, j) = NewGrid(i, k)
                        NewGrid(i, k) = 0
                    End If
                End If
            Next
        Next
        MoveGrid()
    End Sub

    Private Sub MoveRight()
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        UpdateNewGrid()
        For i = 0 To 3
            For j = 3 To 1 Step -1
                k = j
                Do
                    k = k - 1
                Loop Until NewGrid(i, k) <> 0 Or k = 0
                If NewGrid(i, j) = NewGrid(i, k) Then
                    NewGrid(i, k) = 0
                    NewGrid(i, j) = 2 * NewGrid(i, j)
                    NewScore = NewScore + NewGrid(i, j)
                End If
            Next
            For j = 3 To 1 Step -1
                If NewGrid(i, j) = 0 Then
                    k = j
                    Do
                        k = k - 1
                    Loop Until NewGrid(i, k) <> 0 Or k = 0
                    If NewGrid(i, k) <> 0 Then
                        NewGrid(i, j) = NewGrid(i, k)
                        NewGrid(i, k) = 0
                    End If
                End If
            Next
        Next
        MoveGrid()
    End Sub

    Private Sub MoveUp()
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        UpdateNewGrid()
        For i = 0 To 3
            For j = 0 To 2
                k = j
                Do
                    k = k + 1
                Loop Until NewGrid(k, i) <> 0 Or k = 3
                If NewGrid(j, i) = NewGrid(k, i) Then
                    NewGrid(k, i) = 0
                    NewGrid(j, i) = 2 * NewGrid(j, i)
                    NewScore = NewScore + NewGrid(j, i)
                End If
            Next
            For j = 0 To 2
                If NewGrid(j, i) = 0 Then
                    k = j
                    Do
                        k = k + 1
                    Loop Until NewGrid(k, i) <> 0 Or k = 3
                    If NewGrid(k, i) <> 0 Then
                        NewGrid(j, i) = NewGrid(k, i)
                        NewGrid(k, i) = 0
                    End If
                End If
            Next
        Next
        MoveGrid()
    End Sub

    Private Sub RandomGrid()
        Dim i As Integer
        Dim j As Integer
        Dim k As Double
        Dim isExist As Boolean
        isExist = False
        For i = 0 To 3
            For j = 0 To 3
                If OldGrid(i, j) = 0 Then isExist = True
            Next
        Next
        If isExist Then
            Do
                Randomize()
                i = Int((3 - 0 + 1) * Rnd() + 0)
                Randomize()
                j = Int((3 - 0 + 1) * Rnd() + 0)
            Loop Until OldGrid(i, j) = 0
            Randomize()
            k = Rnd()
            If (k >= 0.45) And (k < 0.55) Then
                OldGrid(i, j) = 4
                Grid(i, j).Image = My.Resources.Grid2
                Grid(i, j).Visible = True
            Else
                OldGrid(i, j) = 2
                Grid(i, j).Image = My.Resources.Grid1
                Grid(i, j).Visible = True
            End If
        End If
    End Sub

    Private Sub ReadData()
        BestScore = My.Settings.BestScore
        OldScore = My.Settings.CurrentScore
        NewScore = My.Settings.CurrentScore
        OldGrid(0, 0) = My.Settings.Grid1
        OldGrid(0, 1) = My.Settings.Grid2
        OldGrid(0, 2) = My.Settings.Grid3
        OldGrid(0, 3) = My.Settings.Grid4
        OldGrid(1, 0) = My.Settings.Grid5
        OldGrid(1, 1) = My.Settings.Grid6
        OldGrid(1, 2) = My.Settings.Grid7
        OldGrid(1, 3) = My.Settings.Grid8
        OldGrid(2, 0) = My.Settings.Grid9
        OldGrid(2, 1) = My.Settings.Grid10
        OldGrid(2, 2) = My.Settings.Grid11
        OldGrid(2, 3) = My.Settings.Grid12
        OldGrid(3, 0) = My.Settings.Grid13
        OldGrid(3, 1) = My.Settings.Grid14
        OldGrid(3, 2) = My.Settings.Grid15
        OldGrid(3, 3) = My.Settings.Grid16
    End Sub

    Private Sub RefreshGrid()
        Dim i As Integer
        Dim j As Integer
        For i = 0 To 3
            For j = 0 To 3
                Select Case OldGrid(i, j)
                    Case 0
                        Grid(i, j).Visible = False
                    Case 2
                        Grid(i, j).Image = My.Resources.Grid1
                        Grid(i, j).Visible = True
                    Case 4
                        Grid(i, j).Image = My.Resources.Grid2
                        Grid(i, j).Visible = True
                    Case 8
                        Grid(i, j).Image = My.Resources.Grid3
                        Grid(i, j).Visible = True
                    Case 16
                        Grid(i, j).Image = My.Resources.Grid4
                        Grid(i, j).Visible = True
                    Case 32
                        Grid(i, j).Image = My.Resources.Grid5
                        Grid(i, j).Visible = True
                    Case 64
                        Grid(i, j).Image = My.Resources.Grid6
                        Grid(i, j).Visible = True
                    Case 128
                        Grid(i, j).Image = My.Resources.Grid7
                        Grid(i, j).Visible = True
                    Case 256
                        Grid(i, j).Image = My.Resources.Grid8
                        Grid(i, j).Visible = True
                    Case 512
                        Grid(i, j).Image = My.Resources.Grid9
                        Grid(i, j).Visible = True
                    Case 1024
                        Grid(i, j).Image = My.Resources.Grid10
                        Grid(i, j).Visible = True
                    Case 2048
                        Grid(i, j).Image = My.Resources.Grid11
                        Grid(i, j).Visible = True
                    Case 4096
                        Grid(i, j).Image = My.Resources.Grid12
                        Grid(i, j).Visible = True
                    Case 8192
                        Grid(i, j).Image = My.Resources.Grid13
                        Grid(i, j).Visible = True
                    Case 16384
                        Grid(i, j).Image = My.Resources.Grid14
                        Grid(i, j).Visible = True
                    Case 32768
                        Grid(i, j).Image = My.Resources.Grid15
                        Grid(i, j).Visible = True
                    Case 65536
                        Grid(i, j).Image = My.Resources.Grid16
                        Grid(i, j).Visible = True
                    Case 131072
                        Grid(i, j).Image = My.Resources.Grid17
                        Grid(i, j).Visible = True
                    Case Else
                        ResetGame()
                End Select
            Next
        Next
    End Sub

    Private Sub ResetGame()
        PlusScoreTimer.Enabled = False
        RollScoreTimer.Enabled = False
        OldGrid = {{0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}}
        NewGrid = {{0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}, {0, 0, 0, 0}}
        OldScore = 0
        NewScore = 0
        Me.Text = "FLAT 2048 - 当前:0"
        isAgain = False
        isDisplay = True
        isExit = True
        isExitConfirm = False
        isOver = False
        RandomGrid()
        RandomGrid()
        RefreshGrid()
        RollScoreTimer.Enabled = True
    End Sub

    Private Sub TestOver()
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim isAlive As Boolean
        isAlive = True
        UpdateNewGrid()
        For i = 0 To 3
            For j = 0 To 2
                k = j
                Do
                    k = k + 1
                Loop Until NewGrid(i, k) <> 0 Or k = 3
                If NewGrid(i, j) = NewGrid(i, k) Then
                    isAlive = False
                End If
            Next
            For j = 0 To 2
                If NewGrid(i, j) = 0 Then
                    k = j
                    Do
                        k = k + 1
                    Loop Until NewGrid(i, k) <> 0 Or k = 3
                    If NewGrid(i, k) <> 0 Then
                        isAlive = False
                    End If
                End If
            Next
        Next
        For i = 0 To 3
            For j = 3 To 1 Step -1
                k = j
                Do
                    k = k - 1
                Loop Until NewGrid(i, k) <> 0 Or k = 0
                If NewGrid(i, j) = NewGrid(i, k) Then
                    isAlive = False
                End If
            Next
            For j = 3 To 1 Step -1
                If NewGrid(i, j) = 0 Then
                    k = j
                    Do
                        k = k - 1
                    Loop Until NewGrid(i, k) <> 0 Or k = 0
                    If NewGrid(i, k) <> 0 Then
                        isAlive = False
                    End If
                End If
            Next
        Next
        For i = 0 To 3
            For j = 0 To 2
                k = j
                Do
                    k = k + 1
                Loop Until NewGrid(k, i) <> 0 Or k = 3
                If NewGrid(j, i) = NewGrid(k, i) Then
                    isAlive = False
                End If
            Next
            For j = 0 To 2
                If NewGrid(j, i) = 0 Then
                    k = j
                    Do
                        k = k + 1
                    Loop Until NewGrid(k, i) <> 0 Or k = 3
                    If NewGrid(k, i) <> 0 Then
                        isAlive = False
                    End If
                End If
            Next
        Next
        For i = 0 To 3
            For j = 3 To 1 Step -1
                k = j
                Do
                    k = k - 1
                Loop Until NewGrid(k, i) <> 0 Or k = 0
                If NewGrid(j, i) = NewGrid(k, i) Then
                    isAlive = False
                End If
            Next
            For j = 3 To 1 Step -1
                If NewGrid(j, i) = 0 Then
                    k = j
                    Do
                        k = k - 1
                    Loop Until NewGrid(k, i) <> 0 Or k = 0
                    If NewGrid(k, i) <> 0 Then
                        isAlive = False
                    End If
                End If
            Next
        Next
        UpdateNewGrid()
        If isAlive Then
            isExit = False
            isOver = True
            Panel.Image = My.Resources.Panel2
            Panel.Visible = True
            MovePanelTimer1.Enabled = True
        End If
    End Sub

    Private Sub UpdateNewGrid()
        Dim i As Integer
        Dim j As Integer
        For i = 0 To 3
            For j = 0 To 3
                NewGrid(i, j) = OldGrid(i, j)
            Next
        Next
    End Sub

    Private Sub UpdateOldGrid()
        Dim i As Integer
        Dim j As Integer
        For i = 0 To 3
            For j = 0 To 3
                OldGrid(i, j) = NewGrid(i, j)
            Next
        Next
    End Sub

    Private Sub WriteData()
        My.Settings.BestScore = BestScore
        My.Settings.CurrentScore = NewScore
        My.Settings.Grid1 = OldGrid(0, 0)
        My.Settings.Grid2 = OldGrid(0, 1)
        My.Settings.Grid3 = OldGrid(0, 2)
        My.Settings.Grid4 = OldGrid(0, 3)
        My.Settings.Grid5 = OldGrid(1, 0)
        My.Settings.Grid6 = OldGrid(1, 1)
        My.Settings.Grid7 = OldGrid(1, 2)
        My.Settings.Grid8 = OldGrid(1, 3)
        My.Settings.Grid9 = OldGrid(2, 0)
        My.Settings.Grid10 = OldGrid(2, 1)
        My.Settings.Grid11 = OldGrid(2, 2)
        My.Settings.Grid12 = OldGrid(2, 3)
        My.Settings.Grid13 = OldGrid(3, 0)
        My.Settings.Grid14 = OldGrid(3, 1)
        My.Settings.Grid15 = OldGrid(3, 2)
        My.Settings.Grid16 = OldGrid(3, 3)
    End Sub

End Class
