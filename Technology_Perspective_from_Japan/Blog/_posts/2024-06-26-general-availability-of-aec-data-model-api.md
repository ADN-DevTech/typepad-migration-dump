---
layout: "post"
title: "AEC Data Model API 一般提供開始（アメリカ、欧州データセンター）"
date: "2024-06-26 00:02:12"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/06/general-availability-of-aec-data-model-api.html "
typepad_basename: "general-availability-of-aec-data-model-api"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c7bd99200d-pi" style="display: inline;"><img alt="Screenshot 2024-06-19 at 4.55.36 PM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c7bd99200d image-full img-responsive" src="/assets/image_20703.jpg" title="Screenshot 2024-06-19 at 4.55.36 PM" /></a></p>
<p>ベータ段階でお客様からの貴重なフィードバックを統合し、AEC Data Model API の正式な、そして、最初のリリースを発表できることを嬉しく思います。本番環境で AEC Data Model API を使用してアプリを構築および展開できるようになりました。</p>
<p>リリース時、AEC Data Model API は <strong>AMER</strong>（US）<strong> および EMEA リージョン</strong>で利用出来るようになります。<strong><span style="background-color: #ffff00;">あいにく、アジアのデータセンターは、今回の一般公開の対象に含まれていません。</span></strong></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b3f3b9200c-pi" style="display: inline;"><img alt="Supported_regions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b3f3b9200c image-full img-responsive" src="/assets/image_427346.jpg" title="Supported_regions" /></a></p>
<p>Autodesk Docs にアクセスできるすべてのアカウントは、ACC アカウントがAEC Data Mode への書き込みを開始するように設定することができます。ACC Account admin（アカウント管理者）の方は、アカウント管理者のアカウント設定セクションからAEC Data Model をアクティブ化できます。アカウント内で AEC データ モデルがアクティブ化されると、アカウント内の Revit 2024 以降のモデルは、アップロードまたはパブリッシュ時に AEC Data Model API を介して使用出来るようになります。</p>
<h3 aria-level="1" role="heading"><strong>AEC Data Model とは？</strong></h3>
<p>つまり、AEC Data Model は、Revit プロジェクト（.rvt）や AutoCAD 図面（.dwg）など、さまざまなデータを含む単一ファイルを、安全なクラウドで管理される有用な粒状データに分割・利用する一連の機能を提供します。そのきめ細かでオブジェクト レベルのデータは、利用しやすい API を介してアクセスするとエクスペリエンスも提供します。</p>
<p>AEC Data Model&#0160; を通じて、AEC データを記述する透明性の高い共通言語を用いて、データへのリアルタイムアクセスを可能にするだけでなく、適切なデータを適切な人が適切なタイミングで利用できるようにするプラットフォームを提供することを目指しています。</p>
<p>今後も AEC Data Mode API を成長させる意向なので、開発者は、Civil 3D、Revit、Plant3D、その他の AEC 設計ソフトウェアなど、個々のデスクトップ オーサリング ソフトウェア用カスタム アドイン/プラグインを作成することなく、単一のインタフェースを介してクラウドベースのワークフローに沿って、モデルのサブセットのを読み取り、書き込み、および拡張をおこなうことが出来るようになります。AEC Data Model のミッションは、複数のエクスペリエンスと機能にまたがる反復的で分散したワークフローをサポートし、今日の成長産業におけるプロジェクトの複雑な要件の提供をサポートすることです。AEC データのアクセス性と相互運用性を高めるには、システム間でマッピングして接続できるようにデータを構築することから始まりますが、これは AEC Data Model API が実現する重要な機能です。</p>
<h3 aria-level="1" role="heading">現在、AEC Data Model API で何が可能か？</h3>
<p>最初の API は、パブリッシュされた Revit 2024 以降のバージョン モデルから要素とそのプロパティ データをクエリーするための読み取り機能を提供します。今後、ファイルに閉じ込められていたより多くのデータを解放してと価値を高めていく予定です。<strong><a  _istranslated="1" href="https://aps.autodesk.com/aec-data-model-roadmap" rel="noreferrer noopener" target="_blank">公開ロードマップ</a></strong>で新しいデータや新機能を頻繁に確認してください。</p>
<p>AEC Data Model API は、AEC 業界向けに調整されたユーザーフレンドリーな GraphQL&#0160; インターフェースを通じて、これらの機能を公開します。プログラムでは、次の操作を実行できます。</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="1" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>ACC (Autodesk Construction Cloud) アカウント/ハブ、プロジェクト、デザインに移動して、要素、パラメータ、それらの値などの詳細なデータの取得</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="2" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>デザインの異なるバージョンを取得し、特定のバージョンの要素をクエリー</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="3" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>指定した検索条件を使用して、デザイン内またはプロジェクトまたはハブ内のデザイン全体の要素の検索</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="4" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>設計またはプロジェクトで使用可能なすべてのプロパティ定義の一覧表示</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="5" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>カテゴリ(ドア、窓、パイプなど)などのプロパティに基づいて要素をクエリー、またはパラメータ名＋値（面積、体積など）のクエリー</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="6" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="2">
<p>デザイン内のパラメータの一意の値をクエリー</p>
</li>
</ul>
<p>AEC Data API の最初のリリースでは、次のことが可能になります。</p>
<ul role="list">
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="7" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p>品質管理を改善するためのデザイン ファイル内の異常の特定、null/欠損データの特定などのワークフローの自動化</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="8" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p>数量拾い、スケジュール、調達ダッシュボード、レポートなどの生成</p>
</li>
<li aria-setsize="-1" data-aria-level="1" data-aria-posinset="9" data-font="Symbol" data-leveltext="" data-list-defn-props="{&quot;335552541&quot;:1,&quot;335559685&quot;:720,&quot;335559991&quot;:360,&quot;469769226&quot;:&quot;Symbol&quot;,&quot;469769242&quot;:[8226],&quot;469777803&quot;:&quot;left&quot;,&quot;469777804&quot;:&quot;&quot;,&quot;469777815&quot;:&quot;hybridMultilevel&quot;}" data-listid="1">
<p>Web アプリで、細かな要素（プロパティ）を視覚化し、デザインの異なるバージョン間の変更を比較</p>
</li>
</ul>
<h3 aria-level="1" role="heading">どのように利用するか？</h3>
<p>ここまでの内容に興味をお持ちの場合は、Autodesk Construction Cloud アカウント管理者にコンタクトして、<a href="https://aps.autodesk.com/en/docs/aecdatamodel/v1/developers_guide/onboarding/" rel="noopener" target="_blank">ドキュメント</a>に記載されている手順に従って AEC Data Model のアカウントを有効にしてください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b838eb200b-pi" style="display: inline;"><img alt="Activate_button" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b838eb200b image-full img-responsive" src="/assets/image_26240.jpg" title="Activate_button" /></a></p>
<p>この新機能に関する包括的なドキュメントとコードサンプルを APS を通じて共有します。また、データに対するクエリーをテストし、構文とスキーマを学習するのに役立つデータ エクスプローラー リンク アプリンも提供しています。皆様からのフィードバックは、AEC Data の将来を導く上で不可欠であり、この業界のアプリ間の相互運用やチーム間でのデータ共有の方法を大幅に強化することになります。</p>
<p>また、<a  _istranslated="1" href="https://aps.autodesk.com/aec-data-model-roadmap" rel="noopener noreferrer" target="_blank">公開ロードマップ</a> ページで更なるアップデート、新機能、改善された機能にご注目ください！</p>
<p>AEC Data Model API に関するサポートについては、<a  _istranslated="1" href="https://aps.autodesk.com/get-help" rel="noopener" target="_blank">Get Help</a> ページからお問い合わせいただけます。これらの API が独自ワークフローの改善にどのように役立つかを評価、アプリケーションを構築し、フィードバックをご提供ください。興味深いワークフローを共有したり、サクセスストーリーで紹介したりしたい場合は、こちらの&#0160;<a  _istranslated="1" href="https://aps.autodesk.com/share-your-solution" rel="noopener" target="_blank">フォーム</a> からソリューションのアイデアをお送りいただくことも出来ます。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/general-availability-aec-data-model-api-here" rel="noopener" target="_blank">General Availability of AEC Data Model API is Here! | Autodesk Platform Services</a> から転写・意訳、補足を加えたものです。</p>
<p>By Toshiaki Isezaki</p>
