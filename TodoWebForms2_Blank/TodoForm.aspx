<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TodoForm.aspx.vb" Inherits="TodoWebForms2_Blank.TodoForm" %>

<!DOCTYPE html>
<html>
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>ToDoList</title>
  <link rel="stylesheet" href="style.css">
  <script type="text/javascript">
  </script>
</head>
<body>
  <form id="form1" runat="server">
    <div class="new-button-area">
      <!-- [追加]ボタンを押下すると、TODO編集画面に遷移し、追加モードで表示する。 -->
      <asp:Button runat="server" ID="btnAddTodo" Text="TODOを追加する"  CssClass="button"/>
    </div>
    <div class="incomplete-area">
      <p class="title">未完了のTODO</p>
      <ul id="incomplete-list">
        <asp:Repeater runat="server" ID="rptInCompleteList">
          <ItemTemplate>
            <li>
              <div class="list-row">
                <p><%# DataBinder.Eval(Container.DataItem, "Title")  %></p>
                <!-- [完了]ボタンを押下すると、対象のTODOの状態を完了に更新し、一覧を最新表示する。 -->
                <asp:Button runat="server" ID="btnComplete" Text="完了" CssClass="button" OnClick="btnComplete_Click" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "ID")  %>'/>
                <!-- [修正]ボタンを押下すると、TODO編集画面に遷移し、更新モードで表示する。 -->
                <asp:Button runat="server" ID="btnUpdate" Text="修正" CssClass="button" OnClick="btnUpdate_Click" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "ID")  %>'/>
                <!-- [削除]ボタンを押下すると、対象のTODOを削除、一覧を最新表示する。 -->
                <asp:Button runat="server" ID="btnDelete" Text="削除" CssClass="button" OnClick="btnDelete_Click" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "ID")  %>'/>
                </div>
            </li>
          </ItemTemplate>
        </asp:Repeater>
      </ul>
    </div>
    <div class="complete-area">
      <p class="title">完了したTODO</p>
      <ul id="complete-list">
        <!-- 
          ・完了済TODOを一覧表示させる。
          ・[戻す]ボタンを押下すると、対象のTODOの状態を未完了に更新し、一覧を最新表示する。
          -->
          <asp:Repeater runat="server" ID="rptCompleteList">
          <ItemTemplate>
            <li>
              <div class="list-row">
                <p><%# DataBinder.Eval(Container.DataItem, "Title")  %></p>
                <asp:Button runat="server" ID="btnReturn" Text="戻す" CssClass="button" OnClick="btnReturn_Click" CommandArgument ='<%# DataBinder.Eval(Container.DataItem, "ID")  %>'/>
            </li>
          </ItemTemplate>
        </asp:Repeater>
      </ul>
    </div>
  </form>
</body>
</html>