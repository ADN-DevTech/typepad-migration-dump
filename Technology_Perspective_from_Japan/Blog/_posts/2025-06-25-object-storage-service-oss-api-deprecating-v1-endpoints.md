---
layout: "post"
title: "Object Storage Service (OSS) API: v1 エンドポイント廃止"
date: "2025-06-25 07:19:34"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/06/object-storage-service-oss-api-deprecating-v1-endpoints.html "
typepad_basename: "object-storage-service-oss-api-deprecating-v1-endpoints"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d63c6d200c-pi" style="display: inline;"><img alt="Placeholder - Blog images_Lifestyle 16x9 1920x1080" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d63c6d200c image-full img-responsive" src="/assets/image_175697.jpg" title="Placeholder - Blog images_Lifestyle 16x9 1920x1080" /></a></p>
<p>Forge の前身である <a href="https://adndevblog.typepad.com/cloud_and_mobile/2016/09/autodesk-forge-apis-migrating-from-v1-to-v2.html" rel="noopener" target="_blank">View and Data API</a> 時代から存在していた Data Management API の Object Storage Service（OSS） バージョン 1（v1）エンドポイントを廃止する予定です。2016 年の Forge 導入後、長期的なサポートを確保するように設計されたバージョン 2 (v2) エンドポイントを導入した経緯がありますが、<a href="https://adndevblog.typepad.com/technology_perspective/2022/03/data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach.html" rel="noopener" target="_blank">Data Management OSS (Object Storage Service) の Direct-to-S3 アプローチへの移行</a> に際して、ほぼ OSS v2 への移行は完了しているものと思います。</p>
<h4>必要なアクション</h4>
<p>アプリが OSS API v1 をまだ使用している場合は、<strong>2025 年 10 月 31 日までに v2 に移行する必要があります</strong>。同日以降。OSS v1 エンドポイントは機能を停止します。</p>
<p>使用状況データに基づいて、次の 5 つの v1 エンドポイントが 3rd party アプリによって使用されていることが確認されています。</p>
<ul role="list">
<li>POST oss/v1/buckets (バケットを作成)</li>
<li>DELETE oss/v1/buckets/:bucketKey (バケットを削除する)</li>
<li>GET oss/v1/buckets/:bucketKey/objects/:objectKey/details (特定のオブジェクトに関する詳細情報を取得します。</li>
<li>GET oss/v1/buckets/:bucketKey/objects/:objectKey (ダウンロード)</li>
<li>PUT oss/v1/buckets/:bucketKey/objects/:objectKey (アップロード)</li>
</ul>
<p lang="EN-US" xml:lang="EN-US">もし、上記の OSS v1 エンドポイントをお使いの場合には、出来るだけ早く OSS v2 への移行をお願いいたします。</p>
<h4 lang="EN-US" xml:lang="EN-US">移行ガイダンス</h4>
<p lang="EN-US" xml:lang="EN-US">移行作業を支援するために、代替使用を推奨する OSS v2 エンドポイントを下記にに示します。</p>
<table aria-rowcount="6" border="1" data-tablelook="1184" data-tablestyle="MsoNormalTable" dir="ltr">
<tbody>
<tr aria-rowindex="1" role="row">
<td data-celllook="4369" role="rowheader">
<p class="text-align-center"><strong>廃止対象の OSS v1 エンドポイント</strong></p>
</td>
<td data-celllook="4369" role="columnheader">
<p class="text-align-center"><strong>推奨される OSS v2 エンドポイント</strong></p>
</td>
</tr>
<tr aria-rowindex="2" role="row">
<td data-celllook="4369" role="rowheader">
<p>POST oss/<strong>v1</strong>/buckets (バケットを作成)</p>
</td>
<td data-celllook="4369">
<p><a  _msthash="116"  _msttexthash="347958" href="https://aps.autodesk.com/en/docs/data/v2/reference/http/buckets-POST/" rel="noreferrer noopener" target="_blank">POST oss/<strong>v2</strong>/buckets</a>&#0160;</p>
</td>
</tr>
<tr aria-rowindex="3" role="row">
<td data-celllook="4369" role="rowheader">
<p>DELETE oss/<strong>v1</strong>/buckets/:bucketKey (バケットを削除する)</p>
</td>
<td data-celllook="4369">
<p><a  _msthash="118"  _msttexthash="839878" href="https://aps.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-DELETE/" rel="noreferrer noopener" target="_blank">DELETE oss/<strong>v2</strong>/buckets/:bucketKey</a>&#0160;&#0160;</p>
</td>
</tr>
<tr aria-rowindex="4" role="row">
<td data-celllook="4369" role="rowheader">
<p>GET oss/<strong>v1</strong>/buckets/:bucketKey/objects/:objectKey/details (特定のオブジェクトに関する詳細情報を取得)</p>
</td>
<td data-celllook="4369">
<p><a  _msthash="120"  _msttexthash="50472331" href="https://aps.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-details-GET/" rel="noreferrer noopener" target="_blank">GET oss/<strong>v2</strong>/buckets/:bucketKey/objects/:objectKey/詳細</a>&#0160;</p>
</td>
</tr>
<tr aria-rowindex="5" role="row">
<td data-celllook="4369" role="rowheader">
<p>GET oss/<strong>v1</strong>/buckets/:bucketKey/objects/:objectKey (ダウンロード)</p>
</td>
<td data-celllook="4369">
<p>Direct-to-S3 を使用してダウンロードします。</p>
<ol role="list" start="1">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="" data-leveltext="%1." data-list-defn-props="{&quot;335552541&quot;:0,&quot;335559685&quot;:360,&quot;335559991&quot;:360,&quot;469769242&quot;:[65533,0],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;%1.&quot;,&quot;469777815&quot;:&quot;multilevel&quot;}" data-listid="5">
<p><a  _msthash="123"  _msttexthash="3144726" href="https://aps.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3download-GET/" rel="noreferrer noopener" target="_blank">GET oss/<strong>v2</strong>/buckets/:bucketKey/objects/:objectKey/<strong>signeds3download</strong></a><strong>&#0160;</strong></p>
</li>
</ol>
<ol role="list" start="2">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="" data-leveltext="%1." data-list-defn-props="{&quot;335552541&quot;:0,&quot;335559685&quot;:360,&quot;335559991&quot;:360,&quot;469769242&quot;:[65533,0],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;%1.&quot;,&quot;469777815&quot;:&quot;multilevel&quot;}" data-listid="5">
<p>上記で返された署名付き URL を使用して、AWS S3 にリクエストを行います。</p>
</li>
</ol>
<p>チュートリアルは<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/data/v2/tutorials/download-file/#step-6-get-the-s3-url" rel="noreferrer noopener" target="_blank">こちら</a>でご覧いただけます。</p>
</td>
</tr>
<tr aria-rowindex="6" role="row">
<td data-celllook="4369" role="rowheader">
<p>PUT /oss/<strong>v1</strong>/buckets/:bucketKey/objects/:objectKey (アップロード)</p>
</td>
<td data-celllook="4369">
<p>Direct-to-S3 を使用して、次のものをアップロードします。</p>
<ol role="list" start="1">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="" data-leveltext="%1." data-list-defn-props="{&quot;335552541&quot;:0,&quot;335559685&quot;:360,&quot;335559991&quot;:360,&quot;469769242&quot;:[65533,0],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;%1.&quot;,&quot;469777815&quot;:&quot;multilevel&quot;}" data-listid="7">
<p><a  _msthash="128"  _msttexthash="140661651" href="https://aps.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-GET/" rel="noreferrer noopener" target="_blank"><strong>GET</strong>&#0160;oss/<strong>v2</strong>/buckets/:bucketKey/objects/:objectKey<strong>/signeds3upload</strong>&#0160;を入手してください。</a>&#0160;</p>
</li>
</ol>
<ol role="list" start="2">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="" data-leveltext="%1." data-list-defn-props="{&quot;335552541&quot;:0,&quot;335559685&quot;:360,&quot;335559991&quot;:360,&quot;469769242&quot;:[65533,0],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;%1.&quot;,&quot;469777815&quot;:&quot;multilevel&quot;}" data-listid="7">
<p>上記で取得した署名済みの S3 URL を使用して、ファイルをS3にアップロードします。</p>
</li>
</ol>
<ol role="list" start="3">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="" data-leveltext="%1." data-list-defn-props="{&quot;335552541&quot;:0,&quot;335559685&quot;:360,&quot;335559991&quot;:360,&quot;469769242&quot;:[65533,0],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;%1.&quot;,&quot;469777815&quot;:&quot;multilevel&quot;}" data-listid="7">
<p><a  _msthash="130"  _msttexthash="3040674" href="https://aps.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-POST/" rel="noreferrer noopener" target="_blank"><strong>POST</strong>&#0160;oss/<strong>v2</strong>/buckets/:bucketKey/objects/:objectKey/<strong>signeds3upload</strong></a>&#0160;</p>
</li>
</ol>
<p>プロジェクトにファイルをアップロードするためのチュートリアルは<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/data/v2/tutorials/upload-file/#step-4-generate-a-signed-s3-url" rel="noreferrer noopener" target="_blank">こちら</a>で、管理対象アプリのチュートリアルは<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/data/v2/tutorials/app-managed-bucket/#step-2-initiate-a-direct-to-s3-multipart-upload" rel="noreferrer noopener" target="_blank">こちら</a>でご覧いただけます。</p>
</td>
</tr>
</tbody>
</table>
<p>ご質問をお持ちの場合には、<a  _istranslated="1" href="https://aps.autodesk.com/get-help" rel="noopener noreferrer" target="_blank">Get-Help</a> までお問い合わせください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/object-storage-service-oss-api-deprecating-v1-endpoints" rel="noopener" target="_blank">Object Storage Service (OSS) API: deprecating v1 endpoints | Autodesk Platform Services</a>&#0160;から転写・意訳、補足したものです。</p>
<p>By Toshiaki Isezaki</p>
