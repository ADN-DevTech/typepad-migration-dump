---
layout: "post"
title: "Direct-to-S3 .NET サンプル"
date: "2022-04-14 19:10:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/04/direct-to-s3-net-samples.html "
typepad_basename: "direct-to-s3-net-samples"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942fa63ec0200c-pi" style="display: inline;"><img alt="Dotnet" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942fa63ec0200c image-full img-responsive" src="/assets/image_915199.jpg" title="Dotnet" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/03/data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach.html" rel="noopener" target="_blank"><strong>Data Management OSS (Object Storage Service) の Direct-to-S3 アプローチへの移行について</strong></a>のアナウンスがありましたので、この移行をよりスムーズにおこなっていただくための情報をご提供したいと思います。今回は、Autodesk Forge サービスにおける新しいバイナリ転送のための Node.js ユーティリティについてです。 これらのサンプルは、<a href="https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core#cadence" rel="noopener" target="_blank"><strong>LTS バージョンの .NET</strong></a>&#0160;を使用してビルドされています。</p>
<p>チームはまた、Direct-to-S3 アプローチを使用する新しいSDKの開発にも取り組んでいます。&#0160;</p>
<p>チームの <a href="https://twitter.com/JooPaulodeOrne2">Joao Martins</a> は、OSS Direct-to-S3 アプローチのために新しくリリースされたすべてのエンドポイントを含む、キュレーションされたユーティリティファイルに取り組みました。</p>
<p>Githubのリポジトリは<a href="https://github.com/orgs/Autodesk-Forge/repositories?type=all" rel="noopener" target="_blank"><strong>こちら</strong></a>で確認できます。その中で、.NET 6ブランチは<a href="https://github.com/Autodesk-Forge/forge-directToS3/tree/net6.0" rel="noopener" target="_blank"><strong>こちら</strong></a>、.NET Core 3.1で作業する場合は、<a href="https://github.com/Autodesk-Forge/forge-directToS3/tree/netcoreapp3.1" rel="noopener" target="_blank"><strong>こちら</strong></a>で確認することができます。</p>
<h2 id="blob-path"><strong>BinarytransferClient.cs</strong></h2>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="code-overflow-x hljs cs" id="snippet-0"><span class="hljs-keyword">using</span> Autodesk.Forge;
<span class="hljs-keyword">using</span> Newtonsoft.Json;
<span class="hljs-keyword">using</span> RestSharp;
<span class="hljs-keyword">using</span> System;
<span class="hljs-keyword">using</span> System.Collections.Generic;
<span class="hljs-keyword">using</span> System.IO;
<span class="hljs-keyword">using</span> System.Linq;
<span class="hljs-keyword">using</span> System.Net;
<span class="hljs-keyword">using</span> System.Threading.Tasks;
<span class="hljs-keyword">using</span> System.Web;

