---
layout: "post"
title: "JavaScript API と .Net Managed API 間の 連動カスタマイズ"
date: "2013-08-05 02:26:07"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/08/javascript-api-and-net-managed-api-related-linked-customize.html "
typepad_basename: "javascript-api-and-net-managed-api-related-linked-customize"
typepad_status: "Publish"
---

<p>先回お知らせした「<a href="http://adndevblog.typepad.com/technology_perspective/2013/07/autocad-2014-javascript-api_development_preps.html">AutoCAD 2014 JavaScript APIと既存APIとの連携の開発環境</a>」を使い、JavaScript API と .Net Managed APIによる以下の連動したカスタマイズと、htmlとJavaScript APIによるデータベースI/O処理について掲載させていただきます。<br />
これは、dotNetカスタムコマンド内より、JavaScript APIでコーディングされた html ファイルと jsファイルをAPIによってロードするカスタマイズの方法を理解していただくと共に、パレットとしてhtmlファイルを表示させ、パレット上の各ボタンからは、dotNetカスタムコマンド環境内のdotNetManagedクラス(dotNet API)を使い、データベースアクセス処理およびJavaScriptからの引数情報でオブジェクトの追加処理を実行させる（既存のdotNetAPIで作成されたカスタムコマンド処理を動作させることをイメージとしたサンプル）のほか、パレット上のボタンから直接JavaScript( js )ファイル内にコーディングされたJavaScript APIを使って JavaScriptファイルとhtml間のオブジェクト情報のI/O の動作の理解を目的としています。</p>

<p><strong>今回の処理の内容：</strong><br />
1.	Netカスタムコマンドを使ってhtmlとJavaScriptファイルをロードしツールパレットにhtmlを表示<br />
2.	htmlのボタン1 -> JavaScript -> .Netマネージコードでデータベースへ追加処理の呼び出し<br />
（呼び出された.Netカスタムコマンド内で、Json.Netを使いJavaScriptから引数の引き渡しデータを使ってデータベースへ線分とテキストオブジェクトの追加処理）<br />
3.	htmlのボタン2-> JavaScript -> .Netマネージコードでデータベースクエリー処理の呼び出し<br />
（呼び出された.Netカスタムコマンド内で、データベースのクエリー処理）<br />
4.	htmlのボタン3 -> JavaScript -> JavaScript APIでリアクタ処理の追加<br />
（JavaScript API を使ってオブジェクトにリアクタをセットし、Object ID一覧をhtml上に表示）<br />
5.	htmlのボタン4 -> JavaScript -> JavaScript APIでリアクタ処理の解除<br />
（JavaScript API を使って、オブジェクトに付加されたリアクタの解除処理を実行）<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191049581b1970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0191049581b1970c" alt="0809_001" title="0809_001" src="/assets/image_930203.jpg" /></a></p>

<p><strong><<  Htmlファイルの作成とコーディング  >></strong></p>

<p>(1).  VS Express 2012 for Webを起動し、JsLaunch.html を作成し、”ボタン”を４つと、”テキストエリア”を２つ配置する。<br />
(a). [ファイル] -> [ 新しいファイル ] -> [ Webタブ ]　を選択し、テンプレートより[ HTML ページ ]を選択し[ 開く ] ボタンを押す。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104958c17970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b019104958c17970c" alt="0809_002" title="0809_002" src="/assets/image_671731.jpg" /></a><br /> <br />
(b). [デザイン]を選択し [ ツールボックス ] -> [ Input(Button) ]　をドラッギングして“ボタン”を４つ配置し、続いて”Textarea” をドラッギングして“テキストエリア”を２つ配置する。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac5f02e5970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac5f02e5970d" alt="0809_003" title="0809_003" src="/assets/image_725966.jpg" /></a><br /><br />
(2).  エディタ上の [ ソース ] タブで切り替えて、html のコーディングを開始する。<br />
　　以下は、“デザイン”タブで確認した、完成した html の 表現となります。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac5f0966970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac5f0966970d" alt="0809_004" title="0809_004" src="/assets/image_817000.jpg" /></a><br /></p>

