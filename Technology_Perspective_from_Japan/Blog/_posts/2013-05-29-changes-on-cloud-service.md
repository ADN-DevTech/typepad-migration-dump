---
layout: "post"
title: "クラウド サービスの変更点について"
date: "2013-05-29 01:32:22"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/05/changes_on_cloud_service.html "
typepad_basename: "changes_on_cloud_service"
typepad_status: "Publish"
---

<p>2013年4月に Autodesk 360 クラウドサービスの一部に変更が加えられました。この変更には、Subscription に加入いただいているお客さまに影響のある内容も含まれますので、今日はその変更点についてご案内します。</p>
<p><strong>ストレージ容量の変更</strong></p>
<p style="padding-left: 30px;">従来、Autodesk 360 にアカウントをお持ちで Subscription に未加入のお客様には 3GB、Subscription に加入いただいているお客さまには 25GB のストレージ容量を、アカウント毎に提供してきました。今回の変更で、<strong>&#0160;Subscription に未加入のお客様に 5GB のストレージ容量</strong>を提供するよう変更が加えられました。Subscription に加入いただいているお客様には、いままでとおり、25GB のストレージ容量が提供されます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901c95ba41970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="StorageSize" class="asset  asset-image at-xid-6a0167607c2431970b01901c95ba41970b" src="/assets/image_253270.jpg" title="StorageSize" /></a></p>
<p><strong>演算処理サービスの消費単位の変更</strong></p>
<p style="padding-left: 30px;">Autodesk 360 Rendering、Autodesk 360 Optimization for Inventor など、クラウドに演算をさせる演算処理サービスは、1 回のサービス実行で決まった数の単位を消費する仕組みを導入してきました。Autodesk&#0160;360 の前身である Autodesk Cloud の時代には、この消費単位を<strong> クラウド ユニット</strong> と定義されて、昨年の9月に名称を <strong>ジョブ</strong> に変更しましたが、今回、新たに <strong>クラウド クレジット</strong> と呼称を再変更しています。クラウド クレジットを含め、クラウド ユニットやジョブだった頃も、この単位が提供されるのは、デスクトップ製品をお持ちで、Subscription 契約にご加入いただくことが提供の前提となっていました。</p>
<p style="padding-left: 30px;">消費単位がクラウド ユニットやジョブだった頃、それらは、Suite 製品の各エディション毎に、提供されるクラウド ユニット/ジョブ数が変わっていましたが、今回の変更で4月21日以降、一律 <strong>100 クラウド クレジット</strong>への提供に変更されています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901c95e148970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="CloudCredit" class="asset  asset-image at-xid-6a0167607c2431970b01901c95e148970b" src="/assets/image_461406.jpg" title="CloudCredit" /></a></p>
<p style="padding-left: 30px;">また、いままで所有していたクラウド ユニット/ジョブは、4月21日の段階でリセットされ、継承することは出来なくなっています。なお、各種 LT 単体製品を除く単体製品をお持ちで Subscription に加入いただいている場合も、100 クラウド ユニットが提供されることになっています。</p>
<p style="padding-left: 30px;">クラウド クレジットへの呼称変更に加えて、各種演算処理サービスを実行された場合の消費数についても、次のように見直しが加えられています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901c95eb42970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="ConsumedClousCredit" class="asset  asset-image at-xid-6a0167607c2431970b01901c95eb42970b" src="/assets/image_264561.jpg" title="ConsumedClousCredit" /></a>&#0160;</p>
<p style="padding-left: 30px;">AutoCAD LT Civil&#0160;Suite 製品には、クラウド クレジットの提供はありません。</p>
<p style="padding-left: 30px;">上図では、Autodesk 360 Rendering を利用する際に消費されるクラウド クレジット数が 不定 となっています。これは、レンダリングする品質や画角、静止画、パノラマ、などのレンダリング種別によって、消費数を変化させる手法を採用しているためです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901c95f318970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RenderingConsumedCluodCredit" class="asset  asset-image at-xid-6a0167607c2431970b01901c95f318970b" src="/assets/image_746736.jpg" title="RenderingConsumedCluodCredit" /></a></p>
<p style="padding-left: 30px;">Autodesk 360 Rendering では、従来の品質や画角も変更されています。画角では、最大 4000×4000 ピクセルまで指定できるようになっています。&#0160;</p>
<p><strong>追加クラウド クレジットの購入</strong></p>
<p style="padding-left: 30px;">提供された 100 クラウド クレジットを使い切ってしまった場合、新たにクラウド クレジットを購入できるようになりました、購入は <strong>100 クラウド クレジット単位で 15,750 円（税込）</strong>です。現在のところ、クラウド クレジットの購入は、オートデスク販売店経由でのみおこなっています。購入を検討されている場合には、オートデスク製品を購入いただいた販売点のご相談ください。購入の際には、オートデスク製品のシリアル番号と Subscription 契約番号が必要になります。</p>
<p><strong>Subscription&#0160;ユーザに提供されるサービスの追加</strong>&#0160;</p>
<p style="padding-left: 30px;">Suite 製品をお持ちで Subscription&#0160;に加入いただいている&#0160;には、いくつかの Suite 製品とエディションで、利用可能なサービスが追加されています。次の表で <span style="color: #0000ff;"><strong>青字</strong></span> で記載されたサービスが、新たに利用できるようになったサービスです。</p>
<p>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192aa547874970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="NewSerivceForSuite" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0192aa547874970d image-full" src="/assets/image_833267.jpg" title="NewSerivceForSuite" /></a></p>
<p><strong>ご注意</strong></p>
<p>Subscription に加入しているにもかかわらず、Autodesk 360&#0160;&#0160;にサインインしてアカウント設定画面で提供されるストレージ容量やクラウド クレジットが正しく表示されない場合には、お使いの<strong> Autodesk ID</strong> と<strong> Subscription 契約番号</strong>が正しく関連付けられていない可能性があります。その場合には、オートデスクか製品をお買い求めいただいた販売店にお問い合わせください。&#0160;</p>
<p>By Toshiaki Isezaki</p>
