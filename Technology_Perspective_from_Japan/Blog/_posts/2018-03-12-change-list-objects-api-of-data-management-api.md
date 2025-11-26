---
layout: "post"
title: "Data Management API:'List Objects' API の変更について"
date: "2018-03-12 01:05:06"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/03/change-list-objects-api-of-data-management-api.html "
typepad_basename: "change-list-objects-api-of-data-management-api"
typepad_status: "Publish"
---

<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/02/general-data-protection-regulation.html" rel="noopener noreferrer" target="_blank">General Data Protection Regulation について</a></strong> でご案内した <strong>GDPR</strong> 対応のため、クラウド業界がにわかにざわついているように感じます。オートデスクも&#0160; <strong><a href="https://www.autodesk.com/trust/privacy-and-compliance" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/trust/privacy-and-compliance</a></strong>&#0160;の最後の部分で GDPR 対応を表明していますが、Forge もその対象です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c957306f970b-pi" style="display: inline;"><img alt="Gdpr_announcement" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c957306f970b image-full img-responsive" src="/assets/image_6585.jpg" title="Gdpr_announcement" /></a></p>
<p>GDPR の施行は 5 月からですが、事前の GDPR 対応のため、4 月を目途に Forge の Data Management API にも 一部の RESTful API に変更が加えられる予定です。ここでは、具体的にどの endpoint に、どのような変更が加えられるかをご案内しておきます。なお、変更が反映された時点で、<strong><a href="https://www.facebook.com/adn.open.japan/" rel="noopener noreferrer" target="_blank">ADN オープンの Facebook ページ</a></strong>でご案内する予定です。に</p>
<p>変更が発生するのは、<strong><a href="https://developer.autodesk.com/en/docs/data/v2/reference/http/buckets-:bucketKey-objects-GET/" rel="noopener noreferrer" target="_blank">GET /oss/v2/buckets/&lt;bucket-name&gt;/objects</a></strong> endpoint のレスポンスです。この endpoint は、Object Storage Service (OSS) に保存されている Bucket 内のコンテンツをリストとして JSON 形式で返すものです。この API をお使いの方には、コードの変更が必要になる可能性がありあます。ご不便をお掛けしますが、ご対応をお願いいたします。</p>
<p><strong>変更点:</strong></p>
<ol>
<li><strong>レスポンス結果は語彙順序（アルファベット等）にならな場合がある</strong></li>
<li><strong>次ページの結果を取得するために指定する startAt クエリ パラメータの値が異なる形式にな変更になる</strong></li>
</ol>
<p>1. については、アプリケーションが結果が特定の順序で返されると仮定してはならないということを単に意味するものです。</p>
<p>2. について、アプリケーション/サービスが単にレスポンスの ”next”フィールドの値を読み取って処理をしている場合には、その URL には要求を処理するのに必要なすべてのクエリ パラメータが含まれるのでコードの変更は不要です。ただし、その URL をパースしてパラメータを抽出している場合には、startAt パラメータの値を変更するとコードの変更になることがあります。</p>
<p><strong>例：</strong></p>
<p>&quot;test-bucket&quot; の名称で Bucket を作成し、&quot;file-0001&quot; から &quot;file-0100&quot; までのファイル名で 100 ファイルをアップロードしていると仮定します。</p>
<p>オリジナルの &#39;List Objects&#39; endpoint は次のように動作します。</p>
<p><strong>GET /oss/v2/buckets/test-bucket/objects (オリジナル API)</strong></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-0">{

&#0160; &quot;<span class="hljs-attribute">items</span>&quot; : <span class="hljs-value">[

&#0160;&#0160;&#0160; {

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">bucketKey</span>&quot; : <span class="hljs-string">&quot;test-bucket&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">objectKey</span>&quot; : <span class="hljs-string">&quot;file-0001&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">objectId</span>&quot; : <span class="hljs-string">&quot;urn:adsk.objects:os.object:test-bucket/file-0001&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">sha1</span>&quot; : <span class="hljs-string">&quot;da39a3ee5e6b4b0d3255bfef95601890afd80709&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">size</span>&quot; : <span class="hljs-number">9</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">location</span>&quot; : <span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects/file-0001&quot;</span>

&#0160;&#0160;&#0160; },

&#0160;&#0160;&#0160; ... その他 <span class="hljs-number">8</span> アイテムが返る ...

