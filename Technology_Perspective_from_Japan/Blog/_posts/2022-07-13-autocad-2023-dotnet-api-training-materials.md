---
layout: "post"
title: "AutoCAD 2023 .NET API トレーニング マテリアル"
date: "2022-07-13 00:07:31"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/07/autocad-2023-dotnet_api-training-materials.html "
typepad_basename: "autocad-2023-dotnet_api-training-materials"
typepad_status: "Publish"
---

<p>AutoCAD 2023 用にチューニングした AutoCAD .NET API のトレーニング マテリアルを公開いたします。マテリアルでご紹介している内容の前提は次のとおりです。</p>
<ul>
<li><a href="https://www.autodesk.com/developer-network/platform-technologies/autocad" rel="noopener noreferrer" target="_blank">https://www.autodesk.com/developer-network/platform-technologies/autocad</a>&#0160;で公開している&#0160;<a href="https://github.com/ADN-DevTech/AutoCAD-Net-Wizards/raw/ForAutoCAD2023/AutoCADNetWizardsInstaller/AutoCAD_2023_dotnet_wizards.zip" rel="noopener" target="_blank">AutoCAD 2023 DotNet Wizards</a>&#0160;を利用しています。</li>
<li>AutoCAD VBA をお使いの方向けに Visual Basic .NET を使っています。また、.NET Framework の利点や VBA との違いについても触れています。</li>
<li>C# をお使いの方や VBA を特に意識されていない方は、製品側のオンライン ヘルプ&#0160;<a href="http://help.autodesk.com/view/OARX/2023/JPN/?guid=GUID-C3F3C736-40CF-44A0-9210-55F6A939B6F2" rel="noopener noreferrer" target="_blank">Managed .NET 開発者用ガイド(.NET)</a>&#0160;をご参照いただくことも出来ます。</li>
<li>&quot;C:\ObjectARX 2023” に ObjectARX 2023 SDK が、&quot;C:\Program Files\Autodesk\AutoCAD 2023&quot; に AutoCAD 2023 がインストールされている環境でサンプル プロジェクトを作成、同梱しています。これ以外のパスで ObjectARX 2023 SDK と AutoCAD 2023 をインストールされている環境では、Visual Studio 2019 version 16 以降のバージョンでも正しく同プロジェクトを開けなかったり、ビルドが出来なかったりする可能性があります。ご注意ください。</li>
<li>AutoCAD 2023 DotNet Wizards&#0160; や .NET Framework 4.8 のインストールについては、<a href="https://adndevblog.typepad.com/technology_perspective/2022/04/autocad-2023-interoperability-for-customization.html" rel="noopener" target="_blank">AutoCAD 2023 のカスタマイズ互換性 </a>のブログ記事もご参照ください。</li>
</ul>
<hr />
<p><strong>AutoCAD 2023 .NET API トレーニング マテリアル</strong></p>
<p>AutoCAD 2023 .NET API トレーニング マテリアルは、次のリンクからダウンロードすることが出来ます。&#0160;</p>
<p><span class="asset  asset-generic at-xid-6a0167607c2431970b02942faae827200c img-responsive"><a href="https://adndevblog.typepad.com/files/autocad-2023-dotnet-api-training.zip"><strong>AutoCAD 2023 dotNET API トレーニング マテリアル</strong> をダウンロード</a></span><a href="https://adndevblog.typepad.com/files/autocad-2022-dotnet-api-training.zip"></a><a href="https://adndevblog.typepad.com/files/autocad-2021-dotnet-api-training.zip" rel="noopener" target="_blank"></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e155ba69200b-pi" style="display: inline;"><img alt="Dotnet_api_training" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e155ba69200b image-full img-responsive" src="/assets/image_995415.jpg" title="Dotnet_api_training" /></a></p>
<p>autocad-2023-dotnet-api-training.zip を展開すると、AutoCAD 2023 dotNET API Training フォルダの下に 3 つのファイルとフォルダが展開されます。</p>
<ul>
<li><strong>dotNET API Guide.chm ファイル</strong><br />AutoCAD .NET API ガイドブック と AutoCAD .NET API トレーニング の 2つのパートに分割して記述された CHM 形式のトレーニング マテリアルです。AutoCAD .NET API トレーニング配下の Lesson 1 ～ Lesson 8 に沿って &#0160;AutoCAD .NET API ガイドブックの内容をトレースすることが出来ます。<br />
<ul>
<li>Lesson 1 プロジェクトとコマンドの作成</li>
<li>Lesson 2 モデル空間に円を追加</li>
<li>Lesson 3 ユーザ対話で円を追加</li>
<li>Lesson 4 選択したオブジェクトの色を変更</li>
<li>Lesson 5 パレット ダイアログの作成</li>
<li>Lesson 6 作成した円の画層を指定</li>
<li>Lesson 7 イベント ハンドリングの監視</li>
<li>Lesson 8 拡張エンティティ データの操作<br /><br /></li>
</ul>
</li>
<li><strong>AutoCAD 2023 .NET API Training.pdf ファイル</strong><br />dotNET API Guide.chm ファイルを補完する PowerPoint プレゼンテーションの PDF 版です。<br /><br /></li>
<li><strong>dotNETGuideProject フォルダ</strong><br />AutoCAD .NET API トレーニング パートの回答コードと AutoCAD .NET API ガイドブックに記載された一部サンプル コードを内包する Visual Studio 2019 プロジェクトが含まれます。</li>
</ul>
<hr />
<p>トレーニング マテリアルのダウンロード後、dotNET API Guide.chm ファイルを開いてもコンテンツが空白で何も表示されない場合には、エクスプローラ上でマウス右クリックで .chm ファイルのプロパティを表示させて、「許可する」にチェックを入れてみてください。ブロックされたコンテンツが表示されるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942faae86f200c-pi" style="display: inline;"><img alt="Accept_chm" class="asset  asset-image at-xid-6a0167607c2431970b02942faae86f200c img-responsive" src="/assets/image_839803.jpg" style="width: 350px;" title="Accept_chm" /></a></p>
<p>By Toshiaki Isezaki</p>
