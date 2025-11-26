---
layout: "post"
title: "新 Node.js SDK と VS Code で 3-legged アクセストークンを取得"
date: "2024-12-11 00:08:09"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/12/get-3-legged-token-with-new-nodejs-sdk-vs-code.html "
typepad_basename: "get-3-legged-token-with-new-nodejs-sdk-vs-code"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860daaf4f200b-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dab116200b-pi" style="display: inline;"><img alt="3-legged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dab116200b image-full img-responsive" src="/assets/image_110501.jpg" title="3-legged" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860daaf4f200b-pi" style="display: inline;"><br /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2024/12/get-2-legged-token-with-new-nodejs-sdk-and-vs-code.html" rel="noopener" target="_blank">新 Node.js SDK と VS Code で 2-legged アクセストークンを取得</a> の例に続いて、3-legged 認証フローでのアクセストークン取得を Windows の VS Code 上でテストする例をご紹介したいと思います。</p>
<hr />
<p><strong>新 Node.js SDK のインストール</strong></p>
<p>新しい Node.js SDK のインストールは、基本的に <a href="https://adndevblog.typepad.com/technology_perspective/2024/12/get-2-legged-token-with-new-nodejs-sdk-and-vs-code.html" rel="noopener" target="_blank">新 Node.js SDK と VS Code で 2-legged アクセストークンを取得</a> と同じです。&#0160;<a href="https://www.npmjs.com/~aps.sdk" rel="noopener" target="_blank">https://www.npmjs.com/~aps.sdk</a>（GiiHub リポジトリ：<a href="https://github.com/autodesk-platform-services/aps-sdk-node" rel="noopener" target="_blank">https://github.com/autodesk-platform-services/aps-sdk-node</a>）から Node.js パッケージを次の手順でインストールをおこないます。</p>
<ol>
<li>MKDIR コマンドで <strong>aps-sdk-node</strong> フォルダを作成します。（フォルダ名は任意です。）</li>
<li>CD コマンドで aps-sdk-node フォルダに移動します。</li>
<li><strong>npm install --save @aps_sdk/autodesk-sdkmanager @aps_sdk/authentication</strong> と入力して、aps-sdk-node フォルダ下に SDK Manager と Authentication API パッケージをインストールします。</li>
<li>3-legged 認証フローでは、コールバックを処理するために <a href="https://www.npmjs.com/package/express" rel="noopener" target="_blank">express</a> パッケージを使った <a href="https://expressjs.com/ja/guide/routing.html" rel="noopener" target="_blank">ルーティング</a>&#0160;をおこないます。コマンドプロンプトに <strong>npm install express</strong> と入力して、aps-sdk-node フォルダ下に express パッケージをインストールします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c44917200c-pi" style="display: inline;"><img alt="Npm_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c44917200c img-responsive" src="/assets/image_203071.jpg" title="Npm_install" /></a></li>
</ol>
<hr />
<p><strong>アプリ設定</strong></p>
<ol>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a> の手順 13. で、使用する Callback URL を <strong>http://localhost:3000/callback</strong> に設定します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dace9a200b-pi" style="display: inline;"><img alt="Callback" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dace9a200b image-full img-responsive" src="/assets/image_503683.jpg" title="Callback" /></a></li>
</ol>
<hr />
<p><strong>コードの準備</strong></p>
<ol>
<li>VS Code を起動後、[ファイル] &gt;&gt; [フォルダーを開く...] メニューから aps-sdk-node フォルダを開きます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860daafe3200b-pi" style="display: inline;"><img alt="Open_folder" class="asset  asset-image at-xid-6a0167607c2431970b02e860daafe3200b img-responsive" src="/assets/image_803469.jpg" title="Open_folder" /></a></li>
<li>続いて、[ファイル] &gt;&gt; [新しいテキスト ファイル] メニューから、VS Code 上に新しいファイルを作成します。「言語の選択」では <strong>JavaScript</strong> を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dab027200b-pi" style="display: inline;"><img alt="Select_language" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dab027200b img-responsive" src="/assets/image_24854.jpg" title="Select_language" /></a></li>
<li>作成したファイルに次のコードを貼り付けて。<span style="color: #0000ff; font-family: arial, helvetica, sans-serif; font-size: 10pt;"><strong>&lt;Your Client ID&gt;</strong> </span>と <span style="color: #0000ff; font-family: arial, helvetica, sans-serif; font-size: 10pt;"><strong>&lt;Your Client Secret&gt;</strong></span> の箇所を使用する Client ID と Client Secret にそれぞれ置き換えます。</li>
</ol>
<pre><code>const { SdkManagerBuilder } = require(&quot;@aps_sdk/autodesk-sdkmanager&quot;);
const { AuthenticationClient,  ResponseType, Scopes } = require(&quot;@aps_sdk/authentication&quot;);
const express = require(&#39;express&#39;);
let app = express();

const sdkManager = SdkManagerBuilder.create().build();
const authenticationClient = new AuthenticationClient(sdkManager);

let APS_CLIENT_ID = &quot;<span style="color: #0000ff; font-family: arial, helvetica, sans-serif; font-size: 10pt;"><strong>&lt;Your Client ID&gt;</strong></span>&quot;;
let APS_CLIENT_SECRET = &quot;<span style="color: #0000ff; font-family: arial, helvetica, sans-serif; font-size: 10pt;"><strong>&lt;Your Client Secret&gt;</strong></span>&quot;;
let APS_CALLBACK_URL = &quot;<strong>http://localhost:3000/callback</strong>&quot;;

app.get(&#39;/&#39;, function (req, res) {
    try {
        url = authenticationClient.authorize(APS_CLIENT_ID, ResponseType.Code, APS_CALLBACK_URL, new Array(Scopes.DataRead, Scopes.DataCreate));
        res.redirect(url);
    }
    catch (error) { 
        console.error(error); 
    }
});

