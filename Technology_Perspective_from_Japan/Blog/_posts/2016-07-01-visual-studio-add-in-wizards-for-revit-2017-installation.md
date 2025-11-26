---
layout: "post"
title: "Revit 2017 対応版 Visual Studio 2015 Add-in Wizards の入手方法とデバッグ実行時の注意点"
date: "2016-07-01 01:45:49"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/06/visual-studio-add-in-wizards-for-revit-2017-installation.html "
typepad_basename: "visual-studio-add-in-wizards-for-revit-2017-installation"
typepad_status: "Publish"
---

<p>Revit 2017 アドイン開発に対応した Visual Studio 2015 のテンプレートが、弊社エンジニアによりリリースされております。</p>
<p>このテンプレートをインストールすることで、新規プロジェクトの作成ウィザードから、プロジェクトの初期設定やアドインマニフェストファイル、スケルトンコードを自動生成することができます。 このテンプレートを使用してプロジェクトを作成し、デバッグモードで実行すると、Revit 2017 が自動的に起動し、アドインがロードされ、外部コマンドまたは外部アプリケーションをデバッグすることができます。</p>
<p>そのため、検証用にサンプルコードを作成する際にも、とても便利です。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09195aa8970d-pi" style="display: inline;"><img alt="Revit2017_wizard01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09195aa8970d image-full img-responsive" src="/assets/image_647624.jpg" title="Revit2017_wizard01" /></a></p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>入手方法</strong></span></p>
<p>以下のブログ記事から C# 版 および VB 版の ZIP ファイルをそれぞれダウンロードすることができます。</p>
<ul>
<li>ブログ記事 リンク<br /><a href="http://thebuildingcoder.typepad.com/blog/2016/05/visual-studio-vb-and-c-net-revit-2017-add-in-wizards.html">Visual Studio 2015 Revit 2017 Add-in Wizards<br /></a></li>
<li>C# 版 ダウンロード リンク<br /><a href="http://thebuildingcoder.typepad.com/files/revit2017addinwizardcs0.zip">Revit2017AddinWizardCs0.zip<br /></a></li>
<li>Visual Basic 版 ダウンロード リンク<br /><a href="http://thebuildingcoder.typepad.com/files/revit2017addinwizardvb0.zip">Revit2017AddinWizardVb0.zip<br /></a></li>
<li>最新版 2017.0.0.1 ソースコードのダウンロード リンク<br /><a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/releases/tag/2017.0.0.1">jeremytammik/VisualStudioRevitAddinWizard</a></li>
</ul>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>インストール方法</strong></span></p>
<p>ダウンロードした ZIP ファイルを、解凍せずにそのまま以下のディレクトリに配置し、Visual Studio を再起動します。</p>
<ul>
<li>Revit2017AddinWizardCs1.zip の配置先<br />[My Documents]\Visual Studio 2015\Templates\ProjectTemplates\Visual C#<br /><br /></li>
<li>Revit2017AddinWizardVb1.zip の配置先<br />[My Documents]\Visual Studio 2015\Templates\ProjectTemplates\Visual Basic</li>
</ul>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>Visual Studio Add-in Wizards for Revit 2017 の機能</strong></span></p>
<ul>
<li>スケルトンコードの自動生成</li>
<li>外部コマンド： Command.cs</li>
<li>外部アプリケーション： App.cs</li>
<li>アドインマニフェストファイルの自動生成</li>
<li>Revit SDK の参照設定</li>
<li>ビルド実行後の DLL ファイルおよびマニフェストファイルのデプロイ</li>
<li>デバッグ実行時の Revit.exe 起動設定</li>
</ul>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ffb96e970c-pi" style="display: inline;"><img alt="Revit2017_wizard02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ffb96e970c image-full img-responsive" src="/assets/image_765574.jpg" title="Revit2017_wizard02" /></a></p>
<p>まだインストールされていない方は、ぜひ Revit アドイン開発にご活用ください。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>デバッグ実行時の注意点</strong></span></p>
<p>Visual Studio 2015 で Revit 2017 アドインのプロジェクトをデバッグ実行する際に、下記のようなエラーが表示されて Revit 2017 がデバッグモードで起動しない場合がございます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09195ab7970d-pi" style="display: inline;"><img alt="Revit2017_wizard03" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09195ab7970d image-full img-responsive" src="/assets/image_796248.jpg" title="Revit2017_wizard03" /></a></p>
<p>この問題を回避するには、次の 2 つのいずれかをプロジェクト設定に加えてください。</p>
<ol>
<li>[ツール] &gt;&gt; [オプション] メニューから [オプション] ダイアログを表示させ、[デバッグ] &gt;&gt; [全般] の 「マネージ互換モードの使用」 を有効にする。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09195c43970d-pi" style="display: inline;"><img alt="Revit2017_wizard05" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09195c43970d image-full img-responsive" src="/assets/image_798963.jpg" title="Revit2017_wizard05" /><br /></a></li>
<li>プロジェクトのプロパティ設定を開き、[デバッグ]タブから、「ネイティブコードのデバッグを有効にする」にチェックを入れて有効にする。<br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09195aee970d-pi" style="display: inline;"><img alt="Revit2017_wizard04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09195aee970d image-full img-responsive" src="/assets/image_303844.jpg" title="Revit2017_wizard04" /></a></li>
</ol>
<p>By Ryuji Ogasawara</p>
