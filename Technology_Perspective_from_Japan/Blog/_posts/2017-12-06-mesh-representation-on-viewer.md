---
layout: "post"
title: "Viewer でのメッシュ状表示"
date: "2017-12-06 00:10:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/12/mesh-representation-on-viewer.html "
typepad_basename: "mesh-representation-on-viewer"
typepad_status: "Publish"
---

<p>Forge Viewer 上に 3D モデルを表示すると、設定した環境応じた光源がシーンに反映されて 陰影の付いた状態でモデルがレンダリングされます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09db1094970d-pi" style="display: inline;"><img alt="Normal" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09db1094970d image-full img-responsive" src="/assets/image_696846.jpg" title="Normal" /></a></p>
<p>場合によっては、メッシュ状の表現でモデルを表示したいことがあるかもしれませんが、残念ながら、Viewer にはそのような設定は用意されていません。ただし、非公開の Autodesk.Viewing.Wireframes Extension を利用すると、Model Derivative API や Reality Capture API で生成されたポリゴン メッシュ（ワイヤーフレーム）の状態で、3D モデルを表示させることが可能です。利用時には Autodesk.Viewing.Wireframes Extension のロード後に activate() を呼び出してください。</p>
<pre>&#0160;viewer.loadExtension(&#39;Autodesk.Viewing.Wireframes&#39;).then(function(extension){<br />     extension.activate();<br /> });</pre>
<p>ワイヤーフレームを元のレンダリング状態に戻す場合には、Autodesk.Viewing.Wireframes Extension をロード解除するだけです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09db10b6970d-pi" style="display: inline;"><img alt="Wireframe" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09db10b6970d image-full img-responsive" src="/assets/image_629762.jpg" title="Wireframe" /></a></p>
<p>なお、上記コードでは Viewer3D.loadExtension() で明示的に Extension をロードしていますが、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/02/code-evolution-to-show-models-on-forge-viewer.html" rel="noopener noreferrer" target="_blank">Forge Viewer でのモデル表示コードの進化</a></strong> で触れたような&#0160;<strong>Basic Application</strong>&#0160;方式でのロードが推奨されています。</p>
<p>メッシュ（ワイヤフレーム）表示の利用は、端点が分かりやすくなるので、計測ツール利用時のスナップには便利です。</p>
<p>By Toshiaki Isezaki</p>
