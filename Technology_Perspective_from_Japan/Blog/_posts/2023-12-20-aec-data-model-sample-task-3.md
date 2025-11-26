---
layout: "post"
title: "AEC Data Model サンプル：窓集計表"
date: "2023-12-20 00:01:35"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/12/aec-data-model-sample-task-3.html "
typepad_basename: "aec-data-model-sample-task-3"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a5aaf8200d-pi" style="display: inline;"><img alt="Aec_data_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a5aaf8200d image-full img-responsive" src="/assets/image_488964.jpg" title="Aec_data_model" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2023/11/aec-data-model-sample-preparation.html" rel="noopener" target="_blank">AEC Data Model サンプル：事前準備</a> の内容に沿って、<a href="https://github.com/autodesk-platform-services/aps-aecdatamodel-samples/tree/main" rel="noopener" target="_blank">GitHub - autodesk-platform-services/aps-aecdatamodel-samples</a> リポジトリを Windows ローカル環境にクローン・セットアップ済で実行出来ることを前提に、<a href="https://github.com/autodesk-platform-services/aps-aecdatamodel-samples/blob/main/Schedule.md" rel="noopener" target="_blank">Window Schedule（窓集計表）</a>の内容を処理していきます。</p>
<p>集計表は通常、建設プロジェクトの窓、ドア、仕上げ、まぐさ、基礎、橋脚などの説明を提供する図や表を識別するために使用されます。このサンプルでは、BIM モデルを参照した窓集計表の作成を想定して、窓カテゴリのすべてのデザイン要素のインスタンスのパネル・グレージング、ガラス、フレーム材料、高さ、幅などのプロパティを取得します。</p>
<ul>
<li>AEC Data Model は、現時点（2023 年 12 月）で Beta 版の API になっています。今後、手順や内容が変更される可能性があります。</li>
</ul>
<hr />
<p><strong>はじめに</strong></p>
<p>AEC Data Model を使ってプロジェクト内の設計全体で使用されるデータ・プロパティ名、単位、タイプの粒状データを GraphQL 処理でクエリーして検証（取得）する作業を前提としています。</p>
<p>後述の Step 1 ～ Step 4 の手順を実行するには、<a href="https://adndevblog.typepad.com/technology_perspective/2023/11/aec-data-model-sample-preparation.html" rel="noopener" target="_blank">AEC Data Model サンプル：事前準備</a> でご紹介した手順でセットアップしたサンプルを起動して、「<a href="https://github.com/autodesk-platform-services/aps-aecdatamodel-samples/blob/main/Schedule.md" rel="noopener" target="_blank">Window Schedule Sample Workflow</a>」の&#0160;<span style="background-color: #0d6efd; color: #ffffff;">[Go to sample]</span>&#0160;をクリックしてください。</p>
<ul>
<li>ここでは、Autodesk Construction Cloud の <em>DAS Japan Project</em> プロジェクトに、次のような Revit 2024 のプロジェクト ファイルがアップロードされていると仮定しています。</li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a41f95200d-pi" style="display: inline;"><img alt="Acc_project" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a41f95200d image-full img-responsive" src="/assets/image_642573.jpg" title="Acc_project" /></a></p>
<hr />
<p><strong>Step 1：すべての Hub を取得する</strong></p>
<p>ページ右上の [Login] ボタンをクリックして、Autodesk Construction Cloud のアクセス権を持つオートデスク アカウント（Autodesk ID）でログイン（サインイン）後、<span style="background-color: #0d6efd; color: #ffffff;">[List Hubs]</span>&#0160;をクリックします。画面に Hub ID が表示されるのでデータ交換（Data Exchange）を作成したプロジェクトがある Hub ID（&quot;id&quot; の値）を書き留めておきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a517dc200b-pi" style="display: inline;"><img alt="Step_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a517dc200b image-full img-responsive" src="/assets/image_918432.jpg" title="Step_1" /></a></p>
<p>Hub 一覧には、A360 や Fusion Team の Hub に加え、他者から招待されたプロジェクトの Hub が含まれます。Autodesk Construction Cloud の Hub は、ID の先頭文字が &quot;b.&quot; で始まります。もし、判別が難しい場合には、<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html" rel="noopener" target="_blank">Autodesk Construction Cloud と APS 統合で必要なカスタム統合</a>&#0160;でカスタム統合した際に、<strong>9.</strong>&#0160;の「<strong>BIM 360 のアカウント ID</strong>」が、&quot;b.&quot; を除いた値を目安に判断をしてください。</p>
<ul>
<li>本サンプルの実行で使用している Client Id をカスタム統合していない場合には、Autodesk Construction Cloud の Hub は表示されません。ご注意ください。</li>
</ul>
<p><strong>使用した GraphQL クエリ：</strong></p>
<blockquote>
<div>
<div>query {</div>
<div>&#0160; &#0160; hubs {</div>
<div>&#0160; &#0160; &#0160; &#0160; pagination {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; cursor</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
<div>}</div>
</div>
</blockquote>
<p><strong>Step 2：すべての Project を取得する</strong></p>
<p>Step 1 で取得した Hub ID を「<strong>Step 2: List project for a hub</strong>」下のテキストボックスに入力して、Hub 配下にある全 Project を取得します。<span style="background-color: #0d6efd; color: #ffffff;">[List Projects]</span> をクリックして、Revit プロジェクト ファイルをアップロードしたプロジェクトの Project ID（&quot;id&quot; の値）を書き留めておきます。（下図では <em>DAS Japan Project</em>&#0160;プロジェクト）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a578ca200d-pi" style="display: inline;"><img alt="Step_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a578ca200d image-full img-responsive" src="/assets/image_80315.jpg" title="Step_2" /></a></p>
<ul>
<li>このクエリで返されるのは、プロジェクト管理者が「割り当てられた製品と権限」に Docs が割り当てられている場合です。また、アーカイブされたプロジェクトは返されません。</li>
</ul>
<p><strong>使用した GraphQL クエリ：</strong></p>
<blockquote>
<div>
<div>query GetProjects ($hubId: ID!) {</div>
<div>&#0160; &#0160; projects (hubId: $hubId) {</div>
<div>&#0160; &#0160; &#0160; &#0160; pagination {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;cursor</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; results {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
<div>}</div>
</div>
</blockquote>
<div>
<p><strong>Step 3：すべてのデザインを取得する</strong></p>
<p>Step 2 で取得した Project ID を「<strong>Step 3: List all designs in a project</strong>」下のテキストボックスに入力後、<span style="background-color: #0d6efd; color: #ffffff;">[List all designs]</span> をクリックして Project 配下にある全デザイン（Revit プロジェクト ファイル）を取得します。表示されたデザインから、集計表作成の対象となる Design ID を書き留めます。（下記の例では「サンプル意匠.rvt」）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a595db200b-pi" style="display: inline;"><img alt="Step_3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a595db200b image-full img-responsive" src="/assets/image_128872.jpg" title="Step_3" /></a></p>
<p><strong>使用した GraphQL クエリ：</strong></p>
<blockquote>
<div>
<div>&#0160;query GetDesignsByProject($projectId: ID!) {</div>
<div>&#0160; &#0160; &#0160;aecDesignsByProject(projectId: $projectId) {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160;pagination {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;cursor</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160;}</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160;results{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;id</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160;}</div>
<div>&#0160; &#0160; &#0160;}</div>
<div>&#0160;}</div>
</div>
</blockquote>
<p><strong>Step 4：集計表を生成する</strong></p>
<p>Step 3 で取得した Design ID を「<strong>Step 4: Extract volume all elements</strong>」直下のテキストボックスに入力、また、その下のテキストボックスに集計表作成の対象を窓インスタンスとするよう、「<strong>property.name.category==Windows and &#39;property.name.Element Context&#39;==Instance</strong>」と入力後、<span style="background-color: #0d6efd; color: #ffffff;">[Generate Schedule]</span> をクリックして Design 中にある全ての窓ファミリインスタンスの情報を取得します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a5ff20200d-pi" style="display: inline;"><img alt="Step_4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a5ff20200d image-full img-responsive" src="/assets/image_322825.jpg" title="Step_4" /></a></p>
<p><strong>使用した GraphQL クエリ：</strong></p>
<div>
<blockquote>
<div>
<div>query GetSchedule($designId: ID!, $elementsfilter: String!){</div>
<div>&#0160; &#0160; elements (designId: $designId,filter: { query: $elementsfilter</div>
<div>&#0160; &#0160; }){</div>
<div>&#0160; &#0160; &#0160; &#0160; pagination{</div>
<div>&#0160; &#0160; &#0160; &#0160; pageSize</div>
<div>&#0160; &#0160; &#0160; &#0160; cursor</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; results{</div>
<div>&#0160; &#0160; &#0160; &#0160; id</div>
<div>&#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; properties{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; value</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; displayValue</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; propertyDefinition{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; units</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; references{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; value{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; properties{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; results{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; name</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; value</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; displayValue</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; propertyDefinition{</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; units</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; }</div>
<div>}</div>
</div>
</blockquote>
</div>
<hr />
<ul>
<li>上記説明では、リストすべき項目が多数ある際に指定する <a href="https://e-words.jp/w/%E3%83%9A%E3%83%BC%E3%82%B8%E3%83%8D%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3.html" rel="noopener" target="_blank">pagination</a>（ページ指定）を空白（未指定、null）としてご案内しています。対象項目が複数ページ存在する場合には、未指定時のレスポンスの cursor 値に次ページを指定するための cursor 値が表示されるので、各 Step の「Any cursor?」と記載のあるテキストボックスに同値を入力してください。レスポンスの cursor 値が「null」となっている場合は、次ページはありません。</li>
</ul>
<p>By Toshiaki Isezaki</p>
</div>
