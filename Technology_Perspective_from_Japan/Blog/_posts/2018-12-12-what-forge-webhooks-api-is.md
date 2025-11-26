---
layout: "post"
title: "Forge Webhooks API での通知の受け取りについて"
date: "2018-12-12 02:01:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/12/what-forge-webhooks-api-is.html "
typepad_basename: "what-forge-webhooks-api-is"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3aa17fd200d-pi" style="float: right;"><img alt="Webhooks-api-blue" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3aa17fd200d img-responsive" src="/assets/image_57109.jpg" style="margin: 0px 0px 5px 5px;" title="Webhooks-api-blue" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad380f204200c-pi" style="float: right;"></a>Autodesk University Japan など、すでにいくつかのセミナーでお話していますが、今日は WebHooks API の考え方について触れておきたいと思います。</p>
<p>Windows 上で動作するオートデスク デスクトップ製品用にアドイン（アドオン、プラグイン）アプリケーションを開発されている方は、<strong><a href="https://msdn.microsoft.com/ja-jp/library/cc430103.aspx" rel="noopener" target="_blank">Windows のメッセージ フック</a></strong> をご存知かと思います。Windows アプリケーション上でユーザがおこなった操作に対するイベントを フック、つまり、引っかけて、イベントを取得（通知を取得）、次のアクションへつなげる仕組みです。AutoCAD の場合には、ユーザのマウス操作によって動きまわるクロス ヘア カーソルについて、カーソルが動くたび通知を得て、作図領域のワールド座標を取得したり、メインウィンドウの ☒ ボタンがクリックされたときに通知を得て、AutoCAD がシャットダウンしないよう処理する際などに利用されてきました。</p>
<p>メッセージ フックは、アプリケーション上で発生したイベントを通知するのが主な役割です。この際、取得したいイベント/メッセージを登録したアプリケーションは、通知を受け取るためにコールバック関数を登録します。該当するイベントが発生した場合には、Windows システムや AutoCAD のようなホスト アプリケーションが、登録されているコールバック関数を呼び出す仕組みです。この場合、アプリケーションの実行モジュール自体に通知が送られる（アプリケーション内に定義したコールバック関数が呼び出される）ので、比較的容易にメッセージ フックを利用することが出来ます。</p>
<p>Web やクラウド開発の世界でも「フック」は一般的で、単に Webhook、Web フック、と言っても通用します。この場合、特定の Web サービスやクラウド上でユーザがおこなった操作に対するイベントを ックして（引っかけて）イベントを取得、アプリケーションへ通知して次のアクションへつなげる仕組みを提供します。</p>
<p>Forge の場合も同様です。Forge Webhooks API では、オートデスクのクラウド サービスや Forge Platform API で発生したイベントを Forge アプリケーションに通知してくれます。2018年12月現在、次のイベントが Webhooks API で通知を得ることが出来ます。</p>
<ul>
<li><strong>Data Management Webhook</strong><br />A360/Fusion 360 Team/BIM 360 Docs ストレージ上で発生したイベント（ファイルの追加、変更、削除、移動、複写、フォルダの追加、変更、削除、移動、複写）の通知</li>
<li><strong>Model Derivative Webhook</strong><br />Model Derivative API を使ったファイル変換の過程と完了の通知</li>
<li><strong>Revit Cloud Worksharing Webhook</strong><br />Revit ワークシェアリングを使ったモデルのパブリッシュと同期を通知</li>
<li><strong>Fusion Lifecycle Webhook</strong><br />Fusion Lifecycle を使ったアイテムの作成、クローン、リリース、アップデート、ロック、ロック解除を通知</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a6f822200d-pi" style="display: inline;"><img alt="Webhooks_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a6f822200d image-full img-responsive" src="/assets/image_13935.jpg" title="Webhooks_api" /></a></p>
<p>それでは、通知はどのように受ければいいのでしょう？</p>
<p>Forge の場合、Webhook を利用するのは Web アプリケーションと考えるのが自然です。Forge アプリケーションは Web サーバー実装として用意されているため、Windows メッセージ フックの通知を受信するコールバック関数の代わりに、Forge サーバーが呼び出すことが出来るコールバック URL を登録することになります。</p>
<p>Web サーバー上にコールバック URL を設定するとは、どういった事を意味するのでしょう？</p>
<p>コールバック URL の実装には、<strong>ルーティング</strong> と呼ばれる手法を用いて、外部から呼び出すことが可能な URL を作成、公開することになります。つまり、新しい endpoint を作成します。実際の実装方法は、Web サーバーがどの Web ホスティング テクノロジを利用しているかに依存します。Node.js を利用している Web サーバーでも、利用するパッケージ（ミドルウェア）によって実装方法が変わります。例えば、Node.js でよく利用されている Express パッケージを使った <strong><a href="https://expressjs.com/ja/guide/routing.html" rel="noopener" target="_blank">ルーティング</a></strong> では、下記のような実装 となります。</p>
<p>Model Derivative Webhook で変換終了の通知を得るため、https://www.myapp.co.jp という URL からアクセス可能な Web サ ーバー上に https://www.myapp.co.jp<strong>/callback</strong> という&#0160; endpoint を作成すると同時に、その endpont を Forge サーバーが認識するよう、コールバック URL を登録する必要があります。</p>
<pre>var express = require(&#39;express&#39;);
var router = express.Router();</pre>
<pre>    oAuth2TwoLegged.authenticate().then(function (credentials) {

        var data = JSON.stringify(credentials);
        var token = JSON.parse(data).access_token;

        // Create Webhooks
        uri = &quot;https://developer.api.autodesk.com/webhooks/v1/systems/derivative/events/extraction.finished/hooks&quot;;
        var payLoad =
            {
              <strong>  &quot;callbackUrl&quot;: &quot;https://www.myapp.co.jp/callback&quot;,</strong>
                &quot;scope&quot;: {
                    &quot;workflow&quot;: WEBHOOK_TENANT
                }
            };
        request.post({
            url: uri,
            headers: {
                &#39;content-type&#39;: &#39;application/json&#39;,
                &#39;authorization&#39;: &#39;Bearer &#39; + token,
            },
            body: JSON.stringify(payLoad)
        }, function (error, webhookres, body) {
            var data = JSON.stringify(webhookres);
            if (JSON.parse(data).statusCode == 201) {

                var headers = JSON.parse(data).headers;
                data = JSON.stringify(headers);
                WEBHOOK_LOCATION = JSON.parse(data).location;
                console.log(&quot;**** Webhooks for translation finish was created at &quot; + WEBHOOK_LOCATION);
 
            } else {
                console.log(&quot;Error : &quot; + JSON.parse(body).detail);
            }
            res.send(JSON.stringify(webhookres));
        });

    }, defaultHandleError);</pre>
<p>実際のコールバック実装は次のようになるはずです。</p>
<pre>router.post(&quot;<strong>/callback</strong>&quot;, function (req, res) {

    console.log(&quot;**** Webhooks callback to notify translation finish was invoked !!&quot;);
    oAuth2TwoLegged.authenticate().then(function (credentials) {

        var data = JSON.stringify(credentials);
        var token = JSON.parse(data).access_token;

        if (WEBHOOK_LOCATION != &quot;&quot;) {
            var uri = WEBHOOK_LOCATION;
            request.delete({
                url: uri,
                headers: {
                    &#39;content-type&#39;: &#39;application/json&#39;,
                    &#39;Authorization&#39;: &#39;Bearer &#39; + token,
                }
            }, function (error, res, body) {
                console.log(&quot;**** Webhooks for translation finish at &quot; + WEBHOOK_LOCATION + &quot; was deleted&quot;);
                WEBHOOK_LOCATION = &quot;&quot;;
            });
        }

    }, defaultHandleError);

});</pre>
<p>こういった部分が Forge の性格をよく表しています。デスクトップ製品のアドイン開発では出てこない考え方なので、少し難解に感じる方も多いようです。似たような機能の実装でも Forge/Web 開発の世界では、おもむきが大きく異なります。</p>
<p>機会を改めて、Webhooks のローカル開発環境でのデバッグについてご紹介したいと思います。</p>
<p>By Toshiaki Isezaki</p>
