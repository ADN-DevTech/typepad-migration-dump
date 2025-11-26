---
layout: "post"
title: "Fusion 360 API：コマンド"
date: "2015-12-16 15:54:19"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/12/fusion-360-api-command.html "
typepad_basename: "fusion-360-api-command"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/menu-customize-on-fusion-360-workspace.html" target="_blank"><strong>過去のブログ記事</strong></a>でメニューカスタマイズについて、ご紹介したことがあります。メニューに配置するのはカスタム コマンドになりますが、体系的な情報が揃って来ましたので、改めてコマンドについてご案内します。</p>
<p>Fusion 360 上のコマンドとは、通常、ユーザによってツールバー等に配置されたボタンをクリックすることで実行される実行単位を指します。ボタンとしてのユーザ インタフェース上の表現を持つため、アイコン リソースとして画像イメージを持つだけでなく、マウスカーソルをボタン上にホバーさせた際のツールチップや説明文などの情報も持っています。</p>
<p>Fusion 360 API でカスタム コマンドを定義することが出来ます。Fusion 360 上のすべてのコマンドは、CommandDefinition オブジェクトで定義されています。CommandDefinition オブジェクトは、CommandDefinitions コレクション オブジェクトがオーナーとなり管理されています。カスタム コマンドを作成する場合には、CommandDefinitions コレクション オブジェクトの addxxxxx メソッドによって&#0160;CommandDefinition オブジェクトを作成して定義していくことから始めます。使用するメソッドは、コマンドのタイプによって異なります。ボタン タイプのユーザ インタフェースを作成する場合には、<a caaskey="caas/CloudHelp/cloudhelp/ENU/Fusion-360-API/files/CommandDefinitions-addButtonDefinition-htm.html" href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/files/CommandDefinitions_addButtonDefinition.htm" target="_blank">addButtonDefinition</a>&#0160;メソッドを、チェックボックス タイプの場合には&#0160;<a caaskey="caas/CloudHelp/cloudhelp/ENU/Fusion-360-API/files/CommandDefinitions-addCheckBoxDefinition-htm.html" href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/files/CommandDefinitions_addCheckBoxDefinition.htm" target="_blank">addCheckBoxDefinition</a>&#0160;メソッドを、リストボックス タイプの場合には、<a caaskey="caas/CloudHelp/cloudhelp/ENU/Fusion-360-API/files/CommandDefinitions-addListDefinition-htm.html" href="http://help.autodesk.com/cloudhelp/ENU/Fusion-360-API/files/CommandDefinitions_addListDefinition.htm" target="_blank">addListDefinition</a>&#0160;メソッドをそれぞれ使用します。いずれかのメソッドを呼び出すことで、カスタム コマンドがメニュー上に表示されるコントロール定義（ControlDefinition オブジェクト）と関連付けられます。</p>
<p>CommandDefinition オブジェクトに定義すべき基本情報は次のとおりです。</p>
<ul>
<li>コマンド名</li>
<li>アイコン</li>
<li>ツールチップ</li>
<li>ツールクリップ（オプション）</li>
</ul>
<p>ここでは、最も一般的なボタン コントロールを作成するために、addButtonDefinition メソッドでコマンド定義にコントロールを関連付けたことにします。</p>
<p>さて、マウスをカスタム コマンドで定義したボタンにホバーさせると、そのコマンドを説明するツールチップが表示されますが、そのままの状態を保つと、ツールチップを拡張した画像を含む説明が表示されます。これがツールクリップで、オプションで設定することが出来ます。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d18336d4970c-pi" style="display: inline;"><img alt="Commanddefinition" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d18336d4970c image-full img-responsive" src="/assets/image_518569.jpg" title="Commanddefinition" /></a></p>
<p>次の Python コードは、カスタム コマンドを定義する最初の部分の抜粋です。カスタム コマンドには、Fusion 360 のコマンドも合わせて、一意なコマンド ID を指定している点に注意してください。下記のコードでは、&#39;meshIntersect&#39; が、このコマンドのコマンド ID です。この ID は、Fusion 360 のセッション中存続し続けるので、カスタム コマンドと関連付けられたボタン コントロールの有無を識別に利用することが出来ます。</p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">app = adsk.core.Application.get()</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">ui = app.userInterface</span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;"># CommandDefinitions コレクションを取得</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">cmdDefs = ui.commandDefinitions</span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;"># ボタンのコマンド定義を作成</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">meshIntButton = cmdDefs.addButtonDefinition(&#39;meshIntersect&#39;,  &#39;メッシュボディ交差&#39;, </span><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">&#39;XY スケッチ平面を通るメッシュボディを交差&#39;,</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;"> &#39;Resources/MeshIntersect&#39;)</span></p>
<p>コマンドには、コマンドを選択できない状態にする無効化の状態を指定することが出来ます。カスタム コマンドには、標準とダーク、無効化状態の3つの状態で参照されるアイコン画像（.png ファイル）を、指定したフォルダに合計16個のアイコンイメージをサイズ毎に用意します。各アイコン ファイルは、固定の名称が決められていて、Fuson 360 が状態に応じて使用するアイコンを選択します。もし、適切なアイコン画像が見つからない場合には、Fusion 360 が動的に相当画像を生成します。なお、@2x を含む画像ファイルは、Mac などの高解像度ディスプレイで Fusion 360 を利用する際に参照されるアイコンとなります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d18337e0970c-pi" style="display: inline;"><img alt="Icons" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d18337e0970c img-responsive" src="/assets/image_149262.jpg" title="Icons" /></a></p>
<p>ユーザがメニュー上からカスタム コマンド（ボタン）をクリックする際には、イベントが発生します。カスタム コマンドの定義では、イベント ハンドラを用意して実行すべきコードを記述していきます。この場合、通常、2 つのステップで 2 つのイベント ハンドラを設定します。</p>
<p>最初は、コマンドが作成された際のイベント ハンドラへの接続処理です。ここで設定したイベントハンドラ内で、ボタンクリックの際に実行される処理を実装するイベント ハンドラを設定します。次のコード例では、作成したカスタム コマンド（ボタン）を代入した&#0160;meshIntersectButton 変数を使って、MeshIntersectCommandCreatedEventHandler() の名前のついたイベント ハンドラを設定しています。</p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;"># コマンド作成イベントへの接続</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">meshIntersectCommandCreated = MeshIntersectCommandCreatedEventHandler()</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">meshIntersectButton.commandCreated.add(meshIntersectCommandCreated)</span></p>
<p>最後に、ボタン クリック時のイベント ハンドラ内で、処理すべき内容を記述するわけです。次のコードは、MeshIntersectCommandCreatedEventHandler() イベント ハンドラ内の抜粋です。変数 cmd は、イベントハンドラの引数として取得できる&#0160;CommandCreatedEventArgs オブジェクトです。ボタンクリック時に発生する&#0160;MeshIntersectCommandExecutedEventHandler() イベントハンドラへの接続をしています。</p>
<p><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;cmd = args.command</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">&#0160;&#0160;&#0160;&#0160;inputs = cmd.commandInputs</span></p>
<p><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">&#0160; &#0160; # コマンド実行イベントへの接続</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">&#0160; &#0160; commandExecuted = MeshIntersectCommandExecutedEventHandler()<br />&#0160; &#0160; cmd.<span style="color: #ffffff;">”</span>execute<span style="color: #ffffff;">”</span>.add (commandExecuted)</span></p>
<p>CommandDefinition オブジェクトが定義できたら、そのカスタム コマンドを、既存メニューのどこに挿入するか決定します。この時、Fusion 360 がもともと持っているコマンドの ID を指定して挿入位置を決定します。ここで重要なのは、既存のコマンド ID をどのように知るかです。実は、コマンド ID を取得するコードが、Fusion 360 のオンラインヘルムにあるサンプルコードとして提供されています。オンラインヘルプから Write user interface to a file を探してみてください。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f98476970b-pi" style="display: inline;"><img alt="Sample_to_get_command_id" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f98476970b image-full img-responsive" src="/assets/image_780237.jpg" title="Sample_to_get_command_id" /></a></p>
<p>下記のコードは、スケッチ ツールバー パネル下にある [投影/含める] ドロップダウンの [交差] コマンド下に、カスタム コマンドを挿入する例です。[交差] ボタンの ID が&#0160;&#39;IntersectCmd&#39; であることがわかります。</p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;"># スケッチ ツールバー パネルを取得&#0160;</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">sketchPanel = ui.allToolbarPanels.itemById(&#39;SketchPanel&#39;)</span><br /> <br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">#&quot;投影/含める&quot; ドロップ ダウン&#0160;</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">projDropDown = sketchPanel.controls.itemById(&#39;ProjectIncludeDropDown&#39;)</span><br /> <br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;"># 交差 コマンドの下にメッシュ ボディ コマンド（カスタム コマンド）を追加</span><br /><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">projDropDown.controls.addCommand(meshIntersectButton, &#39;IntersectCmd&#39;, True)</span></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7f98518970b-pi" style="display: inline;"><img alt="Insert_position" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7f98518970b image-full img-responsive" src="/assets/image_268759.jpg" title="Insert_position" /></a></p>
<p>コードが実行されると、[交差] コマンド下にカスタム コマンドが挿入されるはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d18339d0970c-pi" style="display: inline;"><img alt="Inserted_command" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d18339d0970c image-full img-responsive" src="/assets/image_247434.jpg" title="Inserted_command" /></a></p>
<p>最後に、カスタム コマンドをスクリプトとアドインのどちらのタイプで実装すべきかです。基本的には、ユーザ インタフェース項目は Fusion 360 の起動時に表示されて、すぐに使用できるようにすべきなので、必然的に、Fusion 360 の起動直後に処理出来るアドインの形態がベストです。</p>
<p>ここまでの手順をまとめると、次のようになります。</p>
<ol>
<li>コマンド定義を作成 －&#0160;CommandDefinition オブジェクト</li>
<li>ユーザ インタフェース上にカスタム コマンドをどう表示するか、コントロール定義を追加&#0160; －&#0160;CommandDefinitions.addButtonDefinition メソッド</li>
<li>コマンド定義をコマンド作成イベントを接続 －&#0160;CommandDefinition.commandCreated.add メソッド</li>
<li>コマンド作成イベント内でコマンド実行イベントを接続 － CommandEvent.add メソッド</li>
</ol>
<p>この処理が正しく実行されると、ユーザがコントロールをクリックすると、接続されたコマンド作成イベントが Fusion から呼び出され、次にコマンド作成イベントから接続されたコマンド実行イベントが Fusion から呼び出されるようになります。もし、カスタム ダイアログを表示させる場合には、コマンド作成イベント内で更に特定の実装をこなう必要がありますが、今回はカスタム ダイアログの指定はおこないません。</p>
<p>さて、ここでご紹介したサンプルのソースコードは、GitHub 上の<a href="https://github.com/brianekins/MeshIntersect" target="_blank">こちらのサイト</a>で公開されています。なお、ブログに記載するにあたって、コメントやメニュー名を日本語化していますが、オリジナルは英語表記になっていますのでご注意ください。</p>
<p>By Toshiaki Isezaki&#0160;</p>
