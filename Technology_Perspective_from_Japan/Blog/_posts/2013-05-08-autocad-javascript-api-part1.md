---
layout: "post"
title: "AutoCAD JavaScript API の使い方 ～ その 1"
date: "2013-05-08 03:02:45"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/05/autocad-javascript-api-part1.html "
typepad_basename: "autocad-javascript-api-part1"
typepad_status: "Publish"
---

<p>今日は、<a href="http://adndevblog.typepad.com/technology_perspective/2013/04/new-features-on-autocad-2014-part3.html" rel="noopener noreferrer" target="_blank">AutoCAD 2014 の新機能 ～ その 3</a> で紹介した JavaScript API についてご案内しましょう。5 番目の API となる JavaScript API は、AutoCAD にとって、初めて Web 開発者に AutoCAD カスタマイズの門戸を開く役割を担います。</p>
<p>CAD のカスタマイズと言うと、Web やデータベースといった一般的なシステム開発をおこなう開発者にとって、少々敷居の高い印象を与えてしまっているようです。なぜなら、図面や図形、座標やベクトルといった独特の要素に加えて、CAD ソフトウェアの中に、ある意味、完成された OS のような世界が存在しているからです。例えば、その中で OS と同じようにコマンドを実行していくことができますし、独自のユーザ インタフェースや API が 4 つも持っています（いままで）。Windows や Mac といった OS をプラットフォームと呼びますが、AutoCAD を開発プラットフォームとして捉えば、歴史や奥行きなどから、どうしてもハードルが高くなりがちです。</p>
<p>Web 開発の世界はどうでしょう。かなり広範囲な話になってしまいますが、クライアント コンピュータ側での実行を担うプラットフォームという側面として見るなら、Web ブラウザをプラットフォームとして考えることが出来ると思います。そんな Web プラットフォームを AutoCAD に取り込んだのが、AutoCAD&#0160;JavaScript API です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901bdd3dde970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"></a> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901bdd3e1b970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="JavaScript API" class="asset  asset-image at-xid-6a0167607c2431970b01901bdd3e1b970b" src="/assets/image_902704.jpg" title="JavaScript API" /></a></p>
<p>Web を中心に開発をしている開発者の中には、クラウド コンピューティングに精通している方もいらっしゃるかと思います。そんな経験豊富な開発者の方に、オートデスクが推進しているクラウド サービスに参加いただけたらうれしい限りです（いろいろと今後出てきますので）。Web プラットフォームから AutoCAD にチャレンジしていただければ、少しはハードルを下げられるのでは、という期待もあります。</p>
<p>といっても、いままで AutoCAD を開発プラットフォームとして利用してきていただいた方にも、AutoCAD JavaScript API を知っていただきたいので、まずは、JavaScript について簡単にご紹介しましょう。</p>
<p><strong>JavaScript とは?</strong>&#0160;</p>
<p>ここで JavaScript について詳細に説明することはできませんが、概要だけをお伝えしておきます。&#0160;まず、誤解を解くために、Wikipedia に記載されている <a href="http://ja.wikipedia.org/wiki/JavaScript" rel="noopener noreferrer" target="_blank">JavaScript</a> についてご一読いただくことをお勧めします。知っておいていただきたいのは、JavaScript は、言語仕様上、Oracle 社の Java とは無関係であるという点です。</p>
<p>そもそも JavaScript は、Web ブラウザで動作するインタプリタ言語です。AutoCAD で言えば、AutoLISP のようなイメージを持っていただくといいかも知れません。一番の特徴は HTML と共に動的な Web コンテンツ（Webページ）を作成できるという点です。このため、HTML のタグと共に利用する必要があります。次のコードは、&lt;script&gt; タグを使って、HTML ファイルの中に JavaScript の警告ダイアログを表示します。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p class="auto-style2" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; text-align: left; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly;"><span class="auto-style3" style="font-size: 10pt;">&lt;script language=”text/JavaScript”&gt;</span><br class="auto-style3" /><span class="auto-style3" style="font-size: 10pt;"> &#0160;&#0160; alert(“Hello World!”);<br />&lt;/script&gt;</span></p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p>実際には、HTML として &lt;HTML&gt; タグと &lt;BODY&gt; タグも必要になるので、次のような感じになります。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p class="auto-style1" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; text-align: left; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly;"><span lang="en-us" style="font-size: 10pt;">&lt;html&gt;<br />&lt;body&gt;<br />&#0160;&#0160;&#0160;&#0160; &lt;script type=&quot;text/javascript&quot;&gt;<br />&#0160;&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; alert(&quot;Hello World!&quot;);<br />&#0160;&#0160;&#0160;&#0160; &lt;/script&gt;<br />&lt;/body&gt;<br />&lt;/html&gt;</span></p>
</td>
</tr>
</tbody>
</table>
<p>JavaScript の中でも変数を多用します。この変数について、JavaScript では「弱い型付け」と呼ばれる特徴があります。C++、C#、VB.NET のように宣言時にあらかじめ型をしてせずに、値を代入しておくことができます。逆に言えば、変数に値を代入することで、その型が動的に決定されます。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p class="auto-style2" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; text-align: left; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly;"><span style="font-size: 10pt;">&lt;html&gt;</span><br /><span style="font-size: 10pt;">&lt;body&gt;</span><br /><span style="font-size: 10pt;">&lt;script language=”JavaScript”&gt;</span><br /><span style="font-size: 10pt;">&#0160;&#0160; &#0160; var pi=3.14159;</span><br /><span style="font-size: 10pt;">&#0160;&#0160; &#0160; var helloStr = “Welcome!”;</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160; var myVar1 = “Tuesday”;</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160; var myVar2 = 18;</span><br /><span style="font-size: 10pt;">&#0160;&#0160;&#0160;&#0160; var myVar3; // 値がないので型もない</span><br /><span style="font-size: 10pt;"> &lt;/script&gt;</span><br /><span style="font-size: 10pt;">&lt;/body&gt;</span><br /><span style="font-size: 10pt;">&lt;/html&gt;</span></p>
</td>
</tr>
</tbody>
</table>
<p>この点でも&#0160;AutoLISP の変数の扱いや VBA の Variant 型に似ていると感じるかもしれません。</p>
<p>さて、JavaScript でも配列を利用することができます。この場合、配列の各要素を指定するための添え字は、0 が基底となります。x[0]、x[1]、のような感じです。また、同様に、初期化にも対応します。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="MsoNormalTable" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p class="auto-style2" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; text-align: left; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly;"><span style="font-size: 10pt;">&lt;script language=”JavaScript”&gt;</span><br /><span style="font-size: 10pt;">&#0160;&#0160; &#0160; var myArray = new Array(5);</span><br /><span style="font-size: 10pt;">&#0160;&#0160; &#0160; myArray[0] = 0.1;</span><br /><span style="font-size: 10pt;">&#0160;&#0160; &#0160; myArray[1] = 0.2;</span><br /><span style="font-size: 10pt;">&#0160;&#0160; &#0160; myArray[2] = 0.3;</span><br /><span style="font-size: 10pt;">&#0160;&#0160; &#0160; myArray[3] = 0.4;</span><br /><span style="font-size: 10pt;">&#0160;&#0160; &#0160; myArray[4] = 0.5;</span><br /><span style="font-size: 10pt;">&lt;/script&gt;</span><br />&#0160;<br /><span style="font-size: 10pt;">&lt;script language=”JavaScript”&gt;</span><br /><span style="font-size: 10pt;">&#0160;&#0160; &#0160; var DOW = new Array(</span><br /><span style="font-size: 10pt;">&#0160;&quot;Monday&quot;, &quot;Tuesday&quot;, &quot;Wednesday&quot;, &quot;Thursday&quot;, &quot;Friday&quot;, &quot;Saturday&quot;, &quot;Sunday);</span><br /><span style="font-size: 10pt;">&lt;/script&gt;</span></p>
</td>
</tr>
</tbody>
</table>
<p>そして、JavaScript は C 言語のような関数を定義して、別の場所から呼び出すことが出来ます。次の例では、最初の&#0160;&lt;script&gt; タグ内で Hello 関数を定義して、別の &lt;script&gt; タグで Hello 関数を呼び出しています。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p class="auto-style2" style="mso-margin-top-alt: auto; mso-margin-bottom-alt: auto; text-align: left; mso-pagination: widow-orphan; layout-grid-mode: char; mso-element: frame; mso-element-frame-hspace: 7.1pt; mso-element-wrap: around; mso-element-anchor-horizontal: margin; mso-element-top: 26.3pt; mso-height-rule: exactly;"><span class="auto-style3" style="font-size: 10pt;">&lt;script language=&quot;JavaScript&quot;&gt;</span><br class="auto-style3" /><span class="auto-style3" style="font-size: 10pt;"> &#0160;&#0160;&#0160;&#0160; &lt;!--<br />&#0160;&#0160;&#0160;&#0160; function Hello() {<br /> &#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; alert(&quot;Hello there!&quot;);<br />&#0160;&#0160;&#0160;&#0160; }<br />&#0160;&#0160;&#0160;&#0160; // --&gt;<br /> &lt;/script&gt;<br />&#0160;<br />&lt;script language=&quot;JavaScript&quot;&gt;<br />&#0160;&#0160;&#0160;&#0160; &lt;!--<br />&#0160;&#0160;&#0160;&#0160; &#0160; Hello();<br />&#0160;&#0160;&#0160;&#0160; // --&gt;<br />&lt;/script&gt;</span></p>
</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<p><strong>AutoCAD JavaScript API の利用</strong></p>
<p>AutoCAD 2014 で登場した&#0160;JavaScript API は、AutoCAD JavaScript API の第一世代です。今後、数年をかけて実装を強化していきます。AutoCAD JavaScript API&#0160;は、HTML ファイル内から &lt;script&gt; タグを使って <strong><a href="http://www.autocadws.com/jsapi/v1/Autodesk.AutoCAD.js" rel="noopener noreferrer" target="_blank">http://www.autocadws.com/jsapi/v1/Autodesk.AutoCAD.js</a></strong>&#0160;から参照することができます。</p>
<table align="left" border="0" cellpadding="0" cellspacing="0" class="auto-style3" style="margin-left: 4.85pt; margin-right: 4.85pt; width: 100%; background: #f2f2f2;">
<tbody>
<tr style="mso-yfti-irow: 0; mso-yfti-firstrow: yes; mso-yfti-lastrow: yes;">
<td style="width: 435.1pt; padding: 0mm 5.4pt 0mm 5.4pt;" valign="top" width="580">
<p><span style="font-size: 10pt;">&lt;html&gt;</span><br /><span style="font-size: 10pt;">&lt;head&gt;</span><br /><span style="font-size: 10pt;">&lt;meta content=&quot;text/html; charset=utf-8&quot; http-equiv=&quot;Content-Type&quot;&gt;</span></p>
<p><span style="font-size: 10pt;">&lt;script</span><br /><span style="font-size: 10pt;">&#0160; type=“text/javascript”&#0160;&#0160; </span><br /><span style="font-size: 10pt;">&#0160; src=&quot;<a href="http://www.autocadws.com/jsapi/v1/Autodesk.AutoCAD.js">http://www.autocadws.com/jsapi/v1/Autodesk.AutoCAD.js</a>&quot;&gt;</span><br /><span style="font-size: 10pt;">&lt;/script&gt;</span><br /><span style="font-size: 10pt;">&lt;script type=&quot;text/javascript&quot;&gt;</span><br /><span style="font-size: 10pt;">&#0160; function setPromptSelectionOptions(options) {</span><span style="font-size: 10pt;"><span class="auto-style3"><br />：<br /></span>：</span></p>
</td>
</tr>
</tbody>
</table>
<p>現在、デベロッパガイドが残念ながら用意できていませんが、<strong><a href="http://www.autocadws.com/jsapi/v1/docs/index.html" rel="noopener noreferrer" target="_blank">http://www.autocadws.com/jsapi/v1/docs/index.html</a>&#0160;</strong>から英語のリファレンス マニュアルを参照することが出来るようになっています。</p>
<p>実際のプログラム作成では、ライブラリとして公開された Acad 名前空間の関数を呼び出し、その戻り値を参照しながら処理を進めていきます。このとき、各関数は AutoLISP のリスト式に相当するパッケージを使って、多数の情報を返します。JavaScript では、このパッケージに <strong><a href="http://ja.wikipedia.org/wiki/JavaScript_Object_Notation" rel="noopener noreferrer" target="_blank">JSON</a></strong> （JavaScript Object Notation）を利用します。</p>
<p>JSON は括弧で構造化された複数データの集合で、JavaScript の parse 関数で内容を抽出していくことができます。この部分を AutoLISP に当てはめるなら、(car)、(cdr)、(nth) といった要素の取り出し関数に似ています。</p>
<p><strong>開発エディタ</strong></p>
<p>AutoCAD 上で JavaScript API を作成するに際には、当然、エディタが必要になります。ただ、AutoCAD は JavaScript 用の専用エディタないし、統合開発環境（IDE）は用意していませんので、独自に用意していただく必要があります。&#0160;</p>
<p>特に推奨する開発エディタは存在しませんが、無償の開発環境が多数公開されていますので、適宜、お使いの Web ブラウザとの組み合わせも含めて、ダウンロードして評価してみることをお勧めします。下記は代表的な <a href="http://msdn.microsoft.com/ja-jp/library/ie/d1et7k7c(v=vs.94).aspx" rel="noopener noreferrer" target="_blank">JavaScript</a> の無償エディタです。</p>
<p style="padding-left: 30px;"><strong>Visual Studio Express 2012 for Web</strong><br /><a href="http://www.microsoft.com/visualstudio/jpn#products/visual-studio-express-for-web" rel="noopener noreferrer" target="_blank">http://www.microsoft.com/visualstudio/jpn#products/visual-studio-express-for-web</a></p>
<p style="padding-left: 30px;"><strong>NetBeans</strong><br /><a href="http://ja.netbeans.org/" rel="noopener noreferrer" target="_blank">http://ja.netbeans.org/</a></p>
<p style="padding-left: 30px;"><strong>Firebug</strong><br /><a href="https://addons.mozilla.jp/firefox/details/1843" rel="noopener noreferrer" target="_blank">https://addons.mozilla.jp/firefox/details/1843</a></p>
<p>次回は、具体的な AutoCAD JavaScript API のコードを使用して利用方法を見ていきます。&#0160;</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
