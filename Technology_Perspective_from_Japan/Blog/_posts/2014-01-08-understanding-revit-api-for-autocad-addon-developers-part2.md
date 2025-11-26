---
layout: "post"
title: "AutoCAD アドオン開発者のためのRevit API入門～アドイン"
date: "2014-01-08 01:17:33"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/01/understanding-revit-api-for-autocad-addon-developers-part2.html "
typepad_basename: "understanding-revit-api-for-autocad-addon-developers-part2"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2013/12/understanding-revit-api-for-autocad-addon-developers-part1.html" target="_blank"><strong>概説</strong></a>に続いて、Revit API を用いた実装形態の 1 つである <strong>アドイン アプリケーション</strong>についてご紹介します。アドインも、AutoCAD でアドオン、3ds Max でプラグインと呼ぶものと同じロードモジュールで、API を利用してデスクトップ製品を拡張する能力を提供します。</p>
<p>Revit API を用いたカスタマイズの目的には、AutoCAD と同じようにカスタム コマンドの作成を挙げることが出来ます。コマンドは Revit でも機能実行の最小単位であり、Revit を利用する設計者は、Revit に標準で組み込まれたコマンドをリボン インタフェースから選択して、モデルやシートを作成していきます。このコマンドを定義するためには、単に Revit API をアドインの中で利用する必要があります。</p>
<p>ここで言う アドイン とは、AutoCAD 用に AutoCAD .NET API を使ってビルドしたアドオン アプリケーション（DLL アセンブリ）と同じ意味合いを持ちます。アドインを開発するためには、基本的に Revit 製品自身を利用する .NET Framework と同じバージョンを用いたアセンブリをビルドする、Visual Studio が必要となります。Revit 2014 の場合には、Visual Studio 2010 がサポート バージョンとなります。</p>
<p>さて、AutoCAD のコマンドに<a href="http://adndevblog.typepad.com/technology_perspective/2013/11/understandin-command-context-on-autocad.html" target="_blank">実行コンテキスト</a>による区分がありましたが、Revit には、別の区分が存在しています。それが、<strong>外部コマンド</strong> と <strong>外部アプリケーション</strong> です。Revit にはAutoCAD のようなコマンド プロンプト ウィンドウは存在しないので、コマンドを直接入力して実行することは出来ません。いずれのタイプのコマンドも、Revit 上のユーザ インタフェース（ボタンまたはメニュー）から起動して実行することになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b03bb6619970d-pi" style="display: inline;"><img alt="Commands" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b03bb6619970d image-full img-responsive" src="/assets/image_31017.jpg" title="Commands" /></a></p>
<p><strong>外部コマンド</strong></p>
<p>アドインで外部コマンドを定義すると、Revit 上では [アドイン] リボンパネルの [外部ツール] ボタンの下に、コマンド名が表示されることになります。ソースコード上で外部コマンドを定義するには、IExternalCommand インタフェースから派生したクラスを定義します。次の例は、C# 言語で記述された外部コマンドのスケルトン コードです。</p>
<table border="0" cellpadding="0" cellspacing="0" class="auto-style2" style="width: 100%;">
<tbody>
<tr>
<td align="left">&#0160;</td>
</tr>
<tr>
<td>
<table border="0" cellspacing="5" width="100%">
<tbody>
<tr>
<td align="left" bgcolor="#c0c0c0" valign="top" width="100%">
<p style="color: #000000; font-family: Arial, sans-serif; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier;"> [Transaction(TransactionMode.Manual)]</span><br /> <span style="font-family: &#39;courier new&#39;, courier;"> public class HelloWorld : IExternalCommand</span><br /> <span style="font-family: &#39;courier new&#39;, courier;"> {</span><br /> <span style="font-family: &#39;courier new&#39;, courier;">&#0160; public Result Execute(ExternalCommandData commandData,</span><br /> <span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ref string message,</span><br /> <span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; ElementSet elements)</span><br /> <span style="font-family: &#39;courier new&#39;, courier;">&#0160; {</span></p>
<p style="color: #000000; font-family: Arial, sans-serif; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160; // 実装を記述</span></p>
<p style="color: #000000; font-family: Arial, sans-serif; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160; return Result.Succeeded;</span><br /> <span style="font-family: &#39;courier new&#39;, courier;">&#0160; }</span><br /> <span style="font-family: &#39;courier new&#39;, courier;"> }</span></p>
</td>
</tr>
</tbody>
</table>
</td>
</tr>
<tr>
<td align="left">　</td>
</tr>
</tbody>
</table>
<p>この例では、HellowWorld というクラス名で外部コマンド用のクラスを定義しています。このクラスで実際に実行される外部コマンドは、Execute メソッド（必須）になります。</p>
<p>AutoCAD .NET API の場合、コマンド定義関数の前にコマンド属性を記述して、そこでコマンド名やコマンド グループ、コマンド フラグといった情報を与えますが、Revit ではコマンド属性にトランザクション モード（後述）を与えるのみです。</p>
<p>[外部コマンド] ボタンの下に表示されるコマンド名は、Visual Studio 上で記述したソースコード上の文字列ではなく、後述するアドイン マニフェストで記述したものが表示されます。AutoCAD と同じように、1 つのアドインには複数の外部コマンドを定義することが出来ます。また、起動中の Revit には、それぞれ外部コマンドを定義した複数のアドインをロードすることが出来ます。そのような場面では、AutoCAD のプルダウン メニューのようなかたちでコマンドが表示されることになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b03baf6aa970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="External_Commands" class="asset  asset-image at-xid-6a0167607c2431970b019b03baf6aa970c" src="/assets/image_484677.jpg" title="External_Commands" /></a>&#0160;</p>
<p><strong>外部アプリケーション</strong></p>
<p>外部コマンドを沢山登録してしまうと、使用したいコマンドがどこにあるのかわかり難くなってしまいます。そこで、専用のアイコンを持ったボタンやリボン パネル、また、リボン タブを用意したいと考えるのが一般的です。外部コマンドには、そのような独自のユーザ インタフェースを実装する機能がありません。ただし、外部アプリケーションなら可能です。</p>
<p>次の例は、C# 言語で記述された外部アプリケーションのスケルトン コードです。&#0160;どこにもコマンドに必要な属性情報の設定がないことがわかります。また、IExternalApplication インタフェースから派生したクラス内に、OnStartup() と OnShutdown() があり、引数として UIControlledAppication オブジェクトが渡されています。&#0160;</p>
<table border="0" cellspacing="5" width="100%">
<tbody>
<tr>
<td align="left" bgcolor="#c0c0c0" valign="top" width="100%">
<p style="color: #000000; font-family: Arial, sans-serif; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; padding-left: 30px;"><span class="auto-style3" style="font-family: &#39;courier new&#39;, courier;">class App : IExternalApplication<br /> {<br /> &#0160; public Result OnStartup(UIControlledApplication a)<br /> &#0160; {<br /> &#0160;&#0160;&#0160; return Result.Succeeded;<br /> &#0160; }<br /> <br /> &#0160; public Result OnShutdown(UIControlledApplication a)<br /> &#0160; {<br /> &#0160;&#0160;&#0160; return Result.Succeeded;<br /> &#0160; }<br /> }</span></p>
</td>
</tr>
</tbody>
</table>
<p>この例が示すとおり 、外部アプリケーションとは、ボタンやリボン パネル、リボン タブといったユーザ インタフェース要素やイベントの追加や削除の処理に使用するもので、コマンドそのものを定義するものではありません。別の言い方とすると、外部アプリケーションで作成したボタンなどがクリックされた際に、どの外部コマンドを実行するのかを定義するのが外部アプリケーションの役割です（ボタンと外部コマンドの関連付け）。</p>
<p>外部アプリケーションと外部コマンドは、同じアドイン モジュールの中に定義することが可能です。また、外部アプリケーションを定義したアドインから、別のアドインが定義した外部コマンドを呼び出すようなことも出来ます。</p>
<p>次の例は、RevitLookup.dll アセンブリで定義された外部コマンド&#0160;HelloWorld を、独自のリボン パネル上に配置したボタンで実行させる処理の例です（パスなどは省略しています）。 この記事の最初に表示した外部アプリケーションのボタンが、このコードが実現する処理の結果となります。</p>
<table border="0" cellspacing="5" width="100%">
<tbody>
<tr>
<td align="left" bgcolor="#c0c0c0" valign="top" width="100%">
<p style="color: #000000; font-family: Arial, sans-serif; font-size: small; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: auto; word-spacing: 0px; -webkit-text-stroke-width: 0px; padding-left: 30px;"><span class="auto-style3" style="font-family: &#39;courier new&#39;, courier;">class App : IExternalApplication<br /> {<br /> &#0160; public Result OnStartup(UIControlledApplication a)<br /> &#0160; {<br /> &#0160;&#0160;&#0160; RibbonPanel ribbonPanel =<br />&#0160; &#0160; &#0160; &#0160; &#0160;a.CreateRibbonPanel(&quot;NewRibbonPanel&quot;);<br /> &#0160;&#0160;&#0160; PushButton pushButton = ribbonPanel.AddItem(<br />&#0160; &#0160; &#0160; &#0160; &#0160;new PushButtonData(&quot;HelloWorld&quot;,<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;HelloWorld&quot;,<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; @&quot;<a href="file:///C:/RevitLookup.dll">C:\RevitLookup.dll</a>&quot;,<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;RevitLookup.HelloWorld&quot;)<br />&#0160; &#0160; &#0160; &#0160; &#0160;) as PushButton;<br /> &#0160;&#0160;&#0160; Uri uriImage = new Uri(@&quot;C:\earth.ico&quot;);<br /> &#0160;&#0160;&#0160; BitmapImage largeImage = new BitmapImage(uriImage);<br /> &#0160;&#0160;&#0160; pushButton.LargeImage = largeImage;<br /> &#0160;&#0160;&#0160; return Result.Succeeded;<br /> &#0160; }<br /> <br /> &#0160; public Result OnShutdown(UIControlledApplication a)<br /> &#0160; {<br /> &#0160;&#0160;&#0160; return Result.Succeeded;<br /> &#0160; }<br /> }</span></p>
</td>
</tr>
</tbody>
</table>
<p>外部アプリケーション と 外部コマンド&#0160;の違いは、独自のユーザ インタフェースを持つか否かであって、AutoCAD の ドキュメント実行コンテキスト と アプリケーション実行コンテキスト の違いとは全く別物と考えることが出来ます。&#0160;</p>
<p><strong>コマンド内のトランザクション</strong></p>
<p>AutoCAD の ObjectARX や .NET API と同じように、Revit API でもトランザクションを指定して要素を操作することが出来ます。AutoCAD のように、明示的にオープン モードを指定するのではなく、アンドゥ時に表示されるアンドゥの単位に名前を付けることが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b03bc75f2970d-pi" style="display: inline;"><img alt="Transaction_undo" class="asset  asset-image at-xid-6a0167607c2431970b019b03bc75f2970d" src="/assets/image_622773.jpg" title="Transaction_undo" /></a>&#0160;</p>
<p>1 つの外部コマンド内の処理に、複数のトランザクションを設けてることも可能です。この処理は、AutoCAD の UNDO コマンドでグループ化を指定するような処理と同じです。AutoCAD では、ObjectARX でも、.NET API でもアンドゥの細かい制御機能が API で公開されていませんが、Revit API では可能という訳です。</p>
<p>さて、Revit API では、外部コマンドの定義の際にコマンド属性としてトランザクション モードを指定する、という説明をしました。細かいアンドゥ処理を可能にするには、トランザクション モードを&#0160;TransactionMode.Manual に指定する必要があります。</p>
<p>トランザクション モード（TransactionMode）には、Manual の他に Automaric と ReadOnly がありますが、アンドゥなどの利便性から Manual の利用が推奨されています。詳細は後日、別のブログ記事でご紹介する予定です。&#0160;</p>
<p><strong>アドインの開発</strong></p>
<p>Revit 2014 アドインの開発には、前述のとおり Visual Studio 2010 を利用します。Revit API は .NET Framework をベースにしているため、Visual Studio がサポートする .NET 言語であれば、どの言語を選択していただいけも結構です。ただ、Revit SDK に用意されているのは、C# と VB.NET に限定されていますので、この2つの言語のいずれかの利用をお勧めしています。他の言語を使った事例は、ほとんどありません。</p>
<p>Visual Studio を利用する開発では、最初にプロジェクトを作成する必要があります。これは、AutoCAD .NET API とも同じですが、Revit API にもスケルトン プロジェクトを作成する Wizard が用意されています。ただし、Revit&#0160;2014 用の&#0160;Wizard は、Revit SDK にではなく、次のブログ記事上に C# 用と VB.NET 用が記載されています。</p>
<p style="text-align: center;"><a href="http://thebuildingcoder.typepad.com/blog/2013/05/add-in-wizards-for-revit-2014-1.html">http://thebuildingcoder.typepad.com/blog/2013/05/add-in-wizards-for-revit-2014-1.html</a></p>
<p>ここに記載された Wizard には、残念ながらインストーラは含まれていませんが、セットアップは簡単です。ダウンロードした ZIP ファイルを、言語別のフォルダにコピーするだけでセットアップが完了します。Visual Studio 2010 を再起動すると、言語カテゴリ毎にテンプレート アイコンが表示されるはずです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a50fef78d7970c-pi" style="display: inline;"><img alt="Wizard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a50fef78d7970c image-full img-responsive" src="/assets/image_539878.jpg" title="Wizard" /></a></p>
<p>いろいろな Revit API の機能を 試してみたい場合など、この Wizard を使うと便利です。本格的なプロジェクトを作成する場合には、Revit API Developers Guide にある手順に沿って、手動でプロジェクトを作成していくのもいいかも知れません。そのほうが、ファイル名などの命名規則に沿ったプロジェクトを作成することが出来るはずです。</p>
<p><strong>アドインのロード処理</strong></p>
<p>作成したアドイン アセンブリを Revit にロードさせるには、アドイン マニフェストと呼ばれる XML 形式の定義ファイルを、Revit がインストールされたコンピュータの指定されたフォルダに配置する必要があります。アドイン マニフェストは、外部コマンドと外部アプリケーション毎に登録する必要があります。1 つのファイルにまとめて記述することも出来ます。</p>
<p>アドイン マニフェストには、ロードすべきアセンブリ ファイルのパスなどのほか、外部コマンドのコマンド名、会社情報などを指定することが出来ます。外部コマンドを登録した場合には、&lt;Text&gt; タグの値で指定された文字列が、コマンド名として [外部ツール] ボタン下に表示されることになります。</p>
<table border="0" cellspacing="5" width="100%">
<tbody>
<tr>
<td align="left" bgcolor="#c0c0c0" valign="top" width="100%"><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;">&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &lt;RevitAddIns&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160; &lt;AddIn Type=&quot;Command&quot;&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;Text&gt;<strong>HelloWorld</strong>&lt;/Text&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;Description&gt;Some description for HelloWorld&lt;/Description&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;Assembly&gt;HelloWorld.dll&lt;/Assembly&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;FullClassName&gt;HelloWorld.Command&lt;/FullClassName&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;ClientId&gt;d7da1e85-ebae-4f91-bd41-5fc50512dc16&lt;/ClientId&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;VendorId&gt;Autodesk&lt;/VendorId&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;VendorDescription&gt;Autodesk, http://www.autodesk.co.jp&lt;/VendorDescription&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160; &lt;/AddIn&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160; &lt;AddIn Type=&quot;Application&quot;&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;Name&gt;Application HelloWorld&lt;/Name&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;Assembly&gt;HelloWorld.dll&lt;/Assembly&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;FullClassName&gt;HelloWorld.App&lt;/FullClassName&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;ClientId&gt;426c9e14-49a7-4245-8e41-288336d18177&lt;/ClientId&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;VendorId&gt;Autodesk&lt;/VendorId&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160;&#0160;&#0160; &lt;VendorDescription&gt;Autodesk, http://www.autodesk.co.jp&lt;/VendorDescription&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &#0160; &lt;/AddIn&gt;</span><br class="auto-style1" /><span class="auto-style1" style="font-size: 10pt; font-family: helvetica;"> &lt;/RevitAddIns&gt;</span></td>
</tr>
</tbody>
</table>
<p>アドイン マニフェストに登録する外部コマンドや外部アプリケーションは、GUID によって一意に識別されて Revit にロードされます。このため、&lt;ClientId&gt; タグ（または &lt;AddInId&gt; タグ）の値が他の外部コマンドや外部アプリケーションと重複してはいけません。</p>
<p>アドイン マニフェストで利用するタグのリファレンスとマニフェスト ファイルの配置場所については、Revit API Developers Guide の <a href="http://wikihelp.autodesk.com/Revit/enu/2013/Help/00006-API_Developer&#39;s_Guide/0001-Introduc1/0018-Add-In_I18/0022-Add-in_R22" target="_blank"><strong>こちら</strong></a>&#0160;に記載されています。</p>
<p>アドイン ファイルのロードは、Revit SDK に用意された Addin-Manager を利用する場合を除き、ここで紹介したアドイン マニフェストを使う以外、他にはありません。AutoCAD のような<a href="http://adndevblog.typepad.com/technology_perspective/2013/09/auto-loading-for-autocad-addon-apps.html" target="_blank">多彩なロード手法</a>が用意されていないので、逆に迷ってしまうことはなくていいのかも知れません。&#0160;</p>
<p><strong>アドインのバージョン互換性</strong>&#0160;</p>
<p>AutoCAD API と違って、Revit API はバージョンアップ毎に互換性が崩れます。このため、アドインを新バージョンに対応させるためには、基本的に毎回、移植作業が必要になってります。諸説ありますが、互換性を維持しない理由には、次のような理由が挙げられています。</p>
<ul>
<li>Revit がデータファイルや API の後方互換性（古いバージョンとの互換性）を注力せずに、新バージョンへ向けた開発リソースを集中しているため。</li>
<li>Revit API の書式の整合性や一貫性の保持、クリーンナップを毎バージョン実施して、分かり易い API を目指しているため。</li>
</ul>
<p>実際、新バージョンの Revit がリリースされる度に、新バージョン用の&#0160;Revit SDK が用意されます。この時、旧バージョンで利用できた一部のクラス、メソッドやプロパティなどが、Obsolete（旧式）にマークされて、ビルド時に新しいクラス、メソッドやプロパティを利用するよう、警告メッセージが表示されることがあります。</p>
<p>Obsolete（旧式）に指定された Revit バージョンでは、警告メッセージがビルド時に出るものの、コードそのものは問題なく実行できます。ただ、更に次のバージョンがリリースされた時点で、Obsolete（旧式）だったクラス、メソッドやプロパティが完全に削除されていますので、なるばく早めに移植作業（コードの書き直し）を完了しておくことが推奨されています。</p>
<p>同様に、Revit のプロジェクト ファイル（.rvt）などのデータファイルは、一度、最新バージョンにマイグレートしてしまうと、旧バージョン用に保存し直すことが出来ません。</p>
<p>DWG 図面ファイル形式やアドオン アプリケーションの互換性を、3 バージョン間維持するようにバージョン アップを繰り返す AutoCAD は、CAD 製品としては特殊なのかも知れません。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
