---
layout: "post"
title: "Desktop Subscription 製品の導入手順と仕組み"
date: "2015-05-27 00:20:43"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/05/about-desktop-subscription-products.html "
typepad_basename: "about-desktop-subscription-products"
typepad_status: "Publish"
---

<p>オートデスクは、昨年、New York で永久ライセンスの新規販売を 2 年以内に中止し、Desktop Subscription への移行を宣言しています。これを受けて、2 月に日本でも&#0160;<strong><a href="http://www.autodesk.co.jp/adsk/servlet/item?siteID=1169823&amp;id=23983422" target="_blank">プレス リリース</a></strong>&#0160;をしています。曰く、2016 年 2 月 1 日以降に販売する単体ソフトウェア製品を「Desktop Subscription」（デスクトップ・サブスクリプション）でのみ提供する、というものです。</p>
<p>そこで、今回は&#0160;<strong>Desktop Subscription</strong>&#0160;のライセンスの仕組みと導入までの手順を簡単にご紹介しておきたいとます。Desktop Subscription とは、もともと&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2013/09/launching-rental-plan.html" target="_blank"><strong>レンタル ライセンス</strong></a>&#0160;として約 2 年前に導入された製品の運用形態で、名称が変更されているだけで利用されている技術的なメカニズムは同じです。実際に利用する製品は、クライアント コンピュータにインストールして、ライセンスをオートデスクがクラウドで管理するため、<strong>クラウドの利用形態について</strong>&#0160;で紹介した&#0160;<strong>クラウドによるライセンス管理</strong>&#0160;にあたります。</p>
<p><strong>Desktop Subscription のライセンス形態</strong>&#0160;</p>
<p style="padding-left: 30px;">利用するライセンス形態は、2015年5月1日現在、<strong>スタンドアロン ライセンス</strong>となります。近い将来、ネットワーク ライセンスの Desktop Subscription が導入される予定ですが、時期は未定です。オートデスク製品のライセンス形態については、過去のブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2013/07/license-types-for-desktop-products.html" target="_blank"><strong>オートデスク製品のライセンス種別と考え方</strong></a>&#0160;を参考にしてみてください。</p>
<p><strong>用語の整理</strong></p>
<p style="padding-left: 30px;">Desktop Subscription の説明をする前に、1 つだけ用語の整理が必要です。ここで言う用語とは、<strong>契約管理者</strong>&#0160;と&#0160;<strong>指名ユーザ</strong>&#0160;の 2つです。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0834fe64970d-pi" style="display: inline;"><img alt="Terminology" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0834fe64970d image-full img-responsive" src="/assets/image_536702.jpg" title="Terminology" /></a></p>
<p style="padding-left: 30px;"><strong>契約管理者：</strong></p>
<p style="padding-left: 30px;">契約管理者とは、Autodesk ストアや認定リセラーから Desktop Subscription の契約窓口となる人を指します。契約管理者の主な役割は、Desktop Subscription 契約で得られるライセンスを誰に使用させるか指名する点です。ユーザ管理です。企業で Desktop Subscription&#0160;契約した場合には、ユーザ管理の役割も同時に持つことになります。個人ユーザの場合には、必然的にユーザ自身が契約管理者の役割を担うことになります。</p>
<p style="padding-left: 30px;">契約管理者は、メール通知を受け取るために、電子メールアドレスをオートデスクに知らせる必要があります。オートデスクは、提示された電子メールアドレスに関連付けられた Autodesk ID の有無をチェックして、紐付けされた Autodesk ID がない場合、仮パスワードとともに Autodesk ID を作成します。Autodesk ID は、事前に&#0160;<a href="http://accounts.autodesk.com/" target="_blank">http://accounts.autodesk.com</a>&#0160;から取得することも出来ます。</p>
<p style="padding-left: 30px;"><strong>指名ユーザ：</strong></p>
<p style="padding-left: 30px;">Desktop Subscription ライセンスを実際に使用するユーザです。企業ユーザの場合、契約管理者から指名されて初めて使用権利が与えられるので、「指名ユーザ」と呼ばれます。&#0160;</p>
<p style="padding-left: 30px;">指名ユーザも契約管理者と同様に、メール通知を受け取るためと、ライセンス認証を得るために Autodesk ID が必要です。契約管理者は、指名ユーザの電子メールアドレスを登録してライセンスの利用者を指名しますが、Autodesk ID も登録はおこないません。</p>
<p style="padding-left: 30px;">オートデスクは、契約管理者によって作成された指名ユーザの電子メールアドレスに関連付けられた Autodesk ID の有無をチェックして、紐付けされた Autodesk ID がない場合、仮パスワードとともに Autodesk ID を作成します。Autodesk ID は、事前に&#0160;<a href="http://accounts.autodesk.com/" target="_blank">http://accounts.autodesk.com</a>&#0160;から取得することも出来ます。</p>
<p><strong>Desktop Subscription の特徴と利点&#0160;</strong></p>
<p style="padding-left: 30px;">Desktop Subscription の特徴と利点を簡単にまとめると、次のようになります。初回起動時に必ずインターネット接続が必要になる点にご注ください。なお、利用可能となるクラウド ストレージ サイズやクラウド クレジット数など、技術的な部分とは関係なく、運用ルールは今後変更される可能性がある点はご留意ください。</p>
<div style="padding-left: 30px;">
<ul style="padding-left: 30px;">
<li>契約によって利用可能な期間を限定するスタンドアロン ライセンス（レンタル ライセンス）</li>
<li>選択可能な契約期間：1ヶ月、3ヶ月、1年、2年、3年</li>
<li>既存の永久スタンドアロン ライセンスと CAD 機能は同一</li>
<li>定期的なライセンス認証のため<strong>インターネット接続が必須</strong></li>
<li>オートデスク認定リセラーか Autodesk ストアで購入可能</li>
<li>契約期間内はシート毎に 25 GB の A360 ストレージを利用可</li>
<li>契約期間内は指名ユーザ毎に 100 クラウド クレジットを提供</li>
</ul>
</div>
<p><strong>運用開始までの流れ</strong>&#0160;</p>
<p style="padding-left: 30px;">Desktop Subscription ライセンスの運用までの流れは、おおまかに次のようになります。</p>
<div style="padding-left: 30px;">1.オートデスク認定リセラー、または Autodesk ストアから購入</div>
<div style="padding-left: 30px;">
<ul style="padding-left: 30px;">
<li>契約管理者となる方の電子メール アドレスを入力</li>
<li>電子メール アドレスで登録された Autodesk ID の有無をチェック</li>
</ul>
</div>
<div style="padding-left: 30px;">2.オートデスク アカウントにサインイン<br />3.指名ユーザ（利用者）を登録</div>
<div style="padding-left: 30px;">
<ul>
<li>製品使用権(アセス権）を付与</li>
</ul>
</div>
<div style="padding-left: 30px;">4.製品インストーラをダウンロード</div>
<div style="padding-left: 30px;">5.製品をインストール</div>
<div style="padding-left: 30px;">
<ul>
<li>メールで案内されたシリアル番号を利用</li>
</ul>
</div>
<div style="padding-left: 30px;">6.製品を起動</div>
<div style="padding-left: 30px;">
<ul style="padding-left: 30px;">
<li>オンライン アクティベーションを実施</li>
<li>指名ユーザ情報を入力 &gt;&gt; 指名ユーザ認証をオンラインで実施</li>
</ul>
</div>
<p>&#0160;<strong>具体的な運用までの画面遷移</strong></p>
<p style="padding-left: 30px;"><strong>1.&#0160;</strong>製品購入（契約）後に契約管理者にメール通知が届きます。この時、契約時にオートデスクに知らせた電子メールアドレスをキーに、Autodesk ID の有無がチェックされます。Autodesk ID の有り無しによってｍ契約管理者に届くメール内容に若干の違いがあります。Autodesk ID がなく、オートデスク が Autodesk ID を作成した場合には、仮パスワードが記載されたメールとなります。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d11a8e96970c-pi" style="display: inline;"><img alt="Step1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d11a8e96970c image-full img-responsive" src="/assets/image_988156.jpg" title="Step1" /></a></p>
<p style="padding-left: 30px;"><strong>2.</strong>&#0160;契約管理者はユーザ登録と指名ユーザを指定するために、Autodesk ID でオートデスク アカウント（<a href="https://accounts.autodesk.com/">https://</a><a href="https://accounts.autodesk.com/">accounts.autodesk.com</a><a href="https://accounts.autodesk.com/">/</a>）にサインインします。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c790fb38970b-pi" style="display: inline;"><img alt="Step2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c790fb38970b image-full img-responsive" src="/assets/image_216968.jpg" title="Step2" /></a></p>
<p style="padding-left: 30px;"><strong>3.</strong>&#0160;契約管理者は、ライセンスを利用する可能性のあるユーザをユーザ登録して、その中から指名ユーザを指定します。まずは、ユーザ登録をおこないます。ユーザ登録時には、各ユーザの Autodesk ID の有無にかかわらず、名前と電子メールアドレスのみを登録します。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d11a8eaa970c-pi" style="display: inline;"><img alt="Step3-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d11a8eaa970c image-full img-responsive" src="/assets/image_199419.jpg" title="Step3-1" /></a>&#0160;</p>
<p style="padding-left: 30px;">ユーザ登録は 1 人単位でおこなうことが出来るほか、同時に最大 50 人の電子メールアドレスでおこなうことも可能です。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c790fb47970b-pi" style="display: inline;"><img alt="Step3-2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c790fb47970b image-full img-responsive" src="/assets/image_737906.jpg" title="Step3-2" /></a></p>
<p style="padding-left: 30px;">ユーザ登録が完了したら、一覧表示される登録ユーザのなかから「アセス権（本当はアクセス権）」リンクをクリックして、表示された画面から指名ユーザを指定します。指定は下記の画面で「割り当て」チェックボックスにチェックを入れるだけです。なお、契約期間内であれば、契約管理者は指名ユーザを別のユーザに切り替えることが出来ます。指名ユーザは、他の登録ユーザに指名を変更することは出来ません。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0834fe81970d-pi" style="display: inline;"><img alt="Step4-1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0834fe81970d image-full img-responsive" src="/assets/image_695088.jpg" title="Step4-1" /></a></p>
<p style="padding-left: 30px;">指名されたユーザには、電子メールアドレスで自身が契約管理者に指名されたことが電子メールで通知されます。なお、指名ユーザのユーザ登録時に電子メールアドレスと既存の Autodesk ID がチェックされ、もし、Autodesk ID がなければ、オートデスクが Autodesk ID を作成して通知します。この時、通知メールには、仮パスワードが記されています。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d11a8ecd970c-pi" style="display: inline;"><img alt="Step5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d11a8ecd970c image-full img-responsive" src="/assets/image_271471.jpg" title="Step5" /></a></p>
<p style="padding-left: 30px;"><strong>4.</strong>&#0160;指名ユーザは、指名ユーザの Autodesk ID でオートデスク アカウント（<a href="https://accounts.autodesk.com/">https://</a><a href="https://accounts.autodesk.com/">accounts.autodesk.com</a><a href="https://accounts.autodesk.com/">/</a>）にサインインして、利用する製品インストールをおこなます。Desktop Subscription ライセンスはスタンドアロン ライセンスなので、永久ライセンスと同じように製品インストーラをダウンロードして、割り当てられているシリアル番号で製品をクライアント コンピュータにインストールします。なお、<span style="background-color: #ffff00;">指名ユーザのオートデスク アカウント ページに製品インストーラが記載されるまでの、契約管理者が指名ユーザを指定してから、最大 24 時間かかる場合がありますのでご注意ください。指名ユーザを指定してすぐに反映されません。</span></p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0834feb6970d-pi" style="display: inline;"><img alt="Step6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0834feb6970d image-full img-responsive" src="/assets/image_2174.jpg" title="Step6" /></a></p>
<p style="padding-left: 30px;"><strong>5.</strong>&#0160;製品インストール時に利用すべきシリアル番号は、オートデスク アカウント上に表示されてきますので、必ず、そのシリアル番号を使用してください。Desktop Subscription ライセンスの製品インストーラは、現時点で永久ライセンスの製品インストーラを全く同じです。Desktop Subscription ライセンスであるかどうかは、シリアル番号で区別されます。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c790fb84970b-pi" style="display: inline;"><img alt="Step7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c790fb84970b image-full img-responsive" src="/assets/image_332178.jpg" title="Step7" /></a></p>
<p style="padding-left: 30px;"><strong>6.</strong>&#0160;インストールが正常に完了したら、製品を起動します。初回起動時には、指名ユーザの Autodesk ID で製品内からサインインすることを求められます。このサインインで指名ユーザの製品使用権限がチェックされ、同時にスタンドアロン ライセンスのオンライン認証が実行されます。このため、<span style="background-color: #ffff00;">初回起動時には、必ず、インターネット接続が必要となります。</span></p>
<p style="text-align: center; padding-left: 30px;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c790fb8c970b-pi" style="display: inline;"><img alt="Step8" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c790fb8c970b image-full img-responsive" src="/assets/image_224752.jpg" title="Step8" /></a>&#0160;</p>
<p style="padding-left: 30px;">初回起動時のライセンス認証が完了すると、契約管理者には指名ユーザが認証されたことがメールで通知されます。</p>
<p style="text-align: center; padding-left: 30px;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c790fb98970b-pi" style="display: inline;"><img alt="Step9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c790fb98970b image-full img-responsive" src="/assets/image_424555.jpg" title="Step9" /></a>&#0160;</p>
<p style="padding-left: 30px;">&#0160;</p>
<p><strong>運用開始後の流れ&#0160;</strong></p>
<p style="padding-left: 30px;">運用が開始されたら、Desktop Subscription の契約満了まで 30 日毎にオンラインによるライセンス認証が発生します。次の内容は、契約満了まで繰り返す内容です。もちろん、契約満了が近づいた時点で契約を延長することが出来ます。</p>
<div style="padding-left: 30px;"><ol style="padding-left: 30px;">
<li>有効期限切れ14日前に製品内でメッセージを表示</li>
<li>インターネットに接続してユーザ認証</li>
<li>以後、<strong>30日毎</strong>に契約満了日まで1.～2. を繰り返す<br />※ 2015年3月27日以前は14日毎でしたが、30日に改訂されています。</li>
</ol></div>
<p style="padding-left: 30px;">運用開始から 30 日経過した時点で製品内から指名ユーザの Autodesk ID でサインインされた状態で、インターネット接続されている状態なら、暗号化されたキャッシュを使って自動認証が実行されます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c790fba8970b-pi" style="display: inline;"><img alt="License_activation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c790fba8970b image-full img-responsive" src="/assets/image_633838.jpg" title="License_activation" /></a><br /><br /><span style="background-color: #ffff00;">初回起動後も含めて、30 日毎のライセンス認証が完了すれば、次のライセンス認証までオフラインで運用することが出来ます。</span></p>
<p><strong>Desktop Subscription 利用時の注意事項</strong></p>
<p style="padding-left: 30px;">まとめると、Desktop Subscription 運用時の注意事項は、下記のようになります。&#0160;</p>
<div style="padding-left: 30px;">
<ul>
<li>インターネット接続が必要なのは初回起動時と30日毎の認証時</li>
<li>30 日毎の認証時はキャッシュで自動認証</li>
<li>次回認証時までオフラインでの運用が可能</li>
<li>オンライン ライセンス転送ユーティリティは未サポート</li>
</ul>
Desktop Subscription はクラウドを利用したライセンス管理を利用します。指名ユーザの Autodesk ID があれば、クラウドを利用したフローティング ライセンスとして運用することもが可能です。このため、<span style="background-color: #ffff00;">スタンドアロン版の永久ライセンスに搭載されている&#0160;<strong><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=7619" target="_blank"><span style="background-color: #ffff00;">ライセンス転送ユーティリティ</span></a>&#0160;</strong>は利用できません。</span></div>
<div>&#0160;</div>
<div><strong>Desktop Subscription ライセンス（スタンドアロン）のまとめ</strong></div>
<p style="padding-left: 30px;">ここまでに流れをアニメーションにすると、このような感じになるかと思います。契約管理者と指名ユーザの役割が理解できれば、製品自体の機能に変わりがないので、永久ライセンスと同じように、かつ、安価に製品を運用していくことが出来るはずです。</p>
<p style="text-align: center; padding-left: 30px;">&#0160;<iframe allowfullscreen="" frameborder="0" height="344" src="https://www.youtube.com/embed/7yd14zQZ-Z4?feature=oembed" width="459"></iframe>&#0160;</p>
<p><span style="background-color: #ffff00;">なお、Desktop Subscription へ移行した場合でも、過去3バージョンの利用権限行使によって、3世代前までの旧バージョン製品をお使いいただくことが出来ますので、その点、ご注意ください。毎年、アドイン アプリケーションの移植作業を強制するわけではありません。最新バージョン＋過去3バージョンの範囲内で、お客様がお使いいただく製品バージョンを選択することが出来ます。</span></p>
<p><span style="background-color: #ffff00;">現時点で利用可能な過去バージョンは、<a href="http://knowledge.autodesk.com/customer-service/account-management/software-downloads/previous-versions/find-your-eligibility-for-previous-versions/desktop-subscription-previous-use#Available%20Previous%20Releases%20for%20Desktop%20Subscribers" target="_blank"><span style="background-color: #ffff00;"><strong>こちら</strong></span></a>&#0160;から参照いただくことが可能です。</span></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
