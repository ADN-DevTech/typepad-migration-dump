---
layout: "post"
title: "Model Derivative API での IFC ファイル変換の変更について"
date: "2019-04-08 00:03:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/04/model-derivative-ifc-pipeline-call-change.html "
typepad_basename: "model-derivative-ifc-pipeline-call-change"
typepad_status: "Publish"
---

<div class="node__content adskf__section-group">
<div class="node__image"><img alt="" height="223" src="/assets/switching_0.png" width="772" /></div>
<div class="node__body">
<p>Model Derivative API で IFC ファイルを変換する処理では、現在 Navisworks インポーターをベースにした実装が利用されています。変換結果をより良いものにしていく目的で、今回、代替となる Revit ベースの実装に変更、デプロイされています。インポーターを変更するとデータ形式と内容に影響を与えるため、後述のように既存コードに影響を与えないよう考慮しつつのデプロイとなります。今後、これらの変更を適切に処理するために、コードを更新する可能性がありますのでご注意ください。</p>
<p>移行は2段階で行われます。</p>
<p><strong>フェーズ 1：</strong>今後、数ヶ月間、Model Derivative API を使った IFC ファイル変換のパイプラインでは、既定の変換が Navisworks ベースの実装により処理されます。ただし、下記でご紹介するように、明示的にパラメータを使用して Revit パイプラインのテストをおこなうことが出来ます。</p>
<p><strong>フェーズ 2：</strong>数ヶ月後、Model Derivative API での IFC ファイル変換では、Revit ベースのパイプラインを既定に切り替える予定です。出来るだけ早く新しい Revit パイプラインを使用して IFC ファイルの変換をテストしていただき、問題があれば、ご報告いただくことを強くお勧めします。データ構造等が変わっている可能性もありますので、結果に応じてコードを更新をお願いします。従来の Navisworks パイプラインは、同じパラメータを使用してアクセス可能とする予定です。</p>
<p>新しいパイプラインの検証後、フェーズ 2&#0160; の開始をこのブログでご案内します。何か問題があれば <a href="https://forge.autodesk.com/blog/%22mailto:">forge.help@autodesk.com</a> までお問い合わせください。</p>
<p><strong>フェーズ 1：</strong></p>
<p>Navisworks ベースの実装を使用して IFC ファイルを変換（既定）</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-bash code-overflow-x hljs " id="snippet-0">
curl -X <span class="hljs-string">&#39;POST&#39;</span>
     -H <span class="hljs-string">&#39;Content-Type: application/json; charset=utf-8&#39;</span>
     -H <span class="hljs-string">&#39;Authorization: Bearer PtnrvrtSRpWwUi3407QhgvqdUVKL&#39;</span>
     -v <span class="hljs-string">&#39;https://developer.api.autodesk.com/modelderivative/v2/designdata/job&#39;</span>
     <span class="hljs-operator">-d</span>
      <span class="hljs-string">&#39;{
         &quot;input&quot;: {
           &quot;urn&quot;: &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bW9kZWxkZXJpdmF0aXZlL21vZGVsLmlmYw&quot;,
         },
         &quot;output&quot;: {
           &quot;formats&quot;: [
             {
               &quot;type&quot;: &quot;svf&quot;,
               &quot;views&quot;: [
                 &quot;3d&quot;
               ]
             }
           ]
         }
       }&#39;</span>
</code></pre>
<p>新しいパラメータを利用</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-bash code-overflow-x hljs " id="snippet-1">curl -X <span class="hljs-string">&#39;POST&#39;</span>
     -H <span class="hljs-string">&#39;Content-Type: application/json; charset=utf-8&#39;</span>
     -H <span class="hljs-string">&#39;Authorization: Bearer PtnrvrtSRpWwUi3407QhgvqdUVKL&#39;</span>
     -v <span class="hljs-string">&#39;https://developer.api.autodesk.com/modelderivative/v2/designdata/job&#39;</span>
     <span class="hljs-operator">-d</span>
      <span class="hljs-string">&#39;{
         &quot;input&quot;: {
           &quot;urn&quot;: &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bW9kZWxkZXJpdmF0aXZlL21vZGVsLmlmYw&quot;,
         },
         &quot;output&quot;: {
           &quot;formats&quot;: [
             {
               &quot;type&quot;: &quot;svf&quot;,
               &quot;views&quot;: [
                 &quot;3d&quot;
               ],
<span style="color: #0000ff;"><strong>               &quot;advanced&quot;: {
                 &quot;switchLoader&quot;: false
               }</strong></span>
             }
           ]
         }
       }&#39;</span>
