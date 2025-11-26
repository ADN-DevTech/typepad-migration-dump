---
layout: "post"
title: "Forge Viewer ローディング スピナーのカスタマイズ"
date: "2019-03-18 00:05:10"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/forge-viewer-custom-loading-spinner.html "
typepad_basename: "forge-viewer-custom-loading-spinner"
typepad_status: "Draft"
---

<p>Windows や Mac などのプラットフォームやアプリケーションでは、時間のかかる演算タスクの実行やファイルのダウンロード中の待ち時間に、アニメーション効果を使った画像を表示させて、処理中であることを表現することがあります。同じような目的でプログレスバーも利用されていますが、表示領域に制限がある場合も考慮して、くるくると回るアニメーションがよく使われています。これらは、くるくる回ることを由来として、スピナー（Spinner）、あるいは、ローディング スピナー（Loading Spinner）と呼ばれています。</p>
<p>Forge Viewer でも Viewer が初期化されてストリーミング配信されたジオメトリが表示するまでの間、3 つの円を使ったバブル状のローディング スピナーが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4919b30200b-pi" style="display: inline;"><img alt="Standard_spinner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4919b30200b image-full img-responsive" src="/assets/image_328965.jpg" title="Standard_spinner" /></a></p>
<p>Forge Viewer のローディング スピナーは、<strong><a href="https://developer.mozilla.org/ja/docs/Web/CSS/CSS_Animations" rel="noopener" target="_blank">CSS アニメーション</a></strong>で実装されていて、Forge Viewer の利用時に参照するカスケーディング スタイル シート（Cascading Style Sheets）である <strong><a href="https://developer.api.autodesk.com/modelderivative/v2/viewers/style.min.css" rel="noopener" target="_blank">https://developer.api.autodesk.com/modelderivative/v2/viewers/style.min.css</a></strong>&#0160;に定義されています。個々のバブルには bounce1、bounce2、bounce3 のクラス名が与えられていることもわかります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-2"><code class="language-javascript code-overflow-x hljs " id="snippet-4">...<br />viewing-viewer .spinner {
  margin: auto;
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  width: 25%;
  visibility: visible;
  text-align: center;
  height: 12.5%
}

.adsk-viewing-viewer .spinner&gt;div {
  width: 12%;
  height: 0;
  padding-bottom: 12%;
  margin: 0 1.5%;
  background-color: #fff;
  border-radius: 100%;
  display: inline-block;
  -webkit-animation: bouncedelay 1.4s ease-in-out infinite;
  animation: bouncedelay 1.4s ease-in-out infinite;
  -webkit-animation-fill-mode: both;
  animation-fill-mode: both
}

.adsk-viewing-viewer .spinner .bounce1 {
  -webkit-animation-delay: -.75s;
  animation-delay: -.75s
}

.adsk-viewing-viewer .spinner .bounce2 {
  -webkit-animation-delay: -.5s;
  animation-delay: -.5s
}

.adsk-viewing-viewer .spinner .bounce3 {
  -webkit-animation-delay: -.25s;
  animation-delay: -.25s
}

@-webkit-keyframes bouncedelay {
  0%, 95%, to {
    -webkit-transform: scale(0)
  }
  40% {
    -webkit-transform: scale(1)
  }
}

