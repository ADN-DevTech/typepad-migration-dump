---
layout: "post"
title: "クラウド クレジットについて"
date: "2014-12-24 23:27:45"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/12/cloud-credit.html "
typepad_basename: "cloud-credit"
typepad_status: "Publish"
---

<p>A360 クラウド サービスの中で、<a href="https://rendering-gallery.360.autodesk.com/projects/all#" target="_blank"><strong>Rendering in Autodesk A360</strong></a>&#0160;や <a href="http://recap360.autodesk.com/" target="_blank"><strong>Autodesk&#0160;ReCap 360</strong></a>&#0160;などの演算処理サービスでは、演算サービスを実行するたびに、<strong>クラウド クレジット</strong> という消費単位が減算される仕組みを持っています。このクラウド クレジットは、ある意味、有償サービスの課金にあたるものです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7277c24970b-pi" style="display: inline;"><img alt="Cloud_credit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7277c24970b image-full img-responsive" src="/assets/image_537080.jpg" title="Cloud_credit" /></a></p>
<p>クラウド クレジットには、個別 と 共有 の&#0160;2 つのタイプがあります。レンダリング処理の場合には、処理指定の方法が多様になるケースがあるので、下記の内容を理解しておく必要があります。</p>
<p style="padding-left: 30px;">個別:</p>
<p style="padding-left: 30px;">ライセンスで認められた名前のユーザに与えられます。これは、他のユーザに転送したり、共有することはできません。</p>
<p style="padding-left: 30px;">共有:</p>
<p style="padding-left: 30px;">共有契約（Subscription）で認められた名前のユーザが利用できます。</p>
<p>レンダリング時には、1 回に 1 つのクレジット タイプのみを使用することができます。処理を完了するためには、個別と共有のいずれかに十分なクラウド クレジットが残っている必要があります。つまり、1 つのレンダリング イメージに、2 つのタイプを組み合わせて使用することはできません。たとえば、個別クレジットが 5、共有クレジットが 10 ある場合、レンダリングに 11 クレジット必要であると、レンダリングは実行できません。これは、いずれのタイプにも十分なクレジットがなく、レンダリングにこれらのクレジットを組み合わせることができないためです。</p>
<p>個別クラウド クレジットと共有クラウド クレジットを所有している場合には、その使用順序あらかじめ決められています。具体的には、レンダリングに十分な個別クレジットがある場合、これが共有クレジットより先に使用されます。共有クレジットは、十分な個別クレジットがない場合にのみ使用されます。</p>
<p>1つの 3D モデルに対して、同時に複数のレンダリングをおこなう場合には、各レンダリング処理は、別個のレンダリングとしてクラウド クレジットが消費されます。指定したすべてのビューをレンダリングするには、少なくとも 1 つのクレジット タイプに十分なクレジットが必要です。すべてのビューに必要なクラウド クレジットなない場合には、レンダリングは実行されません。&#0160;</p>
<p>現在のアカウントで、どれくらいのクラウド クレジットを利用することが可能か、管理画面でチェックすることも可能です。<a href="http://accounts.autodesk.com" target="_blank">https://accounts.autodesk.com</a>&#0160;サインインして、ページ左側の [レポート] リンクから、「クラウドサービスの使用状況」をクリックしてみてください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07e0c716970d-pi" style="display: inline;"><img alt="Confirm_cloud_credit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb07e0c716970d image-full img-responsive" src="/assets/image_719085.jpg" title="Confirm_cloud_credit" /></a></p>
<p>ここで表示される内容は、もちろんアカウント毎に異なります、同じ契約内でも、ソフトウェア コーディネーターとなっている方のアカウントの場合も同様です。</p>
<p>By Toshiaki Isezaki</p>
