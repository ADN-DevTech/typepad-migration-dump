---
layout: "post"
title: "Visual Studio Add-in Wizards for Revit 2016 の入手方法"
date: "2015-07-24 01:17:18"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/07/visual-studio-add-in-wizards-for-revit-2016-installation.html "
typepad_basename: "visual-studio-add-in-wizards-for-revit-2016-installation"
typepad_status: "Publish"
---

<p>Revit 2016 アドイン開発に対応した Visual Studio 2012 のテンプレートが、弊社エンジニアによりリリースされております。このテンプレートをインストールすることで、新規プロジェクトの作成ウィザードから、プロジェクトの初期設定やアドインマニフェストファイル、スケルトンコードを自動生成することができます。</p>
<p>このテンプレートを使用してプロジェクトを作成すると、デバッグ開始をクリックするだけで、Revit 2016 が起動し、アドインがロードされ、外部コマンドまたは外部アプリケーションを実行することができます。そのため、検証用にサンプルコードを作成する際にも、とても便利です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b33ab6970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Visual Studio Add-in Wizards for Revit 2016_1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b33ab6970b image-full img-responsive" src="/assets/image_976406.jpg" title="Visual Studio Add-in Wizards for Revit 2016_1" /></a></p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7b34117970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Visual Studio Add-in Wizards for Revit 2016_2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7b34117970b image-full img-responsive" src="/assets/image_829340.jpg" title="Visual Studio Add-in Wizards for Revit 2016_2" /></a></p>
<p><strong>入手方法</strong></p>
<p>以下のブログ記事から C# 版 および VB 版の ZIP ファイルをそれぞれダウンロードすることができます。</p>
<ul>
<li>ブログ記事 リンク<br /><a href="http://thebuildingcoder.typepad.com/blog/2015/05/autodesk-university-q1-adn-labs-and-wizard-update.html#5" target="_blank">Updated Visual Studio Add-in Wizards for Revit 2016<br /><br /></a></li>
<li>C# 版 ダウンロード リンク<br /><a href="http://thebuildingcoder.typepad.com/files/revit2016addinwizardcs1.zip" target="_blank">Revit2016AddinWizardCs1.zip<br /><br /></a></li>
<li>Visual Basic 版 ダウンロード リンク<br /><a href="http://thebuildingcoder.typepad.com/files/revit2016addinwizardvb1.zip" target="_blank">Revit2016AddinWizardVb1.zip</a></li>
</ul>
<p><strong>インストール方法</strong></p>
<p>ダウンロードした ZIP ファイルを、解凍せずにそのまま以下のディレクトリに配置し、Visual Studio を再起動します。</p>
<ul>
<li>Revit2016AddinWizardCs1.zip の配置先<br /><span style="color: #111111;">[My Documents]\Visual Studio 2012\Templates\ProjectTemplates\Visual C#</span><br /><br /></li>
<li>Revit2016AddinWizardVb1.zip&#0160;の配置先<br /><span style="color: #111111;">[My Documents]\Visual Studio 2012\Templates\ProjectTemplates\Visual Basic</span></li>
</ul>
<p><strong>Visual Studio Add-in Wizards for Revit 2016 の機能</strong></p>
<ul>
<li>スケルトンコードの自動生成</li>
<ul>
<li>外部コマンド： Command.cs</li>
<li>外部アプリケーション： App.cs</li>
</ul>
<li>アドインマニフェストファイルの自動生成</li>
<li>Revit SDK の参照設定</li>
<li>ビルド実行後の DLL ファイルおよびマニフェストファイルのデプロイ</li>
<li>デバッグ実行時の Revit.exe 起動設定</li>
</ul>
<p>まだインストールされていない方は、ぜひ Revit アドイン開発にご活用ください。</p>
<p>By Ryuji Ogasawara</p>
