---
layout: "post"
title: "Design Automation for Fusion TypeScript add-inとVS Code Extensionのご紹介"
date: "2025-06-23 01:02:24"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/06/fusion-design-automation-typescript-add-in-vs-code-extension.html "
typepad_basename: "fusion-design-automation-typescript-add-in-vs-code-extension"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ed5071200b-pi" style="display: inline;"><img alt="DA4FusionVSCodeExtTitle" class="asset  asset-image at-xid-6a0167607c2431970b02e860ed5071200b img-responsive" src="/assets/image_719371.jpg" title="DA4FusionVSCodeExtTitle" /></a></p>
<p>ブログ記事「<a href="https://adndevblog.typepad.com/technology_perspective/2025/06/design-automation-api-for-fusion-generally-available.html">Design Automation API for Fusion : 一般リリース</a>」でご案内いたしましたが、Design Automation for Fusionが一般リリースとなっております。また「<a href="https://adndevblog.typepad.com/technology_perspective/2025/06/trivia-design-automation-for-fusion-typescript.html">雑学：Design Automation for Fusionでカスタム処理を記述する言語 TypeScriptとは雑学：Design Automation for Fusionでカスタム処理を記述する言語 TypeScriptとは</a>」でご案内しましたように、Design Automation for Fusionでカスタム処理を記述する言語には、TypeScriptが採用されております。</p>
<p>これまでデスクトップ版FusionでPythonまたはC++によりFusionのAPIを用いたカスタムスクリプトやアドイン開発をされていた方々は、どのようにDesing Automation for Fuionで実行するカスタマイズ処理を開発するのか？の疑問をお持ちかと思います。</p>
<p>そこで今回の記事では、デスクトップ版Fusionで TypeScriptで記述されたカスタム処理を実行することが出来るFusion add-inと、開発に便利なVS Code Extensionについてご紹介をいたします。</p>
<h3>TypeScript add-in</h3>
<p>TypeScript add-inはFusionのアドインとして提供されており、デスクトップ版のFusionのローカル環境でTypeScriptで記述されたFusionのカスタム処理を実行することが出来ます。インストーラは<a href="https://github.com/autodesk-platform-services/aps-tutorial-postman/tree/master/DA4Fusion/DA4Fusion-Add-In">GitHubリポジトリ</a>から取得することが可能です。</p>
<p>ツールのインストールをした後、FusionのエディタからDesingAutomationforFuionAddinを「起動時に実行」に設定されていることを確認してください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ed4f9a200b-pi" style="display: inline;"><img alt="DA4FusionAddinRunStartup" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860ed4f9a200b img-responsive" src="/assets/image_149990.jpg" title="DA4FusionAddinRunStartup" /></a></p>
<p>&#0160;</p>
<p>アドインがロードされると「ユーティリティ」-「アドイン」メニューに「Desing Automation for Fuion」が追加されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d699a3200c-pi" style="display: inline;"><img alt="DA4FusionAddin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d699a3200c img-responsive" src="/assets/image_938469.jpg" title="DA4FusionAddin" /></a></p>
<p>&#0160;</p>
<h3>&#0160;</h3>
<p>ツールを起動すると、「DESIGN AUTOMATION FOR FUSION」ダイアログが表示されDesign Automation for Fusionで利用するスクリプトを作成できるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e861043600200d-pi" style="display: inline;"><img alt="DA4FusionAddinDialog" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e861043600200d img-responsive" src="/assets/image_100720.jpg" title="DA4FusionAddinDialog" /></a></p>
<h3>VSCodeExtension</h3>
<p>「DESIGN AUTOMATION FOR FUSION」ダイアログで「Create Script」ボタンをクリックし、任意のスクリプト名を入力します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d699af200c-pi" style="display: inline;"><img alt="DA4FusionCreateTypeScript" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d699af200c img-responsive" src="/assets/image_612624.jpg" title="DA4FusionCreateTypeScript" /></a></p>
<p>次に「Edit Script」ボタンをクリックするとVS Codeが起動し、VS Code Extensionをインストールするかを聞かれますので、インストールをします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ed4fb2200b-pi" style="display: inline;"><img alt="DA4FusionInstallVSCodeExt" class="asset  asset-image at-xid-6a0167607c2431970b02e860ed4fb2200b img-responsive" src="/assets/image_130630.jpg" title="DA4FusionInstallVSCodeExt" /></a></p>
<p>この拡張機能を使用すると、Design Automation API を使用せずに、デスクトップと Design Automation サーバーの両方でスクリプトをテストできます。</p>
<p>TypeScript ファイルのテキストエディター内にカーソルがあるとき、右上隅に 2 つの再生アイコンが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d699c4200c-pi" style="display: inline;"><img alt="DA4FusionInstallVSCodeExtRunLocally" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d699c4200c img-responsive" src="/assets/image_369549.jpg" title="DA4FusionInstallVSCodeExtRunLocally" /></a></p>
<p>&#0160;</p>
<p>「Run script locally」を選択すると、デスクトップ上で実行を開始できます。結果は以下のとおりです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860ed4fc0200b-pi" style="display: inline;"><img alt="DA4FusionInstallVSCodeExtRunLocallyResult" class="asset  asset-image at-xid-6a0167607c2431970b02e860ed4fc0200b img-responsive" src="/assets/image_811831.jpg" title="DA4FusionInstallVSCodeExtRunLocallyResult" /></a></p>
<p>&#0160;</p>
<p>Design Automation サーバーでコードをテストする場合は、APS アプリケーションのClientID、Client SecretとPersonal Access Tokenを作成し、それらの資格情報を VS Code 拡張機能に設定する必要があります。なお、Personal Access Tokenの取得方法については、「<a href="https://adndevblog.typepad.com/technology_perspective/2025/06/desing-automation-for-fusion-configurator-application-sample.html">Desing Automation for Fusion コンフィギュレーター サンプルアプリケーション</a>」で解説をしておりますのでご参照ください。</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d699df200c-pi" style="display: inline;"><img alt="DA4FusionInstallVSCodeExtRunRemoteSetting" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d699df200c img-responsive" src="/assets/image_218851.jpg" title="DA4FusionInstallVSCodeExtRunRemoteSetting" /></a></p>
<p>&#0160;</p>
<p>これで「Run script remotely」を選択できます。これにより、ローカルで実行した場合と同じ出力に加えて、Design Automation サーバーからの追加メッセージが提供されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d699f0200c-pi" style="display: inline;"><img alt="DA4FusionInstallVSCodeExtRunRemoteResult" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d699f0200c img-responsive" src="/assets/image_573351.jpg" title="DA4FusionInstallVSCodeExtRunRemoteResult" /></a></p>
<p>&#0160;</p>
<p>なお、Design Automation for FusionのType Scriptではデスクトップ版のFusionのPython、C++から利用できるAPIのほとんどが利用可能ですが、一部制限事項がございます。制限事項の内容については<a href="https://aps.autodesk.com/en/docs/design-automation/v3/developers_guide/fusion_specific/#api-restrictions">こちらのページ</a>をご確認ください。</p>
<p>&#0160;</p>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/get-started-design-automation-fusion">Get started with Design Automation for Fusion | Autodesk Platform Services</a> から転写・意訳・補足したものです。</p>
<p>By Takehiro Kato</p>
