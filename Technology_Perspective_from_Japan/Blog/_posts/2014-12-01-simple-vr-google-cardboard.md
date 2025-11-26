---
layout: "post"
title: "AU 2014 - 簡単 VR - Google Cardboard"
date: "2014-12-01 23:03:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/12/simple-vr-google-cardboard.html "
typepad_basename: "simple-vr-google-cardboard"
typepad_status: "Publish"
---

<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d09f4eac970c-pi" style="display: inline;"><img alt="Autodesk-university-2014-logo-1-line-color-black-web-large" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d09f4eac970c image-full img-responsive" src="/assets/image_315678.jpg" title="Autodesk-university-2014-logo-1-line-color-black-web-large" /></a>&#0160;&#0160;</p>
<p>現在、ラスベガスで開催されている米国の Autodesk University に来ていますので、今週は、いくつか AU の話題をお知らせします。米国の Autodesk University&#0160;は、日本とは異なり、1 日で終了ではなく、3日間、午前 8 時から午後 6 ～ 7 時くらいまで連続して複数のセッションが開催される「学びの場」になっています。</p>
<p>といっても、Autodesk University が開催されるのは12月2日からでで、現在（現地時間の1日）は、前日の Developer Days のお話です。日本では、東京と大阪で Developer Days の開催をすでに終了していますが、オートデスク本社のある米国では、毎年、この時期に AU の事前イベントとして開催されています。</p>
<p style="text-align: center;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7155914970b-pi" style="display: inline;"><img alt="Au2014" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7155914970b image-full img-responsive" src="/assets/image_22771.jpg" title="Au2014" /></a></p>
<p>Autodesk University の規模も日本とはけた違いですが、Developer Days も同様です。申し込み数ベースの規模比較になりますが、東京、大阪合わせた数が 100 名少しなのに対し、米国では 700 人超といった具合です。</p>
<p>さて、日本の Developer Days でも、もちろんご案内していますが、 今年の目玉は、初めての Web サービス API となっている View and Data API です。機能などは、他のブログ記事に記載していますので、ここでは、View and Data API を利用した安価なバーチャル リアリティ機材をご紹介しましょう。</p>
<p>今年、Google 社が開催した開発者イベント、<a href="https://www.google.com/events/io" target="_blank"><strong>Google I/O</strong></a> で登場したテクノロジに <a href="https://developers.google.com/cardboard/" target="_blank"><strong>Google Cardboard</strong></a> というものがあります。Cardboard とはボール紙などの厚紙のことですが、この厚紙を使ってバーチャル リアリティに利用するグラスを作成しようとするものです。そして、Android スマートフォンに左右、視差のある画像を投影することで立体視を実現します。Google 社は、投影する画像を制御するための VR Toolkit も発表していますが、Autodesk Developer Days では、このコンテンツを&#0160;View and Data API で作ってしまった例をご紹介しています。</p>
<p>ハード面では、厚紙のキットを <a href="http://www.dodocase.com/products/google-cardboard-vr-goggle-toolkit" target="_blank">DODOCASE</a> から購入して参加者に配布しています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7104be6970b-pi" style="display: inline;"><img alt="Gcb_lmv2" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7104be6970b img-responsive" src="/assets/image_518264.jpg" style="width: 400px;" title="Gcb_lmv2" /></a></p>
<p>25 ドル程度のボール紙と手持ちのスマートフォンでバーチャルリアリティが実現できるということで疑心暗鬼でしたが、実際に使ってみると本当に立体に見えるので、少々驚きです。</p>
<p style="text-align: center;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07b5530f970d-pi" style="display: inline;"><img alt="Gcb_lmv3" class="asset  asset-image at-xid-6a0167607c2431970b01bb07b5530f970d img-responsive" src="/assets/image_64971.jpg" style="width: 400px;" title="Gcb_lmv3" /></a></p>
<p>実際の詳細は、同僚のブログ記事を参照してみてください。</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-1.html" target="_blank"><strong>Creating a stereoscopic viewer for Google Cardboard using the Autodesk 360 viewer – Part 1</strong></a></p>
<p><strong><a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-2.html" target="_blank">Creating a stereoscopic viewer for Google Cardboard using the Autodesk 360 viewer – Part 2</a></strong></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/creating-a-stereoscopic-viewer-for-google-cardboard-using-the-autodesk-360-viewer-part-3.html" target="_blank"><strong>Creating a stereoscopic viewer for Google Cardboard using the Autodesk 360 viewer – Part 3</strong></a></p>
<p>この VR コンテンツは、音声認識によって、ビューの変更を指示することが出来ます。例えば、「エクスプロード」と命令すると表示されているモデルが分解表示されたり、「リセット」を命令すると分解表示されたモデルが、元の表示状態に戻って表示される、といった具合です。</p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2014/10/adding-speech-recognition-to-our-stereoscopic-google-cardboard-viewer.html" target="_blank"><strong>Adding speech recognition to our stereoscopic Google Cardboard viewer</strong></a></p>
<p><a href="http://through-the-interface.typepad.com/through_the_interface/2014/11/adding-more-speech-recognition-to-our-stereoscopic-google-cardboard-viewer.html" target="_blank"><strong>Adding more speech recognition to our stereoscopic Google Cardboard viewer</strong></a></p>
<p>View and Data API は、HTML5 と WebGL が有効な Web ブラウザがあれば利用出来ますので、モバイルの世界でもオートデスクの Web サービス API が有効である、ということが分かります。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
