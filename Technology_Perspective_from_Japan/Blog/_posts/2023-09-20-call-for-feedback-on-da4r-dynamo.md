---
layout: "post"
title: "Design Automation for Revit with Dynamo：フィードバックのお願い"
date: "2023-09-20 00:08:24"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/09/call-for-feedback-on-da4r-dynamo.html "
typepad_basename: "call-for-feedback-on-da4r-dynamo"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d39e2720200d-pi" style="display: inline;"><img alt="Banner" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d39e2720200d image-full img-responsive" src="/assets/image_886886.jpg" title="Banner" /></a></p>
<p>Design Automation API は、オートデスクのデスクトップ製品（AutoCAD、Revit、Inventor、3ds Max）からユーザ インタフェースを除去したオーバーヘッドのないコア エンジンをクラウド上で実行し、アドイン アプリやスクリプトを処理する機能を提供します。</p>
<p>Autodesk Docs や OSS（Data Management API 経由）に加え、OneDrive や Dropbox などの他社ストレージ プロバイダから、AWS S3 や Azure Blob Storage などの汎用ソースまで、さまざまなデータ ソースから素材となるデザイン ファイルをダウンロードしたり、処理した成果ファイルをアップロードしたりすることが出来ます。</p>
<p>Design Automation API は、既に多くに自動化処理で使用されています。詳細は、<a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/overview/" rel="noopener" target="_blank">公式ドキュメント</a>と<a href="https://github.com/search?q=topic%3Aautodesk-designautomation+org%3Aautodesk-platform-services&amp;type=Repositories" rel="noopener" target="_blank"> GitHub リポジトリ上のサンプル</a>をご確認いただくことが出来ます。</p>
<p>Dynamo は、BIM ワークフローをカスタマイズできるビジュアル プログラミング インタフェースです。また、設計者向けのオープンソースでもあります。これは、Revit 固有のプログラミング ノードと共に <a  _istranslated="1" href="https://help.autodesk.com/view/RVT/2024/ENU/?guid=RevitDynamo_Dynamo_for_Revit_html" rel="noopener" target="_blank">Dynamo for Revit</a> が Revit 製品とともにインストールされています。&#0160;</p>
<p>オートデスクは、Design Automation API for Revit で Dynamo を実行出来るよう取り組んでいます。これにより、Dynamoグラフ スクリプトを実行して Revit ファイルを開き、自動処理して結果を得ることが出来るようになります。</p>
<p><strong>ベータ版評価のお願い</strong></p>
<p>ベータ プログラムにご参加いただき、Design Automation API for Revit で Dynamo グラフ スクリプトを評価、フィードバックをいただけると大変助かります。</p>
<p>現在、Revit 2024 コア エンジン バージョンのみがサポートされています。Design Automation API の RESTful API のエンドポイント呼び出しをおこなうサーバー実装では任意の言語から使用できますが、Revit コア エンジンにロード・実行するアドイン実装には、通常、.NET の利用が必須です。（ほとんどのサンプルでは C# 言語を使用しています。）</p>
<p>この評価プログラムでは、Revit 用 Dynamo 2.18 の特別バージョンを含む AppBundle（zip ファイル）が必要になります。</p>
<h3><span style="font-size: 11pt;">ベータ プログラムへの参加方法</span></h3>
<p><a  _istranslated="1" href="https://feedback.autodesk.com/da_revit_dynamo" rel="noopener" target="_blank">ベータ ポータルのフィードバック プロジェクト</a>（英語） に参加して（要 Autodesk ID でサインイン）、サンプル コードを含む追加情報や詳細をご確認ください。</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/call-feedback-design-automation-revit-dynamo">Call for feedback: Design Automation for Revit with Dynamo | Autodesk Platform Services</a> から転写・翻訳、補足したものです。</p>
<p>By Toshiaki Isezaki</p>
