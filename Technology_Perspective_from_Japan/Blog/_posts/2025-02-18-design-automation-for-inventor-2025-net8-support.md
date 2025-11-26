---
layout: "post"
title: "Design Automation for Inventor 2025 .NET8のサポートを開始しました。"
date: "2025-02-18 00:01:07"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/02/design-automation-for-inventor-2025-net8-support.html "
typepad_basename: "design-automation-for-inventor-2025-net8-support"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cc3c5d200c-pi" style="display: inline;"><img alt="Titile" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3cc3c5d200c image-full img-responsive" src="/assets/image_633769.jpg" title="Titile" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cc395b200c-pi" style="display: inline;"><br /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3cc395b200c-pi" style="display: inline;"><br /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ba7b10200c-pi"><br /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ba7b10200c-pi"><br /></a></p>
<p>ブログ記事「<a href="https://adndevblog.typepad.com/technology_perspective/2024/08/platform-version-design-automation-for-inventor-2025.html">注意：Design Automation for Inventor 2025のアドイン/プラグインは .NET Frameworkでのビルドが必要です ！ （2025/8/30現在）</a>」にて、Desing Automation for InventorでInventor 2025のコアエンジンをご利用する場合、デスクトップ版のInventor 2025に向けたアドイン/プラグインとは異なり、Desing Automation for Inventor 2025で利用するアドイン/プラグインは .NET Frameworkでのビルドが必要となるとのご案内をいたしました。</p>
<p>&#0160;</p>
<p><a href="https://aps.autodesk.com/en/docs/design-automation/v3/change_history/inventor_release_notes/">Design Automation for Inventor Release Notes</a>でもご案内しておりますが、Desing Automation for InventorでInventor 2025での.NET8サポートが行われ、Design Automation for Inventor 2025のコアエンジンで.NET8のアドイン/プラグインを含む App Bundle のサポートが開始されました。これにより.NET8に移行したアドイン/プラグインをAppBundle に含めて利用することが可能となりす。</p>
<p>&#0160;</p>
<p>なお.NET8 に移行したアドイン/プラグインを含むApp Bundleを使用する場合、コアエンジンには<strong>Autodesk.Inventor.2025_Net8</strong>を指定する必要があります。従来のInventor 2025に対応するコアエンジンであるAutodesk.Inventor.2025は.NET Framework のアドイン/プラグインを含む App Bundle 用である点にご留意ください。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
<p>&#0160;</p>
