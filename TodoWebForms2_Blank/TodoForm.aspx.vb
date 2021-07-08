Imports System.Diagnostics

''' <summary>
''' TODO一覧画面
''' </summary>
Public Class TodoForm
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' 初期表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '初期データのサンプルは、Global.aspx.vbにて登録しています。
        '不要であれば、コメントアウトすること。
        If Not IsPostBack Then
            'リピーターの初期化は、初期表示の1回だけにしないとNG
            UpdateList()

            DbSample.DeleteAndInsertData()
            DbSample.SelectDataUsingDisconnectedType()
            'DbSample.GetCompleteTodoList2()
            'DbSample.GetInCompleteTodoList2()
            'DbSample.GetTodoById2(1)

            'DbSample.SelectDataUsingConnectedType()

        End If

    End Sub

    ''' <summary>
    ''' TODO編集画面に遷移する。
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnAddTodo_Click(sender As Object, e As EventArgs) Handles btnAddTodo.Click
        Session("MODE") = "NEW"
        Server.Transfer("TodoDetail.aspx")
    End Sub

    ''' <summary>
    ''' ・選択されたTODOを完了状態に更新する
    ''' ・リピーターの状態を更新する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnComplete_Click(sender As Object, e As EventArgs)
        ' 選択したボタンのIDを取得
        Dim btn As Button = DirectCast(sender, Button)
        Dim selectedTodoId As Integer = Integer.Parse(btn.CommandArgument)

        ' 選択したTODOを完了状態(True)に変更
        Dim inCompTodo As TodoItem = DBManager.GetTodoById(selectedTodoId)
        inCompTodo.IsCompleted = True
        DBManager.Update(inCompTodo)

        ' リピーターのデータ更新
        UpdateList()

    End Sub

    ''' <summary>
    ''' リピーターデータの最新化
    ''' </summary>
    Private Sub UpdateList()
        ' 未完了リスト
        rptInCompleteList.DataSource = DBManager.GetInCompleteTodoList
        rptInCompleteList.DataBind()
        ' 完了リスト
        rptCompleteList.DataSource = DBManager.GetCompleteTodoList
        rptCompleteList.DataBind()
    End Sub

    ''' <summary>
    ''' ・選択されたTODOを修正
    ''' ・TODO編集画面に遷移
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim selectedTodoId As Integer = Integer.Parse(btn.CommandArgument)

        ' 選択されたTODOのデータ
        Dim updateTodo As TodoItem = DBManager.GetTodoById(selectedTodoId)

        ' 画面遷移
        Session("MODE") = "UPDATE"
        Session("UPDATEDATA") = updateTodo
        Server.Transfer("TodoDetail.aspx")

    End Sub

    ''' <summary>
    ''' ・選択されたTODOを削除
    ''' ・リピーターの状態を更新する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnDelete_Click(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim selectedTodoId As Integer = Integer.Parse(btn.CommandArgument)

        ' 選択したTODOのデータを削除
        Dim deleteTodo As TodoItem = DBManager.GetTodoById(selectedTodoId)
        DBManager.Delete(deleteTodo)

        UpdateList()

    End Sub

    ''' <summary>
    ''' ・選択されたTODOを未完了状態に戻す
    ''' ・リピーターの状態を更新する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub btnReturn_Click(sender As Object, e As EventArgs)
        Dim btn As Button = DirectCast(sender, Button)
        Dim selectedTodoId As Integer = Integer.Parse(btn.CommandArgument)

        ' 選択したTODOを未完了状態(False)に変更
        Dim compTodo As TodoItem = DBManager.GetTodoById(selectedTodoId)
        compTodo.IsCompleted = False
        DBManager.Update(compTodo)

        UpdateList()

    End Sub
End Class