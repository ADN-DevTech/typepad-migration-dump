---
layout: "post"
title: "Autodesk 360 View & Data サービスの操作機能"
date: "2014-09-12 22:15:43"
author: "Toshiaki Isezaki"
categories:
  - "その他カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/09/autodesk-360-view-data-service-api.html "
typepad_basename: "autodesk-360-view-data-service-api"
typepad_status: "Publish"
---

<p>今回は&nbsp;Autodesk 360 View &amp; Data サービスで表示される 3D ビューで可能な操作について、簡単にご紹介しておきたいと思います。</p>
<p>まず、繰り返しになりますが、Autodesk 360 View &amp; Data サービスは&nbsp;Autodesk 360 Tech Preview や Autodesk Fusion 360 で利用されているビューワー機能を API として切り出したものです。このため、表示には <a href="http://ja.wikipedia.org/wiki/WebGL" target="_blank"><strong>WebGL</strong></a> に対応した Web ブラウザが必要になります。逆に言えば、WebGL 対応の&nbsp;Web ブラウザがあれば、プラグインなどの追加インストールは必要ありません。</p>
<p>現在のところ、この要件を満たすのは、Windows 環境では Google Chrome バージョン 30 以上、Mozilla Firefox バージョン 24 以上、Microsoft Internet Explorer バージョン 11 以上、Mac OS 環境では&nbsp;Google Chrome バージョン 30 以上、Mozilla Firefox バージョン 24 以上と Apple Safari となります。</p>
<p>もし、WebGL が有効でないブラウザで、新しくなった Autodesk 360 のビューワ機能や Autodesk 360 View &amp; Data サービスで 3D モデルを表示しようとすると、次のようなエラーが表示されるはずです。&nbsp;</p>
<p style="text-align: center;"><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6d86620970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c6d86620970b img-responsive" style="width: 400px;" title="Missing_webgl" src="/assets/image_440097.jpg" alt="Missing_webgl" /></a></p>
<p>下記に埋め込んだ Autodesk 360 View &amp; Data サービスの 3D モデル表示を埋め込んでいますが、無事に表示された場合には、さまざまな操作を粉うことが出来るはずです。</p>
<p>最初にご紹介するのは製造系の 3D データで、Inventor で作成されたモデルをベースにしています。 大きな画面で試したい方は、<a href="http://lmv.rocks/viewer/?url=http://lmv.rocks/data/tractor4/0.svf" target="_blank">こちら</a> をクリックしてみてください。</p>
<p style="text-align: center;"><iframe allowfullscreen="yes" frameborder="0" height="400px" mozallowfullscreen="yes" scrolling="no" src="http://lmv.rocks/viewer/?url=http://lmv.rocks/data/tractor4/0.svf" webkitallowfullscreen="&quot;" width="470px"></iframe></p>
<p>そして、 次のデータは、Revit で作成された BIM モデルです。 こちらも、オリジナルの大きな画面で試したい方は、<a href="http://lmv.rocks/viewer/?url=http://lmv.rocks/data/house2/0.svf" target="_blank">こちら</a>&nbsp;をクリックしてみてください。</p>
<p style="text-align: center;"><iframe allowfullscreen="yes" frameborder="0" height="400px" mozallowfullscreen="yes" scrolling="no" src="http://lmv.rocks/viewer/?url=http://lmv.rocks/data/house2/0.svf" webkitallowfullscreen="&quot;" width="470px"></iframe></p>
<p>既定値では、オービット モードになっているはずなので、マウスの左ボタンを押しながら表示領域をドラッグすることで、3D モデルをいろいろな方向から表示することが出来るはずです。また、マウス ホイールを利用すると、ズームインとズームアウトの操作が可能なはずです。</p>
<p>デスクトップ製品では、Inventor と AutoCAD でマウスホイールによるズーム イン / アウトの方向が逆になっていますが、&nbsp;Autodesk 360 View &amp; Data サービス API 側では、この設定を変更することも出来ます。</p>
<p>画面下部には、ツールバーが半透明な状態で表示されているはずです。それぞれ、下記のような機能を持っています。</p>
<p style="text-align: center;"><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0623e64970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0623e64970c image-full img-responsive" title="Toolbar_explanation" src="/assets/image_716152.jpg" alt="Toolbar_explanation" border="0" /></a></p>
<p>特に、レンズ長はマウスの左ボタンド ラッグしながらリアルタイム変更できるので、透視投影（パースペクティブ表示）で、奥行感を持たせリアルな表示にすることが出来ます。</p>
<p>モデル階層ボタンをクリックすれば、Inventror や Revit のモデル階層を、そのままパレット上に表示します。パレット上のパーツやサブアセンブリをクリックすると、そのモデル以外が半透明に表示され、対象モデルを強調して画面で表現できます。</p>
<p>分解ボタンをクリックすると、画面中央上部にスライダーが表示されるはずです。このスライダーをマウス左ボタンを押しながらドラッグすれば、表示中の 3D モデルをリアルタイムに分解表示させることが可能です。組図の状態を把握するには、とても便利な機能と言えます。</p>
<p>Autodesk 360 View &amp; Data サービスでは、&nbsp;個々のオブジェクトを選択してプロパティを表示させたり、選択したオブジェクトだけを表示（Isolate）、または、選択したオブジェクトを非表示（Hide Selected）、選択したオブジェクトにフォーカス（Focus）機能も利用することが出来ます。これらの操作は、オブジェクトをマウスの左ボタンで選択してから、マウスの右ボタンをクリックすることで表示されるショートカット メニューで操作することが出来ます。すべてのモデルを表示させる場合には、Show All を利用します。</p>
<p style="text-align: center;"><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6d86e47970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c6d86e47970b img-responsive" title="Right_click" src="/assets/image_173098.jpg" alt="Right_click" border="0" /></a></p>
<p>このように、<a href="http://adndevblog.typepad.com/technology_perspective/2014/08/fusion-360-gallery-and-embed-viewer.html" target="_blank"><strong>Fusion 360 Gallery とビューワー埋め込み</strong></a> と異なり、細かいコントロールが可能な点をご理解いただけると思います。逆に、ここでご案内したツールバーなどは、API 側のコントロールで完全にオフ（非表示）にすることも出来ます。</p>
<p>By Toshiaki Isezaki</p>
<p>&nbsp;</p>
