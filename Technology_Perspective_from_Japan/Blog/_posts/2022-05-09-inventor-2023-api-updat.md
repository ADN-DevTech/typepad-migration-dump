---
layout: "post"
title: "Inventor 2023 API関連情報"
date: "2022-05-09 02:11:27"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/05/inventor-2023-api-updat.html "
typepad_basename: "inventor-2023-api-updat"
typepad_status: "Publish"
---

<p>&#0160;</p>
<p>今回の記事ではInventor 2023でのAPIおよび関連情報について、ご案内をしたいと思います。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><span style="font-size: 14pt;"><strong>Inventor 2023 SDKと日本語版Help</strong></span></span></p>
<p>まず、Inventor 2023で、アドインモジュール等の開発に必要となるSDKおよびVisual Studio 用の.Net Wizardについては、以下の記事にてご案内をしておりますので、こちらをご一読ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/04/inventor-2023-whats-new-part1.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_130920.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor 2023 新機能～ その1</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventorの新バージョンとなる、Inventor 2023がリリースされました。 まずは、概要をご紹介したいと思います。 サポートされるプラットフォームは、2022に引き続き Windows 10 の 64 ビット版（32 ビット版の提供はなし）および、Windows 11 の 64 ビット版（32 ビット版の提供はなし）が追加されれております。また、対応する.Net Frameworkについては、.NET Framework Version 4.8以降となります。 詳細なシステム要件については、オンラインドキュメントの以下のページをご参照ください。 System requirements for Autodesk Inventor 2023 System Requirements for Autodesk® Inventor® 2023 Windows Operating System 64-bit Microsoft® Windows® 11 and Windows 10. See Autodesk&#39;s Product Support Lifecycle for suppor...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>また、日本語版のAPI Helpについては、以下の記事にてご案内をしておりますので、ダウロードして取得をしていただければと思います。<a href="https://adndevblog.typepad.com/technology_perspective/2021/04/japanese-inventor-2022-programming-api-help.html" rel="noopener" target="_blank"></a></p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2022/04/japanese-inventor-2023-programming-api-help.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_203762.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">日本語版 Inventor 2023 API プログラミング用ヘルプ</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventorの新バージョンとなる、Inventor 2023がリリースされました。 Inventor 2023の API の日本語プログラミング用ヘルプ （admapi_27_0.chm）を ZIP 圧縮したファイルをポストしていますので、次のリンクからダウンロードしてください。...</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>API Helpの「Inventor APIの新機能」では、Inventor 2023でのAPIの更新内容について記載されておりますので、是非一度ご確認ください。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline; font-size: 14pt;"><strong>VBA module for Inventor</strong></span></p>
<p>InventorのAPIを用いたカスタムプログラムの開発に、Visual Basic for Applications (VBA)を利用することが出来ます。</p>
<p>本格的なアドインアプリケーションを開発する場合は、Visual Studioを用いたAddin開発を行うことをお勧めいたしますが、VBAを利用することでAPIの動作確認、プロトタイプの開発等を容易に行うことが出来ます。</p>
<p>&#0160;</p>
<p>Inventorから、VBAを利用するために必要なモジュールは以下のダウンロードサイトから取得が可能です。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://knowledge.autodesk.com/support/inventor/downloads/caas/downloads/content/download-the-microsoft-vba-module-for-inventor.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_543490.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Download the Microsoft VBA module for Inventor | Inventor 2023 | Autodesk Knowledge Network</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">The download link below installs VBA and the required VC10 redistributable. NOTE: As an alternative to VBA, consider using Inventor’s iLogic capability. It provides access to the Inventor API using VB.NET. You can learn more about iLogic here. To install the Microsoft Visual Basic for Applications Module (VBA) for Inventor, do the following: Select the appropriate download from the list below. Close all programs. In Windows Explorer, double-click the downloaded self-extracting EXE file</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><span style="font-size: 14pt;"><strong>Inventor Viewer</strong></span></span></p>
<p>Inventorの関連製品として、InventorアプリケーションAPIのサブセットを提供するInventor Apprentice Serverがあります。</p>
<p>Inventor Apprentice Serverは、無償のInventor Viewの一部としてインストールされInventorファイルへのアクセス（主に読み取り、一部書き込みが可能）ができるアプリケーションを開発することが出来ます。<a href="https://knowledge.autodesk.com/ja/support/inventor/downloads/caas/downloads/downloads/JPN/content/inventor-apprentice-server-2023.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"></a></p>
<p>Inventor 2023に対応するInventor View 2023は以下のサイトからダウンロードが可能です。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://knowledge.autodesk.com/support/inventor/downloads/caas/downloads/content/inventor-view-2023.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_543490.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor View 2023 | Inventor 2023 | Autodesk Knowledge Network</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventor View 2023 Update to Trademarks and Copyright documentation English inventor_view_2023_english_win_64bit_dlm.sfx.exe&#0160;(exe - 673MB) English Brazilian Portuguese (Português - Brasil) inventor_view_2023_portuguese_brazil_win_64bit_dlm.sfx.exe (exe - 673MB) Brazilian Portuguese (Português - Brasil) Czech (Čeština) inventor_view_2023_czech_win_64bit_dlm.sfx.exe (exe - 673MB) Czech (Čeština) French (Français) inventor_view_2023_french_win_64bit_dlm.sfx</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>今回の記事は以上となります。</p>
<p>次回の記事では、Inventor 2023のAPIトレーニングマテリアルについてご案内をいたします。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
