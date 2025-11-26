---
layout: "post"
title: "Design Automation API for Inventor - Postman Sample での動作確認 "
date: "2020-05-25 04:38:02"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/05/design-automation-api-for-inventor-postman-sample-tutorial.html "
typepad_basename: "design-automation-api-for-inventor-postman-sample-tutorial"
typepad_status: "Publish"
---

<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e948300b200b-pi" style="display: inline;"><img alt="Autodesk-forge-logo-pms-color-black-stacked" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e948300b200b image-full img-responsive" src="/assets/image_657512.jpg" title="Autodesk-forge-logo-pms-color-black-stacked" /></a></span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/design-automation-api-for-inventor-postman-sample-setup.html">前回の記事</a>では、Design Automation API for Inventorのチュートリアルを行うためのPostmanサンプルのセットアップを行いました。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">今回はセットアップしたPostmanコレクションを使い、以下の手順に従ってDesign Automation APIの動作を確認してみたいと思います。</span></p>
<p><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">なお、以下手順での（）内の記載は、対応するPostmanコレクションのタスク名となります。</span></p>
<ol>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">アクセストークンの取得（Task 1 - Obtain an Access Token）</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ニックネームの作成（Task 2 - Create a Nickname）</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">AppBndleをDesign Automationにアップロード(Task 3 - Upload an AppBundle)</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Activityの公開(Task 4 - Publish an Activity)</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">クラウドストレージの準備(Task 5 - Prepare cloud storage)</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">WorkItemの作成(Task 6 - Submit a WorkItem)</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">結果のダウンロード(Task 7 - Download the Result)</span></li>
</ol>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>１．アクセストークンの取得（Task 1 - Obtain an Access Token）</strong></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>１-１．Forge Appの作成と、Client ID と Client Secretの取得</strong></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/design-automation-api-for-inventor-postman-sample-setup.html">前回の記事</a>のPostman Sampleのセットアップでも記載しましたが、Design Automation API を実行するためには、Forge Appを作成する必要があります。また、作成したForge Appにアクセスするためには、Client ID と Client Secret が必要となります。</span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">今回のサンプルを実行するために、以下の過去のブログ記事を参考に、&quot;<strong>Design Automation API <span style="color: #ff0000;">V3</span></strong>&quot; および &quot;<strong>Data Management API</strong>&quot;の設定と、取得をお願いいたします。</span></p>
<ul>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a href="https://adndevblog.typepad.com/technology_perspective/2016/07/app-registration-and-getting-keys-for-forge-api.html">Forge API を利用するアプリの登録とキーの取得</a></span></li>
</ul>
</ul>
<p style="padding-left: 40px;">&#0160;</p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>１-２．Client ID と Client SecretをPostmanの環境変数に設定</strong></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/design-automation-api-for-inventor-postman-sample-setup.html">前回の記事</a>でPostmanに設定したDA4Inventor環境には、 &quot;client_id&quot; と &quot;client_secret&quot;という環境変数があります。この環境変数に値を設定することで、Forge APIを実行する際に必要となる、これらの値を個別に指定する必要がなくなります。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">1-2-1．Postman アプリケーションの右上にある&quot;Environment quick look&quot;アイコンをクリック</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9483a45200b-pi"></a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2da5113200d-pi" style="display: inline;"><img alt="Task1-environment_quick_look_icon" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2da5113200d image-full img-responsive" src="/assets/image_265741.jpg" title="Task1-environment_quick_look_icon" /></a><br /><br /></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">1-2-2．”client_id&quot;行の”CURRENT VALUE&quot;カラムをクリックすると、編集アイコンが表示されます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19dcac200c-pi" style="display: inline;"><img alt="Task1-environment_edit_variable" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec19dcac200c image-full img-responsive" src="/assets/image_993323.jpg" title="Task1-environment_edit_variable" /></a><br /></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">1-2-3．編集アイコンをクリックして、利用するForge AppのClient IDを入力します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">1-2-4．同様に&quot;client_secret&quot;行の、編集アイコンをクリックして、利用するForge AppのClient Secret を入力します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">1-2-5．&quot;Environment quick look&quot;アイコンをクリックして、環境設定を終了します。</span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>１-3．アクセストークンの取得</strong></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">アクセストークンを取得するためには、Forgeに対して認証リクエストを送信する必要があります。それでは、Postmanコレクションを使用して、認証リクエストを送信してみましょう。</span></p>
<p>&#0160;</p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">1-3-1．Postmanのサイドバーで、「Task 1 - Obtain an Access Token」-「POST Get an Access Token」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">1-3-2．ボディタブを選択</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">1-3-3．マウスカーソルを、&quot;client_id&quot;のValueカラムに合わせて、1-2で設定した値が設定されていることを確認します。同様に&quot;client_secret&quot;についても値を確認します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19dee9200c-pi" style="display: inline;"><img alt="Task1-preview_environment_variables" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec19dee9200c image-full img-responsive" src="/assets/image_131359.jpg" title="Task1-preview_environment_variables" /></a><br /></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">1-3-4．「Send」ボタンをクリックし、リクエストをForgeに送信します。リクエストに成功した場合は、以下のスクリーンショットにあるような期限付きのアクセストークンを含むレスポンスが表示されます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19deed200c-pi" style="display: inline;"><img alt="Task1-authenticate_successfull" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec19deed200c image-full img-responsive" src="/assets/image_760822.jpg" title="Task1-authenticate_successfull" /></a><br /></span></p>
<p>&#0160;</p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ここで取得したアクセストークンは、Postmanの環境変数”dasApiToken”に保存されます。サンプルのPostmanコレクションは、Task2以降のリクエストを送信する際に、この変数の値をアクセストークンとして使用するよう作られているため、以降のTaskの実行時に手作業で指定する必要はありません。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">言うまでもありませんが、同様の処理をプログラムから行う場合には、Postmanコレクションが行っているのと同様の処理を作成し、各HTTPリクエストでアクセストークンを指定する必要があることにご注意ください。</span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>２．ニックネームの作成（Task 2 - Create a Nickname）</strong></span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Forgeでは、Client IDをアプリケーションを一意に識別するIDとしてClient IDを使用します。ただし、Client IDは人間にとっては、わかりにくい、意味を持たない文字列のため、Forge アプリケーションのデータを参照する際にストレスを感じてしまいまし。この点を解消するために、Forgeでは、Client IDを人間にとってわかりやすい意味を持った文字にマッピングする、ニックネームという仕組を用意しています。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">本チュートリアルでは、Postmanの&quot;dasNickName&quot;環境変数にニックネームを保存して、以降のリクエストで使用します。</span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>２-１．ニックネームを変数に設定</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">2-1-1．Postman アプリケーションの右上にある&quot;Environment quick look&quot;アイコンをクリック</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">2-1-2．”dasNickName&quot;行の”CURRENT VALUE&quot;カラムに、アプリケーションの任意のニックネームを入力します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2da54bd200d-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2da54bd200d-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19e073200c-pi" style="display: inline;"><img alt="Task2-environment_variables_grid" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec19e073200c image-full img-responsive" src="/assets/image_969391.jpg" title="Task2-environment_variables_grid" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2da54bd200d-pi" style="display: inline;"><br /></a><br /></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">2-1-3．&quot;Environment quick look&quot;アイコンをクリックして、環境設定を終了します。</span></p>
<p style="padding-left: 80px;">&#0160;</p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>２-２．ニックネームリクエストを送信</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">2-2-1．Postmanのサイドバーで、「Task 2 - Create a Nickname」-「PATCH Create Nickname」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">2-2-2．「Send」ボタンをクリックし、リクエストをForgeに送信します。リクエストに成功した場合は、以下のスクリーンショットにあるようなレスポンスが表示されます。レスポンスはヘッダーのみで、ボディはありません。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9483dfa200b-pi" style="display: inline;"><img alt="Task2-successfull" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9483dfa200b image-full img-responsive" src="/assets/image_348731.jpg" title="Task2-successfull" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2da54bd200d-pi" style="display: inline;"><br /></a></span></p>
<p style="padding-left: 40px;">&#0160;</p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt; color: #ff0000;"><strong>注意事項</strong></span></p>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">データをForge アプリケーションに追加した場合は、ニックネームを設定することは出来ません。この場合に、アプリケーションにニックネームを設定する唯一の方法は、<strong> [DELETE] /forgeapps/me</strong> エンドポイントを使用して、ニックネームを含む<strong><span style="color: #ff0000;">すべてのデータをアプリケーションから削除</span></strong>することになります。Postmanコレクションの<strong>「Extras」-「Delete Forge App Data in Design Automation」</strong>を用いて、<strong>[DELETE] /forgeapps/me</strong>エンドポイントを呼び出すことができます。</span></li>
</ul>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19e144200c-pi" style="display: inline;"><img alt="Task2-delete_forge_app" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec19e144200c image-full img-responsive" src="/assets/image_307493.jpg" title="Task2-delete_forge_app" /></a></span></p>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">もし本チュートリアルの途中で躓いてしまい進めることができない場合、本リクエストを使用して、アプリケーションからすべてのデータクリアしてTask 1からやり直しをしてみてください。</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ニックネームはグローバルに一意である必要があります。このため、もし指定したニックネームが既に使用されている場合、Forgeは409 Conflict errorを返します。</span></li>
</ul>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>３．AppBndleを</strong><strong>Design</strong><strong> Automationにアップロード(Task 3 - Upload an AppBundle)</strong></span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">AppBundleはパッケージ化された、カスタムコマンドを実行するバイナリファイルとサポートファイルです。本チュートリアルでは、サンプルのAppbundle（samplePlugin.bundle）を使用します。このサンプルのAppBundleには、パートまたはアセンブリファイルを読み込み、指定されたサイズに変更後にビットマップファイルを生成するInventorのプラグインファイルが含まれています。</span></p>
<p>&#0160;</p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>３</strong><strong>-１．AppBundleをダウンロード</strong></span></p>
<ul>
<ul>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b0264e2da5d74200d img-responsive" style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a href="https://adndevblog.typepad.com/files/sampleplugin.bundle.zip">SamplePlugin.bundleファイルをダウンロード</a></span></li>
</ul>
</ul>
<p style="padding-left: 40px;">&#0160;</p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>３-２．AppBundleの登録</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-2-1．Postmanのサイドバーで、「Task 3 - Upload AppBundle」-「POST Register the AppBundle」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-2-2．ボディタブを選択して、idの値が”ChangeParamApp”で、engineの値が”Autodesk.Inventor+24” (Inventor 2020)であることを確認します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e948459d200b-pi" style="display: inline;"><img alt="Task3-appbundle_body" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e948459d200b image-full img-responsive" src="/assets/image_318423.jpg" title="Task3-appbundle_body" /></a><br /></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-2-3．「Send」ボタンをクリックして、リクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが表示されます。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e94845b5200b-pi" style="display: inline;"><img alt="Task3-appbundle_registered" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e94845b5200b image-full img-responsive" src="/assets/image_600339.jpg" title="Task3-appbundle_registered" /></a></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">レスポンスのuploadParametersには、AppBundleをForgeにアップロードするための情報が含まれています。この情報は、Postmanの環境変数に保存され、以降の手順で使用されます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19e909200c-pi" style="display: inline;"><img alt="Task3-appbundle_form_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec19e909200c image-full img-responsive" src="/assets/image_507266.jpg" title="Task3-appbundle_form_data" /></a></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>３-３．AppBundleをアップロード</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-3-1．Postmanのサイドバーから「Task 3 - Upload AppBundle」－「POST Upload the AppBundle」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-3-2．ボディタブを選択して、スクロールを下に移動して&quot;file&quot;行を表示します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-3-3．Value列の”Select Files”をクリックしてダウンロードをしたsamplePlugin.bundle.zipを選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2da5ca6200d-pi" style="display: inline;"><img alt="Task3-appbundle_select_file" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2da5ca6200d image-full img-responsive" src="/assets/image_472914.jpg" title="Task3-appbundle_select_file" /></a></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-3-4．「Send」をクリックして、リクエストを送信します。リクエストが成功すると、以下のスクリーンショットにあるようなレスポンスが返ります。レスポンスはヘッダーのみでボディはありません。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec1a1ca4200c-pi" style="display: inline;"><img alt="Task3-appbundle_uploaded" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec1a1ca4200c image-full img-responsive" src="/assets/image_962754.jpg" title="Task3-appbundle_uploaded" /></a></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>３-４．AppBundleのエイリアスを登録</strong></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">先のステップで、AppBundleの登録した際に、バージョン１のAppBudleが登録されています。このステップでは、バージョン１のAppBundleに、 ”my_working_version”という名前のエイリアスを作成します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-4-1．Postmanのサイドバーで、「Task 3 - Upload AppBundle」－「POST Create an Alias for the AppBundle」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-4-2．ボディタブを選択して、idの値が&quot;my_working_version&quot;であることを確認します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e94845f7200b-pi" style="display: inline;"><img alt="Task3-appbundle_alias" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e94845f7200b image-full img-responsive" src="/assets/image_454349.jpg" title="Task3-appbundle_alias" /></a></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">3-4-3．「Send」ボタンをクリックして、リクエストを送信します。成功した場合、以下のスクリーンショットのようなレスポンスが返ります。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec19e95d200c-pi" style="display: inline;"><img alt="Task3-appbundle_alias_set" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec19e95d200c image-full img-responsive" src="/assets/image_154912.jpg" title="Task3-appbundle_alias_set" /></a></span></p>
<p style="padding-left: 80px;">&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>４．Activityの公開(Task 4 - Publish an Activity)</strong></span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">本ステップでは、Design Automationで実行するアクションとなる Activityを作成します。</span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>４-１．新規Activityの作成</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">4-1-1．Postmanのサイドバーから、「Task 4 - Publish an Activity」－「POST Create a New Activity」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">4-1-2．ボディタブを選択してActivityのパラメータを確認します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9487ad2200b-pi" style="display: inline;"><img alt="Task4-create_activity" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9487ad2200b image-full img-responsive" src="/assets/image_678528.jpg" title="Task4-create_activity" /></a></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Activityのパラメータについて</span></p>
<ul>
<ul>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">idは作成するActivityの名前となります。この値は、Postmanの環境変数&quot;dasActivityName&quot;に保存されます。</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">commandLineは、Activityを実行する際のコマンドラインとなります。コマンドラインには、変数を指定することができ、Activity実行時に実際の値と置き換えられます。</span>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">$(engine.path)\\InventorCoreConsole.exe - Inventorエンジンのフルパス。リクエストボディに利用するInventorのバージョンを、 “engine”: “Autodesk.Inventor+24&quot; (Inventor+24はInventor 2020)の形で指定します。</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">$(args[InventorDoc].path) - InventorDocパラメータで指定されるドキュメントがダウンロードされるフォルダのフルパス。</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">$(appbundles[ChangeParamApp].path) - Appbundleパラメータで指定した、AppBundleがUnzipされるフルパス。</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">$(args[InventorParams].path) - 高さと幅のパラメータ（プラグインに指定する値）を記述したJSONファイルへのフルパス。</span></li>
</ul>
</li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">parametersには、Activityの実行時に必要な入力と出力を定義します。 入力パラメータには&quot;verb&quot;:&quot;get&quot;を出力パラメータには&quot;verb&quot;:&quot;put&quot;を指定します。</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">engineには、Activity実行時に使用するDesign Automation エンジン（この場合はInventor 2020）を指定します。</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">appbundlesは、commandLineから参照され、Activityで使用するAppBundleのIDを指定します。このパラメータは配列形式で指定しますが、Design Automation for Inventorでは、Activityに対して一つのAppBundleのみが指定可能です(Design Automation for AutoCAD では、複数のAppBundleを指定可能)。</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">settingsには、Activityで実行するコマンドスクリプトを指定します。チュートリアルではスクリプトは実行しないため空となります。</span></li>
</ul>
</ul>
</ul>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">4-1-3．「Send」をクリックしてリクエストを送信します。リクエストが成功すると、以下のスクリーンショットのようなレスポンスが返されます。ChangeParamActivityに、どのように名前（id）設定されたかを確認してください。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9487ad9200b-pi" style="display: inline;"><img alt="Task4-activity_create_success" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9487ad9200b image-full img-responsive" src="/assets/image_507414.jpg" title="Task4-activity_create_success" /></a></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>４-２．ActivityのAliasを作成</strong></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">先のステップでActivityを作成した際、バージョン１のActivityが作成されました。Activityには、新しいバージョンを作ることができるため、Activityのidのみでは、どのバージョンのActivityを使用するのかを指定することができません。特定のバージョンのActivityに対応するAliasを作成して使用することで、特定のバージョンのActivityを指定することができます。Aliasに対応するバージョンは後から変更することも可能です。</span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">以下の手順で、version&#0160;1のChangeParam Activityに&quot;my_current_version&quot;というAliasを作成します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">4-2-1．Postmanのサイドバーから「Task 4 - Create an Activity」－「POST Create an Alias to the Activity」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">4-2-2．ボディタブを選択し、Aliasの idに”my_current_version”が設定されていることを確認します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">4-2-3．「Send」をクリックしてリクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが戻されます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9487ae2200b-pi" style="display: inline;"><img alt="Task4-activity_alias_create_success" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9487ae2200b image-full img-responsive" src="/assets/image_382242.jpg" title="Task4-activity_alias_create_success" /></a></span></p>
<p style="padding-left: 80px;">&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>５．クラウドストレージの準備(Task 5 - Prepare cloud storage)</strong></span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">チュートリアルで作成したChangeParamActivityは、Inventorのパートまたはアセンブリの高さと幅を、JSONオブジェクトで指定した値に変更します。また、同時にパートまたはアセンブリの画像ファイルを作成します。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Step５では、入力となるInventorファイルおよび、出力されるInventorファイルと画像ファイルを保存するクラウドストレージの設定を行います。ForgeのDesign Automationでは様々なクラウドストレージを利用することができますが本チュートリアルでは、Forge Data Management APIを使って、Object Storage Service (OSS)を使用することにします。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">このタスクを実行にあたって以下の５つのPostmanの環境変数を指定する必要があります。</span></p>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ossBucketKey - 入力となるファイルが格納されたBucketのBucketキー</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ossInputFileObjectKey - Inventorパートファイルのオブジェクトキー</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ossOutputIptFileObjectKey - Activityで生成される、リサイズ後のパートファイルを格納するプレースフォルダーのオブジェクトキー</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ossOutputIamFileObjectKey - Activityで生成される、リサイズ後のアセンブリファイルを格納するプレースフォルダーのオブジェクトキー</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">ossOutputBmpFileObjectKey - Activityで生成される、画像ファイルを格納するプレースフォルダーのオブジェクトキー</span></li>
</ul>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>５-１．Bucketの作成</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-1-1．Postman アプリケーションの右上にある&quot;Environment quick look&quot;アイコンをクリック</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-1-2．&quot;ossBucketKey&quot;行の、CURRENT VALUEカラムに、入力となるInventorファイルを格納するBucket名指定します。</span></p>
<ul>
<ul>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Bucket名は、OSSサービス内で一意である必要があります。もし指定したBucketが既に存在する場合、以下の手順5-1-5で、409 conflict errorが返されます。 その場合は、値を変更して再度実行をしてください。</span></li>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Bucket名には、アルファベット小文字、0-9の数字、アンダースコアが使用できます。</span></li>
</ul>
</ul>
</ul>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-1-3．&quot;Environment quick look&quot;アイコンをクリックして変数のリストを閉じます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-1-4．Postmanサイドバーから「Task 5 - Prepare Cloud Storage」－ 「POST Create a Bucket」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-1-5．「Send」をクリックしてリクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが返されます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2db5bd6200d-pi" style="display: inline;"><img alt="Task5-sucessfull_bucket_creation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2db5bd6200d image-full img-responsive" src="/assets/image_66128.jpg" title="Task5-sucessfull_bucket_creation" /></a><br /></span></p>
<p style="padding-left: 80px;">&#0160;</p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>５-2．入力ファイルをOSSにアップロード</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-2-1．入力となるInventorパートファイルをダウンロードします。</span></p>
<ul>
<ul>
<ul>
<li><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><span class="asset  asset-generic at-xid-6a0167607c2431970b0263ec1aed60200c img-responsive"><a href="https://adndevblog.typepad.com/files/box.ipt">Box.iptをダウンロード。</a></span></span></li>
</ul>
</ul>
</ul>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-2-2．Postman アプリケーションの右上にある&quot;Environment quick look&quot;アイコンをクリック</span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">5-2-3．&quot;ossInputFileObjectKey&quot;行の、CURRENT VALUEカラムに、入力となるInventorファイルのObject Key(入力ファイルをOSSにアップロード後に一意に特定するための名前)を指定します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-2-4．&quot;Environment quick look&quot;アイコンをクリックして変数のリストを閉じます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-2-5．Postmanのサイドーバーから、「Task 5 - Prepare Cloud Storage」ー「PUT Upload Input File」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-2-6．ボディタブをクリックします。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-2-7．「Select File」をクリックし、ダウンロードしたbox.iptを指定します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e949473e200b-pi" style="display: inline;"><img alt="Task5-select_files_button" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e949473e200b image-full img-responsive" src="/assets/image_137070.jpg" title="Task5-select_files_button" /></a></strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-2-8．「Send」をクリックしリクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが戻されます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2db5c03200d-pi" style="display: inline;"><img alt="Task5-successful_upload" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2db5c03200d image-full img-responsive" src="/assets/image_130812.jpg" title="Task5-successful_upload" /></a><br /></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>5-3．一時ダウンロードURLの取得</strong></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Design Automationでは、入力ファイルをダウンロードして処理を実施します。このステップでは、Design Automationで利用可能な、入力ファイルをダウンロードするためのテンポラリーのサイン済みURLを取得します。 このURLの有効期限は1時間です。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-3-1．Postmanのサイドバーから「Task 5 - Prepare Cloud Storage」－ 「POST Get Temporary Download URL」を選択。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-3-2．「Send」をクリックしリクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが返され、テンポラリーのURLが取得できます。URLはTestタブのスクリプトでURLをPostmanの環境変数”ossInputFileSignedUrl”に保存されます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2db5c12200d-pi" style="display: inline;"><img alt="Task5-signed_downloadurl" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2db5c12200d image-full img-responsive" src="/assets/image_819726.jpg" title="Task5-signed_downloadurl" /></a></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>5-4．出力iptファイルのアップロード用一時URLを取得</strong></span></p>
<p style="padding-left: 40px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">Design Automationでは、Activityが出力するサイズ変更後のiptファイルをUploadするためのサイン済みURLを使用します。本ステップでは、Design Automationが利用できるテンポラリのサイン済みURLを取得します。このURLの有効期限は1時間です。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-4-1．Postman アプリケーションの右上にある&quot;Environment quick look&quot;アイコンをクリック。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-4-2．&quot;ossOutputIptFileObjectKey &quot;行の、CURRENT VALUEカラムに、出力されるInventorファイルのObject Key(出力ファイルをOSSにアップロード後に一意に特定するための名前)を指定します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-4-3．&quot;Environment quick look&quot;アイコンをクリックして変数のリストを閉じます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-4-4．Postmanのサイドーバーから、「Task 5 - Prepare Cloud Storage」－ 「POST Get Temporary Upload URL for Output IPT File」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-4-5．「Send」をクリックして、リクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが返さ、テンポラリーのURLが取得できます。URLはTestタブのスクリプトでPostmanの環境変数”ossOutputIptFileSignedUrl”に保存されます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2db5c23200d-pi" style="display: inline;"><img alt="Task5-signed_uploadurl_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2db5c23200d image-full img-responsive" src="/assets/image_409753.jpg" title="Task5-signed_uploadurl_01" /></a></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>5-5．出力BMPファイルのアップロード用一時URLを取得</strong></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Design Automationでは、Activityが出力する画像ファイルをUploadするためのサイン済みURLを使用します。本ステップでは、Design Automationが利用できるテンポラリのサイン済みURLを取得します。このURLの有効期限は1時間です。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-5-1．Postman アプリケーションの右上にある&quot;Environment quick look&quot;アイコンをクリック。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-5-2．&quot;ossOutputBmpFileObjectKey &quot;行の、CURRENT VALUEカラムに、出力される画像ファイルのObject Key(出力ファイルをOSSにアップロード後に一意に特定するための名前)を指定します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-5-3．&quot;Environment quick look&quot;アイコンをクリックして変数のリストを閉じます。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-5-4．Postmanのサイドーバーから、「Task 5 - Prepare Cloud Storage」－ 「POST Get Temporary Upload URL for Output BMP File」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">5-5-5．「Send」をクリックして、リクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが返さ、テンポラリーのURLが取得できます。URLはTestタブのスクリプトでPostmanの環境変数”ossOutputBmpFileSignedUrl”に保存されます。</span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2db5c32200d-pi" style="display: inline;"><img alt="Task5-signed_uploadurl_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2db5c32200d image-full img-responsive" src="/assets/image_57570.jpg" title="Task5-signed_uploadurl_02" /></a></span></p>
<p style="padding-left: 40px;">&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>６．WorkItemの作成(Task 6 - Submit a WorkItem)</strong></span></p>
<p><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">Design AutomationにWorkItemの実行を指示することにより、Design AutomationはWorkItem内で指定されたActivityを実行します。</span></p>
<p><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">ActivityとWorkItemの関係は、ちょうどプログラミングにおける”関数”と”関数の呼び出し”ととらえることができます。Activityの名前付きパラメータには、対応するWorkItemの名前付き引数をが存在します。関数呼び出しでのオプショナルパラメータと同様に、Activityのオプショナルパラメータは、WorkItemで指定せずにスキップをすることが可能です。</span></p>
<p><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">本チュートリアルでは、”ChangeParamActivity” Activityを実行するWorkItemを作成します。このWorkItemは、先のステップでアップロードしたiptファイルを入力ファイルとして使用します。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">このリクエストを実行すると、ossInputFileSignedUrl変数のサイン済みURLからiptファイルをダウンロードして使用します。</span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>6-1．WorkItemの作成</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">6-1-1．Postmanのサイドーバーから、「Task 6 - Submit a WorkItem」－ 「Create a WorkItem」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">6-1-2．ボディタブを選択して、Activity ID、入力ファイル、出力ファイルの指定を確認します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">6-1-3．「Send」をクリックしてリクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが戻されます。</span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e9494789200b-pi" style="display: inline;"><img alt="Task6-result_url" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e9494789200b image-full img-responsive" src="/assets/image_14856.jpg" title="Task6-result_url" /></a></span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">以下は、JSONペイロードの主なアトリビュートとなります。</span></p>
<ul>
<ul>
<ul>
<li><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">activityId - 実行するActivityを指定します。</span></li>
<li><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">arguments - Activityに指定する必要のあるすべてのパラメータを含みます。このパラメータは、Activityの作成時に指定したパラメータと一致する必要があります。</span></li>
<li><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">InventorDoc - Activity実行時に使用するInventorのパートファイルを指定します。この値はPostmanの&quot;ossInputFileSignedUrl&quot;変数に保持されています。</span></li>
</ul>
</ul>
</ul>
<p style="padding-left: 120px;"><span style="color: #111111; font-size: 11pt; font-family: arial, helvetica, sans-serif;"><strong>参考</strong></span></p>
<p style="padding-left: 120px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">IPTファイルの代わりにZipファイルをアップロードした場合、ZIPファイル内のパスを指定するpathInZipに、ZIPファイル内のパスを指定する必要があります。</span></p>
<ul>
<ul>
<ul>
<li><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">InventorParams - JSONオブジェクトを用いて、新しいパートファイルでの、高さと幅を指定します。</span></li>
<li><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">OutputIpt - サイズを変更したパートファイルを格納するクラウドストレージのサイン済みURLを指定します。この値は、Postmanの&quot;ossOutputIptFileSignedUrl&quot;変数に保持されています。</span></li>
<li><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">OutputBmp - 出力した画像ファイルを格納するクラウドストレージのサイン済みURLを指定します。この値は、Postmanの&quot;ossOutputBmpFileSignedUrl&quot;変数に保持されています。</span></li>
</ul>
</ul>
</ul>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>6-2．WorkItemのステータスを確認</strong></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Design AutomationのWorkItemは実行キューに入れられて、順次実行されます。実行完了後に、成功・失敗を確認する必要があります。このため、 WorkItemのステータスを確認する必要があります。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">6-2-1．Postmanのサイドーバーから、「Task 6 - Submit a WorkItem」ー 「Check Status of a WorkItem」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">6-2-2．「Send」をクリックしてリクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが戻されます。</span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e94947a4200b-pi" style="display: inline;"><img alt="Task6-check_status" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e94947a4200b image-full img-responsive" src="/assets/image_178268.jpg" title="Task6-check_status" /></a></span></p>
<p style="padding-left: 40px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;"><strong><span style="color: #ff0000;">注意</span></strong></span></p>
<p style="padding-left: 40px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">本チュートリアルでは、WorkItemの実行状態をリクエストを送信して確認をしています。Design Automation APIを用いて、実際のアプリケーションを作成する場合、WorkItemの作成時に”onComplete”引数を指定してWorkItemの実行が完了した際に呼び出されるコールバックURLを指定する方法を推奨いたします。詳細については、Forgeポータルのドキュメントからコールバックをご参照ください。</span></p>
<p style="padding-left: 40px;">&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>７．結果のダウンロード(Task 7 - Download the Result)</strong></span></p>
<p><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">WorkItemがActivityの実行を完了したらDesign Automationは結果ファイルをOSSにアップロードします。Forge Data Management APIを使用してローカル環境に結果ファイルをダウンロードすることができます。</span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>7-1．IPTファイルをダウンロードする</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">7-1-1．Postmanのサイドーバーから、「Task 7 - Download the Results」ー 「GET Download Resized IPT file from OSS」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">7-1-2．「Send」をクリックしてリクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが戻されます。</span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec1ae9a5200c-pi" style="display: inline;"><img alt="Task7-download_step_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec1ae9a5200c image-full img-responsive" src="/assets/image_869552.jpg" title="Task7-download_step_1" /></a></span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;">7-1-3．レスポンス領域で、「Save Response」ー「Save to a file」を選択して出力ファイルを.iptファイルとしてダウンロードします。</span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2db5d3f200d-pi" style="display: inline;"><img alt="Task7-download_step_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2db5d3f200d image-full img-responsive" src="/assets/image_639739.jpg" title="Task7-download_step_2" /></a></span></p>
<p style="padding-left: 40px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;"><strong>7-2．BMPファイルをダウンロードする</strong></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">7-2-1．Postmanのサイドーバーから、「Task 7 - Download the Results」ー 「GET Download Generated BMP file from OSS」を選択します。</span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">7-2-2．「Send」をクリックしてリクエストを送信します。リクエストが成功すると以下のスクリーンショットのようなレスポンスが戻されます。</span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263ec1aea41200c-pi" style="display: inline;"><img alt="Task7-download_step_3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263ec1aea41200c image-full img-responsive" src="/assets/image_324359.jpg" title="Task7-download_step_3" /></a></span></p>
<p style="padding-left: 80px;"><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">7-2-3．レスポンス領域で、「Save Response」ー「Save to a file」を選択して出力ファイルを.bmpファイルとしてダウンロードします。</span></p>
<p style="padding-left: 80px;"><span style="font-size: 11pt; font-family: arial, helvetica, sans-serif;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0264e2db5da1200d-pi" style="display: inline;"><img alt="Task7-download_step_4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0264e2db5da1200d image-full img-responsive" src="/assets/image_85570.jpg" title="Task7-download_step_4" /></a></span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">以上でPostmanコレクションを利用した、Design Automation API for Inventorのサンプルは終了となります。</span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Postmanコレクションを利用した、Design Automation API for Inventorの動作確認はいかがでしたでしょうか。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">Postmanを利用することで、Design Automation APIの動作手順をイメージすることができたのではないかと思います。</span></p>
<p>&#0160;</p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 11pt;">By Takehiro Kato</span></p>
