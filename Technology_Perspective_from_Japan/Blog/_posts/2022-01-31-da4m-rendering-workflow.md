---
layout: "post"
title: "Design Automation API for 3ds Max：MAXScript レンダリング フロー"
date: "2022-01-31 00:02:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-rendering-workflow.html "
typepad_basename: "da4m-rendering-workflow"
typepad_status: "Publish"
---

<p><span style="background-color: #ffffff;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-maxscript-and-3dsmaxbatch.html" rel="noopener" target="_blank">Design </a><a href="https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-maxscript-and-3dsmaxbatch.html" rel="noopener" target="_blank">Automation API for 3ds Max：MAXScript と 3ds Max Batch</a> と <a href="https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-read-json-by-maxmaxscript.html" rel="noopener" target="_blank">Design Automation API for 3ds Max：MAXScript での JSON 読込み </a></span>でご案内してきた MAXScript を、Design Automation API for 3ds Max で実行出来るようにしていきます。</p>
<p>最初に、Design Automation API の利用手順を再確認しておきます。短く表現するなら、Design Automation API&#0160; は、クラウドで動作するカスタム処理を、クライアント（主に Web ブラウザ）からリモート操作する環境を提供します。</p>
<p>この時、クライアントからリクエストするカスタム処理の実行単位が <strong>WorkItem（ワークアイテム）</strong>です。WorkItem の実行前には、WorkItem が使用するファイルやカスタム処理のコマンド/スクリプトを明記/定義する <strong>Activity（アクティビティ）</strong>と、カスタム処理を実装するコンポーネントである <strong>AppBundle（アップバンドル）</strong>を事前に登録ことになります。</p>
<ul>
<li>Appbundle は、アドイン/プラグインを含む<a href="https://help.autodesk.com/view/MAXDEV/2022/ENU/?guid=Max_Developer_Help_writing_plug_ins_plugin_package_html" rel="noopener" target="_blank">パッケージバンドル</a>を圧縮した .zip ファイルを指します。</li>
<li>WorkItem は、Activity で定義された内容（どの入力ファイル（素材）を使って、どの AppBundleで実装されたカスタム処理を実行し<a class="save-entry" href="https://www.typepad.com/site/blogs/6a0167607c2431970b017ee78dc01c970d/post/6a0167607c2431970b02942f8ff6a7200c/edit#">公開</a>、出力ファイル（成果物）を保存するか）に沿って実行されます。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806643b8200d-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13ee403200b-pi" style="display: inline;"><img alt="Da_flow" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13ee403200b image-full img-responsive" src="/assets/image_488099.jpg" title="Da_flow" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806643b8200d-pi" style="display: inline;"><br /></a></p>
<p>ここで WorkItem が実行するのは、ここまで考察してきた MAXScript ということになります。MAXScript 自体は、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/tutorials/3dsmax/task3-create-activity/" rel="noopener" target="_blank">Step by step tutorial</a> で説明された方法と同じく、Signed URL（署名付き URL）を使って任意に用意したクラウド ストレージから直接ダウンロード、実行します。</p>
<p><span style="background-color: #ffffff;">今回は MAXScript からの JSON 読み込み用に Newtonsoft.Json コンポーネントを使用するため、Newtonsoft.Json コンポーネントをコアエンジンに認識させる必要があります。</span></p>
<p>これを解決する方法が、AppBundle です。AppBundle は、本来、アドイン/プラグイン モジュールを WorkItem の実行環境にロードさせるために使用しますが、ここではコアエンジンに Newtonsoft.Json コンポーネントを認識させるために利用します。</p>
<p>下記は、 Newtonsoft.Json コンポーネント用 AppBundle の PackageContents.xml 記述と ZIP 圧縮ファイルが持つフォルダ階層の例です。</p>
<div>
<blockquote>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880662585200d-pi" style="float: right;"><img alt="Bundle_hierarcy" class="asset  asset-image at-xid-6a0167607c2431970b027880662585200d img-responsive" src="/assets/image_547000.jpg" style="width: 250px; margin: 0px 0px 5px 5px;" title="Bundle_hierarcy" /></a>&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&lt;ApplicationPackage</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; AutodeskProduct=&quot;3ds Max&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; ProductType=&quot;Application&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; Name=&quot;Generic package&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; Description=&quot;Generic package for 3dsMax on Design Automation&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; AppVersion=&quot;1.0.0&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; UpgradeCode=&quot;{5CC054DF-72B7-46D0-BAE8-DCA6A31E26A9}&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; ProductCode=&quot;{56F41568-0851-4FF9-97A2-0707B7E9AA50}&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; SchemaVersion=&quot;1.0&quot;&gt;</span></div>
<div>&#0160;</div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &lt;CompanyDetails</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; Name=&quot;Autodesk Ltd,. Japan&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; Url=&quot;https://www.autodesk.co.jp&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; Email=&quot;&quot; /&gt;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &lt;RuntimeRequirements OS=&quot;Win64&quot; Platform=&quot;3ds Max&quot; SeriesMin=&quot;2020&quot; SeriesMax=&quot;2023&quot; /&gt;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &lt;Components Description=&quot;assemblies parts&quot; &gt;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;RuntimeRequirements OS=&quot;Win64&quot; Platform=&quot;3ds Max&quot; SeriesMin=&quot;2020&quot; SeriesMax=&quot;2023&quot; /&gt;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &lt;ComponentEntry AppName=&quot;NewtonSoft.json.dll&quot; Version=&quot;1.0.0&quot; ModuleName=&quot;./Contents/Newtonsoft.Json.dll&quot; /&gt;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &lt;/Components&gt;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&lt;/ApplicationPackage&gt;</span></div>
</blockquote>
</div>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;"><span style="color: #0000ff;"> UpgradeCode <span style="color: #111111;">と</span> ProductCode</span> には、Visual Studio の [GUID の作成] ツールなどで作成した値を設定する必要があります。</span></p>
<p><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;"> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f93fb68200c-pi" style="display: inline;"><img alt="Create_guid" class="asset  asset-image at-xid-6a0167607c2431970b02942f93fb68200c img-responsive" src="/assets/image_841278.jpg" style="width: 250px;" title="Create_guid" /></a></span></p>
<p>もちろん、この AppBundle も WorkItem 実行前に<a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST/" rel="noopener" target="_blank">登録</a>しておく必要があります。</p>
<p>次に、MAXScript を WorkItem の作業領域にあわせて次のように<span style="color: #0000ff;">変更</span>しておきます。なお、DWG 読み込み時の形状の滑らかさの問題を回避する目的で、事前に読み込み用意したシーン ファイル（<span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">Table Fan.max</span>）ファイルを開いて処理するようにしています。（3ds Max での DWG 読み込み時には「<a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/CloudHelp/cloudhelp/2019/JPN/AutoCAD-Core/files/GUID-DF936433-83E9-47CD-96AB-845D2AE872D1-htm.html" rel="noopener" target="_blank">レイヤ、ノード階層となるブロック、マテリアルでよって分割</a>」設定を使用して準備）</p>
<p>また、上記 AppBundle の効果によって、以前記述していた Newtonsoft.Json アセンブリ読み込み （json = dotNet.loadAssembly &quot;Newtonsoft.Json.dll&quot;）が不要になっている点にもご注意ください。</p>
<div>
<blockquote>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* JSON 読み込み関数</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160;参照:https://forums.cgsociety.org/t/json-and-maxscript/1552038/11</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">*/</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">fn getJsonFileAsString filePath=(</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; local &#0160;jsonString = &quot;&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; fs=openFile filePath</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; while not eof fs do(</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; jsonString += readchar fs</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; )</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; close fs</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; return jsonString</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">)<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* JSON 読み込み */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">paramsFilePath = &quot;params.json&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">jsonString = getJsonFileAsString paramsFilePath</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">myJObject = dotNetObject &quot;Newtonsoft.Json.Linq.JObject&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">myJObject = myJObject.parse jsonString<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* パラメータ取得 */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">global col = myJObject.item[&quot;color&quot;].value as integer</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">global leaf = myJObject.item[&quot;leaf&quot;].value as booleanClass<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* ColorX 画層オフ */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">global lay</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">for i = 1 to 6 do</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">(</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; if col != i then (</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; lay = LayerManager.getLayerFromName ( &quot;Color&quot; + i as string )</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; lay.on = false</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; )</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">)<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* Leaf 画層オン/オフ */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">lay = LayerManager.getLayerFromName ( &quot;Leaf&quot; )</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">lay.on = leaf<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* 既定パース ビューポート */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">viewport.activeviewport = 4</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">viewport.SetRenderLevel #smoothhighlights</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">viewport.setLayout #layout_4<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* ART レンダラー設定 */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">global art = ART_Renderer()</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.quality_db = 20</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.render_method = 1</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.anti_aliasing_filter_diameter = 2.0</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.enable_noise_filter = true</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.noise_filter_strength = 1</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">renderers.current = art<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* レンダリング &amp; 生成画像保存 */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;"><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">global </span>fname = sysInfo.currentdir + &quot;/result.jpg&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">undisplay ( render outputfile:fname )</span></div>
</blockquote>
</div>
<p>この MAXScript（<span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #111111;">myscript.ms</span>）の処理を Design Automation API for 3ds Max で実行させるための Activity 記述は、次のようになります。</p>
<div>
<blockquote>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&quot;id&quot;: &#39;TableFanConfiguratorRender&#39;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&quot;commandLine&quot;: [&#39;$(engine.path)\\3dsmaxbatch.exe -sceneFile &quot;$(args[InputMaxScene].path)&quot;<span style="color: #111111;"> &quot;$(args[MaxscriptToExecute].path)&quot;&#39;],</span></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&quot;parameters&quot;: {</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;InputMaxScene&quot;: {</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Input 3ds Max scene&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;Table Fan.max&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;Params&quot;: {</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Input parameters to specify behavior&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;params.json&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &quot;MaxscriptToExecute&quot;: {</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;MAXScript to render scene on 3dsmaxbatch.exe&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;<strong>myscript.ms</strong>&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; },</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;ImageOutput&quot;: {</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;put&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;output rendering image&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;result.jpg&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; }</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">},</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&quot;engine&quot;: &#39;Autodesk.3dsMax+2022&#39;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&quot;appbundles&quot;: [&#39;<em>&lt;your nicknme&gt;</em>.TableFanConfiguratorRender+dev&#39;],</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&quot;description&quot;: &quot;Create rendering image&quot;</span></div>
</blockquote>
</div>
<p>この Activity を起動する WorkItem ペイロードは次のようなかたちです。</p>
<div>
<blockquote>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">// Create WorkItem</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">var payload =</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">{</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &quot;activityId&quot;: DA4M_FQ_ID,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &quot;arguments&quot;: {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &quot;InputMaxScene&quot;: {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: signedURLforInput,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Content-type&quot;: &quot;application/octet-stream&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &quot;Params&quot;: {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;data:application/json,&quot; + paramsJSON</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &quot;MaxscriptToExecute&quot;: {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: signedURLforInput2,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Content-type&quot;: &quot;text/plain&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &quot;ImageOutput&quot;: {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: signedURLforOutput,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Content-Type&quot;: &quot;application/octet-stream&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &#39;put&#39;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">};<br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">var uri = &quot;https://developer.api.autodesk.com/da/us-east/v3/workitems&quot;;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">request.post({</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; url: uri,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; headers: {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#39;content-type&#39;: &#39;application/json&#39;,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; &#39;authorization&#39;: &#39;Bearer &#39; + credentials.access_token</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; },</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; body: JSON.stringify(payload)</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">}, function (error, workitemres, body) {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; ...</span></div>
</blockquote>
</div>
<p>ここまでの実装で、クライアント（Web ブラウザ）の指定によって変化するレンダリング画像を得ることが出来るようになります。</p>
<p>次の例では、Forge Viewer 上で指定した「羽根」の色と「葉っぱ」有無を、JSON パラメータとして Design Automation API for 3ds Max に渡し、クラウド上のコアエンジンと MAXScript でレンダリング画像を作成してから、クライアントの Web ページの &lt;img&gt; タグ領域に署名付き URL（Signed URL）を使って表示する例です。（Forge Viewer カンバスと同じサイズでオーバーレイ表示）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f948f5e200c-pi" style="display: inline;"><img alt="Rendering" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f948f5e200c image-full img-responsive" src="/assets/image_592854.jpg" title="Rendering" /></a></p>
<p>この WorkItem の成果物であるレンダリング画像は、AutoCAD の Table Fan.dwg を 3ds Max で読み込んで用意した Table Fan.max の第 4 ビューポートの既定ビュー（カメラ視点）になっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f966ca3200c-pi" style="display: inline;"><img alt="Target_viewport" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f966ca3200c image-full img-responsive" src="/assets/image_567041.jpg" title="Target_viewport" /></a></p>
<p>次回は、Forge Viewer 上に表示しているビュー（カメラ）を 3ds Max シーンに反映して、レンダリング画像を得る方法を考察してみます。</p>
<p>By Toshiaki Isezaki</p>
