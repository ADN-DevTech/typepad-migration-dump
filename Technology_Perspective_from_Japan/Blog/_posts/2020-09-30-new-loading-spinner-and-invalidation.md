---
layout: "post"
title: "新しいローディングスピナーと無効化"
date: "2020-09-30 00:39:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/09/new-loading-spinner-and-invalidation.html "
typepad_basename: "new-loading-spinner-and-invalidation"
typepad_status: "Draft"
---

<p><a href="https://www.facebook.com/adn.open.japan/posts/2755018348105431" rel="noopener" target="_blank">Facebook</a> でもご案内のとおり、先日リリースされた Forge Viewer バージョン <span class="d2edcug0 hpfvmrgz qv66sw1b c1et5uql rrkovp55 a8c37x1j keod5gw0 nxhoafnm aigsh9s9 d3f4x2em fe6kdd0r mau55g9w c8b282yb iv3no6db jq4qci2q a3bd9o3v knj5qynh oo9gr5id hzawbc8m" dir="auto">7.28 がリリースされて、Forge ブランディングの観点から、3D モデルや 2D 図面のロード中に表示されるローディングスピナーが新しいタイプに変更されています。</span></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde978c51200c-pi" style="display: inline;"><img alt="New_spinner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde978c51200c image-full img-responsive" src="/assets/image_336451.jpg" title="New_spinner" /></a></p>
<p>ただ、「Powered By Autodesk Forge」と表示されてしまうので、アプリによっては、運用の性格上、このローディング スピナーを元のタイプに戻したい、という方もいらっしゃるようです。</p>
<p>そのような場合には、Viewer の初期化前に、<em><strong>Autodesk.Viewing.Private.DISABLE_FORGE_LOGO = true</strong><strong>;</strong></em> のようにロゴ表示を無効化すると、旧タイプのローディングスピナーに戻すことが出来ます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">
    Autodesk.Viewing.Initializer(options, function () {

        <span style="color: #0000ff;"><strong>Autodesk.Viewing.Private.DISABLE_FORGE_LOGO = true;</strong></span>

        _viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;viewer&#39;));
        var startedCode = _viewer.start();
        if (startedCode &gt; 0) {
            console.error(&#39;Failed to create a 3D Viewer: WebGL not supported.&#39;);
            return;
        }

        // Load viewable
        var documentId = &#39;urn:&#39; + urn_svf;
        Autodesk.Viewing.Document.load(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);
    }

</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4166169200d-pi" style="display: inline;"><img alt="Original_spinner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be4166169200d image-full img-responsive" src="/assets/image_395059.jpg" title="Original_spinner" /></a></p>
<p>可能であれば、新しいローディングスピナーをお使いいただきたいのですが、必要に応じてご対応いただければと思います。</p>
<p>By Toshiaki Isezaki</p>
