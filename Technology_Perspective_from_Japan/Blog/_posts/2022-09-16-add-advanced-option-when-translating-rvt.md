---
layout: "post"
title: "Model Derivative API：RVT 変換時の Advanced オプションの追加"
date: "2022-09-16 00:31:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/09/add-advanced-option-when-translating-rvt.html "
typepad_basename: "add-advanced-option-when-translating-rvt"
typepad_status: "Publish"
---

<p>Revit プロジェクト ファイル（.rvt）を SVF/SVF2 に変換して Forge Viewer にストリーミング配信・表示したり、<a href="https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html" rel="noopener" target="_blank">メタデータを取得</a>したりする際、 <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/" rel="noopener" target="_blank">POST job endpoint</a> を使用します。この変換処理（英語では Translator ないし Extractor と表現）に、新しい Advanced オプションが追加されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed659f2200d-pi" style="display: inline;"><img alt="Header2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed659f2200d image-full img-responsive" src="/assets/image_868100.jpg" title="Header2" /></a></p>
<p>追加された新しい <strong>extractorVersion オプション</strong>は、<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/" rel="noopener" target="_blank">POST job endpoint</a> のリファレンス ドキュメントに記載されています。</p>
<p><a class="asset-img-link" href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/jobs/job-POST/" rel="noopener" style="display: inline;" target="_blank"><img alt="Document" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d4daab7200b image-full img-responsive" src="/assets/image_465010.jpg" title="Document" /></a></p>
<p>ご存じのように、Revit は毎年新しいバージョンがリリースされています。このとき、新しいバージョンの Revit で扱う .rvt ファイルには、それ以前の Revit バージョンと<a href="https://knowledge.autodesk.com/ja/support/revit/learn-explore/caas/sfdcarticles/sfdcarticles/JPN/Backwards-compatibility-of-Revit-with-earlier-releases-of-the-software.html" rel="noopener" target="_blank">下位互換がありません</a>。言い換えれば、Revit は .rvt ファイル形式を毎年変更しています。</p>
<p>このため、Revit プロジェクト ファイル（.rvt）を SVF/SVF2 に変換する（POST job が内部的に使用する）Translator/Extractor プログラムは、新しい .rvt ファイル形式に対応するために毎年更新されています。同様に、Translator/Extractor プログラムに問題があった際の改修（リビジョン アップ）も日々おこなわれています。</p>
<p>追加された <strong>extractorVersion オプション</strong>を指定すると、この変換プログラム（Extractor）のバージョンを指定出来るようになります。この際、指定出来る値は次のとおりです。</p>
<ul>
<li><strong>next</strong>： プレリリース バージョン</li>
<li><strong>previous</strong>：以前のリリース バージョン</li>
</ul>
<p>POST job endpoint 呼び出しで指定 JSON ペイロードは、次のように指定します。</p>
<div>
<blockquote>
<div>
<div>
<div>{</div>
<div>&#0160; &#0160; input: {</div>
<div>&#0160; &#0160; &#0160; &#0160; urn: <em>&lt;&lt;encodedURN&gt;&gt;</em></div>
<div>&#0160; &#0160; },</div>
<div>&#0160; &#0160; output: {</div>
<div>&#0160; &#0160; &#0160; &#0160; formats: [</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; type: &quot;svf2&quot;,</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; views: [</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;2d&quot;,</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;3d&quot;</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ]</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; ],</div>
<div>&#0160; &#0160; &#0160; &#0160; &quot;advanced&quot;: {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;<strong> &quot;extractorVersion&quot;: &quot;previous&quot;</strong> または<strong> &quot;next&quot;</strong></div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
<div>}</div>
</div>
</div>
</blockquote>
</div>
<p>変換プログラム（Extractor）のバージョン番号は、extractorVersion オプションを指定して .rvt ファイルを新規に変換した際のマニフェストにも含まれるようになります。何か変換後の状態に問題が見られる場合、この情報を比較することで、変換に使用されたバージョンを把握して、問題のあるバージョンを特定するのに役立てることが出来ます。</p>
<div>
<blockquote>
<div>{</div>
<div>&#0160; &#0160; &quot;urn&quot;: <em>&lt;&lt;encodedURN&gt;&gt;</em>,</div>
<div>&#0160; &#0160; &quot;derivatives&quot;: [</div>
<div>&#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;<strong> &quot;extractorVersion&quot;: &quot;2024.0.2022.0818&quot;,</strong></div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;hasThumbnail&quot;: &quot;true&quot;,</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;children&quot;: [</div>
</blockquote>
</div>
<p>例えば、現在の Extractor バージョンを 2024.0.2022.<strong>081<span style="background-color: #ffff00;">8</span></strong>、開発チームが改良を加えて評価中の Extractor バージョンを&#0160; 2024.0.2022.<strong>081<span style="background-color: #ffff00;">9 </span></strong>と仮定すると、<strong>next</strong> を指定して 2024.0.2022.<strong>081<span style="background-color: #ffff00;">9</span></strong> バージョンを利用して SVF/SVF2 変換が出来ます。2024.0.2022.<strong>081<span style="background-color: #ffff00;">9</span></strong> の検証が完了して、同バージョンが「現在の」公式バージョンになると、2024.0.2022.<strong>081<span style="background-color: #ffff00;">8</span></strong> バージョンは <strong>previous</strong> 扱いとなり、利用するためには <strong>previous</strong> を指定する必要があります。この際、<strong>next</strong> は 2024.0.2022.<strong>081<span style="background-color: #ffff00;">9</span></strong> のまま維持されます。</p>
<p>もし、「現在の」公式バージョンになった 024.0.2022.<strong>081<span style="background-color: #ffff00;">9</span></strong> で特定のファイル変換等で問題が見つかったような場合には、その修正までの間、<strong>previous</strong> を指定して 2024.0.2022.<strong>081<span style="background-color: #ffff00;">8</span></strong> を利用出来るわけです。また、報告した問題が&#0160; 2024.0.2022.<strong>08<span style="background-color: #ffff00;">20</span></strong>で修正されるような場合、事前に <strong>next</strong> 指定で評価を開始することが出来ます（もし必要があれば）。</p>
<p>通常、運用中のアプリでは extractorVersion オプションを使用する必要はありません。未指定の場合には、常に「現在の」バージョンが使用されますので、特に前述のような理由がないかぎりコードの変更は不要です。運用中のアプリには、常に「現在の」バージョン使用が推奨されます。</p>
<p>By Toshiaki Isezaki</p>