<p>注意：ブログ掲載の都合上、htmlタグ < > の前後に半角スペースが挿入されていますので、ご注意ください。</p>

<p>①	 < head >タグ内のタイトルと、JavaScriptAPI用の Autodesk.AutoCAD.js とカスタマイズ用の JsLaunch.js を定義し、<body>の動作時のファンクションを onMyLoadとして定義。</p>

<p>< head ><br />
    < meta charset="utf-8" / ><br />
    < title >JavaScript連動動作< /title ><br />
    < script type ="text/javascript" src = "http://www.autocadws.com/jsapi/v1/Autodesk.AutoCAD.js">< /script ><br />
    < script type ="text/javascript" src = "JsLaunch.js" >< /script ><br />
    < script type ="text/javascript"  ><br />
        function onMyLoad() {<br />
            write(" HtmlページよりJ avaScriptをロード");<br />
        }<br />
    < /script ><br />
②	 < head >タグ内に“< style type”タグを使って< body >の背景とフォントと文字種類と大きさ、テキストエリアのフォントと文字種類と大きさを設定</p>

<p>    < style type="text/css" ><br />
        body {font-family: sans-serif; font-size: 10pt;<br />
            background-color: #686868; <br />
            color:#FFF;<br />
            }<br />
        textarea {font-family: Consolas; font-size: 8pt; }<br />
        #EventEntity{<br />
            height: 96px;<br />
        }<br />
        #EventLog {<br />
            height: 89px;<br />
            margin-bottom: 5px;<br />
        }<br />
      < /style ><br />
< /head ></p>

<p>③	< body >タグ内に起動時のonMyLoad()ファンクションを追加し、各ボタンが押されたときに onclick を使って jsファイル側の起動関数の呼び出し「Button1は'NetCmdRead()'」でボタン文字は'.Net側のRead'」、「Button2は' NetCmdWrite()'」でボタン文字は'.Net側のWrite '」、「Button3は' JavaSelectEntity()'」でボタン文字は' JavaScript側で監視開始'」、「Button4は' JavaStopEventEntity ()'」でボタン文字は' JavaScript側で監視終了'」に変更し、テキストエリアは、それぞれcols="30"　rows="10" に変更する。</p>

<p>< body onload ="onMyLoad()" ><br />
    < p > コマンド:< br/ ><br />
        < input id="Button1" type="button" style='width: 200px' onclick= NetDBAdd ()' value='.Net側のdotNetDBAdd関数' / >< br / ><br />
        < input id="Button2" type="button" style='width: 200px' onclick='NetDBAccess()' value=".Net側のdotNetDBAccess関数" / >< br / ><br />
        < input id="Button3" type="button" style='width: 200px' onclick='JavaSelectEntity()' value="JavaScript側で監視開始" / >< br / ><br />
        < input id="Button4" type="button" style='width: 200px' onclick='JavaStopEventEntity()' value="JavaScript側で監視終了" / >< /p ><br />
    < p > 監視するエンティティ:< br/ ><br />
        < textarea id=" EventEntity" cols="30"　name="S1"　rows="10"　readonly="readonly" >< /textarea >< /p ><br />
    < p > エンティティのログ:< br/ ><br />
        < textarea id=" EventLog" cols="30" name="S2" rows="10" readonly="readonly" >< /textarea >< /p ><br />
< /body ></p>

<p>(3).  [ファイル] -> [ HtmlPage1.html の保存 ]　にて、任意のホルダに [ ファイル名 ] を JsLaunch.html として保存する。</p>

<p><br />
<strong><<  JavaScriptファイルの作成とコーディング  >></strong></p>

<p>(1).  VS Express for 2012 Webを起動し、JsLaunch.js を作成し、”ボタン”を４つと、”テキストエリア”を２つ配置する。<br />
(a). [ファイル] -> [ 新しいファイル ] -> [ Webタブ ]　を選択し、テンプレートより[ JavaScript ファイル ]を選択し[ 開く ] ボタンを押す。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac5f2829970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac5f2829970d" alt="0809_005" title="0809_005" src="/assets/image_377146.jpg" /></a><br /><br />
①	ボタン１：の実装 <br />
//--------------------------------------------------------------------<br />
// Button1<br />
// .Net側のdotNetDBAdd関数を呼び出す関数<br />
// execAsyncでJSON.stringifyを使い引数付でdotNet側の「dotNetDBAdd」を呼ぶ<br />
//--------------------------------------------------------------------<br />
// dotNet側に引き渡す情報<br />
var stPoint = new Acad.Point3d(10, 10, 0);<br />
var enPoint = new Acad.Point3d(20, 20, 0);<br />
var hight = 5;</p>

