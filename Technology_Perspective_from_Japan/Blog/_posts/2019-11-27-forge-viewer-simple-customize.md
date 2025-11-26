---
layout: "post"
title: "Forge Viewer：簡単な UI カスタマイズ"
date: "2019-11-27 01:32:05"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/11/forge-viewer-simple-customize.html "
typepad_basename: "forge-viewer-simple-customize"
typepad_status: "Publish"
---

<p>ここでは、Forge Viewer で比較的簡単にカスタマイズ出来る内容についてご紹介しておきます。</p>
<p>まずは、一番よくあるのが、ビューア領域のツールバーを含めたユーザ インタフェース関連のご質問です。Forge Viewer 標準では、表示されるツールバーやパレット ダイアログは、背景が黒い半透明で表示される「ダーク テーマ」が既定値になっています。BIM 360 Docs では、背景が黒い半透明で表示される「ライト テーマ」が既定値になっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a1f6dd200c-pi" style="display: inline;"><img alt="Themes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a1f6dd200c image-full img-responsive" src="/assets/image_577056.jpg" title="Themes" /></a></p>
<p>ダーク テーマとライト テーマの切り替えは、Viewer3D クラスの setTheme メソッドで変更することが出来ます。パラメータとして指定する値は、ダーク テーマが ”dark-theme”、ライト テーマが ”light-theme” です。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer.setTheme(&quot;dark-theme&quot;); // ダーク テーマ</code></pre>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer.setTheme(&quot;light-theme&quot;); // ライト テーマ</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>標準ツールバーはビューア領域の中央下を既定値として表示されますが、縦に表示させることも出来ます。縦に表示させるには、Viewer3D.toolbar.addClass メソッドで &#39;adsk-toolbar-vertical’ クラスを追加することで実現することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4cb25d3200d-pi" style="display: inline;"><img alt="Toolbar_align" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4cb25d3200d image-full img-responsive" src="/assets/image_689988.jpg" title="Toolbar_align" /></a></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer.toolbar.addClass(&#39;adsk-toolbar-vertical&#39;); // 縦</code></pre>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer.toolbar.removeClass(&#39;adsk-toolbar-vertical&#39;); // 横（縦解除）</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>この際には、ビューア領域の右側に表示されることになりますが、水平ツールバーも含め、スタイルシートの値を変更して指定することが可能です。この場合、Web ブラウザに搭載されているデベロッパーツールを活用して、ツールバー要素を検索語、指定可能な値や状態を動的に確認することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4cb26eb200d-pi" style="display: inline;"><img alt="Toolbar_align2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4cb26eb200d image-full img-responsive" src="/assets/image_983605.jpg" title="Toolbar_align2" /></a></p>
<p>もし、標準ツールバーの中から、特定のツールセットやボタンを消去したい場合には、デベロッパーツールで対象のツールセットやボタンの id やクラス名を確認後、それらを消去することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4efd836200b-pi" style="display: inline;"><img alt="Tool_class" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4efd836200b image-full img-responsive" src="/assets/image_567245.jpg" title="Tool_class" /></a></p>
<p>例えば、次のコードで標準ツールバー左の &#39;navTools&#39; ツールセットを非表示にすることが出来ます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4"> _tools = _viewer.toolbar.getControl(&#39;navTools&#39;);
 _tools.setVisible(false);
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4cb2901200d-pi" style="display: inline;"><img alt="Remove_toolset" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4cb2901200d image-full img-responsive" src="/assets/image_797210.jpg" title="Remove_toolset" /></a></p>
<p>同じく、次のコードで標準ツールバー右にある &#39;settingsTools&#39; ツールセット内の設定ボタン &#39;toolbar-settingsTool&#39; を非表示にすることが出来ます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">    const settingsTools = _viewer.toolbar.getControl(&#39;settingsTools&#39;);
    _tool = settingsTools.getControl(&#39;toolbar-settingsTool&#39;);
    _tool.setVisible(false);
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a1fc58200c-pi" style="display: inline;"><img alt="Remove_toolbutton" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a1fc58200c image-full img-responsive" src="/assets/image_757067.jpg" title="Remove_toolbutton" /></a></p>
<p>By Toshiaki Isezaki</p>
