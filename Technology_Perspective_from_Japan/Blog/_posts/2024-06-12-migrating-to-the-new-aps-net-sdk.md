---
layout: "post"
title: "新しい APS .NET SDK への移行"
date: "2024-06-12 00:14:52"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/06/migrating-to-the-new-aps-net-sdk.html "
typepad_basename: "migrating-to-the-new-aps-net-sdk"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b32c0f200b-pi" style="display: inline;"><img alt="Aps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b32c0f200b image-full img-responsive" src="/assets/image_255217.jpg" title="Aps" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p data-line="0" dir="auto">Autodesk Platform Services の新しい<a href="https://github.com/autodesk-platform-services/aps-sdk-net" rel="noopener" target="_blank"> .NET SDK</a> は、すでに数ヶ月前からベータ版がリリースされています。この公式 SDK に .NET アプリを移行する方法をご紹介しましょう。</p>
<h2 data-line="4" dir="auto" id="packaging--installation">パッケージとインストール</h2>
<p data-line="6" dir="auto">SDK は、API 毎に個別の NuGet パッケージに分割されて提供されています。</p>
<p data-line="6" dir="auto">例えば、</p>
<ul data-line="8" dir="auto">
<li data-line="8"><a data-href="https://www.nuget.org/packages/Autodesk.Authentication" href="https://www.nuget.org/packages/Autodesk.Authentication" rel="noopener" target="_blank" title="https://www.nuget.org/packages/Autodesk.Authentication">Autodesk.Authentication</a></li>
<li data-line="9"><a data-href="https://www.nuget.org/packages/Autodesk.Oss" href="https://www.nuget.org/packages/Autodesk.Oss" rel="noopener" target="_blank" title="https://www.nuget.org/packages/Autodesk.Oss">Autodesk.Oss</a></li>
<li data-line="10"><a data-href="https://www.nuget.org/packages/Autodesk.DataManagement" href="https://www.nuget.org/packages/Autodesk.DataManagement" rel="noopener" target="_blank" title="https://www.nuget.org/packages/Autodesk.DataManagement">Autodesk.DataManagement</a></li>
<li data-line="11"><a data-href="https://www.nuget.org/packages/Autodesk.ModelDerivative" href="https://www.nuget.org/packages/Autodesk.ModelDerivative" rel="noopener" target="_blank" title="https://www.nuget.org/packages/Autodesk.ModelDerivative">Autodesk.ModelDerivative</a></li>
<li data-line="12">...</li>
</ul>
<p data-line="14" dir="auto">などです。</p>
<p data-line="14" dir="auto">そこで、”管理” モジュール <a data-href="https://www.nuget.org/packages/Autodesk.SDKManager" href="https://www.nuget.org/packages/Autodesk.SDKManager" rel="noopener" target="_blank" title="https://www.nuget.org/packages/Autodesk.SDKManager">Autodesk.SDKManager</a>（SDK マネージャー）を同時に用意して、個々の API モジュールすべてに共有機能と設定（認証、レジリエンシー、ログ機能など）を提供しています。</p>
<ul>
<li data-line="16">本 SDK で利用可能なモジュールは、 <a data-href="https://www.nuget.org/profiles/AutodeskPlatformServices.SDK" href="https://www.nuget.org/profiles/AutodeskPlatformServices.SDK" rel="noopener" target="_blank" title="https://www.nuget.org/profiles/AutodeskPlatformServices.SDK">https://www.nuget.org/profiles/AutodeskPlatformServices.SDK</a> 参照することが出来ます。</li>
</ul>
<p data-line="18" dir="auto">これらの依存関係をプロジェクトに追加する場合は、Visual Studio の NuGetマネージャーUI を使用するか、コマンドラインを使用して、SDK マネージャーと必要な API をインストールするだけです。</p>
<pre><code class=" hljs java" data-line="20" dir="auto">dotnet add <span class="hljs-keyword">package</span> Autodesk.SdkManager
dotnet add <span class="hljs-keyword">package</span> Autodesk.Authentication
dotnet add <span class="hljs-keyword">package</span> Autodesk.OSS
dotnet add <span class="hljs-keyword">package</span> Autodesk.ModelDerivative
</code></pre>
<h2 data-line="27" dir="auto" id="initialization">初期化</h2>
<p data-line="29" dir="auto">各 API モジュールは、対応する APS サービスへのエントリポイントとなる&#0160;<code>*Client</code>&#0160;クラス（<code>AuthenticationClient</code>&#0160;や&#0160;<code>OssClient</code>&#0160;など）を公開しています。これらのクラスのコンストラクタは、唯一のパラメータとして SDK マネージャーのインスタンスを要求とします。以下は、コード内でこれらのクライアントを初期化する例です。</p>
<pre><code class=" hljs cs" data-line="31" dir="auto"><span class="hljs-keyword">using</span> Autodesk.SDKManager;
<span class="hljs-keyword">using</span> Autodesk.Authentication;
<span class="hljs-keyword">using</span> Autodesk.Oss;
<span class="hljs-keyword">using</span> Autodesk.ModelDerivative;

<span class="hljs-comment">// ...</span>

