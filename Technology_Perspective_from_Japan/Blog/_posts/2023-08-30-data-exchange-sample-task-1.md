---
layout: "post"
title: "Data Exchange サンプル：Exchange Item 情報の取得"
date: "2023-08-30 00:12:50"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/08/data-exchange-sample-task-1.html "
typepad_basename: "data-exchange-sample-task-1"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6d2b093200b-pi" style="display: inline;"><img alt="Data_exchange" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6d2b093200b image-full img-responsive" src="/assets/image_276835.jpg" title="Data_exchange" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6d295af200b-pi" style="display: inline;"><br /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2023/08/data-exchange-sample-preparation.html" rel="noopener" target="_blank">Data Exchange サンプル：事前準備</a> の内容に沿って、<a href="https://github.com/autodesk-platform-services/aps-dx-samples-nodejs" rel="noopener" target="_blank">GitHub - autodesk-platform-services/aps-dx-samples-nodejs</a>&#0160; リポジトリが Windows ローカル環境にクローン・セットアップ済で実行出来ることを前提に、<a href="https://aps.autodesk.com/en/docs/fdxgraph/v1/code-samples/retrieve_exchange_item_info/" rel="noopener" target="_blank">Retrieve Exchange Item Information（Exchange Item 情報の取得）</a>の内容を処理していきます。なお、Data Exchange API は、現時点（2023 年 8 月）で Beta 版の API になっています。今後、手順や内容が変更される可能性があります。</p>
<hr />
<p><strong>はじめに</strong></p>
<p>Data Exchange API を使って粒状データでデータ交換する際には、Autodesk Construction Cloud に作成した Exchange Item の ID を取得する必要があります。このタスクでは、Hub、Project、Folder をナビゲートし、Exchange Item を識別するために必要な Data Exchange GraphQL API の処理に焦点を当てています。</p>
<ul>
<li>Hub、Project、Folder、Item、Version は、Autodesk Construction Cloud のドキュメント層である Autodesk Docs が持つ論理構造を識別するオブジェクト単位です。各オブジェクトは一意な ID を持ち、<a href="https://aps.autodesk.com/en/docs/data/v2/developers_guide/basics/" rel="noopener" target="_blank">Data Management API</a> を使ってアクセスの際にも使用します。ただし、Hub を除いて使用される ID は Data Exchange API と Data Management API では形式が異なります。</li>
<li>後述の Step 1 ～ Step 5（5a、5b）の手順を実行するには、<a href="https://adndevblog.typepad.com/technology_perspective/2023/08/data-exchange-sample-preparation.html" rel="noopener" target="_blank">Data Exchange サンプル：事前準備</a> でご紹介した手順でセットアップしたサンプルを起動して、「Get exchange item information Sample Workflow」の <span style="background-color: #a040ff; color: #ffffff;">[Go to sample]</span> をクリックしてください。</li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a9da53200c-pi" style="display: inline;"><img alt="To_go" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a9da53200c image-full img-responsive" src="/assets/image_159786.jpg" title="To_go" /></a></p>
<hr />
<p><strong>Step 1：すべての Hub を取得する</strong></p>
<p style="padding-left: 40px;">ページ右上の [Login] ボタンをクリックして、Autodesk Construction Cloud のアクセス権を持つオートデスク アカウント（Autodesk ID）でログイン（サインイン）後、<span style="background-color: #a040ff; color: #ffffff;">[List Hubs]</span> をクリックします。画面に Hub ID が表示されるのでデータ交換（Data Exchange）を作成したプロジェクトがある Hub ID（&quot;id&quot; の値）を書き留めておきます。</p>
<p style="padding-left: 40px;">Hub 一覧には、A360 や Fusion Team の Hub に加え、他者から招待されたプロジェクトの Hub が含まれます。Autodesk Construction Cloud の Hub は、ID の先頭文字が &quot;b.&quot; で始まります。もし、判別が難しい場合には、<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html" rel="noopener" target="_blank">Autodesk Construction Cloud と APS 統合で必要なカスタム統合</a> でカスタム統合した際に、<strong>9.</strong> の「<strong>BIM 360 のアカウント ID</strong>」が、&quot;b.&quot; を除いた値を目安に判断をしてください。</p>
<ul>
<li>本サンプルの実行で使用している Client Id をカスタム統合していない場合には、Autodesk Construction Cloud の Hub は表示されません。ご注意ください。</li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cc6e84200b-pi" style="display: inline;"><img alt="Step_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cc6e84200b image-full img-responsive" src="/assets/image_631719.jpg" title="Step_1" /></a></p>
<p style="padding-left: 40px;"><strong>使用した GraphQL クエリ：</strong></p>
<div>
<blockquote>
<div>&#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; hubs {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; }</div>
</blockquote>
</div>
<p><strong>Step 2：すべての Project を取得する</strong></p>
<p style="padding-left: 40px;">Step 1 で の Hub ID を「<strong>Step 2: List project for a hub</strong>」下のテキストボックスに入力して、Hub 配下にある全 Project を取得します。<span style="background-color: #a040ff; color: #ffffff;">[List Projects]</span> をクリックして、Revit から Data Connector でデータ交換（Data Exchange）を作成したプロジェクト フォルダがある Project ID（&quot;id&quot; の値）を書き留めておきます。（下図では <em>Toshiaki-ACC-Project-0</em> プロジェクト）</p>
<ul>
<li>このクエリで返されるのは、プロジェクト管理者が「割り当てられた製品と権限」に Docs が割り当てられている場合です。また、アーカイブされたプロジェクトは返されません。</li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b2585063200d-pi" style="display: inline;"><img alt="Step_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b2585063200d image-full img-responsive" src="/assets/image_97333.jpg" title="Step_2" /></a></p>
<p style="padding-left: 40px;"><strong>使用した GraphQL クエリ：</strong></p>
<div>
<blockquote>
<div>&#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; projects (hubId: &quot;${hubId}&quot;) {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; }</div>
</blockquote>
</div>
<p><strong>Step 3：Project フォルダ（Folder）一覧を取得する</strong></p>
<p style="padding-left: 40px;">Step 2 で 書き留めた Project ID を「<strong>Step 3: List Project Folders</strong>」下のテキストボックスに入力して、Project 直下の Folder を取得します。<span style="background-color: #a040ff; color: #ffffff;">[List Project Folders ]</span> をクリックして、Revit から Data Connector でデータ交換（Data Exchange）を作成したプロジェクト フォルダ（下図では <em>Test</em> フォルダ）の ID（&quot;id&quot; の値）を書き留めます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751af9ff0200c-pi" style="display: inline;"><img alt="Step3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751af9ff0200c image-full img-responsive" src="/assets/image_492048.jpg" title="Step3" /></a></p>
<p style="padding-left: 40px;"><strong>使用した GraphQL クエリ：</strong></p>
<div>
<blockquote>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; project(projectId: &quot;${projectId}&quot;) {</div>
<div>&#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; folders {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; __typename</div>
<div>&#0160; &#0160;</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; folders {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; __typename</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; exchanges {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; __typename</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
</blockquote>
</div>
<p style="padding-left: 40px;">注：このリクエストのレスポンスには、Project 内の Folder&#0160; と Exchange が含まれています。</p>
<p><strong>Step 4：Folder 内のコンテンツ一覧を取得する</strong></p>
<p style="padding-left: 40px;">Step 3 で 書き留めた Folder ID を「<strong>Step 4: List Folder content</strong>」下のテキストボックスに入力して、<span style="background-color: #a040ff; color: #ffffff;">[List Folder Content]</span> をクリックし、表示されるレスポンスからデータ交換（Data Exchange）の&#0160; Exchange ID（&quot;id&quot; の値）と fileUrn の値を書き留めます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751b029e0200c-pi" style="display: inline;"><img alt="Step4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751b029e0200c image-full img-responsive" src="/assets/image_914846.jpg" title="Step4" /></a></p>
<p style="padding-left: 40px;"><strong>使用した GraphQL クエリ：</strong></p>
<div>
<blockquote>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; folder(folderId: &quot;${folderId}&quot;) {</div>
<div>&#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; folders {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; __typename</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; exchanges {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; alternativeRepresentations {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; fileUrn</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; fileVersionUrn</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; __typename</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
</blockquote>
</div>
<p><strong>Step 5a：Exchange ID から Exchange 情報を取得する</strong></p>
<p style="padding-left: 40px;">Step 4 で取得した Exchange ID を「<strong>Step 5a: Get Exchange info by ID</strong>」下のテキストボックスに入力して、Data Exchange のプロパティを取得します。<span style="background-color: #a040ff; color: #ffffff;">[Get Exchange Information]</span> をクリックしてください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751b02867200c-pi" style="display: inline;"><img alt="Step5a" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751b02867200c image-full img-responsive" src="/assets/image_787603.jpg" title="Step5a" /></a></p>
<p style="padding-left: 40px;"><strong>使用した GraphQL クエリ：</strong></p>
<div>
<blockquote>
<div>&#0160; {</div>
<div>&#0160; &#0160; exchange(exchangeId: &quot;${exchangeId}&quot;) {</div>
<div>&#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; version {</div>
<div>&#0160; &#0160; &#0160; &#0160; versionNumber</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; alternativeRepresentations {</div>
<div>&#0160; &#0160; &#0160; &#0160; fileUrn</div>
<div>&#0160; &#0160; &#0160; &#0160; fileVersionUrn</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; lineage {</div>
<div>&#0160; &#0160; &#0160; &#0160; versions {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; versionNumber</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; createdOn</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; tipVersion {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; versionNumber</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; propertyDefinitions {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; specification</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; }</div>
</blockquote>
</div>
<p><strong>Step 5b：Exchange FileUrn から Exchange 情報を取得する</strong></p>
<p style="padding-left: 40px;">データ交換（Data Exchange）の Exchange 情報は、Exchange FileUrn からも取得することが出来ます。手順 4 で取得した fileUrn の値を「<strong>Step 5b:&#0160; Get Exchange info by exchangeFileUrn</strong>」下のテキストボックス入力して、<span style="background-color: #a040ff; color: #ffffff;">[Get Exchange Information]</span>&#0160;をクリックし、Exchange のプロパティを一覧表示します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6d2cc2a200b-pi" style="display: inline;"><img alt="Step5b" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6d2cc2a200b image-full img-responsive" src="/assets/image_145903.jpg" title="Step5b" /></a></p>
<p style="padding-left: 40px;"><strong>使用した GraphQL クエリ：</strong></p>
<div>
<blockquote>
<div>
<div>&#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; exchangeByFileId(exchangeFileId: &quot;${exchangeFileUrn}&quot;) {</div>
<div>&#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; version {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; versionNumber</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; lineage {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; versions {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; versionNumber</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; createdOn</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; tipVersion {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; versionNumber</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; propertyDefinitions {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; specification</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160;}</div>
<div>&#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
</div>
</blockquote>
</div>
<hr />
<p>サンプルで使用される実際の GparhQL は、<a href="https://github.com/autodesk-platform-services/aps-dx-samples-nodejs/blob/main/services/aps/dx.js" rel="noopener" target="_blank">https://github.com/autodesk-platform-services/aps-dx-samples-nodejs/blob/main/services/aps/dx.js</a> で参照することが出来ます。なお、<a href="https://aps.autodesk.com/en/docs/fdxgraph/v1/code-samples/before_you_begin_code_samples/" rel="noopener" target="_blank">Code Sample</a> の内容の公開後、一部仕様が変更されたため、Step 5a と Step 5b&#0160; に記載された GraphQL 内の<a href="https://github.com/autodesk-platform-services/aps-dx-samples-nodejs/pull/3/commits/7c9cee311c2babe783fe3feacfd6541a7df901e4" rel="noopener" target="_blank">フィールド名</a>が異なっています。（リポジトリ内の GraphQL 記述が更新されています。）</p>
<p>By Toshiaki Isezaki</p>
