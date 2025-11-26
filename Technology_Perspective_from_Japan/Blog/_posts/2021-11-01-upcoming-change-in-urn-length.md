---
layout: "post"
title: "URN 長の変更について"
date: "2021-11-01 00:28:03"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/11/upcoming-change-in-urn-length.html "
typepad_basename: "upcoming-change-in-urn-length"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788054a91a200d-pi" style="display: inline;"><img alt="Aec-digital-twin-image-05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788054a91a200d image-full img-responsive" src="/assets/image_764782.jpg" title="Aec-digital-twin-image-05" /></a></p>
<p>Forge と BIM 360 のバックエンドシステムの変更に伴い、<span style="text-decoration: underline;">今後</span>リリースされる API では URN 長の変更が予定されていますので、念のため、ご案内いたします。</p>
<p>URN の長さが長くなりますが、この長さがお手持ちのアプリにハードコード（文字列固定値、あるいは定数として埋め込み）されていないことをご確認ください。万が一ハードコードされている場合には、今後変更が予定されていますので、修正をお願いいたします。</p>
<p>次に例を示します。</p>
<p style="padding-left: 40px;"><strong>既存の URN フォーマット<br /></strong><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">urn:adsk.wipmaster:fs.folder:co.9_gmRI9BTFiPVBN_QbtYsg</span><strong><br /></strong></p>
<p style="padding-left: 40px;"><strong>新しい URN フォーマット<br /></strong><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">urn:adsk.wipmaster:fs.folder:co.mATL82RlTii5Cm7-aFu3Gg9_gmRI9BTFiPVBN_QbtYs</span><strong><br /></strong></p>
<p>URN 長が長くなっていることが分かります。URN の長さをチェックする処理を実装しているアプリは、次の点にご注意いただきたいと思います。</p>
<p>この URN の変更は、フォルダ、アイテム、バージョンなど、すべてのタイプの URN に影響します。また、URNの長さは固定されておらず、現時点では242文字までとなっています。</p>
<p>既存の URN には変更はなく、新しいエンティティのみがこれらの新しい URN フォーマットを取得します。</p>
<p>これは単なる予防的な情報ですが、廃止や移行の時期はまだ発表されていません。</p>
<p>By Toshiaki Isezaki</p>
