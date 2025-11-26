---
layout: "post"
title: "Revit / Revit API の .NET への移行について"
date: "2023-12-08 00:16:27"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/12/revit-api-dotnet-migration.html "
typepad_basename: "revit-api-dotnet-migration"
typepad_status: "Publish"
---

<p>&#0160;</p>
<p><strong>免責事項</strong></p>
<p>この記事の内容（当社の製品およびサービスに関する計画済みまたは将来的な開発努力に関する記述）は、将来の Revit 製品、サービス、または機能が将来利用可能になることを約束または保証することを意図したものではなく、単に当社の現在の計画を反映したものであり、現在当社が把握している要素に基づくものです。</p>
<hr />
<p>下記の記事でご案内しているように、オートデスクは、Revit など Windows で稼働するアプリケーション本体だけでなく、それら製品のアドイン/プラグイン アプリケーション開発用の API にも .NET Framework を導入しています。</p>
<ul>
<li><a href="https://adndevblog.typepad.com/technology_perspective/2023/08/net-framework-and-net-core.html">.NET Framework と .NET（.NET Core）</a></li>
</ul>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a0dbd9200c-pi" style="display: inline;"><img alt="Revit_API_Architecture" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a0dbd9200c image-full img-responsive" src="/assets/image_993263.jpg" title="Revit_API_Architecture" /></a></p>
<p>この .NET Framework は、バージョン 4.8 が最後のメジャーバージョンとなることがアナウンスされており、その後継として、.NET（旧名 .NET Core）がリリースされております。</p>
<p>このような状況を背景として、Revit は .NET Framework 4.8 から.NET 7.0 に移行する計画となっております。<br />この重要な変更は、Autodesk Feedback Community にて、次期 Revit のプレビュー版として、11月に公開されております。</p>
<p>Revit プレビューリリースの詳細については、<strong><a href="https://feedback.autodesk.com">Autodesk Feedback Community</a></strong> に登録し、Revit Preview Feedback にご参加ください。</p>
<p>Revit ロードマップについては、<a href="https://blogs.autodesk.com/aec/roadmap/?redirected=1"><strong>Revit Public Roadmap</strong></a> をご参照ください。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a0dbba200c-pi" style="display: inline;"><img alt="Revit_Preview_Top" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a0dbba200c image-full img-responsive" src="/assets/image_562084.jpg" title="Revit_Preview_Top" /></a></p>
<p><br /><strong>なぜこの移行が重要なのか？</strong></p>
<p>.NET Core 7.0 への移行は単なるインクリメンタルなアップデートではなく、.NET Framework の大きな進化です。<br />これまでの .NET Framework 4.8 とは対照的に、.NET 7.0 は、よりモダンでオープンソース、クロスプラットフォームのフレームワークです。<br />この変革により、パフォーマンスの向上、クロスプラットフォームの互換性、開発者に新たな扉を開く豊富なツールやライブラリなど、様々な機能強化が導入されています。</p>
<p><br /><strong>.NET Core 7.0 に移行するメリットは何ですか？</strong></p>
<p>この移行は、Revit API のコミュニティにも大きな改善をもたらします。</p>
<p>これまで、多くの Revit API ユーザーは、.NET Framework 4.8 と互換性を持たせるために外部ライブラリのダウングレードを余儀なくされ、最終的にアプリケーションのパフォーマンスに影響を与え、クロスプラットフォームプログラミングの妨げとなっていました。</p>
<p>.NET 7.0 を利用したアプリケーションでは、最新のテクノロジによりパフォーマンスを向上することができます。</p>
<p><br /><strong>サードパーティーの開発者の皆様へのお願い</strong></p>
<p>Revit API を利用するサードパーティーの開発者の方々におかれましては、現在のアプリケーションが次期 Revit と互換性があることを確認するために、事前にテスト・検証いただくことをお勧めいたします。</p>
<p>この変更は、サードパーティの開発者の皆様において、.NET 7.0 が提供する新機能を最大限に活用しながら、最新のテクノロジを利用してアプリケーションを開発し、最適化する機会となります。</p>
<p><strong>既存のアドインを .NET 7.0 に移行するガイドは、Autodesk Feedback Community の Revit Preview Feedback 、User Forums - API General にて公開されております。</strong></p>
<p>次期 Revit のメジャーリリースまでに、ぜひ一度、プレビューリリースをお試しください。</p>
<p>By Ryuji Ogasawara</p>
