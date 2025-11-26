---
layout: "post"
title: "Inventor 2015 の新機能 その２"
date: "2014-04-14 06:05:42"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/04/inventor_2015_new_product_no2.html "
typepad_basename: "inventor_2015_new_product_no2"
typepad_status: "Publish"
---

<p>前回の<a href="http://adndevblog.typepad.com/technology_perspective/2014/04/inventor_2015_New_Product_No1.html">「Inventor 2015 新機能 その１」</a>に続き、第２回目の今回は昨年(2013/11/15東京・2013/11/19大阪)にて Developer Daysで紹介いたしました Inventor2015製品のシートメタルやスケッチといった新機能についてご紹介させていただきます。</p>

<p><strong>Autodesk Inventor 2015 の新機能</strong></p>

<p><strong>１．	シートメタル</strong><br />
<strong>ウィンドウを使用してパンチを配置する点を選択<br />
</strong>シート メタル パーツにパンチを配置するときに、ウィンドウ選択を使用して複数の点を選択することができるようになりました。個別に選択する従来の機能も使用できます。 <br />
•	領域選択ボックスと少しでも重なる点をすべて選択状態にするには、右から左方向にウィンドウ選択します。 <br />
•	領域選択ボックスで完全に囲まれた点のみを選択状態にするには、左から右方向にウィンドウ選択します。 <br />
•	選択セットから一部の点を削除したり、一部の点を削除して別の点を追加するには、引き続き左から右方向、または右から左方向にウィンドウ選択します。 <br />
パンチ ツールでの選択動作は、[Ctrl]キーを押しながら操作した場合と基本的に同じです。セット内のオブジェクトを選択した場合、[Ctrl]を押さずに削除することができます。<br />
 <br />
<strong>フラット パターンを削除するとき、およびフラット パターンを変換するときのプロンプト表示のコントロール</strong><br />
•	シート メタル パーツのフラット パターンを削除するときに、次の内容のメッセージが表示されます。 <br />
シート メタル パーツのフラット パターンを削除すると、関連する図面のすべてのフラット パターン ビューも削除されます。 <br />
•	シート メタル パーツを標準パーツに変換するときは、次の内容のメッセージが表示されます。 <br />
シート メタル パーツを標準パーツに変換すると、シート メタルのフラット パターンが自動的に削除されます。シート メタル パーツのフラット パターンを削除すると、関連する図面のすべてのフラット パターン ビューも削除されます。 <br />
どちらの警告メッセージでも、[プロンプト]をクリックすると、プロンプトが表示されるタイミングをコントロールするオプションが表示されます。プロンプトが今後表示されないようプロンプトを非表示にすることができます。[アプリケーション オプション]の[プロンプト]タブでも、このプロンプト表示の動作をコントロールできます。 </p>

<p><strong>ブラウザ内の曲げパーツの EOP を EOF に変更</strong><br />
ブラウザ ツリーで、シート メタル パーツのパーツの終端(EOP)マーカーが曲げの終端またはフラットの終端(EOF)に変更されました。マーカーの右クリック メニューには、[EOF マーカーを移動]、[EOF を終端に移動]、[EOF を先端に移動]などのコマンドが追加されました。 </p>

<p><strong>シート メタル パンチ穴フィーチャを曲げに沿って適用</strong><br />
シート メタルのパンチツールでは、パーツの面に曲げがあっても、曲げに沿って単純な穴の形状や複雑な穴の形状をカットすることができます。 <br />
配置済みのシート メタル パンチ穴 iFeature のインスタンスを作成または編集する場合、パンチ穴フィーチャを曲げに沿って適用するかどうかを指定できます。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5119e44b9970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a5119e44b9970c img-responsive" alt="Fig1" title="Fig1" src="/assets/image_745328.jpg" /></a><br /><br />
[<strong>曲げ編集]ダイアログ ボックスでのプロパティ名全体の表示</strong><br />
ロフト フランジの[曲げ編集]ダイアログ ボックスの幅が広くなり、表示されるプロパティ名が途中で切れないようになりました。 </p>

