---
layout: "post"
title: "View and Data API チュートリアル ～ その6 ～ カメラ移動"
date: "2016-04-15 00:11:34"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/04/view-and-data-api-tutorial-part6-moving-camera.html "
typepad_basename: "view-and-data-api-tutorial-part6-moving-camera"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span></p>
<p>この記事では、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/04/view-and-data-api-tutorial-part5-displaying-panel.html" target="_blank">前回</a></strong>まで作成してきた Extension を改良して、選択したオブジェクトの周囲を旋回するカメラ（視点）を設定します。カメラの移動は、View and Data API のベースになっている&#0160;<strong><a href="http://threejs.org/" target="_blank">three.js</a></strong>&#0160;の要素を利用して、タイマーを用いた一定間隔のマトリックス変換で実装しています。</p>
<p>なお、<strong><a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data" target="_blank">Autodesk View &amp; Data API – Getting Started Tutorial リポジトリ</a></strong>&#0160;では、この内容はボーナス ステップとなっています。アニメーションを実装する必要ない場合には、スキップしていただいても問題はありません。</p>
<ol>
<li>まず、Extension のロード時に呼び出される load() 関数内に、カメラ移動の間隔を保持する変数を用意します。<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;"> <span class="pl-smi">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

    <span class="pl-smi">_viewer</span>.<span class="pl-en">addEventListener</span>(
      <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">SELECTION_CHANGED_EVENT</span>,
      <span class="pl-smi">_self</span>.<span class="pl-smi">onSelectionChanged</span>);

    <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span> <span class="pl-k">=</span> <span class="pl-k">new</span> <span class="pl-en">Viewing.Extension</span>.<span class="pl-smi">Workshop</span>.<span class="pl-en">WorkshopPanel</span> (
      <span class="pl-smi">_viewer</span>.<span class="pl-smi">container</span>,
      <span class="pl-s"><span class="pl-pds">&#39;</span>WorkshopPanelId<span class="pl-pds">&#39;</span></span>,
      <span class="pl-s"><span class="pl-pds">&#39;</span>Workshop Panel<span class="pl-pds">&#39;</span></span>);

    <strong><span class="pl-smi">_self</span>.<span class="pl-smi">interval</span> <span class="pl-k">=</span> <span class="pl-c1">0</span>;</strong>

    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop loaded<span class="pl-pds">&#39;</span></span>);

    <span class="pl-k">return</span> <span class="pl-c1">true</span>;
  };</span></pre>
</li>
<li>オブジェクト選択で使用した&#0160;onSelectionChanged() のスコープの後に、アニメーションを処理する実装を追記します。<br />
<pre><strong><span style="font-family: tahoma, arial, helvetica, sans-serif;">    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
    <span class="pl-c">// rotate camera around axis with center origin</span>
    <span class="pl-c">//</span>
    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
    <span class="pl-smi">_self</span>.<span class="pl-en">rotateCamera</span> <span class="pl-k">=</span> <span class="pl-k">function</span>(<span class="pl-smi">angle</span>, <span class="pl-smi">axis</span>) {
      <span class="pl-k">var</span> pos <span class="pl-k">=</span> <span class="pl-smi">_viewer</span>.<span class="pl-smi">navigation</span>.<span class="pl-en">getPosition</span>();

      <span class="pl-k">var</span> position <span class="pl-k">=</span> <span class="pl-k">new</span> <span class="pl-en">THREE.Vector3</span>( <span class="pl-c">// Point?</span>
        <span class="pl-smi">pos</span>.<span class="pl-c1">x</span>, <span class="pl-smi">pos</span>.<span class="pl-c1">y</span>, <span class="pl-smi">pos</span>.<span class="pl-c1">z</span>);

      <span class="pl-k">var</span> rAxis <span class="pl-k">=</span> <span class="pl-k">new</span> <span class="pl-en">THREE.Vector3</span>(
        <span class="pl-smi">axis</span>.<span class="pl-c1">x</span>, <span class="pl-smi">axis</span>.<span class="pl-c1">y</span>, <span class="pl-smi">axis</span>.<span class="pl-c1">z</span>);

      <span class="pl-k">var</span> matrix <span class="pl-k">=</span> <span class="pl-k">new</span> <span class="pl-en">THREE.Matrix4</span>().<span class="pl-en">makeRotationAxis</span>(
        rAxis,
        angle);

      <span class="pl-smi">position</span>.<span class="pl-en">applyMatrix4</span>(matrix);

      <span class="pl-smi">_viewer</span>.<span class="pl-smi">navigation</span>.<span class="pl-en">setPosition</span>(position);
    };

    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
    <span class="pl-c">// start rotation effect</span>
    <span class="pl-c">//</span>
    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>

    <span class="pl-smi">_self</span>.<span class="pl-en">startRotation</span> <span class="pl-k">=</span> <span class="pl-k">function</span>() {
      <span class="pl-c1">clearInterval</span>(<span class="pl-smi">_self</span>.<span class="pl-smi">interval</span>);

      <span class="pl-c">// sets small delay before starting rotation</span>

      <span class="pl-c1">setTimeout</span>(<span class="pl-k">function</span>() {
        <span class="pl-smi">_self</span>.<span class="pl-smi">interval</span> <span class="pl-k">=</span> <span class="pl-c1">setInterval</span>(<span class="pl-k">function</span> () {
        <span class="pl-smi">_self</span>.<span class="pl-en">rotateCamera</span>(<span class="pl-c1">0.05</span>, {x<span class="pl-k">:</span><span class="pl-c1">0</span>, y<span class="pl-k">:</span><span class="pl-c1">1</span>, z<span class="pl-k">:</span><span class="pl-c1">0</span>});
        }, <span class="pl-c1">100</span>)}, <span class="pl-c1">500</span>);
    };</span></strong></pre>