<p>function NetDBAdd() {<br />
    execAsync(JSON.stringify({<br />
        functionName: 'dotNetDBAdd',     // dotNet側の関数名<br />
        invokeAsCommand: false,          // コマンド<br />
        functionParams: {<br />
            stPoint: stPt,               // 直線の開始点<br />
            enPoint: enPt,               // 直線の終了点<br />
            hight: ht                    // テキストの文字高さ<br />
        }<br />
    }),<br />
    OnNetSuccess,     // dotNet側からの成功時に呼び出すJS側の関数<br />
    OnNetError);      // dotNet側からのエラー時に呼び出すJS側の関数<br />
}</p>

<p>②	ボタン２：の実装 <br />
//--------------------------------------------------------------------<br />
// Button2<br />
// .Net側のdotNetDBAccess関数を呼び出す関数<br />
// execAsyncでJSON.stringifyを使い、dotNet側の「dotNetDBAccess」を呼ぶ<br />
//--------------------------------------------------------------------<br />
function NetDBAccess() {<br />
    execAsync(JSON.stringify({<br />
        functionName: 'dotNetDBAccess',  // dotNet側の関数名<br />
        invokeAsCommand: false,          // コマンド<br />
        functionParams: { args: 'args' } // 引数名 args<br />
    }),<br />
    OnNetSuccess,     // dotNet側からの成功時に呼び出すJS側の関数<br />
    OnNetError);      // dotNet側からのエラー時に呼び出すJS側の関数<br />
}<br />
// dotNet側からの成功時に呼び出すJS側の関数<br />
function OnNetSuccess(result) {<br />
    write("\n");<br />
}<br />
// dotNet側からのエラー時に呼び出すJS側の関数<br />
function OnNetError(result) {<br />
    write("\nOnParseError: " + result);<br />
}</p>

<p>③	ボタン３：の実装 <br />
//--------------------------------------------------------------------<br />
// Button3<br />
// JavaScript側でObjectの選択と監視開始ボタンの動作用関数<br />
//--------------------------------------------------------------------<br />
// イベントが付加されたオブジェクト郡用のコレクション<br />
// AutoCADのオブジェクト識別子のコレクションとして初期化<br />
var oEventEnt = new Acad.OSet();</p>

<p>// htmlの「エンティティのログ:」テキストアリア(EventLog)を確保し<br />
// ObjectModifiedイベントで反応したオブジェクトの情報を追加表示<br />
function onObjectModified(eventname, args) {<br />
    var TextLog = document.getElementById('EventLog');<br />
    TextLog.value = TextLog.value + "オブジェクトの修正： " + args.id + '\n';<br />
}<br />
// htmlの「エンティティのログ:」テキストアリア(EventLog)を確保し<br />
// Erasedイベントで反応したオブジェクトの情報を追加表示<br />
function onObjectErased(eventname, args) {<br />
    var TextLog = document.getElementById('EventLog');<br />
    TextLog.value = TextLog.value + "オブジェクトの削除： " + args.id + '\n';<br />
}</p>

