---
layout: "post"
title: "Model Derivative API：DWG・RVT の 2D 変換時オプション"
date: "2023-02-22 01:18:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/02/dwg-translation-option-on-model-derivative-api.html "
typepad_basename: "dwg-translation-option-on-model-derivative-api"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/12/model-derivative-api-dwg-optimization.html" rel="noopener" target="_blank">Model Derivative API：DWG 変換の最適化による変更について</a> でご案内のとおり、DWG ファイルを Viewer 表示する際の 2D View &gt;&gt; SVF/SVF2 変換について、Model Derivative API の <a href="https://aps.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/" rel="noopener" target="_blank">POST job</a> エンドポイントに新しい変換オプションが追加されています。</p>
<p>2D View を SmartPDF 化して変換するか否かは、advanced オプションに新設された <strong>2dviews</strong> フィールドで指定することが出来ます。この値に ”<strong>pdf</strong>” を指定すると、2D View が SmartPDF 化して変換され、冗長なプロパティが抽出されなくなります。逆に、2dviews フィールドに &quot;<strong>legacy</strong>&quot; を指定すると、従来のように、すべてのプロパティが抽出されます。<strong>2dviews</strong> フィールドを指定しない場合には、&quot;<strong>legacy</strong>&quot; 指定時と同じくすべてのプロパティが抽出されます。</p>
<div>
<blockquote>
<div>{</div>
<div>&#0160; &#0160; &quot;input&quot;: {</div>
<div>&#0160; &#0160; &#0160; &#0160; &quot;urn&quot;: encodedURN</div>
<div>&#0160; &#0160; },</div>
<div>&#0160; &#0160; &quot;output&quot;: {</div>
<div>&#0160; &#0160; &#0160; &#0160; &quot;formats&quot;: [</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;type&quot;: &quot;svf2&quot;,</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;views&quot;: [&quot;2d&quot;, &quot;3d&quot;],</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;advanced&quot;:</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <strong>&quot;2dviews&quot;: &quot;pdf&quot;</strong></div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; ]</div>
<div>&#0160; &#0160; }</div>
<div>}</div>
</blockquote>
</div>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b685295df4200d-pi" style="display: inline;"><img alt="Pdf_legacy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b685295df4200d image-full img-responsive" src="/assets/image_467196.jpg" title="Pdf_legacy" /></a></p>
<p>SmartPDF を指定する advanced オプションによる変換指定は、Revit の RVT ファイルから 2D シートを Viewer 表示する際の SVF/SVF2 変換にも適用出来ます。指定するフィールド名と値は、DWG の変換時とまったく同じです。</p>
<p>ただし、SmartPDF が有効になるのは、変単対象の RVT ファイルが 2022 バージョン以降に限定されてしまいますので、ご注意ください。2021 バージョンの Revit の RVT ファイルでは「&quot;2dviews&quot;: &quot;pdf&quot;」と指定しても、従来のプロパティが抽出されます。</p>
<p>By Toshiaki Isezaki</p>
