---
layout: "post"
title: "新しい Forge Viewer チュートリアル改訂版 ～ その2"
date: "2017-11-29 00:03:27"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/11/revised-new-forge-viewer-tutorial-part2.html "
typepad_basename: "revised-new-forge-viewer-tutorial-part2"
typepad_status: "Publish"
---

<p>前回、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/11/revised-new-forge-viewer-tutorial-part1.html" rel="noopener noreferrer" target="_blank">新しい Forge Viewer チュートリアル改訂版 ～ その1</a></strong>&#0160;でご紹介したチュートリアルに Extension 等の拡張を付け加えていきます。Extension とは、Forge Viewer が持つ機能で、JavaScript で決められたルールに従って記述した機能拡張モジュールです。Extension を作成して、実行中の Forge Viewer にロードすることで Viewer 自身を拡張することが出来ます。ちょうど、AutoCAD などのデスクトップ製品用にアドイン モジュールを作成、ロードさせて拡張するコンセプトに似ています。</p>
<p>また、Web 開発の経験が浅い方には、HTML でのボタン要素の追加や CSS でのスタイル参照の部分も参考になると思います。</p>
<hr />
<ol>
<li>www フォルダ下の js フォルダ内から&#0160;index.js ファイルを開いて、青字部分の関数群が定義されていることを確認してください。ここで対象としているのは、HTML 上に定義されたボタンをクリックした際に呼び出される関数実装です。<br />
<pre><span class="pl-c">                                       ：</span><br /><span class="pl-c">/////////////////////////////////////////////////////////////////////////////////<br />//</span>
<span class="pl-c">// Load Viewer Background Color Extension</span>
<span class="pl-c">//</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>

<span style="color: #0000ff;"><strong>   <span class="pl-c">function changeBackground (){</span>
   <span class="pl-c">       viewer.setBackgroundColor(0, 59, 111, 255,255, 255);</span>
   <span class="pl-c">}</span></strong>
</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">//</span>
<span class="pl-c">// Unload Viewer Background Color Extension</span>
<span class="pl-c">//</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>

<span style="color: #0000ff;"><strong>  <span class="pl-c"> function resetBackground (){     </span>
  <span class="pl-c">        viewer.setBackgroundColor(169,169,169, 255,255, 255);</span>
  <span class="pl-c"> }</span></strong></span>

<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">//</span>
<span class="pl-c">// Load Viewer Markup3D Extension</span>
<span class="pl-c">//</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">// 3D Markup extension to display values of the selected objects in the model. </span>

