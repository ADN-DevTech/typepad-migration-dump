---
layout: "post"
title: "Hub アップロード/ダウンロード - オープンソース ツール ユーティリティ"
date: "2024-08-26 00:00:46"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/08/open-source-utility-downloadupload-files-hubs.html "
typepad_basename: "open-source-utility-downloadupload-files-hubs"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0cdaa33200d-pi" style="display: inline;"><img alt="Aps-hubs-bulk-files-manager" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0cdaa33200d image-full img-responsive" src="/assets/image_469584.jpg" title="Aps-hubs-bulk-files-manager" /></a></p>
<p>オートデスクのソリューション アーキテクトである&#0160;<a  _istranslated="1" href="https://github.com/JKAdskHub" rel="noopener" target="_blank">Jayakrishna Kondapu</a> によって作成され、ファイル ダウンロード/アップロード用のオープンソース ユーティリティ <strong>Bulk File Manager</strong> がリリースされました。ローカルマシンと Autodesk BIM 360、Autodesk Construction Cloud（ACC）などのフォルダ間でファイルをアップロード/ダウンロードするためのデスクトップ ベース アプリケーションです。</p>
<p>Bulk File Manager は、次の GitHub リポジトリで公開されています。同ページに説明のある手順に沿ってビルドをおこなってください。</p>
<ul>
<li><a  _msthash="86"  _msttexthash="4001361" href="https://github.com/autodesk-platform-services/aps-hubs-bulk-files-manager" rel="noopener" target="_blank">https://github.com/autodesk-platform-services/aps-hubs-bulk-files-manager</a></li>
</ul>
<p>Bulk File Manager は、アプリケーションコ ンテキスト認証（2-legged）とユーザー コンテキスト認証（3-legged）の両方がサポートされています。</p>
<p>ユーザーは、1 つのローカルフォルダ内のファイルをバッチで Hub 下のフォルダにアップロードすることが出来ます。ジョブは [UPLOAD HISTORY] タブで管理されます。同タブはジョブのステータスを示し、ジョブが失敗した場合は、新しい再試行をトリガーすることも出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b9d889200c-pi" style="display: inline;"><img alt="Upload-history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b9d889200c image-full img-responsive" src="/assets/image_940548.jpg" title="Upload-history" /></a></p>
<p>&#0160;[DOWNLOAD HISTORY] タブでは、単一のフォルダまたは複数のフォルダからファイルをダウンロードすることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b9d858200c-pi" style="display: inline;"><img alt="Download-history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b9d858200c image-full img-responsive" src="/assets/image_277528.jpg" title="Download-history" /></a></p>
<p>複数のフォルダを操作するには、まずジョブで使用可能なすべてのプロジェクトを Excel ファイルに抽出します。ダウンロードするプロジェクトを確認し、Excel フ ァイルを更新します。最後にダウンロードを実行します。一括ダウンロード操作機能は、多数のダウンロード ジョブを同時に作成するように設計されています。同様に、ダウンロード履歴は、ユーザーがジョブを監視するのに便利なジョブの状態も提供します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bd7438200b-pi" style="display: inline;"><img alt="Bulk-download" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bd7438200b image-full img-responsive" src="/assets/image_392961.jpg" title="Bulk-download" /></a></p>
<p>追加のオプションにより、アップロードとダウンロードのジョブに関するタイムラインダッシュボードを監視したり、利用可能な Hub（アカウント）、プロジェクト、フォルダを Excel ファイルに書き出したり、各 HTTP トラフィックのログを追跡するコンソールを実行したりすることが出来ます。</p>
<p>アプリ（現在のバージョン）は、NET 8 と APS の最新の新しい SDK を利用して構築されています。また、いくつかのサードパーティ ライブラリを採用しており、Electron skeleton によってパッケージ化されています。このユーティリティは、ユーザーがファイルをバッチアップロード/ダウンロードするための製品レベルのツールにすることが出来ます。また、パワーユーザーや開発者が機能を追加/調整できるように拡張することもできます。</p>
<p>詳細については、<a  _istranslated="1" href="https://github.com/autodesk-platform-services/aps-hubs-bulk-files-manager/blob/main/Documentation/user-guide.md" rel="noopener" target="_blank">User Guide</a> または <a  _istranslated="1" href="https://github.com/autodesk-platform-services/aps-hubs-bulk-files-manager/blob/main/Documentation/developer-guide.md" rel="noopener" target="_blank">Developer Guide</a> をご確認ください。ご不明な点がございましたら、このリポジトリの Issues をログに記録をお願いします。</p>
<ul>
<li>Bulk File Manager&#0160;アプリ（Bulk-File-Manager.exe）利用には、GitHub リポジトリ <a  _msthash="86"  _msttexthash="4001361" href="https://github.com/autodesk-platform-services/aps-hubs-bulk-files-manager" rel="noopener" target="_blank">https://github.com/autodesk-platform-services/aps-hubs-bulk-files-manager</a> からのクローンとビルドが必要です。</li>
<li>アプリに設定する Client ID は、Callback URL に http://localhost:8083/code を持ち、かつ、<a href="https://adndevblog.typepad.com/technology_perspective/2024/02/acc-new-custom-integration-ui.html" rel="noopener" target="_blank">カスタム統合</a>の処理が必要です。</li>
<li>Bulk File Manager&#0160;アプリ（Bulk-File-Manager.exe）の実行がセキュリティ ソフトウェアにブロックされる場合は、セキュリティ ソフトウェアを管理している IT 部門にご相談ください。</li>
</ul>
<p>※ 本記事は <a href="https://aps.autodesk.com/blog/open-source-utility-downloadupload-files-hubs" rel="noopener" target="_blank">Open Source Utility for Download/Upload Files with Hubs | Autodesk Platform Services</a> から転写・補足・意訳したものです。</p>
<p>By Toshiaki Isezaki</p>
