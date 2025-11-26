---
layout: "post"
title: "Forge Viewer：プロパティパネル"
date: "2022-06-01 00:14:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/06/forge-viewer-properties-panel.html "
typepad_basename: "forge-viewer-properties-panel"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2018/09/creating-forge-viewer-extension.html" rel="noopener" target="_blank"><strong>Forge Viewer Extension の作成</strong></a> などのブログ記事で、過去に Forge Viewer 上にプロパティを表示する例をご紹介したことがあります。</p>
<p>Forge Viewer は、その後も更新され続けていて、現在の最新バージョンでは、<a href="https://forge.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/#id79" rel="noopener" target="_blank">複数オブジェクトの選択</a>（ノードの選択）もサポートされていて、同記事でご紹介したエクステンション（Extension）内のコードが動作しなくなっています。そこで、ここでは、代替となるエクステンション コードをご紹介しておきたいと思います。</p>
<blockquote>
<div>
<div>class WorkshopExtension extends Autodesk.Viewing.Extension {</div>
<div>&#0160; &#0160; constructor(viewer, options) {</div>
<div>&#0160; &#0160; &#0160; &#0160; super(viewer, options);</div>
<div>&#0160; &#0160; }</div>
<br />
<div>&#0160; &#0160; load() {</div>
<div>&#0160; &#0160; &#0160; &#0160; this.viewer.addEventListener(Autodesk.Viewing.EXTENSION_LOADED_EVENT, (ev) =&gt; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; if (ev.extensionId === &#39;Autodesk.PropertiesManager&#39;) {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; const ext = this.viewer.getExtension(&#39;Autodesk.PropertiesManager&#39;);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ext.setPanel(new CustomPropertyPanel(this.viewer));</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; });</div>
<div>&#0160; &#0160; &#0160; &#0160; return true;</div>
<div>&#0160; &#0160; }</div>
<br />
<div>&#0160; &#0160; unload() {</div>
<div>&#0160; &#0160; &#0160; &#0160; var ext = this.viewer.getExtension(&#39;Autodesk.PropertiesManager&#39;);</div>
<div>&#0160; &#0160; &#0160; &#0160; ext.setDefaultPanel();</div>
<div>&#0160; &#0160; &#0160; &#0160; return true;</div>
<div>&#0160; &#0160; }</div>
<div>}</div>
<br />
<div>class CustomPropertyPanel extends Autodesk.Viewing.Extensions.ViewerPropertyPanel {</div>
<div>&#0160; &#0160; constructor(viewer) {</div>
<div>&#0160; &#0160; &#0160; &#0160; super(viewer);</div>
<div>&#0160; &#0160; }</div>
<br />
<div>&#0160; &#0160; setAggregatedProperties(props) {</div>
<div>&#0160; &#0160; &#0160; &#0160; super.setAggregatedProperties(props);</div>
<div>&#0160; &#0160; &#0160; &#0160; var _this = this;</div>
<div>&#0160; &#0160; &#0160; &#0160; this.addProperty(&#39;dbId&#39;, this.propertyNodeId, &#39;デバッグ情報&#39;);</div>
<div>&#0160; &#0160; &#0160; &#0160; this.viewer.getProperties(this.propertyNodeId, function (props2) {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; _this.addProperty(&quot;externalId&quot;, props2.externalId, &#39;デバッグ情報&#39;);</div>
<div>&#0160; &#0160; &#0160; &#0160; })</div>
<div>&#0160; &#0160; }</div>
<div>}</div>
<br />
<div>Autodesk.Viewing.theExtensionManager.registerExtension(&#39;WorkshopExtension&#39;, WorkshopExtension);</div>
</div>
</blockquote>
<p>このエクステンションでは、既存の &quot;Autodesk.PropertiesManager&quot; エクステンションを流用してオブジェクト プロパティ（<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/UI/PropertyPanel/" rel="noopener" target="_blank">PropertyPanel</a> オブジェクト）を表示しているほか、「デバッグ情報」と題して、<a href="https://forge.autodesk.com/en/docs/viewer/v5/reference/javascript/propertypanel/#addproperty-name-value-category-options" rel="noopener" target="_blank">PropertyPanel.addProperty()</a> メソッドで、追加のプロパティ dbId と externalId を表示します。ここで使用しているのが、従来使用していた setProperties() メソッドではなく、複数モデルのプロパティに対応する setAggregatedProperties() メソッドになっている点にご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788075b00f200d-pi" style="display: inline;"><img alt="Custom_properties" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788075b00f200d image-full img-responsive" src="/assets/image_744444.jpg" title="Custom_properties" /></a></p>
<p>上記コードをエクステンションとして動作させるためには、まず、WorkshopExtension.js の名前でエンコード付きで保存してみてください。（ANSI 形式で保存すると「デバッグ情報」の文字が文字化けしてしまいます。）</p>
<p>続いて、Forge Viewer のカンバスを定義する HTML ファイルヘッダー セクション（&lt;head&gt;～&lt;/head&gt;）ので、次のように WorkshopExtension.js を参照します。</p>
<blockquote>
<p>&lt;script type=&quot;text/javascript&quot; src=&quot;/WorkshopExtension.js&quot;&gt;&lt;/script&gt;</p>
</blockquote>
<p>エクステンションを Viewer にロードする部分は、&quot;Autodesk.PropertiesManager&quot; エクステンションのロードの確認処理を EXTENSION_LOADED_EVENT イベントで実装している都合で、Viewer の初回表示時に機能しない場合があるため、Autodesk.Viewing.Document.load 成功時のコールバック関数でロードしてみてください。</p>
<div>
<blockquote>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ：</div>
<div>&#0160; &#0160; &#0160; &#0160; // Load viewable</div>
<div>&#0160; &#0160; &#0160; &#0160; var documentId = &#39;urn:&#39; + urn;</div>
<div>&#0160; &#0160; &#0160; &#0160; Autodesk.Viewing.Document.load(documentId, <strong>onDocumentLoadSuccess</strong>, onDocumentLoadFailure);</div>
<div>&#0160; &#0160; });</div>
<div>&#0160;</div>
<div>&#0160; &#0160; function <strong>onDocumentLoadSuccess</strong>(viewerDocument) {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ：&#0160;</div>
<div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ：</div>
<strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; _viewer.loadExtension(&quot;WorkshopExtension&quot;);</strong></div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ：</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ：</div>
</blockquote>
</div>
<p>By Toshiaki Isezaki</p>
