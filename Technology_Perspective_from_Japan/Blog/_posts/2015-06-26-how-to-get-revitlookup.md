---
layout: "post"
title: "RevitLookup ツール for Revit 2014-2016 の入手方法と機能について"
date: "2015-06-26 01:18:32"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/06/how-to-get-revitlookup.html "
typepad_basename: "how-to-get-revitlookup"
typepad_status: "Publish"
---

<p>RevitLookup ツールは、Revit の UI 上から、アプリケーション、ドキュメント、ファミリ、ジオメトリ等の、各オブジェクトのデータベース構造を調査するアドインです。<br /><br />Revit API を利用してカスタマイズを行う開発者向けツールとして、弊社エンジニアが開発・配布しております。<br />以前は Revit SDK に同梱されておりましたが、現在は GitHub にて、VisualStudio のプロジェクトファイルが公開されており、必要に応じてダウンロードしてご利用いただけます。</p>
<p>それぞれ、Revit 2014 / 2015 / 2016 に対応するアドインの VisualStudio プロジェクトファイルが、以下の GitHub にて公開されております。</p>
<p><strong>RevitLookup トップページ</strong><br /><a href="https://github.com/jeremytammik/RevitLookup" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup</a></p>
<p><strong>リリース一覧</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup/releases</a></p>
<p><strong>Revit 2014 対応版</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2014.0.1.0" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup/releases/tag/2014.0.1.0</a></p>
<p><strong>Revit 2015 対応版</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2015.0.0.8" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup/releases/tag/2015.0.0.8<br /></a></p>
<p><strong>Revit 2016 対応版 最新リリース</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2016.0.0.9" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup/releases/tag/2016.0.0.9</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7a4e481970b-pi" style="display: inline;"><img alt="RevitLookup" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7a4e481970b img-responsive" src="/assets/image_486838.jpg" title="RevitLookup" /></a></p>
<p>RevitLookup をご利用いただく際は、Visual Studio プロジェクトファイルのダウンロード後に、ビルドを実行し、DLLおよびアドイン・マニフェストファイルを アドイン フォルダにデプロイしてください。</p>
<div>ビルドを実行する際には、参照設定にて指定されている RevitAPI.dll および RevitAPIUI.dll をご自身の Revit アプリケーションのインストールフォルダに配置されているものに設定し直してください。</div>
<div>&#0160;</div>
<div style="padding-left: 30px;">C:\Users\ユーザー名\AppData\Roaming\Autodesk\Revit\Addins\201X</div>
<div>&#0160;</div>
<div>Revit 2015/2016 を起動後に、アドインタブに RevitLookup ツールが表示されます。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b41d38970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RevitLookup1" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b41d38970d img-responsive" src="/assets/image_819813.jpg" title="RevitLookup1" /></a></div>
<p>&#0160;</p>
<div>Snoop DB では、 Revit DB 要素をトップレベル階層から確認することができます。</div>
<div>Snoop Current Selection では、 ActiveDocument 上で選択している要素の DB 構造を確認することができます。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08b41d3d970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RevitLookup2" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b41d3d970d img-responsive" src="/assets/image_405660.jpg" title="RevitLookup2" /></a></div>
<div>&#0160;</div>
<div>ぜひ、ご活用ください。</div>
<p>By Ryuji Ogasawara</p>
