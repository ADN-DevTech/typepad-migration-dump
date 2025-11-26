---
layout: "post"
title: "View and Data API チュートリアル ～ その3 ～ Basic Extension"
date: "2016-03-21 04:01:50"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part3-basic-extension.html "
typepad_basename: "view-and-data-api-tutorial-part3-basic-extension"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part2-upload-and-translation.html" target="_blank">前回</a></strong> まで作成した Node.js を使ったチュートリアルに、Extension（エクステンション）を追加していきます。ここで紹介する内容は、<strong><a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data" target="_blank">Autodesk View &amp; Data API – Getting Started Tutorial リポジトリ</a></strong>&#0160;の<a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data/blob/master/chapter-3.md#Chapter3" target="_blank">&#0160;<strong>Chapter 3</strong>&#0160;</a>の Step 1 ～ Step 4&#0160;に該当するものです。</p>
<p><strong>Extension とは ?</strong></p>
<p style="padding-left: 30px;">ご存知のとおり、AutoCAD や Revit、Inventor のようなデスクトップ製品では、その API を使ってアドイン アプリケーションを開発して製品にロードすることで、製品自体の機能を拡張することが出来ます。ここでいう Extension とは、View and Data API で作成した実行中ビューアに、JavaScript で記述したファイルをロードして、機能拡張するモジュールを指しています。</p>
<p style="padding-left: 30px;">つまり、View and Data API も、アドインと同じようなメカニズムを持っています。もちろん、利用する必要がなくなれば、ビューアから Extension をロード解除することも出来ます。</p>
<p style="padding-left: 30px;">Extension を利用することで、管理者権限を持つメンバだけに Extension をロードして機能を利用させたり、権限のないユーザの利用時に Extension をロード解除して特定の機能を無効にするといった制御も可能になります。</p>
<p>まず、Extension の基本メカニズムを理解するために、簡単な Extension のロードとロード解除を実装していきます。</p>
<ol>
<li>Adobe Brackets などの適切なテキスト エディタを使って、&quot;Viewing.Extension.Workshop.js&quot; 名で JavaScript ファイルを作成して、次の内容のスケルトン コードを貼り付けて www フォルダに保存します。<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;">  <span class="pl-c">///////////////////////////////////////////////////////////////////////////////</span>
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
    <span class="pl-smi">Viewing</span>.<span class="pl-smi">Extension</span>.<span class="pl-smi">Workshop</span>);</span></pre>
</li>
<li>index.html ファイルを開いて、&lt;/head&gt; タグの一行前に 1. で作成した Extension ファイルを参照するコードを追記します。<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;"><span class="pl-k">&lt;</span>script src<span class="pl-k">=</span><span class="pl-s"><span class="pl-pds">&quot;</span>/Viewing.Extension.Workshop.js<span class="pl-pds">&quot;</span></span><span class="pl-k">&gt;&lt;</span><span class="pl-k">/</span>script<span class="pl-k">&gt;</span></span></pre>
</li>
<li>index.js に&#0160;Extension ファイルをロードするコードを追記します。ビューアがジオメトリをロードした際に生成される&#0160;GEOMETRY_LOADED_EVENT イベントを待って、Extension をロードさせるため、index.js ファイルの既存コードに、次の太字の部分を追記します。このコードで、イベント発生時に loadExtension() が呼び出されます。<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;"><strong>      <span class="pl-smi">viewer</span>.<span class="pl-en">addEventListener</span>(
        <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">GEOMETRY_LOADED_EVENT</span>,
        <span class="pl-k">function</span>(<span class="pl-smi">event</span>) {
          <span class="pl-en">loadExtensions</span>(viewer);
      });</strong>

      <span class="pl-smi">viewer</span>.<span class="pl-c1">load</span>(<span class="pl-smi">pathInfoCollection</span>.<span class="pl-smi">path3d</span>[<span class="pl-c1">0</span>].<span class="pl-smi">path</span>);
    },
    onError);

  });</span></pre>
</li>
<li>loadExtension() を実装します。index.js ファイルの OnError() の前に、次の太字部分を追記します。<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;"><strong>  <span class="pl-k">function</span> <span class="pl-en">loadExtensions</span>(<span class="pl-smi">viewer</span>) {
    <span class="pl-smi">viewer</span>.<span class="pl-en">loadExtension</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop<span class="pl-pds">&#39;</span></span>);
  }</strong>

  <span class="pl-k">function</span> <span class="pl-en">onError</span>(<span class="pl-smi">error</span>) {
    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Error: <span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> error);
  };</span></pre>
</li>
<li>実装したExtension をテストしてみます。Node.js でクライアント上の Web サーバーを起動して、WebGL 対応ブラウザで&#0160;http://localhost:3000/ を開き、3D モデルを表示してみてください。ジオメトリが表示された直後に、次のようなメッセージ ボックスが表示されれば、Extension が正しくロードされたことになります。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8246863970b-pi" style="display: inline;"><img alt="Extension_loaded" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8246863970b img-responsive" src="/assets/image_46684.jpg" title="Extension_loaded" /><br /></a><br />なお、ここで表示されたのは、Viewing.Extension.Workshop.js 内の&#0160;alert(&#39;Viewing.Extension.Workshop loaded&#39;); の部分となります。</li>
</ol>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part4-object-selection.html" target="_blank">次回</a></strong> は、Extension の内容を変更して、オブジェクトの選択処理を実装していきます。</p>
<p>By Toshiaki Isezaki</p>
