---
layout: "post"
title: "3ds Max 2015 デバッグシンボル公開のお知らせ"
date: "2014-05-08 18:07:07"
author: "Akira Kudo"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/05/3dsmax2015debugsymbol.html "
typepad_basename: "3dsmax2015debugsymbol"
typepad_status: "Publish"
---

<p>Autodesk Developer Networkの工藤　暁です。今回はAutodesk® 3ds Max 2015デバッグシンボルが公開されましたのでご紹介します。</p>
<p>２０１５のリリースに関しOpenSSLのセキュリティ問題等を修正する為のサービスパックが既にリリースされております。</p>
<p>SP1は此方より取得頂けます。<a href="http://knowledge.autodesk.com/support/3ds-max/downloads/caas/downloads/content/autodesk-3ds-max-2015-service-pack-1-and-security-fix.html">Autodesk 3ds Max 2015 Service Pack 1 and Security Fix</a> そしてデザイン用は此方です。 <a href="http://knowledge.autodesk.com/support/3ds-max/downloads/caas/downloads/content/autodesk-3ds-max-design-2015-service-pack-1-and-security-fix.html">Autodesk 3ds Max Design 2015 Service Pack 1 and Security Fix</a></p>
<p>さてデバッグシンボルですがFCS及びSP1両方の環境をご用意しておりますが、お客様には常に最新の環境にてご利用頂く事をお勧めしております。</p>
<p>&#0160;一般的にデバッグシンボルは以下の情報を保持しています。</p>
<ul>
<li>パブリックシンボル(関数、スタティック、グローバル変数)</li>
<li>コードに関連するオブジェクト・ファイルのリスト</li>
<li>スタックトレースの際に使用されるFrame Ｐointer Ｏptimization(FPO)の情報</li>
<li>ローカル変数とデータ構造用の名前と型情報</li>
<li>ソースファイルと行番号の情報</li>
</ul>
<p>&#0160;</p>
<p>3ds Maxのデバッグシンボルはシンボルサーバーにて公開されています。<a href="http://symbols.autodesk.com/symbols">http://symbols.autodesk.com/symbols</a></p>
<p>又VisualStudioのサーバー設定方法等は&#0160;Christopher Digginsのブログにて紹介されています。<a href="http://area.autodesk.com/blogs/chris/debug_symbol_server_for_3ds_max_2012">http://area.autodesk.com/blogs/chris/debug_symbol_server_for_3ds_max_2012</a></p>
<p>&#0160;</p>
<p>是非お試し下さい。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511b4361c970c-pi" style="display: inline;"><img alt="Capture" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511b4361c970c img-responsive" src="/assets/image_466380.jpg" title="Capture" /></a></p>
