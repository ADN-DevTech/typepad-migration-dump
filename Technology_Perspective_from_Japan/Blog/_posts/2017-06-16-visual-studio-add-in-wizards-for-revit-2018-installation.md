---
layout: "post"
title: "Revit 2018 Visual Studio .NET Add-in Wizards の入手方法"
date: "2017-06-16 01:33:17"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/06/visual-studio-add-in-wizards-for-revit-2018-installation.html "
typepad_basename: "visual-studio-add-in-wizards-for-revit-2018-installation"
typepad_status: "Publish"
---

<p>Revit 2018 Visual Studio .NET Add-in Wizards の入手方法とインストール方法について解説いたします。先日ご案内した、<a href="http://adndevblog.typepad.com/technology_perspective/2017/06/revit-api-training-material.html">Revit API トレーニングマテリアル（日本語版）</a>にも同様の内容のテキストが同梱されております。</p>
<p><span style="font-size: 14pt;"><strong>Revit 2018 Visual Studio .NET Add-in Wizards</strong></span></p>
<p>Revit 2018 アドイン開発に対応した Visual Studio 2015 のテンプレートが、弊社エンジニアによりリリースされております。</p>
<p>このテンプレートをインストールすることで、新規プロジェクトの作成ウィザードから、プロジェクトの初期設定やアドインマニフェストファイル、スケルトンコードを自動生成することができます。 このテンプレートを使用してプロジェクトを作成し、デバッグモードで実行すると、Revit 2018 が自動的に起動し、アドインがロードされ、外部コマンドまたは外部アプリケーションをデバッグすることができます。</p>
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
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d28d3ae8970c-pi" style="display: inline;"><img alt="Revit2018_wizard01" class="asset  asset-image at-xid-6a0167607c2431970b01b8d28d3ae8970c img-responsive" src="/assets/image_460567.jpg" title="Revit2018_wizard01" /></a><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c902fe2d970b-pi" style="display: inline;"></a> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c902fe44970b-pi" style="display: inline;"><img alt="Revit2018_wizard02" class="asset  asset-image at-xid-6a0167607c2431970b01b7c902fe44970b img-responsive" src="/assets/image_309760.jpg" title="Revit2018_wizard02" /></a></p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>入手方法</strong></span></p>
<p>以下の GitHub リポジトリから対象のバージョンのリリースをダウンロードすることができます。</p>
<p style="padding-left: 30px;">Visual Studio Revit Add-in Templates<br /><a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard">https://github.com/jeremytammik/VisualStudioRevitAddinWizard</a></p>
<p style="padding-left: 30px;">Revit 2018 対応版<br /><a href="https://github.com/jeremytammik/VisualStudioRevitAddinWizard/archive/2018.0.0.0.zip">https://github.com/jeremytammik/VisualStudioRevitAddinWizard/archive/2018.0.0.0.zip</a></p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong>インストール方法</strong></span></p>
<p>下記の弊社エンジニアのブログ記事を参考に、記載されている手順に沿ってインストールを行います。</p>
<p style="padding-left: 30px;">Revit Add-In Wizard GitHub Installer<br /><a href="http://thebuildingcoder.typepad.com/blog/2015/08/revit-add-in-wizard-github-installer.html">http://thebuildingcoder.typepad.com/blog/2015/08/revit-add-in-wizard-github-installer.html</a></p>
<p>インストールの大まかな流れは以下の通りです。</p>
<ol>
<li>ZIP ファイルを解凍します。</li>
<li>それぞれの開発言語フォルダ内にある、アドインマニフェストファイル（RegisterAddin.addin）の VendorId タグの値を変更します。</li>
<li>Windows のコマンドプロンプトで zip コマンドを実行するために、下記のサイトから zip ユーティリティをダウンロードして、zip-3.0-setup.exe を実行してインストールを行います。<br /><br />Zip for Windows<br /><a href="http://gnuwin32.sourceforge.net/packages/zip.htm">http://gnuwin32.sourceforge.net/packages/zip.htm</a><br /><br /></li>
<li>Windows の環境変数[Path]に下記のパスを追加します。<br /><br />追加するパス-&gt; C:\Program Files (x86)\GnuWin32\bin<br /><br /></li>
<li>コマンドプロンプトを起動し、install.bat のあるディレクトリに移動して、install.bat を実行します。</li>
<li>Visual Studio を起動し、[新しいプロジェクト]を開始すると、テンプレートの選択ダイアログに、Revit 2018 Addin が追加されていることを確認します。</li>
</ol>
<p>ぜひ Revit アドイン開発にご活用ください。</p>
<p>By Ryuji Ogasawara</p>
