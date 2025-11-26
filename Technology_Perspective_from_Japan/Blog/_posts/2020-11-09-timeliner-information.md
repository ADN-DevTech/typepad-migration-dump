---
layout: "post"
title: "Model Derivative API：Timeliner 情報の出力"
date: "2020-11-09 00:01:31"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/11/timeliner-information.html "
typepad_basename: "timeliner-information"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be421f57f200d-pi" style="display: inline;"><img alt="Inuse" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be421f57f200d image-full img-responsive" src="/assets/image_703180.jpg" title="Inuse" /></a><br />Forge Viewer で 3D モデルを表示するには、事前に表示したいシードファイル（デザイン ファイル） を SVF 形式に変換する必要があります。SVF 変換自体は <strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/" rel="noopener" target="_blank">POST job</a></strong> endpoint で実行することになりますが、変換するシードファイルのタイプによって、変換オプションを指定出来るものがあります。</p>
<p>以前、<a href="https://adndevblog.typepad.com/technology_perspective/2019/11/rvt-translation-enhancement-on-model-derivative-api.html" rel="noopener" target="_blank"><strong>Model Derivative API での RVT ファイル変換について</strong></a> のブログ記事でご紹介した、Revit プロジェクト（.rvt ファイル）変換時の&#0160; <strong>generateMasterViews</strong> パラメータ指定もその 1 つです。.rvt ファイル変換時に <strong>advanced オプション</strong>として&#0160; <strong>generateMasterViews</strong> パラメータを true に設定すると、「部屋」、「スペース」、「ゾーン」といった情報を 3D ビュー上で表現出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be41d2bf8200d-pi" style="display: inline;"><img alt="Advanced_option" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be41d2bf8200d image-full img-responsive" src="/assets/image_366197.jpg" title="Advanced_option" /></a></p>
<p><strong>advanced オプション</strong>は、他のファイル タイプでも指定可能なものがあります。Navisworks ファイルの SVF 変換時に指定することが出来る <strong>timelinerProperties</strong> パラメータです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be41d2c11200d-pi" style="display: inline;"><img alt="Advanced_option_nwd" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be41d2c11200d image-full img-responsive" src="/assets/image_177511.jpg" title="Advanced_option_nwd" /></a></p>
<p><strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/" rel="noopener" target="_blank">POST job</a></strong> endpoint 実行時のリクエスト ボディは次のようになります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">{<br />   &quot;input&quot;: {<br />     &quot;urn&quot;: &quot;<em>&lt;Your Encoded URN&gt;</em>&quot;<br />   },<br />   &quot;output&quot;: {<br />     &quot;formats&quot;: [<br />       {<br />         &quot;type&quot;: &quot;svf&quot;,<br />         &quot;views&quot;: [<br />           &quot;2d&quot;,<br />           &quot;3d&quot;<br />         ],<br /><strong>         &quot;advanced&quot;: {</strong><br /><strong>            &quot;timelinerProperties&quot;: true</strong><br /><strong>         }</strong><br />       }<br />     ]<br />   }<br /> }</code></pre>
<p>この&#0160;<strong>timelinerProperties</strong> パラメータを true に設定して SVF 変換を実施すると、Forge Viewer へのストリーイング配信で、Navisworks の TimeLiner で指定したタスク情報が、各タスクで選択指定したジオメトリのプロパティとして表示出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e970f5b3200b-pi" style="display: inline;"><img alt="Navisworks" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e970f5b3200b image-full img-responsive" src="/assets/image_702169.jpg" title="Navisworks" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be41d2d84200d-pi" style="display: inline;"><img alt="Timeliner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be41d2d84200d image-full img-responsive" src="/assets/image_272001.jpg" title="Timeliner" /></a></p>
<p>TimeLiner のタスク名などを利用して、Forge Viewer 上で表示制御をおこなえば、Navisworks と同じように、建設フェーズにあわせた施工過程の可視化が可能になります。</p>
<p>デザイン ファイルのタイプによっては、他の advanced オプションも用意されている場合がありますので、<strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/" rel="noopener" target="_blank">POST job</a></strong> endpoint のリファレンスを確認してみてください。</p>
<p>By Toshiaki Isezaki</p>
