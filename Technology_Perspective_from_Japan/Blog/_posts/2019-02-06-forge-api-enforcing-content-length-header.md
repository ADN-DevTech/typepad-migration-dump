---
layout: "post"
title: "Content-length ヘッダー指定要件の必須化"
date: "2019-02-06 00:03:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/02/forge-api-enforcing-content-length-header.html "
typepad_basename: "forge-api-enforcing-content-length-header"
typepad_status: "Publish"
---

<div class="blog__content--full">
<div class="blog__body node__body">
<div>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bf74c9200d-pi" style="float: right;"><img alt="Data-management-api-blue" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bf74c9200d img-responsive" src="/assets/image_415128.jpg" style="margin: 0px 0px 5px 5px;" title="Data-management-api-blue" /></a>Forge Data Management API のパフォーマンス改善の一環として、Forge チームは Forge API <strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/forge-api-glossary.html#_apiproxy" rel="noopener" target="_blank">Proxy</a></strong> レイヤーでリクエスト/レスポンスのバッファリングを無効にする計画をしています。 必要な <strong>Content-Length</strong> ヘッダー情報なしで POST または PUT リクエストを送信した場合、Forge <strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/forge-api-glossary.html#_apiproxy" rel="noopener" target="_blank">Proxy </a></strong>は 2 月末からコミュニケーションを拒否する予定です。 この変更の影響を受ける最初の API は<strong> <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-PUT/" rel="noopener" target="_blank">OSS PUT objects</a></strong> と <strong><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-resumable-PUT/" rel="noopener" target="_blank">OSS PUT resumable</a></strong> のリクエストとなる予定ですが、今後、すべての POST PUT リクエストが影響を受ける可能性があります。</p>
<p>通常、ほとんどの SDK および REST クライアントはこのヘッダーを自動的に追加します。 REST プログラミングのベストプラクティスに従っていることを確認して、リクエストにヘッダー提供してください。 Forge チームはすでにこの問題を抱えている人々に直接連絡を取っているので、オートデスクから電子メールを受け取っていなければ、おそらく影響はないものと思いますが、念のため、ご確認をお願いいたします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3995cd8200c-pi" style="display: inline;"><img alt="Postman" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3995cd8200c image-full img-responsive" src="/assets/image_527120.jpg" title="Postman" /></a></p>
</div>
</div>
</div>
<div class="card-block-footer">
<div class="card-btn card-text">By Toshiaki Isezaki</div>
</div>
