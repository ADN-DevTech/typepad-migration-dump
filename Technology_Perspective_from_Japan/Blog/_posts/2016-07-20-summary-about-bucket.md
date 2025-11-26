---
layout: "post"
title: "Bucket に関してのサマリー"
date: "2016-07-20 00:04:16"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/07/summary-about-bucket.html "
typepad_basename: "summary-about-bucket"
typepad_status: "Publish"
---

<p>クラウドにアップロードした各種デザイン ファイルを Model Derivative API &#0160;を使用して変換すれば、Web ブラウザ上に用意した領域に Viewer で 3D モデルや 2D 図面を表示することが出来ます。これは、旧 View and Data API を踏襲するワークフローと同じですが、この際、クラウド上に一意な名前の Bucket（バケット）を作成して処理するのも従来と同様です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2073958970c-pi" style="display: inline;"><img alt="Role_of_bucket" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2073958970c image-full img-responsive" src="/assets/image_944415.jpg" title="Role_of_bucket" /></a></p>
<p>現在では、Data Management API を併用することで、A360 上の ユーザ領域にアクセスすることも出来ますが、ここでは、任意に作成する Bucket について、概要と要件をまとめておきたいと思います。</p>
<p><strong>Bucket のライフサイクル</strong></p>
<p style="padding-left: 30px;">Bucket には、そのライフサイクル（寿命）別に 3 つのポリシーが用意されていて、Bucket 作成時に、そのうち の1 つを指定する必要があります。注意が必要なのは、これらのポリシーが、Bucket そのものの寿命ではなく、Bucket 内にアップロードしたファイル（オブジェクト）の寿命である点です。Bucket そのものは、永続的に残り続けます。ただし、Bucket 作成時に使用した Client ID（Consumer Key）をデベロッパ ポータルの My Apps ページで削除してしまった場合は、以後、Bucket にはアクセス出来なくなります。</p>
<ul>
<li><strong>transient</strong><br />24 時間有効な Bucket です。このポリシーを適用した Bucket にオブジェクト（ファイル）をアップロードした場合、24 時間が経過すると、アップロードしたファイルは自動的に削除されます。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0920e4e2970d-pi" style="display: inline;"><img alt="Transient_bucket" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0920e4e2970d image-full img-responsive" src="/assets/image_882280.jpg" title="Transient_bucket" /></a></li>
<li><strong>temporary<br /></strong>30 日間有効な Bucket です。このポリシーを適用した Bucket にオブジェクト（ファイル）をアップロードした場合、30 日が経過すると、アップロードしたファイルは自動的に削除されます。<strong><br /></strong></li>
<li><strong>persistent</strong><br />永続的な Bucket です。この Bucket にアップロードしたファイルは、明示的に削除するまで自動的に削除されることはありません。一度アップロードして変換したドキュメントの URN（ドキュメントID）が明確な場合には、すぐにアクセスして Web ブラウザ上に表示することが出来ます。毎回、アップロードして変換処理をする必要はありません。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0920e573970d-pi" style="display: inline;"><img alt="Persistent_bucket" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0920e573970d image-full img-responsive" src="/assets/image_864381.jpg" title="Persistent_bucket" /></a></li>
</ul>
<p style="padding-left: 30px;">※ Transient、Temporary のポリシーで作成した Bucket では、ポリシーに設定された期間後にアップロードしたファイルは削除されます。ただし、厳密には、アップロードしたファイルから変換されたドキュメントは残り続けます。現時点ではオリジナル ファイルが削除された後に変換された URN（ドキュメントID）にアクセスすると、404 エラーが返るようになっています。将来、このシナリオがエラーなして実装される予定がありますので、実現後には、どのポリシーで作成した Bucket でも、一旦変換したファイルの URN が分かれば Viewer で表示出来るようになるはずです。</p>
<p><strong>Bucket の名前</strong></p>
<p style="padding-left: 30px;">Bucket は、Object Storage Service（OSS） が管理するクラウド上の API &#0160;共有領域で一意である必要があります。また、Bucket 作成時には、次の制限があります。Bucket の名前は、API ドキュメント上では Bucket Key と呼ばれています。</p>
<ul>
<li>Bucket 名は、半角数字（0 ～ 9）と半角英字で小文字（a ～ z）、と幾つかの記号（-_.）で構成された&#0160;3 文字から 128 文字以内で作成する必要があります。</li>
<li>他のデベロッパが作成した Bucket と同じ名前（Bucket Key）の Bucket は作成することが出来ません。</li>
<li>一度作成したBucket の名前（Bucket Key）を変更することは出来ません。</li>
</ul>
<p><strong>Bucket アクセス権限</strong></p>
<p style="padding-left: 30px;">Bucket を作成する際には、事前に Access Token（アクセス トークン）を取得しておく必要があります。OSS には View and Data API 時に使用していた &#0160;v1 と、Model Derivative API で使用している &#0160;v2 があり、それぞれ異なる endpoint が用意されています。OSS v2 にアクセスして Bucket を作成する場合には、Access Token 取得時に Scope（スコープ）と呼ばれるアクセス権限を指定する必要があります。</p>
<p style="padding-left: 30px;">具体的には、Bucket 作成権限&#0160;bucket:create、作成した Bucket へのアクセスに bucket:read、ファイルのアップロードと変換に data:write、変換されたドキュメントの呼び出しに&#0160;data:read を指定する必要があります。</p>
<p style="padding-left: 30px;">また、Bucket は Bucket 作成時に使用された Client ID を記録しています。Client ID とは、デベロッパ ポータルでアプリ登録をする際に取得可能な Developer Key の一部です。セキュリティ担保の目的で、Bucket には Bucket 作成時と同じ&#0160;Client ID（Developer Key）を持ったアプリ/サービスしかアクセス出来ません。</p>
<p>By Toshiaki Isezaki</p>
