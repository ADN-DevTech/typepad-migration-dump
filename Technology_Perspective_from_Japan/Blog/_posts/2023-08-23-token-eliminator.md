---
layout: "post"
title: "トークン予測ツール"
date: "2023-08-23 00:28:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/08/token_eliminator.html "
typepad_basename: "token_eliminator"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751af4c71200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25daa1d200d-pi" style="display: inline;"><img alt="2023-08-23_16-39-05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25daa1d200d image-full img-responsive" src="/assets/image_982616.jpg" title="2023-08-23_16-39-05" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751af4c71200c-pi" style="display: inline;"><br /></a></p>
<p>Autodesk Platform Services（APS）のほとんどの API は無料で使用できますが、プレミアム API と呼ばれる一部の API（Model Derivative API、Design Automation API、Reality Capture API）が <a href="https://adndevblog.typepad.com/technology_perspective/2022/11/flex-token-adoption-into-aps-on-11-7.html" rel="noopener" target="_blank">Autodesk Flex</a> による従量課金の対象になっています。API 毎の消費トークンは、<a href="https://adndevblog.typepad.com/technology_perspective/2022/11/updated-usage-analytics-page-for-flex.html" rel="noopener" target="_blank">Usage Analytics ページ：Flex トークン残高と消費量の確認</a>&#0160;でご案内した Usage Analytics で把握することが出来るようになっています。</p>
<p>ただし、消費トークンは実際に API を使用した後でないと Usage Analytics ページには表示されないため、API の使用前に消費トークン量を理解することが難しい状態でした。今回、API の使用前に消費トークンを推測する <strong>Token Estimator</strong> -<strong>トークン予測ツール</strong>（いまのところ英語）が用意されましたので、ここでご紹介しておきたいと思います。</p>
<p>トークン推測ツールへは、<a href="https://aps.autodesk.com/ja/pricing" rel="noopener" target="_blank">価格ページ</a>中段の [トークン予測] ボタンらアクセスすることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751af4c67200c-pi" style="display: inline;"><img alt="Estimator_button" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751af4c67200c image-full img-responsive" src="/assets/image_933934.jpg" title="Estimator_button" /></a></p>
<p>トークン予測ツールは、実際の運用を想定した情報を入力して、どれくらいのトークンを購入すべきか判断する意思決定を支援するツールで、プレミアム API に必要な 1 か月間の Flex トークン数を推測、表示します。API 機能の詳細を理解していなくても、このツールを使用して、必要なおおまかなトークン数を簡単に確認できます。</p>
<p>トークン予測ツールには 3 つのアセクションがあり、プレミアム API について、それぞれ消費トークンを算出します。</p>
<ul>
<li>「<strong>I need to view 2D or 3D models or extract metadata</strong>（2D または 3D モデルを表示したり、メタデータを抽出する必要がある）」をクリックすると <strong>Model Derivative API</strong></li>
<li>「<strong>I need to create or modify models（</strong>モデルを作成または修正する必要がある）」をクリックすると <strong>Design Automation API</strong></li>
<li>「<strong>I need to generate mesh from photos/pictures</strong>」をクリックすると <strong>Reality Capture API </strong></li>
</ul>
<p>各 API 別にトークン消費量が推測出来れば、ボリューム ディスカウントなどを活用して購入する Flex トークン数を調整することが出来て有用です。</p>
<hr />
<p><strong>Model Derivative API の推測</strong></p>
<p style="padding-left: 40px;">2D 図面/シートや 3Dモデルを表示したりメタデータを抽出したりする場合、Model Derivative API を使用した変換処理が必要になります。Model Derivative API は、<strong>コンプレックス ジョブ</strong>毎（Revit、IFC、Navisworks ファイルの変換毎）に 0.5 トークン、<strong>シンプル ジョブ</strong>毎（Revit、IFC、Navisworks ファイル以外の変換毎）に 0.1ト ークンが消費されます。</p>
<p style="padding-left: 40px;">ただし、モデルの場所が BIM 360 Docs（BIM 360）や Fusion 360 のオートデスクのクラウド サービス ストレージに保存されている場合、クラウド サービス自体の機能で表示用の変換処理が実行されるため、APS アプリ側の明示的な変化処理が不要なため、トークン消費は発生しません。</p>
<p style="padding-left: 40px;"><strong>Model location</strong> では、変換対象のデザイン ファイルがどこに保存されているか、<strong>Model type</strong> でデザイン ファイルの種類、<strong>Number of models</strong> で処理するデザイン ファイルの数、<strong>Updates</strong> で１か月あたりの変換処理数をそれぞれ指定します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751af4b18200c-pi" style="display: inline;"><img alt="Md" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751af4b18200c image-full img-responsive" src="/assets/image_696416.jpg" title="Md" /></a></p>
<hr />
<p><strong>Design Automation API の推測</strong></p>
<p style="padding-left: 40px;">デザイン ファイルを作成または変更する必要がある場合は、Design Automation API を使用します。消費トークンは、クラウド上で実行されるスクリプト（アドイン/プラグイン）の処理時間 １ 時間毎に 2.0 トークンが使用されます。処理に使用する&#0160; 3 つの <strong>Engine</strong>（AutoCAD、Revit、Inventor）のうちの 1 つを選択してください。あいにく、トークン予測ツールは、3ds Maxの消費トークン推測には対応していません。</p>
<p style="padding-left: 40px;">各エンジン使用時の推測では、エンジン毎に既定モデル サイズと指定出来る増減間隔が設定されています。<strong>Model size</strong> で指定可能なエンジン毎の指定可能サイズの範囲と増減間隔は次のとおりです。</p>
<ul>
<li><strong>AutoCAD</strong> のサイズは KB 単位で、最大 10MBまで 100KB 間隔で増加します。</li>
<li><strong>Revit</strong> のサイズは MB 単位で、最大 2.5GB まで 10MB間隔で増加します。</li>
<li><strong>Inventor</strong> のサイズは KB 単位で、最大 100MB まで 200KB 間隔で間隔で増加します。</li>
</ul>
<p style="padding-left: 40px;">トークン予測には、<strong>Model complexity</strong> で指定する次のモデルの複雑さの情報も必要になります。</p>
<ul>
<li><strong>Super Simple</strong>（とても単純）：最小</li>
<li><strong>Simple</strong>（単純）：中央値</li>
<li><strong>Avrage</strong>（平均）</li>
<li><strong>Complex</strong>（複雑）：上位 5 パーセント</li>
<li><strong>Super complex</strong>（超複雑）：最大</li>
</ul>
<p style="padding-left: 40px;">よくわからない場合、あるいは判断が難しい場合には、<strong>Simple</strong>&#0160;を選択してみてください。</p>
<p style="padding-left: 40px;"><strong>Script/code execution</strong> では、１か月あたりの処理数を指定します。</p>
<div class="flex-1 overflow-hidden">
<div class="react-scroll-to-bottom--css-wfqwl-79elbk h-full dark:bg-gray-800">
<div class="react-scroll-to-bottom--css-wfqwl-1n7m0yu">
<div class="flex flex-col text-sm dark:bg-gray-800">
<div class="group w-full text-token-text-primary border-b border-black/10 dark:border-gray-900/50 bg-gray-50 dark:bg-[#444654]">
<div class="flex p-4 gap-4 text-base md:gap-6 md:max-w-2xl lg:max-w-[38rem] xl:max-w-3xl md:py-6 lg:px-0 m-auto">
<div class="relative flex w-[calc(100%-50px)] flex-col gap-1 md:gap-3 lg:w-[calc(100%-115px)]">
<div class="flex flex-grow flex-col gap-3">
<div class="min-h-[20px] flex flex-col items-start gap-3 overflow-x-auto whitespace-pre-wrap break-words">
<div class="markdown prose w-full break-words dark:prose-invert light">
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751af4b02200c-pi" style="display: inline;"><img alt="Da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751af4b02200c image-full img-responsive" src="/assets/image_556024.jpg" title="Da" /></a></p>
<ul style="list-style-type: circle;">
<li>Design Automation API の消費算出は少し複雑です。詳しい考え方は、<a href="https://adndevblog.typepad.com/technology_perspective/2020/02/estimate-design-automation-costs.html" rel="noopener" target="_blank">Design Automation API の課金とコスト算出について</a> の記事を確認してみてください。</li>
</ul>
<hr />
<p><strong>Reality Capture API の推測</strong></p>
<p style="padding-left: 40px;">写真や画像からメッシュや点群を生成する必要がある場合は、<a href="https://ja.wikipedia.org/wiki/%E5%86%99%E7%9C%9F%E6%B8%AC%E9%87%8F%E6%B3%95" rel="noopener" target="_blank">フォトグラメトリー</a>と呼ばれる手法を用いる Reality Capture API を使用します。トークン予測はシンプルです。<strong>Number</strong> で生成に使用する写真/画像の数と、<strong>Updates</strong> で１か月あたりの処理回数を選択するだけです。使用する 50 枚の写真/画像毎に 1.0 トークンを消費します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751af4b2a200c-pi" style="display: inline;"><img alt="Rc" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751af4b2a200c image-full img-responsive" src="/assets/image_251783.jpg" title="Rc" /></a></p>
<hr />
<p>各 API の予測トークン数は、<strong><span style="background-color: #4040ff; color: #ffffff;">&#0160; &#0160;<span style="font-family: helvetica;">Add&#0160;</span> &#0160;</span></strong> ボタンで使用推定を追加することが出来ます。また、予測に使用した計算単位は、右側のごみ箱アイコンで削除することが出来ます。すべての合計の１か月の予測値は、ページ右上の <strong>Monthly tokens</strong> に表示されます。</p>
<p>例えば、ローカル コンピュータ（Local drive）に保存されている Revit プロジェクト ファイル（.rvt）10 個を１か月に 6 回、同じく、ローカル コンピュータに保存されている AutoCAD図面（.dwg）30 個を１か月に 5 回、Viewer 表示用に変換、かつ、1 MBの素材ファイルを利用/参照して Inventor エンジンで自動処理を実行すると仮定すると、合計で毎月 45.12 トークンを消費する予測結果が表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6d1ebf2200b-pi" style="display: inline;"><img alt="Total" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6d1ebf2200b image-full img-responsive" src="/assets/image_826268.jpg" title="Total" /></a></p>
<p>Usage Analytics は、90日間の無償トライアル期間中はトークン消費量を表示しませんが、トークン予測ツールを使用すると、いつでも運用想定に基づいたトークン消費数を予測・表示させることが出来ます。もちろん、このツールの使用は無償です。</p>
<ul style="list-style-type: circle;">
<li>トークン予測ツールが表示する値は、あくまで目安です。さまざまな要因で実際の消費量と異なる場合もありますので、その点はあらかじめご承知おきください。</li>
</ul>
<p>By Toshiaki Isezaki</p>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
