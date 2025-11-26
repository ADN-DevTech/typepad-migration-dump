---
layout: "post"
title: "APS（旧 Forge）開発に際して..."
date: "2022-02-21 00:15:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/02/need-to-know-before-forge-development.html "
typepad_basename: "need-to-know-before-forge-development"
typepad_status: "Publish"
---

<p>言うまでもなく、Autodesk Platform Services（旧 Autodesk Forge）はオートデスクのクラウド開発プラットフォームです。Web 開発のテクノロジ基盤を利用する Web API（Web サービス API）の特徴から、他社やオープンソースの Web API と融合し易い特徴を持っています。</p>
<p>APS の利用者（開発者）は、APS と他の Web API を利用して、いままでにない新しいソリューションを構築することが出来るようになります。ちょうど、さまざまなカタチや色を持つブロックを組み合わせて、新しい「何か」を組み立てるような感覚です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e144d86d200b-pi" style="display: inline;"><img alt="Forge" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e144d86d200b image-full img-responsive" src="/assets/image_237373.jpg" title="Forge" /></a></p>
<p>2016 年の正式導入から 5 年以上経過した Forge（現 APS）は、ここ日本でも数多く<a href="https://forge.autodesk.com/customers" rel="noopener" target="_blank">利用</a>されています。<a href="https://adndevblog.typepad.com/technology_perspective/2021/03/efficient-demo-sites-to-know-forge.html" rel="noopener" target="_blank">Forge を知る効果的なデモ サイト</a> でも触れていますが、現在、大きく 3 つの利用形態が主流になっています。</p>
<p>APS Viewer を使ってデザイン データに含まれる情報を視覚化、洞察力を得る <strong>Visual Insight</strong>（別名 <strong>Viewer ソリューション</strong>）、BIM 360/Autodesk Construction Cloud や Fusion Team など、ストレージ サービスに保存・利用されているデータを独自に活用する <strong>BIM 360 統合</strong>（別名<strong>ストレージ統合ソリューション</strong>）、クラウド上の CAD エンジンにデスクトップ製品アドインをロード、実行させて、デザインデータの作成や編集、データ抽出を自動化する <strong>Design Automation</strong>（別名<strong> 自動化ソリューション</strong>） です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1450a17200b-pi" style="display: inline;"><img alt="Major_solutions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1450a17200b image-full img-responsive" src="/assets/image_686850.jpg" title="Major_solutions" /></a></p>
<p>このような中、いままでのオートデスクの歴史から、Revit や AutoCAD、Inventor などのデスクトップ製品アドイン（別名、アドオン、プラグイン）開発に携わる方が APS に取り組むケースが増えています。</p>
<p>そこで、今回は、デスクトップ開発者の方が APS 開発に着手する際に、把握いただきたい点をまとめてみたいと思います。</p>
<p><strong>Web アプリ</strong></p>
<p style="padding-left: 40px;">APS の 3 つの利用形態でほぼ共通しているのは、フロント エンドとして Web ページを持つ点です。APS Viewer を使った 2D 図面や 3D モデルの表示も Web ページへの埋め込みです。</p>
<p style="padding-left: 40px;">Web ページを配信するには、Web サーバーを作成する必要があります。Web ページの表示では、クライアント デバイスの Web ブラウザから URL をリクエスト（要求）して、Web サーバーが URL に対応するコンテンツ（HTML で定義されたページ）をクライアント デバイスにレスポンス（応答）することで、ブラウザにコンテンツが表示されます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806c40ea200d-pi" style="display: inline;"><img alt="Cobcept" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806c40ea200d image-full img-responsive" src="/assets/image_396055.jpg" title="Cobcept" /></a></p>
<p style="padding-left: 40px;">Web ブラウザに内蔵される<a href="https://adndevblog.typepad.com/technology_perspective/2018/10/about-developer-tool-on-web-browser.html" rel="noopener" target="_blank">デベロッパーツール</a>を使用すると、リクエストやレスポンスの情報をトレース出来るだけではなく、レスポンスとして返された HTML（ページ）の内容や、HTML に付帯する JavaScript コード（ソースコード）を参照することが出来ます。</p>
<p style="padding-left: 40px;">このため、Web ページ コンテンツ（HTML、特に JavaScript コード）には、ユーザ認証で利用する保護すべき<a href="https://qiita.com/miyuki_samitani/items/02ed5da2c129107cfe55" rel="noopener" target="_blank">クレデンシャルな情報</a>はハード コードすべきではありません。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806c4118200d-pi" style="display: inline;"><img alt="Developer_yool" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806c4118200d image-full img-responsive" src="/assets/image_144728.jpg" title="Developer_yool" /></a></p>
<p style="padding-left: 40px;">Web ブラウザからのリクエストには、Web ページを要求する URL のほか、Web サーバーとのコミュニケーションで使用する <a href="https://adndevblog.typepad.com/technology_perspective/2016/07/forge-api-glossary.html#_endpoint" rel="noopener" target="_blank">エンドポイント</a>&#0160;呼び出しも含まれます。</p>
<p style="padding-left: 40px;">エンドポイントとは、デスクトップ開発で使用する関数やメソッドに相当するもので、APS が提供するエンドポイント以外に、Web サーバー側で独自に作成することも出来ます。エンドポイントは URL と同じような表記を持つ URI で呼び出します。</p>
<p style="padding-left: 40px;">独自にエンドポイントを作成してクライアントからのリクエストに応じた Web サーバー上の一連の処理とを結び付けることをルーティング、ないしは Web ルーティングと呼ぶことがあります。デスクトップ開発でも独自に関数やメソッドを作成、サブルーチンとして利用しますが、それと似た感覚です。エンドポイントは、クライアント実装からだけでなく、Web サーバー実装からも呼び出すことが出来ます。</p>
<p style="padding-left: 40px;">クライアントの Web ブラウザでクレデンシャル情報が過度に露見してしまうことを避けるため、独自のエンドポイントを用意して、クレデンシャルが必要な処理を Web サーバーで実行するのが一般的です。（もちろん、例外もあります。）</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f9a7503200c-pi" style="display: inline;"><img alt="Actual" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f9a7503200c image-full img-responsive" src="/assets/image_388495.jpg" title="Actual" /></a></p>
<p style="padding-left: 40px;">Web サーバー実装は、Web ページ コンテンツ配信以外の役割を持たせることが可能でもあり、Web アプリケーション（<strong>Web アプリ</strong>）とも表現されます。</p>
<p><strong>通信経路</strong></p>
<p style="padding-left: 40px;">APS に限らず、クラウアント デバイスからのリクエストや Web サーバーからのレスポンス、総じてクラウドとのコミュニケーションで伝送路として使用するのは、言うまでもなく、公衆回線を流用したインターネットです。</p>
<p style="padding-left: 40px;">このため、「今日は Web ページの表示が早い」、「エンドポイント呼び出しからレスポンス受信までの時間が昨日より遅い」など、アクセスする時間帯や状況によってコミュニケーション時間に差が出る可能性が考えられます。</p>
<p style="padding-left: 40px;">インターネットでは、クライアントとサーバーが直接やり取りをするわけではなく、ゲートウェイやルーターといった中継を担う機器、ドメイン名解決処理、が介在していて、それらを運用・管理する企業が存在します。中継機器の故障や処理の問題で、稀にインターネット全体が影響を受けることがあります。</p>
<p style="padding-left: 40px;">このような問題は、ネットワークの負荷状況によって一時的に発生する場合もあります。クライアントからのリクエストにエラーが返されている場面で、少し時間を空けて再リクエストすると、期待したレスポンスが得られるケースです。これは、APS サーバーに問題がなくても、経路上の問題でリクエストが Web サーバーやクラウド リソースに到達出来ない可能性があろことを意味します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f99f798200c-pi" style="display: inline;"><img alt="Internet" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f99f798200c image-full img-responsive" src="/assets/image_869647.jpg" title="Internet" /></a></p>
<p><strong>呼び出し数制限</strong></p>
<p style="padding-left: 40px;">APS が提供するエンドポイントは、1 分間に呼び出すことが出来る数が制限されています。この制限は <strong>Rate Limit</strong> の表現でドキュメントで明記されています。</p>
<p style="padding-left: 40px;">Rate Limit は API やエンドポイントによって一定ではありませんが、明示的に指定されるものには、<a href="https://forge.autodesk.com/en/docs/oauth/v2/developers_guide/rate-limiting/oauth-rate-limits/" rel="noopener" target="_blank">Authentication API（OAuth API）</a>、<a href="https://forge.autodesk.com/en/docs/data/v2/developers_guide/rate-limiting/dm-rate-limits/" rel="noopener" target="_blank">Data Management API</a>、<a href="https://forge.autodesk.com/en/docs/bim360/v1/overview/rate-limits/" rel="noopener" target="_blank">BIM 360 API</a>、<a href="https://aps.autodesk.com/en/docs/acc/v1/overview/rate-limits/" rel="noopener" target="_blank">Autodesk Construction Cloud API</a>、<a href="https://forge.autodesk.com/en/docs/model-derivative/v2/developers_guide/rate-limiting/md-rate-limits/" rel="noopener" target="_blank">Model Derivative API</a>、<a href="https://forge.autodesk.com/en/docs/webhooks/v1/developers_guide/rate-limits/webhooks-rate-limits/" rel="noopener" target="_blank">Webhooks API</a>、<a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/rate-limits/da-rate-limits/" rel="noopener" target="_blank">Design Automation API</a>、<a href="https://forge.autodesk.com/en/docs/reality-capture/v1/developers_guide/rate-limits/recap_quotas/" rel="noopener" target="_blank">Reality Capture API</a> などがあります。</p>
<p style="padding-left: 40px;">この制限によって、Dos/DDos などの攻撃を防ぐだけでなく、すべての APS アプリに可用性と一貫したサービス品質を保証します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f9a6fd5200c-pi" style="display: inline;"><img alt="Dos" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f9a6fd5200c image-full img-responsive" src="/assets/image_823333.jpg" title="Dos" /></a></p>
<p style="padding-left: 40px;">指定数を超えたエンドポイント呼び出しには、429 コードのレスポンスが返されます。レスポンス（レスポンス ヘッダー）には再試行（再呼び出し）までの時間（秒）が含まれますので、同秒後に再度エンドポイント呼び出しすることが期待されます。</p>
<p style="padding-left: 40px;">過剰な再呼び出しの繰り返しは、ネットワーク トラフィックの増大や APS サーバーの過剰な処理を招き、APS 環境全体のパフォーマンス低下につながってしまいます。</p>
<p style="padding-left: 40px;">なお、APS サービスが何らかの理由で過負荷状態になっている場合にも、過負荷状態が終了するまで Rate Limit が実質的に低下する可能性がありますのご注意ください。Rate Limit の詳細は、<a href="https://adndevblog.typepad.com/technology_perspective/2025/01/rate-limit.html" rel="noopener" target="_blank">Rate Limit（レート制限、呼び出し回数制限）</a> をご確認ください。</p>
<p><strong>非同期処理</strong></p>
<p style="padding-left: 40px;">すべての処理がローカル環境で完結するデスクトップ開発では、作成したプログラムは記述順に上から下へ順番に実行されます。途中の関数やメソッドの処理終了まで時間はかかっても、それらの終了を待って次の関数/メソッドが同期的に実行されることになります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f99f906200c-pi" style="display: inline;"><img alt="Synchronous" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f99f906200c image-full img-responsive" src="/assets/image_14898.jpg" title="Synchronous" /></a></p>
<p style="padding-left: 40px;">インターネットを使ったコミュニケーションでは、非同期処理を意識する必要があります。</p>
<p style="padding-left: 40px;">ここまでの内容から、リクエスト送信からレスポンス受信まで、インターネットを使ったコミュニケーションでは、時々で応答時間が異なることをご理解いただけるはずです。</p>
<p style="padding-left: 40px;">別の表現をするなら、リクエストに対するレスポンスは即時に得られるわけではない、ということになります。プログラム自体は上から下へ順番に実行される点は変わりませんが、リクエスト①の次にリクエスト②を記述した場合、リクエスト①のレスポンスが来る前に、リクエスト②のレスポンスが着信する場合も当然あり得ます。</p>
<p style="padding-left: 40px;">使用する開発言語が非同期処理をサポートする記述/手法を提供しているはずですが、少なくともデスクトップ開発との差は意識すべきです。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f99f90b200c-pi" style="display: inline;"><img alt="Asynchronous" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f99f90b200c image-full img-responsive" src="/assets/image_924536.jpg" title="Asynchronous" /></a></p>
<p><strong>仮想環境</strong></p>
<p style="padding-left: 40px;">Design Automation/自動化ソリューションで使用する Design Automation API は、Web 開発とデスクトップ製品用アドイン開発の両方の知識が必要なハイブリッド環境と言えます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f99f38d200c-pi" style="display: inline;"><img alt="Hardle" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f99f38d200c image-full img-responsive" src="/assets/image_32545.jpg" title="Hardle" /></a></p>
<p style="padding-left: 40px;">Design Automation API のアドイン処理も含め、CPU リソースを利用する演算サービスでは、クラウドの自動伸張機能（elastic computing）の特性も理解していただきたい点です。具体的には、Design Automation API は、リクエスト数に応じて、実行環境となる仮想マシンを動的に増減させます。</p>
<p style="padding-left: 40px;">もし、短い間に数多くにリクエストが集中した場合、必要な数の仮想マシン展開に時間がかかり、処理完了までに「通常」より時間を要してしまう場合もあります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806c3ee2200d-pi" style="display: inline;"><img alt="Elastic" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806c3ee2200d image-full img-responsive" src="/assets/image_558318.jpg" title="Elastic" /></a></p>
<p><strong>オンプレミスとの違い</strong></p>
<p style="padding-left: 40px;">このように、APS の開発はデスクトップ製品のアドイン開発とは大きく異なります。Visual Insight（別名 Viewer ソリューション）や BIM 360 統合（別名ストレージ統合ソリューション）は、APS でしか実現出来ないものです。一方、アドインが介在する Design Automation（別名 自動化ソリューション）では、兎角、ローカル環境でのアドイン実行パフォーマンスと比較されがちです。</p>
<p style="padding-left: 40px;">なるべく処理に問題が発生しないよう、オートデスクは APS に投資を続けていますが、インターネット経路など、一社ではどうにもならない部分があるのも事実です。</p>
<p style="padding-left: 40px;">パブリック クラウドのインフラ（AWS）上に構築されている APS は、世界中の APS デベロッパとの共有リソースである、といった視点も重要です。</p>
<p style="padding-left: 40px;">これらの点を理解していただき、Web やクラウド環境で得られるワークフローの見直しや新しい価値の創出・発見に重きを置いたソリューション開発に挑んでいただきたく思います。</p>
<p>By Toshiaki Isezaki</p>
