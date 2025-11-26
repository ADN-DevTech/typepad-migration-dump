---
layout: "post"
title: "APIカスタマイズでInventorのコマンドを利用したい"
date: "2025-08-04 01:05:55"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/08/how-to-invoke-inventor-command-via-api.html "
typepad_basename: "how-to-invoke-inventor-command-via-api"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f068cf200b-pi" style="display: inline;"><img alt="Title" class="asset  asset-image at-xid-6a0167607c2431970b02e860f068cf200b img-responsive" src="/assets/image_637114.jpg" title="Title" /></a></p>
<p>InventorのAPIを用いたカスタマイズ処理からInventorのコマンドを実行することは出来ないか？というお問い合わせが時折あります。APIが公開されていないが、コマンドでは提供されている機能を実装したいなどの場合に回避としてコマンドを実行したい場合や、処理を自身で開発する必要がない等、様々な動機があるかと思われます。</p>
<p>そこで今回は、InventorのコマンドをAPIから実行する方法について解説をしたいと思います。</p>
<p>&#0160;</p>
<h3>APIからコマンドを実行する方法</h3>
<p>結論から申し上げますと、InventorのコマンドをAPIから実行することは可能です。例として以下は「スケッチライン」コマンドを実行するVBAのサンプルコードとなります。</p>
<p>InventorのAPIでは、Inventorのコマンド（デフォルトのコマンドやアドインで作成したコマンド）は全て、ControlDefinition オブジェクトとして表されております。コマンドを実行したい場合、コマンドに対応するControlDefinition オブジェクトを取得し、取得したControlDefinition オブジェクトのExecute()メソッドを実行するだけです。</p>
<div style="font-size: 8pt; background: #eeeeee; color: black; line-height: 140%; font-family: courier new;">Public Sub RunLineCommand()<br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Get the CommandManager object.<br /></strong></span> &#0160;&#0160;&#0160; Dim oCommandMgr As CommandManager<br />&#0160;&#0160;&#0160; Set oCommandMgr = ThisApplication.CommandManager<br /><br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160;&#39; Get control definition for the line command.<br /></strong></span>&#0160;&#0160;&#0160; Dim oControlDef As ControlDefinition <br />&#0160;&#0160;&#0160; Set oControlDef = oCommandMgr.ControlDefinitions.Item( _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;SketchLineCmd&quot;)&#0160; <br />&#0160;&#0160;&#0160; <span style="color: #0000ff;"><strong>&#39; Execute the command. <br /></strong></span> &#0160;&#0160;&#0160; Call oControlDef.Execute <br />End Sub</div>
<p>&#0160;</p>
<h3>コマンド名の取得方法</h3>
<p>サンプルコードを見ていただくと、CommandManagerオブジェクトからControlDefinitionオブジェクトを取得する際にコマンド名を指定しています。すべてのControlDefinitionは、自身を識別するための一意の名前（=コマンド名）を持っています。では、APIから実行したいコマンドのコマンド名は、どのようにして特定すればよいでしょうか？</p>
<p>コマンド名の特定方法は大きく2つあります。一つ目は以下の様なVBAコードを実行する方法です。以下のVBAコードを実行するとC:\tempフォルダ配下に、CommandNames.txtというファイルを出力します。ファイル内にはコードの実行時にInventor内に登録されているすべてのコマンド名とその説明が出力されます。</p>
<div style="font-size: 8pt; background: #eeeeee; color: black; line-height: 140%; font-family: courier new;">Sub PrintCommandNames() <br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Get the CommandManager object. <br /></strong></span>&#0160;&#0160;&#0160; Dim oCommandMgr As CommandManager <br />&#0160;&#0160;&#0160; Set oCommandMgr = ThisApplication.CommandManager <br />&#0160;&#0160;&#0160; <br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Get the collection of control definitions. <br /></strong></span>&#0160;&#0160;&#0160; Dim oControlDefs As ControlDefinitions <br />&#0160;&#0160;&#0160; Set oControlDefs = oCommandMgr.ControlDefinitions <br /><br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Open the file and print out a header line. <br /></strong></span>&#0160;&#0160;&#0160; Dim oControlDef As ControlDefinition <br />&#0160;&#0160;&#0160; Open &quot;C:\temp\CommandNames.txt&quot; For Output As #1 <br />&#0160;&#0160;&#0160; Print #1, Tab(10); &quot;Command Name&quot;; Tab(75); _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;Description&quot;; vbNewLine <br /><br /><span style="color: #0000ff;">&#0160;&#0160;&#0160; &#39; Iterate through the controls and write out the name. <br /></span>&#0160;&#0160;&#0160; For Each oControlDef In oControlDefs <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Print #1, oControlDef.InternalName; Tab(55); _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oControlDef.DescriptionText <br />&#0160;&#0160;&#0160; Next <br /><br /><span style="color: #0000ff;">&#0160;&#0160;&#0160; &#39; Close the file. <br /></span>&#0160;&#0160;&#0160; Close #1 <br />End Sub</div>
<p>&#0160;</p>
<p>もう一つの方法は、Inventor SDKのDeveloperToolに付属のEventWatcherを利用する方法です。EventWatcherは、Inventor のSDKフォルダ配下（通常C:\Users\Public\Documents\Autodesk\Inventor 20XX\SDK）にあるdevelopertools.msiをインストールすると、C:\Users\Public\Documents\Autodesk\Inventor 20XX\SDK\DeveloperTools\Tools フォルダ配下に展開されます。このEventWatcherは起動中のInventorに接続し、Inventorエディタのイベントを監視し、どんなイベントが発生しているかを見ることが出来るツールとなります。</p>
<p>EventWatcherを用いると、以下の様な手順でコマンド名を確認することが出来ます。</p>
<p>&#0160;</p>
<p>1．Inventorを起動し、対象のコマンドが実行できる状態とする</p>
<p>2．C:\Users\Public\Documents\Autodesk\Inventor 2009\SDK\DeveloperTools\Tools\EventWatcher\bin\Release配下にあるEventWatcher.exeを起動する。</p>
<p>3．起動したEventWatcherで、UserInputEvents.OnActivateCommandにチェックボックスを入れてイベントの監視を開始します。このイベントはコマンドの開始時に発行されるイベントとなります。</p>
<p>4．Inventor上で、対象のコマンドを実行します。コマンドを実行すると、EventWatcherの画面上にコマンド名が出力されるはずです。</p>
<p>&#0160;</p>
<p>もし、コマンド名が表示されない場合はEventWatcherがInventorに正しく接続できていない可能性があります。UserInputEvents.OnActivateCommand以外のイベントを監視対象（チェックボックスを入れる）として、Inventorを操作し他のイベントが取得できているかを確認してみてください。もし他のイベントも取得できていない場合はInventorおよびEventWatcherを再起動してもう一度実行してみてください。</p>
<p>もし、他のイベントは取得できていて、それでも対象のコマンドのUserInputEvents.OnActivateCommandだけが取得できていないような状況の場合、対象の処理がコマンドではなく、後述するコマンドの機能である可能性が高い状況です。残念ながらこの場合には、APIから実行することが出来ないものと思われます。</p>
<p>&#0160;</p>
<h3>コマンド実行後のパラメータのユーザ入力について</h3>
<p>この方法を用いることで、APIからコマンドを実行し処理を自動化することが出来るのでは？と感じられた方もいらっしゃるかと思いますが、実はこの方法には制限があります。例として「押し出し」コマンドの様なコマンドを、APIから実行した場合どのようになるかを考えてみたいと思います。</p>
<p>ご承知の様に、InventorのGUIで押し出しコマンドを実行するとダイアログが表示され、このダイアログでは押し出し対象のジオメトリや押し出し方向、押し出す距離 等のパラメータを指定することで押し出しフィーチャーを作成することが出来ます。では、これらのダイアログの入力をAPIから指定することが出来るのでしょうか？残念ながら答えは、否でありこれらのパラメータをAPIから指定することは出来ません。</p>
<p>また別の場合には、「押し出し」フィーチャーの編集として押し出しの方向を変える処理（押し出しのダイアログ上では押し出し方向を指定するアイコンをクリックする）をAPIから実行したい場合があるかもしれません。ではこの押し出しの方向の指定は、前述のコマンド名の取得で対応するコマンドを取得できるでしょうか？残念ながらこちらも答えは、否で、押し出し方向をAPIから指定することは出来ません。</p>
<p>実は、InventorのAPIの観点から見ると「押し出し」コマンド実行時に表示されるダイアログ内の入力パラメータの指定は「押し出し」コマンド自身の<strong>機能</strong>として実装されている物であり、コマンドの扱いではありません。Inventor APIのControlDefinitionオブジェクトは、ControlDefinitionオブジェクトに対応する<strong>コマンドを実行する</strong>ことは出来ますが、<strong>各コマンド内の機能を実行する</strong>ことは出来ません。</p>
<p>&#0160;</p>
<h3>コマンドへの入力指定について</h3>
<p>先ほど、コマンドの機能として指定しているパラメータ等についてはAPIから指定することが出来ない と申し上げましたが、例外的に指定できる場合あります。</p>
<p>そのうちの一つ名が、処理対象の入力エンティティの指定です。もし対象のコマンド側が対応している場合、APIでのコマンド実行前に対象のエンティティを選択しておいてからコマンドを実行することで、選択中のエンティティを対象にコマンドを実行することが出来る場合があります。</p>
<p>以下は、寸法を整列するコマンドを実行するVBAのサンプルコードなります。コマンド実行前に対象のエンティティをSelectSetに追加して選択状態とし、選択されたエンティティを対象にコマンドを実行しております。</p>
<div style="font-size: 8pt; background: #eeeeee; color: black; line-height: 140%; font-family: courier new;">Public Sub ArrangeDimensions() <br /><span style="color: #0000ff;">&#0160;&#0160;&#0160; <strong>&#39; Get the active document, assuming it is a drawing. <br /></strong></span>&#0160;&#0160;&#0160; Dim oDrawDoc As DrawingDocument <br />&#0160;&#0160;&#0160; Set oDrawDoc = ThisApplication.ActiveDocument <br /><br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Get the collection of dimensions on the active sheet. <br /></strong></span>&#0160;&#0160;&#0160; Dim oDimensions As DrawingDimensions <br />&#0160;&#0160;&#0160; Set oDimensions = oDrawDoc.ActiveSheet.DrawingDimensions <br /><br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Get a reference to the select set and clear it. <br /></strong></span>&#0160;&#0160;&#0160; Dim oSelectSet As SelectSet <br />&#0160;&#0160;&#0160; Set oSelectSet = oDrawDoc.SelectSet <br />&#0160;&#0160;&#0160; oSelectSet.Clear <br /><br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Add each dimension to the select set to select them. <br /></strong></span>&#0160;&#0160;&#0160; Dim oDrawDim As DrawingDimension <br />&#0160;&#0160;&#0160; For Each oDrawDim In oDimensions <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; oSelectSet.Select oDrawDim <br />&#0160;&#0160;&#0160; Next <br /><br /><span style="color: #0000ff;"><strong>&#0160;&#0160; &#39; Get the CommandManager object. <br /></strong></span>&#0160;&#0160;&#0160; Dim oCommandMgr As CommandManager <br />&#0160;&#0160;&#0160; Set oCommandMgr = ThisApplication.CommandManager
<p>&#0160;<span style="color: #0000ff;"><strong>&#0160;&#0160; &#39; Get control definition for the arrange dimensions command. <br /></strong></span>&#0160;&#0160;&#0160; Dim oControlDef As ControlDefinition <br />&#0160;&#0160;&#0160; Set oControlDef = oCommandMgr.ControlDefinitions.Item( _&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;DrawingArrangeDimensionsCmd&quot;)</p>
<p>&#0160;<span style="color: #0000ff;"><strong>&#0160;&#0160; &#39; Execute the command. <br /></strong></span>&#0160;&#0160;&#0160; Call oControlDef.Execute <br />End Sub</p>
</div>
<p>ただし、コマンドによってはこの方法でも上手くいかない場合があることにご留意ください。例えばフィレットコマンドなどにおいては事前選択されたエンティティがあっても、対象のエンティティを追加することが出来るようになっているため、たとえ事前選択のエンティティを用意しておいたとしてもダイアログが表示されてしまいます。</p>
<p>&#0160;</p>
<p>もう一つの例外はファイル名の指定です。コマンドの中には入力としてファイル名を受け付けるコマンドがあります。こういったコマンドの中にはファイル名プリセットしておくことによりファイル名の指定をスキップすることのできるコマンドが存在します。ただしこの方法もコマンド側の実装によるため、ファイル名を事前に指定した場合においてもダイアログを表示するコマンドもあります。なお、ファイル名の事前指定には、CommandManagerのPostPrivateEvent を利用します。以下は、コンポーネント配置を実行するVBAのサンプルコードとなります。CommandManagerのPostPrivateEventで指定したファイルが利用されるため、ファイルの指定ダイアログでのファイル選択をスキップすることが出来ます。</p>
<div style="font-size: 8pt; background: #eeeeee; color: black; line-height: 140%; font-family: courier new;">Public Sub PlacePart() <br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Get the command manager. <br /></strong></span>&#0160;&#0160;&#0160; Dim oCommandMgr As CommandManager <br />&#0160;&#0160;&#0160; Set oCommandMgr = ThisApplication.CommandManager <br /><br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Post the filename. <br /></strong></span>&#0160;&#0160;&#0160; Call oCommandMgr.PostPrivateEvent(kFileNameEvent, _ <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;C:\temp\Part1.ipt&quot;) <br /><br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Get control definition for the place component command. <br /></strong></span>&#0160;&#0160;&#0160; Dim oControlDef As ControlDefinition <br />&#0160;&#0160;&#0160; Set oControlDef = oCommandMgr.ControlDefinitions.Item( _&#0160; <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &quot;AssemblyPlaceComponentCmd&quot;)&#0160; <br /><br /><span style="color: #0000ff;"><strong>&#0160;&#0160;&#0160; &#39; Execute the command. <br /></strong></span>&#0160;&#0160;&#0160; Call oControlDef.Execute <br />End Sub</div>
<p>&#0160;</p>
<h3>APIからのコマンドの同期実行・非同期実行について</h3>
<p>ここまでにご案内しておりますControlDefinition オブジェクトのExecute()メソッドは、コマンドを実行するとコマンドの終了を待たずに、処理が即座にプログラムに戻ります（非同期処理）。カスタマイズの内容によってはコマンドの終了を待ち合わせてから処理を行いたい場合などがあるかと思いますが、この場合はControlDefinition オブジェクトのExecute2()メソッドに、引数にTrueを指定して実行することにより、起動したコマンドの終了を待ち合わせることが可能となります。</p>
<p>&#0160;</p>
<p>※本記事は、「<a href="https://modthemachine.typepad.com/my_weblog/2009/03/running-commands-using-the-api.html">Running Commands Using the API</a>」から、転写・意訳・補足したものです。</p>
<p>By Takehiro Kato</p>
