---
layout: "post"
title: "AutoCAD 2020 の新機能 ～ その2"
date: "2019-03-28 00:10:54"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/new-features-on-autocad-2020-part2.html "
typepad_basename: "new-features-on-autocad-2020-part2"
typepad_status: "Publish"
---

<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/new-features-on-autocad-2020-part1.html" rel="noopener" target="_blank">前回</a></strong>に引き続き、AutoCAD 2020 と AutoCAD LT 2020 に共通した機能として提供される具体的な内容についてご案内していきます。作図や図面編集に直接関係する機能をご案内していきます。</p>
<p><strong>ブロック パレット</strong></p>
<p>ブロック挿入に改良が加えられています。リボン インタフェースの<strong><a href="https://adndevblog.typepad.com/technology_perspective/2014/03/new-features-on-autocad-2015-part1.html" rel="noopener" target="_blank">ギャラリー コントロール</a></strong>からプレビュー画像を使ってブロック挿入出来るようになった AutoCAD 2015 以来の改良です。今回は、今迄あまり変更が加えられていなかった [ブロック挿入] ダイアログが、パレット インタフェースに置き換えられています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4953500200b-pi" style="display: inline;"><img alt="Block_insert" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4953500200b image-full img-responsive" src="/assets/image_304772.jpg" title="Block_insert" /></a></p>
<p>新しい [ブロック] パレットは、INSERT[ブロック挿入] コマンドを実行すると表示されるようになっています。もし、従来の [ブロック挿入] ダイアログを利用したい場合には、CLASSICINSERT[旧ブロック挿入] コマンドを利用することも出来ます。</p>
<p>パレットには挿入可能なブロックがプレビュー画像付きで一覧表示されるので、ドラッグ＆ドロップするか、パレット上にブロック画像をシングル クリックすることで、プロンプトに沿って現在の図面に配置（ブロック挿入）することが出来ます。</p>
<p>&#0160;[ブロック挿入] ダイアログとは異なり、[ブロック] パレットは自身のパレット インタフェースを閉じなくても作図領域の操作が可能なので、挿入座標を順番に指定しながら連続挿入するための [繰り返し配置] オプションが新設されています。なお、コマンド プロンプト バージョンの -INSERT（ハイフン付き）コマンドには [繰り返し配置] オプションはありませんのでご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4476ed7200c-pi" style="display: inline;"><img alt="Repeat_placement" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4476ed7200c image-full img-responsive" src="/assets/image_256284.jpg" title="Repeat_placement" /></a></p>
<p>[ブロック] パレットには、AutoCAD 上に開いている図面内のブロック定義を挿入する [現在の図面] タブ、別の場所に保存されている図面ファイル内のブロック定義を挿入する [他の図面] タブ、挿入したブロック定義の場所にかかわらず、最近挿入したブロック定義を再度挿入する [最近使用] タブの 3 つのタブが用意されています。</p>
<p>[他の図面] タブでは、パレット上部の [...] ボタンから別の図面ファイルを選択すると、Design Center のように、その図面内のブロック定義を抽出して一覧表示します。選択した図面も一覧に表示されるので、図面全体をブロックとして挿入することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44772f3200c-pi" style="display: inline;"><img alt="External_drawing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a44772f3200c image-full img-responsive" src="/assets/image_427561.jpg" title="External_drawing" /></a></p>
<p>ここまでの内容を動画にまとめていますので、ご確認ください。</p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/DAZO69Us5mM?feature=oembed" width="500"></iframe></p>
<p><strong>再デザインされた名前削除</strong></p>
<p>図面内の不要な定義情報をクリーアップする PURGE[名前削除] コマンドでは、ダイアログ ボックス インタフェースが比較的大きな変更を受けています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44777bb200c-pi" style="display: inline;"><img alt="Purge" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a44777bb200c image-full img-responsive" src="/assets/image_830076.jpg" title="Purge" /></a></p>
<p>[名前削除] ダイアログにプレビュー画像の表示エリアが設けられ、対象の定義情報を視覚的に把握出来るようになったほか、削除出来ない定義情報について、なぜ名前削除出来ないのか、その理由を表示するようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4477ba0200c-pi" style="display: inline;"><img alt="Purge_improvements" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4477ba0200c image-full img-responsive" src="/assets/image_596057.jpg" title="Purge_improvements" /></a></p>
<p>[名前削除できない項目を検索] ボタンをクリックして削除出来ない項目の理由を把握した後には、ダイアログ上の虫メガネ アイコンをクリックして、図面上から当該ジオメトリを選択表示することも出来るので、分解操作など、次の操作につなげていくことも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a44780e0200c-pi" style="display: inline;"><img alt="Find_unpurgeable_block" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a44780e0200c image-full img-responsive" src="/assets/image_776179.jpg" title="Find_unpurgeable_block" /></a></p>
<p><strong>改良された図面比較</strong></p>
<p>AutoCAD 2019 で新規に導入された図面比較機能にも改良が加えられています。AutoCAD 2019 では、現在開いている図面とは関係なく、比較対象の 2 つの図面を選択して、違いを新規図面に示すよう処理していました。</p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/DZq134endXc?feature=oembed" width="500"></iframe></p>
<p>AutoCAD 2020 では、比較対象となる一方を開いている状態で、もう一方の図面を選択することで、両者の違いを現在開いている図面に表示するよう変更されています。図面比較で得られた違いの検出機能は従来と同じですが、各種操作はリボン上のボタンではなく、作図領域上部に表示されるドッキング ツールバーでおこなう点が異なります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a470b08e200d-pi" style="display: inline;"><img alt="Drawing_compare_toolbar" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a470b08e200d image-full img-responsive" src="/assets/image_610629.jpg" title="Drawing_compare_toolbar" /></a></p>
<p>新しい図面比較の機能では、現在開いている図面上に動的に書き込んだジオメトリも比較対象となるほか、現在の図面上にないジオメトリを検出した場合、それらを現在の図面にインポートすることが出来るように機能追加されています。AutoCAD 2019 時と同じく、両図面を結合して新規図面を作成したり、相違箇所を順番に拡大表示したりする機能も利用出来ます。</p>
<p>ここまでの内容を動画にまとめていますので、前述の AutoCAD 2019 動画との違いも含め、ご確認ください。</p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/ggykM2v6DEk?feature=oembed" width="500"></iframe></p>
<p>比較した 2 つの図面ファイルに関する情報を表オブジェクトとして作図する場合には、従来通り、<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-63CE0EAD-4FCE-49C4-A24B-449CD5787402" rel="noopener" target="_blank">COMPAREINFO[図面比較情報]</a></strong> コマンドを利用することが出来ます。</p>
<p>なお、図面を開いていない状態で（[スタート] タブの状態で）、アプリケーション メニューから [図面比較] を選択すると、AutoCAD 2019 時のように [図面比較] ダイアログが開きます。ここで、比較対象の 2 つの図面ファイルを選択して図面比較を開始すると、[基点] として選択した図面ファイルが AutoCAD 上に開かれて、検出した相違点が示されます。この点は AutoCAD 2019 時の動作と異なりますのでご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a495b3c7200b-pi" style="display: inline;"><img alt="No_dwg_compare" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a495b3c7200b image-full img-responsive" src="/assets/image_446746.jpg" title="No_dwg_compare" /></a></p>
<p>業種別ツールセットで作成された図面を比較した場合などの制約事項については、<strong><a href="http://help.autodesk.com/view/ACD/2020/JPN/?guid=GUID-3D918CC7-4FB7-4951-8F04-C2434DFD04A2" rel="noopener" target="_blank">こちら</a></strong> をご確認ください。</p>
<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/new-features-on-autocad-2019-part3.html" rel="noopener" target="_blank">次回</a></strong>は、クラウドを使ったシームレスなワークフローに関係する機能をご案内していきます。</p>
<p>By Toshiaki Isezaki</p>
