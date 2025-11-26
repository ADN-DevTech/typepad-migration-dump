---
layout: "post"
title: "Inventor 2025 カスタムプログラムの.NET 8 への移植"
date: "2024-05-20 00:04:10"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/05/inventor-2025-migration-to-net8.html "
typepad_basename: "inventor-2025-migration-to-net8"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3afc882200c-pi"> </a> <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3b3879f200b-pi"><img alt="Dotnet-logo-with-Inventor2025-logo" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3b3879f200b img-responsive" src="/assets/image_304200.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Dotnet-logo-with-Inventor2025-logo" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3afc882200c-pi"><br /></a></p>
<p>Inventor 2025では、従来製品が採用してきた Windows 専用の .NET Framework 4.8 に代わって、クロスプラットフォームで利用可能な .NET 8 を採用しています。これに伴い、Inventorのカスタムプログラムの移植が必要になる場合があります。Inventorは様々なカスタマイズの手法を提供しており、.NET 8への移植が必要か否かについて判断に迷うことがあるかと思います。</p>
<p>そこで、本記事ではInventorのカスタマイズプログラムの種別毎に移植の必要性や移植を行う際のポイントについて解説をしたいと思います。</p>
<p>&#0160;</p>
<p>Inventorのカスタマイズは、カスタマイズ手法により大きく以下の5種類に分けることが出来ます。</p>
<p>1．Inventor .net アドイン（C#、VB.net）</p>
<p>2．iLogic</p>
<p>3. Visual Basic Application(Inventor内の組み込みVBAを利用)</p>
<p>4．外部アプリケーション(独自のexe、ExcelのVBA 等)</p>
<p>5．Inventor Apperentice</p>
<p>&#0160;</p>
<p>それぞれのカスタマイズ手法については、先に公開した記事<a href="https://adndevblog.typepad.com/technology_perspective/2024/05/autodesk-inventor-2025-api_trainingmaterial.html">Autodesk Inventor 2025 の APIトレーニングマテリアル</a>でも触れておりますので、必要に応じてご一読ください。</p>
<p>それでは、各手法毎に.NET 8移植の必要性の要否について解説をしたいと思います。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;">1．Inventor .net アドイン（C#、VB.net）</span></p>
<p>.NET 8は.NET Frameworkとは完全な互換性が無いため、.NET FrameworkベースのInventorのアドインはInventor 2025では動作しない可能性があります。このため、既存のカスタマイズの完全な動作確認を行い、問題がある場合は.Net8への移植が必要となります。</p>
<p>移植の手順は、先日公開したブログ記事「<a href="https://adndevblog.typepad.com/technology_perspective/2024/04/japanese-inventor-2025-programming-api-help.html">日本語版 Inventor 2025 API プログラミング用ヘルプ</a>」から、日本語版のAPIリファレンスを取得し、APIリファレンス内の「.Net Framework ベースのプロジェクトを .Net に移植する」に作業手順が記載されておりますので、そちらをご参照ください。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;">2．iLogic</span></p>
<p>iLogicには、iLogicで用意されているスニペットやiLogic関数によりカスタム処理を記述することが出来ます。また、VB.netの文法でInventor APIを利用する処理を記述することも可能です。一方で、iLogicはアドインプログラムの場合と異なり、内部的にはiLogicの実行時に、VB.netソースコードへの変換・コンパイルが行われるためInventorのAPIの範囲で利用している場合においては.Net8への移植は不要です。</p>
<p>ただ、注意が必要なケースとしてはiLogicのA<a href="https://help.autodesk.com/view/INVNTOR/2025/JPN/?guid=GUID-32B66838-22E4-4A0A-B5BB-862350C76B36">ddReference</a>を用いてアセンブリの参照を追加しているような場合となります。この場合参照しているアセンブリを.Net 8に対応したものに置き換える必要がある場合があります。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;">3. Visual Basic Application(Inventor内の組み込みVBAを利用)</span></p>
<p>Inventorに組み込み（インストールは別途必要）のVBAエディタで開発したVBA処理は、.NET Frameworkの機能を利用していないため.NET 8 移植対応は不要となります。Inventor 2025で動作確認を実施してご利用ください。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;">4．外部アプリケーション(独自のexe、ExcelのVBA 等)</span></p>
<p>外部アプリケーション（.exe)やExcelのVBAマクロ等からInventorのAPIを利用するカスタムプログラムは、Inventorとは別のプロセスで実行されており、InventorのAPIを呼び出す場合、Windowsの提供するActiveX/COM テクノロジーを用いてリモートプロセス(この場合はInventor)に、メッセージ通信をする形で実行されています。</p>
<p>このため外部アプリケーションによるカスタマイズについては.NET 8へのマイグレーションは基本的には不要となります。例外として外部アプリ（.exe)からiLogicのAPI（iLogicVb.Automationを経由して) を利用している場合、外部アプリケーションのプロジェクトの.NET 8へのアップグレードが必要となります。</p>
<p>&#0160;</p>
<p>なお、既存の外部アプリケーション（.exe)についてはVisual Studioで、プロジェクトの参照設定から C:\Program Files\Autodesk\Inventor 2025\Bin\Public Assemblies\ 配下の“Autodesk.Inventor.interop.dll”を参照するようを 変更をして、 “相互運用型の埋め込み”をいいえに、 “ローカルにコピー”をはいに設定しリビルドを行い、動作確認をしてご利用ください。</p>
<p>また、ExcelのVBAマクロ等の場合は参照設定からC:\Program Files\Autodesk\Inventor 2025\Bin配下のRxInventor.tlbを参照するように変更をして動作確認をしてご利用ください。</p>
<p>&#0160;</p>
<p><span style="font-size: 14pt;">5．Inventor Apperentice</span></p>
<p>Inventor ApperenticeもInventorとは異なるプロセスで実行されるアプリケーションとなります。外部アプリケーションとの違いは、外部アプリケーションの場合は、起動中のInventorを通じてInventorの機能にアクセスしているのに対し、Inventor ApperenticeはInventorのデータ(ipt、iam、idwファイル等)にアクセスするInventor Apperenticeのライブラリを外部プロセス自身が読み込み直接Inventorのデータにアクセスする点となります。</p>
<p>このため、Inventor Apperenticeの場合も.NET 8への移植は不要となります。</p>
<p>既存のApperenticeアプリケーション（.exe)についてはVisual Studioで、プロジェクトの参照設定から C:\Program Files\Autodesk\Inventor 2025\Bin\Public Assemblies\ 配下の“Autodesk.Inventor.interop.dll”を参照するようを 変更をして、 “相互運用型の埋め込み”をいいえに、 “ローカルにコピー”をはいに設定しリビルドを行い、動作確認をしてご利用ください。</p>
<p>また、ExcelのVBAマクロ等の場合は、参照設定からInventor2025（またはInventor Apprentice Server 2025）インストールフォルダ配下のRxApprentice.tlbを参照するように変更をしてください。</p>
<p>&#0160;</p>
<p>以上が、カスタマイズプログラムの種別毎での.NET 8対応の要否の説明となります。</p>
<p>&#0160;</p>
<p>なお、Inventorの各リリースにおいてはAPIの廃止・新規追加が行われており、またInventor自身の機能拡張によりAPI自身の挙動が変わることもあります。このため、.NET 8移植対応の有無にかかわらず2024以前のバージョンで利用していたカスタマイズプログラムをInventor 2025で利用する場合は、Inventor 2025環境にて動作検証を行い、必要に応じて修正を行った後に運用環境で利用するようお願いいたします。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
