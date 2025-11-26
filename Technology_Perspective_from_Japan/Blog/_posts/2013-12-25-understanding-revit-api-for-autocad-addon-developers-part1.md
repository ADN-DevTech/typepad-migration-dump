---
layout: "post"
title: "AutoCAD アドオン開発者のための Revit API 入門 ～ 概説"
date: "2013-12-25 02:00:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/12/understanding-revit-api-for-autocad-addon-developers-part1.html "
typepad_basename: "understanding-revit-api-for-autocad-addon-developers-part1"
typepad_status: "Publish"
---

<p>AutoCAD も Revit もオートデスク製品ではありますが、BIM（Building Information Model）を標榜する Revit は、操作性の違いだけでなく、AutoCAD にはない考え方やルールが存在します。そういった違いを十分に理解することで、スムースに Revit API を習得していただけるものと思います。逆に言えば、AutoCAD に対するのと同じ感覚で開発に取り組むと、出来るはずと思っていたことが簡単に出来なかったり、方法を見つけるのに予想以上に時間がかかってしまったりするかも知れません。</p>
<p>そこで、今回から数回にわたって、AutoCAD のアドオン アプリケーション開発経験者向けに、Revit API を使ったカスタマイズの概要や考え方をご紹介しようと思います。このブログの過去のトピックと一部重複しますが、AutoCAD 開発者の目線で AutoCAD 寄りの説明を加えることで、理解しやすくなればと思います。</p>
<p><strong>製品の歴史と API</strong></p>
<p>Revit は、もともと Revit Technology Corporation が開発した製品を、2002 年にオートデスクが買収して拡張し続けている製品です。当初、3rd Party 開発者が Revit を拡張するための API を提供していませんでしたが、Revit 8 バージョンから部分的に API の公開を始めています。</p>
<p>Revit 製品自体は、当初、32 ビット版のみを販売していましたが、Revit 2009 から、64 ビット版 Windows 上でネイティブに動作させることができる 64 版 Revit をパッケージ同梱して提供するようになっています。ちょうど、AutoCAD 2008 から 32 ビット版と 64 ビット版を同梱し始めた AutoCAD と同じです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b037d7e89970c-pi" style="display: inline;"><img alt="Revit_history" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b037d7e89970c image-full img-responsive" src="/assets/image_198945.jpg" title="Revit_history" /></a></p>
<p><strong>製品構成と Revit API</strong></p>
<p><a href="http://www.autodesk.co.jp/products/autodesk-revit-family/overview" target="_blank"><strong>Revit</strong></a> は製品として複数の形態で販売されています。まずは、業種別に単体販売されている建築意匠設計用の <strong>Revit Architecture</strong>、構造設計用の <strong>Revit Structure</strong>、設備設計用の <strong>Revit MEP</strong> が存在します。ちょうど、AutoCAD をベースにした業種別製品に、AutoCAD Archtecure、AutoCAD Mechanical、AutoCAD Electrical などが存在するのと同じようか考え方です。</p>
<p>また、Revit を含む Suite 製品として、<a href="http://www.autodesk.co.jp/suites/building-design-suite/overview" target="_blank"><strong>Building Design Suite</strong></a>&#0160;があり、Standard、Premium、Ultimate の3つのエディションが存在します。Building Design Suite に含まれている Revit は、単体販売されている Revit Architecture、Revit Structure、Revit MEP のすべての機能が含まれた形で提供されていて、One Box という呼称で呼ばれることがあります。</p>
<p>一方、AutoCAD と AutoCAD LT と同じように、Revit にも <a href="http://www.autodesk.co.jp/products/revit-lt/overview" target="_blank"><strong>Revit LT</strong></a> という廉価版製品が存在します。Revit LT は、<strong>Revit LT Suite</strong> という製品形態でも販売されています。</p>
<p>さて、いくつかの製品名を挙げてきましたが、この中で Revit API を用いたカスタマイズをサポートするのは、Revit Archutecture、Revit Sturucture、Revit MEP と One Box の Revit です。AutoCAD LT が API カスタマイズをサポートしないのと同じように、Revit LT は API カスタマイズをサポートしていません。</p>
<p style="text-align: center;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3faf1c73b970b-pi" style="display: inline;"><img alt="Products_and_API" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3faf1c73b970b image-full img-responsive" src="/assets/image_907845.jpg" title="Products_and_API" /></a></p>
<p>&#0160;</p>
<p><strong>Revit API のテクノロジ</strong></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/11/texhnologies-for-apis-on-autodesk-products.html" target="_blank">以前のトピック</a>&#0160;</strong>でも説明をしましたが、 Revit API は .NET Framework をベースにしています。AutoCAD のように、ObjectARX のようなアンマネージ API や、AutoLISP のような独自言語の API は提供していないので、習得するのは 1 種類の API のみです。</p>
<p>.NET Framework を採用しているため、<a href="http://adndevblog.typepad.com/technology_perspective/2013/12/benefit-by-autocad-dotnet-api.html" target="_blank">先日紹介した .NET の利点</a>と同じ利点を享受することが出来ます。つまい、Revit が&#0160;32 ビット版であっても、64 ビット版であっても、Revit API を使って開発するプログラムやコンパイルされたアセンブリに、そういったプラットフォーム差を意識する必要はありません。ただ、Revit API で実現できない処理を、他の API で代替することが出来ないということも言えます。&#0160;</p>
<p>単一の Revit API では、運用形態として 2 つの形態を利用することが出来ます。1 つは、製品にロードして利用する <strong>アドイン</strong> という形態です。オートデスク製品間で用語が統一されていないのですが、アドインは、<strong>アドオン</strong> と呼んだり、<strong>プラグイン</strong> と呼ぶこともあります。形態としては、同じと考えて差し支えありません。</p>
<p><img alt="Revit_API" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3faf3f672970b image-full img-responsive" src="/assets/image_458165.jpg" title="Revit_API" /></p>
<p>アドインを用いることで <strong>コマンド</strong> を作成することが出来ます。カスタム コマンドを作成できるという点は AutoCAD と同じですが、Revit にはコマンド プロンプト ウィンドウのような対話用インタフェースは用意されていません。このため、ユーザにコマンドを実行させるための「外部コマンド」と「外部アプリケーション」という実行形態を用意することになります。外部コマンドは、決められた位置にメニューが表示されますが、外部アプリケーションはオリジナルのリボンやボタンを作成してユーザに実行させることができます。これらについては、後日詳細にご紹介します。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b037fcbde970c-pi" style="display: inline;"><img alt="Addin" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b037fcbde970c image-full img-responsive" src="/assets/image_770190.jpg" title="Addin" /></a></p>
<p>もう1つの運用形態は <strong>マクロ</strong> です。AutoCAD には VBA を後からインストールして VBA マクロを作成する機能がありますが、.NET ベースの API ではなく COM ベースの API を利用する必要があります。Revit API のマクロの場合、.NET ベース テクノロジを利用する必要があるため、用意されるのは VBA ではなく&#0160;<a href="http://ja.wikipedia.org/wiki/SharpDevelop" target="_blank"><strong>SharpDevelop</strong></a> と呼ばれるオープンソースの開発環境です。</p>
<p>数バージョン前までは、Microsoft 社の <a href="http://msdn.microsoft.com/ja-jp/magazine/ee517435.aspx" target="_blank">VSTA（Visual Studio Tools For Applications）</a>を利用していましたが、Revit 自身が利用する .NET Framework に追従するために、Revit 2013 から SharpDevelop に切り替わっています。Revit で作成できるマクロには、「アプリケーション レベル マクロ」と「ドキュメント レベル マクロ」があります。ドキュメント レベル マクロは、Revit のデータファイルであるプロジェクト ファイル（.rvt）などに埋め込むことが出来ます。この方法は、AutoCAD の DWG ファイルに VBA マクロを埋め込む機能に類似していますが、インターネットに適した .NET ベース API を埋め込んで利用することになるので、セキュリティの観点から推奨されていません。マクロの詳細は、後日ご紹介することにします。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b037fce6a970c-pi" style="display: inline;"><img alt="Macro" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b037fce6a970c image-full img-responsive" src="/assets/image_961408.jpg" title="Macro" /></a></p>
<p><strong>Revit SDK</strong></p>
<p>Revit API は .NET Framework ベースの API であるため、API の公開モジュールとしてアセンブリが製品と同時にインストールされています。この部分については、AutoCAD .NET API と同じです。Revit API の主要な公開アセンブリは、Revit のインストール フォルダ直下にある <strong>RevitAPI.dll</strong> と <strong>RevitAPIUI.dll</strong> で、AutoCAD .NET API を使ったアドオン開発時に参照が必須な、acmgd.dll、acdbmgd.dll、accoremgd.dll に相当します。</p>
<p>アドイン開発やマクロ開発の際には、プロジェクトから&#0160;&#0160;<strong>RevitAPI.dll</strong>&#0160;と&#0160;<strong>RevitAPIUI.dll</strong>&#0160;を参照して、プログラムを記述してビルドしてアセンブリ（.dll）ファイルを作成することになります。つまり、Revit がインストールされていれば、実質的な開発は可能です。ただ、プログラム作成に必要な情報を入手する目的で、SDK が別に用意されています。これが Revit SDK で、Revit 製品のインストーラや、<a href="http://www.autodesk.com/developrevit" target="_blank">http://www.autodesk.com/developrevit</a> または、<a href="http://www.autodesk.co.jp/developrevit" target="_blank">http://www.autodesk.co.jp/developrevit</a>&#0160;から無償でダウンロード提供されています。Revit SDK は稀にアップデートされることがあるので、可能であれば Web から入手することが推奨されています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0380001e970c-pi" style="display: inline;"><img alt="SDK_install" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b0380001e970c image-full img-responsive" src="/assets/image_177877.jpg" title="SDK_install" /></a></p>
<p>Revit SDK には数多くのサンプル プロジェクトが VB.NET や C# 言語で同梱されているほか、実際の開発で必ず参照することになる <strong>リファレンス（RevitAPI.chm）</strong>が同梱されています。また、Revit プロジェクトのデータ構造を API メソッド/プロパティ レベルで参照するための <strong>Revit Lookup</strong> ツールが、アドインの形で提供されます。また、アドインのロードを管理する <strong>Add-In Manager</strong> ツールが用意されています。Add-In Manager は、ちょうど、AutoCAD の APPLOAD コマンドのような役割を担います。</p>
<p>なお、Revit SDK には、Revit API を習得するための Revit API Developers Guide は含まれていません。Revit API Developers Guide は、Revit 製品内からオンラインでアクセス可能な Wiki ヘルプ内に含まれていて、次の URL から参照することが出来ます。</p>
<p><a href="http://help.autodesk.com/view/RVT/2014/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42">http://help.autodesk.com/view/RVT/2014/ENU/?guid=GUID-F0A122E0-E556-4D0D-9D0F-7E72A9315A42</a></p>
<p>日本語版 Revit API Developers Guide は、次のリンクから CHM 形式のファイルとしてダウンロードしていただくことが出来ます。</p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb08b42274970d img-responsive"><a href="http://adndevblog.typepad.com/files/revit_api_developer_guide.zip">Revit 2014 API の日本語 Developer Guide をダウンロード</a></span></strong></p>
<p style="padding-left: 30px;"><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb08b42274970d img-responsive" style="background-color: #ffff00;">※ ダウンロードして展開した .chm ファイルは、セキュリティ機能によってコンテンツ表示がブロックされる場合があります。コンテンツがブロックされている場合には、各ページが白く表示され て何もない状態となります。その場合には、エクスプローラから .chm ファイルを右クリックして [プロパティ] ダイアログの [全般] タブを開き、画面右下にある 「ブロックの解除」 にチェックを入れてください。</span></p>
<p><strong>Revit のカスタマイズ範囲</strong></p>
<p>Revit API で何をどのようにカスタマイズするのか、ということが習得のカギになります。まず、AutoCAD と違って、ユーザインタフェースのカスタマイズを専用におこなうための機能（CUI コマンド）や定義ファイル（.cuix ファイル）は Revit には存在していません。ユーザ インタフェースのカスタマイズは、Revit API を用いて動的に処理する必要があります。このため、AutoCAD のようにあらかじ用意した部分カスタマイズ ファイルをロードする、といったことも出来ません。</p>
<p>Revit のカスタマイズでは、一にも二にも「<strong>ファミリ</strong>」をより理解することが重要です。ファミリは、AutoCAD でいうとダイナミック ブロックのようなものと考えることが出来ますが、AutoCAD の図面ファイル（.dwg）内のブロックと違って、Revit のプロジェクト ファイル（.rvt）内のすべてのオブジェクトが、ファミリが主体に構成されている点が異なります。もちろん、開発者の視点からは、定義情報となるブロック定義と、作図空間に配置されるブロック参照と同じように<strong>、ファミリ</strong>（定義情報）<strong>&#0160;</strong>と <strong>ファミリ インスタンス</strong>（作図空間の配置） という言葉で区別することが出来ます。</p>
<p>また、ファミリには、<strong>システム ファミリ</strong>&#0160;と&#0160;<strong>コンポーネント ファミリ</strong> というファミリ区分が存在しています。ファミリは、通常、BIM で扱う意味を持つオブジェクトの単位で作成されています。システム ファミリは、壁や床、天井といったオブジェクトを表現し、コンポーネント ファミリは、窓やドア、家具などのオブジェクトを表現します。後者は建具など、各国、各メーカーの建具のように、さまざまなファミリ定義をファミリ ドキュメント ファイル（.rfa）として外部配布して、プロジェクト ファイル（.rvt）に挿入して利用します。AutoCAD の場合には、配布対象も DWG ファイル、挿入先も DWG ファイルで区別がありませんが、Revit ではこの点が明確に区別されています。なお、Revit API では、オブジェクトという言葉ではなく 要素 という言葉で各オブジェクトを抽象表現します。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b03809dfe970c-pi" style="display: inline;"><img alt="Sample_model" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b03809dfe970c image-full img-responsive" src="/assets/image_556572.jpg" title="Sample_model" /></a></p>
<p>コンポーネント ファミリの中には、ジオメトリ情報やパラメータなどが含まれると同時に、&#0160;それらのパターンを複数持たせるための <strong>タイプ</strong> を設定することが出来ます。Revit 上のタイプを、Revit API 上では <strong>ファミリ シンボル</strong> と呼んでいます。</p>
<p>コンポーネント ファミリはユーザが自由に定義することが出来るため、どのファミリが、そのような情報を持っているかは、定義情報に依存する点にも注意してください。Revit API で特定の情報を取得しようとして、失敗するケースもありうると考えて差し支えありません。Revit API では、ファミリ ドキュメント ファイル（.rfa）が Revit 上に開かれている場合のみ、API でファミリを定義していく実装が可能です。プロジェクト ファイル（.rvt）を開いている場合には、この実装は無効です。</p>
<p>システム ファミリは、ユーザは独自に新しいファミリを定義することが出来ません。この点は Revit API を用いた場合も同様です。システム ファミリでは、テンプレートとして登録されている既存のファミリ カテゴリを複写して、異なる情報を設定していくことしか出来ません。</p>
<p>繰り返しになりますが、AutoCAD の場合には、すべて DWG ファイルベースで、オブジェクトに対する API アクセスに制限はありませんが、Revit の場合には、製品自体が持つルールとファミリのルールに従う必要があります。AutoCAD とは別の CAD システムであることを再認識して取り組む必要があるのです。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
