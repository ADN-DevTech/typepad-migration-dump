---
layout: "post"
title: "Forge の 2 段階認証"
date: "2018-04-16 00:06:31"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/04/forge-2nd-factor-authentication.html "
typepad_basename: "forge-2nd-factor-authentication"
typepad_status: "Publish"
---

<p>Autodesk ID を使ったクラウド サービスへのサインインでは、ユーザ オプションとしてセキュリティを高める目的で、過去にブログ記事&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/11/a360-2-step-verification.html" rel="noopener noreferrer" target="_blank">A360 の 2 段階認証</a></strong>でご案内した設定を利用することが出来ます。</p>
<p>現在、<strong>デベロッパ ポータル（<a href="https://developer.autodesk.com/" rel="noopener noreferrer" target="_blank">https://developer.autodesk.com/</a>）</strong>へのサインインや、Forge&#0160; OAuth API で 3-legged OAuthを経たオートデスクのクラウド サービスのユーザ ストレージへアクセスするアプリのサインインでも、2 段階認証の認知度を向上させるため、適用を促す画面が表示されるようになっています。2-legged OAuth を利用するサービス/アプリには影響はありません。</p>
<p>2 段階認証を設定していないアカウント（Autodesk ID）でサインインをしようとすると、従来通り、ユーザ名（Autodesk ID）とパスワードを入力した後で、2 段階認証を促す「アカウントを保護」画面が表示されます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2eb5907970c-pi" style="display: inline;"><img alt="2nd_factor_authentication_web1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2eb5907970c image-full img-responsive" src="/assets/image_505459.jpg" title="2nd_factor_authentication_web1" /></a></p>
<p>&#0160;[スタートアップ] をクリックすると、2 段階認証を設定するための画面が表示されます。もし、「後で通知」のリンクをクリックすると、2 段階認証の設定をスキップすることが出来ます。この場合、同じアカウントでサインインしようとすると、後 2&#0160; 回（全部で最大で 3 回）、「アカウントを保護」画面が表示されますが、都度「後で通知」を選択することで 2 段階認証の設定をスキップすることが出来ます。つまり、2 段階認証の利用は、あくまで任意です。</p>
<p>[スタートアップ] をクリックして 2 段階認証を利用を選択した場合には、2 段階認証で使用するセキュリティ コードを、どのように受け取るか設定していきます。セキュリティ コードの受け取りには、スマートフォンの電話番号へのテキスト メッセージか、電話を使った音声を利用するかを選択することが出来ますが、日本での利用にはテキスト メッセージでの受け取り方法をお勧めしています。</p>
<p>ここでは、「テキスト メッセージ」にチェックをして、国番号 <strong>+81 </strong>の後に、現在お使いのスマートフォンの携帯電話番号を入力してください。この際、最初の 0 と -（ハイフン）は不要なので、電話番号が 080-XXXX-YYYY の場合には、80XXXXYYYY と入力してください。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c9612223970b-pi" style="display: inline;"><img alt="2nd_factor_authentication_web2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c9612223970b image-full img-responsive" src="/assets/image_153886.jpg" title="2nd_factor_authentication_web2" /></a></p>
<p>[次へ]&#0160; をクリックすると、入力したスマートフォンの電話番号にテキスト メッセージが送られます。送り主は米国カルフォルニア州の電話番号からになっているはずです（国番号 +1 ＝ 米国、市外局番 415 ＝ カルフォルニア）。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2eb6941970c-pi" style="display: inline;"><img alt="2nd_factor_authentication_mobile1" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2eb6941970c img-responsive" src="/assets/image_820005.jpg" style="width: 400px;" title="2nd_factor_authentication_mobile1" /></a></p>
<p style="text-align: left;">下記の Web ページ上部に表示されてる Expires in 1:27 は、セキュリティ コードの有効時間です（下記の場合、1分27秒）。この時間内に受信したセキュリティ コードを入力して [コードを入力] をクリックしてください。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2eb5ed5970c-pi" style="display: inline;"><img alt="2nd_factor_authentication_web3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2eb5ed5970c image-full img-responsive" src="/assets/image_865100.jpg" title="2nd_factor_authentication_web3" /></a></p>
<p>次のページが表示されれば、2 段階認証の設定は完了です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2eb5f1c970c-pi" style="display: inline;"><img alt="2nd_factor_authentication_web4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2eb5f1c970c image-full img-responsive" src="/assets/image_100968.jpg" title="2nd_factor_authentication_web4" /></a></p>
<p>以後、同じアカウントでサインインをすると、ユーザ名とパスワードの入力後にセキュリティ コードの入力を求められるようになります。直前に登録した電話番号にセキュリティ コードが送られているはずなので、その値を入力してサインインをしてください。&#0160;</p>
<p>2 段階認証の設定は、Autodesk Accounts ページでいつでも停止、または、変更することが可能です。冒頭でお知らせしたブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/11/a360-2-step-verification.html" rel="noopener noreferrer" target="_blank">A360 の 2 段階認証</a></strong> か、Autodesk Knowledge Network 記事&#0160;<strong><a href="https://knowledge.autodesk.com/ja/search-result/caas/sfdcarticles/sfdcarticles/JPN/Autodesk-Two-Factor-Authentication-FAQ.html" rel="noopener noreferrer" target="_blank">オートデスクの 2 段階認証の FAQ</a></strong> を参照してみてください。</p>
<p>By Toshiaki Isezaki</p>
