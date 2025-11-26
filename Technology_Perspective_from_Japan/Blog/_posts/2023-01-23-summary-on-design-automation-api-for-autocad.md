---
layout: "post"
title: "Design Automation API for AutoCAD まとめ"
date: "2023-01-23 00:10:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/01/summary-on-design-automation-api-for-autocad.html "
typepad_basename: "summary-on-design-automation-api-for-autocad"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">Design Automation API の名称は、米国太平洋時で 2025年6月30日（日本標準時 7月1日）に Automation API に変更されています。（<a href="https://adndevblog.typepad.com/technology_perspective/2025/06/name-change-design-automation-api-now-automation-api.html" rel="noopener" target="_blank">Design Automation API：Automation API へ名称変更</a>）</span></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c95ea46200d-pi" style="display: inline;"></a>Autodesk Platform Services（旧 Forge）が提供する主なソリューションには、</p>
<ul>
<li>デザイン ファイルに含まれるさまざまな情報を形状とともにブラウザ表示して洞察を得る「Viewer ソリューション」</li>
<li>Autodesk Construction Cloud や BIM 360、Fusion Teamといったオートデスクのクラウド サービス ストレージと統合してデータ運用する「ストレージ統合ソリューション」&#0160;</li>
<li>クラウド上のコアエンジン（AutoCAD、Inventor、Revit、3ds Max）でアドイン/プラグインを実行させて成果を得る「自動化ソリューション」</li>
</ul>
<p>があります。ワークフローに合わせて、どれか 1 つを利用したり、すべてを組み合わせて利用することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c95e811200d-pi" style="display: inline;"><img alt="Aps_solutions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c95e811200d image-full img-responsive" src="/assets/image_494980.jpg" title="Aps_solutions" /></a></p>
<p>数年をかけて粒状データを利用した Cloud Information Model を目指す Autodesk Platform Services ですが、デザイン データを使った業務運用では、当然ながら、まだ「ファイル」が主流です。そんな中、日本での「自動化リューション」では、DWG &gt;&gt; PDF 出力を含め、DWG ファイルの利用を主眼にした Design Automation API for AutoCAD が多く利用されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02af1c98e94a200d-pi" style="display: inline;"><img alt="Popular_dwg" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02af1c98e94a200d image-full img-responsive" src="/assets/image_274620.jpg" title="Popular_dwg" /></a></p>
<p>このブログでも、多く Design Automation API for AutoCAD について触れていますので、ここでまとめておきたいと思います。</p>
<p>まず、自動化における Design Automation API の必要性と位置づけをご紹介しましょう。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/12/design-automation-on-desktop-products.html" rel="noopener" target="_blank">デスクトップ製品使った自動化について</a></p>
<p>Design Automation API for AutoCAD の概要や仕組みを理解した上で、通常の AutoCAD とは異なる視点で、期待できる効果をご案内します。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2019/06/design-automation-api-for-autocad.html" rel="noopener" target="_blank">Design Automation API for AutoCAD 概説</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/08/consider-design-automation-api-operation.html" rel="noopener" target="_blank">Design Automation API for AutoCAD 運用の考察</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/07/effectiveness-on-design-automation-api-for-autocad.html" rel="noopener" target="_blank">Design Automation API for AutoCAD の効果</a></p>
<p>次に Design Automation API for AutoCAD を学習する素材、理解を手助けするツールについてご紹介しましょう。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/10/forge-online-training-design-automation-autocad-materials.html" rel="noopener" target="_blank">Forge Online Training - Design Automation AutoCAD 収録公開</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/08/design-automation-api-sample.html" rel="noopener" target="_blank">Design Automation API サンプル</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/09/workitem-test-on-vs-code-forge-extension.html" rel="noopener" target="_blank">Design Automation API：VS Code を使った WorkItem テスト</a></p>
<p>AutoCAD アドインを運用している場合には、処理内容を Design Automation API for AutoCAD に合わせて修正することで、コアエンジンでも実行出来るようになります。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/07/autocad-addin-conversion-to-design-automation-api.html" rel="noopener" target="_blank">AutoCAD アドインの Design Automation API 化</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/12/notes-on-using-da4a.html" rel="noopener" target="_blank">Design Automation API for AutoCAD 利用の注意点</a></p>
<p>その他、実際の運用に合わせた Tips &amp; Tricks をご案内しています。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/09/design-automation-api-and-object-enabler.html" rel="noopener" target="_blank">Design Automation API for AutoCAD とオブジェクト イネーブラ</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/10/handling-and-resolution-on-custom-font.html" rel="noopener" target="_blank">Design Automation API for AutoCAD：カスタム フォントの扱いと解決&#0160;</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/10/use-of-contents-in-appbundle.html" rel="noopener" target="_blank">Design Automation API for AutoCAD：AppBundle 内のコンテンツ利用</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2021/10/da-retrieving-json-fromappbundle.html" rel="noopener" target="_blank">Design Automation API：WorkItem からの JSON 反映</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/07/output-to-bim-360-docs-from-design-automation.html" rel="noopener" target="_blank">Design Automation から BIM 360 Docs への出力</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/11/design-automation-api-multipart-support-s3-upload.html" rel="noopener" target="_blank">Design Automation API：Direct-to-S3 アプローチを簡素化する新機能</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/11/design-automation-various-activities-and-workitems.html" rel="noopener" target="_blank">Design Automation：さまざまな Activity と WorkItem</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/12/design-automation-encoding-when-zip-compressing.html" rel="noopener" target="_blank">Design Automation API：ZIP 圧縮時のエンコード&#0160;</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/02/design-automation-api-multiple-scripts-sequential-execution.html" rel="noopener" target="_blank">Design Automation API：複数のスクリプトの連続実行</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/03/impacr-from-active-transactio.html" rel="noopener" target="_blank">Design Automation API：アクティブ トランザクションの影響&#0160;</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/03/design-automation-apiadskdebug-option.html" rel="noopener" target="_blank">Design Automation API：adskDebug オプション</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/05/design-automation-api-for-autocad-override-pc3-file.html" rel="noopener" target="_blank">Design Automation API for AutoCAD：.pc3 ファイルの上書き</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/01/desigm-automation-new-endpoints%E3%83%B3%E3%83%88.html" rel="noopener" target="_blank">Design Automation：WorkItem クエリーの追加&#0160;</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/09/desigm-automation-adskmask-option.html" rel="noopener" target="_blank">Design Automation：adskMask オプション</a></p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/10/design-automation-check-health-status.html" rel="noopener" target="_blank">Design Automation：エンジン稼働状況の確認</a></p>
<p>Design Automation API のコアエンジンは、対応するデスクトップ版 AutoCAD と同期しています。このため、コアエンジンにもライフサイクル ポリシーが策定されています。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2023/03/design-automation-api-core-engine-lifecycle-policy.html" rel="noopener" target="_blank">Design Automation API：コアエンジン ライフサイクル ポリシー</a></p>
<p>Design Automation API のコストについては、次の記事でご案内しています。</p>
<p style="padding-left: 40px;"><a href="https://adndevblog.typepad.com/technology_perspective/2020/02/estimate-design-automation-costs.html" rel="noopener" target="_blank">Design Automation API の課金とコスト算出について</a></p>
<p>By Toshiaki Isezaki</p>
