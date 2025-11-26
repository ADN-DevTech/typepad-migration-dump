---
layout: "post"
title: "Design Automation API for Revit - Learn Autodesk Forge チュートリアル .NET Core Sample の処理結果を Viewer で表示"
date: "2019-03-29 01:59:30"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample-part2.html "
typepad_basename: "design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample-part2"
typepad_status: "Publish"
---

<p>
<script src="https://adndevblog.typepad.com/files/run_prettify-3.js" type="text/javascript"></script>
</p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/03/design-automation-api-for-revit-learn-autodesk-forge-tutorial-sample-part1.html">前回</a>、&#0160;<strong><a href="https://learnforge.autodesk.io/#/ja-JP/">Learn Autodesk Forge（日本語）</a></strong>の Design Automation API のセクション、<a href="https://learnforge.autodesk.io/#/ja-JP/tutorials/modifymodels"><strong>モデルを修正する&#0160;セクション</strong></a>で公開されている <a href="https://github.com/Autodesk-Forge/learn.forge.designautomation">learn.forge.designautomation サンプルのソースコード</a>を入手して、ローカル環境で実行する手順をご紹介しました。</p>
<p>このサンプルでは、Design Autiomation API での処理結果を Revit プロジェクトファイルのダウンロードリンクを表示して終わってしまうので、ここでは、Bucket に保存されている Revit プロジェクトファイルを <strong>Model Derivative API で SVF に変換して、Forge Viewer に表示する</strong>部分を追記していきます。</p>
<p>まず前提となりますが、このサンプルは、<a href="https://docs.microsoft.com/ja-jp/aspnet/core/mvc/overview?view=aspnetcore-2.1"><strong>ASP.NET Core MVC</strong></a> を使用して実装されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4744800200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart2_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4744800200d image-full img-responsive" src="/assets/image_110504.jpg" title="LearnForgeTutorialPart2_1" /></a></p>
<p><strong>ASP.NET Core MVC</strong> は、モデル ビュー コントローラー デザイン パターンを使用して、Web アプリと API をビルドするための豊富なフレームワークです。<br />モデル ビュー コントローラー (MVC) アーキテクチャ パターンについては、<strong><a href="https://docs.microsoft.com/ja-jp/aspnet/core/mvc/overview?view=aspnetcore-2.1">こちらの記事</a></strong>を参照ください。</p>
<p><strong>1. (サーバーサイド) Model Derivative API の呼び出し処理を追加</strong></p>
<p>Bucket に保存されている Revit プロジェクトファイルを Forge Viewer で表示するには、<strong>Model Derivative API</strong> を使用して、事前に <strong>SVF ファイルに変換</strong>する必要があります。<br />このサンプルでは、コントローラーに該当する DesignAutomationController.cs から、Forge の API を呼び出しています。</p>
<p><strong>DesignAutomationController クラス</strong>では、<strong>属性ルーティング</strong>という方法によって、クライアントからのリクエストを振り分けています。</p>
<p>※<strong> 属性ルーティング</strong>については、<a href="https://docs.microsoft.com/ja-jp/aspnet/core/mvc/controllers/routing?view=aspnetcore-2.1"><strong>ASP.NET Core でのコントローラー アクションへのルーティングの解説ページ</strong></a>をご参照ください。</p>
<p>Visual Studio 2022 で forgeSample プロジェクトの Controllers フォルダ配下にある DesignAutomationController.cs を開き、<strong>DesignAutomationController クラス</strong>に、<strong>下記の 2つのメソッド（StartTranslation メソッドと GetManifest メソッド）</strong>を追加してください。</p>
<ul>
<li>StartTranslation メソッドは、Revit プロジェクトファイルを SVF に変換する処理を呼び出します。</li>
<li>GetManifest メソッドは、マニフェストファイルを取得して、変換処理の進捗状況を確認します。</li>
</ul>
<p>編集するファイル</p>
<ul>
<li>C:\Users\&lt;Windows ユーザ名&gt;\&lt;任意のディレクトリ&gt;\learn.forge.designautomation\forgeSample\Controllers\DesignAutomationController.cs</li>
</ul>
<pre class="prettyprint"><span style="color: #0000ff;"><strong>[HttpPost]
[HttpPost]
[Route(&quot;api/forge/modelderivative/job&quot;)]
public async Task&lt;IActionResult&gt; StartTranslation([FromBody]JObject translateSpecs)
{
	string urn = translateSpecs[&quot;urn&quot;].Value&lt;string&gt;();

	dynamic oauth = await OAuthController.GetInternalAsync();

	List&lt;JobPayloadItem&gt; outputs = new List&lt;JobPayloadItem&gt;()
	{
		new JobPayloadItem(
			JobPayloadItem.TypeEnum.Svf,
			new List&lt;JobPayloadItem.ViewsEnum&gt;()
			{
			JobPayloadItem.ViewsEnum._2d,
			JobPayloadItem.ViewsEnum._3d
			})
	};
	JobPayload job;
	job = new JobPayload(new JobPayloadInput(urn), new JobPayloadOutput(outputs));

	// start the translation
	DerivativesApi derivative = new DerivativesApi();
	derivative.Configuration.AccessToken = oauth.access_token;
	dynamic jobPosted = await derivative.TranslateAsync(job);

	return Ok();
}

