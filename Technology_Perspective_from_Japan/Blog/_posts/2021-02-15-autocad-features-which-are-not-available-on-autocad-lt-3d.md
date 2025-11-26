---
layout: "post"
title: "AutoCAD LT にない AutoCAD 機能：3D"
date: "2021-02-15 02:07:20"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/02/autocad-features-which-are-not-available-on-autocad-lt-3d.html "
typepad_basename: "autocad-features-which-are-not-available-on-autocad-lt-3d"
typepad_status: "Publish"
---

<p>AutoCAD の姉妹製品として、AutoCAD の一部の機能を省略した AutoCAD LT があります。どちらかというと、&quot;レギュラー版&quot; AutoCAD の廉価版と認識されているのではないかと思います。</p>
<p>用途の違いでよく紹介されているのは、2D と 3D の違いです。2D 図面を作成するには AutoCAD LT、3D モデリングをするなら AutoCAD を、と言ったものです。もちろん、AutoCAD LT の機能は AutoCAD にも搭載されているので、AutoCAD でも 2D 図面作成は可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0263e98ab53d200b-pi" style="display: inline;"><img alt="2d+3d" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0263e98ab53d200b image-full img-responsive" src="/assets/image_636484.jpg" title="2d+3d" /></a></p>
<p>AutoCAD で 3D モデリングをおこなう場合、扱う図形は、大別して <strong>ソリッド</strong>（<strong>3D&#0160;ソリッド</strong> とも呼称）、<strong>サーフェス</strong>、<strong>メッシュ</strong>（<strong>ポリゴンメッシュ</strong> とも呼称）の 3 種類のみです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeb80213200c-pi" style="display: inline;"><img alt="3d_object_type" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeb80213200c image-full img-responsive" src="/assets/image_41928.jpg" title="3d_object_type" /></a></p>
<p>それぞれの特徴は次のとおりです。</p>
<p style="padding-left: 40px;"><strong>ソリッド</strong></p>
<ul>
<li>
<ul>
<li>体積や質量、重心等の情報を持つ中身の詰まった <strong>かたまり</strong></li>
<li><strong>プリミティブ </strong>と呼ばれる基本形状をもとに作成</li>
<li><strong>ブール演算 </strong>でソリッド同士の 和（結合）、差（差し引く）、交差（重複箇所の抽出） の生成が可能</li>
</ul>
</li>
</ul>
<p style="padding-left: 40px;"><strong>サーフェス</strong></p>
<ul>
<li>
<ul>
<li><strong>厚みがない</strong>、平らな平面や、凹凸のある自由曲面を表現</li>
<li>補間多項式などの方程式で形状を定義</li>
</ul>
</li>
</ul>
<p style="padding-left: 40px;"><strong>メッシュ</strong></p>
<ul>
<li>
<ul>
<li><strong>ポリゴン</strong><strong>(</strong><strong>面</strong><strong>)</strong> の集合体</li>
<li><strong>ポリゴンの頂点</strong>、<strong>エッジ</strong>での編集が可能</li>
<li>自由な形状表現に適している</li>
</ul>
</li>
</ul>
<p>少し難しく感じてしまうかも知れませんが、特徴を一言で表現するなら、<strong>積み木のようなソリッド</strong>、<strong>紙のようなサーフェス</strong>、<strong>粘土のようなメッシュ</strong>、といった感じです。モデリング操作は至って簡単です。それぞれの種類の図形が登場したバージョンによってユーザインタフェースが異なりますが、モデリングの様子は下記のようになっています。これらは、最新の AutoCAD でも使用することが出来ます。</p>
<p style="padding-left: 40px;"><strong>積み木のようなソリッド</strong></p>
<p class="asset-video" style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/pm6qE7W6cVs" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>紙のようなサーフェス</strong></p>
<p style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/6w85MQkF8H0" width="480"></iframe></p>
<p style="padding-left: 40px;"><strong>粘土のようなメッシュ</strong></p>
<p style="padding-left: 40px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/CyR5spjfCJU" width="480"></iframe></p>
<p>Revit や Inventor の 3D と何が違うかという点では、BIM やデジタルプロトタイプといった、言わば実施設計や製造までを念頭に置いたものではなく、「こんな建物、モノがあったらいいな」的な 3D 作成、プレゼンを目的としたのが AutoCAD の 役割と捉えることが出来ます、こいうったい意図を持つ 3D を<strong>コンセプトモデル</strong>と呼んでいます。</p>
<p>このため、厳密に 3D モデルを作成する必要はありません。AutoCAD の 3D には拘束もありませんので、線分や円、円弧、ポリラインで 2D 作図する感覚で 3D モデルを構築していくことが出来ます。どこまで詳細に形状を作りこむかも自由です。</p>
<p>モデリング後には素材感を出すために 3D 図形にマテリアルを与えて、レンダリング 機能を使用することで、フォトリアリスティック画像（写真のような CG 画像）を AutoCAD の演算処理で得ることが出来ます。簡単にモデリングして、最終的な「見た目」を比較も容易です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278800fdbfa200d-pi" style="display: inline;"><img alt="Watch_design_comparison" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278800fdbfa200d image-full img-responsive" src="/assets/image_155801.jpg" title="Watch_design_comparison" /></a></p>
<p>3D モデルを含む図面に測地系座標を埋め込んで、実際の緯度経度、高度を利用してレンダリング画像に反映させることも出来ます。何月何日の何時に、太陽光がどのように差し込むかなどのシミュレーションで応用出来るはずです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/soOmYq8uBDo" width="480"></iframe></p>
<p>また、同じ 3D モデルから動画を作成することも出来ます。残念ながら、タイムライン機能がないので、モデリングした 3D 図形が時間経過とともに動くような動画は作成出来ませんが、視点の移動を取り込むことは可能になっています（移動パスアニメーション）。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/weXCkGIh760" width="480"></iframe></p>
<p style="text-align: left;">パスに沿って視点を移動させれば、ウォークスルーを記録した動画を作成して評価で利用できるかもしれません。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/788Rzy-TbP8" width="480"></iframe></p>
<p>AutoCAD 2016 以降では、あらかじめ用意された周囲の環境（背景画像と照明）を使用して動画作成することも出来るようになっているので、より臨場感のある動画が得られます。</p>
<p class="asset-video" style="text-align: center;"><iframe allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="" frameborder="0" height="510" src="https://www.youtube.com/embed/kQ5Bfa1zPmA?feature=oembed" width="660"></iframe></p>
<p>現況の点群データを取り込んで、AutoCAD で改修用のコンセプトモデルを作成、評価することも出来ます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/1FGtx8Wd4F0" width="480"></iframe></p>
<p>同様に、Navisiworks ファイルをアンダーレイとして取り込んで、モデルコーディネーションとして評価、モデリングで利用することも出来ます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/wDoCgBhMkfs" width="480"></iframe></p>
<p>モデル空間にモデリング、あるいは、他の CAD ソフトウェアからインポートされた 3D モデルは、複数の方法でレイアウト上に図面化していくことが出来ます。</p>
<p>指定した等角投影で 3 面図を作成出来るほか、断面図か詳細図を尺度を指定しながら自動的に作成することが出来ます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/1ttJWqDP6O4" width="480"></iframe></p>
<p>もちろん、従来の浮動ビューポートを利用した方法で図面レイアウトを作成することも可能なので、業種を選ばずに図面作成に活用出来ます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/xDdwg01fTgQ" width="480"></iframe></p>
<p>実施設計で 2D 図面を作図されている方には遠い存在かもしれませんが、お使いの AutoCAD が使い方次第で大きな可能性を秘めているのが おわかりいただけるといいかと思います。</p>
<p>ここでご紹介したさまざまな機能を利用するため、AutoCAD には [3D 基本] と [3D モデリング] の 2 つのワークスペースが用意されています。複数のリボンタブで構成されるユーザ インタフェースを見れば、数多くの機能が搭載されていることがわかります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880152c54200d-pi" style="display: inline;"><img alt="3d_workspaces" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880152c54200d image-full img-responsive" src="/assets/image_889693.jpg" title="3d_workspaces" /></a></p>
<p>ぜひ、お試しください。</p>
<p>By Toshiaki Isezaki</p>