<span class="hljs-keyword">var</span> sdkManager = SdkManagerBuilder.Create().Build();
<span class="hljs-keyword">var</span> authenticationClient = <span class="hljs-keyword">new</span> AuthenticationClient(sdkManager);
<span class="hljs-keyword">var</span> ossClient = <span class="hljs-keyword">new</span> OssClient(sdkManager);
<span class="hljs-keyword">var</span> modelDerivativeClient = <span class="hljs-keyword">new</span> ModelDerivativeClient(sdkManager);
</code></pre>
<h2 data-line="54" dir="auto" id="authentication--user-info">認証とユーザー情報</h2>
<p data-line="47" dir="auto"><a data-href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" rel="noopener" target="_blank" title="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/">2-legged でアクセス トークン</a>&#0160;を生成するには、次のようにコーディングすることが出来ます。</p>
<pre><code class=" hljs cs" data-line="49" dir="auto"><span class="hljs-keyword">using</span> Autodesk.Authentication;
<span class="hljs-keyword">using</span> Autodesk.Authentication.Model;

<span class="hljs-comment">// ...</span>

<span class="hljs-keyword">var</span> scopes = <span class="hljs-keyword">new</span> List&lt;Scopes&gt; { Scopes.DataRead, Scopes.ViewablesRead };
<span class="hljs-keyword">var</span> auth = <span class="hljs-keyword">await</span> authenticationClient.GetTwoLeggedTokenAsync(APS_CLIENT_ID, APS_CLIENT_SECRET, scopes);
Console.WriteLine(auth.AccessToken);
</code></pre>
<p data-line="60" dir="auto">同じく、<a data-href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" rel="noopener" target="_blank" title="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/">3-legged トークン</a>&#0160;を生成、または、リフレッシュトークンを得るには、旧 Forge Node.js SDK と似た次のようなコーディングを使用することが出来ます。</p>
<pre><code class=" hljs cs" data-line="62" dir="auto"><span class="hljs-keyword">using</span> Autodesk.Authentication;
<span class="hljs-keyword">using</span> Autodesk.Authentication.Model;

<span class="hljs-comment">// ...</span>

<span class="hljs-comment">// Generating the authorization URL</span>
<span class="hljs-keyword">var</span> url = authenticationClient.Authorize(APS_CLIENT_ID, ResponseType.Code, APS_CALLBACK_URL, scopes);
Console.WriteLine(url);

<span class="hljs-comment">// Exchanging temporary code for an access token</span>
<span class="hljs-keyword">var</span> threeLeggedCredentials = <span class="hljs-keyword">await</span> authenticationClient.GetThreeLeggedTokenAsync(APS_CLIENT_ID, APS_CLIENT_SECRET, temporaryCode, APS_CALLBACK_URL);
Console.WriteLine(threeLeggedCredentials.AccessToken);

<span class="hljs-comment">// Refreshing an access token</span>
<span class="hljs-keyword">var</span> refreshedCredentials = <span class="hljs-keyword">await</span> authenticationClient.GetRefreshTokenAsync(APS_CLIENT_ID, APS_CLIENT_SECRET, threeLeggedCredentials.RefreshToken, scopes);
Console.WriteLine(refreshedCredentials.AccessToken);
</code></pre>
<p data-line="81" dir="auto">最後に、取得済の 3-legged トークンからユーザー情報を抽出する方法を紹介しましょう。</p>
<pre><code class=" hljs cs" data-line="83" dir="auto"><span class="hljs-keyword">var</span> userInfo = <span class="hljs-keyword">await</span> authenticationClient.GetUserInfoAsync(threeLeggedCredentials.AccessToken);
Console.WriteLine(userInfo.Name);
</code></pre>
<ul>
<li data-line="101">注：Authentication API v2 では、<code>firstName</code>&#0160;フィールドと&#0160;<code>lastName</code>フィールドは&#0160;<code>name</code>&#0160;という単一のフィールドに置き換えられています。</li>
</ul>
<h2 data-line="120" dir="auto" id="tip-convenience-methods">便利なメソッド</h2>
<p data-line="92" dir="auto">場合によっては、API クライアントが 2 つ以上の API リクエストを 1 つのメソッド呼び出しにまとめることがあります。例えば、OSS Bucket にファイルをアップロードする場合、アップロード URL を作成し、コンテンツを手動でアップロードし、最後にアップロード完了のリクエストを送信する代わりに</p>
<pre><code class=" hljs bash" data-line="94" dir="auto">await ossClient.Upload(bucketKey, objectName, <span class="hljs-built_in">source</span>ToUpload, accessToken, CancellationToken.None);
</code></pre>
<p data-line="98" dir="auto">を呼び出すような場面です。現在、SDK プロジェクトのドキュメントと変更履歴を作成しているので、そこでこれらのオプションを紹介する予定です。</p>
<h2 data-line="122" dir="auto">その他のリソース</h2>
<p data-line="102" dir="auto">本 SDK の公式ドキュメントの準備が整う前に、<a href="https://github.com/autodesk-platform-services/aps-sdk-node/tree/main/samples" rel="noopener" target="_blank">サンプル</a>（プロジェクトの GitHub リポジトリの一部：<a data-href="https://github.com/autodesk-platform-services/aps-sdk-net" href="https://github.com/autodesk-platform-services/aps-sdk-net" rel="noopener" target="_blank" title="https://github.com/autodesk-platform-services/aps-sdk-net">https://github.com/autodesk-platform-services/aps-sdk-net</a>）や APS チュートリアル（<a href="https://tutorials.autodesk.io/" rel="noopener" target="_blank">https://tutorials.autodesk.io</a>）をチェックすることで、使用方法の詳細を把握することが出来ます。</p>
</div>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/migrating-new-aps-net-sdk">Migrating to the new APS .NET SDK | Autodesk Platform Services</a>&#0160;から転写・意訳したものです。</p>
<p>By Toshiaki Isezaki</p>
