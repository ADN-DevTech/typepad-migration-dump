---
layout: "post"
title: "Forge Viewer：ツールバーとパネルを持つスケルトン Extension "
date: "2020-01-08 00:02:07"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-skeleton-extension-which-has-toolbar-and-panel.html "
typepad_basename: "forge-viewer-skeleton-extension-which-has-toolbar-and-panel"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4b25b3a200c-pi" style="float: right;"><img alt="MyAwesomeExtension2" class="asset  asset-image at-xid-6a0167607c2431970b0240a4b25b3a200c img-responsive" src="/assets/image_961111.jpg" style="width: 220px; margin: 0px 0px 5px 5px;" title="MyAwesomeExtension2" /></a>Extension の作成は、ビューアにカスタムコードを追加するための推奨アプローチです。 ほとんどの場合、Extension の機能を利用するユーザインタフェースとして、専用のパネルが実装されています。また、パネルを表示するためのツールバー ボタンが用意されています。Forge ポータルからリンクされてブログ記事 <a href="https://forge.autodesk.com/blog/extension-skeleton-toolbar-docking-panel" rel="noopener" target="_blank"><strong>Extension Skeleton: Toolbar &amp; Docking Panel</strong></a> では、Extension がパネルとツールバー ボタンを実装する方法が紹介されているので、Forge Viewer v7 での使用に必要な情報を追記しながら、和訳するかたちでご紹介しておきたいと思います。</p>
<hr />
<p><strong>パネルの実装</strong></p>
<p>まず、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/UI/DockingPanel/" rel="noopener" target="_blank">Autodesk.Viewing.UI.DockingPanel</a></strong> を使用して専用パネルを作成します。メイン コンテナがパネルをホストし、document.createElement(&#39;div&#39;) で動的に生成した &lt;div&gt;～&lt;/div&gt; がコンテンツを配置する場所になっているに注意してください。 あくまでスケルトンとしてパネルを作成したいので、 パネル内のコンテンツには「My content here」のテキストのみが表示されます。なお、ここで使用される CSS クラスは、Viewer 4+でユーザインタフェースのライトテーマとダークテーマに対応すます。</p>
<p>MyAwesomeExtension.js ファイルを作成して、次のコードを記述します（貼り付けます）。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-0"><span class="hljs-comment">// *******************************************</span>
<span class="hljs-comment">// My Awesome (Docking) Panel</span>
<span class="hljs-comment">// *******************************************</span>
<span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">MyAwesomePanel</span><span class="hljs-params">(viewer, container, id, title, options)</span> {</span>
    <span class="hljs-keyword">this</span>.viewer = viewer;
    Autodesk.Viewing.UI.DockingPanel.call(<span class="hljs-keyword">this</span>, container, id, title, options);

    <span class="hljs-comment">// the style of the docking panel</span>
    <span class="hljs-comment">// use this built-in style to support Themes on Viewer 4+</span>
    <span class="hljs-keyword">this</span>.container.classList.add(<span class="hljs-string">&#39;docking-panel-container-solid-color-a&#39;</span>);
    <span class="hljs-keyword">this</span>.container.style.top = <span class="hljs-string">&quot;10px&quot;</span>;
    <span class="hljs-keyword">this</span>.container.style.left = <span class="hljs-string">&quot;10px&quot;</span>;
    <span class="hljs-keyword">this</span>.container.style.width = <span class="hljs-string">&quot;auto&quot;</span>;
    <span class="hljs-keyword">this</span>.container.style.height = <span class="hljs-string">&quot;auto&quot;</span>;
    <span class="hljs-keyword">this</span>.container.style.resize = <span class="hljs-string">&quot;auto&quot;</span>;

    <span class="hljs-comment">// this is where we should place the content of our panel</span>
    <span class="hljs-keyword">var</span> div = document.createElement(<span class="hljs-string">&#39;div&#39;</span>);
    div.style.margin = <span class="hljs-string">&#39;20px&#39;</span>;
    div.innerText = <span class="hljs-string">&quot;My content here&quot;</span>;
    <span class="hljs-keyword">this</span>.container.appendChild(div);
    <span class="hljs-comment">// and may also append child elements...</span>

}
MyAwesomePanel.prototype = <span class="hljs-built_in">Object</span>.create(Autodesk.Viewing.UI.DockingPanel.prototype);
MyAwesomePanel.prototype.constructor = MyAwesomePanel;</code></pre>
<hr />
<p><strong>Extension の実装</strong></p>
<p>上記で定義したパネルを持つ Extension を実装します。MyAwesomeExtension.js ファイルの最後に、次のコードを追記（貼り付け）してください。は、パネルを表示するツールバー&#0160; ボタン自身のスタイル（CSS）は、.cssファイルで定義する必要があります（下記コードではコメントになている部分）。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-1"><span class="hljs-comment">// *******************************************</span>
<span class="hljs-comment">// My Awesome Extension</span>
<span class="hljs-comment">// *******************************************</span>
<span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">MyAwesomeExtension</span><span class="hljs-params">(viewer, options)</span> {</span>
    Autodesk.Viewing.Extension.call(<span class="hljs-keyword">this</span>, viewer, options);
    <span class="hljs-keyword">this</span>.panel = <span class="hljs-literal">null</span>;
}

