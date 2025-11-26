---
layout: "post"
title: "Forge Viewer バージョン 6.3 リリース"
date: "2018-12-05 16:57:26"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/11/release-forge-viewer-v6_3.html "
typepad_basename: "release-forge-viewer-v6_3"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3aa17db200d-pi" style="float: right;"><img alt="Viewer-api-blue" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3aa17db200d img-responsive" src="/assets/image_647937.jpg" style="margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad37f6144200c-pi" style="float: right;"></a>新しい Forge Viewer バージョン 6.3 がリリースされていますので、その機能や変更点をご紹介しておきます。 Extension については、<strong><a href="http://lmv.ninja.autodesk.com/" rel="noopener noreferrer" target="_blank">LMV Ninja</a></strong>&#0160; でお試しいただけます。LMV Ninja については、過去のブログ記事&#0160;<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/05/display-specified-urn-on-lmv-ninja.html" rel="noopener noreferrer" target="_blank">LMV Ninja を使った URN 指定表示</a></strong>&#0160;などをご参照ください。</p>
<h3><strong>新機能</strong></h3>
<h4 style="padding-left: 30px;"><strong>Autodesk.PDF Extension</strong></h4>
<p style="padding-left: 30px;">ベクトル データ（ジオメトリ）して PDF ファイルを表示する目的で、Autodesk.PDF Extension が用意されています。実装はプロトタイプとしての扱いなので、今後、実装内容が変更を受ける可能性があります。.</p>
<p style="padding-left: 30px;">サンプルコード:</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code" style="padding-left: 30px;"><code class="language-javascript code-overflow-x hljs " id="snippet-0"><span class="hljs-comment">// Create Viewer instance and load PDF file on page 1</span>
    Autodesk.Viewing.Initializer(options, <span class="hljs-function"><span class="hljs-keyword">function</span><span class="hljs-params">()</span> {</span>
        <span class="hljs-keyword">var</span> viewer = <span class="hljs-keyword">new</span> Autodesk.Viewing.Viewer3D(div,config3d);
        viewer.start()
        viewer.loadExtension(<span class="hljs-string">&#39;<strong>Autodesk.PDF</strong>&#39;</span>).then(<span class="hljs-function"><span class="hljs-keyword">function</span><span class="hljs-params">()</span> {</span>
            <span class="hljs-comment">// URL parameter `page` will override value passed to loadModel</span>
            viewer.loadModel(<span class="hljs-string">&#39;path/to/file.pdf&#39;</span>, { page: <span class="hljs-number">1</span> });
        });
    });</code></pre>
<h4 style="padding-left: 30px;"><strong>Autodesk.DocumentBrowser Extension</strong></h4>
<p style="padding-left: 30px;">JSONマニフェストで使用可能なすべての 2D および 3D モデルを表示するためのツールバーボタンとパネルを追加する Autodesk.DocumentBrrowser Extension です。後述する Viewer3D.loadDocumentNode() を viewer.loadDocumentNode(doc, geometryItems[0]) のように利用してモデルをロードした際にのみ利用することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c99da1200b-pi" style="display: inline;"><img alt="Document_browser" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c99da1200b image-full img-responsive" src="/assets/image_42139.jpg" title="Document_browser" /></a></p>
<h4 style="padding-left: 30px;"><strong>Viewer3D.loadDocumentNode</strong></h4>
<p style="padding-left: 30px;">新しいモデル ロードが追加されました:&#0160;<em><strong>viewer.loadDocumentNode(lmvDocument, bubbleNode, options)</strong></em></p>
<p style="padding-left: 30px;"><img alt="table" data-entity-type="file" data-entity-uuid="ce48da6d-82d1-49c5-8d3f-249f25e00ed1" src="/assets/Screen%20Shot%202018-11-28%20at%209.35.26%20AM.png" /></p>
<p style="padding-left: 30px;">最初に非表示のメッシュをスキップする <strong>loadOptions.skipHiddenFragments:bool</strong> が用意されました。</p>
<h3><strong>変更点</strong></h3>
<p style="padding-left: 30px;">プロパティ パネルのタイトルが選択したノード（オブジェクト）名に変更されています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c40404200b-pi" style="display: inline;"><img alt="Property_title" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c40404200b image-full img-responsive" src="/assets/image_814676.jpg" title="Property_title" /></a></p>
<p style="padding-left: 30px;">互換性のために <strong><em>Autodesk.Viewing.Private.getHtmlTemplate</em></strong> 関数を復元しています。</p>
<h4><strong>その他の変更点：</strong></h4>
<ul>
<li>BlendShader の AO 不透明度の既定値は変更されています。</li>
<li>SAOShader の 半径と強度の既定値が変更されています。</li>
<li>developer.autodesk.comを参照しているすべてのドキュメントURLがforge.developer.comを参照しています。</li>
<li>ジオメトリがレンダリングの準備ができたらすぐにモデル読み込み UI が削除されるようになりました。</li>
</ul>
<p>By Toshiaki Isezaki</p>
