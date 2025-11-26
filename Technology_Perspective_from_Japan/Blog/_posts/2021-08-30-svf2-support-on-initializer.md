---
layout: "post"
title: "Forge Viewer：Viewer 初期化時の SVF2 対応について"
date: "2021-08-30 03:23:54"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/08/svf2-support-on-initializer.html "
typepad_basename: "svf2-support-on-initializer"
typepad_status: "Publish"
---

<p>2021 年 7 月の <strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/07/start-svf2-operation.html" rel="noopener" target="_blank">SVF2 正式サポート</a></strong>に際して、JavaScript ライブラリ バージョン 7.28 以降、SVF2 を利用するアプリは、Forge Viewer の初期化オプションで指定する値について、開発者ツールのコンソールに警告メッセージが表示されるようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e11b2bdc200b-pi" style="display: inline;"><img alt="Warning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e11b2bdc200b image-full img-responsive" src="/assets/image_821679.jpg" title="Warning" /></a></p>
<p>この警告が表示されている場合には、従来の env 値&#0160; MD20ProdUS（または MD20ProdEU）、 api 値 D3S に代わって、env 値 <strong>AutodeskProduction2</strong>（または&#0160;<strong data-stringify-type="bold">AutodeskStaging2&#0160;</strong>/&#0160;<strong data-stringify-type="bold">AutodeskProduction2</strong>&#0160;）、api 値&#0160;<strong>streamingV2</strong>（または&#0160;<strong>streamingV2_EU</strong>）への置き換えが推奨されています。従来値は将来廃止される予定ですので、お早めに上記置き換えをお願いします。（現状、具体的な廃止時期は未定です。）</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    // Initialize Viewer
    var options = {
<strong>        env: &#39;<span style="background-color: #ffff00;">AutodeskProduction2</span>&#39;,
        api: &#39;<span style="background-color: #ffff00;">streamingV2</span>&#39;,
</strong>        language: &#39;ja&#39;,
        getAccessToken: getCredentials
    };

    Autodesk.Viewing.Initializer(options, function () {

        _viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;viewer3d-1&#39;));
        var startedCode = _viewer.start();
        if (startedCode &gt; 0) {
            console.error(&#39;Failed to create a 3D Viewer: WebGL not supported.&#39;);
            return;
        }

        // Load viewable
        var documentId = &#39;urn:&#39; + urn_svf;
        Autodesk.Viewing.Document.load(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);

    });

</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>By Toshiaki Isezaki</p>
