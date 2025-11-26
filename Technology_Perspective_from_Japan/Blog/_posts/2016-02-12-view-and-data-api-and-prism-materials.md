---
layout: "post"
title: "View and Data API と Prism マテリアル"
date: "2016-02-12 00:19:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/02/view-and-data-api-and-prism-materials.html "
typepad_basename: "view-and-data-api-and-prism-materials"
typepad_status: "Publish"
---

<p>View and Data API のデモなどで利用されている<strong>&#0160;<a href="http://lmv.rocks/" rel="noopener" target="_blank">http://lmv.rocks/</a>&#0160;</strong>というサイトがあります。このサイトには、あらかじめ、いくつかのモデルが登録されていて、ページ上部に表示させたり、個別ウィンドウで表示させることが出来ます。</p>
<p>例えば、次のモデルは Race Car と書かれた、おそらくラジコン モデルを独立ウィンドウで表示したものです。View and Data API を使ってカスタマイズされた状態なので、A360 や A360 Online Viewer でモデルを表示するのは異なるユーザ インタフェースが利用されています。</p>
<p><img alt="Prism1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb08b6ac4f970d image-full img-responsive" src="/assets/image_713133.jpg" title="Prism1" /></p>
<p>また、Residential Exterior と書かれたモデルを表示させると、次のように表示することが出来るはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19c059d970c-pi" style="display: inline;"><img alt="Prism2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d19c059d970c image-full img-responsive" src="/assets/image_769014.jpg" title="Prism2" /></a></p>
<p>これらのモデルは、あくまで &#0160;View and Data API のテスト用に作られているもので、利用されているマテリアルを Prism マテリアルと呼んでいます。Prism マテリアルを適用したモデルは、ご覧のとおり、メタル調の質感や、部屋の中のフォトメトリックな明かりなど、高品質な表現が出来ていることがお分かりいただけるはずです。もちろん、環境光を自由に返られるので、その設定によって、見え方も異なります。</p>
<p>View and Data API は A360 などの Online Viewer と同等の機能を有しています。ただ、お気づきの方も多いと思いますが、アップロードしたすべてのモデルが、Prism マテリアルを利用して表現されるわけではありません。</p>
<p>例えば、AutoCAD から出力した DWG ファイルや DWF ファイルと Fusion 360 で出力した F3D アーカイブ ファイルをアップロードした場合、それぞれ、次のような差が生じます。いずれの場合も、Autodesk Material が適用されているものです。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d19c05a8970c-pi" style="display: inline;"><img alt="Different_materal_per_translator" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d19c05a8970c image-full img-responsive" src="/assets/image_425662.jpg" title="Different_materal_per_translator" /></a></p>
<p>A360 Online Viewer も View and Data API も、デザイン ファイルをクラウドにアップロードした後、一律にストリーミング配信するための変換処理をおこないます。この処理を実装してクラウド上で実行されているのが、トランスレータ と呼ばれる変換プログラムです。</p>
<p>トランスレータは、ファイル拡張子毎に独立したものが用意されていて、業種や製品別に、オートデスク社内の別々の開発チームが開発しています。DWG と DWF は AutoCAD 開発チーム、RVT と IFC はRevit 開発チーム、F3D は Fusion 360 開発チーム、といった具合に、約 20 の開発チームが携わっています。</p>
<p>問題は、各開発チームのコンセプトや優先順位によって、変換内容が変わっている点です。AutoCAD の場合には、2D 図面の表示が優先されていて、3D モデルそのものは変換されるものの、マテリアルが無視されています。逆に、Fusion 360 モデルは、前述の Prism マテリアルを適用して変換されます。</p>
<p>今後、どのトランスレータでも同じような結果になるよう変換精度を上げていくはずですが、プレゼンテーション目的で A360 Online Viewer や &#0160;A360、View and Data API を利用される場合には、しばらくの間、ちょっとした工夫が必要かも知れません。AutoCAD で作成した 3D モデルにマテリアルを適用したまま表示させたい場合には、一旦、Navisworks に DWG ファイルをインポートしてから、NWD ファイルにしてアップロードすると、マテリアルを正しく表示できるはずです。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
