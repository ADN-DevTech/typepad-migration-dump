---
layout: "post"
title: "アプリ耐障害性向上の考察"
date: "2021-07-26 00:10:32"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/07/improving-app-resilience.html "
typepad_basename: "improving-app-resilience"
typepad_status: "Publish"
---

<header class="node__header adskf__bg-gray-2">
<div class="x-content-container">
<div class="adskf__section-split">
<div class="adskf__section-aside adskf__border-left">
<div class="node__tags">
<div class="term">
<div>
<div class="name"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e110d1a0200b-pi" style="display: inline;"><img alt="Shutterstock_236451601" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e110d1a0200b image-full img-responsive" src="/assets/image_486594.jpg" title="Shutterstock_236451601" /></a></div>
</div>
</div>
</div>
</div>
</div>
</div>
</header>
<div class="node__main x-content-container">
<div class="adskf__section-split">
<div class="node__content adskf__section-group">
<div class="node__body">
<div class="field-body">
<p lang="EN-US" xml:lang="EN-US">（本記事は <a href="https://forge.autodesk.com/blog/improving-app-resilience">Improving app resilience | Autodesk Forge</a>&#0160;の意訳版です。）</p>
<p lang="EN-US" xml:lang="EN-US">パンデミックはインターネットの重要性を再認識されています。ところが、リモートワークやオンラインミーティングなどによって、インターネット トラフィックの急激に増大してしまい、インターネット接続が不安定になったり、パフォーマンスに悪影響が出る問題が顕在し始めるようになっています。</p>
<p lang="EN-US" xml:lang="EN-US">帯域幅を確保・改善するために、オンラインミーティング時にビデオをオフにしたり、一時的に WiFi 接続を止めてスマートフォンのテザリングを利用したりしなければならないことも多々ありました。</p>
<p lang="EN-US" xml:lang="EN-US">そのため、劣化や障害が発生することを想定してアプリやサービスを設計することが重要になっています。同時に、そのような事態が発生した際にどう対応するかという戦略も必要です。&#0160;</p>
<h3>主な対応領域&#0160;</h3>
<p>まずは、アプリのアーキテクチャを設計し、運用環境を計画する際に注意すべき主な機能・分野からご紹介します。</p>
<ul>
<li>アラート：何かに障害が発生した場合、アプリケーションは適切な対応チームに警告/通知する機能</li>
<li>テスト：ユーザーのワークフローを適度な頻度でテストすることで、アプリケーションが問題を自己診断する 機能</li>
<li>メトリクス：メモリ、ディスク、エラーレートなど、アプリケーションの健全性を追跡・監視するための十分なメトリクスを提供する機能</li>
<li>ログ：意味のある情報を含むコードの実行による出力する機能&#0160;</li>
</ul>
<p>自動化を進めれば進めるほど、障害の発生が問題に発展、影響が大ききなる傾向があります。 ただ、マネージドサービスのように、プラットフォームに組み込まれた機能を使って外部に依存出来る部分も存在します。 例えば、ご自身で MongoDB インスタンスをホスティングした場合には、メンテナンス、モニタリング、バックアップなどが必要になりますが、これらは、<a href="https://aws.amazon.com/documentdb/" rel="noreferrer noopener" target="_blank">AWS&#0160;DocumentDB</a>,&#0160;<a href="https://azure.microsoft.com/ja-jp/services/cosmos-db/" rel="noopener noreferrer" target="_blank">Azure CosmoDB</a> や&#0160;<a href="https://cloud.google.com/mongodb" rel="noreferrer noopener" target="_blank">Google MongoDB</a>&#0160; などのプラットフォームのサービス プロバイダに依存することも出来るわけです。&#0160;</p>
<h3>サンプル アプリ&#0160;</h3>
<p>ここでは、3D モデルを表示するシンプルな Forge アプリを考えてみます。このアプリには、認証の実行、ファイルのアップロードと SVF/SVF2 変換を処理するいくつかのコードが組み込まれています。ブラウザ上で動作する静的コンテンツ（HTML、 JavaScriptファイル）は、サーバーサイド（認証）と Forge（viewable 情報）に依存しています。&#0160;&#0160;</p>
<h3>ハイレベル アーキテクチャ&#0160;&#0160;</h3>
<p>早期の決断がアプリの将来を左右することもありますが、少なくとも時間の経過とともにアプリがどのように変化していくかを考慮することが重要です。&#0160;</p>
<p>一般的な Web アプリでは、コードと静的ページの両方を同じサーバーでホストしています。 欠点は、コードに障害が発生すると、静的ページが提供されず、サイト全体がダウンしてしまうことです。 別々のサーバーを用意すれば、静的ページは引き続きクライアントに提供され、より適切なエラーメッセージを表示したり、基本的なオフライン機能を提供したりすることが出来るので、そのような事態を防ぐことができます。 いずれにしても、ロードバランサーは、定義されたポリシー（CPU 使用率、トラフィックなど）に基づいて、必要なサーバーの数を定義したり、どのサーバーを再起動するか（エラー率、<a href="https://developer.mozilla.org/ja/docs/Glossary/Latency" rel="noopener" target="_blank">レイテンシー</a>などに基づいて）を決定します。&#0160;</p>
<p>より高速にスケールアップ/ダウン出来る最新のアーキテクチャでは、サーバーコーディングをサーバーレスで実行したり、CDN（content-deliver network）を介して静的なページを提供したりすることが考えられます。 主要なクラウド インフラ プロバイダは、そのようなソリューションを提供しています。&#0160;</p>
<ul>
<li>AWS&#0160;<a href="https://aws.amazon.com/lambda/" rel="noreferrer noopener" target="_blank">Lambda</a>&#0160;&amp;&#0160;<a href="https://aws.amazon.com/cloudfront/" rel="noreferrer noopener" target="_blank">Cloud Front</a>&#0160;</li>
<li>Azure&#0160;<a href="https://azure.microsoft.com/ja-jp/services/functions/" rel="noopener noreferrer" target="_blank">Functions</a>&#0160;&amp;&#0160;<a href="https://azure.microsoft.com/ja-jp/services/cdn/" rel="noopener noreferrer" target="_blank">CDN</a>&#0160;</li>
<li>Google&#0160;<a href="https://cloud.google.com/serverless/" rel="noreferrer noopener" target="_blank">Serverless</a>&#0160;&amp;&#0160;<a href="https://cloud.google.com/cdn/" rel="noreferrer noopener" target="_blank">Cloud CDN</a>&#0160;</li>
</ul>
<p>Forge の<a href="https://forge.autodesk.com/code-samples" rel="noopener" target="_blank">サンプル</a>は、シングルサーバ（安価）でスタートし、最終的にはマルチサーバ（コードと静的）に移行するか、あるいはサーバレスアプローチ（スケーラブル）に移行することが出来ます。これは、サーバーとコードの実装が独立して動作するように開発されている限り、可能（または容易）です。.NET や Nodejs の場合、コードはルーティングした endpoint のみを実装し、静的ページは別個に開発され、サーバーコードとは分離されています。&#0160;</p>
<h3>クライアントコードの実装&#0160;</h3>
<p>アーキテクチャの準備ができたところで、サンプルアプリの静的ページをクライアントに提供します。ブラウザ上で動作する JavaScript のコードは、サーバーのコードが動作していなくても、なんとか動作させる必要があります。 さまざまなフレームワーク（React、Angular、Vueなど）があり、それぞれが独自のリトライ機構を持っています。ここではForge Viewerに焦点を当ててみましょう。読み込みが予期せず失敗した場合は、数秒後にモデルの読み込みを数回再試行するか、この記事で説明したようにキャッシュバージョンを持つことを検討することが出来ます。: <a href="https://forge.autodesk.com/blog/disconnected-workflows" rel="noreferrer noopener" target="_blank">Disconnected workflows</a>.&#0160;</p>
<h3>サーバーコードレベルの実装&#0160;</h3>
<p>コードは、接続の問題からサービスのダウングレード/ダウンまで、使用している API プロバイダで起こりうる問題に対応出来るようにすべきです。様々な言語でそれらを実装するライブラリや戦術がいくつか存在します。&#0160;&#0160;</p>
<p>最も基本的な機能は、エラーコード 5xx（500 番台）で失敗したコールを再試行することです。4xx エラーのほとんどのケースでは、再試行の前に入力データを変更する必要があり、401 や 403 のように、新たな認証が必要になる可能性が高いものもあります。例外は 429（レートリミット）ですが、これは後述する特定のケースです。&#0160;</p>
<p>ピーク時トラフィック増大が予想さにれる場合は、キューイングシステムを導入するのも良いアイデアです。今回のサンプルアプリでは、（メモリ消費量やスループット・トラフィックのために）一度に転送するデータの最大量が決まっている場合があります。アプリはそのジョブをキューに入れて、一度に最大 x 個のファイルを処理することができます。 キューの副次的な利点として、処理が失敗した場合に再試行し、アプリを監視したり、処理が予想よりも少し時間がかかっていることをユーザに通知する方法を提供出来る点があります。&#0160;</p>
<p>いくつかの言語固有のライブラリを紹介します。:&#0160;</p>
<ul>
<li>.NET：<a href="https://dotnetfoundation.org/projects/polly" rel="noreferrer noopener" target="_blank">Polly</a> は、リトライ、ウェイト＆リトライ、サーキットブレーカー、フォールバックなどの方法を提供します。<a href="https://www.hangfire.io/" rel="noreferrer noopener" target="_blank">Hangfire</a>&#0160;のキューイングは、着信コールの管理に役立ちます。&#0160;</li>
<li>Nodejs: node-fetch や Axios を使用する典型的なプロジェクトでは、<a href="https://www.npmjs.com/package/node-fetch-retry" rel="noreferrer noopener" target="_blank">node-fetch-retry</a> や&#0160;<a href="https://www.npmjs.com/package/axios-retry" rel="noreferrer noopener" target="_blank">axios-retry</a> を使用することができます。<a href="https://github.com/bee-queue/bee-queue" rel="noreferrer noopener" target="_blank">Bee-queue</a>&#0160; はインカムコールの管理に役立ちます。&#0160;</li>
</ul>
<h3>Forge サービス&#0160;</h3>
<p>Forge に接続する場合、いくつかの特徴があります。&#0160;:&#0160;</p>
<p><strong>キューイング（Queueing）</strong>：Model Derivative ジョブ、Design Automation ジョブ、Reality Capture シーンをリクエストすると、それぞれの API がリクエストをキューに入れ、サーバーが利用可能になり次第、処理が行われます。 これには、その時点でのサービスの混雑状況やファイルの複雑さに応じて、数秒から数分かかることがありますが、キューの時間を出来る限り短縮することを目指していますが、お客様のアプリでは、リクエストの処理にかかる時間に加えて、この待ち時間も考慮する必要があります。&#0160;</p>
<p><span style="text-decoration: underline;"><strong>レートリミット（Rate-Limit）</strong></span>：Forge に限らず、すべてのサービスには endpoint ごとに異なる Rate Limit（呼び出し数制限）が設定されています。アプリケーションは 429 レスポンスを受信した場合、x 秒後に待機して再試行する必要があります（&quot;retry-after &quot; レスポンス ヘッダーに従います）。アプリは、待機中にハングアップしたりフリーズしたりしないように準備しておく必要があります。 各 Forge サービスの Rate Limit ドキュメントをご覧ください。&#0160;&#0160;</p>
<p><span style="text-decoration: underline;"><strong>再試行（Retry）</strong></span>：以前にコードが動作していて、入力データに大きな変更がなかったと仮定すると、ランダムな問題で API リクエストが失敗した可能性が考えられます。典型的なレスポンスは 5xx です。このようなケースでは、数秒後に最大で数回再試行することが妥当です。 例外として、504 エラーコードがあります。これは、サービスが時間内に応答しなかったことを意味します。例えば、ジョブの処理を開始するために POST メソッドの endpoint を呼び出し、サービスが 60 秒以内に応答せず 504 を返したが、62 秒で呼び出しを処理したというシナリオを考えてみましょう。バックエンドは、現在、アプリのジョブを処理しており、別の POST endpoint で再試行すると、別のジョブがキューに入ります。504 の後に即座に POST を呼び出して再試行するのではなく、まず GET メソッドの endpoint を呼び出してジョブがキューに入ったかどうかを確認すべきです。&#0160;</p>
<p><span style="text-decoration: underline;"><strong>Webhooks</strong></span>：Model Derivative ジョブと Design Automation の WorkItem ジョブでは、処理が成功、または失敗した際にアプリケをコールバックする機能があります。&#0160; これにより、Forge サービスがプロセスを実行している間も、ユーザがアプリとのやり取りを続けることができます。 Forge がアプリをコールバックする際には、すぐに 200 を返し、後でアクションを実行することが重要です。ここでキューイング システムが重要になります。 コールバックはサーバーに送られ、サーバーはそれをクライアントに中継する必要があります（<a href="https://ja.wikipedia.org/wiki/WebSocket" rel="noopener" target="_blank">WebSocket</a> の利用が考えられます）。</p>
<p>サンプルアプリでは、<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/developers_guide/rate-limiting/md-rate-limits/" rel="noopener" target="_blank">Model Derivative のレートリミット</a>を確認することをお勧めします。入力データが大幅に変更されていない（ユーザーエラーなど）と仮定して、<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/job-POST/" rel="noopener" target="_blank">POST Job</a> endpoint のリトライ ポリシーと、変換処理が status:failureで失敗した場合の再試行を設定するのは合理的です。 Webhook は、変換処理が完了したときに通知することが出来ます。アプリは、メッセージをキューに入れ、WebSocket（.NET SignalR、または Nodejs の socket.io）を使用して、モデルをロードするようにクライアントに通知します。&#0160;&#0160;&#0160;</p>
<h3>さらに学習するには</h3>
<ul>
<li><a href="https://cloud.google.com/architecture/scalable-and-resilient-apps" rel="noopener noreferrer" target="_blank">スケーラブルで復元性の高いアプリのためのパターン</a>&#0160;by Google&#0160;</li>
<li><a href="https://docs.microsoft.com/ja-jp/dotnet/architecture/cloud-native/resiliency" rel="noopener noreferrer" target="_blank">クラウドネイティブの回復性</a>&#0160;by Microsoft&#0160;</li>
<li><a href="https://www.autodesk.com/autodesk-university/class/Achieving-9999-uptime-tale-Observability-2019#video" rel="noreferrer noopener" target="_blank">Achieving 99.99% uptime - a tale of Observability</a>（英語）by Autodesk&#0160;</li>
<li><a href="https://www.autodesk.com/autodesk-university/class/Tips-and-Tricks-Building-and-Testing-Successful-Cloud-Applications-and-Services-2018" rel="noreferrer noopener" target="_blank">Tips and Tricks for Building and Testing Successful Cloud Applications and Services</a>（英語）by Autodesk&#0160;</li>
<li><a href="https://aws.amazon.com/quickstart/architecture/autodesk-forge/" rel="noopener noreferrer" target="_blank">AWS の Autodesk Forge</a>, by Autodesk</li>
</ul>
<p>By Toshiaki Isezaki</p>
</div>
</div>
</div>
</div>
</div>