MyAwesomeExtension.prototype = <span class="hljs-built_in">Object</span>.create(Autodesk.Viewing.Extension.prototype);
MyAwesomeExtension.prototype.constructor = MyAwesomeExtension;

MyAwesomeExtension.prototype.load = <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-params">()</span> {</span>
    <span class="hljs-keyword">if</span> (<span class="hljs-keyword">this</span>.viewer.toolbar) {
        <span class="hljs-comment">// Toolbar is already available, create the UI</span>
        <span class="hljs-keyword">this</span>.createUI();
    } <span class="hljs-keyword">else</span> {
        <span class="hljs-comment">// Toolbar hasn&#39;t been created yet, wait until we get notification of its creation</span>
        <span class="hljs-keyword">this</span>.onToolbarCreatedBinded = <span class="hljs-keyword">this</span>.onToolbarCreated.bind(<span class="hljs-keyword">this</span>);
        <span class="hljs-keyword">this</span>.viewer.addEventListener(Autodesk.Viewing.TOOLBAR_CREATED_EVENT, <span class="hljs-keyword">this</span>.onToolbarCreatedBinded);
    }
    <span class="hljs-keyword">return</span> <span class="hljs-literal">true</span>;
};

MyAwesomeExtension.prototype.onToolbarCreated = <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-params">()</span> {</span>
    <span class="hljs-keyword">this</span>.viewer.removeEventListener(Autodesk.Viewing.TOOLBAR_CREATED_EVENT, <span class="hljs-keyword">this</span>.onToolbarCreatedBinded);
    <span class="hljs-keyword">this</span>.onToolbarCreatedBinded = <span class="hljs-literal">null</span>;
    <span class="hljs-keyword">this</span>.createUI();
};

