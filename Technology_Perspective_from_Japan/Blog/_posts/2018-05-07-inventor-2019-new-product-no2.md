---
layout: "post"
title: "Inventor 2019 の新機能 その２"
date: "2018-05-07 02:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/05/inventor-2019-new-product-no2.html "
typepad_basename: "inventor-2019-new-product-no2"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2018/04/inventor-2019-new-product-no1.html">先回</a>は、Inventor 2019製品の「サブスクリプション特典」による「クラウド利用による「共有ビュー」によるコラボレーションデータの共有」や「Autodesk Drive クラウドストレージによる共有」及び「アセンブリ」についてお届けしましたが、今回は引き続き Invento2019 製品の「パーツ」「スケッチ」「iLogicの機能拡張」についてご紹介させていただきたいと思います。<br />
( 本内容は、一部 Developer Days 2018 ( 2018/02 開催 ) の内容を含みます )</p>

<p><strong>１．パーツ</p>

<p>１－１．モデルベース定義 ( MBD ) の強化</p>

<p>幾何公差アドバイザーによるカラーリング</strong></p>

<p>公差アドバイザは、色コードを使用して拘束状態を表示します。<br />
公差アドバイザで[面のステータスの色]をクリックすると、色の表示がオンになります。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03706b6200d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224e03706b6200d image-full img-responsive" alt="Inventor2019_New2_Fig1-1" title="Inventor2019_New2_Fig1-1" src="/assets/image_936874.jpg" border="0" /></a><br />
<strong>穴/ねじ 注記コマンドの強化</strong></p>

<p>パターンの数量表記が追加されました。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e0370702200d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224e0370702200d img-responsive" alt="Inventor2019_New2_Fig1-1-2" title="Inventor2019_New2_Fig1-1-2" src="/assets/image_999974.jpg" /></a><br /></p>

<p><strong>１－２．ダイレクト編集で自動ブレンドをサポート</strong><br />
	<br />
これまでは、[3D モデル]タブ  [修正]パネル  [ダイレクト編集]を使用して、<br />
面フィーチャを 回転または別の場所に移動させることのみ可能でした。<br />
オフのときは、[自動ブレンド]がこの動作を維持して、新しい位置に面を移動します。<br />
オンのときは、面フィーチャの合計長さが変更されます。</p>

<p>自動ブレンドは再ブレンド技術で、自動的に隣接する接線面を移動し、必要に応じて新しいブレンドを作成します。面を移動または回転するときに使用できます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df300c5d200b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224df300c5d200b image-full img-responsive" alt="Inventor2019_New2_Fig2" title="Inventor2019_New2_Fig2" src="/assets/image_922324.jpg" border="0" /></a></p>

<p><br />
<strong>１－３．シートメタルの強化</p>

<p>対称フェースの作成</strong></p>

<p>厚みに沿ってシート メタル パーツをセンタリングできるようにしたいという多数の要望がありました。<br />
ユーザの意見を取り入れ、[面]コマンドでは、オフセット方向に「両側」用意の選択が用意され、面作成では押し出しのオフセット方向をサポートし、基準を元に対称面を作成できるようになりました。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c8486118200c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0223c8486118200c img-responsive" alt="Inventor2019_New2_Fig3-1" title="Inventor2019_New2_Fig3-1" src="/assets/image_750396.jpg" /></a><br />
<strong>「レーザー溶接」コーナー レリーフ形状</strong></p>

<p>「レリーフの形状」に「レーザー溶接」が追加されました。<br />
直線結合コーナー レリーフは点で終了し、円弧結合コーナー レリーフの形状は直線セグメントで終了します。<br />
新しいレーザー溶接コーナー レリーフは接線円弧で終了し、レーザー切断パーツにより適しています。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224df300ca2200b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224df300ca2200b image-full img-responsive" alt="Inventor2019_New2_Fig3-2" title="Inventor2019_New2_Fig3-2" src="/assets/image_684871.jpg" border="0" /></a></p>

<p><br />
<strong>１－４．ブラウザ内のフィーチャーノード名の後に拡張情報を表示</strong></p>

