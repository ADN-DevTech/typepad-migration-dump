---
layout: "post"
title: "APS Viewer：以前の内容が表示されてしまう"
date: "2023-10-25 00:09:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/10/aps-viewer-shown-not-updated.html "
typepad_basename: "aps-viewer-shown-not-updated"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39cd741200c-pi" style="display: inline;"><img alt="Triton" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39cd741200c image-full img-responsive" src="/assets/image_562472.jpg" title="Triton" /></a></p>
<p>過去に開催した Autodesk University や Workshop 等で Viewer とブラウザ キャッシュについて触れていますが、ブログ記事としてご案内したことがなかったようです。今回は、改めて Viewer に起こりがちなブラウザ キャッシュにまつわる問題と回避策をご紹介したいと思います。</p>
<p>まず、APS Viewer に 2D 図面/シートや 3D モデルのコンテンツを表示する処理を振り返って見ましょう。</p>
<p>デザイン ファイルの内容を APS Viewer に表示するには、通常、シード（種）ファイルとも呼ばれるデザイン ファイルを OSS Bucket にアップロード後、Model Derivative API の <a class="reference external" href="https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/en/docs/model-derivative/v2/reference/http/job-POST" rel="noopener" target="_blank">POST job</a> エンドポイントで SVF ないし SVF2 に変換、Bucket＋ファイル名を Base64 エンコードした URN を識別子（Document Id）として Viewer の <a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/Document/#load-documentid-onsuccesscallback-onerrorcallback-options" rel="noopener" target="_blank">load</a> メソッドに渡します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a1462e200d-pi" style="display: inline;"><img alt="Viewer_process" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a1462e200d image-full img-responsive" src="/assets/image_195158.jpg" title="Viewer_process" /></a></p>
<p>この際、表示されたコンテンツが期待した内容にならないケースが存在します。</p>
<p><strong>問題</strong></p>
<p style="padding-left: 40px;">毎回、アップロード、変換するのが異なる Bucket 名やファイル名である場合、特に表示上の問題は発生しないはずです。</p>
<p style="padding-left: 40px;"><a href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/viewer_basics/starting-html/" rel="noopener" target="_blank">Viewer 処理の手順</a>で問題になるのは、<span style="text-decoration: underline;">同じ名前の Bucket に、内容を更新したデザイン ファイルを同じ名前でアップロード、再変換、表示した場合</span>です。この場合、デザイン ファイル内のコンテンツを更新しているにもかかわらず、Viewer に以前の状態のコンテンツが表示されてしまう問題が潜在します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39ce870200c-pi" style="display: inline;"><img alt="Phenomenon" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39ce870200c image-full img-responsive" src="/assets/image_240185.jpg" title="Phenomenon" /></a></p>
<p><strong>考察</strong></p>
<p style="padding-left: 40px;">このような際、考えられるのは次の点になります。</p>
<ol>
<li><a class="reference external" href="https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/en/docs/model-derivative/v2/reference/http/job-POST" rel="noopener" target="_blank">POST job</a> エンドポイントを呼び出して SVF/SVF2 変換する場合、以前の変換時に生成されたマニフェストが残っていると再変換がおこなわれません。その結果、サーバー側で SVF/SVF2 自体が更新されない結果になります。</li>
<li>クライアント側で以前のコンテンツがブラウザにャッシュされているため、前回のキャッシュ内容が表示される結果になります。</li>
</ol>
<p><strong>対策</strong></p>
<ol>
<li>上記「考察 1.」 の対策として、確実な再変換を実行するには２通りの方法があります。<br />１つめは POST job エンドポイントの呼び出し前に、<a class="reference external" href="https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/en/docs/model-derivative/v2/reference/http/urn-manifest-DELETE" rel="noopener" target="_blank">DELETE {urn}/manifest</a>&#0160;エンドポイントで以前の変換時に生成されたマニフェストを削除する方法です。マニフェストが存在するか否かは、<a class="reference external" href="https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/en/docs/model-derivative/v2/reference/http/urn-manifest-GET" rel="noopener" target="_blank">GET {urn}/manifest</a> エンドポイントでチェックすることが出来ます。<br />２つめはリクエスト時に&#0160;&#0160;<a href="https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/#headers" rel="noopener" target="_blank">x-ads-force ヘッダーパラメータ</a>を true に設定して、POST job エンドポイントを呼び出す方法です。このパラメータで、マニフェストの有無にかかわらず、強制的に変換を実行させることが出来ます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a0f35a200b-pi" style="display: inline;"><img alt="X-ads-force" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a0f35a200b image-full img-responsive" src="/assets/image_416482.jpg" title="X-ads-force" /></a></li>
<li>「考察 2.」の対策として、変換処理前にブラウザ キャッシュをクリアしておく方法です。お使いの Web ブラウザによってキャッシュのクリアの手順/ UI が異なるので、個々の方法には触れませんが、多くに場合、1. の対策に加えて、この対策を実行することで期待したコンテンツを Viewer 表示させることが出来るはずです。</li>
</ol>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a15519200d-pi" style="display: inline;"><img alt="Expected" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a15519200d image-full img-responsive" src="/assets/image_452521.jpg" title="Expected" /></a></p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a0ef5b200b-pi" style="display: inline;"><img alt="Problem" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a0ef5b200b image-full img-responsive" src="/assets/image_680391.jpg" title="Problem" /></a></p>
<p style="padding-left: 40px;">ただ、開発時のデバッグ作業なら許容できても、当然ながら、実際の運用で毎回ブラウザ キャッシュを削除するのは受け入れられないのも事実です。</p>
<p style="padding-left: 40px;">このような場合、クライアント実装で URN を使って表示コンテンツをフェッチする際、<span style="font-size: 10pt;"><a href="https://developer.mozilla.org/ja/docs/Web/HTTP/Headers/If-Modified-Since" rel="noopener" target="_blank"><strong>If-Modified-Since </strong>リクエストヘッダー</a>に過去の日時を指定することで、常に新しいコンテンツをロード・表示する方法があります。</span></p>
<p style="padding-left: 40px;"><span style="font-size: 11pt;">具体的なクライアント実装は次のようなものになります。</span></p>
<div>
<blockquote>
<div><span style="font-size: 10pt;">...</span></div>
<div><span style="font-size: 10pt;">var options = {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; env: &#39;AutodeskProduction2&#39;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; api: &#39;streamingV2&#39;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; getAccessToken: getCredentials</span></div>
<div><span style="font-size: 10pt;">};</span></div>
<div>&#0160;</div>
<div><span style="font-size: 10pt;">Autodesk.Viewing.Initializer(options, function () {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; _viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;viewer3d&#39;));</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; var startedCode = _viewer.start();</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; if (startedCode &gt; 0) {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; console.error(&#39;Failed to create a Viewer: WebGL not supported.&#39;);</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; return;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; <strong>Autodesk.Viewing.endpoint.HTTP_REQUEST_HEADERS[&#39;If-Modified-Since&#39;] = &#39;Sat, 29 Oct 1994 19:43:31 GMT&#39;;</strong></span></div>
<div>&#0160;</div>
<div><span style="font-size: 10pt;">&#0160; &#0160; // Load viewable</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; var documentId = &#39;urn:&#39; + urn;</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; Autodesk.Viewing.Document.load(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);</span></div>
<div><span style="font-size: 10pt;">});</span></div>
<div><span style="font-size: 10pt;">...</span></div>
</blockquote>
</div>
<p style="padding-left: 40px;"><span style="font-size: 11pt;">この方法では、実際にキャッシュをクリアするのではなく、更新されたコンテンツをサーバーから強制的に再読み込みさせる効果を得ることが出来ます。よりスマートで実用的な方法と言えます。</span></p>
<p style="padding-left: 40px;"><span style="font-size: 11pt;">同じデザイン ファイル名に由来する URN を頻繁に更新・表示する必要のある（Design Automation API と併用するような）コンフィギュレーターでは有効な対策です。</span></p>
<p>By Toshiaki Isezaki</p>
