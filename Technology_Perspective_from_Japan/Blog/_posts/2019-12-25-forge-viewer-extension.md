---
layout: "post"
title: "Forge Viewer：Viewer を拡張する Extension"
date: "2019-12-25 00:05:45"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-extension.html "
typepad_basename: "forge-viewer-extension"
typepad_status: "Publish"
---

<p>このブログでも過去に何回か<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/09/creating-forge-viewer-extension.html" rel="noopener" target="_blank">ご紹介</a></strong>してきましたが、Forge Viewer には、Viewer 自身の機能を拡張する JavaScript コードを .js ファイル単位でロードしたり、ロード解除したりする Extension という機能があります。Extension は、独自に新規作成することが可能なほか、オートデスク自身が作成、提供しているものもあります。</p>
<p>最近の Forge Viewer ライブラリは、バージョン アップで積み重ねてきた各種機能の実装で、ライブラリ自身が肥大化してしまい、ライブラリのロード時間によって、ビューア表示までにタイムラグを感じてしまうケースが散見されはじめていました。</p>
<p>そこで、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/07/release-forge-viewer-v7-0.html" rel="noopener" target="_blank">Forge Viewer v7</a> </strong>になって、Forge Viewer ライブラリ本体から様々な機能を Extension として分離する作業がはじまっています。以前、ご案内したくつかの新機能も Extension として分離され、利用に Extension のロードが必要になっているものが出てきています。</p>
<p>例えば、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/11/release-forge-viewer-v6_3.html" rel="noopener" target="_blank">Forge Viewer バージョン 3.3 リリース</a></strong> でご案内した <strong>非フォトグラフィックス レンダリング スタイル </strong>機能は、v7 になって <strong>Autodesk.NPR</strong> Extension として分離されています。現在、非フォトグラフィックス レンダリング機能を利用する場合には、Autodesk.NPR Extension を明示的にロードして、‘<strong>edging</strong>’、‘<strong>cel</strong>’、‘<strong>grapite</strong>’、‘<strong>pencil</strong>’ の中から利用するスタイルを指定する必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f9d173200b-pi" style="display: inline;"><img alt="Npr_extension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f9d173200b image-full img-responsive" src="/assets/image_344818.jpg" title="Npr_extension" /></a></p>
<p>元々、Extension として提供されているものも、今まで通り利用出来るものがあります。例えば、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/11/release-forge-viewer-v6_3.html" rel="noopener" target="_blank">Forge Viewer バージョン 6.3 リリース</a></strong> でご紹介した <strong>Autodesk.DocumentBrrowser</strong> Extension が有用です。通常、Forge Viewer は、HTML 内で &lt;div&gt;&lt;/div&gt; タグとして指定されたカンバスに、2D あるいは 3D のコンテンツ表示用の JavaScript コードを使って表示します。2D と 3D では、viewables の role が異なるので、コードの一部が、2D 固有、あるいは、3D 固有になりがちです。</p>
<p>Autodesk.DocumentBrrowser Extension をロードすると、表示したいコンテンツの role（2D か 3D）に関係なく、Autodesk.DocumentBrrowser Extension が実装しているパネルから、直接、指定した 2D シート/レイアウト や 3D モデルを表示することが出来るろうになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4ac0507200c-pi" style="display: inline;"><img alt="Browser_extension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4ac0507200c image-full img-responsive" src="/assets/image_20045.jpg" title="Browser_extension" /></a></p>
<p>特定のシード ファイル（Model Derivative API で変換前の CAD ファイル）で機能する Extension も存在します。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/09/fusion-360-animation-play-on-forge-viewer.html" rel="noopener" target="_blank">Forge Viewer での Fusion 360 アニメーション再生</a></strong> でご紹介した <strong>Autodesk.Fusion360.Animation</strong> Extension では、Fusion 360 の [Animation] でワークスペースで定義したアニメーションを Forge Viewer 上で再生する Extension です。もちろん、Forge Viewer v7 でも機能します。Model Derivative API の変換時にオプションを指定する必要もありません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f9d394200b-pi" style="display: inline;"><img alt="Animation_extension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f9d394200b image-full img-responsive" src="/assets/image_608057.jpg" title="Animation_extension" /></a></p>
<p><strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/11/forge-viewerpdf-pdf-drawing.html" rel="noopener" target="_blank">Forge Viewer：PDF 図面</a> </strong>のブログ記事でご紹介した<strong> Autodesk.PDF</strong> Extenson では、PDF ファイル コンテンツをベクトル データとして表示することを可能にする Extension です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4fab633200b-pi" style="display: inline;"><img alt="Pdf" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4fab633200b image-full img-responsive" src="/assets/image_593806.jpg" title="Pdf" /></a></p>
<p>オートデスクが提供する Extension は、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/">https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/</a></strong> に記載されています。Autodesk.DefaultTools.NavTools や Autodesk.Explode、Autodesk.ViewCubeUi Extension など、使用する Forge Viewer インスタンスが<strong><a href="https://adndevblog.typepad.com/technology_perspective/2019/12/forge-viewer-forge-viewer-simple-scene-customize.html#_headless" rel="noopener" target="_blank">ヘッドレス ビューア</a></strong>でない場合、標準でロードされる Extension もあります。</p>
<p><img alt="Autodesk_extensions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f9d142200b image-full img-responsive" src="/assets/image_290093.jpg" title="Autodesk_extensions" /></p>
<p>なお、<strong><a href="https://adndevblog.typepad.com/technology_perspective/2018/05/display-specified-urn-on-lmv-ninja.html" rel="noopener" target="_blank">Forge Viewer のテストとデバッグ</a></strong> で触れた <strong><a href="http://lmv.ninja.autodesk.com/" rel="noopener" target="_blank">LMV Ninja サイト</a></strong>では、これら Extension を部分的にテストすることも出来ます。</p>
<p>もし、既知でないようであれば、ぜひ、ご確認ください。</p>
<p>By Toshiaki Isezaki</p>
