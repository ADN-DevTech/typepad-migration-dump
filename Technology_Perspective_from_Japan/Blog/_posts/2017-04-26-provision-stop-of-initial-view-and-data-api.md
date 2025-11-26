---
layout: "post"
title: "初期の View and Data API の提供停止について"
date: "2017-04-26 00:54:02"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/04/provision-stop-of-initial-view-and-data-api.html "
typepad_basename: "provision-stop-of-initial-view-and-data-api"
typepad_status: "Publish"
---

<p>2014 年に Autodesk Forge の前身となる <strong>View and Data API</strong> がベータ提供され始めてから 、早いもので &#0160;3 年が経過しました。その後、昨年、2016 年の 6 月には正式に Autodesk Forge がリリースされています。この際には、View and Data API の機能が &#0160;Authentication API(OAuth API)、Model Derivative API、Data Management API、Viewer(Forge Viewer) に <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/06/about-changes-of-forge-platform-api.html" rel="noopener noreferrer" target="_blank">分離</a></strong>&#0160;されています。</p>
<p>今回、初期に提供していた &#0160;View and Data API のリタイアがアナウンスされましたので、ここでご案内したいと思います。</p>
<p><span style="background-color: #ffff00;"><strong>2017 年 7 月 17 日以降、旧 View and Data API へのアクセスが出来なくなりますので、それまでの間にお手持ちのコードを &#0160;Autodesk Forge を利用するよう移植していただくようお願いします。7 月 17 日までに Forge への移行が完了していない場合、View and Data API を利用するアプリ/サービスは正しく動作しなくなってしまいます。</strong></span></p>
<p><span style="background-color: #ffffff;"><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09922c7e970d-pi" style="display: inline;"><img alt="Retire_view_and_data_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09922c7e970d image-full img-responsive" src="/assets/image_563729.jpg" title="Retire_view_and_data_api" /></a><br /></strong></span></p>
<p><span style="background-color: #ffffff;">View and Data API の RESTful API で、具体的に移行すべき主要な endpoint は次のとおりです。View and Data API 時は、endpoint に &#0160;v1 のバージョン名が挿入されていますが、Forge になって v2 に更新されています。</span></p>
<p><strong>Access Token の取得</strong></p>
<p style="padding-left: 30px;">2-legged 認証で Access Token を取得する場合は、Forge でも View and Data API 時と同じ endpoint POST https://developer.api.autodesk.com/authentication/v1/authenticate を利用します。ただし、Forge では当初設定のなかった <strong><a href="https://developer.autodesk.com/en/docs/oauth/v2/overview/scopes/" rel="noopener noreferrer" target="_blank">Scope 指定</a></strong>&#0160;が必須になっています。&#0160;</p>
<p><strong>Bucket の作成</strong></p>
<p style="padding-left: 30px;">v1：POST&#0160;https://developer.api.autodesk.com/oss/v1/buckets&#0160;<br />v2：POST&#0160;https://developer.api.autodesk.com/oss/v2/buckets</p>
<p><strong>ファイル アップロード</strong></p>
<p style="padding-left: 30px;">v1：PUT&#0160;https://developer.api.autodesk.com/oss/v1/buckets/{bucketkey}/objects/{objectkey}<br />v2：PUT https://developer.api.autodesk.com/oss/v2/buckets/{bucketkey}/objects/{objectkey}</p>
<p><strong>サポートされる変換形式の取得</strong></p>
<p style="padding-left: 30px;">v1：GET&#0160;https://developer.api.autodesk.com/viewingservice/v1/supported<br />v2：GET&#0160;https://developer.api.autodesk.com/modelderivative/v2/designdata/formats</p>
<p><strong>変換指示（変換登録）</strong></p>
<p style="padding-left: 30px;">v1：POST&#0160;https://developer.api.autodesk.com/viewingservice/v1/register</p>
<ul style="list-style-type: disc;">
<li>リクエスト ボディ：<br />
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class=" hljs json">{
  &quot;<span class="hljs-attribute">urn</span>&quot;: <span class="hljs-value"><span class="hljs-string">&quot;{{base64Urn}}&quot;</span>
</span>}</code></pre>
</li>
</ul>
<p style="padding-left: 30px;">v2：POST https://developer.api.autodesk.com/modelderivative/v2/designdata/job</p>
<ul>
<li>&#0160;リクエスト ボディ:<br />
<pre class="js-more-toggle" data-end-color="rgba(250, 250, 250, 1)" data-height="400"><code class=" hljs json">{
  &quot;<span class="hljs-attribute">input</span>&quot;: <span class="hljs-value">{
      &quot;<span class="hljs-attribute">urn</span>&quot;: <span class="hljs-string">&quot;{{base64Urn}}&quot;</span>
  }</span>,
  &quot;<span class="hljs-attribute">output</span>&quot;: <span class="hljs-value">{
      &quot;<span class="hljs-attribute">destination</span>&quot;: {
          &quot;<span class="hljs-attribute">region</span>&quot;: <span class="hljs-string">&quot;us&quot;</span>
      },
      &quot;<span class="hljs-attribute">formats</span>&quot;: [
      {
          &quot;<span class="hljs-attribute">type</span>&quot;: <span class="hljs-string">&quot;svf&quot;</span>,
          &quot;<span class="hljs-attribute">views</span>&quot;:[<span class="hljs-string">&quot;2d&quot;</span>, <span class="hljs-string">&quot;3d&quot;</span>]
      }]
  }
</span>}</code></pre>
</li>
</ul>
<p><strong>変換ステータス取得（マニフェスト取得）</strong></p>
<p style="padding-left: 30px;">v1：GET&#0160;https://developer.api.autodesk.com/viewingservice/v1/{URN}<br />v2：GET&#0160;https://developer.api.autodesk.com/modelderivative/v2/designdata/{URN}/manifest</p>
<p>すでに上記 v2 の endpoint（Forge）をお使いの方は、特にアクションする必要はありません。</p>
<p><span style="background-color: #ffff00;">ご注意：</span>View and Data API では、2-legged 認証を使って Object Storage Service（OSS）に一意な名前の Bucket を作成し、デザイン ファイルをアップロードして登録（変換）後、Viewer JavaScript ライブラリを使って Web ブラウザに変換されたデザインを表示する機能のみサポートされていました。逆に言えば、View and Data API は、現在、Forge が提供している 3-legged 認証と A360 ストレージへのアクセスを提供していません。</p>
<p>By Toshiaki Isezaki</p>
