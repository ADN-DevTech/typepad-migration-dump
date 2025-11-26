---
layout: "post"
title: "Forge での OAuth 認証シナリオ"
date: "2016-08-22 01:10:08"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/08/oauth-authentication-scenario-on-forge.html "
typepad_basename: "oauth-authentication-scenario-on-forge"
typepad_status: "Publish"
---

<p>インターネットの世界では、特定のメンバーしかアクセス出来ないようなや Web サイトやサービスを提供する際に、しばしば、ユーザ名とパスワードによって、 Web サイトにアクセスしようとする人が本人か否かをチェックする&#0160;<strong><a href="https://ja.wikipedia.org/wiki/%E8%AA%8D%E8%A8%BC" rel="noopener" target="_blank">認証</a>（Authentication）</strong>&#0160;をおこないます。一方、認証が完了してサイトに入った後には、その人がどのデータにアクセス出来るか、適切に &#0160;<strong><a href="https://ja.wikipedia.org/wiki/%E8%AA%8D%E5%8F%AF_(%E3%82%BB%E3%82%AD%E3%83%A5%E3%83%AA%E3%83%86%E3%82%A3)" rel="noopener" target="_blank">認可</a>（Authorization）</strong> されてデータにアクセスすることになります。</p>
<p>API を使用して特定のデータにアクセスする場合には、その API を使用したアプリ（またはサービス）の認証をしたり、そのサービスやアプリがアクセスできるデータを許可する必要があります。場合によっては、第三者のみにアクセスが許可されたデータにアクセスしなければならない場合もあります。Forge Platform API では、これらを円滑に処理するため、オープン スタンダードとなっている <a href="https://ja.wikipedia.org/wiki/OAuth" title="OAuth 2.0"><strong>OAuth&#0160;2.0</strong></a> をサポートすることで、認証と認可を得る仕組みを提供しています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb092a7880970d-pi" style="float: left;"><img alt="Icon - oAuthen" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb092a7880970d img-responsive" src="/assets/image_545978.jpg" style="margin: 0px 5px 5px 0px;" title="Icon - oAuthen" /></a>OAuth を使用する&#0160;Forge Platform の&#0160;<a href="https://developer.autodesk.com/en/docs/oauth/v2/overview/" rel="noopener" target="_blank"><strong>OAuth API</strong> </a>では、API を利用するアプリに対して、2-ledgged 認証と 3-legged 認証の両者をサポートしています。いずれの場合でも、事前にデベロッパ ポータル（<a href="https://developer.autodesk.com/" rel="noopener" target="_blank">https://developer.autodesk.com/</a>）でアプリを登録して、Consumer Key と Consumer Secret を取得しておく必要があります。</p>
<p>Forge ポータルでのアプリ登録の手順は、ブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener" target="_blank">Forge API を利用するアプリの登録とキーの取得</a></strong>&#0160;でご紹介しています。なお、Consumer Key と Consumer Secret は、それぞれ、Client ID と Client Secret と表記される場合があります。</p>
<p>2-legged 認証と 3-legged 認証の違いと目的は、次のとおりです。</p>
<p><a name="_2-legged"></a><strong>2-legged 認証</strong></p>
<p style="padding-left: 30px;">&quot;app-context&quot; とも呼ばれる認証で、Forge API を利用するアプリが、直接、オートデスクのサーバーから Access Token（アクセス トークン） を取得して、OSS（Object Storage Service） のAPI 共有領域に作成した <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/07/summary-about-bucket.html" rel="noopener" target="_blank">Bucket</a></strong>&#0160;にアクセスする際に使用します。&#0160;旧 View and Data API で利用していた認証は、この方法によるものです。</p>
<p style="padding-left: 30px;">2-legged と呼ばれるのは、サービス プロバイダー（オートデスク）とユーザー（Forge API を利用する開発者）の 2 者が登場するためです。</p>
<p><a name="_3-legged"></a><strong>3-legged 認証</strong></p>
<p style="padding-left: 30px;">&quot;user-context&quot; とも呼ばれる認証で、第三者がアクセス権限を持つデータやサービスに、明示的な Web インタフェースを介してアクセスする権限を付与してもらう仕組みを実装することが出来ます。</p>
<p style="padding-left: 30px;">例えば、<strong><a href="https://a360.autodesk.com" rel="noopener" target="_blank">A360 クラウド サービス</a></strong>&#0160;では、通常、エンドユーザーは、そのユーザーだけがアクセスを許可されたストレージ領域を持つことが出来ます。他のユーザーはそのストレージ領域にはアクセス出来ません。もし、アプリが A360 ユーザーのストレージ領域にあるデータにアクセスする場合には、A360 のサインイン画面を表示させてエンドユーザーにユーザー名（Autodesk ID）とパスワードを入力してもらい、アクセス権限を付与してもらう必要があります。</p>
<p style="padding-left: 30px;">このように、3-legged 認証では、サービス プロバイダー（オートデスク）とユーザー（Forge API を利用する開発者）の他に、エンドユーザーが登場します。これが、3-legged と呼ばれる所以です。</p>
<p><strong>2-legged 認証のワークフロー詳細</strong></p>
<p style="padding-left: 30px;">前述のとおり、&#0160;2-legged 認証では、アプリがオートデスクが用意する OSS（Object Storage Service） API &#0160;を使って、一意な名前の Bucket を作成し、データ変換や Viewer への表示をおこないます。</p>
<p style="padding-left: 30px;">開発者が最初におこなわなければならないのは、Forge ポータルでアプリ登録して Consumer Key と Consumer Secret を取得することです。</p>
<p style="padding-left: 60px;">※ Consumer Key と Consumer Secretは、ユーザー名とパスワードに相当するものになるので、他人に知られたりしないように注意しなければなりません。第三者に流出してしまった場合には、最悪、アプリが使用する Bucket にアクセスされる可能性があります。</p>
<p style="padding-left: 30px;">Forge API を利用するアプリが OSS に Bucket を作成するには、Consumer Key と Consumer Secret を用いて Access Token（アクセス トークン） を生成します。Access Token は通行手形のようなもので、API 呼び出し時に正当な Access Token を示せないと、呼び出しに失敗します。実際に は、各種 RESTful API を呼び出す際に、HTTP ヘッダーに Access Token を指定することになります。</p>
<p style="padding-left: 30px;">Access Token には、有効期限があり、<strong>authenticate リクエスト</strong>（<a href="https://developer.autodesk.com/en/docs/oauth/v2/reference/http/authenticate-POST/" rel="noopener" target="_blank">https://developer.autodesk.com/en/docs/oauth/v2/reference/http/authenticate-POST/</a>）&#0160;で返される JSON レスポンスの&#0160;expires_in 値（秒単位）で示されます。Access Token を取得して、<strong>expires_in</strong> にある秒数が経過すると、Access Token が無効になります。この状態で Access Token の指定が必要な RESTful API を呼び出すと 401&#0160;Unauthorized エラーとなってアクセスに失敗します。</p>
<p style="padding-left: 30px;">2-legged 認証をおこなうアプリで Access Token が失効した場合には、単純に、再度、&#0160;2-legged 認証をおこなって Access Token を生成し直します。その後の&#0160; RESTful API を呼び出しには、新しい Access &#0160;Token を指定します。</p>
<p style="padding-left: 30px;">2-legged 認証時に意識しなければならない点に <strong><a href="https://developer.autodesk.com/en/docs/oauth/v2/overview/scopes/" rel="noopener" target="_blank">Scope</a></strong> の指定があります。Scope はアクセス可能な範囲を指定するもので、1 行で指定する必要があります。例えば、Bucket 作成には bucket:create、作成した Bucket へのアクセスに bucket:read、Bucket へのファイルの ップロードと変換に data:write、変換されたドキュメントの呼び出しに&#0160;data:read を指定するには、</p>
<p style="padding-left: 30px;">&#39;bucket:create bucket:read data:read data:write&#39;</p>
<p style="padding-left: 30px;">のように半角スペースで区切って指定してください。&#0160;</p>
<p style="padding-left: 60px;">※ 旧 View and Data API 利用時には OSS API v1 （バージョン 1）を使用しており、Scope の指定は不要でした。2016 年6月に&#0160;View and Data API が OAuth API、Model Derivative API 等に&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/06/about-changes-of-forge-platform-api.html" rel="noopener" target="_blank">分離</a></strong>&#0160;された時点で、OSS API v2 に変更され、同時に Scope 指定が導入されています。</p>
<p><strong>3-legged 認証のワークフロー詳細</strong></p>
<p style="padding-left: 30px;">3-legged 認証でも、Consumer Key と Consumer Secret から生成される Access Token が必要です。また、Access Token に有効期限がある点と、Scope でアクセス範囲を指定しなければならない点も同様です。2-legged 認証との違いは、アクセスするデータが OSS のAPI 共有領域ではなく、第三者であるエンド ユーザーだけがアクセス出来る外部のストレージ領域に格納されている点です。</p>
<p style="padding-left: 30px;">3-legged 認証時には、エンドユーザーに代わってデータが保存されているクラウド ストレージへのアクセスするため、その同意を得る必要があります。まず、クラウド ストレージにアクセスする認証画面を表示させてサインインを促します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d211449d970c-pi" style="display: inline;"><img alt="Sign_in_username" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d211449d970c img-responsive" src="/assets/image_621191.jpg" title="Sign_in_username" /></a></p>
<p style="padding-left: 30px;">サインインが完了すると、指定した Scope に沿ったアクセス権限をアプリに付与するか、同意を求める画面が表示されます。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d211a9eb970c-pi" style="display: inline;"><img alt="Authorize_application" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d211a9eb970c img-responsive" src="/assets/image_337180.jpg" title="Authorize_application" /></a></p>
<p style="padding-left: 30px;">エンドユーザーの同意が得られると、デベロッパ ポータルでアプリ登録した際に指定した Callback URL にリダイレクトされて、Access Token の生成処理に入ります。</p>
<p style="padding-left: 30px;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d211aa91970c-pi" style="display: inline;"><img alt="Registrated_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d211aa91970c image-full img-responsive" src="/assets/image_520174.jpg" title="Registrated_app" /></a><br />前述のとおり、3-legged 認証で生成した Access Token にも有効期限があります。2-legged 認証 とは異なり、新規に Access Token を生成する際には、エンドユーザーのサインインが再度必要になってしまいます。その手順も含めて、有効期限が切れた場合に選択可能な処理には、次の 2 つが考えられます。</p>
<ol>
<li>有効期限が切れた旨を表示し、エンドユーザーに再度クラウドサービスへのサインインを促す。</li>
<li>初回に<strong> gettoken リクエスト</strong>（<a href="https://developer.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/" rel="noopener" target="_blank">https://developer.autodesk.com/en/docs/oauth/v2/reference/http/gettoken-POST/</a>）で Access Token を取得した際、同時に JSON レスポンスから得られる <strong>refresh_token</strong> をサーバー側に保持しておく。有効期限が切れた際には、保持していた <strong>refresh_token</strong> 値を利用して、Access Token をリフレッシュ（更新）する。Access Token のリフレッシュには、<strong>refreshtoken リクエスト</strong>（<a href="https://developer.autodesk.com/en/docs/oauth/v2/reference/http/refreshtoken-POST/" rel="noopener" target="_blank">https://developer.autodesk.com/en/docs/oauth/v2/reference/http/refreshtoken-POST/</a>）を使用します。この場合、エンドユーザーへのサインイン処理を抑止することが出来ます。なお、refresh_token をサーバー側に保持する理由は、純粋にセキュリティ保護のためです。</li>
</ol>
<p style="padding-left: 30px;">どちらを選択するかは、アプリを開発する開発者の判断に依存することになります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c89c789e970b-pi" style="display: inline;"><img alt="3_legged_authentication_steps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c89c789e970b image-full img-responsive" src="/assets/image_339490.jpg" title="3_legged_authentication_steps" /></a></p>
<p>認証プロセスの詳細は、<a href="https://developer.autodesk.com/en/docs/oauth/v2/overview/basics/" rel="noopener" target="_blank">https://developer.autodesk.com/en/docs/oauth/v2/overview/basics/</a> でご確認いただけます。&#0160;</p>
<p>3-legged 認証で Access Token をリフレッシュするサンプルは、<a href="https://forge.autodesk.io/" rel="noopener" target="_blank">https://forge.autodesk.io/</a>&#0160;で、ソースコードは、<a href="https://github.com/leefsmp/forge/" rel="noopener" target="_blank">こちらの Github リポジトリ</a>でご確認いただけます。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
