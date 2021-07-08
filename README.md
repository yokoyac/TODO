# TodoWebForms2_Blank
## 1.起動方法
##### 1.Zip形式でダウンロード
##### 2.適当なフォルダに解凍し、VisualStudio 2017以上でソリューションファイルを開く
##### 3.ソリューションエクスプローラーから、ルートの[ソリューション]を選択し、[NuGetパッケージの復元]を実施
##### 4.[パッケージマネージャーコンソール]以下のコマンドを実行
```
Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -reinstall
```
##### 5.プロジェクトのプロパティを開き、[Web]の開始動作を[ページを指定する]を選択し、TodoForm.aspxファイルを指定する。
##### 6.[F5]ボタン押下
