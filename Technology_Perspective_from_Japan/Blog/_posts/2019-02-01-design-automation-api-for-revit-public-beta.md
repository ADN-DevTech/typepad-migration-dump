---
layout: "post"
title: "Design Automation API for Revit パブリックベータ版の公開"
date: "2019-02-01 00:15:27"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/02/design-automation-api-for-revit-public-beta.html "
typepad_basename: "design-automation-api-for-revit-public-beta"
typepad_status: "Publish"
---

<p>先日の BIM &amp; Forge セミナーで告知していた通り、1月28日、Design Automation API for Revit がパブリックベータ版として公開されました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3dc3bee200b-pi" style="display: inline;"><img alt="DesignAutomationRevit" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3dc3bee200b img-responsive" src="/assets/image_137360.jpg" title="DesignAutomationRevit" /></a></p>
<p>これまでは、招待制で一部のパートナー様にプライベート ベータ版としてご利用いただいておりましたが、今後は、Forge アプリを開発されるデベロッパーの皆様にもお試しいただくことができます。</p>
<p>今回は、Design Automation API for Revit の概要をご紹介し、次回以降、複数回にわたってチュートリアル形式で開発方法をご案内いたします。</p>
<p>これまでリリースされてきた Forge プラットフォーム API は、 Authentication API で認証・認可を行い、Data Management API でクラウド上の CAD データにアクセスし、 Model Derivatives API でデータ変換を行い、Web ブラウザで Viewer を通じてデータを表示・閲覧することが主な用途でした。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bc9103200d-pi" style="display: inline;"><img alt="3d_viewer_app_screenshot" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bc9103200d image-full img-responsive" src="/assets/image_272517.jpg" title="3d_viewer_app_screenshot" /></a></p>
<p>Web ブラウザで CAD データを簡単に閲覧できる仕組みが登場したことで、より多くの潜在的なユーザー層に、CAD データを活用する機会をご提供することができました。</p>
<p>しかし、残念ながら、現状の Viewer は、CAD ソフトウェアのモデリング機能を搭載していないため、CAD データを作成・編集することはできません。</p>
<p>Autodesk は、こうした制約に対する一つの解として、Design Automation API をリリースしました。</p>
<p>Design Automation API は、クラウド上で CAD エンジンのコア API を利用する環境を提供します。<br />Forge プラットフォームを活用して、自動化されたジョブを、大規模かつ効率的に実行することができます。</p>
<ul>
<li>繰り返し行う処理</li>
<li>頻度の高い処理</li>
<li>大規模な演算処理能力を必要とする処理</li>
</ul>
<p>従来は AutoCAD のエンジンのみがサポートされておりましたが、V3 では、Inventor、3ds Max、そして Revit が追加されました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3dc3cd5200b-pi" style="display: inline;"><img alt="DesignAutomationRevit1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3dc3cd5200b image-full img-responsive" src="/assets/image_1629.jpg" title="DesignAutomationRevit1" /></a></p>
<p>Design Automation API for Revit は、Revit のエンジンを Forge のサービスとしてクラウド上で実行できる環境を提供します。</p>
<p>次のようなメリットがあります。</p>
<ul>
<li>Revit がローカルのデスクトップ環境にインストールされていなくても、クラウドサービスを通じて Revit アドインを実行</li>
<li>Revit のサブスクリプションが不要</li>
<li>これまで Revit API を利用して開発した Revit アドインのソースコード資産を再利用</li>
<li>任意のクラウドストレージ上のデータを読み取り、保存できるため、いつでもどこからでもアクセス可能</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3dc3d53200b-pi" style="display: inline;"><img alt="DesignAutomationRevit2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3dc3d53200b image-full img-responsive" src="/assets/image_339300.jpg" title="DesignAutomationRevit2" /></a></p>
<p>Revit アドインを Forge にアップロードし、クラウドネイティブなアプリケーションやサービスを構築すれば、Revit データを「作成」、「抽出」、「編集」することができます。</p>
<p>これまで、Revit のターゲット層は主に設計者でしたが、より幅広いユーザーが Revit データを活用できるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad396860d200c-pi" style="display: inline;"><img alt="DesignAutomationRevit3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad396860d200c image-full img-responsive" src="/assets/image_189697.jpg" title="DesignAutomationRevit3" /></a></p>
<p>Revit アドインのプログラムでは、Revit DB API にフルアクセスすることができ、次のようなワークフローが想定されます。</p>
<ul>
<li>カスタムの Revit ファミリを作成</li>
<li>モデル作成の自動化</li>
<li>モデルデータの抽出・解析</li>
<li>レポートの自動生成</li>
<li>設計基準に合わせて既存モデルを編集</li>
<li>設計図書の自動作成</li>
</ul>
<p>ただし、いくつかの制約事項もございます。</p>
<ul>
<li>Revit ユーザーインターフェースの表示・操作、RevitAPIUI.dll アセンブリの参照はできません。</li>
<li>REST API で直接 Revit のデータにアクセスできません。</li>
<li>複雑なセッション管理は想定されていません。（バッチ処理を想定）</li>
<li>Revit アドインから外部ネットワークにアクセスすることはできません。</li>
<li>ユーザーとのインタラクションが発生する処理はサポートされておりません。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bc928a200d-pi" style="display: inline;"><img alt="DesignAutomationRevit4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bc928a200d image-full img-responsive" src="/assets/image_382423.jpg" title="DesignAutomationRevit4" /></a></p>
<p>先日開催した BIM &amp; Forge セミナーの 4つ目のセッションでは、Design Automation API の概要、仕組みと開発要件、開発の流れ、制約事項などを詳しく解説しております。ぜひご参照ください。</p>
<p>BIM &amp; Forge セミナーのサマリー<br /><a href="https://adndevblog.typepad.com/technology_perspective/2019/01/bim-forge-seminar-summary.html">https://adndevblog.typepad.com/technology_perspective/2019/01/bim-forge-seminar-summary.html</a></p>
<p><strong>Design Automation API V3 のドキュメントへのアクセス方法</strong></p>
<p>デフォルトでは V2 が表示されますので、下記のリンクから直接アクセス頂くか、ドキュメントのバージョンを V3 に指定すると閲覧できます。</p>
<p><a href="https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/">https://forge.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/</a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bc92e9200d-pi" style="display: inline;"><img alt="DesignAutomationRevit5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bc92e9200d image-full img-responsive" src="/assets/image_281916.jpg" title="DesignAutomationRevit5" /></a></p>
<p>Step-by-Step Tutorials では、Revit 版の詳細なチュートリアルをご確認いただけます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3bc9302200d-pi" style="display: inline;"><img alt="DesignAutomationRevit6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3bc9302200d image-full img-responsive" src="/assets/image_530361.jpg" title="DesignAutomationRevit6" /></a></p>
<p>最後に注意点として、パブリックベータ版は、より多くのデベロッパーの皆様からフィードバックを頂いて、API を改善し、機能追加の優先度を整理し、スケーラビリティを検証することが目的となります。</p>
<p>そのため、あくまでもプロトタイプ検証などの用途としてご利用いただき、プロダクションレベルのアプリケーションではご利用されないようお願い致します。</p>
<p>また今後、API が一部変更される可能性があることをご了承ください。</p>
<p>次回から、チュートリアルに沿って、サンプルを実装する手順を解説いたします。</p>
<p>By Ryuji Ogasawara</p>
