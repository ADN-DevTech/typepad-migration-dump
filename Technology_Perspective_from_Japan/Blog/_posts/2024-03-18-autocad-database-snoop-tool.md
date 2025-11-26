---
layout: "post"
title: "AutoCAD 図面データベース検査ツール"
date: "2024-03-18 00:12:58"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/03/autocad-database-snoop-tool.html "
typepad_basename: "autocad-database-snoop-tool"
typepad_status: "Publish"
---

<p>AutoCAD でアドイン アプリを開発していると、オブジェクトが参照している図面データベース内の他のオブジェクトの情報を把握したい時があります。対象が非グラフィカル オブジェクトだと、情報を得るためにプログラムを作る必要が出てくる場合もあります。</p>
<p>そのような場面でツールとして利用出来るサンプルがあります。ObjectARX SDK に同梱されている ArxDbg サンプルです。ObjectARX SDK for AutoCAD 2024 の場合、<em>&lt;ObjectARX SDK インストール フォルダー&gt;\samples\database\ARXDBG</em>&#0160;フォルダーにあります。</p>
<p>Visual Studio でプロジェクトを開いてビルドすると、ArxDbg.arx が生成されます。この .arx ファイルを APPLOAD コマンドなどでロードすると、マウス右クリックでコンテキストメニューが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ad2132200d-pi" style="display: inline;"><img alt="Arxdbg_menu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ad2132200d img-responsive" src="/assets/image_528104.jpg" title="Arxdbg_menu" /></a></p>
<p>もともと、ArxDbg は ObjectARX が提供する API の利用方法を示す目的で用意されたものですが、前述の「検査」ツールとしても利用することが出来ます。</p>
<p>例えば、<strong>Database Info...</strong> では、通常、あまり意識することのない Named Object Dictionary 配下のディクショナリをチェックするすることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3acca26200b-pi" style="display: inline;"><img alt="Dictinaries" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3acca26200b image-full img-responsive" src="/assets/image_78623.jpg" title="Dictinaries" /></a></p>
<p><strong>Entity Info...</strong> で寸法オブジェクトを選択すると、同オブジェクトのプロパティを AcDbEntity レベルで表示します。この状態で [DXF] ボタンをクリックすると、DXF ファイルに出力した際と同じように DXF グループコード毎にプロパティを表示します。AutoLISP でエンティティ リストを扱う場合にも、DXF グループコードと値の関係を把握出来るので便利です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ad2172200d-pi" style="display: inline;"><img alt="Dxf" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ad2172200d image-full img-responsive" src="/assets/image_101376.jpg" title="Dxf" /></a></p>
<p>他にも、さまざまな機能があります。詳細は、<em>&lt;ObjectARX SDK インストール フォルダー&gt;\samples\database\ARXDBG</em> フォルダー内の <strong>ArxDbg.doc</strong>&#0160;ドキュメントをご確認ください。</p>
<hr />
<p>ArcDbg サンプルのような検査ツール サンプルには、AutoCAD .NET API を使ったエディションも存在します。<a href="https://github.com/ADN-DevTech/MgdDbg" rel="noopener" target="_blank">MgdDbg サンプル（https://github.com/ADN-DevTech/MgdDbg）</a>です。このサンプルは ObjectARX SDK には同梱されず、GitHub リポジトリで公開されています。</p>
<p>ローカルに git コマンドでクローン（<em>git clone https://github.com/ADN-DevTech/MgdDbg.git</em>）したソリューション MgdDbg.sln を Visual Studio で開いてビルドすると、MgdDbg.dll が生成されます。このアセンブリ ファイルを NETLOAD コマンドでロードすると、マウス右クリックでコンテキストメニューが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3acca85200b-pi" style="display: inline;"><img alt="Msgdbg_menu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3acca85200b image-full img-responsive" src="/assets/image_253496.jpg" title="Msgdbg_menu" /></a></p>
<p>一見、ArxDbg に比べてメニューの数が 少なく、機能不足な印象ですが、.NET らしいダイナミックな実装を見ることが出来ます。</p>
<p>例えば、<strong>Snoop Entities...</strong> でブロック参照（BlockReference）を選択すると、ブロック参照のプロパティを表示するダイアログ内の操作で、定義元のブロック定義（BlockTableRecord）のプロパティを表示、また、ブロック定義内の個々の要素（オブジェクト）のプロパティをシームレスに遷移して表示することが出来ます。ちょうど、Revit アドイン開発時に利用する <a href="https://github.com/jeremytammik/RevitLookup" rel="noopener" target="_blank">Revit Lookup ツール</a>に似た動きをします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3accaf7200b-pi" style="display: inline;"><img alt="Mgddbg" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3accaf7200b image-full img-responsive" src="/assets/image_416493.jpg" title="Mgddbg" /></a></p>
<hr />
<p>MgdDbg プロジェクトを GitHub リポジトリから ZIP ファイルとしてダウンロードしてビルドした場合には、ローカル環境でアセンブリの参照設定を解決しても、ビルド時に「ファイル XXXX を処理できませんでした。インターネットまたは制限付きゾーン内にあるか、ファイルに Web のマークがあるためです。これらのファイルを処理するには、Web のマークを削除してください。」エラーが発生してしまいます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3accb00200b-pi" style="display: inline;"><img alt="Error" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3accb00200b image-full img-responsive" src="/assets/image_153301.jpg" title="Error" /></a></p>
<p>このエラーを解決するには、エラーの出ているファイルを個々にエクスプローラーで右クリックして、プロパティ ダイアログで「許可する」にチェックする必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a8fdda200c-pi" style="display: inline;"><img alt="Permission" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a8fdda200c img-responsive" src="/assets/image_606047.jpg" title="Permission" /></a></p>
<p>By Toshiaki Isezaki</p>
