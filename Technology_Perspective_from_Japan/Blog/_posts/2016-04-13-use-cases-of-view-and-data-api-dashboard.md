---
layout: "post"
title: "View and Data API の利用例 － ダッシュボード"
date: "2016-04-13 00:32:02"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/04/use-cases-of-view-and-data-api-dashboard.html "
typepad_basename: "use-cases-of-view-and-data-api-dashboard"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span></p>
<p>View and Data API を利用すれば、形状データをビューア上に表示するだけだけでなく、オリジナルの CAD データが持っていたさまざまなメタデータ情報（属性、プロパティ）も、同時に得ることが出来ます。それらメータデータは、ビューアのプロパティ パネルで参照できるほか、View and Data API でクエリして他のデータベース API と連携させることで、いろいろな見せ方を実装することが出来ます。</p>
<p>ここでは、オートデスクが View and Data API で作成した情報ダッシュボードの例を 2 つご案内します。&#0160;</p>
<p><strong>BIM ダッシュボード：</strong></p>
<p><a name="BIM Dashboard"></a></p>
<p style="padding-left: 30px;">Revit プロジェクト （.rvt ファイル） を利用した例です。Revit プロジェクトには、3D モデルと &#0160;2D シートがあり、いずれも View and Data API でストリーミング配信して、1つの Web ページ上に表示することが可能です。また、プロジェクト ファイル内の個々の要素が持っている Building Information Model（BIM）情報は、API で抽出することも出来ます。主な機能は、次のとおりです。</p>
<ul>
<li>API で取得した BIM 情報を元に、オブジェクト タイプやフロア毎の数を集計する円グラフや棒グラフを生成します。</li>
<li>グラフ領域を選択することで、該当するオブジェクトを画面に強調表示します。</li>
<li>2D シート上のオブジェクトを選択することで、対応するオブジェクトを&#0160;3D &#0160;モデルで拡大表示します。</li>
<li>選択した 3D オブジェクトが記載されている 2D シートをリストアップして、そのサムネイル画像をクリックすることで、2D シートを画面に表示します。</li>
<li>オブジェクト タイプやレベル、HVAC システムやマテリアル別に表示テーマを設定し、3D 表示された当該オブジェクトを色分け表示する。</li>
</ul>
<p style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" height="344" src="https://www.youtube.com/embed/yMVIbdNDukA?feature=oembed" width="459"></iframe>&#0160;</p>
<p style="padding-left: 30px;">このサンプルは、<strong><a href="http://calm-inlet-4387.herokuapp.com/" target="_blank">こちら</a></strong> から直接表示してテストすることが出来ます。また、GitHub 上でソースコードも <strong><a href="https://github.com/Developer-Autodesk/LmvNavTest" target="_blank">公開</a></strong> されています。</p>
<p><strong>部品&#0160;ダッシュボード：</strong></p>
<p><a name="PARTS Dashboard"></a></p>
<p style="padding-left: 30px;">Inventor から 出力した DWF ファイルを利用した例です。 View and Data API で形状データを表示するだけでなく、各オブジェクトが持つメタデータを <strong><a href="https://www.mongodb.org/" target="_blank">MangoDB</a></strong> を使って、さまざまなクエリーに応じた集計機能を実装しています。主な機能は、次のとおりです。</p>
<p style="padding-left: 30px;">なお、オブジェクト分解、断面化、パース表示の焦点調整は、いずれも&#0160;View and Data API で表示したビューアの標準機能です。</p>
<ul>
<li>オリジナルのCAD データが持つアセンブリ構造をパースして、画面左手にツリーとして表示する。</li>
<li>ツリー内のサブアセンブリ名、パーツ名をダブル クリックして、当該オブジェクトを強調表示する。</li>
<li>部品コストやマテリアルを集計表示するグラフ領域をクリックして、当該オブジェクトを強調表示する。</li>
<li>画面下部に表示されるマテリアル毎のレコードをクリックすることで、当該オブジェクトを拡大表示する。</li>
<li>表示させるモデルを選択する。</li>
</ul>
<p style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" height="344" src="https://www.youtube.com/embed/Cm6Fv1sUdK0?feature=oembed" width="459"></iframe>&#0160;</p>
<p style="padding-left: 30px;">&#0160;このサンプルは、<strong><a href="http://mongo.autodesk.io/" target="_blank">こちら</a></strong> から直接表示してテストすることが出来ます。また、GitHub 上でソースコードも <strong><a href="https://github.com/Developer-Autodesk/integration-mongo-view.and.data.api" target="_blank">公開</a></strong> されています。</p>
<p>このように、WebGL 対応の Web ブラウザがあれば、タブレットやスマートフォンも含めた多様なデバイスで、設計データから抽出したメタデータを使って十分に活用することが出来ます。特に、いままで進んでいなかった、建築、建設、または、製品ライブサイクル の後工程で再利用することが出来ます。Web カタログやファシリティ マネージメント（FM）などのシステムへの流用も容易なはずです。</p>
<p>By Toshiaki Isezaki</p>
