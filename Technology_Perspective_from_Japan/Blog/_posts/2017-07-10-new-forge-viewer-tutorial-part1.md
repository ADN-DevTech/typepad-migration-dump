---
layout: "post"
title: "新しい Forge Viewer チュートリアル ～ その1"
date: "2017-07-10 02:12:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/07/new-forge-viewer-tutorial-part1.html "
typepad_basename: "new-forge-viewer-tutorial-part1"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2017/04/provision-stop-of-initial-view-and-data-api.html" rel="noopener noreferrer" target="_blank"><strong>初期の View and Data API の提供停止</strong></a>&#0160;にともなって、過去 <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09ad77df970d-pi" style="float: right;"><img alt="Octacat" class="asset  asset-image at-xid-6a0167607c2431970b01bb09ad77df970d img-responsive" src="/assets/image_477601.jpg" style="width: 200px; margin: 0px 0px 5px 5px;" title="Octacat" /></a>にご案内した <strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/forge-viewer-tutorial-part1-implement-nodejs-server.html" rel="noopener noreferrer" target="_blank">Forge Viewer チュートリアル</a>&#0160;</strong>も新しく生まれ変わっています。このブログ記事では、新しいチュートリアルとなる&#0160;<strong><a href="https://github.com/Autodesk-Forge/viewer-nodejs-tutorial" rel="noopener noreferrer" target="_blank">https://github.com/Autodesk-Forge/viewer-nodejs-tutorial</a></strong>&#0160;リポジトリに記載された <strong><a href="https://github.com/Autodesk-Forge/viewer-nodejs-tutorial" rel="noopener noreferrer" target="_blank">viewer-nodejs-tutorial </a></strong>サンプルの利用方法をご紹介するものです。</p>
<hr />
<ol>
<li>GitHub アカウントをお持ちでない場合や、GitHub Desktop と Node.js、Postman をインストールされていない場合には、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener noreferrer" target="_blank">Forge の開発環境</a></strong>&#0160;の内容をご確認の上、アカウント取得とインストールを完了させてください。実際の開発に GitHub アカウントや GitHub Desktop は必須ではありませんが、GirHub 上で公開されている Forge サンプルを参照する場合に便利です。</li>
<li>Forge を利用するために必要な Client ID と Client Secret（別名 Consumer Key と Consumer Secret）を取得していない場合には、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener noreferrer" target="_blank">Forge API を利用するアプリの登録とキーの取得</a>&#0160;</strong>の内容に沿って、キーを取得してください。</li>
<li>チュートリアルの環境をセットアップしてきます。 <strong><a href="https://github.com/Autodesk-Forge/viewer-nodejs-tutorial" rel="noopener noreferrer" target="_blank">https://github.com/Autodesk-Forge/viewer-nodejs-tutorial</a></strong>&#0160;リポジトリ&#0160;ページを表示して、右上の Sign in から GitHub アカウントでサインインします。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09ad789c970d-pi" style="display: inline;"><img alt="Github_signin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09ad789c970d image-full img-responsive" src="/assets/image_674797.jpg" title="Github_signin" /></a></li>
<li>Git Shell 上の git コマンドを使って素材となるソースコード一式をクライアント コンピュータにコピーします。まずは、コピー対象の URL をページ上で確認します。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09ad78c1970d-pi" style="display: inline;"><img alt="Github_repository_url" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09ad78c1970d image-full img-responsive" src="/assets/image_537915.jpg" title="Github_repository_url" /></a></li>
<li><strong>Git Shell</strong>&#0160;を起動して、git clone コマンドで確認した&#0160;URL をパラメータに指定して、<strong><a href="https://github.com/yochiyochirb/meetups/wiki/clone-a-repository" rel="noopener noreferrer" target="_blank">リポジトリ</a>&#0160;</strong>をクライアント コンピュータにコピーします。具体的には、<strong>git clone https://github.com/Autodesk-Forge/viewer-nodejs-tutorial.git</strong>&#0160;と入力してください。リポジトリのコピーが始まるはずです。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c90a3e1c970b-pi" style="display: inline;"><img alt="Clone_source_from_github" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c90a3e1c970b image-full img-responsive" src="/assets/image_835354.jpg" title="Clone_source_from_github" /></a><br />もし、Git Shell を利用せずにサンプルをダウンロードしたい場合には、隣にある [Download ZIP] ボタンを使って、任意に ZIP 圧縮されたソースコードを入手することも可能です。</li>
<li>コピーされたリポジトリが、<strong>C:\Users\&lt;<em>Windows ユーザ名</em>&gt;\Documents\GitHub</strong>&#0160;フォルダ直下の<strong> viewer-nodejs-tutorial</strong>&#0160;フォルダにあることを確認してください。</li>
<li>Node.js を利用した Web サーバーをローカル コンピュータ上に構築していきます。コピーしたリポジトリ内のサンプル コードが利用している Node Package を、npm（Node Package Manager）コマンドを使ってインストールしていきます。<strong>Node.js command prompt</strong>&#0160;を起動してローカル リポジトリのディレクトリに移動したら、<strong>npm install</strong>&#0160;と入力してください。インストールが開始されて、<strong><a href="https://www.npmjs.com/package/express" rel="noopener noreferrer" target="_blank">express</a></strong>、<strong><a href="https://www.npmjs.com/package/request" rel="noopener noreferrer" target="_blank">request</a></strong>、<strong><a href="https://www.npmjs.com/package/serve-favicon" rel="noopener noreferrer" target="_blank">serve-favicon</a>&#0160;</strong>の 3つの Node Package がインストールされるはずです。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2948b78970c-pi" style="display: inline;"><img alt="Npm_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2948b78970c image-full img-responsive" src="/assets/image_668142.jpg" title="Npm_install" /></a></li>
<li>同じ <strong>viewer-nodejs-tutorial</strong>&#0160;フォルダ内で、copy コマンドを使って&#0160;credentials_.js ファイルを&#0160;credentials.js の名前でコピーします。</li>
<li>コピーした&#0160;credentials.js ファイルを Adobe Brackets で開いて、&#0160;<strong><a href="https://developer.autodesk.com/" rel="noopener noreferrer" target="_blank">Developer Portal</a></strong>&#0160;サイトで取得済の Client ID と Client Secret の値に置き換えて上書き保存します。&lt;replace with your client id&gt; を Client ID で、&lt;replace with your client secret&gt; を Client Secret で書き換えて、ファイルを上書き保存してください。</li>
<li>Adobe Brackets で&#0160;<strong>viewer-nodejs-tutorial</strong>&#0160;フォルダ直下の Server.js を開き、コード部分のコメント記号 <strong>//</strong> を削除します。<br />
<pre><span style="color: #0000ff;"><strong><span class="pl-k">var</span> favicon <span class="pl-k">=</span> <span class="pl-c1">require</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>serve-favicon<span class="pl-pds">&#39;</span></span>);
<span class="pl-k">var</span> oauth <span class="pl-k">=</span> <span class="pl-c1">require</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>./routes/oauth<span class="pl-pds">&#39;</span></span>);
<span class="pl-k">var</span> express <span class="pl-k">=</span> <span class="pl-c1">require</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>express<span class="pl-pds">&#39;</span></span>);
<span class="pl-k">var</span> app <span class="pl-k">=</span> <span class="pl-en">express</span>();

