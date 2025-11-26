---
layout: "post"
title: "AutoCAD 2017 の新機能 ～ その1"
date: "2016-03-23 01:09:15"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/03/new-features-on-autocad-2017-part1.html "
typepad_basename: "new-features-on-autocad-2017-part1"
typepad_status: "Publish"
---

<p>今年も AutoCAD 2017 と AutoCAD LT 2017 がリリースされましたので、今回から数回にわたって新バージョンの更新点や新機能をご紹介していきます。AutoCAD 2017、AutoCAD LT 2017 のサブスクリプション メンバーには、AutoCAD 360 Mobile Pro の利用権限が提供されますので、その点にも言及します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1b0fd90970c-pi" style="display: inline;"><img alt="Hero_images" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1b0fd90970c image-full img-responsive" src="/assets/image_138918.jpg" title="Hero_images" /></a></p>
<p>いままでとは大きく異なるのが、ライセンスに対する考え方です。既に&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2016/01/end-of-perpetual-license-and-future-choices.html" rel="noopener noreferrer" target="_blank">ご案内</a>&#0160;しているとおり、今年の新バージョンから、永久ライセンスの新規購入オプションがなくなり、すべての製品がサブスクリプションによる期間限定ライセンスに移行しています。この変更に伴い、いくつかの用語や概念が変更されていますので、まずは、その説明をしておきたいと思います。</p>
<p><strong>ライセンス形態</strong></p>
<p style="padding-left: 30px;">サブスクリプションで入手できるライセンスには、<strong>シングルユーザー ライセンス</strong> と <strong>マルチユーザー ライセンス</strong> の 2 つが用意されています。これらの違いを簡単にまとめると、次のようになります。</p>
<p style="padding-left: 30px;"><strong>シングルユーザー ライセンス</strong></p>
<ul>
<li>旧スタンドアロン ライセンス（<a href="http://adndevblog.typepad.com/technology_perspective/2015/05/about-desktop-subscription-products.html" rel="noopener noreferrer" target="_blank">Desktop&#0160;Subscription</a>&#0160;相当）</li>
<li>旧バージョンからの Maintenance Subscription 利用を含む</li>
<li>30日間の体験版利用が可能（従来から変更なし）</li>
<li>初回起動時と30日毎にインターネット接続が必須</li>
</ul>
<p style="padding-left: 30px;"><strong>マルチユーザー ライセンス</strong>&#0160;</p>
<ul>
<li>旧ネットワーク ライセンス</li>
<li>ローカル ネットワーク内でライセンスを管理</li>
<li>ライセンス サーバーに ADLM のインストールが必要</li>
</ul>
<p style="padding-left: 30px;">永久ライセンスの販売停止に関連する FAQ は、<strong><a href="http://www.autodesk.co.jp/products/perpetual-licenses/perpetual-licenses-faq#overview-7" rel="noopener noreferrer" target="_blank">こちら</a> </strong>に記載されていますので、 念のために参照されることをお勧めします。</p>
<p style="padding-left: 30px;">なお、シングルユーザー ライセンス、マルチユーザー ライセンスとも、実際に AutoCAD/AutoCAD LT をお使いいただくコンピュータには、製品をインストールする必要があるのは従来と同様です。今回の変更は、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/05/use-type-of-cloud.html" rel="noopener noreferrer" target="_blank">クラウドによるライセンス管理</a></strong> が、デスクトップ製品にも適用されたと考えていただくと、分かりやすいかと思います。</p>
<p><strong>シリアル番号</strong>&#0160;</p>
<p style="padding-left: 30px;">AutoCAD 2017、AutoCAD LT 2017 共に、従来通り、製品の利用時にシリアル番号が必要です。特に、シングルユーザー ライセンスでは、30日毎でオンライン認証が行われるので、インターネット接続と同時に正当なシリアル番号が必須になります。</p>
<p style="padding-left: 30px;">前バージョンまでの処理と大きく異なるのは、シリアル番号の指定方法です。従来、製品のインストール時にシリアル番号を指定していたはずです。AutoCAD 2017 や AutoCAD LT 2017 では、インストール時にプロダクトキーとシリアル番号を入力する画面は表示されません。その代わり、<span style="text-decoration: underline;"><strong>製品の初回起動時</strong></span>に次の [ライセンスの種類を変更] ダイアログが表示されるので、シングルユーザー ライセンスのサブスクリプションメンバーの場合には、「シリアル番号を入力」ボタンからプロダクトキーとシリアル番号を入力してください。入力後、オンライン認証が実行されて、正当なシリアル番号が確認されると製品が起動して、利用可能な状態になります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c826c4cc970b-pi" style="display: inline;"><img alt="Choose_license_type" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c826c4cc970b image-full img-responsive" src="/assets/image_245603.jpg" title="Choose_license_type" /></a></p>
<p style="padding-left: 30px;">もし、誤ったシリアル番号を入力をしてしまった場合には、次のようなエラーが表示されて、AutoCAD は強制的にシャットダウンしてしまいます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1b150d8970c-pi" style="display: inline;"><img alt="License_error" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1b150d8970c img-responsive" src="/assets/image_177106.jpg" title="License_error" /></a></p>
<p style="padding-left: 30px;">初回起動時には、マルチユーザー ライセンスを選択することもできます。マルチユーザー ライセンスのサブスクリプション メンバーは、「ネットワーク ライセンスを使用」ボタンをクリックすることで、ライセンス サーバーの形態や名前を入力する画面に遷移します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08cb50b0970d-pi" style="display: inline;"><img alt="Choose_nlm" class="asset  asset-image at-xid-6a0167607c2431970b01bb08cb50b0970d img-responsive" src="/assets/image_879817.jpg" title="Choose_nlm" /></a></p>
<p style="padding-left: 30px;">なお、マルチユーザ ライセンスの管理には、従来どおり、Network License Manager(LMTOOLS Utility) が必要です。管理者の方は、Network License Manager を AutoCAD 2017 に同梱される新しいバージョンに入れ替える必要があります。もちろん、適切なライセンス ファイルが必要になります。</p>
<p style="padding-left: 30px;">もし、契約しているタイプが シングルユーザー ライセンス か マルチユーザー ライセンス か不明な場合、または、正当なシリアル番号がわからない場合には、みなさんの会社の契約管理者か、購入された販売店にご相談ください。</p>
<p><strong>ライセンス マネージャー</strong></p>
<p style="padding-left: 30px;">製品利用時には、いつでも使用中のライセンス情報を確認することが出来ます。右上に表示されてくる [ライセンスを管理] メニューを選択すると、ライセンス マネージャ画面を表示します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c826c6be970b-pi" style="display: inline;"><img alt="Launch_license_manager" class="asset  asset-image at-xid-6a0167607c2431970b01b7c826c6be970b img-responsive" src="/assets/image_727607.jpg" title="Launch_license_manager" /></a></p>
<p style="padding-left: 30px;">ライセンス マネージャーといっても、マルチユーザー ライセンス（ネットワーク ライセンス形態）のライセンス管理で使用する&#0160;Network License Manager(LMTOOLS Utility)のことではありません。このライセンス マネージャーでは、前述のシリアル番号を表示するだけではなく、契約更新にともなうシリアル番号の更新なども可能です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c826c6e3970b-pi" style="display: inline;"><img alt="New_license_manager" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c826c6e3970b image-full img-responsive" src="/assets/image_764019.jpg" title="New_license_manager" /></a></p>
<p style="padding-left: 30px;">また、「ライセンスの種類を変更」をクリックすると、図面保存を促すメッセージ表示の後に AutoCAD がシャットダウンします。次に起動する際には、初回起動時と同じ [ライセンスの種類を変更] ダイアログが表示されるので、シングルユーザー ライセンス か マルチユーザー ライセンス かを指定することも出来るようになっています。正当なサブスクリプション契約があれば、シングルユーザー ライセンス から マルチユーザー ライセンス へ切り替えるようなことも可能です。</p>
<p><strong>&#0160;ライセンス転送ユーティリティ</strong></p>
<p style="padding-left: 30px;">AutoCAD &#0160;2017/AutoCAD LT 2017 をインストールすると、従来のバージョンと同様に、[スタート] メニューに [ライセンス転送ユーティリティ] がインストールされます。</p>
<p style="padding-left: 90px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1b163d4970c-pi" style="display: inline;"><img alt="Ltu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1b163d4970c image-full img-responsive" src="/assets/image_327408.jpg" title="Ltu" /></a></p>
<p style="padding-left: 30px;">ただし、ライセンス転送ユーティリティで他のコンピュータにライセンスを転送 出来るのは、旧バージョンの永久ライセンスで Maintenance Subscription 契約をお持ちの方が 2017 バージョンをインストールした場合のみです。シングルユーザー ライセンスでインストールされた 2017 製品でこのユーティリティを使ってライセンス転送しようとすると、次にようなエラーが表示されて、ライセンスは転送出来ません。これは、従来の Desktop Subscription の制限と同様です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08cced30970d-pi" style="display: inline;"><img alt="Ltu_error" class="asset  asset-image at-xid-6a0167607c2431970b01bb08cced30970d img-responsive" src="/assets/image_399895.jpg" title="Ltu_error" /></a></p>
<p>さて、ライセンスに関係する変更点に終始してしまいましたが、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/new-features-on-autocad-2017-part2.html" rel="noopener noreferrer" target="_blank">次回</a>&#0160;</strong>からは、いよいよ新機能についてご紹介していきます。</p>
<p>By Toshiaki Isezaki</p>
