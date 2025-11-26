---
layout: "post"
title: "Model Derivative API：DWG 変換の最適化による変更について"
date: "2022-12-19 00:01:11"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/12/model-derivative-api-dwg-optimization.html "
typepad_basename: "model-derivative-api-dwg-optimization"
typepad_status: "Publish"
---

<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14aaa9e9200b-pi" style="display: inline;"><img alt="Properties for a circle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14aaa9e9200b image-full img-responsive" src="/assets/image_795434.jpg" title="Properties for a circle" /></a></p>
<p>パフォーマンスと業界での互換性は、Model Derivative API による変換処理にとって重要な要素です。この点を念頭に、以前、<a href="https://adndevblog.typepad.com/technology_perspective/2022/07/call-feedback-model-derivative-dwg-translation-changes.html" rel="noopener" target="_blank">ご意見募集：Model Derivative API による DWG 変換の変更について</a> でご案内したとおり、DWG 2D ビューの変換処理において、現在、SmartPDF 化と抽出プロパティ数の削減の変更準備を進めています。その他の詳細については、前述の<a href="https://adndevblog.typepad.com/technology_perspective/2022/07/call-feedback-model-derivative-dwg-translation-changes.html" rel="noopener" target="_blank">ブログ記事</a>をご確認ください。主な変更点は、（１）異なる dbId、（２）図面範囲の可視化、（３）抽出されなくなるプロパティ、の 3 点です。</p>
<p>なお、この変更は（既存の）変換済モデルには影響を与えません。変更は、今後、新たに変換されるモデルに影響することになります。Autodesk Construction Cloud プロジェクトでは、この変更を徐々に展開していく予定です。 変更がお客様のワークフローに影響を与える場合、または不明な点がある場合には、<a href="mailto:APS.Help@autodesk.com">APS.Help@autodesk.com</a> までご連絡いただき、追加のフィードバックとフォローアップをお願いいたします。</p>
<h3>1. 異なるdbId</h3>
<p>モデルが変換される際には、各要素に連続した dbId が割り当てられます。動作が保証はされている訳ではありませんが、<span style="text-decoration: underline;">まったく</span>同じモデルを 2 回変換した場合、割り当てられる dbId は同じ値になるはずです。今回、2D 情報の SmartPDF 化によって、今後、DWG 内のエンティティは異なる dbId を持つようになります。もし、ワークフローがそれらの値に依存している場合には、変更が必要になる場合があります。次の画像は、現在の動作と SmartPDF 対応後の動作を比較するもので、同じ要素が異なる dbId を持つ例を示しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c9458ed200d-pi" style="display: inline;"><img alt="Current_next_dbid" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c9458ed200d image-full img-responsive" src="/assets/image_595465.jpg" title="Current_next_dbid" /></a></p>
<h3>2. 図面範囲の可視化</h3>
<p>変換された DWG ファイルは、図面領域を示す白い背景と外側の領域を示す灰色の背景を持つようになります。次の画像は、この新しい動作を示しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af148916bd200c-pi" style="display: inline;"><img alt="Viewer showing DWG with gray area_0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af148916bd200c image-full img-responsive" src="/assets/image_452216.jpg" title="Viewer showing DWG with gray area_0" /></a></p>
<h3>3. 抽出されなくなるプロパティ</h3>
<p>最も大きな変更点は、DWG モデル内の各要素に対して抽出されるプロパティ数が削減される点です。Autodesk Platform Services（旧 Forge）では、調査の結果、導入から今日までの数年間、多くのプロパティが一度も使用されず、変換処理に必要なリソースを無意味に消費し、抽出時間が肥大化していることがわかりました。今後、これら、利用されないプロパティは抽出されないように変更されます。例として、代表的な線分（LINE）について、次の表に、現在と今後の状態を示します。</p>
<table border="1">
<tbody>
<tr>
<td class="text-align-center" colspan="8" style="background-color: #1d13e8;"><span style="color: #ffffff;">現在の動作</span></td>
</tr>
<tr>
<td style="background-color: #81e4f7;">displayName</td>
<td style="background-color: #81e4f7;">displayValue</td>
<td style="background-color: #81e4f7;">displayCategory</td>
<td style="background-color: #81e4f7;">attributeName</td>
<td style="background-color: #81e4f7;">type</td>
<td style="background-color: #81e4f7;">units</td>
<td style="background-color: #81e4f7;">hidden</td>
<td style="background-color: #81e4f7;">precision</td>
</tr>
<tr>
<td><em>parent</em></td>
<td><em>8</em></td>
<td><em>__parent__</em></td>
<td><em>parent</em></td>
<td><em>11</em></td>
<td>&#0160;</td>
<td><em>1</em></td>
<td><em>0</em></td>
</tr>
<tr>
<td><em>viewable_in</em></td>
<td><em>Model</em></td>
<td><em>__viewable_in__</em></td>
<td><em>viewable_in</em></td>
<td><em>20</em></td>
<td>&#0160;</td>
<td><em>1</em></td>
<td><em>0</em></td>
</tr>
<tr>
<td><em>viewable_in</em></td>
<td><em>Model-3D</em></td>
<td><em>__viewable_in__</em></td>
<td><em>viewable_in</em></td>
<td><em>20</em></td>
<td>&#0160;</td>
<td><em>1</em></td>
<td><em>0</em></td>
</tr>
<tr>
<td>elementId</td>
<td>29B</td>
<td>&#0160;</td>
<td>elementId</td>
<td>20</td>
<td>&#0160;</td>
<td>1</td>
<td>0</td>
</tr>
<tr>
<td>type</td>
<td>AcDbLine</td>
<td>&#0160;</td>
<td>type</td>
<td>20</td>
<td>&#0160;</td>
<td>1</td>
<td>0</td>
</tr>
<tr>
<td>Handle</td>
<td>29b</td>
<td>General</td>
<td>Handle</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Name</td>
<td>Line</td>
<td>General</td>
<td>Name</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Color</td>
<td>ByLayer</td>
<td>General</td>
<td>Color</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Layer</td>
<td>0</td>
<td>General</td>
<td>Layer</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Linetype</td>
<td>ByLayer</td>
<td>General</td>
<td>Linetype</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Linetype scale</td>
<td>1</td>
<td>General</td>
<td>Linetype scale</td>
<td>3</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Plot style</td>
<td>ByColor</td>
<td>General</td>
<td>Plot style</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Lineweight</td>
<td>ByLayer</td>
<td>General</td>
<td>Lineweight</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Transparency</td>
<td>ByLayer</td>
<td>General</td>
<td>Transparency</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Thickness</td>
<td>0</td>
<td>General</td>
<td>Thickness</td>
<td>3</td>
<td>mm</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Material</td>
<td>ByLayer</td>
<td>3D Visualization</td>
<td>Material</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Length</td>
<td>338.0314756820202</td>
<td>Geometry</td>
<td>Length</td>
<td>3</td>
<td>mm</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Angle</td>
<td>274.6012071318744</td>
<td>Geometry</td>
<td>Angle</td>
<td>3</td>
<td>deg</td>
<td>false</td>
<td>0</td>
</tr>
</tbody>
</table>
<p>&#0160;</p>
<table border="1">
<tbody>
<tr>
<td class="text-align-center" colspan="8" style="background-color: #1d13e8;"><span style="color: #ffffff;">次期動作</span></td>
</tr>
<tr>
<td style="background-color: #81e4f7;">displayName</td>
<td style="background-color: #81e4f7;">displayValue</td>
<td style="background-color: #81e4f7;">displayCategory</td>
<td style="background-color: #81e4f7;">attributeName</td>
<td style="background-color: #81e4f7;">type</td>
<td style="background-color: #81e4f7;">units</td>
<td style="background-color: #81e4f7;">hidden</td>
<td style="background-color: #81e4f7;">precision</td>
</tr>
<tr>
<td><em>parent</em></td>
<td><em>4</em></td>
<td><em>__parent__</em></td>
<td><em>parent</em></td>
<td><em>11</em></td>
<td>&#0160;</td>
<td><em>1</em></td>
<td><em>0</em></td>
</tr>
<tr>
<td><em>viewable_in</em></td>
<td><em>Model</em></td>
<td><em>__viewable_in__</em></td>
<td><em>viewable_in</em></td>
<td><em>20</em></td>
<td>&#0160;</td>
<td><em>1</em></td>
<td><em>0</em></td>
</tr>
<tr>
<td><em>viewable_in</em></td>
<td><em>Model</em></td>
<td><em>__viewable_in__</em></td>
<td><em>viewable_in</em></td>
<td><em>20</em></td>
<td>&#0160;</td>
<td><em>1</em></td>
<td><em>0</em></td>
</tr>
<tr>
<td>elementId</td>
<td>29B</td>
<td>&#0160;</td>
<td>elementId</td>
<td>20</td>
<td>&#0160;</td>
<td>1</td>
<td>0</td>
</tr>
<tr>
<td>type</td>
<td>AcDbLine</td>
<td>&#0160;</td>
<td>type</td>
<td>20</td>
<td>&#0160;</td>
<td>1</td>
<td>0</td>
</tr>
<tr>
<td>Handle</td>
<td>29b</td>
<td>General</td>
<td>Handle</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Layer</td>
<td>0</td>
<td>General</td>
<td>Layer</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Name</td>
<td>Line</td>
<td>General</td>
<td>Name</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
<tr>
<td>Linetype</td>
<td>ByLayer</td>
<td>General</td>
<td>Linetype</td>
<td>20</td>
<td>&#0160;</td>
<td>false</td>
<td>0</td>
</tr>
</tbody>
</table>
</div>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/model-derivative-dwg-translation-optimizations" rel="noopener" target="_blank">Model Derivative DWG translation optimizations | Autodesk Platform Services</a> から転写・翻訳して一部加筆。修正したものです。</p>
<p>By Toshiaki Isezaki</p>
