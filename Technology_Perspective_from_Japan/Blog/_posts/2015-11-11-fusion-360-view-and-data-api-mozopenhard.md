---
layout: "post"
title: "Fusion 360 + View and Data API + MozOpenHard"
date: "2015-11-11 00:02:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/11/fusion-360-view-and-data-api-mozopenhard.html "
typepad_basename: "fusion-360-view-and-data-api-mozopenhard"
typepad_status: "Publish"
---

<p>Makers や Fbricaors と呼ばれる方が製作した情報を共有する&#0160;<strong><a href="http://www.instructables.com/" target="_blank">instructables</a>&#0160;</strong>というサイトをご存知でしょうか。このサイトには、さまざまなアイデアや製作に必要な情報が記載されています。3D プリンタが普及期になったいま、このようなポータル サイトが多く見受けられるようになってきました。</p>
<p>この inscructables からインスピレーションを得て作成されたのが、Autodesk University Japan 2015 や <a href="http://adndevblog.typepad.com/technology_perspective/2015/10/tokyo-pop-up-gallery.html" target="_blank"><strong>Gallery Pop-Up Tokyo</strong></a>&#0160;で <a href="http://www.mozilla.jp/" target="_blank"><strong>Mozilla Japan</strong></a> 赤塚さんと <strong><a href="http://www.pxgrid.com/" target="_self">PixcelGrid</a></strong> の小山田さんにデモしていただいた世界時計です。Brian Wagner 氏が公開している情報は、<strong>instructables</strong> の&#0160;<strong><a href="http://www.instructables.com/id/Laser-cut-gear-clock-with-Chronodot/?ALLSTEPS" target="_blank">こちらのページ</a>&#0160;</strong>に記載されています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e8f98a970b-pi" style="display: inline;"><img alt="Instructables" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e8f98a970b image-full img-responsive" src="/assets/image_549698.jpg" title="Instructables" /></a></p>
<p>記載されているオリジナルの情報では、Arduino（アルドゥイーノ）というワンボード マイコン互換のキットがプログラムとともに記載されています。Autodesk University Japan にあたって、Arduino&#0160;部分を <a href="http://mozopenhard.mozillafactory.org/" target="_blank"><strong>MozOpenHard</strong></a> の CHIRIMEN ボードで JavaScript を使ってコントロールしようというものです。</p>
<p>時計を表現する可動部分は、同サイトで <a href="https://ja.wikipedia.org/wiki/Scalable_Vector_Graphics" target="_blank"><strong>SVG ファイル</strong></a>形式でダウンロードすることが出来ます。SVG は、もともとベクトル情報を扱うために定義された形式ですが、現在では <a href="http://www.w3.org/Graphics/SVG/" target="_blank">W3C で標準化されている</a>ので、Web の世界では一般的です。レーザーカッターで&#0160;SVG の形状に沿って木材の板カットすることで、時計の形状を得ることが出来ます。</p>
<p>Autodesk University Japan では、このデータを再利用されたのが、実際に披露された MozOpenHard 版の世界時計です。もちろん、イベントでは 3D 化された世界時計を View and Data API で制御して、フロント インタフェースとして利用していただきました。</p>
<p>実は、Fusion 360 には、SVG ファイルをスケッチとしてインポートする機能があります。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d16f397f970c-pi" style="display: inline;"><img alt="Import_svg" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d16f397f970c image-full img-responsive" src="/assets/image_863137.jpg" title="Import_svg" /></a></p>
<p>インポートされたデータはレーザーカット用に余白を利用した入り組んだ状態になっていますが、Fusion 360 のコンポーネント作成用にスケッチを分割して作成、編集していくことも容易です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088cd6a1970d-pi" style="display: inline;"><img alt="Import_svg_onto_fusion_360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb088cd6a1970d image-full img-responsive" src="/assets/image_768160.jpg" title="Import_svg_onto_fusion_360" /></a></p>
<p>適宜、スケッチからコンポーネントを作成してジョイントさせていくだけで、アニメーションを含むシミュレーションをおこなうことが出来るわけです。もちろん、マテリアルの適用して意匠を検討することも出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088cd750970d-pi" style="display: inline;"><img alt="Clock_on_fusion_360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb088cd750970d image-full img-responsive" src="/assets/image_656961.jpg" title="Clock_on_fusion_360" /></a></p>
<p>マテリアルを変えたパターンを幾つか用意したら、View and Data API で扱えるようにクラウドにバケットを作ってストリーミング配信できるように調整します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e8fc69970b-pi" style="display: inline;"><img alt="Lmv" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e8fc69970b image-full img-responsive" src="/assets/image_941069.jpg" title="Lmv" /></a></p>
<p>もちろん、Web ブラウザで表示する際には、JavaScript でコントロールを加えることが出来ます、また、CHIRIMEN ボードも JavScript でコントロールするので、Web 開発の知識さえあれば、ハードウェアの制御が可能なのです。<br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb088cd80f970d-pi" style="display: inline;"><img alt="Gallery_pop-up" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb088cd80f970d image-full img-responsive" src="/assets/image_232804.jpg" title="Gallery_pop-up" /></a></p>
<p>CHIRIMEN ボードは WiFi 経由で赤塚さんの Mac に接続されています。Mac 上では 3D モデルを表示中の Web ページが表示されていて、この Web ページが View and Data API と CHIRIMEN ボードをコントロールする&#0160;JavaScript が記述されています。まさに、Makers 向けの IoT/WoT と考えることが出来るわけです。</p>
<p>View and Data API や Fusion 360 は、さまざまな用途で利用することが出来ますが、こういった利用方法は、その一例です。</p>
<p>By Toshiaki Isezaki&#0160;</p>
