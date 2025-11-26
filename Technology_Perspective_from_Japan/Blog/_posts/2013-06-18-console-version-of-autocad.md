---
layout: "post"
title: "コンソール バージョンの AutoCAD"
date: "2013-06-18 21:57:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/06/console-version-of-autocad.html "
typepad_basename: "console-version-of-autocad"
typepad_status: "Publish"
---

<p>Windows では、拡張子 .exe のファイルを実行形式ファイル、あるいは、実行ファイルを呼びますが、AutoCAD の本体となる実行形式をご存じでしょうか。AutoCAD を実行する際には、デスクトップのショートカットをダブルクリックして起動をしていると思います。ここで実際に実行されるのは、AutoCAD のインストール フォルダにある <strong>acad.exe</strong> で、AutoCAD の特徴となる作図領域やユーザ インタフェースを持っています。</p>
<p>AutoCAD 2013 以降、AutoCAD のインストール フォルダには、<strong>accoreconsole.exe</strong> という別の AutoCAD 実行形式がインストールされています。もちろん、個別に実行することが出来ますが、この実行形式にはユーザインタ インタフェースがありません。この accoreconsole.exe は、いわゆる コンソールバージョンの AutoCAD です。デスクトップやスタートメニューにはショートカットは登録されないので、直接、Windows エクスプローラで AutoCAD のインストール フォルダから accoreconsole.exe を見つけないといけません。ただし、ユーザ インタフェースを持たないので、ダブルクリックして起動しても何も起こりません。</p>
<p>accoreconsole.exe の起動を確認する最も分かりやすい方法は、Windows の <a href="http://ja.wikipedia.org/wiki/%E3%82%B3%E3%83%9E%E3%83%B3%E3%83%89%E3%83%97%E3%83%AD%E3%83%B3%E3%83%97%E3%83%88" target="_blank">コマンド プロンプト</a> からの操作です。スタートボタンなどからアクセラリ &gt;&gt; コマンド プロンプト を選択して、CD コマンドで AutoCAD のインストール フォルダに移動します。</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191024ad174970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"></a>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191024ad1e3970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"></a>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192aa133029970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="コマンドプロンプト" class="asset  asset-image at-xid-6a0167607c2431970b0192aa133029970d" src="/assets/image_100179.jpg" title="コマンドプロンプト" /></a></p>
<p>accoreconsole.exe には、AutoCAD（acad.exe）のショートカットで使用するようなコマンドライン スイッチが用意されています。acad.exe 用のコマンドライン スイッチについては、<strong><a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-7FDC500C-E04A-432D-A73D-EB1B6C6728B1.htm" target="_blank">こちら</a></strong> から詳細を参照することができます。さて、accoreconsole.exe は、次の 4 つのコマンドライン スイッチを利用することができます。</p>
<p style="padding-left: 30px;"><strong>/i&#0160;<br /></strong>図面ファイルへのパスを指定します。accoreconsole.exe 起動時に指定すると、図面をメモリ上に展開して各種コマンドの操作対象とすることができます。</p>
<p style="padding-left: 30px;"><strong>/s<br /></strong>AutoCAD のスクリプト ファイル（*.scr）へのパスを指定します。<a href="http://docs.autodesk.com/ACD/2014/JPN/files/GUID-95BB6824-0700-4019-9672-E6B502659E9E.htm" target="_blank">スクリプト</a> に記述されたコマンドに沿って、自動実行をさせることができます。</p>
<p style="padding-left: 30px;"><strong>/l<br /></strong>Launguage Pack がインストールされていれば、起動した accoreconsole.exe が表示する言語を指定することができます。</p>
<p style="padding-left: 30px;"><strong>/isolate<br /></strong>AutoCAD（acad.exe）とシステム変数を分離します。これによって、通常起動した AutoCAD からの影響を受けないようにすることができます。</p>
<p>それでは、&#0160;コマンドプロンプトから accoerconsole.exe を起動してみましょう。accoreconsole.exe の後には必要に応じてコマンドライン スイッチを指定することができます。起動前と起動後のプロンプト表示に注意してみてください。</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901c550fa0970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="コマンドライン" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01901c550fa0970b image-full" src="/assets/image_333156.jpg" title="コマンドライン" /></a></p>
<p>accoreconsole.exe の起動前には、コマンド プロンプトを示すプロンプト文字が、現在のパスを示しています（<span class="auto-style1" lang="EN-US" style="color: black; font-family: verdana,geneva; font-size: 10pt; mso-fareast-font-family: メイリオ; mso-bidi-font-family: メイリオ;">C:￥Program
Files￥Autodesk￥AutoCAD 2014￥</span><span lang="EN-US" style="font-family: &quot;Verdana&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: メイリオ; mso-bidi-font-family: メイリオ;">）。また、起動後には、AutoCAD のコマンド プロンプトである「コマンド：」が表示されています。この例では、ファイルダイアログ ボックスの表示を抑止するシステム変数 FILEDIA を 0 に設定（確認）した後で、LINE コマンドを使って線分を作図しています。また、SAVEAS コマンドを使って Drawing1.dwg の名前で図面を保存していることもわかります。その後、QUIT コマンドを使って accoreconsole.exe を終了しています。終了後には、再びコマンド プロンプト文字が Windows が持つ表示の表示内容に変化していることも見て取れます。</span></p>
<p><span lang="EN-US" style="font-family: &quot;Verdana&quot;,&quot;sans-serif&quot;; mso-fareast-font-family: メイリオ; mso-bidi-font-family: メイリオ;">つまり、画面に図面はグラフィカル ユーザ インタフェースが表示されないだけで、起動している中身は AutoCAD になります。この方法を利用すれば、あらかじめ AutoCAD 上で作成したスクリプトを accoreconsole.exe に実行させて、自動印刷や作図を少ない実行時メモリで処理することが可能になるわけです。ユーザ インタフェースの表示には、多大なメモリを使用することになるので、特に 32 ビット版 Windows 上で自動処理をされている場合には、有効な方法と言えます。</span></p>
<p>唯一、注意が必要なので、accoreconsole.exe の実行中には、一切のグラフィカル ユーザ インタフェースの使用が許されない点です。先の例では、システム変数 FILEDIA の値を 0 にして、SAVEAS コマンドで [図面に名前を付けて保存] ダイアログ卜すの表示を抑止しています。accoreconsole.exe の実行時には、この手のダイアログも含めて一切を表示できないので注意が必要です、当然、図面を見ながら操作することもできないので、図面上のオブジェクトを選択するようなことも不可能です。</p>
<p>次に、なぜ、このような実行形式が用意されたのか、少し歴史をひも解いてみましょう。</p>
<p><strong>コンソール モードの AutoCAD 登場の背景</strong></p>
<p style="text-align: left; padding-left: 30px;">
AutoCAD は、今年登場した AutoCAD 2014 で 31 年を迎えています。登場当初は、当時のソフトウェアと同様に、DOS と呼ばれる OS で動くシンプルなソフトウェアでした。その後、Windows が登場して、しばらくの間、Windows だけをプラットフォームとして採用してきました。</p>
<p style="text-align: left; padding-left: 30px;">実は、AutoCAD R12 の時代には、Macintosh 版、UNIX 版など、Windows 以外のプラットフォームも採用していた時代もあったのですが、結果として Windows が主流となり、サポートを停止した経緯もあったのです。</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192aa139cc2970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"></a>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901c553426970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"></a>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191024b3f91970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="AutoCADの歴史" class="asset  asset-image at-xid-6a0167607c2431970b0191024b3f91970c" src="/assets/image_84394.jpg" title="AutoCADの歴史" /></a></p>
<p style="text-align: left; padding-left: 30px;">設計現場で Windows が日本で主流となったのは、Windows 3.1 を経て、Windows 95 が登場した時期だと思います。その頃の AutoCAD は、既にプラットフォームを&#0160;Windows に絞って開発されていました。AutoCAD をプログラムとして見た場合、Windows&#0160;にサポートされる Microsoft テクノロジに沿ったもので Windows 専用のコンポーネントやテクノロジ構成されていた時代です。カスタマイズの環境では、VBA や VBA のメカニズムとなっている COM が、Windows 固有のテクノロジです。ObjectARX の開発と、その実行環境が初めて導入されたのも、この頃、正確には AutoCAD R13 でした。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901bddf5ad970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="BigSplit1" class="asset  asset-image at-xid-6a0167607c2431970b01901bddf5ad970b" src="/assets/image_321445.jpg" title="BigSplit1" /></a></p>
<p style="padding-left: 30px;">1999 年に AutoCAD 2000 がリリースされて、AutoCAD 内部のアークテクチャにも変化が訪れます。ObjectARX は、AutoCAD 本体の開発コードから一部を抜き出した C++ クラスライブラリですが、その普及とともに、図面に作図したカスタム オブジェクトの問題が認識され出しました。</p>
<p style="padding-left: 30px;">ObjectARX の最大の特徴であるカスタム オブジェクトは、オブジェクトを定義した ObjectARX アプリケーションがロードされていない環境では、プロキシ オブジェクト化してしまい、カスタム オブジェクトとしての振る舞いができません。図面作成が外注されて図面が一人歩きを始めると、アプリケーションを所有している会社内では正しくカスタム オブジェクトを認識できても、そうでない会社では図面の意図を読み取れない、といった現象が発生しだしました</p>
<p style="padding-left: 30px;">これを防止するには、カスタム オブジェクトの定義アプリケーションを常にロードさせる必要がありますが、有償販売されているアプリケーションの場合、すべての設計環境にアプリケーションを販売しなければいけなくなります。しかし、予算が有り余るといっや状況でない限り、それは難しい話なのは言うまでもありません。</p>
<p style="padding-left: 30px;">そこで考え出されたのが、コマンドを含むユーザインタフェースとデータベース機能の分離を、UI/DB 分離として提唱した<strong>オブジェクト イネーブラー</strong>です。有償販売のアプリケーションには、カスタム オブジェクトを新規に作図、または、編集する機能を持つ ObjectARX アプリケーションを拡張子 .arx で提供してもらうようにして、カスタム オブジェクトを定義した ObjectARX アプリケーションには、拡張子 .dbx で無償ダウンロードすることを開発者に提案したのです。.dbx ファイルを持つアプリケーションは、オブジェクト イネーブラーの呼び名の他に、ObjectDBX アプリケーションとも呼ばれました。ちなみに、オートデスクが開発・販売した AutoCAD ベース製品にもカスタム オブジェクトが疲れわれていますが、それらのオブジェクト イネーブラーも <a href="http://www.autodesk.com/oe">http://www.autodesk.com/oe</a> からダウンロードすることが出来ます。</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eeadb862a970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"></a>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019101d3f1a9970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="BigSplit2" class="asset  asset-image at-xid-6a0167607c2431970b019101d3f1a9970c" src="/assets/image_800918.jpg" title="BigSplit2" /></a></p>
<p style="padding-left: 30px;">しばらく、Windows に特化して開発を進めてきたのですが、2000 年代後半になってコンピュータの世界に変革が訪れます。 Apple iPhone に代表されるモバイルとクラウドです。iPhone の登場でにわかに活気づいたのが、同じ Apple 社の OS である Mac OS です。そこで、オートデスクは <a href="http://www.autodesk.com/autocadformac" target="_blank">AutoCAD for Mac</a> に開発に着手したのです。</p>
<p style="padding-left: 30px;">もちろん、AutoCAD R12 時代のように、Windows 版と Mac 版 の AutoCAD をゼロから作成するのは労力とコストがかかります。そこで、AutoCAD のコアにあたる部分を共通化するプロジェクトが発足しました。そこで考え出されたのが、コンソール アプリケーションの存在です。コンソール アプリケーションは、.arx、.dbx と特別するために、新しい拡張子 .crx を与えられています。ただ、いずれも中身は ObjectARX アプリケーションです。そして、AutoCAD 2013 から、.crx が Windows 版、Mac 版に依存しない共通機能を持つよう、アーキテクチャが変更されたのです。逆に言えば、.crx アプリケーションは、プラットフォームに依存しないよう、グラフィカル ユーザ インタフェースを持たないよう提唱しています。&#0160;</p>
<p style="text-align: center;">
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901eccc537970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CoreApps" class="asset  asset-image at-xid-6a0167607c2431970b01901eccc537970b" src="/assets/image_300813.jpg" title="CoreApps" /></a></p>
<p><strong>カスタマイズでコンソール モードの AutoCADを利用する</strong></p>
<p>このように、accoreconsole.exe は非常に軽量な AutoCAD です。ユーザ インタフェースが表示できないので、対話的な作業に制限がありますが、利点もあります。スクリプト以外にも、.crx アプリケーションを開発して accoreconsole.exe にロードさせることで、カスタム コマンドを実行させることが出来るのです。.crx アプリケーションは、ObjectARX アプリケーションの1つとして名づけられたものですが、ユーザ インタフェースを表示をしない他の種類の .NET API アプリケーションもロードして実行させることが可能です。軽量な環境で、自動製図プログラムや変換プログラム、あるいは、連続印刷などの処理を実行できます。</p>
<p>非常に便利なコンソール モードの AutoCAD（accoreconsole.exe）ですが、AutoCAD のインストール フォルダからこのファイルだけを抜き出しても利用はできません。AutoCAD の一部として、インストール フォルダにある様々なコンポーネントとともに実行されるようになっているためです。また、当然、ライセンスは AutoCAD に付帯する、というか AutoCAD そのものなので、コンポーネントの分離は禁止された行為となります。</p>
<p>また、共有サーバーで AutoCAD をプログラム制御したい、といったご要望を多く聞きますが、現段階では 開発者向けに用意されたサーバー ライセンスは存在しません。このような用途で、スタンドアロン ライセンス、ネットワーク ライセンス、セッション用ネットワーク ライセンス等に含まれる、acad.exe、accoreconsole.exe を利用する行為は、使用許諾違反となります。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
