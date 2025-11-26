---
layout: "post"
title: "Revit Visual Studio .NET Add-in Wizard の入手方法"
date: "2019-05-31 02:28:53"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2019/05/visual-studio-add-in-wizards-for-revit-2020-installation.html "
typepad_basename: "visual-studio-add-in-wizards-for-revit-2020-installation"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4af54d0200b-pi" style="float: right;"><img alt="Revit-icon-128px" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4af54d0200b img-responsive" src="/assets/image_671402.jpg" style="margin: 0px 0px 5px 5px;" title="Revit-icon-128px" /></a><span style="color: #ff0000;"><strong>※ この記事は、Revit 2026 までのバージョンに対応しています。</strong></span></p>
<p>Revit アドイン開発者向けの Visual Studio のプロジェクトテンプレートが、弊社エンジニアによりリリースされております。今回は、Revit 対応の Visual Studio .NET Add-in Wizard の入手方法とインストール方法について解説いたします。</p>
<p>このプロジェクトテンプレートをインストールすることで、Visual Studio の新規プロジェクトの作成から、プロジェクトの初期設定やアドインマニフェストファイル、スケルトンコードを自動生成することができます。</p>
<p>プロジェクトの作成後、デバッグモードを実行すると、Revit が自動的に起動し、アドインがロードされ、外部コマンドまたは外部アプリケーションをデバッグすることができます。</p>
<p>そのため、検証用にサンプルコードを作成する際にも、とても便利です。&#0160;</p>
<ul>
<li>スケルトンコードの自動生成</li>
<li>外部コマンド： Command.cs</li>
<li>外部アプリケーション： App.cs</li>
<li>アドインマニフェストファイルの自動生成</li>
<li>Revit SDK の参照設定</li>
<li>ビルド実行後の DLL ファイルおよびマニフェストファイルのデプロイ</li>
<li>デバッグ実行時の Revit.exe 起動設定</li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4617a19200c-pi" style="display: inline;"><img alt="Revit2020 Wizards1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4617a19200c image-full img-responsive" src="/assets/image_230423.jpg" title="Revit2020 Wizards1" /></a><br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4af4e81200b-pi" style="display: inline;"><img alt="Revit2020 Wizards2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4af4e81200b image-full img-responsive" src="/assets/image_940578.jpg" title="Revit2020 Wizards2" /></a><br /><span style="font-size: 14pt;"><strong><br />入手方法</strong></span></p>
<p>以下の GitHub リポジトリから対象のバージョンのリリースをダウンロードすることができます。</p>
<p>Visual Studio Revit Add-in Templates</p>
<ul>
<li><a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard">https://github.com/jeremytammik/VisualStudioRevitAddinWizard</a></li>
</ul>
<p>Revit バージョン毎に対応の最新リリースをダウンロードします。</p>
<ul>
<li><a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases">https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases</a></li>
</ul>
<p>Revit 各バージョンのビルド済みプロジェクトテンプレート（C#）</p>
<ul>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3ac0dbc200c img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3b46193200b img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3d90400200c img-responsive"><a href="https://adndevblog.typepad.com/files/revit-2026-addin.zip">Revit 2026 Addin Wizard をダウンロード ※要 Visual Studio 2022 (17.8 以降) .NET 8</a></span></span></span></li>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3ac0dbc200c img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3b46193200b img-responsive"><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3d90400200c img-responsive"> <span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3d90409200c img-responsive"><a href="https://adndevblog.typepad.com/files/revit-2025-addin-1.zip">Revit 2025 Addin Wizard をダウンロード ※要 Visual Studio 2022 (17.8 以降) .NET 8</a></span></span></span></span><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3ac0dbc200c img-responsive"><br /></span></li>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3ac0dbc200c img-responsive"><a href="https://adndevblog.typepad.com/files/revit2024addinwizardcs0.zip">Revit2024AddinWizardCs0をダウンロード</a></span></li>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3ac0dc1200c img-responsive"><a href="https://adndevblog.typepad.com/files/revit2023addinwizardcs0.zip">Revit2023AddinWizardCs0をダウンロード</a></span></li>
<li><span class="asset  asset-generic at-xid-6a0167607c2431970b02c8d3afd493200b img-responsive"><a href="https://adndevblog.typepad.com/files/revit2022addinwizardcs0.zip">Revit2022AddinWizardCs0をダウンロード</a></span></li>
</ul>
<hr />
<p><span style="font-size: 14pt;"><strong>インストール方法（Revit 2023 まで）</strong></span></p>
<p>インストールの大まかな流れは以下の通りです。</p>
<ol>
<li>ZIP ファイルを解凍します。<br /><br /></li>
<li>それぞれの開発言語フォルダ内にある、アドインマニフェストファイル（RegisterAddin.addin）の VendorId タグの値を変更します。<br /><br /></li>
<li>install.bat をテキストエディタで開き、下記内容を確認し適宜修正します。<br /><br />下記、14行目の[D=] 以下には、プロジェクトテンプレートを配置するフォルダのパスを指定します。ここで指定するパスは、Visual Studio のオプションダイアログ-&gt;[場所]で設定するパス（ユーザープロジェクトテンプレートの場所）と一致する必要があります。<br /><br />14行目 set &quot;D=Y:\Documents\Visual Studio %1\Templates\ProjectTemplates&quot;（修正前）<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ac0dab200c-pi" style="display: inline;"><img alt="VisualStudioProjectTemplateLocation" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ac0dab200c image-full img-responsive" src="/assets/image_59292.jpg" title="VisualStudioProjectTemplateLocation" /></a><br /><br /></li>
<li>コマンドプロンプトを起動し、install.bat のあるディレクトリに移動して、install.bat の後に引数としてインストールする Visual Studio のバージョンを指定して実行します。※ Revit のバージョンではありません。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ac0dba200c-pi" style="display: inline;"><img alt="VisualStudioProjectTemplateCmd" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ac0dba200c image-full img-responsive" src="/assets/image_477571.jpg" title="VisualStudioProjectTemplateCmd" /></a><br /><br /></li>
<li>Visual Studio を起動し、[新しいプロジェクト]を開始すると、テンプレートの選択ダイアログに、Revit 20xx Addin が追加されていることを確認します。</li>
</ol>
<hr />
<p><span style="font-size: 14pt;"><strong>インストール方法（Revit 2024, 2025, 2026）</strong></span></p>
<ol>
<li>上記、Revit 各バージョンのビルド済みプロジェクトテンプレート(ZIPファイル)をダウンロードします。</li>
<li>Visual Studio を起動し、[ツール]-&gt;[オプション]からオプションダイアログを表示します。</li>
<li>[場所]セクションの[ユーザー プロジェクト テンプレートの場所]で指定されているプロジェクトテンプレートの配置場所を確認します。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c48cb2200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VisualStudioTemplatePath" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c48cb2200d image-full img-responsive" src="/assets/image_102654.jpg" title="VisualStudioTemplatePath" /></a><br /><br /></li>
<li>上記、オプションで指定されている配置場所に、ダウンロードしたプロジェクトテンプレートの ZIP ファイルを配置します。Revit 2025 の場合は、現状、タグによる分類は未サポートとなっております。<br /><br /></li>
</ol>
<p>&#0160;</p>
<p>ぜひ Revit アドイン開発にご活用ください。</p>
<hr />
<p>By Ryuji Ogasawara</p>
