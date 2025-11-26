---
layout: "post"
title: "Access Token について"
date: "2018-11-04 23:02:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/11/about-access-token.html "
typepad_basename: "about-access-token"
typepad_status: "Publish"
---

<p>先日のブログ記事&#0160;<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html" rel="noopener noreferrer" target="_blank">Web ブラウザのデベロッパーツールについて </a></strong>で少し触れたこともあるので、今回は Forge を利用する上で重要になる Access Token について、まとめておきたいと思います。</p>
<p>Access Token は、Forge に限らず、Web 開発の世界では一般的なもので、アプリが サーバー（クラウド）上にあるリソースにアクセスする権限があるかチェックする仕組みを提供します。Forge の場合、2-legged、3-legged の違いを考えずに単純に考えると、下図のアニメーションのようになります。</p>
<ol>
<li>Forge を利用しようとする開発者は、まず、Autodesk ID を使って <strong><a href="https://forge.autodesk.com/" rel="noopener noreferrer" target="_blank">Forge ポータル（https://forge.autodesk.com）</a></strong>にサインインし、アプリを登録する作業をおこないます。</li>
<li>アプリを登録すると、一意な Client ID と Client Secret が発行され、Web ページ上に文字列として表示されます。ここまでの 1.～ 2. の手順については、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener noreferrer" target="_blank">Forge API を利用するアプリの登録とキーの取得</a></strong> で詳しく説明しています。</li>
<li>開発者は 2. で取得した Client ID と Client Secret をアプリ内のコードに書き込み、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/08/oauth-authentication-scenario-on-forge.html" rel="noopener noreferrer" target="_blank">OAuth シナリオ</a></strong> に合わせた <strong><a href="https://forge.autodesk.com/en/docs/oauth/v2/developers_guide/overview/" rel="noopener noreferrer" target="_blank">Forge Authentication API </a></strong>の&#0160;endpoint 呼び出しをおこないます。</li>
<li>Forge サーバーから Access Token を含む <strong><a href="https://ja.wikipedia.org/wiki/JavaScript_Object_Notation" rel="noopener noreferrer" target="_blank">JSON</a></strong>&#0160;レスポンスが返されます。</li>
</ol>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39e16c6200d-pi" style="display: inline;"><img alt="Get_access_token" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39e16c6200d image-full img-responsive" src="/assets/image_911508.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Get_access_token" /></a></p>
<p>先に触れたブログ記事 <strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html" rel="noopener noreferrer" target="_blank">Web ブラウザのデベロッパーツールについて </a></strong>のとおり、セキュリティ上の理由から、クライアント（Web ブラウザ）の JavaScript コードから Authentication API の endpoint を呼び出すと、<strong><a href="https://developer.mozilla.org/ja/docs/Web/HTTP/HTTP_access_control" rel="noopener noreferrer" target="_blank">CORS</a></strong>&#0160;エラーが発生して Access Token を取得することは出来ません（Access-Control-Allow-Origin: * などを返さない） 。このため、Authentication API&#0160; はサーバー実装でおこなうことになります。</p>
<p>Authentication API によって返されるのは、Access Token を含む JSON です。</p>
<pre>{
    &quot;access_token&quot;: &quot;eyJhbGciOiJIUzI1NiIsImtpZCI6Imp3dF9zeW1tZXRyaWNfa2V5In0.eyJjbGllbnRfaWQiOiJ1MzZUa2lhQVpmUnRJa2p0S2ZzR0FtdXo2NEJNenZmdyIsImV4cCI6MTU0MTEzNDg0MSwic2NvcGUiOlsiYnVja2V0OmNyZWF0ZSIsImJ1Y2tldDpyZWFkIiwiZGF0YTpyZWFkIiwiZGF0YTp3cml0ZSIsImRhdGE6Y3JlYXRlIl0sImF1ZCI6Imh0dHBzOi8vYXV0b2Rlc2suY29tL2F1ZC9qd3RleHA2MCIsImp0aSI6InBvSkhpVlBMeFp6NG1wQkxSbG5raUpiQWpHN1BEeXpBa1M4ZElBZVV1TDZSdkcyUUprUTZTQ0JJOW9rSVJNMk8ifQ.Fr4lUU21AfbDcIRBvMWlOXtxd0zjd9E7Tj2V6w6pp8U&quot;,
    &quot;token_type&quot;: &quot;Bearer&quot;,
    &quot;expires_in&quot;: 3599
}</pre>
<p>実際の Access Token となる文字列は JSON 内に含まれる &quot;access_token&quot; キーの値として格納されています。また、&quot;token_type&quot; は、Forge で扱う Token のタイプである Bearer（ベアラ）を示しています。なお、Forge で利用する Access Token には、有効期限が設定されています。有効期限を示すのが &quot;expires_in&quot; キーの値として格納されている秒数を表す数字です。</p>
<p>先の JSON 例では、Accesss Token が&#0160;eyJhbGciOiJIUzI1NiIsImtpZCI6Imp3dF9zeW1tZXRyaWNfa2V5In0.eyJjbGllbnRfaWQiOiJ1MzZUa2lhQVpmUnRJa2p0S2ZzR0FtdXo2NEJNenZmdyIsImV4cCI6MTU0MTEzNDg0MSwic2NvcGUiOlsiYnVja2V0OmNyZWF0ZSIsImJ1Y2tldDpyZWFkIiwiZGF0YTpyZWFkIiwiZGF0YTp3cml0ZSIsImRhdGE6Y3JlYXRlIl0sImF1ZCI6Imh0dHBzOi8vYXV0b2Rlc2suY29tL2F1ZC9qd3RleHA2MCIsImp0aSI6InBvSkhpVlBMeFp6NG1wQkxSbG5raUpiQWpHN1BEeXpBa1M4ZElBZVV1TDZSdkcyUUprUTZTQ0JJOW9rSVJNMk8ifQ.Fr4lUU21AfbDcIRBvMWlOXtxd0zjd9E7Tj2V6w6pp8U、有効期限が 3599 秒（約 60 分）ということになります。</p>
<p>取得した Access Token は、Forge の RESTful API を呼び出す際に、Header（ヘッダー）に指定して endpoint を呼び出すことで、リソースへのアクセス権を行使することが出来ます。もし、この Access Token の有効期限を過ぎてしまった際には、endpoint の呼び出しに失敗することになります。その場合には、再度 Access Token を取得しなおす必要があります（3-legged の場合は RefreshToken リクエストで Access Token を更新します）。</p>
<p>次のアニメーションは、2-legged OAuth を使って Bucket を作成、デザイン ファイルを当該 Bucket にアップロード後に変換し、Viewer に表示する過程を Access Token を明記したイメージです。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad377f241200c-pi" rel="noopener noreferrer" style="display: inline;" target="_blank"><img alt="Use_of_access_token" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad377f241200c image-full img-responsive" src="/assets/image_369241.jpg" title="Use_of_access_token" /></a><br /><br />余談となりますが、オートデスクは Access Token のタイプを過去に 1 度変更しています。詳細は <strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/02/about-changing-type-of-access-token.html" rel="noopener noreferrer" target="_blank">Access Token タイプの変更について</a></strong> の記事に譲りますが、Access Token の長さが最大 500 桁に及ぶ場合がある点に注意してください。Forge 登場後、しばらくの間、View and Data API から引き継いだ&#0160;<strong>Reference Token タイプ</strong>を採用していましたが、この頃、Access Token は比較的短い文字列として扱うことが出来ました。現在の&#0160;<a href="https://en.wikipedia.org/wiki/JSON_Web_Token" rel="noopener noreferrer" target="_blank">&#0160;<strong>JSON Web Token</strong></a>&#0160;<strong>(JWT)</strong>&#0160;<strong>タイプ</strong>では Access Token が可変長となるため、変数等に格納してプログラムで利用する際には、文字列領域の確保に配慮する必要があります。</p>
<p>Accesss Token については、もう 1 つ、Forge 登場後に変更を受けた点にがあります。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/10/to-developers-who-use-access-token-without-scope.html" rel="noopener noreferrer" target="_blank">Scope 指定なしで Access Token を利用している方へ</a></strong> でもご案内していますが、Access Token 発行時にアクセスレベルを指定する&#0160; <strong>Scope</strong> です。Scope は、Forge を利用するアプリに対し、Forge サーバー（クラウド）側のリソース アクセスに必要な権限を与えるか否かを指定するもので、Authentication API&#0160; 呼び出し時に次の文字列を使って指定します。十分な Scope を指定せずに Access Token を取得した場合、 その Access Token を指定した endpoint 呼び出しに失敗する可能性があります。</p>
<p>例えば、上図の例で Bucket を新規に作成してデザイン ファイルをアップロードするシナリオをご案内しました。この場面で、Bucket を作成するための&#0160;<strong><a href="https://forge.autodesk.com/en/docs/data/v2/reference/https/buckets-POST/" rel="noopener noreferrer" target="_blank">POST buckets</a></strong> endpointの呼び出す時に、bucket:create を Scope 指定せずに取得した Access Token を与えてしまうと、401 UNAUTHORIZED のエラーコードとともに呼び出しに失敗することになります。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad39d66b0200d-pi" style="display: inline;"><img alt="Scope" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad39d66b0200d image-full img-responsive" src="/assets/image_569469.jpg" title="Scope" /></a></p>
<p>各 endpoint の API リファレンスには、当該 endpoint 呼び出しに必要な Scope が記載されているので、あらかじめ必要となる Scope 値を把握することが出来るはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bd087f200b-pi" style="display: inline;"><img alt="Required_scope" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bd087f200b image-full img-responsive" src="/assets/image_832547.jpg" title="Required_scope" /></a></p>
<p>なお、取得した Access Token で異なる endpoint を呼び出す必要がある場合には、Scope 文字列を半角スペースで区切って、複数の Scope 文字列を指定することも可能です（例：&quot;bucket:create bucket:read data:read data:write data:create&quot;）。</p>
<ul style="list-style-type: circle;">
<li>Scope には Bucket を削除する bucket:delete が定義されていますが、実際の Bucket を削除する endpoint は非公開になっていて利用することは出来ません。</li>
</ul>
<p>逆に、不要な Scope を指定してしまうと、取得した Access Token を悪用される危険性がある点にご注意ください。クライアント（Web ブラウザ）上で Forge Viewer を使って 3D モデルや 2D 図面を表示するだけなら、viewables:read の Scope で十分なはずです。この場面で不要な data:write を指定してしまと、Access Token が悪意のあるマルウェアなどに読み取られた場合、クラウド上のストレージにアクセスされてデータを書き換えられたり、削除されたりしてしまう可能性があります。&#0160;</p>
<p>By Toshiaki Isezaki&#0160;</p>
