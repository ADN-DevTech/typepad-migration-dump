---
layout: "post"
title: "BIM 360 Viewer と Forge Viewer の違い"
date: "2020-09-23 00:06:30"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/09/bim-360-viewer-vs-forge-viewer.html "
typepad_basename: "bim-360-viewer-vs-forge-viewer"
typepad_status: "Publish"
---

<p>オートデスク製のクラウド サービスである A360、Fusion Team、BIM 360 Docs や、Forge を利用した 3rd party 製アプリなど、数多くのアプリが Forge Viewer を使用しています。</p>
<p>BIM 360 Viewer は Forge Viewer をベースにいくつかの BIM 関連機能を提供していますが、表示機能を含め、ほとんどの機能は Forge Viewer の機能と同等です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4130b7c200d-pi" style="display: inline;"><img alt="Viewer_apperances" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be4130b7c200d image-full img-responsive" src="/assets/image_806432.jpg" title="Viewer_apperances" /></a></p>
<p>BIM 360 Viewer&#0160; が使用しているバージョン名は、基になる Forge Viewer のバージョンと別に付けられているため、混乱を招くかもしれません。</p>
<p>同時期の取得した次のスクリーン キャプチャでは、BIM 360 Viewer&#0160; v4.21.0、 Forge Viewer v7.27.0 になっていることがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e966ea25200b-pi" style="display: inline;"><img alt="Version_differences" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e966ea25200b image-full img-responsive" src="/assets/image_680588.jpg" title="Version_differences" /></a></p>
<p>特定のモデルでは、BIM 360 Viewer と Forge Viewer を使用する他のアプリでのモデルの表示に視覚的な違いが出る可能性があります。</p>
<p>これらの違いは、Viewer の設定の違い（<a href="https://forge.autodesk.com/en/docs/viewer/v7/developers_guide/advanced_options/profiles/" rel="noopener" target="_blank">Profile</a> による）の他に、ロードされる <a href="https://forge.autodesk.com/en/docs/viewer/v7/reference/Extensions/" rel="noopener" target="_blank">Extension</a> によって生じる場合もあります。例えば、BIM 360 Viewer 上でレベルの表示切り替えを実現している Autodesk.AEC.LevelsExtension では、一部のマテリアルの透明度が無効化されてしまう場合があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be4130d0b200d-pi" style="display: inline;"><img alt="Transparency" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be4130d0b200d image-full img-responsive" src="/assets/image_318134.jpg" title="Transparency" /></a></p>
<p>これは、BIM 360 ViewerとForge Viewerで視覚化がどのように異なるかを示す1つの例にすぎません。オートデスク製のほとんどの Extension は、3rd party 製の Forge アプリでも使用することが出来ますが、BIM 360 Viewer 用に用意された一部の Extension では、前述した差異が生じる可能性がある点をご認識いただければと思います。</p>
<p>By Toshiaki Isezaki</p>
