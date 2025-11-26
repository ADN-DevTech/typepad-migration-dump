---
layout: "post"
title: "A360 のプレゼンテーション作成機能"
date: "2014-11-26 00:08:04"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/11/a360-presentation-dapabilities.html "
typepad_basename: "a360-presentation-dapabilities"
typepad_status: "Publish"
---

<p>Web の世界では、異なるクラウド サービスを組み合わせて別のサービスを構築することを <a href="http://ja.wikipedia.org/wiki/%E3%83%9E%E3%83%83%E3%82%B7%E3%83%A5%E3%82%A2%E3%83%83%E3%83%97_%28Web%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0%29" target="_blank"><strong>マッシュアップ</strong></a> と呼んでいます。オートデスクが提供するクラウド サービスにも、このマッシュアップを見ることが出来ます。少し前になりますが、過去の<a href="http://adndevblog.typepad.com/technology_perspective/2014/08/fusion-360-gallery-and-embed-viewer.html" target="_blank">ブログ記事</a>で Fusion 360 に統合された A360 のビューワー機能や、A360 Rendering をご紹介したこともあります。</p>
<p>現在の Fusion 360 では、このマッシュアップは取り除かれていますが、一部の機能を強化して、A360 に実装を移していますので、今日は、マッシュアップによって A360 に組み込まれたプレゼンテーション機能に特化してご案内したいと思います。</p>
<p>Fusion 360 で新規に作成したり、Fusion 360 上にインポートして編集したデータが A360（<a href="http://autodesk360.com" target="_blank">http://autodesk360.com</a>） 上に保存されている場合、その 3D モデルをビューワーで表示、検索するだけでなく、マッシュアップされた A360 Rendering の機能を用いて、A360 上で、フォトリアリスティックなレンダリング画像を入手することが出来ます。</p>
<p>生成されるレンダリング画像の品質は Preview レベルですが、ここから品質やその他のオプションを再指定して、新しくレンダリング画像を生成することも可能です。また、A360 上で生成されたレンダリング画像は、A360 Rendering（<a href="http://rendering.360.autodesk.com" target="_blank">http://rendering.360.autodesk.com</a>）からも確認することが可能です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d09a02e8970c-pi" style="display: inline;"><img alt="A360_viewer_mashup" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d09a02e8970c image-full img-responsive" src="/assets/image_482617.jpg" title="A360_viewer_mashup" /></a></p>
<p>さて、このプレゼンテーション機能には、<strong>ターンテーブル</strong> と呼ばれるアニメーション作成機能も追加されています。この機能を利用すると、最大36 フレームを使って、対象物を 360 度回転させるアニメーションを作成することが出来ます。言い換えれば、36 枚の静止レンダリング画像をつなぎ合わせて動画を作成することになるので、なめらかな動きにはなりませんが、3D モデルをさまざまな角度からレンダリング画像品質で確認することが出来ます。この機能を動画にまとめましたので参照してみてください。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/f42103QAOGw?feature=oembed" width="500"></iframe>&#0160;</p>
<p>ターンテーブル機能で作成した動画は、クライアント側にダウンロードして再生することも出来ます。実際にダウンロードした動画は、次のようなものになっています。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="344" src="http://www.youtube.com/embed/tc6piWWr-Qg?feature=oembed" width="459"></iframe>&#0160;</p>
<p>また、A360 ビューワーの Rendering タブ右手には、<strong>AUTODESK A360 INTERACTIVE RENDERING <span style="color: #60bf00;">BETA</span></strong> と書かれたラベルがあります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7104cc2970b-pi" style="display: inline;"><img alt="A360_interactive_rendering" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7104cc2970b img-responsive" src="/assets/image_547845.jpg" title="A360_interactive_rendering" /></a>&#0160;</p>
<p>まだ Beta 版扱いなので正式なものではありませんが、この機能もユニークなプレゼンテーション機能を提供します。オートデスク製品では、しばしば、Interactive を「対話的」と訳していますが、静止画としてのレンダリング画像ではなく、まさしく対話的にプレゼンテーション画像を提供します。</p>
<p>このラベルをクリックすると、別ウィンドウが起動します（Google Chrome を推奨）。マウス操作でズームやパン、オービットなどの視点変更操作をしたり、Drag &amp; Drop 操作で対話的にマテリアルを置き換えて対象物を評価することが出来るようになります。</p>
<p>特徴的なのは、「環境」を反映できる点です。環境とは、光源情報を持つ周囲 360 度の画像を指しています。このため、用意された環境を画面上に適用すると、視点によって背景ばかりか光や影の状態も変化するので、よりリアリティを持つ画像を生成することが出来ます。実際に操作した動画を用意しましたので、ここまで説明した内容をご確認ください。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/duMWJALo-oo?feature=oembed" width="500"></iframe>&#0160;</p>
<p>もし、オートデスクのデスクトップ製品の中で <a href="http://www.autodesk.com/products/showcase/" target="_blank"><strong>Showcase</strong></a> という製品をご存じであれば、画像品質や応答性はさておき、類似した機能を持っていることにお気づきかも知れません。残念ながら、Fusion 360 の 3D モデルでしか使用することが出来ませんが、Web ブラウザだけで、ここまで実現できるようになっているのです。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