</code></pre>
<p>Revit ベースの実装を使用して（新しいパラメータを使用して）IFC ファイルを変換</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-bash code-overflow-x hljs " id="snippet-2">curl -X <span class="hljs-string">&#39;POST&#39;</span> \
     -H <span class="hljs-string">&#39;Content-Type: application/json; charset=utf-8&#39;</span> \
     -H <span class="hljs-string">&#39;Authorization: Bearer PtnrvrtSRpWwUi3407QhgvqdUVKL&#39;</span>
     -v <span class="hljs-string">&#39;https://developer.api.autodesk.com/modelderivative/v2/designdata/job&#39;</span> \
     <span class="hljs-operator">-d</span>
      <span class="hljs-string">&#39;{
         &quot;input&quot;: {
           &quot;urn&quot;: &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bW9kZWxkZXJpdmF0aXZlL21vZGVsLmlmYw&quot;,
         },
         &quot;output&quot;: {
           &quot;formats&quot;: [
             {
               &quot;type&quot;: &quot;svf&quot;,
               &quot;views&quot;: [
                 &quot;3d&quot;
               ],
<span style="color: #0000ff;"><strong>               &quot;advanced&quot;: {
                 &quot;switchLoader&quot;: true
               }</strong></span>
             }
           ]
         }
       }&#39;</span>
</code></pre>
<p><strong>フェーズ2：</strong></p>
<p>Revit ベースの実装を使用して IFC ファイルを変換（既定）</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-bash code-overflow-x hljs " id="snippet-3">curl -X <span class="hljs-string">&#39;POST&#39;</span>
     -H <span class="hljs-string">&#39;Content-Type: application/json; charset=utf-8&#39;</span>
     -H <span class="hljs-string">&#39;Authorization: Bearer PtnrvrtSRpWwUi3407QhgvqdUVKL&#39;</span>
     -v <span class="hljs-string">&#39;https://developer.api.autodesk.com/modelderivative/v2/designdata/job&#39;</span>
     <span class="hljs-operator">-d</span>
      <span class="hljs-string">&#39;{
         &quot;input&quot;: {
           &quot;urn&quot;: &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bW9kZWxkZXJpdmF0aXZlL21vZGVsLmlmYw&quot;,
         },
         &quot;output&quot;: {
           &quot;formats&quot;: [
             {
               &quot;type&quot;: &quot;svf&quot;,
               &quot;views&quot;: [
                 &quot;3d&quot;
               ]
             }
           ]
         }
       }&#39;</span>
</code></pre>
<p>新しいパラメータを利用</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-bash code-overflow-x hljs " id="snippet-4">curl -X <span class="hljs-string">&#39;POST&#39;</span>
     -H <span class="hljs-string">&#39;Content-Type: application/json; charset=utf-8&#39;</span>
     -H <span class="hljs-string">&#39;Authorization: Bearer PtnrvrtSRpWwUi3407QhgvqdUVKL&#39;</span>
     -v <span class="hljs-string">&#39;https://developer.api.autodesk.com/modelderivative/v2/designdata/job&#39;</span>
     <span class="hljs-operator">-d</span>
      <span class="hljs-string">&#39;{
         &quot;input&quot;: {
           &quot;urn&quot;: &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bW9kZWxkZXJpdmF0aXZlL21vZGVsLmlmYw&quot;,
         },
         &quot;output&quot;: {
           &quot;formats&quot;: [
             {
               &quot;type&quot;: &quot;svf&quot;,
               &quot;views&quot;: [
                 &quot;3d&quot;
               ],
<span style="color: #0000ff;"><strong>               &quot;advanced&quot;: {
                 &quot;switchLoader&quot;: false
               }</strong></span>
             }
           ]
         }
       }&#39;</span>
</code></pre>
<p>Navisworks ベースの実装を使用して（新しいパラメータを使用して）IFC ファイルを変換</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-bash code-overflow-x hljs " id="snippet-5">curl -X <span class="hljs-string">&#39;POST&#39;</span> \
     -H <span class="hljs-string">&#39;Content-Type: application/json; charset=utf-8&#39;</span>
     -H <span class="hljs-string">&#39;Authorization: Bearer PtnrvrtSRpWwUi3407QhgvqdUVKL&#39;</span>
     -v <span class="hljs-string">&#39;https://developer.api.autodesk.com/modelderivative/v2/designdata/job&#39;</span>
     <span class="hljs-operator">-d</span>
      <span class="hljs-string">&#39;{
         &quot;input&quot;: {
           &quot;urn&quot;: &quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bW9kZWxkZXJpdmF0aXZlL21vZGVsLmlmYw&quot;,
         },
         &quot;output&quot;: {
           &quot;formats&quot;: [
             {
               &quot;type&quot;: &quot;svf&quot;,
               &quot;views&quot;: [
                 &quot;3d&quot;
               ],
<span style="color: #0000ff;"><strong>               &quot;advanced&quot;: {
                 &quot;switchLoader&quot;: true
               }</strong></span>
             }
           ]
         }
       }&#39;</span>
</code></pre>
</div>
<div class="node__tags"><label class="adskf__text-caps"></label>By Toshiaki Isezaki</div>
</div>
