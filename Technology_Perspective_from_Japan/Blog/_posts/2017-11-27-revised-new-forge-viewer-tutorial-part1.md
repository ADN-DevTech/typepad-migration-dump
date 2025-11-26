---
layout: "post"
title: "新しい Forge Viewer チュートリアル改定版 ～ その1"
date: "2017-11-27 00:01:28"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/11/revised-new-forge-viewer-tutorial-part1.html "
typepad_basename: "revised-new-forge-viewer-tutorial-part1"
typepad_status: "Publish"
---

<p>以前ご案内した&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/07/new-forge-viewer-tutorial-part1.html" rel="noopener noreferrer" target="_blank">新しい Forge Viewer チュートリアル ～ その1</a></strong> の手順が改定されていますので、改めて、更新された内容を記しておきたいと思います。Forge Viewer の評価等でお使いいただければと思います。なお、この記事では&#0160;Windows 上での利用を想定し、<strong><a href="https://github.com/Autodesk-Forge/viewer-nodejs-tutorial" rel="noopener noreferrer" target="_blank">https://github.com/Autodesk-Forge/viewer-nodejs-tutorial</a></strong>&#0160;リポジトリに記載された手順を踏襲していきます。</p>
<hr />
<ol>
<li>&#0160;git for Windows と Node.js、Postman をインストールされていない場合には、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener noreferrer" target="_blank">Forge の開発環境</a></strong>&#0160;の内容をご確認の上、アカウント取得とインストールを完了させてください。</li>
<li>Forge を利用するために必要な Client ID と Client Secret（別名 Consumer Key と Consumer Secret）を取得していない場合には、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener noreferrer" target="_blank">Forge API を利用するアプリの登録とキーの取得</a>&#0160;</strong>の内容に沿って、キーを取得してください。</li>
<li>チュートリアルの環境をセットアップしてきます。&#0160;<strong><a href="https://github.com/Autodesk-Forge/viewer-nodejs-tutorial" rel="noopener noreferrer" target="_blank">https://github.com/Autodesk-Forge/viewer-nodejs-tutorial</a></strong>&#0160;リポジトリ&#0160;ページを表示します。</li>
<li>コマンド プロンプから git コマンドを使って素材となるソースコード一式をクライアント コンピュータにコピーします。まずは、コピー対象の URL をページ上でクリップボードにコピーします。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2c0f54f970c-pi" style="display: inline;"><img alt="Repository_url" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2c0f54f970c image-full img-responsive" src="/assets/image_670633.jpg" title="Repository_url" /></a></li>
<li><strong>コマンド プロンプト</strong>&#0160;を起動後、<strong>cd documents</strong> と入力してカレント フォルダを Documents フォルダに変更します。</li>
<li>git clone コマンドで確認した&#0160;URL をパラメータに指定して、<strong><a href="https://github.com/yochiyochirb/meetups/wiki/clone-a-repository" rel="noopener noreferrer" target="_blank">リポジトリ</a>&#0160;</strong>の内容をクライアント コンピュータにコピーします。具体的には、<strong>git clone https://github.com/Autodesk-Forge/viewer-nodejs-tutorial.git</strong>&#0160;と入力してください。リポジトリのコピーが始まります。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2c0fa8f970c-pi" style="display: inline;"><img alt="Git_clone" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2c0fa8f970c image-full img-responsive" src="/assets/image_551057.jpg" title="Git_clone" /></a></li>
<li>C:\Users\&lt;<em>Windows ログイン ユーザ名</em>&gt;\Documents フォルダ（ドキュメント フォルダ）直下に viewer-nodejs-tutorial フォルダが作成されていることを確認の上、コマンド プロンプトで <strong>cd viewer-nodejs-tutorial</strong> と入力してカレント フォルダを変更してください。</li>
<li>続いて、<strong><a href="https://ja.wikipedia.org/wiki/Npm_(%E3%83%91%E3%83%83%E3%82%B1%E3%83%BC%E3%82%B8%E7%AE%A1%E7%90%86%E3%83%84%E3%83%BC%E3%83%AB)" rel="noopener noreferrer" target="_blank">ノード パッケージ マネージャ（npm）</a></strong>を利用し、package.json 内に記載された依存関係に沿って 、Node.js 機能を拡張するパッケージ（ミドルウェア）をインストールしていきます。&#0160;<strong>npm install</strong> と入力してください。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2c13608970c-pi" style="display: inline;"><img alt="Npm_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2c13608970c image-full img-responsive" src="/assets/image_982543.jpg" title="Npm_install" /></a></li>
<li>Node パッケージは&#0160;viewer-nodejs-tutorial フォルダ直下に新しく作られた&#0160;node_modules フォルダにインストールされます。念のため、同フォルダが作成されていることを確認してください。</li>
<li>このサンプルでは、Web サーバーを構築する目的で Node.js を利用します。Web ブラウザを介して Forge サイトを閲覧する際には、Web サーバー側のコードは隠蔽されて閲覧できない点に注意してください。逆に、ユーザが Web ブラウザ側（クライアント）で閲覧、実行可能なのは、 Viewer 制御などで利用する JavaScitpt コードになります。本サンプル実装では、&#0160;viewer-nodejs-tutorial フォルダ直下の&#0160;server フォルダ内のコードがサーバー側で実行されるコード、www&#0160;フォルダ内のコードがクライアント側で実行されるコードになります。ここでは、server フォルダ内の<strong>&#0160;config.js</strong> を Adobe Brackets で開いて、下記の<span style="color: #0000ff;"><strong>青字部分</strong></span>を<strong>&#0160;</strong>2. で取得済の Client ID と Client Secret で置き換えます。リポジトリにある説明では、環境変数&#0160;FORGE_CLIENT_ID と FORGE_CLIENT_SECRET を設定するようになっていますが、今回は、直接&#0160;config.js に書き込みをします。<br />
<pre><span class="pl-s"><span class="pl-pds">&#39;</span>use strict<span class="pl-pds">&#39;</span></span>; <span class="pl-c">// http://www.w3schools.com/js/js_strict.asp</span>

