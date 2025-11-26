---
layout: "post"
title: "Direct S3 アプローチ対応（暫定）SDK"
date: "2022-10-14 00:06:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/10/sirect-s3-upload-and-download-with-sdks.html "
typepad_basename: "sirect-s3-upload-and-download-with-sdks"
typepad_status: "Publish"
---

<p data-line="2" dir="auto"><span style="background-color: #ffffff;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02acc60f8aaf200b-pi" style="display: inline;"><img alt="Image" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02acc60f8aaf200b image-full img-responsive" src="/assets/image_157404.jpg" title="Image" /></a><br /></span></p>
<p data-line="2" dir="auto"><span style="background-color: #ffff00;">ご注意：このブログ記事で紹介している SDK は、当社の開発者支援チームによって開発および保守されているものです。Autodesk Platform Services の公式 SDK を提供する取り組みは現在進行中であり、来年早々には公式 OSS SDK をリリースする予定です。</span></p>
<p data-line="2" dir="auto">2022年3月、<a href="https://adndevblog.typepad.com/technology_perspective/2022/03/data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach.html" rel="noopener" target="_blank">Data Management OSS (Object Storage Service) の Direct-to-S3 アプローチへの移行について</a> も記事でデータをアップロード/ダウンロードするための新しい最適化されたアプローチと、.NET および Node.js 用の新しいロジックの参照実装を発表しました。今回、新しい Direct S3 アプローチによるアップロード/ダウンロードが <a data-href="https://forge.autodesk.com/blog/direct-s3-net-samples" href="https://adndevblog.typepad.com/technology_perspective/2022/04/direct-to-s3-net-samples.html" rel="noopener" target="_blank" title="https://forge.autodesk.com/blog/direct-s3-net-samples">.NET</a> と &#0160;<a data-href="https://forge.autodesk.com/blog/direct-s3-nodejs-samples" href="https://adndevblog.typepad.com/technology_perspective/2022/04/direct-to-s3-nodejs-samples.html" rel="noopener" target="_blank" title="https://forge.autodesk.com/blog/direct-s3-nodejs-samples">Node.js</a> 用の SDK にも追加されましたので、アプリケーションで新しい効率的なデータ転送を有効にする方法をご案内いたします。</p>
<h2 data-line="4" dir="auto" id="uploading-to-oss">OSS へのアップロード</h2>
<p data-line="6" dir="auto">OSS（Object Storage Service）にデザインファイルをアップロードする場合、アプリケーションは通常、例えば次のように SDK の <code>ObjectsApi#uploadObject</code> メソッドを使用されているはずです。</p>
<p data-line="8" dir="auto">.NET:</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-typescript code-overflow-x hljs cs" id="snippet-0"><span class="hljs-comment">// ...</span>

<span class="hljs-keyword">var</span> api = <span class="hljs-keyword">new</span> ObjectsApi();
api.Configuration.AccessToken = token;
<span class="hljs-keyword">var</span> obj = <span class="hljs-keyword">await</span> api.UploadObjectAsync(bucketKey, objectKey, contentLength, content);

<span class="hljs-comment">// ...</span></code></pre>
<div class="clipboard-container" data-clipboard-target="#snippet-0" data-on-clipboard="tooltip" data-placement="left" data-title="Copied!" data-trigger="manual" data-trigger-clipboard="data-trigger-clipboard">&#0160;</div>
<p data-line="20" dir="auto">Node.js:</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-1"><span class="hljs-comment">// ...</span>

<span class="hljs-keyword">const</span> api = <span class="hljs-keyword">new</span> ObjectsApi();
<span class="hljs-keyword">const</span> obj = await api.uploadObject(bucketKey, objectKey, contentLength, content, {}, <span class="hljs-literal">null</span>, token);

