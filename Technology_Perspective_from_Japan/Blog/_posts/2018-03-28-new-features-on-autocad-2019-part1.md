---
layout: "post"
title: "AutoCAD 2019 の新機能 ～ その1"
date: "2018-03-28 04:10:23"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/03/new-features-on-autocad-2019-part1.html "
typepad_basename: "new-features-on-autocad-2019-part1"
typepad_status: "Publish"
---

<p>誕生から 36 年を迎え、AutoCAD の新バージョン、AutoCAD 2019(AutoCAD LT 2019) がリリースされました。この AutoCAD を含め、オートデスクはデスクトップ製品の永久ライセンスの販売を終了していますので、このバージョンも&#0160;<strong><a href="https://www.autodesk.co.jp/buy-online" rel="noopener noreferrer" target="_blank">サブスクリプション</a>&#0160;</strong>のみの販売となります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e2e966970c-pi" style="display: inline;"><img alt="36_years_history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e2e966970c image-full img-responsive" src="/assets/image_244815.jpg" title="36_years_history" /></a></p>
<p>サポートされる AutoCAD 2019 の動作環境は次のとおりです。近年販売されている Windows コンピュータは、ほぼ 64 ビット CPU で占められているので、最新 Windows OS である Windows 10 では 32 ビット環境はサポートされていない点にご注意ください。</p>
<ul>
<li><strong>Windows 7</strong> <strong>SP1</strong>
<ul>
<li>下記エディションの 32 ビット、及び 64 ビット版</li>
<li>Enterprise、Ultimate、Professional、Home Premium</li>
</ul>
</li>
<li><strong>Windows 8.1</strong> ＋ <a href="https://www.microsoft.com/ja-jp/download/details.aspx?id=42327">KB2919355</a> <a href="https://www.microsoft.com/ja-jp/download/details.aspx?id=42327">Update</a>
<ul>
<li>下記エディションの 32 ビット、及び 64 ビット版</li>
<li>（標準）、Pro、Enterprise</li>
</ul>
</li>
<li><strong>Windows 10</strong> <strong>Anniversary Update</strong> (version 1607 以降)
<ul>
<li>下記エディションの 64 ビット版</li>
<li>Home、Pro、Enterprise、Education</li>
<li><strong>32</strong><strong> ビット版のサポートはなし</strong></li>
</ul>
</li>
</ul>
<p>AutoCAD 2019 の図面ファイル形式は、AutoCAD 2018 同様、引き続き <strong>2018 図面ファイル形式&#0160;</strong>をサポートしています。アドイン アプリケーションは、今回、バイナリ非互換リリースとなってしまいます。残念ながら、AutoCAD 2018 用のアドイン アプリケーションは移植作業が必要となります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09fc58d4970d-pi" style="display: inline;"><img alt="Compatibility" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09fc58d4970d image-full img-responsive" src="/assets/image_556788.jpg" title="Compatibility" /></a></p>
<p>それでは、今年も数回に渡って新機能や従来バージョンから改良された機能をご紹介していきたいと思います。</p>
<p><strong>Only One AutoCAD</strong></p>
<p style="padding-left: 30px;">まず、比較的おおきな変更として、いままで個別にサブスクリプションで提供されてきた業種別の AutoCAD 製品が、<strong>AutoCAD including specialized toolsets</strong> の名称で、ツールセットとして AutoCAD サブスクライバの皆様に提供される点が挙げられます。</p>
<p style="padding-left: 30px;">機能というよりは、提供方法の変更ですが、AutoCAD のサブスクリプション価格で、ほぼすべての業種別製品機能が利用出来るので、便利で製品管理が容易になるはずです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e2e89b970c-pi" style="display: inline;"><img alt="One_autocad" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e2e89b970c image-full img-responsive" src="/assets/image_251053.jpg" title="One_autocad" /></a></p>
<p style="padding-left: 30px;">FAQ を含めた詳細は、先のブログ記事&#0160;&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/03/only-one-autocad.html" rel="noopener noreferrer" target="_blank">Only One AutoCAD - 業種別 AutoCAD 製品の変更</a></strong>&#0160;でご案内していますのでご確認ください。</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allow="autoplay; encrypted-media" allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/YKMINAmHdDA?feature=oembed" width="500"></iframe></p>
<p><strong>ユーザ インタフェースの改良</strong></p>
<p style="padding-left: 30px;"><strong>4K ディスプレイ サポート</strong></p>
<p style="padding-left: 30px;">4K ディスプレイ（高解像度ディスプレイ）サポートとして、数バージョン前から取り組んでいるユーザ インタフェースの改良を引き続きおこなっています。高解像度化が進んでいる現状では、ディスプレイ解像度の高精細化にともなって、AutoCAD 内で表示されるボタンなどが比例して小さく表示されてしまい、操作がしにくくなることがありまた。</p>
<p style="padding-left: 30px;">本バージョンでは、対応が後回しになっていた Visual LISP エディタなどにも、対応の手が及ぶようになっています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c958b495970b-pi" style="display: inline;"><img alt="4k_display_support" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c958b495970b image-full img-responsive" src="/assets/image_170090.jpg" title="4k_display_support" /></a></p>
<p style="padding-left: 30px;"><strong>フラットアイコンの採用</strong></p>
<p style="padding-left: 30px;">ボタン内の図柄についていた陰影を取り除いて、高解像度ディスプレイで AutoCAD を実行した際の視認性を向上させています。つまり、ボタン内で陰影に利用していた領域にも図柄を表示出来るようになり、全体に大きな図柄をボタンに表現出来るようになっています。</p>
<p style="padding-left: 30px;">4K ディスプレイ サポートの一環とも言える改良となります。下記の画像をクリックすると、実際の大きさで見た目の違いを把握していただけるはずです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09fbe398970d-pi" style="display: inline;"><img alt="Flat_icons" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09fbe398970d image-full img-responsive" src="/assets/image_94359.jpg" title="Flat_icons" /></a></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/04/new-features-on-autocad-2019-part2.html" rel="noopener noreferrer" target="_blank">次回</a></strong>は新しく追加された機能を含め、コラボレーション機能をご紹介します。</p>
<p>By Toshiaki Isezaki</p>