<span class="pl-c1">module</span>.<span class="pl-smi">exports</span> <span class="pl-k">=</span> {

  <span class="pl-c">// Autodesk Forge configuration</span>

  <span class="pl-c">// this this callback URL when creating your client ID and secret</span>
  callbackURL<span class="pl-k">:</span> <span class="pl-c1">process</span>.<span class="pl-smi">env</span>.<span class="pl-c1">FORGE_CALLBACK_URL</span> <span class="pl-k">||</span> <span class="pl-s"><span class="pl-pds">&#39;</span>YOURCALLBACKURL<span class="pl-pds">&#39;</span></span>,

  <span class="pl-c">// set environment variables or hard-code here</span>
  credentials<span class="pl-k">:</span> {
    client_id<span class="pl-k">:</span> <span class="pl-c1">process</span>.<span class="pl-smi">env</span>.<span class="pl-c1">FORGE_CLIENT_ID</span> <span class="pl-k">||</span> <span class="pl-s"><span class="pl-pds">&#39;<span style="color: #0000ff;"><strong>YOUR CLIENT ID</strong></span></span><span class="pl-pds">&#39;</span></span>,
    client_secret<span class="pl-k">:</span> <span class="pl-c1">process</span>.<span class="pl-smi">env</span>.<span class="pl-c1">FORGE_CLIENT_SECRET</span> <span class="pl-k">||</span> <span class="pl-s"><span class="pl-pds">&#39;<span style="color: #0000ff;"><strong>YOUR</strong><strong> CLIENT SECRET</strong></span></span><span class="pl-pds">&#39;</span></span>
  },

  <span class="pl-c">// Required scopes for your application on server-side</span>
  scopeInternal<span class="pl-k">:</span> [<span class="pl-s"><span class="pl-pds">&#39;</span>data:read<span class="pl-pds">&#39;</span></span>,<span class="pl-s"><span class="pl-pds">&#39;</span>data:write<span class="pl-pds">&#39;</span></span>,<span class="pl-s"><span class="pl-pds">&#39;</span>data:create<span class="pl-pds">&#39;</span></span>,<span class="pl-s"><span class="pl-pds">&#39;</span>data:search<span class="pl-pds">&#39;</span></span>],
  <span class="pl-c">// Required scope of the token sent to the client</span>
  scopePublic<span class="pl-k">:</span> [<span class="pl-s"><span class="pl-pds">&#39;</span>viewables:read<span class="pl-pds">&#39;</span></span>]
};</pre>
<ul>
<li>server フォルダ内には、Client ID と Client Secret を設定した config.js 以外にも、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/forge-sdk.html" rel="noopener noreferrer" target="_blank">Forge SDK</a></strong> を使ってクライアントに Access Token を返すルーティング（/user/token）を定義する oauth.js、Access Token をセッションに保存する server.js、Token プロトタイプを定義する token.js がインストールされています。</li>
<li>www&#0160;フォルダ内には、このチュートリアルが Web サーバーにホストしてクライアントに表示する HTML 定義である&#0160;index.html と、同ファイルが参照する&#0160;\www\js\index.js や CSS 定義などのファイルがフォルダ毎にインストールされてます。</li>
</ul>
</li>
<li><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/09/understanding-steps-to-use-viewer-on-postman.html" rel="noopener noreferrer" target="_blank">Postman による Viewer 利用手順の理解 - 2 legged 認証</a></strong> などで、Viewer に表示させるドキュメント ID（URN）を取得してください。</li>
<li>www/js/index.js&#0160;を Adobe Brackets で開いて、下記の<span style="color: #0000ff;"><strong>青字部分</strong></span>を取得済のドキュメント ID（URN）で置き換えてください。<br />
<pre><span class="pl-k">var</span> viewer;
<span class="pl-k">var</span> options <span class="pl-k">=</span> {
    env<span class="pl-k">:</span> <span class="pl-s"><span class="pl-pds">&#39;</span>AutodeskProduction<span class="pl-pds">&#39;</span></span>,
    getAccessToken<span class="pl-k">:</span> getForgeToken
}

<span class="pl-k">var</span> documentId <span class="pl-k">=</span> <span class="pl-s"><span class="pl-pds">&#39;</span>urn:<span style="color: #0000ff;"><strong>YOUR-URN</strong></span><span class="pl-pds">&#39;</span></span>;

<span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-en">Initializer</span>(options, <span class="pl-k">function</span> <span class="pl-en">onInitialized</span>() {
    <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-smi">Document</span>.<span class="pl-c1">load</span>(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);
});

