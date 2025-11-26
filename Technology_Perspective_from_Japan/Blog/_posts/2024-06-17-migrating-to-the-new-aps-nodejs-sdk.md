---
layout: "post"
title: "新しい APS Node.js SDK への移行"
date: "2024-06-17 00:05:24"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/06/migrating-to-the-new-aps-nodejs-sdk.html "
typepad_basename: "migrating-to-the-new-aps-nodejs-sdk"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b32c13200b-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b32c13200b image-full img-responsive" src="/assets/image_197965.jpg" title="Aps" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p>2023 年末に、新しい <a href="https://adndevblog.typepad.com/technology_perspective/2023/11/call-for-feedback-nodejs-sdk-beta.html" rel="noopener" target="_blank">APS Node.js SDK</a> をアナウンスしています。このプロジェクトはまだベータ扱いですが、正式な公開時にインターフェースに劇的な変更が加えられる可能性は低いため、Node.js アプリでこの新しい公式 SDK への移行を検討する良い機会です。</p>
<h2 data-line="4" dir="auto" id="packaging--installation">パッケージとインストール</h2>
<p data-line="6" dir="auto">SDK は、API 毎に個別の NPM パッケージに分割されて提供されています。</p>
<p data-line="6" dir="auto">例えば、</p>
<ul data-line="8" dir="auto">
<li data-line="8"><a data-href="https://www.npmjs.com/package/@aps_sdk/authentication" href="https://www.npmjs.com/package/@aps_sdk/authentication" rel="noopener" target="_blank" title="https://www.npmjs.com/package/@aps_sdk/authentication">@aps_sdk/authentication</a></li>
<li data-line="9"><a data-href="https://www.npmjs.com/package/@aps_sdk/oss" href="https://www.npmjs.com/package/@aps_sdk/oss" rel="noopener" target="_blank" title="https://www.npmjs.com/package/@aps_sdk/oss">@aps_sdk/oss</a></li>
<li data-line="10"><a data-href="https://www.npmjs.com/package/@aps_sdk/data-management" href="https://www.npmjs.com/package/@aps_sdk/data-management" rel="noopener" target="_blank" title="https://www.npmjs.com/package/@aps_sdk/data-management">@aps_sdk/data-management</a></li>
<li data-line="11"><a data-href="https://www.npmjs.com/package/@aps_sdk/model-derivative" href="https://www.npmjs.com/package/@aps_sdk/model-derivative" rel="noopener" target="_blank" title="https://www.npmjs.com/package/@aps_sdk/model-derivative">@aps_sdk/model-derivative</a></li>
<li data-line="12">...</li>
</ul>
<p data-line="14" dir="auto">などです。</p>
<p data-line="14" dir="auto">そこで、”管理” モジュール<a data-href="https://www.npmjs.com/package/@aps_sdk/autodesk-sdkmanager" href="https://www.npmjs.com/package/@aps_sdk/autodesk-sdkmanager" rel="noopener" target="_blank" title="https://www.npmjs.com/package/@aps_sdk/autodesk-sdkmanager"> @aps_sdk/autodesk-sdkmanager</a>（SDK マネージャー）を同時に用意して、個々の API モジュールすべてに共有機能と設定（認証、レジリエンシー、ログ機能など）を提供しています。</p>
<ul>
<li data-line="14">本 SDK で利用可能なモジュールは、<a data-href="https://www.npmjs.com/~aps.sdk" href="https://www.npmjs.com/~aps.sdk" rel="noopener" target="_blank" title="https://www.npmjs.com/~aps.sdk">https://www.npmjs.com/~aps.sdk</a>&#0160;の [Dependents] タブで参照することが出来ます。</li>
</ul>
<p data-line="18" dir="auto">これらの依存関係をプロジェクトに追加するには、<a data-href="https://www.cookielab.io/blog/package-managers-comparison-yarn-npm-pnpm" href="https://www.cookielab.io/blog/package-managers-comparison-yarn-npm-pnpm" rel="noopener" target="_blank" title="https://www.cookielab.io/blog/package-managers-comparison-yarn-npm-pnpm">Node.js package manager</a> などを使用して、SDK マネージャーと必要な API モジュールをインストールするだけです。</p>
<pre><code class="language-bash hljs " data-line="20" dir="auto">npm install --save @aps_sdk/autodesk-sdkmanager @aps_sdk/authentication @aps_sdk/oss @aps_sdk/model-derivative
</code></pre>
<h2 data-line="24" dir="auto" id="initialization">初期化</h2>
<p data-line="26" dir="auto">各 API モジュールは、対応する APS サービスへのエントリポイントとなる <code>*Client</code> クラス（<code>AuthenticationClient</code> や <code>OssClient</code> など）を公開しています。これらのクラスのコンストラクタは、唯一のパラメータとして SDK マネージャーのインスタンスを要求とします。以下は、コード内でこれらのクライアントを初期化する例です。</p>
<pre><code class="language-javascript hljs " data-line="28" dir="auto"><span class="hljs-keyword">const</span> { SdkManagerBuilder } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&quot;@aps_sdk/autodesk-sdkmanager&quot;</span>);
<span class="hljs-keyword">const</span> { AuthenticationClient } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&quot;@aps_sdk/authentication&quot;</span>);
<span class="hljs-keyword">const</span> { OssClient } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&quot;@aps_sdk/oss&quot;</span>);
<span class="hljs-keyword">const</span> { ModelDerivativeClient } = <span class="hljs-built_in">require</span>(<span class="hljs-string">&quot;@aps_sdk/model-derivative&quot;</span>);

