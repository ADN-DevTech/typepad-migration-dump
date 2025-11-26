---
layout: "post"
title: "Forge Viewer で指定可能なバージョン"
date: "2017-02-10 00:03:39"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/02/designatable-versions-on-forge-viewer.html "
typepad_basename: "designatable-versions-on-forge-viewer"
typepad_status: "Publish"
---

<p>Forge Viewer を利用するアプリの互換性を維持する目的で、先日、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/specifying-version-to-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer のバージョン指定</a></strong>&#0160;記事をご案内しました。ただし、どのバージョン番号が存在して、参照時に指定が可能なのか、判断出来ない場合があります。例えば、v2.7.51 というバージョン名が実際に存在して指定することが出来るのか、HTML 内で参照してみて、エラーとなるかどうかをチェックするのは面倒です。</p>
<p>このような場面では、デベロッパ ポータルに記載されていない &#0160;endpoint <strong>https://developer.api.autodesk.com/viewingservice/v1/viewers</strong>、メソッド <strong>GET</strong> で、実際に指定可能なバージョン名を <strong><a href="https://ja.wikipedia.org/wiki/JavaScript_Object_Notation" rel="noopener noreferrer" target="_blank">JSON</a></strong> 形式で取得することが出来ます。</p>
<pre style="padding-left: 30px;">[<br /> &quot;2.13&quot;,<br /> &quot;2.13.1&quot;,<br /> &quot;2.12.60&quot;,<br /> &quot;2.12&quot;,<br /> &quot;2.11.58&quot;,<br /> &quot;2.11.57&quot;,<br /> &quot;2.11.55&quot;,<br /> &quot;2.11&quot;,<br /> &quot;2.10.58&quot;,<br /> &quot;2.10.57&quot;,<br /> &quot;2.10.56&quot;,<br /> &quot;2.10.54&quot;,<br /> &quot;2.10.53&quot;,<br /> &quot;2.10.52&quot;,<br /> &quot;2.10.51&quot;,<br /> &quot;2.10.50&quot;,<br /> &quot;2.10&quot;,<br /> &quot;2.9.49&quot;,<br /> &quot;2.9.48&quot;,<br /> &quot;2.9&quot;,<br />...&lt;中略&gt;...<br /> &quot;0.1.65&quot;,<br /> &quot;0.1.63&quot;<br />]</pre>
<p>この endpoint は Header セクションに Access Token 指定を必要としないため、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/09/restful-api-and-testing-tools.html" rel="noopener noreferrer" target="_blank">Postman</a></strong> などの RESTful API テストツール以外に、JSON レスポンスが整形されない状態ではありますが、Web ブラウザの URL 指定でも JSON を確認することが出来るはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d25e8c35970c-pi" style="display: inline;"><img alt="Available_viewer_versions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d25e8c35970c image-full img-responsive" src="/assets/image_579176.jpg" title="Available_viewer_versions" /></a></p>
<p>まだリリースされていなバージョン番号も含まれますが、デベロッパ ポータルの&#0160;<a href="https://developer.autodesk.com/en/docs/viewer/v2/overview/changelog/" rel="noopener noreferrer" target="_blank">Recent Changes</a>&#0160;にある記載以外にも、細かなアップデートがあることが見て取れます。Forge 利用時の補足情報として開発時にお役立てください。</p>
<p>By Toshiaki Isezaki</p>