<span class="hljs-comment">// ...</span></code></pre>
<p data-line="31" dir="auto">このメソッドはまだ利用可能ですが、現在は非推奨としてマークされています。S3 への直接アップロードに切り替えるには、当社の SDK の最新バージョンにアップグレードし、代わりに <code>ObjectsApi#uploadResources</code> メソッドを使用してください。</p>
<p data-line="33" dir="auto">.NET:</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-typescript code-overflow-x hljs cs" id="snippet-2"><span class="hljs-comment">// Upgrade the `Autodesk.Forge` NuGet package to version 1.9.7 or newer</span>
<span class="hljs-keyword">var</span> api = <span class="hljs-keyword">new</span> ObjectsApi();
api.Configuration.AccessToken = token;
<span class="hljs-keyword">var</span> results = <span class="hljs-keyword">await</span> api.uploadResources(bucketKey, <span class="hljs-keyword">new</span> List&lt;UploadItemDesc&gt; {
    <span class="hljs-keyword">new</span> UploadItemDesc(objectKey, content)
});</code></pre>
<p data-line="44" dir="auto">&#0160;</p>
<p data-line="44" dir="auto">Node.js:</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-3"><span class="hljs-comment">// Upgrade the `forge-apis` npm package to version 0.9.4 or newer</span>
<span class="hljs-keyword">const</span> api = <span class="hljs-keyword">new</span> ObjectsApi();
<span class="hljs-keyword">const</span> results = await api.uploadResources(
    bucketKey,
    [{ objectKey: objectKey, data: content }],
    {},
    <span class="hljs-literal">null</span>,
    token
);</code></pre>
<p data-line="58" dir="auto"><code>uploadResources</code> メソッドは、同時にアップロードされる複数のオブジェクトを受け入れ、アップロードされた個々のオブジェクトに対応する結果のリストを常に返します。各結果には、<code>error</code>（アップロードが失敗したかどうかを示すブール値のフラグ）、<code>completed</code> （アップロードに対する完全なレスポンスで、成功した場合はオブジェクトの詳細、失敗した場合はエラーの詳細が含まれます）などのプロパティが含まれます。</p>
<p data-line="62" dir="auto">.NET:</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-typescript code-overflow-x hljs cs" id="snippet-4"><span class="hljs-keyword">var</span> results = <span class="hljs-keyword">await</span> api.uploadResources(bucketKey, <span class="hljs-keyword">new</span> List&lt;UploadItemDesc&gt; {
    <span class="hljs-keyword">new</span> UploadItemDesc(objectKey1, content1),
    <span class="hljs-keyword">new</span> UploadItemDesc(objectKey2, content2),
    <span class="hljs-keyword">new</span> UploadItemDesc(objectKey3, content3)
});
<span class="hljs-keyword">foreach</span> (<span class="hljs-keyword">var</span> result <span class="hljs-keyword">in</span> results)
{
    <span class="hljs-keyword">if</span> (result.Error)
    {
        <span class="hljs-keyword">throw</span> <span class="hljs-keyword">new</span> Exception(<span class="hljs-keyword">string</span>.Format(<span class="hljs-string">&quot;Upload failed: {0}&quot;</span>, result.completed.ToString()));
    }
    <span class="hljs-keyword">else</span>
    {
        <span class="hljs-keyword">var</span> json = result.completed.ToJson();
        Console.WriteLine(json.ToObject&lt;ObjectDetails&gt;());
    }
}</code></pre>
<div class="clipboard-container" data-clipboard-target="#snippet-4" data-on-clipboard="tooltip" data-placement="left" data-title="Copied!" data-trigger="manual" data-trigger-clipboard="data-trigger-clipboard">&#0160;</div>
<p data-line="84" dir="auto">Node.js:</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-5"><span class="hljs-keyword">const</span> results = await api.uploadResources(
    bucketKey,
    [
        { objectKey: objectKey1, data: content1 },
        { objectKey: objectKey2, data: content2 },
        { objectKey: objectKey3, data: content3 }
    ],
    {},
    <span class="hljs-literal">null</span>,
    token
);
<span class="hljs-keyword">for</span> (<span class="hljs-keyword">const</span> result of results) {
    <span class="hljs-keyword">if</span> (result.error) {
        <span class="hljs-keyword">throw</span> `Upload failed: ${result.completed}`;
    } <span class="hljs-keyword">else</span> {
        console.log(result.completed);
    }
}</code></pre>
<p data-line="107" dir="auto">また、<code>uploadResources</code> メソッドに追加のオプションを渡して動作を調整したり、コールバック関数を渡してアップロードの進捗を監視したり、アップロードに時間がかかりすぎる場合にアクセストークンを更新するコールバック関数を渡したりすることも可能です。詳細については、Github の SDK リポジトリを参照してください。</p>
<ul data-line="110" dir="auto">
<li data-line="110">.NET: <a data-href="https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/src/Autodesk.Forge/Api/ObjectsApi.cs#L4818-L4840" href="https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/src/Autodesk.Forge/Api/ObjectsApi.cs#L4818-L4840" rel="noopener" target="_blank" title="https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/src/Autodesk.Forge/Api/ObjectsApi.cs#L4818-L4840">https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/src/Autodesk.Forge/Api/ObjectsApi.cs#L4818-L4840</a></li>
<li data-line="111">Node.js: <a data-href="https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/src/api/ObjectsApi.js#L1325-L1352" href="https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/src/api/ObjectsApi.js#L1325-L1352" rel="noopener" target="_blank" title="https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/src/api/ObjectsApi.js#L1325-L1352">https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/src/api/ObjectsApi.js#L1325-L1352</a></li>
</ul>
<p data-line="113" dir="auto">また、新しいSDKのメソッドをすでに使用している <a data-href="https://forge-tutorials.autodesk.io/tutorials/simple-viewer" href="https://forge-tutorials.autodesk.io/tutorials/simple-viewer" rel="noopener" target="_blank" title="https://forge-tutorials.autodesk.io/tutorials/simple-viewer">Simple Viewer</a> チュートリアルを確認することもできます。</p>
<h2 data-line="115" dir="auto" id="downloading-from-oss">OSS からのダウンロード</h2>
<p data-line="117" dir="auto">OSS からオブジェクトのダウンロードでは、既存のアプリケーションは通常、SDK（<a data-href="https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/docs/ObjectsApi.md#getobject" href="https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/docs/ObjectsApi.md#getobject" rel="noopener" target="_blank" title="https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/docs/ObjectsApi.md#getobject">.NET</a>、<a data-href="https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/docs/ObjectsApi.md#getObject" href="https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/docs/ObjectsApi.md#getObject" rel="noopener" target="_blank" title="https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/docs/ObjectsApi.md#getObject">Node.js</a>）から <code>ObjectsApi#getObject</code> メソッドを使用しているはずです。アップデートされた SDK では、<code>ObjectsApi#uploadResources</code> と同様のシグネチャを持つ <code>ObjectsApi#downloadResources</code>&#0160; メソッドを使用することができるようになりました。</p>
<p data-line="120" dir="auto">.NET:</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-typescript code-overflow-x hljs cs" id="snippet-6"><span class="hljs-keyword">var</span> results = <span class="hljs-keyword">await</span> ObjectsAPI.downloadResources(
    bucketKey,
    <span class="hljs-keyword">new</span> List&lt;DownloadItemDesc&gt;() {
        <span class="hljs-keyword">new</span> DownloadItemDesc(objectKey1, <span class="hljs-string">&quot;arraybuffer&quot;</span>),
        <span class="hljs-keyword">new</span> DownloadItemDesc(objectKey2, <span class="hljs-string">&quot;arraybuffer&quot;</span>),
        <span class="hljs-keyword">new</span> DownloadItemDesc(objectKey3, <span class="hljs-string">&quot;arraybuffer&quot;</span>)
	},
	<span class="hljs-keyword">new</span> Dictionary&lt;<span class="hljs-keyword">string</span>, <span class="hljs-keyword">object</span>&gt;() {
        <span class="hljs-comment">// { &quot;minutesExpiration&quot;, 5 },</span>
        <span class="hljs-comment">// { &quot;useCdn&quot;, true }</span>
    },
    <span class="hljs-comment">// onDownloadProgress,</span>
    <span class="hljs-comment">// onRefreshToken</span>
);</code></pre>
<div class="clipboard-container" data-clipboard-target="#snippet-6" data-on-clipboard="tooltip" data-placement="left" data-title="Copied!" data-trigger="manual" data-trigger-clipboard="data-trigger-clipboard">&#0160;</div>
<p data-line="139" dir="auto">Node.js:</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-7"><span class="hljs-keyword">const</span> results = await objectsApi.downloadResources(
    bucketKey,
    [
        { objectKey: objectKey1, responseType: <span class="hljs-string">&#39;arraybuffer&#39;</span> },
        { objectKey: objectKey2, responseType: <span class="hljs-string">&#39;arraybuffer&#39;</span> },
        { objectKey: objectKey3, responseType: <span class="hljs-string">&#39;arraybuffer&#39;</span> }
    ],
    {
        <span class="hljs-comment">// minutesExpiration: 5,</span>
        <span class="hljs-comment">// useCdn: true,</span>
        <span class="hljs-comment">// onDownloadProgress: (data) =&gt; { ... },</span>
        <span class="hljs-comment">// onRefreshToken: async () =&gt; { ... },</span>
    },
	oAuthTwoLeggedClient,
    oAuthTwoLeggedClient.getCredentials()
);</code></pre>
<p data-line="160" dir="auto">各オプションの詳細については、GitHub の SDK リポジトリを参照してください。</p>
<ul data-line="162" dir="auto">
<li data-line="162">.NET: <a data-href="https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/src/Autodesk.Forge/Api/ObjectsApi.cs#L4634-L4648" href="https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/src/Autodesk.Forge/Api/ObjectsApi.cs#L4634-L4648" rel="noopener" target="_blank" title="https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/src/Autodesk.Forge/Api/ObjectsApi.cs#L4634-L4648">https://github.com/Autodesk-Forge/forge-api-dotnet-client/blob/v1.9.7/src/Autodesk.Forge/Api/ObjectsApi.cs#L4634-L4648</a></li>
<li data-line="163">Node.js: <a data-href="https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/src/api/ObjectsApi.js#L1128-L1147" href="https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/src/api/ObjectsApi.js#L1128-L1147" rel="noopener" target="_blank" title="https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/src/api/ObjectsApi.js#L1128-L1147">https://github.com/Autodesk-Forge/forge-api-nodejs-client/blob/v0.9.4/src/api/ObjectsApi.js#L1128-L1147</a></li>
</ul>
<p><em>※ 本記事は <a href="https://forge.autodesk.com/ja/node/2509" rel="noopener" target="_blank">Direct-S3 upload and download with SDKs | Autodesk Forge</a>&#0160;</em><em>の内容をもとに翻訳・加筆修正したものです。</em></p>
<p>By Toshiaki Isezaki</p>
