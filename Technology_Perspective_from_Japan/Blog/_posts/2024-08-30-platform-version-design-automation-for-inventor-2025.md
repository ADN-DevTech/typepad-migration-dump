---
layout: "post"
title: "注意：Design Automation for Inventor 2025のアドイン/プラグインは .NET Frameworkでのビルドが必要です ！ （2025/8/30現在）"
date: "2024-08-30 01:02:27"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/08/platform-version-design-automation-for-inventor-2025.html "
typepad_basename: "platform-version-design-automation-for-inventor-2025"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ba7b10200c-pi"><img alt="Titile" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ba7b10200c image-full img-responsive" src="/assets/image_931700.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Titile" /></a></p>
<p>&#0160;</p>
<p>&#0160;</p>
<p>先日公開した以下のブログ記事でもご案内をしましたが、今春リリースされた AutoCAD、Revit、Inventor、3ds Max の 2025 バージョンに対応するエンジンが Design Automation API でお使いいただけるようになっています。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/08/design-automation-api-2025-version-all-engines.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 150px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_74948.jpg" style="width: 100%; height: auto; max-height: 150px; min-width: 0; border: 0 none; margin: 0;" width="150" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Design Automation API：2025 バージョン相当エンジン</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">ご案内が遅くなってしまいましたが、今春リリースされた AutoCAD、Revit、Inventor、3ds Max の 2025 バージョンに対応するエンジンが Design Automation API でお使いいただけるようになっています。....</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>Design Automation for Inventor 2025をご利用いただく場合、AppBundle 内のアドイン/プラグインは<strong>.NET に移行せず.NET Framework を利用する</strong>必要がある点にご留意ください。これは、Design Automation for Inventor 2025で利用しているInventor 2025のコアエンジン（InventorCoreConsole.exe)が.NET Frameworkをベースにビルドされているためとなります。</p>
<p>※Design Automation for AutoCAD、Revitのアドイン/プラグインについては、2025のエンジンで利用する場合は.NETへの移行が必要となります。</p>
<p>&#0160;</p>
<p>Appbundle内に.NETに移行したInventorのアドイン/プラグインを指定しWorkItemから実行した場合、実行時に以下のようなエラーが発生してカスタム処理を実行することが出来ません。</p>
<p><span style="font-size: 13pt;">.NETでビルドしたアドイン/プラグインを利用した場合のWorkItemの実行レポート</span></p>
<pre><code class="language-json hljs ">...<br />[MM/dd/yyyy hh:mm:ss] CoreConsole Dispose end<br />[MM/dd/yyyy hh:mm:ss] <strong>Could not load file or assembly &#39;System.Runtime, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a&#39; or one of its dependencies. The system cannot find the file specified.</strong><br />[MM/dd/yyyy hh:mm:ss] CoreConsole exiting (-1)<br />[MM/dd/yyyy hh:mm:ss] End Inventor Core Engine standard output dump.<br />[MM/dd/yyyy hh:mm:ss] Error: Application InventorCoreConsole.exe exits with code -1 which indicates an error.<br />...</code></pre>
<p>&#0160;</p>
<p>Design Automation for Inventorのアドイン/プラグインの新規作成や、ローカルでのデバッグ実行に利用可能なVisual StudioのTemplateは以下のサイトから入手可能です。2024年5月22日の更新で、Inventor 2025のサポートも追加されておりますので、必要に応じて最新版を取得してご利用ください。</p>
<h2 class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://marketplace.visualstudio.com/items?itemName=Autodesk.DesignAutomation" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 100px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/Microsoft.VisualStudio.Services.Icons.Default" style="width: 100%; height: auto; max-height: 100px; min-width: 0; border: 0 none; margin: 0;" width="100" /></span><br style="display: none;" /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;"><span style="font-size: 12pt;"><strong>Design Automation: Visual Studio Project Templates for Inventor</strong></span><br /><span style="font-size: 11pt;">Design Automation for Inventor (recommended) </span><span style="font-size: 11pt;">It is a multiple project solution. There are three projects: &quot;Debug Plugin Locally&quot;, &quot;Plugin project&quot; and &quot;Interaction project&quot;. It is recommended to start with this template....</span></span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></h2>
<p>&#0160;</p>
<p>なお、<strong>デスクトップ版のInventor 2025</strong>に向けたアドイン/プラグインの場合は、過去のブログ記事でもご案内したように.NET(.NET 8) への移行が必要となります。</p>
<div class="blogcardfu" style="width: auto; max-width: 9999px; border: 1px solid #E0E0E0; border-radius: 3px; margin: 10px 0; padding: 15px; line-height: 1.4; text-align: left; background: #FFFFFF;"><a href="https://adndevblog.typepad.com/technology_perspective/2024/05/inventor-2025-migration-to-net8.html" rel="noopener" style="display: block; text-decoration: none;" target="_blank"><span class="blogcardfu-image" style="float: right; width: 150px; padding: 0 0 0 10px; margin: 0 0 5px 5px;"><img src="/assets/image_304200.jpg" style="width: 100%; height: auto; max-height: 150px; min-width: 0; border: 0 none; margin: 0;" width="150" /></span><br style="display: none;" /><span class="blogcardfu-title" style="font-size: 112.5%; font-weight: bold; color: #333333; margin: 0 0 5px 0;">Inventor 2025 カスタムプログラムの.NET 8 への移植</span><br /><span class="blogcardfu-content" style="font-size: 87.5%; font-weight: 400; color: #666666;">Inventor 2025では、従来製品が採用してきた Windows 専用の .NET Framework 4.8 に代わって、クロスプラットフォームで利用可能な .NET 8 を採用しています。これに伴い、Inventorのカスタムプログラムの移植が必要になる場合があります。Inventorは様々なカスタマイズの手法を提供しており、.NET 8への移植が必要か否かについて判断に迷うことがあるかと思います。</span><br /><span style="clear: both; display: block; overflow: hidden; height: 0;">&#0160;</span></a></div>
<p>&#0160;</p>
<p>デスクトップアプリケーションとDesign Automation for Inventorで、ビルドのターゲットプラットフォームを変える必要がある状況となりますので、ご留意いただけますようお願い致します。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
