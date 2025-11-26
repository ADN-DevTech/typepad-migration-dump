---
layout: "post"
title: "Public Beta：AEC Data Model API"
date: "2023-11-22 00:02:22"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/11/aec-data-model-apis-are-now-public-beta.html "
typepad_basename: "aec-data-model-apis-are-now-public-beta"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a28aa1200b-pi" style="display: inline;"><img alt="Data Model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a28aa1200b image-full img-responsive" src="/assets/image_601440.jpg" title="Data Model" /></a></p>
<p>オートデスクは、<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/design-and-make-platforms.html" rel="noopener" target="_blank">Autodesk Data Model</a> を使用して、さまざまな業界において、より優れた Design &amp; Make（デザインと創造）データ管理プラットフォームを提供するというビジョンの実現に向けて取り組んできました。厳選された AEC 業界のお客様と独占的なプライベートベータプログラムを実施し、貴重なフィードバックを取り入れた後、建築、エンジニアリング、建設業界向けの最新のデータモデルである AEC Data Model が Public Beta になりました。</p>
<p aria-level="1" role="heading"><strong>AEC Data Model とは？</strong></p>
<p>つまり、AEC Data Model は、Revit プロジェクト（.rvt）や AutoCAD 図面（.dwg）などの一体型 AEC ファイルを、安全なクラウドで管理される貴重な粒度の高いデータに分割します。そのきめ細かなオブジェクトレベルのデータは、使いやすい API を介してアクセスできるようになります。</p>
<p>AEC Data Model を通じて、AEC データを記述するための透明で共通言語を優先し、このデータへのリアルタイムアクセスを可能にし、適切なデータが適切な人に適切なタイミングで利用できるようにするプラットフォームを提供することを目指しています。</p>
<p>AEC Data Model API を使用すると、開発者は、Civil 3D、Revit、Plant3D、その他の土木建築設計アプリケーションなどのデスクトップ オーサリング アプリケーション用のカスタムプラグインを作成することなく、単一のインターフェイスを介してクラウドベースのワークフローを通じてモデルのサブセットを読み取り、書き込み、および拡張できます。AEC データモデルの使命は、今日の成長産業におけるプロジェクトの複雑な要件の提供 をサポートするために、複数の経験と機能にまたがる反復的で分散された顧客ワークフローをサポートすることです。AEC データのアクセス性と相互運用性を高めるには、システム間でマッピングして接続できるようにデータを構築することから始まりますが、これは AEC Data Model APIが実現する重要な機能です。</p>
<p aria-level="1" role="heading"><strong>現在、AEC Data Model API で何が出来るのか？</strong></p>
<p>現在、これらの API は、パブリッシュされた Revit 2024 モデルから要素とそのプロパティ データをクエリするための読み取り専用です。今後、このデータの旅路が進むにつれて、より多くのデータと価値を解き放つことに尽力していますので、新しいデータや新機能を頻繁にチェックしてください。</p>
<p>AEC Data Model API は、AEC 業界向けに調整されたユーザーフレンドリーな GraphQL インターフェースを通じて、これらの機能を公開します。プログラムでは、次の操作を実行できます。</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>ACC （Autodesk Construction Cloud）アカウント/Hub、Project、デザインに移動して、要素、パラメータ、それらの値などの詳細なデータを取得します。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>デザインの異なるバージョンを取得し、特定の設計バージョンの要素をクエリします。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>指定した検索条件を使用して、デザイン内またはプロジェクトまたは Hub 内のデザイン全体の要素を検索します。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="4" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>デザインまたは プロジェクトで使用可能なすべてのプロパティ定義を一覧表示します。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="5" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>カテゴリ(ドア、窓、パイプなど)などのプロパティに基づいて要素をクエリします。またはパラメータ名+値(面積、体積など)。</p>
</li>
</ul>
<p>AEC Data API の第 1 弾では、次の点が可能になります。</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="6" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p>品質管理を改善するためのデザイン ファイル内の異常の特定、null（ヌル）/欠損データの特定などのワークフローの自動化。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="7" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p>数量拾い、スケジュール、調達ダッシュボード、レポートなどの生成。</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="8" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559684&quot;:-2,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p>詳細な要素(プロパティ)を視覚化して操作し、設計の異なるバージョン間の変更を比較するための Web アプリ。</p>
</li>
</ul>
<p aria-level="1" role="heading"><strong>どうすれば参加出来るか？</strong></p>
<p aria-level="1" role="heading">ご興味をお持ちいただける場合、ぜひ、<a  _istranslated="1" href="https://feedback.autodesk.com/key/AECDataModelPublicBeta" rel="noopener" target="_blank" title="Join the beta">こちら</a> から Public Beta プログラムへの参加をリクエストをお願いします。</p>
<p>この新機能に関する広範なドキュメントとコード サンプルは、<a  _istranslated="1" href="https://aps.autodesk.com/autodesk-aec-data-model-api" title="AEC Data Model">Autodesk Platform Services</a>&#0160;を通じて提供されます。また、データに対するクエリをテストし、構文とスキーマを理解するのに役立つ&#0160;&#0160;<a  _istranslated="1" href="https://aecdatamodel-explorer.autodesk.io/" rel="noopener" target="_blank">Data Exprorer</a>アプリも用意されています。</p>
<p>皆様からのフィードバックは、AEC データの未来を形作る上で重要であり、この業界のアプリ間の相互運用やチーム間でのデータ共有の方法を劇的に改善します。<a  _istranslated="1" href="https://aps.autodesk.com/aec-data-model-roadmap" rel="noopener" target="_blank">ロードマップ</a>に参加して、さらなるアップデート、新機能、拡張機能にご期待ください!</p>
<p>これらの API が独自のワークフローの改善にどのように役立つかを評価し、それらを使用して概念実証を構築し、フィードバックを提供することを嬉しく思います。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/aec-data-model-apis-are-now-public-beta" rel="noopener" target="_blank">AEC Data Model APIs are now in Public Beta! | Autodesk Platform Services</a> から転写・翻訳したものです。</p>
<p>By Toshiaki Isezaki</p>
