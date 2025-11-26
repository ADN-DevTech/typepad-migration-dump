---
layout: "post"
title: "Inventor 2015 の新機能 その４"
date: "2014-05-12 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/05/inventor_2015_new_product_no4.html "
typepad_basename: "inventor_2015_new_product_no4"
typepad_status: "Publish"
---

<p>先回の「<a href="http://adndevblog.typepad.com/technology_perspective/2014/04/inventor_2015_New_Product_No1.html">Inventor 2015 新機能 その１</a>」「<a href="http://adndevblog.typepad.com/technology_perspective/2014/04/inventor_2015_New_Product_No2.html">Inventor 2015 新機能 その２</a>」「<a href="http://adndevblog.typepad.com/technology_perspective/2014/04/inventor_2015_New_Product_No3.html">Inventor 2015 新機能 その３</a>」に続き、第４回目の今回は昨年(2013/11/15東京・2013/11/19大阪)にて Developer Daysで紹介いたしました Inventor2015製品の図面の新機能についてご紹介させていただきます。</p>

<p><strong><br />
Autodesk Inventor 2015 の新機能</strong><br />
<strong><br />
１．Expressグラフィックを用いて ラスタービューによる大規模アセンブリの高速図面ビューの作成</strong><br />
<strong><br />
・	簡易データでの図面ビューの作成時間が短くなりました。 </strong><br />
Expressグラフィックを用いて 図面ビュー(ラスタービュー)の作成時間が短くなりました。<br />
アセンブリ内の簡易モード データは、正確なビューおよびラスター ビューの高速プレビューとビュー配置を生成する目的で使用されます。<br />
注: 旧形式のアセンブリおよびパーツは、Inventor 2015 にマイグレーションする必要があります。<br />
図面ビューを作成する際、[ラスター ビューのみ]オプションを選択すると、簡易モード データを含む大規模アセンブリのビューがすばやく生成されます。<br />
注: モデルのサイズはパフォーマンスに影響を与えます。パフォーマンスを大幅に高めるには、パーツおよびアセンブリをどちらも最新バージョンにマイグレーションする必要があります。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511a9bcda970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a511a9bcda970c img-responsive" alt="Fig1" title="Fig10" src="/assets/image_898279.jpg" /></a><br /><br />
<strong>２．図面ファイルを高速で開く</strong><br />
図面の[ファイルを開くオプション]ダイアログ ボックスに新たに導入された[高速オープン]オプションを使用すると、図面をすばやく開くことができます。[高速オープン]を選択した場合、図面の参照先となっているファイルの検索または解決は行われません。開いた図面は、ファイル参照が存在しない図面を開いた場合と同様に動作します。編集内容を入力したり保存することができます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd0083a1970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fd0083a1970b img-responsive" alt="Fig2" title="Fig2" src="/assets/image_395578.jpg" /></a><br /><br />
<strong>３．短縮寸法</strong><br />
長さ（Inventor 2014製品でサポート済み）、角度、円弧長の短縮寸法を作成できます。[寸法]コマンドを実行して 2 つの参照線または参照エッジを選択するか、円弧を 1 つ選択し、右クリックしてメニューを表示します。[寸法タイプ]リストで、使用する短縮寸法のタイプを選択し、図面に寸法を配置します。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73dbb5e63970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73dbb5e63970d img-responsive" alt="Fig3" title="Fig3" src="/assets/image_43782.jpg" /></a><br /><br />
<strong>４． 常にパーツ一覧を自動的に並べ替える</strong><br />
[パーツ一覧を並べ替え]ダイアログ ボックスに、[更新時に自動的に並べ替え]オプションが新たに追加されました。このオプションをオンにすると、並べ替え設定が特定のパーツ一覧に継続的に適用されます。このオプションは、ダイアログ ボックスで[OK]をクリックすると実行されます。[更新時に自動的に並べ替え]をオンにすると、パーツを追加または削除した後にテーブルを手動で並べ替える必要がなくなります。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd0083fc970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fd0083fc970b img-responsive" alt="Fig4" title="Fig4" src="/assets/image_621952.jpg" /></a><br /><br />
<strong>５． ビュー ラベルで図面ビューに関連付けられたシート参照を識別する</strong><br />
断面図(断面/詳細)を別のシートに移動しても、正しいシートを簡単に特定できます。ビュー ラベル プロパティに、親シート名と親シート インデックスが追加されました。親ビューを別のシートに移動した場合、またはシートの名前またはインデックスを修正した場合、文字列の値が更新されます。<br />
ビュー ラベルを追加または編集するときは、これらのプロパティをアクティブにし、<親シート名> および <親シート インデックス> を選択します。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511b03012970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a511b03012970c img-responsive" alt="Fig5" title="Fig5" src="/assets/image_529382.jpg" /></a><br /><br />
このプロパティは、[スタイルおよび規格エディタ] [規格] [ビューの設定] [表示]の既定のビュー ラベルにも追加されていますので、「ビューラベルを編集」にて規定値を変更する事もできます。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73dbb5e93970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73dbb5e93970d img-responsive" alt="Fig6" title="Fig6" src="/assets/image_492283.jpg" /></a><br /><br />
<strong>６． 分割したテーブルを別のシートに移動する</strong><br />
ブラウザ ツリーを使用して、分割した図面テーブル(一般テーブル、穴テーブル、およびパーツ一覧)を別のシートに移動することができます。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd008437970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fd008437970b img-responsive" alt="Fig7" title="Fig7" src="/assets/image_362337.jpg" /></a><br /></p>

<p>次回より、Autodesk Inventor 2015 の新機能のAPI についてご紹介する予定です。<br />
Shigekazu Saito.</p>
