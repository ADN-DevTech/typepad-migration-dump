---
layout: "post"
title: "Firefox で A360 Viewer が利用できない"
date: "2016-01-20 00:29:13"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/01/unable-to-use-a360-viewer-on-firefox.html "
typepad_basename: "unable-to-use-a360-viewer-on-firefox"
typepad_status: "Publish"
---

<p>A360 を使ってプロジェクト内のデザイン ファイルや、無償の&#0160;<strong>A360 Viewer</strong>(<strong><a href="https://a360.autodesk.com/viewer" target="_blank">https://a360.autodesk.com/viewer</a></strong>) を使ってデザイン データを表示させる際に、次のようなエラーが出てデータの表示が出来ない場合があります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c80927e3970b-pi" style="display: inline;"><img alt="Webgl_disabled" class="asset  asset-image at-xid-6a0167607c2431970b01b7c80927e3970b img-responsive" src="/assets/image_197072.jpg" title="Webgl_disabled" /></a><br />A360 Viewer の利用には <strong><a href="https://ja.wikipedia.org/wiki/WebGL" target="_blank">WebGL</a></strong> をサポートする Web ブラウザが必要です。このエラーが出てしまう原因には、大きく次の 2 つを挙げることが出来ます。</p>
<ul>
<li>お使いの Web ブラウザ/バージョンが WebGL をサポートしていない。</li>
<li>お使いの Web ブラウザ/バージョンは WebGL をサポートしているが WebGL 設定が無効になっている。</li>
</ul>
<p>まずは、問題の切り分けのために、お使いの Web ブラウザが WebGL に対応しているか否かを調べてみましょう。チェック方法はいたって簡単です。<span style="font-size: 12pt;"><strong><a href="http://get.webgl.org/" target="_blank">http://get.webgl.org/</a></strong></span>&#0160;にアクセスしてみてください。もし、次のように表示されたら、残念ながら、お使いの Web ブラウザ/バージョンが WeGL をサポートいないことになります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1932a0a970c-pi" style="display: inline;"><img alt="Not_support_webgl" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1932a0a970c image-full img-responsive" src="/assets/image_872132.jpg" title="Not_support_webgl" /></a></p>
<p>この場合には、Google Chrome や Mozilla Firefox、Microsoft Edge 各最新バージョンなど、WebGL をサポートする Web ブラウザをお試しください。もし、次のように表示されたら、お使いの Web ブラウザ/バージョンが WebGL をサポートしています。なお、下図にある立方体は、アニメーション化されているので回転するはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ac79df970c-pi" style="display: inline;"><img alt="WebGL_enabled" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ac79df970c img-responsive" src="/assets/image_334553.jpg" title="WebGL_enabled" /></a></p>
<p>お使いの Web ブラウザ/バージョンが WebGL をサポートしているにもかかわらず、A360 Viewer が利用できない場合には、WebGL 設定が無効化されていことになります。WebGL 設定は、強制的に有効化することも出来ますので、設定変更をお試しいただくことも出来ます。</p>
<p>Mozilla Firefox をお使いの場合には、次の手順で WebGL 設定を強制的に有効化することが出来ます。なお、この設定変更は自由ですが、設定変更後に Web ブラウザの動作が不安定になる可能性もあります。設定変更は、自己責任で行うことが前提となりますのでご注意ください。</p>
<ol>
<li>通常、URL を入力するアドレスバーに <strong>about:config</strong>&#0160;と入力して設定画面を開きます。この際に、警告メッセージが表示されます。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1932d49970c-pi" style="display: inline;"><img alt="Warning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1932d49970c image-full img-responsive" src="/assets/image_704443.jpg" title="Warning" /></a><br /><br /></li>
<li>画面上部にある検索ボックスに <strong>webgl</strong>&#0160;と入力して、WebGL 関連設定のみを表示させます。</li>
<li><strong>webgl.force-enabled</strong> 設定を見つけて、右クリックメニューの [切り替え] メニューかダブルクリックで、値を <strong>true</strong> に変更します。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1932b60970c-pi" style="display: inline;"><img alt="Firefox_webgl_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1932b60970c image-full img-responsive" src="/assets/image_292188.jpg" title="Firefox_webgl_settings" /></a></li>
<li>起動中の Firefox セッションをすべて終了して、再起動後に A360 Viewer を再度使用してみてください。</li>
</ol>
<p>WebGL 設定が無効化されてしまう原因には、コンピュータに搭載されているグラフィックス カードを使ったハードウェア アクセラレーションに問題が報告されているなど、さまざまな要因が考えられます。詳細は、Mozilla support ページにある Knowledge Base&#0160;<strong><a href="https://support.mozilla.org/ja/kb/upgrade-graphics-drivers-use-hardware-acceleration" target="_blank">グラフィックドライバを更新してハードウェアアクセラレーション機能と WebGL を使用する</a></strong>&#0160;を参照してみてください。</p>
<p>By Toshiaki Isezaki</p>
