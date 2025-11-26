---
layout: "post"
title: "AutoCAD 2019 の新機能 ～ その3"
date: "2018-04-04 01:46:48"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/04/new-features-on-autocad-2019-part3.html "
typepad_basename: "new-features-on-autocad-2019-part3"
typepad_status: "Publish"
---

<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/04/new-features-on-autocad-2019-part2.html" rel="noopener noreferrer" target="_blank">前回</a></strong>、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/03/new-features-on-autocad-2019-part1.html" rel="noopener noreferrer" target="_blank">前々回</a></strong>に続いて、AutoCAD 2019/AutoCAD LT 2019 の新機能や改良された機能をご紹介したいと思います。</p>
<p><strong>図面化</strong></p>
<p style="padding-left: 30px;"><strong>ビューとビューポート</strong></p>
<p style="padding-left: 30px;">このバージョンでは、モデル空間、ペーパー空間（レイアウト）を問わず、名前の付いたビューを再利用する機能が加えられています。まず、名前のついたビューを登録する際に、不必要な情報設定を隠蔽するよう、[新しいビュー/ショットのプロパティ] ダイアログ ボックスが簡素化されています。登録するビューの表示スタイルや背景を設定したり、ビューの呼び出し時にアニメーション効果を設定したりする場合には、<strong>∨</strong> ボタンをクリックして、詳細情報を展開するようになっています。なお、AutoCAD LT 2019 では、 [新しいビュー/ショットのプロパティ] ダイアログ ボックスの詳細情報設定はありません。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9596344970b-pi" style="display: inline;"><img alt="Register_named_view" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c9596344970b image-full img-responsive" src="/assets/image_897467.jpg" title="Register_named_view" /></a></p>
<p style="padding-left: 30px;">また、ビューの呼び出し操作にも一貫性を持たせています。従来、モデル空間では、作図ウィンドウの左上に表示されるビューポート コントロールで、登録済の名前の付いたビューを切り替えることが出来ました。ただ、このビューポート コントロールはペーパー空間では利用することが出来ませんでした。AutoCAD 2019/AutoCAD LT 2019 では、[表示] リボン タブに [名前の付いたビュー] リボン パネルが新設されて、モデル空間、ペーパー空間問わす、同じようにビューを呼び出すことが出来ます。 [名前の付いたビュー] リボン パネルにリストされるビュー名は、モデル空間、ペーパー空間別に選別されて表示されます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e3a0e1970c-pi" style="display: inline;"><img alt="View_change" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e3a0e1970c image-full img-responsive" src="/assets/image_889292.jpg" title="View_change" /></a></p>
<p style="padding-left: 30px;">モデル空間で登録した名前の付いたビューは、ペーパー空間上に簡単に配置することが出来るようになっています。配置したビューポートは、グリップ操作で位置や大きさを変更出来るばかりでなく、ビューポート尺度を設定することも可能になりました。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09fc9699970d-pi" style="display: inline;"><img alt="Viewport" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09fc9699970d image-full img-responsive" src="/assets/image_743399.jpg" title="Viewport" /></a></p>
<p style="padding-left: 30px;">ここまでの内容を動画にまとめていますので、ご確認ください。</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/lelzjd-jRkE?feature=oembed" width="500"></iframe></p>
<p class="asset-video" style="padding-left: 30px;"><strong>優先画層プロパティ</strong></p>
<p style="padding-left: 30px;">AutoCAD でプリンタやプロッターに図面を出力する場合には、ペーパー空間（レイアウト）にビューポート（メタビュー）を複数配置して、尺度とともに印刷される紙図面の出力イメージを調整します。このとき、ビューポートはモデル空間に作図されているデザインを、レイアウト空間に表現する領域と考えることが出来ます。</p>
<p style="padding-left: 30px;">AutoCAD 2008 で導入された<strong>画層優先プロパティ</strong>（ビューポート毎の画層設定）の機能を利用すると、複数のビューポートをレイアウト配置した場合に、ビューポート毎に線の太さや線種、色といった画層に付帯するプロパティを優先（上書き）して、表現出来るようになります。この機能については、Autodesk Knowledge Network 記事の&#0160;<strong><a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2016/JPN/AutoCAD-Core/files/GUID-04F71808-2FFA-4BFE-A3C5-7FD418A63B4C-htm.html" rel="noopener noreferrer" target="_blank">概要 - レイアウト ビューポートで画層のプロパティを優先させる</a></strong>&#0160;をご確認ください。</p>
<p style="padding-left: 30px;">今回のバージョンでは、レイアウトに配置したビューポートに外部参照図面が含まれていて、かつ、優先画層プロパティが設定されている場合、 [画層プロパティ管理] パレットでの状況把握がし易くなるよう改良が加えられています。例えば、マウスカーソルを該当する画層名上にホバーさせるとツールチップが現在の状態を表示するようになります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e3dcae970c-pi" style="display: inline;"><img alt="Viewport_layer1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e3dcae970c image-full img-responsive" src="/assets/image_793566.jpg" title="Viewport_layer1" /></a></p>
<p style="padding-left: 30px;">また、[画層プロパティ管理] パレットで背景色を利用して優先画層プロパティが適用された画層を判別する際、外部参照図面の画層を区別して表示出来る設定が新設されています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9599fca970b-pi" style="display: inline;"><img alt="Viewport_layer_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c9599fca970b image-full img-responsive" src="/assets/image_504199.jpg" title="Viewport_layer_settings" /></a></p>
<p style="padding-left: 30px;">これら機能によって、優先画層プロパティを持つ画層や、どのプロパティが優先されているかを理解し易くなっています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09fcd1f8970d-pi" style="display: inline;"><img alt="Viewport_layer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09fcd1f8970d image-full img-responsive" src="/assets/image_875439.jpg" title="Viewport_layer" /></a></p>
<p>この他にも、AutoCAD 2019 にはグラフィックス パフォーマンスの向上などの改良が加えられています。詳しくは <strong><a href="http://adndevblog.typepad.com/files/autocad-2019-%E3%83%97%E3%83%AC%E3%83%93%E3%83%A5%E3%83%BC%E3%82%AC%E3%82%A4%E3%83%89.pdf" rel="noopener noreferrer" target="_blank">AutoCAD 2019 プレビュー ガイド</a></strong> を確認してみてください。</p>
<p>また、AutoCAD 2019 と AutoCAD LT 2019 の機能については <a href="http://adndevblog.typepad.com/files/autocad-2019-autocad-lt-2019-comparison-matrix-ja.pdf" rel="noopener noreferrer" target="_blank"><strong>AutoCAD 2019 - AutoCAD LT 2019 機能比較</strong></a>&#0160;を、AutoCAD の過去バージョンとの機能差については <a href="http://adndevblog.typepad.com/files/autocad-2019-release-comparison-matrix-ja.pdf" rel="noopener noreferrer" target="_blank"><strong>旧リリースとの機能比較</strong></a>&#0160;を、それぞれダウンロードしてご確認いただけます。</p>
<p>By Toshiaki Isezaki&#0160;&#0160;</p>
