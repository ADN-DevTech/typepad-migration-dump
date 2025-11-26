---
layout: "post"
title: "BIM 360 Docs でのファイルアップロードとダウンロードについてのご注意"
date: "2020-08-28 02:06:17"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/08/uploading-downloading-files-from-bim360docs-best-practice.html "
typepad_basename: "uploading-downloading-files-from-bim360docs-best-practice"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e95ecaf3200b-pi" style="display: inline;"></a>BIM 360 Docs チュートリアルでは、ファイルを<strong><a href="https://forge.autodesk.com/en/docs/bim360/v1/tutorials/document-management/upload-document/" rel="noopener" target="_blank">アップロード</a></strong>、または、<strong><a href="https://forge.autodesk.com/en/docs/bim360/v1/tutorials/document-management/download-document/" rel="noopener" target="_blank">ダウンロード</a></strong>する際の bucketKey（Buccket 名）が常に <strong>wip.dm.prod </strong>とされています。実際には、常に <strong>wip.dm.prod </strong>であるとは限りません。 したがって、アプリケーション実装時には、この名前が決して変化しないとは想定してハードコードすることは避けてください。 それでは、ファイル ダウンロード チュートリアルの <strong><a href="https://forge.autodesk.com/en/docs/bim360/v1/tutorials/document-management/download-document/#step-4-find-the-storage-object-id-for-the-file" rel="noopener" target="_blank">Step 4: Find the Storage Object ID for the file</a></strong> を再度確認してみると、返されたペイロードで、次の内容の JSON レスポンスが返されていることがわかります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-0">...
<span class="hljs-string">&quot;storage&quot;</span>: {
   &quot;<span class="hljs-attribute">data</span>&quot;: <span class="hljs-value">{
       &quot;<span class="hljs-attribute">type</span>&quot;: <span class="hljs-string">&quot;objects&quot;</span>,
       &quot;<span class="hljs-attribute">id</span>&quot;: <span class="hljs-string">&quot;urn:adsk.objects:os.object:wip.dm.prod/72d5e7e4-89a7-4cb9-9da0-2e2bbc61ca8e.dwg&quot;</span>
   }</span>,
   ...
}
...</code></pre>
<p><strong>store.data.id</strong> から bucketKey と objectKey を抽出して、<a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-GET/" rel="noopener" target="_blank"><strong>GET buckets/:bucketKey/objects/:objectName</strong> </a>endpoint を継続処理が出来るようにしている点にご着目ください。 両キーの抽出には様々な手法を使用できますが、正規表現でこれを簡単に行うことができます。 ^urn:adsk\.objects:os\.object:([-_.a-z0-9]{3,128})\/(.+)$ を使用して、両方のキーを抽出し、それらの値を検証することが出来ます。</p>
<p>ファイルのアップロード時（ファイル アップロード チュートリアルの <strong><a href="https://forge.autodesk.com/en/docs/bim360/v1/tutorials/document-management/upload-document/#step-5-create-a-storage-object" rel="noopener" target="_blank">Step 5: Create a Storage Object</a></strong> を参照）も同様です。<strong><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-PUT/" rel="noopener" target="_blank">PUT buckets/:bucketKey/objects/:objectName</a></strong>、または、<strong><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-resumable-PUT/" rel="noopener" target="_blank">PUT buckets/:bucketKey/objects/:objectName/resumable</a></strong> endpoint で同じ手法を使用する必要があります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-1">...
<span class="hljs-string">&quot;data&quot;</span>: {
  &quot;<span class="hljs-attribute">type</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;objects&quot;</span></span>,
  &quot;<span class="hljs-attribute">id</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;urn:adsk.objects:os.object:wip.dm.prod/2a6d61f2-49df-4d7b.jpg&quot;</span></span>,
  ...
</code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bde8c1ad2200c-pi" style="float: right;"><img alt="Upload_download" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bde8c1ad2200c img-responsive" src="/assets/image_416554.jpg" style="margin: 0px 0px 5px 5px;" title="Upload_download" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>By Toshiaki Isezaki</p>
