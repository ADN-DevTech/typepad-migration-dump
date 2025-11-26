---
layout: "post"
title: "Design Automation：さまざまな Activity と WorkItem"
date: "2022-12-14 00:20:54"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/11/design-automation-various-activities-and-workitems.html "
typepad_basename: "design-automation-various-activities-and-workitems"
typepad_status: "Publish"
---

<p>Design Automation API を利用するには、Activity でコアエンジンで処理させる入出力ファイルを宣言、実際の実行タスクとなる WorkItem の実行時に、Activity で宣言した入出力ファイルの物理的なパス/URL を指定することになります。</p>
<p>Activity の登録には &#0160;<a class="reference external" href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/activities-POST" rel="noopener" target="_blank">POST activities</a> エンドポイントを、WorkItem の実行には <a class="reference external" href="https://forge.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST" rel="noopener" target="_blank">POST workitems</a> エンドポイントを、それぞれ利用することになります。</p>
<p>両エンドポイントのリクエスト ボディで指定する JSON ペイロードには、リファレンスに記載されるさまざまなパラメータを利用します。</p>
<p style="padding-left: 40px;"><a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/activities-POST/#body-structure" rel="noopener" target="_blank">POST activities | Request body</a></p>
<p style="padding-left: 40px;"><a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/workitems-POST/#body-structure" rel="noopener" target="_blank">POST workitems | Requst Body</a></p>
<p>入手力ファイルを指定するパラメータは、アプリの内容によって様々なパターンに別れます。ここでは、<a href="https://adndevblog.typepad.com/technology_perspective/2022/11/design-automation-api-multipart-support-s3-upload.html" rel="noopener" target="_blank">Design Automation API：Direct-to-S3 アプローチを簡素化する新機能</a> の方法を使って、AutoCAD コアエンジンの代表的な例でご紹介します。</p>
<p>※ WorkItem で指定する <span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">BUCKET_KEY&#0160; と OBJECT_KEY には <a href="https://ja.wikipedia.org/wiki/%E3%83%91%E3%83%BC%E3%82%BB%E3%83%B3%E3%83%88%E3%82%A8%E3%83%B3%E3%82%B3%E3%83%BC%E3%83%87%E3%82%A3%E3%83%B3%E3%82%B0" rel="noopener" target="_blank">URL エンコード</a>された値が必要です。</span></p>
<hr />
<p><strong>固定ファイル名での入出力</strong></p>
<p>コイルの回転数、半径、高さを入力してコアエンジンにパラメータとして渡し、コイル形状のスイープ ソリッドを作成して 3D DWG をダウンロードさせる例です。</p>
<p>事前に画層やレイアウト ビューポートを設定したテンプレート DWG を利用することで、モデル空間にスイープ ソリッドを作成、レイアウトでモデル空間のスイープ ソリッドを投影を表示させます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14875134200c-pi" style="display: inline;"><img alt="Coil_creation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14875134200c image-full img-responsive" src="/assets/image_784608.jpg" title="Coil_creation" /></a></p>
<p>入力ファイルのテンプレート DWG を template.dwg、パラメータ ファイル（JSON）を <span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">params.json、</span>出力ファイルは result.dwg の固定名にしています。Activity と WorkItem の JSON ペイロードは次のようになります。</p>
<p><strong>Activity：</strong></p>
<div>
<blockquote>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">var payload =</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">{</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;id&quot;: DA4A_UQ_ID,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;commandLine&quot;: [&#39;$(engine.path)\\accoreconsole.exe /i &quot;$(args[DWGInput].path)&quot; /al &quot;$(appbundles[CreateCoil].path)&quot; /s &quot;$(settings[script].path)&quot;&#39;],</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;parameters&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGInput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Template drawing&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true, <br />&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;template.dwg&quot;<br /></span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;Params&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Input parameters to create coil&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;params.json&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;put&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Created drawing&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;result.dwg&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;settings&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;script&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;value&quot;: &quot;CreateCoil\n&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;engine&quot;: &quot;Autodesk.AutoCAD+23_1&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;appbundles&quot;: [DA4A_FQ_ID],</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;description&quot;: &quot;Create a coil solid to new drawing&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">};</span></div>
</blockquote>
</div>
<p><strong>WorkItem：</strong></p>
<blockquote>
<p><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">var payload =</span><br /><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">{</span></p>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;activityId&quot;: DA4A_FQ_ID,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;arguments&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGInput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/template.dwg&quot; ,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;Params&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;data:application/json,&quot; + paramsJSON // ex). {&quot;turn&quot;:&quot;5&quot;,&quot;radius&quot;:&quot;50&quot;,&quot;height&quot;:&quot;100&quot;}</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/result.dwg&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &#39;put&#39;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">};</span></div>
</blockquote>
<p>次の例は、卓上扇風機の羽根の色、葉っぱ形状の有無、オーダーする数量をコアエンジンに渡し、あらかじめ用意した DWG の内容を編集して見積書レイアウトと指示書図面レイアウトを用意、 1 つの PDF ファイルに出力してダウンロードさせる例です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a8e4ad200b-pi" style="display: inline;"><img alt="Fan_condigurator" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a8e4ad200b image-full img-responsive" src="/assets/image_243917.jpg" title="Fan_condigurator" /></a></p>
<p>入力ファイルとなるテンプレート DWG を template.dwg、パラメータ ファイル（JSON）を <span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">params.json、</span>出力ファイルには&#0160;<span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">quotation.pdf</span> の固定名にしています。また、Design Automation API の実行環境にない TrueType フォントを解決するために、フォント マッピング ファイルを&#0160;<span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">dwg.fmp の固定名で</span>入力ファイルとしています。 フォント マッピング ファイルの詳細は、<a href="https://adndevblog.typepad.com/technology_perspective/2021/10/handling-and-resolution-on-custom-font.html" rel="noopener" target="_blank">Design Automation API for AutoCAD：カスタム フォントの扱いと解決</a> のブログ記事でご紹介しています。</p>
<p>この Activity と WorkItem の JSON ペイロードは次のようになります。</p>
<p><strong>Activity：</strong></p>
<div>
<blockquote>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">var payload =</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">{</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;id&quot;: DA4A_UQ_ID,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;commandLine&quot;: [&#39;$(engine.path)\\accoreconsole.exe /i &quot;$(args[DWGInput].path)&quot; /dwgfontmap /al &quot;$(appbundles[TableFanConfigurator].path)&quot; /s $(settings[script].path)&#39;],</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;parameters&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGInput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Template drawing&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;template.dwg&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;Params&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Input parameters to specify behavior&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;params.json&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;FontMap&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Input font map file for user truetype font&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;dwg.fmp&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;PDFOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;put&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;output PDF quotation&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;quotation.pdf&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;settings&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;script&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;value&quot;: &quot;CreateQuotation\n&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;engine&quot;: &quot;Autodesk.AutoCAD+24_1&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;appbundles&quot;: [DA4A_FQ_ID],</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;description&quot;: &quot;Create Quotation&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">};</span></div>
</blockquote>
</div>
<p><strong>WotkItem：</strong></p>
<div>
<blockquote>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">var payload =</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">{</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;activityId&quot;: DA4A_FQ_ID,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;arguments&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGInput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/template.dwg&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;Params&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;data:application/json,&quot; + paramsJSON // ex).&#0160; {&quot;color&quot;:&quot;2&quot;,&quot;quantity&quot;:&quot;1&quot;,&quot;leaf&quot;:&quot;true&quot;}</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;FontMap&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/dwg.fmp&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;PDFOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/quotation.pdf&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &#39;put&#39;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;onComplete&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;post&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;https://**********************/api/oncomplete&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">};</span></div>
</blockquote>
</div>
<hr />
<p><strong>可変ファイル名での入出力</strong></p>
<p>ユーザが選択した任意名の DWG 内部を走査し、不要なブロック定義を削除（パージ）、パージ後のクリーンな DWG を同じファイル名（選択した DWG による）でダウンロードさせる例です。</p>
<p>パージしたブロック定義名とブロック定義内の図形数をグラフ化するため、&#0160;<a href="https://www.chartjs.org/" rel="noopener" target="_blank"><strong>Chart.js</strong>（https://www.chartjs.org/）</a>へ渡す JSON ファイルも同時に<a href="https://adndevblog.typepad.com/technology_perspective/2021/10/da-retrieving-json-fromappbundle.html" rel="noopener" target="_blank">出力</a>してます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14a8e47c200b-pi" style="display: inline;"><img alt="Blovk_cleaner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14a8e47c200b image-full img-responsive" src="/assets/image_251215.jpg" title="Blovk_cleaner" /></a></p>
<p>入力ファイルとなる DWG ファイル名は可変になるため、Activity の <span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&quot;DWGInput&quot; と名付けたセクション配下の</span>&#0160;<span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">localName で特定名を指定はしていません。同様に、ここでは &quot;DWGOutput&quot; パラメータで指定する出力ファイル名も入力ファイルと同じファイル名にします。つまり、出力ファイル名も可変になりますが、localName 指定がないと Activity 登録時に Bad Request エラーになってしまうため、ダミーの名前&#0160; &quot;purged.dwg&quot; を指定しています。</span></p>
<p><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">出力ファイル名は、WorkItem 実行時に &quot;DWGOutput&quot; セクション配下に localName で指定することで、ファイル名をオーバーライドすることが出来ます。</span></p>
<p><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">なお、入力ファイルのパラメータ ファイル（JSON）は params.json、グラフ化で使用する出力ファイルは chart.json の固定にしています。</span></p>
<p><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">アドイン処理で chart.json を生成する内容は、<a href="https://adndevblog.typepad.com/technology_perspective/2021/10/da-retrieving-json-fromappbundle.html" rel="noopener" target="_blank">Design Automation API：WorkItem からの JSON 反映</a> のブログ記事でご紹介しています。</span></p>
<p><strong>Activity：</strong></p>
<div>
<blockquote>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">var payload =</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">{</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;id&quot;: DA4A_UQ_ID,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;commandLine&quot;: [&#39;$(engine.path)\\accoreconsole.exe /i &quot;$(args[DWGInput].path)&quot; /al &quot;$(appbundles[PurgeBlock].path)&quot; /s $(settings[script].path)&#39;],</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;parameters&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGInput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Source drawing&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;Params&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Input parameters to specify behavior&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;params.json&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;put&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Output DWG drawing&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;purged.dwg&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;ChartOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;put&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Output Chart JSON&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;chart.json&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;settings&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;script&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;value&quot;: &quot;PurgeBlock\n&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;engine&quot;: &quot;Autodesk.AutoCAD+24_1&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;appbundles&quot;: [DA4A_FQ_ID],</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;description&quot;: &quot;Purge Block&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">};</span></div>
</blockquote>
</div>
<p><strong>WorkItem：</strong></p>
<div>
<blockquote>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">var payload =</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">{</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;activityId&quot;: DA4A_FQ_ID,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;arguments&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGInput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/&quot; + SOURCE_DWG,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;Params&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;data:application/json,&quot; + paramsJSON // ex).&#0160; {&quot;purge&quot;:&quot;true&quot;,&quot;preview&quot;:&quot;false&quot;}</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/&quot; + RESULT_DWG, // 可変</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &#39;put&#39;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localname&quot;: RESULT_DWG // 可変</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;ChartOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: CHART_JSON,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &#39;put&#39;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;onComplete&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;post&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;https://**********************/api/oncomplete&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">};</span></div>
</blockquote>
</div>
<hr />
<p><strong>ZIP 圧縮の入力</strong></p>
<p>外部参照図面と親図面を ZIP 圧縮して、固定ファイル名で PDF 出力、ダウンロードさせる例です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af14875153200c-pi" style="display: inline;"><img alt="Pdf_output" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af14875153200c image-full img-responsive" src="/assets/image_349607.jpg" title="Pdf_output" /></a></p>
<p>DWG ファイル、ラスター画像ファイル、アンダーレイ ファイルなどを外部参照する DWG 図面を処理する際には、それら一式を ZIP 圧縮後、入力ファイルとしてコアエンジンに渡すことが出来ます。この際、Actvity の zip 指定を true で設定して、WorkItem の同じ名前のセクションで、コアエンジンに開かせる親ファイル名を指定します。親ファイル名の指定で利用するのが、<span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">pathInZip パラメータです。</span></p>
<p><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">なお、この Activity は AutoCAD のコマンドのみを利用するので、AppBundle を使用しません。このため、appbundles パラメータの値は <a href="https://aps.autodesk.com/en/docs/design-automation/v3/reference/http/appbundles-POST/" rel="noopener" target="_blank">POST appbundles</a> エンドポイントを使った登録で使用する AppBundle の Id ではなく、ブランク（[]）になっています。</span></p>
<p><strong>Activity：</strong></p>
<div>
<blockquote>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">var payload =</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">{</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;id&quot;: DA4A_UQ_ID,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;commandLine&quot;: [&#39;$(engine.path)\\accoreconsole.exe /i &quot;$(args[DWGInput].path)&quot; /s &quot;$(settings[script].path)&quot; /l ja-JP&#39;],</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;parameters&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGInput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;Source drawing&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;PDFOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;zip&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;ondemand&quot;: false,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;put&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;description&quot;: &quot;putput PDF drawing&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;required&quot;: true,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;localName&quot;: &quot;result.pdf&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;settings&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;script&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;value&quot;: &quot;_tilemode 0 -export _pdf _all result.pdf\n&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;engine&quot;: &quot;Autodesk.AutoCAD+24_1&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;appbundles&quot;: [],</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;description&quot;: &quot;PDF output&quot;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">};</span></div>
</blockquote>
</div>
<p><strong>WorkItem：</strong></p>
<div>
<blockquote>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">var payload =</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">{</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;activityId&quot;: DA4A_FQ_ID,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &quot;arguments&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;DWGInput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;pathInZip&quot;: PARENT_DWG, // ex) . 8th floor.dwg</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/&quot; + SOURCE_ZIP, // ユーザ選択の任意ファイル名</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;get&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;PDFOutput&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;urn:adsk.objects:os.object:&quot; + BUCKET_KEY + &quot;/result.pdf&quot;,</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;headers&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;Authorization&quot;: &quot;Bearer &quot; + credentials.access_token</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &#39;put&#39;</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; },</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &quot;onComplete&quot;: {</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;verb&quot;: &quot;post&quot;,</span></div>
<div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &quot;url&quot;: &quot;https://**********************/api/oncomplete&quot;</span></div>
</div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; }</span></div>
<div><span style="font-family: tahoma, arial, helvetica, sans-serif; font-size: 10pt;">};</span></div>
</blockquote>
</div>
<hr />
<p>By Toshiaki Isezaki</p>
