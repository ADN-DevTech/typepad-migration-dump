---
layout: "post"
title: "AutoCAD  API が持つコマンド実行コンテキストの理解"
date: "2013-11-20 00:07:41"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/11/understandin-command-context-on-autocad.html "
typepad_basename: "understandin-command-context-on-autocad"
typepad_status: "Publish"
---

<p>今回は少し踏み込んで、AutoCAD のコマンド実行で知っておきたい <strong>コマンド実行コンテキスト</strong>&#0160;をご紹介します。もちろん、AutoCAD API でカスタマイズしている方に重要な情報です。</p>
<p><strong>実行コンテキスト</strong>は、簡単に言うと「コマンドを動作させる内部的な環境」のことです。多くの場合、コマンドを実行するために、AutoCAD の画面上に図面が表示されていることが前提になります。言い換えれば、コマンドの実行は、ドキュメント(図面を表示する MDI 子ウィンドウ) に依存します。</p>
<p>通常、AutoCAD のコマンドを実行は、コマンドは現在アクティブなドキュメントに表示されている図面に作用します。このようなコマンドは、<strong>ドキュメント実行コンテキスト</strong>&#0160;で動作するコマンドと考えてください。ドキュメント実行コンテキストで動作するコマンドは、コマンドの実行前と実行後で、対象とするドキュメントが同一である点に注意してください。AutoCAD の標準コマンドや、API を使って作成するカスタム コマンドのほとんどは、このドキュメント実行コンテキストで動作します。</p>
<p>一方、コマンド実行前と後のドキュメントが異なるようなコマンドも存在します。新規図面を作成する NEW [新規作成] コマンドや OPEN [開く] コマンドが、その代表例です。このようなコマンドを&#0160;<strong>アプリケーション実行コンテキスト</strong>&#0160;で動作するコマンドといいます。</p>
<p>カスタムコマンドを作成する場合には、コマンド フラグと呼ばれる値をコマンド定義に加えることで、定義するコマンドをドキュメント実行コンテキストで実行させるか、アプリケーション実行コンテキストで実行させるかを指定できます。具体的には、AutoCAD .NET API で同様の定義をする場合にはコマンド属性に&#0160;<strong>CommandFlags.Session</strong>&#0160;を、ObjectARX の場合には AcEdCommandStack::addCommand() の第4引数に <strong>ACRX_CMD_SESSION</strong>&#0160;を、それぞれ指定するしなければなりません。これらを特に指定しなければ、コマンドはドキュメント実行コンテキストで実行されます。</p>
<p>このような状況でカスタム コマンドの問題が発生しがちです。ドキュメント実行コンテキストで定義したカスタム コマンド内で OPEN コマンドを呼び出すことを考えてみましょう。そのカスタム コマンドを実行すると、コマンドの実行途中で実行コンテキストが開いたドキュメントに移動することになります。つまり、対象となるドキュメントが変わってしまします。AutoCAD は、ドキュメント実行コンテスト コマンドが、途中で対象ドキュメントを変更する処理を許容しないため、ドキュメント実行コンテキストで定義したカスタム コマンドの中で OPEN コマンドを呼び出すと、その処理は中断してしまいます。このような場面では、このカスタム コマンドはアプリケーション実行コンテキストで定義されるべきです。図面を1つ1つ AutoCAD に開きながら自動製図や印刷を繰り返すコマンドを作成する場合には、明示的にアプリケーション実行コンテキストで実行されるような指定をしないと、処理途中でコマンドの実行が中断していまいますので注意してください。</p>
<table border="0" cellspacing="5" id="table11">
<tbody>
<tr>
<td align="left" valign="top"><strong>図面オープンの失敗例: </strong>モーダル コマンドでの呼び出し</td>
</tr>
<tr>
<td align="left" bgcolor="#cccccc" valign="top">&lt;CommandMethod(&quot;MyCommand&quot;, <strong>CommandFlags.Modal</strong>)&gt; _<br />Public Sub MyCommand7()<br />&#0160;&#0160;&#0160; Dim oDocs As DocumentCollection = Application.DocumentManager<br />&#0160;&#0160;&#0160; Dim oDoc As Document = DocumentCollectionExtension.Open(oDocs, &quot;C:\Test.dwg&quot;, False)<br />End Sub</td>
</tr>
<tr>
<td align="left" valign="top">&#0160;</td>
</tr>
<tr>
<td align="left" valign="top"><strong>図面オープンの成功例: </strong>セッション コマンドでの呼び出し</td>
</tr>
<tr>
<td align="left" bgcolor="#cccccc" valign="top">&lt;CommandMethod(&quot;MyCommand&quot;, <strong>CommandFlags.Session</strong>)&gt; _<br />Public Sub MyCommand7()<br />&#0160;&#0160;&#0160; Dim oDocs As DocumentCollection = Application.DocumentManager<br />&#0160;&#0160;&#0160; Dim oDoc As Document = DocumentCollectionExtension.Open(oDocs, &quot;C:\Test.dwg&quot;, False)<br />End Sub</td>
</tr>
<tr>
<td align="left" valign="top">&#0160;</td>
</tr>
<tr>
<td align="left" valign="top"><strong>図面オープンの成功例:&#0160;</strong></td>
</tr>
<tr>
<td align="left" bgcolor="#cccccc" valign="top">Public Shared Sub DocOpenHelper(data As Object)<br />&#0160;&#0160;&#0160; Dim oDocs As DocumentCollection = Application.DocumentManager<br />&#0160;&#0160;&#0160; If (oDocs.IsApplicationContext) Then<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; DocumentCollectionExtension.Open(oDocs, CType(data, String), False)<br />&#0160;&#0160;&#0160; End If<br />End Sub<br /><br />&lt;CommandMethod(&quot;MyCommand6&quot;, <strong>CommandFlags.Modal</strong>)&gt; _<br />Public Sub MyCommand6()<br />&#0160;&#0160;&#0160; Dim oDocs As DocumentCollection = Application.DocumentManager<br />&#0160;&#0160;&#0160; Dim oCallback As ExecuteInApplicationContextCallback = _<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; New ExecuteInApplicationContextCallback(AddressOf DocOpenHelper)<br />&#0160;&#0160;&#0160; Application.DocumentManager.ExecuteInApplicationContext(oCallback, &quot;C:\Test.dwg&quot;)<br />End Sub</td>
</tr>
</tbody>
</table>
<p>もし、アプリケーションが アプリケーション実行コンテキスト で実行していて、かつ、図面データベースに編集を伴う書き込みを実行する場合には、明示的にドキュメントをロックしないと、eLockViolation エラーが発生します。一般的には、ドキュメント ロックはパレット ダイアログを含むモードレス ダイアログやツールバーから、図面データベースに書き込みを行う場合に指定しますが、&#0160;アプリケーション実行コンテキスト コマンド内の実装でも、対象ドキュメントが不定となるため、ドキュメント ロックが必要です。</p>
<p>具体的なドキュメント ロックの方法は、次の Autodesk Knowledge Network も参考にしてください。</p>
<p style="padding-left: 30px;"><strong><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/4SC0BvLCPfUisNSuJvpCPZ.html" rel="noopener" target="_blank">AutoCAD .NET API：パレット ダイアログを作成する方法</a></strong></p>
<p>最後に、もう1つ注意点をご紹介しておきます。前述の例では、AutoCAD .NET API のコードを使用しました。AutoCAD .NET API は Visual Basic 言語を利用できるので、VBA の移植先として推奨された環境でもあります。もし、VBA のコードを AutoCAD .NET API 環境へ移植しているなら、コマンド実行コンテキストやドキュメント ロックについて、注意が必要な場合があります。</p>
<p>VBA では、Component Object Model、略して COM テクノロジを使ったカスタマイズ環境です。AutoCAD .NET API は .NET Framework テクノロジを使ったカスタマイズ環境ですが、COM と親和性が高く、COM のコードを、ほぼそのまま利用できます。問題は、AutoCAD の COM API のメソッドやプロパティが、コマンド実行コンテキストやドキュメント ロックを意識しなくてもいいように、内部的に処理を隠ぺいしているという点です。わかりやすく言えば、AutoCAD COM API で図面データベースに変更を加えるようなメソッドやプロパティを呼び出した場合、各メソッド、プロパティ内部でドキュメントロック/ロック解除の処理が実行されています。当然、冗長であるため、場合によっては実行速度に影響が出る場合があります。可能であれば、AutoCAD .NET API での書き換えを検討いただいたほうがいい、という例です。</p>
<p>By Toshiaki Isezaki</p>
