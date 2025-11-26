---
layout: "post"
title: "Forge Viewer チュートリアル ～ その2 ～ Extension の実装"
date: "2017-01-20 00:01:07"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/01/forge-viewer-tutorial-part2-implement-an-extension.html "
typepad_basename: "forge-viewer-tutorial-part2-implement-an-extension"
typepad_status: "Publish"
---

<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/forge-viewer-tutorial-part1-implement-nodejs-server.html" rel="noopener noreferrer" target="_blank">前回</a></strong>、作成した Node.js を使った Web サーバーがホストする Forge Viewer に、Extension（エクステンション）を追加していきます。ここで紹介する内容は、<strong><a href="https://github.com/Developer-Autodesk/viewer-javascript-tutorial" rel="noopener noreferrer" target="_blank">viewer-javascript-tutorial リポジトリ</a></strong>&#0160;の<a href="https://github.com/Developer-Autodesk/viewer-javascript-tutorial/blob/master/chapters/chapter-3.md#Chapter3" rel="noopener noreferrer" target="_blank">&#0160;<strong>Chapter 3</strong>&#0160;</a>の Step 1 ～ Step 6 に該当するものです。</p>
<p><strong>Extension とは ?</strong></p>
<p style="padding-left: 30px;">AutoCAD や Revit、Inventor のようなデスクトップ製品では、その API を使ってアドイン アプリケーションを開発して製品にロードすることで、製品自体の機能を拡張することが出来ます。Forge Viewer も、アドインと同じようなメカニズムを持っています。JavaScript で決められたルールに従って記述した機能拡張モジュール、すなわち、Extension を作成して、実行中の Forge Viewer にロードすることで利用するメカニズムです。</p>
<p style="padding-left: 30px;">Extension を利用することで、管理者権限を持つメンバだけに Extension をロードして機能を利用させたり、権限のないユーザの利用時に Extension をロード解除して特定の機能を無効にするといった制御も可能になります。もちろん、利用する必要がなくなれば、ビューアから Extension をロード解除することも出来ます。</p>
<hr />
<p>最初に Extension の基本メカニズムを理解するために、簡単な Extension のロードとロード解除を実装していきます。</p>
<ol>
<li>Adobe Brackets などの適切なテキスト エディタを使って、&quot;Viewing.Extension.Workshop.js&quot; 名で JavaScript ファイルを作成して、次の内容のスケルトン コードを貼り付けて www フォルダに保存します。<br />
<pre>  <span class="pl-c">///////////////////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// Demo Workshop Viewer Extension</span>
  <span class="pl-c">// by Philippe Leefsma, April 2015</span>
  <span class="pl-c">//</span>
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

    <span class="pl-en">alert</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop loaded<span class="pl-pds">&#39;</span></span>);
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
    <span class="pl-smi">Viewing</span>.<span class="pl-smi">Extension</span>.<span class="pl-smi">Workshop</span>);</pre>
</li>
<li>index.html ファイルを開いて、&lt;/head&gt; タグの一行前に 1. で作成した Extension ファイルを参照するコードを追記します。<br />
<pre><span class="pl-k">&lt;</span>script src<span class="pl-k">=</span><span class="pl-s"><span class="pl-pds">&quot;</span>/Viewing.Extension.Workshop.js<span class="pl-pds">&quot;</span></span><span class="pl-k">&gt;&lt;</span><span class="pl-k">/</span>script<span class="pl-k">&gt;</span></pre>
</li>
<li>index.js に&#0160;Extension ファイルをロードするコードを追記します。ビューアがジオメトリをロードした際に生成される&#0160;GEOMETRY_LOADED_EVENT イベントを待って、Extension をロードさせるため、index.js ファイルの既存コードに、次の太字の部分を追記します。このコードで、イベント発生時に loadExtension() が呼び出されます。<br />
<pre><strong>      <span class="pl-smi">viewer</span>.<span class="pl-en">addEventListener</span>(
        <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">GEOMETRY_LOADED_EVENT</span>,
        <span class="pl-k">function</span>(<span class="pl-smi">event</span>) {
          <span class="pl-en">loadExtensions</span>(viewer);
      });</strong>

      <span class="pl-smi">viewer</span>.<span class="pl-c1">load</span>(<span class="pl-smi">pathInfoCollection</span>.<span class="pl-smi">path3d</span>[<span class="pl-c1">0</span>].<span class="pl-smi">path</span>);
    },
    onError);

  });</pre>
