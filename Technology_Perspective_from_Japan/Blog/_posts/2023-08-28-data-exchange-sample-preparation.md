---
layout: "post"
title: "Data Exchange サンプル：事前準備"
date: "2023-08-28 00:03:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/08/data-exchange-sample-preparation.html "
typepad_basename: "data-exchange-sample-preparation"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2023/06/fusion-data-sample.html" rel="noopener" target="_blank">Fusion Data サンプル</a> と同様に、APS ポータルの Data Exchange ドキュメントに記載のある <a href="https://aps.autodesk.com/en/docs/fdxgraph/v1/code-samples/before_you_begin_code_samples/" rel="noopener" target="_blank">Code Sample</a> の内容も、GitHub リポジトリで公開されています。<a href="https://github.com/autodesk-platform-services/aps-dx-samples-nodejs" rel="noopener" target="_blank">GitHub - autodesk-platform-services/aps-dx-samples-nodejs</a>&#0160;です。</p>
<p>ここでは、リポジトリの内容を Windows ローカル環境で実行出来るよう、セットアップと次の内容の実行手順について、数回に渡ってご紹介していきます。</p>
<ol>
<li><a href="https://aps.autodesk.com/en/docs/fdxgraph/v1/code-samples/retrieve_exchange_item_info/" rel="noopener" target="_blank">Retrieve Exchange Item Information（Exchange Item 情報の取得）</a></li>
<li><a href="https://aps.autodesk.com/en/docs/fdxgraph/v1/code-samples/quantity_takeoff/" rel="noopener" target="_blank">Quantity Takeoff for Doors（ドア数量拾い）</a></li>
<li><a href="https://aps.autodesk.com/en/docs/fdxgraph/v1/code-samples/window_schedule/" rel="noopener" target="_blank">Window Schedule（窓集計表）</a></li>
</ol>
<p>なお、このサンプルの実行には、サーバー実装に Node.js を、リポジトリのローカル環境へのクローンには Git コマンドを、それぞれ使用します。事前に Node.js と Git for Windows をインストールしてください。両者の入手先やインストールについては、<a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank">APS の開発環境</a>&#0160;でもご紹介していますので、必要に応じてご確認ください。</p>
<hr />
<p><strong>前提</strong></p>
<p>このサンプルは、Revit 2024 の Data Connector で作成されたデータ交換（Data Exchange）を使って作成された内容を GraphQL でクエリするものです。以前ご紹介した<a href="https://adndevblog.typepad.com/technology_perspective/2023/07/data-exchange-with-revit-connector-to-inventor.html" rel="noopener" target="_blank"> Revit Connector を使った Inventor へのデータ交換</a> のブログ記事では、<strong>階段</strong> カテゴリの幾何データを Inventor に引き渡す例をご紹介しています。</p>
<p><a href="https://aps.autodesk.com/en/docs/fdxgraph/v1/code-samples/before_you_begin_code_samples/" rel="noopener" target="_blank">Code Sample</a> では Doors や Windows カテゴリを使用して説明がされているので、対応した処理を評価する目的で、<span style="background-color: #ffffff;">日本語版 Revit 2024 で同梱される</span>「サンプル意匠.rvt」を開いて、<strong>ドア</strong>、 <strong>床</strong>、 <strong>壁</strong>、 <strong>窓</strong> カテゴリを指定したデータ交換作成を前提にします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39e95e7200d-pi" style="display: inline;"><img alt="Create_data_exchange" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39e95e7200d image-full img-responsive" src="/assets/image_372104.jpg" title="Create_data_exchange" /></a></p>
<ul>
<li><span style="background-color: #ffff00;">本記事の公開当初、英語版 Revit 2024 を使って英語のカテゴリ名を書き出したデータ交換を使用する必要がありましたが、日本語対応の実装が想定より早く終了したため、2023年9月1日以降、日本語版 Revit 2024で作成したデータ交換に対して、日本語カテゴリ名を使った GraphQL クエリが可能になっています。<a href="https://apps.autodesk.com/RVT/ja/Detail/Index?id=827207946618909505&amp;appLang=en&amp;os=Web" rel="noopener" target="_blank">Data Connector</a> の再インストールは不要です。</span></li>
</ul>
<hr />
<p><strong>セットアップ</strong></p>
<ol>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a> の手順で、開発に必要なデベロッパーキー（アプリの登録）を取得してください。 この際、サンプルは 3-legged OAuth で認可プロセスを実行するため、コールバック URL（Callback URL）に <strong>http://localhost:8080/api/auth/callback</strong>&#0160;を指定する必要があります。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b258340f200d-pi" style="display: inline;"><img alt="Callback_url" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b258340f200d image-full img-responsive" src="/assets/image_163460.jpg" title="Callback_url" /></a></li>
<li>Autodesk Construction Cloud へ 上記 １. で取得した Client Id の登録（カスタム統合）をおこないます。カスタム統合の手順は、<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html" rel="noopener" target="_blank">Autodesk Construction Cloud と APS 統合で必要なカスタム統合</a> の記事でご案内しています。</li>
<li>コマンドプロンプトを起動して、CD コマンドでリポジトリをクローンしたいフォルダに移動したら、<strong>git clone https://github.com/autodesk-platform-services/aps-dx-samples-nodejs.git&#0160;</strong>と入力して、リポジトリをローカル環境にクローンします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cc544a200b-pi" style="display: inline;"><img alt="Clone_repo" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cc544a200b image-full img-responsive" src="/assets/image_817561.jpg" title="Clone_repo" /></a></li>
<li>クローンが正常に実行されると、<strong>aps-dx-samples-nodejs</strong> フォルダが作成されます。CD コマンドで&#0160;<strong>aps-dx-samples-nodejs</strong> フォルダに移動したら、<strong>npm install</strong> と入力して、実行に必要な Node.js パッケージをインストールします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a9c378200c-pi" style="display: inline;"><img alt="Npm_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a9c378200c image-full img-responsive" src="/assets/image_206939.jpg" title="Npm_install" /></a></li>
<li>実行に必要な Client ID、Client Secret、Callback URL を環境変数として登録します。次の <span style="background-color: #ffff00;">&lt;&lt;YOUR CLIENT ID&gt;&gt;</span> と <span style="background-color: #ffff00;">&lt;&lt;YOUR CLIENT SECRET&gt;</span>&gt; 部分を、上記、1. で取得した Client ID と Client Secret に、<span style="background-color: #ffff00;">&lt;&lt;RANDOM STRING&gt;&gt;</span> を任意の文字列（例：<em>Test</em>）それぞれ置き換えて、コマンドプロンプトに 4 行とも入力します。
<blockquote>set APS_CLIENT_ID=<span style="background-color: #ffff00;">&lt;&lt;YOUR CLIENT ID&gt;&gt;</span><br />set APS_CLIENT_SECRET=<span style="background-color: #ffff00;">&lt;&lt;YOUR CLIENT SECRET&gt;&gt;</span><br />set APS_CALLBACK_URL=<strong>http://localhost:8080/api/auth/callback</strong><br />set SERVER_SESSION_SECRET=<span style="background-color: #ffff00;">&lt;&lt;RANDOM STRING&gt;&gt;</span><span style="background-color: #ffffff;"> ←任意の文字列</span></blockquote>
</li>
<li>コマンドプロンプトに <strong>npm start</strong> と入力して、Web サーバーを起動します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cc5498200b-pi" style="display: inline;"><img alt="Npm_start" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cc5498200b image-full img-responsive" src="/assets/image_917975.jpg" title="Npm_start" /></a></li>
<li>Web ブラウザを起動後、URL 欄に <strong>localhost:8080</strong> と入力して、次のように表示されれば正常です。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a9c3bf200c-pi" style="display: inline;"><img alt="Localhost_8080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a9c3bf200c image-full img-responsive" src="/assets/image_626044.jpg" title="Localhost_8080" /></a></li>
</ol>
<hr />
<p>By Toshiaki Isezaki</p>
