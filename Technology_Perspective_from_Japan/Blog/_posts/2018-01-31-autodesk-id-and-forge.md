---
layout: "post"
title: "Autodesk ID と Forge"
date: "2018-01-31 00:45:54"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/01/autodesk-id-and-forge.html "
typepad_basename: "autodesk-id-and-forge"
typepad_status: "Publish"
---

<p>ご存じのとおり、Forge を利用するには、オートデスク アカウント（Autodesk ID）を使って<strong>デベロッパ&#0160; ポータル（<a href="https://developer.autodesk.com" rel="noopener noreferrer" target="_blank">https://developer.autodesk.com</a>）</strong>にサインインして、デベロッパキー（Client ID と Client Secret）を取得する必要があります。具体的な手順は、過去のブログ記事、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener noreferrer" target="_blank">Forge API を利用するアプリの登録とキーの取得</a></strong>&#0160;でご紹介していますので、必要に応じてご確認ください。</p>
<p>Autodesk ID は、<a href="https://accounts.autodesk.com" rel="noopener noreferrer" target="_blank"><strong>Autodesk Accounts（https://accounts.autodesk.com）</strong></a>ページから無償で作成することが出来ますが、背後に関連付けられた情報が多数あるという点に注意が必要です。オートデスク製品のサブスクリプション契約もその 1 つですし、上記デベロッパキーの取得で追跡される Forge の利用状況も関連付けられている情報です。</p>
<p>この Autodesk ID には、アカウント登録時にサインインに使用するユーザ名と各種通知や連絡に利用される電子メール アドレス（Email Address）が含まれています。また、両者の情報は必要に応じて書き換えることが出来ます。設定を変更するには、例えば、 <strong>デベロッパ&#0160; ポータル（<a href="https://developer.autodesk.com" rel="noopener noreferrer" target="_blank">https://developer.autodesk.com</a>）</strong>にサインイ後、右上のメニューから [My Profile] をクリックします。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09ee88b4970d-pi" style="display: inline;"><img alt="My_profile" class="asset  asset-image at-xid-6a0167607c2431970b01bb09ee88b4970d img-responsive" src="/assets/image_883962.jpg" style="width: 530px;" title="My_profile" /></a></p>
<p>続いて、表示されたページで [セキュリティの設定] タブを表示させることで、ユーザ名と電子メールアドレスを変更することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c94b19ac970b-pi" style="display: inline;"><img alt="Security_settings" border="0" class="asset asset-image at-xid-6a0167607c2431970b01b7c94b19ac970b image-full img-responsive" src="/assets/image_739759.jpg" title="Security_settings" /></a></p>
<p>この変更は、直接 <a href="https://accounts.autodesk.com" rel="noopener noreferrer" target="_blank"><strong>Autodesk Accounts（https://accounts.autodesk.com）</strong></a>ページを開いてサインインしてもおこなうことが出来ます、重要なのは、クラウド クレジットを利用する Forge の課金が、この電子メールアドレスに対して通知されることです。Autodesk ID 作成時には課金手続きまで念頭に置いていない場合がほとんどだと思いますので、今のうちに然るべき電子メール アドレスに書き換えることをお勧めします。</p>
<ul>
<li>もちろん、Forge に利用する&#0160; Autodesk ID を変えてしまうことも考えられますが、その場合、Autodesk ID に関連付けられたデベロッパキーも変わってしまうことになるため、開発時に Data Management API を使って作成した <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/summary-about-bucket.html" rel="noopener noreferrer" target="_blank">Bucket</a></strong> にはアクセス出来なくなってしまいます。</li>
</ul>
<p>技術的には、この方法を使って開発中と運用時でAutodesk ID に設定された電子メール アドレスを変えてしまうことも可能になっています。<span style="background-color: #ffff00;">受託開発などでは、この方法で Forge を使ったアプリ/サービス開発時は開発を請け負った開発会社の担当者の電子メール アドレスを、開発完了後にアプリ/サービスを運用するユーザ企業の担当者の電子メール アドレスを設定することも出来てしまいます。ただ、主にサブスクリプション契約で使用される Autodesk ID の性格上、残念ながら、異なるオーナー企業にまたがって電子メール アドレスを書き換える行為は許諾されていません。今後、なんらかの設定上の制約が設けられる可能性もありますが、Autodesk ID 運用上の制約として覚えていただくことをお勧めします。</span></p>
<p>受託開発などのように、アプリ/サービス開発企業とアプリ/サービスを運用するユーザ企業が異なる場合は、事前にユーザ企業と協議の上、Autodesk IDとの、その電子メール アドレスを使用して開発することが推奨されています。</p>
<p>By Toshiaki Isezaki</p>
