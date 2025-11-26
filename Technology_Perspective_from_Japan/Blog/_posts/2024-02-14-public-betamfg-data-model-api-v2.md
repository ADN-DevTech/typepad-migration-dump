---
layout: "post"
title: "Public Beta：Manufacturing(MFG) Data Model API v2"
date: "2024-02-14 00:16:01"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/01/public-betamfg-data-model-api-v2.html "
typepad_basename: "public-betamfg-data-model-api-v2"
typepad_status: "Publish"
---

<div class="field field--name-field-primary-image field--type-image field--label-hidden field__item"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a5daf6200c-pi" style="display: inline;"><img alt="Image_rev2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a5daf6200c image-full img-responsive" src="/assets/image_186108.jpg" title="Image_rev2" /></a></div>
<div class="clearfix text-formatted field field--name-field-body field--type-text-with-summary field--label-hidden field__item">
<p>オートデスクでは、製品ライフサイクル全体にわたってデータとワークフローを統合するために、大胆な投資をおこなっています。この取り組みは、製品ライフサイクルのワークフローを再考することで、DX（デジタルトランスフォーメーション）を加速させます。その結果、情報へのアクセスが合理化され、分野や場所に関係なく、組織全体でシームレスなデータ接続が促進されます。</p>
<p>この度、すでにリリースされていた Manufacturing Data Model APIs v1 に続いて、Manufacturing Data Model APIs v2 の Public Beta としてリリースされしました。これは、Manufacturing Data Model APIs v1 の初期 API からの大幅なアップグレードとなります。Manufacturing Data Model は、製造業用のデザインデータをクラウドに保存し、どこからでも簡単にアクセスできるようにする方法です。データを詳細なサブセットに分割し、Manufacturing Data Model API を使用して読み取り、書き込み、拡張できるようにしています。</p>
<p>ここでは、v1 から v2 への移行に伴い、主な機能強化、改善点、また、Manufacturing Data Model API の将来をについて説明します。</p>
<h3><strong>API v2 の新機能</strong></h3>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p><strong>Unified Autodesk Data Model の導入</strong></p>
</li>
</ul>
<p style="padding-left: 40px;">オートデスクでは、Manufacturing(MFG) Data Model、AEC Data Model、M&amp;E Data Model の&#0160; 3 つのインダストリークラウドに準じたデータ モデルを進化させています。これらデータモデルは GraphQL API を通じて公開されており、各データ モデルのデータを詳細なレベルでクエリーすることが可能です。データ モデルが成熟するにつれて、3つのインダストリー（業界）でユーザーオブジェクト、サムネイル、プロパティタイプ、ユニットなどの概念にまたがる共通のデータ概念、オブジェクト、パターンを作成しています。共通オブジェクトを 1 つのデータグラフにまとめるのは自然なことです。3 つの異なるデータモデルを統合し、単一の GraphQL API を介してアクセスできるようにすることで、複雑になりがちな複数のインダストリーにまたがるデータの横断的なアクセスを容易にし、この新しい統合データエクスペリエンスを通じてコンバージェンス（収束）とイノベーションを促進します。</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p><strong>カスタムプロパティによる Manufacturing Data Model の拡張</strong></p>
</li>
</ul>
<p style="padding-left: 40px;">メタデータ、プロセス情報、および外部システム由来の ID をカスタム プロパティとして追加し、それらのプロパティを変更し、値をクエリーできるようになりました。カスタム プロパティは、さまざまな Autodesk Fusion エクスペリエンスでもネイティブに表示されます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a5dafc200c-pi" style="display: inline;"><img alt="Custom_Properties" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a5dafc200c image-full img-responsive" src="/assets/image_277146.jpg" title="Custom_Properties" /></a></p>
<ul role="list">
<li>プロパティ定義をグローバルに作成/管理し、複数の Hub で使用します。</li>
</ul>
<ul role="list">
<li>ユースケースに基づいてプロパティの動作を設定します。さまざまなプロパティの動作がサポートされており、特定のコンポーネント/図面バージョンに値をアタッチしたり、プロパティの変更時にコンポーネント/図面バージョンを更新が可能になります。</li>
</ul>
<ul role="list">
<li>Autodesk Fusion コンポーネントのプロパティ ビューやその他のナビゲーション エクスペリエンスでカスタム プロパティを使用します。</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p><strong>サムネイルへのアクセスの簡素化</strong></p>
</li>
</ul>
<p style="padding-left: 40px;">署名付き URL（Signed URL）を使用して、Autodesk Fusion コンポーネント/図面のサムネイルに単一サイズでアクセス出来るようになりました。この改良で、アプリケーションでサムネイル画像を表示する手順の数が減ります。</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="4" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p><strong>Autodesk Fusion コンポーネント ジオメトリの他のファイル形式（OBJ や STL など）への書き出し</strong></p>
</li>
</ul>
<p lang="EN-US" style="padding-left: 40px;" xml:lang="EN-US">OBJ または STL ファイル形式でエクスポートしたコンポーネントを、視覚化や 3D プリントなどの用途に利用できるようになりました。</p>
<h3 lang="EN-US" xml:lang="EN-US">v1 から v2 へのシームレスな移行</h3>
<p>あるバージョンから別のバージョンへの移行は、重要なプロセスになる可能性があることを理解しています。そのため、移行をスムーズにして中断を最小限に抑えるため、<a  _istranslated="1" href="https://aps.autodesk.com/en/docs/mfgdatamodel-publicbeta/v2/developers_guide/migration/" rel="noopener" target="_blank" title="Migration Guide">Migration Guide（移行ガイド）</a>を設計しました。このセクションでは、API v1 から v2 への切り替え方法に関する詳細なガイドラインを示します。</p>
<h3>API バージョン 2 の使用開始</h3>
<p>API v2 の<a href="https://aps.autodesk.com/en/docs/mfgdatamodel-publicbeta/v2/developers_guide/overview/" rel="noopener" target="_blank">ドキュメント</a>には、開発を始めるための <a href="https://aps.autodesk.com/en/docs/mfgdatamodel-publicbeta/v2/tutorials/before_you_begin/" rel="noopener" target="_blank">Step-by-step Tutorials</a>、<a href="https://aps.autodesk.com/en/docs/mfgdatamodel-publicbeta/v2/code-samples/sample1/" rel="noopener" target="_blank">Code Samples</a>、詳細な <a href="https://aps.autodesk.com/en/docs/mfgdatamodel-publicbeta/v2/reference/quick-reference/" rel="noopener" target="_blank">API Reference</a> ドキュメントが用意されています。Manufacturing Data Model APIs v2 のパワーをいち早く体験してみてください。</p>
<h3 lang="EN-US" xml:lang="EN-US"><strong>Manufacturing Data Model</strong> API でできること</h3>
<p lang="EN-US" xml:lang="EN-US">Manufacturing Data Model API は、ワークフローに革命をもたらし、生産性を向上させるのに役立ちます。現在実現出来る内容を例としていくつかご紹介しましょう。</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="5" data-font="Calibri" data-leveltext="-" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559683&quot;:0,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Calibri&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p lang="EN-US" xml:lang="EN-US"><strong>反復的なタスクを自動化する：</strong>部品表作成などのタスクを自動化することで、プロセスを合理化します。設計と同期させ、サプライヤーの詳細やコストなどのカスタムプロパティでデータを充実させます。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="5" data-font="Calibri" data-leveltext="-" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559683&quot;:0,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Calibri&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p lang="EN-US" xml:lang="EN-US"><strong>環境影響分析：</strong>設計段階を通じて、製品の環境への影響を総合的に理解します。材料、質量、体積、密度などの詳細なデータを活用して、二酸化炭素排出量や水使用量を分析します。この貴重な情報をカスタムプロパティとしてデータモデルに書き戻し、継続的なトレーサビリティを実現します。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="7" data-font="Calibri" data-leveltext="-" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559683&quot;:0,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Calibri&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p lang="EN-US" xml:lang="EN-US"><strong>ERP システムとのシームレスな統合：</strong>製造設計とデータを ERP システムに統合することで、コスト管理の効率を高めます。調達、調達、価格設定、在庫の決定を簡単に自動化します。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="8" data-font="Calibri" data-leveltext="-" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559683&quot;:0,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Calibri&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p lang="EN-US" xml:lang="EN-US"><strong>柔軟なコンポーネントのエクスポート：</strong>Autodesk Fusion コンポーネントを STL や OBJ などのさまざまな形式で書き出し、視覚化 3D プリントなどのアプリケーションに使用できます。オーサリング アプリケーションを超えてデザインの可能性を解き放ちます。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="9" data-font="Calibri" data-leveltext="-" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559683&quot;:0,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Calibri&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;-&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p lang="EN-US" xml:lang="EN-US"><strong>簡単なプロジェクト管理：</strong>API を利用して、ユーザーの Hub 内での新しい Project の作成を自動化したり、Project や Hub にユーザーを追加したりして、プロジェクトデータ管理を合理化します。</p>
</li>
</ul>
<p lang="EN-US" xml:lang="EN-US">今回の Public Beta フェーズは単なるリリースではありません。これは、API の進化の一部となるための招待状でもあります。皆さんからのフィードバックは、API を改良し、完成させる上で非常に貴重です。<a  _istranslated="1"  _mstmutation="1" href="https://feedback.autodesk.com/key/MFGDataModelPublicBeta">ベータ</a>テストコミュニティに参加して、経験を共有、問題を報告いただき、API を微調整して多様なニーズを確実に満たすようにしていきましょう。<strong>&#0160;&#0160;</strong></p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/manufacturing-data-model-api-version-20-now-public-beta" rel="noopener" target="_blank">Manufacturing Data Model API Version 2.0 is now in public beta! | Autodesk Platform Services</a>&#0160;から転写・翻訳したものです。</p>
<p>By Toshiaki Isezaki</p>
</div>
