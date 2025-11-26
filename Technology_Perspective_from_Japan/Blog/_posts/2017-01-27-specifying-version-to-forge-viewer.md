---
layout: "post"
title: "Forge Viewer のバージョン指定"
date: "2017-01-27 00:01:32"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/01/specifying-version-to-forge-viewer.html "
typepad_basename: "specifying-version-to-forge-viewer"
typepad_status: "Publish"
---

<p>Forge が提供する各種 API は、クラウド サービスと同様に、定期的に更新されています。ただし、Authentication API や Model Derivative API、Data Management API など、RESTful API でクラウドとコミュニケーションをするようなタイプの API は、互換性を崩すような大きな変更や更新が頻繁にあるわけではありません。むしろ、既存アプリの互換性を維持するために、endpoint などを変更してしまうことは稀と言えます。</p>
<p>実際のところ、2016年6月の正式リリース以降、それらの API には互換性を崩すような変更は加えられていません。もし、今後、大きな変更がある場合でも、変更前の endpoint はそのまま維持して、新バージョン用の endpoint を追加するなど、新旧バージョンが安全に稼動する状況を維持するはずです。</p>
<p>このような考え方は、正式には &quot;API&quot; と呼んでいない Forge Viewer にも当てはまります。Viewer が &quot;API&quot; &#0160;と呼ばれない理由については、過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/forge-api-glossary.html" rel="noopener noreferrer" target="_blank">Forge 用語について</a></strong> をご参照ください。重要なのは、最も頻繁に更新され、既存アプリに互換性の問題が生じがちなのが、エンドユーザのフロント インタフェースと なる Viewer が提供する JavaScript ライブラリであるという点です。Viewer JavaScript ライブラリの更新履歴は、デベロッパ ポータルの <strong><a href="https://developer.autodesk.com/en/docs/viewer/v2/overview/" rel="noopener noreferrer" target="_blank">Viewer</a></strong>&#0160;ページにある <strong><a href="https://developer.autodesk.com/en/docs/viewer/v2/overview/changelog/" rel="noopener noreferrer" target="_blank">Recent Changes</a></strong>（最近の変更）リンクから参照することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8ccd957970b-pi" style="display: inline;"><img alt="Recent_changes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8ccd957970b image-full img-responsive" src="/assets/image_483623.jpg" title="Recent_changes" /></a></p>
<p>Recent Changes では、現在の Viewer 最新バージョン は 2.11 になっていますが、ある理由から、実際に配信されているのは 2.10 というバージョンです。使用する Forge Viewer JavaScript ライブラリのバージョンは、Forge Viewer の表示領域（div タグ セクション）を持つ HTML ファイル内で指定することができます。具体的には、Forge Viewer の JavaScript ライブラリと付随する Cascating Style Sheet（CSS）参照でバージョンを指定することが可能です。もし、この部分でバージョン指定をおこなわないと、常に最新バージョンが参照されることになります。2016年1月現在では、2.10 バージョンです。</p>
<pre>&lt;html&gt;<br />&lt;head&gt;<br /> &lt;title&gt;ADN Viewer Demo&lt;/title&gt;<br /> &lt;link rel=&quot;shortcut icon&quot; href=&quot;/images/favicon.ico&quot; type=&quot;image/x-icon&quot; /&gt;<br /><strong> &lt;link type=&quot;text/css&quot; rel=&quot;stylesheet&quot; href=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css&quot;/&gt;</strong><br /><strong> &lt;script src=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js&quot;&gt;&lt;/script&gt;</strong><br />&lt;/head&gt;<br /><br />&lt;body style=&quot;margin:0&quot;&gt;<br /> &lt;div id=&quot;viewerDiv&quot;&gt;&lt;/div&gt;<br />&lt;/body&gt;<br />&lt;/html&gt;</pre>
<p>上記の場合、バージョン 2.10 が使用されます。分かりやすく設定画面を表示させると、2.10 が持つ設定パネルを表示します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8ccdb4f970b-pi" style="display: inline;"><img alt="2.10_version" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8ccdb4f970b image-full img-responsive" src="/assets/image_911665.jpg" title="2.10_version" /></a></p>
<p>&#0160;一方、明示的なバージョンを <strong>?v=v</strong> の後に指定することも出来ます。例えば、2.7.44 バージョンを指定する場合には、次のように記入します。&#0160;</p>
<pre>&lt;html&gt;<br />&lt;head&gt;<br /> &lt;title&gt;ADN Viewer Demo&lt;/title&gt;<br /> &lt;link rel=&quot;shortcut icon&quot; href=&quot;/images/favicon.ico&quot; type=&quot;image/x-icon&quot; /&gt;<br /><strong> &lt;link type=&quot;text/css&quot; rel=&quot;stylesheet&quot; href=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css<span style="color: #0000ff;">?v=v2.7.44</span>&quot;/&gt;</strong><br /><strong> &lt;script src=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js<span style="color: #0000ff;">?v=v2.7.44</span>&quot;&gt;&lt;/script&gt;</strong><br />&lt;/head&gt;<br /><br />&lt;body style=&quot;margin:0&quot;&gt;<br /> &lt;div id=&quot;viewerDiv&quot;&gt;&lt;/div&gt;<br />&lt;/body&gt;<br />&lt;/html&gt;</pre>
<p>この内容で Forge Viewer を表示させると、バージョン 2.7.44 の JavaScript ライブラリと CSS を使用した Viewer に指定したコンテンツを表示することが出来ます。こちらの場合も、分かりやすく設定画面を表示させると、2.7.44 が持つ設定パネルを表示します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb097004f1970d-pi" style="display: inline;"><img alt="2.7.44_version" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb097004f1970d image-full img-responsive" src="/assets/image_511808.jpg" title="2.7.44_version" /></a></p>
<p>バージョン指定をおこなう場合は、細かい末尾の指定はアスタリスク * でカバーさせることも可能です。前述の&#0160;2.7.&#0160;44 の部分を 2.7.* とすると、2.7.44 ではなく、2.7 バージョン シリーズ上の最新のリビジョンを参照することになります。</p>
<p>このように、Forge Viewer を利用する場合には、動作を確認したバージョンを指定して動作させることが出来ます。この方法を利用することで、ある日、関数仕様の変更などで互換性を崩す新バージョンの Forge Viewer JavaScript ライブラリがリリースされた場合でも、動作を継続させることが出来るようになります。</p>
<p>By Toshiaki Isezaki</p>
