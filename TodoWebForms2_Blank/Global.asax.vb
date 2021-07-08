
Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' アプリケーションの起動時に呼び出されます
        DBManager.InitDB()

        ' サンプルデータ
        DBManager.Insert(New TodoItem With {.ID = 1, .Title = "課題作成"})
        DBManager.Insert(New TodoItem With {.ID = 2, .Title = "部屋の掃除"})
        DBManager.Insert(New TodoItem With {.ID = 3, .Title = "旅行準備"})
        DBManager.Insert(New TodoItem With {.ID = 4, .Title = "試験の申込", .IsCompleted = True})
        DBManager.Insert(New TodoItem With {.ID = 5, .Title = "家電の調査", .IsCompleted = True})
    End Sub
End Class