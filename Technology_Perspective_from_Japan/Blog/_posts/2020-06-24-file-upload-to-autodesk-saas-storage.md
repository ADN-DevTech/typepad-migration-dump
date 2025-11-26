---
layout: "post"
title: "オートデスク SaaS ストレージへのファイル アップロード"
date: "2020-06-24 01:52:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/06/file-upload-to-autodesk-saas-storage.html "
typepad_basename: "file-upload-to-autodesk-saas-storage"
typepad_status: "Publish"
---

<div class="node__content adskf__section-group">
<div class="node__body">
<p>OSS Bucket へのファイル アップロードでは、<strong><a href="https://developer.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-PUT/" rel="noopener" target="_blank">PUT object</a></strong> endpoint を使って作成した Bucket 名を指定してアップロードしているはずで。この際、<strong>:bucketKey </strong>で指定するアップロード先の Bucket 名は、通常、開発中の Forge アプリで作成したものなので自明なはずです。</p>
<div class="display--table">
<div class="method-badge method-badge--patch" style="padding-left: 40px;">PUT buckets/<wbr /><strong>:bucketKey</strong>/<wbr />objects/<wbr />:objectName</div>
<div class="method-badge method-badge--patch" style="padding-left: 40px;">&#0160;</div>
<div class="method-badge method-badge--patch" style="padding-left: 40px;">例）https://developer.api.autodesk.com/oss/v2/buckets/<strong>japan-test-transient-202007</strong>/objects/chair.f3d</div>
</div>
<p>A360 などのオートデスク SaaS が使用するストレージにファイルをアップロードする場合も、使用する endpoint は同じです。この際、アップロード先は、下図左手のストレージ構造に沿って特定の「フォルダ」になるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9517c9a200b-pi" style="display: inline;"><img alt="Saas_storage_structure" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9517c9a200b image-full img-responsive" src="/assets/image_306362.jpg" title="Saas_storage_structure" /></a></p>
<p>オートデスク SaaS が使用するストレージも、最下層では、オートデスクの OSS（Object Storage Service）配下の Bucket 名で表現することが可能で、その Bucket 名が <strong>wip.dm.prod</strong>&#0160;になっています。</p>
<p>ただ、<strong>wip.dm.prod</strong> のオーナーは A360、Fusion Team、BIM 360 Docs などの SaaS アプリであるため、<strong>wip.dm.prod</strong> 配下に自由にファイルをアップロードすることは出来ません。</p>
<p>このため、SaaS ストレージにファイルをアップロードする際には、フォルダを特定した後、ファイルのアップロード領域となる Storage 領域を作成する必要があるわけです。</p>
<p>オブジェクト名（:objectName）とともに Storage を作成すると、相当する ID が、都度、返されます。<strong><a href="https://developer.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-PUT/" rel="noopener" target="_blank">PUT object</a></strong> endpoint を使ったアップロード時には、その<span style="color: #0000ff;"><strong> ID</strong></span> を指定することになります。</p>
<p style="padding-left: 40px;">例）https://developer.api.autodesk.com/oss/v2/buckets/<strong>wip.dm.prod</strong>/objects/<span style="color: #0000ff;"><strong>48f8dcde-ebb2-4944-b191-34b66990aa93.f3d</strong></span></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9517c55200b-pi" style="display: inline;"><img alt="File_upload" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9517c55200b image-full img-responsive" src="/assets/image_194011.jpg" title="File_upload" /></a></p>
<p>作成した Storage にファイルをアップロードしても、そのままでは SaaS のユーザインタフェース上（フォルダ内一覧）には表示されない点に注意してください。アップロード後にバージョン付けが必要なためです。逆に、バージョン付けの処理が完了すれば、SaaS のユーザインタフェースに表示されるようになります。</p>
<p>オートデスク SaaS が使用するストレージへのファイル アップロードの詳細は、次の Forge ポータルのドキュメントに記載されています。</p>
<p style="padding-left: 40px;"><strong><a href="https://developer.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="noopener" target="_blank">https://developer.autodesk.com/en/docs/data/v2/tutorials/upload-file/</a>&#0160;&#0160;</strong></p>
<p>By Toshiaki Isezaki</p>
</div>
</div>
