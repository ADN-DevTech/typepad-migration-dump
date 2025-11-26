---
layout: "post"
title: "NuGet と AutoCAD.NET API"
date: "2014-12-12 03:21:09"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/12/nugetorg-and-autocadnet-api.html "
typepad_basename: "nugetorg-and-autocadnet-api"
typepad_status: "Publish"
---

<p>AutoCAD.NET API で必ず参照しなければならない AutoCAD アセンブリが、NuGet&#0160;パッケージとしてサポートされるようになりました。対象となる AutoCAD は、AutoCAD 2015 です。</p>
<p>もし、NuGet についてご存じなければ、Wikipedia の <a href="http://ja.wikipedia.org/wiki/NuGet" rel="noopener" target="_blank"><strong>NuGet</strong>&#0160;</a>&#0160;ページに目をとおしてみてください。.NET &#0160;Framework ベースで NuGet Gallerly ページ（<a href="https://www.nuget.org/" rel="noopener" target="_blank">https://www.nuget.org/</a>）で検索可能なアセンブリであれば、お手元のプロジェクトに NuGet を介して新規にパッケージ（アセンブリ群）を参照設定することが出来ます。</p>
<p>AutoCAD 2015 でサポートされている開発環境、Visual Studio 2012 では、NuGet パッケージ マネージャを [プロジェクト] メニューから直接利用することが出来ます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07c283d3970d-pi" style="display: inline;"><img alt="NuGet" class="asset  asset-image at-xid-6a0167607c2431970b01bb07c283d3970d img-responsive" src="/assets/image_963815.jpg" style="width: 400px;" title="NuGet" /></a></p>
<p>次の動画は、NuGet パッケージ マネージャを利用して、新しいプロジェクトに AutoCAD .NET API で必要となる acdbmgd.dll、acmgd.dll、accoremgd.dll などのアセンブリを参照設定する手順を紹介するものです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/uYNsKoK95rY?feature=oembed" width="500"></iframe>&#0160;</p>
<p>また、他のコンピュータから移動させた AutoCAD .NET API プロジェクトや、ダウンロードしたサンプル プロジェクトを Visual Studio で開いた場合、稀に、アセンブリ参照を解決できない場合があります。このような場面でも NuGet でアセンブリ参照を解決することが出来ます。次の動画では、パッケージ マネージャ コンソールを使って参照を解決する操作を紹介しています。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/acOOWcpKiU8?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="text-align: left;">この方法は、<a href="http://adndevblog.typepad.com/technology_perspective/2014/10/view-data-api-sample2.html" rel="noopener" target="_blank"><strong>View &amp; Data API サンプル ～ その2</strong> </a>で紹介したアセンブリ参照解決の方法と同じです。一度、NuGet を利用すると、ソリューション フォルダ直下に&#0160;packages フォルダが作成されて、NuGet サイトで管理されている最新のアセンブリを参照するようになります。</p>
<p>このように、NuGet でアセンブリ管理すると、手動で参照設定するよりも簡単にプロジェクトを維持することが出来ます。GitHub と同じく、開発環境もクラウドによって変革されています。</p>
<p>By Toshiaki Isezaki</p>
