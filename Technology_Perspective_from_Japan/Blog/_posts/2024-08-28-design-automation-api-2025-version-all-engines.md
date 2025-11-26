---
layout: "post"
title: "Design Automation API：2025 バージョン相当エンジン"
date: "2024-08-28 03:13:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/08/design-automation-api-2025-version-all-engines.html "
typepad_basename: "design-automation-api-2025-version-all-engines"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ba3208200c-pi" style="display: inline;"><img alt="Da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ba3208200c image-full img-responsive" src="/assets/image_74948.jpg" title="Da" /></a></p>
<p>ご案内が遅くなってしまいましたが、今春リリースされた AutoCAD、Revit、Inventor、3ds Max の 2025 バージョンに対応するエンジンが Design Automation API でお使いいただけるようになっています。</p>
<h4><strong>AutoCAD</strong></h4>
<pre><code class="language-json hljs ">{
    &quot;<span class="hljs-attribute">productVersion</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;25.0&quot;</span></span>,
    &quot;<span class="hljs-attribute">deprecationDate</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;2028-03-29&quot;</span></span>,
    &quot;<span class="hljs-attribute">description</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;AutoCAD 2025 (Venn) Core Engine&quot;</span></span>,
    &quot;<span class="hljs-attribute">version</span>&quot;: <span class="hljs-value"><span class="hljs-number">39</span></span>,
    &quot;<span class="hljs-attribute">id</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;Autodesk.AutoCAD+25_0&quot;</span>
</span>}</code></pre>
<h4><strong>Revit</strong></h4>
<pre><code class="language-json hljs ">{
    &quot;<span class="hljs-attribute">productVersion</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;25.0&quot;</span></span>,
    &quot;<span class="hljs-attribute">deprecationDate</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;2028-03-29&quot;</span></span>,
    &quot;<span class="hljs-attribute">description</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;Revit 2025 (RVTDA 04-04-2024).&quot;</span></span>,
    &quot;<span class="hljs-attribute">version</span>&quot;: <span class="hljs-value"><span class="hljs-number">117</span></span>,
    &quot;<span class="hljs-attribute">id</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;Autodesk.Revit+2025&quot;</span>
</span>}</code></pre>
<h4><strong>Inventor</strong></h4>
<pre><code class="language-json hljs ">{
    &quot;<span class="hljs-attribute">productVersion</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;29.00&quot;</span></span>,
    &quot;<span class="hljs-attribute">deprecationDate</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;2028-03-29&quot;</span></span>,
    &quot;<span class="hljs-attribute">description</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;Inventor 2025&quot;</span></span>,
    &quot;<span class="hljs-attribute">version</span>&quot;: <span class="hljs-value"><span class="hljs-number">44</span></span>,
    &quot;<span class="hljs-attribute">id</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;Autodesk.Inventor+2025&quot;</span>
</span>}</code></pre>
<h4><strong>3ds Max</strong></h4>
<pre><code class="language-json hljs ">{
    &quot;<span class="hljs-attribute">productVersion</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;27.1.0.11275&quot;</span></span>,
    &quot;<span class="hljs-attribute">description</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;3dsMax 2025&quot;</span></span>,
    &quot;<span class="hljs-attribute">version</span>&quot;: <span class="hljs-value"><span class="hljs-number">62</span></span>,
    &quot;<span class="hljs-attribute">id</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;Autodesk.3dsMax+2025&quot;</span>
</span>}</code></pre>
<p>AutoCAD と Revit の旧エンジンで .NET Framework を利用していた AppBundle 内のアドイン/プラグインは、.NET に移行していただく必要があります。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2024/04/autocad-2025-dotnet8-migration.html" rel="noopener" target="_blank">AutoCAD 2025 .NET 8 へのアドイン移植</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2024/05/revit-2025-dotnet8-migration.html" rel="noopener" target="_blank">Revit 2025 .NET 8 へのアドイン移植 </a></li>
</ul>
<p>By Toshiaki Isezaki</p>