@keyframes bouncedelay {
  0%, 95%, to {
    transform: scale(0);
    -webkit-transform: scale(0)
  }
  40% {
    transform: scale(1);
    -webkit-transform: scale(1)
  }
}</code><br /><code class="language-javascript code-overflow-x hljs " id="snippet-4">...</code> </code></pre>
<p>そして、この loading-spinner の名前に付いた CSS アニメーションは、Forge Viewer 本体となる JsvsScript ライブラリ内（<strong><a href="https://developer.api.autodesk.com/modelderivative/v2/viewers/viewer3D.min.js" rel="noopener" target="_blank">https://developer.api.autodesk.com/modelderivative/v2/viewers/viewer3D.min.js</a></strong>）で、次のように動的に生成される &lt;div&gt; タグを持つ HTML で画面上に表示されています。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">...<br />function(e) {
  var t=document.createElement(&quot;div&quot;);
  t.className=&quot;spinner&quot;, e&amp;&amp;e.appendChild(t);
  for(var n=1;
  n&lt;=3;
  n++) {
    var i=document.createElement(&quot;div&quot;);
    i.className=&quot;bounce&quot;+n, t.appendChild(i)
  }
  return this.domElement=t, this.hide(), t
}
...</code></pre>
<p>このローディング スピナーは、spinner の名前が付いた &lt;div&gt; タグ毎、他の CSS アニメーション定義と置き換えて変更してしまうことが出来ます。あまり重要な話題ではありませんし、ここで複雑な形状を表示するなど、過剰なリソースを投入はまったくお勧めしませんが、Tips として少し触れておきます。</p>
<p>CSS アニメーションを使ったスピナーは、もちろん独自に作成することが出来ますし、多くのサイトで無償、有償を含め公開されています。例えば、<strong><a href="https://connoratherton.com/loaders" rel="noopener" target="_blank">https://connoratherton.com/loaders</a></strong> や <strong><a href="https://projects.lukehaas.me/css-loaders/" rel="noopener" target="_blank">https://projects.lukehaas.me/css-loaders/</a></strong>、<strong><a href="https://codepen.io/pieter-biesemans/pen/NwQWGy/" rel="noopener" target="_blank">https://codepen.io/pieter-biesemans/pen/NwQWGy/</a></strong> や <strong><a href="https://loading.io/css/" rel="noopener" target="_blank">https://loading.io/css/</a></strong> など、さまざまです。ここでは、ページ上で著作権が <strong><a href="https://creativecommons.jp/sciencecommons/aboutcc0/" rel="noopener" target="_blank">CC0 ライセンス</a></strong>として明確になっている <strong><a href="https://loading.io/css/" rel="noopener" target="_blank">https://loading.io/css/</a></strong> トップページにある 12 個の中の 1 つで、プロペラ状の lds-hourglass を使って、Forge Viewer 標準のローディング スピナーを置き換えてみます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46d5daf200d-pi" style="display: inline;"><img alt="Loading_io_css" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46d5daf200d image-full img-responsive" src="/assets/image_842254.jpg" title="Loading_io_css" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a491fb5c200b-pi" style="display: inline;"><img alt="Cc0_license" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a491fb5c200b image-full img-responsive" src="/assets/image_78838.jpg" title="Cc0_license" /></a></p>
<p>同ページでプロペラ アニメーションをクリックすると、CSS アニメーションの記述が表示されるので、クリップボードを使うなどして、作成中の Forge プロジェクトで参照している独自の CSS ファイルか HTML ファイルの &lt;style&gt;&lt;/style&gt; タグ内に内容を貼り付けます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">.lds-hourglass {
  display: inline-block;
  position: relative;
  width: 64px;
  height: 64px;
}

.lds-hourglass:after {
  content: &quot; &quot;;
  display: block;
  border-radius: 50%;
  width: 0;
  height: 0;
  margin: 6px;
  box-sizing: border-box;
  border: 26px solid #fff;
  border-color: #fff transparent #fff transparent;
  animation: lds-hourglass 1.2s infinite;
}

@keyframes lds-hourglass {
  0% {
    transform: rotate(0);
    animation-timing-function: cubic-bezier(0.55, 0.055, 0.675, 0.19);
  }
  50% {
    transform: rotate(900deg);
    animation-timing-function: cubic-bezier(0.215, 0.61, 0.355, 1);
  }
  100% {
    transform: rotate(1800deg);
  }
}</code></pre>
<p>後は、標準にスピナーを定義いた &lt;div&gt;&lt;/div&gt; タグセクションを lds-hourglass に置き変える処理を Viewer のロード処理を実装箇所に記述するだけです。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs ">...<br />    // Load viewable
    function loadDocument(viewer, documentId) {
        // Find the first 3d geometry and load that.
        Autodesk.Viewing.Document.load(documentId, function (doc) {// onLoadCallback
            var geometryItems = [];
            if (geometryItems.length == 0) {
                geometryItems = Autodesk.Viewing.Document.getSubItemsWithProperties(doc.getRootItem(), {
                    &#39;type&#39;: &#39;geometry&#39;,
                    &#39;role&#39;: &#39;3d&#39;
                }, true);
            }
            if (geometryItems.length &gt; 0) {
                viewer.load(doc.getViewablePath(geometryItems[geometryItems.length - 1]));
<span style="color: #0000ff;"><strong>                replaceSpinner(); 
</strong></span>            }
        }
    }, function (errorMsg) {// onErrorCallback
        console.log(&quot;Load Error: &quot; + errorMsg);
    });

}

<span style="color: #0000ff;"><strong>function replaceSpinner() {
    var spinners = document.getElementsByClassName(&quot;spinner&quot;);
    if (spinners.length == 0) return;
    var spinner = spinners[0];
    spinner.classList.remove(&quot;spinner&quot;);
    spinner.classList.add(&#39;lds-hourglass&#39;);
    spinner.innerHTML = &#39;&lt;div&gt;&lt;/div&gt;&#39;;
}</strong></span></code></pre>
<p>Viewer コードを実装した Web ページを表示させると、置き換えたローディング スピナーを確認出来るはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4444752200c-pi" style="display: inline;"><img alt="Custom_spinner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4444752200c image-full img-responsive" src="/assets/image_410368.jpg" title="Custom_spinner" /></a></p>
<p>By Toshiaki Isezaki</p>
