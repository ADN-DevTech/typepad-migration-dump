---
layout: "post"
title: "Data Management OSS (Object Storage Service) の Direct-to-S3 アプローチへの移行について"
date: "2022-03-25 00:31:57"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/03/data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach.html "
typepad_basename: "data-management-oss-object-storage-service-migrating-to-direct-to-s3-approach"
typepad_status: "Publish"
---

<div class="node__image"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880728709200d-pi" style="display: inline;"><img alt="Screen Shot 2022-03-16 at 6.14.40 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880728709200d image-full img-responsive" src="/assets/image_488344.jpg" title="Screen Shot 2022-03-16 at 6.14.40 PM" /></a></div>
<div class="node__body">
<div class="field-body">
<p>革新テクノロジの反映とサービス向上の一環として、オートデスクは、基盤となる AWS S3 Bucket（バケット）からの直接アップロードとダウンロードを可能にする Data Management API の新しいエンドポイント セットをリリースしました。この direct-to-S3 アプローチの主な利点は、パフォーマンスです。</p>
<p>オートデスク社内でのテストと検証では、特に大容量ファイルのアップロードとダウンロードの速度が大幅に向上していることが継続して確認されています。&#0160;</p>
<p><strong>どのように？</strong> オブジェクトが OSS（Object Storage Services）Bucket へのアップロード/からのダウンロード時、オートデスクは AWS S3 ストレージを使用しています。</p>
<p>現在のバイナリ転送アプローチでは、オートデスクのプロキシを介してデータを移動させますが、新しい direct-to-S3 アプローチでは、基盤となる S3 Bucket と Object に直接アクセスすることが可能になります。    </p>
<p><strong>Data Management のストリーミング エンドポイントを 2022年<span style="text-decoration: line-through;">9月30日</span> <span style="background-color: #ffff00;">12月31日</span>に廃止予定 </strong></p>
<p>オートデスクは、2022 年 <span style="text-decoration: line-through;">9 月 30 日</span>1<span style="background-color: #ffff00;">2 月 31 日</span>に、現在のプロキシ (<em><strong>developer.api.autodesk.com</strong></em>) と直接ファイルをアップロード、また、ダウンロードする現在のバイナリ転送ストリーミング用のエンドポイントのアプローチを廃止し、同日以降は転送を許可しない方針です。</p>
<p>2022 年 <span style="text-decoration: line-through;">9 月 30 日</span>1<span style="background-color: #ffff00;">2 月 31 日</span>までに、影響を受ける API を使用する既存の Forge アプリを新しい direct-to-S3 方式に移行する必要があります。 </p>
<p>影響を受ける API エンドポイントに関する最新情報は下記をご確認ください。</p>
<p><strong>影響を受ける API:</strong></p>
<ul>
<li><strong>Data Management API</strong> : Bucket と Object:&#0160; <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-PUT/" rel="noreferrer noopener" target="_blank">PUT object</a>、 <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-resumable-PUT/" rel="noreferrer noopener" target="_blank">PUT resumable</a>、 <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-GET/" rel="noreferrer noopener" target="_blank">GET object</a> エンドポイントが削除されます。同エンドポイントを使用している Forge アプリは、次の <strong><a href="#direct-to-s3-approach">Data Management OSS  の Direct-to-S3 アプローチ</a> &#0160;</strong>セクションで詳しく説明する signeds3upload と signeds3download に移行する必要があります。 </li>
<li><strong>Model Derivative API</strong> : <a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-derivativeurn-GET/%22%20/t%20%22_blank" rel="noreferrer noopener" target="_blank">GET Derivatives</a> エンドポイントは、新しいエンドポイント<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/reference/http/urn-manifest-derivativeUrn-signedcookies-GET/" rel="noopener" target="_blank">GET Fetch Derivative Download</a> に移行してください。</li>
<li><strong>Viewer:</strong>：v7.68 以前のバージョンを使用している場合、Viewer の初期化時にエンドポイント オプションを追加または変更する必要があります。他のオプションの変更は必要ありません。</li>
</ul>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code" style="padding-left: 40px;"><code class="language-javascript code-overflow-x hljs " id="snippet-0"><span class="hljs-keyword">var</span> options = {
&#0160;&#0160;&#0160; endpoint: <span class="hljs-string">&#39;https://cdn.derivative.autodesk.com&#39;</span>
&#0160;&#0160;  <span class="hljs-comment">// keep other options unchanged</span>
};&#0160;</code></pre>
<ul>
<li><strong>BIM 360 Docs&#0160;PDF Export</strong> ：<a href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/document-management-projects-project_id-versions-version_id-exports-export_id-GET/" rel="noopener" target="_blank">GET export</a> エンドポイントは、追加のS3署名付きURL、（すなわち、レスポンス内のsignedUrlフィールド）を提供するように更新されます。この署名付きURLを呼び出すことで、実際のコンテンツをダウンロードすることができます。現在の<strong> link</strong> フィールドは、移行期間中の互換性のために、まだ機能しています。 詳しくは<strong><a href="https://forge.autodesk.com/blog/bim-360-docs-export-pdf-api-migrating-s3-signed-url" rel="noopener" target="_blank">こちら</a></strong>をご覧ください。</li>
</ul>
<p><strong>次の API は影響を受けませんが、アプリには相互依存性がある可能性があります :</strong></p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-listid="2"><strong>Design Automation API</strong>：OSS は、Design Automation API の WorkItem で入出力用のストレージに OSS Bucket 使用している場合です。Design Automation API 自体に変更はありません。次の <strong><a href="#direct-to-s3-approach">Data Management OSS  の Direct-to-S3 アプローチ</a></strong><strong> &#0160;</strong>セクションで解説しているように、WorkItem が OSS に保存されている場合、新しい <strong>useCdn=true</strong>クエリーパラメータを使用して署名付き URL を取得する既存のエンドポイントを使用することができます。Autodesk Construction Cloud&#0160;&#0160;または BIM 360 を使用している場合には、次の <strong>Data Management API</strong> 項の手順を参照してださい。</li>
<li><strong>Autodesk Construction Cloud &amp; BIM 360</strong>：&#0160;OSSは、これらのサービスのストレージとして一般的に使用されています。ストレージへのアクセス方法にはありません。次の <strong>Data Management API</strong> 項の手順を参照してださい。</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-listid="2"><strong>Data Management API</strong> <strong>：</strong>Projects、Folders 、Files :&#0160; &#0160;ファイルをアップロード、およびダウンロードするアプリは、署名付き S3 URL （Signed S3 URL）使用する必要があります。
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-listid="2">ダウンロード：（改定された）チュートリアルの <strong><a href="https://forge.autodesk.com/en/docs/data/v2/tutorials/download-file/" rel="noopener" target="_blank">Step 6</a></strong> のように、署名されたS3 URLを生成し、それを使用してダウンロードする必要があります。</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-listid="2">アップロード：（改定された）チュートリアルの <strong><a href="https://forge.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="noopener" target="_blank">Step 4</a></strong> のように、アップロード用の署名付きS3 URLを生成し、S3 URLを使用してアップロードし、アップロード完了をコールするという 3 つのアクションが必要になります。</li>
</ul>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-listid="2"><strong>BIM 360/Autodesk Build Cost API</strong> : Cost API にいては、変更はありません。今回の更新は、変更の影響を受ける Data Management API に依存するアップロード/ダウンロードのワークフローに対するものです。Data Management API を使用して Docs のファイルをアップロード/ダウンロードされている方は、すでにこの変更についてご存じかと思われます。詳しくは<a href="https://forge.autodesk.com/blog/bim-360autodesk-build-cost-api-tutorials-updated-use-s3-signed-url" rel="noopener" target="_blank"><strong>こちら</strong></a>をご確認ださい。 &#0160;</li>
</ul>
<p><strong>影響を受けない API:</strong></p>
<ul>
<li>Authentication  API（OAuth API）</li>
<li>Data Visualization  API</li>
<li>Token Flex  API</li>
<li>Webhooks  API</li>
<li>Assets API</li>
</ul>
<p><strong><a id="direct-to-s3-approach"></a>Data Management OSS  の Direct-to-S3 アプローチ &#0160;</strong></p>
<p>ファイルをアップロード、あるいは、ダウンロードするためには、Forge アプリは署名付き URL を生成してバイナリ ファイルをアップロード、ダウンロードする必要があります。疑似シナリオに沿った、おおまかな手順は次のとおりです。  &#0160;</p>
<p><strong>アップロード</strong></p>
<ol role="list" start="1">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Calibri" data-leveltext="%1." data-listid="7">
<p>アップロードするファイルのパート数を算出</p>
<ul>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Calibri" data-leveltext="%1." data-listid="7">
<p>&#0160;注意：最後の 1 つを除き、アップロードする各パートは 5 MB（1024×5）以上であること &#0160;</p>
</li>
</ul>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-GET/" rel="noopener" target="_blank">GET buckets/:bucketKey/objects/:objectKey/signeds3upload?firstPart=&lt;index of first part&gt;&amp;parts=&lt;number of parts&gt;</a> エンドポイントを使用して特定のパートのファイルをアップロードするための、最大 25 の URL を生成</p>
<ul>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p>パート番号は 1 から始まるものと仮定  &#0160;</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p>例えば、10 番パートから 15 番パートまでのアップロード用 URL を生成するには、&lt;index of first part&gt; を 10 に、&lt;number of parts&gt; を 6 に設定 &#0160;</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="9">
<p>このエンドポイントは、後で追加の URL を要求したり、アップロードを確定するために使用する uploadKey も返す &#0160;</p>
</li>
</ul>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>&#0160;残りのパート ファイルを、対応するアップロード URL にアップロード &#0160;</p>
<ul>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>レスポンスコードが 100～199、429、500～599 の場合、個々のアップロードの再試行を検討する（例えば指数関数的バックオフを使用）</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>レスポンスコードが 403 の場合、アップロード用 URL の有効期限が切れているため、上記手順 2. へ戻る &#0160;</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Calibri" data-leveltext="%1." data-listid="13">
<p>アップロード用 URL をすべて使い切ってしまい、まだアップロードする必要があるパートが存在する場合、手順 2. に戻って URL を生成する &#0160;</p>
</li>
</ul>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="4" data-font="Calibri" data-leveltext="%1." data-listid="17">
<p><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectKey-signeds3upload-POST/" rel="noopener" target="_blank">POST buckets/:bucketKey/objects/:objectKey/signeds3upload</a> エンドポイントを使用して、ステップ 2. からの uploadKey 値を使用してアップロードを確定させる</p>
</li>
</ol>
<p><strong>ダウンロード</strong></p>
<ol role="list" start="1">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Calibri" data-leveltext="%1." data-listid="18">
<p><a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-:objectName-signeds3download-GET" rel="noopener noreferrer" target="_blank">GET buckets/:bucketKey/objects/:objectName/signeds3download</a> エンドポイントを使ってダウンロード URL を生成 &#0160;</p>
</li>
</ol>
<ol role="list" start="2">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="19">
<p>&#0160;新しい URL を使用して、AWS S3 から直接 OSS オブジェクトをダウンロード &#0160;</p>
<ul>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Calibri" data-leveltext="%1." data-listid="19">
<p>&#0160;応答コードが 100～199、429、500～599 の場合、ダウンロードの再試行を考慮する（例えば指数関数的バックオフを使用）</p>
</li>
</ul>
</li>
</ol>
<p>署名付き URL の有効期限は、既定値で 2 分です（※<em>minutesExpiration</em> パラメータで最大 60 分まで設定可能）。有効期限が切れる前にダウンロードまたはアップロード処理を開始することが重要です。シングルまたはマルチパートのアップロードの場合、署名付き URL を再度要求することができます。<a href="https://forge.autodesk.com/en/docs/data/v2/tutorials/upload-file/" rel="noopener" target="_blank">アップロード</a>と<a href="https://forge.autodesk.com/en/docs/data/v2/tutorials/download-file/" rel="noopener" target="_blank">ダウンロード</a>の手順については、Step-by-Step Documentationを参照してください。&#0160;</p>
<p>以後公開するブログ記事では、<a href="https://adndevblog.typepad.com/technology_perspective/2022/04/direct-to-s3-nodejs-samples.html" rel="noopener" target="_blank">Node.js</a>、<a href="https://adndevblog.typepad.com/technology_perspective/2022/04/direct-to-s3-net-samples.html" rel="noopener" target="_blank">.NET Core/.NET</a> サンプルについてご紹介します。</p>
<p>ご不明な点やご質問がありましたら、<a href="mailto:forge.help@autodesk.com">forge.help@autodesk.com</a> までお問合せください。&#0160;</p>
<p><em data-stringify-type="italic">*免責事項 <br />署名付き URL を生成する場合、URL が公開された場合のアクセス延長を避けるため、できるだけ短い有効期限を使用することが重要です。</em></p>
<p><em>※ 本記事は <a href="https://forge.autodesk.com/blog/data-management-oss-object-storage-service-migrating-direct-s3-approach">Data Management OSS (Object Storage Service) migrating to Direct-to-S3 approach | Autodesk Forge</a>&#0160;から転写・翻訳して一部加筆したものです。</em></p>
<p>By Toshiaki Isezaki</p>
</div>
</div>
