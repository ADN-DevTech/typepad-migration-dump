---
layout: "post"
title: "Data Management API：application/vnd.api+json ヘッダーを使った圧縮有効化"
date: "2021-12-13 00:03:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/12/data-management-apinow-enable-compression-for-applicationvndapijson.html "
typepad_basename: "data-management-apinow-enable-compression-for-applicationvndapijson"
typepad_status: "Publish"
---

<p style="font-weight: 400;">従来、Data Management APIでは、クライアント側のヘッダーに<strong>Accept-Encoding: gzip</strong> が設定されていても、コンテンツタイプが <strong>application/vnd.api+json</strong> の場合は圧縮が有効にならないため、一部の Data Management API ではレスポンスのペイロードが非常に大きくなっていました。</p>
<p style="font-weight: 400;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278805f39a5200d-pi" style="display: inline;"><img alt="With-without-compression" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278805f39a5200d image-full img-responsive" src="/assets/image_792561.jpg" title="With-without-compression" /></a></p>
<p style="font-weight: 400;">パフォーマンスを向上させるため、2021年11月22日から&#0160;<strong>application/vnd.api+json</strong> のコンテンツタイプの圧縮を有効になっています。これにより、クライアント側のヘッダーに A<strong>ccept-Encoding: gzip</strong> が設定されている場合は、レスポンスのペイロードが圧縮されるようになります。&#0160;</p>
<p style="font-weight: 400;"><strong>この変更による既存の Forge アプリへの影響は？</strong></p>
<ol>
<li>NodeJS や .NET SDK が含む Forge SDK をお使いの場合には、本変更による Forge アプリへの影響はありません。</li>
<li>Data Management API を直接呼び出していて、かつ、ヘッダーに <strong>Accept-Encoding: gzip</strong> を設定している場合には、圧縮されたレスポンスを受け取ることになりますので、圧縮されたコンテンツをクライアント側で処理するか、圧縮されたコンテンツを処理したくない場合は <strong>Accept-Encoding: gzip</strong> を削除してください。&#0160;</li>
</ol>
<p style="font-weight: 400;">他に問題お持ちの場合、また、ご質問をお持ちの場合には、直接 forge.help@autodesk.com までご連絡をお願いいたします。</p>
<p style="font-weight: 400;">By Toshiaki Isezaki</p>
