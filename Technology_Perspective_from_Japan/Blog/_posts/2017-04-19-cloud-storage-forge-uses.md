---
layout: "post"
title: "Forge が使用するクラウド ストレージ"
date: "2017-04-19 00:02:50"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/04/cloud-storage-forge-uses.html "
typepad_basename: "cloud-storage-forge-uses"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d27680ba970c-pi" style="float: right;"><img alt="Icon - data management" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d27680ba970c img-responsive" src="/assets/image_280535.jpg" style="margin: 0px 0px 5px 5px;" title="Icon - data management" /></a>Autodesk Forge はクラウド ベースのプラットフォームとして、各種 API（Web サービス API）を提供しています。Forge を利用するアプリは、一時的にせよ、永続的にせよ、デザイン データ（ファイル）をクラウド ストレージにアップロードして利用することになります。ただ、ここで使用するクラウド ストレージが具体的にどのようなものか、明確になっていないように思います。ここで、Forge で利用するストレージについて改めてご案内しておきたいと思います。</p>
<p>Forge で扱うストレージには、大きく分けて 2 通りが存在すると考えることが出来ます。</p>
<ol>
<li>エンドユーザが A360 や BIM 360 Docs、Fusion 360 などのオートデスク クラウド サービスを利用していて、Forge &#0160;アプリが、そのユーザ ストレージにアクセスする方法です。この場合、Forge アプリはエンドユーザによるアクセス許可を得る必要があるため、適切な Scope 指定とともに&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/08/oauth-authentication-scenario-on-forge.html#_3-legged" rel="noopener noreferrer" target="_blank">3-legged 認証</a>&#0160;</strong>で Access Token を得ることが必須となります。適切な Access Token を得ることが出来れば、当該ユーザのストレージ領域にアクセスすることが出来るようになります。ただし、アクセスの際には A360 のデータ構造を意識する必要があります。その構造とは、Hub、Project、Folder、Item、Version の順に構成されるストレージの論理構造を指します。</li>
<li>Forge アプリが直接 OSS 上に Bucket（バケット、＝バケツ）と呼ばれる保存場所を作成して、アプリ自身がデータを管理していく方法です。この場合、第 3 者が所有権を持つストレージ アクセスは発生しないため、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/08/oauth-authentication-scenario-on-forge.html#_2-legged" rel="noopener noreferrer" target="_blank">2-legged 認証</a>&#0160;</strong>で Access Token を取得する際に適切な Scope を指定していればアクセスが可能になります。&#0160;</li>
</ol>
<p>いずれの場合も、最終的にデータが保存される場所は、OSS（Object Storage Service）によって管理されている領域となります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb098ed625970d-pi" style="display: inline;"><img alt="OSS_structure" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb098ed625970d image-full img-responsive" src="/assets/image_62674.jpg" title="OSS_structure" /></a></p>
<p><strong>A360 ストレージ &#0160;アクセスで必要な知識</strong></p>
<p style="padding-left: 30px;">ストレージ内の論理構造は、次の図のようになっています。Forge では、Data Management API でこれらの構造にアクセスすることになります。ここでは、基本的な役割と A360 上の該当箇所を解説しておきます。通常、実際のデータ管理は、エンドユーザがこの論理構造を使っておこなうことになります。</p>
<p style="padding-left: 30px;"><strong>Hub（ハブ）：</strong></p>
<p style="padding-left: 60px;">オートデスクには A360、Fusion Team、BIM 360 Team など、複数の異なるクラウド サービスがありますが、ユーザがこれらクラウド サービスを Subscription を購入した際には、必ず1つの Hub に関連付けられます。もし、他の Hub 上にある Project への招待を受け入れた場合には、本来関連付けられた Hub 以外の Hub にも関連付けられます。すなわち、1 つのアカウントが複数の Hub を持つことが出来ます。</p>
<p style="padding-left: 60px;">A360 では、 サインイン後のアカウントイメージをクリックすることで、既定の Hub を切り替えることが出来ます。なお、無償版の A360 を利用している場合で、第 3 者からプロジェクトに招待されていなければ、Hub は「既定」の &#0160;1 つしか表示されません。&#0160;</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2768222970c-pi" style="display: inline;"><img alt="Hub" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2768222970c img-responsive" src="/assets/image_704107.jpg" title="Hub" /></a></p>
<p style="padding-left: 30px;"><strong>Project（プロジェクト）：</strong></p>
<p style="padding-left: 60px;">1 つの Hub には複数の Project を持つことが出来ます。Project を作成出来るのは Administrator 権限を持つユーザで、Hub 内に作成出来る Project 数は無償版（A360）と有償版（Fusion Team、BIM 360 Team、BIM 360 Docs など）によって異なります。</p>
<p style="padding-left: 60px;">下記は BIM 360 Team 内に作成された 3 つのプロジェクト（Admin project、Demo project、MyProject）を示す例です。</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d27681fa970c-pi" style="display: inline;"><img alt="Project" class="asset  asset-image at-xid-6a0167607c2431970b01b8d27681fa970c img-responsive" src="/assets/image_858150.jpg" style="width: 700px;" title="Project" /></a></p>
<p style="padding-left: 30px;"><strong>Item（アイテム）：</strong></p>
<p style="padding-left: 60px;">Project 直下には、デザイン ファイルなど、アップロードしたファイルが <strong>Item（アイテム）</strong> という単位で格納されます。また、Project 直下に&#0160;<strong>Folder（フォルダ）</strong>を作成して、更に、その配下に Item を持つことも出来ます。Item と Folder は、A360 を利用するエンドユーザによって任意にアップロード/作成されることになります。</p>
<p style="padding-left: 60px;">下記は BIM 360 Team 内の Project 配下に作成された 1 つの Folder と 2 つの Item を示す例です。</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8ec2ccc970b-pi" style="display: inline;"><img alt="Folder_item" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8ec2ccc970b img-responsive" src="/assets/image_125243.jpg" style="width: 700px;" title="Folder_item" /></a></p>
<p style="padding-left: 30px;"><strong>Version（バージョン）：</strong></p>
<p style="padding-left: 60px;">Item はバージョン管理の対象になります。同じ名前の Item を同じ場所（Project 直下、ないしは、Folder 直下）にアップロードすると、 Item の新しいバージョンとして認識されます。また、Fusion 360 のように、デザイン ファイルを直接編集して内容を更新した場合でも、新しいバージョンとして認識されます。Data Management API では、バージョン付けされた Item を、バージョンを指定して個々にアクセス出来るようになっています。指定された Item バージョンは、OSS 上に格納されたデザイン データと直接関連付けられています。</p>
<p style="padding-left: 60px;">下記は BIM 360 Team 内の Item に関連付けられた 3 つの Version を示す例です。Item をクリックすると最新 Version のモデルや図面が Viewer 上に表示され、上部に表示される [ｖ.3] などのボタンから、古いバージョンを最新バージョンに切り替える操作が可能です。また、同じ操作は Item の「概要」ページでもおこなうことが出来ます。</p>
<p style="padding-left: 60px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8ec2d84970b-pi" style="display: inline;"><img alt="Version" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8ec2d84970b img-responsive" src="/assets/image_622790.jpg" style="width: 700px;" title="Version" /></a></p>
<p style="padding-left: 30px;">Folder や Project にアップロードされたデザイン ファイルは、ブラウザで表示出来るように A360 が自動的に SVF ファイル変換します。Forge アプリが Model Derivative API を使って明示的に SVF ファイルに変換する必要はありません。この場合、Forge アプリが表示したい Item（Version）の <a href="https://ja.wikipedia.org/wiki/Base64" rel="noopener noreferrer" target="_blank"><strong>Base64</strong> </a>エンコード済み URN（Object ID）を取得できれば、そのまま Viewer に表示することが出来ます。</p>
<p><strong>Forge アプリ専用ストレージ アクセスで必要な知識</strong></p>
<p style="padding-left: 30px;">Forge アプリは自身で OSS 上に一意な名前の Bucket を作成して、その中にファイルをアップロードして管理していくことになります。A360 ストレージにあるような論理構造は存在しないため、バージョン管理はおこなわれません。</p>
<p style="padding-left: 30px;">なお、Bucket には Bucket ポリシーという概念があり、Transient、Temporary、Persistent タイプが用意されています。使用する Bucket ポリシーによって Bucket 内に保存したデザイン ファイルの寿命が決まります。Bucket ポリシーを含む Bucket の詳細は、過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/summary-about-bucket.html" rel="noopener noreferrer" target="_blank">Bucket に関してのサマリー</a></strong>&#0160;でご案内していますので、必要に応じてご参照ください。</p>
<p style="padding-left: 30px;">Bucket にアップロードされたデザイン ファイルは、Forge アプリが Model Derivative API を使って明示的に SVF ファイルに変換しない限り、Viewer で表示出来るようにはなりません。</p>
<p style="padding-left: 30px;">Bucket や Bucket 内のファイルは、Forge アプリが構造を表示するユーザ インタフェースを用意しない限り、エンドユーザの目に触れることはありません。</p>
<p style="padding-left: 30px;">Bucket は Bucket を作成したデベロッパの &#0160;Client ID（Consumer Key）と関連付けられて、同じ Client ID を使用するアプリからしかアクセスすることが出来ません。同じデベロッパでも、異なるアプリ登録で取得した Client ID を使ったアプリでは、Bucket 内部へのアクセスは拒否されます。</p>
<p>&#0160;By Toshiaki Isezaki</p>
