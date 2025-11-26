---
layout: "post"
title: "「Visual StudioでInventor アドインテンプレートが出てこない」そんな時は"
date: "2025-07-28 01:04:20"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/07/in-case-inventor-addin-template-doesnot-appeared-on-visual-studio.html "
typepad_basename: "in-case-inventor-addin-template-doesnot-appeared-on-visual-studio"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860efe3ec200b-pi" style="display: inline;"><img alt="Title" class="asset  asset-image at-xid-6a0167607c2431970b02e860efe3ec200b img-responsive" src="/assets/image_288075.jpg" title="Title" /></a></p>
<p>Inventor SDKには、Visual StudioでC#やVB.NETを使ったアドインの開発時に便利な、プロジェクトテンプレートが含まれております。</p>
<p>プロジェクトテンプレートは、InventorのSDKフォルダ( C:\Users\Public\Documents\Autodesk\Inventor XXXX\SDK) 配下のdevelopertools.msiをインストールするとことでインストールされ、Visual Studioからプロジェクトの新規作成時にテンプレートを選択することが出来るようになります。</p>
<p>時折、Visual Studioでこのプロジェクトテンプレートが表示されないというお問い合わせをいただくことがありますので、本記事では対処方法についてご案内をしたいと思います。</p>
<p>&#0160;</p>
<p>通常developertools.msiをインストールすると、Visual Studioのテンプレートファイルは %USERPROFILE%\Documents\Visual Studio 2022\Templates\ProjectTemplates 配下に展開されます（Visual Studio 2022の場合）。</p>
<p>一方で、Visual Studioは設定によりテンプレートフォルダの場所を任意に変更することが出来ます。設定はVisual Studio 2022では、[ツール]-[オプション]からオプションダイアログを開いて、ダイアログの左のツリーで「プロジェクトおよびソリューション」-「場所」を選ぶと「ユーザテンプレートの場所」で確認できます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3d93299200c-pi" style="display: inline;"><img alt="Optiondialog" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3d93299200c img-responsive" src="/assets/image_397932.jpg" title="Optiondialog" /></a></p>
<p>「ユーザテンプレートの場所」が、%USERPROFILE%\Documents\Visual Studio 2022\Templates\ProjectTemplates ではない場所に設定されている場合、Visual Studioはdevelopertools.msiが展開したテンプレートを見つけることが出来ずに、プロジェクトの新規作成時の一覧に出てこない状態となります。</p>
<p>&#0160;</p>
<p>対応方法は、この「ユーザテンプレートの場所」で指定されているフォルダ配下に、%USERPROFILE%\Documents\Visual Studio 2022\Templates\ProjectTemplatesフォルダ配下のテンプレートファイル（VBInventorAddInTemplateXXXX.zip、VCSInventorAddInTemplateXXXX.zip)をコピーした後に、Visual Studioを再起動することで、プロジェクトの新規作成時にテンプレートファイルが選択できるようになります。</p>
<p>&#0160;</p>
<p>なお、developertools.msiをインストールするとプロジェクトテンプレートのほかに、サンプルコードやEventWatcherなどのツールも併せてSDKフォルダ配下に展開されますので、アドイン作成のサンプルコードが欲しいといった場合にはご活用ください。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
