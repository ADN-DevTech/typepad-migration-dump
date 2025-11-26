---
layout: "post"
title: "Design Automation API for Revit - Postman Sample で動作確認 1"
date: "2019-03-08 17:39:11"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-postman-sample-1.html "
typepad_basename: "design-automation-api-for-revit-postman-sample-1"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/02/setup-design-automation-api-for-revit-postman-sample.html">前回のブログ記事</a>では、Design Automation API for Revit について、Postman サンプルで動作を確認するためのテスト環境のセットアップ手順をご紹介しました。</p>
<p>今回は、実際に Postman サンプルで動作を確認してみましょう。<br />Postman サンプルには、以下の３つのサンプルが同梱されていますが、今回は、<strong>SketchIt サンプル</strong>を使用します。</p>
<p style="padding-left: 40px;"><strong>CountIt サンプル</strong><br />Revit プロジェクトと Revit リンクに配置されている壁、床、ドア、窓カテゴリの数量を集計し、その結果をテキストファイルで出力します。<br />どのカテゴリを集計するかをプロパティで指定することができます。</p>
<p style="padding-left: 40px;"><strong>SketchIt サンプル</strong><br />新規の Revit プロジェクト上に壁と床を作成するサンプルです。結果は、Revit プロジェクトファイルとして出力します。<br />プロパティで作成する壁や床の座標を指定します。</p>
<p style="padding-left: 40px;"><strong>DeleteWalls サンプル</strong><br />既存の Revit プロジェクト上に配置されている壁を全て削除して、別の Revit プロジェクトとして出力します。</p>
<p><br /><strong>1. 環境変数の確認と Revit アドインのバンドルパッケージの準備</strong></p>
<p>前回の手順で、環境変数を設定いただきましたが、もう一度、環境変数が設定されているか確認しましょう。<br />Forge アプリの Client ID と Client Secret、そして Nickname をそれぞれ設定します。</p>
<p>※<strong>ニックネームはグローバルで一意でなければなりません。</strong>他のユーザーに既に使用されている場合は、そのニックネームを使用することはできません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49109bb200b-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49109fa200b-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman1" class="asset  asset-image at-xid-6a0167607c2431970b0240a49109fa200b img-responsive" src="/assets/image_865383.jpg" title="DesignAutomationRevitPostman1" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49109bb200b-pi" style="display: inline;"><br /></a></p>
<p>同様に、Revit アドインのバンドルパッケージをローカルにダウンロードしておきます。</p>
<p><span style="color: #ff0000;">※右側のリンクからダウンロードした場合、システムの都合上、ファイル名が全て小文字になってしまいます。お手数をおかけしますが、それぞれ左のファイル名に名前を変更してご利用ください。</span></p>
<ul>
<li><a href="https://s3.amazonaws.com/revitio/documentation/SketchItApp.zip">SketchItApp.zip</a>（リンクが切れている場合は&#0160;<a href="https://adndevblog.typepad.com/files/sketchitapp.zip">こちら&#0160;</a>からダウンロードできます。）<a href="https://s3.amazonaws.com/revitio/documentation/SketchItApp.zip"></a><br /><br /></li>
</ul>
<p><strong>2. Authentication API によるアクセス トークンの取得</strong></p>
<p>まず、Design Automation API を利用するために、Authentication API の 2-legged 認証によって、Access Token（アクセス トークン） を取得します。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/authentication/v1/authenticate</strong>、メソッドは <strong>POST</strong> となります。</p>
<p>2-legged 認証のワークフローについては、<a href="https://adndevblog.typepad.com/technology_perspective/2016/08/oauth-authentication-scenario-on-forge.html#_2-legged">こちらのブログ記事（Forge での OAuth 認証シナリオ）</a>をご参照ください。</p>
<p>Postman コレクションの[Authentication]フォルダ配下にある [New token]リクエストを選択します。<br />すると、[New token]リクエストの画面がメイン画面に表示されます。</p>
<p>Headers タブをアクティブにすると、Content-Type パラメータに &quot;application/x-www-form-urlencoded&quot; という値が予め設定されております。</p>
<p>Body タブをアクティブにすると、client_id、client_secret、grant_type、scope の各パラメータが確認できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46c6d8a200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46c6d8a200d image-full img-responsive" src="/assets/image_911751.jpg" title="DesignAutomationRevitPostman2" /></a></p>
<p>この API のエンドポイントに POST リクエストを送信する際、Client ID と Client Secret は、Postman の環境変数に設定されている値が自動的に入力されるよう参照が設定されております。</p>
<p>そして、grant_type と scope というパラメータにも予め値が設定されています。</p>
<ul>
<li>grant_type は、サーバー間の認証の場合、<strong>client_credentials</strong> を指定します。</li>
<li>scope は、Design Automation API の場合、<strong>コードの生成と実行の権限に相当する、code:all を指定</strong>します。</li>
</ul>
<p>Access Token と Scope に関する解説は、<a href="https://adndevblog.typepad.com/technology_perspective/2018/11/about-access-token.html">こちらのブログ記事（Access Token について）</a>をご参照ください。</p>
<p>Send ボタンをクリックすると、リクエストが送信され、レスポンスには、アクセス トークンとその有効期限が返却されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4910ab0200b-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4910ab0200b image-full img-responsive" src="/assets/image_910323.jpg" title="DesignAutomationRevitPostman3" /></a></p>
<p>取得されたアクセス トークンは、環境変数に新たに dasApiToken パラメータとして追加されます。以降の Design Automaion API のリクエストには、この環境変数のパラメータを通じて、アクセス トークンが設定されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a443f4f0200c-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman2-2" class="asset  asset-image at-xid-6a0167607c2431970b0240a443f4f0200c img-responsive" src="/assets/image_381120.jpg" title="DesignAutomationRevitPostman2-2" /></a></p>
<p>&#0160;</p>
<p><strong>3. Nickname の作成</strong></p>
<p>Postman コレクションの [Nickname]フォルダ配下にある[Create nickname]リクエストを選択します。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/da/us-east/v3/forgeapps/me</strong>、メソッドは <strong>PATCH</strong> となります。</p>
<p>Headers タブをアクティブにすると、Content-Type パラメータに &quot;application/json&quot; という値が予め設定されております。また、Authorization パラメータには、&quot;Bearer {{dasApiToken}}&quot; として環境変数 &quot;dasApiToken&quot;を参照するよう設定されています。マウスカーソルを変数の上に移動すると、設定される実際の値がツールチップで表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46c6e39200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman4" class="asset  asset-image at-xid-6a0167607c2431970b0240a46c6e39200d img-responsive" src="/assets/image_900322.jpg" title="DesignAutomationRevitPostman4" /></a></p>
<p>Body タブをアクティブにすると、nickname パラメータの値に、環境変数&quot;dasNickName&quot;を参照するように設定されているのが確認できます。<br />レスポンスには、Status 200 OK が返却されます。</p>
<p>Nickname が作成できたら、環境変数&quot;dasNickName&quot;の CurrentValue にも同じ値をセットします。</p>
<p>&#0160;</p>
<p><strong>4. AppBundle の登録</strong></p>
<p>Postman コレクションの [SketchItApi]-&gt;[App]-&gt;[Create]フォルダ配下にある[Create app]リクエストを選択します。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/da/us-east/v3/appbundles</strong>、メソッドは <strong>POST</strong> となります。</p>
<p>Body タブでは、リクエストパラメータに、AppBundle の id、使用する engine、description を設定します。<br />それぞれの値は予め設定されています。<br />エンジンは、Revit アドインを実行する際に使用する Revit のバージョンを指定します。<br />現状の Design Automation API for Revit では、<strong>Revit 2018 と Revit 2019 に対応</strong>しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4434e15200c-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4434e15200c image-full img-responsive" src="/assets/image_393050.jpg" title="DesignAutomationRevitPostman6" /></a></p>
<p>レスポンスには、リクエストで送信したパラメータに加えて、<strong>AppBundle のバージョンと、Revit アドインのバンドルパッケージをアップロードするためのクラウドストレージの URL とそれに必要なパラメータ</strong>が返却されます。<br />初めて AppBundle を作成した場合、<strong>初回はバージョン 1 が自動的に付与</strong>されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46c6efe200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman7" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46c6efe200d image-full img-responsive" src="/assets/image_774298.jpg" title="DesignAutomationRevitPostman7" /></a></p>
<p>&#0160;</p>
<p><strong>5. Revit アドインのバンドルパッケージのアップロード</strong></p>
<p>Postman コレクションの [SketchItApi]-&gt;[App]-&gt;[Create]フォルダ配下にある[Upload app to Design Automation]リクエストを選択します。</p>
<p>エンドポイントは、AppBundle 登録時に返されたアップロード URLで、メソッドは POST となります。</p>
<p>Body タブでは、リクエストパラメータに、AppBundle 登録時のレスポンスで取得したパラメータがそれぞれ設定されています。<br />file パラメータの値フィールドにある Select Files ボタンをクリックして、アップロードする<strong> Revit アドインのバンドルパッケージを選択</strong>します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4434e52200c-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman8" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4434e52200c image-full img-responsive" src="/assets/image_521650.jpg" title="DesignAutomationRevitPostman8" /></a></p>
<p>レスポンスには、Status 200 OK が返却されます。</p>
<p>&#0160;</p>
<p><strong>6. AppBundle の Alias の登録</strong></p>
<p>Postman コレクションの [SketchItApi]-&gt;[App]-&gt;[Create]フォルダ配下にある[Create a new app alias]リクエストを選択します。</p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/da/us-east/v3/appbundles/SketchItApp/aliases</strong>、メソッドは <strong>POST</strong> となります。</p>
<p>Body タブでは、AppBundle のバージョンを指定して、そのバージョンに対するエイリアスを設定します。ここでは、&quot;test&quot;という Alias を登録します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46c6fdd200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman9" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46c6fdd200d image-full img-responsive" src="/assets/image_556329.jpg" title="DesignAutomationRevitPostman9" /></a></p>
<p>レスポンスには、Status 200 OK で設定した値が返却されます。</p>
<p>&#0160;</p>
<p><strong>7. Activity の登録</strong></p>
<p>エンドポイントは、<strong>https://developer.api.autodesk.com/da/us-east/v3/activities</strong>、メソッドは <strong>POST</strong> となります。</p>
<p>Body タブでは、JSON 形式で、<strong>Activity の定義</strong>を送信します。</p>
<p style="padding-left: 40px;">&quot;id&quot;: Activity を識別するための ID<br />&quot;commandLine&quot;: 実行するエンジンのパスと引数<br />&quot;parameters&quot;: 入出力のファイルに関する詳細な定義<br />&quot;engine&quot;: アプリケーションのエンジンとバージョン（Autodesk.ApplicationName+Version）<br />&quot;appbundles&quot;: AppBundle の指定（Nickname.AppBundleId.AliasId）<br />&quot;description&quot;: 説明</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a46c6f58200d-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman10" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a46c6f58200d image-full img-responsive" src="/assets/image_995779.jpg" title="DesignAutomationRevitPostman10" /></a></p>
<p>&quot;parameters&quot;の&quot;sketchItInput&quot;プロパティは、入力ファイルの定義です。<br />Design Automation API 側からみたときに、処理を実行するために取得する必要があることから、<strong>&quot;verb&quot;の値は、&quot;get&quot;を指定</strong>します。<br />SketchIt サンプルは、新規に Revit プロジェクト上に壁と床を作成するため、予め入力として Revit プロジェクトは必要ありません。</p>
<p>ただし、作成する壁や床の座標データを送る必要があります。<br />この座標データは、JSON 形式の文字列で WorkItem のリクエストパラメータで送信します。そして、送信された座標データは、<strong>JSON ファイルとして作業領域（ワーキングディレクトリ）に一時的に保存され、Revit アドインのプログラムから JSON ファイルを開いてデータにアクセスすることができます。</strong></p>
<p>Revit プロジェクトを入力として送信した場合も同様に、作業領域（ワーキングディレクトリ）に一時的に保存され、<strong>WorkItem の処理が終了したら、自動的に作業領域は削除されます。</strong></p>
<p>&quot;parameters&quot;の&quot;result&quot;プロパティは、出力ファイルの定義です。<br />出力結果を任意のクラウドストレージにアップロードすることから、<strong>&quot;verb&quot;の値は、&quot;put&quot;を指定</strong>します。</p>
<p>レスポンスには、Status 200 OK で設定した値が返却されます。</p>
<p>&#0160;</p>
<p><strong>8. Activity の Alias の登録</strong></p>
<p>AppBundle の Alias の登録と同様に、Activity にも Alias を登録します。ここでは、AppBundle の Alias と同じ &quot;test&quot; が設定されていますが、別の名称でも問題ありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4910c0c200b-pi" style="display: inline;"><img alt="DesignAutomationRevitPostman11" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4910c0c200b image-full img-responsive" src="/assets/image_350179.jpg" title="DesignAutomationRevitPostman11" /></a></p>
<p>レスポンスには、Status 200 OK で設定した値が返却されます。</p>
<p>これで、Design Automation API の Nickname、AppBundle と Activity の登録ができました。</p>
<p>次回は、WorkItem の作成、処理結果を出力するためのクラウドストレージの URL の取得、WorkItem の進捗確認と処理結果のダウンロードまでの動作を確認します。</p>
<p>By Ryuji Ogasawara</p>
