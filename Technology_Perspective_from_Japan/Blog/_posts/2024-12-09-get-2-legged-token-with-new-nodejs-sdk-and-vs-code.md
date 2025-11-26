---
layout: "post"
title: "新 Node.js SDK と VS Code で 2-legged アクセストークンを取得"
date: "2024-12-09 00:01:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/12/get-2-legged-token-with-new-nodejs-sdk-and-vs-code.html "
typepad_basename: "get-2-legged-token-with-new-nodejs-sdk-and-vs-code"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dab10f200b-pi" style="display: inline;"><img alt="2-legged" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dab10f200b img-responsive" src="/assets/image_580255.jpg" title="2-legged" /></a></p>
<p><span style="background-color: #ffffff;">新しい Node.js SDK（Beta）と Windows の VS Code を使い、単純にアクセストークンを取得する例を</span>ご紹介したいと思います。まずは、<span style="background-color: #ffffff;">2-legged 認証フローでの方法です。</span></p>
<hr />
<p><strong>新 Node.js SDK のインストール</strong></p>
<p>新しい Node.js SDK は、Node.js パッケージとして <a href="https://www.npmjs.com/~aps.sdk" rel="noopener" target="_blank">https://www.npmjs.com/~aps.sdk</a>（GiiHub リポジトリ：<a href="https://github.com/autodesk-platform-services/aps-sdk-node" rel="noopener" target="_blank">https://github.com/autodesk-platform-services/aps-sdk-node</a>）されています。ローカル環境で SDK（ここでは SDK Manager と Authentication API パッケージのみ）を利用するのは、コマンド プロンプトを使って、次の手順でインストールをおこないます。</p>
<ol>
<li>MKDIR コマンドで <strong>aps-sdk-node</strong> フォルダを作成します。（フォルダ名は任意です。）</li>
<li>CD コマンドで aps-sdk-node フォルダに移動します。</li>
<li><strong>npm install --save @aps_sdk/autodesk-sdkmanager @aps_sdk/authentication</strong>&#0160;と入力して、aps-sdk-node フォルダ下に SDK Manager と Authentication API パッケージをインストールします。</li>
</ol>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c429e3200c-pi" style="display: inline;"><img alt="Npm_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c429e3200c image-full img-responsive" src="/assets/image_667939.jpg" title="Npm_install" /></a></p>
<hr />
<p><strong>コードの準備</strong></p>
<ol>
<li>VS Code を起動後、[ファイル] &gt;&gt; [フォルダーを開く...] メニューから aps-sdk-node フォルダを開きます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860daafe3200b-pi" style="display: inline;"><img alt="Open_folder" class="asset  asset-image at-xid-6a0167607c2431970b02e860daafe3200b img-responsive" src="/assets/image_803469.jpg" title="Open_folder" /></a></li>
<li>続いて、[ファイル] &gt;&gt; [新しいテキスト ファイル] メニューから、VS Code 上に新しいファイルを作成します。「言語の選択」では <strong>JavaScript</strong> を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860dab027200b-pi" style="display: inline;"><img alt="Select_language" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860dab027200b img-responsive" src="/assets/image_24854.jpg" title="Select_language" /></a></li>
<li>作成したファイルに次のコードを貼り付けて。<span style="color: #0000ff; font-family: arial, helvetica, sans-serif; font-size: 10pt;"><strong>&lt;Your Client ID&gt;</strong> </span>と <span style="color: #0000ff; font-family: arial, helvetica, sans-serif; font-size: 10pt;"><strong>&lt;Your Client Secret&gt;</strong></span> の箇所を使用する Client ID と Client Secret にそれぞれ置き換えます。</li>
</ol>
<pre><code>const { SdkManagerBuilder } = require(&quot;@aps_sdk/autodesk-sdkmanager&quot;);
const { AuthenticationClient, Scopes } = require(&quot;@aps_sdk/authentication&quot;);

const sdkManager = SdkManagerBuilder.create().build();
const authenticationClient = new AuthenticationClient(sdkManager);

let APS_CLIENT_ID = &quot;<span style="color: #0000ff;"><strong>&lt;Your Client ID&gt;</strong></span>&quot;;
let APS_CLIENT_SECRET = &quot;<strong><span style="color: #0000ff;">&lt;Your Client Secret&gt;</span></strong>&quot;;

async function getTwoLeggedToken() {
    try {
        const twoLeggedCredentials = await authenticationClient.getTwoLeggedToken(APS_CLIENT_ID, APS_CLIENT_SECRET, [Scopes.DataRead, Scopes.ViewablesRead]); 
        console.log(twoLeggedCredentials.access_token);
    }
    catch (error) {
        console.error(error);
    }
}

getTwoLeggedToken();
</code></pre>
<ol start="4">
<li>[ファイル] &gt;&gt; [名前を付けて保存...] メニューを選択して、aps-sdk-node フォルダ下に <strong>2-legged.js</strong> の名前で保存します。（ファイル名は任意です。）</li>
</ol>
<hr />
<p><strong>コードの実行 - アクセストークンの取得</strong></p>
<ol>
<li>2-legged.js を表示した状態で、[実行] &gt;&gt; [デバッグの開始] メニューを選択、続けて「デバッガ―の選択」で <strong>Node.js</strong> を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f1d3d8200d-pi" style="display: inline;"><img alt="Start_debug" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f1d3d8200d image-full img-responsive" src="/assets/image_503449.jpg" title="Start_debug" /></a></li>
<li>getTwoLeggedToken() 関数が実行されて、[デバッグ コンソール] タブに取得したアクセストークンが表示されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f1f01e200d-pi" style="display: inline;"><img alt="Access_token" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f1f01e200d img-responsive" src="/assets/image_439990.jpg" title="Access_token" /></a></li>
</ol>
<hr />
<p>By Toshiaki Isezaki</p>
