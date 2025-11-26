---
layout: "post"
title: "A360 ストレージ サービスのわかりにくいところ"
date: "2019-01-28 00:03:10"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/01/about-obscure-things-on-a360-storage-service.html "
typepad_basename: "about-obscure-things-on-a360-storage-service"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad392bafa200c-pi" style="float: right;"><img alt="Autodesk-storage-services" class="asset  asset-image at-xid-6a0167607c2431970b022ad392bafa200c img-responsive" src="/assets/image_529965.jpg" style="margin: 0px 0px 5px 5px;" title="Autodesk-storage-services" /></a>オートデスクが継続して開発、拡張に取り組んでいるクラウド サービスに、デスクトップ CAD 製品で作成したファイルをクラウドにアップロードしてバックアップしたり、関係者とコラボレーションしたりするためのストレージ サービスがあります。（Buzzsaw を除き）2011 年 9 月の Autodesk Cloud Documents から A360 までの発展の詳細は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/transition-on-autodesk-cloud-storage-service.html" rel="noopener" target="_blank">オートデスク クラウド ストレージ サービスの遷移</a></strong> のブログ記事でご案内したことがありますが、現在、A360 Team の<strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/10/name-change-of-a360-team.html" rel="noopener" target="_blank">分化 </a></strong>が原因で製品構成が把握しにくくなっている感があります。ここでは、改めて、現在の状況をまとめておきたいと思います。</p>
<p>まず、新規に契約、あるいは、お使いいただけるストレージ サービスについてです。2019 年 1 月現在、オートデスクがストレージ サービスとして提供しているのは、個人用途の A360 Personal、製造業向けでチーム用途の Fusion Team、そして、建設業のチーム用途となる BIM 360 Docs の 3 種類です。BIM 360 Docs は、A360 Team から名称変更した BIM 360 Team の新規販売の停止後、BIM 360 Glue や Field といったユーティリティ サービスのストレージを統合する目的も担っています。別の言い方をするなら、建設業向けに提供される各種クラウドサービスのデータ集約の場として利用されるのが BIM 360 Docs であり、BIM 360 Docs 上のデータを活用する BIM 360 サービス群が、いわゆる、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/05/nextgen-bim-360.html" rel="noopener" target="_blank">次世代 BIM 360</a></strong> と言えます。</p>
<ul style="list-style-type: disc;">
<li>BIM 360 Team は、サブスクリプションとして提供されていましたが、現在、新規に契約することは出来ません。ただし、過去にサブスクリプション契約されていた方は、契約を更新して利用し続けることが出来ます。</li>
</ul>
<hr />
<p><strong>A360 Drive？</strong></p>
<p style="padding-left: 40px;"><span style="background-color: #ffff00;">このトピックにある A360 と A360 Drive との 切り替え機能は2019年4月以降削除されています。A360 Drive へは個別 URL <a href="https://a360.autodesk.com/drive/" rel="noopener" target="_blank">https://a360.autodesk.com/drive/</a> からアクセス出来ます。</span></p>
<p style="padding-left: 40px;"><span style="color: #b9b9b9;">さて、オートデスクのストレージ サービスをお使いいただく中で混乱しがちな点の 1 つめは、A360 Drive（A360 ドライブ）です。ストレージ サービスには、データ管理の方法にフォルダ ベースの管理を提供する A360 Drive と、プロジェクト ベースの管理を提供する A360 があります。後者には、A360 Personal、旧 A360 Team、旧 BIM 360 Team、Fusion Team、BIM 360 Docs がありますが、ユーザ インタフェースを A360 Drive に切り替えられるのは、A360 Personal、旧 A360 Team、旧 BIM 360 Team、Fusion Team のみです。</span></p>
<p style="padding-left: 40px;"><span style="color: #b9b9b9;">ただし、既定値で A360 Drive への切り替えがオフになっています。A360 Drive の利用が必要な場合は、A360 の設定をを変更し「A360 Drive を表示」をオンにしてみてください。</span></p>
<p style="padding-left: 40px;"><span style="color: #b9b9b9;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3d87a75200b-pi" style="display: inline; color: #b9b9b9;"><img alt="Enable_a360_drive" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3d87a75200b image-full img-responsive" src="/assets/image_308912.jpg" title="Enable_a360_drive" /></a></span></p>
<p style="padding-left: 40px;"><span style="color: #b9b9b9;">「A360 Drive を表示」がオンになると、ページ左上にに表示される A360 アイコンをクリックすることで、A360 Drive への切り替えがおこなえるようになります。</span></p>
<p style="padding-left: 40px;"><span style="color: #b9b9b9;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad392c6a1200c-pi" style="display: inline; color: #b9b9b9;"><img alt="Switch_to_a360_drive" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad392c6a1200c image-full img-responsive" src="/assets/image_208909.jpg" title="Switch_to_a360_drive" /></a></span></p>
<p style="padding-left: 40px;"><span style="color: #b9b9b9;">A360 Drive の名称は、Fusion Team、旧 BIM 360 Team から切り替えた場合も同一です。Fusion Drive や BIM 360 Drive のような表記にはなりません。<span style="background-color: #ffff00; color: #111111;">最も重要なのは、プロジェクト ベース管理のストレージ サービスでアップロード、管理しているファイルは、フォルダ ベースの A360 Drive 上には表示されない点です。また、<strong>Forge の Data Management API でアクセスすることが出来るのは、プロジェクト ベースのストレージのみです。A360 Drive にアクセスする Forge Platform API は提供されていません。</strong></span></span></p>
<p style="padding-left: 40px;"><span style="color: #111111;">A360 Drive は、フォルダ ベース管理の系譜を持つ Autodesk Cloud Documents や Autodesk 360 を利用されていた方向けに用意されているものなので、新規活用はお勧めしていません。ご注意ください。</span></p>
<p><strong>A360 Personal？</strong></p>
<p style="padding-left: 40px;">旧 A360 Team、旧 BIM 360 Team、Fusion Team、BIM 360 Docs はサブスクリプション製品（有償）ですが、A360 Personal は無償です。といっても、明示的にユーザ インタフェースに A360 Personal と表示される訳ではありません。Autodesk ID を作成（<strong><a href="http://help.autodesk.com/view/ADSK360/JPN/?guid=GUID-D708094A-BF86-4D03-927B-AD0387ADBB85" rel="noopener" target="_blank">A360 にサインアップ</a></strong>）して、A360 を使い始めると、すでに A360 Personal を利用していることになります。</p>
<p style="padding-left: 40px;">A360 Personal はプロジェクト ベースのデータ管理を提供しますが、作成出来るプロジェクト数は 1 つに制限されています（既定で作成されている<strong> <em>Demo Project</em> </strong>を除く）。もし、[プロジェクトを作成] をクリックして次のようなメッセージが表示されるなら、お使いの A360 は A360 Personal ということになります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad392cc7d200c-pi" style="display: inline;"><img alt="Project_limit_message" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad392cc7d200c image-full img-responsive" src="/assets/image_898761.jpg" title="Project_limit_message" /></a></p>
<p><strong>A360 Personal のアップグレード先？</strong></p>
<p style="padding-left: 40px;">もし、複数のプロジェクトを利用する必要がある場合には、上記の画面から Team 版にアップグレードすることが出来ます。ここでいうアップグレードとは、<strong><a href="https://fusionteam.autodesk.com/" rel="noopener" target="_blank">Fusion Team</a></strong> か <strong><a href="https://info.bim360.autodesk.com/bim-360-docs" rel="noopener" target="_blank">BIM 360 Docs</a></strong> への<strong><a href="http://help.autodesk.com/view/FSNT/JPN/?guid=GUID-297688D0-3512-4313-B4E5-0C4BD5AEFC7A" rel="noopener" target="_blank">新規サブスクリプション契約</a></strong>を指すことになります。前述のとおり、A360 Team や BIM 360 Team への新規契約は停止されているのが理由です。</p>
<p><strong>Fusion Personal？</strong></p>
<p style="padding-left: 40px;">A360 Personal を利用中に、ページに表示される A360 のアイコンが、いつの間にか Fusion のアイコンになってしまう場合があります。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3d88486200b-pi" style="display: inline;"><img alt="A60-fusion" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3d88486200b image-full img-responsive" src="/assets/image_682319.jpg" title="A60-fusion" /></a></p>
<p style="padding-left: 40px;">Fusion 360 をお使いの方はご存知と思いますが、Fusion 360 は作成したデザインの保存にプロジェクト ベースの A360 ストレージを利用しています。もし、A360 Personal で使用した Autodesk ID を使って Fusion 360 にサインイン、使用してしまうと、A360 Personal 側も Fusion（Personal）アイコンで再ブランド化されてしまいます。これは、自動的に Fusion Team へアップグレードされた訳ではなく、Fusion 360 の 30 日間トライアル（体験版）モードが適用されたことを意味するマーケティング的なアイコンの変更です。このトライアルの開始と同時に、本来、A360 Personal で 1 つしか作成出来ないプロジェクトが複数作成出来るようになってしまいます。</p>
<p style="padding-left: 40px;">トライアル期間を経て Fusion 360、あるいは、Fusion Team をサブスクリプション契約しないと、トライアル期間中に作成したプロジェクトやプロジェクト内のファイルは読み取り専用になり、新しいデータの書き込みは出来なくなってしまいます。具体的には、プロジェクトに新しいファイルをアップロードしたり、既存のファイルに新しいバージョンを追加したりすることは出来ません。ご注意ください。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3b8e5bc200d-pi" style="display: inline;"><img alt="Fusion_360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3b8e5bc200d image-full img-responsive" src="/assets/image_195696.jpg" title="Fusion_360" /></a></p>
<p style="padding-left: 40px;">残念ながら、Fusion 360 のトライアル期間 30 日を終了しても、A360 Personal に戻るようなことは出来ません。同様に A360 アイコンに戻す設定等はありません。なお、トライアル期間中に作成したプロジェクトやアップロードしたファイルは、トライアル期間終了後もダウンロードすることは可能です。</p>
<p><strong>各オンライン ヘルプ</strong></p>
<p style="padding-left: 40px;">ここまでご紹介してきたのは、オンライン ヘルプに記載された機能的なものではなく、オートデスクの都合で変更してきた販売・運用上のルールによるものが大きく関係しています。このため、将来、内容が変更される可能性がある点をご理解ください。ご参考まで、次に各ストレージ サービスのオンライン ヘルプ ドキュメントの URL を記しておきます。</p>
<p style="padding-left: 40px;"><strong>A360：<a href="http://help.autodesk.com/view/ADSK360/JPN/" rel="noopener" target="_blank">http://help.autodesk.com/view/ADSK360/JPN/</a></strong></p>
<p style="padding-left: 40px;"><strong>Fusion Team：<a href="http://help.autodesk.com/view/FSNT/JPN/" rel="noopener" target="_blank">http://help.autodesk.com/view/FSNT/JPN/</a></strong></p>
<p style="padding-left: 40px;"><strong>BIM 360 Team：<a href="http://help.autodesk.com/view/BIM360T/JPN/" rel="noopener" target="_blank">http://help.autodesk.com/view/BIM360T/JPN/</a></strong></p>
<p style="padding-left: 40px;"><strong>BIM 360 Docs：<a href="http://help.autodesk.com/view/BIM360D/JPN/" rel="noopener" target="_blank">http://help.autodesk.com/view/BIM360D/JPN</a>/</strong></p>
<hr />
<p>By Toshiaki Isezaki</p>
