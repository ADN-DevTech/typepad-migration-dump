---
layout: "post"
title: "Revit 2025 .NET 8 へのアドイン移植"
date: "2024-05-24 00:39:45"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/05/revit-2025-dotnet8-migration.html "
typepad_basename: "revit-2025-dotnet8-migration"
typepad_status: "Publish"
---

<p>Revit 2025 は、.NET 8 をベースに再構築されております。そのため、Revit 2025 のアドイン開発においても、これまでの .NET Framework ではなく、.NET 8 をターゲットフレームワークに指定する必要があります。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/12/revit-api-dotnet-migration.html">Revit / Revit API の .NET への移行について</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b3f92f200b-pi" style="display: inline;"><img alt="Revit2025_01_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b3f92f200b image-full img-responsive" src="/assets/image_574334.jpg" title="Revit2025_01_02" /></a></p>
<p>また、過去バージョンの Revit （.NET Framework4.x ベース）向けに開発されたアドインプロジェクトを Revit 2025 に移植する際、.NET 8 へ移行して再ビルドする必要があります。</p>
<p>今回は、既存の .NET Framework 4.x ベースのプロジェクトを .NET 8 へアップグレードする手順について、ご案内いたします。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b02885200c-pi" style="display: inline;"><img alt="Revit2025_06_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b02885200c image-full img-responsive" src="/assets/image_918374.jpg" title="Revit2025_06_01" /></a></p>
<hr />
<p><strong>Visual Studio 2022 と .NET 8 の入手</strong></p>
<p>.NET 8 コードをビルドするには、Visual Studio 2022 (17.8 以降)が必要です。また、.NET 8 も必要に応じて、インストールする必要がございます。</p>
<p>※Revit 2025 のビルドには、.NET SDK 8.0.100 が使用されています。</p>
<p>※Revit 2025 をインストールすると、.NET 8 Windows Desktop Runtime x64 8.0.0.33101 も併せてインストールされます。</p>
<hr />
<p><strong>.NET アップグレード アシスタントの入手</strong></p>
<p>Visual Studio プロジェクトを .NET 8 ベースに移行するには、Microsoft が提供している「.NET アップグレード アシスタント」を利用することが出来ます。</p>
<p>.NET アップグレード アシスタントの入手ととインストールについては、<a href="https://learn.microsoft.com/ja-jp/dotnet/core/porting/upgrade-assistant-overview">.NET アップグレード アシスタントをインストールする方法</a> で説明されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c40ca5200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_06_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c40ca5200d image-full img-responsive" src="/assets/image_895128.jpg" title="Revit2025_06_02" /></a></p>
<p>.NET アップグレード アシスタントがサポートするプロジェクトは、C# と VB のみです。</p>
<p>なお、一部、.NET 8 へ移行出来ない（コードの移植/改変が必要）なものも存在します。それらの概要は、次の記事で紹介されていますので、プロジェクトのアップグレード前に一読されることをお勧めします。</p>
<ul>
<li><a href="https://learn.microsoft.com/ja-jp/dotnet/core/porting/">.NET Framework から .NET 6 に移植する - .NET Core | Microsoft Learn</a></li>
<li><a href="https://learn.microsoft.com/ja-jp/dotnet/core/compatibility/fx-core">破壊的変更 - .NET Framework から .NET Core | Microsoft Learn</a></li>
</ul>
<hr />
<p><strong>アップグレード手順</strong></p>
<ol>
<li>C# プロジェクトを新しい SDK スタイルのプロジェクト形式に変換
<ul>
<li>アップグレード前に AssemblyInfo.cs ファイルのバックアップを推奨します。.NET 8 では、アセンブリ情報の一部がプロジェクトファイルに移動しています。<br /><br /></li>
<li>前項を参考に、.NET Upgrade Assistant ツールをインストールし、対象のプロジェクトを選択してアップグレードアシスタントを起動して、ガイドに沿ってアップグレードを実行します。<br /><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b02b30200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"> </a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0c40f92200d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_06_04" class="asset  asset-image at-xid-6a0167607c2431970b02dad0c40f92200d img-responsive" src="/assets/image_191748.jpg" title="Revit2025_06_04" /></a><br /><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b0445a200c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="Revit2025_06_05" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b0445a200c img-responsive" src="/assets/image_392121.jpg" title="Revit2025_06_05" /></a><br /><br /></li>
<li>パッケージ参照を更新します。<br />.csproj ファイル内の packages.config を PackageReferences に変換します。これにより、パッケージ管理が簡素化され、新しいプロジェクト形式に合致します。<br /><br /></li>
</ul>
</li>
<li>ターゲット フレームワークの更新
<ul>
<li>.csproj 内の &lt;TargetFrameworkVersion&gt; を目的のターゲット フレームワーク バージョンに更新します<br />（例: &lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;）。<br /><br /></li>
<li>WPF アプリケーションの場合<br />CSPROJ に <strong>&lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;</strong> と <strong>&lt;UseWPF&gt;true&lt;/UseWPF&gt;</strong> を追加します。<br /><br /></li>
<li>Windows フォームを使用するアプリケーションの場合<br />CSPROJ に <strong>&lt;TargetFramework&gt;net8.0-windows&lt;/TargetFramework&gt;</strong> と <strong>&lt;UseWindowsForms&gt;true&lt;/UseWindowsForms&gt;</strong> を追加します。<br /><br /></li>
</ul>
</li>
<li>システム参照の削除<br /><br />
<ul>
<li>&#0160; システム参照はデフォルトで利用可能なため、CSPROJ から削除できます。<br /><br /></li>
</ul>
</li>
<li>非互換なパッケージ、ライブラリ参照、非サポートのコードの対応<br /><br />
<ul>
<li>アドインを .NET 8 に移行する際、.NET Framework 4.8 で作成したアドインが利用しているライブラリや Nuget パッケージも .NET 8 に移行されている必要があります。</li>
<li><br />非互換なパッケージ、ライブラリ参照、非サポートのコードを修正してください。</li>
</ul>
</li>
</ol>
<hr />
<p><strong>コンポーネントのバージョンに関する注意事項</strong></p>
<p>アドインが、下記のコンポーネントを使用している場合、Revit 2025 で使用されているコンポーネントのバージョンと一致させることで、アドインの不安定になることを回避します。</p>
<ul>
<li>CefSharp
<ul>
<li>&quot;cef.redist.x64&quot; Version=&quot;119.4.3&quot;</li>
<li>&quot;cef.redist.x86&quot; Version=&quot;119.4.3&quot;</li>
<li>&quot;CefSharp.Wpf.HwndHost&quot; Version=&quot;119.4.30&quot;</li>
<li>&quot;CefSharp.Common.NetCore&quot; Version=&quot;119.4.30&quot;</li>
<li>&quot;CefSharp.Wpf.NetCore&quot; Version=&quot;119.1.20&quot;</li>
</ul>
</li>
<li>Newtonsoft Json
<ul>
<li>&quot;Newtonsoft.Json&quot; Version=&quot;13.0.1&quot;</li>
</ul>
</li>
</ul>
<hr />
<p><strong>一般的な問題への対処法</strong></p>
<ul>
<li>MSB3277 ビルド警告<br />
<ul>
<li>RevitAPI.dll、RevitUIAPI.dll を参照するコードのビルド時</li>
<li>
<p>対処：Windowsデスクトップ・フレームワークへの参照を追加：<br />&lt;ItemGroup&gt;<br />&#0160;&#0160;&#0160; &lt;FrameworkReference Include=&quot;Microsoft.WindowsDesktop.App&quot; /&gt;<br />&lt;/ItemGroup&gt;</p>
</li>
</ul>
</li>
<li>CA1416 ビルド警告
<ul>
<li>Windows システム機能のみを使用する場合<br /><br /></li>
<li>対処１：AssemblyInfo.cs に次の行を追加：<br />[assembly:System.Runtime.Versioning.SupportedOSPlatformAttribute(&quot;windows&quot;)]<br /><br /></li>
<li>対処２：プロジェクトファイルに下記を設定してリビルド<br />&lt;GenerateAssemblyInfo&gt;true&lt;/GenerateAssemblyInfo&gt;</li>
</ul>
</li>
</ul>
<hr />
<p>By Ryuji Ogasawara</p>
