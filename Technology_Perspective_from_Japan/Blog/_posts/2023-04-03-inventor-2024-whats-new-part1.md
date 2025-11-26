---
layout: "post"
title: "Inventor 2024 新機能～ その1"
date: "2023-04-03 01:00:59"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/04/inventor-2024-whats-new-part1.html "
typepad_basename: "inventor-2024-whats-new-part1"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7519daa41200c-pi" style="display: inline;"><img alt="Title" class="asset  asset-image at-xid-6a0167607c2431970b02b7519daa41200c img-responsive" src="/assets/image_393696.jpg" title="Title" /></a></p>
<p>Inventorの新バージョンとなる、Inventor 2024がリリースされました。まずは、概要をご紹介したいと思います。</p>
<p>サポートされるプラットフォームは、2023に引き続き Windows 10 の 64 ビット版（32 ビット版の提供はなし）および、Windows 11 の 64 ビット版（32 ビット版の提供はなし）となります。また、対応する.Net Frameworkについては、.NET Framework Version 4.8以降となります。</p>
<p>詳細なシステム要件については、オンラインドキュメントの以下のページをご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2024/JPN/?caas=caas/sfdcarticles/sfdcarticles/System-requirements-for-Autodesk-Inventor-2024.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">System requirements for Autodesk Inventor 2024</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Operating System 64-bit Microsoft® Windows® 11 and Windows 10. See Autodesk&#39;s Product Support Lifecycle for support information. CPU Recommended:<br />3.0 GHz or greater, 4 or more cores Minimum:2.5 GHz or greater ...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>また、これまでのInventorのリリースと同様に、Inventorファイルの保存形式が変更されており、Inventor 2024で作成したファイルをInventor 2023で開こうとすると、以下のようなダイアログが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751794125200b-pi" style="display: inline;"><img alt="1" class="asset  asset-image at-xid-6a0167607c2431970b02b751794125200b img-responsive" src="/assets/image_715522.jpg" title="1" /></a></p>
<p>ダイアログに記載のように、Inventor 2024で作成、保存したファイルを、Inventor 2023で開くためには、Inventor Interoperability 2024 コンポーネントの適用が必要となります。ダイアログでインストールボタンを押下するとコンポーネントがインストールされ、Inventor 2023でも開くことが出来るようになります。</p>
<p>Inventorのファイル互換性に関するトラブルシューティングについては、オンラインヘルプの以下のページをご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2024/JPN/?guid=GUID-489A3E1D-EB3E-4468-9592-F2EAD0E584DD" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">トラブル シューティング: 新しい Inventor ファイルを開く</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventor ファイルを開こうとするときに、開こうとする Inventor ファイルが現在のバージョンより新しいことを示すエラーが表示されます。 問題次のものを開くことができません。 現在のバージョンより 1 年以上新しいパーツまたはアセンブリ ファイル。現在のバージョンより 2 年以上新しいパーツまたはアセンブリ ファイル。新しいバージョンの Inventor の図面(.idw)またはプレゼンテーション(.ipn)ファイル<br />...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>また、Inventorのアドインプログラム等を開発するためのSDKのインストーラについては、Inventorのインストーラに含まれており、通常C:\Users\Public\Documents\Autodesk\Inventor 2024\SDK フォルダ配下に配置されます。</p>
<p>SDKフォルダ配下には、「SDK_Readme.htm」、「developertools.msi」、「usertools.msi」が含まれており、「developertools.msi」をインストールすることで、C:\Users\Public\Documents\Autodesk\Inventor 2024\SDK\DeveloperToolsフォルダ配下に、Inventorのオブジェクトモデルドキュメント、各種サンプルプログラムやツール等がインストールされます。</p>
<p>&#0160;</p>
<p>なお、Inventor add-inを開発する際に使用する、Visual StudioのアドインWizardについては、DeveloperTools.msiをインストールすることで併せてインストールされます。</p>
<p>詳細については英語となりますが、SDKフォルダ内の「SDK_Readme.htm」をご参照ください。</p>
<p>&#0160;</p>
<p>次回以降の記事では、Inventor 2024での機能拡張についてご案内をしていきます。</p>
<p>by Takehiro Kato</p>
