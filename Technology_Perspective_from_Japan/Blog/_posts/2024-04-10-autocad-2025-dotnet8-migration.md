---
layout: "post"
title: "AutoCAD 2025 .NET 8 へのアドイン移植"
date: "2024-04-10 02:00:20"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/04/autocad-2025-dotnet8-migration.html "
typepad_basename: "autocad-2025-dotnet8-migration"
typepad_status: "Publish"
---

<p>Windows 版 AutoCAD 2025 は、従来製品が採用してきた Windows 専用の&#0160; .NET Framework 4.8 に代わって、クロスプラットフォームで利用可能な .NET 8 を採用しています。<a href="https://adndevblog.typepad.com/technology_perspective/2023/08/net-framework-and-net-core.html" rel="noopener" target="_blank">.NET Framework と .NET（.NET Core）</a>でも少し触れていますが、.NET 登場の背景から、この変更は将来を見据えた判断によるものです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af6bc8200d-pi" style="display: inline;"><img alt="Dotnet" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af6bc8200d image-full img-responsive" src="/assets/image_236743.jpg" title="Dotnet" /></a></p>
<p>製品自体のルック＆フィール（見た目や操作感）に変化はありませんが、AutoCAD .NET API を使用する .NET Framework 4.8&#0160; ベースの Visual Studio プロジェクトは、.NET 8 へアップグレードして再ビルドする必要があります。</p>
<p>ここでは、AutoCAD 2024 用の .NET Framework 4.8 ベースの Visual Studio 2022 アドイン プロジェクトを、AutoCAD 2025 用に .NET 8 ベースへ移植する具体的な手順をご紹介します。</p>
<hr />
<p><strong>.NET アップグレード アシスタントの入手</strong></p>
<p style="padding-left: 40px;">あいにく、Visual Studio プロジェクト自体に互換性がないため、プロジェクト設定でターゲット フレームワークと参照アセンブリのみを更新して対応することが出来ません。Visual Studio プロジェクトを .NET 8 ベースに移行するには、Microsoft が提供している「<a href="https://learn.microsoft.com/ja-jp/dotnet/core/porting/upgrade-assistant-overview" rel="noopener" target="_blank">.NET アップグレード アシスタント</a>」を利用することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af62a0200b-pi" style="display: inline;"><img alt="Upgrade_assistant" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af62a0200b image-full img-responsive" src="/assets/image_673437.jpg" title="Upgrade_assistant" /></a></p>
<p style="padding-left: 40px;">.NET アップグレード アシスタントの入手ととインストールについては、<a href="https://learn.microsoft.com/ja-jp/dotnet/core/porting/upgrade-assistant-install" rel="noopener" target="_blank">.NET アップグレード アシスタントをインストールする方法</a> で説明されています。</p>
<p style="padding-left: 40px;">.NET アップグレード アシスタントがサポートするプロジェクトは、C# と VB のみです。ObjectARX を利用する mixed mode の C++ プロジェクトは、 .NET アップグレード アシスタントを利用したプロジェクトのアップグレードはサポートされません。</p>
<p style="padding-left: 40px;">なお、一部、.NET 8 へ移行出来ない（コードの移植/改変が必要）なものも存在します。それらの概要は、次の記事で紹介されていますので、プロジェクトのアップグレード前に一読されることをお勧めします。</p>
<ul>
<li><a href="https://learn.microsoft.com/ja-jp/dotnet/core/porting/" rel="noopener" target="_blank">.NET Framework から .NET 6 に移植する - .NET Core | Microsoft Learn</a></li>
<li><a href="https://learn.microsoft.com/ja-jp/dotnet/core/compatibility/fx-core" rel="noopener" target="_blank">破壊的変更 - .NET Framework から .NET Core | Microsoft Learn</a></li>
</ul>
<hr />
<p><strong>.NET 8 の入手</strong></p>
<p style="padding-left: 40px;">.NET アップグレード アシスタントを後述する手順で利用した際、プロジェクトが .NET 8 ベースに移行出来ない、あるいは、AutoCAD 2025 のアセンブリを参照設定出来ない場合、お使いのコンピュータに .NET 8 がインストールされていない可能性があります。</p>
<p style="padding-left: 40px;">Visual Studio Installer から Visual Studio 2022 の [個別のコンポーネント] で導入することも出来ますが、 必要に応じて <a href="https://dotnet.microsoft.com/ja-jp/download/dotnet/8.0">.NET 8.0 (Linux、macOS、Windows) をダウンロードする (microsoft.com)</a> からインストーラーをダウンロードしてインストールすることも可能です。（Windows の x64 インストーラー）</p>
<hr />
<p><strong>新しいアセンブリの入手</strong></p>
<p style="padding-left: 40px;">AutoCAD 2025 へのプロジェクト移植には、同バージョン用に用意された .NET 8 ベースのアセンブリと、同アセンブリの参照設定が必要です。AutoCAD 2025&#0160; アセンブリは、従来と同じく、AutoCAD のインストール フォルダや、ObjectARX SDK に含まれていますが、<strong><a href="https://www.nuget.org/packages/AutoCAD.NET/" rel="noopener" target="_blank">https://www.nuget.org/packages/AutoCAD.NET/</a></strong>&#0160; で公開されている NuGet パッケージをオンラインで入手、利用することが出来ます。今回は、この NuGet パッケージを使っていきます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab535c200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af6245200b-pi" style="display: inline;"><img alt="Nuget_package_download" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af6245200b image-full img-responsive" src="/assets/image_799394.jpg" title="Nuget_package_download" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab535c200c-pi" style="display: inline;"><br /></a></p>
<hr />
<p><strong>スムーズなアップグレード例</strong></p>
<p style="padding-left: 40px;">まず、おおまかな手順の把握のために、Visual Studio 2022 上で AutoCAD 2024 .NET Wizard を使って作成したシンプルな C# プロジェクトを、アップグレード アシスタントでアップグレードする例をご紹介します。</p>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/eFqylreai-Y" width="480"></iframe></p>
<p style="text-align: left; padding-left: 40px;">この例ではスムーズにプロジェクトのアップグレードが完了しますが、場合によってはプロジェクト ファイル（ここでは C# プロジェクトなので .csproj ファイル）をテキストとして編集しなければならない場合もあります。手順事態は、次のようになります。</p>
<p style="padding-left: 80px;">1. C# プロジェクトを新しい SDK スタイル形式に変換<br />・ <strong>.NET Upgrade Assistant </strong>を使用<br />2. ターゲット フレームワークを .NET 8.0-Windows に更新<br />&#0160; &#0160;&lt;TargetFramework&gt;<strong>net8.0-windows</strong>&lt;/TargetFramework&gt;<br />&#0160; &#0160;・WPF 使用の場合 – <strong>&lt;</strong><strong>UseWPF</strong><strong>&gt;true&lt;/</strong><strong>UseWPF</strong><strong>&gt;</strong> を追加<br />&#0160; &#0160;・WinForms 使用の場合 – <strong>&lt;</strong><strong>UseWindowsForms</strong><strong>&gt;true&lt;/</strong><strong>UseWindowsForms</strong><strong>&gt;</strong> を追加<br />3. 参照を更新：System 参照は削除が可能（.NET 8 の既定）<br />4. 互換性のないパッケージ、ライブラリ参照、形骸化した古いコードに対処</p>
<p style="padding-left: 40px;">なお、プロジェクトによって参照しているアセンブリが異なりますが、この例では NuGet パッケージから AutoCAD .NET、AutoCAD .NET Core、AutoCAD .NET Model 別にすべてをインストール・参照するようにしています。</p>
<hr />
<p><strong>アップグレード アシスタントでエラーになってしまう例（VB プロジェクトの例）</strong></p>
<p style="padding-left: 40px;">次に、ObjectARX SDK for AutoCAD 2024 で提供されている TabExtension サンプル（\ObjectARX 2024\samples\dotNet\TabExtension）である VB プロジェクトを、Visual Studio 2022 上でアップグレード アシスタントを使ってアップグレードする例をご紹介します。</p>
<p style="padding-left: 40px;">あいにく、アップグレード アシスタントも完璧ではないようで、少し前に公開されていたアップグレード アシスタントのケースでは、プロジェクトのアップグレードが失敗してしまいました。そこで、同ケースでは、クリーンな（必要最低限な）プロジェクト ファイルを別に用意して、アップグレードに失敗したプロジェクト ファイル（ここでは tabextension.vbproj）の内容を置き換える処理を加えてアップグレードを実施しています。</p>
<ul>
<li>なお、最新の .NET アップグレード アシスタントでは、TabExtension サンプル プロジェクトもエラーなくアップグレード出来ることを確認済です。</li>
</ul>
<p style="text-align: center; padding-left: 40px;"><iframe allowfullscreen="" frameborder="0" height="270" src="//www.youtube.com/embed/U5jIp3sJT0w" width="480"></iframe></p>
<p style="padding-left: 40px;">このアップグレードでプロジェクト ファイル tabextension.vbproj 内容置き換えに使用した記述は、次のとおりです。プロジェクト名を反映した &lt;RootNamespace&gt;TabExtension&lt;/RootNamespace&gt; の他に、AutoCAD .NET API を使用するアドインに最低限必要な AcDbMgd.dll、AcMgd.dll、AcCoreMgd.dll アセンブリを参照する &#0160; &#0160; &lt;Reference&gt;～&lt;/Reference&gt; を確認出来るかと思います。</p>
<div>
<blockquote>
<div>&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;</div>
<div>&lt;Project Sdk=&quot;Microsoft.NET.Sdk&quot;&gt;</div>
<div>&#0160; &lt;PropertyGroup&gt;</div>
<div>&#0160; &#0160; &lt;OutputType&gt;Library&lt;/OutputType&gt;</div>
<div>&#0160; &#0160; &lt;RootNamespace&gt;TabExtension&lt;/RootNamespace&gt;</div>
<div>&#0160; &#0160; &lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;</div>
<div>&#0160; &#0160; &lt;UseWindowsForms&gt;true&lt;/UseWindowsForms&gt;</div>
<div>&#0160; &#0160; &lt;AssemblySearchPaths&gt;..\..\..\inc\;$(AssemblySearchPaths)&lt;/AssemblySearchPaths&gt;</div>
<div>&#0160; &lt;/PropertyGroup&gt;</div>
<div>&#0160; &lt;ItemGroup&gt;</div>
<div>&#0160; &#0160; &lt;Reference Include=&quot;AcDbMgd&quot;&gt;</div>
<div>&#0160; &#0160; &#0160; &lt;Private&gt;False&lt;/Private&gt;</div>
<div>&#0160; &#0160; &lt;/Reference&gt;</div>
<div>&#0160; &#0160; &lt;Reference Include=&quot;AcMgd&quot;&gt;</div>
<div>&#0160; &#0160; &#0160; &lt;Private&gt;False&lt;/Private&gt;</div>
<div>&#0160; &#0160; &lt;/Reference&gt;</div>
<div>&#0160; &#0160; &lt;Reference Include=&quot;AcCoreMgd&quot;&gt;</div>
<div>&#0160; &#0160; &#0160; &lt;Private&gt;False&lt;/Private&gt;</div>
<div>&#0160; &#0160; &lt;/Reference&gt;</div>
<div>&#0160; &lt;/ItemGroup&gt;</div>
<div>&lt;/Project&gt;</div>
</blockquote>
</div>
<p style="padding-left: 40px;">上記、動画でご紹介した手順は次のようになります。</p>
<p style="padding-left: 80px;">1. アップグレード アシスタントによるプロジェクト アップグレードを実行<br />2. アップグレードエラー回避のため、.vbproj 定義（XML）の内容を置換（前述）<br />3. NuGet パッケージ ソースを追加してアセンブリ参照を解決<br />4. ビルドして AutoCAD 2025 で動作テスト</p>
<hr />
<p><strong>既知の問題に対する対応例</strong></p>
<p style="padding-left: 40px;">アップグレード後のビルドでエラーが発生してしまった場合の既知の対応は次の通りです。</p>
<p style="padding-left: 40px;"><strong>MSB3277 ビルド警告</strong></p>
<p style="padding-left: 40px;">・AcDbMgd.dll、AcCoreMgd.dll を参照するコードのビルド時</p>
<p style="padding-left: 40px;">対処：Windowsデスクトップ・フレームワークへの参照を追加：</p>
<blockquote>
<p>&lt;ItemGroup&gt;<br />&#0160;&#0160;&#0160; &lt;FrameworkReference Include=&quot;Microsoft.WindowsDesktop.App&quot; /&gt;<br />&#0160; &lt;/ItemGroup&gt;</p>
</blockquote>
<p style="padding-left: 40px;"><strong>CA1416 ビルド警告</strong></p>
<p style="padding-left: 40px;">・Windows システム機能のみを使用する場合</p>
<p style="padding-left: 40px;">対処：AssemblyInfo.cs に次の行を追加：</p>
<blockquote>
<p>[assembly:System.Runtime.Versioning.SupportedOSPlatformAttribute</p>
</blockquote>
<p style="padding-left: 40px;"><a href="https://learn.microsoft.com/ja-jp/dotnet/core/compatibility/fx-core" rel="noopener" target="_blank">破壊的変更 - .NET Framework から .NET Core | Microsoft Learn</a> にあるとおり、一部のクラスや機能にコード上の変更や設定の変更を求められる場合もあり得ます。<a href="https://adndevblog.typepad.com/technology_perspective/2024/03/autocad-database-snoop-tool.html" rel="noopener" target="_blank">AutoCAD 図面データベース検査ツール</a>&#0160;でご紹介した <a href="https://github.com/ADN-DevTech/MgdDbg" rel="noopener" target="_blank">MgdDbg サンプル</a>では、次のクラスの置き換えが必要でした。</p>
<table style="width: 542px; margin-left: auto; margin-right: auto;" width="542">
<tbody>
<tr>
<td style="background-color: #a190a6; width: 153.07px;"><span style="color: #ffffff;">.NET Framework</span></td>
<td style="background-color: #a190a6; width: 375.33px;"><span style="color: #ffffff;">.NET 8.0</span></td>
</tr>
<tr>
<td style="width: 153.07px;">ContextMenu</td>
<td style="width: 375.33px;">ContextMenuStrip</td>
</tr>
<tr>
<td style="width: 153.07px;">MenuItem</td>
<td style="width: 375.33px;">ToolStripMenuItem</td>
</tr>
<tr>
<td style="width: 153.07px;">Thread.Abort</td>
<td style="width: 375.33px;">CancellationToken</td>
</tr>
<tr>
<td style="width: 153.07px;">BinaryFormatter</td>
<td style="width: 375.33px;">EnableUnsafeBinaryFormatterSerialization=true<br /><a href="https://github.com/dotnet/winforms/issues/9701" rel="noopener" target="_blank">https://github.com/dotnet/winforms/issues/9701</a></td>
</tr>
</tbody>
</table>
<p style="padding-left: 40px;">MgdDbg リポジトリには、 .NET Framework 4.8 と .NET 8.0 の <a href="https://github.com/ADN-DevTech/MgdDbg/branches" rel="noopener" target="_blank">2 つのブランチ</a>が用意されていますので、適宜、両者を比較してみてください。</p>
<ul>
<li>.NET Framework 4.8 ブランチ（NET4,8 ブランチ）：<a href="https://github.com/ADN-DevTech/MgdDbg/tree/NET4.8" rel="noopener" target="_blank">https://github.com/ADN-DevTech/MgdDbg/tree/NET4.8</a></li>
<li>.NET 8.0 ブランチ（master ブランチ）：<a href="https://github.com/ADN-DevTech/MgdDbg" rel="noopener" target="_blank">https://github.com/ADN-DevTech/MgdDbg</a></li>
</ul>
<hr />
<p><strong>デバッグ設定</strong></p>
<p style="padding-left: 40px;">.NET 8 では、デバッガーのコアタイプとして Managed .NET Core を選択する必要があります。C++/CLI アプリケーションの場合、アンマネージコードへのデバッグには追加のステップが必要です。デバッガーのコアタイプとして、Managed .NET Core と Native の両方を選択する必要があります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3af9b60200d-pi" style="display: inline;"><img alt="Debugger_settings" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3af9b60200d image-full img-responsive" src="/assets/image_889734.jpg" title="Debugger_settings" /></a></p>
<hr />
<p>これ以外にも、<a href="https://help.autodesk.com/view/OARX/2025/JPN/?guid=GUID-A6C680F2-DE2E-418A-A182-E4884073338A" rel="noopener" target="_blank">概要 - Managed .NET の互換性</a>&#0160;にあるような、通年の AutoCAD バージョンアップにともなう AutoCAD API 上の変更を加える必要もあります。</p>
<p>By Toshiaki Isezaki</p>