app.get(&#39;/callback&#39;, async function (req, res) {
    try{
        let acode = req.query.code;
        const threeLeggedCredentials = await authenticationClient.getThreeLeggedToken(APS_CLIENT_ID, acode, APS_CALLBACK_URL, {
            clientSecret: APS_CLIENT_SECRET
        });
        console.log(threeLeggedCredentials.access_token);
        res.send(&#39;Got Credentials!&#39;);    
    }
    catch (error) {
        console.error(error);
    }
});

app.listen(3000);
console.log(&quot;Server is listening port 3000&quot;);
</code></pre>
<ol start="4">
<li>[ファイル] &gt;&gt; [名前を付けて保存...] メニューを選択して、aps-sdk-node フォルダ下に <strong>3-legged.js</strong> の名前で保存します。（ファイル名は任意です。）</li>
</ol>
<hr />
<p><strong>コードの実行 - アクセストークンの取得</strong></p>
<ol>
<li>3-legged.js を表示した状態で、[実行] &gt;&gt; [デバッグの開始] メニューを選択、続けて「デバッガ―の選択」で <strong>Node.js</strong> を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f1d3d8200d-pi" style="display: inline;"><img alt="Start_debug" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f1d3d8200d image-full img-responsive" src="/assets/image_503449.jpg" title="Start_debug" /></a></li>
<li>Web ブラウザを起動して、URL 欄に <strong>localhost:3000</strong> と入力します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f1f09d200d-pi" style="display: inline;"><img alt="Localhost" class="asset  asset-image at-xid-6a0167607c2431970b02e860f1f09d200d img-responsive" src="/assets/image_890367.jpg" title="Localhost" /></a></li>
<li>サインインを求められるので、Autodesk ID（ユーザー名とパスワード）を入力後、「アプリケーションをオーソライズ」画面で認可のために <strong><span style="background-color: #000000; color: #ffffff;">[許可]</span></strong> ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c44811200c-pi" style="display: inline;"><img alt="Signin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c44811200c img-responsive" src="/assets/image_623954.jpg" title="Signin" /></a></li>
<li>[デバッグ コンソール]タブに取得したアクセストークンが表示されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c447b3200c-pi" style="display: inline;"><img alt="Access_token" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c447b3200c img-responsive" src="/assets/image_735599.jpg" title="Access_token" /></a></li>
</ol>
<hr />
<p>By Toshiaki Isezaki</p>