<span class="pl-c">/**</span>
<span class="pl-c"> * Autodesk.Viewing.Document.load() success callback.</span>
<span class="pl-c"> * Proceeds with model initialization.</span>
<span class="pl-c"> */</span>
<span class="pl-k">function</span> <span class="pl-en">onDocumentLoadSuccess</span>(<span class="pl-smi">doc</span>) {

    <span class="pl-c">// A document contains references to 3D and 2D viewables.</span>
    <span class="pl-k">var</span> viewable <span class="pl-k">=</span> <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-smi">Document</span>.<span class="pl-en">getSubItemsWithProperties</span>(<span class="pl-smi">doc</span>.<span class="pl-en">getRootItem</span>(), {
        <span class="pl-s"><span class="pl-pds">&#39;</span>type<span class="pl-pds">&#39;</span></span><span class="pl-k">:</span> <span class="pl-s"><span class="pl-pds">&#39;</span>geometry<span class="pl-pds">&#39;</span></span>,
        <span class="pl-s"><span class="pl-pds">&#39;</span>role<span class="pl-pds">&#39;</span></span><span class="pl-k">:</span> <span class="pl-s"><span class="pl-pds">&#39;</span>3d<span class="pl-pds">&#39;</span></span>
    }, <span class="pl-c1">true</span>);
    <span class="pl-k">if</span> (<span class="pl-smi">viewable</span>.<span class="pl-c1">length</span> <span class="pl-k">===</span> <span class="pl-c1">0</span>) {
        <span class="pl-en">console</span>.<span class="pl-c1">error</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Document contains no viewables.<span class="pl-pds">&#39;</span></span>);
        <span class="pl-k">return</span>;
    }

    <span class="pl-c">// Choose any of the available viewable</span>
    <span class="pl-k">var</span> initialViewable <span class="pl-k">=</span> viewable[<span class="pl-c1">0</span>]; <span class="pl-c">// You can check for other available views in your model,</span>
    <span class="pl-k">var</span> svfUrl <span class="pl-k">=</span> <span class="pl-smi">doc</span>.<span class="pl-en">getViewablePath</span>(initialViewable);
    <span class="pl-k">var</span> modelOptions <span class="pl-k">=</span> {
        sharedPropertyDbPath<span class="pl-k">:</span> <span class="pl-smi">doc</span>.<span class="pl-en">getPropertyDbPath</span>()
    };

    <span class="pl-k">var</span> viewerDiv <span class="pl-k">=</span> <span class="pl-c1">document</span>.<span class="pl-c1">getElementById</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>viewerDiv<span class="pl-pds">&#39;</span></span>);

    <span class="pl-c">///////////////USE ONLY ONE OPTION AT A TIME/////////////////////////</span>

    <span class="pl-c">/////////////////////// Headless Viewer /////////////////////////////</span>
    <span class="pl-c">// viewer = new Autodesk.Viewing.Viewer3D(viewerDiv);</span>
    <span class="pl-c">//////////////////////////////////////////////////////////////////////</span>

    <span class="pl-c">//////////////////Viewer with Autodesk Toolbar///////////////////////</span>
    viewer <span class="pl-k">=</span> <span class="pl-k">new</span> <span class="pl-en">Autodesk.Viewing.Private.GuiViewer3D</span>(viewerDiv);
    <span class="pl-c">//////////////////////////////////////////////////////////////////////</span>

    <span class="pl-smi">viewer</span>.<span class="pl-c1">start</span>(svfUrl, modelOptions, onLoadModelSuccess, onLoadModelError);
}


