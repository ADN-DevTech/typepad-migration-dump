---
layout: "post"
title: "Forge Viewer バージョン 2.15 リリース"
date: "2017-06-12 00:38:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/06/release-forge-viewer-v2_15.html "
typepad_basename: "release-forge-viewer-v2_15"
typepad_status: "Publish"
---

<p>Forge Viewer の新バージョン &#0160;2.15 がリリースされました。そこで、今回はバージョン 2.15 の改良点や新機能をご紹介していきたいと思います。</p>
<p>なお、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/02/about-vr-support-on-forge-viewer.html" rel="noopener" target="_blank">Forge Viewer の VR サポートについて</a></strong>&#0160;ブログ記事の最後でもご案内したとおり、Viewer バージョンを指定をせずに使用した場合は、Forge Viewer &#0160;を利用しているオートデスク製品にあわせて、無条件に バージョン 2.10 が適用されてしまいます。このため、バージョン 2.15 を利用するには、明示的に&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/specifying-version-to-forge-viewer.html" rel="noopener" target="_blank">Forge Viewer のバージョン指定</a></strong> をおこなう必要がありますのでご注意ください。</p>
<hr />
<h2><span style="font-size: 18pt;"><strong>Extension ロード方法の変更</strong></span></h2>
<p>ご存じのとおり、Forge Viewer には Extension と呼ばれる拡張モジュールを JavaScript ファイル単位で作成して必要に応じて Viewer にロードさせることで、Viewer 自身の機能を拡張していくメカニズムが提供されています。ちょうと、AutoCAD や Revit などのデスクトップ製品いうアドインの開発と利用と似ています。Extension は &#0160;3rd party が作成/利用するだけでなく、<a href="http://adndevblog.typepad.com/technology_perspective/2016/12/extensions-abailable-on-forge-viewer.html" rel="noopener" target="_blank"><strong>Forge Viewer で利用可能な Extension</strong></a> のブログ記事でご案内したとおり、オートデスク自身も利用しています。</p>
<p>今回のバージョンでは、表示中の Forge Viewer に Extension をロードする方法が大きく変更されています。 バージョン 2.14 まで、オートデスクが提供するすべての Extension は viewer3D.min.js にバンドルして提供してきましたが、バージョン 2.15 から &#0160;Extension のコードを viewer3D.min.js から分離する処理を開始します。分離対象となる最初の Extension は&#0160;InViewerSearch.min.js です。今後、他の標準 Extension も分離していくことを予定しています。</p>
<p><strong>コードの変更 [2.14 バージョンとは</strong><strong>互換性なし]</strong></p>
<p>viewer.loadExtension() 関数を利用して Extension をロードしている場合、この関数は、従来のブール値ではなく、非同期処理でのロードを実現させる目的で&#0160;<strong><a href="https://developer.mozilla.org/ja/docs/Web/JavaScript/Reference/Global_Objects/Promise" rel="noopener" target="_blank">Promise</a></strong> を返すようになります。このため、以前使用していたコードとの互換性がありません。</p>
<p><strong>バージョン 2.14 以前</strong></p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-keyword">var</span> result = viewer.loadExtension(<span class="hljs-string">&#39;Autodesk.InViewerSearch&#39;</span>);
<span class="hljs-keyword">if</span> (result === <span class="hljs-literal">true</span>) {
   <span class="hljs-keyword">var</span> extension = viewer.getExtension(<span class="hljs-string">&#39;Autodesk.InViewerSearch&#39;</span>);
   console.log(<span class="hljs-string">&#39;Extension がロードされました: &#39;</span> + extension.id);
}</code></pre>
<p><strong>バージョン 2.15 以降</strong></p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-keyword">var</span> promise = viewer.loadExtension(<span class="hljs-string">&#39;Autodesk.InViewerSearch&#39;</span>); <span class="hljs-comment">// サーバーから非同期でロード</span>
promise.then(<span class="hljs-function"><span class="hljs-keyword">function</span><span class="hljs-params">(extension)</span>{</span>
   console.log(<span class="hljs-string">&#39;Extension がロードされました: &#39;</span> + extension.id);
});</code></pre>
<p>なお、viewer.getExtension() と&#0160;viewer.unloadExtension() 関数に変更はありません。また、</p>
<hr />
<h2><span style="font-size: 18pt;"><strong>メモリ管理のオプションの追加</strong></span></h2>
<p>エンドユーザが直面する問題の 1 つに、メモリ不足で 3D モデルを表示できないという問題があります。これは、使用中の Web ブラウザが、すべての 3D モデルを表示するのに十分なメモリを確保出来ない、というのが主な理由です。そのような場面では、結果として、ブラウザ自身がクラッシュしてしまいます。</p>
<p>バージョン 2.15 では、3D デザイン ファイルをロードする際に Viewer コードが割り当てることのできるメモリ量を指示するメカニズムが導入されています。この機能は<strong>既定では無効化されている</strong>点に注意してください。</p>
<p>機能を有効化する方法は次のとおりです。:</p>
<p><strong>使用方法</strong></p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-keyword">var</span> config3d = {
    memory: {
        limit: <span class="hljs-number">400</span><span class="hljs-comment">// メガバイト</span>
    }
};
<span class="hljs-keyword">var</span> viewer = <span class="hljs-keyword">new</span> Autodesk.Viewing.Viewer3D(container, config3d);
viewer.loadModel( modelUrl );</code></pre>
<p>デザイン毎に機能を有効化する方法として、ブラウザの URL に &#0160;viewermemory=&lt;value&gt; の引数を追加する方法もあります。</p>
<p>例 - https://mywebsite.com/viewer/document-id-here<strong>?viewermemory=400</strong>&#0160;</p>
<p>この URL パラメータでの指定は、コードによって記述されたメモリ制限をオーバーライドする点に注意してください。</p>
<p>この機能が動作しているかチェックするには、Viewer インスタンスで利用可能な次の関数を利用してみてください。:</p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-keyword">var</span> memInfo = viewer.getMemoryInfo();
console.log(memInfo.limit);          <span class="hljs-comment">// == 400 MB</span>
console.log(memInfo.effectiveLimit); <span class="hljs-comment">// &gt;= 400 MB</span>
console.log(memInfo.loaded);         <span class="hljs-comment">// &lt;= 400 MB</span></code></pre>
<p>機能が無効な場合には、これらの関数は null を返します。</p>
<hr />
<h2><span style="font-size: 18pt;"><strong>建築モデル用の既定照明</strong></span></h2>
<p>Viewer にプリセットされた環境光に「野原(Field)」が新設されて、BIM モデルをロードした際の既定となるように設定されています。なお、ここでいう BIM モデルとは、Revit の RVT ファイル、Navisworks の NWD ファイルと NWC ファイルを意味します。</p>
<p><img alt="Default_environment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09a4a6b6970d image-full img-responsive" src="/assets/image_181645.jpg" title="Default_environment" /></p>
<p>また、「パフォーマンスと外観」設定には「エッジを表示」が新設され、こちらも既定でオンの状態になります。上記、バージョン 2.15 の表示イメージでも「エッジを表示」が有効な状態になっています。</p>
<div class="file file-image file-image-png" id="file-2050">
<div class="content"><a href="https://forge.autodesk.com/image/image2017-4-28-15-20-3png"> </a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d28bbfc3970c-pi" style="display: inline;"><img alt="Settings_panel_comparison" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d28bbfc3970c image-full img-responsive" src="/assets/image_445337.jpg" title="Settings_panel_comparison" /></a><a href="https://forge.autodesk.com/image/image2017-4-28-15-20-3png"></a></div>
</div>
<hr />
<h2><span style="font-size: 18pt;"><strong>ViewingApplication の強化</strong></span></h2>
<p>Viewer 表示の際の利用が推奨されているユーティリティ クラスである &#0160;ViewingApplication では、Model Derivative API でデザイン ファイルを変換した際に生成されるマニフェスト ファイルから Viewer を初期化出来るようになっています。<br />この更新には、下記の機能も含まれています。</p>
<h3><span style="font-size: 12pt;">1. 追加された関数: setDocument()</span></h3>
<p>Forge Viewer を利用する場合、以前は URN 文字列で ViewingApplication を初期化する必要がありました。下記は、<strong><a href="https://developer.autodesk.com/en/docs/viewer/v2/tutorials/" rel="noopener" target="_blank">Step-by-Step Tutorials</a></strong> の <strong><a href="https://developer.autodesk.com/en/docs/viewer/v2/tutorials/basic-application/" rel="noopener" target="_blank">Basic Application</a></strong>&#0160;からの引用です。</p>
<p><strong>バージョン 2.14 以前</strong></p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-comment">// Autodesk.Viewing.Initializer() 成功後</span>
<span class="hljs-keyword">var</span> documentId = <span class="hljs-string">&#39;urn:&lt;YOUR_URN_ID&gt;&#39;</span>;
viewerApp = <span class="hljs-keyword">new</span> Autodesk.Viewing.ViewingApplication(<span class="hljs-string">&#39;MyViewerDiv&#39;</span>);
viewerApp.registerViewer(viewerApp.k3D, Autodesk.Viewing.Private.GuiViewer3D);
viewerApp.loadDocument(documentId, onDocumentLoadSuccess, onDocumentLoadFailure); <span class="hljs-comment">// このメソッドはオートデスク サーバーを非同期に呼び出す</span>
 
<span class="hljs-comment">// 非同期コールバック</span>
<span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">onDocumentLoadSuccess</span><span class="hljs-params">(doc)</span> {</span>
  <span class="hljs-keyword">var</span> viewables = viewerApp.bubble.search({<span class="hljs-string">&#39;type&#39;</span>:<span class="hljs-string">&#39;geometry&#39;</span>});
  viewerApp.selectItem(viewables[<span class="hljs-number">0</span>].data, onItemLoadSuccess, onItemLoadFail);
}</code></pre>
<p>この方法の欠点は、アプリケーション独自の処理のためにすでにマニフェスト取得処理をしている場合でも、開発者にサーバーからのマニフェスト取得を強制する点です。</p>
<p>バージョン 2.15 では、新しい関数 ViewingApplication:&#0160;setDocument() を追加して、標準的な JavaScript のオブジェクト パラメータを受け入れるものです。使用方法は次のとおりです。:</p>
<p><strong>バージョン 2.15 以降</strong></p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-comment">// https://developer.api.autodesk.com/modelderivative/v2/designdata/:urn/manifest</span>
<span class="hljs-comment">// から JSON を取得して、オブジェクトとして保存します</span>
<span class="hljs-keyword">var</span> manifestObject = {...};
  
<span class="hljs-comment">// Autodesk.Viewing.Initializer() 成功後</span>
viewerApp = <span class="hljs-keyword">new</span> Autodesk.Viewing.ViewingApplication(<span class="hljs-string">&#39;MyViewerDiv&#39;</span>);
viewerApp.registerViewer(viewerApp.k3D, Autodesk.Viewing.Private.GuiViewer3D);
viewerApp.setDocument(manifestObject); <span class="hljs-comment">// 非同期ではない!</span>
<span class="hljs-keyword">var</span> viewables = viewerApp.bubble.search({<span class="hljs-string">&#39;type&#39;</span>:<span class="hljs-string">&#39;geometry&#39;</span>});
viewerApp.selectItem(viewables[<span class="hljs-number">0</span>].data, onItemLoadSuccess, onItemLoadFail);</code></pre>
<h3><span style="font-size: 12pt;">2.関数の強化:&#0160;selectItem()&#0160;</span></h3>
<p>BubbleNode オブジェクトのサポートを追加しています。</p>
<p><strong>バージョン 2.14 以前</strong></p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-comment">// 前述の例で使用</span>
<span class="hljs-keyword">var</span> viewables = viewerApp.bubble.search({<span class="hljs-string">&#39;type&#39;</span>:<span class="hljs-string">&#39;geometry&#39;</span>});
viewerApp.selectItem(viewables[<span class="hljs-number">0</span>].data, onItemLoadSuccess, onItemLoadFail);</code></pre>
<p><strong>バージョン 2.15 以前</strong></p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-keyword">var</span> viewables = viewerApp.bubble.search({<span class="hljs-string">&#39;type&#39;</span>:<span class="hljs-string">&#39;geometry&#39;</span>});
viewerApp.selectItem(viewables[<span class="hljs-number">0</span>], onItemLoadSuccess, onItemLoadFail);</code></pre>
<h3><span style="font-size: 12pt;">3. 追加された関数: getNamedViews()</span></h3>
<p>名前で識別可能な 3D viewable からすべてのカメラ ビューを取得するヘルパー メソッドです。</p>
<p>使用方法</p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-keyword">var</span> namedViews = viewerApp.getNamedViews(viewables[<span class="hljs-number">0</span>]);
alert(<span class="hljs-string">&#39;Selecting named view: &#39;</span> + namedViews[<span class="hljs-number">0</span>].data.name);
viewerApp.selectItem(namedViews[<span class="hljs-number">0</span>], onItemLoadSuccess);</code></pre>
<p>マニフェストに格納される名前の付いたビューHere&#39;s how named views show up in the manifest:</p>
<div class="file file-image file-image-png" id="file-2054">
<h2 class="element-invisible"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09a4a8c6970d-pi" style="display: inline;"><img alt="Named_vew_in_manifest" class="asset  asset-image at-xid-6a0167607c2431970b01bb09a4a8c6970d img-responsive" src="/assets/image_149986.jpg" style="width: 400px;" title="Named_vew_in_manifest" /></a></h2>
</div>
<hr />
<h2><span style="font-size: 18pt;"><strong>エッジの表示</strong></span></h2>
<p>新設された「エッジを表示」を有効にする新しい関数が追加されています。:</p>
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class="language-javascript hljs "><span class="hljs-comment">// 「エッジを表示」をオンに設定</span>
viewer.setDisplayEdges(<span class="hljs-literal">true</span>);</code></pre>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d28bc2b7970c-pi" style="display: inline;"><img alt="Edge_differences" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d28bc2b7970c image-full img-responsive" src="/assets/image_126593.jpg" title="Edge_differences" /></a></p>
<hr />
<div class="file file-image file-image-png" id="file-2056">
<div class="content">&#0160;</div>
<div class="content">&#0160;</div>
<div class="content">By Toshiaki Isezaki</div>
</div>
