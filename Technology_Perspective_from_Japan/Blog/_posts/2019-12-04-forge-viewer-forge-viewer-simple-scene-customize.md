---
layout: "post"
title: "Forge Viewer：簡単なシーン カスタマイズ"
date: "2019-12-04 00:01:54"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-forge-viewer-simple-scene-customize.html "
typepad_basename: "forge-viewer-forge-viewer-simple-scene-customize"
typepad_status: "Publish"
---

<p>Forge Viewer のカンバス領域に表示されるシーンも比較的簡単にカスタマイズして利用することが出来ます。今回は、過去にご紹介した内容も含め、一端をご紹介しておきます。</p>
<p>まずは、シーン内に 3D モデルを表示した際にツールバーの [設定] ボタンから変更することが可能な「環境」についてです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4cd935d200d-pi" style="display: inline;"><img alt="Environment_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4cd935d200d img-responsive" src="/assets/image_758682.jpg" title="Environment_settings" /></a></p>
<p>Forge Viewer v7 には 事前設定された 20 種の環境、あるいは、環境光が用意されていて、変換前の CAD データ ファイル（シード ファイル）の光源設定に関係なく、ビューア上で自由に環境を変更することが出来ます。</p>
<p>環境には、環境毎に証明が設定されていて、環境を変更するだけで 3D モデルへの照明を変化させることが出来るため、建築や製造などの業種用途にあわせて、適切な表現をおこなうことが出来ます。また、環境には、あらかじめ設定されているシーン背景の画像「環境イメージ」を表示させることも出来るので、用途に合わせて表示/非表示を切り替えることも可能です。</p>
<p>Forge Viewer JavaScript ライブラリでは、環境の切り替えを <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#setlightpreset-index" rel="noopener" target="_blank">Viewer3D.setLightPreset</a> メソッドで、環境イメージの表示切替を <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#setenvmapbackground-value" rel="noopener" target="_blank">Viewer3D.setEnvMapBackground</a> メソッドで、それぞれ指定することが出来ます。Viewer3D.setLightPreset メソッドで指定するパラメータは、設定パレットに表示される環境名に並び順のインデックス値です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f2ba7c200b-pi" style="display: inline;"><img alt="Set_environment" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f2ba7c200b image-full img-responsive" src="/assets/image_767334.jpg" title="Set_environment" /></a></p>
<p>3D モデルを表示した際、マウスカーソルをオブジェクト上にホバーさせると、選択可能なオブジェクトを知らせる目的で、当該オブジェクトがハイライトします。場合によっては、このハイライトが 3D モデルの閲覧時に邪魔になることがあります。そのような場面では、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#disablehighlight" rel="noopener" target="_blank">Viewer3D.disableHighlight</a> メソッドでハイライトの挙動を無効化することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f24482200b-pi" style="display: inline;"><img alt="Hover" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f24482200b image-full img-responsive" src="/assets/image_60183.jpg" title="Hover" /></a></p>
<p>マウス左ボタン クリックでのオブジェクト選択も同様です。クリック時のオブジェクト選択は、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#disableselection-disable" rel="noopener" target="_blank">Viewer3D.disableSelection</a> メソッドで無効化することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a46f30200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a46f3c200c-pi" style="display: inline;"><img alt="Selection" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a46f3c200c image-full img-responsive" src="/assets/image_156410.jpg" title="Selection" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a46f30200c-pi" style="display: inline;"><br /></a></p>
<p>なお、オブジェクト選択時のハイライト色は、<a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Viewing/Viewer3D/#setselectioncolor-color-selectiontype" rel="noopener" target="_blank">Viewer3D.setSelectionColor</a> メソッドで変更出来ます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer.setSelectionColor(new THREE.Color(0xffff00));
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4cd9b60200d-pi" style="display: inline;"><img alt="Yellow_highlight" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4cd9b60200d image-full img-responsive" src="/assets/image_671732.jpg" title="Yellow_highlight" /></a></p>
<p>ビューアで表示中にのシーンは、State API と呼ばれている API を利用することで、ビューとして記録したり、復元することが出来ます。ここで記録、復元出来るのは、カメラ視点だけではなく、環境やオブジェクト選択の状態も含まれます。State API については、過去のブログ記事 <strong><a href="https://adndevblog.typepad.com/technology_perspective/2017/12/state-api-in-forge-viewer.html" rel="noopener" target="_blank">Forge Viewer の State API とビュー</a></strong> でご紹介しています。</p>
<p>Forge Viewer インスタンスを作成すると、標準のツールバーや ViewCube が表示されます。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer = new Autodesk.Viewing.GuiViewer3D(document.getElementById(&#39;viewer3d&#39;));
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f248ef200b-pi" style="display: inline;"><img alt="Standard_instance" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f248ef200b image-full img-responsive" src="/assets/image_62959.jpg" title="Standard_instance" /></a></p>
<p>&#0160;</p>
<p><a name="_headless"></a>これらを一切表示させないことも出来ます、Viewer3D インスタンス作成時のメソッド呼び出しを少し変更するだけです。この方法で作成されるビューアは、ユーザ インタフェースの表示に消費されるメモリ消費を抑止するだけでなく、3D モデル表示までの時間も短縮することが出来るので、オーバーヘッドのない、あるいは、少ないビューア、という意味で、<strong>ヘッドレス ビューア</strong>と呼ばれています。</p>
<pre class="code-snippet more-toggle--slide snippet-container js-more-toggle more-toggle" data-toggle-end-color="rgba(250, 250, 250, 1)" data-toggle-height="400" data-toggle-less="Hide Code" data-toggle-more="Show Code"><code class="language-javascript code-overflow-x hljs " id="snippet-4">_viewer = new Autodesk.Viewing.Viewer3D(document.getElementById(&#39;viewer3d&#39;), {});
</code><code class="language-javascript code-overflow-x hljs "></code></pre>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4a47380200c-pi" style="display: inline;"><img alt="Heaadless_instance" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4a47380200c image-full img-responsive" src="/assets/image_723203.jpg" title="Heaadless_instance" /></a></p>
<p>環境、ハイライト、オブジェクト選択、State API とも、ヘッドレスビューアでも利用可能です。標準ツールバーなどのユーザ操作を抑止して、独自のインタフェース経由での操作をさせたい場合には有効と思います。</p>
<p>By Toshiaki Isezaki</p>
