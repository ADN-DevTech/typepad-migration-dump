---
layout: "post"
title: "RevitLookup ツール for Revit 2018-2023 の入手方法と機能について"
date: "2019-06-07 01:45:46"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/06/revit-lookup-tool-for-revit-2018-2023.html "
typepad_basename: "revit-lookup-tool-for-revit-2018-2023"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4af54d0200b-pi" style="float: right;"><img alt="Revit-icon-128px" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4af54d0200b img-responsive" src="/assets/image_671402.jpg" style="margin: 0px 0px 5px 5px;" title="Revit-icon-128px" /></a><strong><span style="color: #ff0000;">※ この記事は、Revit 2023 までのバージョンに対応しています。</span></strong></p>
<p>RevitLookup ツールは、Revit の UI 上から、アプリケーション、ドキュメント、ファミリ、ジオメトリ等の、各オブジェクトのデータベース構造を確認できるアドインです。<br /><br />Revit API を利用してカスタマイズを行う開発者向けツールとして、弊社エンジニアが開発・配布しております。<br />以前は Revit SDK に同梱されておりましたが、現在は GitHub にて、VisualStudio のプロジェクトファイルが公開されており、必要に応じてダウンロードしてご利用いただけます。</p>
<p>それぞれ、Revit 2018-2023 に対応するアドインの VisualStudio プロジェクトファイルとインストーラが、以下の GitHub にて公開されております。</p>
<p><strong>RevitLookup トップページ</strong><br /><a href="https://github.com/jeremytammik/RevitLookup" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup</a></p>
<p><strong>リリース一覧</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup/releases</a></p>
<p><strong>Revit 2018 対応版</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2018.0.0.8">https://github.com/jeremytammik/RevitLookup/releases/tag/2018.0.0.8</a></p>
<p><strong>Revit 2019 対応版</strong><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2019.0.0.13" rel="noopener" target="_blank"><br />https://github.com/jeremytammik/RevitLookup/releases/tag/2019.0.0.13</a></p>
<p><strong>Revit 2020 対応版</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2020.0.0.4" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup/releases/tag/2020.0.0.4</a></p>
<p><strong>Revit 2021 対応版</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.13" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup/releases/tag/2021.0.0.13</a></p>
<p><strong>Revit 2022 対応版</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.4.1" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup/releases/tag/2022.0.4.1</a></p>
<p><strong>Revit 2023 対応版</strong><br /><a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2023.1.0" rel="noopener" target="_blank">https://github.com/jeremytammik/RevitLookup/releases/tag/2023.1.0</a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7a4e481970b-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a48c4356200d-pi" style="display: inline;"><img alt="Revit2020 Lookup1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a48c4356200d image-full img-responsive" src="/assets/image_988516.jpg" title="Revit2020 Lookup1" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7a4e481970b-pi" style="display: inline;"></a></p>
<p><strong>インストール方法</strong></p>
<p>上記、各バージョンの Release リンクから、MSI 形式のインストーラをダウロードし、インストーラを起動します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b0a2b4200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RevitLookupToolInstaller" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b0a2b4200c img-responsive" src="/assets/image_956073.jpg" title="RevitLookupToolInstaller" /></a></p>
<p>このインストーラで RevitLookup ツールをインストールすると、アドインファイルは下記のフォルダに配置されます。</p>
<ul>
<li>
<p>C:\ProgramData\Autodesk\Revit\Addins\20xx</p>
</li>
</ul>
<div><hr /></div>
<p><strong>RevitLookup ツールの機能</strong></p>
<p>※Revit バージョン毎に利用可能な機能は異なります。下記は Revit 2023 の機能です。</p>
<ul>
<li>Snoop Selection：選択されている要素のデータ構造を確認します。</li>
<li>Snoop Active View：現在アクティブなビューのデータ構造を確認します。</li>
<li>Snoop Active Document：現在アクティブなドキュメントのデータ構造を確認します。</li>
<li>Snoop Application：Revit アプリケーションのデータ構造を確認します。</li>
<li>Snoop Database：Revit プロジェクト内データベースを確認します。</li>
<li>Snoop Linked Element：Revit リンク内要素を選択して、その要素のデータ構造を確認します。</li>
<li>Snoop Dependent Elements：選択されている要素に依存する要素のデータ構造を確認します。</li>
<li>Snoop Pick Face：面を選択して、面のジオメトリに関する情報を確認します。</li>
<li>Snoop Pick Edge：線を選択して、線のジオメトリに関する情報を確認します。</li>
<li>Snoop Id：ElementId または、UniqueId を指定して、その要素のデータ構造を確認します。</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c487bc200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RevitLookupToolMenu" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c487bc200d image-full img-responsive" src="/assets/image_166092.jpg" title="RevitLookupToolMenu" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b0a32e200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="RevitLookupToolDialog1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b0a32e200c image-full img-responsive" src="/assets/image_21105.jpg" title="RevitLookupToolDialog1" /></a></p>
<div>ぜひご活用ください。</div>
<div><hr /></div>
<p>By Ryuji Ogasawara</p>
