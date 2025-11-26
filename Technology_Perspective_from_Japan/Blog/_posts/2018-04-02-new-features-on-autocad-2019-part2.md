---
layout: "post"
title: "AutoCAD 2019 の新機能 ～ その2"
date: "2018-04-02 00:05:23"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/04/new-features-on-autocad-2019-part2.html "
typepad_basename: "new-features-on-autocad-2019-part2"
typepad_status: "Publish"
---

<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/03/new-features-on-autocad-2019-part1.html" rel="noopener noreferrer" target="_blank">前回</a></strong>に引き続き、AutoCAD 2019/AutoCAD LT 2019 の新機能についてご紹介していきます。まずは、関係者間で図面内容をチェック・レビューする際に便利なコラボレーション機能です。</p>
<p><strong>コラボレーション</strong></p>
<p style="padding-left: 30px;"><strong>図面比較</strong></p>
<p style="padding-left: 30px;">微細な変更を設計繰り返して図面に複数のリビジョンが存在する場合、両者間の差異を見つけ出すのは容易ではありません。AutoCAD 2019（AutoCAD LT 2019）では、このような図面間のジオメトリの差を検出して表示する機能が新設されました。見つけ出した違いは雲マークで囲み、図面1 と 図面2 の差を異なる色で表現するので、小さな違いも判別しやすくなるよう工夫されています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9592715970b-pi" style="display: inline;"><img alt="Drwing_compare" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c9592715970b image-full img-responsive" src="/assets/image_113003.jpg" title="Drwing_compare" /></a></p>
<p style="padding-left: 30px;">具体的に 2 つの図面の比較する際には、図面1 と 図面2 の指定と同時に両者を識別する色指定をおこないます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09fc5a04970d-pi" style="display: inline;"><img alt="Drwing_compare1" class="asset  asset-image at-xid-6a0167607c2431970b01bb09fc5a04970d img-responsive" src="/assets/image_740136.jpg" style="width: 600px;" title="Drwing_compare1" /></a></p>
<p style="padding-left: 30px;">比較が開始されると、まず、2 図面を合成した図面が新規に作成され、雲マークで差異が表示されることになります。図面はそのまま保存することが出来るので、関係者に違いを明示することが可能です。比較した2 図面のファイル名や作成日付などの基本情報は、表オブジェクトとして作図したり、クリップボードにコピーしてメールに貼り付けたりすることも出来ます、実際の手順を動画にしていますのでご確認ください。</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/DZq134endXc?feature=oembed" width="500"></iframe></p>
<p class="asset-video" style="padding-left: 30px;"><strong>共有ビュー<a id="shared_view"></a></strong></p>
<p style="padding-left: 30px;">AutoCAD 2017 で <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/04/new-features-on-autocad-2017-part3.html" rel="noopener noreferrer" target="_blank">ライブレビュー</a></strong> として導入され、AutoCAD 2018 で強化された <strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/03/new-features-on-autocad-2018-part3.html" rel="noopener noreferrer" target="_blank">デザイン ビュー</a></strong> の機能が、分かりやすく <strong>共有ビュー</strong> の名前に変更されて引き続き強化されています。誰でも利用出来るオンラインの <strong><a href="https://viewer.autodesk.com/" rel="noopener noreferrer" target="_blank">Autodesk ビューア</a></strong>（旧名：A360 ビューア）を使って、関係者とデザインを共有する機能を提供します。Autodesk ビューアは、AutoCAD 図面以外にも、2D、3D を問わず、多様な CAD のデザインファイルをストリーミング配信で Web ブラウザに表示する、オートデスクの Forge テクノロジを利用したオンライン ビューアです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e35e33970c-pi" style="display: inline;"><img alt="Autodesk_viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e35e33970c image-full img-responsive" src="/assets/image_648994.jpg" title="Autodesk_viewer" /></a></p>
<p style="padding-left: 30px;">共有ビューはクラウドにアプロードされて、読み取り専用に変換された図面を Web ブラウザにストリーミング配信することで、安全にリアルタイム コラボレーション機能です。Web ブラウザさえあれば、遠隔地にいる関係者とコメントやマークアップを残しながらコラボレーションすることが可能になります。</p>
<p style="padding-left: 30px;">共有した読み取り図面は、アップロードから 30 日間経過すると自動的に削除されて閲覧できないようになるので、セキュアな状態を保つことが出来るだけでなく、従来、印刷された紙図面や PDF ファイルで実施していた作業を代替することが出来ます。</p>
<p style="padding-left: 30px;">今回の AutoCAD 2019/AutoCAD LT 2019 では、AutoCAD 内から [共有ビュー] パレットを使って 30 日間の共有期間を延長したり、コラボレーションの終了にあわせた図面の削除を実行したりする機能が追加されています。これらの作業は、前バージョンの AutoCAD 2018 ではブラウザ側でしか出来ませんでした。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c959288f970b-pi" style="display: inline;"><img alt="Shared_view_palette" class="asset  asset-image at-xid-6a0167607c2431970b01b7c959288f970b img-responsive" src="/assets/image_572059.jpg" title="Shared_view_palette" /></a></p>
<p style="padding-left: 30px;">一連の内容を動画にしていますのでご確認ください。&#0160;</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/BBMw2fAMo_0?feature=oembed" width="500"></iframe></p>
<p class="asset-video" style="padding-left: 30px;"><strong>Web、モバイルとの作業</strong></p>
<p class="asset-video" style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2018/03/only-one-autocad-2.html"><strong>Only One AutoCAD - モバイル アプリと Web アプリ</strong></a>&#0160;で触れた AutoCAD モバイル アプリと AutoCAD Web アプリで作業をするために、AutoCAD 2019 と AutoCAD LT 2019 にはクイック アクセス ツールバーとアプリケーション メニュー内に <a href="http://help.autodesk.com/view/ACD/2019/JPN/?guid=GUID-79C33702-07E6-4261-92E1-D69595ED1B6D" rel="noopener noreferrer" target="_blank"><strong>[Web およびモバイルから開く]</strong></a> と <a href="http://help.autodesk.com/view/ACD/2019/JPN/?guid=GUID-A7507398-50B1-4B00-B79B-EB99A068DACA" rel="noopener noreferrer" target="_blank"><strong>[Web およびモバイルに保存]</strong></a> のコマンドがそれぞれ新規に追加されています。</p>
<p class="asset-video" style="padding-left: 30px;">これらのコマンドを使用すると、AutoCAD や AutoCAD LT にサインイン中のアカウントで A360ドライブから図面を開いたり、図面を保存することが出来ます。AutoCAD モバイル アプリと AutoCAD Web アプリは A360 ドライブ内の図面を開いて編集、保存するので、このコマンドを利用することで、AutoCAD モバイル アプリと AutoCAD Web アプリとの連携が可能になります。</p>
<p class="asset-video" style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c95edac2970b-pi" style="display: inline;"><img alt="Web_mobile" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c95edac2970b image-full img-responsive" src="/assets/image_210909.jpg" title="Web_mobile" /></a></p>
<p class="asset-video" style="padding-left: 30px;">なお、[Web およびモバイルから開く] コマンドと [Web およびモバイルに保存] コマンドは、プラグイン(アドイン) アプリケーションで実装されています。このアドイン アプリケーションは AutoCAD 2019 や AutoCAD LT 2019 のインストール時にはインストールされず、両者のコマンドを始めて実行した際にインストールされるので注意してください。インストール時には、同意を求める次のダイアログが表示されるので、適宜、App Store エンドユーザ使用許諾契約をご確認の上、同チェックボックスにチェックして [インストール] ボタンでインストールしてください。</p>
<p class="asset-video" style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e9194e970c-pi" style="display: inline;"><img alt="Web_mobile_app_install" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e9194e970c img-responsive" src="/assets/image_479734.jpg" title="Web_mobile_app_install" /></a></p>
<p class="asset-video" style="padding-left: 30px;">なお、[AutoCAD Web およびモバイルに保存] アプリケーションは、Windows コントロール パネルからいつでもアンインストールすることが出来ます。</p>
<p class="asset-video" style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0a01ff11970d-pi" style="display: inline;"><img alt="Save_web_mobile" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0a01ff11970d image-full img-responsive" src="/assets/image_559470.jpg" title="Save_web_mobile" /></a></p>
<p class="asset-video"><strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/04/new-features-on-autocad-2019-part3.html" rel="noopener noreferrer" target="_blank">次回</a></strong>は図面作成時に生産性を向上させる図面化の機能についてご紹介します。</p>
<p>By Toshiaki Isezaki</p>
