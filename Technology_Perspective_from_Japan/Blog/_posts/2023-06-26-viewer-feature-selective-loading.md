---
layout: "post"
title: "APS Viewer：選択ロード"
date: "2023-06-26 00:03:21"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/06/viewer-feature-selective-loading.html "
typepad_basename: "viewer-feature-selective-loading"
typepad_status: "Publish"
---

<p>APS Viewer <a href="https://aps.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/#section-1" rel="noopener" target="_blank">v7.89</a> で指定した空間（領域）、あるいは、メタデータ（プロパティ）条件に基づいて選択的にモデルをロード・表示する「選択ロード」の機能が追加されています。指定する条件は Viewer でモデル表示時に使用することになるため、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#loaddocumentnode-avdocument-manifestnode-options" rel="noopener" target="_blank">loadDocomentNode</a> メソッド、または、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#loadmodel-url-options-onsuccesscallback-onerrorcallback" rel="noopener" target="_blank">loadModel</a> メソッド呼び出し時に、オプションとして フィルタ（filter）を JSON 形式で指定することになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cabab6200b-pi" style="display: inline;"><img alt="No_filter" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cabab6200b image-full img-responsive" src="/assets/image_732054.jpg" title="No_filter" /></a></p>
<p><strong>空間フィルタ（spatial_query）</strong></p>
<p style="padding-left: 40px;">指定した空間（領域）のモデルのみをロード、表示します。空間は、以前ご紹介したことがある <a href="https://adndevblog.typepad.com/technology_perspective/2020/09/displaying-per-level-on-forge-viewer.html" rel="noopener" target="_blank">Forge Viewer：レベル別の表示</a>&#0160;の <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#setcutplanes-planes" rel="noopener" target="_blank">Viewer3D.setCutPlanes</a>&#0160;メソッドとは異なり、three.js の THREE.Vector4 ではなく、軸平行境界ボックス(AABB:Axis-Aligned Bounding Box）のかたちで指定します。</p>
<div>
<blockquote>
<div>_viewer.loadDocumentNode(viewerDocument, viewables[0], {</div>
<div><strong>&#0160; &#0160; filter: { &#0160;</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; spatial_query: {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#39;$encloses&#39;: [{ &#39;aabox&#39;: [-130, -105, 0, 130, 105, 20] }]</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; }</strong></div>
<div>}).then(i =&gt; {</div>
<div>&#0160; &#0160; // something</div>
<div>});</div>
</blockquote>
</div>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a82be3200c-pi" style="display: inline;"><img alt="Spatial_filter" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a82be3200c image-full img-responsive" src="/assets/image_847029.jpg" title="Spatial_filter" /></a></p>
<p><strong>メタデータ フィルタ（property_query）</strong></p>
<p style="padding-left: 40px;">メタデータ・フィルタは、<a href="https://aps.autodesk.com/en/docs/acc/v1/tutorials/model-properties/query-ref/" rel="noopener" target="_blank">Model Properties クエリ言語</a>に利用して指定します。プロパティには、それぞれ対応するハッシュを用いる必要があるので、まず、フィルタで指定するプロパティのハッシュを取得する必要があります。</p>
<p style="padding-left: 40px;">ハッシュの取得には、モデルを Viewer に開いた状態で、デベロッパー ツールのコンソールから <a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/Model/#getpropertyhashes-namere-categoryre" rel="noopener" target="_blank">getPropertyHashes</a> メソッドを呼び出してハッシュテーブルを得ることで確認することが出来ます。この際、表示中の Viewer インスタンスアクセスには、<a href="https://adndevblog.typepad.com/technology_perspective/2021/12/access_viewer_instance.html" rel="noopener" target="_blank">コンソールの利用と Viewer インスタンスへのアクセス</a>&#0160;でご案内した <strong>NOP_VIEWER</strong> を利用することが可能です。</p>
<p style="padding-left: 40px;">例えば、「文字」カテゴリの「仕様 名称」プロパティをフィルタで使用する場合には、次のように指定します。</p>
<blockquote>
<p>props = await NOP_VIEWER.model.getPropertyHashes();</p>
<p>props.find(record =&gt; record[1] === &#39;仕様 名称&#39; &amp;&amp; record[2] === &#39;文字&#39;);</p>
</blockquote>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39d64ea200c-pi" style="display: inline;"><img alt="Hash" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39d64ea200c image-full img-responsive" src="/assets/image_860155.jpg" title="Hash" /></a></p>
<p style="padding-left: 40px;">もし、コンソール上で次のようなエラーが表示されるようなら、Viewer カンバスを定義する HTML の CDN 参照の URL から「.min」を削除して、ページを再ロード後に再度試してみてください。&#0160;</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25693d9200d-pi" style="display: inline;"><img alt="Conssole_error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25693d9200d image-full img-responsive" src="/assets/image_961458.jpg" title="Conssole_error" /></a></p>
<blockquote>
<p>&lt;link type=&quot;text/css&quot; rel=&quot;stylesheet&quot; href=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/style<span style="text-decoration: line-through;"><strong>.min</strong></span>.css&quot;&gt;<br />&lt;script type=&quot;text/javascript&quot; src=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/viewer3D<span style="text-decoration: line-through;"><strong>.min</strong></span>.js&quot;&gt;&lt;/script&gt;</p>
</blockquote>
<p style="padding-left: 40px;">上記モデルの場合、「Text」カテゴリの「仕様 名称」プロパティのハッシュが &#39;<strong>p6e5e93bd</strong>&#39; になっていることがわかります。このハッシュを用いたメタデータ フィルタで、「仕様 名称」プロパティの値が「ELV」の要素をフィルタすると、エレベータ扉のみがロード、表示されます。</p>
<div>
<blockquote>
<div>_viewer.loadDocumentNode(viewerDocument, viewables[0], {</div>
<div><strong>&#0160; &#0160; filter: { &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; property_query: [</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#39;$like&#39;: [&#39;s.props.p6e5e93bd&#39;, &quot;&#39;ELV&#39;&quot;]</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; ]</strong></div>
<div><strong>&#0160; &#0160; }</strong></div>
<div>}).then(i =&gt; {</div>
<div>&#0160; &#0160; // something</div>
<div>});</div>
</blockquote>
</div>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cabb4a200b-pi" style="display: inline;"><img alt="Property_filter1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cabb4a200b image-full img-responsive" src="/assets/image_916412.jpg" title="Property_filter1" /></a></p>
<p style="padding-left: 40px;">メタデータ フィルタは、配列になっているので、複数のフィルタを指定することも出来るようになっています。「仕様 名称」プロパティの値に「壁」の文字が含まれるフィルタ（%壁%）を追加で指定すると、次のような結果を得ることが出来ます。</p>
<div style="padding-left: 40px;">
<div>_viewer.loadDocumentNode(viewerDocument, viewables[0], {</div>
<div><strong>&#0160; &#0160; filter: { &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; property_query: [</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#39;$like&#39;: [&#39;s.props.p6e5e93bd</strong><strong>&#39;, &quot;&#39;%壁%&#39;&quot;]</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#39;$like&#39;: [&#39;s.props.p6e5e93bd&#39;, &quot;&#39;ELV&#39;&quot;]</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; ]</strong></div>
<div><strong>&#0160; &#0160; }</strong></div>
<div>}).then(i =&gt; {</div>
<div>&#0160; &#0160; // something</div>
<div>});</div>
<div>&#0160;</div>
<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a82c78200c-pi" style="display: inline;"><img alt="Property_filter2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a82c78200c image-full img-responsive" src="/assets/image_492822.jpg" title="Property_filter2" /></a></div>
<div>&#0160;</div>
<div><strong>複合フィルタ</strong></div>
<p style="padding-left: 40px;">空間フィルタとメタデータ フィルタを組み合わせることも出来ます。</p>
<div>
<blockquote>
<div>_viewer.loadDocumentNode(viewerDocument, viewables[0], {</div>
<div><strong>&#0160; &#0160; filter: { &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; property_query: [</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#39;$like&#39;: [&#39;s.props.p6e5e93bd&#39;, &quot;&#39;%壁%&#39;&quot;]</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#39;$like&#39;: [&#39;s.props.p6e5e93bd&#39;, &quot;&#39;ELV&#39;&quot;]</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; ],</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; spatial_query: {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#39;$encloses&#39;: [{ &#39;aabox&#39;: [-130, -105, 0, 130, 105, 20] }]</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; }</strong></div>
<div>}).then(i =&gt; {</div>
<div>&#0160; &#0160; // something</div>
<div>});</div>
</blockquote>
</div>
<div style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25693b1200d-pi" style="display: inline;"><img alt="Combined_filter" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25693b1200d image-full img-responsive" src="/assets/image_604718.jpg" title="Combined_filter" /></a></div>
<p>By Toshiaki Isezaki</p>
