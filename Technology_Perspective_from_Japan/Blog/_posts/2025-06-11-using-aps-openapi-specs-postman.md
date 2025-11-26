---
layout: "post"
title: "APS OpenAPI 仕様を Postman で利用"
date: "2025-06-11 00:29:26"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/06/using-aps-openapi-specs-postman.html "
typepad_basename: "using-aps-openapi-specs-postman"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861026400200d-pi" style="display: inline;"><img alt="Postman2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861026400200d img-responsive" src="/assets/image_491676.jpg" title="Postman2" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p data-line="2" dir="auto"><a href="https://adndevblog.typepad.com/technology_perspective/2025/05/openapi-specs-are-here.html" rel="noopener" target="_blank">OpenAPI 仕様をオープンソース化</a> により、開発者が API を探索して統合することがこれまで以上に簡単になっています。今回は、これらの仕様を活用するための実用的な方法、特に <a  _istranslated="1" data-href="https://www.postman.com" href="https://www.postman.com/" rel="noopener" target="_blank">Postman</a> でそれらを使用する方法をフォローアップしてみたいと思います。</p>
<p data-line="4" dir="auto">過去に Postmanで REST API のテストした経験をお持ち場合には、エンドポイント毎に Postman コレクションを作成する手作業が必要であることをご存じと思います。ただし、API が進化して連続して複数のエンドポイントを使するようになると、コレクションの作成に時間がかかり、エラーを招きやすくなる傾向があります。</p>
<p data-line="8" dir="auto">OpenAPI 仕様が公開されるようになったことで、そのような苦労はもう必要ありません。Postman に直接インポートして、（ほぼ)）即座に使用できるリクエストの完全なコレクションを即座に生成できます。エンドポイントのテスト、ワークフローの構築、利用可能な運用の探索など、Postmanはプラットフォームの機能へのワンクリック ゲートウェイになります。</p>
<hr />
<p data-line="8" dir="auto"><strong>利用方法（<a  _istranslated="1" data-href="https://www.postman.com" href="https://www.postman.com/" rel="noopener" target="_blank">Postman</a> がインストールされている前提）</strong></p>
<ol data-line="14" dir="auto">
<li data-line="14">Postmanを開き、[Collections] セクションに移動して、上部にある&#0160;[Import] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e86102086c200d-pi" style="display: inline;"><img alt="Import_1" class="asset  asset-image at-xid-6a0167607c2431970b02e86102086c200d img-responsive" src="/assets/image_689332.jpg" title="Import_1" /></a></li>
<li data-line="15">インポートする仕様ファイルを、次のいずれかのオプションを使用して指定します。</li>
</ol>
<ul data-line="16" dir="auto">
<li data-line="16"><a href="https://github.com/autodesk-platform-services/aps-sdk-openapi" rel="noopener" target="_blank">リポジトリ</a>から仕様ファイルの URL（<a  _istranslated="1"  _mstmutation="1" data-href="https://raw.githubusercontent.com/autodesk-platform-services/aps-sdk-openapi/refs/heads/main/oss/oss.yaml" href="https://raw.githubusercontent.com/autodesk-platform-services/aps-sdk-openapi/refs/heads/main/oss/oss.yaml">https://raw.githubusercontent.com/autodesk-platform-services/aps-sdk-openapi/refs/heads/main/oss/oss.yaml</a> など）をコピーして貼り付けます。</li>
<li data-line="17">仕様ファイルの URL（.yaml コンテンツの URL）をコピーして貼り付けます。</li>
<li data-line="18">(詳細) Postman を GitHub アカウントにリンクし、リポジトリの 1 つから 仕様ファイル URL を選択します。</li>
</ul>
<ol data-line="19" dir="auto" start="3">
<li data-line="19">[Choose how to import your Specification] ダイアログで [Postman Collection] を選択します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861020878200d-pi" style="display: inline;"><img alt="Import_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861020878200d img-responsive" src="/assets/image_589545.jpg" title="Import_2" /></a><br /><br /></li>
<li data-line="20">Postman コレクションを作成する前に、左下に表示されている ⚙ View Import Settings に移動して次の設定になっていることを確認します（推奨）。</li>
</ol>
<ul data-line="21" dir="auto">
<li data-line="21">Parameter generation:&#0160;<strong>Schema</strong></li>
<li data-line="22">Folder organization:&#0160;<strong>Tags</strong></li>
<li data-line="23">Enable optional parameters:&#0160;<strong>false（オフ）</strong></li>
<li data-line="24">Always inherit authentication:&#0160;<strong>true（オン）<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e86102090c200d-pi" style="display: inline;"><img alt="Import_3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e86102090c200d image-full img-responsive" src="/assets/image_615677.jpg" title="Import_3" /></a><br /></strong></li>
</ul>
<ol data-line="25" dir="auto" start="5">
<li data-line="25">[Import] ボタンをクリックしインポートします。<br /><iframe allowfullscreen="allowfullscreen" data-gtm-yt-inspected-1081073_237="true" data-gtm-yt-inspected-30="true" data-wat-video-found="true" frameborder="0" height="470px" id="waf_detected_youtube0" src="https://www.youtube.com/embed/KnTWh3FeDNo?autoplay=0&amp;start=0&amp;rel=0&amp;enablejsapi=1&amp;origin=https://aps.autodesk.com" title="Import OpenAPI Specs to Postman" width="854"></iframe></li>
</ol>
<hr />
<p><strong>認証・認可</strong></p>
<p data-line="31" dir="auto" style="padding-left: 40px;">本ブログ記事執筆時点で、OpenAPI 仕様のインポートが自動設定されない点は、認証/認可のフローです。Postman リクエストの認証を設定するにはさまざまな方法がありますが、最も汎用的なのはコレクション内のリクエストの使用方法に応じて、<a  _istranslated="1" data-href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-2-legged-token/" rel="noopener" target="_blank">2-legged</a>&#0160;または&#0160;<a  _istranslated="1" data-href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" href="https://aps.autodesk.com/en/docs/oauth/v2/tutorials/get-3-legged-token/" rel="noopener" target="_blank">3-legged</a> OAuth 認証フローを使用した Postman コレクションの設定です。</p>
<ul>
<li data-line="31">参考：<a href="https://adndevblog.typepad.com/technology_perspective/2024/12/aps-access-2-legged-and-3-legged.html" rel="noopener" target="_blank">APS アクセス：2-legged と 3-legged</a></li>
</ul>
<hr />
<p data-line="35" dir="auto"><strong>Environment（環境）</strong></p>
<p data-line="35" dir="auto" style="padding-left: 40px;">APS の Client ID や Client Secret のクレデンシャル情報を Postman の Environment - 環境 に保存すると、Postman の標準的な使用でユーザー インターフェイスに値が公開（表示）されることはなく、また、同じリクエストを異なる環境（異なる APS アプリなど）で簡単に再利用することが出来るようになります。新しい環境で Client ID と Client Secret を指定する手順は次のとおりです。</p>
<ol>
<li data-line="37">Postman を開き、[Environments] セクションに移動して、[Create Environment] リンクをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d46c83200c-pi" style="display: inline;"><img alt="Environment_1" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d46c83200c img-responsive" src="/assets/image_619682.jpg" style="width: 600px;" title="Environment_1" /></a></li>
<li data-line="38">新しい環境に名前を指定します。（ここでは New Environment）</li>
<li data-line="39">新しい環境変数&#0160;APS_CLIENT_ID を作成して&#0160;Type&#0160;を&#0160;secret に設定後、&#0160;Initial value に Client ID を指定します。</li>
<li data-line="40">同様に、新しい環境変数&#0160;APS_CLIENT_SECRET を作成、Type&#0160;を&#0160;secret に設定し、Initial value に Client Secret を指定します。</li>
<li data-line="41">右上隅にある&#0160;[保存] ボタンを使用して環境を保存します。</li>
<li><a id="activate_env"></a>New Environment を選択してアクティブ（チェックされた状態）にします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861020baa200d-pi" style="display: inline;"><img alt="Environment_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861020baa200d image-full img-responsive" src="/assets/image_319257.jpg" title="Environment_2" /></a><br /><iframe allowfullscreen="allowfullscreen" data-wat-video-found="true" frameborder="0" height="470px" id="waf_detected_youtube1" src="https://www.youtube.com/embed/f7hd1ZAeGKQ?autoplay=0&amp;start=0&amp;rel=0&amp;enablejsapi=1&amp;origin=https://aps.autodesk.com" title="Setup Postman Environment" width="854"></iframe></li>
</ol>
<hr />
<p data-line="48" dir="auto"><strong>2-legged Auth（2-legged 認証フロー）</strong></p>
<p data-line="48" dir="auto" style="padding-left: 40px;">Postman コレクションに 2-legged 認証フローを追加する手順は、次のとおりです。</p>
<ol data-line="50" dir="auto">
<li data-line="50">Postman で [Collections] セクションに移動して使用するコレクションを選択します。</li>
<li data-line="51">コレクションの詳細ページで [Authorization] タブに切り替えます。</li>
<li data-line="52">[Auth Type] を&#0160;[OAuth 2.0] に設定し、[Add auth data to] が [Request Headers] に設定されていることを確認します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d46e0e200c-pi" style="display: inline;"><img alt="2-legged_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d46e0e200c img-responsive" src="/assets/image_909255.jpg" title="2-legged_1" /></a></li>
<li data-line="52"><strong>Configure New Token</strong> セクションで、トークンに名前（ここでは <strong>APS 2LO</strong>）を指定して、次の入力を設定します。</li>
</ol>
<ul data-line="54" dir="auto">
<li data-line="54">Grant Type:&#0160;<strong>Client Credentials</strong></li>
<li data-line="55">Access Token URL:&#0160;<strong><code>{{baseUrl}}/authentication/v2/token</code></strong></li>
<li data-line="56">Client ID:&#0160;<strong><code>{{APS_CLIENT_ID}}</code></strong></li>
<li data-line="57">Client Secret:&#0160;<strong><code>{{APS_CLIENT_SECRET}}</code></strong></li>
<li data-line="58">Scope:&#0160;<strong><code>bucket:read data:read</code></strong></li>
<li data-line="59">Client Authentication:&#0160;<strong>Send as Basic Auth Header</strong></li>
</ul>
<p style="padding-left: 40px;"><strong> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861020b4d200d-pi" style="display: inline;"><img alt="2-legged_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861020b4d200d img-responsive" src="/assets/image_197016.jpg" title="2-legged_2" /></a><br /></strong></p>
<ol data-line="60" dir="auto" start="5">
<li data-line="60">一番下にある [Get New Access Token] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d46eb0200c-pi" style="display: inline;"><img alt="2-legged_3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d46eb0200c img-responsive" src="/assets/image_89200.jpg" title="2-legged_3" /></a></li>
<li data-line="60">[MANAGE ACCESS TOKENS] ダイアログで [Use Token] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d46eb9200c-pi" style="display: inline;"><img alt="2-legged_4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d46eb9200c img-responsive" src="/assets/image_259694.jpg" title="2-legged_4" /></a></li>
<li data-line="60">コレクションの詳細ページに戻り、<strong>Auto-refresh Token</strong>&#0160;オプションが有効になっていることを確認します。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d46ec9200c-pi" style="display: inline;"><img alt="2-legged_5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d46ec9200c image-full img-responsive" src="/assets/image_997518.jpg" title="2-legged_5" /></a></li>
<li data-line="63">コレクションを保存します。</li>
</ol>
<p data-line="65" dir="auto" style="padding-left: 40px;">注: <code>{{baseUrl}}</code><code> </code>で指定されている変数は、インポートした OpenAPI 仕様のコレクションに対して定義されていて、Variables タブで確認することが出来ます。既定値は、APS エンドポイントのベース URLである <code>https://developer.api.autodesk.com</code> になっているはずです。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d46edd200c-pi" style="display: inline;"><img alt="2-legged_6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d46edd200c img-responsive" src="/assets/image_994210.jpg" title="2-legged_6" /></a></p>
<p data-line="65" dir="auto" style="padding-left: 40px;"><iframe allowfullscreen="allowfullscreen" data-wat-video-found="true" frameborder="0" height="470px" id="waf_detected_youtube2" src="https://www.youtube.com/embed/F2XRjme7BuE?autoplay=0&amp;start=0&amp;rel=0&amp;enablejsapi=1&amp;origin=https://aps.autodesk.com" title="Setup 2-Legged OAuth in Postman" width="854"></iframe></p>
<hr />
<p data-line="71" dir="auto"><strong>3-Legged Auth（3-legged 認証フロー）</strong></p>
<p data-line="71" dir="auto" style="padding-left: 40px;">Postman コレクションに 3-legged 認証フローを追加する手順は、次のとおりです。</p>
<ol data-line="73" dir="auto">
<li data-line="73">Postman で [Collections] セクションに移動して使用するコレクションを選択します。</li>
<li data-line="74">コレクションの詳細ページで [Authorize] タブに切り替えます。</li>
<li data-line="74">[Auth Type] を&#0160;[OAuth 2.0] に設定し、[Add auth data to] が [Request Headers] に設定されていることを確認します。</li>
<li>Configure New Token セクションで、トークンに名前（ここでは APS 3LO）を指定して、次の入力を設定します。</li>
</ol>
<ul data-line="77" dir="auto">
<li data-line="77">Grant Type:&#0160;<strong>Authorization Code<br /></strong><strong>Authorize using browser</strong> にチェックすると&#0160;<strong>Callback URL</strong> フィールドにコールバック URL がグレー表示されるのでメモして 5. のアプリ設定で指定します。</li>
<li data-line="79">Auth URL:&#0160;<strong><code>{{baseUrl}}/authentication/v2/authorize</code></strong></li>
<li data-line="80">Access Token URL:&#0160;<code><strong>{{baseUrl}}/authentication/v2/token</strong></code></li>
<li data-line="81">Client ID:&#0160;<code>{<strong>{APS_CLIENT_ID}}</strong></code></li>
<li data-line="82">Client Secret:&#0160;<strong><code>{{APS_CLIENT_SECRET}}</code></strong></li>
<li data-line="83">Scope:<strong>&#0160;</strong><strong><code>data:read</code></strong></li>
<li data-line="84">Client Authentication:&#0160;<strong>Send as Basic Auth header</strong></li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d46f98200c-pi" style="display: inline;"><img alt="3-legged_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d46f98200c image-full img-responsive" src="/assets/image_887778.jpg" title="3-legged_2" /></a><strong><br /></strong></p>
<ol data-line="85" dir="auto" start="5">
<li data-line="85"><a  _istranslated="1" data-href="https://aps.autodesk.com/myapps" href="https://aps.autodesk.com/myapps" rel="noopener" target="_blank">https://aps.autodesk.com/myapps</a> で APS アプリに移動し、Postman によって生成されたコールバック URL を追加して、[Save changes] ボタンで設定を保存してください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860eb20c6200b-pi" style="display: inline;"><img alt="3-legged_3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860eb20c6200b image-full img-responsive" src="/assets/image_619044.jpg" title="3-legged_3" /></a><br /><br /></li>
<li data-line="86">Postman に戻り、一番下の [Get New Access Token] ボタンをクリックします。</li>
<li data-line="60">[MANAGE ACCESS TOKENS] ダイアログで [Use Token] ボタンをクリックします。</li>
<li data-line="60">コレクションの詳細ページに戻り、Auto-refresh Token&#0160;オプションが有効になっていることを確認します。</li>
<li data-line="88">コレクションを保存します。</li>
</ol>
<p data-line="91" dir="auto" style="padding-left: 40px;">注: 2-Legged Auth と同様に、 <code>{{baseUrl}}</code><code> </code>変数がインポートした OpenAPI 仕様のコレクションに対して定義されています。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d46ff3200c-pi" style="display: inline;"><img alt="3-legged_4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d46ff3200c img-responsive" src="/assets/image_236225.jpg" title="3-legged_4" /></a></p>
<p data-line="91" dir="auto" style="padding-left: 40px;"><iframe allowfullscreen="allowfullscreen" data-wat-video-found="true" frameborder="0" height="470px" id="waf_detected_youtube3" src="https://www.youtube.com/embed/LLawbBn46HU?autoplay=0&amp;start=0&amp;rel=0&amp;enablejsapi=1&amp;origin=https://aps.autodesk.com" title="Setup 3-Legged OAuth in Postman" width="854"></iframe></p>
<hr />
<p data-line="95" dir="auto"><strong>Tips:</strong></p>
<ul>
<li>[Get New Access Token] ボタン クリック後、次のエラーが表示される場合には、Client ID と Client Secret を設定した環境が<a href="#activate_env">アクティブ</a>になっていない（チェックがはずれている）可能性があります。</li>
</ul>
<blockquote>
<p>{ &quot;developerMessage&quot;:&quot;The client_id specified does not have access to the api product&quot;, &quot;moreInfo&quot;: &quot;https://aps.autodesk.com/en/docs/oauth/v2/developers_guide/error_handling/&quot;, &quot;errorCode&quot;: &quot;AUTH-001&quot;}</p>
</blockquote>
<ul>
<li>3-Legged Auth（3-legged 認証フロー）で Authorize using browser&#0160; にチェックすると、ブラウザ側での認可処理後、Postman への画面遷移を額人するポップアップが表示されます。ブラウザ設定でポップアップがブロックされていると、遷移に失敗してしまいます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d4c549200c-pi" style="display: inline;"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d4c54f200c-pi" style="display: inline;"><img alt="Popup" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d4c54f200c img-responsive" src="/assets/image_379522.jpg" title="Popup" /></a></li>
<li>3-Legged Auth（3-legged 認証フロー）が正常に動作せず、Postman 起動時に次のメッセージが表示される場合には、アクセスを許可して動作を確認してみてください。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d4c557200c-pi" style="display: inline;"><img alt="Permit_access" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d4c557200c img-responsive" src="/assets/image_847662.jpg" title="Permit_access" /></a></li>
</ul>
<hr />
<p data-line="95" dir="auto">これで、Postman リクエストのコレクションを使用する準備が整いました。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/using-aps-openapi-specs-postman" rel="noopener" target="_blank">Using APS OpenAPI Specs with Postman | Autodesk Platform Services</a> から転写・意訳・補足したものです。</p>
<p>By Toshiaki Isezaki</p>
</div>
