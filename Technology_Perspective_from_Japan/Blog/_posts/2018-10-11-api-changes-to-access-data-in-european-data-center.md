---
layout: "post"
title: "欧州データ センターを利用する場合の一部 endpoint の変更"
date: "2018-10-11 21:34:48"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/10/api-changes-to-access-data-in-european-data-center.html "
typepad_basename: "api-changes-to-access-data-in-european-data-center"
typepad_status: "Publish"
---

<p>以前、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/06/bim-360-docs-and-data-management-api-access.html" rel="noopener noreferrer" target="_blank">BIM 360 Docs と Data Management API アクセス</a></strong> のブログ記事でも触れていますが、BIM 360 Docs の契約時には、利用するストレージを持つデータセンターの場所を、US と EMEA の 2 箇所から 1 つを選択指定するが出来ます。この場合、一部 endpoint に変更が生じていますので、念のためご紹介しておきます。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/02/general-data-protection-regulation.html" rel="noopener noreferrer" target="_blank">GDPR</a> </strong>対応の一環と思います。なお、本ブログ記事は Forge ブログ記事&#0160;<strong><a href="https://forge.autodesk.com/blog/bim-360-docs-api-changes-access-data-european-data-center" rel="noopener noreferrer" target="_blank">BIM 360 Docs: API Changes to Access Data in European Data Center</a></strong> を踏襲するものです。</p>
<hr />
<div class="blog__image">
<div class="clearfix text-formatted">
<p><strong>注意: コードの非互換性を生む変更があります。 欧州（ヨーロッパ）のデータセンターで BIM 360 Docs を使用するお客様をお持ちの場合、新しい endpoint を使用するようコードを修正する必要があります。</strong></p>
<p>BIM 360 Docs は欧州でのデータセンターの運用を始めていますが、 残念ながら、今回の変更により、BIM 360 Docs のモデルにアクセスするアプリケーション開発者は、ヨーロッパのデータセンターに格納されているモデルにアクセスするためにコードを変更する必要があります。</p>
</div>
</div>
<div class="blog__body node__body">
<p>ご存知のとおり、BIM 360 APIは、アカウント管理機能など、いくつかのリソースに対して既に異なる 米国 および EU（欧州）の endpoint を使用しています。 詳細は<strong><a href="https://forge.autodesk.com/en/docs/viewer/v6/tutorials/basic-application/" rel="noopener noreferrer" target="_blank">こちらのドキュメント</a></strong>をご参照ください。 Data Management API は影響を受けません。</p>
<div class="blog__content--full">
<div class="blog__body node__body">
<p><strong>影響のある endpoint 一覧</strong></p>
<p><strong>Model Derivative API</strong></p>
<p>次の Model Derivative API をお使いの場合には、欧州データ センターに保存されているデータにアクセスする endpoint に明示的に地域情報を含めるように変更する必要があります。</p>
<ul>
<li>POST references</li>
<li>GET :urn/thumbnail</li>
<li>GET :urn/manifest</li>
<li>DELETE :urn/manifest</li>
<li>GET :urn/manifest/:derivativeurn</li>
<li>GET :urn/metadata</li>
<li>GET :urn/metadata/:guid</li>
<li>GET :urn/metadata/:guid/properties</li>
</ul>
<p style="padding-left: 30px;">例）</p>
<p style="padding-left: 30px;">米国データセンター<br />https://developer.api.autodesk.com/modelderivative/v2/designdata/:urn/manifest</p>
<p style="padding-left: 30px;">欧州データセンター<br />https://developer.api.autodesk.com/modelderivative/v2<strong>/regions/eu</strong>/designdata/:urn/manifest</p>
<p><strong>Viewer</strong></p>
<p>Viewer の場合、欧州データセンターで BIM 360 Docs にアップロードされたモデルを表示する場合、初期化処理でオプション パラメータとして &#39;derivativeV2_EU&#39; を指定する必要があります。</p>
<p><a href="https://forge.autodesk.com/en/docs/viewer/v6/tutorials/basic-application/" rel="noopener noreferrer" target="_blank"><strong>ViewingApplication</strong></a> を使用する場合は、bucketKeyが EMEA（欧州） か US(米国） かどうかを判断する必要があります。 base64で URN をデコードし、urn：adsk.wipemea：xxx（欧州の場合）またはurn：adsk.wipprod：xxx（米国の場合）が含まれているかどうかを確認します。</p>
<p>Viewer コードでは、米国と欧州の両方の地域で動作するよう、以下のような実装を試行することができます：</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-0">  <span class="hljs-keyword">var</span> options = {
    env: <span class="hljs-string">&#39;AutodeskProduction&#39;</span>,
    getAccessToken: getForgeToken,
    api: <span class="hljs-string">&#39;derivativeV2&#39;</span> + (atob(urn.replace(<span class="hljs-string">&#39;_&#39;</span>, <span class="hljs-string">&#39;/&#39;</span>)).indexOf(<span class="hljs-string">&#39;emea&#39;</span>) &gt; -<span class="hljs-number">1</span> ? <span class="hljs-string">&#39;_EU&#39;</span> : <span class="hljs-string">&#39;&#39;</span>)
  };
  <span class="hljs-keyword">var</span> documentId = <span class="hljs-string">&#39;urn:&#39;</span> + urn;
  Autodesk.Viewing.Initializer(options, <span class="hljs-function"><span class="hljs-keyword">function</span> <span class="hljs-title">onInitialized</span><span class="hljs-params">()</span> {</span>
      <span class="hljs-comment">// rest of code here…</span>
  });
</code></pre>
<div class="clipboard-container" data-clipboard-target="#snippet-0" data-on-clipboard="tooltip" data-placement="left" data-title="Copied!" data-trigger="manual" data-trigger-clipboard="data-trigger-clipboard"><a href="https://www.w3schools.com/jsref/met_win_atob.asp">atob</a>&#0160; JavaScript関数はbase64文字列をデコードします。 Forgeは_（アンダースコア）の代わりに/（スラッシュ）を使用するため、 &#39;replace&#39;が必要です。 コードは、apiオプションを次のように設定します。</div>
<ul>
<li><strong>derivativeV2&#0160;</strong>(米国)&#0160;&#0160;</li>
<li><strong>derivativeV2_EU&#0160;</strong>(欧州).</li>
</ul>
<p><strong><a href="https://forge.autodesk.com/" rel="noopener noreferrer" target="_blank">Forge ポータル</a></strong>内の SDK ドキュメントもまもなく更新される予定です。</p>
<p>ご不便をお掛けして大変申し訳ございません。本変更に関するご質問は、<a href="mailto:forge.help@autodesk.com">forge.help@autodesk.com</a> までお問い合わせください。</p>
<hr />
<p>By Toshiaki Isezaki&#0160;</p>
</div>
</div>
</div>
