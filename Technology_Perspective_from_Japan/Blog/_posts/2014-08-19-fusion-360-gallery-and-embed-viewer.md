---
layout: "post"
title: "Fusion 360 Gallery とビューワー埋め込み"
date: "2014-08-19 19:05:40"
author: "Toshiaki Isezaki"
categories:
  - "その他カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/08/fusion-360-gallery-and-embed-viewer.html "
typepad_basename: "fusion-360-gallery-and-embed-viewer"
typepad_status: "Publish"
---

<p>Fusion 360 にはマッシュアップによって、他の Autodesk 360 で利用されているサービスが組み込まれています。</p>
<p>Fusion 360 を起動してプロジェクト内の既存編集モデルをクリックすると、Fusion 360 エディタを表示する前に、プレビューや検索が可能なビューワーが表示されます。このビューワーは、マッシュアップによって Fusion 360 に組み込まれた Autodesk 360 Tech Preview のビューワーと同等のものです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73e02b537970d-pi" style="display: inline;"><img alt="Fusion_360_Viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73e02b537970d image-full img-responsive" src="/assets/image_315105.jpg" title="Fusion_360_Viewer" /></a></p>
<p>もう1つ、Fusion 360 にマッシュアップされているのが Autodesk 360 Rendering の機能です。これもプレビューとしての位置づけですが、Fusion 360 の編集バージョンに合わせて、決められた視点で自動的にレンダリング画像が生成されます。あくまでプレビューとしての機能なので、明示的に高品質設定でレンダリングしない限り、クラウド クレジットが消費されることはありません。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511f75ca9970c-pi" style="display: inline;"><img alt="Fusion_360_Rendering" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511f75ca9970c image-full img-responsive" src="/assets/image_980589.jpg" title="Fusion_360_Rendering" /></a></p>
<p>プレビューで編集対象のモデルやバージョンを確認できたら、実際に編集をおこなうために [Open Design] ボタンをクリックして Fusion 360 エディタを起動できます。Fusion 360 で編集中のモデルは、新しく導入された<a href="https://fusion360.autodesk.com/projects/popular" target="_blank"><strong> Fusion 360 Gallery</strong></a> に公開することが出来るようになっています。</p>
<p>Fusion 360 Gallery へは、プレビュー レンダリングされた画像やエディタ上のスクリーンショットに加え、3D モデル自体（.f3d ファイル）も選択的に公開することが出来ます。このとき、Gallery 上の表示単位となるプロジェクトを作成するのが一般的です。公開したい画像やモデルにチェックと入れて <span style="color: #fdeee0;"><strong><span style="background-color: #bfbf00;">&#0160;Publish </span></strong></span>&#0160;ボタンをクリックしてください。</p>
<p style="text-align: center;">&#0160;&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6ccf255970b-pi" style="display: inline;"><img alt="Fusion_360_Publish" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6ccf255970b image-full img-responsive" src="/assets/image_892377.jpg" title="Fusion_360_Publish" /></a></p>
<p>公開が完了すると、<a href="https://fusion360.autodesk.com/projects/recent" target="_blank">Gallery の RECENT ページ</a>にモデルが表示されるはずです。</p>
<p style="text-align: center;">&#0160;&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6ccf414970b-pi" style="display: inline;"><img alt="Fusion_360_Gallery" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6ccf414970b image-full img-responsive" src="/assets/image_370605.jpg" title="Fusion_360_Gallery" /></a></p>
<p style="text-align: left;">さて、Fusion 360 Gallery に公開されたモデルの中には、3D モデル（.f3d ファイル）自体が公開されているものがあります。このような公開モデルは、<span style="color: #fdeee0;"><strong><span style="background-color: #bfbf00;"> &#0160;3d mo<strong>de</strong>l &#0160;<span style="background-color: #fdeee0;">&#0160;</span></span></strong></span>の帯が表示されているはずです。3D モデルが公開されいる場合、プレビュー画像にAutoCAD 360 のビューワー機能を利用して、自身で作成した Web サイトに埋め込んで再利用することが出来ます。方法はいたって簡単です。対象のモデルのプレビューをクリックしてみてください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6ccf54e970b-pi" style="display: inline;"><img alt="Fusion_360_Published_Model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6ccf54e970b image-full img-responsive" src="/assets/image_770708.jpg" title="Fusion_360_Published_Model" /></a></p>
<p style="text-align: left;">ページ下部にある公開画像やモデルの中から、3D モデルを見つけてクリックすると（①）、画面上にビューワーが表示されます。この際に、ビューワー領域右下にある <span style="color: #fdeee0; background-color: #737373;">&#0160;&lt;/&gt;Embed </span><span style="background-color: #fdeee0;"><span style="color: #fdeee0;">&#0160;<span style="color: #111111;">ボタン</span></span><span style="color: #111111;"> &#0160;</span></span>（②）をクリックしてください。ビューワー埋め込みに利用する HTML コードが表示されるはずです。</p>
<p style="text-align: left;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73e02bc24970d-pi" style="display: inline;"><img alt="Fusion_360_Embed_Code" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73e02bc24970d image-full img-responsive" src="/assets/image_94014.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Fusion_360_Embed_Code" /></a></p>
<p style="text-align: left;">あとは表示サイズを指定して、&lt;iframe&gt;～&lt;/iframe&gt; の箇所をすべて選択して&#0160;Web ページに貼り付けるだけです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="400" mozallowfullscreen="" src="https://fusion360.autodesk.com/models/f6d4a0b2461281553825096cc151b18d/embed" webkitallowfullscreen="" width="520"></iframe></p>
<p style="text-align: left;">この埋め込み機能も Autodesk 360 View &amp; Data サービス API を利用したものであることは言うまでもありません。Autodesk 360 View &amp; Data サービス API が持つ機能の一部しか利用していませんが、同等の表示機能を提供しています。表示には WebGL 対応の Web ブラウザ（Google Chrome、Mizilla Firefox、Micoroft Internet Explorer 11 以上など）が必要になりますが、プラグインのインストールは必要ありません。</p>
<p style="text-align: left;">JavaScript や REST API を使った Autodesk 360 View &amp; Data サービスのフルカスタマイズを実施する前に、ビューワー埋め込みを少し体験することが出来ます。</p>
<p style="text-align: left;">By Toshiaki Isezaki&#0160;</p>
<p style="text-align: left;">&#0160;</p>