<span class="pl-c">/**</span>
<span class="pl-c"> * Autodesk.Viewing.Document.load() failure callback.</span>
<span class="pl-c"> */</span>
<span class="pl-k">function</span> <span class="pl-en">onDocumentLoadFailure</span>(<span class="pl-smi">viewerErrorCode</span>) {
    <span class="pl-en">console</span>.<span class="pl-c1">error</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>onDocumentLoadFailure() - errorCode:<span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> viewerErrorCode);
}

<span class="pl-c">/**</span>
<span class="pl-c"> * viewer.loadModel() success callback.</span>
<span class="pl-c"> * Invoked after the model&#39;s SVF has been initially loaded.</span>
<span class="pl-c"> * It may trigger before any geometry has been downloaded and displayed on-screen.</span>
<span class="pl-c"> */</span>
<span class="pl-k">function</span> <span class="pl-en">onLoadModelSuccess</span>(<span class="pl-smi">model</span>) {
    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>onLoadModelSuccess()!<span class="pl-pds">&#39;</span></span>);
    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Validate model loaded: <span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> (<span class="pl-smi">viewer</span>.<span class="pl-smi">model</span> <span class="pl-k">===</span> model));
    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(model);
}

<span class="pl-c">/**</span>
<span class="pl-c"> * viewer.loadModel() failure callback.</span>
<span class="pl-c"> * Invoked when there&#39;s an error fetching the SVF file.</span>
<span class="pl-c"> */</span>
<span class="pl-k">function</span> <span class="pl-en">onLoadModelError</span>(<span class="pl-smi">viewerErrorCode</span>) {
    <span class="pl-en">console</span>.<span class="pl-c1">error</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>onLoadModelError() - errorCode:<span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> viewerErrorCode);
}</pre>
</li>
<li>ここまで用意してきた Web サーバー コードを開発環境上（お使いのコンピュータ）で実行して確認します。コマンド プロンプト上で<strong> npm run dev</strong> と入力してください。<strong>Node start.js</strong> と入力しても Web サーバーを起動することが出来ます。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2c13947970c-pi" style="display: inline;"><img alt="Npm_run" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2c13947970c image-full img-responsive" src="/assets/image_494412.jpg" title="Npm_run" /></a></li>
<li>エラーなく Node.js サーバー実装が稼働したら、クライアント側の動作を確認をします。Google Chrome などの WebGL 互換 Web ブラウザを起動して、URL 欄に&#0160;<strong>http://localhost:3000</strong> と入力、実行してみてください。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c936eab9970b-pi" style="display: inline;"><img alt="Model_on_viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c936eab9970b image-full img-responsive" src="/assets/image_526924.jpg" title="Model_on_viewer" /></a></li>
<li>正しくモデルが表示されたら成功です。Web ブラウザを表示した状態で F12 キーを押下するとデベロッパー ツール(開発者ツール) 画面が表示され、クライアント実装となる 12. で編集した index.js の内容が確認出来るはずです。サーバー実装した Client ID や Client Secret は表示されない点に注意してください。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c936eadc970b-pi" style="display: inline;"><img alt="Developer_tool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c936eadc970b image-full img-responsive" src="/assets/image_850295.jpg" title="Developer_tool" /></a>&#0160;</li>
</ol>
<hr />
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/11/revised-new-forge-viewer-tutorial-part2.html" rel="noopener noreferrer" target="_blank">次回</a>&#0160;</strong>は、各種 Extension をロードさせたり、背景を変更するなどして、Viewer の状態を拡張します。&#0160;</p>
<p>By Toshiaki Isezaki</p>
