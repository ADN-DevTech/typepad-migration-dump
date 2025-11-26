---
layout: "post"
title: "Forge Webhooks API ローカル開発環境について"
date: "2018-12-26 00:29:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/12/local-development-environment-for-forge-webhooks-api.html "
typepad_basename: "local-development-environment-for-forge-webhooks-api"
typepad_status: "Publish"
---

<p><strong> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a88905200d-pi" style="float: right;"><img alt="Webhooks-api-blue" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a88905200d img-responsive" src="/assets/image_111848.jpg" style="margin: 0px 0px 5px 5px;" title="Webhooks-api-blue" /></a><a href="https://adndevblog.typepad.com/technology_perspective/2018/12/what-forge-webhooks-api-is.html" rel="noopener" target="_blank">Forge Webhooks API での通知の受け取りについて</a></strong> の記事で、Wibhooks API から通知を受けるコールバック URL の実装についてご紹介しました。Forge サーバーは、Webhooks API で登録（指定）されたコールバック URL を RESTful API の POST メソッドで呼び出すことになりますが、この時、コールバック URL を実装する Web サーバーが、Forge サーバーからアクセス可能なパブリックな場所にデプロイされている必要があります。</p>
<p>Forge アプリ（Web サーバー）をローカル PC で開発中の場合には、Forge サーバーがコールバック URL を見つけられないので（はずなので）、Webhook からの通知を得ることが出来ないことになります。例えば、ローカル環境でポート番号 1337 を使って Forge アプリを開発している場合、Web ブラウザの URL ボックスに <strong>http://localhost:1337</strong> と入力することで、同 PC 内で Forge アプリの動作を確認することが可能なはずです。もし、この Forge アプリに<strong> /callback</strong> でコールバック URL をルーティング実装したと仮定すると、Webhooks API でコールバック URL を登録すると、次のようになるはずです。</p>
<pre> oAuth2TwoLegged.authenticate().then(function (credentials) {

        var data = JSON.stringify(credentials);
        var token = JSON.parse(data).access_token;

        // Create Webhooks
        uri = "https://developer.api.autodesk.com/webhooks/v1/systems/derivative/events/extraction.finished/hooks";
        var payLoad =
            {
              <strong>  "callbackUrl": "<span style="background-color: #ffff00;">http://localhost:1337</span>/callback",</strong>
                "scope": {
                    "workflow": WEBHOOK_TENANT
                }
            };
        request.post({
            url: uri,
            headers: {
                'content-type': 'application/json',
                'authorization': 'Bearer ' + token,
            },
            body: JSON.stringify(payLoad)
        }, function (error, webhookres, body) {
                                                 &lt;以下省略&gt;
</pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a889a9200d-pi" style="float: right;"><img alt="Ngrok logo" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a889a9200d img-responsive" src="/assets/image_361409.jpg" style="width: 220px; margin: 0px 0px 5px 5px;" title="Ngrok logo" /></a>ただし、前述のとおり、このコールバック URL はパブリックでないため、Forge サーバーが通知を送ることが出来ません。こんな時に利用するのが、ローカルな環境を透過的にすることが出来る <strong>ngrok</strong>&nbsp;です。ngrok は、パブリックアドレスでトラフィックを受け取り、そのトラフィックをマシン上で実行されている ngrok プロセスに中継し、指定したローカルアドレスに中継します。別の言い方をするなら、ngrok がローカルな URL をパブリックにして通知を受け取り、ローカルな環境（先の例では http://localhost:1337）上の /callback へ通知の橋渡しをしてくれます。</p>
<p>ngrok を利用するには、<strong><a href="https://ngrok.com/download" rel="noopener" target="_blank">https://ngrok.com/download</a></strong> からアーカイブされている&nbsp;<strong>ngrok.exe</strong>（Windows の場合 zip ファイル）をダウンロードして、コマンドプロンプト上で <strong>ngrok &lt;プロトコル&gt; &lt;ポート番号&gt;</strong> と入力して実行するだけです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad38288c9200c-pi" style="display: inline;"><img alt="Ngrok1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad38288c9200c image-full img-responsive" src="/assets/image_755716.jpg" title="Ngrok1" /></a></p>
<p>このコマンドを実行すると、ngrook の実行中のみ、ローカル ⇔ パブリックの橋渡し（トンネル化）効果を得ることが有効になります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3c83574200b-pi" style="display: inline;"><img alt="Ngrok2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3c83574200b image-full img-responsive" src="/assets/image_600856.jpg" title="Ngrok2" /></a></p>
<p>上記の例では、ngrook セッションの間（ngrook の実行中）、ローカルな http://localhost:1337 は <strong>http://7149283e.ngrok.io</strong> または <strong>https://7149283e.ngrok.io</strong> として参照可能な状態になります。このため、ローカル Web サーバーの実装内で、次のように Wenhooks API のコールバック URL を登録すると、Forge サーバーからの通知（呼び出し）を受けることが可能になるわけです。</p>
<pre> oAuth2TwoLegged.authenticate().then(function (credentials) {

        var data = JSON.stringify(credentials);
        var token = JSON.parse(data).access_token;

        // Create Webhooks
        uri = "https://developer.api.autodesk.com/webhooks/v1/systems/derivative/events/extraction.finished/hooks";
        var payLoad =
            {
              <strong>  "callbackUrl": "<span style="background-color: #ffff00;">http://7149283e.ngrok.io</span>/callback",</strong>
                "scope": {
                    "workflow": WEBHOOK_TENANT
                }
            };
        request.post({
            url: uri,
            headers: {
                'content-type': 'application/json',
                'authorization': 'Bearer ' + token,
            },
            body: JSON.stringify(payLoad)
        }, function (error, webhookres, body) {
                                                  &lt;以下省略&gt;</pre>
<p>ngrok のセッション毎（実行毎）に異なるパブリック URL が割り当てられることにご注意ください。このため、ngrok を使ったローカル環境での開発/デバッグでは、都度、Webhook API に登録するコールバック URL を書き換える必要があります。</p>
<p>By Toshiaki Isezaki</p>
