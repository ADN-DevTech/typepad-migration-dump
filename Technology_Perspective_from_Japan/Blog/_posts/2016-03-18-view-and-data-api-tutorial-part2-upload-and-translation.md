---
layout: "post"
title: "View and Data API チュートリアル ～ その2 ～ アップロードと変換"
date: "2016-03-18 01:20:29"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part2-upload-and-translation.html "
typepad_basename: "view-and-data-api-tutorial-part2-upload-and-translation"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span></p>
<p><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part1-nodejs-basic.html" target="_blank"><strong>前回の内容</strong></a> を拡張して、ファイルのアップロード処理とストリーミング配信用の変換処理を実装していきます。なお、ここで紹介する内容は、<strong><a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data" target="_blank">Autodesk View &amp; Data API – Getting Started Tutorial リポジトリ</a></strong>&#0160;の<strong> <a href="https://github.com/Developer-Autodesk/tutorial-getting.started-view.and.data/blob/master/chapter-2.md#Chapter2&#0160;" target="_blank">Chapter 2</a></strong>&#0160;に該当するものです。Chapter 2 では、ファイルをアップロードしてストリーミング配信用に変換する処理には、クライアント側処理 -&#0160;<a href="https://github.com/Developer-Autodesk/viewer-javascript-tutorial/blob/master/chapters/chapter-2a.md#Chapter2a" target="_blank">Translating from the client&#0160;</a>とサーバー側処理 - <a href="https://github.com/Developer-Autodesk/viewer-javascript-tutorial/blob/master/chapters/chapter-2b.md#Chapter2b" target="_blank">Translating from the server</a>&#0160;の 2 通りのオプションが用意されていますが、ここでは手順の少ないクライアント側処理を実装していきます。セキュリティを考慮すると、すべてサーバー側処理で実装するのが&#0160;望ましい点に留意してください。</p>
<ol>
<li>&#0160;Adobe Brackets を使って新規に&#0160;upload.html と upload.js の 2 つのファイルを作成して www フォルダに保存してください。各ファイルの内容は、次のとおりです。<br /><strong>upload.html：</strong><br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;"><code>&lt;html&gt;
&lt;head&gt;
    &lt;title&gt;ADN Viewer Demo (client upload)&lt;/title&gt;
    &lt;link rel=&quot;shortcut icon&quot; href=&quot;/images/favicon.ico&quot; type=&quot;image/x-icon&quot; /&gt;

    &lt;!-- jquery --&gt;
    &lt;script src=&quot;https://code.jquery.com/jquery-2.1.2.min.js&quot;&gt;&lt;/script&gt;

    &lt;!-- Bootstrap CSS --&gt;
    &lt;link href=&quot;http://netdna.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css&quot; rel=&quot;stylesheet&quot; /&gt;

    &lt;!-- Autodesk.ADN.Toolkit.Viewer --&gt;
    &lt;script src=&quot; https://rawgit.com/Developer-Autodesk/library-javascript-view.and.data.api/master/js/Autodesk.ADN.Toolkit.ViewData.js&quot;&gt;&lt;/script&gt;
    &lt;script src=&quot;/upload.js&quot;&gt;&lt;/script&gt;
&lt;/head&gt;

&lt;body&gt;

&lt;/body&gt;
&lt;/html&gt;</code></span></pre>
<strong>upload.js:</strong><br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;"><code>var oViewDataClient =null ;

