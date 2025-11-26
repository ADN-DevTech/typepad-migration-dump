---
layout: "post"
title: "グラフ データベース（Graph Database）"
date: "2023-01-30 00:12:40"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/12/graph-database.html "
typepad_basename: "graph-database"
typepad_status: "Publish"
---

<p>今回は、かなりざっくりとですが、今後、Autodesk Platform Services（旧 Forge、以後 APS と表現）で扱うことになるデータ構造、あるいは、データベース構造に触れておきたいと思います。</p>
<hr />
<p><strong>設計データのファイル</strong></p>
<p>CAD ソフトウェアが扱うファイルには、さまざまなデータが論理的に格納されています。最もシンプルな AutoCAD の DWGで見てみると、分かりやすくタイプ別にまとめられていることがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14adb74b200b-pi" style="display: inline;"><img alt="Dwg_structure" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14adb74b200b image-full img-responsive" src="/assets/image_140167.jpg" title="Dwg_structure" /></a><br />また、CAD ソフトウェアのファイルには、データ間の関係性も同時に保持されています。BIM を活用されている方には、自明のことと思いますが、この関係性の情報は非常に重要です。先の DWG でも、各データが別のデータを参照し合っています。モデル空間に作図されている円（Circle）や線分（Line）などの図形と画層（LayerTableRecord）の関係やブロック参照（BlockReference）とブロック定義（BlockTableRecord）の関係などです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14adceef200b-pi" style="display: inline;"><img alt="Dwg_structure" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14adceef200b image-full img-responsive" src="/assets/image_780942.jpg" title="Dwg_structure" /></a></p>
<p>そして、<a href="https://adndevblog.typepad.com/technology_perspective/2021/12/required-design-tools-per-era.html" rel="noopener" target="_blank">設計ツールとデザインデータの遷移</a> でも触れたとおり、 時代とともに、扱うデータと表現は多岐にわたり、かつ、膨大になりつつあります。</p>
<hr />
<p><strong>SQL データベース</strong></p>
<p>別の視点でデータを考えてみると、「設計データ」として直接扱わないタイプのデータとも連携が必要になることがあります。他社のコンポーネントや部材を設計に組み込む場合、それらのメーカーや型番、場合によっては在庫状況や新たに発注した場合の納期などの情報です。</p>
<p>このような場面では、サイドデータベースと呼ばれる外部データベースと連携する仕組みを構築するのが一般的です。この外部データベースとの併用は古くから活用されています。2D 図面運用でも比較的早い時期から機能を提供していました。例えば、AutoCAD では AutoCAD SQL Extension（略称 ASE、現 <a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2023/JPN/AutoCAD-Core/files/GUID-8374B198-C4F9-485B-8062-8996D8571304-htm.html" rel="noopener" target="_blank">dbConnect</a>）という機能を用意して、エンティティ（図形）と外部データベースに関係性を持てるようにしています。</p>
<p>CAD ソフトウェアとの連携で利用されてきたのは、リレーショナルデータベース（RDB）と呼ばれる SQL（Structured Query Language）タイプのデータベースです（以後 SQL データベース） 。クライアント コンピュータでの利用では、おそらく、<a href="https://ja.wikipedia.org/wiki/Microsoft_Access" rel="noopener" target="_blank">Microsoft Access</a> が最も馴染みのある RDB かもしれません。</p>
<p>SQL データベースは、データを格納するテーブル、カラム（行）、レコード（列）、フィールド、で構成された、いわゆる、構造化された（Structure）データベースです。格納すべきデータはデータタイプ（整数、実数、文字列など）と共に厳密に定義されているのが特徴です。また、レコードは特定のフィールド（キー）を使用して、他のテーブルの特定のレコードと関係性を維持しています。そして、RDB にデータを照会（クエリ）するのが 、SELECT 句を駆使する SQL 文というわけです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14add0b8200b-pi" style="display: inline;"><img alt="Side_database" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14add0b8200b image-full img-responsive" src="/assets/image_747531.jpg" title="Side_database" /></a></p>
<p>SQL データベースについて簡単にまとめると、「データ タイプ別のテーブルに格納した情報が互いに参照し合うことでデータを表現する最適化されたデータベース」ということになるかと思います。</p>
<p>少し話が逸れますが、先の図で紹介した DWG が持つデータも、RDB の似た構造であることにお気づきと思います。もちろん、DWG 自体は AutoCAD 固有のファイルなので、SQL 言語で内部データをクエリ出来るわけではありません。</p>
<hr />
<p><strong>グラフ データベース</strong></p>
<p>長らく利用されてきた SQL データベースですが、Web やクラウドの活用、IoT センサーや点群など、データソースが多様化し、データ間の関係性が複雑化してきています。言うまでもなく、オートデスクも APS を使った API 環境を提供することで、こういった活用を支援しています。</p>
<p>ただ、パフォーマンスの問題が表面化してきています。例えば、大規模な BIM データの場合、<a href="https://adndevblog.typepad.com/technology_perspective/2020/11/utilizeing-meta-data.html" rel="noopener" target="_blank">Model Derivative API：メタデータの活用</a> の方法で JSON データを取得するまでに一定程度時間がかかりますし、そうのような JSON データのパースにも時間がかかることも想像に難くありません。参考まで、Model Derivative API はバックエンドで SQL データベースとも言える&#0160;<a href="https://ja.wikipedia.org/wiki/SQLite" rel="noopener" target="_blank">SQLite</a> を利用しています。</p>
<p>いわゆる、ビックデータに取り組む必要が急務な状態です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c973553200d-pi" style="display: inline;"><img alt="Data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c973553200d image-full img-responsive" src="/assets/image_861962.jpg" title="Data" /></a></p>
<p>そんな中で、パフォーマンスを維持したままビックデータを扱える新たなデータベースとして、NoSQL（Not only SQL）タイプのデータベースが脚光を浴びています。</p>
<p>NoSQL にはいくつかタイプが存在しますが、共通するのがデータ タイプや数に寛容な点です。既に APS で開発をされているなら、テーブルやレコードを気にせずに JSON が持つ内容をそのまま扱える、といった気軽さがあります。</p>
<p>NoSQL はパフォーマンスにも優れています。SQL データベースのように事前に定義されたデータタイプに沿った処理に限定されないので、ビックデータであっても、圧倒的に短い時間でクエリ結果を得ることが出来るようになる利点もあります。</p>
<p>APS を使ったアプリがサイドデータベースを使う <a href="https://github.com/autodesk-platform-services/aps-db-sample" rel="noopener" target="_blank">APS サンプル</a>では、利用している <a href="https://www.mongodb.com/ja-jp" rel="noopener" target="_blank">MongoDB</a> もNoSQL データベースの 1 つです。MongoDB は、NoSQL データベースの中でドキュメント指向データベースに分類されています。</p>
<p>さて、前置きが長くなりましたが、APS が採用を進めているデータ構造、ないし、データベース構造とは何でしょうか？</p>
<p>NoSQL データベースの 1 つであるグラフ指向データベース、<strong>グラフ データベース</strong>です。グラフ データベースはデータ間の関係を扱うネットワーク状モデルです。データはキーと値の組み合わせで構成される<strong>プロパティ</strong>で、プロパティ間の関係性は<strong>ノード</strong>と呼ばれる単位を、<strong>エッジ</strong>で結ぶかたちで表現されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14add239200b-pi" style="display: inline;"><img alt="Graph" class="asset  asset-image at-xid-6a0167607c2431970b02af14add239200b img-responsive" src="/assets/image_789463.jpg" title="Graph" /></a></p>
<p>この構造は、CAD のデータとデータ間の関係性を表現する最小単位として考えられると思います。そして、この構造こそが、インダストリークラウドや <a href="https://adndevblog.typepad.com/technology_perspective/2023/02/data-exchange-api.html" rel="noopener" target="_blank">Data Exchange</a>（データ交換）で取り組むことになる粒状データを表現するデータ構造ということになります。</p>
<p>これらは、ファイル内ではなく、クラウド上で実現されることになります。BIM で扱うようなビックデータも、グラフ データベース構造を持つ粒状データをクエリしていくことになります。</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c9780d2200d-pi" style="display: inline;"><img alt="Graph_database" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c9780d2200d image-full img-responsive" src="/assets/image_290022.jpg" title="Graph_database" /></a></p>
<p>&#0160;</p>
<hr />
<p>By Toshiaki Isezaki</p>
