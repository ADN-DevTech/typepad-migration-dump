---
layout: "post"
title: "Built-in Parameter Checker for Revit 2016"
date: "2015-07-10 01:17:36"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/07/built-in-parameter-checker-for-revit-2016.html "
typepad_basename: "built-in-parameter-checker-for-revit-2016"
typepad_status: "Publish"
---

<p>Revit &#0160;API には多数の組み込みパラメータがあり、Autodesk.Revit.Parameters.BuiltInParameter 列挙に定義されています。</p>
<p>要素から特定のパラメータを取得するにはパラメータ ID を使用して、以下のようなコードを記述するのが一般的です。ただし、その要素にどんなパラメータが関連づけられているのか、指定するパラメータ ID がわからなければなりません。これを調べるにはとても手間がかかります。</p>
<p>public Parameter FindWithBuiltinParameterID(Wall wall)<br />{<br /> &#0160; &#0160; //Use the WALL_BASE_OFFSET paramametId<br /> &#0160;&#0160;&#0160;&#0160;// to get the base offset parameter of the wall.<br /> &#0160;&#0160;&#0160;&#0160;BuiltInParameter paraIndex = BuiltInParameter.WALL_BASE_OFFSET;<br /> &#0160;&#0160;&#0160;&#0160;Parameter parameter = wall.get_Parameter(paraIndex);</p>
<p>&#0160;&#0160;&#0160;&#0160;return parameter;<br />}</p>
<p>そのため、正確な BuiltInParameter ID が分からない場合は、ParameterSet コレクションを繰り返してパラメータをすべて確認するという方法があります。しかし、これもデバッグ環境で実際に一つずつ確認しなければならないため、時間がかかります。</p>
<p>そこで、先日ご案内した&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2015/06/how-to-get-revitlookup.html" target="_blank">RevitLookup ツール</a>の開発者でもある弊社エンジニアが、UI 上で選択されている要素の BuiltInParameter&#0160;を一覧表示する Revit のアドインを作成して公開しております。</p>
<p><strong>Built-in Parameter Checker for Revit 2016&#0160;</strong><br /><a href="https://github.com/jeremytammik/BipChecker/releases/tag/2016.0.0.0" target="_blank">https://github.com/jeremytammik/BipChecker/releases/tag/2016.0.0.0</a></p>
<p>要素を選択して、アドインコマンドを実行すると、ファミリインスタンスもしくはファミリタイプの&#0160;BuiltInParameter を一覧できるダイアログが表示されます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb084fc388970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="BuiltInParameter for Revit 2016" class="asset  asset-image at-xid-6a0167607c2431970b01bb084fc388970d img-responsive" src="/assets/image_784568.jpg" title="BuiltInParameter for Revit 2016" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb085032e7970d-pi" style="display: inline;"><img alt="BuiltInParameter for Revit 2016_dialog1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb085032e7970d image-full img-responsive" src="/assets/image_703150.jpg" title="BuiltInParameter for Revit 2016_dialog1" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7ac1b73970b-pi" style="display: inline;"><img alt="BuiltInParameter for Revit 2016_dialog" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7ac1b73970b image-full img-responsive" src="/assets/image_961254.jpg" title="BuiltInParameter for Revit 2016_dialog" /></a></p>
<p>選択している要素にどんな&#0160;Built-in Parameter が関連づけられているのかを一覧できますので、RevitLookup ツールと併用して、ぜひカスタマイズ開発にご活用ください。</p>
<p>By Ryuji Ogasawara</p>
<p>&#0160;</p>