namespace Forge_Upload_DirectToS3
{
  <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">class</span> BinarytransferClient
  {
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">string</span> BASE_URL { <span class="hljs-keyword">get</span>; <span class="hljs-keyword">set</span>; }

    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> dynamic CREDENTIAL { <span class="hljs-keyword">get</span>; <span class="hljs-keyword">set</span>; }

    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">int</span> UPLOAD_CHUNK_SIZE { <span class="hljs-keyword">get</span>; <span class="hljs-keyword">set</span>; }

    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">string</span> CLIENT_ID { <span class="hljs-keyword">get</span>; <span class="hljs-keyword">set</span>; }
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">string</span> CLIENT_SECRET { <span class="hljs-keyword">get</span>; <span class="hljs-keyword">set</span>; }

    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">int</span> MAX_RETRY { <span class="hljs-keyword">get</span>; <span class="hljs-keyword">set</span>; }


    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> Return the URLs to upload the file</span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;/summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;bucketKey&quot;&gt;</span>Bucket key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;objectKey&quot;&gt;</span>Object key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;parts&quot;&gt;</span>[parts=1] How many URLs to generate in case of multi-part upload<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;firstPart&quot;&gt;</span>B[firstPart=1] Index of the part the first returned URL should point to<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;uploadKey&quot;&gt;</span>[uploadKey] Optional upload key if this is a continuation of a previously initiated upload<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;minutesExpiration&quot;&gt;</span>[minutesExpiration] Custom expiration for the upload URLs (within the 1 to 60 minutes range). If not specified, default is 2 minutes.</span>
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;dynamic&gt; <span class="hljs-title">getUploadUrls</span>(<span class="hljs-keyword">string</span> bucketKey, <span class="hljs-keyword">string</span> objectKey, <span class="hljs-keyword">int</span>? minutesExpiration, <span class="hljs-keyword">int</span> parts = <span class="hljs-number">1</span>, <span class="hljs-keyword">int</span> firstPart = <span class="hljs-number">1</span>, <span class="hljs-keyword">string</span> uploadKey = <span class="hljs-keyword">null</span>)
    {
      <span class="hljs-keyword">string</span> endpoint = $<span class="hljs-string">&quot;/buckets/{bucketKey}/objects/{HttpUtility.UrlEncode(objectKey)}/signeds3upload&quot;</span>;

      RestClient client = <span class="hljs-keyword">new</span> RestClient(BASE_URL);
      RestRequest request = <span class="hljs-keyword">new</span> RestRequest(endpoint, RestSharp.Method.GET);
      request.AddHeader(<span class="hljs-string">&quot;Authorization&quot;</span>, <span class="hljs-string">&quot;Bearer &quot;</span> + CREDENTIAL.access_token);
      request.AddHeader(<span class="hljs-string">&quot;Content-Type&quot;</span>, <span class="hljs-string">&quot;application/json&quot;</span>);
      request.AddParameter(<span class="hljs-string">&quot;parts&quot;</span>, parts, ParameterType.QueryString);
      request.AddParameter(<span class="hljs-string">&quot;firstPart&quot;</span>, firstPart, ParameterType.QueryString);

      <span class="hljs-keyword">if</span> (!<span class="hljs-keyword">string</span>.IsNullOrEmpty(uploadKey))
      {
        request.AddParameter(<span class="hljs-string">&quot;uploadKey&quot;</span>, uploadKey, ParameterType.QueryString);
      }

      <span class="hljs-keyword">if</span> (minutesExpiration != <span class="hljs-keyword">null</span>)
      {
        request.AddParameter(<span class="hljs-string">&quot;minutesExpiration&quot;</span>, minutesExpiration, ParameterType.QueryString);
      }

      <span class="hljs-keyword">var</span> response = <span class="hljs-keyword">await</span> client.ExecuteAsync(request);

      <span class="hljs-comment">//Here we handle 429 for Get Upload URLs</span>
      <span class="hljs-keyword">if</span> (response.StatusCode == HttpStatusCode.TooManyRequests)
      {
        <span class="hljs-keyword">int</span> retryAfter = <span class="hljs-number">0</span>;
        <span class="hljs-keyword">int</span>.TryParse(response.Headers.ToList()
            .Find(x =&gt; x.Name == <span class="hljs-string">&quot;Retry-After&quot;</span>)
            .Value.ToString(), <span class="hljs-keyword">out</span> retryAfter);
        Task.WaitAll(Task.Delay(retryAfter));
        <span class="hljs-keyword">return</span> <span class="hljs-keyword">await</span> getUploadUrls(bucketKey, objectKey, minutesExpiration, parts, firstPart, uploadKey);
      }

      <span class="hljs-keyword">return</span> JsonConvert.DeserializeObject(response.Content);
    }

    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> Upload the FileStream to specified bucket</span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;/summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;bucketKey&quot;&gt;</span>Bucket key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;objectKey&quot;&gt;</span>Object key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;fileStream&quot;&gt;</span>FileStream from input file<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;dynamic&gt; <span class="hljs-title">UploadToBucket</span>(<span class="hljs-keyword">string</span> bucketKey, <span class="hljs-keyword">string</span> objectKey, FileStream fileStream)
    {
      <span class="hljs-keyword">long</span> fileSize = fileStream.Length;
      <span class="hljs-keyword">int</span> maxBatches = <span class="hljs-number">25</span>;
      <span class="hljs-keyword">int</span> numberOfChunks = (<span class="hljs-keyword">int</span>)Math.Round((<span class="hljs-keyword">double</span>)(fileSize / UPLOAD_CHUNK_SIZE)) + <span class="hljs-number">1</span>;
      <span class="hljs-keyword">int</span> partsUploaded = <span class="hljs-number">0</span>;
      <span class="hljs-keyword">long</span> start = <span class="hljs-number">0</span>;
      List&lt;<span class="hljs-keyword">string</span>&gt; uploadUrls = <span class="hljs-keyword">new</span> List&lt;<span class="hljs-keyword">string</span>&gt;();
      <span class="hljs-keyword">string</span> uploadKey = <span class="hljs-keyword">null</span>;

      <span class="hljs-keyword">using</span> (BinaryReader reader = <span class="hljs-keyword">new</span> BinaryReader(fileStream))
      {
        <span class="hljs-keyword">while</span> (partsUploaded &lt; numberOfChunks)
        {
          <span class="hljs-keyword">int</span> attempts = <span class="hljs-number">0</span>;

          <span class="hljs-keyword">long</span> end = Math.Min((partsUploaded + <span class="hljs-number">1</span>) * UPLOAD_CHUNK_SIZE, fileSize);

          <span class="hljs-keyword">long</span> numberOfBytes = end - start;
          <span class="hljs-keyword">byte</span>[] fileBytes = <span class="hljs-keyword">new</span> <span class="hljs-keyword">byte</span>[numberOfBytes];
          reader.BaseStream.Seek((<span class="hljs-keyword">int</span>)start, SeekOrigin.Begin);
          <span class="hljs-keyword">int</span> count = reader.Read(fileBytes, <span class="hljs-number">0</span>, (<span class="hljs-keyword">int</span>)numberOfBytes);

          <span class="hljs-keyword">while</span> (<span class="hljs-keyword">true</span>)
          {
            attempts++;
            Console.WriteLine($<span class="hljs-string">&quot;Uploading part {partsUploaded + 1}, attempt {attempts}&quot;</span>);
            <span class="hljs-keyword">if</span> (uploadUrls.Count == <span class="hljs-number">0</span>)
            {
              CREDENTIAL = <span class="hljs-keyword">await</span> Get2LeggedTokenAsync(<span class="hljs-keyword">new</span> Scope[] { Scope.DataRead, Scope.DataWrite, Scope.DataCreate });
              dynamic uploadParams = <span class="hljs-keyword">await</span> getUploadUrls(bucketKey, objectKey, <span class="hljs-keyword">null</span>, Math.Min(numberOfChunks - partsUploaded, maxBatches), partsUploaded + <span class="hljs-number">1</span>, uploadKey);
              uploadKey = uploadParams.uploadKey;
              uploadUrls = uploadParams.urls.ToObject&lt;List&lt;<span class="hljs-keyword">string</span>&gt;&gt;();
            }

            <span class="hljs-keyword">string</span> currentUrl = uploadUrls[<span class="hljs-number">0</span>];
            uploadUrls.RemoveAt(<span class="hljs-number">0</span>);

            <span class="hljs-keyword">try</span>
            {
              <span class="hljs-keyword">var</span> responseBuffer = <span class="hljs-keyword">await</span> UploadBufferRestSharp(currentUrl, fileBytes);

              <span class="hljs-keyword">int</span> statusCode = (<span class="hljs-keyword">int</span>)responseBuffer.StatusCode;

              <span class="hljs-keyword">switch</span> (statusCode)
              {
                <span class="hljs-keyword">case</span> <span class="hljs-number">403</span>:
                  Console.WriteLine(<span class="hljs-string">&quot;403, refreshing urls&quot;</span>);
                  uploadUrls = <span class="hljs-keyword">new</span> List&lt;<span class="hljs-keyword">string</span>&gt;();
                  <span class="hljs-keyword">break</span>;
                <span class="hljs-keyword">case</span> <span class="hljs-keyword">int</span> n when (n &gt;= <span class="hljs-number">400</span>):
                  <span class="hljs-keyword">throw</span> <span class="hljs-keyword">new</span> Exception(responseBuffer.Content);
                <span class="hljs-keyword">default</span>:
                  <span class="hljs-keyword">goto</span> NextChunk;
              }

            }
            <span class="hljs-keyword">catch</span> (Exception ex)
            {
              Console.WriteLine(ex.Message);
              <span class="hljs-keyword">if</span> (attempts == MAX_RETRY)
                <span class="hljs-keyword">throw</span>;
            }
          }
        NextChunk:
          partsUploaded++;
          start = end;
          System.Console.WriteLine($<span class="hljs-string">&quot;{partsUploaded.ToString()} parts uploaded!&quot;</span>);

        }
      }

      <span class="hljs-keyword">var</span> responseUpload = <span class="hljs-keyword">await</span> CompleteUpload(bucketKey, objectKey, uploadKey);

      <span class="hljs-keyword">return</span> responseUpload;
    }

    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> Upload the specific part through url</span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;/summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;url&quot;&gt;</span>URL to upload the specified part<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;buffer&quot;&gt;</span>Buffer array to upload<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;dynamic&gt; <span class="hljs-title">UploadBufferRestSharp</span>(<span class="hljs-keyword">string</span> url, <span class="hljs-keyword">byte</span>[] buffer)
    {
      RestClient client = <span class="hljs-keyword">new</span> RestClient();
      RestRequest request = <span class="hljs-keyword">new</span> RestRequest(url, RestSharp.Method.PUT);
      request.AddParameter(<span class="hljs-string">&quot;&quot;</span>, buffer, ParameterType.RequestBody);

      <span class="hljs-keyword">var</span> response = <span class="hljs-keyword">await</span> client.ExecuteAsync(request);

      <span class="hljs-keyword">return</span> response;
    }

    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> Finalizes the upload of a file to OSS.</span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;/summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;bucketKey&quot;&gt;</span>Bucket key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;objectKey&quot;&gt;</span>Object key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;uploadKey&quot;&gt;</span>[uploadKey] Optional upload key if this is a continuation of a previously initiated upload<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;dynamic&gt; <span class="hljs-title">CompleteUpload</span>(<span class="hljs-keyword">string</span> bucketKey, <span class="hljs-keyword">string</span> objectKey, <span class="hljs-keyword">string</span> uploadKey)
    {
      <span class="hljs-keyword">string</span> endpoint = $<span class="hljs-string">&quot;/buckets/{bucketKey}/objects/{HttpUtility.UrlEncode(objectKey)}/signeds3upload&quot;</span>;
      RestClient client = <span class="hljs-keyword">new</span> RestClient(BASE_URL);
      RestRequest request = <span class="hljs-keyword">new</span> RestRequest(endpoint, Method.POST);

      request.AddHeader(<span class="hljs-string">&quot;Authorization&quot;</span>, <span class="hljs-string">&quot;Bearer &quot;</span> + CREDENTIAL.access_token);
      request.AddHeader(<span class="hljs-string">&quot;Content-Type&quot;</span>, <span class="hljs-string">&quot;application/json&quot;</span>);

      request.AddJsonBody(<span class="hljs-keyword">new</span> { uploadKey = $<span class="hljs-string">&quot;{uploadKey}&quot;</span> });

      <span class="hljs-keyword">var</span> response = <span class="hljs-keyword">await</span> client.ExecuteAsync(request);

      <span class="hljs-keyword">return</span> response;
    }

    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> Return the URLs to upload the file</span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;/summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;bucketKey&quot;&gt;</span>Bucket key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;objectKey&quot;&gt;</span>Object key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;minutesExpiration&quot;&gt;</span>[minutesExpiration] Custom expiration for the upload URLs (within the 1 to 60 minutes range). If not specified, default is 2 minutes.</span>
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;dynamic&gt; <span class="hljs-title">getDownloadUrl</span>(<span class="hljs-keyword">string</span> bucketKey, <span class="hljs-keyword">string</span> objectKey, <span class="hljs-keyword">int</span>? minutesExpiration)
    {
      <span class="hljs-keyword">string</span> endpoint = $<span class="hljs-string">&quot;/buckets/{bucketKey}/objects/{HttpUtility.UrlEncode(objectKey)}/signeds3download&quot;</span>;
      RestClient client = <span class="hljs-keyword">new</span> RestClient(BASE_URL);
      RestRequest request = <span class="hljs-keyword">new</span> RestRequest(endpoint, RestSharp.Method.GET);
      request.AddHeader(<span class="hljs-string">&quot;Authorization&quot;</span>, <span class="hljs-string">&quot;Bearer &quot;</span> + CREDENTIAL.access_token);
      request.AddHeader(<span class="hljs-string">&quot;Content-Type&quot;</span>, <span class="hljs-string">&quot;application/json&quot;</span>);

      <span class="hljs-keyword">if</span> (minutesExpiration != <span class="hljs-keyword">null</span>)
      {
        request.AddParameter(<span class="hljs-string">&quot;minutesExpiration&quot;</span>, minutesExpiration, ParameterType.QueryString);
      }

      <span class="hljs-keyword">var</span> response = <span class="hljs-keyword">await</span> client.ExecuteAsync(request);

      <span class="hljs-comment">//Here we handle 429 for Get Download URLs</span>
      <span class="hljs-keyword">if</span> (response.StatusCode == HttpStatusCode.TooManyRequests)
      {
        <span class="hljs-keyword">int</span> retryAfter = <span class="hljs-number">0</span>;
        <span class="hljs-keyword">int</span>.TryParse(response.Headers.ToList()
            .Find(x =&gt; x.Name == <span class="hljs-string">&quot;Retry-After&quot;</span>)
            .Value.ToString(), <span class="hljs-keyword">out</span> retryAfter);
        Task.WaitAll(Task.Delay(retryAfter));
        <span class="hljs-keyword">return</span> <span class="hljs-keyword">await</span> getDownloadUrl(bucketKey, objectKey, minutesExpiration);
      }

      <span class="hljs-keyword">return</span> JsonConvert.DeserializeObject(response.Content);
    }

    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> Download the specific part through url</span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;/summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;url&quot;&gt;</span>URL to download the file<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">byte</span>[] <span class="hljs-title">DownloadBufferRestSharp</span>(<span class="hljs-keyword">string</span> url)
    {
      RestClient client = <span class="hljs-keyword">new</span> RestClient();
      RestRequest request = <span class="hljs-keyword">new</span> RestRequest(url, RestSharp.Method.GET);

      <span class="hljs-keyword">byte</span>[] data = client.DownloadData(request);

      <span class="hljs-keyword">return</span> data;
    }

    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> Return the byte array of the downloaded content</span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;/summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;bucketKey&quot;&gt;</span>Bucket key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;objectKey&quot;&gt;</span>Object key<span class="hljs-xmlDocTag">&lt;/param&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;param name=&quot;minutesExpiration&quot;&gt;</span>[minutesExpiration] Custom expiration for the upload URLs (within the 1 to 60 minutes range). If not specified, default is 2 minutes.</span>
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;<span class="hljs-keyword">byte</span>[]&gt; <span class="hljs-title">DownloadFromBucket</span>(<span class="hljs-keyword">string</span> bucketKey, <span class="hljs-keyword">string</span> objectKey, <span class="hljs-keyword">int</span>? minutesExpiration)
    {
      dynamic downloadParams = <span class="hljs-keyword">await</span> getDownloadUrl(bucketKey, objectKey, minutesExpiration);

      <span class="hljs-keyword">if</span> (downloadParams.status != <span class="hljs-string">&quot;complete&quot;</span>)
      {
        <span class="hljs-keyword">throw</span> <span class="hljs-keyword">new</span> Exception(<span class="hljs-string">&quot;File not available for download yet.&quot;</span>);
      }

      <span class="hljs-keyword">byte</span>[] downloadedBuffer = DownloadBufferRestSharp(downloadParams.url.ToString());

      <span class="hljs-keyword">return</span> downloadedBuffer;

    }

    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;summary&gt;</span></span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> Get the access token from Autodesk</span>
    <span class="hljs-comment"><span class="hljs-xmlDocTag">///</span> <span class="hljs-xmlDocTag">&lt;/summary&gt;</span></span>
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;dynamic&gt; <span class="hljs-title">Get2LeggedTokenAsync</span>(Scope[] scopes)
    {
      TwoLeggedApi oauth = <span class="hljs-keyword">new</span> TwoLeggedApi();
      <span class="hljs-keyword">string</span> grantType = <span class="hljs-string">&quot;client_credentials&quot;</span>;
      dynamic bearer = <span class="hljs-keyword">await</span> oauth.AuthenticateAsync(
        CLIENT_ID,
        CLIENT_SECRET,
        grantType,
        scopes);
      <span class="hljs-keyword">return</span> bearer;
    }
  }
}</code></pre>
<p>署名済み URL（Signed URL）のデフォルトの有効期限は2分です（<em>minutesExpiration</em>&#0160;パラメータで最大60分まで有効期限を延長することができます）。</p>
<h2>ダウンロード</h2>
<p>まず、ダウンロードの手順からご紹介します。AWS S3 から署名済み URL（Signed URL）を使ってファイルを直接ダウンロードするために、2つのステップを踏む必要があります。以下は、その仕組みを説明する擬似コードです。</p>
<ol dir="auto">
<li><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-signeds3download-GET" rel="nofollow noopener" target="_blank">GET buckets/:bucketKey/objects/:objectName/signeds3download</a>&#0160;エンドポイントを使ってダウンロード用の URL を生成します。</li>
<li>新しいURLを使用して、AWS S3 から直接 OSS オブジェクトをダウンロードします。<br />
<ul dir="auto">
<li>レスポンス コードが 100～199、429、500～599 の場合、ダウンロードの再試行（例えば指数関数的バックオフ）を検討する。</li>
</ul>
</li>
</ol>
<p>OSS Bucket からファイルをダウンロードする場合のコードは次のようになります（最初にファイル全体をメモリに受信します）。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="code-overflow-x hljs cs" id="snippet-1"><span class="hljs-keyword">using</span> System.IO;
<span class="hljs-keyword">using</span> System.Threading.Tasks;
<span class="hljs-keyword">using</span> Autodesk.Forge;