<p>// Acad.Editor.getEntityで正常の場合に呼び出される関数<br />
// JSON.parseを使って引数の実行結果の解析<br />
function onComplete(jsonPromptResult) {<br />
    // JSON.parseを使って引数の実行結果の解析<br />
    var resultObj = JSON.parse(jsonPromptResult);<br />
    if (resultObj) {</p>

<p>        // 正常の場合<br />
        if (resultObj.status == 5100) {<br />
            // htmlの「監視するエンティティ:」テキストアリア(EventEntity)を確保し<br />
            // 選択されたオブジェクトの情報を追加表示<br />
            var TextIds = document.getElementById('EventEntity');<br />
            EventEntity.value = EventEntity.value + resultObj.objectId + '\n';</p>

<p>            // AutoCADのオブジェクト識別子のコレクションに<br />
            // 選択オブジェクトされたオブジェクトを追加<br />
            oEventEnt.add(resultObj.objectId);</p>

<p>            // オブジェクトの「modified」イベント通知をセット<br />
            Acad.Application.activedocument.startObserving(<br />
                oEventEnt,<br />
                Acad.Application.activedocument.eventname.modified,<br />
                onObjectModified);<br />
            // オブジェクトの「erased」イベント通知をセット<br />
            Acad.Application.activedocument.startObserving(<br />
               oEventEnt,<br />
               Acad.Application.activedocument.eventname.erased,<br />
               onObjectErased);<br />
        }</p>

<p>            //キーワードの入力は何もしない<br />
        else if (resultObj.status == -5005) {<br />
        }</p>

<p>            // リターン/スペースバー＝null入力は何もしない<br />
        else if (resultObj.status == 5000) {<br />
        }</p>

<p>            // キャンセルキーは何もしない<br />
        else if (resultObj.status == -5002) {<br />
        }</p>

<p>            //その他も何もしない<br />
        else {<br />
        }<br />
    }<br />
}<br />
// 選択のエラー<br />
function onError(jsonPromptResult) {<br />
    write("選択エラー: " + jsonPromptResult);</p>

<p>    var resultObj = JSON.parse(jsonPromptResult);<br />
    if (resultObj) {<br />
    }<br />
}<br />
// 実際にhtml側の「ボタン３」が押された時に呼び出される関数<br />
function JavaSelectEntity() {<br />
    try {<br />
        // エンティティ選択用のPromptEntityOptionsを用意し<br />
        // 選択条件をセット<br />
        var peo = new Acad.PromptEntityOptions();<br />
        peo.setMessageAndKeywords("\nエンティティの選択：", "");<br />
        peo.rejectMessage = "\n無効なオブジェクトの選択です";<br />
        peo.singlePickInSpace = true;<br />
        peo.allowObjectOnLockedLayer = true;</p>

<p>        // エンティティの選択<br />
        // 正常=onComplete, エラー= onErrorの呼び出し<br />
        Acad.Editor.getEntity(peo).then(<br />
            onComplete,<br />
            onError);<br />
    }<br />
    catch (e) {<br />
        write(e.message);<br />
    }<br />
}</p>

<p>④	ボタン４：の実装 <br />
//--------------------------------------------------------------------<br />
// Button4<br />
// JavaScript側で監視終了ボタンの動作用関数<br />
// テキストエリアの初期化・各イベント通知の中止・<br />
// イベントが付加されたオブジェクト郡用のコレクションの初期化<br />
//--------------------------------------------------------------------<br />
function JavaStopEventEntity() {<br />
    // htmlの「監視するエンティティ:」テキストアリア(EventEntity)のクリア<br />
    var TextIds = document.getElementById('EventEntity');<br />
    EventEntity.value = "";<br />
    // htmlの「エンティティのログ:」テキストアリア(EventLog)のクリア<br />
    var TextLog = document.getElementById('EventLog');<br />
    EventLog.value = "";</p>

<p>    // オブジェクトの「modified」イベント通知を中止<br />
    Acad.Application.activedocument.stopObserving(<br />
        oEventEnt,<br />
        Acad.Application.activedocument.eventname.modified,<br />
        onObjectModified);<br />
    // オブジェクトの「erased」イベント通知を中止<br />
    Acad.Application.activedocument.stopObserving(<br />
        oEventEnt,<br />
        Acad.Application.activedocument.eventname.erased,<br />
        onObjectErased);<br />
    // イベントが付加されたオブジェクト郡用のコレクションの初期化<br />
    oEventEnt = new Acad.OSet();<br />
}</p>

<p>(2).  [ファイル] -> [JavaScript1.js の保存 ]　にて、任意のホルダに [ ファイル名 ] を JsLaunch.js として、JsLaunc.htmlファイルと同じホルダに保存する。<- 注意してください。</p>

