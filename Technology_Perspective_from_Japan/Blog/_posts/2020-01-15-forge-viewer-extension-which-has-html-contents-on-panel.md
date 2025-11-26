---
layout: "post"
title: "Forge Viewer：HTML コンテンツ パネルを持つ Extension "
date: "2020-01-15 00:12:05"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-extension-which-has-html-contents-on-panel.html "
typepad_basename: "forge-viewer-extension-which-has-html-contents-on-panel"
typepad_status: "Publish"
---

<p><a href="https://www.typepad.com/site/blogs/6a0167607c2431970b017ee78dc01c970d/post/6a0167607c2431970b0240a4af7412200c/edit">Forge Viewer：ツールバーとパネルを持つスケルトン Extension</a> で作成したスケルトン Extension のパネルにHTML コンテンツを用意して、アイコン画像付きのツールバー ボタンから表示/非表示するよう、 Extension を拡張してみたいと思います。</p>
<hr />
<p><strong>パネルの実装</strong></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4af8747200c-pi" style="float: right;"><img alt="Panel" class="asset  asset-image at-xid-6a0167607c2431970b0240a4af8747200c img-responsive" src="/assets/image_925132.jpg" style="width: 220px; margin: 0px 0px 5px 5px;" title="Panel" /></a>Forge Viewer 内に表示されるコンテンツは、HTML&#0160; として定義したユーザ インタフェースと、ユーザ インタフェースのイベント処理関数を内包する必要があります。まずは、Viewing.Extension.ViewControl.js ファイルを新規作成して、ViewControlPanel となる次のコードを記入します。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs ">// *******************************************
// View Control Panel
// *******************************************
function ViewControlPanel(viewer, container, id, title, options) {
    this.viewer = viewer;
    Autodesk.Viewing.UI.DockingPanel.call(this, container, id, title, options);

    var _myView = viewer.getState();

    // the style of the docking panel
    // use this built-in style to support Themes on Viewer 4+
    this.container.classList.add(&#39;docking-panel-container-solid-color-a&#39;);
    this.container.style.top = &quot;10px&quot;;
    this.container.style.left = &quot;10px&quot;;
    this.container.style.width = &quot;240px&quot;;
    this.container.style.height = &quot;250px&quot;;
    this.container.style.resize = &quot;auto&quot;;

    // this is where we should place the content of our panel
    var div = document.createElement(&#39;div&#39;);
    // and may also append child elements...
    var html = [        &#39;&lt; div id=&quot;controls&quot; style=&quot;position:absolute; width:100%&quot;&gt;&#39;,
        &#39; &lt; div style=&quot;position:absolute; top:10px; left:10px&quot;&gt;&#39;,
        &#39;  &lt; button class=&quot;docking-panel-tertiary-button&quot; type=&quot;button&quot; id=&quot;register&quot; style=&quot;width:75px; height:30px&quot;&gt;ビュー登録&#39;,
        &#39;  &lt; button class=&quot;docking-panel-tertiary-button&quot; type=&quot;button&quot; id=&quot;restore&quot; style=&quot;width:75px; height:30px&quot;&gt;ビュー復元&#39;,
        &#39; &lt; /div&gt;&#39;,
        &#39; &lt; div style=&quot;position:absolute; top:50px; left:30px&quot;&gt;&#39;,
        &#39;  &lt; button class=&quot;docking-panel-tertiary-button&quot; id=&quot;zoomin&quot; style=&quot;position:absolute; top:0px; left:60px; width:30px&quot;&gt;∧&#39;,
        &#39;  &lt; button class=&quot;docking-panel-tertiary-button&quot; id=&quot;zoomout&quot; style=&quot;position:absolute; top:80px; left:60px; width:30px&quot;&gt;∨&#39;,
        &#39;  &lt; button class=&quot;docking-panel-tertiary-button&quot; id=&quot;turnright&quot; style=&quot;position:absolute; top:40px; left:140px; width:30px&quot;&gt;&gt;&#39;,
        &#39;  &lt; button class=&quot;docking-panel-tertiary-button&quot; id=&quot;turnleft&quot; style=&quot;position:absolute; top:40px; left:-20px; width:30px&quot;&gt;&lt;&#39;,
        &#39;  &lt; input id=&quot;step&quot; style=&quot;position:absolute; top:52px; left:65px; width:50px; height:20px&quot; size=&quot;5&quot; maxlength=&quot;4&quot; value=&quot;100&quot; type=&quot;text&quot; /&gt;&#39;,
        &#39; &lt; /div&gt;&#39;,
        &#39;&lt; /div&gt;&#39;
].join(&#39;\n&#39;);
    div.innerHTML = html;
    this.container.appendChild(div);

    // Register View
    $(document).on(&quot;click&quot;, &quot;[id^=&#39;register&#39;]&quot;, function () {
        _myView = viewer.getState();
    });

    // Restore View
    $(document).on(&quot;click&quot;, &quot;[id^=&#39;restore&#39;]&quot;, function () {
        _viewer.restoreState(_myView);
    });

    // Zoom In
    $(document).on(&quot;click&quot;, &quot;[id^=&#39;zoomin&#39;]&quot;, function () {
        viewer.canvas.focus();
        console.log(viewer.getActiveNavigationTool());
        viewer.setActiveNavigationTool(&quot;dolly&quot;);
        var step = document.getElementById(&#39;step&#39;).value * -1.0;
        var cam = viewer.getCamera();
        cam.translateX(0);
        cam.translateY(0);
        cam.translateZ(step);
        viewer.impl.syncCamera();
        viewer.setActiveNavigationTool(&quot;orbit&quot;);
    });

    // ↓ Zoom Out
    $(document).on(&quot;click&quot;, &quot;[id^=&#39;zoomout&#39;]&quot;, function () {
        viewer.canvas.focus();
        viewer.setActiveNavigationTool(&quot;dolly&quot;);
        var step = document.getElementById(&#39;step&#39;).value;
        var cam = viewer.getCamera();
        cam.translateX(0);
        cam.translateY(0);
        cam.translateZ(step);
        viewer.impl.syncCamera();
        viewer.setActiveNavigationTool(&quot;orbit&quot;);
    });

    // Turn Right
    $(document).on(&quot;click&quot;, &quot;[id^=&#39;turnright&#39;]&quot;, function () {
        viewer.canvas.focus();
        viewer.setActiveNavigationTool(&quot;orbit&quot;);
        var step = document.getElementById(&#39;step&#39;).value * -1.0;
        var cam = viewer.getCamera();
        cam.translateX(step);
        cam.translateY(0);
        cam.translateZ(0);
        viewer.impl.syncCamera();
    });

    // Turn Left
    $(document).on(&quot;click&quot;, &quot;[id^=&#39;turnleft&#39;]&quot;, function () {
        viewer.canvas.focus();
        viewer.setActiveNavigationTool(&quot;orbit&quot;);
        var step = document.getElementById(&#39;step&#39;).value;
        var cam = viewer.getCamera();
        cam.translateX(step);
        cam.translateY(0);
        cam.translateZ(0);
        viewer.impl.syncCamera();
    });

}
ViewControlPanel.prototype = Object.create(Autodesk.Viewing.UI.DockingPanel.prototype);
ViewControlPanel.prototype.constructor = ViewControlPanel;</code></pre>
<hr />
<p><strong>Extension の実装</strong></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4af8750200c-pi" style="float: right;"><img alt="Toolbar_button" class="asset  asset-image at-xid-6a0167607c2431970b0240a4af8750200c img-responsive" src="/assets/image_122031.jpg" style="width: 220px; margin: 0px 0px 5px 5px;" title="Toolbar_button" /></a>上記のパネルを実装することになる ViewControlExtension Extension を実装します。ここでは、ViewControlPanel 実装の下に、次のコードを追記します。</p>
<p>ViewControlPanel パネルを表示するツールバー ボタンには、サーバー上の images フォルダの icon_ready.png が設定されてます。<span style="color: #111111;">このアイコン画像は、ツールバー スタイル（CSS）に動的に追加されている点に注意してください。</span></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">// *******************************************
// View Control Extension
// *******************************************
function ViewControlExtension(viewer, options) {
    Autodesk.Viewing.Extension.call(this, viewer, options);
    this.panel = null;
}

ViewControlExtension.prototype = Object.create(Autodesk.Viewing.Extension.prototype);
ViewControlExtension.prototype.constructor = ViewControlExtension;

ViewControlExtension.prototype.load = function () {
    if (this.viewer.toolbar) {
        // Toolbar is already available, create the UI
        this.createUI();
    } else {
        // Toolbar hasn&#39;t been created yet, wait until we get notification of its creation
        this.onToolbarCreatedBinded = this.onToolbarCreated.bind(this);
        this.viewer.addEventListener(Autodesk.Viewing.TOOLBAR_CREATED_EVENT, this.onToolbarCreatedBinded);
    }
    return true;
};

ViewControlExtension.prototype.onToolbarCreated = function () {
    this.viewer.removeEventListener(Autodesk.Viewing.TOOLBAR_CREATED_EVENT, this.onToolbarCreatedBinded);
    this.onToolbarCreatedBinded = null;
    this.createUI();
};

ViewControlExtension.prototype.createUI = function () {
    var viewer = this.viewer;
    var panel = this.panel;

    // button to show the docking panel
    var toolbarButtonShowDockingPanel = new Autodesk.Viewing.UI.Button(&#39;showViewControlPanel&#39;);
    toolbarButtonShowDockingPanel.onClick = function (e) {
        // if null, create it
        if (panel === null) {
            panel = new ViewControlPanel(viewer, viewer.container,
                &#39;viewControlPanel&#39;, &#39;View Control&#39;);
        }
        // show/hide docking panel
        panel.setVisible(!panel.isVisible());
    };
    // ViewControlToolbarButton CSS class should be defined on your .css file
    // you may include icons, below is a sample class:
    <span style="color: #0000ff;"><span style="color: #111111;">var css = [
            &#39;.ViewControlToolbarButton {&#39;,
            &#39; background-image: url(/images/icon_ready.png);&#39;,
            &#39; background-size: 24px;&#39;,
            &#39; background-repeat: no-repeat;&#39;,
            &#39; background-position: center;&#39;,
            &#39;}&#39;
    ].join(&#39;\n&#39;);
    $(&#39;&lt; style type=&quot;text/css&quot;&gt;&#39; + css + &#39;&lt; /style&gt;&#39;).appendTo(&#39;head&#39;);

    toolbarButtonShowDockingPanel.addClass(&#39;ViewControlToolbarButton&#39;);
    toolbarButtonShowDockingPanel.setToolTip(&#39;View Control&#39;);

    // SubToolbar
    this.subToolbar = new Autodesk.Viewing.UI.ControlGroup(&#39;UtilitiesToolbar&#39;);
    this.subToolbar.addControl(toolbarButtonShowDockingPanel);

    viewer.toolbar.addControl(this.subToolbar);
};

ViewControlExtension.prototype.unload = function () {
    this.viewer.toolbar.removeControl(this.subToolbar);
    return true;
};

Autodesk.Viewing.theExtensionManager.registerExtension(&#39;Viewing.Extension.ViewControl&#39;, ViewControlExtension);</span></span></code></pre>
<hr />
<p><strong>Extension のロード</strong></p>
<p>Viewing.Extension.ViewControl.js ファイルに実装した Extension の ID は、<strong>Viewing.Extension.ViewControl</strong> です。Extension のロードにも、この Extension ID を使用します。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer.loadExtension(&#39;Viewing.Extension.ViewControl&#39;);
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>Extension で実装した ViewControlPanel パネルで配置されたボタンの クリック ハンドラに、<a href="https://jquery.com"><strong>JQuery</strong></a> を使用している点に注意してください。このため、ビューアを実装する本体の HTML には、Viewing.Extension.ViewControl.js ファイルとともに、JQuery ライブラリの参照記述も必要になります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    &lt; script type=&quot;text/javascript&quot; src=&quot;https://code.jquery.com/jquery-2.1.2.min.js&quot;&gt;
    &lt; script type=&quot;text/javascript&quot; src=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/viewer3D.min.js&quot;&gt;
    &lt; script type=&quot;text/javascript&quot; src=&quot;index.js&quot;&gt;
    &lt; script type=&quot;text/javascript&quot; src=&quot;Viewing.Extension.ViewControl.js&quot;&gt;

</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4af90c0200c-pi" style="display: inline;"><img alt="ViewControl" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4af90c0200c image-full img-responsive" src="/assets/image_496055.jpg" title="ViewControl" /></a></p>
<p>作成した Extension のツールバー ボタンとパネルは、Forge Viewer のテーマにも対応しているので、ライトテーマでは次のようになるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4af8427200c-pi" style="display: inline;"><img alt="ViewControl" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4af8427200c image-full img-responsive" src="/assets/image_52710.jpg" title="ViewControl" /></a></p>
<hr />
<p>By Toshiaki Isezaki</p>