<span class="pl-smi">app</span>.<span class="pl-c1">set</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>port<span class="pl-pds">&#39;</span></span>, <span class="pl-c1">process</span>.<span class="pl-smi">env</span>.<span class="pl-c1">PORT</span> <span class="pl-k">||</span> <span class="pl-c1">3000</span>);
<span class="pl-smi">app</span>.<span class="pl-en">use</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>/<span class="pl-pds">&#39;</span></span>, <span class="pl-smi">express</span>.<span class="pl-en">static</span>(<span class="pl-c1">__dirname</span> <span class="pl-k">+</span> <span class="pl-s"><span class="pl-pds">&#39;</span>/www<span class="pl-pds">&#39;</span></span>));
<span class="pl-smi">app</span>.<span class="pl-en">use</span>(<span class="pl-en">favicon</span>(<span class="pl-c1">__dirname</span> <span class="pl-k">+</span> <span class="pl-s"><span class="pl-pds">&#39;</span>/www/images/favicon.ico<span class="pl-pds">&#39;</span></span>));</strong></span>

<span class="pl-c">// /////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">// //</span>
<span class="pl-c">// // Use this route for proxying access token requests</span>
<span class="pl-c">// //</span>
<span class="pl-c">// /////////////////////////////////////////////////////////////////////////////////</span>

<strong><span style="color: #0000ff;"><span class="pl-smi">app</span>.<span class="pl-en">use</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>/oauth<span class="pl-pds">&#39;</span></span>, oauth);
<span class="pl-k">var</span> server <span class="pl-k">=</span> <span class="pl-smi">app</span>.<span class="pl-en">listen</span>(<span class="pl-smi">app</span>.<span class="pl-c1">get</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>port<span class="pl-pds">&#39;</span></span>), <span class="pl-k">function</span>() {
    <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Server listening on port <span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> <span class="pl-smi">server</span>.<span class="pl-en">address</span>().<span class="pl-c1">port</span>);
});</span></strong></pre>
</li>
<li>同様に Adobe Brackets で&#0160;routes フォルダ直下の&#0160;oauth.js&#0160;を開いて、コード部分のコメント記号 <strong>//</strong> を削除します。<br />
<pre><span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">//</span>
<span class="pl-c">// Obtaining our Token </span>
<span class="pl-c">//</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>

