---
layout: "post"
title: "Forge Viewer Extension の作成"
date: "2018-09-25 00:11:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/09/creating-forge-viewer-extension.html "
typepad_basename: "creating-forge-viewer-extension"
typepad_status: "Publish"
---

<p><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3939701200d-pi" style="float: right;"><img alt="Icon - viewer" class="asset  asset-image at-xid-6a0167607c2431970b022ad3939701200d img-responsive" src="/assets/image_611304.jpg" style="margin: 0px 0px 5px 5px;" title="Icon - viewer" /></a><a href="http://adndevblog.typepad.com/technology_perspective/2016/12/extensions-abailable-on-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer で利用可能な Extension</a></strong> でご案内のとおり、オートデスクが Forge Viewer 用に用意、サポートしている <strong><a href="https://forge.autodesk.com/en/docs/viewer/v2/developers_guide/extensions/" rel="noopener noreferrer" target="_blank">Extensions</a></strong>（JavaScript&#0160; ロード モジュール） が複数存在します。同時に、独自に Extension を作成して必要に応じて、Forge Viewer を拡張していくことも可能です。</p>
<p>Extension の作成方法は、公式ドキュメント <strong><a href="https://forge.autodesk.com/en/docs/viewer/v2/tutorials/extensions/" rel="noopener noreferrer" target="_blank">Extensions</a></strong> にも記載されていますが、ここではオブジェクトを選択して、オブジェクトが持つプロパティをパレットに表示する Extension の作成手順を記載しておきたいと思います。</p>
<hr />
<p>Extension はロード モジュールとして動作するため、単独では機能しません。Forge Viewer を使用するメイン ルーチン（HTML と関連する JavaScript コード）がすでに完成していることが前提となります。例えば、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/02/code-evolution-to-show-models-on-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer でのモデル表示コードの進化</a></strong> でもご紹介した <strong><a href="https://forge.autodesk.com/en/docs/viewer/v2/tutorials/basic-viewer/" rel="noopener noreferrer" target="_blank">Basic Viewer</a></strong> や <strong><a href="https://forge.autodesk.com/en/docs/viewer/v2/tutorials/basic-application/" rel="noopener noreferrer" target="_blank">Basic Application</a></strong> がこれに相当します。ここでは、<a href="http://adndevblog.typepad.com/technology_perspective/2017/11/revised-new-forge-viewer-tutorial-part1.html" rel="noopener noreferrer" target="_blank"><strong>新しい Forge Viewer チュートリアル改定版 ～ その1 </strong></a>で作成した Viewer に Extension をロードするものと仮定して、HTML を www フォルダ直下の index.html、index.html が参照する JavaScript を js フォルダ下の index.js として話を進めることにします。</p>
<ol>
<ol>
<ol>
<li>まず、Adobe Brackets などのテキスト エディタを使って Extension 本体を作成していきます。 JavaScript ファイルを新しく作成して、次の<span style="color: #0000ff;"><strong>太字</strong></span>のコードを貼り付けて www フォルダに &quot;<strong>Viewing.Extension.Workshop.js</strong>&quot; の名前で保存します。このコードは、Extension のスケルトンとなるものです。<br />
<pre><span style="color: #0000ff;"><strong><span class="pl-c">///////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">// Demo Workshop Viewer Extension</span>
<span class="pl-c">///////////////////////////////////////////////////////////////////////////////</span>

<span class="pl-en">AutodeskNamespace</span>(<span class="pl-s"><span class="pl-pds">&quot;</span>Viewing.Extension<span class="pl-pds">&quot;</span></span>);

<span class="pl-c1">Viewing.Extension</span>.<span class="pl-en">Workshop</span> <span class="pl-k">=</span> <span class="pl-k">function</span> (<span class="pl-smi">viewer</span>, <span class="pl-smi">options</span>) {

    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
    <span class="pl-c">//  base class constructor</span>
    <span class="pl-c">//</span>
    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>

    <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-smi">Extension</span>.<span class="pl-c1">call</span>(<span class="pl-v">this</span>, viewer, options);

    <span class="pl-k">var</span> _self <span class="pl-k">=</span> <span class="pl-v">this</span>;
    <span class="pl-k">var</span> _viewer <span class="pl-k">=</span> viewer;

    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
    <span class="pl-c">// load callback: invoked when viewer.loadExtension is called</span>
    <span class="pl-c">//</span>
    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>

    <span class="pl-c1">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

        <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop loaded<span class="pl-pds">&#39;</span></span>);

        <span class="pl-k">return</span> <span class="pl-c1">true</span>;

    };

    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
    <span class="pl-c">// unload callback: invoked when viewer.unloadExtension is called</span>
    <span class="pl-c">//</span>
    <span class="pl-c">/////////////////////////////////////////////////////////////////</span>

    <span class="pl-c1">_self</span>.<span class="pl-en">unload</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

        <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop unloaded<span class="pl-pds">&#39;</span></span>);

        <span class="pl-k">return</span> <span class="pl-c1">true</span>;

    };

};

