---
layout: "post"
title: "APS Viewer：スクリーンキャプチャ"
date: "2023-02-13 00:01:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/02/aps-viewer-screen-capture.html "
typepad_basename: "aps-viewer-screen-capture"
typepad_status: "Publish"
---

<p>APS Viewer でカンバス上に表示中の状態をキャプチャ画像として取得したい場面があります。</p>
<p>キーボード ショートカット（Windows&#0160; の場合、例えば [Alt]+[Print Screen] キーなど）やキャプチャー ツールでアプリの表示画面をキャプチャすることが出来ますが、ウィンドウ タイトルやフレームも映り込んでしまいます。Viewer 内のツールバーやパネルも同様です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7517033bc200b-pi" style="display: inline;"><img alt="Environment2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7517033bc200b image-full img-responsive" src="/assets/image_36941.jpg" title="Environment2" /></a></p>
<p>ツールバーやパネルなどのユーザインタフェースを排除して、純粋にカンバスの表示内容だけをキャプチャしたい場合には、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#getscreenshot-w-h-cb-overlayrenderer" rel="noopener" target="_blank">getScreenShot</a> メソッドを利用することが出来ます。</p>
<div>
<blockquote>
<div>_viewer.getScreenShot(_viewer.container.clientWidth, _viewer.container.clientHeight,);</div>
</blockquote>
</div>
<p>上記のように使用すると、ブラウザ上に新しいタブが開かれて、キャプチャした画像が表示されます。なお、上記コード中の _viewer は Viewer インスタンスを格納する変数です。<a href="https://adndevblog.typepad.com/technology_perspective/2021/12/access_viewer_instance.html" rel="noopener" target="_blank">NOP_VIEWER</a> に置き換えて考えることも可能です。</p>
<p>キャプチャ画像をファイルとして入手したい場合には、第３パラメータにコールバックを設定して Blob URL からダウンロードすることも出来ます。</p>
<div>
<blockquote>
<div>_viewer.getScreenShot(_viewer.container.clientWidth, _viewer.container.clientHeight, function (blobURL) {</div>
<div>&#0160; &#0160; const a = document.createElement(&#39;a&#39;);</div>
<div>&#0160; &#0160; document.body.appendChild(a);</div>
<div>&#0160; &#0160; a.href = blobURL;</div>
<div>&#0160; &#0160; console.log(a.href);</div>
<div>&#0160; &#0160; a.download = &#39;captured screen.png&#39;;</div>
<div>&#0160; &#0160; a.click();</div>
<div>&#0160; &#0160; document.body.removeChild(a);</div>
<div>});</div>
</blockquote>
</div>
<p>使用する「環境」にもよりますが、先のバスルームのように、「周囲光の影」（Ambient Occlusion）が投げかける影によって表現したいモデルがくすんで見えたり、強調したい微細な箇所が明瞭にならない場合も出てきます。次の例は、[グリッドライト] 環境選択時に「周囲光の影」をオンにした状態の Viewer 画面です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75194852d200c-pi" style="display: inline;"><img alt="Shadow_setting" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75194852d200c image-full img-responsive" src="/assets/image_9061.jpg" title="Shadow_setting" /></a></p>
<p>「周囲光の影」設定は、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#setqualitylevel-usesao-usefxaa" rel="noopener" target="_blank">setQualityLevel</a> メソッドの第１パラメータで指定することが出来ます。このパラメータ値を false で「周囲光の影」をオフにしてから、キャプチャすると、オン時よりも明瞭な形状確認が出来る場合があります。</p>
<blockquote>
<p>_viewer.setQualityLevel(false, true);</p>
</blockquote>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7517034c7200b-pi" style="display: inline;"><img alt="W_captured screen (1)" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7517034c7200b image-full img-responsive" src="/assets/image_457181.jpg" title="W_captured screen (1)" /></a></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-forge-viewer-simple-scene-customize.html" rel="noopener" target="_blank">Forge Viewer：簡単なシーン カスタマイズ</a> でもご紹介したように、Viewer 内の「環境」はプログラムからもコントロールすることが出来るので、環境変更後にキャプチャすることも可能です。次の例は、先のパスルームについて、[寒色ライト] 環境に、また、「周囲光の影」をオフに、それぞれ設定を変更してからキャプチャした画像です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b68527443b200d-pi" style="display: inline;"><img alt="Captured screen" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b68527443b200d image-full img-responsive" src="/assets/image_536863.jpg" title="Captured screen" /></a></p>
<p>なお、<a href="https://aps.autodesk.com/blog/using-autodeskviewingmarkupscore-extension" rel="noopener" target="_blank">Autodesk.Viewing.MarkupsCore</a> エクステンションを利用したマークアップは、<a href="https://aps.autodesk.com/en/docs/viewer/v7/reference/Viewing/GuiViewer3D/#getscreenshot-w-h-cb-overlayrenderer" rel="noopener" target="_blank">getScreenShot</a> メソッドのみではキャプチャされませんのでご注意ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75170114d200b-pi" style="display: inline;"><img alt="Markup" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75170114d200b image-full img-responsive" src="/assets/image_859716.jpg" title="Markup" /></a></p>
<p>カンバス上の内容と同時にマークアップもキャプチャしたい場面では、<a href="https://aps.autodesk.com/en/docs/viewer/v2/reference/javascript/markupscore/" rel="noopener" target="_blank">MarkupsCore</a> エクステンションの <a href="https://aps.autodesk.com/en/docs/viewer/v2/reference/javascript/markupscore/#rendertocanvas-context" rel="noopener" target="_blank">renderToCanvas</a> メソッドを併用する必要があります。</p>
<div>
<blockquote>
<div>
<div>let screenshot = new Image();</div>
<div>screenshot.onload = async function () {</div>
<div>&#0160; &#0160; let canvas = document.createElement(&#39;canvas&#39;);</div>
<div>&#0160; &#0160; canvas.width = _viewer.container.clientWidth;</div>
<div>&#0160; &#0160; canvas.height = _viewer.container.clientHeight;</div>
<div>&#0160; &#0160; let ctx = canvas.getContext(&#39;2d&#39;);</div>
<div>&#0160; &#0160; ctx.clearRect(0, 0, canvas.width, canvas.height);</div>
<div>&#0160; &#0160; ctx.drawImage(screenshot, 0, 0, canvas.width, canvas.height);</div>
<br />
<div>&#0160; &#0160; _markup = _viewer.getExtension(&#39;Autodesk.Viewing.MarkupsCore&#39;);</div>
<div>&#0160; &#0160; _markup.show();</div>
<div>&#0160; &#0160; _markup.loadMarkups(_markupsPersist, &quot;layer1&quot;);</div>
<div>&#0160; &#0160; _markup.renderToCanvas(ctx, function () {</div>
<div>&#0160; &#0160; &#0160; &#0160; const a = document.createElement(&#39;a&#39;);</div>
<div>&#0160; &#0160; &#0160; &#0160; document.body.appendChild(a);</div>
<div>&#0160; &#0160; &#0160; &#0160; a.href = canvas.toDataURL();</div>
<div>&#0160; &#0160; &#0160; &#0160; console.log(a.href);</div>
<div>&#0160; &#0160; &#0160; &#0160; a.download = &#39;captured screen.png&#39;;</div>
<div>&#0160; &#0160; &#0160; &#0160; a.click();</div>
<div>&#0160; &#0160; &#0160; &#0160; document.body.removeChild(a);</div>
<div>&#0160; &#0160; &#0160; &#0160; _markup.hide();</div>
<div>&#0160; &#0160; }, true);</div>
<br />
<div>};</div>
<br />
<div>_viewer.getScreenShot(_viewer.container.clientWidth, _viewer.container.clientHeight, function (blobURL) {</div>
<div>&#0160; &#0160; screenshot.src = blobURL;</div>
<div>});</div>
</div>
</blockquote>
</div>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75195414b200c-pi" style="display: inline;"><img alt="Captured screen3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75195414b200c image-full img-responsive" src="/assets/image_866398.jpg" title="Captured screen3" /></a></p>
<p>Viewer なので、標準の「一人称視点」ツールで周囲を見回したり、歩き回ってしたりして、ビューを調整してからキャプチャすることも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b685274621200d-pi" style="display: inline;"><img alt="Look_around" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b685274621200d image-full img-responsive" src="/assets/image_310356.jpg" title="Look_around" /></a></p>
<p>この他にも、個々のオブジェクトの非表示状態（「ゴースト非表示オブジェクト」）に加えて、「断面解析」ツールや「分解モデル」ツールの操作内容もキャプチャされるので、特定の視点を記録する機能として便利です。ただし、「計測」ツールでツールチップ上に表示される計測値はキャプチャ出来ません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b68525c23e200c-pi" style="display: inline;"><img alt="Transparency_explode" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b68525c23e200c image-full img-responsive" src="/assets/image_888202.jpg" title="Transparency_explode" /></a></p>
<p>By Toshiaki Iezaki</p>
