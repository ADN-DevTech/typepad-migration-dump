---
layout: "post"
title: "Forge Viewer：オーバーレイとシーン ビルダー"
date: "2020-12-21 00:02:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/12/forge-viewer-overlay-and-scene-builder.html "
typepad_basename: "forge-viewer-overlay-and-scene-builder"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e981c2dc200b-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e981c2dc200b-pi" style="display: inline;"></a>Forge Viewer は Web ブラウザ内に 2D 図面や 3D モデルを表示するための JavaScript ライブラリです。表示前に、CAD や CG ソフトウェアで作成したデザイン ファイル（シード ファイル）を、Model Derivative API で変換することで、ストリーミングで使ったカンバス領域での表示が可能になります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeaf135f200c-pi" style="display: inline;"><img alt="Viewer_solution" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeaf135f200c image-full img-responsive" src="/assets/image_630257.jpg" title="Viewer_solution" /></a></p>
<p>Forge Viewer JavaScript ライブラリは、WebGL を土台としたオープンソースの <strong><a href="https://threejs.org/" rel="noopener" target="_blank">Three.js</a></strong> をベースにしています。両者の最も大きな違いは、デザイン ファイル内の形状を変換してをカンバス上に描画するか、プログラミングでシーン、ライト、ポリゴンを定義して形状をカンバス上に描画するか、です。</p>
<p>Forge Viewer では、2D/3D 形状に加えてメタデータと呼ばれる属性、あるいはプロパティ情報も同時にストリーミング配信されるため、ダッシュボードなど、Web ブラウザ上でのリッチ コンテンツ表現が可能になっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be42e1cd1200d-pi" style="display: inline;"><img alt="Library_hierarchy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be42e1cd1200d image-full img-responsive" src="/assets/image_94468.jpg" title="Library_hierarchy" /></a></p>
<p>ただ、カンバス上にデザイン ファイルにない形状を表示させたい、という要望は少なからず存在します。Forge Viewer の特性上、Three.js とは一定程度の親和性を持っていますが、両者コンセプトの違いから、Three.js の作法がそのまま Forge Viewer カンバスに適用出来るわけではありません。</p>
<p>そのため、若干ではありますが、Three.js オブジェクトを表現させるためのインタフェースが用意されています。１つめは Forge Viewer カンバスにシーンをオーバーレイ（透過的に重ねて表示）させる方法、２つめはシーン ビルダーを使ってカスタム モデルを描画させる方法です。</p>
<p>具体的な実現手順は、Forge ポータルの Viewer ドキュメント上、<strong><a href="https://forge.autodesk.com/en/docs/viewer/v7/developers_guide/advanced_options/" rel="noopener" target="_blank">Advanced Options</a></strong> として記載されています。前者のオーバーレイが<a href="https://forge.autodesk.com/en/docs/viewer/v7/developers_guide/advanced_options/custom-geometry/" rel="noopener" target="_blank"><strong>Add Custom Geometry</strong></a>、後者のシーン ビルダーが <strong><a class="menu-leaf" href="https://forge.autodesk.com/en/docs/viewer/v7/developers_guide/advanced_options/scene-builder/" id="3a8e03ba-0652-968f-5585-5b0c40a4f8a0" rel="noopener" target="_blank">Scene Builder</a></strong>、でそれぞれ説明されています。</p>
<p>詳細はこれらドキュメントに譲りますが、ここでは両手法によって表現された形状の差について触れておきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be42e1e28200d-pi" style="display: inline;"><img alt="Docs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be42e1e28200d image-full img-responsive" src="/assets/image_512141.jpg" title="Docs" /></a></p>
<p>次に示すのは、シーン オーバーレイとシーン ビルダーの手法を利用してカンバスに描画させた球の例です。黒のモザイクタイルをテクスチャ マップした球がオーバーレイ、青のモザイクタイルをテクスチャ マップした球がシーン ビルダーを使っています。</p>
<p>いずれの球も、操作によって表示の拡大縮小、画面移動、オービットなど、デザイン モデルから変換された形状と同じように扱われていることがわかります。</p>
<p>一方、違いも存在します。シーン オーバーレイで描画された球と違い、シーンビルダーで描画された球は、コード上で割り当てた dbid によって、デザイン モデルから変換されたオブジェクトと同じように識別されています。この例では、球を選択操作出来たり、モデル ブラウザに Model ノードが作成されて、表示/非表示の操作対象になっていたりすることがわかります。（シーン オーバーレイで描画された球を選択しようとすると、背後のオブジェクトを選択してしまっています。）</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be42e1f67200d-pi" style="display: inline;"><img alt="Model_browser" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be42e1f67200d image-full img-responsive" src="/assets/image_298781.jpg" title="Model_browser" /></a></p>
<p>次の例では、Forge Viewer に用意された環境を変更後、描画した球に環境光は反映されるかを示しています。この場合、シーン オーバーレイで描画した球には、環境光が反映されていないことがわかります（陰影表現の有無）。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026be42e1f6f200d-pi" style="display: inline;"><img alt="Lighting" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026be42e1f6f200d image-full img-responsive" src="/assets/image_55390.jpg" title="Lighting" /></a></p>
<p>断面解析ツールをつかった際の表現も同様です。シーン オーバーレイで描画した球は断面解析の対象になっていませんが、シーンビルダーの球は正しく切断されてきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e981c191200b-pi" style="display: inline;"><img alt="Section" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e981c191200b image-full img-responsive" src="/assets/image_947670.jpg" title="Section" /></a></p>
<p>どの方法を選択すべきかは目的とする用途によって異なりますが、いずれの場合も、単純な要素表現を前提にしている点にご注意ください。アニメーションなどを伴った複雑な Three.js 要素の表現は制限される場合があります。</p>
<p>デザイン ファイルから変換されたオブジェクトにガラスなどの透過度を持ったマテリアルが適用されていると、オーバーレイとシーンビルダーで描画されたオブジェクトをガラス越しに表示することが出来ません。次の例では、非表示にしたガラス領域内にのみ、球が見えています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e981c2dc200b-pi" style="display: inline;"><img alt="Glass" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e981c2dc200b image-full img-responsive" src="/assets/image_967543.jpg" title="Glass" /></a></p>
<p>なお、シーンビルダーで描画されたオブジェクトのモデル ブラウザ上のノード名は、自動的に Model になってしまいます。また、Model ノード下に階層付けすることも出来ません。同様に、同オブジェクトを選択した際のプロパティ パネルは、常にブランク（表示内容なし）になります。</p>
<p>By Toshiaki Isezaki</p>
