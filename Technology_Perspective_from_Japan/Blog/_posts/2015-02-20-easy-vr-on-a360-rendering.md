---
layout: "post"
title: "Rendering in Autodesk A360 で簡単 VR"
date: "2015-02-20 00:04:05"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/02/easy-vr-on-a360-rendering.html "
typepad_basename: "easy-vr-on-a360-rendering"
typepad_status: "Publish"
---

<p>クラウドを使ったレンダリング サービスである <a href="https://rendering.360.autodesk.com" target="_blank"><strong>Rendering in Autodesk A360</strong></a> には、フォトリアリスティックな静止画レンダリングや照度レンダリングの他に、インタラクティブ パノラマ、日照シミュレーション、ターンテーブル といった動きのあるレンダリング結果を得る機能がありました。これらの機能に加えて、今回、新しく&#0160;<strong>ステレオ パノラマ</strong>&#0160;レンダリングの機能が加わりました。</p>
<p style="text-align: center;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7508891970b-pi"><img alt="Rendering_features" border="0" src="/assets/image_639677.jpg" title="Rendering_features" /></a></p>
<p>ステレオ パノラマが他のレンダリング機能を異なるのは、ブラウザ上での操作で評価したり、ファイルをダウンロードして再利用したりするのではなく、スマートフォンを使ったバーチャル リアリティ体験（VR）が出来る点です。生成された画像をブラウザ上で見ても、意味がありません。もちろん、以前、ご<a href="http://adndevblog.typepad.com/technology_perspective/2014/12/simple-vr-google-cardboard.html" target="_blank"><strong>紹介した</strong></a> Google Cardborad のようなスマートフォンを両眼に装着できるホルダーも必要になります。最近では、日本の企業も同様の Cardboard キットを販売し始めているようです。Web で&#0160;<strong><a href="http://lmgtfy.com/?q=google+cardboard+キット#" target="_blank">検索</a>&#0160;</strong>していただければ、比較的容易に販売サイトを見つけることが出来るはずです。</p>
<p>ステレオ パノラマ &#0160;レンダリング（Stereo Panorama）も、従来と同様、最初に静止画レンダリングを作成して、サムネイル画像右下に ▼ から [名前を付けてレンダリング] メニューから指定することが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07f43e1a970d-pi" style="display: inline;"><img alt="Stereo_panorama" class="asset  asset-image at-xid-6a0167607c2431970b01bb07f43e1a970d img-responsive" src="/assets/image_392615.jpg" style="width: 400px;" title="Stereo_panorama" /></a></p>
<p>ステレオ パノラマを利用する際には、生成されたステレオ パノラマのプレビュー画像左下にある「Preview on your phone」チェックボックスにチェックを入れて、スマートフォンぼ Web ブラウザから直接参照できる URL を取得してください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d9e93f970c-pi" style="display: inline;"><img alt="Retrieve_url" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d9e93f970c image-full img-responsive" src="/assets/image_169753.jpg" title="Retrieve_url" /></a></p>
<p style="text-align: left;">あとは取得した URL をスマートフォンの Web ブラウザで表示するだけです。スマートフォンが持っているセンサーに反応するので、上下左右に向きを振ることで、レンダリングされた画像の中をバーチャルに見回すことが出来ます。まずは、お手持ちのスマートフォンで&#0160;<a href="http://cardboard.autodesk.com/panogl.html?url=panos/73cc1b03-8985-412a-bba2-83ceb798ef78" target="_blank">http://cardboard.autodesk.com/panogl.html?url=panos/73cc1b03-8985-412a-bba2-83ceb798ef78</a> にアクセスして、このモデルを見回してみてください。</p>
<p style="text-align: left;">もちろん、みなさんが AutoCAD や Revit、Navisworks から作成したレンダリング画像で VR 体験が出来ます。ぜひお試しください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d9f519970c-pi" style="display: inline;"><img alt="Iphone6_onto_cardboard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d9f519970c image-full img-responsive" src="/assets/image_471755.jpg" title="Iphone6_onto_cardboard" /></a></p>
<p style="text-align: left;">Rendering in Autodesk A360 のステレオ パノラマでは、あくまでレンダリングした視点の周囲を見回すことが出来るだけです。View and Data API で任意の視点を呼び出せるようカスタマイズすれば、より効果的な VR 体験をすることも可能かと思います。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/gIjh9tzR7Ac?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="text-align: left;">ステレオ パノラマ &#0160;レンダリング（Stereo Panorama）は、3月31日まで、クラウド クレジットを消費せずに無償で実行していただけますので、ぜひ、このタイミングでにお試しください。</p>
<p style="text-align: left;">By Toshiaki Isezaki</p>
<p style="text-align: left;">&#0160;</p>