<span class="hljs-keyword">const</span> sdkManager = SdkManagerBuilder.create().build();
<span class="hljs-keyword">const</span> authenticationClient = <span class="hljs-keyword">new</span> AuthenticationClient(sdkManager);
<span class="hljs-keyword">const</span> ossClient = <span class="hljs-keyword">new</span> OssClient(sdkManager);
<span class="hljs-keyword">const</span> modelDerivativeClient = <span class="hljs-keyword">new</span> ModelDerivativeClient(sdkManager);
</code></pre>
<p data-line="40" dir="auto">SDK は <a href="https://nodejs.org/api/esm.html" rel="noopener" target="_blank">ES6 モジュール</a>もサポートしています。</p>
<pre><code class="language-javascript hljs " data-line="42" dir="auto">import { SdkManagerBuilder } from <span class="hljs-string">&quot;@aps_sdk/autodesk-sdkmanager&quot;</span>;
import { AuthenticationClient } from <span class="hljs-string">&quot;@aps_sdk/authentication&quot;</span>;
import { OssClient } from <span class="hljs-string">&quot;@aps_sdk/oss&quot;</span>;
import { ModelDerivativeClient } from <span class="hljs-string">&quot;@aps_sdk/model-derivative&quot;</span>;

<span class="hljs-keyword">const</span> sdkManager = SdkManagerBuilder.create().build();
<span class="hljs-keyword">const</span> authenticationClient = <span class="hljs-keyword">new</span> AuthenticationClient(sdkManager);
<span class="hljs-keyword">const</span> ossClient = <span class="hljs-keyword">new</span> OssClient(sdkManager);
<span class="hljs-keyword">const</span> modelDerivativeClient = <span class="hljs-keyword">new</span> ModelDerivativeClient(sdkManager);
</code></pre>
<h2 data-line="54" dir="auto" id="authentication--user-info">認証とユーザー情報</h2>
<p data-line="56" dir="auto"><a data-href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" rel="noopener" target="_blank" title="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/">2-legged でアクセス トークン</a> を生成するには、次のようにコーディングすることが出来ます。</p>
<pre><code class="language-javascript hljs " data-line="58" dir="auto"><span class="hljs-comment">// ...</span>

import { AuthenticationClient, Scopes } from <span class="hljs-string">&quot;@aps_sdk/authentication&quot;</span>;

<span class="hljs-comment">// ...</span>

<span class="hljs-keyword">const</span> twoLeggedCredentials = await authenticationClient.getTwoLeggedToken(APS_CLIENT_ID, APS_CLIENT_SECRET, [Scopes.DataRead, Scopes.ViewablesRead]); 
console.log(twoLeggedCredentials.access_token);
</code></pre>
<p data-line="69" dir="auto">同じく、<a data-href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" rel="noopener" target="_blank" title="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/">3-legged トークン</a>&#0160;を生成、または、リフレッシュトークンを得るには、旧 Forge Node.js SDK と似た次のようなコーディングを使用することが出来ます。</p>
<pre><code class="language-javascript hljs " data-line="71" dir="auto"><span class="hljs-comment">// ...</span>

import { AuthenticationClient, ResponseType, Scopes } from <span class="hljs-string">&quot;@aps_sdk/authentication&quot;</span>;

<span class="hljs-comment">// Generating the authorization URL</span>
<span class="hljs-keyword">const</span> url = authenticationClient.authorize(APS_CLIENT_ID, ResponseType.Code, APS_CALLBACK_URL, [Scopes.DataRead, Scopes.ViewablesRead]);
console.log(url);

