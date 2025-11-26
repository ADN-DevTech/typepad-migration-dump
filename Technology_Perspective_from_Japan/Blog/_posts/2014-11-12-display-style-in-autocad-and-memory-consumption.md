---
layout: "post"
title: "AutoCAD の表示スタイルとメモリ消費"
date: "2014-11-12 00:05:35"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/11/display-style-in-autocad-and-memory-consumption.html "
typepad_basename: "display-style-in-autocad-and-memory-consumption"
typepad_status: "Publish"
---

<p>今日は、アドイン開発者の知識として、AutoCAD 実行中に消費されるメモリについてご紹介したいと思います。あまり知られていない仕組みなのですが、AutoCAD を利用して図面を作成をする際には、<a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-F9113233-6798-4F5C-9A9F-7BA41CFA2533" rel="noopener" target="_blank"><strong>表示スタイル</strong></a>によって消費されるメモリ量が異なってきます。</p>
<p>最新バージョンの AutoCAD 2015 では、2D 図面や 3D モデルの表示用に <strong>2D ワイヤーフレーム</strong>、<strong>コンセプト</strong>、<strong>陰線処理</strong>、<strong>リアリスティック</strong>、<strong>シェード</strong>、<strong>シェードとエッジ</strong>、<strong>グレーシェード</strong>、<strong>スケッチ</strong>、<strong>ワイヤーフレーム</strong>、<strong>X 線</strong> の計 10 種類の表示スタイルが用意されています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a7557d970d-pi" style="display: inline;"><img alt="Display_style" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a7557d970d img-responsive" src="/assets/image_319213.jpg" style="width: 320px;" title="Display_style" /></a></p>
<p>2D ワイヤーフレーム 表示スタイルは、AutoCAD が昔から利用していた&#0160;<strong>Whip</strong> と呼ばれるテクノロジを利用していて、主に 2D 図面を表示して編集する際に利用する表示スタイルです。</p>
<p>3D モデリングをされない方は特に意識されていないかも知れませんが、2D ワイヤーフレーム以外の表示スタイルは、3D モデリングや 3D 表示用にチューニングが施されたテクノロジが利用されています。このテクノロジは、<strong>One Graphics System（OGS）</strong> と呼ばれていて、AutoCAD 以外のオートデスク製品、例えば、Inventor や Revit といった製品の 3D 表示でも利用されているものです。</p>
<p>Whip と OGS の両者は、表示スタイルでの見た目の表現以外にも、消費されるメモリ量で大きな違いを持っています。 その前に、Windows アプリケーションのメモリ消費量を正しく計測する方法を復習しておきます。よく、メモリ消費量を図る上で Windows のタスクマネージャ上で AutoCAD のメモリ消費を計測されている方がいらっしゃいますが、正確には、ここで表示される値は AutoCAD が利用している全メモリ消費量とは言えません。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07a75531970d-pi" style="display: inline;"><img alt="Task_manager" class="asset  asset-image at-xid-6a0167607c2431970b01bb07a75531970d img-responsive" src="/assets/image_778754.jpg" style="width: 400px;" title="Task_manager" /></a></p>
<p>タスクマネージャ上には、ハードディスクへのメモリ スワップの領域を含む、仮想メモリが反映されてきません。ご参考まで、次の記事も参照してみてください。</p>
<p style="padding-left: 30px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/sfdcarticles/sfdcarticles/JPN/Error-Out-of-memory.html" rel="noopener" target="_blank"><strong>AutoCAD での作業時の「メモリ不足」通知</strong></a></p>
<p>さて、表示スタイルの話題に戻ります。Whip ドライバを利用する 2D ワイヤ―フレーム 表示スタイルは、2D 表示に最適化されたものなので、2D 図面の描画速度いう点でパフォーマンスは非常に良好です。ただし、2D ワイヤーフレーム環境下では、表示している図面のオブジェクトのすべての情報を、フラットにメモリ展開する仕様になっています。</p>
<p>一方、2D ワイヤーフレーム以外の OGS を利用する表示スタイルでは、大規模な 3D モデル表示に対応するため、メモリ上に展開されるオブジェクト情報を制限する仕組みが働きます。例えば、単一形状を持つブロックを何度かネストして定義して、モデル空間に挿入配置していた場合、<strong>インスタンス共有（instance sharing）</strong> と呼ばれるメカニズムを用いて、最少要素となる単一ブロックが何度もメモリ上に展開されることを防止するようになっています。このメカニズムを利用することで、より少ないメモリ消費量で、3D モデルを表示したり、操作することが可能になっています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7022010970b-pi" style="display: inline;"><img alt="Shared_instance" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7022010970b image-full img-responsive" src="/assets/image_384785.jpg" title="Shared_instance" /></a></p>
<p>逆に、同じ図面を Whip を使う 2D ワイヤーフレーム表示スタイルで開いた場合、正確に全オブジェクトの数分、すべてメモリ展開してしまうため、表示時に多くのメモリを消費する結果となります。</p>
<ul>
<li>上記のような内容を持つ図面を2つファイルコピーして、1つを 2D ワイヤーフレーム、もう 1 つをワイヤーフレームの表示スタイルのまま保存後、AutoCAD を再起動して、それぞれ1つ図面を開いた状態で消費メモリを計測してみてください。一度図面を開いてから表示スタイルを切り替えても、メモリ消費量の差を把握することは出来ません。</li>
</ul>
<p>上記のような図面では、図面ファイルサイズが小さいからと言って、必ずしも図面ファイルを開いた際に消費されるメモリ量が小さい、という結果になりません。</p>
<p>特に、64 ビット版の AutoCAD で編集して保存した上記のような図面が、32 ビット版の AutoCAD で開けない、という現象も発生する可能性もあります。そのような場面では、一度、64 ビット版の AutoCAD で開いて、表示スタイルを確認してみてください。2D ワイヤーフレーム以外で保存したファイルなら、32 ビット版の AutoCAD で開けるようになる可能性があります。</p>
<p>ここでご紹介した内容は、比較的シンプルかつ極端なものですが、もちろん、2D 図面でブロックを多用している場合にも発生するものです。特に、32ビット Windows でAutoCAD を利用されていて、「メモリ不足」エラーが多く発生するようなら、表示スタイルを<strong> ワイヤーフレーム</strong> に変更してみたり、不要な情報を<strong> PURGE コマンド</strong>で削除することをお試しいただけるはずです。PURGE コマンドについては、過去のブログ記事でも&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/08/remove-unreferenced-objects-from-dwg.html" rel="noopener" target="_blank">ご案内</a>&#0160;</strong>していますので、ご一読ください。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
