---
layout: "post"
title: "AutoCAD 2014 の新機能 ～ その 2"
date: "2013-04-03 01:10:12"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/04/new-features-on-autocad-2014-part2.html "
typepad_basename: "new-features-on-autocad-2014-part2"
typepad_status: "Publish"
---

<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/03/new-features-on-autocad-2014-part1.html" target="_blank">前回</a></strong>に続いて、AutoCAD 2014 の新機能をご紹介します。今回は、生産性向上 にかかわるさまざまな機能を見ていきたいと思います。</p>
<p><strong>コマンド ライン機能の強化</strong></p>
<p style="padding-left: 30px;">AutoCAD 2014 では、コマンド ライン上の操作や機能が大幅に強化、改善されています。まずは、次の画像をクリックして動画の内容を参照してみてください。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9b29648970d-pi" style="display: inline;"><img alt="コマンドライン" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017ee9b29648970d image-full" src="/assets/image_383406.jpg" title="コマンドライン" /></a></p>
<p style="padding-left: 30px;">この動画で紹介しているのは、下記の機能の中で、自動修正、オートコンプリート、コンテンツ、インターネット検索の機能です。その他の機能も提供しています。</p>
<p style="padding-left: 30px;"><strong>自動修正（オート コレクト）</strong><br />AutoCAD 2014 のコマンド ラインは、自動修正をサポートしています。コマンドを間違って入力した場合、「不明なコマンド」と表示されるのではなく、最も可能性の高い有効な AutoCAD コマンドに自動修正されます。たとえば、間違って TOBEL と入力しても、TABLE[表] コマンドが自動的に起動されます。</p>
<p style="padding-left: 30px;"><strong>オートコンプリート</strong><br />コマンド入力時のオートコンプリートは、AutoCAD 2014 で部分文字列検索をサポートするよう強化されました。たとえば、コマンド ラインに SETTING と入力すると、SETTING という文字列から始まるコマンドだけでなく、任意の場所に含まれるコマンドも候補リストに表示されます。</p>
<p style="padding-left: 30px;"><strong>候補の最適化</strong><br />候補リストのコマンドは、当初は一般的なユーザの使用順序に従って表示されます。AutoCAD 2014 を使い続けるうちに、候補リストのコマンドの順序は、ユーザ自身の使用傾向に沿って最適化されます。コマンドの使用データは、ユーザごとにプロファイルに格納され、最適化されます。</p>
<p style="padding-left: 30px;"><strong>同義語候補<br /></strong>コマンド ラインには、組み込み済みの同義語リストがあります。コマンド ラインに文字列を入力すると、その文字列が同義語リストに見つかった場合は、そのコマンドが返されます。たとえば、Symbol と入力すると、INSERT[ブロック挿入] コマンドが返され、ブロックを挿入することができます。または、Round と入力すると、FILLET[フィレット] コマンドが返され、コーナーにフィレットを施すことができます。[管理 ] リボン タブの [エイリアスを編集] ツールを使用して、自動修正リストと同義語リストに独自の単語を追加することができます。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9b28bc6970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="同義語リスト編集" class="asset  asset-image at-xid-6a0167607c2431970b017ee9b28bc6970d" src="/assets/image_366482.jpg" title="同義語リスト編集" /></a></p>
<p style="padding-left: 30px;"><strong>インターネット検索</strong><br />AutoCAD 2014 では、候補リストのコマンドやシステム変数の詳細情報をすばやく検索することができます。候補リストのコマンドまたはシステム変数上にカーソルを移動し、[ヘルプで検索] または [インターネットで検索] アイコンを選択することで、関連情報の検索が可能です。インターネットで検索する場合には、現在の検索語の前に AutoCAD という名前が自動的に付加されます。</p>
<p style="padding-left: 30px;"><strong>コンテンツ</strong><br />AutoCAD 2014 のコマンド ラインを使用することで、画層、ブロック、ハッチング パターンとグラデーション、文字スタイル、寸法スタイル、表示スタイルにアクセスすることができます。たとえば、現在の図面に ドア という名前のブロック定義が存在している場合、コマンド ラインに ドア と入力することにより、候補リストから目的のブロックをすばやく挿入することができます。</p>
<p style="padding-left: 30px;"><strong>分類</strong><br />候補リストを簡単にナビゲートできるよう、システム変数とその他のコンテンツは展開可能な分類項目に編成されています。分類項目を展開して結果を表示したり、[Tab] キーを押すことによって分類項目を順に切り替えることができます。&#0160;</p>
<p style="padding-left: 30px;"><strong>入力設定</strong><br />コマンド ラインを右クリックしたときの [入力設定] メニューのコントロールを使用して、コマンド ラインの動作をカスタマイズすることができます。[オートコンプリート] や [システム変数を検索] を有効にする従来からのオプションに加えて、[自動修正]、[コンテンツを検索]、[部分文字列検索] を有効にすることができます。これらのオプションは、既定ではすべてオンになっています。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c380f5836970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="入力検索オプション" class="asset  asset-image at-xid-6a0167607c2431970b017c380f5836970b" src="/assets/image_278415.jpg" title="入力検索オプション" /></a></p>
<p><strong>ファイル タブ</strong></p>
<p style="padding-left: 30px;">AutoCAD 2014 に用意されたファイル タブを使うと、すばやく視覚的に、開いている図面を切り替えたり新しい図面を作成することができます。ファイル タブ バーの表示/非表示は、[表示] リボン タブの [ファイル タブ] コントロールを使用して切り替えることができます。[ファイル タブ] をオンにすると、開いている図面のタブが作図領域の上部に表示されます。</p>
<p style="padding-left: 30px;">タブをドラッグ アンド ドロップすることで、タブの並び順序を変更することもできます。また、ファイル タブにカーソルを移動すると、モデルとレイアウトのプレビュー イメージが表示されます。プレビュー イメージの 1 つにカーソルを移動すると、対応するモデルまたはレイアウトが一時的に作図領域に表示され、プレビュー イメージから [印刷] および [パブリッシュ] ツールにアクセスできます。従来からあった クイックビュー図面 や クイック ビュー レイアウト の機能を併せ持っていることがわかります。</p>
<p style="padding-left: 30px;">また、図面タブの右側のプラス(+) アイコンを使用して新規図面を簡単に作成できます。新規図面を作成すると、そのタブが自動的に追加されます。</p>
<p style="padding-left: 30px;">次の画像をクリックすると、これらの機能を動画で参照することができます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c380f6147970b-pi" style="display: inline;"><img alt="ファイルタブ" border="0" class="asset  asset-image at-xid-6a0167607c2431970b017c380f6147970b image-full" src="/assets/image_974060.jpg" title="ファイルタブ" /></a></p>
<p><strong>画層管理の強化</strong></p>
<p style="padding-left: 30px;">リボンに表示される画層の数が増えました。また、自然なソート順で、画層が表示されるようになりました。たとえば、画層名 1、4、25、6、21、2、10 は、1、10、2、25、21、4、6 と並ぶのではなく、1、2、4、6、10、21、25 と並ぶようになりました。</p>
<p style="padding-left: 30px;">[画層プロパティ管理] パレットダイアログ上で [合成] オプションを使用すると、画層一覧で 1 つまたは複数の画層を選択し、それらの画層上のオブジェクトを別の画層に合成することができます。元の画層は図面から自動的に名前削除されます。</p>
<p style="text-align: center; padding-left: 30px;">&#0160;<iframe frameborder="0" height="281" src="http://www.youtube.com/embed/sVrj-4n9sBQ?feature=oembed" width="500"></iframe>&#0160;</p>
<p><strong>外部参照の機能強化</strong></p>
<p style="padding-left: 30px;">[種類] 列をダブルクリックすることにより、外部参照のアタッチの種類をアタッチとオーバーレイ間で簡単に変更することができます。右クリック メニューの新しいオプションを使用して、選択した複数の外部参照の種類を一度に変更することができます。また、[外部参照] パレットには、選択した外部参照のパスを、絶対パスと相対パス間で簡単に変更できるツールが含まれています。パスを完全に除去することもできます。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d425d5b53970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="XRef" class="asset  asset-image at-xid-6a0167607c2431970b017d425d5b53970c" src="/assets/image_266539.jpg" title="XRef" /></a></p>
<p style="padding-left: 30px;">-XREF[外部参照] コマンドには、スクリプトを使用してこれらのパスの変更を自動化するための新しい [パスのタイプ(T)] オプションがあります。</p>
<p><strong>作図、注釈機能の強化</strong></p>
<p style="padding-left: 30px;"><strong>円弧</strong><br />作図時に [Ctrl] キーを押して方向を切り替えることにより、どちら回りにも簡単に円弧を描くことができ ます。</p>
<p style="padding-left: 30px;"><strong>ポリライン</strong><br />AUGI からの Wish List に要望があったポリラインの自己フィレット、つまり閉じたポリ ラインの作成機能が、AutoCAD 2014 で実現しました。</p>
<p style="padding-left: 30px;"><strong>シート セット</strong><br />シート セットに新しいシートを作成するとき、関連付けるテンプレート(.dwt) の [作成日] フィールドには、テンプレート ファイルの作成日ではなく、新しいシートの作成日が表示されます。</p>
<p style="padding-left: 30px;"><strong>印刷スタイル</strong><br />CONVERTPSTYLES[印刷スタイル変換] コマンドを使用して、現在の図面を名前の付いた印刷スタイルまたは色従属印刷スタイルに変換することができます。AutoCAD 2014 では、スペースが含まれた印刷スタイル名にも対応するよう強化されました。</p>
<p style="padding-left: 30px;"><strong>属性</strong><br />属性が含まれたブロックを挿入するときの既定の動作は、コマンド ラインからの属性値の入力ではなく、ダイアログ ボックスからの入力に変更されました。つまり、システム変数 ATTDIA の値 は、1 に設定されています。</p>
<p style="padding-left: 30px;"><strong>文字</strong><br />1 行文字は、位置合わせの設定を変更しない限り、最後に行った位置合わせの設定が保持されるよう強化されました。</p>
<p style="padding-left: 30px;"><strong>寸法記入</strong><br />新しいシステム変数 DIMCONTINUEMODE を使用することで、直列寸法または並列寸法を記入する際に、より多彩にコントロールすることができます。DIMCONTINUEMODE を 0 (ゼロ) に設定すると、DIMCONTINUE[直列寸法記入] および DIMBASELINE[並列寸法記入] コマンドは、現在の寸法スタイルに基づいて寸法を記入します。1 に設定すると、選択した寸法の寸法スタイルが適用されます。</p>
<p style="padding-left: 30px;"><strong>ハッチング</strong><br />リボンの[ハッチング]ツールは、直前に使用したハッチングするオブジェクトの選択方法 ([内側の点を指定] または [オブジェクトを選択]) を保持します。[元に戻す(U)] オプションが、コマンド ラインに追加されました。</p>
<p>次回は、カスタマイズ関係の新機能や改良点をご紹介しましょう。</p>
<p>&#0160;By Toshaiki Isezaki</p>
<p>&#0160;</p>
