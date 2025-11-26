---
layout: "post"
title: "Forge Viewer バージョン 6.4 リリース"
date: "2019-02-18 00:50:34"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/02/release-forge-viewer-v6-4.html "
typepad_basename: "release-forge-viewer-v6-4"
typepad_status: "Publish"
---

<p>新しい Forge Viewer バージョン 6.4 がリリースされていますので、その機能や変更点をご紹介しておきます。 Extension については、<strong><a href="http://lmv.ninja.autodesk.com/" rel="noopener noreferrer" target="_blank">LMV Ninja</a></strong>&#0160; でお試しいただけます。LMV Ninja については、過去のブログ記事<strong>&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2018/05/display-specified-urn-on-lmv-ninja.html" rel="noopener noreferrer" target="_blank">LMV Ninja を使った URN 指定表示</a></strong>&#0160;などをご参照ください。</p>
<p><strong>新機能</strong></p>
<ul>
<li><strong>CrossFadeEffects Extension</strong></li>
</ul>
<p style="padding-left: 40px;">CrossFade エフェクトは、複数のカラーターゲットへのレンダリングとそれらのブレンドをサポートするビューアのオプション機能です。 以前は、このコードはビューア本体のコードの一部でしたが、今回、新しい Extension である &quot;CrossFadeEffects&quot; に移動することのなりました。Extension 名は <strong>Autodesk.CrossFadeEffects</strong> となります。</p>
<h5 id="ReleaseNotes6.4-Example:" style="padding-left: 40px;">例:</h5>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code" style="padding-left: 40px;"><code class="language-javascript code-overflow-x hljs " id="snippet-0"><span class="hljs-keyword">var</span> extName = <span class="hljs-string">&#39;Autodesk.CrossFadeEffects&#39;</span>;
NOP_VIEWER.loadExtension(extName);

<span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">setCameraToRoom</span><span class="hljs-params">()</span> {</span>
    <span class="hljs-keyword">var</span> cam = {
       fov:          <span class="hljs-number">53.13010235415598</span>,
       isPerspective:<span class="hljs-literal">true</span>,
       orthoScale:   <span class="hljs-number">6.442020414517138</span>,
       position:     {x:-<span class="hljs-number">23.63091853857176</span>, y: <span class="hljs-number">0.9033896546012906</span>, z:-<span class="hljs-number">4.261154219883789</span>},
       target:       {x:-<span class="hljs-number">20.871083468967406</span>, y: <span class="hljs-number">6.520671770079398</span>, z:-<span class="hljs-number">5.787286273399167</span>},
       up:           {x:<span class="hljs-number">0.10446560472788749</span>, y: <span class="hljs-number">0.21262602957092375</span>, z:<span class="hljs-number">0.9715333802694284</span>}
    };
    NOP_VIEWER.impl.setViewFromCamera(cam, <span class="hljs-literal">true</span>);
}

<span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">fadeExample</span><span class="hljs-params">()</span> {</span>

    <span class="hljs-comment">// apply fade transition</span>
    <span class="hljs-keyword">var</span> viewer = NOP_VIEWER;
    <span class="hljs-keyword">var</span> ext = viewer.getExtension(extName);   
    ext.fadeToViewerState(setCameraToRoom, <span class="hljs-number">1.5</span>);
}
</code></pre>
<ul>
<li id="ReleaseNotes6.4-Geolocationextension"><strong>Geolocation Extension</strong></li>
</ul>
<p style="padding-left: 40px;">WGS-84 形式（x：経度、y：緯度、z：高さ（m））の GPS 座標をビューア シーン座標に変換したり、その逆の変換をおこなうための機能を提供します。 シーンにロードされた単一の 3D モデルをサポートします。 Extension 名は <strong>Autodesk.Geolocation</strong> です。</p>
<p style="padding-left: 40px;"><strong>例:</strong></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code" style="padding-left: 40px;"><code class="language-javascript code-overflow-x hljs " id="snippet-1">viewer.loadExtension(&#39;Autodesk.Geolocation&#39;).then(function (extension) {<br />    extension.activate();<br />});)</code></pre>
<p style="padding-left: 40px;"><strong>機能:</strong></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code" style="padding-left: 40px;"><code class="language-javascript code-overflow-x hljs " id="snippet-2">hasGeolocationData()&#0160;</code></pre>
<p style="padding-left: 40px;">モデルに位置情報データが含まれているかをブール値を返します（モデルに位置情報データが含まれている場合は <em><strong>true</strong></em>）。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code" style="padding-left: 40px;"><code class="language-javascript code-overflow-x hljs " id="snippet-3">lmvToLonLat( lmvPoint&#0160;)&#0160;</code></pre>
<p style="padding-left: 40px;">viewer.clientToWorld（）などで取得したビューア座標を WGS-84 形式の座標（x：経度、y：緯度、z：高さ（メートル））に変換します。</p>
<p style="padding-left: 40px;">戻り値：THREE.Vector3：WGS-84 形式の GPS 座標：（x：経度、y：緯度、z：高さ）</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code" style="padding-left: 40px;"><code class="language-javascript code-overflow-x hljs " id="snippet-4">lonLatToLmv( lonLat&#0160;)</code></pre>
<p style="padding-left: 40px;">WGS-84 形式の座標（x：経度、y：緯度、z：高さ（メートル））をビューア シーン座標に変換します。</p>
<p style="padding-left: 40px;">戻り値：<em>THREE.Vector3</em>:&#0160; シーンの 3D 点</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code" style="padding-left: 40px;"><code class="language-javascript code-overflow-x hljs " id="snippet-5">openGoogleMaps( [&#0160;pointLL84&#0160;]&#0160;)</code></pre>
<p style="padding-left: 40px;">指定された GPS 位置に PIN を付けて Google マップのURLを返します。 引数が指定されていない場合、URL はモデルの位置情報があればそれを使用します。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code" style="padding-left: 40px;"><code class="language-javascript code-overflow-x hljs " id="snippet-6">activate()&#0160;</code></pre>
<p style="padding-left: 40px;">モデルをクリックして点を設定し、テストおよびデバッグ用のオプションのパネル UI（英語）を表示します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c3b59d200d-pi" style="display: inline;"><img alt="Geographic_location_extension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c3b59d200d image-full img-responsive" src="/assets/image_305881.jpg" title="Geographic_location_extension" /></a></p>
<ul>
<li>デンマーク語ロケールのサポート</li>
<li>viewer.impl.getScreenshotProgressive へのリーフレットのサポート</li>
<li>getScreenshotProgressiveでゴーストオブジェクトをサポート</li>
<li>Markup Extension&#0160; でのアンドゥのサポート</li>
<li>異なる dpi リーフレット-pdfファイルの比較をサポート</li>
<li>viewer.setTheme（ &#39;bim-theme&#39;）による BIM カラーテーマ</li>
<li>メソッドBubbleNode.useAsDefault（）：Boolean</li>
</ul>
<p><strong>変更点</strong></p>
<ul>
<li>設定パネルの環境 UI の再デザイン</li>
<li>[PushPin Extension] LMV IDとともに外部 ID を保存</li>
<li>[PushPin Extension] globalOffsetをviewerState に保存</li>
<li>FloorSelector：モバイルでの実行時には crossFade 効果を使用を抑止</li>
</ul>
<p>By Toshiaki Isezaki</p>
