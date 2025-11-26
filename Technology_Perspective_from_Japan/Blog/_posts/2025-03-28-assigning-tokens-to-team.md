---
layout: "post"
title: "チームへのトークン割り当て"
date: "2025-03-28 09:02:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/03/assigning-tokens-to-team.html "
typepad_basename: "assigning-tokens-to-team"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2025/03/aps-introduces-team-assignment.html" rel="noopener" target="_blank"><strong>APS にチームの割り当てを導入</strong></a>&#0160;にともない、アプリ作成時、または既存アプリにチーム割り当ての項目が設置されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860e7e546200b-pi" style="display: inline;"><img alt="Team_assignment_on_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860e7e546200b img-responsive" src="/assets/image_578754.jpg" title="Team_assignment_on_app" /></a></p>
<p>この設定によって、アプリが課金対象の API を使用した際のトークン消費が、割り当てたチームから差し引かれるようになります。チームに関しての詳細は、<strong><a href="https://www.autodesk.com/jp/support/account/admin/users/manage-teams" rel="noopener" target="_blank">チームの管理</a></strong>&#0160; をご確認ください。</p>
<p>もし、割り当てたチームに十分なトークン残高がない場合には、アプリを所有するアカウントの My applications ページ（<strong><a href="https://aps.autodesk.com/hubs/@personal/applications/" rel="noopener" target="_blank">https://aps.autodesk.com/hubs/@personal/applications/</a></strong>）を表示した際に、<span style="font-family: helvetica; background-color: #d85528; color: #ffffff;">&#0160;<strong>Uncharged APS usage&#0160;</strong></span>&#0160;のメッセージが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d13084200c-pi" style="display: inline;"><img alt="Uncharged_aps_usage" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d13084200c image-full img-responsive" src="/assets/image_756728.jpg" title="Uncharged_aps_usage" /></a></p>
<p>同じように、アプリ所有者には「<strong>Uncharged usage for your Autodesk team</strong>」のタイトルを持つ次のようなメールが届きます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d15e41200c-pi" style="display: inline;"><img alt="Uncharged_usage_for_your_autodesk_ team" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d15e41200c image-full img-responsive" src="/assets/image_238553.jpg" title="Uncharged_usage_for_your_autodesk_ team" /></a></p>
<p>トークンを購入して十分な残高があるような場合、チームにトークンが割り当てられていない可能性がります。チームへのトークン割り当ては、次の手順でおこなうことが出来ます。なお、トークンの割り当てが可能なのは、トークンを購入したアカウントが作成したチームのみなります。他のアカウントが作成したチームへのトークン割り当ては出来ません。</p>
<ul>
<li><a href="https://www.autodesk.com/jp/support/technical/article/caas/sfdcarticles/sfdcarticles/JPN/How-to-transfer-software-or-subscription-to-a-new-team.html"><strong>ソフトウェアまたはサブスクリプションを新しいチームに移行する方法</strong></a></li>
<li><strong>（動画）</strong><a href="https://help.autodesk.com/videos/b9e17f10-ec61-11ee-b0de-3725a07fd20a/video.mp4"><strong>autodesk.com/videos/b9e17f10-ec61-11ee-b0de-3725a07fd20a/video.mp4</strong></a></li>
</ul>
<p>なお、チームに対するトークンをについてまとめと、次のようになります。</p>
<ul>
<li>チーム割り当ての導入前は、アプリ オーナー アカウントに対して API 使用量に応じた従量課金がおこなわれていましたが、チーム割り当ての導入後は、アプリ毎に割り当てられたチームに対してAPI 使用量に応じた従量課金がおこなわれます。このため、以後、アプリ オーナーである個々のアカウントへのトークンの割り当ては、以前と同じようには機能しません。</li>
<li>チームに割り当てられたトークン残高がない場合、またはトークンが関連付けられていない場合、消費量はチームにマイナストークンとして記録されます。</li>
<li>記録されたマイナストークンは、トークンを新規に購入、あるいは購入から 1 年の有効期限が切れていない有効なトークンをチームに割り当てると、マイナス分が相殺されます。</li>
<li>マイナス トークンを作成したアプリが、トークンを持つ別のチームに割り当てられた場合でも、記録されたマイナストークンは元のチームに残ります。</li>
<li>マイナスのトークン残高は、トークンを購入するか、既存のトークンプールをチームに割り当てて相殺する必要があります。</li>
<li>マイナス残高のあるチームに割り当てるトークンは、チームを編成した契約管理者ロールを持つアカウントで購入する必要があります。プライマリ管理者 - 既定では、最初の購入者（契約管理者とも呼ばれます）がプライマリ管理者のロールを引き継ぎます。<br />ご参考：<a href="https://www.autodesk.com/jp/support/account/admin/users/roles">オートデスク管理者 | ユーザ管理 | 管理者の役割</a></li>
</ul>
<p>By Toshiaki Isezaki</p>