namespace Forge_Upload_DirectToS3.test
{
  <span class="hljs-keyword">public</span> <span class="hljs-keyword">class</span> download_from_bucket
  {
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;dynamic&gt; <span class="hljs-title">DownloadFile</span>(<span class="hljs-keyword">string</span> filePath, <span class="hljs-keyword">string</span> bucketKey, <span class="hljs-keyword">string</span> objectKey)
    {
      BinarytransferClient.CREDENTIAL = <span class="hljs-keyword">await</span> BinarytransferClient.Get2LeggedTokenAsync(<span class="hljs-keyword">new</span> Scope[] { Scope.DataRead, Scope.DataWrite, Scope.DataCreate });

      dynamic response = <span class="hljs-keyword">new</span> System.Dynamic.ExpandoObject();
      response.Status = <span class="hljs-string">&quot;Download started!&quot;</span>;

      System.Console.WriteLine(response.Status);

      <span class="hljs-keyword">byte</span>[] downloadedBuffer = <span class="hljs-keyword">await</span> BinarytransferClient.DownloadFromBucket(bucketKey, objectKey, <span class="hljs-keyword">null</span>);

      <span class="hljs-keyword">await</span> File.WriteAllBytesAsync(filePath, downloadedBuffer);

      response.Status = <span class="hljs-string">&quot;Download Complete!&quot;</span>;

      <span class="hljs-keyword">return</span> response;
    }
  }
}</code></pre>
<h2>アップロード</h2>
<p>次にアップロードの手順をご紹介します。AWS S3 から署名付き URL（Signed URL）を使って直接ファイルをアップロードするには、3 つのステップを踏む必要があります。以下は、その仕組みを説明する擬似コードです。&#0160;</p>
<ol dir="auto">
<li>アップロードするファイルのパーツ数を算出<br />
<ul dir="auto">
<li>&#0160;注意：最後の 1 つを除き、アップロードする各パーツは 5 MB 以上であること</li>
</ul>
</li>
<li><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-GET/">GET buckets/:bucketKey/objects/:objectKey/signeds3upload?firstPart=&lt;index of first part&gt;&amp;parts=&lt;number of parts&gt;</a>&#0160;エンドポイントを使用して特定のパーツのファイルをアップロードするための、最大 25 の URL を生成<br />
<ul dir="auto">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p>パーツ番号は 1 から始まるものと仮定  &#0160;</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p>例えば、10 番パーツから 15 番パーツまでのアップロード用 URL を生成するには、&lt;index of first part&gt; を 10 に、&lt;number of parts&gt;&#0160;を 6 に設定 &#0160;</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p>このエンドポイントは、後で追加の URL を要求したり、アップロードを確定するために使用する uploadKey も返す &#0160;</p>
</li>
</ul>
</li>
<li>残りのパーツ ファイルを、対応するアップロード URL にアップロード &#0160;<br />
<ul dir="auto">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>レスポンスコードが 100～199、429、500～599 の場合、個々のアップロードの再試行を検討する（例えば指数関数的バックオフを使用）</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>レスポンスコードが 403 の場合、アップロード用 URL の有効期限が切れているため、上記手順 2. へ戻る &#0160;</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>アップロード用 URL をすべて使い切ってしまい、まだアップロードする必要があるパーツが存在する場合、手順 2. に戻って URL を生成する &#0160;</p>
</li>
</ul>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="4" data-font="Calibri" data-leveltext="%1." data-listid="17">
<p><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-POST/">POST buckets/:bucketKey/objects/:objectKey/signeds3upload</a>&#0160;エンドポイントを使用して、ステップ 2. からの uploadKey 値を使用してアップロードを確定させる</p>
</li>
</ol>
<p>ローカルファイルを OSS Bucket にアップロードする場合（FileStream経由）のコードは、下記のようになります。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="code-overflow-x hljs cs" id="snippet-2"><span class="hljs-keyword">using</span> System.IO;
<span class="hljs-keyword">using</span> System.Threading.Tasks;

