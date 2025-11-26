---
layout: "post"
title: "Forge Viewer バージョン 7.1 リリース"
date: "2019-08-05 00:02:50"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/08/release-forge-viewer-v7-1.html "
typepad_basename: "release-forge-viewer-v7-1"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49be850200d-pi" style="float: right;"><img alt="Viewer-api-blue" class="asset  asset-image at-xid-6a0167607c2431970b0240a49be850200d img-responsive" src="/assets/image_145037.jpg" style="margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49bbe45200d-pi" style="float: right;"></a>先日、Forge Viewer の新バージョン 7.0 がリリースされたばかりですが、今回、新バージョン となるバージョン7.1 がリリースされましたので、簡単ですがご案内しておきたいと思います。</p>
<hr />
<p><strong>変更された項目</strong></p>
<ul>
<li>PDF ファイルのハイパーリンクはマウス ホバーでリンク先ページのプレビューを表示します。</li>
<li><strong>Autodesk.Section</strong> extension は、<strong>viewer.setCutPlanes()</strong> によって設定された切断面を考慮に入れるようになりました。</li>
<li>プログレスバーは、メモリ制限のある Extension がアクティブになっている（ロードされているだけではない）場合にのみ青く表示されます。</li>
<li>BimWalk はセクションツールを無効にしません。</li>
<li>マークアップエラーハンドラは、第2パラメータとしてエラーキーを返します。</li>
</ul>
<p><strong>追加された項目</strong></p>
<ul>
<li><strong>Autodesk.BIM360.GestureDocumentNavigation</strong> Extension が追加されています。</li>
<li><strong>Autodesk.BIM360.RollCamera</strong> Extension が追加されています。</li>
<li><strong>Autodesk.BIM360.Minimap</strong> Extension が追加されています。</li>
<li><strong>Extension.load()</strong> は Promise を返すようになりました。</li>
<li><strong>Autodesk.DocumentBrowser</strong> Eextension にサムネイル ビューが追加されています。</li>
<li>コンテキストメニュー オプションの ”断面ボックス” で選択したオブジェクトの周囲に断面ボックスを配置します。</li>
<li>コンテキストメニュー オプション ”Section Plane（断面プレーン）” 選択した点の断面平面を配置します。</li>
</ul>
<p style="text-align: left; padding-left: 160px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4729140200c-pi" style="display: inline;"> <img alt="Context_menu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4729140200c img-responsive" src="/assets/image_669731.jpg" title="Context_menu" /></a></p>
<ul>
<li><strong>viewer.getState() </strong>メソッド、および、<strong>viewer.restoreState()</strong> メソッドに対して複数モデルがサポートされました。</li>
<li><strong>LOADER_LOAD_FILE_EVENT </strong>イベントが追加されました。</li>
<li><strong>AGGREGATE_HIDDEN_CHANGED_EVENT&#0160;</strong>イベントが追加されました。</li>
<li>結果セットに externalId データを含めないようにする model.getProperties2(...) メソッドが追加されています。 これにより、プロパティデータの取得が高速化され、externalId 値が不要な場合のメモリ使用量が削減されます。</li>
<li><strong>model.getBulkProperties2(...)</strong> メソッドにも <strong>model.getProperties2(...)</strong> 同様の利点が追加されています。</li>
</ul>
<hr />
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