$(document).ready (function () {
    oViewDataClient =new Autodesk.ADN.Toolkit.ViewData.AdnViewDataClient (
        &#39;https://developer.api.autodesk.com&#39;,
        &#39;http://&#39; + window.location.host + &#39;/api/token&#39;
    ) ;
}) ;</code></span></pre>
</li>
<li>作成した <strong>upload.html</strong> は、まだブランク ページです。ここで、ストリーミング配信するファイルをアップロードするため、各種のコントロールを追記します。<strong>upload.html</strong> の <strong>&lt;body&gt;</strong> ～ <strong>&lt;/body&gt;</strong> タグの間に、次のコードを追記してください。<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;"><code>&lt;div class=&quot;container&quot;&gt;
        &lt;div class=&quot;panel panel-default&quot;&gt;
            &lt;div class=&quot;panel-heading&quot;&gt;
                &lt;h3 class=&quot;panel-title&quot;&gt;Upload and translate a file&lt;/h3&gt;
            &lt;/div&gt;
            &lt;div class=&quot;panel-body&quot;&gt;
                &lt;input class=&quot;form-control&quot; type=&quot;file&quot; id=&quot;files&quot; name=&quot;files&quot; multiple /&gt;
                &lt;br /&gt;
                &lt;div style=&quot;text-align: center;&quot;&gt;
                    &lt;a class=&quot;btn btn-primary&quot; id=&quot;btnTranslateThisOne&quot;&gt;Translate this one for me&lt;/a&gt;
                &lt;/div&gt;

                &lt;br /&gt;
                &lt;div id=&quot;msg&quot;&gt;&lt;/div&gt;
            &lt;/div&gt;
        &lt;/div&gt;
    &lt;/div&gt;

    &lt;div class=&quot;container&quot;&gt;
        &lt;div class=&quot;panel panel-default&quot;&gt;
            &lt;div class=&quot;panel-heading&quot;&gt;
                &lt;h3 class=&quot;panel-title&quot;&gt;My URNs&lt;/h3&gt;
            &lt;/div&gt;
            &lt;div class=&quot;panel-body&quot;&gt;
                &lt;div class=&quot;row&quot;&gt;
                    &lt;div class=&quot;col-md-8&quot;&gt;
                        &lt;input class=&quot;form-control&quot; type=&quot;text&quot; id=&quot;urn&quot; name=&quot;urn&quot; value=&quot;&quot; /&gt;
                    &lt;/div&gt;
                    &lt;div class=&quot;col-md-4&quot;&gt;
                        &lt;a class=&quot;btn btn-primary&quot; id=&quot;btnAddThisOne&quot;&gt;Add to the list&lt;/a&gt;
                    &lt;/div&gt;
                &lt;/div&gt;

                &lt;br /&gt;
                &lt;legend&gt;My URN list&lt;/legend&gt;
                &lt;div&gt;Click on a urn below to launch the viewer&lt;/div&gt;
                &lt;div id=&quot;list&quot;&gt;&lt;/div&gt;
            &lt;/div&gt;
        &lt;/div&gt;
    &lt;/div&gt;</code></span></pre>
</li>
<li><strong>upload.html</strong> 追記したコントロールの振る舞いを定義する処理を、<strong>upload.js</strong> に<strong>太字</strong>部分を追記します。追記する位置に分かりにくいので、1. で記入した部分を<span style="background-color: #b9b9b9;">グレー背景</span>で色分けをしています。また、青字で書かれた <span style="color: #0000ff;"><strong>&lt;my_consumer_key&gt;</strong></span> の部分は、一意なバケット名を作成するための変数代入の設定です。この部分は、お手持ちの <strong>Consumer Key</strong>&#0160;の値で置き換えてみてください（Consumer Secret ではなく）。<br />
<pre><span style="font-family: tahoma, arial, helvetica, sans-serif;"><code><span style="background-color: #b9b9b9;">var oViewDataClient =null ;

