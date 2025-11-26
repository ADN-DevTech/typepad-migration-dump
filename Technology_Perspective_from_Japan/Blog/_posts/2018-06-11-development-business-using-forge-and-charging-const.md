---
layout: "post"
title: "Forge を使った開発ビジネスと課金について"
date: "2018-06-11 01:19:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/06/development-business-using-forge-and-charging-const.html "
typepad_basename: "development-business-using-forge-and-charging-const"
typepad_status: "Publish"
---

<p>Forge はクラウドを利用した開発プラットフォームです。今回は、Forge を使って開発されたアプリやサービスのエンドユーザへの課金について、開発ビジネスの視点で、よくお問合せいただく内容に沿って補足していきたいと思います。&#0160;</p>
<p><strong>Forge 開発に契約が必要ですか？</strong></p>
<p style="padding-left: 30px;">Forge を使った開発を開始する際には、オートデスクとの明示的な契約やなんらかの支払いの義務はありません。強いて言えば、<strong><a href="https://forge.autodesk.com/" rel="noopener noreferrer" target="_blank">Forge ポータル </a></strong>サイトへ&#0160;Autodesk ID を使ってサインインしてから、開発時に必要な Developer Key（Client ID と Client Secret） を取得する必要であるため、Autodesk ID の登録行為がオートデスクとの契約と考えることも可能です。なぜなら、Autodesk ID の登録時には、<a href="https://www.autodesk.com/company/legal-notices-trademarks/terms-of-service-autodesk360-web-services/forge-platform-web-services-api-terms-of-service" rel="noopener noreferrer" target="_blank">Autodesk Web Service API サービス使用条件</a>、および、<a href="https://www.autodesk.com/company/legal-notices-trademarks/privacy-statement" rel="noopener noreferrer" target="_blank">Autodesk プライバシー ステートメント</a> に同意する必要があるためです。もっとも、Autodesk ID の登録も&#0160;Developer Key の取得も&#0160;<strong><span style="text-decoration: underline;">無償</span>&#0160;</strong>なので、契約書等への捺印や署名はありませんし、そのような手続きは不要です。</p>
<p style="padding-left: 30px;">なお、Autodesk ID の登録と Developer Key の取得手順については、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener noreferrer" target="_blank">Forge API を利用するアプリの登録とキーの取得</a></strong> にまとめていますので必要に応じて確認してみてください。</p>
<p><strong>Forge 課金は誰に対しておこなわれますか？</strong></p>
<p style="padding-left: 30px;">具体的な API 別の課金の仕組みは&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html" rel="noopener noreferrer" target="_blank">Forge 課金について</a></strong> でご紹介しているとおりですが、重要なのは、<strong>Forge で課金する（クラウド クレジット消費）の請求先が、Autodesk ID に登録されているメールアドレスの所有者に対しておこなわれる</strong>、という点です。</p>
<p style="padding-left: 30px;">そして、Autodesk ID に登録されているメールアドレスは、その作成後に変更することも出来ますが、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/01/autodesk-id-and-forge.html" rel="noopener noreferrer" target="_blank">Autodesk ID と Forge</a></strong> でご案内のとおり、主にサブスクリプション契約で使用される Autodesk ID の性格上、残念ながら、<strong>異なるオーナー企業にまたがって電子メール アドレスを書き換える行為は許諾されません</strong>のでご注意ください。</p>
<p><strong>オートデスクはユーザ タイプによって Forge の課金内容を変えることがありますか？</strong></p>
<p style="padding-left: 30px;">いいえ、クラウド クレジットによる課金内容は&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html" rel="noopener noreferrer" target="_blank">Forge 課金について</a></strong> でご紹介した内容以外、従量課金制度や内容が変わることはありません。</p>
<p style="padding-left: 30px;">一部、オートデスクと直接、エンタープライズ プライオリティ サポート契約、ないし、Enterprise 契約をしている場合には、特典である Token&#0160;Flex をクラウド クレジットに置き換えることが可能です。この場合、契約期間内で固定費としてコストを捉えることも出来るわけです。エンタープライズ プライオリティ サポート契約については、<a href="https://www.autodesk.co.jp/support-offerings" rel="noopener noreferrer" target="_blank">https://www.autodesk.co.jp/support-offerings</a> をご確認ください。</p>
<p style="padding-left: 30px;">また、BIM 360 など、オートデスクにクラウド サービスをサブスクライブしているお客様向けに Forge アプリを開発した場合、本来、Model Derivative API で実施するデザイン ファイル変換をサブスクライブしているクラウド サービス側で代替出来るため、サブスクリプションの固定費で Forge 課金を代替させる、といった考え方を当てはめることも可能と思います。</p>
<p style="padding-left: 30px;">余談ですが、上記いずれの場合も、Forge を含むオートデスクのクラウド サービスが構築されている AWS（Amazon Web Service）から、オートデスク自身が 従量課金されている点に変わりありません。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad397f1c3200b-pi" style="display: inline;"><img alt="Cloud_business_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad397f1c3200b image-full img-responsive" src="/assets/image_772424.jpg" title="Cloud_business_model" /></a></p>
<p><strong>（開発会社の立場で）独自開発した Forge を使ったアプリについて、お客様に独自の課金を適用することは可能でしょうか？</strong></p>
<p style="padding-left: 30px;">先に触れたとおり、オートデスクは Forge アプリの開発で使用されている Developer Key が、どの Autodesk ID で取得されたかを調べ、その Autodesk ID に登録されたメールアドレス所有者にクラウド クレジット消費による課金をおこないます。もし、貴社で開発された Forge アプリが貴社で登録している Autodesk ID で Developer Key を取得、開発されているなら、課金は貴社に対しておこなわれることになります。</p>
<p style="padding-left: 30px;">この場合、貴社アプリのお客様には、貴社とお客様の利用契約に基づいて課金をしていただいて結構です。オートデスクは、貴社とお客様の利用契約、あるいは、貴社のお客様への課金方法について関知することはありません。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad352149c200c-pi" style="display: inline;"><img alt="Forge_business_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad352149c200c image-full img-responsive" src="/assets/image_482218.jpg" title="Forge_business_model" /></a></p>
<p><strong>（開発会社の立場で）受託開発したアプリの本格稼働後（運用時）も&#0160;お客様へ独自課金することは許諾されますか？</strong></p>
<p style="padding-left: 30px;">繰り返しになりますが、オートデスクが課金するのはアプリで利用する Developer Key 取得者（Autodesk ID に登録されたメールアドレス所有者）です。もし、アプリ稼働後に貴社アプリがお客様の Autodesk ID で取得した Developer Key を使って運用されている場合には、&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html" rel="noopener noreferrer" target="_blank">Forge 課金について</a></strong> でご紹介した従量課金を貴社アプリのお客様に対して実施することになります。もし、貴社アプリが貴社の Autodesk ID で取得した Developer Key を使っている場合には、課金は貴社に対しておこなわれることになります。</p>
<p style="padding-left: 30px;">つまり、貴社とお客様の利用契約に基づいて課金いただいて結構です。受託開発を開始する時点で、Forge 課金に関わるコストについて、どちらが取得した Developer Key を利用するか、開発時や運用時のコストをどちらがカバーするか、など、あらかじめ取り決めをしておくことをお勧めします。オートデスクは、貴社とお客様の利用契約、あるいは、貴社のお客様への課金方法について関知することはありません。もちろん、受託開発費用は貴社の基準でご判断ください。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad397e92d200b-pi" style="display: inline;"><img alt="Forge_business_model2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad397e92d200b image-full img-responsive" src="/assets/image_727707.jpg" title="Forge_business_model2" /></a></p>
<p><strong>開発時の Forge 課金は免除されますか？</strong></p>
<p style="padding-left: 30px;">Forge の利用において、オートデスクは、それが開発時のものか、運用時のものなのかを判断する手段を持ちません。同様に、開発期間中に Forge 利用に対するクラウド クレジット課金を免除する有償プログラムありません。唯一、無償で Forge トライアルを提供するのみです。もちろん、トライアルが終了した時点で &#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/01/about-charging-to-forge.html" rel="noopener noreferrer" target="_blank">Forge 課金について</a></strong> でご紹介した従量課金の適用対象となります。Forge トライアルについては、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/04/how-to-get-started-forge.html" rel="noopener noreferrer" target="_blank">Forge の始め方</a></strong>&#0160;でもご案内しています。</p>
<p style="padding-left: 30px;">なお、オートデスクのデスクトップ製品 API を使った開発サポートプログラムに、有償の <strong><a href="https://www.autodesk.co.jp/developer-network/membership" rel="noopener noreferrer" target="_blank">Autodesk Developer Network（ADN）</a></strong>があります。ADN では、開発利用目的限定で、デスクトップ製品に加えて、クラウドクレジットとともに一部クラウドサービスを提供しています。ただし、ここで提供されるクラウド クレジットを Forge 利用に転換することは出来ません（クラウド クレジット自体は、<a href="https://rendering-beta.360.autodesk.com" rel="noopener noreferrer" target="_blank">レンダリング サービス</a>等、一般のクラウド サービスで消費されるものと同一ですが、Forge 利用への関連付けが必要なため）。</p>
<p><strong>開発時と運用時で Developer Key を変えることは出来ますか？</strong></p>
<p style="padding-left: 30px;">可能です。ただし、2-leggded OAuth で Bucket を使った処理をしている場合は、Bucket にアクセス出来るアプリが Bucket 作成時のアプリに限定されてしまうので、途中で Developer Key を変えてしまうと、以後、Bucket へのアクセスが拒否されてしまうので注意してください。<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/summary-about-bucket.html" rel="noopener noreferrer" target="_blank">Bucket に関してのサマリー </a></strong>もご確認ください。</p>
<p>By Toshiaki Isezaki&#0160;</p>
