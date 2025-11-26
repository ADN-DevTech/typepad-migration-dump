---
layout: "post"
title: "Inventor 2025 API関連情報"
date: "2024-04-22 00:21:00"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/04/inventor-2025-api-update.html "
typepad_basename: "inventor-2025-api-update"
typepad_status: "Publish"
---

<p>今回の記事ではInventor 2025でのAPIおよび関連情報について、ご案内をしたいと思います。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><span style="font-size: 14pt;"><strong>Inventor 2025 SDKと日本語版Help</strong></span></span></p>
<p>まず、Inventor 2025で、アドインモジュール等の開発に必要となるSDKおよびVisual Studio 用の.Net Wizardについては、以下の記事にてご案内をしておりますので、こちらをご一読ください。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/04/inventor-2025-released.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"> <span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"> <img src="/assets/image_590194.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /> </span> <br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor 2025 新機能～ その1</span> <br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;"> Inventorの新バージョンとなる、Inventor 2025 がリリースされました。この記事ではInventor 2025の概要についてご紹介をいたします。サポートされるプラットフォームは、2024に引き続き Windows 10 の 64 ビット版およびWindows 11 の 64 ビット版となります。詳細なシステム要件については、オンラインドキュメントの以下のページをご参照ください。... </span> <br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160; </span> </a></div>
<p>&#0160;</p>
<p>また、日本語版のAPI Helpについては、以下の記事にてご案内をしておりますので、ダウロードして取得をしていただければと思います。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/04/japanese-inventor-2025-programming-api-help.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"> <span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"> <img src="/assets/image_950536.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /> </span> <br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">日本語版 Inventor 2025 API プログラミング用ヘルプ </span> <br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;"> Inventorの新バージョンとなる、Inventor 2025がリリースされました。 Inventor 2025の API の日本語プログラミング用ヘルプ （admapi_29_0.chm）を ZIP 圧縮したファイルをポストしていますので、次のリンクからダウンロードしてください。... </span> <br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160; </span> </a></div>
<p>API Helpの「Inventor APIの新機能」では、Inventor 2025でのAPIの更新内容について記載されておりますので、是非一度ご確認ください。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline; font-size: 14pt;"><strong>&#0160;.NET 8への対応</strong></span></p>
<p>Inventor 2025からは、2024以前の .NET Framework系とは異なる系統のソフトウェアフレームワーク.NET 8を採用しております。</p>
<p>これに伴い、既存のInventor SDKを用いた.NET Framework4.8 ベースのアドインプロジェクトは、.NET 8 へアップグレードして再ビルドする必要があります。</p>
<p>.NET 8へのVisual Studio プロジェクトの移植方法については、上述の日本語版のAPIヘルプ内の「.Net Framework ベースのプロジェクトを .Net に移植する」をご参照ください。</p>
<p>iLogicやVBA、Apprentice Serverを用いたカスタマイズ方法での.NET 8への(要否を含む)対応方法については、別の記事にてご案内をいたします。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline; font-size: 14pt;"><strong>VBA module for Inventor</strong></span></p>
<p>InventorのAPIを用いたカスタムプログラムの開発に、Visual Basic for Applications (VBA)を利用することが出来ます。</p>
<p>本格的なアドインアプリケーションを開発する場合は、Visual Studioを用いたAddin開発を行うことをお勧めいたしますが、VBAを利用することでAPIの動作確認、プロトタイプの開発等を容易に行うことが出来ます。</p>
<p>Inventorから、VBAを利用するために必要なモジュールは以下のダウンロードサイトから取得が可能です。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/tsarticles/JPN/ts/580m5V9igpBgk3WNek5Ydf.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_169662.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor 用の Microsoft VBA モジュールをダウンロードする</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">以下のダウンロード リンクをクリックすると、VBA および必要な VC10 再配布可能コンポーネントがインストールされます。 注: VBA の代わりに、Inventor の iLogic 機能の使用を検討してください。VB</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong><span style="text-decoration: underline;">レジストリ登録アドインの完全なリタイア</span></strong></span></p>
<p>Inventor 2010で、レジストリ登録が不要なアドインが導入されて以降、ご案内をしておりましたがInventor のアドインを作成には、レジストリ登録が不要なRegfree形式で作成することを推奨しておりました。</p>
<p>Inventor 2022からは、レジストリ登録を行う形式で作成されたアドインは、Inventorの Add-Insマネージャ ダイアログに表示されなくなりましたが、回避手段としてレジストリキーを作成することで利用可能とする方法が提供されておりました。</p>
<p>Inventor 2024以降、<strong><span style="text-decoration: underline;">完全にレジストリ登録が不要なアドインのみが利用可能</span></strong>となります。</p>
<p>レジストリ登録を行うアドインから、レジストリ登録が不要なアドインへの変換方法については以下のURLにてご案内をしております。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://help.autodesk.com/view/INVNTOR/2025/JPN/?guid=GUID-CFFA5CC6-38E6-4ACD-A2BC-8B8732727996" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_162025.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Converting an Existing Add-In to be Registry-Free</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">he following describes the process of converting a standard add-in into a registry-free add-in. Since the process is different for the different programming languages, the process is described for Visual Basic, C#, and VC++.<br /><br />Making your Visual Basic Add-In Registry Free<br />Create a new file in the same folder as your project file with the name &quot;MyOEMApp.X.manifest&quot;, where MyOEMApp will be replaced with the name of your add-in. Add the following to the manifest file. The portions highlighted in yellow need to be edited to match your add-in.<br /></span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p><span style="font-size: 14pt;"><strong><span style="text-decoration: underline;">すべてのユーザとバージョンに依存するアドイン マニフェスト ファイル(.addin)の場所の変更</span></strong></span></p>
<p>Inventor 2024以降、セキュリティ面を考慮し、すべてのユーザとバージョンに依存するアドインマニフェストファイルの場所が%ALLUSERSPROFILE%\Autodesk\Inventor 20xx\Addins\に代わり、%PROGRAMFILES%\Autodesk\Inventor 20xx\Bin\Addins\ に変更されました。</p>
<p>&#0160;</p>
<p><span style="text-decoration: underline;"><span style="font-size: 14pt;"><strong>Inventor Apprentice Server</strong></span></span></p>
<p>Inventorの関連製品として、InventorアプリケーションAPIのサブセットを提供するInventor Apprentice Serverがあります。</p>
<p>Inventor Apprentice Serverは、無償で提供されInventorファイルへのアクセス（主に読み取り、一部書き込みが可能）ができるアプリケーションを開発することが出来ます。</p>
<p>Inventor 2025に対応するInventor Apprentice Server 2025は以下のサイトからダウンロードが可能です。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/tsarticles/JPN/ts/7vKcwE5KANyKIPrzcxdLvf.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"> <span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"> <img src="/assets/image_384916.jpg" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /> </span> <br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor Apprentice Server 2025 </span> <br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventor Apprentice Server 2025 商標と著作権のドキュメントが更新されました...</span> <br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160; </span> </a></div>
<p>なお、Inventor 2023まで提供されておりましたInventor ViewはInventor 2023が最終リリースとなり、Inventor 2024以降は公開されません。</p>
<p>詳細については、以下URLのドキュメントを参照ください。</p>
<p><a href="https://damassets.autodesk.net/content/dam/autodesk/www/pdfs/inventor-view-end-of-life-faq-external-en.pdf">Autodesk® Inventor® View Replacement FAQ</a></p>
<p>&#0160;</p>
<p>今回の記事は以上となります。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
