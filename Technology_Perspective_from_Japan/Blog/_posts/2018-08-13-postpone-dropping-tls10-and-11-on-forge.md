---
layout: "post"
title: "TLS 1.0/1.1 の Forge サポート中止の延期処置について"
date: "2018-08-13 00:20:15"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2018/07/postpone-dropping-tls10-and-11-on-forge.html "
typepad_basename: "postpone-dropping-tls10-and-11-on-forge"
typepad_status: "Publish"
---

<p><strong> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b022ad3a815c6200b-pi" style="display: inline;"><img alt="Security-and-technology_0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b022ad3a815c6200b image-full img-responsive" src="/assets/image_280987.jpg" title="Security-and-technology_0" /></a></strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/07/upcoming-forge-system-upgrade-tls-12.html" rel="noopener noreferrer" target="_blank"></a></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/07/upcoming-forge-system-upgrade-tls-12.html" rel="noopener noreferrer" target="_blank">TLS 1.2 への Forge システム アップグレードについて</a></strong> でお知らせしたとおり、オートデスクはセキュリティとデータの整合性に関する業界のベストプラクティスに対応する目的で、オートデスクは Forgeプラットフォーム を TLS 1.2 に移行するとともに、TLS 1.0、及び TLS 1. 1のサポートを中止を予定しています。これらのサポートが中止されると、以後、Forge Platform API を呼び出すことが出来なくなってしまうため、&#0160;TLS バージョンを 1.2 に更新するようお願いしていました。</p>
<p>ただ、2018 年 7 月 31 日に予定していた <span style="background-color: #ffff00;"><strong>TLS 1.0、及び TLS 1. 1 のサポート中止の期日は、より確実な移行を促進するため、3 か月ほど遅らせて 2018 年 10 月 31 日に再設定されています。</strong></span>TLS の概要と移行に関する詳細は、前述のブログ記事&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2018/07/upcoming-forge-system-upgrade-tls-12.html" rel="noopener noreferrer" target="_blank">TLS 1.2 への Forge システム アップグレードについて</a></strong> をご参照ください。</p>
<p>なお、TLE 1.2 に対応するため、<strong><a href="https://github.com/Autodesk-Forge/forge-api-nodejs-client" rel="noopener noreferrer" target="_blank">Node.js 用 Forge SDK </a></strong>をお使いの方は、利用する Node.js パッケージ（ミドルウェア）とバージョン、依存関係を記述した Package,json で、forge-apis（Forge SDK）の dependency version を 0.4.3&#0160;+ に設定し、npm update を実行してください。同じく<strong><a href="https://github.com/Autodesk-Forge/forge-api-dotnet-client" rel="noopener noreferrer" target="_blank"> .NET 用 Forge SDK</a></strong> をお使いの方は、Nuget から TLS 1.2 をサポートする<a href="https://www.nuget.org/packages/Autodesk.Forge/" rel="noopener noreferrer" target="_blank"> Forge SDK 1.2</a>&#0160;以降を適用してください。&#0160;</p>
<p>By&#0160;Toshiaki Isezaki</p>