<p>[アプリケーションオプション/パーツ]タブ、[ブラウザ内のフィーチャーノード名の後に拡張情報を表示]設定が有効になっているときに、深さとメソッドに関する情報がブラウザに表示されるようになりました。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e037076f200d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224e037076f200d image-full img-responsive" alt="Inventor2019_New2_Fig4-1" title="Inventor2019_New2_Fig4-1" src="/assets/image_911809.jpg" border="0" /></a><br /></p>

<p><strong>１－５．反転したフィレットの作成</strong></p>

<p>フィレットタイプに「反転したフィレット」が追加されました。<br />
新しい反転フィレットオプションを使用する事で、1)凸形または 2)凹形エッジを持つ反転したフィレット形状を作成する事ができます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c8486169200c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0223c8486169200c image-full img-responsive" alt="Inventor2019_New2_Fig5" title="Inventor2019_New2_Fig5" src="/assets/image_764952.jpg" border="0" /></a><br /></p>

<p><strong>２．スケッチの強化</p>

<p>２－１．可変のらせん曲線の作成</strong></p>

<p>可変ピッチらせんの作成を効率化するために機能強化され、複数のピッチ、回転数、直径、高さの値 で 可変のらせん曲線を作成する事ができます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0223c8486182200c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0223c8486182200c image-full img-responsive" alt="Inventor2019_New2_Fig6" title="Inventor2019_New2_Fig6" src="/assets/image_617529.jpg" border="0" /></a><br /></p>

<p><strong>２－２．図面スケッチの自動投影</strong></p>

<p>図面ビューのスケッチで[アプリケーションオプション/スケッチ]タブの「曲線作成時にエッジの自動投影」を設定する事で、部品スケッチの場合と同じように、図面ビューのスケッチで同じように動作するようになりました。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e03707ec200d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224e03707ec200d img-responsive" alt="Inventor2019_New2_Fig7" title="Inventor2019_New2_Fig7" src="/assets/image_66475.jpg" /></a><br /></p>

<p><strong>３．iLogic の機能拡張</p>

<p>３－１．ルール編集エディター内で「インテリセンス」機能の充実</strong></p>

<p>ルール編集エディター内で「インテリセンス」機能が充実されました。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e0370802200d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224e0370802200d img-responsive" alt="Inventor2019_New2_Fig8" title="Inventor2019_New2_Fig8" src="/assets/image_430682.jpg" /></a><br /></p>

<p><strong>３－２．iLogicコンポーネントと管理動作<br />
</strong><br />
コンポーネントおよび拘束を追加、修正、および削除するためのルール コードの作成が簡単になりました。<br />
Components.Add： <br />
	コンポーネントが存在し、プロパティが指定されていることの確認。<br />
Components.AddiPart：  <br />
	iPartファクトリとメンバーの指定を使用してオカレンスを作成または更新。<br />
ThisAssembly.BeginおよびManageThisAssembly.EndManage：<br />
		特にComponents.Deleteを呼び出さずにコンポーネントを削除。</p>

<p><br />
<strong>３－３．ドキュメント単位ジオメトリ</strong></p>

<p>座標値にデータベース単位の代わりにドキュメント単位を使用する ポイント、ベクトル、行列を表すオブジェクトの存在</p>

<p>Components.Addおよび関連する関数によって作成されたコンポーネントの位置と向きを指定するために使用可能。<br />
ThisDoc.Geometry.Point()または同様の関数を使用してオブジェクトを作成。<br /></p>

<p><br />
<strong>３－４．iLogicアセンブリで拘束関数の追加</strong></p>

<p>iLogicリレーションシップ（追加）機能を使用して、ルールによってオカレンスと拘束が生成されるiLogicアセンブリを作成可能。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0224e037081e200d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0224e037081e200d img-responsive" alt="Inventor2019_New2_Fig9" title="Inventor2019_New2_Fig9" src="/assets/image_197051.jpg" /></a><br /></p>

<p>今回は、Inventor 2019製品の「パーツ」や「スケッチ」「iLogicの機能拡張」についてお届けしました。<br />
次回は、Invento2019 製品の その他の機能についてご紹介する予定です。</p>

<p><br />
By Shigekazu Saito.</p>
