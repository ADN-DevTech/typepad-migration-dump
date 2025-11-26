---
layout: "post"
title: "AEC Data Model サンプル：事前準備"
date: "2023-11-27 00:03:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/11/aec-data-model-sample-preparation.html "
typepad_basename: "aec-data-model-sample-preparation"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a3d46e200b-pi" style="display: inline;"><img alt="Aec_data_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a3d46e200b image-full img-responsive" src="/assets/image_757803.jpg" title="Aec_data_model" /></a></p>
<p>Public Bata になった AEC Data Model には、APS ポータルの AEC Data Model ドキュメントに <a href="https://aps.autodesk.com/en/docs/aecdatamodel-beta/v1/code-samples/" rel="noopener" target="_blank">Code Sample</a> の解説が用意されています。GitHub リポジトリは、<a href="https://github.com/autodesk-platform-services/aps-aecdatamodel-samples/tree/main" rel="noopener" target="_blank">GitHub - autodesk-platform-services/aps-aecdatamodel-samples</a>&#0160;です。</p>
<p>ここでは、リポジトリの内容を Windows ローカル環境で実行出来るよう、セットアップと次の内容の実行手順について、数回に渡ってご紹介していきます。</p>
<ol>
<li><a href="https://github.com/autodesk-platform-services/aps-aecdatamodel-samples/blob/main/DesignValidation.md" rel="noopener" target="_blank">Design Validation（デザイン検証）</a></li>
<li><a href="https://github.com/autodesk-platform-services/aps-aecdatamodel-samples/blob/main/QuantityTakeOff.md" rel="noopener" target="_blank">Quantity takeoff for Doors（ドア数量拾い）</a></li>
<li><a href="https://github.com/autodesk-platform-services/aps-aecdatamodel-samples/blob/main/Schedule.md" rel="noopener" target="_blank">Window Schedule（窓集計表）</a></li>
<li><a href="https://github.com/autodesk-platform-services/aps-aecdatamodel-samples/blob/main/ProcurementDashboard.md" rel="noopener" target="_blank">Furniture Procurement Dashboard（家具調達ダッシュボード）</a></li>
<li><a href="https://github.com/autodesk-platform-services/aps-aecdatamodel-samples/blob/main/CompareVersions.md" rel="noopener" target="_blank">Compare Designs （デザイン比較）</a></li>
</ol>
<p>なお、このサンプルのリポジトリをローカル環境へクローンするには、Git for Windows のインストールで利用可能となる Git コマンドを使用します。また、サンプルのサーバー実装には NET 6 を使用しているため、事前に .NET 6 以降をインストールする必要があります。</p>
<p>Git for Windows の入手先やインストールについては、<a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank">APS の開発環境</a>&#0160;でもご紹介していますので、必要に応じてご確認ください。</p>
<p>.NET 6 は、<a href="https://dotnet.microsoft.com/ja-jp/download/dotnet/6.0" rel="noopener" target="_blank">.NET 6.0 (Linux、macOS、Windows) をダウンロードする (microsoft.com)</a> から入手してインストールすることが出来ます。Visual Studio 2022 をお持ちの方は、「個別のコンポーネント」タブからインストールすることも可能です。</p>
<p><img alt="Vs2022_component" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39f3849200c image-full img-responsive" src="/assets/image_285445.jpg" title="Vs2022_component" /></p>
<hr />
<p><strong>前提</strong></p>
<p>AEC Data Model は、2023年11月現在 Public Beta 扱いになっています、評価・利用には、<a href="https://feedback.autodesk.com/key/AECDataModelPublicBeta" rel="noopener" target="_blank">Join the AEC Data Model Public Beta Program (autodesk.com)</a> から Beta プログラムに参加いただく必要があります。</p>
<p>[Login or Create Account] から、お手持ちの Autodesk ID でサインイン後に Agreement の同意（メールアドレスの入力後に [I Accept] ボタンをクリック）して表示される質問に回答してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39f5411200c-pi" style="display: inline;"><img alt="Beta_portal" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39f5411200c image-full img-responsive" src="/assets/image_275466.jpg" title="Beta_portal" /></a></p>
<hr />
<p><strong>セットアップ</strong></p>
<ol>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a>&#0160;の手順で、開発に必要なデベロッパーキー（アプリの登録）を取得してください。 この際、サンプルは 3-legged OAuth で認可プロセスを実行するため、コールバック URL（Callback URL）に&#0160;<strong>http://localhost:8080/api/auth/callback</strong>&#0160;を指定する必要があります。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39f53be200c-pi" style="display: inline;"><img alt="Callback_url" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39f53be200c image-full img-responsive" src="/assets/image_299931.jpg" title="Callback_url" /></a></li>
<li>Autodesk Construction Cloud へ 上記 １. で取得した Client Id の登録（カスタム統合）をおこないます。カスタム統合の手順は、<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html" rel="noopener" target="_blank">Autodesk Construction Cloud と APS 統合で必要なカスタム統合</a>&#0160;の記事でご案内しています。</li>
<li>コマンドプロンプトを起動して、CD コマンドでリポジトリをクローンしたいフォルダに移動したら、<strong>git clone https://github.com/autodesk-platform-services/aps-aecdatamodel-samples.git&#0160;</strong>と入力して、リポジトリをローカル環境にクローンします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a3b6ee200d-pi" style="display: inline;"><img alt="Clone_repo" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a3b6ee200d image-full img-responsive" src="/assets/image_212047.jpg" title="Clone_repo" /></a></li>
<li>クローンが正常に実行されると、<strong>aps-aecdatamodel-samples</strong> フォルダが作成されます。CD コマンドで&#0160;<strong>aps-aecdatamodel-samples</strong> フォルダに移動したら、<a href="https://learn.microsoft.com/ja-jp/dotnet/core/tools/dotnet-restore" rel="noopener" target="_blank"><strong>dotnet restore</strong></a> と入力して、依存関係を修復して実行に必要な NuGet パッケージをインストールします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a355df200b-pi" style="display: inline;"><img alt="Dotnet_restore" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a355df200b image-full img-responsive" src="/assets/image_86802.jpg" title="Dotnet_restore" /></a></li>
<li>実行に必要な Client ID、Client Secret、Callback URL を環境変数として登録します。次の &lt;&lt;YOUR CLIENT ID FROM DEVELOPER PORTAL&gt;&gt; と &lt;&lt;YOUR CLIENT SECRET&gt;&gt; 部分を、上記、1. で取得した Client ID と Client Secret にそれぞれ置き換えて、コマンドプロンプトに 3 行とも入力します。
<blockquote>set APS_CLIENT_ID =&lt;&lt;YOUR CLIENT ID FROM DEVELOPER PORTAL&gt;&gt;<br />set APS_CLIENT_SECRET =&lt;&lt;YOUR CLIENT SECRET&gt;&gt;<br />set APS_CALLBACK_URL=http://localhost:8080/api/auth/callback</blockquote>
</li>
<li>コマンドプロンプトに <a href="https://learn.microsoft.com/ja-jp/dotnet/core/tools/dotnet-run" rel="noopener" target="_blank"><strong>dotnet run</strong></a>&#0160;と入力して、Web サーバーを起動します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a355fc200b-pi" style="display: inline;"><img alt="Dotnet_run" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a355fc200b image-full img-responsive" src="/assets/image_987115.jpg" title="Dotnet_run" /></a></li>
<li>Web ブラウザを起動後、URL 欄に&#0160;<strong>localhost:8080</strong>&#0160;と入力して、次のように表示されれば正常です。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a3560c200b-pi" style="display: inline;"><img alt="Localhost_8080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a3560c200b image-full img-responsive" src="/assets/image_326524.jpg" title="Localhost_8080" /></a></li>
</ol>
<p>By Toshiaki Isezaki</p>