<span class="hljs-comment">// Exchanging temporary code for an access token</span>
<span class="hljs-keyword">const</span> threeLeggedCredentials = await authenticationClient.getThreeLeggedToken(APS_CLIENT_ID, temporaryCode, APS_CALLBACK_URL, {
    clientSecret: APS_CLIENT_SECRET
});
console.log(threeLeggedCredentials.access_token);

<span class="hljs-comment">// Refreshing an access token</span>
<span class="hljs-keyword">const</span> refreshedCredentials = await authenticationClient.getRefreshToken(APS_CLIENT_ID, threeLeggedCredentials.refresh_token, {
    clientSecret: APS_CLIENT_SECRET,
    scopes: [Scopes.ViewablesRead]
});
console.log(refreshedCredentials.access_token);
</code></pre>
<p data-line="94" dir="auto">最後に、取得済の 3-legged トークンからユーザー情報を抽出する方法を紹介しましょう。</p>
<pre><code class="language-javascript hljs " data-line="96" dir="auto"><span class="hljs-keyword">const</span> userInfo = await authenticationClient.getUserInfo(threeLeggedCredentials.access_token);
console.log(userInfo.name);
</code></pre>
<ul>
<li data-line="101">注：Authentication API v2 では、<code>firstName</code> フィールドと <code>lastName</code>フィールドは <code>name</code> という単一のフィールドに置き換えられています。</li>
</ul>
<h2 data-line="103" dir="auto" id="error-checking">エラーチェック</h2>
<p data-line="105" dir="auto">SDK はすべての HTTP リクエストで <a data-href="https://www.npmjs.com/package/axios" href="https://www.npmjs.com/package/axios" rel="noopener" target="_blank" title="https://www.npmjs.com/package/axios">axios</a> を使用します。SDK が例外をスローするたびに、エラーオブジェクトの <code>err.axiosError</code> に axios 固有の情報が含まれます。これは、例えば、API 呼び出しから非 2xx レスポンスを期待する場合など、特定のシナリオで便利です。</p>
<pre><code class="language-javascript hljs " data-line="107" dir="auto"><span class="hljs-keyword">try</span> {
    await ossClient.getBucketDetails(accessToken, bucketKey);
    console.log(<span class="hljs-string">&quot;Bucket exists&quot;</span>);
} <span class="hljs-keyword">catch</span> (err) {
    <span class="hljs-keyword">if</span> (err.axiosError.response.status === <span class="hljs-number">404</span>) {
        console.log(<span class="hljs-string">&quot;Bucket does not exist&quot;</span>);
    } <span class="hljs-keyword">else</span> {
        <span class="hljs-keyword">throw</span> err;  
    }
}
</code></pre>
<h2 data-line="120" dir="auto" id="tip-convenience-methods">便利なメソッド</h2>
<p data-line="122" dir="auto">場合によっては、API クライアントが 2 つ以上の API リクエストを 1 つのメソッド呼び出しにまとめることがあります。例えば、OSS Bucket にファイルをアップロードする場合、アップロード URL を作成し、コンテンツを手動でアップロードし、最後にアップロード完了のリクエストを送信する代わりに、<code>await ossClient.upload(APS_BUCKET, objectName, localFilePath, accessToken)</code>&#0160;を呼び出すような場面です。現在、SDK プロジェクトのドキュメントと変更履歴を作成しているので、そこでこれらのオプションを紹介する予定です。</p>
<h2 data-line="122" dir="auto">その他のリソース</h2>
<p data-line="122" dir="auto">本 SDK の公式ドキュメントの準備が整う前に、<a href="https://github.com/autodesk-platform-services/aps-sdk-node/tree/main/samples" rel="noopener" target="_blank">サンプル</a>（プロジェクトの GitHub リポジトリの一部：<a href="https://github.com/autodesk-platform-services/aps-sdk-node" rel="noopener" target="_blank">https://github.com/autodesk-platform-services/aps-sdk-node</a>）や APS チュートリアル（<a href="https://tutorials.autodesk.io" rel="noopener" target="_blank">https://tutorials.autodesk.io</a>）をチェックすることで、使用方法の詳細を把握することが出来ます。</p>
</div>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/migrating-new-aps-nodejs-sdk-0" rel="noopener" target="_blank">Migrating to the new APS Node.js SDK | Autodesk Platform Services</a> から転写・意訳したものです。</p>
<p>By Toshiaki Isezaki</p>
