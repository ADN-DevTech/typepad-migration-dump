---
layout: "post"
title: "Manufacturing Data Model サンプル"
date: "2023-06-19 00:02:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/06/fusion-data-sample.html "
typepad_basename: "fusion-data-sample"
typepad_status: "Publish"
---

<p><a href="https://github.com/autodesk-platform-services/fdx-graph-explorer" rel="noopener" target="_blank">Data Explorer</a> 以外にも、Fusion Data、改め Manufacturing Data Model（MFG Data Model） の GraphQL を利用するサンプルが用意されています。<a href="https://github.com/autodesk-platform-services/aps-fusion-data-samples">GitHub - autodesk-platform-services/aps-fusion-data-samples</a> です。この Github リポジトリは、APS ポータルの Manufacturing Data Model ドキュメントにある <a href="https://aps.autodesk.com/en/docs/fusiondata/v1/code-samples/" rel="noopener" target="_blank">Code Samples</a> セクションにある内容を実装したリポジトリになっています。</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6853a2097200d-pi" style="display: inline;"><img alt="Fusion_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6853a2097200d image-full img-responsive" src="/assets/image_554867.jpg" title="Fusion_data" /></a></p>
<p>ここでは、リポジトリの内容をローカル環境で実行出来るよう、(手順が重複するので）次の内容について、セットアップのと実行の手順をご紹介しておきます。</p>
<ul>
<li><a class="adskf__sidebar-link    " href="https://aps.autodesk.com/en/docs/fusiondata/v1/code-samples/read-model" id="53ee875c-e0f6-f3e0-f503-ff8e513ddb0f" rel="noopener" target="_blank">Read Model Hierarchy of a Design</a></li>
<li><a class="adskf__sidebar-link    " href="https://aps.autodesk.com/en/docs/fusiondata/v1/code-samples/thumbnail" id="2cec0f2e-ed8f-bd62-a8f1-874e7409cc0f" rel="noopener" target="_blank">Get the Thumbnail of a Part</a></li>
</ul>
<p>なお、サンプルの実行にはサーバー実装に Node.js を、リポジトリのローカル環境へのクローンには Git コマンドを使用します。事前に Node.js と Git for Windows をインストールしてください。それぞれの入手先やインストールについては、<a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank">APS の開発環境</a> でもご紹介していますので、必要に応じてご確認ください。</p>
<hr />
<p><strong><a id="common_settings"></a>共通設定と手順：</strong></p>
<ol>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a>&#0160;の手順で開発に必要なデベロッパーキー（アプリの登録）を取得してください。 この際、Fusion Data サンプルは 3-legged OAuth で認可プロセスを実行するので、コールバック URL（Callback URL）には、<strong>http://localhost:8080/callback/oauth</strong> を指定する必要があります。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a736ee200c-pi" style="display: inline;"><img alt="Callback_url" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a736ee200c image-full img-responsive" src="/assets/image_625534.jpg" title="Callback_url" /></a></li>
<li>コマンドプロンプトを起動して、CD コマンドでリポジトリをクローンしたいフォルダに移動したら、<strong>git clone https://github.com/autodesk-platform-services/aps-fusion-data-samples</strong>&#0160;と入力して、リポジトリをローカル環境にクローンします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75182ec9f200b-pi" style="display: inline;"><img alt="Clone_repo" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75182ec9f200b image-full img-responsive" src="/assets/image_126282.jpg" title="Clone_repo" /></a></li>
<li>クローンが正常に実行されると、<strong>aps-fusion-data-samples</strong> フォルダが作成されているはずです。</li>
</ol>
<hr />
<p><strong><a id="target_data"></a>アクセス対象のデータ</strong></p>
<p>Fusion Data は Autodesk Data Model のうち、製造業向けの MFG Data Model（Manufacturing Data Model）の粒状データにアクセスします。現状、クラウド上に保存されている Fusion 360 のデザイン データと関連が対象です。実際には、Fusion Team 内のユーザ所有領域へアクセスすることになります。</p>
<p>Fusion Data のアクセスは、Data Management API で Fusion Team にアクセスするのと同様、Hub、Project、Folder といった<a href="https://adndevblog.typepad.com/technology_perspective/2017/04/cloud-storage-forge-uses.html" rel="noopener" target="_blank">データ構造</a>を把握する必要があります。ただ、Data Management API が複数のエンドポイントでデータ構造やキーとなる Id を取得していくのと異なり、GraphQL では一度にクエリ出来る利点があります。ここでは、Hub、Project 等の名前が分かれば十分です。</p>
<p>Fusion 360 のデータパネルから「Open on the Web」をクリックすると、Fusion Team が開き、キーとなる名前を把握する事が出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75182ed57200b-pi" style="display: inline;"><img alt="Names" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75182ed57200b image-full img-responsive" src="/assets/image_238479.jpg" title="Names" /></a></p>
<hr />
<p><strong>Read Model Hierarchy of a Design（デザインのモデル階層の読み取り）設定手順と実行</strong></p>
<ol>
<li>aps-fusion-data-samples フォルダ直下に <strong>1.Read the Complete Model Hierarchy of a Design</strong> フォルダがクローンされています。コマンドプロンプトで、CD コマンドを実行して 1.Read the Complete Model Hierarchy of a Design フォルダに移動後、<strong>npm install</strong> と入力して Node.js パッケージをインストールします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75182ecc8200b-pi" style="display: inline;"><img alt="Npm_install1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75182ecc8200b image-full img-responsive" src="/assets/image_825370.jpg" title="Npm_install1" /></a></li>
<li>1.Read the Complete Model Hierarchy of a Design フォルダ直下の<strong> index.js</strong> をテキスト エディタで開いて、&#39;&lt;YOUR_CLIENT_ID&gt;&#39;、&#39;&lt;YOUR_CLIENT_SECRET&gt;&#39;、&#39;&lt;YOUR_HUB_NAME&gt;&#39;、&#39;&lt;YOUR_PROJECT_NAME&gt;&#39;、&#39;&lt;YOUR_COMPONENT_NAME&gt;&#39; のシングル クォーテーション内の文字列を、<a href="#common_settings"><strong>共通設定と手順</strong></a> と <a href="#target_data"><strong>アクセス対象のデータ</strong></a> の内容から、それぞれ適切な内容に置き換えて index.js を上書き保存し ます。（下図では &#39;&lt;YOUR_CLIENT_ID&gt;&#39; と &#39;&lt;YOUR_CLIENT_SECRET&gt;&#39; をぼかしています。）<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a78120200c-pi" style="display: inline;"><img alt="Index_.js1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a78120200c image-full img-responsive" src="/assets/image_320171.jpg" title="Index_.js1" /></a></li>
<li>コマンドプロンプトの <strong>npm start</strong> と入力して Web サーバーを起動します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6853a06a4200d-pi" style="display: inline;"><img alt="Npm_start1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6853a06a4200d image-full img-responsive" src="/assets/image_282275.jpg" title="Npm_start1" /></a></li>
<li>Web ブラウザを起動後、URL 欄に <strong>localhost:8080</strong> と入力してリターンキーを押下します。</li>
<li>index,js に設定した Hub にアクセス可能な Autodesk Id でサインインします。正しくアクセスが出来ると、ブラウザ画面に「Got the access token. You can close the browser!」と表示されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751831c8a200b-pi" style="display: inline;"><img alt="Access_succeeded" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751831c8a200b image-full img-responsive" src="/assets/image_732281.jpg" title="Access_succeeded" /></a></li>
<li>index.js で指定したプロジェクト内のコンポーネント情報がクエリされて、コマンドプロンプトに Fusion 360 のコンポーネント一覧が表示されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751831cb0200b-pi" style="display: inline;"><img alt="Model_hirerarchy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751831cb0200b image-full img-responsive" src="/assets/image_809107.jpg" title="Model_hirerarchy" /></a></li>
</ol>
<p>ここで実行されるクエリは、1.Read the Complete Model Hierarchy of a Design フォルダ直下の <strong>app.js</strong> 下部にある次の GraphQL です。</p>
<div>
<blockquote>
<div>&#0160; async getModelHierarchy(hubName, projectName, componentName) {</div>
<div>&#0160; &#0160; try {</div>
<div>&#0160; &#0160; &#0160; let response = await this.sendQuery(</div>
<div><strong>&#0160; &#0160; &#0160; &#0160; `query GetModelHierarchy($hubName: String!, $projectName: String!, $componentName: String!) {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; hubs(filter:{name:$hubName}) {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; projects(filter:{name:$projectName}) {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rootFolder {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; items(filter:{name:$componentName}) {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ... on Component {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; tipVersion {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; modelOccurrences {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; componentVersion {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; }`</strong>,</div>
<div>&#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; hubName,</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; projectName,</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; componentName</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; )</div>
<div>&#0160; &#0160; &#0160; let rootComponentVersion = this.getComponentVersion(</div>
<div>&#0160; &#0160; &#0160; &#0160; response, hubName, projectName, componentName</div>
<div>&#0160; &#0160; &#0160; );</div>
<div>&#0160; &#0160; &#0160; let componentVersions = {};</div>
<div>&#0160; &#0160; &#0160; componentVersions[rootComponentVersion.id] = rootComponentVersion;</div>
<div>&#0160; &#0160; &#0160; await this.getSubComponents(componentVersions, rootComponentVersion.modelOccurrences.results);</div>
<div>&#0160; &#0160; &#0160; return {</div>
<div>&#0160; &#0160; &#0160; &#0160; rootId: rootComponentVersion.id,</div>
<div>&#0160; &#0160; &#0160; &#0160; componentVersions: componentVersions</div>
<div>&#0160; &#0160; &#0160; };</div>
<div>&#0160; &#0160; } catch (err) {</div>
<div>&#0160; &#0160; &#0160; console.log(&quot;There was an issue: &quot; + err.message)</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; }</div>
</blockquote>
</div>
<hr />
<p><strong>Get the Thumbnail of a Part（特定パーツのサムネイル画像の取得）設定手順と実行</strong></p>
<ol>
<li>aps-fusion-data-samples フォルダ直下に <strong>3.Find the Thumbnail of a specific Part</strong>&#0160;フォルダがクローンされています。コマンドプロンプトで、CD コマンドを実行して 3.Find the Thumbnail of a specific Part フォルダに移動後、<strong>npm install</strong> と入力して Node.js パッケージをインストールします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751831d8a200b-pi" style="display: inline;"><img alt="Npm_install2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751831d8a200b image-full img-responsive" src="/assets/image_559761.jpg" title="Npm_install2" /></a></li>
<li>1.Read the Complete Model Hierarchy of a Design フォルダ直下の index.js をテキスト エディタで開いて、&#39;&lt;YOUR_CLIENT_ID&gt;&#39;、&#39;&lt;YOUR_CLIENT_SECRET&gt;&#39;、&#39;&lt;YOUR_HUB_NAME&gt;&#39;、&#39;&lt;YOUR_PROJECT_NAME&gt;&#39;、&#39;&lt;YOUR_COMPONENT_NAME&gt;&#39; のシングル クォーテーション内の文字列を、<a href="#common_settings"><strong>共通設定と手順</strong></a> と <a href="#target_data"><strong>アクセス対象のデータ</strong></a> の内容から、それぞれそれぞれ適切な内容に置き換えて index.js を上書き保存し ます。（下図では &#39;&lt;YOUR_CLIENT_ID&gt;&#39; と &#39;&lt;YOUR_CLIENT_SECRET&gt;&#39; をぼかしています。）<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a78268200c-pi" style="display: inline;"><img alt="Index_.js2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a78268200c image-full img-responsive" src="/assets/image_146333.jpg" title="Index_.js2" /></a></li>
<li>コマンドプロンプトの <strong>npm start</strong> と入力して Web サーバーを起動します。</li>
<li>Web ブラウザを起動後、URL 欄に <strong>localhost:8080</strong> と入力してリターンキーを押下します。</li>
<li>index,js に設定した Hub にアクセス可能な Autodesk Id でサインインします。正しくアクセスが出来ると、ブラウザ画面に「Got the access token. You can close the browser!」と表示されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6853a07f0200d-pi" style="display: inline;"><img alt="Access_succeeded" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6853a07f0200d image-full img-responsive" src="/assets/image_669499.jpg" title="Access_succeeded" /></a></li>
<li>指定したプロジェクト内のコンポーネントのサムネイル画像がクエリされて、3.Find the Thumbnail of a specific Part フォルダ下に thumbnail.png ファイルがダウンロードされます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6853a0801200d-pi" style="display: inline;"><img alt="Get_thimbnail" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6853a0801200d image-full img-responsive" src="/assets/image_818717.jpg" title="Get_thimbnail" /></a></li>
<li>thumbnail.png を開くと、当該コンポーネントのサムネイル画像を確認することが出来ます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751831deb200b-pi" style="display: inline;"><img alt="Thumbnail" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751831deb200b image-full img-responsive" src="/assets/image_449897.jpg" title="Thumbnail" /></a></li>
</ol>
<p>ここで実行されるクエリは、3.Find the Thumbnail of a specific Part フォルダ直下の <strong>app.js</strong> 下部にある次の GraphQL です。</p>
<div>
<blockquote>
<div>&#0160; async downloadThumbnail(hubName, projectName, componentName) {</div>
<div>&#0160; &#0160; try {</div>
<div>&#0160; &#0160; &#0160; while (true) {</div>
<div>&#0160; &#0160; &#0160; &#0160; let response = await this.sendQuery(</div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; `query GetThumbnail($hubName: String!, $projectName: String!, $componentName: String!) {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; hubs(filter:{name:$hubName}) {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; projects(filter:{name:$projectName}) {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; rootFolder {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; items(filter:{name:$componentName}) {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ... on Component {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; tipVersion {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; thumbnail {</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; status</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; mediumImageUrl</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; } &#0160; &#0160; &#0160; &#0160; &#0160;</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</strong></div>
<div><strong>&#0160; &#0160; &#0160; &#0160; &#0160; }`</strong>,</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; hubName,</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; projectName,</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; componentName</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; )</div>
<div>&#0160; &#0160; &#0160; &#0160; let thumbnail = this.getComponentVersionThumbnail(</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; response, hubName, projectName, componentName</div>
<div>&#0160; &#0160; &#0160; &#0160; );</div>
<div>&#0160; &#0160; &#0160; &#0160; if (thumbnail.status === &quot;SUCCESS&quot;) {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; // If the thumbnail generation finished then we can download it</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; // from the url</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; let thumbnailPath = path.resolve(&#39;thumbnail.png&#39;);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; await this.downloadImage(thumbnail.mediumImageUrl, thumbnailPath);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; return &quot;file://&quot; + encodeURI(thumbnailPath);</div>
<div>&#0160; &#0160; &#0160; &#0160; } else {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; console.log(&quot;Extracting thumbnail … (it may take a few seconds)&quot;)</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; // Let&#39;s wait a second before checking the status of the thumbnail again</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; await new Promise(resolve =&gt; setTimeout(resolve, 1000));</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; } catch (err) {</div>
<div>&#0160; &#0160; &#0160; console.log(&quot;There was an issue: &quot; + err.message)</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; }</div>
</blockquote>
</div>
<hr />
<p>By Toshiaki Isezaki</p>
