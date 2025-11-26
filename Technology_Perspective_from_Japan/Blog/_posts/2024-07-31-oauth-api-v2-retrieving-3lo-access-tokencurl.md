---
layout: "post"
title: "OAuth API v2：cURL を使った APS 3-legged アクセストークンの取得"
date: "2024-07-31 01:06:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/07/oauth-api-v2-retrieving-3lo-access-tokencurl.html "
typepad_basename: "oauth-api-v2-retrieving-3lo-access-tokencurl"
typepad_status: "Publish"
---

<p>Autodesk Platform Services（APS）の公式ドキュメントでは、REST API エンドポイントの使用例に <a href="https://ja.wikipedia.org/wiki/CURL" rel="noopener" target="_blank">cURL</a>&#0160;を使った例を多用しています。</p>
<p>ドキュメントに記された cURL での記述例を使って、ほとんどのエンドポイントを実際にテストすることが出来ます。エンドポイントのテスト・評価には、ユーザー インターフェースを持つ <a href="https://www.postman.com/" rel="noopener" target="_blank">Postman</a> や <a href="https://insomnia.rest/" rel="noopener" target="_blank">Insomnia</a> がよく利用されますが、cURL だとリクエスト ヘッダーやリクエスト ボディを同時に確認することが出来る、という利点があります。</p>
<p>ここでは、一部 Web ブラウザと <a href="https://techtarget.itmedia.co.jp/tt/news/1910/13/news01.html" rel="noopener" target="_blank">Bash</a> を使い、<a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" rel="noopener" target="_blank">Get a 3-Legged Token with Authorization Code Grant</a> の手順でアクセストークンを取得する手順をご紹介してみます。なお、この記事では <a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank">APS の開発環境</a>でご紹介した git for Windows に同梱されている Git Bash を利用しています。</p>
<hr />
<p><strong>事前準備</strong></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a> を参考に、APS ポータルでアプリを作成して Client ID と Client Secret を取得します。この際、Callback URL に <strong>http://localhost:8080/oauth/callback/</strong> を指定してください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3baaa76200b-pi" style="display: inline;"><img alt="App_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3baaa76200b image-full img-responsive" src="/assets/image_478716.jpg" title="App_settings" /></a></p>
<hr />
<p><a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/#step-1-direct-the-user-to-the-authorization-web-flow" rel="noopener" target="_blank"><strong>Step 1</strong></a></p>
<p style="padding-left: 40px;">Web ブラウザーを起動して、URL 欄に取得した Client ID、 Scope、Callback URL をクエリー パラメーターに指定して <a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/authorize-GET/" rel="noopener" target="_blank">GET authorize</a> エンドポイントを入力、オーソライゼーション コードを取得します。</p>
<blockquote>
<p><span style="font-size: 10pt;">https://developer.api.autodesk.com/authentication/v2/authorize?response_type=code&amp;client_id=&lt;<strong>CLIENT ID&gt;</strong>&amp;redirect_uri=<strong>http://localhost:8080/oauth/callback/</strong>&amp;scope=<strong>data:read%20data:write</strong></span></p>
</blockquote>
<p style="padding-left: 40px;"><a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/authorize-GET/" rel="noopener" target="_blank">GET authorize</a> エンドポイントの入力を完了すると、アクセス許可（認可）を得るためにユーザーのサインインを求めるページが表示されます。Autodesk ID（オートデスク アカウント）のユーザー名を入力してください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3baaae7200b-pi" style="display: inline;"><img alt="Signin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3baaae7200b image-full img-responsive" src="/assets/image_636482.jpg" title="Signin" /></a></p>
<p style="padding-left: 40px;">続いて、入力した Autodesk ID のパスワードを求められます。適切なパスワードを入力してください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3baaaf0200b-pi" style="display: inline;"><img alt="Password" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3baaaf0200b image-full img-responsive" src="/assets/image_831418.jpg" title="Password" /></a></p>
<p style="padding-left: 40px;">サインインが完了すると、APS アプリ（ここでは Cur Test）が、サインインしたアカウント権限でクラウド リソースにアクセスしていいか、認可を求めるページに遷移します。認可する場合には、<span style="background-color: #111111; color: #ffffff;">[許可]</span> ボタンをクリックします。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0cad538200d-pi" style="display: inline;"><img alt="Authorize" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0cad538200d image-full img-responsive" src="/assets/image_523416.jpg" title="Authorize" /></a></p>
<p><a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/#step-2-implement-code-that-extracts-the-authorization-code" rel="noopener" target="_blank"><strong>Step 2</strong></a></p>
<p style="padding-left: 40px;">認可が完了すると、Web ブラウザーの URL 欄に表示されている http://localhost:8080/oauth/callback/?code=&lt;AUTHORIZATION CODE&gt; の文字列から、クエリー パラメータの code=<strong>&lt;AUTHORIZATION CODE&gt;</strong> 値をクリップボードを介してどこかに保存しておきます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3baab00200b-pi" style="display: inline;"><img alt="Error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3baab00200b image-full img-responsive" src="/assets/image_149559.jpg" title="Error" /></a></p>
<p><a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/#step-3-exchange-the-authorization-code-for-an-access-token" rel="noopener" target="_blank"><strong>Step 3</strong></a></p>
<p style="padding-left: 40px;">次に、オーソライゼーション コードからアクセス トークンを取得する <a href="https://aps.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/">POST token</a> エンドポイントを呼び出します。まず、リクエスト ヘッダーを準備するため、Client ID と Client Secret を :（コロン）で挟んだ値を Base64 エンコードします。Bash に次のように入力します。</p>
<blockquote>
<p><span style="font-size: 10pt;">echo -n &#39;&lt;CLIENT ID&gt;:&lt;CLIENT SECRET&gt;&#39; | base64</span></p>
</blockquote>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b7067c200c-pi" style="display: inline;"><img alt="Base64_encode" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b7067c200c image-full img-responsive" src="/assets/image_71338.jpg" title="Base64_encode" /></a></p>
<p style="padding-left: 40px;">Base64 エンコードされた値が改行されているので、改行を削除したら、次の書式で cURL 構文を整え、Bash に入力します。</p>
<blockquote>
<p><span style="font-size: 10pt;">curl -v &#39;https://developer.api.autodesk.com/authentication/v2/token&#39; \</span><br /><span style="font-size: 10pt;">-X &#39;POST&#39; \</span><br /><span style="font-size: 10pt;">-H &#39;Content-Type: application/x-www-form-urlencoded&#39; \</span><br /><span style="font-size: 10pt;">-H &#39;Authorization: Basic <strong>&lt;BASE64 ENCODED AUTHORIZATION VALUE&gt;</strong>&#39; \</span><br /><span style="font-size: 10pt;">-d &#39;grant_type=authorization_code&#39; \</span><br /><span style="font-size: 10pt;">-d &#39;code=<strong>&lt;AUTHORIZATION CODE&gt;</strong>&#39; \</span><br /><span style="font-size: 10pt;">-d &#39;redirect_uri=<strong>http://localhost:8080/oauth/callback/</strong>&#39;</span></p>
</blockquote>
<p style="padding-left: 40px;">正しくプロセスが完了すると、アクセス トークン（&quot;access_token&quot; 値）を含む JSON が返されます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3baab6e200b-pi" style="display: inline;"><img alt="Access_token" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3baab6e200b image-full img-responsive" src="/assets/image_87721.jpg" title="Access_token" /></a></p>
<hr />
<p>By Toshiaki Isezaki</p>
