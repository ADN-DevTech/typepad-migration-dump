---
layout: "post"
title: "Forge Viewer バージョン 7.8/7.9 リリース"
date: "2019-12-23 00:07:45"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/12/release-forge-viewer-v7-8_9.html "
typepad_basename: "release-forge-viewer-v7-8_9"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4abfb6f200c-pi" style="float: right;"><img alt="Viewer-api-blue" class="asset  asset-image at-xid-6a0167607c2431970b0240a4abfb6f200c img-responsive" src="/assets/image_778817.jpg" style="width: 150px; margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4d51fe8200d-pi" style="float: right;"></a>少しバージョンをスキップしてしまいましたが、Forge Viewer の バージョン 7.8 と 7.9 がリースされていますので、簡単ですがご案内しておきたいと思います。</p>
<hr />
<p>最初に、バージョン 7.8 の内容です。</p>
<h3><strong>追加された項目</strong></h3>
<ul>
<li>Viewer3D.lockVisible(dbids, lock, model) - 親ノードの dbId をオフにしても可視性をオンのままにします。</li>
<li>Viewer3D.lockExplode(dbids, lock, model) - 特定の dbId のノードを分解操作から除外します。</li>
<li>Viewer3D.lockSelection(dbIds, lock, model) - 特定の dbId のノードを選択とハイライト表示から除外します。</li>
<li>BimWalk Extension の重力の有無を切り替えるチェックボッスが追加されています。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4abfcce200c-pi" style="display: inline;"><img alt="Bimwalk_gravity" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4abfcce200c img-responsive" src="/assets/image_770080.jpg" title="Bimwalk_gravity" /></a></li>
<li>options.env での <code>FluentProductionEU</code> と <code>FluentStagingEU</code>値をサポートが追加されています。</li>
<li>計測モードの開始時、また、終了時に新しいイベントAutodesk.Viewing.MeasureCommon.Events.MEASUREMENT_MODE_ENTER イベントとAutodesk.Viewing.MeasureCommon.Events.MEASUREMENT_MODE_LEAVEをそれぞれ発行します。</li>
</ul>
<h3><strong>削除された項目</strong></h3>
<ul>
<li>BubbleNode.getPlacementTransform と BubbleNode.getHash 関数が廃止されています。</li>
</ul>
<hr />
<p>続いて、バージョン 7.9 の内容です。</p>
<h3><strong>追加された項目</strong></h3>
<ul>
<li>クリップ領域をパスアウトラインに適用して、2D表示ソリューションを改善します。</li>
<li>インライン画像グループを含むPDFのパフォーマンスが向上しました。</li>
<li>PDF のプロパティをロードするサポート。</li>
<li>マニフェストのリクエスト時に <code>bypassds</code> フラグの指定が追加されました。</li>
</ul>
<hr />
<p>By Toshiaki Isezaki</p>
