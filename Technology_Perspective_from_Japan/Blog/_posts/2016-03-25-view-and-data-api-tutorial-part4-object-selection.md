---
layout: "post"
title: "View and Data API チュートリアル ～ その4 ～ オブジェクト選択"
date: "2016-03-25 01:14:08"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part4-object-selection.html "
typepad_basename: "view-and-data-api-tutorial-part4-object-selection"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part3-basic-extension.html" target="_blank">前回</a></strong>、基本的な Extension を実装したチュートリアルに、オブジェクト選択の処理を追加します。ここで紹介する内容は、<strong><a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data" target="_blank">Autodesk View &amp; Data API – Getting Started Tutorial リポジトリ</a></strong>&#0160;の<a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data/blob/master/chapter-3.md#Chapter3" target="_blank">&#0160;<strong>Chapter 3</strong>&#0160;</a>の Step 5 に該当するものです。</p>
<p>Extension 内に実装することで、Extension をロードするまで、オブジェクト選択が出来ないことに注意してください。</p>
<p>オブジェクトの選択には、イベント処理を利用します。View and Data API には、オブジェクト選択を検出する&#0160;SELECTION_CHANGED_EVENT イベントが用意されているので、このイベント ハンドラを宣言して実装するだけで、オブジェクトを選択した後の各種処理を実装していくことが可能です。</p>
<ol>
<li>前回追加した Extension ファイル、Viewing.Extension.Workshop.js を Adobe Brackets などのテキスト エディタで開いて、次の太字部分を追記してください。ジオメトリをロードされた際に表示していた alert() は、コメントにし表示しないように変更してください。<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;">  <span class="pl-c1">_self</span>.<span class="pl-en">load</span> <span class="pl-k">=</span> <span class="pl-k">function</span> () {

<strong>    <span class="pl-smi">_viewer</span>.<span class="pl-en">addEventListener</span>(
      <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-c1">SELECTION_CHANGED_EVENT</span>,
      <span class="pl-smi">_self</span>.<span class="pl-smi">onSelectionChanged</span>);
</strong>
   <span style="color: #737373;"> //alert(&#39;Viewing.Extension.Workshop loaded&#39;);</span><br />    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Viewing.Extension.Workshop loaded<span class="pl-pds">&#39;</span></span>);

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
  }<br /></strong></span></pre>
</li>
<li><span style="font-family: tahoma, arial, helvetica, sans-serif;">Viewing.Extension.Workshop.js を上書き保存して、Node.js で Web サーバーを再起動してから、Web ブラウザで localhost:3000 を表示してください。モデル表示後にオブジェクト選択が可能になりますが、現在のコードではオブジェクトを選択しても何も起こりません。イベント ハンドラが正しく動作しているかは、Web ブラウザのデバッグ環境で確かめることができます。例えば、Google Chrome をお使いの場合には、3D モデルの表示後に F12 キーを押すことで、デバッグ画面を表示させることが可能です。あとは、Viewing.Extension.Workshop.js の適切な位置にブレーク ポイントを置いて、オブジェクトを選択するだけです。このコードでは、選択したオブジェクトの ID が変数 dbId に代入される過程を把握することが出来るはずです。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8246df2970b-pi" style="display: inline;"><img alt="Debug_selection" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8246df2970b image-full img-responsive" src="/assets/image_495327.jpg" title="Debug_selection" /></a><br /></span></li>
</ol>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/04/view-and-data-api-tutorial-part5-displaying-panel.html" target="_blank">次回</a></strong>&#0160;は、選択したオブジェクトの各種プロパティを、ビューア上のパネルに表示する実装をしていきます。</p>
<p>By Toshiaki Isezaki</p>
