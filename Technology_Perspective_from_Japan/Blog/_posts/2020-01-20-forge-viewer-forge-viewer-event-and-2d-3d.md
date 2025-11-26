---
layout: "post"
title: "Forge Viewer：イベント処理と 2D / 3D 連携"
date: "2020-01-20 00:14:19"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-forge-viewer-event-and-2d-3d.html "
typepad_basename: "forge-viewer-forge-viewer-event-and-2d-3d"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/10/forge-viewer-2d-sheets-layouts.html" rel="noopener" target="_blank">Forge Viewer：2D シート/レイアウト</a></strong> のブログ記事でご案内したとおり、2D シートと 3D モデルが 1 つのシードファイルに存在する場合、同ファイルを Model Derivative API で変換して 2D 情報を Forge Viewer 上で表示することが出来ます。この際、変換された viewables には 3D 情報も同時ぬ含まれているので、表示するカンバス領域（&lt;div&gt;～&lt;/div&gt;）を個別に定義することで、同時に 2D と 3D を表示させることが可能です。</p>
<p>Forge Viewer 上に表示されるジオメトリは、カンバス上で選択出来る点は周知のとおりです。また、ジオメトリは、<strong>dbId</strong> と呼ばれる識別子で一意に識別されていて、3D カンバス上の 3D viewables のジオメトリは、投影された2D カンバス上の 2D viewables ジオメトリとで同じ dbId を持っています。CAD ソフトウェアでは、ごく一般的な構造ですが、このジオメトリ識別をイベント処理と併用することで、容易に 2D と 3D カンバス間の連携を実現することが出来るようになります。</p>
<p>例えば、HTML ページに 2D と 3D を表示する 2 つの &lt;div&gt; セクションが Forge Viewer カンバスとして用意されていて、Revit プロジェクト ファイルから変換された情報を表示すると仮定してください。この時、前者カンバスのタグ id が <strong>viewer2d</strong>、後者カンバスのタグ id が <strong>viewer3d</strong> になっていて、それぞれの Viewer3D インスタンスを格納する JavaScript 変数が、<strong>_viewer2d</strong> と <strong>_viewer3d</strong> とします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4fd6393200b-pi" style="display: inline;"><img alt="2s_3d_canvas" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4fd6393200b image-full img-responsive" src="/assets/image_533884.jpg" title="2s_3d_canvas" /></a></p>
<p>この状態で、_viewer2d カンバスのドキュメント ロード成功時に、次のように、ジオメトリ選択 イベント SELECTION_CHANGED_EVENT&#0160; をフックします。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer2d.addEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, onSelected2D);
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>同じく、_viewer3d カンバスのドキュメント ロード成功時に、次のように、ジオメトリ選択 イベント SELECTION_CHANGED_EVENT&#0160; をフックします。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer3d.addEventListener(Autodesk.Viewing.SELECTION_CHANGED_EVENT, onSelected3D);
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>そして、それぞれのカンバスでジオメトリが選択された際に呼ぼ出されるイベントハンドラ関数 onSelected2D() と onSelected3D() を次のように定義します。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    function onSelected2D(event) {
        var dbIdArray = event.dbIdArray;
        if (dbIdArray.length &gt; 0) {
            _viewer3d.clearSelection();
            _viewer3d.isolate(dbIdArray);
            _viewer3d.fitToView(dbIdArray);
        }
    }

    function onSelected3D(event) {
        var dbIdArray = event.dbIdArray;
        if (dbIdArray.length &gt; 0) {
            _viewer2d.clearSelection();
            _viewer2d.isolate(dbIdArray);
            _viewer2d.fitToView(dbIdArray);
        }
    }
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>SELECTION_CHANGED_EVENT イベントハンドラ関数のパラメータには、選択したジオメトリの dbId が格納されているので、もう一方のカンバスの同じ dbId ジオメトリを isolate() － 単独表示して、fitToView() － 拡大 する記述をすることで、次のような連携処理を実現することが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4d8bfe3200d-pi" style="display: inline;"><img alt="2d-3d" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4d8bfe3200d image-full img-responsive" src="/assets/image_222286.jpg" title="2d-3d" /></a></p>
<p>By Toshiaki Isezaki</p>
