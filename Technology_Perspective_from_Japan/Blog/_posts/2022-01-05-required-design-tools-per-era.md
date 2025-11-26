---
layout: "post"
title: "設計ツールとデザインデータの遷移"
date: "2022-01-05 03:32:35"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/12/required-design-tools-per-era.html "
typepad_basename: "required-design-tools-per-era"
typepad_status: "Publish"
---

<p>ご存じのとおり、昨年の秋口にオートデスクは会社のロゴを変更しています。会社設立以来、通算で 4 回目、ロゴ色の変更を含めると 5 回目の変更です。</p>
<p>これらロゴはオートデスク自身が変化してきている証でもあります。年初の今回は、それぞれの時代で何が訴求されていたのか、あるいは、何が求められていたのか、今後期待される展開を踏まえて、「データ」を視点に振り返ってみたいと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f92287b200c-pi" style="display: inline;"><img alt="Technologies" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f92287b200c image-full img-responsive" src="/assets/image_416117.jpg" title="Technologies" /></a></p>
<p><strong>パソコン CAD のデータ</strong></p>
<p style="padding-left: 40px;">それまで UNIX OS を搭載する高価なエンジニアリング ワークステーションで運用するのが当たり前だった CAD、Computer Aided Design ソフトウェアを、比較的安価なパーソナル コンピュータ（PC）上で運用出来るようにしたのが AutoCAD の、そしてオートデスクの始まりです。</p>
<p style="padding-left: 40px;">といっても、当時の PC には CAD で用いる図形処理はまだまだ荷が重く、2D 図面作成が主な役割でした。<a href="https://ja.wikipedia.org/wiki/%E3%83%89%E3%83%A9%E3%83%95%E3%82%BF%E3%83%BC" rel="noopener" target="_blank">ドラフター</a>で製図していた紙図面をコンピュータ上で作図する代替手段、という捉え方です。もちろん、最終的な成果物は紙図面だった時代です。AutoCAD の「画層（レイヤ）」といった考え方も、建築躯体図面を元に<a href="https://ja.wikipedia.org/wiki/%E3%83%88%E3%83%AC%E3%83%BC%E3%82%B7%E3%83%B3%E3%82%B0%E3%83%9A%E3%83%BC%E3%83%91%E3%83%BC" rel="noopener" target="_blank">トレーシングペーパー</a>を使って設備図面や電気図面を作成する過程から生まれた紙図面由来の考え方だったりします。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f9229a7200c-pi" style="display: inline;"><img alt="2d_drawings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f9229a7200c image-full img-responsive" src="/assets/image_169899.jpg" title="2d_drawings" /></a></p>
<p style="padding-left: 40px;">2D 図面のデータ要素で求められたのは、製造業、建設業を問わず、形状とそのサイズを表現する基本的な要素図形です。代表的なものには、次のようなものがあります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13d0641200b-pi" style="float: right;"><img alt="2d_elements" class="asset  asset-image at-xid-6a0167607c2431970b0282e13d0641200b img-responsive" src="/assets/image_533806.jpg" style="margin: 0px 0px 5px 5px;" title="2d_elements" /></a></p>
<ul>
<li>線分、円、円弧、楕円</li>
<li>四角形、多角形</li>
<li>文字、寸法、ハッチング</li>
<li>連続線（ポリライン）：頂点間補間は直線か円弧</li>
<li>自由曲線（スプライン）：頂点間補間は B-Spline 多項式</li>
</ul>
<p style="padding-left: 40px;">こういった要素図形の中でも、演算負荷の影響からか、補完多項式から生まれる自由曲線がサポートされれたのは、大分後年になってからです。</p>
<p style="padding-left: 40px;">同じように後年になって初めて登場してきた要素も存在します。ラスター画像です。PC 聡明期には、.bmp に代表されるラスター画像の表示すら重たい処理の 1 つでした。</p>
<p style="padding-left: 40px;">ただ、図面作成の手段がドラフターによる手書きから、 CAD が「当たり前」になるのは、1990 年代半ばになってからの気がします。この頃になるると、PC を社内ネットワークで繋ぐ運用が徐々に広まっていきます。「ファイルサーバー」の登場です。CAD の 2D 図面データは「ファイル」で保存/管理されることになります。</p>
<p><strong>見せるためのデザイン データ</strong></p>
<p style="padding-left: 40px;">PC の性能が向上するにつれ、エンジニアリング ワークステーションの独壇場だった 3D の運用が PC に求められるようになっていきます。PC 上でも、当初、製造業用のメカニカル設計 CAD が用意されていきますが、実際には建築業種で 3D が広く利用されるようになります。</p>
<p style="padding-left: 40px;">この 頃、表現する要素はソリッドやサーフェスで、ブール演算による編集が一般的でした。用途にもよりますが、CAD でメッシュによるフリーフォーム モデリングが可能になるのは、少し時間が経ってからです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13d064e200b-pi"><br /><img alt="3d_elements" class="asset  asset-image at-xid-6a0167607c2431970b0282e13d064e200b img-responsive" src="/assets/image_188330.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="3d_elements" /></a></p>
<p style="padding-left: 40px;">パース図やレンダリング画像で用いられる形状は、当初、簡単な形状にテクスチャ画像を貼り付ける（マッピング）方法で PC 負荷を低減していました。<a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880646b6d200d-pi" style="display: inline;"><img alt="Texture1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880646b6d200d image-full img-responsive" src="/assets/image_896539.jpg" title="Texture1" /></a></p>
<p style="padding-left: 40px;">こういった部分でも、時代とともに、より精緻な形状へ遷移して、よりリアルなものへ変わっていきます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880649b10200d-pi" style="display: inline;"><img alt="Texture2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880649b10200d image-full img-responsive" src="/assets/image_527175.jpg" title="Texture2" /></a></p>
<p style="padding-left: 40px;">3D による「見せるためのデータ」は、2D 図面作成とは別のソフトウェアを使って、独立した業務として作成されていたのが特徴です。</p>
<p style="padding-left: 40px;">紙図面の代替として作成される 2D の図面データも、ある意味「見せるためのデータ」と考えることが出来ます。もちろん、これら「見せるためのデータ」も「ファイル」で保存/管理されています。</p>
<p><strong>意味を持つデザイン データ</strong></p>
<p style="padding-left: 40px;">PC が高性能化するにつれ、設計で用いるデータと「見せるためのデータ」を融合して、図形要素に意味を持たせようという動きが出てきます。最初、2D 図面で図形に形状データとは別のカスタム データを付加して、集計/積算や部品表作成に利用していました。</p>
<p style="padding-left: 40px;">3D データが扱えるようになると、ソリッド、サーフェス、ポリゴン メッシュへの意味づけが始まり、理論として研究されていた建設業種の BIM/CIM によるライフサイクル管理へと繋がっていきます。もちろん、製造業種でエンジニアリング ワークステーションで実現されていたの部品材質の付加と、それらをベースにした解析/シミュレーションも PC 化が進んでいくことになります。</p>
<p style="padding-left: 40px;">意味づけされた図形要素には、次のようなものがあります。</p>
<ul>
<li>柱、梁、壁、床、鉄筋（鉄筋径、ラップ、曲げR…）</li>
<li>空調ダクト、排水パイプ、窓、ドア、机、椅子 …</li>
<li>クランクシャフト、歯車、ピストン、ゴムパッキン …</li>
<li>材質、密度、質量、重心 …</li>
<li>熱伝導率、粘性度、溶解温度、沸点 …</li>
</ul>
<p style="padding-left: 40px;">言うまでもなく、これら「意味を持つデータ」も「ファイル」で保存/管理されます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13d326f200b-pi" style="display: inline;"><img alt="Meaning_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13d326f200b image-full img-responsive" src="/assets/image_388184.jpg" title="Meaning_data" /></a></p>
<p><strong>現況データ</strong></p>
<p style="padding-left: 40px;">設計データのない現況を取り込んで利用する動きも出てきます。写真から 3D メッシュを合成するフォトグラメトリー、また、レーザースキャナーから得られる点群データです。いずれもサイズが大きいため、クラウドが登場して実現できたデータと言えます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880646c39200d-pi" style="display: inline;"><img alt="Photogrametry" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880646c39200d image-full img-responsive" src="/assets/image_719973.jpg" title="Photogrametry" /></a></p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13d0701200b-pi" style="display: inline;"><img alt="Point_cloud" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13d0701200b image-full img-responsive" src="/assets/image_508487.jpg" title="Point_cloud" /></a></p>
<p><strong>クラウド時代のデータ</strong></p>
<p style="padding-left: 40px;">2D、3D を問わず「見せるためのデータ」は「意味を持つデータ」になり、「現況データ」も取り込んで肥大化しています。にもかかわらず、データは「ファイル」であり続けています。また、デザイン データと直接関係のないデータは、「ファイル」に関連付けられて外部データベースで管理されています。そして、「ファイル」は保存元の CAD ソフトウェアによって異なる形式を持ってしまっています。</p>
<p style="padding-left: 40px;">「ファイル」は今後もデータの可搬性を維持するために残り続けるはずです。ただ、「ファイル」ありきのデータは限界が見えてきています。そんな背景でアナウンスされたのが <a href="https://adndevblog.typepad.com/technology_perspective/2021/10/forge-as-a-platform.html" rel="noopener" target="_blank">Forge プラットフォーム</a>が提供するクラウド時代のデータの在り方です。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f925273200c-pi" style="display: inline;"><img alt="Platform_vision-transformation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f925273200c image-full img-responsive" src="/assets/image_307938.jpg" title="Platform_vision-transformation" /></a></p>
<p style="padding-left: 40px;">ファイルを分解した粒状データとして個々に扱えるようになれば、データは今まで以上に自由な存在になっていきます。図形/形状は属性/プロパティと同レベルになるので、図形が意味を持つ、というより、同等な両者をロジックで関連付けるような考えが成り立ちます。ファイルもデータの表現形態の 1 つでしかありません。異なるソフトウェアでも、同一プラットフォームを採用すれば、クラウド上の同一データを別の用途で利用できます。</p>
<p>By Toshiaki Isezaki</p>
