---
layout: "post"
title: "改めて Direct-S3 アプローチ移行について"
date: "2022-11-09 00:06:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/11/considering-direct-s3-approach.html "
typepad_basename: "considering-direct-s3-approach"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a52e7d200b-pi" style="display: inline;"><img alt="S3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a52e7d200b image-full img-responsive" src="/assets/image_176552.jpg" title="S3" /></a></p>
<p>オートデスクでは、Autodesk Platform Services（旧 Forge）で利用している AWS S3 Bucket（バケット）から、直接ファイルのアップロード/ダウンロードを可能にする Data Management API の新しいエンドポイント セット（Direct-to-S3アプローチ）をリリースしています。Direct-to-S3アプローチに移行することで、大きなサイズのバイナリ ファイルのアップロードやダウンロードのパフォーマンスを向上させることが出来るようになります。</p>
<p>Direct-to-S3アプローチ エンドポイント セットの導入を受けて、従来のバイナリ転送アプローチで利用する既存エンドポイントを、12 月 31 日に廃止する予定です。</p>
<p>廃止対象となるエンドポイントや変更点については、次のブログ記事でご案内していますので、もしご存じないようでしたら、まずはご確認をお願いいたします。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/03/data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach.html" rel="noopener" target="_blank">Data Management OSS (Object Storage Service) の Direct-to-S3 アプローチへの移行について <span style="background-color: #ffff00;">（内容更新）</span></a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/04/direct-to-s3-nodejs-samples.html" rel="noopener" target="_blank">Direct-to-S3 Node.js サンプル</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/04/direct-to-s3-net-samples.html" rel="noopener" target="_blank">Direct-to-S3 .NET サンプル</a></p>
<p>廃止予定のエンドポイントは、リファレンス ドキュメントで (Deprecated) 表記され、同時に、ページ上部に次のバナーが追加されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a54980200b-pi" style="display: inline;"><img alt="Deprecated" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a54980200b image-full img-responsive" src="/assets/image_479376.jpg" title="Deprecated" /></a></p>
<p>なお、現時点ではまだリリース出来ていませんが、Direct-to-S3 アプローチをサポートする Node.js&#0160; と&#0160; .NET&#0160; ベースの新しい SDK&#0160; をリリースする予定です（調整中）。新 SDK が利用可能になり次第、この日本語ブログ <a href="https://adndevblog.typepad.com/technology_perspective/" rel="noopener" target="_blank">Technology Perspective from Japan (typepad.com)</a> でご案内します。既存の SDK を拡張して Direct-to-S3 アプローチをサポートした SDK（Node.js、.NET のみ） を暫定リリースしていますので、次のブログ記事も併せてご確認ください。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/10/sirect-s3-upload-and-download-with-sdks.html" rel="noopener" target="_blank">Direct S3 アプローチ対応（暫定）SDK</a></p>
<p>Direct-S3 アプローチの新しいエンドポイントを直接利用する場合には、日本からのS3アクセス用にSigned URL 取得いただく際には、Upload 用、Download 用ともに、次のパラメータを指定することを強くお薦めしております。</p>
<p style="padding-left: 40px;"><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fforge.autodesk.com%2Fen%2Fdocs%2Fdata%2Fv2%2Freference%2Fhttp%2Fbuckets-%3AbucketKey-objects-%3AobjectKey-signeds3upload-GET%2F&amp;data=05%7C01%7Ctoshiaki.isezaki%40autodesk.com%7C516eb8877980433ecf8a08da599bf09e%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C637920823562339064%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=UR06hW5dzFQtmvw7a4wsYbTypOOwmuJF%2BF0RwNqOXqc%3D&amp;reserved=0" rel="noopener" target="_blank">GET buckets/:bucketKey/objects/:objectKey/signeds3upload</a>：<strong>“useAcceleration=true” </strong></p>
<p style="padding-left: 40px;"><a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fforge.autodesk.com%2Fen%2Fdocs%2Fdata%2Fv2%2Freference%2Fhttp%2Fbuckets-%3AbucketKey-objects-%3AobjectKey-signeds3download-GET%2F&amp;data=05%7C01%7Ctoshiaki.isezaki%40autodesk.com%7C516eb8877980433ecf8a08da599bf09e%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C637920823562339064%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=CSATIdj3hvnsuH6Bd9%2BTm3QUXtFc86DSsCYdJUIZKBg%3D&amp;reserved=0" rel="noopener" target="_blank">GET buckets/:bucketKey/objects/:objectKey/signeds3download</a>：<strong>“useCdn=true”</strong></p>
<p>これらパラメータ指定がない場合、S3 Transfer Acceleration と CloudFront CDN を使った署名付き URL がリクエストされないため、日本からのパフォーマンスが低下してしまいます。この箇所につきましては、次のブログ記事でご案内しております。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/07/request-signed-url-on-direct-to-s3-approach.html" rel="noopener" target="_blank">Direct-to-S3 アプローチの署名付き URL 要求について</a></p>
<p>Design Automation API をお使いの場合には、APS（旧 Forge）ポータル内のドキュメントにて、当該箇所の Step by step tutorials が更新されていますので、必要に応じてご確認をお願いいたします。</p>
<p style="padding-left: 40px;">3ds Max：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/3dsmax/task4-manage-cloud-storage/" rel="noopener" target="_blank">Task 4 - Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></p>
<p style="padding-left: 40px;">AutoCAD：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/autocad/task5-prepare_cloud_storage/" rel="noopener" target="_blank">Task 5 – Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></p>
<p style="padding-left: 40px;">Inventor：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/inventor/task5-prepare_cloud_storage/" rel="noopener" target="_blank">Task 5 – Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></p>
<p style="padding-left: 40px;">Revit：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step6-prepare-cloud-storage/" rel="noopener" target="_blank">Task 6 – Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></p>
<p><strong>暫定的に対応可能な既存エンドポイント</strong>（Direct-to-S3 アプローチのエンドポイントではない）<strong>：</strong></p>
<p style="padding-left: 40px;">Design Automation API 等でファイルのアップロードとダウンロードを署名付き URL を取得する<span style="text-decoration: underline;">既存の</span> <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signed-POST/"><strong>POST buckets/:bucketKey/objects/:objectKey/signed</strong></a><strong> エンドポイントをお使いの場合、</strong>このエンドポイント自体は 12 月 31 日の廃止対象外となりますが、S3アクセスをサポートするよう、<strong>useCdn パラメータを追加して S3 にアクセスする方法もあります。ただし、パフォーマンス向上は期待出来ません。</strong></p>
<p style="padding-left: 40px;">同エンドポイントをお使いで、12 月 31 日前に Direct-to-S3 アプローチへの置き換えが難しい場合には、<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signed-POST/" rel="noopener" target="_blank">POST buckets/:bucketKey/objects/:objectKey/signed </a>エンドポイント呼び出し時に useCdn=true を指定するますよう、お願いします。useCdn パラメータについては、次の URL からご確認いただけます。</p>
<p style="padding-left: 40px;"><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signed-POST/#query-string-parameters" rel="noopener" target="_blank">POST buckets/:bucketKey/objects/:objectKey/signed</a></p>
<p>By Toshiaki Isezaki</p>