$(document).ready (function () {
    oViewDataClient =new Autodesk.ADN.Toolkit.ViewData.AdnViewDataClient (
        &#39;https://developer.api.autodesk.com&#39;,
        &#39;http://&#39; + window.location.host + &#39;/api/token&#39;
    ) ;</span>

<span style="color: #000000;"><strong>    $(&#39;#btnTranslateThisOne&#39;).click (function (evt) {
        var files =document.getElementById(&#39;files&#39;).files ;
        if ( files.length == 0 )
            return ;
        var bucket =
            &#39;model&#39;
            + new Date ().toISOString ().replace (/T/, &#39;-&#39;).replace (/:+/g, &#39;-&#39;).replace (/\..+/, &#39;&#39;)
            + &#39;-&#39; + &#39;<span style="color: #0000ff;">&lt;my_consumer_key&gt;</span>&#39;.toLowerCase ().replace (/\W+/g, &#39;&#39;) ;

        createBucket (bucket, files)
    }) ;

    $(&#39;#btnAddThisOne&#39;).click (function (evt) {
        var urn =$(&#39;#urn&#39;).val ().trim () ;
        if ( urn == &#39;&#39; )
            return ;
        AddThisOne (urn) ;
    }) ;</strong></span>
<span style="background-color: #b9b9b9;">}) ;</span>

<span style="color: #000000;"><strong>function AddThisOne (urn) {
    var id =urn.replace (/=+/g, &#39;&#39;) ;
    $(&#39;#list&#39;).append (&#39;&lt;div class=&quot;list-group-item row&quot;&gt;&#39;
            + &#39;&lt;button id=&quot;&#39; + id + &#39;&quot; type=&quot;text&quot; class=&quot;form-control&quot;&gt;&#39; + urn + &#39;&lt;/button&gt;&#39;
        + &#39;&lt;/div&gt;&#39;
    ) ;
    $(&#39;#&#39; + id).click (function (evt) {
        window.open (&#39;/?urn=&#39; + $(this).text (), &#39;_blank&#39;) ;
    }) ;
}

function createBucket (bucket, files) {
    var bucketData ={
        bucketKey: bucket,
        servicesAllowed: {},
        policy: &#39;transient&#39;
    } ;
    oViewDataClient.createBucketAsync (
        bucketData,
        //onSuccess
        function (response) {
            console.log (&#39;Bucket creation successful:&#39;) ;
            console.log (response) ;
            $(&#39;#msg&#39;).text (&#39;Bucket creation successful&#39;) ;
            uploadFiles (response.key, files) ;
        },
        //onError
        function (error) {
            console.log (&#39;Bucket creation failed:&#39;);
            console.log (error) ;
            $(&#39;#msg&#39;).text (&#39;Bucket creation failed!&#39;) ;
        }
    ) ;
}

function uploadFiles (bucket, files) {
    for ( var i =0 ; i &lt; files.length ; i++ ) {
        var file =files [i] ;
        //var filename =file.replace (/^.*[\\\/]/, &#39;&#39;) ;
        console.log (&#39;Uploading file: &#39; + file.name + &#39; ...&#39;) ;
        $(&#39;#msg&#39;).text (&#39;Uploading file: &#39; + file.name + &#39; ...&#39;) ;
        oViewDataClient.uploadFileAsync (
            file,
            bucket,
            file.name,
            //onSuccess
            function (response) {
                console.log (&#39;File was uploaded successfully:&#39;) ;
                console.log (response) ;
                $(&#39;#msg&#39;).text (&#39;File was uploaded successfully&#39;) ;
                var fileId =response.objects [0].id ;
                var registerResponse =oViewDataClient.register (fileId) ;
                if (   registerResponse.Result === &quot;Success&quot;
                    || registerResponse.Result === &quot;Created&quot;
                ) {
                    console.log (&quot;Registration result: &quot; + registerResponse.Result) ;
                    console.log (&#39;Starting translation: &#39; + fileId) ;
                    $(&#39;#msg&#39;).text (&#39;Your model was uploaded successfully. Translation starting...&#39;) ;
                    checkTranslationStatus (
                        fileId,
                        1000 * 60 * 5, // 5 mins timeout
                        //onSuccess
                        function (viewable) {
                            console.log (&quot;Translation was successful: &quot; + response.file.name) ;
                            console.log (&quot;Viewable: &quot;) ;
                            console.log (viewable) ;
                            $(&#39;#msg&#39;).text (&#39;Translation was successful: &#39; + response.file.name + &#39;.&#39;) ;
                            //var fileId =oViewDataClient.fromBase64 (viewable.urn) ;
                            AddThisOne (viewable.urn) ;
                        }
                    ) ;
                }
            },
            //onError
            function (error) {
                console.log (&#39;File upload failed:&#39;) ;
                console.log (error) ;
                $(&#39;#msg&#39;).text (&#39;File upload failed!&#39;) ;
            }
        ) ;
    }
}

function checkTranslationStatus (fileId, timeout, onSuccess) {
    var startTime =new Date ().getTime () ;
    var timer =setInterval (function () {
            var dt =(new Date ().getTime () - startTime) / timeout ;
            if ( dt &gt;= 1.0 ) {
                clearInterval (timer) ;
            } else {
                oViewDataClient.getViewableAsync (
                    fileId,
                    function (response) {
                        var msg =&#39;Translation Progress &#39; + fileId + &#39;: &#39; + response.progress ;
                        console.log (msg) ;
                        $(&#39;#msg&#39;).text (msg) ;
                        if ( response.progress === &#39;complete&#39; ) {
                            clearInterval (timer) ;
                            onSuccess (response) ;
                        }
                    },
                    function (error) {
                    }
                ) ;
            }
        },
        2000
    ) ;
}</strong></span></code></span></pre>
</li>
<li>前回作成した&#0160;<strong>credentials.js</strong> の&#0160;grant_type: &#39;client_credentials&#39; の下に、バケットの作成とデータの書き込みをサポートする Scope に &#39;bucket:create data:read data:write bucket:read&#39; と追記します。<br />
<pre>var credentials ={<br /><br /> credentials: {<br /> // Replace placeholder below by the Consumer Key and Consumer Secret you got from<br /> // http://developer.autodesk.com/ for the production server<br /> client_id: process.env.CONSUMERKEY || &#39;<em>Consumer Key</em>&#39;,<br /> client_secret: process.env.CONSUMERSECRET || &#39;<em>Consumer Secret</em>&#39;,<br /> grant_type: &#39;client_credentials&#39;<strong>,</strong><br /><strong> scope: &#39;bucket:create bucket:read data:read data:write&#39;</strong><br /> },<br /> <br /> // If you which to use the Autodesk View &amp; Data API on the staging server, change this url<br /> BaseUrl: &#39;https://developer.api.autodesk.com&#39;,<br /> Version: &#39;v1&#39;<br />} ;<br /><br />credentials.Authentication =credentials.BaseUrl + &#39;/authentication/&#39; + credentials.Version + &#39;/authenticate&#39;<br /><br />module.exports =credentials ;</pre>
</li>
<li>アップロードと変換処理の実装が完了しましたので、Node.js command prompt で Node.js で作成した Web サーバーを起動します。<strong>node server.js</strong> と入力して、Server listening on port 3000 と表示されることを確認してください。<br /><br /></li>
<li>Google Chrome 等 WebGL 対応ブラウザで、URL に <a href="http://localhost:3000/upload.html">http://localhost:3000/upload.html</a>&#0160;と指定して実装を確認してください。次の画面が表示されるはずです。[ファイル選択] ボタンをクリックして任意のデザイン ファイルを選択したら、[Translate this one for me] ボタンをクリックしてください。ファイルのアップロード後に変換ステータスを 2000 ミリセカンド毎にチェックし、変換処理が完了すると、My URN list の欄に Base64 で変換された URN が表示されます。<br /><br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1aca945970c-pi" style="display: inline;"><img alt="Upload" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1aca945970c image-full img-responsive" src="/assets/image_812235.jpg" title="Upload" /><br /></a>なお、ここで表示される URN をクリックすると、前回作成した index.js にパラメータとして URN を渡す、3D モデルを表示します。</li>
</ol>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/03/view-and-data-api-tutorial-part3-basic-extension.html" target="_blank">次回</a></strong>&#0160;は、View and Data API で作成したビューアを、Extension と呼ばれるメカニズムを利用して拡張する方法をご案内します。</p>
<p>By Toshiaki Isezaki</p>