</li>
<li>loadExtension() を実装します。index.js ファイルの OnError() の前に、次の太字部分を追記します。<br />
<pre><strong>  <span class="pl-k">function</span> <span class="pl-en">loadExtensions</span>(<span class="pl-smi">viewer</span>) {
    <span class="pl-smi">viewer</span>.<span class="pl-en">loadExtension</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop<span class="pl-pds">&#39;</span></span>);
  }</strong>

  <span class="pl-k">function</span> <span class="pl-en">onError</span>(<span class="pl-smi">error</span>) {
    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Error: <span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> error);
  };</pre>
</li>
<li>実装したExtension をテストしてみます。Node.js でクライアント上の Web サーバーを起動して、WebGL 対応ブラウザで&#0160;http://localhost:3000/ を開き、3D モデルを表示してみてください。ジオメトリが表示された直後に、次のようなメッセージ ボックスが表示されれば、Extension が正しくロードされたことになります。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb096e5e77970d-pi" style="display: inline;"><img alt="Extension_loaded" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb096e5e77970d img-responsive" src="/assets/image_417482.jpg" title="Extension_loaded" /><br /></a>なお、ここで表示されたのは、Viewing.Extension.Workshop.js 内の&#0160;alert(&#39;Viewing.Extension.Workshop loaded&#39;); の部分となります。</li>
</ol>
<hr />
<p>続いて、作成した Extension にオブジェクト選択の処理を追加ていきします。オブジェクトの選択には、イベント処理を利用します。Forge Viewer には、オブジェクト選択を検出する&#0160;SELECTION_CHANGED_EVENT イベントが用意されているので、このイベント ハンドラを宣言して実装すると、オブジェクトを選択した後の各種処理を実装していくことが可能です。</p>
<ol>
<li>追加した Extension ファイル、Viewing.Extension.Workshop.js を Adobe Brackets などのテキスト エディタで開いて、次の太字部分を追記してください。ジオメトリをロードされた際に表示していた alert() は、コメントにし表示しないように変更してください。<br />
<pre>  <span class="pl-c1">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

<strong>    <span class="pl-smi">_viewer</span>.<span class="pl-en">addEventListener</span>(
      <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">SELECTION_CHANGED_EVENT</span>,
      <span class="pl-smi">_self</span>.<span class="pl-smi">onSelectionChanged</span>);
</strong>
    //alert(&#39;Viewing.Extension.Workshop loaded&#39;);<br />    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop loaded<span class="pl-pds">&#39;</span></span>);

    <span class="pl-k">return</span> <span class="pl-c1">true</span>;
  };

<strong>  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// selection changed callback</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c1">_self</span>.<span class="pl-en">onSelectionChanged</span> <span class="pl-k">=</span> <span class="pl-k">function</span> (<span class="pl-smi">event</span>) {

    <span class="pl-c">// event is triggered also when component is unselected</span>

    <span class="pl-c">// in that case event.dbIdArray is an empty array</span>
    <span class="pl-k">if</span>(<span class="pl-c1">event</span>.<span class="pl-smi">dbIdArray</span>.<span class="pl-c1">length</span>) {

      <span class="pl-k">var</span> dbId <span class="pl-k">=</span> <span class="pl-c1">event</span>.<span class="pl-smi">dbIdArray</span>[<span class="pl-c1">0</span>];

      <span class="pl-c">// do stuff with selected component</span>
    }
    <span class="pl-k">else</span> {
      <span class="pl-c">// all components unselected</span>
    }
  }<br /></strong></pre>