<span style="color: #0000ff;"><strong>  <span class="pl-c"> function loadMarkup3D (){</span>
  <span class="pl-c">        viewer.loadExtension(&#39;Viewing.Extension.Markup3D&#39;);</span>
  <span class="pl-c"> }</span></strong>
</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">//</span>
<span class="pl-c">// Load Viewer Transform Extension</span>
<span class="pl-c">//</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">// Transformation is allowed with this extension to move object selected in the XYZ</span>
<span class="pl-c">// position or rotation in XYZ as well.</span>

<span style="color: #0000ff;"><strong>  <span class="pl-c"> function loadTransform (){</span>
  <span class="pl-c">        viewer.loadExtension(&#39;Viewing.Extension.Transform&#39;);</span>
  <span class="pl-c"> }</span></strong></span>

<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">//</span>
<span class="pl-c">// Load Viewer Control Selector Extension</span>
<span class="pl-c">//</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">// This extension allows you to remove certain extensions from the original toolbar </span>
<span class="pl-c">// provided to you.</span>

<span style="color: #0000ff;"><strong>  <span class="pl-c"> function loadControlSelector(){</span>
  <span class="pl-c">        viewer.loadExtension(&#39;_Viewing.Extension.ControlSelector&#39;);</span>
  <span class="pl-c"> }</span></strong></span></pre>
<ul>
<li>Viewer3D.setBackgroundColor() で背景色を変更する際には、 呼び出し前にビューア設定の「環境イメージの表示」をオフにするか、Viewer3D.setEnvMapBackground() で false を指定してください。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2c1aa4c970c-pi" style="display: inline;"><img alt="Backgroud_image_settings" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2c1aa4c970c img-responsive" src="/assets/image_90863.jpg" title="Backgroud_image_settings" /></a></li>
</ul>
</li>
<li>続いて、同じ www フォルダ直下から index.html ファイルを開いて、&lt;head&gt; ～ &lt;/head&gt; セクション内の&#0160;<span class="pl-c">The Viewer Extensionsコメント行下にある Extension 参照部の 3 行分を囲む</span>コメント&#0160;<span class="pl-c"><strong>&lt;!-</strong>- &#0160;記号と&#0160;<strong>--&gt;</strong>&#0160;記号</span>を削除します。<br />
<pre><span class="pl-c">                                       ：<br /></span><span class="pl-c">    &lt;!-- The Viewer Extensions --&gt;</span>
<span style="color: #0000ff;"><strong>    <span class="pl-c">&lt;script src=&quot;/extensions/Viewing.Extension.Markup3D.min.js&quot;&gt;&lt;/script&gt;</span>
<span class="pl-c">    &lt;script src=&quot;/extensions/Viewing.Extension.Transform.min.js&quot;&gt;&lt;/script&gt;</span>
<span class="pl-c">    &lt;script src=&quot;/extensions/_Viewing.Extension.ControlSelector.min.js&quot;&gt;&lt;/script&gt;</span></strong>
</span>
    <span class="pl-c">&lt;!-- The Viewer JS --&gt;</span>
    &lt;<span class="pl-ent">script</span> <span class="pl-e">src</span>=<span class="pl-s"><span class="pl-pds">&quot;</span>/js/index.js<span class="pl-pds">&quot;</span></span>&gt;<span class="pl-s1">&lt;</span>/<span class="pl-ent">script</span>&gt;<br /><span class="pl-c">                                       ：</span>&#0160;</pre>
</li>
<li>同じ index.html ファイル内で &lt;body&gt; ～ &lt;/body&gt; セクション内でボタン定義を記述している箇所 6 行を囲むコメント&#0160;<span class="pl-c"><strong>&lt;!-</strong>- &#0160;記号と&#0160;<strong>--&gt;</strong>&#0160;記号</span>を削除します。<br />
<pre><span class="pl-c">                                       ：</span><br />&lt;<span class="pl-ent">div</span> <span class="pl-e">class</span>=<span class="pl-s"><span class="pl-pds">&quot;</span>container<span class="pl-pds">&quot;</span></span>&gt;
    <span class="pl-c">&lt;!-- This is where your viewer should attach --&gt;</span>
    &lt;<span class="pl-ent">div</span> <span class="pl-e">class</span>=<span class="pl-s"><span class="pl-pds">&quot;</span>center-block<span class="pl-pds">&quot;</span></span> <span class="pl-e">id</span>=<span class="pl-s"><span class="pl-pds">&quot;</span>viewerDiv<span class="pl-pds">&quot;</span></span>&gt;&lt;/<span class="pl-ent">div</span>&gt;
        
    <span class="pl-c">&lt;!-- Extension Buttons --&gt;</span>
<strong>    <span class="pl-c"><span style="color: #0000ff;">&lt;div class=&quot;row&quot;&gt;</span> </span>
<span style="color: #0000ff;">             
<span class="pl-c">       &lt;div class=&quot;myButton&quot; id=&quot;background&quot; onclick=&quot;changeBackground()&quot;&gt;Change Background&lt;/div&gt; </span>
<span class="pl-c">       &lt;div class=&quot;myButton&quot; id=&quot;background&quot; onclick=&quot;resetBackground()&quot;&gt;Reset Background&lt;/div&gt; </span>
<span class="pl-c">       &lt;div class=&quot;myButton&quot; id=&quot;background&quot; onclick=&quot;loadMarkup3D()&quot;&gt;Markup3D&lt;/div&gt;</span>
<span class="pl-c">       &lt;div class=&quot;myButton&quot; id=&quot;background&quot; onclick=&quot;loadTransform()&quot;&gt;Transform&lt;/div&gt;</span>
<span class="pl-c">       &lt;div class=&quot;myButton&quot; id=&quot;background&quot; onclick=&quot;loadControlSelector()&quot;&gt;Control Selector&lt;/div&gt;</span>
<span class="pl-c">    &lt;/div&gt;</span></span></strong>
        
&lt;/<span class="pl-ent">div</span>&gt;<span class="pl-c">&lt;!-- /container --&gt;<br />                                       ：<br /></span></pre>
</li>
<li>最後に、www フォルダ下の css フォルダ内から main.css ファイルを開いて、3. で定義したボタン群のスタイルシート定義のコメント&#0160;<strong>/*</strong>&#0160;と&#0160;<strong>*/</strong>&#0160;を削除します。このスタイルが、ボタンの色やマウス ホバーの際の振る舞いなど、ボタン共通のスタイルを定義しています。<br />
<pre><span class="pl-ent"><span class="pl-c">                                       ：</span><br />h4</span> {
  <span class="pl-c1">color</span>: <span class="pl-c1">white</span>;
}

<span style="color: #0000ff;"><strong><span class="pl-c">.myButton {</span>
<span class="pl-c">      background-color: white;</span>
<span class="pl-c">      color: #4CAF50;</span>
<span class="pl-c">      border: 2px solid #4CAF50;</span>
<span class="pl-c">      border-radius: 8px;</span>
<span class="pl-c">      display:inline-block;</span>
<span class="pl-c">      cursor:pointer;</span>
<span class="pl-c">      font-family:Verdana;</span>
<span class="pl-c">      font-size:17px;</span>
<span class="pl-c">      padding:16px 31px;</span>
<span class="pl-c">      text-decoration:none;</span>
<span class="pl-c">      margin-top: 1em;</span>
<span class="pl-c">      -webkit-transition-duration: 0.4s; </span>
<span class="pl-c">      transition-duration: 0.4s;</span>
<span class="pl-c">}</span>

<span class="pl-c">.myButton:hover {</span>
<span class="pl-c">      background-color: #4CAF50; </span>
<span class="pl-c">      color: white;</span>
<span class="pl-c">}</span>

<span class="pl-c">.myButton:active {</span>
<span class="pl-c">      position:relative;</span>
<span class="pl-c">      top:1px;</span>
<span class="pl-c">}</span></strong></span></pre>
</li>
<li><span class="pl-c">Node.js command prompt&#0160;上で カレント &#0160;ディレクトリが&#0160;viewer-nodejs-tutorial&#0160;フォルダであることを確認したら、コマンド プロンプト上で<strong>&#0160;npm run dev</strong>&#0160;または&#0160;<strong>Node start.js</strong>&#0160;と入力して Node サーバーを起動します。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09da7022970d-pi" style="display: inline;"><img alt="Npm_run" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09da7022970d image-full img-responsive" src="/assets/image_410446.jpg" title="Npm_run" /></a><br /></span></li>
<li>Google Chrome か、他の WebGL がサポートされる Web ブラウザを起動して、URL に localhost:3000 と入力してください。指定したドキュメントが表示されるはずです。前回とは異なり、Viewer 領域の下に 3. でコメントを外したボタンが 5 つ表示されるはずです。ボタンをクリックすることで、Viewer 背景の色を変更したり、Extension をロードして Viewer 上にマークアップを記入したり、オブジェクトを選択して移動や回転を加えることが出来るはずです。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09da7002970d-pi" style="display: inline;"><img alt="Viewer_with_extensions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09da7002970d image-full img-responsive" src="/assets/image_597494.jpg" title="Viewer_with_extensions" /></a><br /><span class="pl-c">各ボタンの振る舞いを動画にしていますので実装内容をご確認ください。[Markup3D] ボタンでロードされるのは、www\extensions フォルダ内の Viewing.Extension.Markup3D.min.js ファイルで定義された Extension です。同様に、[Transform] ボタンでロードされるのは、Viewing.Extension.Transform.min.js ファイルで定義された Extension です。<br /></span>
<p class="asset-video"><iframe allowfullscreen="" frameborder="0" height="344" src="https://www.youtube.com/embed/jUj6XrimelE?feature=oembed" width="459"></iframe></p>
</li>
</ol>
<hr />
<p>オートデスクが提供する Extension に加え、独自に Extension を作成することが出来ます。詳しくは、過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/12/extensions-abailable-on-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer で利用可能な Extension</a></strong>&#0160;をご参照ください。GitHub リポジトリ&#0160;<strong><a href="https://github.com/Autodesk-Forge/library-javascript-viewer-extensions" rel="noopener noreferrer" target="_blank">https://github.com/Autodesk-Forge/library-javascript-viewer-extensions</a></strong> 上でその他の有用な Extension サンプルも公開されています。</p>
<p>By Toshiaki Isezaki</p>
