---
layout: "post"
title: "APS Viewer：強調表現"
date: "2023-10-18 00:21:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/10/aps-viewer-highlighting.html "
typepad_basename: "aps-viewer-highlighting"
typepad_status: "Publish"
---

<p>3D モデルを表示する際、特定のオブジェクトを強調して表示したい時があります。このような際、カスタムシェーダーを使用せずに強調表現する簡単な方法をご紹介しておきたいと思います。</p>
<p>最も簡単な方法は、「選択表示」（英語名：Isolate）機能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39ff2ef200b-pi" style="display: inline;"><img alt="Isolate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39ff2ef200b image-full img-responsive" src="/assets/image_252077.jpg" title="Isolate" /></a></p>
<p>オブジェクトを選択して「選択表示」を実行すると、周囲のオブジェクトが非表示になって指定したオブジェクトのみが表示されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39ff31c200b-pi" style="display: inline;"><img alt="Wo_ghost" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39ff31c200b image-full img-responsive" src="/assets/image_298144.jpg" title="Wo_ghost" /></a></p>
<p>もし、周囲の状態も同時に把握したい場合には、Viewer 設定から「ゴースト非表示オブジェクト」を&#0160;<span style="text-decoration: underline;">オン</span>&#0160;にすると、非表示になったオブジェクトが半透明に表示されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a04932200d-pi" style="display: inline;"><img alt="Ghost_setting" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a04932200d image-full img-responsive" src="/assets/image_440538.jpg" title="Ghost_setting" /></a></p>
<p>「選択表示」は、Viewer API でも&#0160;<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#isolate-node-model" rel="noopener" target="_blank">isolate()</a> メソッドで同様の効果を自動化することが可能です。また、「ゴースト非表示オブジェクト」は <a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#setghosting-value" rel="noopener" target="_blank">setGhosting()</a> メソッドでコントロールすることが可能です。ただし、残念ながら、透過度や色は指定することは出来ません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39bdaab200c-pi" style="display: inline;"><img alt="W_ghost" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39bdaab200c image-full img-responsive" src="/assets/image_989029.jpg" title="W_ghost" /></a></p>
<p>オブジェクトの背後にある別のオブジェクトを透過的に協調したいなら、選択表示を利用する方法もあります。</p>
<p>通常、オブジェクトの選択は対象が選択できるよう手前に表示されている際におこないます。この選択動作は、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#select-dbids-model-selectiontype" rel="noopener" target="_blank">select()</a> メソッドでコントロールすることが出来るので、dbId が分かっていれば、マウス操作でオブジェクトをクリックしなくても、オブジェクトを選択状態にすることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39bda23200c-pi" style="display: inline;"><img alt="Selection_color" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39bda23200c image-full img-responsive" src="/assets/image_867018.jpg" title="Selection_color" /></a></p>
<p>オブジェクトの選択色は、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#setselectioncolor-color-selectiontype" rel="noopener" target="_blank">setSelectionColor()</a> メソッドで変更することも出来るので、適宜、目立った色合いを指定が可能です。例えば、NOP_VIEWER.setSelectionColor(new THREE.Color(<strong>0xff0000</strong>)) のように指定すると、赤い選択色で選択が表現されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39bda03200c-pi" style="display: inline;"><img alt="Selection_color" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39bda03200c image-full img-responsive" src="/assets/image_164500.jpg" title="Selection_color" /></a></p>
<p><a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#select-dbids-model-selectiontype" rel="noopener" target="_blank">select()</a> メソッドで選択状態を表現した場合、[ESC] キーで選択状態が解除されてしまいます。選択状態にしなくても対象オブジェクトの色や透過度を指定したい場合には、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#setthemingcolor-dbid-color-model-recursive" rel="noopener" target="_blank">setThemingColor()</a> メソッドでオブジェクト色をオーバーライドすると、選択状態の如何にかかわらず、同色で表示を維持することも出来ます。</p>
<p>なお、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#setthemingcolor-dbid-color-model-recursive" rel="noopener" target="_blank">setThemingColor()</a> メソッドでオブジェクト色をオーバーライドした場合には、透過的にオブジェクトを表示する効果が得られません。また、「部屋」のような空間は表現出来ないため、周囲の面が指定色でオブジェクトが持つマテリアルとのブレンド色で表現されます。例えば、上図にある ”LDK 303” の部屋を NOP_VIEWER.setThemingColor(idArray[0], new THREE.Vector4(1, 0, 0, 1)) のように指定すると、周囲の壁が赤いブレンド色で表現されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a04b07200d-pi" style="display: inline;"><img alt="Colorthema" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a04b07200d image-full img-responsive" src="/assets/image_146606.jpg" title="Colorthema" /></a></p>
<p><a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#setthemingcolor-dbid-color-model-recursive" rel="noopener" target="_blank">setThemingColor()</a> メソッドで設定したブレンド色は、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#clearthemingcolors-model" rel="noopener" target="_blank">clearThemingColors()</a> が呼び出されるまで維持されます。</p>
<p>By Toshiaki Isezaki</p>
