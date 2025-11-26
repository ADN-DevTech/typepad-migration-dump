---
layout: "post"
title: "A360 View and Data サービス API 利用の手引き"
date: "2014-10-03 01:06:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/10/a360-view-data-service-api-startup-guide.html "
typepad_basename: "a360-view-data-service-api-startup-guide"
typepad_status: "Publish"
---

<p>今日は、View and Data サービス API を利用するための手順を、「クイック スタート ドキュメント」としてご紹介したいと思います。オリジナルの英語ドキュメントは、<strong><a href="http://developer.autodesk.com" target="_blank">Autodesk Web Services API&#0160;ポータル</a>&#0160;</strong>から参照することが出来ます。</p>
<p><strong>概要</strong></p>
<p>Autodesk View and Data API は、次の 2 つのコンポーネントから構成されています。</p>
<ul>
<li>認証とデータ アップロードに利用する REST API</li>
<li>Web ブラウザ ベースのアプリケーションに埋め込んで利用する JavaScript クライアント API</li>
</ul>
<p>このガイドは、上記 API を使って、アカウントの作成から ブラウザでモデルを表示するまでをカバーします。</p>
<p>ワークフローはシンプルです。</p>
<ul>
<li>アカウントを登録してアプリケーションを作成する。</li>
<li>認証 API を使ってアクセス トークンを取得する。アクセス トークンは、他のすべての API 呼び出しで必要になります。</li>
<li>オブジェクト（3D モデル/2D 図面）を保存するやめのバケット（バケツ）を作成する。</li>
<li>バケットにオブジェクトをアップロードして、ビューイング サービスでオブジェクトを表示できるようにトランスレーションをリクエストする。</li>
<li>この段階で、ビューワー クライアントに渡すオブジェクトの URN（Uniform Resource Name）を得ることが出来ます。</li>
</ul>
<div id="overview">
<p><strong>注意：</strong>ドキュメントとこのガイドに加えて、&#0160;GitHub リポジトリにある View and Data API サンプルを参照することが出来ます。&gt;&gt;&#0160;&#0160;<a href="https://github.com/Developer-Autodesk/autodesk-view-and-data-api-samples">https://github.com/Developer-Autodesk/autodesk-view-and-data-api-samples</a></p>
</div>
<div id="step-1-register-and-create-an-application" style="text-align: center;">
<h2 style="text-align: left;"><strong>ステップ 1: アプリケーションの登録と作成</strong></h2>
<p style="text-align: left;">Autodesk View and Data API では、最初に <strong><a href="http://developer.autodesk.com" target="_blank">http://developer.autodesk.com</a>&#0160;</strong>へアクセスして、アカウントを作成します。これをおこなうためには、“REGISTER” をクリックして、各項目に必要な情報を入力してください。もし、既に <a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=6626" target="_blank"><strong>Autodesk ID</strong></a> をお持ちであれば、この登録は必要ありません。</p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6e9f223970b-pi" style="display: inline;"><img alt="Register" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6e9f223970b image-full img-responsive" src="/assets/image_320207.jpg" title="Register" /></a><br />
<p style="text-align: left;"><strong><a href="http://developer.autodesk.com" target="_blank">http://developer.autodesk.com</a>&#0160;</strong>の &quot;LOGIN&quot; から、登録したアカウント情報を使ってデベロッパー ポータルにログインします。</p>
<p style="text-align: left;">ログインが完了したら、画面中央の My Apps をクリックしてアプリケーションを作成します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d07400c0970c-pi" style="display: inline;"><img alt="My_apps" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d07400c0970c img-responsive" src="/assets/image_894174.jpg" title="My_apps" /></a></p>
<p style="text-align: left;">次の画面が表示されたら、&quot;Add a new app <strong>＋</strong>&quot; をクリックして、作成するアプリケーションの情報を入力していきます。&#0160;</p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6e9f385970b-pi" style="display: inline;"><img alt="Create_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6e9f385970b image-full img-responsive" src="/assets/image_552330.jpg" title="Create_app" /></a><span style="text-align: left;">&#0160;</span></div>
<div style="text-align: justify;">
<ul>
<li>”App Name” 項にアプリケーション名を、“Callback URL” 項にWebサイトの URLを入力してください。URLが未定の場合には、ダミーで結構です（http://null.net など）。</li>
<li>&quot;Product&quot; 項には、“View and Data API” を選択してください。</li>
<li>入力が完了したら [Create App] をクリックします。</li>
</ul>
<p style="text-align: left;"><strong>注意:</strong>&#0160;Callback URL は、OAuth2 の 2 段階 認証用のものです。この段階でコールバックが、まだ、セットアップされていなくても結構です。ただし、処理のための、この項目の入力は必須です（ダミーでも）。</p>
<p style="text-align: left;">アプリケーションが作成されると、Consumer Key と Consumer Secret、Callback URL が表示されます。これらの値は、次のステップで必要になるものです。</p>
&#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6e9fa28970b-pi" style="display: inline;"><img alt="Secrets" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6e9fa28970b img-responsive" src="/assets/image_621023.jpg" style="width: 420px;" title="Secrets" /></a><br />
<p style="text-align: justify;"><strong>注意：</strong>&#0160;Consumer Key と&#0160;Consumer Secret がグレーアウトされている場合には、アプリケーションが承認待ちであることを表しています。承認を得られるまで、認証と API 呼び出しを利用することは出来ません。</p>
</div>
<div id="step-2-get-your-access-token">
<h2 style="text-align: left;"><strong>ステップ 2: アクセス トークンの取得</strong></h2>
<p style="text-align: left;">このクイック スタート ガイドでは、curl（<a href="http://curl.haxx.se/">http://curl.haxx.se/</a>）を使ってすべてのリクエストをおこないます。もちろん、他の言語で記述されるクライアントでも、原則と書式は同じです。</p>
<p style="text-align: left;">アクセス トークンを取得するためには、Consumer Key（client_id パラメータ）と、Consumer Secret（ client_secret パラメータ）を認証 API に渡す必要があります（<em><a href="http://developer.api.autodesk.com/documentation/v1/authentication.html#authenticate">Create an OAuth2 token</a></em>）。</p>
<div style="text-align: left;">
<div><span style="font-family: verdana, geneva; font-size: 8pt;">curl --data &quot;client_id=obQDn8P0GanGFQha4ngKKVWcxwyvFAGE&amp;client_secret=eUruM8HRyc7BAQ1e&amp;grant_type=client_credentials&quot; \ https://developer.api.autodesk.com/authentication/v1/authenticate --header &quot;Content-Type: application/x-www-form-urlencoded&quot; -k </span></div>
</div>
<p style="text-align: left;">サーバーからのレスポンスは、JSON ボディで 200 ステータス値を持ちます。</p>
<div style="text-align: left;">
<div><span style="font-family: verdana, geneva; font-size: 8pt;">{ &quot;token_type&quot; : &quot;Bearer&quot;, &quot;expires_in&quot; : 899, &quot;access_token&quot; : &quot;GX6OONHlQ9qoVaCSmBqJvqPFUT5i&quot; } </span></div>
</div>
<p style="text-align: left;">このレスポンスは、アクセス トークンと有効期間（秒単位）が含まれています。アプリケーションは、この情報を保持して、古いアクセス トークンの期間終了後に新しいトークンを取得します。</p>
</div>
<div id="step-3-create-a-bucket" style="text-align: left;">
<h2><strong>ステップ 3: バケットの作成</strong></h2>
<p>“Buckets（バケット）” は、Autodesk View and Data API で使われるデータのためのコンテナです。ファイルをアップロードする前に、バケットを作成してデータ保持ポリシーを設定します。ポリシーには次の 3 つがあります。</p>
<ul>
<li><strong>Transient</strong>: 24 時間だけ存続するストレージ キャッシュのようなもので、一時的に利用するオブジェクトには最適です。</li>
<li><strong>Temporary</strong>: 30 日間存続するストレージです。後日必要としないデータのアップロードとアクセスに適しています。このタイプのバケット ストレージは、サービスが課金された際の節約に役立つかも知れません。</li>
<li><strong>Persistent</strong>: 削除されるまで存続するストレージです。2年間アクセスされていないアイテムは、アーカイブされることがあります。</li>
</ul>
<p>バケット名で使用する文字には、いくつかの制限があります。例えば、バケット名には小文字しか含めることが出来ません。詳細は、API ドキュメントを参照してください。</p>
<p><strong>POST /oss/{version}/buckets</strong> API を使ってバケットを作成します。（<em><a href="http://developer.api.autodesk.com/documentation/v1/oss.html#oss-buckets-api">OSS Bucket and Object API v1.0</a>&#0160;</em>を参照）。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">curl -k --header &quot;Content-Type: application/json&quot; --header &quot;Authorization: Bearer GX6OONHlQ9qoVaCSmBqJvqPFUT5i&quot; \ --data &quot;{\&quot;bucketKey\&quot;:\&quot;mybucket\&quot;,\&quot;policy\&quot;:\&quot;transient\&quot;}&quot; https://developer.api.autodesk.com/oss/v1/buckets </span></div>
</div>
<p><strong>注意：</strong>&#0160;この例では、Windows を利用しているので、JSON ペイロードがエスケープされています。他の OS では、これをおこなう必要はありません。</p>
<p>この例では、“Authorization: Bearer” ヘッダー パラメータがステップ 2 からのアクセス トークンを含んでいます。</p>
<p>バケットが作成されると、すぐに、<a href="http://developer.api.autodesk.com/documentation/v1/oss.html#oss-get-bucket-details"><em>Get Bucket Details API</em></a> を使って、その存在を確認します。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">curl -k -H &quot;Authorization: Bearer GX6OONHlQ9qoVaCSmBqJvqPFUT5i&quot; -X GET \ https://developer.api.autodesk.com/oss/v1/buckets/mybucket/details </span></div>
</div>
<p>成功すると、次のようなレスポンス ボディが返されます。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">{ &quot;key&quot; : &quot;mybucket&quot;, &quot;owner&quot; : &quot;obQDn8P0GanGFQha4ngKKVWcxwyvFAGE&quot;, &quot;createDate&quot; : 1401735235495, &quot;permissions&quot; : [{ &quot;serviceId&quot; : &quot;obQDn8P0GanGFQha4ngKKVWcxwyvFAGE&quot;, &quot;access&quot; : &quot;full&quot; } ], &quot;policyKey&quot; : &quot;transient&quot; } </span></div>
</div>
</div>
<div id="step-4-upload-a-file" style="text-align: left;">
<h2><strong>ステップ 4: ファイルのアップロード</strong></h2>
<p>次のステップでは、表示をおこなえるようにするために、バケットにファイルをアップロードします。アップロード後には、ファイルはビューイング サービスで登録されます。現在、約 60 種類のファイル形式がサポートされています。</p>
<ul>
<li>Autodesk DWG™</li>
<li>Autodesk Inventor®</li>
<li>Fusion 360™</li>
<li>SIM 360™</li>
<li>Autodesk Navisworks®</li>
<li>Autodesk Revit®</li>
<li>Autodesk 3Ds Max®</li>
<li>Solidworks®</li>
<li>CATIA®</li>
<li>Siemens Parasolid™</li>
<li>Siemens NX™</li>
<li>Siemens OpenJT™</li>
<li>WaveFront Technologies OBJ</li>
</ul>
<p>サポートされるファイル形式の一覧を取得するためには、<strong>GET /viewingservice/{version}/supported</strong> API (<a href="http://developer.api.autodesk.com/documentation/v1/viewing_service.html#viewing-service-supported-api"><em>Supported API</em></a>) を利用します。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">curl -k --header &quot;Authorization: Bearer lEaixuJ5wXby7Trk6Tb77g6Mi8IL&quot; https://developer.api.autodesk.com/viewingservice/v1/supported </span></div>
</div>
<p>This returns a response that includes an array of supported extensions for translation:</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">&quot;extensions&quot; : [&quot;ipt&quot;, &quot;neu&quot;, &quot;stla&quot;, &quot;stl&quot;, &quot;xlsx&quot;, &quot;jt&quot;, &quot;jpg&quot;, &quot;skp&quot;, &quot;prt&quot;, &quot;dwf&quot;, &quot;xls&quot;, &quot;png&quot;, &quot;sldasm&quot;, &quot;step&quot;, &quot;dwg&quot;, &quot;zip&quot;, &quot;nwc&quot;, &quot;model&quot;, &quot;sim&quot;, &quot;stp&quot;, &quot;ste&quot;, &quot;f3d&quot;, &quot;pdf&quot;, &quot;iges&quot;, &quot;dwt&quot;, &quot;catproduct&quot;, &quot;csv&quot;, &quot;igs&quot;, &quot;sldprt&quot;, &quot;cgr&quot;, &quot;lll&quot;, &quot;3dm&quot;, &quot;sab&quot;, &quot;obj&quot;, &quot;pptx&quot;, &quot;cam360&quot;, &quot;jpeg&quot;, &quot;bmp&quot;, &quot;exp&quot;, &quot;ppt&quot;, &quot;doc&quot;, &quot;wire&quot;, &quot;ige&quot;, &quot;rcp&quot;, &quot;txt&quot;, &quot;dae&quot;, &quot;x_b&quot;, &quot;3ds&quot;, &quot;rtf&quot;, &quot;rvt&quot;, &quot;g&quot;, &quot;sim360&quot;, &quot;iam&quot;, &quot;asm&quot;, &quot;dlv3&quot;, &quot;x_t&quot;, &quot;pps&quot;, &quot;session&quot;, &quot;xas&quot;, &quot;xpr&quot;, &quot;docx&quot;, &quot;catpart&quot;, &quot;stlb&quot;, &quot;tiff&quot;, &quot;nwd&quot;, &quot;sat&quot;, &quot;fbx&quot;, &quot;smb&quot;, &quot;smt&quot;, &quot;dwfx&quot;, &quot;tif&quot;], </span></div>
</div>
<p>“Viewing-*” のチャネル名を持つ拡張子は、ビューイングでサポートされるものです。</p>
<p>PUT リクエストでファイルがアップロードされます。: <strong>PUT /oss/{version}/buckets/{bucketname}/objects/{filename}</strong> API(<a href="http://developer.api.autodesk.com/documentation/v1/oss.html#oss-upload-api"><em>OSS Upload API v1.0</em></a>):</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">curl --header &quot;Authorization: Bearer K16B98iaYNElzVheldlUAUqOoMRC&quot; --header &quot;Content-Length: 308331&quot; \ -H &quot;Content-Type:application/octet-stream&quot; --header &quot;Expect:&quot; \ --upload-file &quot;skyscpr1.3ds&quot; -X PUT https://developer.api.autodesk.com/oss/v1/buckets/mybucket/objects/skyscpr1.3ds -k </span></div>
</div>
<p>コンテント長（Content-Length）、コンテント タイプと、Expect ヘッダーを空に設定することが重要です。この API は、大きなファイルのアップロード用に Expect ヘッダーをサポートします。詳細は、<a href="http://developer.autodesk.com" target="_blank">Autodesk Web Services API&#0160;ポータル</a>&#0160;のドキュメントを参照してください。For more information, see the documentation.</p>
<p>アップロードが成功すると、次のようなレスポンスが返ります。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">{ &quot;bucket-key&quot;: &quot;mybucket&quot;, &quot;objects&quot;: [ { &quot;location&quot;: &quot;https://developer.api.autodesk.com/oss/v1/buckets/mybucket/objects/skyscpr1.3ds&quot;, &quot;size&quot;: 308331, &quot;key&quot;: &quot;skyscpr1.3ds&quot;, &quot;id&quot;: &quot;urn:adsk.objects:os.object:mybucket/skyscpr1.3ds&quot;, &quot;sha-1&quot;: &quot;e84021849a9f5d1842bf792bbcbc6445c280e15b&quot;, &quot;content-type&quot;: &quot;application/octet-stream&quot; } ] } </span></div>
</div>
<p>この情報でのキーは、次のステップで必要となる&#0160;<strong>id&#0160;</strong>です。</p>
<p>もし、Inventor アセンブリ（IAM ファイル）と参照される複数パーツ ファイル（IPT ファイル群）のように、アップロードしたファイルが複数から構成される場合には、同じバケットにそれらのファイルをアップロードして、それぞれの ID を保持してください。</p>
</div>
<div id="step-5-set-up-references-between-multiple-files" style="text-align: left;">
<h2><strong>ステップ 5: 複数ファイル間の参照設定</strong></h2>
<p>複数のファイルから構成されるファイルをアップロードする場合のみ、このステップを実行する必要があります。Data and Viewing サービスでファイル群を登録する前には、References API を使って、それらファイル間の関係を指示する必要があります。</p>
<p>この API は、マスター ドキュメントとなる URN と、1 つ、または、それ以上の依存関係を持つファイルの URN を指定するボディとして、JSON ドキュメントを利用します。&#0160;POST /references/{version}/setreference API (<a href="http://developer.api.autodesk.com/documentation/v1/references.html#references-service-api"><em>References Service REST API Specification</em></a>).</p>
<p>次のようなかたちで、モデルの依存関係情報を持つ JSON ファイルを作成します。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">{ &quot;master&quot; : &quot;urn:adsk.objects:os.object:alexbicalhobucket2/A1.iam&quot;, &quot;dependencies&quot; : [ { &quot;file&quot; : &quot;urn:adsk.objects:os.object:alexbicalhobucket2/A1A1.iam&quot;, &quot;metadata&quot; : { &quot;childPath&quot; : &quot;A1A1.iam&quot;, &quot;parentPath&quot; : &quot;A1.iam&quot; } }, { &quot;file&quot; : &quot;urn:adsk.objects:os.object:alexbicalhobucket2/A1P1.ipt&quot;, &quot;metadata&quot; : { &quot;childPath&quot; : &quot;A1P1.ipt&quot;, &quot;parentPath&quot; : &quot;A1.iam&quot; } }, { &quot;file&quot; : &quot;urn:adsk.objects:os.object:alexbicalhobucket2/A1P2.ipt&quot;, &quot;metadata&quot; : { &quot;childPath&quot; : &quot;A1P2.ipt&quot;, &quot;parentPath&quot; : &quot;A1.iam&quot; } } ] } </span></div>
</div>
<p>ステップ4からマスターと依存関係の URN での URN を置き換えて、“childPath” と “parentPath” パラメータで親子関係を関連付けます。必要なら、追加の依存関係も追加してください。この例では、&#0160;A1P1.ipt &#0160;パーツと &#0160;A1P2.ipt パーツ、A1A1.iam アセンブリが、マスターファイルである A1A.iam アセンブリの子となっています。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">curl -k -H &quot;Content-Type: application/json&quot; -H &quot;Authorization:Bearer GX6OONHlQ9qoVaCSmBqJvqPFUT5i&quot; \ -i -d @references.json https://developer.api.autodesk.com/references/v1/setreference </span></div>
</div>
</div>
<div id="step-6-register-your-data-with-the-viewing-services" style="text-align: left;">
<h2><strong>ステップ 6: ビューイング サービスでデータを登録する</strong></h2>
<p>ファイルがアップロードされたら、ビューイング サービスに登録される必要があります。 POST /viewingservice/{version}/register API (<a href="http://developer.api.autodesk.com/documentation/v1/viewing_service.html#viewing-service-create-bubbles-api"><em>Post Data</em></a>) でデータを表示を準備するプロセスを起動します。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">curl -k -H &quot;Content-Type: application/json&quot; -H &quot;Authorization:Bearer GX6OONHlQ9qoVaCSmBqJvqPFUT5i&quot; \ -i -d &quot;{\&quot;urn\&quot;:\&quot;dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bXlidWNrZXQvc2t5c2NwcjEuM2Rz\&quot;}&quot; \ https://developer.api.autodesk.com/viewingservice/v1/register </span></div>
</div>
<p>この API は、アップロードしたファイルの URN を、JSON ボディに 1 つのパラメータとして用います。ただし、ここで指定している URN は、base64 でエンコードされているため、同じようには見えません。ここと次のステップでは、URN の base64 エンコード バージョンを利用します。ユーティリティや <a href="http://www.base64encode.org/">http://www.base64encode.org/</a>&#0160;のようなオンライン ツールを使って、URN を変換することが出来ます。</p>
<p><strong>注意：</strong>&#0160;base64 に変換する際には、URN にキャレッジ リターンや改行を入れないようにしてください。API は、改行コードを正しく扱うことが出来ません。</p>
<p>ここで、URN を渡して、GET /viewingservice/{version}/{urn}/status (<a href="http://developer.api.autodesk.com/documentation/v1/viewing_service.html#viewing-service-get-bubbles-api"><em>Get Viewable</em></a>) で登録の状況をチェックすることが可能です。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">curl -k -i -H &quot;Authorization: Bearer GX6OONHlQ9qoVaCSmBqJvqPFUT5i&quot; -X GET \ https://developer.api.autodesk.com/viewingservice/v1/dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bXlidWNrZXQvc2t5c2NwcjEuM2Rz/status </span></div>
</div>
<p>これによって、コンポーネント パーツと同様に、ドキュメントについての情報を表示する JSON を返します。いくつかのパーツだけが &quot;complete&quot; ステータスを持つ場合でも、登録されたデータをビューアで表示することができることに注意してください。</p>
<p>登録後には、API は GET /viewingservice/{version}/thumbnails/{urn} API (<a href="http://developer.api.autodesk.com/documentation/v1/viewing_service.html#viewing-service-get-thumbnails-api"><em>Get Thumbnails</em></a>) で、サムネイル イメージの取得が出来ます。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">curl -k -H &quot;Authorization: Bearer GX6OONHlQ9qoVaCSmBqJvqPFUT5i&quot; -X GET \ https://developer.api.autodesk.com/viewingservice/v1/thumbnails/dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bXlidWNrZXQvc2t5c2NwcjEuM2Rz &gt; C:\Users\Public\thumb.png </span></div>
</div>
<p>この例のサムネイルは、次のようなになります。</p>
<blockquote>
<div><img alt="_images/thumb.png" src="/assets/thumb.png" /></div>
</blockquote>
</div>
<div id="step-7-load-the-urn-in-the-javascript-viewer" style="text-align: left;">
<h2><strong>ステップ 7: JavaScript ビューワーでの URN のロード</strong></h2>
<p>以前のステップは、&#0160;Autodesk View and Data API（ここでは curl で説明）の REST API を使用しました。ここからは、ビューワーのクライアント JavaScript API に切り替えて説明をしていきます。</p>
<p>はじめに、&lt;head&gt; と &lt;body&gt; 要素で、新しく、空の&#0160;HTML ドキュメントを作成します。必要なスタイルとビューワーへの JavaScript を参照するために、&lt;head&gt; セクションに次のリンクを追加します。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">&lt;link rel=&quot;stylesheet&quot; href=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/style.css&quot; type=&quot;text/css&quot;&gt; &lt;script src=&quot;https://developer.api.autodesk.com/viewingservice/v1/viewers/viewer3D.min.js&quot;&gt;&lt;/script&gt; </span></div>
</div>
<p>次に、&lt;script&gt; 要素を作成して、ビューワーをセットアップするために初期化関数を追加します。</p>
<div>
<pre><span style="font-family: &#39;courier new&#39;, courier; font-size: 10pt;">function initialize() {
var options = {
&#39;document&#39; : &#39;urn:dXJuOmFkc2sub2JqZWN0czpvcy5vYmplY3Q6bXlidWNrZXQvc2t5c2NwcjEuM2Rz&#39;,
&#39;getAccessToken&#39;: getToken,
&#39;refreshToken&#39;: getToken,
};

var viewerElement = document.getElementById(&#39;viewer&#39;);
var viewer = new Autodesk.Viewing.Viewer3D(viewerElement, {});

Autodesk.Viewing.Initializer(options,function() {
   viewer.initialize();
   loadDocument(viewer, options.document);
});
}

// This method returns a valid access token  For the Quick Start we are just returning the access token
// we obtained in step 2.  In the real world, you would never do this.
function getToken() {
    return &quot;GX6OONHlQ9qoVaCSmBqJvqPFUT5i&quot;;
}</span></pre>
</div>
<p>ここでの&#0160;options.document パラメータは、先に作成した登録済の表示可能オブジェクト用の base64 URN となります。URN は、ドキュメントによって当然異なることになります。</p>
<p>getToken() 関数は、単純にアクセス トークの文字列を返しています。もちろん、実運用で、このような処理をするべきではありません。loadDocument() に渡す関数には、正当な認証トークンを提供する実装を持つことが出来ます。例えば、認証 API を呼び出す Web サービス エンドポイントを呼び出すことも出来ます。クライアントの JavaScript 側で、直接 、Client ID と Client Secret を公開するように、認証 API を呼び出すべきではありません。</p>
<p>getToken() 関数の実際の実装は、このようになるはずです。</p>
<div>
<pre><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">function getToken(){
    // This method should fetch a token from a service you create to provide authentication.
    // See the ADN Samples for examples of how to create such a service.  For example, see
    // https://github.com/Developer-Autodesk/workflow-aspnet-webform-view.and.data.api/blob/master/ViewAndShare/ViewAndShare/GetAccessToken.ashx.cs
    // This method might look something like:
    var xmlHttp = null;
    xmlHttp = new XMLHttpRequest();
    xmlHttp.open( &quot;GET&quot;, &quot;https://myservice.com/token&quot;, false );
    xmlHttp.send( null );
    var newToken= xmlHttp.responseText;
    return newToken;
}</span></pre>
</div>
<p>ロード処理と、ドキュメントがロードされた際のエラー コールバックを持つ関数を追加します。</p>
<div>
<pre><span style="font-family: &#39;courier new&#39;, courier; font-size: 8pt;">function loadDocument(viewer, documentId) {
    // Find the first 3d geometry and load that.
    Autodesk.Viewing.Document.load(documentId, function(doc) {// onLoadCallback
    var geometryItems = [];
    geometryItems = Autodesk.Viewing.Document.getSubItemsWithProperties(doc.getRootItem(), {
        &#39;type&#39; : &#39;geometry&#39;,
        &#39;role&#39; : &#39;3d&#39;
    }, true);

    if (geometryItems.length &gt; 0) {
        viewer.load(doc.getViewablePath(geometryItems[0]));
    }
 }, function(errorMsg) {// onErrorCallback
    alert(&quot;Load Error: &quot; + errorMsg);
    });
}</span></pre>
</div>
<p>最後に、&lt;body&gt; タグへ、次のような追記をします。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">&lt;body onload=&quot;initialize()&quot;&gt; &lt;div id=&quot;viewer&quot; style=&quot;position:absolute; width:90%; height:60%;&quot;&gt;&lt;/div&gt; &lt;/body&gt; </span></div>
</div>
<p>WebGL をサポートする Web ブラウザで作成した HTML ファイルを開いて、URL にアクセス トークンを次のように記入します（ローカルで実行している場合）。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">http://localhost:8080/quickstart_3d.html?accessToken=GX6OONHlQ9qoVaCSmBqJvqPFUT5i </span></div>
</div>
<p>または、シンプルにファイルを開きます。</p>
<div>
<div><span style="font-family: verdana, geneva; font-size: 8pt;">file://c:/path_to_file/quickstart_3d.html?accessToken=GX6OONHlQ9qoVaCSmBqJvqPFUT5i </span></div>
</div>
<p>Autodesk View and Data API サービスで、オブジェクトを表示できるはずです&#0160;!!</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
</div>
