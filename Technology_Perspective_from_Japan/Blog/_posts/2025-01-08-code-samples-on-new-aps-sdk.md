---
layout: "post"
title: "新 APS SDK の最小コード サンプル"
date: "2025-01-08 00:11:52"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/01/code-samples-on-new-aps-sdk.html "
typepad_basename: "code-samples-on-new-aps-sdk"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860df27c9200b-pi" style="display: inline;"><img alt="Aps_sdk" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860df27c9200b img-responsive" src="/assets/image_647213.jpg" title="Aps_sdk" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860df279b200b-pi" style="display: inline;"></a><a href="https://adndevblog.typepad.com/technology_perspective/2024/06/migrating-to-the-new-aps-net-sdk.html" rel="noopener" target="_blank">新しい APS .NET SDK への移行</a>、<a href="https://adndevblog.typepad.com/technology_perspective/2024/06/migrating-to-the-new-aps-nodejs-sdk.html" rel="noopener" target="_blank">新しい APS Node.js SDK への移行</a> でそれぞれご案内した新 APS SDK ですが、後者の Node.js SDK も昨年末に General Availability を迎え、正式にリリースされています。（APS Node.js SDK の<a href="https://aps.autodesk.com/developer/documentation" rel="noopener" target="_blank">ドキュメント</a>記載は少し遅れています。）</p>
<p>これら 新 APS SDK&#0160; を利用するサンプルには、APS 学習リソースの <strong><a href="https://get-started.aps.autodesk.com/" rel="noopener" target="_blank">Learn APS Tutorials</a></strong> で活用されている（説明に使用されている）Simple Viewer と Hub Browser が用意されています。このため、リポジトリから直接ソースコードを参照して、使用方法を確認することが可能になっています。</p>
<p>Simple Viewer は、2-legged 認証フローを利用して OSS Bucket にシード ファイル（デザイン ファイル）をアップロード、変換、Viewer 表示するものです。Hub Browser は、3-legged 認証フローを利用して Autodesk Construction Cloud（Autodesk Docs）や Fusion Team に保存された表示変換済のデザインを Viewer 表示するものです。</p>
<p>Simple Viewer と Hub Browser の GitHub リポジトリは、APS ポータルの <a href="https://aps.autodesk.com/code-samples" rel="noopener" target="_blank">Code Samples</a> ページからアクセスすることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860f63f77200d-pi" style="display: inline;"><img alt="Code_samples" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860f63f77200d img-responsive" src="/assets/image_191066.jpg" title="Code_samples" /></a></p>
<ul>
<li><a href="https://github.com/autodesk-platform-services/aps-simple-viewer-dotnet" rel="noopener" target="_blank">GitHub - Simple Viewer (.NET)</a></li>
<li><a href="https://github.com/autodesk-platform-services/aps-simple-viewer-nodejs" rel="noopener" target="_blank">GitHub - Simple Viewer (Node.js)</a></li>
<li><a href="https://github.com/autodesk-platform-services/aps-hubs-browser-dotnet" rel="noopener" target="_blank">GitHub - Hubs Browser (.NET)</a></li>
<li><a href="https://github.com/autodesk-platform-services/aps-hubs-browser-nodejs" rel="noopener" target="_blank">GitHub - Hubs Browser (Node.js)</a></li>
</ul>
<p>Node.js バージョンのローカル環境でのセットアップと実行については、過去にブログ記事でも紹介していますので、必要に応じてご確認ください。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/01/aps-tutorial-simple-viewer.html" rel="noopener" target="_blank">APS チュートリアル：Simple Viewer</a></li>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/01/aps-tutorial-hub-browser.html" rel="noopener" target="_blank">APS チュートリアル：Hub Browser</a></li>
</ul>
<p>体系的なコード サンプルではなく、純粋に SDK がサポートする APS API の使用方法を把握するには、SDK 自体の GitHub リポジトリを参照することも可能です。APS Node.js SDK と APS .NET SDK のリポジトリは、それぞれ、次のとおりです。</p>
<ul>
<li><a href="https://github.com/autodesk-platform-services/aps-sdk-node" rel="noopener" target="_blank">GitHub - The official APS SDK for Nodejs</a></li>
<li><a href="https://github.com/autodesk-platform-services/aps-sdk-net" rel="noopener" target="_blank">GitHub - The official APS SDK for .NET</a></li>
</ul>
<p>各 GitHub リポジトリには、SDK API を使用する最小コードが用意されています。単純にパラメーターなどを確認する際にも役立つはずです。</p>
<ul>
<li><a href="https://github.com/autodesk-platform-services/aps-sdk-node/tree/main/samples" rel="noopener" target="_blank">aps-sdk-node/samples at main</a></li>
<li><a href="https://github.com/autodesk-platform-services/aps-sdk-net/tree/main/samples" rel="noopener" target="_blank">aps-sdk-net/samples at main</a></li>
</ul>
<p>APS Node.js SDK では、拡張子が .ts、<a href="https://ja.wikipedia.org/wiki/TypeScript" rel="noopener" target="_blank">TypeScript</a> になっていることがわかります。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3c893bc200c-pi" style="display: inline;"><img alt="Samples" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3c893bc200c img-responsive" src="/assets/image_120496.jpg" title="Samples" /></a></p>
<p>By Toshiaki Isezaki</p>