<span style="color: #0000ff;"><strong><span class="pl-k">var</span> express <span class="pl-k">=</span> <span class="pl-c1">require</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>express<span class="pl-pds">&#39;</span></span>);
<span class="pl-k">var</span> request <span class="pl-k">=</span> <span class="pl-c1">require</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>request<span class="pl-pds">&#39;</span></span>);
<span class="pl-k">var</span> router <span class="pl-k">=</span> <span class="pl-smi">express</span>.<span class="pl-en">Router</span>();

<span class="pl-k">var</span> credentials <span class="pl-k">=</span> (<span class="pl-c1">require</span> (<span class="pl-s"><span class="pl-pds">&#39;</span>fs<span class="pl-pds">&#39;</span></span>).<span class="pl-en">existsSync</span> (<span class="pl-c1">__dirname</span> <span class="pl-k">+</span> <span class="pl-s"><span class="pl-pds">&#39;</span>/../credentials.js<span class="pl-pds">&#39;</span></span>) <span class="pl-k">?</span>
    <span class="pl-c1">require</span> (<span class="pl-c1">__dirname</span> <span class="pl-k">+</span> <span class="pl-s"><span class="pl-pds">&#39;</span>/../credentials<span class="pl-pds">&#39;</span></span>)
    <span class="pl-k">:</span> (<span class="pl-en">console</span>.<span class="pl-c1">log</span> (<span class="pl-s"><span class="pl-pds">&#39;</span>No credentials.js file present, assuming using FORGE_CLIENT_ID &amp; FORGE_CLIENT_SECRET system variables.<span class="pl-pds">&#39;</span></span>), 
    <span class="pl-c1">require</span> (<span class="pl-c1">__dirname</span> <span class="pl-k">+</span> <span class="pl-s"><span class="pl-pds">&#39;</span>/../credentials_<span class="pl-pds">&#39;</span></span>))) ;

<span class="pl-smi">router</span>.<span class="pl-c1">get</span> (<span class="pl-s"><span class="pl-pds">&#39;</span>/token<span class="pl-pds">&#39;</span></span>, <span class="pl-k">function</span> (<span class="pl-smi">req</span>, <span class="pl-smi">res</span>) {
    <span class="pl-smi">request</span>.<span class="pl-en">post</span> (
        <span class="pl-smi">credentials</span>.<span class="pl-smi">Authentication</span>,
        { form<span class="pl-k">:</span> <span class="pl-smi">credentials</span>.<span class="pl-smi">credentials</span> },
        <span class="pl-k">function</span> (<span class="pl-smi">error</span>, <span class="pl-smi">response</span>, <span class="pl-smi">body</span>) {
            <span class="pl-k">if</span> ( <span class="pl-k">!</span>error <span class="pl-k">&amp;&amp;</span> <span class="pl-smi">response</span>.<span class="pl-smi">statusCode</span> <span class="pl-k">==</span> <span class="pl-c1">200</span> )
                <span class="pl-smi">res</span>.<span class="pl-c1">send</span> (body) ;
        }) ;
}) ;

<span class="pl-c1">module</span>.<span class="pl-smi">exports</span> <span class="pl-k">=</span> router;</strong></span></pre>
</li>
<li><span style="color: #111111;"><span style="color: #111111;">更に&#0160;www フォルダ下の js フォルダ内から&#0160;index.js ファイルを開いて、コード部のコメント記号&#0160;<strong>//</strong> を削除し、&lt;YOUR_URN_ID&gt; 箇所に&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/09/understanding-steps-to-use-viewer-on-postman.html" rel="noopener noreferrer" target="_blank">Postman による Viewer 利用手順の理解 - 2 legged 認証</a></strong>&#0160;で紹介した手順で取得した Base64 エンコード済みの値で置き換えます。この際、&#39;urn:&lt;YOUR_URN_ID&gt;&#39; の urn: は削除しないでください。<br /></span></span>
<pre><span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">//</span>
<span class="pl-c">// Use this call to get back an object json of your token</span>
<span class="pl-c">//</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>

<span style="color: #0000ff;"><strong><span class="pl-k">var</span> tokenurl <span class="pl-k">=</span> <span class="pl-c1">window</span>.<span class="pl-c1">location</span>.<span class="pl-c1">protocol</span> <span class="pl-k">+</span> <span class="pl-s"><span class="pl-pds">&#39;</span>//<span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> <span class="pl-c1">window</span>.<span class="pl-c1">location</span>.<span class="pl-c1">host</span> <span class="pl-k">+</span> <span class="pl-s"><span class="pl-pds">&#39;</span>/api/token<span class="pl-pds">&#39;</span></span>;
<span class="pl-k">function</span> <span class="pl-en">tokenAjax</span>() {
      <span class="pl-k">return</span> <span class="pl-smi">$</span>.<span class="pl-en">ajax</span>({
          url<span class="pl-k">:</span>tokenurl,
          dataType<span class="pl-k">:</span> <span class="pl-s"><span class="pl-pds">&#39;</span>json<span class="pl-pds">&#39;</span></span>
      });
}</strong></span>

<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>
<span class="pl-c">//</span>
<span class="pl-c">// Initialize function to the Viewer inside of Async Promise</span>
<span class="pl-c">//</span>
<span class="pl-c">/////////////////////////////////////////////////////////////////////////////////</span>

<span style="color: #0000ff;"><strong><span class="pl-k">var</span> viewer;
<span class="pl-k">var</span> options <span class="pl-k">=</span> {};
<span class="pl-k">var</span> documentId <span class="pl-k">=</span> <span class="pl-s"><span class="pl-pds">&#39;</span>urn:&lt;YOUR_URN_ID&gt;<span class="pl-pds">&#39;</span></span>;
<span class="pl-k">var</span> promise <span class="pl-k">=</span> <span class="pl-en">tokenAjax</span>();

<span class="pl-smi">promise</span>.<span class="pl-en">success</span>(<span class="pl-k">function</span> (<span class="pl-smi">data</span>) {
 options <span class="pl-k">=</span> {
      env<span class="pl-k">:</span> <span class="pl-s"><span class="pl-pds">&#39;</span>AutodeskProduction<span class="pl-pds">&#39;</span></span>,
      accessToken<span class="pl-k">:</span> <span class="pl-smi">data</span>.<span class="pl-smi">access_token</span>
    };
  <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-en">Initializer</span>(options, <span class="pl-k">function</span> <span class="pl-en">onInitialized</span>(){
      <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-smi">Document</span>.<span class="pl-c1">load</span>(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);
  }); 
})
</strong></span>
<span class="pl-c">/**</span>
<span class="pl-c">* Autodesk.Viewing.Document.load() success callback.</span>
<span class="pl-c">* Proceeds with model initialization.</span>
<span class="pl-c">*/</span>
 
<span style="color: #0000ff;"><strong><span class="pl-k">function</span> <span class="pl-en">onDocumentLoadSuccess</span>(<span class="pl-smi">doc</span>) {</strong></span>

 <span class="pl-c">// A document contains references to 3D and 2D viewables.</span>
 <strong> <span style="color: #0000ff;"><span class="pl-k">var</span> viewables <span class="pl-k">=</span> <span class="pl-smi">Autodesk</span>.<span class="pl-smi">Viewing</span>.<span class="pl-smi">Document</span>.<span class="pl-en">getSubItemsWithProperties</span>(<span class="pl-smi">doc</span>.<span class="pl-en">getRootItem</span>(), {<span class="pl-s"><span class="pl-pds">&#39;</span>type<span class="pl-pds">&#39;</span></span><span class="pl-k">:</span><span class="pl-s"><span class="pl-pds">&#39;</span>geometry<span class="pl-pds">&#39;</span></span>}, <span class="pl-c1">true</span>);
  <span class="pl-k">if</span> (<span class="pl-smi">viewables</span>.<span class="pl-c1">length</span> <span class="pl-k">===</span> <span class="pl-c1">0</span>) {
      <span class="pl-en">console</span>.<span class="pl-c1">error</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Document contains no viewables.<span class="pl-pds">&#39;</span></span>);
      <span class="pl-k">return</span>;
  }</span></strong>

  <span class="pl-c">// Choose any of the avialble viewables</span>
<span style="color: #0000ff;"><strong>  <span class="pl-k">var</span> initialViewable <span class="pl-k">=</span> viewables[<span class="pl-c1">0</span>];
  <span class="pl-k">var</span> svfUrl <span class="pl-k">=</span> <span class="pl-smi">doc</span>.<span class="pl-en">getViewablePath</span>(initialViewable);
  <span class="pl-k">var</span> modelOptions <span class="pl-k">=</span> {
      sharedPropertyDbPath<span class="pl-k">:</span> <span class="pl-smi">doc</span>.<span class="pl-en">getPropertyDbPath</span>()
  };

  <span class="pl-k">var</span> viewerDiv <span class="pl-k">=</span> <span class="pl-c1">document</span>.<span class="pl-c1">getElementById</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>viewerDiv<span class="pl-pds">&#39;</span></span>);</strong></span>
  
  <span class="pl-c">///////////////USE ONLY ONE OPTION AT A TIME/////////////////////////</span>
  <span class="pl-c">/////////////////////// Headless Viewer ///////////////////////////// </span>
<span style="color: #0000ff;"><strong>  viewer <span class="pl-k">=</span> <span class="pl-k">new</span> <span class="pl-en">Autodesk.Viewing.Viewer3D</span>(viewerDiv);</strong></span>
  
  <span class="pl-c">//////////////////Viewer with Autodesk Toolbar///////////////////////</span>
  <span style="color: #0000ff;"><strong><span class="pl-c">viewer = new Autodesk.Viewing.Private.GuiViewer3D(viewerDiv);</span></strong></span>
  <span class="pl-c">//////////////////////////////////////////////////////////////////////</span>
<span style="color: #0000ff;"><span style="color: #111111;">  // <span class="pl-smi">viewer</span>.<span class="pl-c1">start</span>(svfUrl, modelOptions, onLoadModelSuccess, onLoadModelError);</span><strong>
}</strong></span>

<span class="pl-c">/**</span>
<span class="pl-c">* Autodesk.Viewing.Document.load() failuire callback.</span>
<span class="pl-c">*/</span>
<span style="color: #0000ff;"><strong><span class="pl-k">function</span> <span class="pl-en">onDocumentLoadFailure</span>(<span class="pl-smi">viewerErrorCode</span>) {
  <span class="pl-en">console</span>.<span class="pl-c1">error</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>onDocumentLoadFailure() - errorCode:<span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> viewerErrorCode);
}</strong></span>

<span class="pl-c">/**</span>
<span class="pl-c">* viewer.loadModel() success callback.</span>
<span class="pl-c">* Invoked after the model&#39;s SVF has been initially loaded.</span>
<span class="pl-c">* It may trigger before any geometry has been downloaded and displayed on-screen.</span>
<span class="pl-c">*/</span>
<span style="color: #0000ff;"><strong><span class="pl-k">function</span> <span class="pl-en">onLoadModelSuccess</span>(<span class="pl-smi">model</span>) {
  <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>onLoadModelSuccess()!<span class="pl-pds">&#39;</span></span>);
  <span class="pl-en">console</span>.<span class="pl-c1">log</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>Validate model loaded: <span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> (<span class="pl-smi">viewer</span>.<span class="pl-smi">model</span> <span class="pl-k">===</span> model));
  <span class="pl-en">console</span>.<span class="pl-c1">log</span>(model);
}</strong></span>

<span class="pl-c">/**</span>
<span class="pl-c">* viewer.loadModel() failure callback.</span>
<span class="pl-c">* Invoked when there&#39;s an error fetching the SVF file.</span>
<span class="pl-c">*/</span>
<span style="color: #0000ff;"><strong><span class="pl-k">function</span> <span class="pl-en">onLoadModelError</span>(<span class="pl-smi">viewerErrorCode</span>) {
  <span class="pl-en">console</span>.<span class="pl-c1">error</span>(<span class="pl-s"><span class="pl-pds">&#39;</span>onLoadModelError() - errorCode:<span class="pl-pds">&#39;</span></span> <span class="pl-k">+</span> viewerErrorCode);
}</strong></span></pre>
</li>
<li><strong>Node.js command prompt</strong>&#0160;上で カレント &#0160;ディレクトリが&#0160;<strong>viewer-nodejs-tutorial</strong>&#0160;フォルダであることを確認したら、<strong>npm start</strong> または<strong> node server.js</strong> と入力して Node サーバーを起動します。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2948e92970c-pi" style="display: inline;"><img alt="Run_node_server" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2948e92970c image-full img-responsive" src="/assets/image_227819.jpg" title="Run_node_server" /></a></li>
<li>Google Chrome か、他の WebGL がサポートされる Web ブラウザを起動して、URL に localhost:3000 と入力してください。指定したドキュメントが表示されるはずです。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2958fb0970c-pi" style="display: inline;"><img alt="Model_on_viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2958fb0970c image-full img-responsive" src="/assets/image_688523.jpg" title="Model_on_viewer" /></a><br /><br /></li>
</ol>
<hr />
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/07/new-forge-viewer-tutorial-part2.html" rel="noopener noreferrer" target="_blank">次回</a>&#0160;</strong>は、各種 Extension をロードさせたり、背景を変更するなどして、Viewer の状態を拡張します。</p>
<p>By Toshiaki Isezaki</p>
