---
layout: "post"
title: "Forge Viewer：v7 コード"
date: "2019-10-23 00:26:24"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/10/forge-viewer-v7-code.html "
typepad_basename: "forge-viewer-v7-code"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4e1d3a6200b-pi" style="float: right;"><img alt="Viewer-api-blue" class="asset  asset-image at-xid-6a0167607c2431970b0240a4e1d3a6200b img-responsive" src="/assets/image_831350.jpg" style="margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a>Forge Viewer JavaScript ライブラリの新バージョンである v7 が <strong><a href="https://ja.wikipedia.org/wiki/%E3%82%B3%E3%83%B3%E3%83%86%E3%83%B3%E3%83%84%E3%83%87%E3%83%AA%E3%83%90%E3%83%AA%E3%83%8D%E3%83%83%E3%83%88%E3%83%AF%E3%83%BC%E3%82%AF" rel="noopener" target="_blank">CDN</a></strong> から配信され始めてて、約 2 か月が過ぎました。v7 ライブラリは、それまでの JavaScript コードと互換性がないため、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/another-way-to-specify-forge-viewer-version.html" rel="noopener" target="_blank">バージョン指定</a></strong>がない Forge アプリの中には、この配信開始とともに期待した動作をしなくなったものがあったかもしれません。</p>
<p>v7 コードの紹介は、Forge ポータルの公式ドキュメントでも<a href="https://forge.autodesk.com/en/docs/viewer/v7/developers_guide/viewer_basics/" rel="noopener" target="_blank"><strong>紹介</strong></a>されていますが、ここでは改めて、Forge Viewer の基本的なスケルトン コード例をご案内しておきたいと思います。</p>
<hr />
<p><strong>HTML コード</strong></p>
<p>Forge Viewer がストリーミング配信で 2D 図面や 3D モデルを表示するカンバス領域は、HTML の &lt;div&gt; タグで指定した領域です。この部分は v6 以前のバージョンと変わっていません。また、カンバス領域となる &lt;div&gt;～&lt;/div&gt; 領域には、JavaScript コードでビューア領域を指定する目的で、id を定義しておかなければなりません。</p>
<p>Forge Viewer のライブラリとスタイルシートの参照とともに、最も簡単な HTML 定義は次のようになります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"><span style="font-size: 12pt;">&lt;html&gt;</span><br /><span style="font-size: 12pt;">&lt;head&gt;</span><br /><span style="font-size: 12pt;">    &lt;title&gt;Standard Forge Viewer&lt;/title&gt;</span><br /><span style="font-size: 12pt;">    &lt;meta charset=&quot;utf-8&quot;&gt;</span><br /><span style="font-size: 12pt;">    &lt;link rel=&quot;stylesheet&quot; href=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/style.min.css&quot; type=&quot;text/css&quot;&gt;</span><br /><span style="font-size: 12pt;">    &lt;script src=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/viewer3D.min.js&quot; type=&quot;text/javascript&quot;&gt;&lt;/script&gt;</span><br /><span style="font-size: 12pt;">    &lt;script src=&quot;/index.js&quot; type=&quot;text/javascript&quot;&gt;&lt;/script&gt;</span><br /><span style="font-size: 12pt;">&lt;/head&gt;</span><br /><span style="font-size: 12pt;">&lt;body onload=&quot;initializeViewer()&quot;&gt;</span><br /><span style="font-size: 12pt;">    &lt;div <strong>id=&quot;viewer3d</strong>&quot;&gt;&lt;/div&gt;</span><br /><span style="font-size: 12pt;">&lt;/body&gt;</span><br /><span style="font-size: 12pt;">&lt;/html&gt;</span></code></pre>
<hr />
<p><strong>JavaScript コード</strong></p>
<p>上記 HTML が参照する index.js によってビューア処理が実装されることになります。もちろん、あらかじめ OAuth API による Access Token と、表示させるドキュメント（本例では 3D モデル）が Model Derivative API によって変換されていて、base64 エンコードした URN が用意されている必要があります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"><span style="font-size: 12pt;">var _viewer = null;

function initializeViewer() {

    var options = {
        env: &#39;AutodeskProduction&#39;,
        api: &#39;derivativeV2&#39;,  // for models uploaded to EMEA change this option to &#39;derivativeV2_EU&#39;
        language: &#39;ja&#39;,
        getAccessToken: getCredentials
    };

    Autodesk.Viewing.Initializer(options, function () {

        _viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;<strong>viewer3d</strong>&#39;));

        var startedCode = _viewer.start();
        if (startedCode &gt; 0) {
            console.error(&#39;Failed to create a Viewer: WebGL not supported.&#39;);
            return;
        } &#0160; &#0160; &#0160; &#0160; &#0160;console.log(&#39;Initialization complete, loading a model next...&#39;);

        var documentId = &#39;urn:&#39; + <strong>&lt;your base64 encoded urn&gt;</strong>;
        Autodesk.Viewing.Document.load(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);

    });

    function onDocumentLoadSuccess(viewerDocument) {

        var viewables = viewerDocument.getRoot().search({
            &#39;role&#39;: &#39;3d&#39;
        });

        _viewer.loadDocumentNode(viewerDocument, viewables[0]).then(i =&gt; {
        });

    }

    function onDocumentLoadFailure() {
        console.error(&#39;Failed fetching Forge manifest&#39;);
    }

}

function getCredentials(callback) {
    fetch(&#39;/api/credentials&#39;).then(res =&gt; {
        res.json().then(data =&gt; {
            callback(data.access_token, data.expires_in);
        });
    });
}</span></code></pre>
<hr />
<p>利用している Forge Viewer のバージョンは、グローバル変数 LMV_VIEWER_VERSION から参照することが出来るほか、設定パネルの右下に控えめに表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4bd2d6b200d-pi" style="display: inline;"><img alt="Viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4bd2d6b200d image-full img-responsive" src="/assets/image_24050.jpg" title="Viewer" /></a></p>
<p>By Toshiaki Isezaki</p>
