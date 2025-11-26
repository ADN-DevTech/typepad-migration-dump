---
layout: "post"
title: "APS Viewer ドキュメント：Interactive Examples の追加"
date: "2023-08-14 00:32:26"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/08/aps-viewer-document-adding-interactive-examples.html "
typepad_basename: "aps-viewer-document-adding-interactive-examples"
typepad_status: "Publish"
---

<p><a href="https://aps.autodesk.com/" rel="noopener" target="_blank">APS ポータル（https://aps.autodesk.com/）</a>に記載されている APS Viewer の Developer&#39;s Guide に、<a href="https://codepen.io/" rel="noopener" target="_blank">CodePen</a> を使った&#0160;<a href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/interactive_examples" rel="noopener" target="_blank">Interactive Examples</a> セクションが追加されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6d0b5a5200b-pi" style="display: inline;"><img alt="Interactive_examples" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6d0b5a5200b image-full img-responsive" src="/assets/image_754443.jpg" title="Interactive_examples" /></a></p>
<p>CodePen は、フロントエンドを担当する Web&#0160; デザイナーや Web 開発者が、HTML、CSS、JavaScript コードを対話的に編集しながら、リアルタイムに結果を確認出来る開発環境と考えることが出来ます。今回の Interactive Examples セクションの記載で、APS Viewer の基本的な動作を HTML、CSS、JavaScript タブを切り替え、実際のコードを見ながら確認することが可能になりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6d0b6b8200b-pi" style="display: inline;"><img alt="Codes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6d0b6b8200b image-full img-responsive" src="/assets/image_577041.jpg" title="Codes" /></a></p>
<p>Interactive Examples セクションでは、次の機能を評価するコードをテストすることが出来ます。</p>
<p style="padding-left: 40px;"><a class="adskf__sidebar-link    " href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/interactive_examples/example_1" id="3bbae14f-b55e-6d7e-cbe7-17cfbc503754" rel="noopener" target="_blank">Controlling Viewer State</a></p>
<p style="padding-left: 80px;">[Toggle Explode] ボタンのクリック イベントを利用して viewer.explode() を呼び出し、モデルの状態を 50% の割合で分解表示に切り替えます。</p>
<p style="padding-left: 40px;"><a class="adskf__sidebar-link    " href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/interactive_examples/example_2" id="a9e9acbd-ac42-fd33-611b-f3b173b67321" rel="noopener" target="_blank">Handling Viewer Events</a></p>
<p style="padding-left: 80px;">Viewer の SELECTION_CHANGED_EVENT イベントを利用して、オブジェクト選択が発生するたびに、指定されたインライン関数を呼び出し、ObjectId（dbId）を取得、アラートダイアログに表示します。</p>
<p style="padding-left: 40px;"><a class="adskf__sidebar-link    " href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/interactive_examples/example_3" id="93c833e6-6cc7-4042-eade-7c1a17a5c729" rel="noopener" target="_blank">Querying Model Properties</a></p>
<p style="padding-left: 80px;">テキストボックスに door や frame などのプロパティ名を入力、[Search] ボタンをクリックしてモデルをクエリし、プロパティ名でヒットしたオブジェクトにズーム、選択表示します。</p>
<p style="padding-left: 40px;"><a class="adskf__sidebar-link    " href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/interactive_examples/example_4" id="88a7922e-e755-ee05-b0cf-3af06327c6fb" rel="noopener" target="_blank">Customizing Viewer UI</a></p>
<p style="padding-left: 80px;">Viewer カンバス内のツールバーの右端にカスタムボタンを追加して、onClick イベントハンドラで環境を Photo Booth（スピード写真）に変更します。</p>
<p style="padding-left: 40px;"><a class="adskf__sidebar-link    " href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/interactive_examples/example_5" id="61d2220c-7fb3-7d7c-b074-cb0826503f42" rel="noopener" target="_blank">Customizing Viewer Scene</a></p>
<p style="padding-left: 80px;">[Add Sphere] ボタンのクリックで&#0160; custom-scene シーンの登録の有無をチェックし、存在しない場合はシーンを作成してをランダムに球を配置します。</p>
<p style="padding-left: 40px;"><a class="adskf__sidebar-link    " href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/interactive_examples/example_6" id="d50858bb-65cf-3683-259e-b7be13a6d02e" rel="noopener" target="_blank">Aggregated View</a></p>
<p style="padding-left: 80px;">異なる URN を持つモデル 2 つ（Inventor モデルと Revit モデル）を 1 つの Viewer カンバスに集約表示します。</p>
<p>Viewer 右上の「EDIT ON CODEPEN」をクリックすると、別ウィンドウが開き、直接コードを変更しながら結果を評価することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6d0b6cf200b-pi" style="display: inline;"><img alt="Codepen" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6d0b6cf200b image-full img-responsive" src="/assets/image_649915.jpg" title="Codepen" /></a></p>
<p>例えば、<a class="adskf__sidebar-link    " href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/interactive_examples/example_1" id="3bbae14f-b55e-6d7e-cbe7-17cfbc503754" rel="noopener" target="_blank">Controlling Viewer State</a> の内容の場合、既定のまま [Toggle Explode] ボタンをクリックすると、次の下部のようにモデルが分解表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25c78db200d-pi" style="display: inline;"><img alt="Default" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25c78db200d image-full img-responsive" src="/assets/image_668046.jpg" title="Default" /></a></p>
<p>「JS」タブの黄色く囲った [Toggle Explode] ボタンのクリック&#0160; イベントハンドラを次のように変更すると、ボタンをクリックするたびにツールバーのテーマ色を変えたり、モデル分解時の割合を変更したりすることが出来ます。</p>
<div>
<blockquote>
<div>document.getElementById(&quot;explode&quot;).addEventListener(&quot;click&quot;, function () {</div>
<div>&#0160; &#0160; if (viewer.getExplodeScale() &gt; 0.0) {</div>
<div>&#0160; &#0160; &#0160; viewer.explode(0.0);</div>
<div>&#0160; &#0160; &#0160; <span style="background-color: #bfffff;"><strong>viewer.setTheme(&quot;dark-theme&quot;);</strong></span></div>
<div>&#0160; &#0160; } else {</div>
<div>&#0160; &#0160; &#0160; viewer.explode(<span style="background-color: #bfffff;"><strong>1.5</strong></span>);</div>
<div>&#0160; &#0160; &#0160; <strong><span style="background-color: #ffff00;"><span style="background-color: #bfffff;">viewer.setTheme(&quot;light-theme&quot;);</span></span></strong></div>
<div>&#0160; &#0160; }</div>
<div>&#0160; });</div>
</blockquote>
<div>もちろん、コードの変更はリアルタイムに Viewer カンバスに反映されるので、すぐに結果を確認することが出来ます。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25c7915200d-pi" style="display: inline;"><img alt="Exploded_after_changes" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25c7915200d image-full img-responsive" src="/assets/image_766479.jpg" title="Exploded_after_changes" /></a></div>
</div>
<p>Interactive Examples セクションは、今後も評価出来る APS Viewer の機能（コード）を追加・拡張していく予定です。</p>
<p>By Toshiaki Isezaki</p>
