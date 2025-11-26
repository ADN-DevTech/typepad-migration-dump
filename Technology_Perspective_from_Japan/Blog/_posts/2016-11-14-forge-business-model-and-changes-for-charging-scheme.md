---
layout: "post"
title: "Forge ビジネスモデルと課金方法の変更"
date: "2016-11-14 12:34:02"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/11/forge_business_model_and_changes_for_charging_scheme.html "
typepad_basename: "forge_business_model_and_changes_for_charging_scheme"
typepad_status: "Publish"
---

<p>今週、Autodesk University（AU）が米国ラスベガスで開催されますが、AU 開催に先立ち、11月14日に米国在中の ADN メンバ向けに Developer Day が開催されました。<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/09/new-forge-cost-for-us-canada-europe.html" target="_blank">既報</a>&#0160;</strong>のとおり、米国とヨーロッパでは &#0160;Autodesk Forge 利用者に対する課金が始まってますが、今回の Developer Day では、課金方法とその内容についての変更がアナウンスされています。</p>
<p><span style="background-color: #ffff00;">なお、今回の変更は、あくまで、既に課金が開始されている米国とヨーロッパに在中する開発者に対するものです。課金が開始されていない日本では、従来どおり、当面の間、無償・無制限で Forge Platform API をお使いいただくことが出来ます。</span></p>
<p>変更された新しい課金内容については、14日付けで&#0160;<strong><a href="https://developer.autodesk.com/" target="_blank">デベロッパ ポータル</a></strong>&#0160;への記載が更新されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2385dbf970c-pi" style="display: inline;"><img alt="New_bisiness_model_web" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2385dbf970c image-full img-responsive" src="/assets/image_96236.jpg" title="New_bisiness_model_web" /></a></p>
<p>また、デベロッパ ポータルで Forge 開発で必要なデベロッパキー（Consumer Key、Consumer Secret）を取得されている方には、先週の段階で、「Forge Update: Subscription Availability and New Terms of Service」のタイトルでメールが配信されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09518c77970d-pi" style="display: inline;"><img alt="New_bisiness_model_email" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09518c77970d image-full img-responsive" src="/assets/image_254042.jpg" title="New_bisiness_model_email" /></a></p>
<p>通知メールにあるとおり、今回の変更は、開発者コミュニティなどからのフィードバックを元に、よりシンプルで利用し易くなることを観点に作成されてものです。概要は、次のとおりです。</p>
<ul>
<li>課金はクレジット カードによる引き落としではなく、オートデスクが既に導入している クラウド クレジットでおこなう。</li>
<li>課金対象は、Model Derivative API と Design Automation API に限定される。従来のアナウンスで対象となっていた Data Management API は課金対象からはずれる。Viewer 利用は、従来想定通り無償。</li>
<li>Model Derivative API での消費されるクラウド クレジットは、変換対象のファイルの種類によって異なる。現在のところ、Revit と Navisworks ファイルの変換には、内部的なプロセルの難易さから、1 回の変換で 1.5 クラウド クレジットが消費される。その他のファイル変換には、0.2 クラウド クレジットが消費される。</li>
<li>Design Automation API の課金は、バッチ処理の内容を担う WorkItem の演算時間によってクラウドクレジットが消費される。1 時間処理に時間がかかった場合、消費されるのは 4&#0160;クラウド クレジットとなる。</li>
<li>Autodesk Forge を使用し始めた初年度は、Trial（体験）期間として 500 クラウド クレジット提供される。このクラウド クレジットを範囲内の消費であれば、無償で API を利用することが出来る。なお、クラウド クレジットが残っていても、1 年を経過すると、Forge を Subscription 契約しなければならない。</li>
<li>Forge のSubscription 契約は、現在のところ、1 ヶ月単位で、月500米国ドル（375 英国ポンド、440 ユーロ）になる。この価格には消費税は含まれない。</li>
<li>クラウド クレジットとは別に、Trial 期間には 5GB、Subscription 購入で 500GB のストレージ領域が提供される。この領域は、主に、2-legged 認証下で利用する OSS の Bucket 利用等で利用することが可能。</li>
<li>Forge Subscription 契約は、Autodesk Accounts ページで停止しない限り、自動更新される。</li>
<li>Forge Subscription 契約して使用せずに残ったクラウド クレジットは、月替わりで削除されてしまうため、持ち越しは出来ない。なお、Trial で提供されるクラウド クレジットは、アカウント登録から 12 ヶ月間は削除されない。</li>
<li>Forge Subscription で入手したクラウド クレジットは、現在のところ、Forge 専用となるため、他のオートデスクのクラウド サービスでは利用することが出来ない。</li>
<li>当初、学生や教育機関の開発者用のビジネス モデルを検討していたが、現時点で、無償の Trial を利用するよう改定された。ただし、今後の改定で変更される可能性がある。</li>
<li>Trial で入手したクラウド クレジットでも、商用サービス/アプリケーションで利用可能。</li>
<li>クラウド クレジットを大規模消費をする場合には、個別契約で対応することも可能。</li>
<li>既に Forge を利用している開発者は、12月9日まで制限なし・無償で利用可能。</li>
<li>既に取得済みのデベロッパキー（Consumer Key、Consumer Secret）は取得し直す必要はない。</li>
</ul>
<p>日本での課金開始時期や内容が明確になった時点で、このブログでご案内する予定です。繰り返しになりますが、上記内容は、既に課金が開始されている米国とヨーロッパに在中する開発者を対するものです。課金が開始されていない日本では、従来どおり、当面の間、無償・無制限で Forge Platform API をお使いいただくことが出来ます。</p>
<p>By Toshiaki Isezaki</p>
