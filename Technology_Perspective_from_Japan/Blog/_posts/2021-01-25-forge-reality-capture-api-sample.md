---
layout: "post"
title: "Forge Reality Capture API サンプル"
date: "2021-01-25 00:25:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/01/forge-reality-capture-api-sample.html "
typepad_basename: "forge-reality-capture-api-sample"
typepad_status: "Publish"
---

<p>Forge には、最大 1,000 枚の写真から 3D データを生成する Reality Capture API が提供されています。一般に<strong>フォトグラメトリー</strong>と呼ばれるテクノロジを API 化したものです。</p>
<p>もともと、特定の業種に特化したものではありませんでしたが、ドローンから撮影した写真の利用が多いことから、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/updates-to-reality-capture-api-point-clouds-meshing-engines-and-geolocation-for-infraworks-1.html" rel="noopener" target="_blank">Reality Capture API アップデート</a></strong> のブログ記事でご案内したとおり、現在では建設業用途にチューニングされています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeb843e3200c-pi" style="display: inline;"><img alt="Sample" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeb843e3200c img-responsive" src="/assets/image_758564.jpg" title="Sample" /></a></p>
<p>Reality Capture API で生成/取得可能なデータは、次のファイル形式になります。</p>
<ul class="simple">
<li>rcm: Autodesk ReCap Photo メッシュ データ ファイル</li>
<li>rcs: Autodesk ReCap 点群データ ファイル</li>
<li>obj: Wavefront Object データ ファイル</li>
<li>fbx:&#0160;<a class="reference external" href="https://www.autodesk.com/products/fbx/overview" rel="noopener" target="_blank">Autodesk FBX</a>&#0160;3D アセット交換データ ファイル</li>
<li>ortho: オルソ画像とエレベーション マップ</li>
<li>report: 品質レポート</li>
</ul>
<p><strong><a href="https://forge.autodesk.com/code-samples" rel="noopener" target="_blank">Forge Code Sample</a></strong> ページにも Reality Capture API を評価するものが公開されています。過去に、サンプルの実行までの手順をまとめた<a href="https://adndevblog.typepad.com/technology_perspective/2017/12/about-reality-capture-api-sample.html" rel="noopener" target="_blank"><strong>ブログ記事</strong></a>を記載したことがありましたが、当時から内容が大きく変更されていますので、ここでは <a href="https://github.com/Autodesk-Forge/reality.capture-nodejs-photo.to.3d" rel="noopener" target="_blank"><strong>Photo to 3D (photogrammetry) sample</strong></a>&#0160; を使った評価手順をご紹介したいと思います。</p>
<hr />
<p><strong>準備：（ここでは Windows 環境での利用を前提にご紹介します）</strong></p>
<ul>
<li>本サンプルは、Web サーバー実装に Node.js を使用しています。このため、事前に Node.js をインストールしておく必要があります。また、GiiHub リポジトリからのプロジェクト入手（クローン）に git コマンドを使用します。両者を利用可能とする手順は、<a href="https://adndevblog.typepad.com/technology_perspective/2017/01/development-environment-for-forge.html" rel="noopener" target="_blank"><strong>Forge の開発環境</strong> </a>でご紹介しています。もちろん、ソースコード等を編集する場合には、テキストエディタも必要となります。</li>
<li>
<p>本サンプルをローカル環境でテストするには、利用する Client ID と Client Secret（Consumer Key と Consumer Secret）を事前に <strong><a href="https://forge.autodesk.com/" rel="noopener noreferrer" target="_blank">Forge ポータル</a></strong> から取得しておく必要があります。これらの手順は、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html" rel="noopener noreferrer" target="_blank">Forge API を利用するアプリの登録とキーの取得</a></strong> でご紹介しています。</p>
</li>
<li>
<p>本サンプルをローカル環境で実行するには、Forge ポータルでのアプリ作成時に、あらかじめ <strong>&#0160;CallBack URL</strong>&#0160;に<strong> https://localhost:3000/api/forge/callback/oauth </strong>&#0160;を設定しておく必要があります。Node.js で https サーバーが利用出来ない環境では、認可プロセスでエラーになってしまいます。この場合には、設定する CallBack URL に&#0160;<strong>s</strong>&#0160;を省いた <strong><span style="background-color: #ffff00;">http:</span>//localhost:3000/api/forge/callback/oauth</strong> を設定してみてください。</p>
</li>
</ul>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeb8455d200c-pi" style="display: inline;"><img alt="Forge_app_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeb8455d200c image-full img-responsive" src="/assets/image_660105.jpg" title="Forge_app_settings" /></a></p>
<ul>
<li>本ンプルの実行時には、環境変数から Client ID、Client Secret、CallBack URL を読み取ります。テキストエディタでシステム環境変数、FORGE_CLIENT_ID、FORGE_CLIENT_SECRET、FORGE_CALLBACK_URL の代入式を用意定します。FORGE_CALLBACK_URL 値には、上記で設定したものを記述してください。<br />
<pre><code>set FORGE_CLIENT_ID=&lt;&lt;YOUR CLIENT ID FROM DEVELOPER PORTAL&gt;&gt;
set FORGE_CLIENT_SECRET=&lt;&lt;YOUR CLIENT SECRET&gt;&gt;
set FORGE_CALLBACK_URL=&lt;&lt;YOUR CALLBACK URL&gt;&gt;</code></pre>
</li>
</ul>
<p style="padding-left: 40px;">コマンド プロンプトを起動して、テキストエディタ上の set コマンドをクリップボードへコピー、コマンド プロンプトへ貼り付けしながら 環境変数への設定を完了します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e98af929200b-pi" style="display: inline;"><img alt="Set_environment_variables" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e98af929200b image-full img-responsive" src="/assets/image_82703.jpg" title="Set_environment_variables" /></a></p>
<hr />
<p><strong>実行手順：</strong></p>
<ol>
<li>GitHub 上のプロジェクトを開発に使用するクライアント コンピュータにクローンします。コマンド プロンプトを起動して、CD コマンドでリポジトリ内容をコピーしたいフォルダ（ディレクトリ）に 移動し、 <strong>git clone https://github.com/Autodesk-Forge/reality.capture-nodejs-photo.to.3d.git</strong> と入力します。</li>
<li>クローン（コピー）されたプロジェクトに Node.js の実行で必要となるパッケージ（ミドルウェア）をインストールします。CD コマンドで現在のフォルダを <strong>photo-to-3d-sample</strong> フォルダへ移動してから、<strong>npm install</strong>&#0160;と入力します。</li>
<li>現在のフォルダが <strong>photo-to-3d-sample</strong> フォルダであることを確認したら、&#0160;<strong>node index.js</strong>&#0160;と入力、リターンキーを押して Node.js サーバーを起動します。</li>
<li>コマンド プロンプトに Server listening on port 3000 と表示されたら、Web ブラウザを起動して URL に <strong>https://localhost:3000</strong> と入力してリターンします。この時、CallBack URL や環境変数 FORGE_CALLBACK_URL に <strong><span style="background-color: #ffff00;">http:</span>//localhost:3000/api/forge/callback/oauth</strong>&#0160;とした場合には、<strong><span style="background-color: #ffff00;">http:</span>//localhost:3000 </strong>としてください。</li>
<li>次の画面が表示されたら、中央の [Authorize me] ボタンをクリックします。&#0160;<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e98afa0a200b-pi" style="display: inline;"><img alt="Intial_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e98afa0a200b image-full img-responsive" src="/assets/image_91573.jpg" title="Intial_page" /></a></li>
<li>3-legged 認証/認可のプロセスを実行します。ユーザとして使用する方の Autodesk ID（ユーザ名とパスワード）を画面遷移にしたがって入力、[サインイン] ボタンをクリックしてください。</li>
<li>認可の確認ページが表示されたら、[許可] ボタンをクリックします。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880101d29200d-pi" style="display: inline;"><img alt="Authoriza" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880101d29200d image-full img-responsive" src="/assets/image_167185.jpg" title="Authoriza" /></a></li>
<li>サンプルのメインページが表示されたら、シーン名を入力して（①）[Create] ボタンをクリック（②）、「ID created」欄にランダムに生成されるシーン ID が表示されたら、[Upload remotely located photos] ボタンをクリックします（③）。写真がすべてアップロードされると、[Upload remotely located photos] ボタン下に 「<strong>Number of posted images:&#0160;<span id="post-count">7</span></strong>」と表示されるので（④）、[Launch photoscene] ボタンをクリックして演算処理を開始します（⑤）。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e98afa93200b-pi" style="display: inline;"><img alt="App_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e98afa93200b image-full img-responsive" src="/assets/image_402058.jpg" title="App_page" /></a></li>
<li>[Get Result] ボタンで演算状態を紹介することが出来ます（⑥）。処理が完了するには十数分かかります。演算中には「Data is not ready」と表示されます。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e98afb23200b-pi" style="display: inline;"><img alt="Donotready" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e98afb23200b image-full img-responsive" src="/assets/image_457060.jpg" title="Donotready" /></a><br /><br /></li>
<li>処理が完了すると、次のように、生成したファイルのダウンロード URL が表示されるので、Web ブラウザで URL を入力（”scenelink” 値）してダウンロードすることが出来ます。この URL は 30 日間のみ有効です。<br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e98afb02200b-pi" style="display: inline;"><img alt="Result" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e98afb02200b image-full img-responsive" src="/assets/image_850516.jpg" title="Result" /></a></li>
<li>サンプルでは、rcm ファイルの生成、ダウンロードを指定しています。ダウンロードしたファイルは、<a href="https://www.autodesk.co.jp/products/recap/overview" rel="noopener" target="_blank"><strong>ReCap Pro</strong></a> に同梱される ReCap Photo で表示、編集することが可能です。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788010190c200d-pi" style="display: inline;"><img alt="Recap_photo" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788010190c200d image-full img-responsive" src="/assets/image_893005.jpg" title="Recap_photo" /></a></li>
</ol>
<hr />
<p>入手したいファイルの指定は、<strong><a href="https://forge.autodesk.com/en/docs/reality-capture/v1/reference/http/photoscene-POST/" rel="noopener" target="_blank">POST photoscene</a></strong> endpoint で、生成後のファイル ダウンロードは、<strong><a href="https://forge.autodesk.com/en/docs/reality-capture/v1/reference/http/photoscene-:photosceneid-GET/" rel="noopener" target="_blank">GET photoscene/:photosceneid</a></strong> endpoint でおこなうことが出来ます。obj ファイルを指定すれば、テクスチャ画像も含めた ZIP 圧縮ファイルをダウンロード出来るので、Model Derivative API で SVF 変換、Forge Viewer で表示することも可能です。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/09/viewer-workflow-on-vs-code-forge-extension.html" rel="noopener" target="_blank">VS Code Forge Extension を使った Viewer ワークフローの確認</a> </strong>の手順で VSCode でも確認出来るはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e98afcd3200b-pi" style="display: inline;"><img alt="Vscode" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e98afcd3200b image-full img-responsive" src="/assets/image_291784.jpg" title="Vscode" /></a></p>
<p>By Toshiaki Isezaki</p>
