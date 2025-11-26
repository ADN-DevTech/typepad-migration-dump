---
layout: "post"
title: "Inventor 2022 新機能～ その1"
date: "2021-04-12 01:26:25"
author: "Takehiro Kato"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/04/inventor-2022-whats-new-part1.html "
typepad_basename: "inventor-2022-whats-new-part1"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788021a07c200d-pi" style="display: inline;"><img alt="Autodesk-inventor-badge-1024" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788021a07c200d image-full img-responsive" src="/assets/image_508336.jpg" title="Autodesk-inventor-badge-1024" /></a></p>
<p>Inventorの新バージョンとなる、Inventor 2022がリリースされました。</p>
<p>まずは、概要をご紹介したいと思います。</p>
<p>&#0160;</p>
<p>サポートされるプラットフォームは、2021に引き続き Windows 10 の 64 ビット版（32 ビット版の提供はなし）となります。また、対応する.Net Frameworkについては、.NET Framework Version 4.8以降となります。&#0160;</p>
<p>&#0160;</p>
<p>詳細なシステム要件については、オンラインドキュメントの以下のページをご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2022/JPN/?caas=caas/sfdcarticles/sfdcarticles/System-requirements-for-Autodesk-Inventor-2022.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 200px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_739025.jpg" style="width: 100%; height: auto; max-height: 200px; min-width: 0; border: 0 none; margin: 0;" width="200" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">System requirements for Autodesk Inventor 2022</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">System Requirements for Autodesk® Inventor® 2022 Windows<br />Operating System 64-bit Microsoft® Windows® 10. See Autodesk&#39;s Product Support Lifecycle for support information....</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>また、これまでのInventorのリリースと同様に、Inventorファイルの保存形式が変更されており、Inventor 2022で作成したファイルをInventor 2021で開こうとすると、以下のようなダイアログが表示されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880219ebd200d-pi" style="display: inline;"><img alt="1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880219ebd200d image-full img-responsive" src="/assets/image_169668.jpg" title="1" /></a></p>
<p>&#0160;</p>
<p>ダイアログに記載のように、Inventor 2022で作成、保存したファイルを、Inventor 2021で開くためには、最新のUpdateの適用が必要となります。</p>
<p>この、新しいファイル バージョンを開けるようにする Update は、通常、Inventor の最新バージョンのリリースから 1 か月後に使用可能になります。</p>
<p>&#0160;</p>
<p>Inventorのファイル互換性に関するトラブルシューティングについては、オンラインヘルプの以下のページをご参照ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2022/JPN/?guid=GUID-489A3E1D-EB3E-4468-9592-F2EAD0E584DD" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 200px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_739025.jpg" style="width: 100%; height: auto; max-height: 200px; min-width: 0; border: 0 none; margin: 0;" width="200" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">トラブル シューティング: 新しい Inventor ファイルを開く</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventor ファイルを開こうとするときに、開こうとする Inventor ファイルが現在のバージョンより新しいことを示すエラーが表示されます。...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>また、Inventorのアドインプログラム等を開発するためのSDKのインストーラについては、Inventorのインストーラに含まれており、通常C:\Users\Public\Documents\Autodesk\Inventor 2022\SDK フォルダ配下に配置されます。</p>
<p>SDKフォルダ配下には、「SDK_Readme.htm」、「DeveloperTools.msi」、「usertools.msi」が含まれており、「DeveloperTools.msi」をインストールすることで、C:\Users\Public\Documents\Autodesk\Inventor 2022\SDK\DeveloperToolsフォルダ配下に、Inventorのオブジェクトモデルドキュメント、各種サンプルプログラムやツール等がインストールされます。</p>
<p>なお、Inventor add-inを開発する際に使用する、Visual StudioのアドインWizardについては、DeveloperTools.msiをインストールすることで併せてインストールされます。</p>
<p>&#0160;</p>
<p>詳細については英語となりますが、SDKフォルダ内の「SDK_Readme.htm」をご参照ください。</p>
<p>&#0160;</p>
<p>次回は、Inventor 2022の新機能についてご案内をしていきます。</p>
<p>&#0160;</p>
<p>by Takehiro Kato</p>