namespace Forge_Upload_DirectToS3.test
{
  <span class="hljs-keyword">public</span> <span class="hljs-keyword">class</span> upload_to_bucket
  {
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;dynamic&gt; <span class="hljs-title">UploadFile</span>(<span class="hljs-keyword">string</span> filePath, <span class="hljs-keyword">string</span> bucketKey, <span class="hljs-keyword">string</span> objectKey)
    {
      FileStream fileStream = <span class="hljs-keyword">new</span> FileStream(filePath, FileMode.Open);

      <span class="hljs-keyword">var</span> response = <span class="hljs-keyword">await</span> BinarytransferClient.UploadToBucket(bucketKey, objectKey, fileStream);

      <span class="hljs-keyword">return</span> response;
    }
  }
}</code></pre>
<div class="clipboard-container" data-clipboard-target="#snippet-2" data-on-clipboard="tooltip" data-placement="left" data-title="Copied!" data-trigger="manual" data-trigger-clipboard="data-trigger-clipboard">また、Data Management API で Hub（BIM 360、Fusion Teams、ACC など）にローカルファイルをアップロードする方法も忘れてはいけません。</div>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="code-overflow-x hljs cs" id="snippet-3"><span class="hljs-keyword">using</span> Autodesk.Forge;
<span class="hljs-keyword">using</span> Autodesk.Forge.Model;
<span class="hljs-keyword">using</span> System;
<span class="hljs-keyword">using</span> System.Collections.Generic;
<span class="hljs-keyword">using</span> System.IO;
<span class="hljs-keyword">using</span> System.Threading.Tasks;

