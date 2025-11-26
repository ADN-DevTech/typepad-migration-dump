---
layout: "post"
title: "Forge Viewer：PDF 図面"
date: "2019-11-06 00:16:41"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/11/forge-viewerpdf-pdf-drawing.html "
typepad_basename: "forge-viewerpdf-pdf-drawing"
typepad_status: "Publish"
---

<p>Forge DevCon Japan の会場で、何名かの方から、Forge Viewer での PDF ファイルの表示についてご質問を受けました。Model Derivative API でも PDF ファイルを変換すること出来るので、Base64 エンコードされた SVF への URN&#0160; があれば、Forge Viewer で表示させることが出来ます。</p>
<p>PDF 図面を表示するコードは、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/10/forge-viewer-2d-sheets-layouts.html">Forge Viewer：2D シート/レイアウト</a></strong> の記事でご紹介したものを流用することが可能です。ただし、いくつか注意しなければならない点があります。</p>
<ul>
<li>表示される PDF ファイルは、すべてラスター データとして表示されるため、拡大率に限度があるほか、図面上のジオメトリを選択することは出来ません。</li>
<li>PDF ファイルが複数ページで構成されていても、Forge Viewer で表示出来るのは、1 ページ単位です。</li>
<li>PDF ファイル表示時には、viewable の配列にページの名前と &#39;&#39;INITIAL&#39; という名前が対で格納されてきます。このため、ユーザ インタフェースで一覧を作成するような場合は、&#39;INITIAL&#39; を除外してコントロールに名前をに登録する必要があります。</li>
<li>Model Derivative API で 3D PDF ファイルを SVF 変換することは出来ますが、変換されて表示出来るのは、2D の情報のみです。3D コンテンツを表示して操作することは出来ません。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4e54f95200b-pi" style="display: inline;"><img alt="Multi_sheet_raster_pdf" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4e54f95200b image-full img-responsive" src="/assets/image_936499.jpg" title="Multi_sheet_raster_pdf" /></a></p>
<p>この方法では、Microsoft Word や PowerPoint などから作成した 設計図書などの PDF ファイル コンテンツも表示させることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4c0a7e9200d-pi" style="display: inline;"><img alt="Pptx_pdf" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4c0a7e9200d image-full img-responsive" src="/assets/image_876813.jpg" title="Pptx_pdf" /></a></p>
<p>PDF ファイルのコンテンツをベクトル データとして表示することも可能です。ただし、この方法をサポートしているのは、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#loadmodel-url-options-onsuccesscallback-onerrorcallback" rel="noopener" target="_blank">Viewer3D.LoadModel</a></strong> メソッドを利用した場合のみです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;viewer2d&#39;));
startedCode = _viewer.start();
if (startedCode &gt; 0) {
        console.error(&#39;Failed to create a Viewer: WebGL not supported.&#39;);
        return;
}

_viewer.loadExtension(&#39;Autodesk.PDF&#39;).then(function () {
        _viewer.loadModel(&#39;https://developer.api.autodesk.com/derivativeservice/v2/derivatives/urn:adsk.viewing:fs.file:~~~/page.pdf&#39;);
});</code></pre>
<p>ベクトル データを表示するために利用するのが、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/PDFExtension/" rel="noopener" target="_blank">Autodesk.PDF Extenson</a></strong> です。複数ページの場合、ページ毎に viewable が生成されるので、SVF 変換した際のマニフェストから、表示すべきページの &quot;resource&quot; 情報をパースして取得してください。次の例は、PDF ファイル 5 ページ目の URN です。LoadModel メソッドには、この URN の前に &quot;https://developer.api.autodesk.com/derivativeservice/v2/derivatives/&quot; を追加してください。</p>
<p><code class="language-javascript code-overflow-x hljs ">
                        {<br />&#0160; &quot;guid&quot;: &quot;8de2115a-c15d-40c7-a588-9e4efaf97d2f&quot;,<br />&#0160; &quot;type&quot;: &quot;resource&quot;,<br />&#0160; &quot;urn&quot;:&#0160; &quot;<strong>urn:adsk.viewing:fs.file:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6ZnBkLWphcGFuLWF2cGc1ZmdyaDVxYnBvOGhrMTVsc3p6ZzhkcmZrbnJvdXdtd2QwcDhsbXNlMzJwN29qb3h6NXB6b251dGktN2cvOHRoJTIwZmxvb3IucGRm/output/5/page.pdf</strong>&quot;,
<br />&#0160; &quot;role&quot;: &quot;pdf-page&quot;,<br />&#0160; &quot;mime&quot;: &quot;application/octec-stream&quot;
<br />                        },
</code></p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4969ae1200c-pi" style="display: inline;"><img alt="Raster_pdf" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4969ae1200c image-full img-responsive" src="/assets/image_535162.jpg" title="Raster_pdf" /></a><br /><strong>&lt;ラスター表示時&gt;</strong></p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4969afd200c-pi" style="display: inline;"><img alt="Vector_pdf" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4969afd200c image-full img-responsive" src="/assets/image_347339.jpg" title="Vector_pdf" /></a><br /><strong>&lt;ベクトル表示時&gt;</strong></p>
<p>ベクトル データで PDF ページを表示した場合には、ジオメトリを選択することが出来るようになります。もちろんプロパティは含まれません。なお、[T] ボタンをクリックすると、文字のジオメトリを選択してクリップボードにテキストをコピー出来ますので、後でコピーしたテキストを再利用するようなことが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4e47909200b-pi" style="display: inline;"><img alt="Text_selection_vector_pdf" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4e47909200b image-full img-responsive" src="/assets/image_89644.jpg" title="Text_selection_vector_pdf" /></a></p>
<p>また、ベクトル データで PDF ページを表示した場合のみ、図面の背景色を変更することが可能です。<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#setswapblackandwhite-value" rel="noopener" target="_blank">Viewer3D.setSwapBlackAndWhite()</a>&#0160;で変更することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4bfe247200d-pi" style="display: inline;"><img alt="Vector_background" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4bfe247200d image-full img-responsive" src="/assets/image_365279.jpg" title="Vector_background" /></a></p>
<p>ベクトル データでの PDF ページ表示時には、ラスター データ表示に比べて若干タイムラグがあるのも事実です。PDF ファイルの閲覧では、いまのところ、ラスター データとしての表示が現実的かもしれません。</p>
<p>By Toshiaki Isezaki</p>
