---
layout: "post"
title: "3ds Max パノラマ画像の利用"
date: "2022-08-03 00:06:04"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/08/use-of-panorama-image.html "
typepad_basename: "use-of-panorama-image"
typepad_status: "Publish"
---

<p><a href="https://aps.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/#section-14" rel="noopener" target="_blank">https://forge-da4m-panorama.herokuapp.com</a><a href="https://aps.autodesk.com/en/docs/viewer/v7/change_history/changelog_v7/#section-14" rel="noopener" target="_blank">v7 Changelog</a> で触れられていますが、Forge Viewer v7.70 では、Internet Explorer 11 の<a href="https://adndevblog.typepad.com/technology_perspective/2021/11/internet-explorer-11-support-end-and-forge.html" rel="noopener" target="_blank">サポート終了</a>とともに、2 つの Extension が廃止されています。WebSokcet を用いた Live View を実装する <a href="https://adndevblog.typepad.com/technology_perspective/2016/05/live-review-extension-on-view-and-data-api.html" rel="noopener" target="_blank">Autodesk.Viewing.Collaboration</a> extension と、モバイル デバイスで VR（WebVR） を実装する&#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2017/02/about-vr-support-on-forge-viewer.html" rel="noopener" target="_blank">Autodesk.Viewing.WebVR</a> extension です。</p>
<p><span class="d2edcug0 hpfvmrgz qv66sw1b c1et5uql lr9zc1uh a8c37x1j fe6kdd0r mau55g9w c8b282yb keod5gw0 nxhoafnm aigsh9s9 d3f4x2em iv3no6db jq4qci2q a3bd9o3v b1v8xokw oo9gr5id hzawbc8m" dir="auto">Forge Viewer ベースではありませんが、WebVR extension で実現していた内容は、レンダリング画像とオープンソースで比較的簡単に実装することが出来ます。</span>残念ながら Extension は廃止されてしまったのですが、ここではカードボード用の VR の実装についてご紹介しておきたいと思います。</p>
<p>まず、素材となる 3D モデルを用意します。例えば、無償ダウンロード可能な AutoCAD の<a href="https://knowledge.autodesk.com/ja/support/autocad/downloads/caas/downloads/downloads/JPN/content/autocad-sample-files.html" rel="noopener" target="_blank">サンプル図面</a>には、<a href="https://download.autodesk.com/us/samplefiles/acad/visualization_-_condominium_with_skylight.dwg" rel="noopener" target="_blank">ビジュアライゼーション - 天窓付きコンドミニアム</a> というファイルがあります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eecde025200d-pi" style="display: inline;"><img alt="Autocad" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eecde025200d image-full img-responsive" src="/assets/image_595763.jpg" title="Autocad" /></a></p>
<p>このサンプル図面ファイル（visualization_-_condominium_with_skylight.dwg）を 3ds Max に読み込ませたら、必要に応じてマテリアルやカメラの設定を変更、<a href="https://knowledge.autodesk.com/ja/support/3ds-max/learn-explore/caas/CloudHelp/cloudhelp/2016/JPN/3DSMax/files/GUID-432745B8-66E9-416C-8BCA-EF762CD3495F-htm.html" rel="noopener" target="_blank">パノラマを作成するには | 3ds Max | Autodesk Knowledge Network</a> の手順でパノラマ画像を書き出します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a30d44b235200b-pi" style="display: inline;"><img alt="3ds_max" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d44b235200b image-full img-responsive" src="/assets/image_60694.jpg" title="3ds_max" /></a></p>
<p>パノラマ画像の生成には相応の時間が必要になりますが、書き出した球状のレンダリング画像は、おおむね次のようなものになるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eecd532b200d-pi" style="display: inline;"><img alt="Autocad" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eecd532b200d image-full img-responsive" src="/assets/image_228996.jpg" title="Autocad" /></a></p>
<p>このようなパノラマ画像は、オープンソースの Panolens.js を使って簡単に Web ページ化出来ます。</p>
<p>HTML と JavaScript コードを下記にように定義したとします。</p>
<p><strong>HTML：</strong></p>
<div>
<blockquote>
<div>&lt;html&gt;</div>
<div>&lt;head&gt;</div>
<div>&#0160; &#0160; &lt;meta charset=&quot;utf-8&quot;&gt;</div>
<div>&#0160; &#0160; &lt;title&gt;Panolens.js Test&lt;/title&gt;</div>
<div>&#0160; &#0160; &lt;script src=&quot;https://cdnjs.cloudflare.com/ajax/libs/three.js/105/three.js&quot;&gt;&lt;/script&gt;</div>
<div>&#0160; &#0160; &lt;script src=&quot;https://cdn.jsdelivr.net/npm/panolens@0.11.0/build/panolens.min.js&quot;&gt;&lt;/script&gt;</div>
<div>&#0160; &#0160; &lt;script src=&quot;/index.js&quot;&gt;&lt;/script&gt;</div>
<div>&lt;/head&gt;</div>
<div>&lt;body onload=&quot;onView()&quot;&gt;</div>
<div>&#0160; &#0160; &lt;div id=&quot;panolens&quot; style=&quot;width: 100%; height: 100%&quot;&gt;&lt;/div&gt;</div>
<div>&lt;/body&gt;</div>
<div>&lt;/html&gt;</div>
</blockquote>
</div>
<p><strong>JavaScript：</strong></p>
<div>
<blockquote>
<div>function onView() {</div>
<div>&#0160; &#0160; var canvas = document.querySelector(&quot;#panolens&quot;);</div>
<div>&#0160; &#0160; var panorama = new PANOLENS.ImagePanorama(&quot;images/autocad.jpg&quot;);</div>
<div>&#0160; &#0160; if (navigator.userAgent.match(/iPhone|Android.+Mobile/)) {</div>
<div>&#0160; &#0160; &#0160; &#0160; alert(&quot;Tap screen to get permission for gyro-censor on mobile device&quot;);</div>
<div>&#0160; &#0160; &#0160; &#0160; panorama.addEventListener(&quot;click&quot;, function () {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; if (DeviceOrientationEvent &amp;&amp; DeviceOrientationEvent.requestPermission) {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; DeviceOrientationEvent.requestPermission();</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; });</div>
<div>&#0160; &#0160; }</div>
<div>&#0160; &#0160; var viewer = new PANOLENS.Viewer({</div>
<div>&#0160; &#0160; &#0160; &#0160; container: canvas,</div>
<div>&#0160; &#0160; &#0160; &#0160; cameraFov: 100,</div>
<div>&#0160; &#0160; &#0160; &#0160; autoRotate: true,</div>
<div>&#0160; &#0160; &#0160; &#0160; autoRotateSpeed: 0.1,</div>
<div>&#0160; &#0160; &#0160; &#0160; autoRotateActivationDuration: 10</div>
<div>&#0160; &#0160; });</div>
<div>&#0160; &#0160; viewer.add(panorama);</div>
<div>}</div>
</blockquote>
この静的ページを Web サーバーにホストすると、次のようなパノラマを得ることが出来ます。</div>
<p><iframe frameborder="0" height="480px" scrolling="no" src="https://aps-3dsmax-panorama.herokuapp.com//" width="100%"></iframe></p>
<p>PC や Mac などの Web ブラウザでは、マウスの動きで 360 度俯瞰状況を変更することになります。また、モバイルデバイスの Web ブラウザでページを表示させれば、ジャイロセンサーを活用することも可能です。（iOS では Web ページのセンサーへのアクセス許可が必要です。）</p>
<p>Panolens.js では、カンバス右下の画車アイコンからコントロール方法や表示形態の変更もサポートしています。ジャイロセンサーとカードボードを選択すれば、WebVR extension のような効果を得られます。</p>
<p style="text-align: center;"><span style="font-size: 8pt;">&lt;次の画像をクリック/タップしてみてください&gt;</span><br /><a class="asset-img-link" href="https://aps-3dsmax-panorama.herokuapp.com/" rel="noopener" style="display: inline;" target="_blank"><img alt="IMG_2006" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a30d44b1c0200b image-full img-responsive" src="/assets/image_544465.jpg" title="IMG_2006" /></a></p>
<p>なお、Autodesk.Viewing.Collaboration extension の Live Review が実現していた WebSocket を使った Forge Viewer の同期については、<a href="https://forge.autodesk.com/blog/share-viewer-state-websockets" rel="noopener" target="_blank">Share Viewer state with websockets</a>&#0160;のブログ記事で触れられています。</p>
<p>By Toshiaki Isezaki</p>