[HttpGet]
[Route(&quot;api/forge/modelderivative/manifest&quot;)]
public async Task&lt;IActionResult&gt; GetManifest([FromQuery]string urn)
{
	DerivativesApi derivative = new DerivativesApi();
	dynamic result = await derivative.GetManifestAsyncWithHttpInfo(urn);

	return Ok(new { Status = (string)result.Data.status, Progress = (string)result.Data.progress });
}
</strong></span></pre>
<p>&#0160;</p>
<p><strong>2. (サーバーサイド) WorkItem のコールバック処理で Revit プロジェクトファイルの objectId を返却</strong></p>
<p>Design Automation API の WorkItem を POST する際に、<strong>&quot;onComplete&quot;</strong> という引数に<strong>コールバック URL</strong> を設定することができます。<br />この引数を事前に設定しておけば、WorkItem の完了時に、指定の URL を自動的に呼び出してくれます。詳細は、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/revit/step6-post-workitem/"><strong>こちらのページ</strong></a>に記載されています。</p>
<p>このサンプルでは、該当の処理が <strong>DesignAutomationController クラス</strong>の中に実装されています。<br />変数 callbackUrl で設定している<strong>コールバック URL</strong> には、クエリパラメータが組み込まれていますが、<strong>Model Derivative API による変換処理</strong>を実行するためには、<strong>BucketKey</strong> が必要になります。<br />StartWorkitem メソッドの下記の変数 callbackUrl で、3つ目のクエリパラメータとして、<strong>bucketKey</strong> を追加してください。</p>
<p>編集するファイル</p>
<ul>
<li>C:\Users\&lt;Windows ユーザ名&gt;\&lt;任意のディレクトリ&gt;\learn.forge.designautomation\forgeSample\Controllers\DesignAutomationController.cs</li>
</ul>
<pre class="prettyprint">// prepare &amp; submit workitem
string callbackUrl = string.Format(&quot;{0}/api/forge/callback/designautomation?id={1}&amp;outputFileName={2}<strong><span style="color: #0000ff;">&amp;bucketKey={3}</span></strong>&quot;, OAuthController.GetAppSetting(&quot;FORGE_WEBHOOK_URL&quot;), browerConnectionId, outputFileNameOSS, <strong><span style="color: #0000ff;">bucketKey</span></strong>);
WorkItem workItemSpec = new WorkItem()
{
	ActivityId = activityName,
	Arguments = new Dictionary&lt;string, IArgument&gt;()
	{
		{ &quot;inputFile&quot;, inputFileArgument },
		{ &quot;inputJson&quot;,  inputJsonArgument },
		{ &quot;outputFile&quot;, outputFileArgument },
		{ &quot;onComplete&quot;, new XrefTreeArgument { Verb = Verb.Post, Url = callbackUrl } }
	}
};
</pre>
<p>そして、コールバック URLがリクエストされた際に呼び出される <strong>OnCallback メソッド</strong>では、<strong>引数で bucketKey を取得する</strong>ように変更します。<br />OnCallback メソッドの最後に、<strong>Bucket に保存されている Revit プロジェクトファイルの objectId を取得し、Base64 エンコードした文字列をクライアントに返す処理</strong>を追加してください。</p>
<p>Forge Viewer の表示に必要な <strong>objectId</strong> については、<strong><a href="https://forge.autodesk.com/en/docs/model-derivative/v2/tutorials/prepare-file-for-viewer/">こちらのページ</a></strong>をご参照ください。</p>
<p>まず、名前空間 <strong>using System.Text;</strong> をインポートしてください。</p>
<p>編集するファイル</p>
<ul>
<li>C:\Users\&lt;Windows ユーザ名&gt;\&lt;任意のディレクトリ&gt;\learn.forge.designautomation\forgeSample\Controllers\DesignAutomationController.cs</li>
</ul>
<pre class="prettyprint">///
/// Callback from Design Automation Workitem (onProgress or onComplete)
/// 
[HttpPost]
[Route(&quot;/api/forge/callback/designautomation&quot;)]
public async Task&lt;IActionResult&gt; OnCallback(string id, string outputFileName, <strong><span style="color: #0000ff;">string bucketKey</span></strong>, [FromBody]dynamic body)
{
	try
	{
		// your webhook should return immediately! we can use Hangfire to schedule a job
		JObject bodyJson = JObject.Parse((string)body.ToString());
		await _hubContext.Clients.Client(id).SendAsync(&quot;onComplete&quot;, bodyJson.ToString());

		var client = new RestClient(bodyJson[&quot;reportUrl&quot;].Value&lt;string&gt;());
		var request = new RestRequest(string.Empty);

		byte[] bs = client.DownloadData(request);
		string report = System.Text.Encoding.Default.GetString(bs);
		await _hubContext.Clients.Client(id).SendAsync(&quot;onComplete&quot;, report);

		ObjectsApi objectsApi = new ObjectsApi();
		dynamic signedUrl = await objectsApi.CreateSignedResourceAsyncWithHttpInfo(NickName.ToLower() + &quot;_designautomation&quot;, outputFileName, new PostBucketsSigned(10), &quot;read&quot;);
		await _hubContext.Clients.Client(id).SendAsync(&quot;downloadResult&quot;, (string)(signedUrl.Data.signedUrl));

		<strong><span style="color: #0000ff;">dynamic objectDetail = await objectsApi.GetObjectDetailsAsyncWithHttpInfo(bucketKey, outputFileName);
		Encoding encoding = Encoding.UTF8;
		string objectUrn = (string)(objectDetail.Data.objectId);
		byte[] bytes = encoding.GetBytes(objectUrn);
		string urn = System.Convert.ToBase64String(bytes);
		await _hubContext.Clients.Client(id).SendAsync(&quot;translateResult&quot;, urn);</span></strong>
	}
	catch (Exception e) { }

	// ALWAYS return ok (200)
	return Ok();
}
</pre>
<p>ここで追加した処理の中で、最後に <strong>SendAsync() メソッド</strong>を呼び出していることがわかります。<br />これは、非同期でクライアントにレスポンスを返す処理ですが、このサンプルでは、<a href="https://docs.microsoft.com/ja-jp/aspnet/core/signalr/introduction?view=aspnetcore-2.1"><strong>ASP.NET Core SignalR</strong></a> という仕組みを利用して、プッシュ通知を実装しています。</p>
<p><strong><a href="https://docs.microsoft.com/ja-jp/aspnet/core/signalr/introduction?view=aspnetcore-2.1">ASP.NET Core SignalR</a> は、アプリへのリアルタイム Web 機能の追加を簡素化するオープン ソース ライブラリです。 </strong><br /><strong>リアルタイム Web 機能は、サーバー側コードからクライアントにコンテンツを即座にプッシュすることを可能にします。</strong></p>
<p>SignalR を利用してサーバーが <a href="https://docs.microsoft.com/ja-jp/aspnet/core/signalr/hubs?view=aspnetcore-2.1"><strong>SignalR ハブ</strong></a>を作成すると、<strong>サーバーは、このハブに接続しているクライアントとリアルタイムに通信することができる</strong>ようになります。<br />サーバー コードでは、クライアントによって呼び出されるメソッドを定義します。 <br />クライアント コードでは、サーバーから呼び出されるメソッドを定義します。</p>
<p>このサンプルでは、SendAsync() メソッドの第1引数で指定している文字列は、クライアントサイドの JavaScript で呼び出すメソッドに対応しています。</p>
<p>&#0160;</p>
<p><strong>3. (クライアントサイド) SignalR ハブで呼び出されるメソッドで、SVF 変換ボタンを追加</strong></p>
<p>クライアントサイドの処理は、<strong>ForgeDesignAutomation.js</strong> に実装されています。<br />ForgeDesignAutomation.js を開いて、startConnection() メソッドの中に、下記の <strong>translateResult メソッド</strong>を追加してください。</p>
<p>編集するファイル</p>
<ul>
<li>C:\Users\&lt;Windows ユーザ名&gt;\&lt;任意のディレクトリ&gt;\learn.forge.designautomation.modified\forgeSample\wwwroot\js\ForgeDesignAutomation.js</li>
</ul>
<pre class="prettyprint">function startConnection(onReady) {
    if (connection &amp;&amp; connection.connectionState) { if (onReady) onReady(); return; }
    connection = new signalR.HubConnectionBuilder().withUrl(&quot;/api/signalr/designautomation&quot;).build();
    connection.start()
        .then(function () {
            connection.invoke(&#39;getConnectionId&#39;)
                .then(function (id) {
                    connectionId = id; // we&#39;ll need this...
                    if (onReady) onReady();
                });
        });

    connection.on(&quot;downloadResult&quot;, function (url) {
        writeLog(&#39;&lt;a href=&quot;&#39; + url +&#39;&quot;&gt;Download result file here&lt;/a&gt;&#39;);
    });

<span style="color: #0000ff;"><strong>    connection.on(&quot;translateResult&quot;, function (urn) {
        writeLog(&#39;&lt;button class=&quot;btn btn-primary btn-start-translation&quot; data-object-urn=&quot;&#39; + urn + &#39;&quot; style=&quot;width: 200px;margin: 5px 0px;&quot;&gt;Start Translation&lt;/button&gt;&#39;);
    });</strong></span>

    connection.on(&quot;onComplete&quot;, function (message) {
        writeLog(message);
    });
}
</pre>
<p>これで、WorkItem のコールバック処理から、クライアントサイドの translateResult メソッドが呼び出され、画面の出力コンポーネントに SVF 変換ボタンを追加することができました。</p>
<p>WorkItem のコールバック処理で、SendAsync() メソッドの第2引数で渡した <strong>Revit プロジェクトファイルの objectId (Base64 エンコード済み)</strong>は、メソッドの引数で受け取り、<a href="http://www.html5.jp/tag/attributes/data.html"><strong>独自データ属性</strong></a>によって、ボタンの属性データに保持させておきます。<br />この値は、以降に追加するボタンにも渡していきます。</p>
<p>&#0160;</p>
<p><strong>4. (クライアントサイド) SVF 変換とマニフェスト取得のリクエスト処理を追加</strong></p>
<p>ForgeDesignAutomation.js に、<strong>SVF 変換ボタンをクリックした際に呼び出されるリクエストの処理</strong>を追加します。<br />jQuery.ajax の success コールバックメソッドで、変換処理の完了時に、進捗状況を確認するためのボタンを追加しています。</p>
<p>編集するファイル</p>
<ul>
<li>C:\Users\&lt;Windows ユーザ名&gt;\&lt;任意のディレクトリ&gt;\learn.forge.designautomation.modified\forgeSample\wwwroot\js\ForgeDesignAutomation.js</li>
</ul>
<pre class="prettyprint"><span style="color: #0000ff;"><strong>function startTranslation(e) {
    var urn = $(e.currentTarget).data(&#39;object-urn&#39;);

    writeLog(&#39;Start translation: &#39; + urn);

    jQuery.ajax({
        url: &#39;api/forge/modelderivative/job&#39;,
        method: &#39;POST&#39;,
        contentType: &#39;application/json&#39;,
        data: JSON.stringify({
            urn: urn
        }),
        success: function (res) {
            writeLog(&#39;&lt;button class=&quot;btn btn-primary btn-get-manifest&quot; data-object-urn=&quot;&#39; + urn + &#39;&quot; style=&quot;width: 200px;margin: 5px 0px;&quot;&gt;Get Manifest&lt;/button&gt;&#39;);
        }
    });
}
</strong></span></pre>
<p><strong>マニフェストを取得するメソッド</strong>も追加します。<br />jQuery.ajax の success コールバックメソッドでは、進捗状況を出力し、変換処理が完了していれば、 Viewer を起動するボタンを追加します。</p>
<pre class="prettyprint"><span style="color: #0000ff;"><strong>function getManifest(e) {

    var urn = $(e.currentTarget).data(&#39;object-urn&#39;);

    jQuery.ajax({
        url: &#39;api/forge/modelderivative/manifest&#39;,
        method: &#39;GET&#39;,
        data: {
            urn: urn
        },
        success: function (res) {
            writeLog(&#39;Translation Status: &#39; + res.status + &#39;, Progress: &#39; + res.progress);

            if (res.status == &#39;success&#39;) {
                writeLog(&#39;&lt;button class=&quot;btn btn-primary btn-launch-viewer&quot; data-object-urn=&quot;&#39; + urn + &#39;&quot; style=&quot;width: 200px;margin: 5px 0px;&quot;&gt;Launch Viewer&lt;/button&gt;&#39;);
                $(&#39;#launchViewer&#39;).click(launchViewer);
            } 
        }
    });
}
</strong></span></pre>
<p>&#0160;</p>
<p><strong>5. (クライアントサイド) 各ボタンにクリックイベントを追加</strong></p>
<p>「Start Translation」ボタン、「Get Manifest」ボタン、「Launch Viewer」ボタンに<strong>クリックイベント</strong>を追加して、それぞれに対応するメソッドを呼び出す処理を記述してください。。</p>
<p>これらのボタンは、<strong>WorkItem を作成する度に動的に追加され</strong>ていくため、出力コンポーネントに同じボタンが複数表示される可能性があります。</p>
<p>そのため、ドキュメントロード時に、<strong>.on( events [, selector ] [, data ], handler )メソッド</strong>を実行して、予めボタン要素にイベントハンドラをアタッチしておきます。<br /><a href="https://api.jquery.com/on/"><strong>.on()メソッド</strong></a>は、マッチした要素に任意のイベントをバインドするメソッドですが、対象となるエレメントは現在マッチしているものも含め、<strong>将来的にマッチするものも対象</strong>となります。</p>
<p>編集するファイル</p>
<ul>
<li>C:\Users\&lt;Windows ユーザ名&gt;\&lt;任意のディレクトリ&gt;\learn.forge.designautomation.modified\forgeSample\wwwroot\js\ForgeDesignAutomation.js</li>
</ul>
<pre class="prettyprint">$(document).ready(function () {
    prepareLists();

    $(&#39;#clearAccount&#39;).click(clearAccount);
    $(&#39;#defineActivityShow&#39;).click(defineActivityModal);
    $(&#39;#createAppBundleActivity&#39;).click(createAppBundleActivity);
    $(&#39;#startWorkitem&#39;).click(startWorkitem);

<strong><span style="color: #0000ff;">    $(&#39;#outputlog&#39;).on(&quot;click&quot;, &#39;.btn-start-translation&#39;, startTranslation);
    $(&#39;#outputlog&#39;).on(&quot;click&quot;, &#39;.btn-get-manifest&#39;, getManifest);
    $(&#39;#outputlog&#39;).on(&quot;click&quot;, &#39;.btn-launch-viewer&#39;, launchViewer);</span></strong>

    startConnection();
});
</pre>
<p>&#0160;</p>
<p><strong>6. (クライアントサイド) Forge Viewer コンポーネントの追加</strong></p>
<p>index.html を開き、<strong>Forge Viewer のライブラリ</strong>を読み込んでください。<br />そして、forgeViewer という ID を持つ <strong>DIV タグ</strong>を追加してください。</p>
<p>編集するファイル</p>
<ul>
<li>C:\Users\ogasawr\GitHub Repo\learn.forge.designautomation\forgeSample\wwwroot\index.html</li>
</ul>
<pre class="prettyprint">&lt;head&gt;
    &lt;title&gt;Autodesk Forge - Design Automation&lt;/title&gt;
	
    ～中略～

<span style="color: #0000ff;"><strong>    &lt;!-- Autodesk Forge Viewer files --&gt;
    &lt;meta name=&quot;viewport&quot; content=&quot;width=device-width, minimum-scale=1.0, initial-scale=1, user-scalable=no&quot; /&gt;
    &lt;meta charset=&quot;utf-8&quot;&gt;

    &lt;link rel=&quot;stylesheet&quot; href=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/style.min.css&quot; type=&quot;text/css&quot;&gt;
    &lt;script src=&quot;https://developer.api.autodesk.com/modelderivative/v2/viewers/7.*/viewer3D.min.js&quot;&gt;&lt;/script&gt;

    &lt;style&gt;
        body {
            margin: 0;
        }

        #forgeViewer {
            width: 100%;
            height: 100%;
            margin: 0;
            background-color: #F0F8FF;
        }
    &lt;/style&gt;</strong></span>
&lt;/head&gt;
</pre>
<pre class="prettyprint">&lt;div class=&quot;container-fluid&quot; style=&quot;margin-top: 70px;&quot;&gt;
	&lt;div class=&quot;row&quot;&gt;
		&lt;div class=&quot;col-sm-4&quot;&gt;
			&lt;div class=&quot;form-group&quot;&gt;
				&lt;label for=&quot;width&quot;&gt;Width:&lt;/label&gt;
				&lt;input type=&quot;number&quot; class=&quot;form-control&quot; id=&quot;width&quot; placeholder=&quot;Enter new width value&quot;&gt;
			&lt;/div&gt;
			&lt;div class=&quot;form-group&quot;&gt;
				&lt;label for=&quot;height&quot;&gt;Height:&lt;/label&gt;
				&lt;input type=&quot;number&quot; class=&quot;form-control&quot; id=&quot;height&quot; placeholder=&quot;Enter new height value&quot;&gt;
			&lt;/div&gt;

			&lt;div class=&quot;form-group&quot;&gt;
				&lt;label for=&quot;inputFile&quot;&gt;Input file&lt;/label&gt;
				&lt;input type=&quot;file&quot; class=&quot;form-control-file&quot; id=&quot;inputFile&quot;&gt;
			&lt;/div&gt;
			&lt;div class=&quot;form-group&quot;&gt;
				&lt;label for=&quot;activity&quot;&gt;Existing activities&lt;/label&gt;
				&lt;select class=&quot;form-control&quot; id=&quot;activity&quot;&gt; &lt;/select&gt;
			&lt;/div&gt;
			&lt;center&gt;&lt;button class=&quot;btn btn-primary&quot; id=&quot;startWorkitem&quot;&gt;Start workitem&lt;/button&gt;&lt;/center&gt;&lt;br /&gt;
		&lt;/div&gt;
		&lt;div class=&quot;col-sm-8&quot;&gt;
			&lt;pre id=&quot;outputlog&quot; style=&quot;height: calc(100vh - 120px); overflow-y: scroll;&quot;&gt;&lt;/pre&gt;
		&lt;/div&gt;
	&lt;/div&gt;
	<span style="color: #0000ff;"><strong>&lt;div class=&quot;row&quot;&gt;
		&lt;div class=&quot;col-sm-12&quot; style=&quot;height: calc(100vh - 120px)&quot;&gt;
			&lt;div id=&quot;forgeViewer&quot;&gt;&lt;/div&gt;
		&lt;/div&gt;
	&lt;/div&gt;</strong></span>
&lt;/div&gt;
</pre>
<p>&#0160;</p>
<p><strong>7. (クライアントサイド) Forge Viewer の初期化処理を追加</strong></p>
<p>ForgeDesignAutomation.js に、下記の <strong>Forge Viewer の初期化処理</strong>を追加してください。<br />Forge Viewer を起動するためには、<strong>アクセストークン</strong>と <strong>Revit プロジェクトファイルの objectId (Base64 エンコード済み)</strong>が必要になります。<br />前者は、既に値を取得し、ボタンの独自データ属性として保持していますが、アクセストークンを取得する処理はまだ実装していません。</p>
<p>編集するファイル</p>
<ul>
<li>C:\Users\&lt;Windows ユーザ名&gt;\&lt;任意のディレクトリ&gt;\learn.forge.designautomation\forgeSample\wwwroot\js\ForgeDesignAutomation.js</li>
</ul>
<pre class="prettyprint"><span style="color: #0000ff;"><strong>var viewer;

function launchViewer(e) {

    var urn = $(e.currentTarget).data(&#39;object-urn&#39;);

    writeLog(&#39;launchViewer: &#39; + urn);

    var options = {
        env: &#39;AutodeskProduction2&#39;,
        api: &#39;streamingV2&#39;,  // for models uploaded to EMEA change this option to &#39;streamingV2_EU&#39;
        getAccessToken: getForgeToken
    };

    var documentId = &#39;urn:&#39; + urn;

    Autodesk.Viewing.Initializer(options, function () {

        var htmlDiv = document.getElementById(&#39;forgeViewer&#39;);
        viewer = new Autodesk.Viewing.GuiViewer3D(htmlDiv);
        var startedCode = viewer.start();
        if (startedCode &gt; 0) {
            console.error(&#39;Failed to create a Viewer: WebGL not supported.&#39;);
            return;
        }

        console.log(&#39;Initialization complete, loading a model next...&#39;);

        Autodesk.Viewing.Document.load(documentId, onDocumentLoadSuccess, onDocumentLoadFailure);
    });
}

function onDocumentLoadSuccess(viewerDocument) {
    var defaultModel = viewerDocument.getRoot().getDefaultGeometry();
    viewer.loadDocumentNode(viewerDocument, defaultModel);
}

function onDocumentLoadFailure() {
    console.error(&#39;Failed fetching Forge manifest&#39;);
}

function getForgeToken(callback) {
    jQuery.ajax({
        url: &#39;/api/forge/oauth/token&#39;,
        success: function (res) {
            callback(res.access_token, res.expires_in);
        }
    });
}
</strong></span></pre>
<p>&#0160;</p>
<p><strong>8. (サーバーサイド) Authentication API で、パブリックトークンを取得</strong></p>
<p>このサンプルでは、Design Automation API と Data Management API を利用するために、サーバーサイドでのみ利用することを前提として、アクセストークンを取得するように実装されています。</p>
<p><strong>Forge を利用するにあたっては、セキュリティの観点から、サーバーサイドで利用するアクセストークンと、クライアントサイドで利用するアクセストークンを、それぞれ分けて利用するよう推奨しております。</strong></p>
<p>そして、前者をインターナルトークン、後者をパブリックトークンとして、それぞれに異なるスコープを割り当てます。<br />今回の場合は、インターナルトークンのスコープには下記のスコープを許可しています。</p>
<ul>
<li><strong>Scope.BucketCreate, Scope.BucketRead, Scope.BucketDelete, Scope.DataRead, Scope.DataWrite, Scope.DataCreate, Scope.CodeAll</strong></li>
</ul>
<p>Forge Viewer で表示する上では、これらのスコープは必要なく、最低限の<strong> Scope.ViewablesRead</strong> だけが割り当てられていれば問題ありません。</p>
<p>OAuthController.cs を開いてください。<br />ここでは、インターナルトークンのみが実装されていることがわかります。</p>
<p>編集するファイル</p>
<ul>
<li>C:\Users\&lt;Windows ユーザ名&gt;\&lt;任意のディレクトリ&gt;\learn.forge.designautomation\forgeSample\Controllers\OAuthController.cs</li>
</ul>
<p>下記のように、<strong>変数 PublicToken と、クライアントサイドに返却する処理 GetPublicAsync メソッド</strong>を追加してください。</p>
<pre class="prettyprint">private static dynamic InternalToken { get; set; }
<span style="color: #0000ff;"><strong>private static dynamic PublicToken { get; set; }</strong>

<strong>[HttpGet]
[Route(&quot;api/forge/oauth/token&quot;)]
public async Task&lt;dynamic&gt; GetPublicAsync()
{
	if (PublicToken == null || PublicToken.ExpiresAt &lt; DateTime.UtcNow)
	{
		PublicToken = await Get2LeggedTokenAsync(new Scope[] { Scope.ViewablesRead });
		PublicToken.ExpiresAt = DateTime.UtcNow.AddSeconds(PublicToken.expires_in);
	}
	return PublicToken;
}
</strong></span></pre>
<p>これで準備は完了です。</p>
<p>&#0160;</p>
<p><strong>9. サンプルの実行</strong></p>
<p>前回と同じ手順でサンプルを実行してみましょう。</p>
<p><strong>コマンド プロンプト</strong> を起動し、CD コマンドで<strong> ngrok.exe が保存されているディレクトリ</strong>に移動します。<br />そして、下記のコマンドを実行してください。</p>
<ul>
<li>
<p><strong>ngrok http 3000 --host-header=localhost:3000 --scheme http</strong></p>
</li>
</ul>
<p>ngrok ツールを再起動すると、<strong>割り当てられたアドレスも変更します</strong>ので、新しく割り当てられたアドレスを<strong>環境変数 FORGE_WEBHOOK_URL</strong> に設定してください。</p>
<p>WorkItem の処理が終了すると、「Download result file here」リンクの後に、<strong>「Start Translation」ボタン</strong>が表示されるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4744b63200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart2_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4744b63200d image-full img-responsive" src="/assets/image_915910.jpg" title="LearnForgeTutorialPart2_2" /></a></p>
<p>「Start Translation」ボタンをクリックすると、SVF 変換処理が始まり、<strong>「Get Manifest」ボタン</strong>が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4744b6f200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart2_3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4744b6f200d image-full img-responsive" src="/assets/image_804151.jpg" title="LearnForgeTutorialPart2_3" /></a></p>
<p>「Get Manifest」ボタンをクリックして進捗状況を取得し、処理が完了していれば、<strong>「Launch Viewer」ボタン</strong>が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4744b78200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart2_4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4744b78200d image-full img-responsive" src="/assets/image_176627.jpg" title="LearnForgeTutorialPart2_4" /></a></p>
<p>「Launch Viewer」ボタンをクリックすると、Forge Viewer が起動して、Revit プロジェクトファイルを表示することができます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4744b89200d-pi" style="display: inline;"><img alt="LearnForgeTutorialPart2_5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4744b89200d image-full img-responsive" src="/assets/image_481027.jpg" title="LearnForgeTutorialPart2_5" /></a></p>
<p>今回は、learn.forge.designautomation サンプルに、Bucket に保存されている Revit プロジェクトファイルを Model Derivative API で変換して、Forge Viewer に表示する部分を追加しました。</p>
<p>&#0160;<strong><a href="https://learnforge.autodesk.io/#/ja-JP/">Learn Autodesk Forge（日本語）</a></strong>チュートリアルの <a href="https://learnforge.autodesk.io/#/ja-JP/tutorials/viewmodels"><strong>モデルを表示する セクション</strong></a>では、別の Forge アプリとして、Bucket に保存されている Revit プロジェクトファイルを Model Derivative API で変換して、Forge Viewer に表示するサンプルが公開されています。</p>
<p><strong>Bucket のファイルをツリー構造のコンポーネントで表示する ForgeTree.js</strong> も参考になりますので、ご興味ある方はぜひお試しください。</p>
<p>By Ryuji Ogasawara</p>
