---
layout: "post"
title: "Direct-to-S3 - 旧エンドポイント廃止期日延長"
date: "2022-09-23 20:46:52"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/09/direct-to-s3-new-deprecation-date.html "
typepad_basename: "direct-to-s3-new-deprecation-date"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308e12dbf200c-pi" style="display: inline;"><img alt="Screen Shot 2022-03-16 at 6.14.40 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308e12dbf200c image-full img-responsive" src="/assets/image_734393.jpg" title="Screen Shot 2022-03-16 at 6.14.40 PM" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/03/data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach.html" rel="noopener" target="_blank">Data Management OSS (Object Storage Service) の Direct-to-S3 アプローチへの移行について</a> でご案内した次のエンドポイントの廃止期日について、諸般の事情に鑑みて、当初予定していた 2022年9月30日 から <span style="background-color: #ffff00;"><strong>2022年12月31日 に延長</strong></span>することが決定されました。</p>
<ul>
<li><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-PUT" rel="noopener" target="_blank">PUT buckets/:bucketKey/objects/:objectKey</a>（非推奨）</li>
<li><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-resumable-PUT" rel="noopener" target="_blank">PUT buckets/:bucketKey/objects/:objectKey/resumable</a>（非推奨）</li>
<li><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-GET" rel="noopener" target="_blank">GET buckets/:bucketKey/objects/:objectKey</a>（非推奨）</li>
</ul>
<p>お早目に Direct-to-S3 アプローチへの移行をお願いするとともに、上記エンドポイントの新規使用は避けていただくようにお願いします。</p>
<p>なお、新しく Direct-S3 アプローチで利用する Signed URL 取得のエンドポイントを日本からお使いいただく場合には、<a href="https://adndevblog.typepad.com/technology_perspective/2022/07/request-signed-url-on-direct-to-s3-approach.html" rel="noopener" target="_blank">Direct-to-S3 アプローチの署名付き URL 要求について</a> でご紹介しているオプション パラメータの使用をご検討ください。</p>
<p>また、Design Automation API をお使いの場合には、Forge ポータル内のドキュメントにて、当該箇所の Step by step tutorials が更新されていますので、必要に応じてご確認をお願いいたします。</p>
<ul>
<li>3ds Max：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/3dsmax/task4-manage-cloud-storage/" rel="noopener" target="_blank">Task 4 - Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></li>
<li>AutoCAD：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/autocad/task5-prepare_cloud_storage/" rel="noopener" target="_blank">Task 5 – Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></li>
<li>Inventor：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/inventor/task5-prepare_cloud_storage/" rel="noopener" target="_blank">Task 5 – Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></li>
<li>Revit：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step6-prepare-cloud-storage/" rel="noopener" target="_blank">Task 6 – Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></li>
</ul>
<p>ご不明な点やご質問がありましたら、<a href="mailto:forge.help@autodesk.com">forge.help@autodesk.com</a>&#0160;までお問合せください。&#0160;</p>
<p><em>※ 本記事は <a href="https://forge.autodesk.com/blog/direct-s3-new-deprecation-date" rel="noopener" target="_blank">Direct-to-S3 - New deprecation Date! | Autodesk Forge</a>&#0160;</em><em>の内容をもとに翻訳・加筆修正したものです</em></p>
<p>By Toshiaki Isezaki</p>
