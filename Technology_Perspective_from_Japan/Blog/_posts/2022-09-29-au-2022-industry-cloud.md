---
layout: "post"
title: "AU 2022：Industry Cloud - インダストリー クラウド"
date: "2022-09-29 00:03:09"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/09/au-2022-industry-cloud.html "
typepad_basename: "au-2022-industry-cloud"
typepad_status: "Publish"
---

<p>Autodesk University 2022 会期 2 日めの <a href="https://events-platform.autodesk.com/event/au-2022-digital-experience/planning/UGxhbm5pbmdfOTg5NDQx" rel="noopener" target="_blank">General Session Day 2</a> で、<strong>インダストリー クラウド</strong>が何を実現するのか、いくつかのデモ動画（含む、既に導入されている機能）を用いて、その方向性が示されました。</p>
<p>最初に、<a href="https://adsknews.autodesk.com/pressrelease/au22-digital-transformation" rel="noopener" target="_blank">ニュースリリース（英語）</a>ではインダストリークラウドを次のように要約しています。</p>
<p style="padding-left: 40px;"><strong>Autodesk Forma</strong> は、建築、エンジニアリング、建設、運用向けのインダストリー クラウドです。ビルディング インフォメーション モデリング（BIM）ワークフローを、建築環境の設計、建設、運用のチーム間で統一し、プロジェクトの段階、人材、アセットの種類を問わず、全プロセスにわたってシームレスなデータ フローを可能にします。</p>
<p style="padding-left: 40px;"><strong>Autodesk Fusion</strong> は製造向けのインダストリー クラウドです。データと人をつなぎ、経営層から製造現場まで、製品開発のライフサイクルを通して、組織とエコシステム全体に次世代ワークフローを提供します。</p>
<p style="padding-left: 40px;"><strong>Autodesk Flow</strong> は、メディアおよびエンターテインメント向けのインダストリー クラウドです。初期のコンセプトから最終納品までの制作ライフサイクル全体にわたって、ワークフロー、データ、チームをつなぎます。</p>
<p>抽象的な表現で分かりにくいかもしれませんが、インダストリー クラウドのベースになっている Autodesk Platform Services（旧名 Autodesk Forge）の視点で見てみると、少し把握し易くなるはずです。</p>
<p style="padding-left: 40px;"><strong>Autodesk Platform Services</strong><strong>（</strong><strong>APS</strong><strong>）</strong>（旧名 Autodesk Forge、または Forge ）は、ソリューションのカスタマイズ、革新的なワークフローの作成、他のツールやデータとオートデスクのプラットフォームの統合を支援する API およびサービスを提供します。既製のソリューションを集めたアプリ マーケットプレイスは、分断された状態を迅速につなげるのに役立ち、クラウド情報モデルは、チームがプロジェクト データを作成して共有する方法を効率化します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a308dfa20e200c-pi" style="display: inline;"><img alt="Aps_industry_cloud" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a308dfa20e200c image-full img-responsive" src="/assets/image_544888.jpg" title="Aps_industry_cloud" /></a></p>
<p>General Session Day 2 のデモ動画では、デザイン データ全体ではなく、特定のオブジェクトがコラボレーションやコミュニケーションに利用されている点に注目してみてください。従来であれば、デザイン データを持つ「ファイル」を使われていたはずです。</p>
<p>ここで言うオブジェクトとは、幾何情報（図形）だけでなく、メタデータ（商品コードは価格、ライフサイクル データなどの文字情報または属性、プロパティ）をも包含しています。</p>
<p>つまり、インダストリー クラウドが実現するのは、従前、ファイル単位で扱われていた「データ」を分解して、内包する個々の情報を粒状化して、本当に必要なデータのみを抽出・取得したり、書き込んだりする能力を持つクラウド基盤ということです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02a2eed56eb9200d-pi" style="display: inline;"><img alt="Industry_cloud" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02a2eed56eb9200d image-full img-responsive" src="/assets/image_919634.jpg" title="Industry_cloud" /></a></p>
<p>このような能力によって、例えば、デザイン データ ファイルを直接開いて参照出来ない ERP や CRM システムなどにも、必要なメタデータのみを抽出して渡すことが出来るようになるわけです。</p>
<p>もちろん、そういったカスタム ソリューションを実現するには、Autodesk Platform Services（旧名 Autodesk Forge）が提供する API を利用する必要があります。逆に、インダストリークラウドは、そのような拡張性も併せ持つということになります。</p>
<p>By Toshiaki Isezaki</p>
