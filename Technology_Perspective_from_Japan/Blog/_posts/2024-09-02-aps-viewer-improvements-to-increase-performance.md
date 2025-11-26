---
layout: "post"
title: "APS Viewer：パフォーマンス向上に向けたさまざまな改良"
date: "2024-09-02 00:02:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/09/aps-viewer-improvements-to-increase-performance.html "
typepad_basename: "aps-viewer-improvements-to-increase-performance"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ba5ba4200c-pi" style="display: inline;"><img alt="Viewer_illustration" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ba5ba4200c image-full img-responsive" src="/assets/image_977337.jpg" title="Viewer_illustration" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2016/06/renamed-forge-platform-api-and-added-new-api.html" rel="noopener" target="_blank">Autodesk Forge の登場</a>以来、Autodesk Platform Services に名称を変更しても、Viewer は最も多用されている機能/API です。その後も、<a href="https://adndevblog.typepad.com/technology_perspective/2020/11/svf-and-svf2.html" rel="noopener" target="_blank">SVF2 サポート</a>&#0160;や <a href="https://adndevblog.typepad.com/technology_perspective/2023/06/viewer-feature-selective-loading.html" rel="noopener" target="_blank">選択ロード</a> に代表されるように、主に大規模 BIM モデルの表示を目的に、継続してパフォーマンスを改善する対応が施されてきました。</p>
<p>ここ最近も、集中して Viewer パフォーマンスを改善・改良が導入されていますので、簡単にまとめてご紹介しておきたいと思います。導入された内容は、公式ドキュメントの Changelog に記載があるものです。</p>
<ul>
<li><a href="https://aps.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/#id14" rel="noopener" target="_blank">v7.95</a></li>
</ul>
<p style="padding-left: 40px;">SVF2 モデルのロード時間を短縮・改善する機能が盛り込まれています。v7.95 以降の APS Viewer バージョンをお使いいただければ、機能享受することが出来ま。、同機能を明示的に有効/無効にする設定等はありません。</p>
<ul>
<li><a href="https://aps.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/#id9" rel="noopener" target="_blank">v7.96</a></li>
</ul>
<p style="padding-left: 40px;">URL パラメータ useOPFS=true を使用して有効化出来る、新しく改良されたジオメトリーキャッシュを導入しています。</p>
<p style="padding-left: 40px;">APS Viewer は、初めて SVF2 モデルを開くと、大量のジオメトリ データを取得します。この際、２度め以降のロードで再度同じデータを再度取得しないように、SVF2 を介してジオメトリデータをローカルキャッシュに保存します。このキャッシュは、これまで <a href="https://developer.mozilla.org/ja/docs/Web/API/IndexedDB_API/Using_IndexedDB" rel="noopener" target="_blank">IndexedDB</a> と呼ばれるブラウザ技術を使用してきました。ただし、特に Google Chrome では、IndexedDB は大量のデータに対してパフォーマンス上の問題を抱えており、初回のモデル読み込み時間が大幅に遅くなる場合がありました。</p>
<p style="padding-left: 40px;">この問題を修正するために、<a href="https://developer.mozilla.org/ja/docs/Web/API/File_System_API/Origin_private_file_system" rel="noopener" target="_blank">OPFS（Origin Private File System）</a>キャッシュと呼ばれる新しいキャッシュ機構がバックエンドのリソースが実装されました。OPFS はハードドライブ上のファイルのようなストレージへの高性能アクセスを提供します。OPFS を活用することで、IndexedDB のデータベースライクな API に関連するオーバーヘッドが排除され、Chromeで発生するパフォーマンスの低下が解消されます。</p>
<p style="padding-left: 40px;">ただし、OPFS キャッシュには独自の制限があることに注意することが重要です。&#0160;Viewer は、デザインの系統 （ACC または BIM 360 の特定のデザインのすべてのバージョン) 毎に 1 つの OPFS キャッシュを作成します。系統がない場合は URN ごとに 1 つの&#0160; OPFS キャッシュを作成します。</p>
<p style="padding-left: 40px;">IndexedDB を使ったキャッシュバックエンドと比較すると、OPFS キャッシュは初回モデルの読み込み時間が小規模キャッシュで１.１〜１.７ 倍、中規模キャッシュでは約２〜７ 倍、大規模キャッシュでは更に改善される可能性があります。２度め以降のモデル ロード時間では、小規模キャッシュで２～４ 倍、中規模キャッシュで３～６倍、大規模キャッシュではさらに短縮されます。大規模なモデルには、50k から 100万以上のユニークなジオメトリを含めることができます。</p>
<p style="padding-left: 40px;">ここで言う小規模キャッシュや中規模キャッシュとは、キャッシュに含まれるジオメトリ数を表すために使用されていることにご注意ください。通常、小規模キャッシュには最大で数千のジオメトリが含まれますが、中規模キャッシュには約 250 万のジオメトリが含まれます。</p>
<p style="padding-left: 40px;">※ キャッシュ方式の変更で SVF2 のオフライン運用が可能になるわけではありません。</p>
<p style="padding-left: 40px;">なお、<a href="https://aps.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/#id2" rel="noopener" target="_blank">v7.98</a> 以降のバージョンでは、既定でこの機能が有効になっているので、明示的なパラメーター指定は不要です。</p>
<ul>
<li><a href="https://aps.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/#id1" rel="noopener" target="_blank">v7.99</a>（Beta）</li>
</ul>
<p style="padding-left: 40px;">Viewer の設定パネルに「大規模モデルの操作性」設定が新設されて、大規模モデルのレンダリングを最適化するスムーズ ナビゲーション機能を利用することが出来ます。スムーズ ナビゲーション機能は、まだ、開発テストという段階なので、Beta 版扱いになっています。既定ではオフに設定されているので、利用には明示的な有効化指定が必要です。また、この機能は、スマートフォンやタブレットなどのモバイル デバイスではお使いいただけません。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0ce29ce200d-pi"><img alt="Smooth_navigation_settings" class="asset  asset-image at-xid-6a0167607c2431970b02dad0ce29ce200d img-responsive" src="/assets/image_199961.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Smooth_navigation_settings" /></a></p>
<p style="padding-left: 40px;">機能が有効になると、モデルのロード/表示時に Viewer 左下に表示されるプログレスバー上に「読み込み中」&gt;&gt;「最適化中」&gt;&gt;「レンダリング中」と表示されるようになります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bdf371200b-pi" style="display: inline;"><img alt="Transition" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bdf371200b image-full img-responsive" src="/assets/image_185095.jpg" title="Transition" /></a></p>
<p style="padding-left: 40px;"><strong>スムーズ ナビゲーション機能オフ<br /></strong><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/_dWNRm9HOTk" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>スムーズ ナビゲーション機能オン<br /></strong><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/4XeoUfhkaNw" width="480"></iframe></p>
<p style="padding-left: 40px;">設定変更後は、念のため、ブラウザ自体を再起動することをお勧めします。</p>
<p style="padding-left: 40px;">スムーズ ナビゲーション機能が提供する最適化は Beta 版扱いで、あらゆるモデルに対応しているわけではありません。設定パネルから「大規模モデルの操作性」設定項目から非表示にする機能フラグも提供しています。Autodesk.Viewing.Initializer() の呼び出し前に次の行を挿入してください。</p>
<div>
<blockquote>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; :</span></div>
<div><span style="font-size: 10pt;">var options = {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; env: &#39;AutodeskProduction2&#39;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; api: &#39;streamingV2&#39;,</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; getAccessToken: getCredentials</span></div>
<div><span style="font-size: 10pt;">};</span></div>
<div><span style="color: #0000ff;"><strong><span style="font-size: 10pt;">Autodesk.Viewing.FeatureFlags.set(&#39;HIDE_LARGE_MODEL_EXPERIENCE&#39;, true);</span></strong></span></div>
<div><span style="font-size: 10pt;">Autodesk.Viewing.Initializer(options, function () {</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; _viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;viewer3d&#39;));</span></div>
<div><span style="font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; :</span></div>
</blockquote>
</div>
<p>上記いずれも、SVF（SVF1 とも呼称する場合あり）ではなく、SVF2 を利用した場合の効果を得られるものです。ご注意ください。</p>
<p>By Toshiaki Isezaki</p>
