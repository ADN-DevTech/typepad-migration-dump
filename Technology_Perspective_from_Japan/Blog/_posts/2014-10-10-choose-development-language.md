---
layout: "post"
title: "開発言語の選定について"
date: "2014-10-10 03:16:25"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/10/choose-development-language.html "
typepad_basename: "choose-development-language"
typepad_status: "Publish"
---

<p style="text-align: left;">オートデスクのデスクトップ製品用にアドイン（プラグイン、アドオン） アプリケーションを開発する際、その開発言語の選定に迷われる場合があるようです。特に、AutoCAD は 5 つの API からカスタマイズする開発言語を選択することが可能なので、API の選択も悩ましいところです。</p>
<p>開発言語は、API に利用されているテクノロジや開発環境によって左右されるものが存在します。もし、選定に迷われた場合には、実装される機能を実現するために、どれが最適なのか、テクノロジ、歴史、移植性などの要素によって選定されることをお勧めします。もちろん、使用しようとする開発言語を習得されたメンバが存在するか否か、将来に渡って、その言語を利用する後継開発者を維持できるか、といった人的リソース面も考慮に入れていただく必要があるでしょう。</p>
<p style="text-align: right;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb079348c3970d-pi" style="display: inline;"><img alt="Development_languages" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb079348c3970d image-full img-responsive" src="/assets/image_772284.jpg" title="Development_languages" /></a></p>
<p>さて、API が利用するテクノロジと歴史については、次のブログ記事でご紹介しています。まずは、こちらをご参照ください。</p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2013/11/texhnologies-for-apis-on-autodesk-products.html" target="_blank"><strong>オートデスク製品の API が利用するテクノロジ</strong></a></p>
<p>この記事を見ていただくと、API によっては開発言語に選択の余地がないものがあることをご理解いただけるとお思います。例えば、AutoCAD のカスタマイズで利用される AutoLISP では、AutoLISP 言語でしか開発することが出来ません。</p>
<p>また、開発言語が開発環境によって左右される場合もあります。例えば、Inventor や AutoCAD で利用可能な VBA では、Visual Basic 言語でしか開発が出来ません、一方、VBA に拘らなければ、COM テクノロジを利用する C++ などの言語を選択することも出来ます。別の言い方をするなら、VBA も COM テクノロジをベースにしていますが、統合開発環境として Visual Basic 言語しかサポートしていない、ということです。</p>
<p>さて、Windows で利用されるオートデスク デスクトップ製品では、.NET テクノロジを積極的に採用してきた経緯があります。.NET テクノロジは、もちろん、Microsoft .NET Framework を利用する基盤技術を指しています。そして、.NET テクノロジを利用する API も、オートデスク製品内で利用可能で、VB.NET や C# などの開発言語を選択することが出来ます。AutoCAD .NET API や Revit API では、開発着手時にすぐ、この選択をする必要があります。AutoCAD 上での .NET テクノロジと .NET API の利点については、次のブログ記事でご紹介しています。</p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2013/12/benefit-by-autocad-dotnet-api.html" target="_blank"><strong>AutoCAD .NET API の利点&#0160;</strong></a></p>
<p>この記事では、AutoCAD を題材にとって説明していますが、Revit API や Maya .NET API などでも仕組みや利点は同じです。つまり、.NET テクノロジを利用する API では、どの開発言語を利用しても、実行速度や機能に差は発生しない、ということです。</p>
<p>更に悩ましいのは、C# や VB.NET 以外にも、.NET テクノロジで利用可能な言語が存在している点です。マネージ C++ や F# といった言語が、これに該当します。ただい、F# は制御系の開発を念頭に用意された開発言語であり、利用する開発者人口も多い印象がないので、除外してもいいかも知れません、少なくとも、過去、F# の問い合わせを受けた経験はありません。</p>
<p>問題は C++ です。CAD や CG を含む科学技術系の分野では、UNIX が主流だった時代から、数多くの開発者に利用されています。AutoCAD では、ObjectARX の言語が C++ に限定されています。ただ、.NET ベースで利用可能な C++ は .NET Framework によって管理される <strong>マネージ C++</strong> となりますが、ObjectARX は .NET Framework を利用しない <strong>アンマネージ C++</strong> となります。これらの差は、主にメモリ管理の差と考えることが出来ます。アンマネージ C++ では、メモリ上のアドレスを指し示す&#0160;<strong>ポインタ</strong> を多用してプログラミングしていきますが、マネージ C++ では、.NET Framework の<a href="http://msdn.microsoft.com/ja-jp/library/hh156531(v=vs.110).aspx" target="_blank"><strong>ガベージ コレクション</strong></a>という機能がメモリ管理してくれるので、ポインタを使ってメモリを管理していく必要はありません。メモリやアドレスといった仕組みや考え方を理解するには、少し時間がかかるので、これからプログラム開発を始める方には アンマネージC++ はお勧めしていません。</p>
<p>そうなると、.NET ベースでは、現実的に VB.NET、C#、マネージ C++ からの選択となります。あとは好みの問題です。VBA から .NET ベースの環境に移行される場合には、言語使用が似ている VB.NET を選択するのが自然でしょう。ObjectARX からの移行では、マネージ C++ になるのかも知れません。ただし、前述のように ObjectARX のアンマネージ C++ と AutoCAD .NET API のマネージ C++ では、プログラム自身に互換性がありません。同じ C++ 言語でも、ObjectARX で作成したプログラム相当のプログラムを、.NET Framework ベースのクラス ライブラリを利用したマネージ C++で書き換える必要があります。このため、古い自社ライブラリなど、アンマネージ プログラムとの相互利用を想定していない場合には、C++ 言語にこだわる必要はありません。言語使用の似た C# を移行先に選択するのが自然と思います。</p>
<p>なお、VB.NET と C# の間では、.NET Framework の仕組みを利用したプログラムの相互変換サイトも存在しています。</p>
<p style="padding-left: 30px;"><strong>C# から VB.NET への変換<br /></strong><a href="http://www.developerfusion.com/tools/convert/csharp-to-vb/">http://www.developerfusion.com/tools/convert/csharp-to-vb/</a></p>
<p style="padding-left: 30px;"><strong>VB.NET から C# &#0160;への変換<br /></strong><a href="http://www.developerfusion.com/tools/convert/vb-to-csharp/">http://www.developerfusion.com/tools/convert/vb-to-csharp/</a></p>
<p>一方、クラウドやインターネット接続を目的としたり、前提とするプログラム開発では、オープンな仕様を利用することが一般化しています。JavaScript や Python などの開発言語も、オートデスク製品内で利用されています。デスクトップ PC だけでなく、タブレットやスマートフォンの利用も広まっている中では、どのプラットフォームでも利用可能な JavaScript の利用を必然です。<a href="http://adndevblog.typepad.com/technology_perspective/2014/05/autocad-2015-javascript-api.html" target="_blank"><strong>AutoCAD JavaScript API</strong></a>、<a href="http://adndevblog.typepad.com/technology_perspective/2014/09/fusion-360-api.html" target="_blank"><strong>Fusion 360 API</strong></a>、<a href="http://adndevblog.typepad.com/technology_perspective/2014/10/a360-view-data-service-api-startup-guide.html" target="_blank"><strong>View and Data Web サービス API</strong> </a>でも、この動きを反映したものと言えます。</p>
<p>AutoCAD を説明の中心に置いてしまいましたが、他の製品でも同じ考え方を当てはめることが出来ます。 AutoCAD API の選定の場合には、次のブログ記事も参考になるかと思います。</p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2013/02/autocad-api-helps-cutomizing-autocad.html" target="_blank"><strong>AutoCAD のカスタマイズを手助けする AutoCAD API</strong></a></p>
<p style="padding-left: 30px;"><a href="http://adndevblog.typepad.com/technology_perspective/2013/06/vertical-autocad-api-1.html" target="_blank"><strong>業種別 AutoCAD ベース製品の API カスタマイズ</strong></a></p>
<p>繰り返しになりますが、開発言語の選定には、テクノロジ、歴史、移植性などの要素、また、その言語を解する開発者の確保や維持を念頭に置かれることをお勧めします。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
