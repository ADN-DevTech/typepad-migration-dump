---
layout: "post"
title: "AutoCAD 2016 の新機能 ～ その1"
date: "2015-03-25 00:10:29"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/03/new-features-on-autocad-2016-part1.html "
typepad_basename: "new-features-on-autocad-2016-part1"
typepad_status: "Publish"
---

<p>AutoCAD の最新バージョンとなる AutoCAD 2016 が発売されました。まずは、このバージョンが持つ図面ファイル形式やサポートする OS などの基本的な情報です。</p>
<p><strong>図面ファイル形式</strong></p>
<p>AutoCAD 2016 では、引き続き AutoCAD 2013 から採用された 2013 図面ファイル形式（DWG、DXF）を採用します。従来、3 世代毎に採用する図面ファイル形式を変更してきましたが、今回は、4 世代同じ図面ファイル形式を継承することになります。これは、同時に発売された AutoCAD LT 2016 も同様です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb080af330970d-pi" style="display: inline;"><img alt="Drawing_format" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb080af330970d image-full img-responsive" src="/assets/image_710874.jpg" title="Drawing_format" /></a></p>
<p>もちろん、従来のバージョンと同様、下位形式で図面ファイルを保存することも可能です。DWG ファイルでは、R14、2000、2004、2007、2010 形式で、DXF ファイルでは、R12、2000、2004、2007、2010 形式で、それぞれ保存することが出来ます。図面ファイルの読み込みでは、すべての AutoCAD バージョンで作成された DWG ファイルと DXF ファイルを読み込むことが出来ます。&#0160;</p>
<p><strong>サポートする OS</strong></p>
<p>AutoCAD 2016 と AutoCAD LT 2016 は、次の Windows OS をサポートしています。</p>
<p style="padding-left: 30px;"><strong>Windows 7</strong></p>
<p style="padding-left: 30px;">Windows 7&#0160;Enterprise、Windows 7&#0160;Ultimate、Windows 7&#0160;Professional、Windows 7&#0160;Home Premium の各エディションの 32 ビット、及び 、64 ビット版</p>
<p style="padding-left: 30px;"><strong>Windows 8</strong></p>
<p style="padding-left: 30px;">Windows 8、Windows 8&#0160;Pro、Windows 8&#0160;Enterprise&#0160;の各エディションの 32 ビット、及び 、64 ビット版</p>
<p style="padding-left: 30px;"><strong>Windows 8.1</strong></p>
<p style="padding-left: 30px;">Windows 8.1、Windows 8.1&#0160;Pro、Windows 8.1&#0160;Enterprise&#0160;の各エディションの 32 ビット、及び 、64 ビット版</p>
<p>それでは、AutoCAD 2016 で利用することが出来る新機能や改良・改善された機能をご紹介していきたいと思います。紹介する機能は モダン-スマート、生産性向上、接続性 のテーマに沿うことにします。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0eedcdf970c-pi" style="display: inline;"><img alt="Theme" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0eedcdf970c img-responsive" src="/assets/image_627934.jpg" style="width: 420px;" title="Theme" /></a></p>
<p><strong>モダン - スマート ～&#0160;<strong>ユーザ インタフェースの改良</strong></strong></p>
<p>AutoCAD 2016 では、昨年の AutoCAD 2015 に引き続き、ダークテーマを採用したモダンなユーザ インタフェースを継承しています。その中でも、お客様からのご意見を反映した改良が施されています。</p>
<p style="padding-left: 30px;"><strong>[スタート] タブ</strong></p>
<p style="padding-left: 30px;">AutoCAD 2015 の起動時には、それまでの AutoCAD にはなかった [新しいタブ] という名前のタブが図面ウィンドウの代わりに表示され、より直観的に新規図面の作成や既存図面のオープン、といった操作を明示的に操作できるようになりました。ただ、[新しいタブ] は同時に複数表示することが出来てしまったので、一部、混乱される方もいらしゃったようです。</p>
<p style="padding-left: 30px;">そこで、AutoCAD 2016 では、[新しいタブ] の名称を改め [スタート] タブとし、同時に 1 つしか [スタート] タブが表示されないように改良されています。よりシンプルになったので、操作に迷うことはありません。いつでも [スタート] タブに戻って新しい作業を始めることが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08095cd2970d-pi" style="display: inline;"><img alt="Start_tab" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08095cd2970d image-full img-responsive" src="/assets/image_802826.jpg" title="Start_tab" /></a></p>
<p style="padding-left: 30px;"><strong>リボン プレビュー</strong></p>
<p style="padding-left: 30px;">AutoCAD 2015 で登場した機能に、ブロック挿入やスタイル選択のリボン インタフェースへのプレビュー画像の表示がありました。この機能によって、間違って違うブロックを挿入したり、異なる寸法スタイルをアクティブに設定してしまうこと抑止することが出来ます。</p>
<p style="padding-left: 30px;">AutoCAD 2016 では、このプレビュー表示の有無をシステム変数&#0160;GALLERYVIEW でコントロール出来るようになりました。設計者の好みで自由に変更することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0eedf0d970c-pi" style="display: inline;"><img alt="Gallerry_preview" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0eedf0d970c image-full img-responsive" src="/assets/image_944720.jpg" title="Gallerry_preview" /></a></p>
<p style="padding-left: 30px;"><strong>ステータスバー ボタン</strong></p>
<p style="padding-left: 30px;">ステータスバーを 2 行で表示することで、ウィンドウを小さくした場合にステータスバー上に配置されているボタンが隠れてしまう問題を回避出来るようになりました。この処理は、AutoCAD が自動的に処理します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7656688970b-pi" style="display: inline;"><img alt="Statusbar" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7656688970b image-full img-responsive" src="/assets/image_999796.jpg" title="Statusbar" /></a>&#0160;&#0160;</p>
<p><strong>モダン - スマート ～ グラフィックス表現の改善<strong><br /></strong></strong></p>
<p style="padding-left: 30px;"><strong>線の太さと曲線表現</strong></p>
<p style="padding-left: 30px;">AutoCAD 2016 では、図面を表示する際の表現が、より精緻になっています。DirectX 11 に対応したグラフィックスカードを搭載しているコンピュータでハードウェアアクセラレーションを有効にすると、線の太さ（実線）と曲線（円、円弧、楕円、楕円弧）がより正確に表現されます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08095ef5970d-pi" style="display: inline;"><img alt="Hardware_acceleration" class="asset  asset-image at-xid-6a0167607c2431970b01bb08095ef5970d img-responsive" src="/assets/image_80954.jpg" style="width: 400px;" title="Hardware_acceleration" /></a></p>
<p style="padding-left: 30px;">次のスクリーン ショットは、AutoCAD 2015 と AutoCAD 2016 で同じ図面の同じ領域の表現を比較するものです。クリックして実寸で参照してみてください。円や円弧が、従来より正確に表示されていることがわかります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c76568ee970b-pi" style="display: inline;"><img alt="Graphics_improvement" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c76568ee970b image-full img-responsive" src="/assets/image_843786.jpg" title="Graphics_improvement" /></a></p>
<p style="padding-left: 30px;"><strong>再作図なしのズームとパン</strong></p>
<p style="padding-left: 30px;">図面の編集作業では、しばしば極端な倍率してズームインしたり、ズームアウトしたるすることがあります。従来のバージョンでは、このような場面でキャッシュを使い果たしてしまい、手動で&#0160;<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-CC98095F-B4C4-4B25-9097-A7B6EF4260B2" target="_blank"><strong>REGEN[再作図] コマンド</strong></a> で再作図をしないと、現在の状態よりズームインやアウト、または、パンすることが出来くなることがありました。AutoCAD 2016 は、この再作図を極力低減するようになっているので、思考を妨げられることなく、操作を継続できるようになっています。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/odHKtARGW_k?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>次回は、点群を再利用できる新しい機能や、新しくなったレンダリングエンジンと機能についてご紹介します。</p>
<p>By Toshiaki Isezaki</p>