</li>
<li><span style="font-family: tahoma, arial, helvetica, sans-serif;"><span style="font-family: tahoma, arial, helvetica, sans-serif;">最後に、オブジェクトを選択した際にアニメーションが起動するよう、onSelectionChanged() 関数の内容を書き換えます。<br /></span></span>
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;">  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// selection changed callback</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-smi">_self</span>.<span class="pl-en">onSelectionChanged</span> <span class="pl-k">=</span> <span class="pl-k">function</span> (<span class="pl-c1">event</span>) {

    <span class="pl-k">function</span> <span class="pl-en">propertiesHandler</span>(<span class="pl-smi">result</span>) {

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

     <strong> <span class="pl-smi">_self</span>.<span class="pl-en">startRotation</span>();</strong>
    }
    <span class="pl-k">else</span> {
      <strong><span class="pl-c1">clearInterval</span>(<span class="pl-smi">_self</span>.<span class="pl-smi">interval</span>);</strong> <span class="pl-c">// where is this function defined?</span>

      <span class="pl-smi">_viewer</span>.<span class="pl-en">isolateById</span>([]);
      <span class="pl-smi">_viewer</span>.<span class="pl-en">fitToView</span>();
      <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">setVisible</span>(<span class="pl-c1">false</span>);
    }
  }</span></pre>
</li>
<li><span style="font-family: tahoma, arial, helvetica, sans-serif;">実装が完了したら、localhost:3000 でローカルに設定下Web サーバー ページを表示してから、オブジェクトを選択してみてください。パネルが表示されると同時に、選択したオブジェクトの周囲にカメラが旋回して、オブジェクトが回転して見えるはずです。<br /><br /><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/g4TOegkarcg?feature=oembed" width="500"></iframe>&#0160;<br /></span></li>
</ol>
<p>ここまでの実装で、<strong><a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data" target="_blank">Autodesk View &amp; Data API – Getting Started Tutorial リポジトリ</a></strong>&#0160; にあるチュートリアルは終了です。おつかれさまでした。</p>
<p><strong>次のステップ</strong></p>
<p>Node.js で作成した Web サーバーは、あくまでローカルな環境に用意した開発環境です。この環境を <strong><a href="https://aws.amazon.com/jp/" target="_blank">AWS</a></strong> や <strong><a href="https://azure.microsoft.com/ja-jp/" target="_blank">Microsoft Arure</a></strong>、<strong><a href="https://www.heroku.com/" target="_blank">Heroku</a></strong> などにホストして運用することも出来ますし、プライベート クラウド(オンプレミス サーバー）にホストしてイントラネットで利用することも出来ます。もちろん、Node.js 以外にも、ASP.NET で Web サーバーを構成することも出来ます。</p>
<p>また、View and Data API &#0160;自体も他の Web サービス API と一緒に組み合わせて利用することで、単なるビューアとしてだけではなく、さまざまな情報を Web ブラウザだけで配信するダッシュボードとして運用することも出来るはずです。<strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/11/fusion-360-view-and-data-api-mozopenhard.html" target="_blank">ハードウェア</a></strong>や <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/use-cases-of-view-and-data-api-iot-monitor.html" target="_blank">IoT</a></strong>&#0160;のインタフェースとしても利用可能です。</p>
<p>オートデスクが GitHub 上に用意した <strong><a href="https://github.com/Developer-Autodesk" target="_blank">Forge Platform サンプル ページ</a></strong>&#0160;には、沢山のサンプルが多くのバリエーションで用意されていています。ぜひ、<strong><a href="https://github.com/Developer-Autodesk" target="_blank">https://github.com/Developer-Autodesk</a></strong> を覗いてみてください。また、<strong><a href="http://forums.autodesk.com/t5/view-and-data-api/bd-p/95" target="_blank">フォーラム</a>&#0160;</strong>に投稿してサポートを受けたり、他の開発者の質問と解決策を参照することも有用です。Autodesk Forge サンプルの&#0160;<strong><a href="http://adndevblog.typepad.com/cloud_and_mobile/" target="_blank">ブログ</a></strong>&#0160;からも、テクニックや実装手法を得ることが出来るようになっています。</p>
<p>View and Data API を含む Forge プラットフォーム API を利用して、ぜひ、<strong><a href="http://www.autodesk.co.jp/fomt" target="_blank">The&#0160;Future Of Making Things</a></strong>&#0160; を実現してみてください。</p>
<p><span style="font-family: tahoma, arial, helvetica, sans-serif;">By Toshiaki Isezaki</span></p>
