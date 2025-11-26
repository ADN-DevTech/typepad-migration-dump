---
layout: "post"
title: "Fusion 360 API：ジオメトリとトポロジ ～ その2 "
date: "2016-03-04 00:07:39"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/03/fusion-360-api-geometry-and-topology-part2.html "
typepad_basename: "fusion-360-api-geometry-and-topology-part2"
typepad_status: "Publish"
---

<p>前回、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/fusion-360-api-geometry-and-topology-part1.html" target="_blank">Fusion 360 API：ジオメトリとトポロジ ～ その1</a></strong>&#0160;で、Fusion 360 API で B-Rep 情報を扱う基本的な概念をご案内しました。ここでは、B-Rep を扱う上での注意点とトポロジからジオメトリを取得する際のクラスについてご紹介します。</p>
<p><strong>ソリッドとサーフェスの違い <br /></strong></p>
<p style="padding-left: 30px;">Fusion 360 API でB-Rep 情報を扱う上で、ソリッドとサーフェスの違いが微々たるものです。いずれも BRepBody 配下に B-Rep オブジェクトがある構造は同じです。ただ、次の例のように、特定の面のエッジが他の面のエッジに接していない場合には、BRepBody.isSolid プロパティによってサーフェスと判定されます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c1f76d970d-pi" style="display: inline;"><img alt="Solids_vs_surfaces" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c1f76d970d img-responsive" src="/assets/image_272395.jpg" style="width: 230px;" title="Solids_vs_surfaces" /></a></p>
<p style="padding-left: 30px;">BRepFace から取得できる法線ベクトル（Normal Vector）は、ソリッドの場合、外側に向かって法線ベクトルが一定になりますが、サーフェスと判定された場合には、法線ベクトルは不定になります。</p>
<p><strong>B-Rep データの作成と編集</strong></p>
<p style="padding-left: 30px;">B-Rep データを意図的に構築することは出来ません。B-Rep データは、フィーチャを作成したり、3D データをインポートした際に、Fusion 360 によって自動的に生成されるものです。編集も同様にフィーチャ編集によって B-Rep データが間接的に編集されることになります。現在、一部のデータ コンバータで利用されているように、B-Rep が持つトポロジ データから Fusion 360 上にB-Rep を直接生成することは出来ませんが、将来、そのような処理が実装される可能性はあります。</p>
<p><strong>T-Spline の API サポート</strong>&#0160;</p>
<p style="padding-left: 30px;">FormFeature オブジェクトは、T-Spline によって形状定義されるフリーフォーム オブジェクトです。残念ながら、この T-Spline データは、現時点で Fusion 360 API ではサポートされていません。ただし、FormFeature.bodies プロパティから、BRepBodies コレクションを取得することは可能です。</p>
<p><strong>エンティティとジオメトリ</strong></p>
<p style="padding-left: 30px;">&#0160;Fusion 360 API を扱う上で、Entity（エンティティ）とGeometory（ジオメトリ）という言葉が利用されることがあります。エンティティとは、一般に、Fusion 360 上で選択できる面やエッジなどのトポロジ オブジェクトを指しています。選択したエンティティ オブジェクトには Geometry プロパティがあり、形状を構成する幾何データ、つまり、ジオメトリを取得することが出来ます。トポロジは論理構造（位相情報）を提供し、ジオメトリは形状を決定する幾何情報を提供します。</p>
<p><strong>ジオメトリ オブジェクト</strong>&#0160;</p>
<p style="padding-left: 30px;">Fusion 360 API でエンティティ オブジェクトの Geometry プロパティ取得して、直接扱うことができるジオメトリ オブジェクトは次のとおりです。サーフェス ジオメトリは主に BRepFace、3D ワイヤフレーム ジオメトリは BRepEdge 経由で取得可能と考えることが出来ます。また、2D ジオメトリはスケッチ上の幾何情報でもあり、BRepCoEdge からも得ることが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08c1eb26970d-pi" style="display: inline;"><img alt="Geometry_objects" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08c1eb26970d image-full img-responsive" src="/assets/image_65548.jpg" title="Geometry_objects" /></a></p>
<p><strong>Curve Evaluator と Surface Evaluator</strong></p>
<p style="padding-left: 30px;">B-Rep を利用することで、自由曲線や自由曲面を含む曲線やサーフェスを評価して、特定のパラメータ位置のジオメトリを取得することが出来ます。例えば、BRepEdge の&#0160;evaluator プロパティから&#0160;&#0160;CurveEvaluator3D オブジェクトを取得すると、CurveEvaluator3D.getPointAtParameter メソッドで任意の1次パラメータ位置に相当する場所の Point3D ジオメトリを取得できます。</p>
<p style="padding-left: 30px;">曲線範囲のパラメータは Fusion 360 が設定していて、CurveEvaluator3D.getParameterExtents メソッドで曲線の開始位置のパラメータ値と終了位置のパラメータ値得ることができるので、あとは&#0160;getPointAtParameter メソッドで範囲内の任意の値を与えることで、そのパラメータ値に相当する位置のジオメトリを Point3D ジオメトリを取得できるわけです。下図の例では、4.5 のパラメータ値に相当する Point3D ジオメトリを得ることが出来るわけです。</p>
<p style="padding-left: 30px;">&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1a74d6e970c-pi" style="display: inline;"><img alt="Curve_evaluator" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1a74d6e970c img-responsive" src="/assets/image_634966.jpg" title="Curve_evaluator" /></a></p>
<p style="padding-left: 30px;">この考え方は、曲面にも対応しています。BRepFace.evaluator プロパティから SurfaceEvaluator オブジェクトを取得して、UV 座標で表現される 2 次パラメータの値で曲面上の位置と特定し、getPointAtParameter メソッドで Point3D ジオメトリを、getNormalAtParameter メソッドで法線ベクトルを、それぞれ取得することが出来ます。</p>
<p style="padding-left: 30px;">U パラメータと &#0160;V パラメータの範囲は、SurfaceEvaluator.parametricRange メソッドで得ることが出来ます。下図では、UVの各範囲が 0 から 1 と仮定して、U パラメータが 0.3、V パラメータが 0.1 の位置を取得するイメージです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81d2be9970b-pi" style="display: inline;"><img alt="Surface_evaluator" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81d2be9970b img-responsive" src="/assets/image_751266.jpg" title="Surface_evaluator" /></a></p>
<p>Surface Evaluator を利用した Python サンプルが、<a href="https://github.com/brianekins/FusionHackathonSamples" target="_blank">GitHub</a>&#0160;上で公開されていますので、B-Rep を利用した例として参照してみてください。該当するアドイン名は&#0160;<span class="css-truncate css-truncate-target"><a class="js-directory-link js-navigation-open" href="https://github.com/brianekins/FusionHackathonSamples/tree/master/GeometryEval" id="2893e6283d0371a1e308c2e4304a72dc-2e0e54553bc84b26deec11cfcd915b792aac4bf7" target="_blank" title="GeometryEval">GeometryEval</a>&#0160;で、パラメータ位置の点や法線ベクトルを描画します。</span></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81d3589970b-pi" style="display: inline;"><img alt="Geometry_info" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81d3589970b image-full img-responsive" src="/assets/image_338132.jpg" title="Geometry_info" /></a></p>
<p>&#0160;By Toshiaki Isezaki</p>
