---
layout: "post"
title: "Design & Make（デザインと創造）プラットフォーム"
date: "2023-06-07 00:05:26"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/06/design-and-make-platforms.html "
typepad_basename: "design-and-make-platforms"
typepad_status: "Publish"
---

<p>オートデスクは、<a href="https://adndevblog.typepad.com/technology_perspective/2022/09/reason-why-industry-cloud.html" rel="noopener" target="_blank">プラットフォーム</a>を構築しようとしています。言うまでもなく、従来からの「ファイル」によって扱われていた「データ」を解放するのがゴールの 1 つです。</p>
<p>ファイルはデータを作成したソフトウェアのみが理解できるファイル形式に拘束されてしまい、異なるファイル形式を持つソフトウェア間、異なるソフトウェアを利用するチーム間、また、異なるチームが持つワークフロー間で円滑なコミュニケーションやコラボレーション（共同作業）を阻害する一因になっています。</p>
<p>そこで、ファイル形式の壁を取り除くため、クラウド利用をより深化させる手法の提供を選択したわけです。ファイルに含まれるさまざまなデータをファイルから取り出し、「粒状化」して扱えるようにすることで、ファイル単位ではなく、本当に必要な個々のデータ単位で情報を読み込んだり、書き込んだりする環境の提供です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7518244ad200b-pi" style="display: inline;"><img alt="Granular_data" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7518244ad200b image-full img-responsive" src="/assets/image_293503.jpg" title="Granular_data" /></a></p>
<p>「粒状データ」を扱う環境は、特定のソフトウェアやクラウド サービスのみが採用しても意味を為しません。複数の異なるソフトウェアやソクラウド サービスが採用することで、はじめてを同じ手法で「粒状データ」を扱えるようになり、ファイル形式の壁を超えることが可能になります。この環境こそが、オートデスクが目指す <strong>Design &amp; Make（ デザインと創造 ）プラットフォーム</strong> です。</p>
<p>そして、Design &amp; Make プラットフォーム の構築を API として支えるのが、Autodesk Platform Services（旧 Forge）ということになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a6a4cc200c-pi" style="display: inline;"><img alt="Autodesk_platform" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751a6a4cc200c image-full img-responsive" src="/assets/image_716065.jpg" title="Autodesk_platform" /></a></p>
<p>少しわかりにくいのですが、Autodesk Platform Services がプラットフォームを実現するため、内部で 機能別にカテゴリ分けがされています。インダストリー クラウドのデータ モデルを担う <strong>Autodesk Data Model</strong>（旧名 Cloud Information Model）、他製品と粒状データを利用したデータ交換を担う <strong>Autodesk Data Exchange</strong>、オートデスク製品による一貫したデータアクセスを担う <strong>Autodesk Data Access</strong>&#0160;の 3 つです。</p>
<p><a id="data_model"></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751825b3e200b-pi" style="display: inline;"><img alt="Categories" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751825b3e200b image-full img-responsive" src="/assets/image_182919.jpg" title="Categories" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751a6a543200c-pi" style="display: inline;"></a>Autodesk Data Model は、インダストリー クラウドのデータモデルとなる 建設業向けの AEC Data Model（旧名 AEC Cloud Information Model（AIM））、製造業向けのMFG Data Model（旧名 Product Information Model（PIM））、マルチメディア・エンターテイメント行向けの M&amp;E Data Model（旧名 M&amp;E Cloud Information Model（MIM））の総称と考えることが出来ます。</p>
<p>そして、Design &amp; Make - デザインと創造 プラットフォーム全般が利用するテクノロジが、<a href="https://adndevblog.typepad.com/technology_perspective/2022/12/graph-database.html" rel="noopener" target="_blank">グラフ データベース</a>や <a href="https://adndevblog.typepad.com/technology_perspective/2023/06/graphql.html" rel="noopener" target="_blank">GraphQL</a> といった新機軸のということになります。</p>
<p>さて、これら各カテゴリで扱うデータが、業界を跨いでデータを適切に、かつ、一貫した方法り扱えるようにする必要があります。共通したスキーマ定義や API、デスクトップ製品との協調利用など、乗越えるべきハードルこそ残されてはいますが、方向性は明確なので、あとは着実に実装をしていくのみです。すべてが稼働した暁には、正しいデータを（必要とするデータのみを）、正しいタイミングで、正しい人々が利用出来るようになるはずです。</p>
<p>By Toshiaki Isezaki</p>