namespace Forge_Upload_DirectToS3.test
{
  <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">class</span> upload_to_docs
  {
    <span class="hljs-keyword">public</span> <span class="hljs-keyword">static</span> <span class="hljs-keyword">async</span> Task&lt;dynamic&gt; <span class="hljs-title">UploadFile</span>(<span class="hljs-keyword">string</span> filePath, <span class="hljs-keyword">string</span> projectId, <span class="hljs-keyword">string</span> folderId, <span class="hljs-keyword">string</span> fileName)
    {
      BinarytransferClient.CREDENTIAL = <span class="hljs-keyword">await</span> BinarytransferClient.Get2LeggedTokenAsync(<span class="hljs-keyword">new</span> Scope[] { Scope.DataRead, Scope.DataWrite, Scope.DataCreate });

      FileStream fileStream = <span class="hljs-keyword">new</span> FileStream(filePath, FileMode.Open);

      <span class="hljs-comment">// prepare storage</span>
      ProjectsApi projectApi = <span class="hljs-keyword">new</span> ProjectsApi();
      projectApi.Configuration.AccessToken = BinarytransferClient.CREDENTIAL.access_token;
      StorageRelationshipsTargetData storageRelData = <span class="hljs-keyword">new</span> StorageRelationshipsTargetData(StorageRelationshipsTargetData.TypeEnum.Folders, folderId);
      CreateStorageDataRelationshipsTarget storageTarget = <span class="hljs-keyword">new</span> CreateStorageDataRelationshipsTarget(storageRelData);
      CreateStorageDataRelationships storageRel = <span class="hljs-keyword">new</span> CreateStorageDataRelationships(storageTarget);
      BaseAttributesExtensionObject attributes = <span class="hljs-keyword">new</span> BaseAttributesExtensionObject(<span class="hljs-keyword">string</span>.Empty, <span class="hljs-keyword">string</span>.Empty, <span class="hljs-keyword">new</span> JsonApiLink(<span class="hljs-keyword">string</span>.Empty), <span class="hljs-keyword">null</span>);
      CreateStorageDataAttributes storageAtt = <span class="hljs-keyword">new</span> CreateStorageDataAttributes(fileName, attributes);
      CreateStorageData storageData = <span class="hljs-keyword">new</span> CreateStorageData(CreateStorageData.TypeEnum.Objects, storageAtt, storageRel);
      CreateStorage storage = <span class="hljs-keyword">new</span> CreateStorage(<span class="hljs-keyword">new</span> JsonApiVersionJsonapi(JsonApiVersionJsonapi.VersionEnum._0), storageData);
      dynamic storageCreated = <span class="hljs-keyword">await</span> projectApi.PostStorageAsync(projectId, storage);

      <span class="hljs-keyword">string</span>[] storageIdParams = ((<span class="hljs-keyword">string</span>)storageCreated.data.id).Split(<span class="hljs-string">&#39;/&#39;</span>);
      <span class="hljs-keyword">string</span>[] bucketKeyParams = storageIdParams[storageIdParams.Length - <span class="hljs-number">2</span>].Split(<span class="hljs-string">&#39;:&#39;</span>);
      <span class="hljs-keyword">string</span> bucketKey = bucketKeyParams[bucketKeyParams.Length - <span class="hljs-number">1</span>];
      <span class="hljs-keyword">string</span> objectName = storageIdParams[storageIdParams.Length - <span class="hljs-number">1</span>];

      <span class="hljs-comment">// upload the file/object, which will create a new object</span>
      ObjectsApi objects = <span class="hljs-keyword">new</span> ObjectsApi();
      objects.Configuration.AccessToken = BinarytransferClient.CREDENTIAL.access_token;

      <span class="hljs-comment">//This is the only difference from the old method</span>
      <span class="hljs-keyword">var</span> response = <span class="hljs-keyword">await</span> BinarytransferClient.UploadToBucket(bucketKey, objectName, fileStream);

      <span class="hljs-keyword">if</span> ((<span class="hljs-keyword">int</span>)response.StatusCode &gt;= <span class="hljs-number">400</span>)
      {
        <span class="hljs-keyword">throw</span> <span class="hljs-keyword">new</span> Exception(response.Content);
      }

      <span class="hljs-comment">// check if file already exists...</span>
      FoldersApi folderApi = <span class="hljs-keyword">new</span> FoldersApi();
      folderApi.Configuration.AccessToken = BinarytransferClient.CREDENTIAL.access_token;
      <span class="hljs-keyword">var</span> filesInFolder = <span class="hljs-keyword">await</span> folderApi.GetFolderContentsAsync(projectId, folderId);
      <span class="hljs-keyword">string</span> itemId = <span class="hljs-keyword">string</span>.Empty;
      <span class="hljs-keyword">foreach</span> (KeyValuePair&lt;<span class="hljs-keyword">string</span>, dynamic&gt; item <span class="hljs-keyword">in</span> <span class="hljs-keyword">new</span> DynamicDictionaryItems(filesInFolder.data))
        <span class="hljs-keyword">if</span> (item.Value.attributes.displayName == fileName)
          itemId = item.Value.id; <span class="hljs-comment">// this means a file with same name is already there, so we&#39;ll create a new version</span>

      <span class="hljs-comment">// now decide whether create a new item or new version</span>
      <span class="hljs-keyword">if</span> (<span class="hljs-keyword">string</span>.IsNullOrWhiteSpace(itemId))
      {
        <span class="hljs-comment">// create a new item</span>
        BaseAttributesExtensionObject baseAttribute = <span class="hljs-keyword">new</span> BaseAttributesExtensionObject(projectId.StartsWith(<span class="hljs-string">&quot;a.&quot;</span>) ? <span class="hljs-string">&quot;items:autodesk.core:File&quot;</span> : <span class="hljs-string">&quot;items:autodesk.bim360:File&quot;</span>, <span class="hljs-string">&quot;1.0&quot;</span>);
        CreateItemDataAttributes createItemAttributes = <span class="hljs-keyword">new</span> CreateItemDataAttributes(fileName, baseAttribute);
        CreateItemDataRelationshipsTipData createItemRelationshipsTipData = <span class="hljs-keyword">new</span> CreateItemDataRelationshipsTipData(CreateItemDataRelationshipsTipData.TypeEnum.Versions, CreateItemDataRelationshipsTipData.IdEnum._1);
        CreateItemDataRelationshipsTip createItemRelationshipsTip = <span class="hljs-keyword">new</span> CreateItemDataRelationshipsTip(createItemRelationshipsTipData);
        StorageRelationshipsTargetData storageTargetData = <span class="hljs-keyword">new</span> StorageRelationshipsTargetData(StorageRelationshipsTargetData.TypeEnum.Folders, folderId);
        CreateStorageDataRelationshipsTarget createStorageRelationshipTarget = <span class="hljs-keyword">new</span> CreateStorageDataRelationshipsTarget(storageTargetData);
        CreateItemDataRelationships createItemDataRelationhips = <span class="hljs-keyword">new</span> CreateItemDataRelationships(createItemRelationshipsTip, createStorageRelationshipTarget);
        CreateItemData createItemData = <span class="hljs-keyword">new</span> CreateItemData(CreateItemData.TypeEnum.Items, createItemAttributes, createItemDataRelationhips);
        BaseAttributesExtensionObject baseAttExtensionObj = <span class="hljs-keyword">new</span> BaseAttributesExtensionObject(projectId.StartsWith(<span class="hljs-string">&quot;a.&quot;</span>) ? <span class="hljs-string">&quot;versions:autodesk.core:File&quot;</span> : <span class="hljs-string">&quot;versions:autodesk.bim360:File&quot;</span>, <span class="hljs-string">&quot;1.0&quot;</span>);
        CreateStorageDataAttributes storageDataAtt = <span class="hljs-keyword">new</span> CreateStorageDataAttributes(fileName, baseAttExtensionObj);
        CreateItemRelationshipsStorageData createItemRelationshipsStorageData = <span class="hljs-keyword">new</span> CreateItemRelationshipsStorageData(CreateItemRelationshipsStorageData.TypeEnum.Objects, storageCreated.data.id);
        CreateItemRelationshipsStorage createItemRelationshipsStorage = <span class="hljs-keyword">new</span> CreateItemRelationshipsStorage(createItemRelationshipsStorageData);
        CreateItemRelationships createItemRelationship = <span class="hljs-keyword">new</span> CreateItemRelationships(createItemRelationshipsStorage);
        CreateItemIncluded includedVersion = <span class="hljs-keyword">new</span> CreateItemIncluded(CreateItemIncluded.TypeEnum.Versions, CreateItemIncluded.IdEnum._1, storageDataAtt, createItemRelationship);
        CreateItem createItem = <span class="hljs-keyword">new</span> CreateItem(<span class="hljs-keyword">new</span> JsonApiVersionJsonapi(JsonApiVersionJsonapi.VersionEnum._0), createItemData, <span class="hljs-keyword">new</span> List&lt;CreateItemIncluded&gt;() { includedVersion });

        ItemsApi itemsApi = <span class="hljs-keyword">new</span> ItemsApi();
        itemsApi.Configuration.AccessToken = BinarytransferClient.CREDENTIAL.access_token;
        <span class="hljs-keyword">var</span> newItem = <span class="hljs-keyword">await</span> itemsApi.PostItemAsync(projectId, createItem);
        <span class="hljs-keyword">return</span> newItem;
      }
      <span class="hljs-keyword">else</span>
      {
        <span class="hljs-comment">// create a new version</span>
        BaseAttributesExtensionObject attExtensionObj = <span class="hljs-keyword">new</span> BaseAttributesExtensionObject(projectId.StartsWith(<span class="hljs-string">&quot;a.&quot;</span>) ? <span class="hljs-string">&quot;versions:autodesk.core:File&quot;</span> : <span class="hljs-string">&quot;versions:autodesk.bim360:File&quot;</span>, <span class="hljs-string">&quot;1.0&quot;</span>);
        CreateStorageDataAttributes storageDataAtt = <span class="hljs-keyword">new</span> CreateStorageDataAttributes(fileName, attExtensionObj);
        CreateVersionDataRelationshipsItemData dataRelationshipsItemData = <span class="hljs-keyword">new</span> CreateVersionDataRelationshipsItemData(CreateVersionDataRelationshipsItemData.TypeEnum.Items, itemId);
        CreateVersionDataRelationshipsItem dataRelationshipsItem = <span class="hljs-keyword">new</span> CreateVersionDataRelationshipsItem(dataRelationshipsItemData);
        CreateItemRelationshipsStorageData itemRelationshipsStorageData = <span class="hljs-keyword">new</span> CreateItemRelationshipsStorageData(CreateItemRelationshipsStorageData.TypeEnum.Objects, storageCreated.data.id);
        CreateItemRelationshipsStorage itemRelationshipsStorage = <span class="hljs-keyword">new</span> CreateItemRelationshipsStorage(itemRelationshipsStorageData);
        CreateVersionDataRelationships dataRelationships = <span class="hljs-keyword">new</span> CreateVersionDataRelationships(dataRelationshipsItem, itemRelationshipsStorage);
        CreateVersionData versionData = <span class="hljs-keyword">new</span> CreateVersionData(CreateVersionData.TypeEnum.Versions, storageDataAtt, dataRelationships);
        CreateVersion newVersionData = <span class="hljs-keyword">new</span> CreateVersion(<span class="hljs-keyword">new</span> JsonApiVersionJsonapi(JsonApiVersionJsonapi.VersionEnum._0), versionData);

        VersionsApi versionsApis = <span class="hljs-keyword">new</span> VersionsApi();
        versionsApis.Configuration.AccessToken = BinarytransferClient.CREDENTIAL.access_token;
        dynamic newVersion = <span class="hljs-keyword">await</span> versionsApis.PostVersionAsync(projectId, newVersionData);
        <span class="hljs-keyword">return</span> newVersion;
      }
    }
  }
}</code></pre>
<p>ご不明な点等ございましたら、<a href="mailto:forge.help@autodesk.com">forge.help@autodesk.com</a>.までお問い合わせください。&#0160;</p>
<p><em>※ 本記事は <a href="https://forge.autodesk.com/ja/node/2230" rel="noopener" target="_blank">Direct-to-S3 .NET samples</a>&#0160;から転写・翻訳して一部加筆したものです。</em></p>
<p>By Toshiaki Isezaki</p>
