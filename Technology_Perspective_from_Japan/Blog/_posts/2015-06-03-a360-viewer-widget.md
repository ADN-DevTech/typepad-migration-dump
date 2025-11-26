---
layout: "post"
title: "A360 ビューワー ウィジェット"
date: "2015-06-03 00:08:04"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/06/a360-viewer-widget.html "
typepad_basename: "a360-viewer-widget"
typepad_status: "Publish"
---

<p>
<script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"></script>
<script type="text/javascript" src="https://360.autodesk.com/Scripts/a360Viewer/widget.js"></script>
</p>
<p>以前、<a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank">ご紹介した </a><strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-capabilitie-a360-viewer.html" target="_blank">A360 ビューワー</a>&nbsp;</strong>に&nbsp;<strong><a href="http://ja.wikipedia.org/wiki/%E3%82%A6%E3%82%A3%E3%82%B8%E3%82%A7%E3%83%83%E3%83%88" target="_blank">ウィジェット</a>&nbsp;</strong>が登場しました。<strong><a href="https://360.autodesk.com/Viewer" target="_blank">https://360.autodesk.com/Viewer</a>&nbsp;</strong>を直接開かなくても、みなさんの Web ページに Drag &amp; Drop 領域を埋め込んで、A360 ビューワーを起動することが出来るようになります。 埋め込み方法は、<strong><a href="https://a360.autodesk.com/viewer/widget/" target="_blank">https://a360.autodesk.com/viewer/widget/</a></strong>&nbsp;に紹介されていますが、簡単にご案内しておきましょう。</p>
<p>ウィジェットは数行の JavaScript のコードとウィジェットの表示領域を HTML の &lt;div&gt; 要素で実現することが出来ます。まず、ウィジェットを埋め込みたい HTML ページに、次の 3&nbsp;行を追加して&nbsp;JavaScript を参照します。</p>
<p style="padding-left: 30px;">&lt;link href="https://360.autodesk.com/Content/css/a360Viewer/widget.css" rel="stylesheet" type="text/css"&gt;<br />&lt;script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"&gt;&lt;/script&gt;<br />&lt;script src="https://360.autodesk.com/Scripts/a360Viewer/widget.js" type="text/javascript"&gt;&lt;/script&gt;</p>
<p>次に埋め込んだウィジェットを表示領域を一意な id とともに指定します。</p>
<p style="padding-left: 30px;">&lt;div id="dropAreaContainer" style="width: 400px; height: 300px;"&lt;/div&gt;</p>
<p>style で要素幅を指定していますが、この指定は任意です。あとは、実施に処理を担う JavaScript を記述するのみです。</p>
<p style="padding-left: 30px;"><span style="color: #434343;">&lt;script type="text/javascript"&gt;</span><br /><span style="color: #434343;">var adskViewerWidget = adskViewerWidget();</span><br /><span style="color: #434343;">adskViewerWidget.Init('#dropAreaContainer', true);</span><br /><span style="color: #434343;">&lt;/script&gt;</span></p>
<p>この HTML 記述で下記のような領域が表示されるようになるはずです。</p>
<center>
<div id="dropAreaContainer" style="width: 400px; height: 300px;">&nbsp;</div>
</center>
<p>&nbsp;</p>
<p>表示には 2 通りがありますが、adskViewerWidget.Init() の第二引数を false にすることで、英語の説明文入りの完全なウィジェットを表示することが出来ます。</p>
<center>
<div id="widgetContainer" style="width: 400px; height: 300px;">&nbsp;</div>
</center>
<script type="text/javascript">// <![CDATA[
// &lt;![CDATA[
// &amp;lt;![CDATA[
var adskViewerWidget = new adskViewerWidget();
adskViewerWidget.Init('#dropAreaContainer',true);
adskViewerWidget.Init('#widgetContainer',false);
// ]]&amp;gt;
// ]]&gt;
// ]]></script>
<p>&nbsp;</p>
<p>あとは、&nbsp;Drag &amp; Drop 領域に CAD データ ファイルをドロップするだけです。ぜひお試しください。</p>
<div style="width: 400px;">By Toshiaki Isezaki</div>