&#0160;&#0160;&#0160; {

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">bucketKey</span>&quot; : <span class="hljs-string">&quot;test-bucket&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">objectKey</span>&quot; : <span class="hljs-string">&quot;file-0010&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">objectId</span>&quot; : <span class="hljs-string">&quot;urn:adsk.objects:os.object:test-bucket/file-0010&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">sha1</span>&quot; : <span class="hljs-string">&quot;da39a3ee5e6b4b0d3255bfef95601890afd80709&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">size</span>&quot; : <span class="hljs-number">9</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">location</span>&quot; : <span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects/file-0010&quot;</span>

&#0160;&#0160;&#0160; }

&#0160; ]</span>,

&#0160; &quot;<span class="hljs-attribute">next</span>&quot; : <span class="hljs-value"><span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects?startAt=file-0010&quot;</span>

</span>}</code></pre>
<p>レスポンスの &quot;next&quot; フィールドを使用するだけで、次のページをリクエストできました。</p>
<p><strong>GET /oss/v2/buckets/test-bucket/objects?startAt=file-0010 (オリジナル API)</strong></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-1">{

&#0160; &quot;<span class="hljs-attribute">items</span>&quot; : <span class="hljs-value">[

&#0160;&#0160;&#0160; {

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">bucketKey</span>&quot; : <span class="hljs-string">&quot;test-bucket&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">objectKey</span>&quot; : <span class="hljs-string">&quot;file-0011&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">objectId</span>&quot; : <span class="hljs-string">&quot;urn:adsk.objects:os.object:test-bucket/file-0011&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">sha1</span>&quot; : <span class="hljs-string">&quot;da39a3ee5e6b4b0d3255bfef95601890afd80709&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">size</span>&quot; : <span class="hljs-number">9</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">location</span>&quot; : <span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects/file-0011&quot;</span>

&#0160;&#0160;&#0160; },

&#0160;&#0160;&#0160; ... その他 <span class="hljs-number">8</span> アイテムが返る ...

&#0160;&#0160;&#0160; {

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">bucketKey</span>&quot; : <span class="hljs-string">&quot;test-bucket&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">objectKey</span>&quot; : <span class="hljs-string">&quot;file-0020&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">objectId</span>&quot; : <span class="hljs-string">&quot;urn:adsk.objects:os.object:test-bucket/file-0020&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">sha1</span>&quot; : <span class="hljs-string">&quot;da39a3ee5e6b4b0d3255bfef95601890afd80709&quot;</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">size</span>&quot; : <span class="hljs-number">9</span>,

&#0160;&#0160;&#0160;&#0160;&#0160; &quot;<span class="hljs-attribute">location</span>&quot; : <span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects/file-0020&quot;</span>

&#0160;&#0160;&#0160; }

&#0160; ]</span>,

&#0160; &quot;<span class="hljs-attribute">next</span>&quot; : <span class="hljs-value"><span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects?startAt=file-0020&quot;</span>

</span>}</code></pre>
<p>同様の処理は &quot;next&quot; フィールドがなくなる迄繰り返せました。&#0160;</p>
<p><strong>GET /oss/v2/buckets/test-bucket/objects?startAt=file-0090 (ORIGINAL API)</strong></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-2">{

  &quot;<span class="hljs-attribute">items</span>&quot; : <span class="hljs-value">[

    {

      &quot;<span class="hljs-attribute">bucketKey</span>&quot; : <span class="hljs-string">&quot;test-bucket&quot;</span>,

      &quot;<span class="hljs-attribute">objectKey</span>&quot; : <span class="hljs-string">&quot;file-0091&quot;</span>,

      &quot;<span class="hljs-attribute">objectId</span>&quot; : <span class="hljs-string">&quot;urn:adsk.objects:os.object:test-bucket/file-0091&quot;</span>,

      &quot;<span class="hljs-attribute">sha1</span>&quot; : <span class="hljs-string">&quot;da39a3ee5e6b4b0d3255bfef95601890afd80709&quot;</span>,

      &quot;<span class="hljs-attribute">size</span>&quot; : <span class="hljs-number">9</span>,

      &quot;<span class="hljs-attribute">location</span>&quot; : <span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects/file-0091&quot;</span>

    },

&#0160;&#0160;&#0160; ... その他 <span class="hljs-number">8</span> アイテムが返る ...

    {

      &quot;<span class="hljs-attribute">bucketKey</span>&quot; : <span class="hljs-string">&quot;test-bucket&quot;</span>,

      &quot;<span class="hljs-attribute">objectKey</span>&quot; : <span class="hljs-string">&quot;file-0100&quot;</span>,

      &quot;<span class="hljs-attribute">objectId</span>&quot; : <span class="hljs-string">&quot;urn:adsk.objects:os.object:test-bucket/file-0100&quot;</span>,

      &quot;<span class="hljs-attribute">sha1</span>&quot; : <span class="hljs-string">&quot;da39a3ee5e6b4b0d3255bfef95601890afd80709&quot;</span>,

      &quot;<span class="hljs-attribute">size</span>&quot; : <span class="hljs-number">9</span>,

      &quot;<span class="hljs-attribute">location</span>&quot; : <span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects/file-0100&quot;</span>

    }

  ]

