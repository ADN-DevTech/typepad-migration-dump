---
layout: "post"
title: "Forge Viewer バージョン 7.0 リリース"
date: "2019-07-12 08:53:31"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/07/release-forge-viewer-v7-0.html "
typepad_basename: "release-forge-viewer-v7-0"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46e9b70200c-pi" style="float: right;"><img alt="Viewer-api-blue" class="asset  asset-image at-xid-6a0167607c2431970b0240a46e9b70200c img-responsive" src="/assets/image_715564.jpg" style="margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46e4a6d200c-pi" style="float: right;"></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4978451200d-pi" style="float: right;"></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4976ddf200d-pi" style="float: right;"></a>Forge Viewer の新バージョン 7.0 がリリースされています。今回のバージョンは、今後の拡張を見据えて大幅な変更が加えられているため、Forge Viewer を利用している既存の Forge アプリは、一定程度の移植作業が必要になる場合があります。</p>
<p>もし、Forge アプリをお持ちの場合、バージョン 7.0 は、旧バージョンとの非互換リリースとなりますので、ご注意ください。以前のバージョンを利用し続ける場合には、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/another-way-to-specify-forge-viewer-version.html" rel="noopener" target="_blank">Forge Viewer バージョン指定のもう1つの方法</a></strong>&#0160;をご確認ください。</p>
<hr />
<p><strong>変更された項目</strong></p>
<ul>
<li>ViewingApplication は、独自の JavaScriptファイルにバンドルされています。 &gt;&gt; <strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/migration_guide_v6_to_v7/" rel="noopener" target="_blank">Migration Guide</a></strong> を参照してください。</li>
<li>viewer.loadModel() API は非同期になり、Promise を返すようになっています。</li>
<li>ノン フォト リアリスティック レンダリング機能は、Autodesk.NPR&#0160; extension に移動しています。</li>
<li>ViewCube API は、Autodesk.ViewCubeUi extension に移動しています。</li>
<li>Document.getMessagesはプレーンな GUID を受け付けなくなりました。 対応する BubbleNode を取得するには、Document.getRoot().findByGuid() を使用してください。</li>
<li>Document.getViewGeometry は、JSON の代わりに BubbleNode を返します。</li>
<li>Pushpin のサムネイルは、拡張機能のオプションで generateIssueThumbnail または generateRFIThumbnail を指定した場合にのみ生成されます。</li>
</ul>
<p><strong>追加された項目</strong></p>
<ul>
<li><span class="hljs-keyword">JavaScript バンドル viewerCE.min.js、SVF および F2D モデルをロードするのに十分な機能を備えたコンパクトなライブラリ。</span></li>
<li><span class="hljs-keyword">アプリケーションを適切にシャットダウンし、メモリを解放するグローバル シャットダウン API Autodesk.Viewing.shutdown()。</span></li>
<li><span class="hljs-keyword">ビューアのスクリーン ショットをキャプチャする静的関数を含む名前空間 Autodesk.Viewing.ScreenShot。</span></li>
<li><span class="hljs-keyword">model.getPropertyDb() への第2引数 userData &gt;&gt;&#0160; executeUserFunction(userFunction、userData)。</span></li>
<li><span class="hljs-keyword">set2dSelectionColor() への第2引数の opacity &gt;&gt; set2dSelectionColor(newTHREE.Color(r, g, b), opacity)。</span></li>
<li><span class="hljs-keyword">ツールバーを垂直方向に表示するオプション new avu.ToolBar(&#39;toolbar-id&#39;、{alignVertically：true});。</span></li>
<li><span class="hljs-keyword">Extension.onToolbarCreated（ツールバー）API。<br />VertexBufferReader へのコンパクトな頂点バッファのサポート。</span></li>
<li><span class="hljs-keyword">viewer.overlays - オーバーレイシーン、メッシュなどを追加するための API の公開。</span></li>
<li><span class="hljs-keyword">viewer.unloadModel()、viewer.hitTest()、および viewer.refresh()。</span></li>
<li><span class="hljs-keyword">モデル内の特定のフラグメントの FragmentPointer クラスを</span><span class="hljs-keyword">公開する model.getFragmentPointer() 。</span></li>
<li><span class="hljs-keyword">ズーム ドリーの速度を変更する&#0160; API：</span></li>
</ul>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-0"><span class="hljs-keyword">var</span> tool = viewer.toolController.getTool(<span class="hljs-string">&#39;dolly&#39;</span>);
tool.setDollyDragScale(value); <span class="hljs-comment">//Drag Speed</span>
tool.setDollyScrollScale(value); <span class="hljs-comment">//Scroll Speed</span></code></pre>
<ul>
<li>設定にズーム速度とスクロール ホイール速度のスライダ コントロールｦ追加。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49ace82200d-pi" style="display: inline;"><img alt="Zoom_settings" class="asset  asset-image at-xid-6a0167607c2431970b0240a49ace82200d img-responsive" src="/assets/image_675777.jpg" style="width: 800px;" title="Zoom_settings" /></a></li>
<li>カナダ - フランス語のローカル言語サポート。</li>
<li>ピンチ ジェスチャ サポートを Autodesk.BimWalk extension へ移動</li>
<li>設定パネルからアクセス可能なパネルでの Autodesk.Fusion360.Animation extension の設定。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4bf76ec200b-pi" style="display: inline;"><img alt="Animation_settings" class="asset  asset-image at-xid-6a0167607c2431970b0240a4bf76ec200b img-responsive" src="/assets/image_66857.jpg" style="width: 710px;" title="Animation_settings" /></a></li>
<li>Autodesk.DiffToo extension に対するヘッドレスビューアのサポート。</li>
<li>設定パネルからアクセスできる&#0160; Autodesk.NPR extension の設定。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4bf7700200b-pi" style="display: inline;"><img alt="Nfr_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4bf7700200b image-full img-responsive" src="/assets/image_849050.jpg" title="Nfr_settings" /></a></li>
<li>3D ミニマップのパン機能。</li>
</ul>
<p><strong>削除された項目</strong></p>
<ul>
<li>viewer.getToolbar(true) はツールバーを作成しなくなりました。関数はパラメータを取りません。</li>
<li>viewer.getToolbarProm()：Promise &gt;&gt;&#0160;<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/migration_guide_v6_to_v7/" rel="noopener" target="_blank">Migration Guide</a></strong> を参照してください。</li>
<li>viewer.playAnimation(callback) &gt;&gt;&#0160;<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/migration_guide_v6_to_v7/" rel="noopener" target="_blank">Migration Guide</a></strong> を参照してください。</li>
<li>Document.getViewableItems(document) - Document.getRoot() を使用してください。代わりに findAllViewables() を使用してください。</li>
<li>Document.getItemById(id) - Document.getRoot()。findByGuid() を使用してください。</li>
<li>Document.getPropertyDbPath - Viewer3D.loadModelを直接呼び出し、プロパティを自動的に設定する loadDocuementNode を使用しない場合は、Document.getFullPath(Document.getRoot()。findPropertyDbPath()) を使用してこのフィールドに値を設定します。</li>
<li>Document.getRootItem - Document.getRoot() を使用してください。</li>
<li>Autodesk.BIM360.Extension.PushPin extension の createItem(data) メソッドは&#0160; loadItems([data]) に置き換えられました。</li>
<li>RESET_EVENT - コードのどの部分もイベントを発生させていません。それに応答するクラスはほとんどありません。</li>
<li>viewer.load() は viewer.loadModel() に置き換えられました。</li>
<li>viewer.isolateById() は viewer.isolate() に置き換えられました。</li>
<li>viewer.displayHomeandInfoButton()。</li>
<li>MarkupsCoreUtils.getClipPathId() は MarkupsCoreUtils.getUniqueID() に置き換えられました。</li>
</ul>
<hr />
<p>Forge Viewer バージョン 6 から バージョン 7 への具体的な関数マッピング情報が Forge ポータルに用意されていましたので、関数などの移植情報は <strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/migration_guide_v6_to_v7/" rel="noopener" target="_blank">Migration Guide</a></strong> をご参照ください。</p>
<p>By Toshiaki Isezaki</p>
