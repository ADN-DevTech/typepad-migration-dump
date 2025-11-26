---
layout: "post"
title: "Design Automation API for 3ds Max：MAXScript での JSON 読込み"
date: "2022-01-26 00:04:39"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-read-json-by-maxmaxscript.html "
typepad_basename: "da4m-read-json-by-maxmaxscript"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-maxscript-and-3dsmaxbatch.html" rel="noopener" target="_blank"><span style="background-color: #ffffff;">Design Automation API for 3ds Max：MAXScript と 3ds Max Batch </span></a>の続きです。Design Automation API でコアエンジンにアドイン/プラグインに自動処理をさせる際、多くの Forge アプリが、指定したパラメータによって処理内容を変化させています。Forge アプリはフロント エンドとして Web ページを持っているので、パラメータ渡しに Web で親和性に高い <a href="https://ja.wikipedia.org/wiki/JavaScript_Object_Notation" rel="noopener" target="_blank">JSON</a> が利用されるのが一般的です。</p>
<p>Web ページから得たパラメータは、Design Automation API の WorkItem 起動時に JSON データとして渡すことで、Activity で定義したファイル名で WorkItem の作業領域に保存されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13ebccc200b-pi" style="display: inline;"><img alt="Param_json" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13ebccc200b image-full img-responsive" src="/assets/image_565776.jpg" title="Param_json" /></a></p>
<p>コアエンジンにロードして実行されるアドイン/プラグインは、作業領域の JSON ファイルを読み込んで処理に反映すればいいわけです。</p>
<p>Design Automation API for 3ds Max で MAXScript を実行させる場合、MAXScript が JSON ファイルにアクセスする機能は提供されていないため、この部分がネックになります。幸い、MAXScript は <a href="https://help.autodesk.com/view/3DSMAX/2017/JPN/?guid=__files_GUID_779FD7AC_953D_4567_B2A8_60B1D8695B95_htm" rel="noopener" target="_blank">.NET との親和性</a>も持ち合わせているので、.NET から機能を流用することが出来ます。</p>
<p>Forge の学習リソースである Learn Forge では、Design Automation API for 3ds Max 用に .NET プラグイン アプリを用いる方法を<a href="https://learnforge.autodesk.io/#/ja-JP/designautomation/appbundle/engines/max" rel="noopener" target="_blank">ご紹介</a>しています。このプラグインでは、JSON ファイルの読み込みに <a href="https://www.newtonsoft.com/json" rel="noopener" target="_blank">Newtonsoft.Json</a> を使用しています。</p>
<p>MAXScript でも Newtonsoft.Json を使った JSON ファイル読み込みの方法が<a href="https://forums.cgsociety.org/t/json-and-maxscript/1552038/11" rel="noopener" target="_blank">議論</a>されています。ローカル環境の 3ds Max インストール フォルダに Newtonsoft.Json.dll が配置されていれば、この<span style="color: #0000ff;">方法</span>を参考に、次のような MAXScript で JSON ファイルの内容をスクリプトに反映させることが出来ます。</p>
<div>
<blockquote>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">/* Newtonsoft.Json アセンブリ読み込み */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">json = dotNet.loadAssembly &quot;Newtonsoft.Json.dll&quot;<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">/* JSON 読み込み関数</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160;参照:https://forums.cgsociety.org/t/json-and-maxscript/1552038/11</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">*/</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">fn getJsonFileAsString filePath=(</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; local &#0160;jsonString = &quot;&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; fs=openFile filePath</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; while not eof fs do(</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; jsonString += readchar fs</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; )</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; close fs</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">&#0160; &#0160; return jsonString</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;"><span style="color: #0000ff;">)</span><br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* シーンリセット &amp; DWG 読み込み */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">resetMaxFile #noprompt</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">importFile &quot;C:/<em>&lt;your path&gt;</em>/Table Fan.dwg&quot; #noprompt<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">/* JSON 読み込み */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">paramsFilePath=&quot;C:/<span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;"><em>&lt;your path&gt;</em></span>/params.json&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">jsonString = getJsonFileAsString paramsFilePath</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">myJObject = dotNetObject &quot;Newtonsoft.Json.Linq.JObject&quot;</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">myJObject = myJObject.parse jsonString<br /><br /></span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">/* パラメータ取得 */</span></div>
<div>
<div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">global col = myJObject.item[&quot;color&quot;].value as integer</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt; color: #0000ff;">global leaf = myJObject.item[&quot;leaf&quot;].value as booleanClass</span></div>
</div>
</div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160;</span></div>
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
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">undisplay ( render outputfile:&quot;C:/<em>&lt;your path&gt;</em>/result.jpg&quot; )</span></div>
</blockquote>
</div>
<ul>
<li>前回のブログ記事 <a href="https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-maxscript-and-3dsmaxbatch.html" rel="noopener" target="_blank"><span style="background-color: #ffffff;">Design Automation API for 3ds MAX：MAXScript と 3ds Max Batch</span></a><span style="background-color: #ffffff; color: #111111;">&#0160;</span>のスクリプトを拡張して、卓上扇風機モデルの「羽根」の色と「葉っぱ」の有無をレンダリング画像に反映するものです。前提とする JSON ファイルはこのような内容です。</li>
</ul>
<div>
<blockquote>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">{</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &quot;color&quot;: 5,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &quot;quantity&quot;: 1,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &quot;leaf&quot;: false</span><br /><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">}</span></div>
</blockquote>
</div>
<p>もちろん、上記スクリプトを 3ds Max Batch でレンダリング処理を実行することも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13ec121200b-pi" style="display: inline;"><img alt="3dsmaxbatch" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13ec121200b image-full img-responsive" src="/assets/image_295897.jpg" title="3dsmaxbatch" /></a></p>
<p>生成されるレンダリング画像は、先のパラメータ JSON の内容（「羽根」の色を青 - 色番号 5、「葉っぱ」無し）の状態を反映して、次のようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880688106200d-pi" style="display: inline;"><img alt="Result2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880688106200d image-full img-responsive" src="/assets/image_725873.jpg" title="Result2" /></a></p>
<p>By Toshiaki Isezaki</p>