</span>}</code></pre>
<p>このように、オブジェクトはアルファベット順に返され、&quot;next&quot; フィールドの &quot;startAt&quot; クエリ パラメータ は objectKey として簡単に解釈できました。このような動作は、次のような新しい動作に変更されることになります。</p>
<p><strong>GET /oss/v2/buckets/test-bucket/objects (新 API)</strong></p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-json code-overflow-x hljs " id="snippet-3">{

  &quot;<span class="hljs-attribute">items</span>&quot; : <span class="hljs-value">[

    {

      &quot;<span class="hljs-attribute">bucketKey</span>&quot; : <span class="hljs-string">&quot;test-bucket&quot;</span>,

      &quot;<span class="hljs-attribute">objectKey</span>&quot; : <span class="hljs-string">&quot;file-0061&quot;</span>,

      &quot;<span class="hljs-attribute">objectId</span>&quot; : <span class="hljs-string">&quot;urn:adsk.objects:os.object:test-bucket/file-0061&quot;</span>,

      &quot;<span class="hljs-attribute">sha1</span>&quot; : <span class="hljs-string">&quot;da39a3ee5e6b4b0d3255bfef95601890afd80709&quot;</span>,

      &quot;<span class="hljs-attribute">size</span>&quot; : <span class="hljs-number">0</span>,

      &quot;<span class="hljs-attribute">location</span>&quot; : <span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects/file-0061&quot;</span>

    }, {

      &quot;<span class="hljs-attribute">bucketKey</span>&quot; : <span class="hljs-string">&quot;test-bucket&quot;</span>,

      &quot;<span class="hljs-attribute">objectKey</span>&quot; : <span class="hljs-string">&quot;file-0098&quot;</span>,

      &quot;<span class="hljs-attribute">objectId</span>&quot; : <span class="hljs-string">&quot;urn:adsk.objects:os.object:test-bucket/file-0098&quot;</span>,

      &quot;<span class="hljs-attribute">sha1</span>&quot; : <span class="hljs-string">&quot;da39a3ee5e6b4b0d3255bfef95601890afd80709&quot;</span>,

      &quot;<span class="hljs-attribute">size</span>&quot; : <span class="hljs-number">0</span>,

      &quot;<span class="hljs-attribute">location</span>&quot; : <span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects/file-0098&quot;</span>

    },

&#0160;&#0160;&#0160; ... その他 7 アイテムが返る ...

    {

      &quot;<span class="hljs-attribute">bucketKey</span>&quot; : <span class="hljs-string">&quot;test-bucket&quot;</span>,

      &quot;<span class="hljs-attribute">objectKey</span>&quot; : <span class="hljs-string">&quot;file-0067&quot;</span>,

      &quot;<span class="hljs-attribute">objectId</span>&quot; : <span class="hljs-string">&quot;urn:adsk.objects:os.object:test-bucket/file-0067&quot;</span>,

      &quot;<span class="hljs-attribute">sha1</span>&quot; : <span class="hljs-string">&quot;da39a3ee5e6b4b0d3255bfef95601890afd80709&quot;</span>,

      &quot;<span class="hljs-attribute">size</span>&quot; : <span class="hljs-number">0</span>,

      &quot;<span class="hljs-attribute">location</span>&quot; : <span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects/file-0067&quot;</span>

    }

  ]</span>,

  &quot;<span class="hljs-attribute">next</span>&quot; : <span class="hljs-value"><span class="hljs-string">&quot;https://developer.api.autodesk.com/oss/v2/buckets/test-bucket/objects?startAt=7%7C&quot;</span>

</span>}</code></pre>
<p>このように、新しい動作ではレスポンス結果は順序だった整然なものではなくなります。&quot;next&quot; フィールドは次のページを取得するために使うことができますが、もはや単純な objectKey ではなくなります。つまり、startAtフィールドが何であるかに関係なく、次のページを取得するために直接使用すべきではありません。</p>
<p>By Toshiaki Isezaki</p>
