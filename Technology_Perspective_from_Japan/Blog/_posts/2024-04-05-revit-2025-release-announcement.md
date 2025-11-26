---
layout: "post"
title: "Revit 2025 リリースのご案内"
date: "2024-04-05 01:07:52"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/04/revit-2025-release-announcement.html "
typepad_basename: "revit-2025-release-announcement"
typepad_status: "Publish"
---

<p>Revit の最新バージョンとなる Revit 2025 がリリースされました。今回から複数回にわたり、Revit 2025 の新機能と更新内容をご紹介していきます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab9548200c-pi" style="display: inline;"><img alt="Revit2025_01_01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ab9548200c image-full img-responsive" src="/assets/image_899104.jpg" title="Revit2025_01_01" /></a></p>
<hr />
<p><strong>Autodesk Revit 2025 の動作環境</strong></p>
<p>動作要件として、Revit 2024 に引き続き 64 ビット版 Windows 10 および 64 ビット版 Windows 11 をサポートしています。</p>
<p>Revit 2024 では、プラットフォームは .Net Framework 4.8 でしたが、Revit 2025 では <strong>.NET 8</strong> に更新されました。</p>
<p>詳細なシステム要件については、オンラインドキュメントの以下のページをご参照ください。</p>
<ul>
<li><a href="https://www.autodesk.com/support/technical/article/caas/sfdcarticles/sfdcarticles/System-requirements-for-Revit-2025-products.html" rel="noopener" target="_blank">System requirements for Revit 2025 products</a></li>
</ul>
<hr />
<p><strong>アドインの互換性と .NET 8 への対応</strong></p>
<p>下記の記事でご案内しておりました通り、Revit 2025 は、<strong>.NET 8</strong> をベースに再構築されております。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/12/revit-api-dotnet-migration.html" rel="noopener" target="_blank">Revit / Revit API の .NET への移行について</a></li>
</ul>
<p>この変更は、サードパーティの開発者の皆様において、.NET 8.0 が提供する新機能を最大限に活用しながら、最新のテクノロジを利用してアプリケーションを開発し、最適化する機会となります。</p>
<p>これに伴い、過去バージョンの Revit （.NET Framework4.X ベース）で開発されたアドインプロジェクトを Revit 2025 に移植する際、.NET 8 へアップグレードして再ビルドする必要があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3afb158200d-pi" style="display: inline;"><img alt="Revit2025_01_02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3afb158200d image-full img-responsive" src="/assets/image_796452.jpg" title="Revit2025_01_02" /></a></p>
<p>既存の Visual Studio プロジェクトを .NET 8 へアップグレードする手順については、別の記事にてご案内をする予定です。</p>
<p>なお、.NET 8 コードのビルドには、Visual Studio 2022 (17.8 以降)が必要となりますので、ご注意ください。</p>
<ul>
<li><a href="https://learn.microsoft.com/ja-jp/visualstudio/releases/2022/release-notes" rel="noopener" target="_blank">Visual Studio 2022 バージョン 17.9 リリース ノート</a></li>
</ul>
<p>.NET Framework と .NET（.NET Core）の役割や違いは、.NET Framework と .NET（.NET Core） の次の記事をご確認ください。</p>
<ul>
<li>.<a href="https://adndevblog.typepad.com/technology_perspective/2023/08/net-framework-and-net-core.html" rel="noopener" target="_blank">NET Framework と .NET（.NET Core）</a></li>
</ul>
<p>次回以降の記事では、Revit 2025 の新機能・更新内容についてご案内をしていきます。</p>
<hr />
<p>By Ryuji Ogasawara</p>
