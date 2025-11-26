---
layout: "post"
title: "A360 の 2 段階認証"
date: "2015-11-09 00:04:11"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/11/a360-2-step-verification.html "
typepad_basename: "a360-2-step-verification"
typepad_status: "Publish"
---

<p>A360 をはじめとしたオートデスクのクラウド サービスを利用するには、Autodesk ID と呼ぶアカウントを利用して&#0160;<strong><a href="https://ja.wikipedia.org/wiki/%E8%AA%8D%E8%A8%BC" target="_blank">認証</a>&#0160;</strong>をおこなう必要があります。一部のサービスには Subscription 契約が必要ですが、アカウントが 1 つあれば、基本的にすべてのサービスにアクセスすることができます。</p>
<p>ここで言う認証とは、利用するサービスに ユーザ名にあたる Autodesk ID とパスワードを入力することで実行されていました。シンプルな認証方法であるため、極めて高い利便性を得ることが出来ています。海外出張時や作業現場からサービスにアクセスする場合でも、Autodesk ID とパスワードさえあれば、不自由はありません。</p>
<p>ただ、この方法に不安を感じられる方もいらっしゃるようです。A360 には、そのような不安に応えるため、本人認証をより厳密に実施するオプションが用意されています。それが、2 段階認証です。既に、Google、Microsoft、Apple などのクラウド サービスにも導入されています。</p>
<p><strong>2 段階認証のセットアップ</strong></p>
<p>A360 で2段階認証を利用するためには、セットアップが必要です。次の手順で、セットアップを完了してください。</p>
<ol>
<li>Autodesk Account ページ（<a href="https://accounts.autodesk.com" target="_blank">https://accounts.autodesk.com</a>）にアクセスして、プロファイル カテゴリの 2 段階認証 の [セットアップ] ボタンから設定をしてください。<br /><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d172bfcb970c-pi" style="display: inline;"><img alt="2steps_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d172bfcb970c image-full img-responsive" src="/assets/image_299097.jpg" title="2steps_settings" /><br /></a></li>
<li>セットアップを始めると、次の画面が表示されます。まず、Autodesk ID のパスワードを入力してください。入力が完了したら、[続行] ボタンで次の画面へ進みます。<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d172c6c0970c-pi" style="display: inline;"><img alt="Step1_passord" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d172c6c0970c image-full img-responsive" src="/assets/image_382206.jpg" title="Step1_passord" /></a></li>
<li>2 段階認証のセットアップが正しいユーザにおこなわれているか確認するため、セキュリティ コードの入力で判定をおこないます。A360 からセキュリティ コードが送られてくるので、<a href="https://ja.wikipedia.org/wiki/%E3%82%B7%E3%83%A7%E3%83%BC%E3%83%88%E3%83%A1%E3%83%83%E3%82%BB%E3%83%BC%E3%82%B8%E3%82%B5%E3%83%BC%E3%83%93%E3%82%B9" target="_blank"><strong>SMS</strong></a> で受信するか、電話で受信するかを選択してください。SMS とは、Short&#0160;Message Service の略で、携帯キャリアで提供されている<strong><a href="https://ja.wikipedia.org/wiki/%E3%82%B7%E3%83%A7%E3%83%BC%E3%83%88%E3%83%A1%E3%83%83%E3%82%BB%E3%83%BC%E3%82%B8%E3%82%B5%E3%83%BC%E3%83%93%E3%82%B9" target="_blank">ショート メッセージ サービス</a></strong>です。電話での受信では、現段階で、通知が英語のメッセージになってしまうようなので、履歴の残る SMS での受信をお勧めします。ちなみに、電話でのセキュリティ コード受信を選択すると、サンフランシスコから国際電話がかかってきますので驚かないでください（寂しくても自動通知なので会話は出来ません!）。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088d3a70970d-pi" style="display: inline;"><img alt="Step3_getting_scode_via_phoncall" class="asset  asset-image at-xid-6a0167607c2431970b01bb088d3a70970d img-responsive" src="/assets/image_109646.jpg" style="width: 250px;" title="Step3_getting_scode_via_phoncall" /></a><br />いずれの場合も、国際電話でセキュリティ コードが送られてくるので、日本で受信するために、国番号に +81 を指定後、最初の 0 を除いた携帯番号を入力する必要があります（080-xxxx-yyyy の場合は 80xxxxyyyy と入力）。<br /><br />ここでは、SMS でセキュリティ コードを受信するよう指定して、[次へ] をクリックします。<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e8f34d970b-pi" style="display: inline;"><img alt="Step2_receving_choice" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e8f34d970b image-full img-responsive" src="/assets/image_305194.jpg" title="Step2_receving_choice" /></a></li>
<li>しばらくすると、指定した電話番号を持つデバイスに、SMS を使ってセキュリティ コードが通知されてきます。<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d172ca6b970c-pi" style="display: inline;"><img alt="Step3_getting_scode" class="asset  asset-image at-xid-6a0167607c2431970b01b8d172ca6b970c img-responsive" src="/assets/image_403743.jpg" style="width: 250px;" title="Step3_getting_scode" /></a></li>
<li>受信した 6 桁のセキュリティ コードを次の画面に入力して、[次へ] をクリックしてください。なお、受信したセキュリティ コードには、使用期限が設定されています。受信後、すみやかに入力を完了するようにしてください。<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e8f6e9970b-pi" style="display: inline;"><img alt="Step4_enter_scode" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e8f6e9970b image-full img-responsive" src="/assets/image_990022.jpg" title="Step4_enter_scode" /></a></li>
<li>次の画面が表示されたら。セットアップは完了です。<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d172cac1970c-pi" style="display: inline;"><img alt="Step5_complete" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d172cac1970c image-full img-responsive" src="/assets/image_293706.jpg" title="Step5_complete" /></a></li>
</ol>
<p><strong>&#0160;2 段階認証を使ったサインイン</strong></p>
<p>&#0160;2 段階認証のセットアップが完了すると、A360 の各種サービスにサインインする際に、常に 2 段階認証を求められるようになります。すなわち、最初の認証が Autodesk ID とパスワード入力による認証、2 つめの認証が、セットアップでも実施したセキュリティ コードの入力による認証です。セキュリティ コードは、サインインの度にセットアップで指定した電話番号に SMS で送られてきます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d172cb6e970c-pi" style="display: inline;"><img alt="2steps_certification" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d172cb6e970c image-full img-responsive" src="/assets/image_866083.jpg" title="2steps_certification" /></a></p>
<p>なお、従来と同様に、サインインの情報はキャッシュすることが出来るので、毎回、2 段階認証を要求される訳ではありません。</p>
<p><strong>2 段階認証の解除</strong></p>
<p>2 段階認証の設定は、いつでも解除することができます。セットアップをおこなった Autodesk Accounts 画面で [設定の編集] ボタンをクリックしてください。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088cd30f970d-pi" style="display: inline;"><img alt="2steps_cenceling" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb088cd30f970d image-full img-responsive" src="/assets/image_390518.jpg" title="2steps_cenceling" /></a></p>
<p>表示される画面で [無効] ボタンをクリックすると、2 段階認証を解除することが出来ます。この画面では、2 段階認証の解除の他に、セキュリティ コードが送信される電話番号の変更も設定することも可能です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d172cbe4970c-pi" style="display: inline;"><img alt="2steps_cenceling2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d172cbe4970c image-full img-responsive" src="/assets/image_525565.jpg" title="2steps_cenceling2" /></a></p>
<p>2 段階認証は必須ではありませんが、必要に応じてクラウド セキュリティを向上させる手段としてお使いいただくことが出来ます。</p>
<p>By Toshiaki Isezaki</p>
