---
layout: "post"
title: "非推奨の POST CreateFolder Command endpoint"
date: "2021-06-29 00:01:35"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/06/post-command-createfolder-deprecated.html "
typepad_basename: "post-command-createfolder-deprecated"
typepad_status: "Publish"
---

<div class="node__image"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788033d0e3200d-pi" style="display: inline;"><img alt="Shutterstock_1783494773" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788033d0e3200d image-full img-responsive" src="/assets/image_681829.jpg" title="Shutterstock_1783494773" /></a></div>
<div class="node__body">
<div class="field-body">
<p>Forge の Data Management API で利用するストレージ上にフォルダを作成する際には、BIM 360 Docs、Autodesk Docs、Fusion Team など、あらゆる種類のオートデスク SaaS プロジェクトで使用することが可能な&#0160;<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-POST/" rel="noopener" target="_blank">POST projects/:project_id/folders</a> endpointの利用をお勧めしています。BIM 360 Docs にフォルダを作成する<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/CreateFolder/" rel="noopener" target="_blank"> POST Command CreateFolder</a> endpoint も利用することが出来ますが、現在、この endpoint は非推奨扱いとなっていますのでご注意ください。</p>
<p><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/CreateFolder/" rel="noopener" target="_blank">POST Command CreateFolder</a> endpoint のドキュメントでは、次のような記載が開始されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e10c4363200b-pi" style="display: inline;"><img alt="Depreation_warning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e10c4363200b image-full img-responsive" src="/assets/image_599012.jpg" title="Depreation_warning" /></a></p>
<p>「<strong>2022 年 1 月 10 日にシステムから削除される予定です。代替として、フォルダ作成には <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-POST/" rel="noopener" target="_blank">POST projects/:project_id/folders</a> endpoint をお使いください。</strong>」</p>
<p>上記のとおり、<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/CreateFolder/" rel="noopener" target="_blank">POST Command CreateFolder</a> endpoint は、2022 年 1 月初旬まで既存のアプリケーションで動作しますが、お早目に <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-POST/" rel="noopener" target="_blank">POST projects/:project_id/folders</a> endpointへの移行されることをお勧めします。</p>
<p>両 endpoint の重要な違いは、<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-POST/" rel="noopener" target="_blank">POST projects/:project_id/folders</a> endpoint には endpoint 毎の<span style="text-decoration: underline;">特定の</span>&#0160;<a href="https://forge.autodesk.com/en/docs/data/v2/developers_guide/rate-limiting/dm-rate-limits/#project-and-data-service-rate-limits" rel="noopener" target="_blank">レート制限（Rate Limit）</a>が適用されているのに対し、POST Command には可能なすべてのコマンドに対する<span style="text-decoration: underline;">全般的な</span>&#0160;レート制限（Rate Limit）が適用され点です。</p>
<p><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-POST/" rel="noopener" target="_blank">POST projects/:project_id/folders</a> endpoint は各フォルダに対して 1 つの呼び出しとなってしまうめ、<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/CreateFolder/" rel="noopener" target="_blank">POST Command CreateFolder</a> endpoint から移行する場合には、コードのロジック調整が必要になる場合があります。詳細については、同 endpoint の<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-POST/" rel="noopener" target="_blank">ドキュメント</a>をご確認してください。</p>
<p><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-POST/" rel="noopener" target="_blank"> POST projects/:project_id/folders</a> endpoint の使用方法、レート制限についてのご質問 やご相談がございましたら、<a href="mailto://forge.help@autodesk.com" rel="noopener" target="_blank">forge.help@autodesk.com</a> までご連絡ください。</p>
</div>
</div>
<p>By Toshiaki Isezaki</p>
