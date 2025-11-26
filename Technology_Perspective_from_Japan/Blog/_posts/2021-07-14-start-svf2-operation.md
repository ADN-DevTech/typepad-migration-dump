---
layout: "post"
title: "Model Derivative API：SVF2 の運用開始"
date: "2021-07-14 00:08:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/07/start-svf2-operation.html "
typepad_basename: "start-svf2-operation"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdedfb609200c-pi" style="display: inline;"><img alt="Svf_svf2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdedfb609200c image-full img-responsive" src="/assets/image_155391.jpg" title="Svf_svf2" /></a></p>
<p>昨年 <a href="https://adndevblog.typepad.com/technology_perspective/2020/11/svf-and-svf2.html" rel="noopener" target="_blank"><strong>Model Derivative API：SVF と SVF2</strong></a> でご案内した SVF2（Streaming Vector Format 2）が正式にリリースされました。</p>
<p>SVF2 を利用したワークフローは、従来の&#0160; SVF と同じです。Forge Viewer 設定が少し異なるのみです。詳細は <a href="https://adndevblog.typepad.com/technology_perspective/2020/11/svf-and-svf2.html" rel="noopener" target="_blank"><strong>Model Derivative API：SVF と SVF2</strong></a> でご案内のとおりです。</p>
<p style="padding-left: 40px;"><strong>SVF2 はいつから使用すべきですか？</strong></p>
<p style="padding-left: 40px;">新規の変換には、SVF2 を使用することをお勧めします。古い SVF 形式への変換を選択する強い理由は 1 つしかありません。現在、SVF2 は 3ds Max からの物理的なマテリアルを扱いません。これは現在対応検討中で、近々アップデートされる予定です。</p>
<p style="padding-left: 40px;"><strong>既存のモデルは SVF2 形式に再変換する必要がありますか？</strong></p>
<p style="padding-left: 40px;">現在のモデルのパフォーマンスに問題がある場合には、SVF2 形式に再変換して評価することをお勧めします。Revit のプロジェクト ファイルや IFC ファイルで作成された建設業向けの大規模なモデルは、一般的に再変換のメリットがあります。 ほとんどの場合、パフォーマンスが向上するはずですが、最適化はジオメトリ固有のものであり、元のデザイン ファイル形式には独自の最適化が含まれることがあるため、必ずしも大きな違いが出るとは限らない点にもご注意ください。</p>
<p style="padding-left: 40px;"><strong>SVF と SVF2 の違いは何ですか？</strong></p>
<p style="padding-left: 40px;">SVF2 形式では、個々のジオメトリ作成に代わり、インスタンス化によって情報を共有使用することで、ジオメトリを最適化しています。そのため、SVF2 形式を使用する際にはいくつか注意すべき点があります。Model Derivative API では、もともと Forge Viewer への表示時に使用していた SVF とは独立して、SVF2 形式として扱うようになります。一度、どちらかの形式に変換すると、同じシードファイル（元のデザイン ファイル）を、並行して、もう一方の形式に２度目の変換を行うことは出来ません。つまり、１度に１つの形式にしか変換することしか出来ません。Beta でマニフェストに現れていた overrideOutputType: svf2 属性は廃止されています。これは、オブジェクト識別に関する問題を最小限に抑えるためです。特に dbid は２つの形式間で異なります。ExternalId も同様です。</p>
<p style="padding-left: 40px;"><span style="background-color: #ffff00;">JavaScipt ライブラリ バージョン 7.28 以降、SVF2 を利用するアプリは、Forge Viewer の初期化オプションで指定する従来の env 値&#0160; MD20ProdUS（または MD20ProdEU）、 api 値 D3S に代わって、env 値 A<strong>utodeskProduction2</strong>（または <strong data-stringify-type="bold">AutodeskStaging2&#0160;</strong>/&#0160;<strong data-stringify-type="bold">AutodeskProduction2</strong>&#0160;）、api 値 <strong>streamingV2</strong>（または <strong>streamingV2_EU</strong>）への置き換えが推奨されています。従来値は将来廃止される予定ですので、お早めに上記置き換えをお願いします。</span></p>
<p>BIM 360 サービスから提供される以前のベータ版 OTG/Fluent ワークフローは現在非推奨となっていますのでご注意ください。BIM 360 の URN で Forge Viewer を使用するには、SVF2 を表示するための手順を踏襲してください。</p>
<p>また、SVF2 を Forge Viewer で利用するには、Forge Viewer の JavaScript ライブラリ バージョン 7.25 以降をお使いいただく必要があります。</p>
<p>By Toshiaki Isezaki</p>
