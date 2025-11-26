---
layout: "post"
title: "Design Automation API：Direct-to-S3 アプローチを簡素化する新機能"
date: "2022-11-16 00:03:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/11/design-automation-api-multipart-support-s3-upload.html "
typepad_basename: "design-automation-api-multipart-support-s3-upload"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c8f19d8200d-pi" style="display: inline;"><img alt="S3_da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c8f19d8200d image-full img-responsive" src="/assets/image_674906.jpg" title="S3_da" /></a></p>
<p>すでに何度かご案内していますが、Direct-to-S3アプローチの導入にともない、Data Management API でファイル アップロードとダウンロードをおこなうエンドポイントと、同じくアップロードとダウンロードに関係する他の API&#0160; のエンドポイントが廃止、または変更が加えられます。</p>
<p>既存のアプリは、使用する API によって移行作業が必要になります。対象の API やエンドポイント、実装については、<a href="https://adndevblog.typepad.com/technology_perspective/2022/03/data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach.html" rel="noopener" target="_blank"><strong>Data Management OSS (Object Storage Service) の Direct-to-S3 アプローチへの移行について</strong></a><strong><span style="background-color: #ffff00;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/03/data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach.html" rel="noopener" style="background-color: #ffff00;" target="_blank">（内容更新）</a></span></strong>の記事をご確認ください。</p>
<p>この変更は、Design Automation API で WorkItem が素材ファイルを OSS Bucket からダウンロードしたり、結果ファイルを OSS Bucket にアップロードしたりする場合にも適用されます。既存アプリが Design Automation API を利用していて、かつ、WorkItem 内の入出力ファイル指定に OSS Bucket（含む、Autodesk Docs/BIM 360&#0160; Docs）を指定している場合には、新しい Direct-to-S3 アプローチをお使いいただけるようになります。日本でも動作を確認することが出来ましたので、ご案内いたします。</p>
<p>具体的には、Design Automation API の WorkItem で、&quot;urn:adsk.objects:os.object:&lt;Bucket name&gt;/&lt;Object key&gt;&quot; 形式の入力/出力 URL をサポートしました。この方法は、Design Automation API の Step by step tutorials に記された方法を大幅に簡素化するものです。</p>
<ul>
<li>3ds Max：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/3dsmax/task4-manage-cloud-storage/" rel="noopener" target="_blank">Task 4 - Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></li>
<li>AutoCAD：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/autocad/task5-prepare_cloud_storage/" rel="noopener" target="_blank">Task 5 – Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></li>
<li>Inventor：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/inventor/task5-prepare_cloud_storage/" rel="noopener" target="_blank">Task 5 – Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></li>
<li>Revit：<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step6-prepare-cloud-storage/" rel="noopener" target="_blank">Task 6 – Prepare Cloud Storage | Design Automation API | Autodesk Forge</a></li>
</ul>
<p>新し方法を採用すると、Direct-to-S3 アプローチのエンドポイントセットで署名付き S3 URLを生成する必要がなくなります。必要となるのは <strong>objectId</strong> とアクセストークンのみです。</p>
<p><strong>objectId</strong>&#0160;とは、Model Derivative API や Viewer で使用される <a href="https://ja.wikipedia.org/wiki/Uniform_Resource_Name" rel="noopener noreferrer" target="_blank">URN</a>（ドキュメント ID）の&#0160;<a href="https://ja.wikipedia.org/wiki/Base64" rel="noopener noreferrer" target="_blank" title="https://ja.wikipedia.org/wiki/Base64">BASE64&#0160;エンコード</a>前の値（文字列）を指します。<strong>objectId</strong> を取得・準備するための詳細は次とおりです。</p>
<p><strong>OSS Bucket からの入力ファイルの場合：</strong></p>
<ul>
<li>Bucket から objectId を検索し、<u><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-GET/" rel="noopener" target="_blank">GET Objects</a></u> でオブジェクト情報を取得することができます。</li>
</ul>
<p><strong>OSS Bucket からの出力ファイルの場合：</strong></p>
<ul>
<li>ストレージを作成して objectId を取得することもできますが、ストレージの作成は必須ではありません。既存の Bucket を使用して &quot;urn:adsk.objects:os.object:&lt;url_encoded_BucketKey&gt;/&lt;url_encoded_BucketKey&gt;&quot; の形式で直接objectId を作成することが出来ます。例として、<u><a href="https://github.com/Autodesk-Forge/design.automation-nodejs-revit.parameters.excel/blob/master/routes/da4revit.js#L99" rel="noopener" target="_blank">design.automation-nodejs-revit.parameters.excel</a></u> をご参照いただくことが出来ます。</li>
</ul>
<p><strong>Autodesk Docs からの入力ファイルの場合：</strong></p>
<ul>
<li>ファイルのバージョンは <u><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-versions-version_id-GET/" rel="noopener" target="_blank">GET Version</a></u> で取得し、objectId は <strong>response.data.relationships.storage.data.id</strong> で確認することができます。例は、<a href="https://github.com/Autodesk-Forge/design.automation-nodejs-revit.parameters.excel/blob/master/routes/common/datamanagementImp.js#L182" rel="noopener" target="_blank">こちら</a>でご確認いただけます。</li>
</ul>
<p><strong>Autodesk Docs からの出力ファイルの場合：</strong></p>
<ul>
<li>新しいファイルをアップロードや新しいバージョンを追加する全体のプロセスは、<a href="https://forge.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="noopener" target="_blank">チュートリアル</a>をご参照ください。step 3 では、<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-storage-POST" rel="noopener" target="_blank">POST projects/:project_id/storage</a> エンドポイントを使用して、OSS にファイルをアップロードできるストレージの場所を作成します。この際、objectId は <strong>response.data.id</strong> で取得することが出来ます。例：<a href="https://github.com/Autodesk-Forge/design.automation-nodejs-revit.parameters.excel/blob/4502cd8d3ac376d2a091264b64c061bdf7311bc9/routes/common/da4revitImp.js#L352" rel="noopener" target="_blank">こちら</a> を参照</li>
</ul>
<p>なお、OSS Bucket 内の <strong>objectId</strong> は、VS Code 用の <a href="https://adndevblog.typepad.com/technology_perspective/2020/06/forge-development-using-vs-code.html" rel="noopener" target="_blank">Forge Extension</a> でも確認することが出来ます。</p>
<p><strong> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a56ba5200b-pi" style="display: inline;"><img alt="Vs_code" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a56ba5200b image-full img-responsive" src="/assets/image_671432.jpg" title="Vs_code" /></a><br /></strong></p>
<p><strong>objectId</strong> の用意が出来たら、それをアクセストークンとともに WorkItem の <strong>URL</strong> に渡します。アクセストークンはストレージに対する十分なアクセス権限（<a href="https://adndevblog.typepad.com/technology_perspective/2019/06/scopes-on-oauth.html" rel="noopener" target="_blank">Scope</a>）を持っている必要があります。例えば、入力ファイルの objectId には <strong>data:read</strong> 、出力ファイルには <strong>data:write </strong>が必要。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-0">{
  &quot;<span class="hljs-attribute">activityId</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;mynickname.myactivity+prod&quot;</span></span>,
  &quot;<span class="hljs-attribute">arguments</span>&quot;: <span class="hljs-value">{
    &quot;<span class="hljs-attribute">rvtFile</span>&quot;: {
      &quot;<span class="hljs-attribute">url</span>&quot;: <span class="hljs-string">&quot;urn:adsk.objects:os.object:revit_da_integration_tests/CountIt.rvt&quot;</span>,
      &quot;<span class="hljs-attribute">verb</span>&quot;: <span class="hljs-string">&quot;get&quot;</span>,
      &quot;<span class="hljs-attribute">headers</span>&quot;: {
        &quot;<span class="hljs-attribute">Authorization</span>&quot;: <span class="hljs-string">&quot;Bearer &lt;access_token&gt;&quot;</span>
       }
    },
    &quot;<span class="hljs-attribute">result</span>&quot;: {
      &quot;<span class="hljs-attribute">verb</span>&quot;: <span class="hljs-string">&quot;put&quot;</span>,
      &quot;<span class="hljs-attribute">url</span>&quot;: <span class="hljs-string">&quot;urn:adsk.objects:os.object:revit_da_integration_tests/result.txt&quot;</span>,
      &quot;<span class="hljs-attribute">headers</span>&quot;: {
        &quot;<span class="hljs-attribute">Authorization</span>&quot;: <span class="hljs-string">&quot;Bearer &lt;access_token&gt;&quot;</span>
       }
    }
  }
