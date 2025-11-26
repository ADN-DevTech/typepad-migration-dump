---
layout: "post"
title: "Dynamo - Grasshopper Data Exchange コネクタ Public Beta"
date: "2023-05-22 00:15:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/05/dynamo-and-grasshopper-data-exchange-connectors-now-in-beta-autodeskcom.html "
typepad_basename: "dynamo-and-grasshopper-data-exchange-connectors-now-in-beta-autodeskcom"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75181342d200b-pi" style="display: inline;"><img alt="DynandGHConnectors" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75181342d200b image-full img-responsive" src="/assets/image_680256.jpg" title="DynandGHConnectors" /></a></p>
<p>昨年、オートデスクが米国 Autodesk University の場で粒状データを利用したプラットフォームを<a href="https://adndevblog.typepad.com/technology_perspective/2022/09/au-2022-industry-cloud.html" rel="noopener" target="_blank">発表</a>して以来、着々とその実装を進めています。その中で、最も目に見える形をとっているのが、Data Exchange API によりデータ交換をおこなうコネクタです。粒状データを利用したデータ交換の仕組みや初期に登場したコネクタについては、<a href="https://adndevblog.typepad.com/technology_perspective/2022/10/benefits-of-granular-data-on-data-exchange.html" rel="noopener" target="_blank">データ交換に見る粒状データの効果</a>&#0160;でご紹介しています。</p>
<p>さて、デザイン データにおけるコネクタというと、幾何情報（図形形状）や文字情報（属性/メタデータ）のデータ交換が思い浮かびますが、今回、少し視点を変えたコネクタが Public Beta/Tech Preview として登場しています。Autodesk Dynamo と McNeel Grasshopper との間でデータ交換をおこなうコネクタです。</p>
<p><strong>なぜ Dynamo と Grasshopper で Data Exchange なのか？</strong></p>
<p>Dynamo や Grasshopper は、俗に「コード」と呼ばれるプログラム言語を用いたプログラム作成なしに、ビジュアル プログラミングという手法で特定のタスクの自動化や複雑な計算が必要になるロジックを定義する機能を提供します。デザイナーは、Dynamo や Grasshopper のスクリプトを使用して、高度な複雑さと反復性を持つジオメトリをモデリングし、時間と労力の面だけでなく、創造性を最大限に引き出すために、手作業を超えるレベルの自動化にアクセスすることが出来るようになります。</p>
<p>オートデスクは、小規模な設計事務所が大規模な多国籍企業並みの技術的・業務的パフォーマンスを達成し、業務全体とプロジェクト チームの両方で業務効率を最適化するためにコンピューティングを活用しているのを目の当たりにしています。</p>
<p><strong>コネクターは何をするか？</strong></p>
<p>Dynamo と Grasshopper の両コネクタを使用すると、他のコネクタと同様に、より簡単にデザイン データを管理、共有、受信し、ポイントツーポイントの相互運用性を超えてワークフローを拡張することができます。しかし、Dynamo と Grasshopper は、幾何情報や文字情報のマッピングを使用して、Revit や Rhino のネイティブ要素の作成をおこなうことが出来るため、さらに高度なデータ共有ワークフローに独自のメリットをもたらすことができます。ロジックの共有が新たなインスピレーションの機会になるのです。</p>
<p><strong>Dynamo - Grasshopper コネクンPublic Beta を試してみる</strong></p>
<p>既存または新規のスクリプトの中で両方のコネクタを使用します。Dynamo と Grasshoppe の強力なフィルタリング機能を活用しながら、送信と受信の交換ノードを利用して、デザインモデルのサブセットを共有します。他のコネクタと同様に、Data Exchange は Autodesk Docs に保存され、共同作業者がそれを閲覧し、アクセス許可を設定し、既存またはカスタムビルドのコネクタを使用してデータを別のアプリに取り込むことができます。両コネクタの主な機能の詳細については、デモ ビデオをご覧ください。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/Xe7__qvUv-o" width="480"></iframe></p>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2023/05/autodesk-app-store-and-aps.html" rel="noopener" target="_blank">Autodesk App Store と APS</a>&#0160;でも触れているとおり、は、<a href="https://apps.autodesk.com/All/ja/List/Search?isAppSearch=True&amp;searchboxstore=All&amp;facet=&amp;collection=&amp;sort=&amp;query=+Data+Exchange+connector+" rel="noopener" target="_blank">Autodesk App Store</a> に記載されています。</p>
<p>By Toshiaki Isezaki<a href="https://blogs.autodesk.com/revit/2023/03/01/data-exchange-connectors-for-dynamo-and-grasshopper-now-available-in-public-beta/"></a></p>