<p><strong>[切り取り]ダイアログ ボックスで使用できる[垂直にカット]</strong><br />
[切り取り]ダイアログ ボックスに[垂直にカット]オプションが追加されました。選択したスケッチなどのプロファイルがサーフェスに投影され、投影と交差している面に対して垂直にカットされます。 <br />
Inventor2014までの切り取りは、右側に表現されているように軸方向に限定した動作によるものでしたが、Inventor2015では左に表現されているように法線方向による切断のために、折り曲げた形状が考慮された展開図になります。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcee9d01970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcee9d01970b img-responsive" alt="Fig2" title="Fig2" src="/assets/image_481958.jpg" /></a><br /><br />
<strong>フラット パターン内での方向のコントロールの改良</strong><br />
曲げモデルで、フラット パターンの方向を調整できるようになりました。座標系のコントロール機能を強化するため、[フラット パターン]ダイアログ ボックスに、回転角度を設定するオプションが追加されました。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcee9e0d970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcee9e0d970b img-responsive" alt="Fig3" title="Fig3" src="/assets/image_427556.jpg" /></a><br /><br />
<strong>フラット パターンおよびパンチツールの A 側面の選択</strong><br />
シート メタル パーツの A 側面は、フラット パターン(パンチ マシン)の上側の面であることを示します。新たに導入されたコマンドの[A 側面の定義]  が、フラット パターン グループのリボンに追加されました。このコマンドを使用すると、シート メタル パーツの A 側面を選択して、パンチ穴方向を示すことができます。A 側面を選択せずにフラット パターンを作成した場合は、A 側面が自動的に作成され、ブラウザ ノードのエントリが追加されます。ブラウザでフラット パターン定義を編集する際に、基準面を反転することで、A 側面を変更することもできます。 <br />
フラット パターンが存在しない場合は、現在の A 側面を削除できます。フラット パターンの方向を変更すると、ブラウザ ノードを選択したときにハイライト表示される A 側面に反映されます。変更を行った結果、A 側面の計算に失敗した場合は、A 側面のブラウザ ノードを右クリックして、新しい A 側面を選択することができます。選択後に、新しい A 側面のブラウザ ノードが表示されます。 <br />
パンチ ツールの配置を開始すると、指定したフラット パターンの A 側面がグラフィックス領域でハイライト表示されます。右クリック メニューのオプションを使用して、A 側面のハイライト表示を設定したり、方向、パンチ穴リプレゼンテーション、曲げ角度計測を調整することができます。[A 側面をハイライト表示]コマンドをクリックすると、すべての A 側面をドキュメントの選択済みセットに追加できます。 </p>

<p><strong>インポートした曲げ半径ゼロのシート メタル パーツの展開または再折り曲げ</strong><br />
インポートしたサードパーティのシート メタル モデルの曲げ半径がゼロの場合に、シート メタル モデルを展開または再折り曲げすることができます。シート メタル環境の[ジオメトリを展開]の[展開]および[再折り曲げ]コマンドで、半径がゼロの曲げを選択できるようになりました。フラット パターンを作成するときに、半径がゼロの曲げを平坦化することができます。 <br />
展開を実行すると、曲げ半径がゼロのエッジが存在する場合、新しい面が追加されます。この面の領域は、ユーザが定義した K ファクターによって決まります。 <br />
再折り曲げを実行すると、モデルを展開するときに作成された参照は引き続き残ります。この動作は、半径がゼロではない曲げの場合と同じです。 </p>

<p><strong>２．	スケッチ</strong><br />
<strong>スケッチタブ＆コマンドの起動改善</strong><br />
3Dモデル作成時に、「スケッチ用の2D」コマンドを起動させる事で、「自動的に2Dスケッチ作成またはスケッチ選択モードが起動」されます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcee9e67970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcee9e67970b img-responsive" alt="Fig4" title="Fig4" src="/assets/image_260644.jpg" /></a><br /><br />
<strong>リボンパネル内の個別カスタマイズ操作</strong><br />
パネル内に存在するアイテムのドロップダウンメニューは任意のコマンド単位でグループ化・グループ解除をする事ができ、パネル内のコマンド群として独立させて表示できるようになります。<br />
これは、よく使用するコマンドをプルダウンから取り出して見えるようにパネルに配置する事ができるものであり、逆に使わなくなったら、元のグループ化に戻すといった事が可能になり、ボタンサイズも個別に切り替えられるようになりましたので、グループ化を解除しておき、特に使用するボタンなどを大きくしたり、あまり使用しないものを小さなボタンで表現したり、展開表示パネル内に移動しておくといった独自のパネル内の表現が可能となります。</p>