<p><br />
<strong><<  C# dotNetカスタムコマンドの作成とコーディング  >></strong></p>

<p>(1).  VS Express 2012 for Desktopを起動し、.Net C#言語で JsLaunchCmd プロジェクトを作成する。</p>

<p> (a). [ファイル] -> [ 新しいファイル ] ->テンプレートより[ Visual C# ] ->[ Autodesk ]を選択し[ Autodesk 2014 CSharp plug-in ]を選択し、名前を[ JsLaunchCmd ]、場所を[JsLaunchCmd ]( JsLaunch.html とJsLaunch.js ファイルの格納ホルダ ) 、ソリューションのディレクトリを作成チェックボックスをOn にして[ 開く ] ボタンを押す。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910495b6cb970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01910495b6cb970c image-full" alt="0809_006" title="0809_006" src="/assets/image_873142.jpg" border="0" /></a></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910495b75a970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01910495b75a970c" alt="0809_007" title="0809_007" src="/assets/image_802709.jpg" /></a><br /><br />
(b). [AutoCAD .NET Wizard Configurator ] で　ObjectARX SDKのホルダーを [C:\Autodesk_ObjectARX_2014_Win_64_and_32Bit\inc ] 、AutoCAD 2014のホルダーを[C:\Program Files\Autodesk\AutoCAD 2014 ]として[ OK ] ボタンを押す。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e9fce08970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e9fce08970b" alt="0809_008" title="0809_008" src="/assets/image_28123.jpg" /></a><br /> <br />
(c). 参照設定で[ AcCoreMgd , AcDbMgd , AcMgd ] の [ ローカルコピー ] が [ False ] で [ バージョン ] が [ 19.1.0.0 ] であるか確認する。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910495b9eb970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01910495b9eb970c" alt="0809_009" title="0809_009" src="/assets/image_838344.jpg" /></a><br /> <br />
(d). 参照設定の [ 参照の追加 ] で [ System.Windows.Forms ] を追加する。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e9fd092970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e9fd092970b" alt="0809_010" title="0809_010" src="/assets/image_121155.jpg" /></a><br /> </p>

<p> (2) . JsLaunch.html ファイルをロードしパレットに表示するコマンドを作成する。</p>

<p> 　(a). [ソリューションエクスプローラー] で [myCommands.cs ] を選択し、コマンド名を[ AdnJsLoad ] とする。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910495bcc6970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01910495bcc6970c" alt="0809_011" title="0809_011" src="/assets/image_492570.jpg" /></a><br /> <br />
(b). GetStringメソッドでJsLaunch.htmlのホルダー名の入力後に、パレットセットを確認して[ツール] -> [ GUIDの作成 ] にて作成した 唯一無二のGUIDを用いてPaletttsetを追加する。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e9fd246970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e9fd246970b" alt="0809_012" title="0809_012" src="/assets/image_599462.jpg" /></a><br /><br />
Document doc = Application.DocumentManager.MdiActiveDocument;<br />
Database db = doc.Database;<br />
Editor ed = doc.Editor;<br />
PromptResult filepath = ed.GetString("\nJsLaunch.htmlファイルのホルダー名をフルパスで入力 : ");<br />
if (filepath.Status != PromptStatus.OK)<br />
    return;<br />
string filename = filepath.StringResult  + "\\" + "" + "JsLaunch.html";<br />
// JsLaunch.htmlファイルの存在の確認<br />
if (System.IO.File.Exists(filename))<br />
{<br />
    // パレットセットを確認して、PaletteSetの追加<br />
    if (oPaletttset == null)<br />
    {<br />
        // 第２引数のGuidは「ツール」->「Guidの作成」で作成します<br />
        oPaletttset = new Autodesk.AutoCAD.Windows.PaletteSet(<br />
            "JavaScript連動動作",<br />
            new Guid("AE909B5F-9245-4DDE-AC9F-A427F1771DE0"));<br />
    }<br />
    // U.R.L 文字に変換<br />
    String url = "file:///" + filename;<br />
    try<br />
    {<br />
        String tabName = "";<br />
        Uri uri = new Uri(url);<br />
        if (uri.IsFile)<br />
        {<br />
            //セグメントよりタブを抽出<br />
            String[] segments = uri.Segments;<br />
            if (segments.Length > 0)<br />
            {<br />
                tabName = segments[segments.Length - 1];<br />
                String[] fileSplit = tabName.Split('.');<br />
                if (fileSplit.Length > 0)<br />
                    tabName = fileSplit[0];<br />
            }<br />
        }<br />
        else<br />
        {<br />
            tabName = uri.Host;<br />
        }<br />
        // パレットの存在を確認し更新のために、一旦削除する<br />
        if (oPaletttset.Count != 0)<br />
        {<br />
            oPaletttset[0].PaletteSet.Remove(0);<br />
        }<br />
        // パレットの追加表示<br />
        Autodesk.AutoCAD.Windows.Palette p = oPaletttset.Add(tabName, uri);<br />
        oPaletttset.Visible = true;<br />
    }<br />
    catch (UriFormatException ex)<br />
    {<br />
        ed.WriteMessage("U.R.Lが見つかりません : " + ex.Message);<br />
    }<br />
}<br />
else<br />
{<br />
    ed.WriteMessage(filename + "ファイルは見つかりません");<br />
}</p>

<p>　<br />
(3). JsLaunch.htmlの“ボタン１”が押された時の処理を作成する。<br />
　JavaScriptからdotNet環境の「JavaScriptCallback」に引数を渡すにはJson.Net環境を利用する方法があります。<br />
　このJson.Netは以下の手順で、使用するプロジェクトにインストールして組み込むことができます。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910495bf71970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01910495bf71970c" alt="0809_013" title="0809_013" src="/assets/image_961238.jpg" /></a></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac5f33b9970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac5f33b9970d" alt="0809_014" title="0809_014" src="/assets/image_182547.jpg" /></a></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910495c049970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01910495c049970c" alt="0809_015" title="0809_015" src="/assets/image_195329.jpg" /></a></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac5f34a2970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac5f34a2970d" alt="0809_016" title="0809_016" src="/assets/image_169182.jpg" /></a><br /> <br />
コーディング内で使用するに当たり、myCommands.csの先頭に“using Newtonsoft.Json.Linq;”を追加<br />
using Newtonsoft.Json.Linq;<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac5f3693970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b0192ac5f3693970d" alt="0809_017" title="0809_017" src="/assets/image_712383.jpg" /></a><br /> <br />
// ----------------------------------------------------------<br />
// ボタンで１呼び出される関数<br />
// JavaScriptより引き渡された引数を、Json.Netを使って開始点と終了点に変換し<br />
// Lineの作成 及び 終了点と文字高さを使って DBTestの作成<br />
[JavaScriptCallback("dotNetDBAdd")]<br />
public string dotNetDBAdd(string jsonArgs)<br />
{<br />
    // 引数を変換( Json.NetによるJObjectを使用 )<br />
    var o = JObject.Parse(jsonArgs);<br />
    // 開始点の作成<br />
    var sx = (double)o["functionParams"]["stPoint"]["x"];<br />
    var sy = (double)o["functionParams"]["stPoint"]["y"];<br />
    var sz = (double)o["functionParams"]["stPoint"]["z"];<br />
    var stPoint = new Point3d(sx, sy, sz);<br />
    // 終了点の作成<br />
    var ex = (double)o["functionParams"]["enPoint"]["x"];<br />
    var ey = (double)o["functionParams"]["enPoint"]["y"];<br />
    var ez = (double)o["functionParams"]["enPoint"]["z"];<br />
    var enPoint = new Point3d(ex, ey, ez);<br />
    // 文字高さの作成<br />
    var high = (double)o["functionParams"]["hight"];</p>

<p>    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;<br />
    Database db = doc.Database;<br />
    Editor ed = doc.Editor;</p>

<p>    //ドキュメントをロックする<br />
    using (DocumentLock lk = doc.LockDocument())<br />
    {<br />
        using (Transaction tx = doc.TransactionManager.StartTransaction())<br />
        {<br />
            BlockTableRecord btr = tx.GetObject(db.CurrentSpaceId, OpenMode.ForWrite)<br />
                as BlockTableRecord;<br />
            // 開始点と終了点を使って Lineの作成<br />
            Line oLine = new Line(stPoint, enPoint);<br />
            btr.AppendEntity(oLine);<br />
            tx.AddNewlyCreatedDBObject(oLine, true);<br />
            // 終了点と文字高さを使って DBTestの作成<br />
            DBText otext = new DBText();<br />
            otext.TextString = "JavaScriptからの起動指示";<br />
            otext.Position = enPoint ;<br />
            otext.Height=high;<br />
            btr.AppendEntity(otext);<br />
            tx.AddNewlyCreatedDBObject(otext, true);<br />
            // コミット<br />
            tx.Commit();<br />
        }<br />
    }<br />
    return "{\"retCode\":0, \"result\":\"OK\"}";<br />
}</p>

<p>(4). JsLaunch.htmlの“ボタン２”が押された時の処理を作成する。<br />
JavaScriptCallback ステートメントを使って、JavaScriptより呼び出し可能な”dotNetDBAccess” コマンドを作成する。<br />
[JavaScriptCallback("dotNetDBAccess")]<br />
public string dotNetDBAccess(string jsonArgs)<br />
{<br />
    Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;<br />
    Database db = doc.Database;<br />
    Editor ed = doc.Editor;</p>

<p>    using (Transaction tx = doc.TransactionManager.StartTransaction())<br />
    {<br />
        BlockTableRecord btr = tx.GetObject(db.CurrentSpaceId, OpenMode.ForRead)<br />
            as BlockTableRecord;<br />
        ed.WriteMessage("\nブロックテーブルレコード内のダンプ");<br />
        foreach (ObjectId id in btr)<br />
        {<br />
            ed.WriteMessage("\nエンティティ: " + id.ObjectClass.Name);<br />
        }</p>

<p>        tx.Commit();<br />
    }</p>

<p>    return "{\"retCode\":0, \"result\":\"OK\"}";<br />
}</p>

<p>(5). [ ビルド ] -> [ ソリューションのビルド ] にて エラーの無いことを確認する。 <br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910495c479970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01910495c479970c" alt="0809_018" title="0809_018" src="/assets/image_322081.jpg" /></a><br /><br />
(6). [ ファイル ] -> [ 全てを保存 ] にて プロジェクトを含むすべてのファイルを保存する。</p>

<p><br />
<strong><<  実行と確認  >></strong>　</p>

<p>(1). [ NETLOAD ] コマンドで-> [JsLaunchCmd.dll ] ロード。<br />
　　例、C:\AdnJavaDemoホルダにVisualStudioのC#プロジェクトを作成したならば、以下となります。<br />
C:\AdnJavaDemo\JsLaunchCmd\JsLaunchCmd\bin\Debug\ JsLaunchCmd.dll<br />
(2).コマンドラインより[ ADNJSLOAD ] コマンドを起動して、フルパス入力文字に” C:\AdnJavaDemo” を入力し確認する。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e9fda17970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01901e9fda17970b" alt="0809_019" title="0809_019" src="/assets/image_911955.jpg" /></a></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910495c631970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01910495c631970c" alt="0809_020" title="0809_020" src="/assets/image_328153.jpg" /></a><br /> <br />
(3).セキュリティ警告ダイアログが現れる場合は、以下の[ Autodesk-Technical Q&A ] を参照してシステム変数TRUSTEDPATHS および TRUSTEDDOMAINS への追加変更をお願いします。</p>

<p>セキュリティ警告ダイアログについて<br />
<a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=7977">http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=7977</a></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01910495c735970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01910495c735970c" alt="0809_021" title="0809_021" src="/assets/image_784944.jpg" /></a><br /><br />
サンプルプロジェクト一式は <span class="asset  asset-generic at-xid-6a0167607c2431970b01910495cd18970c"><a href="http://adndevblog.typepad.com/files/adnjavademo.zip">ここより</a></span> ダウンロードいただけます。</p>

<p>Shigekazu Saito. </p>
