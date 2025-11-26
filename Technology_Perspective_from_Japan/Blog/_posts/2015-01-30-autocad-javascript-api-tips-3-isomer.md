---
layout: "post"
title: "AutoCAD JavaScript API TIPS ～ 3 Isomer編"
date: "2015-01-30 22:14:35"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/01/autocad-javascript-api-tips-3-isomer.html "
typepad_basename: "autocad-javascript-api-tips-3-isomer"
typepad_status: "Publish"
---

<p>この連載では、AutoCAD JavaScript API と連携してご利用いただける HTML5 ベースのオープンソースライブラリをご紹介いたします。</p>
<p>これまで、AutoCAD JavaScript API をご活用いただく上で知っておいて頂きたい前提知識と、ベクタ・ラスタ形式のグラフィックスを扱えるオープンソースのグラフィックスライブラリ Paper.js についてご紹介いたしました。</p>
<ul>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-2015-javascript-api-usage1.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 1 前提知識編">AutoCAD JavaScript API TIPS ～ 1 前提知識編</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-javascript-api-tips-2-paperjs.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 2 Paper.js編">AutoCAD JavaScript API TIPS ～ 2 Paper.js編</a></li>
</ul>
<p>今回は Isomer をご紹介いたします。</p>
<ul>
<li><a href="http://jdan.github.io/isomer/" target="_blank" title="Isomer">Isomer</a></li>
</ul>
<p>Isomer は、HTML5 Canvas 上で、アイソメトリックな図形を描画できるオープンソースのグラフィックスライブラリです。<br />ポイント、パス、シェイプの3つの基本構成要素からなります。ポイントからパスを形成し、パスからシェイプを作ります。<br />それぞれの要素は、移動、回転、スケールのベーシックなメソッドを持っております。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07e6480b970d-pi" style="display: inline;"><img alt="Isomer_image" class="asset  asset-image at-xid-6a0167607c2431970b01bb07e6480b970d img-responsive" src="/assets/image_903512.jpg" title="Isomer_image" /></a></p>
<p>今回は、AutoCAD から 3D ソリッドのバウンディングボックスに関する情報を JSON で取得して、シェイプで形状をCanvas 上に再現するサンプルを作成してみました。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0cc0d23970c-pi" style="display: inline;"><img alt="Isomer" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0cc0d23970c img-responsive" src="/assets/image_334933.jpg" title="Isomer" /></a></p>
<p>Isomer の特徴として、描画する順序を指定できるという点があります。アイソメ図の場合、要素の奥行き方向の距離によって重なり方が異なりますので、これをコントロールしなければなりません。</p>
<p>たとえば、AutoCAD から取得したソリッドの配列に対して、順序でソートするロジックを組み込むことができます。要素の手前側の面の中心点とカメラの距離を計算する方法が考えられるでしょう。</p>
<p>AutoCAD 上でソリッドが追加されたり編集された際には、これを Canvas 上のシェイプに即座に反映させることもできます。</p>
<p>ご興味のある方は、ぜひ一度お試しください。</p>
<p>By Ryuji Ogasawara</p>
