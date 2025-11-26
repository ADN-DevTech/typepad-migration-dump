---
layout: "post"
title: "Fusion 360 API：ジオメトリとトポロジ ～ その1"
date: "2016-03-01 00:10:24"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/03/fusion-360-api-geometry-and-topology-part1.html "
typepad_basename: "fusion-360-api-geometry-and-topology-part1"
typepad_status: "Publish"
---

<p>Fusion 360 では、スケッチ、ソリッド、サーフェス、&#0160;T スプラインなど、多様なオブジェクトを扱うことになります。ここでは、API でそれらを扱うための基本的な概念をご案内します。まず最初に、パラメトリック フィーチャ ベースの 3D CAD にある概念、用語をご紹介しておきます。</p>
<p><strong>ジオメトリ</strong></p>
<p style="padding-left: 30px;">Fusion 360 内で扱われる様々な形状をかたち作る<strong>幾何情報</strong>です。スケッチで考えると分かり易いと思います。具体的には、線分や円、円弧などが、その構成要素になります。</p>
<p style="padding-left: 30px;">少し考えを発展させて、今度はソリッドを考えてみましょう。ソリッドとは、複数のサーフェイスで体積を取り囲む集合体と言えます。この場合、ソリッドの構成要素は、サーフェスと考えることが出来ます。さらに、個々のサーフェスは、その輪郭形状を線分や円、円弧などの構成要素として保持しています。ジオメトリとは、この構成要素の幾何形状や座標値などを指します。</p>
<p style="padding-left: 30px;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c0e201970d-pi" style="display: inline;"><img alt="Geometries" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c0e201970d image-full img-responsive" src="/assets/image_993196.jpg" title="Geometries" /></a></p>
<p><strong>トポロジ</strong></p>
<p style="padding-left: 30px;">上記のソリッドで考えた場合、体積を囲むサーフェス群には、ソリッドを形作る上で重要な情報があります。サーフェス同士が、どのように隣接し合っているかの情報です。具体的には、あるサーフェスに隣接するサーフェス同士が、どのエッジとどのエッジで接しているか、などの情報です。このような位置関係を維持する情報を、<strong>位相情報</strong>、あるいは、トポロジと呼びます。</p>
<p>そして、ジオメトリとトポロジの両者を扱うための概念が、<strong>B-Rep</strong>（Boundary Representation）、日本語表現では <strong>境界表現</strong>&#0160;と呼ばれる考え方です。Fusion 360 API でジオメトリを取得する必要がある場合には、このB-Rep を用いて、必要となるジオメトリを構成要素として取得出来る階層まで、トポロジを追跡していく作業が必要になります。</p>
<hr />
<p>ここからは、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/02/fusion-360-api-document-structure.html" target="_blank">Fusion 360 API：ドキュメント構造</a></strong>&#0160;のブログ記事で紹介した Component オブジェクトから、どのようにトポロジを追跡していくか、また、個々の B-Rep がどのようなオブジェクトで表現されているかを見ていきます。</p>
<p><strong>Bodies - ソリッドとサーフェス <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c0e603970d-pi" style="float: right;"><img alt="Solids_and_surfaces_hierarcy" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c0e603970d img-responsive" src="/assets/image_177263.jpg" style="margin: 0px 0px 5px 5px;" title="Solids_and_surfaces_hierarcy" /></a><br /></strong></p>
<p style="padding-left: 30px;">BRepBody オブジェクトは、ソリッド、または、サーフェスのグループを表現しています。Component オブジェクトの bRepBodies プロパティから&#0160;BRepBodies コレクションを取得できます。&#0160;さらに、BRepBodies の item プロパティで、個々の BRepBody オブジェクトを取得することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c0edc9970d-pi" style="display: inline;"><img alt="Solids_and_surfaces" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c0edc9970d img-responsive" src="/assets/image_296906.jpg" title="Solids_and_surfaces" /></a></p>
<p><strong>Lumps - ランプ <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c0ebd7970d-pi" style="float: right;"><img alt="Lumps_hierarcy" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c0ebd7970d img-responsive" src="/assets/image_899597.jpg" style="width: 180px; margin: 0px 0px 5px 5px;" title="Lumps_hierarcy" /></a> </strong></p>
<p style="padding-left: 30px;">ランプ（BrepLumps）は、接続されたサーフェイス群のグループで、常に&#0160;BRepBody と 1:1 になります。BRepBody の lumps プロパティで BRepLumps コレクションを、BRepLumps コレクションの&#0160;item プロパティで、個々の BRepLump オブジェクトを取得することが出来ます。</p>
<p style="padding-left: 30px;">&#0160;</p>
<p>&#0160;</p>
<p><strong>Shells - シェル <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c0ed1f970d-pi" style="float: right;"><img alt="Shells_hierarcy" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c0ed1f970d img-responsive" src="/assets/image_825297.jpg" style="width: 220px; margin: 0px 0px 5px 5px;" title="Shells_hierarcy" /></a><br /></strong></p>
<p style="padding-left: 30px;">BRepShells コレクションは、接続されたサーフェイス群のセットです。BRepShells コレクションの&#0160;item プロパティで、個々の BRepShell オブジェクトを取得することが出来ます。シェルの内側の空間が存在する可能性もありますが、この判定は isVoid プロパティでおこなうことができます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a6424c970c-pi" style="display: inline;"><img alt="Shell" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a6424c970c img-responsive" src="/assets/image_582943.jpg" title="Shell" /></a>&#0160;</p>
<p><strong>Faces - 面 <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a64306970c-pi" style="float: right;"><img alt="Faces_hierarcy" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a64306970c img-responsive" src="/assets/image_864674.jpg" style="width: 260px; margin: 0px 0px 5px 5px;" title="Faces_hierarcy" /></a><br /></strong></p>
<p style="padding-left: 30px;">BRepFace は、単一のサーフェスを表現します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c0ee21970d-pi" style="display: inline;"><img alt="Faces" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c0ee21970d img-responsive" src="/assets/image_828448.jpg" title="Faces" /></a></p>
<p><strong>Loops - ループ <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81c30a1970b-pi" style="float: right;"><img alt="Loops_hierarcy" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81c30a1970b img-responsive" src="/assets/image_987989.jpg" style="width: 280px; margin: 0px 0px 5px 5px;" title="Loops_hierarcy" /></a></strong></p>
<p style="padding-left: 30px;">BRepLoop は、サーフェスの境界を表現します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81c307c970b-pi" style="display: inline;"><img alt="Loops" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81c307c970b img-responsive" src="/assets/image_152675.jpg" title="Loops" /></a></p>
<p><strong>Edges - エッジ <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c0ee5f970d-pi" style="float: right;"><img alt="Edges_hierarcy" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c0ee5f970d img-responsive" src="/assets/image_978373.jpg" style="width: 300px; margin: 0px 0px 5px 5px;" title="Edges_hierarcy" /></a></strong></p>
<p style="padding-left: 30px;">BrepEdge は、面の外形となり、隣接する面と接するカーブです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81c30c5970b-pi" style="display: inline;"><img alt="Edges" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81c30c5970b img-responsive" src="/assets/image_379867.jpg" title="Edges" /></a></p>
<p><strong>Verteces -&#0160;頂点<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a643b8970c-pi" style="float: right;"><img alt="Verteces_hierarcy" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a643b8970c img-responsive" src="/assets/image_989287.jpg" style="margin: 0px 0px 5px 5px;" title="Verteces_hierarcy" /></a></strong></p>
<p style="padding-left: 30px;">&#0160;BrepVertex は、エッジの端点を表現します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81c3102970b-pi" style="display: inline;"><img alt="Verteces" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81c3102970b img-responsive" src="/assets/image_611747.jpg" title="Verteces" /></a></p>
<p><strong>&#0160;CoEdges - 共通エッジ <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c0effe970d-pi" style="float: right;"><img alt="Coedges_hierarcy" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c0effe970d img-responsive" src="/assets/image_933714.jpg" style="margin: 0px 0px 5px 5px;" title="Coedges_hierarcy" /></a></strong></p>
<p style="padding-left: 30px;">BRepCoEdge は、各サーフェスのエッジを表現します。ジオメトリは、サーフェイスの 2D 空間に定義されることになります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a64716970c-pi" style="display: inline;"><img alt="CoEdges" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a64716970c img-responsive" src="/assets/image_584308.jpg" title="CoEdges" /></a>&#0160;</p>
<p>3D CAD によって異なりますが、B-Rep を扱うためのオブジェクト（主にトポロジ）&#0160;には非常に多くのオブジェクトが存在します。</p>
<p>次回は、ジオメトリの注意点とトポロジからジオメトリの取得ついて、ご案内しましょう。</p>
<p>By&#0160;Toshiaki Isezaki</p>