<span class="pl-c">/////////////////////////////////////////////////////////////////</span>
<span class="pl-c">// sets up inheritance for extension and register</span>
<span class="pl-c">//</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////</span>

<span class="pl-c1">Viewing.Extension.Workshop</span>.<span class="pl-c1">prototype</span> <span class="pl-k">=</span>
    <span class="pl-c1">Object</span>.<span class="pl-en">create</span>(<span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-smi">Extension</span>.<span class="pl-c1">prototype</span>);

<span class="pl-c1">Viewing.Extension.Workshop</span>.<span class="pl-c1">prototype</span>.<span class="pl-en">constructor</span> <span class="pl-k">=</span>
    <span class="pl-smi">Viewing</span>.<span class="pl-smi">Extension</span>.<span class="pl-smi">Workshop</span>;

<span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-smi">theExtensionManager</span>.<span class="pl-en">registerExtension</span>(
    <span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop<span class="pl-pds">&#39;</span></span>,
    <span class="pl-smi">Viewing</span>.<span class="pl-smi">Extension</span>.<span class="pl-smi">Workshop</span>);</strong></span></pre>
</li>
<li>www フォルダ内の index.html ファイルを開いて、&lt;/head&gt; タグの一行前に 1. で作成した Extension ファイルを参照するコードを追記します。<br />
<pre><span style="color: #0000ff;"><strong><span class="pl-k">&lt;</span>script src<span class="pl-k">=</span><span class="pl-s"><span class="pl-pds">&quot;</span>/Viewing.Extension.Workshop.js<span class="pl-pds">&quot;</span></span><span class="pl-k">&gt;&lt;</span><span class="pl-k">/</span>script<span class="pl-k">&gt;</span></strong></span></pre>
</li>
<li>www フォルダの index.js を開いて、Extension ファイルをロードするコードを追記します。ビューアがジオメトリをロードした際に生成される<strong>&#0160;<a href="https://forge.autodesk.com/en/docs/viewer/v2/reference/javascript/viewer3d/#geometry-loaded-event" rel="noopener noreferrer" target="_blank">GEOMETRY_LOADED_EVENT イベント</a></strong>を待って、Extension をロードさせるため、index.js ファイルの onLoadModelSuccess() 内に、次の<span style="color: #0000ff;"><strong>太字</strong></span>の部分を追記します。このコードで、イベント発生時に loadExtension() が呼び出されます。<br />
<pre>function onLoadModelSuccess(model) {<br />    console.log(&#39;onLoadModelSuccess()!&#39;);<br />    console.log(&#39;Validate model loaded: &#39; + (viewer.model === model));<br />    console.log(model);<br /> <br /><span style="color: #0000ff;"><strong>    viewer.addEventListener(</strong></span><br /><span style="color: #0000ff;"><strong>        Autodesk.Viewing.GEOMETRY_LOADED_EVENT,</strong></span><br /><span style="color: #0000ff;"><strong>        function(event) {</strong></span><br /><span style="color: #0000ff;"><strong>            loadExtensions(viewer);</strong></span><br /><span style="color: #0000ff;"><strong>        });</strong></span><br />}</pre>
</li>
<li>loadExtension() を実装します。index.js ファイルの onLoadModelError() の後に、次の<span style="color: #0000ff;"><strong>太字</strong></span>部分を追記します。<br />
<pre>function onLoadModelError(viewerErrorCode) {<br />    console.error(&#39;onLoadModelError() - errorCode:&#39; + viewerErrorCode);<br />}<br /><br /><span style="color: #0000ff;"><strong>function loadExtensions(viewer) {</strong></span><br /><span style="color: #0000ff;"><strong>    viewer.loadExtension(&#39;Viewing.Extension.Workshop&#39;);</strong></span><br /><span style="color: #0000ff;"><strong>}</strong>
</span></pre>
</li>
<li>イベント処理を利用してオブジェクト選択を検出します。Forge Viewer の JavaScript API には、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v2/reference/javascript/viewer3d/#aggregate-selection-changed-event" rel="noopener noreferrer" target="_blank">SELECTION_CHANGED_EVENT イベント</a></strong>が用意されているので、このイベント ハンドラを宣言して実装するだけで、オブジェクトを選択した後の各種処理を実装していくことが可能です。</li>
<li>Viewing.Extension.Workshop.js の _self.load() 内に、次の<span style="color: #0000ff;"><strong>太字</strong></span>部分を追記してください。<br />
<pre><span class="pl-c1">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

<span style="color: #0000ff;"><strong>    <span class="pl-smi">_viewer</span>.<span class="pl-en">addEventListener</span>(
      <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">SELECTION_CHANGED_EVENT</span>,
      <span class="pl-smi">_self</span>.<span class="pl-smi">onSelectionChanged</span>);
</strong></span>
    //alert(&#39;Viewing.Extension.Workshop loaded&#39;);<br />    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop loaded<span class="pl-pds">&#39;</span></span>);

    <span class="pl-k">return</span> <span class="pl-c1">true</span>;
};<strong><br /></strong></pre>
</li>
<li>選択したオブジェクトのプロパティを表示するパネルを実装していきます。&#0160;Viewing.Extension.Workshop.js を Adobe Brackets などで開いて、次の<span style="color: #0000ff;"><strong>太字</strong></span>部分を追記します。
<pre>  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">//  base class constructor</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>

  <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-smi">Extension</span>.<span class="pl-c1">call</span>(<span class="pl-v">this</span>, viewer, options);

  <span class="pl-k">var</span> _self <span class="pl-k">=</span> <span class="pl-v">this</span>;
  <span class="pl-k">var</span> _viewer <span class="pl-k">=</span> viewer;

<span style="color: #0000ff;"> <strong> <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
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
</strong></span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// load callback: invoked when viewer.loadExtension is called</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>

  <span class="pl-c1">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {</pre>
</li>
<li>Extension のロードとロード解除を処理する load() と unload() 内で、定義したパネルのインスタンス化と後処理を追記します。load() と unload() 関数内の<span style="color: #0000ff;"><strong>太字</strong></span>部分を追記してください。<br />
<pre>  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// load callback: invoked when viewer.loadExtension is called</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c1">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

    <span class="pl-smi">_viewer</span>.<span class="pl-en">addEventListener</span>(
      <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">SELECTION_CHANGED_EVENT</span>,
      <span class="pl-smi">_self</span>.<span class="pl-smi">onSelectionChanged</span>);

<span style="color: #0000ff;"><strong>    <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span> <span class="pl-k">=</span> <span class="pl-k">new</span> <span class="pl-en">Viewing.Extension</span>.<span class="pl-smi">Workshop</span>.<span class="pl-en">WorkshopPanel</span> (
      <span class="pl-smi">_viewer</span>.<span class="pl-smi">container</span>,
      <span class="pl-s"><span class="pl-pds">&#39;</span>WorkshopPanelId<span class="pl-pds">&#39;</span></span>,
      <span class="pl-s"><span class="pl-pds">&#39;</span>Workshop Panel<span class="pl-pds">&#39;</span></span>);</strong></span>

    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop loaded<span class="pl-pds">&#39;</span></span>);

    <span class="pl-k">return</span> <span class="pl-c1">true</span>;
  };</pre>
<pre>  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// unload callback: invoked when viewer.unloadExtension is called</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c1">_self</span>.<span class="pl-en">unload</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

<span style="color: #0000ff;"><strong>    <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">setVisible</span>(<span class="pl-c1">false</span>);
    <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">uninitialize</span>();</strong></span>

    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop unloaded<span class="pl-pds">&#39;</span></span>);

    <span class="pl-k">return</span> <span class="pl-c1">true</span>;
  };</pre>
</li>
<li>オブジェクト選択の検出で利用した&#0160;SELECTION_CHANGED イベントのイベント ハンドラ関数である&#0160;onSelectionChanged() を _self.load() と _self.unload() の間に定義します。このコードは、選択したオブジェクトをビュー内に拡大表示して、そのプロパティをインスタンス化したパネルに表示します。<br />
<pre><span style="color: #0000ff;"><strong>  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// selection changed callback</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c1">_self</span>.<span class="pl-en">onSelectionChanged</span> <span class="pl-k">=</span> <span class="pl-k">function</span> (<span class="pl-smi">event</span>) {</strong></span>

<span style="color: #0000ff;">   <strong> <span class="pl-k">function</span> <span class="pl-en">propertiesHandler</span>(<span class="pl-smi">result</span>) {

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
<strong>  }</strong></span></pre>
</li>
<li>Viewing.Extension.Workshop.js を上書き保存してから、Node.js で構築してある Web サーバーを localhost:3000 のURLで表示してみてください。オブジェクトを選択するたびに選択状態が変わり、オブジェクトのフォーカスが遷移すると同時に、Workshop Panel のタイトルを持つパネルがプロパティを表示されるはずです。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3b38c89200b-pi" style="display: inline;"><img alt="Result" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3b38c89200b image-full img-responsive" src="/assets/image_562610.jpg" title="Result" /></a></li>
</ol>
</ol>
</ol>
<hr />
<p>このように、Extension 単位でさまざまな機能を盛り込んでいくことができます。なお、Extension のロード方法は、使用する Viewer 実装によって変化します。詳細は、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/02/code-evolution-to-show-models-on-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer でのモデル表示コードの進化</a></strong> の後半で触れていますので、一度ご確認ください。</p>
<p>By Toshiaki Isezaki</p>
