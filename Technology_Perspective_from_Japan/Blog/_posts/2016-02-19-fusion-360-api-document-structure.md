---
layout: "post"
title: "Fusion 360 API：ドキュメント構造"
date: "2016-02-19 03:05:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/02/fusion-360-api-document-structure.html "
typepad_basename: "fusion-360-api-document-structure"
typepad_status: "Publish"
---

<p>今回は Fusion 360 で扱うことになるドキュメントの構造について、主要な項目に分けてご紹介しておきたいと思います。用語が幾分抽象的になりますが、ご容赦ください。アルファベット表記の場合は Fusion 360 API のクラス、カタカナ表記の場合は、一般的なフィーチャ ベースの 3D CAD の概念に合致するはずです。</p>
<p><strong>Docuemnt（ドキュメント）</strong></p>
<p style="padding-left: 30px;">Fusion 360 で扱うことになるファイルを、API では「ドキュメント」として表現しています。ドキュメントには、さまざまなデータを格納するコンテナとしての役割があります。この考え方は、Fusion 360 に限らず、Inventor や AutoCAD などの CAD データも含め、オフィス系ソフトが作成するデータにも当てはめることが出来るはずです。</p>
<p style="padding-left: 30px;">さて、Fusion 360 で扱うドキュメントは、API 上 Document という基本クラスで表現されています。実際に Fusion 360 が扱うデータの種類によって、次の派生クラス名で表現される 2 種類のドキュメントが存在しています。</p>
<ul>
<li><strong>FusionDocument</strong><br />デザインや CAM、シミュレーションで利用される 3D モデルを含みます。</li>
</ul>
<ul>
<li><strong>DrawingDocument</strong><br />3D モデルから生成される 2D 図面を含みます。</li>
</ul>
<p style="padding-left: 30px;">現時点では Fusion 360 API では図面をサポート出来ていないので、DrawingDocument クラスにアクセスすることは出来ません。なので、ここでは FusionDocument に特化して話を進めていくことにします。</p>
<p><strong>Product（プロダクト）</strong></p>
<p style="padding-left: 30px;">FusionDocument には、格納されるデータタイプによって Product クラスで表現されるデータが格納されています。</p>
<ul>
<li>Design<br />すべての Fusion データで、常にドキュメント内で 1 つだけ存在します。<br /><br /></li>
<li>CAM<br />デザインデータを利用して作成された CAM 用データです。ドキュメント内には、CAM データが必ずしも含まれるものではなく、また、定義されている場合でも、存在する CAM &#0160;データはドキュメント内に &#0160;1つのみです。<br /><br /></li>
<li>SIMStudy<br />デザインデータを利用して作成されたシミュレーション スタディ &#0160;データです。ドキュメント内には、シミュレーション スタディデータが必ずしも含まれるものではなく、また、定義されている場合でも、存在するシミュレーション スタディ データはドキュメント内に &#0160;1つのみです。<br /><br /></li>
<li>SIMCase<br />個々に作成したシミュレーション スタディ です。ドキュメント内に全く存在しない場合も、複数存在する場合もあり得ます。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08aa4d30970d-pi" style="display: inline;"><img alt="Fusion_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08aa4d30970d image-full img-responsive" src="/assets/image_378962.jpg" title="Fusion_data" /></a></li>
</ul>
<p><strong>Design（デザイン）</strong></p>
<p style="padding-left: 30px;">FusionDocument に &#0160;1 &#0160;対 1 で対応するオブジェクトで、実際のデータ要素であるコンポーネントにアクセスする窓口となります。</p>
<p><strong>Component（コンポーネント）</strong></p>
<p style="padding-left: 30px;">Design に 1 対 1 で対応するルート コンポーネントがあり、このルート コンポーネントが、パーツやサブアセンブリなどの各種コンポーネントへのアクセスを提供します。</p>
<p style="padding-left: 30px;"><img alt="Design" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08bc63ba970d image-full img-responsive" src="/assets/image_626606.jpg" title="Design" /></p>
<p style="padding-left: 30px;">ユーザ インタフェースを使って新しいコンポーネントを追加する際には、常にアクティブ コンポーネントに新しく作成したコンポーネントが追加されます。API でのコンポーネント追加の場合には、追加対象するコンテナとなるコレクション オブジェクトにコンポーネントを追加することが出来ます。いずれの場合も、コンポーネントはワールド座標系に沿って配置されます。</p>
<p><strong>Occurrence（オカレンス）</strong></p>
<p style="padding-left: 30px;">Occurrence は、ブラウザ上では &quot;コンポーネント&quot; として表示され、Occurrence が他の&#0160;Occurrence を含むことが出来ます。また、&#0160;Occurrence 再配置が可能です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81792d8970b-pi" style="display: inline;"><img alt="Browser_hierarchy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81792d8970b image-full img-responsive" src="/assets/image_343002.jpg" title="Browser_hierarchy" /></a></p>
<p style="padding-left: 30px;">複数の&#0160;Occurrence が同時に別の 1 つの&#0160;Occurrence を参照することも出来ます。</p>
<p style="padding-left: 30px;">もし、この Occurrence が、別ドキュメントにある Occurence を参照する場合、つまり、アセンブリとパーツの関係がドキュメントをまたいでいる場合を想定してみます。アセンブリ上で、アセンブリが参照するパーツ（Occurrence ）のジオメトリにアクセスしようとした場合に問題が起こります。形状の実態であるジオメトリは、パーツ ファイルとして定義されたコンポーネント内にしか存在しません。にもかかわらず、アセンブリ上に配置されたコンポーネントとして、そのジオメトリを個々に取得する必要があるという場合です。</p>
<p><strong>Proxy（プロキシ）</strong></p>
<p style="padding-left: 30px;">このようば場面では、アセンブリ上に配置されている Occurrence を Proxy として扱うことで、個々の独自性を維持することが出来ます。Proxy からは、参照元の別ドキュメントの Occurrence へのアクセスが提供されます。もし、同一ドキュメント内にジオメトリが存在する場合には、実際にジオメトリに直接アクセスすることが出来ます。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08bc6630970d-pi" style="display: inline;"><img alt="Proxy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08bc6630970d image-full img-responsive" src="/assets/image_992022.jpg" title="Proxy" /></a></p>
<p>By Toshiaki Isezaki</p>
