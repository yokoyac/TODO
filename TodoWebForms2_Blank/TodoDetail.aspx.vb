''' <summary>
''' TODO編集画面
''' </summary>
Public Class TodoDetail
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 初期画面表示
    ''' ・追加ボタンで画面呼び出された場合、追加モードで表示する
    ''' 　・入力欄は空欄
    ''' 　・アクションボタン名は「追加」
    ''' ・更新ボタンで画面呼び出しされた場合、更新
    ''' 　・入力欄は、選択されたTODOの内容を表示
    ''' 　・アクションボタン名は「更新」
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            '初期表示の場合
            ' セッションに値が存在する場合
            If Session("MODE") IsNot Nothing Then
                ' 追加モード
                If (Session("MODE").Equals("NEW")) Then
                    btnAction.Text = "追加"
                    Return
                End If

                ' 更新モード
                If (Session("MODE").Equals("UPDATE")) Then
                    btnAction.Text = "更新"

                    ' 初期値設定(タイトル、期限、コメント)
                    txtTitle.Text = Session("UPDATEDATA").Title
                    txtDueDate.Text = Session("UPDATEDATA").DueDate
                    txtComment.Text = Session("UPDATEDATA").Comment
                    Return
                End If
            End If
        Else
                'PostBackの場合（＝自画面再表示の場合）
            End If
    End Sub

    ''' <summary>
    ''' TODO一覧画面に戻る
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnBack_Click(sender As Object, e As EventArgs)
        Response.Redirect("TodoForm.aspx")
    End Sub

    ''' <summary>
    ''' 追加、または更新処理を実施
    ''' ・追加ボタンで画面呼び出された場合、追加
    ''' ・更新ボタンで画面呼び出しされた場合、更新
    ''' ・追加、または更新処理後は、TODO一覧画面に戻る。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnAction_Click(sender As Object, e As EventArgs)
        ' 入力エラーがない場合
        If Not InputChcek() Then Return

        ' 入力値(Title, DueData, Comment)
        Dim todoTitle As String = txtTitle.Text
        Dim todoDate As String = txtDueDate.Text
        Dim todoComment As String = txtComment.Text

        '追加処理
        If (Session("MODE").Equals("NEW")) Then

            ' データの生成
            Dim addTodo As New TodoItem
            addTodo.Title = todoTitle
            addTodo.DueDate = todoDate
            addTodo.Comment = todoComment

            DBManager.Insert(addTodo)
        End If

        ' 更新処理
        If (Session("MODE").Equals("UPDATE")) Then

            Dim changeTodo As TodoItem = Session("UPDATEDATA")
            changeTodo.Title = todoTitle
            changeTodo.DueDate = todoDate
            changeTodo.Comment = todoComment

            DBManager.Update(changeTodo)

        End If

        ' 一覧画面に戻る
        Response.Redirect("TodoForm.aspx")
    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' ・TODOタイトルが未入力の場合、エラー
    ''' ・期限がYYYY/M/D形式ではない場合、または、無効な日付の場合、エラー。
    ''' </summary>
    ''' <returns></returns>
    Private Function InputChcek() As Boolean

        ' タイトルが空文字の場合、エラー
        If txtTitle.Text Is "" Then
            ShowAlert("必須入力です。")
            Return False
        End If

        ' 期限が空文字の場合でも可
        If txtDueDate.Text Is "" Then
            Return True
        End If

        ' yyyy/MM/ddもしくはyyyy/M/d形式か判定
        If (System.Text.RegularExpressions.Regex.IsMatch(txtDueDate.Text, "^[0-9]{4}/[0-9]{1,2}/[0-9]{1,2}$")) Then
            ' 日付型に変更できるか(無効な日付でないか)
            Dim dateTime As DateTime
            If DateTime.TryParse(txtDueDate.Text, dateTime) Then
                Return True
            Else
                ShowAlert("無効な期限です。")
                Return False
            End If
        Else
            ShowAlert("形式が異なります。")
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' 簡易エラーメッセージ表示（JavaScriptのalert）
    ''' </summary>
    ''' <param name="msg"></param>
    Private Sub ShowAlert(msg As String)
        Dim script As String = $"alert('{msg}');"
        ClientScript.RegisterStartupScript(Me.GetType(), "ErrorMessage", script, True)
    End Sub
End Class