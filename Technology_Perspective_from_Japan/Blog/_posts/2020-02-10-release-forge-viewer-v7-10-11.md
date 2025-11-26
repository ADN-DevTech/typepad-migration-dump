---
layout: "post"
title: "Forge Viewer バージョン 7.10/7.11 リリース"
date: "2020-02-10 00:06:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/02/release-forge-viewer-v7-10_11.html "
typepad_basename: "release-forge-viewer-v7-10_11"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b025d9b377959200c-pi" style="float: right;"><img alt="Viewer-api-blue" class="asset  asset-image at-xid-6a0167607c2431970b025d9b377959200c img-responsive" src="/assets/image_578687.jpg" style="width: 150px; margin: 0px 0px 5px 5px;" title="Viewer-api-blue" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ed0e56200d-pi" style="float: right;"></a>Forge Viewer の バージョン 7.10 と 7.11 がリースされていますので、簡単ですがご案内しておきたいと思います。</p>
<hr />
<p>最初に、バージョン 7.10 の内容です。</p>
<h3>変更<strong>された項目</strong></h3>
<ul>
<li>パフォーマンスの改善と同時に PDF の新しいクリッピング実装が追加されています。</li>
</ul>
<h3><strong>追加された項目</strong></h3>
<ul>
<li>分解機能の使用時に使用するアルゴリズム（放射状または階層状）を制御する新しい設定値 <code>avp.Prefs3D.EXPLODE_STRATEG</code>が追加されています。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4e594a4200d-pi" style="display: inline;"><img alt="Explode" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4e594a4200d img-responsive" src="/assets/image_675402.jpg" title="Explode" /></a></li>
<li>PDF にブレンドモードのサポートが追加されています。</li>
<li>このバージョンは、WebGL2 を使用して初期化を試み、WebGL1 にフォールバックします。</li>
</ul>
<hr />
<p>続いて、バージョン 7.11 の内容です。</p>
<h3><strong>変更された項目</strong></h3>
<ul>
<li>Minimap3D Extension: &#39;Autodesk.AEC.Minimap3D&#39; は、新しいレベル選択ドロップダウン UI を実装しています。</li>
</ul>
<h3><strong>追加された項目</strong></h3>
<ul>
<li>ビューアのサイズが変更されるたびに呼び出されるコールバック関数を登録するための <code>GuiViewer3D.registerCustomizeToolbarCB(callbackFunction) </code>ソッドが追加されています。 コールバック関数の書式は、<code>関数名 (viewer, width-幅, height-高さ)</code>&#0160;です。コールバック関数は、新しい拡張機能がロードされるたびに呼び出されます。</li>
<li>PDF ファイルのロード時にラインアニメーションを有効/無効にする設定パネル オプションが追加されています。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4bc57f2200c-pi" style="display: inline;"><img alt="Pdf_ami" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4bc57f2200c img-responsive" src="/assets/image_650340.jpg" title="Pdf_ami" /></a></li>
<li>プログラムで使用してPDF ファイルのラインアニメーションを有効にするかどうかを制御するプロファイルの設定値 <code>avp.Prefs2D.LOADING_ANIMATION&#0160;</code>が追加されています。</li>
<li>ユーザが 2D シートからオブジェクトを選択して3D で表示できるようにするコンテキスト メニュー エントリが追加されています。</li>
<li><code>viewer.addEventListener(eventId, callback, options) </code>での options.priority がサポートされています。これによって、 優先度の高いコールバック関数を最初に呼び出すことができます。</li>
<li><code>MeasureExtension.setMeasurements()</code> 関数は、<code>MeasureExtension.getMeasurementList()</code>&#0160; API によって保存された測定値を復元します。</li>
<li>PDF 塗りつぶしイメージ パターンのレンダリングをサポートしています。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4bc58fc200c-pi" style="display: inline;"><img alt="2020-02-09_14-52-36" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4bc58fc200c image-full img-responsive" src="/assets/image_967707.jpg" title="2020-02-09_14-52-36" /></a></li>
</ul>
<hr />
<p>By Toshiaki Isezaki</p>
