---
layout: "post"
title: "View and Data API チュートリアル ～ その5 ～ パネル表示"
date: "2016-04-08 00:31:11"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/04/view-and-data-api-tutorial-part5-displaying-panel.html "
typepad_basename: "view-and-data-api-tutorial-part5-displaying-panel"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span></p>
<p>引き続き、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part4-object-selection.html" target="_blank">前回</a></strong>まで作成した Extension を拡張していきます。View and Data API では、表示領域にカスタムなボタンやユーザ インタフェースを表示させることも出来ます。今回は、前回、オブジェクト選択の機能で拡張した Extension に、選択したオブジェクトのプロパティを表示するパネル表示機能を実装していきます。なお、ここで紹介する内容は、<strong><a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data" target="_blank">Autodesk View &amp; Data API – Getting Started Tutorial リポジトリ</a></strong>&#0160;の<a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data/blob/master/chapter-3.md#Chapter3" target="_blank">&#0160;<strong>Chapter 3</strong>&#0160;</a>の Step 6 に該当するものです。</p>
<ol>
<li>&#0160;Viewing.Extension.Workshop.js を Adobe Brackets などで開いて、次の太字部分を追記します。<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;">  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">//  base class constructor</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>

  <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-smi">Extension</span>.<span class="pl-c1">call</span>(<span class="pl-v">this</span>, viewer, options);

  <span class="pl-k">var</span> _self <span class="pl-k">=</span> <span class="pl-v">this</span>;
  <span class="pl-k">var</span> _viewer <span class="pl-k">=</span> viewer;

 <strong> <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// create panel and set up inheritance</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>

  <span class="pl-c1">Viewing.Extension.Workshop</span>.<span class="pl-en">WorkshopPanel</span> <span class="pl-k">=</span> <span class="pl-k">function</span>(
    <span class="pl-smi">parentContainer</span>,
    <span class="pl-smi">id</span>,
    <span class="pl-smi">title</span>,
    <span class="pl-smi">options</span>)
  {
    <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">UI</span>.<span class="pl-smi">PropertyPanel</span>.<span class="pl-c1">call</span>(
      <span class="pl-v">this</span>,
      parentContainer,
      id, title);
  };

  <span class="pl-c1">Viewing.Extension.Workshop.WorkshopPanel</span>.<span class="pl-c1">prototype</span> <span class="pl-k">=</span> <span class="pl-c1">Object</span>.<span class="pl-en">create</span>(
    <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">UI</span>.<span class="pl-smi">PropertyPanel</span>.<span class="pl-c1">prototype</span>);

  <span class="pl-c1">Viewing.Extension.Workshop.WorkshopPanel</span>.<span class="pl-c1">prototype</span>.<span class="pl-en">constructor</span> <span class="pl-k">=</span>
    <span class="pl-smi">Viewing</span>.<span class="pl-smi">Extension</span>.<span class="pl-smi">Workshop</span>.<span class="pl-smi">WorkshopPanel</span>;
</strong>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// load callback: invoked when viewer.loadExtension is called</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>

  <span class="pl-c1">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {</span></pre>
</li>
<li>Extension のロードとロード解除を担当する load() と unload() 関数で、定義したパネルのインスタンス化と後処理を追記します。load() と unload() 関数内の太字部分を追記してください。<br />
<pre><span style="font-family: helvetica;">  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// load callback: invoked when viewer.loadExtension is called</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c1">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

    <span class="pl-smi">_viewer</span>.<span class="pl-en">addEventListener</span>(
      <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">SELECTION_CHANGED_EVENT</span>,
      <span class="pl-smi">_self</span>.<span class="pl-smi">onSelectionChanged</span>);

<strong>    <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span> <span class="pl-k">=</span> <span class="pl-k">new</span> <span class="pl-en">Viewing.Extension</span>.<span class="pl-smi">Workshop</span>.<span class="pl-en">WorkshopPanel</span> (
      <span class="pl-smi">_viewer</span>.<span class="pl-smi">container</span>,
      <span class="pl-s"><span class="pl-pds">&#39;</span>WorkshopPanelId<span class="pl-pds">&#39;</span></span>,
      <span class="pl-s"><span class="pl-pds">&#39;</span>Workshop Panel<span class="pl-pds">&#39;</span></span>);</strong>

    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop loaded<span class="pl-pds">&#39;</span></span>);

    <span class="pl-k">return</span> <span class="pl-c1">true</span>;
  };</span></pre>
<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;">  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// unload callback: invoked when viewer.unloadExtension is called</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c1">_self</span>.<span class="pl-en">unload</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

<strong>    <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">setVisible</span>(<span class="pl-c1">false</span>);
    <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">uninitialize</span>();</strong>

    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop unloaded<span class="pl-pds">&#39;</span></span>);

    <span class="pl-k">return</span> <span class="pl-c1">true</span>;
  };</span></pre>
</li>
<li>前回、オブジェクト選択で利用した&#0160;SELECTION_CHANGED イベントのイベント ハンドラ関数である&#0160;onSelectionChanged() の中身を次の内容で置き換えます。このコードは、選択したオブジェクトをビュー内に拡大表示して、そのプロパティをインスタンス化したパネルに表示します。<br /><br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;">  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// selection changed callback</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c1">_self</span>.<span class="pl-en">onSelectionChanged</span> <span class="pl-k">=</span> <span class="pl-k">function</span> (<span class="pl-smi">event</span>) {

   <strong> <span class="pl-k">function</span> <span class="pl-en">propertiesHandler</span>(<span class="pl-smi">result</span>) {

      <span class="pl-k">if</span> (<span class="pl-smi">result</span>.<span class="pl-smi">properties</span>) {
        <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">setProperties</span>(
        <span class="pl-smi">result</span>.<span class="pl-smi">properties</span>);
        <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">setVisible</span>(<span class="pl-c1">true</span>);
      }
    }

    <span class="pl-k">if</span>(<span class="pl-c1">event</span>.<span class="pl-smi">dbIdArray</span>.<span class="pl-c1">length</span>) {
      <span class="pl-k">var</span> dbId <span class="pl-k">=</span> <span class="pl-c1">event</span>.<span class="pl-smi">dbIdArray</span>[<span class="pl-c1">0</span>];

      <span class="pl-smi">_viewer</span>.<span class="pl-en">getProperties</span>(
        dbId,
        propertiesHandler);

      <span class="pl-smi">_viewer</span>.<span class="pl-en">fitToView</span>(dbId);
      <span class="pl-smi">_viewer</span>.<span class="pl-en">isolateById</span>(dbId);
    }
    <span class="pl-k">else</span> {
      <span class="pl-smi">_viewer</span>.<span class="pl-en">isolateById</span>([]);
      <span class="pl-smi">_viewer</span>.<span class="pl-en">fitToView</span>();
      <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">setVisible</span>(<span class="pl-c1">false</span>);
    }</strong>
  }</span></pre>
</li>
<li>Viewing.Extension.Workshop.js を上書き保存してから、Node.js で構築してある Web サーバーを localhost:3000 のURLで表示してみてください。選択したオブジェクトが拡大表示され、同時にプロパティが Workshop Panal のタイトルを持つパネルに表示されるはずです。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c990eb970d-pi" style="display: inline;"><img alt="Show_properties_on_panel" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c990eb970d image-full img-responsive" src="/assets/image_345152.jpg" title="Show_properties_on_panel" /></a></li>
</ol>
<p>ここまでの実装で、オブジェクト選択とパネル表示を Extension としてモジュール化する実装が完了しました。このように、Extension 単位でさまざまな機能を盛り込んでいくことができます。</p>
<p>さて、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/04/view-and-data-api-tutorial-part6-moving-camera.html" target="_blank">次回</a></strong>&#0160;は表示したオブジェクトにアニメーション機能を追加してみます。</p>
<p>By Toshiaki Isezaki</p>
