---
layout: "post"
title: "APS アクセス：2-legged と 3-legged"
date: "2024-12-02 00:39:29"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/12/aps-access-2-legged-and-3-legged.html "
typepad_basename: "aps-access-2-legged-and-3-legged"
typepad_status: "Publish"
---

<p>Autodesk Platform Services（APS）でエンドポイントを呼び出す際には、言うまでもなく <a href="https://adndevblog.typepad.com/technology_perspective/2018/11/about-access-token.html" rel="noopener" target="_blank">アクセストークン（Access Token）</a> が必要となります。アクセストークンは Client ID と Client Secret を元に<a href="https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/overview/" rel="noopener" target="_blank"> Authentication API（OAuth API）</a>を使って取得します。そして、アクセストークンの取得フローには、大きく <a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" rel="noopener" target="_blank">2-legged</a>&#0160;認証と <a href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" rel="noopener" target="_blank">3-legged</a>&#0160;認証の 2 つのフローがあります。</p>
<p><strong>2-legged 認証</strong></p>
<p style="padding-left: 40px;">&quot;app-context（アプリケーション コンテキスト）&quot; とも呼ばれる認証で、 APS API を利用するアプリが、直接、オートデスクのサーバーからアクセス トークン を取得して、OSS（Object Storage Service） のAPI 共有領域に作成した <strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/summary-about-bucket.html" rel="noopener" target="_blank">Bucket</a></strong>&#0160;にアクセスする際に使用します。&#0160;旧 View and Data API で利用していた認証は、この方法によるものです。</p>
<p style="padding-left: 40px;">2-legged と呼ばれるのは、サービス プロバイダー（オートデスク）とユーザー（APS API を利用する開発者）の 2 者が登場するためです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c2eca3200c-pi" style="display: inline;"><img alt="2-legged-flow" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c2eca3200c image-full img-responsive" src="/assets/image_263432.jpg" title="2-legged-flow" /></a></p>
<p><strong>3-legged 認証</strong></p>
<p style="padding-left: 40px;">&quot;user-context（ユーザー コンテキスト）&quot; とも呼ばれる認証で、第三者がアクセス権限を持つデータやサービスに、明示的な Web インタフェースを介してアクセスする権限を付与してもらう仕組みを実装することが出来ます。</p>
<p style="padding-left: 40px;">例えば、<strong><a href="https://construction.autodesk.co.jp/" rel="noopener" target="_blank">Autodesk Construction Cloud（ACC）</a></strong> では、通常、エンドユーザーは、そのユーザーだけがアクセスを許可されたストレージ領域を持つことが出来ます。他のユーザーはそのストレージ領域にはアクセス出来ません。もし、アプリが ACC のユーザー ストレージ領域（Autodesk Docs）にあるデータにアクセスする場合には、ACC のサインイン画面を表示させて同ユーザーにユーザー名（Autodesk ID）とパスワードを入力してもらい、アプリのアクセスを許可（認可）してもらう必要があります。</p>
<p style="padding-left: 40px;">このように、3-legged 認証では、サービス プロバイダー（オートデスク）とアプリ（APS AP を利用するアプリ）の他に、データの所有権（ストレージのアクセス権）を持つエンドユーザーが登場します。これが、3-legged と呼ばれる所以です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f09651200d-pi" style="display: inline;"><img alt="3-legged-flow" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f09651200d image-full img-responsive" src="/assets/image_164730.jpg" title="3-legged-flow" /></a></p>
<p><strong>なぜ、2 つの認証フローが存在するのか？</strong></p>
<p style="padding-left: 40px;">APS に初めて、特に RESTful API に初めて触れる場合、この違いに戸惑われる方も多いようです。ここで 1 つの指標になるのが、APS へのアクセス目的です。具体的には、開発するアプリがオートデスクのクラウド製品のストレージ領域にアクセスする、また、オートデスクのクラウド製品の機能を利用する場合には 3-legged、そうでない場合には 2-legged といった区分けです。</p>
<p style="padding-left: 40px;">Data Management API の <a href="https://aps.autodesk.com/en/docs/data/v2/developers_guide/basics/" rel="noopener" target="_blank">Developer&#39;s Guide</a> の API Basics ページには、次のような記述があります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ef2e61200d-pi" style="display: inline;"><img alt="Dm_api_basics" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ef2e61200d image-full img-responsive" src="/assets/image_852326.jpg" title="Dm_api_basics" /></a></p>
<p style="padding-left: 40px;">ご注目いただきたいのは、赤い下線を引いた記述です。Forge 時代から継承しているドキュメントなので、少し製品名が古くなっていますが、現時点で新規に購入可能なものに置き換えて意訳すると、次のようになります。</p>
<ul>
<li>Fusion Team にアクセスする場合、エンドユーザーがデータにアクセスするには、APS アプリに <span style="text-decoration: underline;">3-legged 認証フローで得た</span>アクセス トークンを提供する必要があります。</li>
<li>Autodesk Docs（Autodesk Construction Cloud）にアクセスする場合、Account Admin が Autodesk Construction Cloud（ACC）で「<strong>カスタム統合</strong>」を使ってアプリを追加する必要があります。APS アプリは、<span style="text-decoration: underline;">2-legged 認証フローまたは 3-legged 認証フローのいずれかで得た</span>アクセス トークンを使用してデータにアクセスできます。</li>
</ul>
<p style="padding-left: 40px;">ここで最初にご紹介した認証の意味合いを思い出してください。APS を利用するアプリが、Fusion Team、Autodesk Construction Cloud（ドキュメント層が Autodesk Docs）に保存したデータにアクセスする際には、データ所有者にアクセス許可（認可）を得る必要があります。つまり、原則、3-legged 認証フローでアクセス トークンを取得する必要があります。</p>
<p style="padding-left: 40px;">ただ、1 つ問題が出て来ます。3-legged 認証フローで特定のユーザーにアクセス許可を得た場合、そのアクセス トークンを使用するアプリは、同ユーザーがアクセス権を持つデータ（プロジェクト、フォルダー、ファイル）にしかアクセスすることが出来ません。</p>
<p style="padding-left: 40px;">開発する APS アプリの機能にも依りますが、複数のプロジェクトへのアクセスが必要なアプリの場合、それぞれのプロジェクトにアクセス権も持つユーザーにサインインしてもらい、都度、アクセス許可を得るのはナンセンスです。強力なユーザーロールやアクセス権の設定が可能な ACC へのアクセスでは、特に問題になります。</p>
<p><strong>ユーザー権限を越えたアクセス</strong></p>
<p style="padding-left: 40px;">そこで考え出されたのが、2-legged 認証フローを使ったアクセスです。ACC へのアクセスに 2-legged 認証フローで得たアクセス トークンを使うと、Account Admin と同じく、アプリはスーパー ユーザー権限ですべてのプロジェクトにアクセス出来るようになります。これによって、プロジェクトを横断的にアクセスして、さまざまなデータから包括的なインサイト（洞察）を視覚化出来る等の利点が生まれます。</p>
<p><strong>予期しないアクセスへの懸念</strong></p>
<p style="padding-left: 40px;">すべてにアクセス出来る 2-legged 認証フローを使ったアプリには、懸念が提起されることもあります。そういった懸念を完全に払拭することは出来ないかもしれませんが、安全弁のような機能があります。それが前述で触れた「<strong>カスタム統合</strong>」の仕組みです。</p>
<p style="padding-left: 40px;">APS アプリには、<a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">APS API を利用するアプリの登録とキーの取得</a> の手順で取得した Client ID と Client Secret が必要です。ACC にアクセス、あるいは、ACC API を利用するアプリは、事前に Client ID を「カスタム統合」で ACC アカウント（ハブ）に登録しなければなりません。</p>
<p style="padding-left: 40px;">「カスタム統合」の具体的な手順は、次の記事でご案内しています。<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html" rel="noopener" target="_blank"></a>&#0160;</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2024/02/acc-new-custom-integration-ui.html" rel="noopener" target="_blank">ACC：新しいカスタム統合 UI</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/06/custom-integration-steps-for-acc-and-aps-integration.html" rel="noopener" target="_blank">Autodesk Construction Cloud と APS 統合で必要なカスタム統合</a></li>
</ul>
<p style="padding-left: 40px;">逆に言えば、「カスタム統合」していない APS アプリは、2-legged 認証フォロー、3-legged&#0160; 認証フローに関係なく、ACC にアクセスすることは出来ません。また、「カスタム統合」の処理は、Account Admin 権限のユーザーが ACC にサインインして実施することが必須です。</p>
<p style="padding-left: 40px;">また、いつでも「カスタム統合」で登録した Client ID の登録を解除することも出来ます。もちろん、解除された Client ID を持つ APS アプリは、ACC へのアクセスを拒否されます。</p>
<p><strong>2-legged 認証でのユーザー コンテキスト</strong></p>
<p style="padding-left: 40px;">スーパー ユーザーとしてACC のデータ アクセスが可能な 2-legged 認証フローですが、特定のユーザーに代わって処理をする必要がある場合も出てくる場面があります。ACC にファイルをアップロードしてバージョン登録するような場面です。</p>
<p style="padding-left: 40px;">例えば、アプリケーション コンテキストとなるアプリでは、特定のユーザーと関連づけられていないため、ユーザー インターフェース上、アップロードしたユーザーは「<strong>ACC システム</strong>」になってしまいます。<a href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="noopener" target="_blank">Upload a Files</a>&#0160;の手順で利用する <a href="https://aps.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-items-POST/" rel="noopener" target="_blank">POST projects/:project_id/items</a>&#0160;エンドポイントでは、リクエスト ヘッダーの説明が次のように記されています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860d81b22200b-pi" style="display: inline;"><img alt="X-user-id" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860d81b22200b image-full img-responsive" src="/assets/image_207263.jpg" title="X-user-id" /></a></p>
<p style="padding-left: 40px;">先と同じように、赤い矩形内の x-user-id ヘッダー パラメーターの記述を意訳すると、次のようになります。</p>
<ul>
<li>2-legged 認証フローを使ったアプリ コンテキストでは、アプリ は SaaS 統合ユーザー インタフェースで管理者が指定したすべてのユーザーにアクセスできます。ただし、x-user-id&#0160; ヘッダー パラメーターを指定することで、エンドポイント呼び出しは、指定されたユーザーの代理として動作するように制限されます。</li>
<li>3-legged 認証フロー、またはユーザー代理（<strong><code>x-user-id</code></strong> ）の 2-legged 認証フローでは、ユーザーは指定されたフォルダー（<strong><code>data.attributes.relations.parent.data.id</code></strong> ）にファイルをアップロードする権限を持っている必要があることに注意してください。</li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f1876a200d-pi" style="display: inline;"><img alt="2-legged_user" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f1876a200d img-responsive" src="/assets/image_390774.jpg" title="2-legged_user" /></a></p>
<p style="padding-left: 40px;"><strong><code>x-user-id</code></strong> で指定する値は、<a href="https://aps.autodesk.com/en/docs/profile/v1/reference/profile/oidcuserinfo/" rel="noopener" target="_blank">GET OIDC User Info</a> エンドポイントで返されるレスポンスの sub フィールドから取得することが出来ます。 <a href="https://aps.autodesk.com/en/docs/profile/v1/reference/profile/oidcuserinfo/" rel="noopener" target="_blank">GET OIDC User Info</a> エンドポイントの呼び出しには、3-legged 認証フローで取得したアクセス トークンが必要です。</p>
<p style="padding-left: 40px;">ACC ユーザーの <strong><code>x-user-id </code></strong>をリストするには、2-legged 認証フローで取得したアクセス トークンを用いて、<a href="https://aps.autodesk.com/en/docs/acc/v1/reference/http/users-GET/" rel="noopener" target="_blank">GET users</a> エンドポイント レスポンスの uid フィールドから取得することが出来ます。</p>
<p><strong>認証フローの違いによるオートデスク製品へのデータ アクセス</strong></p>
<p style="padding-left: 40px;">認証フローによる ACC と Fusion Team へのデータ アクセスの違いをまとめると、次のようになります。</p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ef4488200d-pi" style="display: inline;"><img alt="Context_summary" class="asset  asset-image at-xid-6a0167607c2431970b02e860ef4488200d img-responsive" src="/assets/image_330475.jpg" style="width: 400px;" title="Context_summary" /></a></p>
<p>ここまで、オートデスク製品へのデータ アクセスを主眼に 2-legged 認証フローと 3-legged 認証フロー差を見てきました。厳密には、どちらの認証フローで得たアクセス トークンを使用するかは、各エンドポイントが規定するものを使用する必要があります。</p>
<p>例えば、ACC に保持されている粒状データに AEC Data Model API の <a href="https://aps.autodesk.com/en/docs/aecdatamodel/v1/reference/graphqlendpoint/" rel="noopener" target="_blank">GraphQL エンドポイント</a>を使ってアクセスする場合、エンドポイントは 3-legged 認証フローのみしかサポートしていません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ef44d5200d-pi" style="display: inline;"><img alt="Aec_data_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ef44d5200d image-full img-responsive" src="/assets/image_522149.jpg" title="Aec_data_model" /></a></p>
<p>ACC API の機能をサポートするモジュール毎の API も含め、ACC といっても、すべてが 2-legged 認証フローをサポートしているわけでがありません。</p>
<p>By Toshiaki Isezaki</p>
