---
layout: "post"
title: "Inventor 2015 の新機能 その３"
date: "2014-04-28 03:00:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/04/inventor_2015_new_product_no3.html "
typepad_basename: "inventor_2015_new_product_no3"
typepad_status: "Publish"
---

<p>前回の「<a href="http://adndevblog.typepad.com/technology_perspective/2014/04/inventor_2015_New_Product_No1.html">Inventor 2015 新機能 その１</a>」「<a href="http://adndevblog.typepad.com/technology_perspective/2014/04/inventor_2015_New_Product_No2.html">Inventor 2015 新機能 その２</a>」に続き、第３回目の今回は昨年(2013/11/15東京・2013/11/19大阪)にて Developer Daysで紹介いたしました Inventor2015製品のパーツやアセンブリといった新機能についてご紹介させていただきます。</p>

<p><strong>１．パーツ内の未使用のパラメータ削除</strong></p>

<p><strong>1 回のアクションで未使用のパラメータをすべて削除</strong><br />
[パラメータ]ダイアログ ボックスの下部にある[未使用の項目を削除]オプションを選択すると、別のウィンドウに未使用のパラメータがすべて一覧表示されます。これらのパラメータを 1 回の操作で削除することができます。[すべてはい]をクリックすると、ドキュメント内の使用されていないパラメータがすべて削除され、[すべていいえ]をクリックすると、現在のドキュメント内の使用されていないパラメータが保持されます。 <br />
ウィンドウに表示されるパラメータを保持するには、既定の設定である[いいえ]のままにしておきます。ウィンドウに表示されるパラメータを削除するには、既定の設定の[いいえ]ではなく[はい]をクリックし、削除を実行します。エクスポートされた未使用のパラメータは、他のドキュメントで使用されている場合、[いいえ]が既定の設定となっています。 <br />
[パラメータ]ダイアログ ボックスで、パラメータを個別に削除することもできます。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73db4d5dd970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73db4d5dd970d img-responsive" alt="Fig1" title="Fig1" src="/assets/image_757538.jpg" /></a><br /><br />
<strong>２．２つの平面間の中点平面の作成</strong><br />
	２つの並行または非並行面を用いて中間に作業平面を作成する事ができます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73db4d5f2970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73db4d5f2970d img-responsive" alt="Fig2" title="Fig2" src="/assets/image_408483.jpg" /></a><br /><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511a9bc50970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a511a9bc50970c img-responsive" alt="Fig3" title="Fig3" src="/assets/image_137357.jpg" /></a><br /><br />
<strong>３．ジョイントの作成</strong><br />
右クリック メニューの新しいオプション、[2 つの面間]および[原点をオフセット]を使用すると、ジョイントをより簡単に、より柔軟に作成することができます。 <br />
・	[2 つの面間]では、ジョイントの原点を作成できます。面を 2 つ、点を 1 つ選択することで、2 つの面間の仮想中点を指定します。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73db4d5f8970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73db4d5f8970d img-responsive" alt="Fig4" title="Fig4" src="/assets/image_151733.jpg" /></a><br /><br />
・	[原点をオフセット]では、ジョイントの原点をオフセットできます。マニピュレータ矢印をドラッグするかオフセット値を入力して、原点の位置を変更します。原点を位置合わせする参照ジオメトリを選択します。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511a9bc78970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a511a9bc78970c img-responsive" alt="Fig5" title="Fig5" src="/assets/image_190474.jpg" /></a><br /><br />
これで、作業ジオメトリを使用して、パーツの方向を位置合わせできるようになります。[位置合わせ 1]で点(スケッチ点または作業点)を選択した場合は、[位置合わせ 2]でも点を選択する必要があります。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73db4d644970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73db4d644970d img-responsive" alt="Fig6" title="Fig6" src="/assets/image_80987.jpg" /></a><br /><br />
<strong>４．アセンブリのフレーム ジェネレータ</strong><br />
フレーム メンバの再利用と、再利用したフレーム メンバの変更 <br />
2 つの新しいコマンド、[再利用]  および[再利用を変更]  がリボンの[デザイン]タブでの[フレーム]パネルに追加されました。 <br />
[再利用]を使用すると、ソース フレーム メンバを選択してメンバを再利用できるので、同一のファイルの元のファイル数を削減できます。 <br />
・	[再利用]は、直線のメンバにのみ使用できます。 <br />
・	再利用したメンバのセットからメンバを削除するには、メンバを右クリックし、[再利用メンバを破棄]を選択します。 <br />
・	再利用の方向が適切でない場合は、[メンバの方向を反転]を使用できます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511a9bca2970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a511a9bca2970c img-responsive" alt="Fig7" title="Fig7" src="/assets/image_949738.jpg" /></a><br /> <br />
[再利用を変更]を使用すると、再利用したフレーム メンバを別のフレーム メンバに切り替えることができます。また、ジオメトリの選択、または再利用するフレーム メンバの位置の入力を変更することもできます。 <br />
・	フレーム再利用のワークフローでは、複数のフレーム メンバのオカレンスをウィンドウ選択することができます。 <br />
・	再利用したメンバの末端処理を正常に変更するには、再利用する前に、ソース メンバに末端処理を適用します。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73db4d665970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73db4d665970d img-responsive" alt="Fig8" title="Fig8" src="/assets/image_7365.jpg" /></a><br /><br />
マイタが 2 箇所にあるフレーム メンバについて正確な長さの自動取得 <br />
互いに向かい合う 2 つの内側コーナー間の傾斜梁など、各末端にマイタがある直線状のフレーム メンバについては、正確なフレームの長さが自動的に計算されます。 </p>

<p><strong>５．アセンブリの簡易モードの機能強化</strong> <br />
・	パーツおよびサブアセンブリをインプレイスで編集できるようになり、別ウィンドウで開く必要がなくなりました。 <br />
・	次の操作に対応するコマンドが簡易モードで有効になりました。 <br />
o	コンポーネントを作成する(インプレイス) <br />
o	断面図コマンドを使用する <br />
o	作業フィーチャを作成または編集する <br />
o	パターン コンポーネント <br />
o	コピー コンポーネントおよびミラー コンポーネント <br />
o	アセンブリ <br />
o	点群のコマンド <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcfa1176970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcfa1176970b img-responsive" alt="Fig9" title="Fig9" src="/assets/image_569959.jpg" /></a><br /><br />
次回は、図面の新機能についてご紹介する予定です。<br />
Shigekazu Saito.<br />
</p>
