---
layout: "post"
title: "静的 Web ページ での WebSocket テスト"
date: "2021-11-15 00:05:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/11/websocket-testing-on-static-web-page.html "
typepad_basename: "websocket-testing-on-static-web-page"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2021/11/design-automation-apiwebsocket-api-workflow.html" rel="noopener" target="_blank">Design Automation API：WebSocket API ワークフロー</a></strong> で 3-legged&#0160; OAuth と&#0160; Das.WorkitemSigner ツールを使った Design Automation API の WebSocket API についてご紹介しました。</p>
<p>3-legged&#0160; OAuth と言うと、Web サーバーでコールバック URL を実装するのが一般的と思いますが、クラウアントとの直接コミュニケーションを目指した Design Automation API の WebSocket API では、少し矛盾があるように感じてしまいます。そこで、前回のブログ記事で「<strong><a href="https://forge.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token-implicit/" rel="noopener" target="_blank">Implicit Grant</a></strong> も含め」としたのは、Web サーバー実装を伴う動的 Web ページではなく、<strong><a href="https://ja.wikipedia.org/wiki/%E9%9D%99%E7%9A%84%E3%82%A6%E3%82%A7%E3%83%96%E3%83%9A%E3%83%BC%E3%82%B8" rel="noopener" target="_blank">静的 Web ページ</a></strong>での利用を意識したためです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdefff9c5200c-pi" style="display: inline;"><img alt="Socket_comminication" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdefff9c5200c image-full img-responsive" src="/assets/image_433566.jpg" title="Socket_comminication" /></a></p>
<p>常に同じコンテンツを配信することになる静的 Web ページ、極端な例えと思いますが、クラウド ストレージでパブリック共有してしまうと、Web ページの配信が出来てしまいます。ただ、クラウド ストレージ サービスの制限で、スクリプトを含む可能性のある HTML ファイルの共有やプレビューが制限されてしまい、すぐにテストが出来ないかと思います。</p>
<p>そんな時に便利なのが、Visual Studio Code（VS Code）に用意されている<a href="https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer" rel="noopener" target="_blank"><strong> Live Server</strong> </a>エクステンションです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdefff640200c-pi" style="display: inline;"><img alt="Live_server" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdefff640200c image-full img-responsive" src="/assets/image_315857.jpg" title="Live_server" /></a></p>
<p>VS Code でフォルダを開いて静的 Web ページを選択後、マウスの右クリックで「Open with Live Server」を選択すると、ローカル コンピュータで既定のポート番号 5500 を利用したページのテストが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13053e5200b-pi" style="display: inline;"><img alt="Launch_server" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13053e5200b image-full img-responsive" src="/assets/image_73160.jpg" title="Launch_server" /></a></p>
<p>次のような HTML ページ（index.html）を用意して、Forge アプリの Callback URL を http://localhost:5500 しておけば、Das.WorkitemSigner ツールで署名した Activity と 3-legged&#0160; OAuth で、WebSocket による WorkItwm の起動と通知をテストすることが出来ます。</p>
<div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&lt;html&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &lt;head&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;title&gt;DA4A WebSocket Test&lt;/title&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;link type=&quot;text/css&quot; rel=&quot;stylesheet&quot; href=&quot;https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css&quot;&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;script type=&quot;text/javascript&quot; src=&quot;https://ajax.googleapis.com/ajax/libs/jquery/3.4.0/jquery.min.js&quot;&gt;&lt;/script&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;script type=&quot;text/javascript&quot; src=&quot;https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js&quot;&gt;&lt;/script&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;script type=&quot;text/javascript&quot; src=&quot;https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js&quot;&gt;&lt;/script&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &lt;/head&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &lt;body onload=&quot;MyStuff.onLoad()&quot;&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;script&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; var MyStuff = {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;SOCKET&quot;: &quot;&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;access_token&quot;: &quot;&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;LogIn&quot;: [&quot;Log In&quot;, &quot;Logged In&quot;],</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;onLoad&quot;: function () {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;onLoad&quot;)</span></div>
<br />
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; var url = new URL(window.location.href.replace(&#39;#&#39;, &#39;?&#39;))</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; var query_string = url.search</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; var search_params = new URLSearchParams(query_string)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; MyStuff.access_token = search_params.get(&#39;access_token&#39;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; let logInButton = document.getElementById(&quot;LogIn&quot;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; logInButton.innerText = (MyStuff.access_token) ? MyStuff.LogIn[1] : MyStuff.LogIn[0]</span></div>
<br />
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(MyStuff.access_token)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;getUserInfo&quot;: function () {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;getUserInfo&quot;)</span></div>
<br />
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; if (MyStuff.access_token === &quot;&quot;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; return</span></div>
<br />
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; fetch(&#39;https://developer.api.autodesk.com/userprofile/v1/users/@me&#39;, {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; headers: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#39;Authorization&#39;: `Bearer ${MyStuff.access_token}`</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; })</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; .then(res =&gt; res.text())</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; .then(data =&gt; {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; let json = JSON.parse(data)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; let pretty = JSON.stringify(json, null, 2)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; MyStuff.showInfo(pretty)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(data)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; })</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;logIn&quot;: function () {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;logIn&quot;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; let clientId = <span style="color: #0000ff;"><em>&lt;Client Id&gt;</em></span></span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; let scopes = &quot;code:all&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; let redirectUri = encodeURI(&quot;http://localhost:5500&quot;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; window.open(`https://developer.api.autodesk.com/authentication/v1/authorize` +</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; `?response_type=token&amp;client_id=${clientId}&amp;redirect_uri=${redirectUri}&amp;scope=${scopes}`, &quot;_self&quot;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;doit&quot;: function () {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; var msg = JSON.stringify(</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;action&quot;: &quot;post-workitem&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;data&quot;:</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;activityId&quot;: &quot;Autodesk_Japan.Hello+dev&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;signatures&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;activityId&quot;:<span style="color: #0000ff;"><em> &lt;署名済 Activity Id&gt;</em>&#0160;</span></span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;:</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + MyStuff.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; })</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; console.log(msg)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; SOCKET.send(msg)</span></div>
<br />
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; SOCKET.onmessage = function (event) {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;通知&quot; + event.data)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; $(&quot;#message&quot;).text(event.data)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<br />
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; SOCKET.onerror = function (error) {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;エラー&quot; + error.data)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; $(&quot;#message&quot;).text(error.data)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;connect&quot;: function () {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; SOCKET = new WebSocket(&#39;wss://websockets.forgedesignautomation.io&#39;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; SOCKET.onopen = function (event) {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;接続&quot; + event.data)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; $(&quot;#message&quot;).text(&quot;接続&quot;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }, &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;disconnect&quot;: function () {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; SOCKET.close()</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; SOCKET.onclose = function () {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;切断&quot;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; $(&quot;#message&quot;).text(&quot;切断&quot;)</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; } &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;/script&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;button id=&quot;LogIn&quot; class=&quot;btn btn-outline-primary ml-3 my-2&quot; onclick=&quot;MyStuff.logIn()&quot;&gt;Log In&lt;/button&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;p&gt;&lt;/p&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;button id=&quot;connect&quot; class=&quot;btn btn-outline-dark ml-3 my-2&quot; onclick=&quot;MyStuff.connect()&quot;&gt;WebSocket 接続&lt;/button&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;button id=&quot;doit&quot; class=&quot;btn btn-outline-dark ml-3 my-2&quot; onclick=&quot;MyStuff.doit()&quot;&gt;WorkItem 実行&lt;/button&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;button id=&quot;disconnect&quot; class=&quot;btn btn-outline-dark ml-3 my-2&quot; onclick=&quot;MyStuff.disconnect()&quot;&gt;WebSocket 切断&lt;/button&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;p&gt;&lt;/p&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;label id=&quot;message&quot; class=&quot;ml-3 my-2&quot; style=&quot;width:90%&quot;&gt;&lt;/label&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &lt;/div&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &lt;/body&gt;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&lt;/html&gt;</span></div>
</div>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdefff6e6200c-pi" style="display: inline;"><img alt="App_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdefff6e6200c image-full img-responsive" src="/assets/image_619466.jpg" title="App_settings" /></a></p>
<p>実際の Forge アプリは、Forge Viewer や他のサーバー実装を使った動的 Web ページを利用することが多いので、あまり利用する機会はないかもしれませんが、知っておくと便利です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13b5437200b-pi" style="display: inline;"><img alt="Local_test" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13b5437200b image-full img-responsive" src="/assets/image_723959.jpg" title="Local_test" /></a></p>
<p>By Toshiaki Isezaki</p>
