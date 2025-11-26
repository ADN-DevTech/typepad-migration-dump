---
layout: "post"
title: "Forge Viewer：Extension なしのツールバー ボタン"
date: "2020-02-03 00:03:11"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/02/forge-viewer-toolbar-button-without-extension.html "
typepad_basename: "forge-viewer-toolbar-button-without-extension"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4d98b45200d-pi" style="display: inline;"></a><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-extension-which-has-html-contents-on-panel.html" rel="noopener" target="_blank">Forge Viewer：HTML コンテンツ パネルを持つ Extension</a></strong> のブログ記事では、パネルとツールバー ボタンを実装する Extension&#0160; をご紹介しましたが、ツールバー ボタンに Extension での実装が必須というわけではありません。例えば、ちょっとした機能をツールバー ボタンで切り替えるだけなら、Extension なしで実装したほうが便利な場合があります。もちろん、そのような実装も可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4b05d5e200c-pi" style="display: inline;"><img alt="Theme_switcher" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4b05d5e200c image-full img-responsive" src="/assets/image_956704.jpg" title="Theme_switcher" /></a></p>
<p>下記は、新しく作成したツールバー &#39;UtilitiesToolbar&#39; に新しいボタンを追加して、クリック毎に Forge Viewer の UI カラーテーマを切り替える例です。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">        var css = [
            &#39;.ThemeSwitcherToolbarButton {&#39;,
            &#39; background-image: url(/images/icon_theme.png);&#39;,
            &#39; background-size: 24px;&#39;,
            &#39; background-repeat: no-repeat;&#39;,
            &#39; background-position: center;&#39;,
            &#39;}&#39;
        ].join(&#39;\n&#39;);
        $( &#39; &lt; style type=&quot;text/css&quot;&gt;&#39; + css + &#39; &#39;).appendTo(&#39;head&#39;);

        var button = new Autodesk.Viewing.UI.Button(&#39;ThemeSwitchToolbarButton&#39;);
        button.onClick = function (e) {
            console.log(_viewer.theme);
            if (_viewer.theme === &#39;light-theme&#39;) {
                _viewer.setTheme(&#39;dark-theme&#39;);
                _viewer.theme = &#39;dark-theme&#39;;
            } else {
                _viewer.setTheme(&#39;light-theme&#39;);
                _viewer.theme = &#39;light-theme&#39;;
            }
        };
        button.addClass(&#39;ThemeSwitcherToolbarButton&#39;);
        button.setToolTip(&#39;Theme Switcher&#39;);

        var subToolbar = new Autodesk.Viewing.UI.ControlGroup(&#39;UtilitiesToolbar&#39;);
        subToolbar.addControl(button);
        _viewer.toolbar.addControl(subToolbar);
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p>Extension での実装かに関係なく、ツールバー ボタンを追加する場合には、新規に作成したツールバーにボタンを追加するのか、既存のツールバーにボタンを追加するのか、を選択することが出来ます。</p>
<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/11/forge-viewer-simple-customize.html" rel="noopener" target="_blank">Forge Viewer：簡単な UI カスタマイズ</a></strong> でも触れたように、ツールバーの id を利用してツールバーを探して、ボタンを追加するだけです。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/01/forge-viewer-extension-which-has-html-contents-on-panel.html" rel="noopener" target="_blank">Forge Viewer：HTML コンテンツ パネルを持つ Extension</a></strong> で実装した &#39;UtilitiesToolbar&#39; にボタンを追加すると、次のようになります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">        var subToolbar = _viewer.toolbar.getControl(&#39;UtilitiesToolbar&#39;);
        if (subToolbar === null) {
            subToolbar = new Autodesk.Viewing.UI.ControlGroup(&#39;UtilitiesToolbar&#39;);
            subToolbar.addControl(button);
            _viewer.toolbar.addControl(subToolbar);
        } else {
            subToolbar.addControl(button);
        }
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4d98db2200d-pi" style="display: inline;"><img alt="Theme_changer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4d98db2200d image-full img-responsive" src="/assets/image_637029.jpg" title="Theme_changer" /></a></p>
<p>既存ツールバーの id は、もちろん、デベロッパー ツールで確認することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4d98b45200d-pi" style="display: inline;"><img alt="Custom_control" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4d98b45200d image-full img-responsive" src="/assets/image_217435.jpg" title="Custom_control" /></a></p>
<p>By Toshiaki Isezaki</p>
