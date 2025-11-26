---
layout: "post"
title: "AutoCAD 2016 の新機能 ～ その3"
date: "2015-04-08 00:08:54"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/04/new-features-on-autocad-2016-part3.html "
typepad_basename: "new-features-on-autocad-2016-part3"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/new-features-on-autocad-2016-part1.html" target="_blank">前々回</a>、<a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-features-on-autocad-2016-part2.html" target="_blank">前回</a>に引き続き、AutoCAD 2016 の機能をご紹介していきます。今回は、2D 作図の生産性を向上させる新機能や機能改良についてご紹介します。</p>
<p><strong>生産性向上 ～ スマート寸法記入</strong></p>
<p style="padding-left: 30px;">DIM[寸法記入] コマンドが一新され、1 つのコマンドで多彩な寸法を作図できるようになりました。DIM コマンドを起動したら、寸法を記入したいオブジェクト上にマウスカーソルを近づけるだけで、そのオブジェクトの形状に応じた寸法タイプで寸法プレビューを表示します。適切な寸法タイプであれば、そのまま配置位置をマウス クリックで指定することで作図が完了します。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb080a46b5970d-pi" style="display: inline;"><img alt="Dim_command" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb080a46b5970d image-full img-responsive" src="/assets/image_430968.jpg" title="Dim_command" /></a></p>
<p style="padding-left: 30px;">DIM[寸法記入] コマンドの実行中には、コマンド オプションがコマンド ラインおよび右クリック メニューに表示されるので、直観的な操作で寸法記入を完結することが出来ます。従来のように、作図したい寸法タイプ別にコマンドを使い分ける必要がないため、とても便利です。DIM[寸法記入] コマンド 1 つので、垂直寸法、水平寸法、平行寸法、長さ寸法、角度寸法、半径寸法、直径寸法、折り曲げ半径寸法、弧長寸法、並列寸法、直列寸法 をカバーしています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0efd6cd970c-pi" style="display: inline;"><img alt="Dim_command_options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0efd6cd970c image-full img-responsive" src="/assets/image_96998.jpg" title="Dim_command_options" /></a></p>
<p style="padding-left: 30px;">DIM[寸法記入] コマンドで作図した寸法オブジェクトは、DIMLAYER システム変数で指定された画層に作図されるようになります。この変数を有効に活用することで、作図環境の標準化にも役立てることが出来るはずです。この機能は、DIM[寸法記入] コマンドで記入した寸法オブジェクトにのみ有効です。もちろん、DIMLAYER システム変数には、現在の画層に記入するよう指定することも可能です。ここまでの内容ど動画にしていますので、内容をご確認ください。</p>
<p style="padding-left: 30px; text-align: center;">&#0160;<iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/FIG4LN8UBd4?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="padding-left: 30px;"><strong>寸法値の折り返し</strong></p>
<p style="padding-left: 30px;">寸法値を編集するとき、幅のサイズを変更するためのルーラー コントロールが寸法値の上に表示されるので、簡単に寸法値の折り返し幅を変更出来るようになっています。これらの機能は、オートデスク ユーザ会 AUGI からの要望を実現したものです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0efdec7970c-pi" style="display: inline;"><img alt="Dim_mtext" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0efdec7970c image-full img-responsive" src="/assets/image_102036.jpg" title="Dim_mtext" /></a>&#0160;</p>
<p><strong>生産性向上 ～ 雲マーク <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0efd904970c-pi" style="float: right;"><img alt="Revision_cloud_presets" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0efd904970c img-responsive" src="/assets/image_331358.jpg" style="margin: 0px 0px 5px 5px;" title="Revision_cloud_presets" /></a></strong></p>
<p style="padding-left: 30px;">雲マークを作図する際にプリセットが用意され、リボン インタフェースから直接希望する雲マークを作図することが出来るようになりました。選択可能なプリセットは、矩形上、ポリゴン状、フリーハンドの 3 種類です。いままで通り、作図済のオブジェクトを選択して、雲マークに変更することも出来ます。</p>
<p style="padding-left: 30px;">作図した雲マークの編集では、雲マーク選択時に表示されるグリップが、プリセット形状によって適切な場所と数で表示されるようになっています。従来のバージョンでは、雲マークのふくらみセグメント毎に、すべてグリップが表示されていました。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7664f9f970b-pi" style="display: inline;"><img alt="Revision_cloud_grips" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7664f9f970b image-full img-responsive" src="/assets/image_290806.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Revision_cloud_grips" /></a></p>
<p style="padding-left: 30px;">また、オートデスク ユーザ会 AUGI の要望を取り入れた新しい編集オプションを利用することで、作図済の雲マーク境界を簡単に変更することも出来ます。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7665011970b-pi" style="display: inline;"><img alt="Revision_cloud_modification" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7665011970b image-full img-responsive" src="/assets/image_724007.jpg" title="Revision_cloud_modification" /></a>&#0160;</p>
<p><strong>生産性向上 ～ コマンド プレビュー</strong></p>
<p style="padding-left: 30px;">AutoCAD 2015 では、編集コマンドの内容を示すカーソル バッチがコマンド実行中のマウスカーソル右上に表示され、同時に編集後の状態を事前のプレビューさせて知らせるコマンド プレビュー機能が追加されています。これらは、TRIM[トリム]、EXTEND[延長]、BREAK[部分削除]、OFFSET[オフセット]、FILLET[フィレット]、CHAMFER[面取り] で採用されていました。&#0160;</p>
<p style="padding-left: 30px;">AutoCAD 2016 では、カーソル バッチとコマンド プレビューの機能が、MOVE[移動]、SCALE[尺度変更]、HATCH[ハッチング]、ERASE[削除]、BLEND[ブレンド曲線]、STRETCH[ストレッチ]、MIRROR[鏡像] の各コマンドにも実装されています。</p>
<p style="padding-left: 30px; text-align: center;">&#0160;<iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/8Tmqow-VEy8?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="padding-left: 30px; text-align: left;">コマンド プレビューの機能によって、作図後に処理をアンドゥしたり、図形を削除して、再度、コマンドを実行する二度手間を省くことで、生産性を向上が実現されるのは言うまでもありません。</p>
<p style="padding-left: 30px; text-align: left;">なお、カーソル バッチの表示有無は、システム変数&#0160;CURSORBADGE でコントロールすることが出来ます。</p>
<p><strong>生産性向上 ～ マルチテキスト文字の文字枠</strong></p>
<p style="padding-left: 30px;">オートデスク ユーザ会 AUGI からの要望に応えるため、マルチ テキストに新しい[文字枠] プロパティが追加されました。このプロパティ値を「はい」にすると、マルチテキスト文字を囲む境界を表示します。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f0826e970c-pi" style="display: inline;"><img alt="Mtext_frame" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f0826e970c image-full img-responsive" src="/assets/image_623583.jpg" title="Mtext_frame" /></a></p>
<p style="padding-left: 30px;">[文字枠] プロパティを「はい」に設定したマルチテキスト文字を含む図面ファイルを、AutoCAD 2015 以前のバージョンで開くと、閉じたポリラインがマルチテキスト文字と独立した状態で表示されます。</p>
<p><strong>生産性向上 ～ ポリラインの図芯スナップ</strong></p>
<p style="padding-left: 30px;">[作図補助設定] ダイアログの [オブジェクト スナップ] タブに「図芯」 スナップが新設されて、閉じたポリラインの中心にスナップすることが出来るようになりました。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c766f728970b-pi" style="display: inline;"><img alt="Polyline_center" class="asset  asset-image at-xid-6a0167607c2431970b01b7c766f728970b img-responsive" src="/assets/image_605000.jpg" style="width: 420px;" title="Polyline_center" /></a></p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f0855f970c-pi" style="display: inline;"><img alt="Polyline_center1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f0855f970c image-full img-responsive" src="/assets/image_904565.jpg" title="Polyline_center1" /></a></p>
<p style="padding-left: 30px;">今回のバージョンで改良された雲マークもポリライン オブジェクトとして作図されることになるので、図芯を利用したオブジェクト スナップが有効です。これらの機能も、オートデスク ユーザ会 AUGI からの要望で実現したものです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c766f82e970b-pi" style="display: inline;"><img alt="Polyline_center2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c766f82e970b image-full img-responsive" src="/assets/image_823630.jpg" title="Polyline_center2" /></a></p>
<p>次回は、「接続性」と題されている設計ワークフローに関係する新機能やその他の機能についてご紹介しましょう。</p>
<p>By Toshiaki Isezaki&#0160;</p>
<p>&#0160;</p>
