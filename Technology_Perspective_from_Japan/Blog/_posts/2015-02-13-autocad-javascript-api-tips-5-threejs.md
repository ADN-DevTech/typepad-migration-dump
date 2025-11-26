---
layout: "post"
title: "AutoCAD JavaScript API TIPS ～ 5 three.js編"
date: "2015-02-13 00:32:47"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/02/autocad-javascript-api-tips-5-threejs.html "
typepad_basename: "autocad-javascript-api-tips-5-threejs"
typepad_status: "Publish"
---

<p>この連載では、AutoCAD JavaScript API と連携してご利用いただける HTML5 ベースのオープンソースライブラリをご紹介いたします。</p>
<p>前回は、データを視覚化するためのライブラリ D3.js についてご紹介いたしました。</p>
<ul>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-2015-javascript-api-usage1.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 1 前提知識編">AutoCAD JavaScript API TIPS ～ 1 前提知識編</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-javascript-api-tips-2-paperjs.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 2 Paper.js編">AutoCAD JavaScript API TIPS ～ 2 Paper.js編</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-javascript-api-tips-3-isomer.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 3 Isomer編">AutoCAD JavaScript API TIPS ～ 3 Isomer編</a></li>
<li><a href="http://adndevblog.typepad.com/technology_perspective/2015/02/autocad-javascript-api-tips-4-d3js.html" target="_blank" title="AutoCAD JavaScript API TIPS ～ 4 D3.js編">AutoCAD JavaScript API TIPS ～ 4 D3.js編</a></li>
</ul>
<p>今回は three.js をご紹介いたします。</p>
<p>three.js は、 Web ブラウザ上でプラグインなしに JavaScript によって 3DCG を扱うことのできるオープンソースのグラフィックスライブラリです。 WebGL, &#0160;HTML5 Canvas, SVG に対応するレンダラーを提供しており、またそれらの API をラップすることで、より扱いやすいインターフェースとなっております。</p>
<p>ここではまず、 three.js がサポートしている WebGL という技術についてご説明いたします。</p>
<p>WebGL は、OpenGL 2.0 / OpenGL ES2.0 に対応するプラットフォーム上で、特別なプラグインをインストールすることなく、ハードウェアでアクセラレートされた 3DCG を表現することができる技術です。 WebGL の API を通じて、 OpenGL や DiretX のランタイムにアクセスできるため、 Web ブラウザから GPU のリソースを活用することができます。</p>
<p>WebGLデモ（ <a href="http://webglsamples.org/aquarium/aquarium.html" target="_blank" title="Aquarium">Aquarium</a>&#0160;）</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07efdfdb970d-pi" style="display: inline;"><img alt="Aquarium2" class="asset  asset-image at-xid-6a0167607c2431970b01bb07efdfdb970d img-responsive" src="/assets/image_734647.jpg" title="Aquarium2" /></a></p>
<ul>
<li><a href="http://www.chromeexperiments.com/webgl/" target="_blank" title="Chrome Experiments">Chrome Experiments</a></li>
<li><a href="https://developer.mozilla.org/ja/demos/tag/tech:webgl" target="_blank" title="mozilla WebGL Demo">mozilla WebGL Demo</a></li>
</ul>
<p>WebGL は高速な描画処理を可能とし、OpenGL ES2.0 の機能を利用できるという点においては非常に強力なライブラリですが、その仕様の複雑さから、実装が難しいという問題があります。</p>
<p>そこで WebGL の機能をラップすることで、レンダラ、シーン、ライト、カメラ、ジオメトリ、マテリアル、テクスチャ等といったわかりやすい要素から構成されたライブラリが three.js です。</p>
<ul>
<li><a href="http://threejs.org/" target="_blank" title="three.js">three.js</a>　(&#0160;<a href="http://mrdoob.github.io/three.js/" target="_blank" title="github">github</a>&#0160;)</li>
</ul>
<p><a href="http://adndevblog.typepad.com/technology_perspective/2015/01/autocad-2015-javascript-api-usage1.html" target="_blank">1 前提知識編</a>&#0160;でご説明したとおり、AutoCAD JavaScript API が、Google 社の Chrome ブラウザがベースとしている Chromium Embedded Framework を描画用のブラウザに採用していることにより、AutoCAD 上でもこのthree.js を利用することができます。</p>
<p>以下のサンプル画面は、AutoCAD の図面ドキュメント上に配置したソリッドモデルを、JavaScript API と three.js を利用して表示するサンプル（ ADN チームの Kean Walmsley が作成したサンプルプロジェクト）です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c74c2881970b-pi" style="display: inline;"><img alt="Threejs" class="asset  asset-image at-xid-6a0167607c2431970b01b7c74c2881970b img-responsive" src="/assets/image_754445.jpg" title="Threejs" /></a></p>
<p>AutoCAD の C# プログラムからソリッドモデルのリストを取得して、そのジオメトリ情報を JavaScript API を通じてJSON データとして渡し、 three.js の API から WebGL のプログラムに変換して表示しています。</p>
<p>three.js でのオブジェクトは、 Object3D クラスを基底クラスとするいくつかのクラス、たとえば Mesh クラス等からインスタンスを生成します。 Mesh オブジェクトのインスタンスを生成する際には、引数として Geometry オブジェクトと Material オブジェクトを渡します。 Geometry オブジェクト、 Material オブジェクトは、それぞれ形状と質感を表現する複数のクラスからなります。</p>
<p>シーンにカメラとライトを追加し、オブジェクトを配置して、 WebGL レンダラーの render メソッドを呼び出すというシンプルなステップで Web ブラウザ上に描画することができます。ご興味のある方は、ぜひ一度お試しください。</p>
<p>また three.js に関連する情報として、 Autodesk View and Data API でも、ライブラリの内部で three.js を利用しております。これにより、 WebGL を通じた高速な描画処理と OpenGL ES2.0 の機能を活用して、クロスブラウザで 3D モデルを表示することを実現しております。</p>
<p>本連載を通じて、AutoCAD JavaScript API と親和性の高いオープンソースのグラフィックスライブラリをご紹介してきました。<br />Web の世界の技術を取り入れることで、AutoCAD の新しいユーザーインターフェースデザイン、Web サービス連携、新機能による拡張など、様々な可能性を発見していただければ幸いです。</p>
<p>By Ryuji Ogasawara</p>
