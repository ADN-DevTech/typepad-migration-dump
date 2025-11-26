---
layout: "post"
title: "OAuth2 v1 から v2 への移行ガイド"
date: "2023-05-15 00:34:48"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/05/migration-guide-oauth2-v1-to-v2.html "
typepad_basename: "migration-guide-oauth2-v1-to-v2"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75180880c200b-pi" style="display: inline;"></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a4ee04200c-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9 1920x1080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a4ee04200c image-full img-responsive" src="/assets/image_961149.jpg" title="Placeholder - Blog images_Lifestyle 16x9 1920x1080" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75180880c200b-pi" style="display: inline;"><br /></a></p>
<p>Authentication（OAuth）API v2 は、<a href="https://ja.wikipedia.org/wiki/OpenID" rel="noopener" target="_blank">OpenID</a> 仕様と PKCE ワークフロー（デスクトップおよび Single-Page Web アプリ用）との整合性を提供し、パフォーマンスと最新のテクノロジー スタックを提供するようになりました。この記事では、アプリ コードを移行する方法を示します。完全なドキュメントは、<a href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/overview/" rel="noopener" target="_blank">こちら</a>から利用可能です。その他のご質問は、aps.help@autodesk.com までご連絡ください。v1 はまもなく非推奨となります。</p>
<p><span style="text-decoration: underline;"><strong>2-legged トークンの OAuth 2 v1 エンドポイントから v2 エンドポイントへの移行</strong></span></p>
<p>OAuth 2 v1 から v2 に移行するには、リクエストで渡されるクライアント認証情報の方法を簡単に変更する必要があります。v1 APIは 、リクエストボディで Client Id&#0160; と Client Secret を指定します。一方、v2 では Authorization ヘッダーに Basic Auth Type で Client Id と Client Secret を指定します。</p>
<p>※ 以下、APS_HOST は https://developer.api.autodesk.com と等価</p>
<ol>
<li>エンドポイントのベース URL の変更</li>
</ol>
<p style="padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p>APS_HOST/authentication/v1/authenticate</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：&#0160;</p>
<blockquote>
<p>APS_HOST/authentication/v2/token</p>
</blockquote>
<ol start="2">
<li>v1 のパラメータは、リクエスト ボディにクライアントの認証情報を使用するのに対して、v2 はヘッダーに Authorization: basic でクライアントの認証情報を要求します。認証は、Basic ${Base64&lt;client_id&gt;:&lt;client_secret&gt;)} の形式でなければなりません。</li>
<li>リクエスト ヘッダー（パラメータ）の変更</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">Content-Type: application/x-www-form-urlencoded</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">Content-Type: application/x-www-form-urlencoded<br />Authorization: Basic RG4ydUlwOGp1S0hzRmV1WHV0bmtmZ0FQWHFkdWx5WHA6b01YZWEyMEZVY3Q0REJqYw=</p>
</blockquote>
<ol start="4">
<li>リクエスト ボディ（パラメータ）の変更</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">client_id=obQDn8P0GanGFQha4ngKKVWcxwyvFAGE <br />client_secret=xyz <br />grant_type=client_credentials <br />scope=data:read</p>
</blockquote>
<p style="font-weight: 400; padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">grant_type=client_credentials <br />scope=data:read</p>
</blockquote>
<ol start="5">
<li>&#0160;レスポンスに変更はありません</li>
</ol>
<ol start="6">
<li>&#0160; Curl コマンド例</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p>curl -v &#39;APS_HOST/authentication/v1/authenticate&#39; -X &#39;POST&#39; -H &#39;Content-Type: application/x-www-form-urlencoded&#39; -d &#39; client_id=obQDn8P0GanGFQha4ngKKVWcxwyvFAGE&amp;client_secret=eUruM8HRyc7BAQ1e&amp; grant_type=client_credentials&amp; scope=data:read &#39;</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p>curl --location -g --request POST &#39;APS_HOST/authentication/v2/token&#39; \ --header &#39;Authorization: Basic RG4ydUlwOGp1S0hzRmV1WHV0bmtmZ0FQWHFkdWx5WHA6b01YZWEyMEZVY3Q0REJqYw==&#39; \ --header &#39;Content-Type: application/x-www-form-urlencoded&#39; \ --data-urlencode &#39;grant_type=client_credentials&#39; \ --data-urlencode &#39;scope=data:read&#39;</p>
</blockquote>
<ol start="7">
<li>&#0160;API ドキュメント リンク&#0160; &#0160; &#0160;</li>
</ol>
<p style="padding-left: 40px;">v1：<a href="https://aps.autodesk.com/en/docs/oauth/v1/tutorials/get-2-legged-token/" rel="noopener" target="_blank">https://aps.autodesk.com/en/docs/oauth/v1/tutorials/get-2-legged-token/</a></p>
<p style="padding-left: 40px;">v2：<a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" rel="noopener" target="_blank">https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/</a></p>
<p><span style="text-decoration: underline;"><strong>3-legged トークンの OAuth 2 v1 エンドポイントから v2 エンドポイントへの移行</strong></span></p>
<p>authorize エンドポイントの変更点は、バージョン番号が v1 から v2 になったのみです。</p>
<ol start="1">
<li>エンドポイントのベース URL の変更</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">APS_HOST/authentication/v1/authorize</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">APS_HOST/authentication/v2/authorize</p>
</blockquote>
<ol start="2">
<li>Curl コマンド例</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">curl --location -g --request ‘APS_HOST/authentication/v1/authorize? response_type=code &amp;client_id=obQDn8P0GanGFQha4ngKKVWcxwyvFAGE &amp;redirect_uri=http%3A%2F%2Fsampleapp.com%2Foauth%2Fcallback%3Ffoo%3Dbar &amp;scope=data:<strong>read</strong></p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">curl <em>--location -g --request GET &#0160;&#39;APS_HOST/authentication/v2/authorize?</em> response_type=code &amp;client_id=obQDn8P0GanGFQha4ngKKVWcxwyvFAGE &amp;redirect_uri=http%3A%2F%2Fsampleapp.com%2Foauth%2Fcallback%3Ffoo%3Dbar &amp;scope=data:read</p>
</blockquote>
<ol start="3">
<li>&#0160;API ドキュメント リンク&#0160; &#0160;</li>
</ol>
<p style="padding-left: 40px;">v1：<a href="https://aps.autodesk.com/en/docs/oauth/v1/tutorials/get-3-legged-token/" rel="noopener" target="_blank">https://aps.autodesk.com/en/docs/oauth/v1/tutorials/get-3-legged-token/</a></p>
<p style="padding-left: 40px;">v2：<a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" rel="noopener" target="_blank">https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/</a></p>
<p><u><strong>オーソライゼーション コードからアクセス トークンへの変換</strong></u></p>
<ol start="1">
<li>エンドポイントのベース URL の変更</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">APS_HOST/authentication/v1/gettoken</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">APS_HOST/authentication/v2/token</p>
</blockquote>
<ol start="2">
<li>v1は、リクエスト ボディにクライアントの認証情報を使用するのに対し、v2 は、リクエスト ヘッダーに Authorization: basic でクライアントの認証情報を要求します。認証は、Basic ${Base64(&lt;client_id&gt;:&lt;client_secret&gt;)} の形式でなければなりません。また、v2 はヘッダーで application/json を受け入れます。</li>
<li>リクエスト ヘッダー（パラメータ）の変更</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">Content-Type: application/x-www-form-urlencoded</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">Content-Type: application/x-www-form-urlencoded <br />Authorization: Basic RG4ydUlwOGp1S0hzRmV1WHV0bmtmZ0FQWHFkdWx5WHA6b01YZWEyMEZVY3Q0REJqYw== <br />Accept: application/json</p>
</blockquote>
<ol start="4">
<li>リクエスト ボディ（パラメータ）の変更</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">grant_type=authorization_code <br />client_id=obQDn8P0GanGFQha4ngKKVWcxwyvFAGE <br />client_secret=eUruM8HRyc7BAQ1e redirect_uri=http%3A%2F%2Fsampleapp.com%2Foauth%2Fcallback%3Ffoo%3Dbar <br />code=wroM1vFA4E-Aj241-quh_LVjm7UldawnNgYEHQ8I</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">grant_type=authorization_code redirect_uri=http%3A%2F%2Fsampleapp.com%2Foauth%2Fcallback%3Ffoo%3Dbar <br />code=wroM1vFA4E-Aj241-quh_LVjm7UldawnNgYEHQ8I</p>
</blockquote>
<ol start="5">
<li>&#0160;レスポンスに変更はありません</li>
<li>Curl コマンド例</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">curl -v &#39;APS_HOST/authentication/v1/gettoken&#39; \ -X &#39;POST&#39; \ -H &#39;Content-Type: application/x-www-form-urlencoded&#39; \ -d &#39;client_id=obQDn8P0GanGFQha4ngKKVWcxwyvFAGE&#39; \ -d &#39;client_secret=eUruM8HRyc7BAQ1e&#39; \ -d &#39;grant_type=authorization_code&#39; \ -d &#39;code=wroM1vFA4E-Aj241-quh_LVjm7UldawnNgYEHQ8I&#39; \ -d &#39;redirect_uri=http://sampleapp.com/oauth/callback&#39;</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">curl -v &#39;APS_HOST/authentication/v2/token&#39; -X &#39;POST&#39; -H &#39;Content-Type: application/x-www-form-urlencoded&#39;&#39; -H &#39;accept: application/json&#39; \&#39; -d &#39;grant_type=authorization_code&#39; -d &#39;code=wroM1vFA4E-Aj241-quh_LVjm7UldawnNgYEHQ8I&#39; -d &#39;redirect_uri=http://sampleapp.com/oauth/callback&#39;</p>
</blockquote>
<ol start="7">
<li>API ドキュメント リンク&#0160;</li>
</ol>
<p style="padding-left: 40px;">v1：<a href="https://aps.autodesk.com/en/docs/oauth/v1/reference/http/gettoken-POST/" rel="noopener" target="_blank">https://aps.autodesk.com/en/docs/oauth/v1/reference/http/gettoken-POST/</a></p>
<p style="padding-left: 40px;">v2：<a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/#section-1-authorization-code-grant-type" rel="noopener" target="_blank">https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/#section-1-authorization-code-grant-type</a></p>
<p><u><strong>リフレッシュ トークン</strong></u></p>
<ol start="8">
<li>エンドポイントのベース URL の変更</li>
</ol>
<p style="padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">https://developer.api.autodesk.com/authentication/v1/refreshtoken</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">https://developer.api.autodesk.com/authentication/v2/token</p>
</blockquote>
<ol start="9">
<li>v1 は、リクエスト ボディにクライアントの認証情報を使用するのに対し、v2 は、リクエスト ヘッダーに Authorization: basic でクライアントの認証情報を要求します。認証は、Basic ${Base64(&lt;client_id&gt;:&lt;client_secret&gt;)} の形式でなければなりません。また、v2 はヘッダーで application/json を受け入れます。</li>
<li>リクエスト ヘッダー（パラメータ）の変更</li>
</ol>
<p style="padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">Content-Type: application/x-www-form-urlencoded</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">Content-Type: application/x-www-form-urlencoded Authorization: Basic RG4ydUlwOGp1S0hzRmV1WHV0bmtmZ0FQWHFkd Wx5WHA6b01YZWEyMEZVY3Q0REJqYw== Accept: application/json</p>
</blockquote>
<ol start="11">
<li>リクエストボディ（パラメータ）の変更</li>
</ol>
<p style="padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">grant_type=refresh_token client_id=obQDn8P0GanGFQha4ngKKVWcxwyvFAGE client_secret=eUruM8HRyc7BAQ1e refresh_token=Jnrqqp7b8GrfqIE53WocjEyt59RClDYhqbYeOCWeqM scope=data:read</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">grant_type=refresh_token refresh_token=Jnrqqp7b8GrfqIE53WocjEyt59RClDYhqbYeOCWeqM scope=data:read</p>
</blockquote>
<ol start="12">
<li>レスポンスに変更はありません</li>
</ol>
<ol start="13">
<li>Curl コマンド例</li>
</ol>
<p style="padding-left: 40px;">対応前（v1）：</p>
<blockquote>
<p style="font-weight: 400;">curl -v &#39;https://developer.api.autodesk.com/authentication/v1/refreshtoken&#39; -X &#39;POST&#39; -H &#39;Content-Type: application/x-www-form-urlencoded&#39; -d &#39; client_id=obQDn8P0GanGFQha4ngKKVWcxwyvFAGE&amp; client_secret=eUruM8HRyc7BAQ1e&amp; grant_type=refresh_token&amp; refresh_token=i0kBWTHzI0JVKWTOoFn6cvPk32SZcs5CUtwic3ndu</p>
</blockquote>
<p style="padding-left: 40px;">対応後（v2）：</p>
<blockquote>
<p style="font-weight: 400;">curl -v &#39;https://developer.api.autodesk.com/authentication/v2/token&#39; -X &#39;POST&#39; -H &#39;Content-Type: application/x-www-form-urlencoded&#39; -H &#39;Accept: application/json&#39; -H &#39;Authorization: Basic YWthc2h0ZXN0OmFrYXNodGVzdA==&#39; \ -d &#39;grant_type=refresh_token&#39; -d &#39;refresh_token=Jnrqqp7b8GrfqIE53WocjEyt59RClDYhqbYeOCWeqM&#39; -d &#39;scope=data:read&#39;</p>
</blockquote>
<ol start="14">
<li>API ドキュメント リンク&#0160;</li>
</ol>
<p style="padding-left: 40px;">v1：<a href="https://aps.autodesk.com/en/docs/oauth/v1/reference/http/refreshtoken-POST/" rel="noopener" target="_blank">https://aps.autodesk.com/en/docs/oauth/v1/reference/http/refreshtoken-POST/</a></p>
<p style="padding-left: 40px;">v2：<a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/" rel="noopener" target="_blank">https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/</a></p>
<p><u><strong>v2 でのリフレッシュトークン / アクセストークン無効化の導入</strong></u></p>
<ol start="1">
<li>エンドポイントのベース URL</li>
</ol>
<blockquote>
<p style="font-weight: 400;">https://developer.api.autodesk.com/authentication/v2/revoke</p>
</blockquote>
<ol start="2">
<li>リクエスト ヘッダー</li>
</ol>
<blockquote>
<p style="font-weight: 400;">Content-Type: application/x-www-form-urlencoded Authorization: Basic RG4ydUlwOGp1S0hzRmV1WHV0bmtmZ0FQWHFkd Wx5WHA6b01YZWEyMEZVY3Q0REJqYw==</p>
</blockquote>
<ol start="3">
<li>リクエスト ボディ</li>
</ol>
<blockquote>
<p style="font-weight: 400;">token_type_hint=refresh_token refresh_token=Jnrqqp7b8GrfqIE53WocjEyt59RClDYhqbYeOCWeq</p>
</blockquote>
<ol start="4">
<li>Curl コマンド例</li>
</ol>
<blockquote>
<p style="font-weight: 400;">curl -v &#39;https://developer.api.autodesk.com/authentication/v2/revoke&#39; -X &#39;POST&#39; -H &#39;Content-Type: application/x-www-form-urlencoded&#39; -d &#39;{ &#39;token=9uACOhcF7d94rYJDKmulcyssEeyZ4HVNTwqng6Qekt&#39; \ &#39;token_type_hint=refresh_token&#39; \ &#39;client_id=0oawv18w63i03CgmZ0h7&#39; }</p>
</blockquote>
<ol start="5">
<li>API ドキュメント リンク</li>
</ol>
<p style="font-weight: 400; padding-left: 40px;"><a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/revoke-POST/" rel="noopener" target="_blank">https://aps.autodesk.com/en/docs/oauth/v2/reference/http/revoke-POST/</a></p>
<p><span style="background-color: #ffff00;">※ Authentication（OAuth）API v1 は <span style="text-decoration: line-through;">2023 年 10 月 30 日</span> <strong>2024 年 4 月 30 日</strong>に非推奨設定となります。</span></p>
<p><span style="background-color: #ffff00;">※ 新規にアプリを開発する場合には、Authentication（OAuth）API v2 の使用が推奨されます。</span></p>
<p>※ 旧 Forge SDK では、<a href="https://github.com/Autodesk-Forge/forge-api-nodejs-client" rel="noopener" target="_blank">Forge Node.js SDK</a> が v2 対応しています。既存のプロジェクトで Forge Node.js SDK をお使いの場合には、最新のパッケージのアップデートを適用、デプロイをお願いします。<br />&#0160; &#0160; &#0160;<a href="https://github.com/Autodesk-Forge/forge-api-nodejs-client/commit/f1b3691981f1ab74856e07f7ac9fcce72ea0106e" rel="noopener" target="_blank">Feature (OUATH2 V2 Migration) (#98) · Autodesk-Forge/forge-api-nodejs-client@f1b3691 · GitHub</a></p>
<p>※ 旧 <a href="https://github.com/Autodesk-Forge/forge-api-dotnet-client" rel="noopener" target="_blank">Forge .NET SDK</a> は、5 月末に v2 対応しました。既存のプロジェクトで Forge .NET SDK をお使いの場合には、最新のパッケージのアップデートを適用、デプロイをお願いします。<br />&#0160; &#0160; <a href="https://github.com/Autodesk-Forge/forge-api-dotnet-client/commit/8fe4900024d15cf0115e8f6fd0ef172bd5bc31a0" rel="noopener" target="_blank">Feature (Migrate from OuathV1 to OuathV2) · Autodesk-Forge/forge-api-dotnet-client@8fe4900 · GitHub</a></p>
<p>※ その他の 旧 Forge SDK では対応予定はありません。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/migration-guide-oauth2-v1-v2" rel="noopener" target="_blank">Migration guide - OAuth2 v1 to v2 | Autodesk Platform Services</a>&#0160;から転写・翻訳、<a href="https://aps.autodesk.com/blog/authentication-v2-and-deprecation-v1" rel="noopener" target="_blank">Authentication v2 and deprecation of v1 | Autodesk Platform Services</a> の内容を加味したものです。</p>
<p>By Toshiaki Isezaki<a href="https://forge.zendesk.com/agent/tickets/14962" rel="noopener" target="_blank"></a></p>
