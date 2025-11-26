---
layout: "post"
title: "APS Viewer：Initializer メソッドの変更"
date: "2024-03-04 00:08:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/02/aps-viewer-changes-initializer.html "
typepad_basename: "aps-viewer-changes-initializer"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3acaa64200d-pi" style="display: inline;"><img alt="Office-SF-300_Mission-6861" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3acaa64200d image-full img-responsive" src="/assets/image_63380.jpg" title="Office-SF-300_Mission-6861" /></a></p>
<p>SVF2 のロード パフォーマンスを改善した APS Viewer <a href="https://aps.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/#id1" rel="noopener" target="_blank">v7.95</a> から、従来オプション扱いだった <a href="https://aps.autodesk.com/en/docs/viewer/v7/developers_guide/viewer_basics/starting-html/#initializing-the-viewer-for-svf-and-svf2-support" rel="noopener" target="_blank">Initializer</a>&#0160;メソッドの <strong>env</strong> と <strong>api</strong> 指定が必須になっています。従来、両者を利用するコードを多くご紹介していますので、影響は限定的かと思いますが、念のためご確認いただくことをお勧めします。</p>
<pre><code class="language-javascript hljs ">Autodesk.Viewing.Initializer({ env: <span class="hljs-string">&#39;AutodeskProduction2&#39;</span>, api: <span class="hljs-string">&#39;streamingV2&#39;</span>, getAccessToken }, <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-params">()</span> {</span>
   <span class="hljs-keyword">const</span> viewer = <span class="hljs-keyword">new</span> Autodesk.Viewing.GuiViewer3D(container, { extensions });
   viewer.start();
});</code></pre>
<p>もし、ヨーロッパのデータセンターをお使いの場合には、<strong>api</strong> の指定値を <strong>streamingV2_EU</strong>&#0160;とする必要があります。（従来通り）</p>
<pre><code class="language-javascript hljs ">Autodesk.Viewing.Initializer({ env: <span class="hljs-string">&#39;AutodeskProduction2&#39;</span>, api: <span class="hljs-string">&#39;streamingV2_EU&#39;</span>, getAccessToken }, <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-params">()</span> {</span>
   <span class="hljs-keyword">const</span> viewer = <span class="hljs-keyword">new</span> Autodesk.Viewing.GuiViewer3D(container, { extensions });
   viewer.start();
});</code></pre>
<p>両者の指定がない場合、「404 リソースが見つからない」のエラーが返されますのでご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ac4f9f200b-pi" style="display: inline;"><img alt="404_error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ac4f9f200b image-full img-responsive" src="/assets/image_139851.jpg" title="404_error" /></a></p>
<p>By Toshiaki Isezaki</p>
