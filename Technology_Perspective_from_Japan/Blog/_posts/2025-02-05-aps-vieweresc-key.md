---
layout: "post"
title: "APS Viewer：ESC キー"
date: "2025-02-05 00:03:40"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/02/aps-vieweresc-key.html "
typepad_basename: "aps-vieweresc-key"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e168c9200b-pi" style="display: inline;"><img alt="Esc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e168c9200b image-full img-responsive" src="/assets/image_410642.jpg" title="Esc" /></a></p>
<p>APS Viewer の使用時には、アプリ側でさまざまなイベントを利用することが出来ます。この中で <a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/#escape-event" rel="noopener" target="_blank">ESCAPE_EVENT</a> を利用すると、[ESC] キーの押下を検出してアプリ側の設定や環境をリセットする挙動を実装することが出来ます。</p>
<p>例えば、<a href="https://adndevblog.typepad.com/technology_perspective/2024/10/aps-viewer-adjustment-on-npr-extension.html" rel="noopener" target="_blank">APS Viewer：NPR エクステンションの調整</a> でご紹介した Autodesk.NPR エクステンションのスタイル表現や各値設定用の UI をリセットするような場面です。</p>
<pre>...
Autodesk.Viewing.Initializer(options, function () {


&#0160; &#0160; _viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;viewer3d&#39;));
&#0160; &#0160; var startedCode = _viewer.start();
&#0160; &#0160; if (startedCode &gt; 0) {
&#0160; &#0160; &#0160; &#0160; console.error(&#39;Failed to create a Viewer: WebGL not supported.&#39;);
&#0160; &#0160; &#0160; &#0160; return;
&#0160; &#0160; }

&#0160; &#0160; _viewer.addEventListener(<span style="color: #0000ff;"><strong>Autodesk.Viewing.ESCAPE_EVENT, onViewerESC</strong></span>);

&#0160; &#0160; var documentId = &#39;urn:&#39; + urn;
&#0160; &#0160; Autodesk.Viewing.Document.load(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);

});

...

<span style="color: #0000ff;"><strong>function onViewerESC(event) {</strong></span>
&#0160; &#0160; console.log(&quot;*** onViewerESC&quot;);
&#0160; &#0160; $(&quot;#filter&quot;).val(&#39;none&#39;);
&#0160; &#0160; $(&#39;#adjust&#39;).css(&#39;display&#39;, &#39;none&#39;);
&#0160; &#0160; _viewer.getExtensionAsync(&#39;Autodesk.NPR&#39;).then(ext =&gt; {
&#0160; &#0160; &#0160; &#0160; ext.setParameter(&#39;style&#39;, &#39;none&#39;);
&#0160; &#0160; });
<span style="color: #0000ff;"><strong>}</strong></span>
...
</pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f87a30200d-pi" style="display: inline;"><img alt="Esc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f87a30200d image-full img-responsive" src="/assets/image_970298.jpg" title="Esc" /></a></p>
<p>ただ、一部のエクステンションがアクティブな場合、ESCAPE_EVENT イベントを得られない場合があります。例えば、計測ツールでジオメトリの計測中、あるいは、断面解析ツールでライブ断面を設定中、ESC キーは、それぞれのツールの非アクティブ化に消費されてしまい、addEventListener() で ESCAPE_EVENT イベントを設定したアプリのコールバックには通知が届きません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e0cb55200b-pi" style="display: inline;"><img alt="In_tool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e0cb55200b img-responsive" src="/assets/image_279235.jpg" title="In_tool" /></a></p>
<p>このような場面では、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/HotkeyManager/" rel="noopener" target="_blank">HotkeyManager</a> を使って、ESC キーの押下を低レベル OnPress、Onrelease イベント ハンドラーとして利用することが出来ます。</p>
<pre>...
const hotkeys = [{
    keycodes: [
        Autodesk.Viewing.KeyCode.ESCAPE
    ],
<strong><span style="color: #0000ff;">    onPress: (e) =&gt; { console.log(&quot;ESC KeyDown&quot;) },
    onRelease: (e) =&gt; { console.log(&quot;ESC KeyUp&quot;) }</span></strong>
}];
<span style="color: #0000ff;"><strong>_viewer.getHotkeyManager().pushHotkeys(&quot;MYHOTKEYS&quot;, hotkeys);</strong></span>
...
</pre>
<p>By Toshiaki Isezaki</p>