<p><strong>(1).ドロップダウンメニューのグループ/グループ解除</strong><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73da954a2970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73da954a2970d img-responsive" alt="Fig5" title="Fig5" src="/assets/image_356570.jpg" /></a><br /><br />
<strong>(2).ボタンサイズの切り替え</strong><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73da954cc970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73da954cc970d img-responsive" alt="Fig6" title="Fig6" src="/assets/image_128042.jpg" /></a><br /><br />
<strong>(3).グループ単位で展開表示パネルとメインパネル間の移動</strong><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcee9ecb970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcee9ecb970b img-responsive" alt="Fig7" title="Fig7" src="/assets/image_51986.jpg" /></a><br /><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73da95523970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73da95523970d img-responsive" alt="Fig8" title="Fig8" src="/assets/image_162454.jpg" /></a><br /><br />
<strong>スケッチの機能強化</strong><br />
<strong>仮想交点にポイントの作成</strong><br />
2D スケッチの 2 つの要素間の仮想交差をキャプチャしたり、2D スケッチの仮想曲線上の推定配置を自動キャプチャできるようになりました。実際の交差要素に対する既存の拘束の推定配置を仮想交差にも適用できます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73da95693970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73da95693970d img-responsive" alt="Fig9" title="Fig9" src="/assets/image_151373.jpg" /></a><br /><br />
<strong>新しい拘束ツールと設定</strong><br />
<strong>新しい拘束を適用できる新機能の解除モード</strong><br />
ジオメトリを修正する際に、新たに導入された解除モードを使用すると、既に拘束されているジオメトリに新しい拘束を適用することができます。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5119e4741970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a5119e4741970c img-responsive" alt="Fig10" title="Fig10" src="/assets/image_753184.jpg" /></a><br /> <br />
解除モードをオンにした状態で、新しい拘束または寸法を追加すると、矛盾する拘束が削除されます。選択したスケッチ ジオメトリが既に拘束されている場合でも、自由にドラッグして、既存のモデルの拘束を修正することができます。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73da95593970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73da95593970d img-responsive" alt="Fig11" title="Fig11" src="/assets/image_846896.jpg" /></a><br /><br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73da955a5970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a73da955a5970d img-responsive" alt="Fig12" title="Fig12" src="/assets/image_212454.jpg" /></a><br /> <br />
新しい寸法または拘束を追加することで、(一致、スムーズ、正接、対称、パターン、および投影の拘束を除く)すべての矛盾する拘束を削除できます。新しい拘束を追加できない場合は、矛盾する拘束を手動で削除します。 </p>

<p><strong>2D スケッチ拘束の設定へのアクセスの改善</strong><br />
2D スケッチ拘束に関連するすべての設定が、[拘束設定]という新しい 1 つのコマンドに再編成されました。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcee9fa5970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcee9fa5970b img-responsive" alt="Fig13" title="Fig13" src="/assets/image_905302.jpg" /></a><br /><br />
<strong>スケッチ拘束の改善</strong><br />
<strong>新しい拘束の設定</strong><br />
新しい設定として[作成時に拘束を表示]が導入されました。この設定では、作成した拘束に関する明確なフィードバックを表示できます。 <br />
新しい設定として[選択したオブジェクトの拘束を表示]が導入されました。この設定では、グラフィックス ウィンドウで選択したジオメトリの拘束を強調表示できます。表示された拘束は、選択して削除することができます。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5119e47dd970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a5119e47dd970c img-responsive" alt="Fig14" title="Fig14" src="/assets/image_884753.jpg" /></a><br /><br />
<strong>スケッチ内に文字を作成するときの文字スタイルの指定</strong><br />
スケッチ文字を作成するときに、ドキュメント内に存在する使用可能な(有効な)文字スタイルを一覧から選択できるようになりました。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcee9fce970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01a3fcee9fce970b img-responsive" alt="Fig15" title="Fig15" src="/assets/image_397708.jpg" /></a><br /><br />
次回は、パーツやアセンブリの新機能についてご紹介する予定です。<br />
Shigekazu Saito.<br />
</p>
