---
layout: "post"
title: "Inventor 2025がリリースされました"
date: "2024-04-01 00:08:43"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/04/inventor-2025-released.html "
typepad_basename: "inventor-2025-released"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af1884200d-pi"><img alt="無題" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af1884200d img-responsive" src="/assets/image_121066.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="無題" /></a></p>
<p>Inventorの新バージョンとなる、Inventor 2025 がリリースされました。この記事ではInventor 2025の概要についてご紹介をいたします。</p>
<p>&#0160;</p>
<p>サポートされるプラットフォームは、2024に引き続き Windows 10 の 64 ビット版およびWindows 11 の 64 ビット版となります。</p>
<p>詳細なシステム要件については、オンラインドキュメントの以下のページをご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2025/JPN/?caas=caas/sfdcarticles/sfdcarticles/System-requirements-for-Autodesk-Inventor-2025.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 50px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_474393.jpg" style="width: 100%; height: auto; max-height: 50px; min-width: 0; border: 0 none; margin: 0;" width="50" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">System requirements for Autodesk Inventor 2025</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Operating System 64-bit Microsoft® Windows® 11 and Windows 10. See Autodesk&#39;s Product Support Lifecycle for support information.<br />CPU Recommended:3.0 GHz or greater, 4 or more cores</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>また、Inventor 2025からは、2024以前の .NET Framework系とは異なる系統のソフトウェアフレームワーク.NET 8を採用しております。これに伴い、既存のInventor SDKを用いた.NET Framework4.8 ベースのアドインプロジェクトは、.NET 8 へアップグレードして再ビルドする必要があります。.NET 8へのVisual Studio プロジェクトの移植については、別の記事にてご案内をする予定ですが、お急ぎの場合は英語となりますが以下オンラインのAPIリファレンスに移植手順が記載されておりますのでご参照ください</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2025/JPN/?guid=GUID-522FF5BC-7CC5-43D5-99B1-14840CF54A82" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 50px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_682025.jpg" style="width: 100%; height: auto; max-height: 50px; min-width: 0; border: 0 none; margin: 0;" width="50" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Port .Net Framework-based project to .Net</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventor 2025 has migrated from .Net Framework to .Net 8, it is recommended to port the .Net Framework-based projects to .Net 8 to keep compatibility. This article demonstrates how to port Inventor VB.net/C# projects from .Net Framework-based to .Net 8-based, and also C++/CLI settings for C++ projects which use managed code.<br /><br />Environment Preparation:</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>これまでのInventorのリリースと同様に、Inventorファイルの保存形式が変更されており、Inventor 2025で作成したファイルをInventor 2024で開こうとすると、以下のようなダイアログが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3aecbcd200b-pi" style="display: inline;"><img alt="FileVersion" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3aecbcd200b img-responsive" src="/assets/image_438585.jpg" title="FileVersion" /></a></p>
<p>ダイアログに記載のように、Inventor 2025で作成、保存したファイルを、Inventor 2024で開くためには、Inventor Interoperability 2025 コンポーネントの適用が必要となります。ダイアログでインストールボタンを押下するとコンポーネントがインストールされ、Inventor 2024でも開くことが出来るようになります。</p>
<p>Inventorのファイル互換性に関するトラブルシューティングについては、オンラインヘルプの以下のページをご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2025/JPN/?guid=GUID-489A3E1D-EB3E-4468-9592-F2EAD0E584DD" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">トラブル シューティング: 新しい Inventor ファイルを開く</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventor ファイルを開こうとするときに、開こうとする Inventor ファイルが現在のバージョンより新しいことを示すエラーが表示されます。 問題次のものを開くことができません。 現在のバージョンより 1 年以上新しいパーツまたはアセンブリ ファイル。現在のバージョンより 2 年以上新しいパーツまたはアセンブリ ファイル。新しいバージョンの Inventor の図面(.idw)またはプレゼンテーション(.ipn)ファイル<br />...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>また、Inventorのアドインプログラム等を開発するためのSDKのインストーラについては、Inventorのインストーラに含まれており、通常C:\Users\Public\Documents\Autodesk\Inventor 2025\SDK フォルダ配下に配置されます。</p>
<p>SDKフォルダ配下には、「SDK_Readme.htm」、「developertools.msi」、「usertools.msi」が含まれており、「developertools.msi」をインストールすることで、C:\Users\Public\Documents\Autodesk\Inventor 2025\SDK\DeveloperToolsフォルダ配下に、Inventorのオブジェクトモデルドキュメント、各種サンプルプログラムやツール等がインストールされます。</p>
<p><br />なお、Inventor add-inを開発する際に使用する、Visual StudioのアドインWizardについては、DeveloperTools.msiをインストールすることで併せてインストールされます。VB.net やC#等の.Net言語を用いてInventor 2025のアドインを開発する場合、Inventor の.NET 8対応に伴い.NET 8のアセンブリをビルドすることが可能なVisual Studio 2022 Update18.5以降が開発環境として必要となります点にご留意ください。</p>
<p>開発環境の詳細については英語となりますが、SDKフォルダ内の「SDK_Readme.htm」をご参照ください。</p>
<p>&#0160;</p>
<p>次回以降の記事では、Inventor 2025での機能拡張についてご案内をしていきます。</p>
<p>&#0160;</p>
<p>by Takehiro Kato</p>
<p>&#0160;</p>