MyAwesomeExtension.prototype.createUI = <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-params">()</span> {</span>
    <span class="hljs-keyword">var</span> viewer = <span class="hljs-keyword">this</span>.viewer;
    <span class="hljs-keyword">var</span> panel = <span class="hljs-keyword">this</span>.panel;

    <span class="hljs-comment">// button to show the docking panel</span>
    <span class="hljs-keyword">var</span> toolbarButtonShowDockingPanel = <span class="hljs-keyword">new</span> Autodesk.Viewing.UI.Button(<span class="hljs-string">&#39;showMyAwesomePanel&#39;</span>);
    toolbarButtonShowDockingPanel.onClick = <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-params">(e)</span> {</span>
        <span class="hljs-comment">// if null, create it</span>
        <span class="hljs-keyword">if</span> (panel === <span class="hljs-literal">null</span>) {
            panel = <span class="hljs-keyword">new</span> MyAwesomePanel(viewer, viewer.container, 
                <span class="hljs-string">&#39;awesomeExtensionPanel&#39;</span>, <span class="hljs-string">&#39;My Awesome Extension&#39;</span>);
        }
        <span class="hljs-comment">// show/hide docking panel</span>
        panel.setVisible(!panel.isVisible());
    };
    <span class="hljs-comment">// myAwesomeToolbarButton CSS class should be defined on your .css file</span>
    <span class="hljs-comment">// you may include icons, below is a sample class:</span>
    <span class="hljs-comment">/* 
    .myAwesomeToolbarButton {
        background-image: url(/img/myAwesomeIcon.png);
        background-size: 24px;
        background-repeat: no-repeat;
        background-position: center;
    }*/</span>
    toolbarButtonShowDockingPanel.addClass(<span class="hljs-string">&#39;myAwesomeToolbarButton&#39;</span>);
    toolbarButtonShowDockingPanel.setToolTip(<span class="hljs-string">&#39;My Awesome extension&#39;</span>);

    <span class="hljs-comment">// SubToolbar</span>
    <span class="hljs-keyword">this</span>.subToolbar = <span class="hljs-keyword">new</span> Autodesk.Viewing.UI.ControlGroup(<span class="hljs-string">&#39;MyAwesomeAppToolbar&#39;</span>);
    <span class="hljs-keyword">this</span>.subToolbar.addControl(toolbarButtonShowDockingPanel);

    viewer.toolbar.addControl(<span class="hljs-keyword">this</span>.subToolbar);
};

MyAwesomeExtension.prototype.unload = <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-params">()</span> {</span>
    <span class="hljs-keyword">this</span>.viewer.toolbar.removeControl(<span class="hljs-keyword">this</span>.subToolbar);
    <span class="hljs-keyword">return</span> <span class="hljs-literal">true</span>;
};

Autodesk.Viewing.theExtensionManager.registerExtension(<span class="hljs-string">&#39;MyAwesomeExtension&#39;</span>, MyAwesomeExtension);</code></pre>
<hr />
<p><strong>Extension のロード</strong></p>
<p>Extension を使用するには、ビューアへのロードが必須です。ただし、ビューアへのロードの前に、Extension を実装する JavaScript ファイル（ここでは MyAwesomeExtension.js ファイル）をビューア本体の HTML ファイルで参照しておく必要があります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-xml code-overflow-x hljs " id="snippet-2"><span class="hljs-tag">&lt;<span class="hljs-title">script</span> <span class="hljs-attribute">src</span>=<span class="hljs-value">&quot;your_folder/MyExtensionFileName.js&quot;</span>&gt;</span><span class="hljs-tag">&lt;/<span class="hljs-title">script</span>&gt;</span></code></pre>
<p>Extension のロードにはいくつかの方法がありますが、ドキュメント ロードの成功時に呼び出される onDocumentLoadSuccess() でロードさせるのが一般的です。Extension が表示するドキュメント データにアクセスする場合には、onDocumentLoadSuccess() でイベント ハンドラ登録した GEOMETRY_LOADED_EVENT イベントで <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#loadextension-extensionid-options" rel="noopener" target="_blank">viewer3D.loadaExtension()</a> を呼び出すのが確実です。</p>
<p>ここでは、MyAwesomeExtension Extension に <strong>options</strong> （Extension にカスタムパラメータ）を渡しながらロードする例を示します。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"><span class="hljs-function"><span class="hljs-keyword">_viewer.addEventListener(Autodesk.Viewing.GEOMETRY_LOADED_EVENT, onViewerGeometryLoaded);</span></span></code></pre>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"><span class="hljs-function">function onViewerGeometryLoaded(event) {</span>
  viewer.loadExtension(<span class="hljs-string">&#39;MyAwesomeExtension&#39;</span>, { param1: <span class="hljs-string">&#39;value1&#39;</span> });
}</code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4af819c200c-pi" style="display: inline;"><img alt="MyAwesomeExtension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4af819c200c image-full img-responsive" src="/assets/image_965887.jpg" title="MyAwesomeExtension" /></a></p>
<hr />
<p>次回は、このスケルトンを変更して、実際の運用に近づけた実装をご紹介します。</p>
<p>By Toshiaki Isezaki</p>
