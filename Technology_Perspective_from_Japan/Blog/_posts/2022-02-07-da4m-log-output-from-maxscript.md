---
layout: "post"
title: "Design Automation API for 3ds Max：MAXScript からのログ出力"
date: "2022-02-07 00:03:04"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/02/da4m-log-output-from-maxscript.html "
typepad_basename: "da4m-log-output-from-maxscript"
typepad_status: "Publish"
---

<p>どのようなプログラムであっても、コードの作成中に実行状況をテキスト表示して<a href="https://ja.wikipedia.org/wiki/%E3%83%87%E3%83%90%E3%83%83%E3%82%B0" rel="noopener" target="_blank">デバッグ</a>に役立てたいことがあります。Design Automation API でも、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-id-GET/" rel="noopener" target="_blank">GET workitems/:id</a> endpoint や <a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/callbacks/#oncomplete-callback" rel="noopener" target="_blank">OnComplete</a> コールバックからコアエンジンで実行された WorkItem 単位でログファイルをダウンロードして、アドイン（プラグイン/スクリプト）の実行状態を把握することが出来ます。</p>
<p>もし、Design Automation API for 3ds Max で MAXScript からログにテキストを表示させる場合、少し注意が必要です。MAXScript リスナーでのように <a href="https://help.autodesk.com/view/3DSMAX/2017/JPN/?guid=__files_GUID_0AEEF510_1859_48B7_9A19_A6158B0310A7_htm" rel="noopener" target="_blank">print や format</a> をコード中に挿入しても、ログに反映させることが出来ません。</p>
<p>MAXScript で Design Automation API のログにテキストを表示させるには、<a href="https://help.autodesk.com/view/3DSMAX/2017/JPN/?guid=__files_GUID_930627BF_AC41_4033_9768_64CC5A1CE684_htm" rel="noopener" target="_blank">logsystem.logEntry</a> を broadcast:true とともに使用してください。</p>
<p>また、コアエンジンに 3ds Max Batch（3dsmaxbatch.exe） を使用しているので、Activity 定義の CommandLine に、Autodesk Knowledge Network 記事 <a href="https://knowledge.autodesk.com/ja/support/3ds-max/learn-explore/caas/CloudHelp/cloudhelp/2019/JPN/3DSMax-Batch/files/GUID-48A78515-C24B-4E46-AC5F-884FBCF40D59-htm.html" rel="noopener" target="_blank">3ds Max Batch を使用する</a> に記載の -v オプションで 5 （ デバッグ レベルのメッセージ）を指定する必要があります。</p>
<p>これによって、「羽根」の色や「葉っぱ」の有無、ビュー情報などが、クライアント（Web ブラウザ実装）から MAXScript にパラメータが正しく渡されているか、確認出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f96f171200c-pi" style="display: inline;"><img alt="Canvas_split" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f96f171200c image-full img-responsive" src="/assets/image_875880.jpg" title="Canvas_split" /></a></p>
<p>下記は、<span style="background-color: #ffffff;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/02/da4m-reflect-view-from-forge-viewer.html" rel="noopener" style="background-color: #ffffff;" target="_blank">Design Automation API for 3ds Max：Forge Viewer ビューの反映</a></span> でご紹介した MAXScript に用意したログ主力を意識した Activity です。</p>
<div>
<blockquote>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&quot;id&quot;: &#39;TableFanConfiguratorRender&#39;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&quot;commandLine&quot;: [&#39;$(engine.path)\\3dsmaxbatch.exe <strong><span style="color: #0000ff;">-v 5</span></strong> -sceneFile &quot;$(args[InputMaxScene].path)&quot;<span style="color: #111111;"> &quot;$(args[MaxscriptToExecute].path)&quot;&#39;],</span></span></div>
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
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #111111;">&#0160; &#0160; &quot;MaxscriptToExecute&quot;: {</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #111111;">&#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #111111;">&#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #111111;">&#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;MAXScript to render scene on 3dsmaxbatch.exe&quot;,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #111111;">&#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #111111;">&#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #111111;">&#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;myscript.ms&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #111111;">&#0160; &#0160; },</span></div>
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
<p>また、ログ出力する MAXScript は次のようになります。</p>
<div>
<blockquote>
<div><strong><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">logsystem.logEntry &quot;************ myscript process start ************&quot; info:true broadcast:true</span></strong></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;"><br />/* JSON 読み込み関数</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160;参照:https://forums.cgsociety.org/t/json-and-maxscript/1552038/11</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">*/</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">fn getJsonFileAsString filePath=(</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; local &#0160;jsonString = &quot;&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; fs=openFile filePath</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; while not eof fs do(</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; jsonString += readchar fs</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; )</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; close fs</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; return jsonString</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">)<br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* JSON 読み込み */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">paramsFilePath = &quot;params.json&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">jsonString = getJsonFileAsString paramsFilePath</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">myJObject = dotNetObject &quot;Newtonsoft.Json.Linq.JObject&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">myJObject = myJObject.parse jsonString<br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* パラメータ取得 */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global col = myJObject.item[&quot;color&quot;].value as integer</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global leaf = myJObject.item[&quot;leaf&quot;].value as booleanClass</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global width = myJObject.item[&quot;width&quot;].value as integer</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global height = myJObject.item[&quot;height&quot;].value as integer</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global fov = myJObject.item[&quot;fov&quot;].value as double</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global temp = myJObject.item[&quot;position&quot;].value</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global pos = FilterString temp &quot;,&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global temp = myJObject.item[&quot;rotation&quot;].value</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global rot = FilterString temp &quot;,&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;"><span style="color: #0000ff;"><strong>logsystem.logEntry (&quot;*** width, height = &quot; + width as string + &quot;, &quot; + height as string) info:true broadcast:true</strong></span><br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* ColorX 画層オフ */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global lay</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">for i = 1 to 6 do</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">(</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; if col != i then (</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; lay = LayerManager.getLayerFromName ( &quot;Color&quot; + i as string )</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; lay.on = false</span></div>
<div><span style="color: #0000ff;"><strong><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; logsystem.logEntry (&quot;*** trun off layer : &quot; + lay.name) info:true broadcast:true</span></strong></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; )</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">)<br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* Leaf 画層オン/オフ */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">lay = LayerManager.getLayerFromName ( &quot;Leaf&quot; )</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">lay.on = leaf<br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* ART レンダラー設定 */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global art = ART_Renderer()</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.quality_db = 20</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.render_method = 1</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.anti_aliasing_filter_diameter = 2.0</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.enable_noise_filter = true</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.noise_filter_strength = 1</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">renderers.current = art<br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* カメラ設定 */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">x = rot[1] as float</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">y = rot[2] as float</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">z = rot[3] as float</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">w = rot[4] as float</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global cam = freecamera rotation:(quat x y z w) position:[pos[1] as float, pos[2] as float, pos[3] as float] fov:fov<br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* レンダリング &amp; 生成画像保存 */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">fname = sysInfo.currentdir + &quot;/result.jpg&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">undisplay ( render camera:cam outputwidth:width outputheight:height outputfile:fname )</span></div>
<div><span style="color: #0000ff;"><strong><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">logsystem.logEntry &quot;************ myscript process end ************&quot; info:true broadcast:true</span></strong></span></div>
</blockquote>
</div>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f963663200c-pi" style="display: inline;"><img alt="Log_output" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f963663200c image-full img-responsive" src="/assets/image_313679.jpg" title="Log_output" /></a></p>
<p>By Toshiaki Isezaki</p>