</li>
<li>Viewing.Extension.Workshop.js を上書き保存して、Node.js で Web サーバーを再起動してから、Web ブラウザで localhost:3000 を表示してください。モデル表示後にオブジェクト選択が可能になりますが、現在のコードではオブジェクトを選択しても何も起こりません。イベント ハンドラが正しく動作しているかは、Web ブラウザのデバッグ環境で確かめることができます。例えば、Google Chrome をお使いの場合には、3D モデルの表示後に F12 キーを押すことで、デベロッパー ツールを表示させることが可能です。あとは、Viewing.Extension.Workshop.js の適切な位置にブレーク ポイントを置いて、オブジェクトを選択するだけです。このコードでは、選択したオブジェクトの ID が変数 dbId に代入される過程を把握することが出来るはずです。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d255685e970c-pi" style="display: inline;"><img alt="Developer_tool_on_chrome" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d255685e970c image-full img-responsive" src="/assets/image_535612.jpg" title="Developer_tool_on_chrome" /></a></li>
</ol>
<hr />
<p>次に、オブジェクト選択の機能で拡張した Extension に、選択したオブジェクトのプロパティを表示するパネル表示機能を実装していきます。</p>
<ol>
<li>Viewing.Extension.Workshop.js を Adobe Brackets などで開いて、次の太字部分を追記します。<br />
<pre>  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
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

  <span class="pl-c1">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {</pre>
</li>
<li>Extension のロードとロード解除を担当する load() と unload() 関数で、定義したパネルのインスタンス化と後処理を追記します。load() と unload() 関数内の太字部分を追記してください。<br />
<pre>  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
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
  };</pre>
<br />
<pre>  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c">// unload callback: invoked when viewer.unloadExtension is called</span>
  <span class="pl-c">//</span>
  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
  <span class="pl-c1">_self</span>.<span class="pl-en">unload</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

<strong>    <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">setVisible</span>(<span class="pl-c1">false</span>);
    <span class="pl-smi">_self</span>.<span class="pl-smi">panel</span>.<span class="pl-en">uninitialize</span>();</strong>

    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop unloaded<span class="pl-pds">&#39;</span></span>);

    <span class="pl-k">return</span> <span class="pl-c1">true</span>;
  };</pre>
</li>
<li>前回、オブジェクト選択で利用した&#0160;SELECTION_CHANGED イベントのイベント ハンドラ関数である&#0160;onSelectionChanged() の中身を次の内容で置き換えます。このコードは、選択したオブジェクトをビュー内に拡大表示して、そのプロパティをインスタンス化したパネルに表示します。<br /><br />
<pre>  <span class="pl-c">/////////////////////////////////////////////////////////////////</span>
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
  }</pre>
</li>
<li>Viewing.Extension.Workshop.js を上書き保存してから、Node.js で構築してある Web サーバーを localhost:3000 のURLで表示してみてください。選択したオブジェクトが拡大表示され、同時にプロパティが Workshop Panel のタイトルを持つパネルに表示されるはずです。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8cb432a970b-pi" style="display: inline;"><img alt="Workshop_panel" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8cb432a970b image-full img-responsive" src="/assets/image_780918.jpg" title="Workshop_panel" /></a></li>
</ol>
<hr />
<p>ここまでの実装で、オブジェクト選択とパネル表示を Extension としてモジュール化する実装が完了しました。このように、Extension 単位でさまざまな機能を盛り込んでいくことができます。Extension には、オートデスクが用意した標準の Extension も存在します。過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/12/extensions-abailable-on-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer で利用可能な Extension</a></strong>&#0160;でもご紹介していますのでご確認ください。</p>
<p>By Toshiaki Isezaki</p>
