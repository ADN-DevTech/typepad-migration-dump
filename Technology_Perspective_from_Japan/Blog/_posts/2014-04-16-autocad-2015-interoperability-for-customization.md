---
layout: "post"
title: "AutoCAD 2015 のカスタマイズ互換性"
date: "2014-04-16 00:18:35"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/04/autocad-2015-interoperability-for-customization.html "
typepad_basename: "autocad-2015-interoperability-for-customization"
typepad_status: "Publish"
---

<p>以前の<a href="http://adndevblog.typepad.com/technology_perspective/2013/03/migrate-autocad-api-addon-apps.html" rel="noopener noreferrer" target="_blank"><strong>ブログ記事</strong></a>では、AutoCAD 2004 以降、DWG と DXF の図面ファイル形式とアドオン アプリケーションの互換性が、3 バージョン毎に改められていることをご紹介しました。この考えに沿って考えれば、AutoCAD 2015 は、2 バージョン前の AutoCAD 2013 から続くサイクルの最後のバージョンになるので、図面ファイル形式は 2013 DWG 形式に、アドオン アプリケーション は AutoCAD 2013 用と AutoCAD 2014 用のアプリケーションと上位互換を持つことになります。</p>
<p>実は、AutoCAD 2015 は内部的に大きな変更が加えられたバージョンとなってしまったため、残念ながら、このシナリオが一旦崩れることになりました。図面ファイル形式は、AutoCAD 2013 のまま互換性を維持しますが、&#0160;AutoCAD 2013/2014 用に作成されたアドオン アプリケーションには移植作業が必要となります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5119d5b14970c-pi" style="display: inline;"><img alt="Interoperability" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a5119d5b14970c image-full img-responsive" src="/assets/image_461465.jpg" title="Interoperability" /></a></p>
<p>ここでは、AutoCAD 2015 で加えられた内部的な変更点とその影響について、その概要をご案内します。また、AutoCAD 2015 の機能について把握されていない場合には、次のリンクからブログ記事を一読していただくことをお勧めします。</p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2014/03/new-features-on-autocad-2015-part1.html" rel="noopener noreferrer" target="_blank">AutoCAD 2015 の新機能 ～ その 1</a></p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2014/03/new-features-on-autocad-2015-part2.html" rel="noopener noreferrer" target="_blank">AutoCAD 2015 の新機能 ～ その 2</a></p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2014/04/new-features-on-autocad-2015-part3.html" rel="noopener noreferrer" target="_blank">AutoCAD 2015 の新機能 ～ その 3</a></p>
<p>移植作業と言っても、大規模な改修作業と言うより小規模な改修と考えていただくほうが適切です。ObjectARX や AutoCAD .NET API を使ったアドオン アプリケーションでも、旧バージョンで綿密なエラー処理を実装しているような場合には、新しい環境で再ビルドするだけで済むかもしれません。&#0160;</p>
<p><strong>開発環境</strong></p>
<p>AutoCAD 2015 は、引き続き、32 ビット版と 64 ビット版の Windows をサポートします。ObjectARX アプリケーションを開発する場合は、従来通り、32 ビット版と 64 ビット版の両方を区別してビルドしてください。なお、ObjectARX SDK for AutoCAD 2015 は、<a href="http://www.autodesk.com/objectarx" rel="noopener noreferrer" target="_blank">http://www.autodesk.com/objectarx</a>&#0160;から無償でダウンロードすることが出来ます。</p>
<p>ObjectARX と AutoCAD .NET API の開発でサポートされる環境は、次のとおりです。</p>
<div id="section1">
<ul>
<li>Microsoft<sup>®</sup> Windows<sup>®</sup> 7 Enterprise、Professional、Ultimate の各エディション<br />&#0160; &#0160; &#0160; または、Windows 8/8.1 Enterprise、Pro の各エディション</li>
<li>Microsoft<sup>®</sup> Internet Explorer<sup>®</sup> 9 または、それ以降</li>
<li>Microsoft<sup>®</sup> Visual Studio® 2012 (Update 4)</li>
<li>.NET Framework 4.5</li>
</ul>
</div>
<div>VBA は、AutoCAD 2014 と同様に VBA 7.1 をサポートします。VBA コンポーネントのダウンロードは、<a href="http://www.autodesk.com/vba-download" rel="noopener noreferrer" target="_blank">http://www.autodesk.com/vba-download</a> から無償で可能です。</div>
<div>&#0160;</div>
<div><strong>ゼロドキュメントの状態</strong>&#0160;</div>
<p>AutoCAD 2015 では、いままでのバージョンのように、起動直後に図面を表示する子ウィンドウは表示されません。既定では [新しいタブ] が表示されます。ObjectARX や AutoCAD .NET API を使ったアプリケーションでアクティブなドキュメント（AcApDocument、Document）や図面データベース（AcDbDatabase、Database）を参照している場合には、図面が表示されていない状態、つまり、ゼロドキュメント状態 を想定したプログラム改修が必要です。例えば、現在アクティブな図面データベースの取得を実装している場合には、acdbHostApplicationServices()-&gt;workingDatabase() や&#0160;HostApplicationServices.WorkingDatabase の戻り値が、null、Null、Nothing（言語によって異なる）でないことを明示的に確認する必要があります。&#0160;</p>
<p><strong>ファイバー削除の影響</strong>&#0160;</p>
<p>AutoCAD 2015 で最も大きな内部的な変更は、このファイバー削除です。&#0160;<a href="http://ja.wikipedia.org/wiki/%E3%83%95%E3%82%A1%E3%82%A4%E3%83%90%E3%83%BC_(%E3%82%B3%E3%83%B3%E3%83%94%E3%83%A5%E3%83%BC%E3%82%BF)" rel="noopener noreferrer" target="_blank"><strong>ファイバー</strong></a>とは、古いバージョンの Windows OS など、オペレーティング システム レベルでマルチタスクを実装する初期の手段として利用されてました。</p>
<p>AutoCAD 上では、AutoCAD の親ウィンドウ内に複数の図面子ウィンドウを表示することが出来るようになった AutoCAD 2000 から、ファイバーを使ってコマンド実行中の図面子ウィンドウの切り替えをサポートしてきました。しかし、Microsoft 社は、ファイバーのサポートを既に停止しているため、AutoCAD 内部の実装からファイバーを使った処理を削除する必要がありました。その削除が、今回の AutoCAD 2015 で実行された訳です。</p>
<p>次の動画は、ファイバー削除によって影響を受ける AutoCAD 動作の違いを示してます。ファイバーが削除された AutoCAD 2015 では、コマンド実行中のコマンド切り替えがサポートされなくなります。AutoCAD 2000～AutoCAD 2014 では、コマンド実行中に図面子ウィンドウ（ドキュメント）を切り替え、また、元のドキュメントに切り替えても、実行中だったコマンド処理に復帰することが出来ますが、AutoCAD 2015 では、ドキュメント切り替え時に実行中だったコマンドは、AutoCAD によって強制的にキャンセルされるようになります。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/3F8RVL4TMFM?feature=oembed" width="500"></iframe>&#0160;</p>
<p>ファイバー削除は、AutoCAD ネイティブ コマンドを呼び出している ObjectARX アプリケーションに影響を与えます。acedCommand()/acedCmd() 関数でコマンドを呼び出している場合には、acedCommandS()/acedCmdS() または、acedCommandC()/acedCmdC() のいずれかの関数に置き換える必要があります。acedCommandS() と acedCommandC() を利用する場合の区別は、acedCommand() 内で PAUSE キーワードを使ってユーザ操作を待機する処理をしているか否かです。</p>
<p><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;acedCommand(RTSTR, _T(“_Line”),</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; RTSTR, _T(&quot;0,0&quot;),</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; RTSTR, _T(&quot;111,111&quot;),</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; RTSTR, _T(&quot;&quot;),</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; RTNONE);</span></p>
<p>のように、コマンドの実行がacedCommand() 関数内で完結している場合には、acedCommand() 関数を acedCommandS() 関数に置き換えれば移植は完了です。&#0160;</p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier;">void foo(void)<br />{</span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; acedCommand(RTSTR, _T(&quot;_Line&quot;),</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; RTSTR, _T(&quot;0,0&quot;),</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; RTSTR, _T(&quot;111,111&quot;),</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; RTNONE);</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;while(isLineActive())</span><br /><span style="font-family: &#39;courier new&#39;, courier;"> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;acedCommand(RTSTR, PAUSE, RTNONE);</span><br /><br /><span style="font-family: &#39;courier new&#39;, courier;">}</span></p>
<p>のような場合には、acedCommandC() 関数でコマンド呼び出しを置き換えた上で、PAUSE にあたる箇所をコールバック関数として実装する必要があります。</p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier;">void foo(void)<br />{</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;acedCommandC(&amp;myCallbackFn, NULL,<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; RTSTR, _T(“_Line”),<br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;RTSTR, _T(&quot;0,0&quot;),<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; RTSTR, _T(&quot;111,111&quot;),<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;RTNONE);</span><br /><br /><span style="font-family: &#39;courier new&#39;, courier;">}</span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier;">int myCallbackFn(void * pData)<br />{</span><br /><span style="font-family: &#39;courier new&#39;, courier;"> &#0160;&#0160;&#0160;&#0160;int nReturn = RTNONE;</span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160;&#0160;&#0160;&#0160;if (isLineActive())</span><br /><span style="font-family: &#39;courier new&#39;, courier;"> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;nReturn = acedCommandC(&amp;myCallbackFn, NULL,<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;RTSTR, PAUSE,<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;RTNONE); </span><br /><span style="font-family: &#39;courier new&#39;, courier;"> &#0160;&#0160;&#0160;&#0160;</span><span style="font-family: &#39;courier new&#39;, courier;">}</span><br /><span style="font-family: &#39;courier new&#39;, courier;"> &#0160;&#0160;&#0160;&#0160;return nReturn;</span><br /><span style="font-family: &#39;courier new&#39;, courier;">}</span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier;">static Adesk::Boolean isLineActive()<br /></span><span style="font-family: &#39;courier new&#39;, courier;">{&#0160;</span></p>
<p style="padding-left: 30px;"><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; struct resbuf rb; </span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; acedGetVar(_T(&quot;CMDNAMES&quot;),&amp;rb); </span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; if (_tcsstr(rb.resval.rstring, _T(&quot;LINE&quot;))) </span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; &#0160; &#0160; return Adesk::kTrue; </span><br /><span style="font-family: &#39;courier new&#39;, courier;">&#0160; &#0160; return Adesk::kFalse; </span><br /><span style="font-family: &#39;courier new&#39;, courier;"> }</span></p>
<p>ObjectARX で acedCommandS()、acedCommandC() 関数を利用する場合には、&#0160;acedCmdNF.h をインクルードする必要があります。このヘッダーファイルには、ANSI コードではないコードが混入している問題がレポートされていますので対処が必要です。。詳細は、次のドキュメントをご参照ください。</p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b8d19a0b6b970c img-responsive"><a href="http://adndevblog.typepad.com/files/qa-8870.pdf" rel="noopener noreferrer" target="_blank">QA-8870 ObjectARXアプリケーションのビルド時にSDK内のヘッダーでwarning C4819エラーになる</a></span></strong></p>
<p>AutoLISP で (command) 関数を利用している場合には、(command-s) 関数か (command) 関数を使い分ける必要があります。ユーザ対話なしでコマンドを完結できる場合には、(command) 関数を&#0160;(command-s) 関数 に置き換えてください。詳細は、<a href="http://help.autodesk.com/cloudhelp/2015/JPN/AutoCAD-AutoLISP/files/GUID-5C9DC003-3DD2-4770-95E7-7E19A4EE19A1.htm" rel="noopener noreferrer" target="_blank">こちら</a> をご参照ください。</p>
<p>.NET API では、ファイバー削除にともなって、それまでサポートされていなかったネイティブ コマンドの実行が Editor.Command メソッド と Editor.CommandAsync メソッドによってサポートされるようになりました。2つのメソッドの関係は、前述の ObjectARX の acedCommandS() と acedCommandC() の関係と同様です。従来は Editor にコマンド呼び出しをサポートするメソッドが存在していなかったため、ObjectARX の acedCommand() や acedCmd() を P/Invoke で利用したり、Document.SendStringToExecute メソッドでコマンドを呼び出す実装が必要でした。</p>
<p><strong>ダークテーマ</strong>&#0160;</p>
<p>必須ではありませんが、メニューカスタマイズで独自のボタンを用意されている場合には、ダークテーマに対応できるよう、アイコンイメージをダークテーマに対応すべきです。ダークテーマとライトテーマで利用するビットマップ リソースを管理する方法も提供されます。</p>
<p><strong>ワークスペース&#0160;</strong></p>
<p>AutoCAD 2015 には、プルダウンメニューやツールバーで構成された「AutoCAD クラシック」ワークスペースが削除されています。ただし、プロダウンメニューやツールバーなどのユーザ インタフェースは、AutoCAD 2015 でも表示して利用することが出来ます。AutoCAD クラシック相当のワークスペースの作成方法は、次の Autodesk Knowledge Network に記されています。</p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/sfdcarticles/sfdcarticles/kA93g0000000ISl.html" rel="noopener noreferrer" target="_blank">Autodesk AutoCAD 2015 でクラシック表示にすることができますか?</a></strong></p>
<p>個々の移植方法については、今後のセミナーやブログ記事で扱う予定です。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
