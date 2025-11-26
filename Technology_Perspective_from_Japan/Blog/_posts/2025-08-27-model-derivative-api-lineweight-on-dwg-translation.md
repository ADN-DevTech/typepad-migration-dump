---
layout: "post"
title: "Model Derivative API：DWG 変換時の「線の太さ」"
date: "2025-08-27 00:10:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/08/model-derivative-api-lineweight-on-dwg-translation.html "
typepad_basename: "model-derivative-api-lineweight-on-dwg-translation"
typepad_status: "Publish"
---

<p>AutoCAD で図面を編集する際、多くの場合、図面上のオブジェクト（図形）に指定する「<a href="https://help.autodesk.com/view/ACD/2026/JPN/?guid=GUID-4B33ACD3-F6DD-4CB5-8C55-D6D0D7130905" rel="noopener" target="_blank">線の太さ</a>」には気とめず、既定値の「BYLAYER」をそのまま利用しているのではないかと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f07ab4200b-pi" style="display: inline;"><img alt="Properties" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f07ab4200b image-full img-responsive" src="/assets/image_486682.jpg" title="Properties" /></a></p>
<p>この場合、オブジェクトに指定した画層の「線の太さ」が採用されることになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861075810200d-pi" style="display: inline;"><img alt="Bylayer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861075810200d img-responsive" src="/assets/image_793956.jpg" title="Bylayer" /></a></p>
<p>「線の太さ」は、AutoCAD での図面編集中や印刷時に反映されることを期待して指定するものですが、Model Derivative API で変換して APS Viewer で表示する場合、AutoCAD 上の振る舞いとは異なる仕様が適用されてしまうので、少し注意が必要です。</p>
<p>「線の太さ」の指定をされたオブジェクトを持つ図面を Model Derivative API で変換、APS Viewer で表示すると、モデル空間の状態が次のように表示されます。ご覧の通り、モデル空間では「線の太さ」がすべて無視されて表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f07c78200b-pi" style="display: inline;"><img alt="Model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f07c78200b img-responsive" src="/assets/image_268457.jpg" title="Model" /></a></p>
<p>これは、AutoCAD が持つ考え方、つまり、モデル空間に詳細を作図し、レイアウト（ペーパー空間）にビューポートを配置（レイアウト）して、モデル空間に作図されたオブジェクトを表示、印刷時の紙のイメージを用意する、といったコンセプトを反映した仕様です。</p>
<p>具体的には、モデル空間に作図された入り組んだオブジェクトを正確に選択するため、意図的に「線の太さ」を無視して表示する仕組みになっています。</p>
<p>逆に、同じ図面のレイアウトを APS Viewer で表示すると、オブジェクトに「線の太さ」が反映されて標示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d9cd7e200c-pi" style="display: inline;"><img alt="Layout_default" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d9cd7e200c img-responsive" src="/assets/image_714590.jpg" title="Layout_default" /></a></p>
<p>ご想像のとおり、これは前述の仕様によるものです。モデル空間と異なり、レイアウトは印刷イメージという位置づけなので、オブジェクトに指定された「線の太さ」はレイアウト（ペーパー空間）には反映されて標示される結果になります。</p>
<p>ここで疑問なのが、オブジェクトの「線の太さ」に直接「既定」の値を指定している場合、あるいは、例のように BYLAYER の「線の太さ」を「既定」にしている場合にも、APS Viewer に表示されたオブジェクトが太く表示されてしまう点です。</p>
<p>実は、「既定」の「線の太さ」は <strong><span style="font-family: terminal, monaco;">0.00 ミリメートル（0.00 mm）</span></strong>ではありません。AutoCAD の <a href="https://help.autodesk.com/view/ACD/2026/JPN/?guid=GUID-1BB43E62-DF93-494E-ACF2-55824ACD5130" rel="noopener" target="_blank">オンラインヘルプ</a> には、次のように記載されています。</p>
<h3 class="title sectiontitle" style="padding-left: 40px;">[線の太さ]</h3>
<p class="p" id="GUID-1BB43E62-DF93-494E-ACF2-55824ACD5130__WSC30CD3D5FAA8F6D813D93F4FFC2D60BB4-7F8F" style="padding-left: 40px;">使用可能な線の太さが表示されます。</p>
<p class="p" id="GUID-1BB43E62-DF93-494E-ACF2-55824ACD5130__GUID-A68886D8-10AA-4D0F-A071-69018E0FB420" style="padding-left: 40px;">線の太さの値は、標準の設定で構成されていて、ByLayer、ByBlock、既定が含まれています。<span style="background-color: #ffff00;">[既定]の値はシステム変数 LWDEFAULT で設定され、その初期値は 0.01 インチまたは <strong>0.25 mm</strong> です。</span>新しい画層では、既定の設定が使用されます。線の太さを 0 にすると、指定した印刷デバイスで使用可能な最も細い線の太さで印刷され、モデル空間ではピクセル幅 1 で表示されます。</p>
<p>あいにく、<a href="https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/" rel="noopener" target="_blank">POST Create Translation Job</a> エンドポイントで DWG 変換時に指定出来る advanced オプションは、<a href="https://adndevblog.typepad.com/technology_perspective/2023/02/dwg-translation-option-on-model-derivative-api.html">Model Derivative API：DWG・RVT の 2D 変換時オプション</a> で触れた SmartPDF 指定（プロパティ抽出の切り替え）のみで、「線の太さ」をコントロールする」オプションはありません。</p>
<p>もし、APS Viewer 上でレイアウト（ペーパー空間）のオブジェクトに「線の太さ」を反映したくない場合には、AutoCAD 上で図面のオブジェクトの「線の太さ」、あるいは、前述の例のように BYLAYER の「線の太さ」を <strong><span style="font-family: terminal, monaco;">0.00 ミリメートル（0.00 mm）</span></strong> に指定してみてください（レイアウトをアクティにした際の「VP の線の太さ」ではなく）。</p>
<p>下図は、BYLAYER の「線の太さ」を <strong><span style="font-family: terminal, monaco;">0.00 ミリメートル（0.00 mm）</span></strong> に指定した例です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861075a4a200d-pi" style="display: inline;"><img alt="0mm" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861075a4a200d image-full img-responsive" src="/assets/image_511568.jpg" title="0mm" /></a></p>
<p>「線の太さ」に <strong><span style="font-family: terminal, monaco;">0.00 ミリメートル（0.00 mm）</span></strong> に指定したオブジェクトは、レイアウト表示の際にも「線の太さ」を反映せずに表示させることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d9cd84200c-pi" style="display: inline;"><img alt="Layout_0mm" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d9cd84200c image-full img-responsive" src="/assets/image_160402.jpg" title="Layout_0mm" /></a></p>
<p>なお、ここまでの内容は、<a href="https://viewer.autodesk.com/" rel="noopener" target="_blank">Autodesk Viewer</a> を用いた場合も同様です。</p>
<p>By Toshiaki Isezaki</p>
