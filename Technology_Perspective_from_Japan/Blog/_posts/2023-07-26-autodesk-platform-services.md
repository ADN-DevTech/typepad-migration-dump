---
layout: "post"
title: "Autodesk Platform Services とは"
date: "2023-07-26 00:04:56"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/07/autodesk-platform-services.html "
typepad_basename: "autodesk-platform-services"
typepad_status: "Publish"
---

<div class="forgeBox01">
<p>クラウドに「繋がる」ことで利便性や生産性が向上していく時代、オートデスクは、Autodesk Platform Services のブランド名で、設計/デザインを活用する複数の Web API（Web サービス API）を公開して、API エコノミーに参画しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cf2998200b-pi" style="display: inline;"><img alt="Api_economy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cf2998200b image-full img-responsive" src="/assets/image_861557.jpg" title="Api_economy" /></a></p>
</div>
<div class="forgeMv">
<div class="wrap">
<p class="asset-video" style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/OjRMkCCypgg" width="480"></iframe></p>
<p class="asset-video">Autodesk Platform Services（APS）は、もともと Autodesk Forge の名称で 2016 年に公開をはじめたものですが、<a href="https://adndevblog.typepad.com/technology_perspective/2023/06/design-and-make-platforms.html" rel="noopener" target="_blank"><strong>オートデスクのプラットフォーム戦略</strong></a>を推し進めるにあたり、2022 年秋に名称を変更したものです。このため、既存の API も従来どおり利用することが出来ます。</p>
<p class="asset-video" style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/WjE5YGFrwHk" width="480"></iframe></p>
<p class="asset-video">APS が提供する主要 API やその機能概要は、 <a href="https://adndevblog.typepad.com/files/aps-flyer_2025_rev1.pdf" rel="noopener" target="_blank"><strong>リーフレット</strong>をダウンロード（2025年7月更新）</a>してご確認ください。</p>
<p class="asset-video"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cf2a71200b-pi" style="display: inline;"><img alt="Aps_apis" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cf2a71200b image-full img-responsive" src="/assets/image_444773.jpg" title="Aps_apis" /></a></p>
<hr />
<p class="asset-video"><strong>新たな試み - Design &amp; Make（デザインと創造）プラットフォーム</strong></p>
<p>オートデスクは、「Design and Make（デザインと創造）プラットフォーム」を整備してデータのアクセス方法に変革をもたらします。</p>
<p>従来のデザインデータは、使用する製品/ツールによって固有の形式を持つファイルとして保存・管理されています。クラウドの利用が進むにつれて、ファイル形式の違いによって起こる時の相互理解の阻害、ファイルの肥大化と転送・同期の遅延、設計変更で生じるファイル バージョンの重複データの冗長性、すべてのデータを含むファイル共有による意図しない知的財産情報の漏洩などが問題視されるようになっています。</p>
<p class="asset-video"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39cf9cd200b-pi" style="display: inline;"><img alt="Issues_on_file" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39cf9cd200b image-full img-responsive" src="/assets/image_817508.jpg" title="Issues_on_file" /></a>&#0160;</p>
<p>デザインと創造プラットフォームでは、ファイルに内包されているデータをバラバラに分解、「粒状データ」化することで、必要なデータのみを個別に取り出したり、書き込んだりする機能を提供します。</p>
<p>同プラットフォームは APS が新しく提供する API 群を使って構築されます。機能別に、各種製品から粒状データを扱うネイティブに Autodesk Deta Model、粒状データを使ってデータ交換を実現する Data Exchange、製品間で共通したナビゲーションや共通データ アクセスを提供する Autodesk Data Access の 3 つのカテゴリに分けられ、粒状データにアクセスするための GraphQL や SDK が用意されます。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/06/design-and-make-platforms.html" rel="noopener" target="_blank"><strong>Design &amp; Make（デザインと創造）プラットフォーム</strong></a></li>
</ul>
</div>
</div>
<hr />
<p class="forgeTxt02"><strong>Autodesk Platform Services を始めるには？</strong></p>
<p class="forgeTxt02">Autodesk Platform Services（APS）は、開発作業に必要なデベロッパーキー（Client ID と Client Secret）を取得すれば、すぐに評価を始めることが出来ます。<strong><a href="#cost">一部 API は課金対象</a></strong>になっていますが、90 日間の無償利用を提供するトライアル期間を使用することが出来ます。APS の始め方は、次のブログ記事で詳しくご案内しています。</p>
<ul>
<li class="forgeTxt02"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2023/03/how-to-get-started-aps.html" rel="noopener" target="_blank">Autodesk Platform Services の始め方</a></strong></li>
</ul>
<p class="forgeTxt02"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25ae45d200d-pi" style="display: inline;"><img alt="How_to_begin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25ae45d200d image-full img-responsive" src="/assets/image_704012.jpg" title="How_to_begin" /></a></p>
<hr />
<p><strong>Autodesk Platform Services の公式ドキュメントは？</strong></p>
<p>APS の公式ドキュメントは、<strong><a href="https://aps.autodesk.com/developer/documentation" rel="noopener" target="_blank">https://aps.autodesk.com/developer/documentation</a></strong>（英語のみ）で公開されています。API の概要を解説する Developer&#39;s Guide、使用手順に沿った説明をおこなう Step by Step Tutorials、各エンドポイントの URL やパラメータ等の詳細を記した Reference で構成されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cf2be6200b-pi" style="display: inline;"><img alt="Documents" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cf2be6200b image-full img-responsive" src="/assets/image_209645.jpg" title="Documents" /></a></p>
<hr />
<p class="forgeTxt02"><strong>Autodesk Platform Services を知るには？</strong></p>
<p class="forgeTxt02">Forge 時代に作成したショート ビデオ シリーズ「Forge Online」がありますので、ご参考まで。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/forge-online-basic.html" rel="noopener" target="_blank"><strong>Forge Online -&#0160;</strong><strong>概説と基礎</strong></a></li>
<li><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/05/forge-online-viewer-basics.html" rel="noopener" target="_blank">Forge Online - Viewer ソリューションの流れ</a></strong></li>
<li><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/06/forge-online-use-of-oss-bucket.html" rel="noopener" target="_blank">Forge Online - OSS Bucket の利用</a></strong></li>
<li><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/06/forge-online-use-of-autodesk-saas-storage.html" rel="noopener" target="_blank">Forge Online - オートデスク SaaS ストレージの利用</a></strong></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-basics.html" rel="noopener" target="_blank"><strong>Forge Online - Design Automation：タスクの自動化</strong></a></li>
<li><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-api-for-autocad.html" rel="noopener" target="_blank">Forge Online - Design Automation：AutoCAD タスクの自動化</a><br /></strong></li>
<li><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-api-for-revit.html" rel="noopener" target="_blank">Forge Online - Design Automation：Revit タスクの自動化</a><br /></strong></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-api-for-inventor.html" rel="noopener" target="_blank"><strong>Forge Online - Design Automation：Inventor タスクの自動化</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2020/08/forge-online-viewer-basics-1.html" rel="noopener" target="_blank"><strong>Forge Online：Viewer 基本カスタマイズ</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2020/08/forge-online-webhooks-api.html" rel="noopener" target="_blank"><strong>Forge Online：イベント通知と活用：WebHooks API</strong></a></li>
<li><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/08/forge-online-bim360-api.html" rel="noopener" target="_blank">Forge Online：BIM 360 API 概説</a><br /></strong></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/01/forge-online-model-derivative-api-update.html" rel="noopener" target="_blank"><strong>Forge Online：Model Derivative API アップデート</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/01/forge-onlinebim-360-api-project-document-management-automation.html" rel="noopener" target="_blank"><strong>Forge Online：BIM 360 API によるプロジェクト・ドキュメント管理の自動化</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/01/forge-online-bim-360-integration-with-desktop-software.html" rel="noopener" target="_blank"><strong>Forge Online：BIM 360とデスクトップソフトウェアとの連携</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/01/forge-online-bim-360-api-update.html" rel="noopener" target="_blank"><strong>Forge Online：BIM 360 API アップデート</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/02/forge-online-viewer-update.html" rel="noopener" target="_blank"><strong>Forge Online：Viewerアップデート</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/01/forge-onlinedesign-automationinventor-ilogic.html" rel="noopener" target="_blank"><strong>Forge Online：Design Automation：Inventor iLogicの活用</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/06/forge-online-modelstate-and-forgeapi.html" rel="noopener" target="_blank"><strong>Forge Online：Inventor 2022 新機能 モデル状態と Forge API</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/06/forge-online-introduction-of-autodesk-construction-cloud.html" rel="noopener" target="_blank"><strong>Forge Online：次世代の統合プラットフォーム Autodesk Construction Cloud とは</strong></a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2021/07/forge-online-da4a-benefit-abd-migration.html" rel="noopener" target="_blank"><strong>Forge Online：Design Automation API for AutoCAD の効果とアドイン移植</strong></a></li>
</ul>
<hr />
<p class="forgeTxt02"><strong>Autodesk Platform Services を学習するには？</strong></p>
<p class="forgeTxt02">APS 全体を通して学習する場合には、<a href="https://tutorials.autodesk.io/" rel="noopener" target="_blank"><strong>Learn APS Tutorial</strong></a>（英語） をご活用ください。</p>
<p class="forgeTxt02">Forge 時代に翻訳、活用した Learn Forge をローカル環境で実行して、オンライン トレーニングの収録動画とともに確認する方法は、次のブログ記事でご案内しています。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/06/run-learn-forge-locally.html" rel="noopener" target="_blank"><strong>旧 Learn Forge のローカル実行</strong></a></li>
</ul>
<hr />
<p class="forgeTxt02"><strong>Autodesk Platform Services のコードサンプルは？</strong></p>
<p class="forgeTxt02">数多くのコード サンプルが GitHub 上で公開されています。コード サンプルには、<strong><a href="https://aps.autodesk.com/code-samples" rel="noopener" target="_blank">https://aps.autodesk.com/code-samples</a></strong>&#0160;からアクセスすることが出来ます。</p>
<p class="forgeTxt02"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25ae61d200d-pi" style="display: inline;"><img alt="Samples" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25ae61d200d image-full img-responsive" src="/assets/image_863140.jpg" title="Samples" /></a></p>
<hr />
<p class="forgeTxt02"><strong>Autodesk Platform Services の活用事例は？</strong></p>
<p class="forgeTxt02">&#0160;日本からの事例も含め、Autodesk Platform Services の活用事例は <strong><a href="https://aps.autodesk.com/success-stories" rel="noopener" target="_blank">https://aps.autodesk.com/success-stories</a></strong>&#0160;からご確認することが出来ます。すべてではありませんが、お使いの Web ブラウザの言語設定が「日本語」になっていれば、日本語化した事例をご覧いただけます。</p>
<p class="forgeTxt02"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1a6cf2a1a200b-pi" style="display: inline;"><img alt="Success_stories" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1a6cf2a1a200b image-full img-responsive" src="/assets/image_457250.jpg" title="Success_stories" /></a></p>
<hr />
<p class="forgeTxt02"><strong>Autodesk Platform Services の稼働状況を知るには？</strong></p>
<p class="forgeTxt02">オートデスクのクラウド サービスの稼働状態と同様に、各 API の稼働状況は <a href="https://health.autodesk.com/" rel="noopener" target="_blank">ヘルス ダッシュボード サイト（https://health.autodesk.com/）</a>&#0160;で確認することが出来ます。インシデント発生時には、インシデントを Subscribe すると、入力したメール アドレスに状況を知らせる通知ールが届きます。</p>
<ul>
<li class="forgeTxt02"><a href="https://adndevblog.typepad.com/technology_perspective/2023/05/integrate-aps-info-onto-hds.html"><strong>ヘルス ダッシュボードへの APS 情報の統合</strong></a></li>
</ul>
<hr />
<p class="forgeTxt02"><strong><a id="cost"></a>Autodesk Platform Services のコストは？</strong></p>
<p class="forgeTxt02">複数ある APS API のうち、課金対象にっているのは、Model Derivative API、Design Automation API、Reality Capture API の 3 つです。課金は、デベロッパーキーの取得で使用したアカウントで購入した <a href="https://www.autodesk.co.jp/buying/flex" rel="noopener" target="_blank"><strong>Autodesk Flex</strong></a> を使って、従量課金でおこなわれます。</p>
<p class="forgeTxt02">例えば、Viewer でデザインファイルを変換する際に使用する Model Derivative API では、Revit/Navisworks/IFC ファイルを 1 回変換すると、アカウントにプールされている Flex トークンから 0.5 トークンが減算されます。DWG ファイルなど、Revit/Navisworks/IFC ファイル以外の変換時には、 1 回の変換で 0.1 トークンが減算されます。</p>
<p class="forgeTxt02"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25ae532200d-pi" style="display: inline;"><img alt="Cost_per_api" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25ae532200d image-full img-responsive" src="/assets/image_647315.jpg" title="Cost_per_api" /></a></p>
<p class="forgeTxt02">ユーザインタフェースを持たないコアエンジン（AutoCAD、Inventor、Revit、3ds Max）をクラウド上で起動し、アドイン/プラグイン アプリをロード、実行させて成果を得る Design Automation API では、素材ファイルのダウンロードと成果ファイルのアップロードを含めたアドイン処理時間に対して、1 時間当たり 2.0 トークンが減算されます。Design Automation API の課金については、次のブログ記事で詳しくご案内しています。</p>
<ul>
<li class="forgeTxt02"><strong><a href="https://adndevblog.typepad.com/technology_perspective/2020/02/estimate-design-automation-costs.html" rel="noopener" target="_blank">Design Automation API の課金とコスト算出について</a></strong></li>
</ul>
<p class="forgeTxt02">複数写真から 3D メッシュや点群を生成するフォトグラメトリーを実現する Reality Capture API では、生成に使用する画像 50 毎につき、1.0 トークンが減算されます。</p>
<p class="forgeTxt02">トークン残高と APS API 毎の消費量の把握方法は、次のブログ記事で詳しくご案内しています。</p>
<ul>
<li class="forgeTxt02"><a href="https://adndevblog.typepad.com/technology_perspective/2022/11/updated-usage-analytics-page-for-flex.html" rel="noopener" target="_blank"><strong>Usage Analytics ページ：Flex トークン残高と消費量の確認&#0160;</strong></a></li>
</ul>
<hr />
<p class="forgeTxt02"><strong>Autodesk Flex の購入方法</strong></p>
<p class="forgeTxt02">Autodesk Flex は、特定のオートデスク認定リセラーから購入することが出来ます。当該リセラーは、Flex 特約を締結済の Platinum/Gold 認定リセラーで、<strong><a href="https://customersuccess.autodesk.com/partners/search?search=&amp;locations%5B%5D=Japan&amp;languages%5B%5D=Japanese&amp;services%5B%5D=Flex" rel="noopener" target="_blank">Autodesk Customer Success Hub</a></strong> から検索することが出来ます。</p>
<p class="forgeTxt02">また、オートデスクの eStore から、オンラインで購入することも可能です。</p>
<ul>
<li class="forgeTxt02"><a href="https://adndevblog.typepad.com/technology_perspective/2023/06/autodesk-flex-now-available-in-japan-from-estore.html" rel="noopener" target="_blank"><strong>日本で eStore から Autodesk Flex の販売を開始</strong></a></li>
</ul>
<hr />
<p class="forgeTxt02"><strong><a id="support"></a>Autodesk Platform Services のサポートは？</strong></p>
<p class="forgeTxt02"><a href="https://aps.autodesk.com/get-help" rel="noopener" target="_blank"><strong>https://aps.autodesk.com/get-help</strong></a>（英語）から新しい質問を投稿したり、知りたい内容にかかわるキーワードを入力して検索することで、過去に世界中の開発者から寄せられた質問や回答を閲覧することも可能です。</p>
<p class="forgeTxt02">APS 開発は一般の Web/クラウド開発で利用されるオープンソース、Web 標準化技術などが混在し、オートデスク テクノロジとの差別化が困難であるためオートデスクのエンジニアも介在するかたちで前述の URL から&#0160;<strong><a href="https://forge.autodesk.com/en/support/get-help" rel="noopener noreferrer" target="_blank">StackOverflow</a></strong>によるコミュニティ サポートを提供しています。広範なコミュニティをご活用いただくため、Google 翻訳などを活用して英語での投稿をお願いいたします。</p>
<hr />
<p>By Toshiaki Isezaki</p>
