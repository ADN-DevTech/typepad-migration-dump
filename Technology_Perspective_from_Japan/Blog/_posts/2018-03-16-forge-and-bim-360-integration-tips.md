---
layout: "post"
title: "Forge アプリを BIM 360 と連携するための「カスタム統合機能」の注意点"
date: "2018-03-16 01:42:31"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/03/forge-and-bim-360-integration-tips.html "
typepad_basename: "forge-and-bim-360-integration-tips"
typepad_status: "Publish"
---

<p>今回は、Autodesk Forge を利用するアプリケーションを開発する際に、BIM 360 と連携するための「カスタム統合機能」の注意点についてご案内させて頂きます。</p>
<p>Forge アプリが BIM 360 と連携するためには、 BIM 360 のアカウント管理者が、BIM 360 のアカウント管理者ページにある「カスタム統合機能」というタブにアクセスする必要があります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e2e5d2970c-pi" style="display: inline;"><img alt="Forge-BIM360-Integration1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e2e5d2970c image-full img-responsive" src="/assets/image_11250.jpg" title="Forge-BIM360-Integration1" /></a></p>
<p><span style="color: #ff0000;">ただし、BIM 360 のデフォルトの設定で、全ての BIM 360 のアカウント管理者に、この「カスタム統合機能」が利用できる状態にはなっておりません。</span>そのため、下記のいずれかの条件に一致する内容をご確認いただき、必要な手順を実施してください。</p>
<ul>
<li>BIM 360 Docs アカウントを 100 以上ご購入頂いているお客様で、そのアカウントが BIM 360 アカウント管理者の場合は、自動的に「カスタム統合機能」タブが有効化されております。弊社システム上の問題で「カスタム統合機能」が有効化されていない場合もございます。その際は、アクティベーション依頼用の窓口にリクエスト頂きますようお願い致します。</li>
<li>それよりも少ないアカウント数をお持ちのお客様で、Forge アプリと連携したい場合は、「カスタム統合機能」アクティベーション依頼用の窓口にアクティベーションのリクエストを頂けましたら、オートデスクの担当者が有効化の手続きを行います。</li>
</ul>
<p style="padding-left: 60px;"><strong>アクティベーション依頼用の窓口：</strong> <a href="mailto:bim360appsactivations@autodesk.com">bim360appsactivations@autodesk.com</a></p>
<ul>
<li>Forge アプリの開発を目的としたデベロッパ様の場合、BIM 360 Docs のトライアルアカウントを作成いただき、上記と同じアクティベーション依頼用の窓口に、下記の情報を Email の本文に含めて、トライアルアカウントをデベロッパアカウントに変更するようリクエストしてください。</li>
</ul>
<p style="padding-left: 60px;"><strong>トライアルアカウントの作成：</strong>&#0160;<a href="https://bim360.autodesk.com/try-for-free">https://bim360.autodesk.com/try-for-free</a><br /><strong>デベロッパアカウントへの変更依頼：</strong> <a href="mailto:bim360appsactivations@autodesk.com">bim360appsactivations@autodesk.com</a></p>
<p style="padding-left: 60px;">BIM 360 アカウント名 - アカウント作成時の Email アドレス<br />BIM 360 アカウント ID - アカウント作成時のアカウント ID<br />BIM 360 のアカウント管理者の Email アドレス</p>
<ul>
<li>ADN デベロッパ様で Partner Evaluation and Demonstration (PED) を目的として Forge アプリを開発頂く場合は、ADN extranet からデベロッパ向けの BIM 360 ライセンスを取得頂けます。</li>
</ul>
<p>またお手数をおかけして大変恐れ入りますが、リクエストの際は、英語にてご連絡いただきますようお願い致します。</p>
<p>この内容は、下記のデベロッパポータルの BIM 360 API のチュートリアルページにも掲載されております。</p>
<p style="padding-left: 30px;">Get Access to a BIM 360 Account<br /><a href="https://developer.autodesk.com/en/docs/bim360/v1/tutorials/get-access-to-account/">https://developer.autodesk.com/en/docs/bim360/v1/tutorials/get-access-to-account/</a></p>
<p>&#0160;</p>
<p>次に、この「カスタム統合機能」の設定における注意点についてご案内いたします。</p>
<p>この設定方法の詳細な手順については、下記の記事をご参照ください。</p>
<p style="padding-left: 30px;">BIM 360 Docs と Data Management API アクセス<br /><a href="http://adndevblog.typepad.com/technology_perspective/2017/06/bim-360-docs-and-data-management-api-access.html">http://adndevblog.typepad.com/technology_perspective/2017/06/bim-360-docs-and-data-management-api-access.html</a></p>
<p>ここでは、上記の記事を補足して説明いたします。</p>
<p>1．まず Forge アプリのアクセス権を指定します。下記のどちか一方、または両方を選択することができます。</p>
<ul>
<li><strong>BIM 360 アカウント管理</strong><br />アカウント管理の各機能（プロジェクトの作成、サービスのアクティブ化、ビジネスパートナーフォルダ、メンバーフォルダなど）に対する読み取りと書き込みのアクセス権</li>
<li><strong>BIM 360 Docs</strong><br />ドキュメント管理の各機能（フォルダ、ドキュメント、モデルなど）に対する読み取りと書き込みのアクセス権</li>
</ul>
<p>2．Forge アプリを自身で開発するか、外部の開発者に依頼するかを指定します。</p>
<p>ご自身を選択した場合は、そのまま Forge アプリとの連携設定の画面に移行します。開発者を招待を選択した場合、開発者のメールアドレスを入力すると、下記のような招待メールが送信されます。開発者はこの招待メールから、オートデスクのアカウントでログインすると、Forge アプリとの連携設定を行うことができます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e2e621970c-pi" style="display: inline;"><img alt="Forge-BIM360-Integration2" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e2e621970c img-responsive" src="/assets/image_102106.jpg" title="Forge-BIM360-Integration2" /></a></p>
<p>3．カスタム統合機能を利用するための必要事項を入力します。</p>
<p>ここでは、BIM 360 アカウント ID が自動的に発行されて、下記のような注意書きが表示されます。</p>
<p>「アプリから BIM 360 API にアクセスするときには、BIM 360 アカウント ID を入力する必要があります。続行する前に、BIM 360 アカウント ID 情報を記録用に保存してください。」</p>
<p>そして、Forge アプリの登録時に取得した Client ID とアプリ名を入力します。</p>
<p>ここでご注意いただきたいのは、<span style="color: #ff0000;">この統合機能の設定後は、 BIM 360 アカウント ID を再度確認することはできません。また入力した Forge の Client ID を確認する機能やバリデーション機能もまだ提供されていません。</span></p>
<p><span style="color: #ff0000;"><span style="color: #111111;">手入力で誤った Client ID を設定してしまった場合は、再度、連携設定をやり直す必要がございます。</span>そのため、設定を完了する前には、入力した情報を十分にご確認頂きますようお願い致します。</span></p>
<p><span style="color: #ff0000;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2e2e648970c-pi" style="display: inline;"><img alt="Forge-BIM360-Integration3" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2e2e648970c img-responsive" src="/assets/image_606515.jpg" title="Forge-BIM360-Integration3" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c958b139970b-pi" style="display: inline;"><br /></a><br /></span></p>
<p>上記の手順で、Forge アプリと BIM 360 との連携設定が完了すると、Forge の API から BIM 360 Docs のデータにアクセスすることができるようになります。</p>
<p>By Ryuji Ogasawara</p>
