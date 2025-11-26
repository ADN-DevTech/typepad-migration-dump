---
layout: "post"
title: "Design Automation API：WebSocket API ワークフロー"
date: "2021-11-10 00:01:48"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/11/design-automation-apiwebsocket-api-workflow.html "
typepad_basename: "design-automation-apiwebsocket-api-workflow"
typepad_status: "Publish"
---

<p>ご承知にように、Forge では Forge サーバー（Forge のクラウド リソース）とのコミュニケーションに HTTP ベースの RESTful API を多用しています。ただ、<a href="https://adndevblog.typepad.com/technology_perspective/2021/11/evolving-design-automation-api.html" rel="noopener" target="_blank"><strong>進化する Design Automation API</strong></a> の記事でご案内したとおり、今回、Design Automation API の一部の機能で、<strong><a href="https://ja.wikipedia.org/wiki/WebSocket" rel="noopener" target="_blank">WebSocket</a></strong> と呼ばれる形態のコミュニケーション手段が導入されています。</p>
<p>WebSocket は、Web サーバーとクライアント（主に Web ブラウザ）間で、<strong>直接</strong>、双方向通信を可能にするテクノロジです。接続確立後には、双方向でメッセージを送受信することが出来るようになります。</p>
<p><img alt="Socket_comminication" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788054c0c1200d image-full img-responsive" src="/assets/image_656603.jpg" title="Socket_comminication" /></p>
<p>一見すると RESTful API と同じに感じてしまいますが、クライアントからサーバーへの要求リクエストだけでなく、サーバーからクライアントへのプッシュ型通信が可能な点が大きく異なります。また、一度、確立した接続を維持することが出来るので、リアルタイム性を重視したコミュニケーションで利用される傾向があります。Forge でも、SVF2 を Forge Viewer で表示する際のロード処理や、過去にサポートしていた <a href="https://adndevblog.typepad.com/technology_perspective/2016/05/live-review-extension-on-view-and-data-api.html" rel="noopener" target="_blank">Live Review Extension</a> で WebSocket が使われています。</p>
<p>Design Automation API の WebSocket API については、Forge ポータルの Developer&#39;s Guide に&#0160;<strong><a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/websocket-api/" rel="noopener" target="_blank">WebSocket ページ</a></strong> が追加されています。ここでは、そのワークフローと例をご紹介しておきたいと思います。</p>
<p>まず、Developer&#39;s Guide でも触れられている <a href="https://www.npmjs.com/package/wscat" rel="noopener" target="_blank"><strong>wscat</strong></a> を使ったテストからご紹介します。wscat は Node.js 用に用意されたパッケージ（ミドルウェア）で、ローカル リポジトリ毎ではなく、Node.js 環境にグローバルにインストール（<em>npm install -g wscat</em>）することで、コマンド プロンプト（Windows）やターミナル（Mac）上で WebSocket を試すことが出来るようになります。（Windows への Node.js のインストールについては、<a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank"><strong>Forge の開発環境</strong></a> のブログ記事で触れています。）</p>
<p>Design Automation API の WebSocket API を試すには、コマンド プロンプトやターミナルを起動後、まず、接続ターゲットである <span style="font-size: 10pt;"><strong>wss://websockets.forgedesignautomation.io</strong> を</span>次の構文で指定します。</p>
<blockquote>
<p><span style="font-size: 10pt;">wscat -c wss://websockets.forgedesignautomation.io</span></p>
</blockquote>
<p>接続が完了したら、必要な JSON ペイロードを指定して、直接、WorkItem を起動することが出来るようになります。次の例では、テスト用に用意されている <span style="font-size: 10pt;">&quot;Autodesk.Nop+Latest&quot; Activity を指定して WorkItem を起動しています。</span></p>
<blockquote>
<p><span style="font-size: 10pt;">{ &quot;action&quot; : &quot;post-workitem&quot;, &quot;data&quot; : { &quot;activityId&quot; : &quot;Autodesk.Nop+Latest&quot;}, &quot;headers&quot;: {&quot;Authorization&quot;: &quot;Bearer <em>&lt;your access token&gt;</em>&quot;}}</span></p>
</blockquote>
<p>下記は、コマンド プロンプトで実行した様子です。特に何もしなくても、キューに入った WorkItem の終了後、（サーバーからのプッシュによって）自動的に WorkItem の終了とレポート URL を含むレスポンスが JSONで表示されるのがわかります。クラウアント側から <strong><a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a></strong> endpoint を呼び出す必要はありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880531b03200d-pi" style="display: inline;"><img alt="Wscat" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880531b03200d image-full img-responsive" src="/assets/image_441282.jpg" title="Wscat" /></a></p>
<p>wscat の処理をクライアント側の&#0160; JavaScript コード化したものが次のコードです。ここでは、あらかじめ、&quot;Autodesk_Japan.Hello+dev&quot; Activity を定義済で、便宜上、2-legged 認証を利用しています。</p>
<p><strong>&quot;Autodesk_Japan.Hello+dev&quot; Activity 定義</strong></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">{
&#0160; &#0160; &quot;id&quot;: DA4A_UQ_ID,
&#0160; &#0160; &quot;commandLine&quot;: &quot;$(engine.path)\\accoreconsole.exe /s \&quot;$(settings[script].path)\&quot;&quot;,
&#0160; &#0160; &quot;parameters&quot;: {
&#0160; &#0160; },
&#0160; &#0160; &quot;settings&quot;: {
&#0160; &#0160; &#0160; &#0160; &quot;script&quot;: {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;value&quot;: &quot;(print \&quot;Hello!\&quot;)\n&quot;
&#0160; &#0160; &#0160; &#0160; }
&#0160; &#0160; },
&#0160; &#0160; &quot;engine&quot;: DA4A_ENGINE,
&#0160; &#0160; &quot;appbundles&quot;: [],
&#0160; &#0160; &quot;description&quot;: &quot;DA4A WebSocket Test&quot;
}
</code></pre>
<p><strong>JavaScript コード例</strong></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">var CLIENT_ID = &#39;<em>&lt;your client id&gt;</em>&#39;,
&#0160; &#0160; CLIENT_SECRET = &#39;<em>&lt;your client secret&gt;</em>&#39;,
&#0160; &#0160; SOCKET;
&#0160; &#0160; $(document).on(&quot;click&quot;, &quot;[id^=&#39;doit&#39;]&quot;, function () {
&#0160; &#0160; &#0160; &#0160; var header =
&#0160; &#0160; &#0160; &#0160; {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#39;Content-Type&#39;: &#39;application/x-www-form-urlencoded&#39;
&#0160; &#0160; &#0160; &#0160; };
&#0160; &#0160; &#0160; &#0160; var payload =
&#0160; &#0160; &#0160; &#0160; {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; client_id : CLIENT_ID,
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; client_secret : CLIENT_SECRET,
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; grant_type : &#39;client_credentials&#39;,
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; scope : &#39;code:all&#39;
&#0160; &#0160; &#0160; &#0160; };
&#0160; &#0160; &#0160; &#0160; var uri = &#39;https://developer.api.autodesk.com/authentication/v1/authenticate&#39;;
&#0160; &#0160; &#0160; &#0160; $.ajax({
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; url: uri,
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; type: &#39;POST&#39;,
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; headers: header,
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; contentType: &#39;json&#39;,
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; data: payload
&#0160; &#0160; &#0160; &#0160; }).done(function (res) {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(res);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; var msg = JSON.stringify(
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;action&quot; : &quot;post-workitem&quot;,
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;data&quot; :
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;activityId&quot; : &quot;Autodesk_Japan.Hello+dev&quot;
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;:
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; { &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + JSON.parse(JSON.stringify(res)).access_token
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; });
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(msg);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; SOCKET.send(msg);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; SOCKET.onmessage = function(event) {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;通知&quot; + event.data);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; $(&quot;#message&quot;).text(event.data);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; };
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; SOCKET.onerror = function(error) {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;エラー&quot; + error.data);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; $(&quot;#message&quot;).text(error.data);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;
&#0160; &#0160; &#0160; &#0160; }).fail(function (jqXHR, textStatus, errorThrown) {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;Failed : &quot; + errorThrown);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; $(&quot;#message&quot;).text(&quot;Failed : &quot; + errorThrown);
&#0160; &#0160; &#0160; &#0160; }); &#0160; &#0160;
&#0160; &#0160; });
&#0160; &#0160; $(document).on(&quot;click&quot;, &quot;[id^=&#39;connect&#39;]&quot;, function() {
&#0160; &#0160; &#0160; &#0160; SOCKET = new WebSocket(&#39;wss://websockets.forgedesignautomation.io&#39;);
&#0160; &#0160; &#0160; &#0160; SOCKET.onopen = function(event) {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;接続&quot; + event.data);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; $(&quot;#message&quot;).text(&quot;接続&quot;);
&#0160; &#0160; &#0160; &#0160; };
&#0160; &#0160; });
&#0160; &#0160; $(document).on(&quot;click&quot;, &quot;[id^=&#39;disconnect&#39;]&quot;, function () {
&#0160; &#0160; &#0160; &#0160; SOCKET.close();
&#0160; &#0160; &#0160; &#0160; SOCKET.onclose = function() {
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;切断&quot;);
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; $(&quot;#message&quot;).text(&quot;切断&quot;);
&#0160; &#0160; &#0160; &#0160; }; &#0160;
&#0160; &#0160; });
</code></pre>
<p><strong>HTML 定義 </strong></p>
<p>このブログサイトで HTML 記述がページ記述に誤認識されてしまうので、下記 &lt;&gt; を意図的に全角にしています。ご注意ください。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs" id="snippet-4">＜html＞
＜head＞
&#0160; &#0160; ＜title＞Test＜/title＞
&#0160; &#0160; ＜meta charset=&quot;utf-8&quot;＞
&#0160; &#0160; ＜link type=&quot;text/css&quot; rel=&quot;stylesheet&quot; href=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/style.min.css&quot;＞
&#0160; &#0160; ＜link type=&quot;text/css&quot; rel=&quot;stylesheet&quot; href=&quot;https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css&quot;＞
&#0160; &#0160; ＜script type=&quot;text/javascript&quot; src=&quot;https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js&quot;＞＜/script＞
&#0160; &#0160; ＜script type=&quot;text/javascript&quot; src=&quot;https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js&quot;＞＜/script＞
&#0160; &#0160; ＜script type=&quot;text/javascript&quot; src=&quot;https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js&quot;＞＜/script＞
&#0160; &#0160; ＜script type=&quot;text/javascript&quot; src=&quot;index.js&quot;＞＜/script＞
&#0160; &#0160; ＜style＞
&#0160; &#0160; ＜/style＞
＜/head＞
＜body＞
&#0160; &#0160; ＜div style=&quot;padding:20px&quot;＞
&#0160; &#0160; &#0160; &#0160; ＜button id=&quot;connect&quot; class=&quot;btn btn-outline-dark&quot;＞WebSocket 接続＜/button＞
&#0160; &#0160; &#0160; &#0160; ＜button id=&quot;doit&quot; class=&quot;btn btn-outline-dark&quot;＞WorkItem 実行＜/button＞
&#0160; &#0160; &#0160; &#0160; ＜button id=&quot;disconnect&quot; class=&quot;btn btn-outline-dark&quot;＞WebSocket 切断＜/button＞
&#0160; &#0160; &#0160; &#0160; ＜p＞＜/p＞
&#0160; &#0160; &#0160; &#0160; ＜label id=&quot;message&quot; style=&quot;width:90%&quot;＞＜/label＞
&#0160; &#0160; ＜/div＞
＜/body＞
＜/html＞
</code></pre>
<p>上記 HTML ページを Web ブラウザで開いて左からボタンをクリックしていくと、次のように遷移します。wscat と同じく、プッシュ通信で WorkItem の終了のレスポンス JSONで表示されるのがわかります。最後の部分では、デベロッパ＾ ルール（F12 キー）のコンソールに表示したレポート URL から、WorkItem の処理レポートをダウンロードして表示しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e12d26b2200b-pi" style="display: inline;"><img alt="2-legged_socket" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e12d26b2200b image-full img-responsive" src="/assets/image_656239.jpg" title="2-legged_socket" /></a></p>
<p>クライアント側のコードのみで 2-legged の Access Token を取得するには、デベロッパキー（Client Id と Client Secret）を保持ことが必要になってしまうため、セキュリティ上好ましくありません。そこで、Design Automation API で WebSocket API を使用する際には、<strong><a href="https://forge.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token-implicit/" rel="noopener" target="_blank">Implicit Grant</a></strong> も含め、3-legged で得た Access Token を取得する方法も用意されています。</p>
<p>ただ、code:all <a href="https://adndevblog.typepad.com/technology_perspective/2019/06/scopes-on-oauth.html" rel="noopener" target="_blank">Scope</a> を持つ Access Token をクライアント側で持つ点には変わりありません。この Access Token で WebSocket を使って WorkItem を起動する際、使用する JSON ペイロードを書き換えられてしまう懸念が残ります。ここでは、<strong><a href="https://github.com/Autodesk-Forge/forge-designautomation-signer/releases" rel="noopener" target="_blank">Das.WorkitemSigner </a>ツール</strong>を使用して公開キーと秘密キーのペアを生成、使用する Activity Id にデジタル署名して WorkItem を呼び出しす方法が提供されています。</p>
<p>手順は次のとおりです。</p>
<p>①.<a href="https://github.com/Autodesk-Forge/forge-designautomation-signer/releases" rel="noopener" target="_blank">Das.WorkitemSigner </a>ツールをダウンロード</p>
<p>②.Das.WorkitemSigner ツールで公開キーと秘密キーのペアを生成</p>
<blockquote>
<p><span style="font-size: 10pt;">Das.WorkItemSigner.exe generate mykey.json<br /></span></p>
</blockquote>
<p>③.Das.WorkItemSigner ツールで公開キーを JSON ファイルにエクスポート</p>
<blockquote>
<p><span style="font-size: 10pt;">Das.WorkitemSigner.exe export mykey.json mypublickey.json</span></p>
</blockquote>
<p>④.<strong><a  _istranslated="1" class="reference external" href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/forgeapps-id-PATCH" rel="noopener" target="_blank">PATCH forgeapps id</a></strong> endpoint で公開キーをアップロード</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e12deec1200b-pi" style="display: inline;"><img alt="Patch" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e12deec1200b image-full img-responsive" src="/assets/image_160685.jpg" title="Patch" /></a></p>
<p>⑤.Das.WorkitemSigner ツールで Activity Id のデジタル署名を生成</p>
<blockquote>
<p><span style="font-size: 10pt;">Das.WorkitemSigner.exe sign mykey.json Autodesk_Japan.Hello+dev</span></p>
</blockquote>
<p>⑥.WorkItem 呼び出し時に <span style="color: #0000ff;">Signatures スコープ</span>で署名された Activity Id を使用</p>
<div>
<blockquote>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; var msg = JSON.stringify(</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;action&quot;: &quot;post-workitem&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;data&quot;:</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;activityId&quot;: &quot;Autodesk_Japan.Hello+dev&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;signatures&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;activityId&quot;: &quot;<strong>⑤ で返されるデジタル署名&quot;</strong></span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; <span style="color: #0000ff;">}</span></span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;:</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + JSON.parse(JSON.stringify(res)).access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; });</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; SOCKET.send(msg);</span></div>
</blockquote>
</div>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdefd94ee200c-pi" style="display: inline;"><img alt="Das.WorkitemSigner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdefd94ee200c image-full img-responsive" src="/assets/image_20066.jpg" title="Das.WorkitemSigner" /></a></p>
<p>なお、3-legged の Access Token を使って、Signatures スコープの指定なしで WorkItem を呼び出すと、次のエラーが返されます。</p>
<blockquote>
<p><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">{&quot;action&quot;:&quot;error&quot;,&quot;data&quot;:&quot;{\&quot;Signatures.ActivityId\&quot;:[\&quot;Argument must be specified when using 3-legged oauth token. (Parameter &#39;Signatures.ActivityId&#39;)\&quot;]}&quot;}</span></p>
</blockquote>
<p>また、WorkItem 呼び出し以外は 3-legged 認証で得た Access Token は使用出来ません。Activity や AppBundle の登録は、従来通り、2-legged 認証の Access Token&#0160; とともに RESTful API の各 endpoint 呼び出しが必要です。</p>
<p>By Toshiaki Isezaki</p>
