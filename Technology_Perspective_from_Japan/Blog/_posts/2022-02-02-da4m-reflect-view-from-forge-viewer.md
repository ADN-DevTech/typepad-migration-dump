---
layout: "post"
title: "Design Automation API for 3ds Max：Forge Viewer ビューの反映"
date: "2022-02-02 00:03:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/02/da4m-reflect-view-from-forge-viewer.html "
typepad_basename: "da4m-reflect-view-from-forge-viewer"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-rendering-workflow.html" rel="noopener" target="_blank"><span style="background-color: #ffffff;">Design Automation API for 3ds Max：MAXScript レンダリング フロー</span></a> で生成されるレンダリング画像は、シーンファイル Table Fan.max の第 4 ビューポートになっていました。</p>
<p>今回は、Forge Viewer 上で表示しているビュー（カメラ）を 3ds Max シーンに反映して、レンダリング画像を得る方法を考察してみます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880689dbb200d-pi" style="display: inline;"><img alt="Intention" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880689dbb200d image-full img-responsive" src="/assets/image_618786.jpg" title="Intention" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2022/01/restore-named-view-from-manifest.html" rel="noopener" target="_blank">Forge Viewer：シードファイルのビュー復元</a> で触れたとおり、Forge では,、シードファイル（元のデザインファイルが持つビューを Forge Viewer で表示するための情報を得る方法が用意されています。</p>
<p>逆に Forge Viewer からシードファイルへのビューの反映については、シードファイルへの書き込みをしない「Viewer」としての位置づけから、特に一貫した方法が用意されていない状況です。ただ、Forge ポータルへの過去の<a href="https://forge.autodesk.com/en/support/get-help" rel="noopener" target="_blank">問い合わせ</a>実績から、一定の需要があることも知られています。</p>
<p>英語のブログ記事になってしまいますが、<a href="https://forge.autodesk.com/ja/node/1311" rel="noopener" target="_blank">Map Forge Viewer Camera back to Revit</a>&#0160;や&#0160;<a href="https://forge.autodesk.com/ja/node/1312" rel="noopener" target="_blank">Map Forge Viewer Camera back to Navisworks</a> では、<a href="https://adndevblog.typepad.com/technology_perspective/2021/12/view-change-by-state-api.html" rel="noopener" target="_blank">Forge Viewer：State API でビューを更新</a> でもご紹介した State API からのビュー情報を利用する方法が説明されています。</p>
<p>3ds Max では、少し前の Autodesk University クラス、<a href="https://www.autodesk.com/autodesk-university/class/3ds-Max-Design-Automation-Add-Beautiful-Renders-Your-Web-Site-2020" rel="noopener" target="_blank">3ds Max Design Automation: Add Beautiful Renders to Your Web Site</a> で、少し違った方法が紹介されています。いずれも、あらゆるケースで試行したものではありませんので、ここでご紹介する内容も、調整が必要になる可能性があります。その点は事前にご了承ください。</p>
<p>前述の Autodesk University クラスの手法では、State API ではなく、次のようにビュー情報を得ています。</p>
<div>
<blockquote>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">getCameraCoordinates() {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let cam = this.viewer.getCamera()</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let matrix = cam.matrixWorld</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let position = new THREE.Vector3();</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let rotation = new THREE.Quaternion();</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let scale = new THREE.Vector3();</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; matrix.decompose(position, rotation, scale)<br /><br /></span></div>
<div><span style="color: #0000ff;"><strong><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; //TODO: replace magic numbers with derived ones</span></strong></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;"><span style="color: #0000ff;"><strong>&#0160; &#0160; let offset = new THREE.Vector3(60.5, -24.5, 60.5)</strong></span><br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; position.addVectors(position, offset);</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let aspect = cam.aspect;<br /><br /></span></div>
<div><span style="color: #0000ff;"><strong><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; //TODO: derive the 1.67 multiplicator</span></strong></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;"><span style="color: #0000ff;"><strong>&#0160; &#0160; let new_fov = cam.fov*1.67;</strong></span><br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; return {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; position: position.toArray(),</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; rotation: rotation.toArray(),</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; fov: new_fov,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; renderingSize: [this.viewer.canvas.width, this.viewer.canvas.height]</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">}</span></div>
</blockquote>
</div>
<p>青字で記した部分は、シーン毎に調整が必要な値であることを意味しています。</p>
<p>Forge Viewer は、シードファイルから得た 3D モデルを、カンバス領域の中心に表示するようになっています。この際にシードファイル時の位置との差を補正しているのが、Global Offset（グローバル オフセット）と呼ばれる位置合わせ用の（X、Y、Z）値です。上記コードの最初の <span style="color: #0000ff;"><strong>// TO DO</strong></span> には、この Global Offset を当てはめることが出来ます。Global Offset については、過去に&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2020/01/forge-viewer-showing-mulriple-modelsson-on-forge-viewer-scene.html" rel="noopener" target="_blank">Forge Viewer シーンへの複数モデルの表示（一部改定・追記）</a> のブログ記事でも触れたことがあります。</p>
<p>2 つめの <span style="color: #0000ff;"><strong>// TO DO</strong></span> には、Web ブラウザで表示している Forge Viewer 上の 3D モデルと、レンダリングした画像を、オーバーラップした領域に同じ大きさで表示させる、FOV（Field Of View、視野角）の値に乗算させる画像表示用の任意の係数が含まれます。</p>
<p>下記は、前述の <span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">getCameraCoordinates() に&#0160;</span>Global Offset と表示係数を設定し、JSON パラメータしてWeb サーバーへ投げかける箇所の抜粋です。</p>
<div>
<blockquote>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">function getCameraCoordinates() {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let cam = _viewer.getCamera();</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let matrix = cam.matrixWorld</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let position = new THREE.Vector3();</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let rotation = new THREE.Quaternion();</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let scale = new THREE.Vector3();</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; matrix.decompose(position, rotation, scale);<br /><br /></span></div>
<div><strong><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; //TODO: replace magic numbers with derived ones</span></strong></div>
<div><strong><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; let element = JSON.stringify(_viewer.model.getData().globalOffset);</span></strong></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;"><strong><span style="color: #0000ff;">&#0160; &#0160; let offset = new THREE.Vector3(JSON.parse(element).x, JSON.parse(element).y, JSON.parse(element).z);</span></strong><br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; position.addVectors(position, offset);</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; let aspect = cam.aspect;<br /><br /></span></div>
<div><span style="color: #0000ff;"><strong><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; //TODO: derive the 1.67 multiplicator</span></strong></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;"><span style="color: #0000ff;"><strong>&#0160; &#0160; let new_fov = cam.fov * 2.4; // Table Fan.max</strong></span><br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; return {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; position: position.toArray(),</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; rotation: rotation.toArray(),</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; fov: new_fov,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; renderingSize: [_viewer.canvas.width, _viewer.canvas.height]</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; }</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">}<br /><br /></span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">// Render button</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">$(document).on(&quot;click&quot;, &quot;[id^=&#39;start&#39;]&quot;, function () {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; var vp = getCameraCoordinates();</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; var params = &#39;&amp;color=&#39; + _colorIndex +</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#39;&amp;quantity=&#39; + $(&quot;#quantity&quot;).val() +</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#39;&amp;leaf=&#39; + _leafFlag +</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#39;&amp;width=&#39; + JSON.parse(JSON.stringify(vp)).renderingSize[0] +</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#39;&amp;height=&#39; + JSON.parse(JSON.stringify(vp)).renderingSize[1] +</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#39;&amp;fov=&#39; + JSON.parse(JSON.stringify(vp)).fov +</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#39;&amp;position=&#39; + JSON.parse(JSON.stringify(vp)).position +</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &#0160; &#0160; &#39;&amp;rotation=&#39; + JSON.parse(JSON.stringify(vp)).rotation;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; var uri = &#39;/api/process/&#39; + <span style="color: #0000ff;">params</span>;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; $.ajax({</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; url: uri,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; type: &#39;POST&#39;,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; contentType: &#39;text/plain&#39;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; }).done(function (res) {</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; ...</span></div>
</blockquote>
</div>
<div>このコードから渡される JSON パラメータは次のようになります。<span style="color: #0000ff;">青字</span>部分が<a href="https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-rendering-workflow.html" rel="noopener" target="_blank"><span style="background-color: #ffffff;">前回</span></a>の JSON パラメータから拡張された部分です。</div>
<div>
<div>
<blockquote>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">{</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &quot;color&quot;: &quot;3&quot;,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &quot;quantity&quot;: &quot;1&quot;,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &quot;leaf&quot;: &quot;true&quot;,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &quot;width&quot;: &quot;1920&quot;,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &quot;height&quot;: &quot;942&quot;,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &quot;fov&quot;: &quot;54.28767677618733&quot;,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &quot;position&quot;: &quot;-241.98164558410645,-447.09261417388916,179.48493644408882&quot;,</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">&#0160; &#0160; &quot;rotation&quot;: &quot;0.6990782139317431,-0.16547652955068382,-0.1602335643066114,0.6769286269720552&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">}</span></div>
</blockquote>
</div>
</div>
<div>カンバスの幅と高さをレンダリング画像のサイズとして渡すのは、Forge Viewer のカンバスに重ね合わせて表示するこをを意図しているためです。<br /><br />次のコードは、Web サーバー実装でルーティングに用意した /process endpoint から呼び出した WorkItem が処理する MAXScript です。<span style="color: #0000ff;">青字</span>部分が<span style="background-color: #ffffff;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-rendering-workflow.html" rel="noopener" style="background-color: #ffffff;" target="_blank">前回</a></span>の&#0160; MAXScript から拡張された部分です。レンダリングするビューを、JSON パラメータを元に作成したカメラに設定していることがわかります。</div>
<div>
<blockquote>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* JSON 読み込み関数</span></div>
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
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">)</span></div>
<br />
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* JSON 読み込み */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">paramsFilePath = &quot;params.json&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">jsonString = getJsonFileAsString paramsFilePath</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">myJObject = dotNetObject &quot;Newtonsoft.Json.Linq.JObject&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">myJObject = myJObject.parse jsonString</span></div>
<br />
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* パラメータ取得 */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global col = myJObject.item[&quot;color&quot;].value as integer</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global leaf = myJObject.item[&quot;leaf&quot;].value as booleanClass</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">global width = myJObject.item[&quot;width&quot;].value as integer</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">global height = myJObject.item[&quot;height&quot;].value as integer</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">global fov = myJObject.item[&quot;fov&quot;].value as double</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">global temp = myJObject.item[&quot;position&quot;].value</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">global pos = FilterString temp &quot;,&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">temp = myJObject.item[&quot;rotation&quot;].value</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">global rot = FilterString temp &quot;,&quot;</span></div>
<br />
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* ColorX 画層オフ */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global lay</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">for i = 1 to 6 do</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">(</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; if col != i then (</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; lay = LayerManager.getLayerFromName ( &quot;Color&quot; + i as string )</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; &#0160; &#0160; lay.on = false</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">&#0160; &#0160; )</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">)</span></div>
<br />
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* Leaf 画層オン/オフ */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">lay = LayerManager.getLayerFromName ( &quot;Leaf&quot; )</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">lay.on = leaf</span></div>
<br />
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* ART レンダラー設定 */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">global art = ART_Renderer()</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.quality_db = 20</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.render_method = 1</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.anti_aliasing_filter_diameter = 2.0</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.enable_noise_filter = true</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">art.noise_filter_strength = 1</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">renderers.current = art</span></div>
<br />
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">/* カメラ設定 */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">x = rot[1] as float</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">y = rot[2] as float</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">z = rot[3] as float</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">w = rot[4] as float</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif; color: #0000ff;">global cam = freecamera rotation:(quat x y z w) position:[pos[1] as float, pos[2] as float, pos[3] as float] fov:fov</span></div>
<br />
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">/* レンダリング &amp; 生成画像保存 */</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">fname = sysInfo.currentdir + &quot;/result.jpg&quot;</span></div>
<div><span style="font-size: 10pt; font-family: arial, helvetica, sans-serif;">undisplay ( render <span style="color: #0000ff;">camera:cam outputwidth:width outputheight:height</span> outputfile:fname )</span></div>
</blockquote>
<div>下記は、Full HD&#0160; 解像度（1920 x1080）環境で最大化したブラウザ サイズ条件で表示調整した例です。Forge Viewer とレンダリング画像のカンバスを、スライダで分割表示させています。スライダ境界右側がビュー情報を抽出した Forge Viewer、左側がビュー情報を反映して生成されたレンダリング画像 です。</div>
</div>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880680abe200d-pi" style="display: inline;"><img alt="Fan_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880680abe200d image-full img-responsive" src="/assets/image_711779.jpg" title="Fan_model" /></a></p>
<p>Web ブラウザのズーム設定（Web ページの表示スケール）やウィンドウ サイズ（カンバスのサイズ）だけでなく、Windows 側のデスクトップ表示スケールの要素もあり、条件に合わせて表示状態を動的に変化させてしまう Forge Viewer との表示の一致はチャレンジな部分があります。</p>
<p>ただし、前提条件を固定化出来るのであれば、このシナリオを別のシーンで利用することも可能かと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806806ee200d-pi" style="display: inline;"><img alt="Aec_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806806ee200d image-full img-responsive" src="/assets/image_952341.jpg" title="Aec_model" /></a></p>
<p>By Toshiaki Isezaki</p>