</span>}</code></pre>
<p>この新機能を使用すると、Design Automation API のタスクを自動的に処理するようになります。</p>
<ul>
<li>Design Automation API は、objectId とアクセストークンを使って Direct-to-S3 アプローチの署名付き URL を作成し、WorkItem 内部でダウンロード/アップロードを実行出来ます。</li>
<li>アクセストークンが期限切れの場合、Design Automation API は提供された有効なアクセストークンを延長します。1 時間後に署名付き URL が期限切れになる心配はもうありません。</li>
<li>OSS への大容量ファイルのアップロードは、マルチパートアップロードがデフォルトでサポートされます。</li>
<li>OSS Bucket と ACC（Autodesk Docs）/BIM 360（BIM 360 Docs） の Bucket の両方が動作しますが、後者が所有するBucket には 3-legged トークンを使用するなど、適宜正しいトークンを提供する必要があります。</li>
</ul>
<p>必要に応じて&#0160;<u><a data-pjax="#repo-content-pjax-container" data-turbo-frame="repo-content-turbo-frame" href="https://github.com/Autodesk-Forge/design.automation-nodejs-revit.parameters.excel" rel="noopener" target="_blank">design.automation-nodejs-revit.parameters.excel</a></u>&#0160;サンプルですべての実装をご確認ください。</p>
<p>なお、この機能では、アップロード用署名付き URL 取得（<a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fforge.autodesk.com%2Fen%2Fdocs%2Fdata%2Fv2%2Freference%2Fhttp%2Fbuckets-%3AbucketKey-objects-%3AobjectKey-signeds3upload-GET%2F&amp;data=05%7C01%7Ctoshiaki.isezaki%40autodesk.com%7C516eb8877980433ecf8a08da599bf09e%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C637920823562339064%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=UR06hW5dzFQtmvw7a4wsYbTypOOwmuJF%2BF0RwNqOXqc%3D&amp;reserved=0" rel="noopener" target="_blank">GET buckets/:bucketKey/objects/:objectKey/signeds3upload</a>）に <strong>“useAcceleration=true”</strong> パラメータ、ダウンロード用署名付き URL 取得（<a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fforge.autodesk.com%2Fen%2Fdocs%2Fdata%2Fv2%2Freference%2Fhttp%2Fbuckets-%3AbucketKey-objects-%3AobjectKey-signeds3download-GET%2F&amp;data=05%7C01%7Ctoshiaki.isezaki%40autodesk.com%7C516eb8877980433ecf8a08da599bf09e%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C637920823562339064%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=CSATIdj3hvnsuH6Bd9%2BTm3QUXtFc86DSsCYdJUIZKBg%3D&amp;reserved=0" rel="noopener" target="_blank">GET buckets/:bucketKey/objects/:objectKey/signeds3download</a>）に <strong>“useCdn=true”</strong> パラメータが、それぞれ内部的に指定されています。</p>
<p>ご質問は <a href="https://aps.autodesk.com/get-help" rel="noreferrer noopener" target="_blank">APS support</a> にコンタクトをお願いします。</p>
<p>By Toshiaki Isezaki</p>
