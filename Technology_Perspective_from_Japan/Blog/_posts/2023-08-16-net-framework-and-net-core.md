---
layout: "post"
title: ".NET Framework と .NET（.NET Core）"
date: "2023-08-16 00:06:21"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/08/net-framework-and-net-core.html "
typepad_basename: "net-framework-and-net-core"
typepad_status: "Publish"
---

<p><a href="https://ja.wikipedia.org/wiki/Microsoft_Windows" rel="noopener" target="_blank">Windows</a> 上で動作するオートデスク製品は、Microsoft が開発・提供する <a href="https://ja.wikipedia.org/wiki/.NET_Framework#cite_note-future_of_dotnet-1" rel="noopener" target="_blank">.NET Framework</a> を利用しています。2000 年に .NET Framework が発表された際、当時、販売されていた Windows には .NET Framework はまだ搭載さてはいなかったため、別途、入手してインストールする必要がありました。現在では Windows に最初から組み込まれているのはご存じのとおりです。</p>
<p>さて、この .NET Framework、なぜ、重宝され、オートデスク製品の多くが利用するようになったかご存じでしょうか？</p>
<hr />
<p><strong>.NET Framework </strong></p>
<p style="padding-left: 40px;">.NET Framework は、おおまかに 2 つの役割、<strong>アプリケーションの実行環境 </strong>と <strong>開発機能提供</strong> を持っていると考えることが出来ます。</p>
<p style="padding-left: 40px;"><strong>アプリケーションの実行環境</strong></p>
<p style="padding-left: 80px;">ビジネスの領域で最も使われていると言っても過言ではない Microsoft Windows ですが、Windows 3.1 → Windows 95 → Windows XP → Windows 2000 → Windows Vista などのように、バージョン名に一貫性は見られないものの、その登場以来、バージョンアップが実施されています。</p>
<p style="padding-left: 80px;">このバージョン名に隠れた形で変化し続けてきたのが、Windows が稼働するハードウェアの向上です。そして、着目すべきは、演算をおこなう頭脳である CPU のビット数です。Windows 初期には 16 ビット CPU だったのが、32 ビット時代を経て、現在は 64 ビット CPU が主流になっています。CPU のビット数が大きくなると、演算スピードの向上だけでなく、使用出来るメモリも増えていきます。メモリを多く使用する図形処理（CAD/BIM/CG）では、CPU の性能向上はとても重要です。</p>
<p style="padding-left: 80px;">さて、Windows で稼働するアプリケーションは、Windows が採用するビット数に合致する実行ファイル（.exe や .dll）が必要です。言い換えれば、64 ビット アプリケーションは 32 ビット Windows では実行出来ないのです。（32 ビット アプリケーションは 、Microsoft が用意した互換モード <a href="https://ja.wikipedia.org/wiki/WOW64" rel="noopener" target="_blank">Wow64</a> を使って 64 ビット Windows で実行出来ました。）</p>
<p style="padding-left: 80px;">32 ビット (x86) Windows&#0160; と 64 ビット (x64)&#0160; &#0160;Windows&#0160; が混在して利用されていた時期、アプリケーションを用意する開発者（オートデスク）にとって両方のビット数に対応するアプリケーションを用意しなければならず、いろいろな意味でとても大変でした。これを解決してくれたのが .NET Framework です。</p>
<p style="padding-left: 80px;">一般に、Windows アプリケーションの開発には Microsoft Visual Studio という開発ツールを使用します。オートデスクの Windows 向け製品も Visual Studio を使って開発されています。Visual Studio で .NET Framework を利用するアプリケーション用のプログラム コードをコンパイルすると、拡張子 .exe や .dll ファイルを生成します。これら実行ファイルは、内部的には中間言語（IL、Intermediate Language）で構成されています。</p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751aea633200c-pi" style="display: inline;"><img alt="Dotnet_framework1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751aea633200c image-full img-responsive" src="/assets/image_890042.jpg" title="Dotnet_framework1" /></a></p>
<p style="padding-left: 80px;">中間言語で構成された .exe や .dll ファイルは、実行時に Windows のビット数に合わせて実行時コンパイルが実施され、CPU ビット数に合ったアプリケーションを生成、実行されることになります。</p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25d00b8200d-pi" style="display: inline;"><img alt="Dotnet_framework2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25d00b8200d image-full img-responsive" src="/assets/image_528154.jpg" title="Dotnet_framework2" /></a></p>
<p style="padding-left: 80px;">つまり、.NET Framework を使うと、Windows のビット数毎にアプリケーションを用意しなくても良いわけです。（ドライバーなどは個別用意が必要な場合もあります。）</p>
<p style="padding-left: 40px;"><strong>開発機能提供</strong></p>
<p style="padding-left: 80px;">.NET Framework は、Windows が持つ機能を開発者に開発用ライブラリとして提供する役割も持っています。提供は、.NET Framework クラス ライブラリ（開発用ライブラリ） を介しておこなわれます。.NET Framework クラス ライブラリは非常に膨大なので詳細は割愛しますが、次のように機能別に分けることができます。 <br /><br />・ データ型、属性、数値演算など基本機能 <br />・ データベース (ADO.NET) アクセス関連機能 <br />・ Web サービスを含む Web 関連機能 <br />・ XML 処理関連機能 <br />・ グラフィックス関連機能 <br />・ Windows フォームなど UI 関連機能　</p>
<p style="padding-left: 80px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751ae5f01200c-pi" style="display: inline;"> </a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751ae5fab200c-pi" style="display: inline;"><img alt="Bcl_dornet_framework" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751ae5fab200c image-full img-responsive" src="/assets/image_453973.jpg" title="Bcl_dornet_framework" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751ae5f01200c-pi" style="display: inline;"><br /><br /></a>つまり、.NET Framework を使うと、Windows が持っている機能を開発するアプリケーションのプログラム（ソースコード）に簡単に組み込んで使用出来るわけです。　</p>
<p style="padding-left: 80px;">.NET Framework を利用して構築されたアプリケーションは、メモリ管理の恩恵も受けることが出来るようになります。アプリケーションのプログラム側で、処理に必要なメモリの確保や解放処理を組み込む手間が省けるだけでなく、メモリの解放が自動化されて（<a href="https://ja.wikipedia.org/wiki/%E3%82%AC%E3%83%99%E3%83%BC%E3%82%B8%E3%82%B3%E3%83%AC%E3%82%AF%E3%82%B7%E3%83%A7%E3%83%B3" rel="noopener" target="_blank">ガベージコレクション</a>）、メモリの解放し忘れ（<a href="https://ja.wikipedia.org/wiki/%E3%83%A1%E3%83%A2%E3%83%AA%E3%83%AA%E3%83%BC%E3%82%AF" rel="noopener" target="_blank">メモリリーク</a>）のリスクが低下します。このことから、.NET Framework に管理されない古いタイプのアプリケーションを <strong>アンマネージ コード</strong>と呼んでいます。同じように、実行時に .NET Framework を必要とする新しいタイプのアプリケーションを <a href="https://ja.wikipedia.org/wiki/%E3%83%9E%E3%83%8D%E3%83%BC%E3%82%B8%E3%82%B3%E3%83%BC%E3%83%89" rel="noopener" target="_blank"><strong>マネージ コード</strong> </a>と呼んで区別することがあります。</p>
<hr />
<p>使用されている Windows がほぼ 64 ビットになり、AutoCAD や Revit、Inventor といった主要アプリケーションも、64 ビット版しかリリースされなくなっています。ただ、他の観点からも、オートデスクにとって .NET Framework がとても便利で有用であることはご理解いただけると思います。</p>
<p>オートデスクは、Windows で稼働するアプリケーション本体だけでなく、それら製品のアドイン/プラグイン アプリケーション開発用の API にも .NET Framework を導入しています。</p>
<p style="padding-left: 40px;">参考：<a href="https://adndevblog.typepad.com/technology_perspective/2013/11/texhnologies-for-apis-on-autodesk-products.html">オートデスク製品の API が利用するテクノロジ</a></p>
<p>ところで、.NET Framework にもバージョンが存在しています。オートデスク製品が使用する .NET Framework も、その時々の最新バージョンが採用されています。</p>
<p style="padding-left: 40px;">参考：<a href="https://adndevblog.typepad.com/technology_perspective/2015/05/autocad-and-net-framework-version.html">AutoCAD と対応する .NET Framework バージョン</a></p>
<p>このように、とても有用な .NET Framework なのですが、.NET Framework 4.8 が最後のメジャーバージョンとなることが<strong><a href="https://devblogs.microsoft.com/dotnet/net-core-is-the-future-of-net/" rel="noopener" target="_blank">アナウンス</a></strong>されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c1b25cac16200d-pi" style="display: inline;"><img alt="Announcement" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c1b25cac16200d image-full img-responsive" src="/assets/image_955925.jpg" title="Announcement" /></a></p>
<p>今後が気になります。Microsoft は、.NET Framework をどうするのでしょう？</p>
<p>答えは先に触れた<strong><a href="https://devblogs.microsoft.com/dotnet/net-core-is-the-future-of-net/" rel="noopener" target="_blank">アナウンス</a></strong>のタイトル「.NET Core is the Future of .NET 」にあります。Microsoft は、.NET Framework とは別に .NET Core と呼ばれるオープン ソースを用意、.NET Framework の後継に位置付けています。注意したいのは、.NET Framework を即座に廃止してしまうのではなく、メインテナンスが継続される点です。</p>
<p>Microsoft の<a href="https://learn.microsoft.com/ja-jp/lifecycle/faq/dotnet-framework" rel="noopener" target="_blank">ライフサイクルに関する FAQ - .NET Framework | Microsoft Learn</a> を見る限り、インストール先の Windows のバージョンと同じライフサイクル ポリシーが適用されるようなので、.NET Framework 4.8 がプリインストールされている最新の Windows 11 を念頭におくと、当面運用に支障が出ることは考えにくいかと思います。</p>
<hr />
<p><strong>.NET（旧名 .NET Core）</strong></p>
<p style="padding-left: 40px;">.NET Framework は Windows 上のアプリケーション開発やメンテナンス、インターネットを使ったコミュニケーション方法（Web サービス）などを劇的に変化させることに成功しました。一方、iOS や Android といったモバイルデバイス向けのオペレーティングシステム（OS）が登場する中、macOS（Mac） や Linux といった Windows 以外の既存の OS でも同様のフレームワークを求める声もありました。</p>
<p style="padding-left: 40px;">.NET Framework の後継となる .NET Core は、<a href="https://github.com/dotnet" rel="noopener" target="_blank">オープンソース</a>であると同時に複数の OS のサポートするクロスプラットフォームを謳っています。もちろん、基本的なアーキテクチャは .NET Framework と同じと考えることが出来ます。なお、.NET Core 3.1 を最後に、現在は、よりシンプルに <a href="https://ja.wikipedia.org/wiki/.NET" rel="noopener" target="_blank"><strong>.NET</strong></a> に名称を改名しています（.NET 5 ～）。</p>
<p style="padding-left: 40px;">.NET（旧名 .NET Core）は、.NET Framework 同様、初期リリース以来バージョンアップを繰り返しているわけですが、当初、用意されるクラスライブラリは Web 開発を中心にしたものでした。最近では&#0160; Windows UI 関連（<a href="https://github.com/dotnet/winforms" rel="noopener" target="_blank">WinForms</a>&#0160;や <a href="https://github.com/dotnet/wpf" rel="noopener" target="_blank">WPF</a> など）についても別パッケージによる実装が進んでいます。<a href="https://adndevblog.typepad.com/technology_perspective/2023/07/all-feedback-net-sdk-beta.html" rel="noopener" target="_blank">.NET SDK Beta：フィードバックのお願い</a>で触れた Autodesk Platform Services（APS）用の SDK は、.NET ベースの SDK となります。</p>
<p style="padding-left: 40px;">さて、Windows 以外の環境でも .NET が持つフレームワークの恩恵を受ける環境が整っている点がわかりました。ただ、Windows に特化した .NET Framework のクラスライブラリと .NET Core&#0160; のクラスライブラリには、どうしても差が出てしまいます。ソースコードの互換性を考えるアプリケーション開発では、この差は吸収すべき、というのは言うまでもありません。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751ae5fbc200c-pi" style="display: inline;"><img alt="Vs" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751ae5fbc200c image-full img-responsive" src="/assets/image_529457.jpg" title="Vs" /></a></p>
<p style="padding-left: 40px;">そこで、複数の .NET 実装で使用できるよう、両者が持つクラスライブラリを出来る限り共通化させて違いを吸収する目的で考えられたのが、<a href="https://learn.microsoft.com/ja-jp/archive/msdn-magazine/2017/september/net-standard-demystifying-net-core-and-net-standard" rel="noopener" target="_blank">.NET Standard</a> でした。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751ae9ed8200c-pi" style="display: inline;"><img alt="Dotnet_standard" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751ae9ed8200c image-full img-responsive" src="/assets/image_973564.jpg" title="Dotnet_standard" /></a></p>
<p style="padding-left: 40px;">.NET Standard の考え方は理にかなったもののように思われましたが、<a href="https://learn.microsoft.com/ja-jp/dotnet/standard/net-standard?tabs=net-standard-1-0#net-standard-problems" rel="noopener" target="_blank">問題点も指摘</a>されています。 このため、.NET Standard 2.x を<a href="https://learn.microsoft.com/ja-jp/dotnet/core/porting/#the-future-of-net-standard" rel="noopener" target="_blank">最後</a>に方向性を変え、.NET 5 以降、その利用は必須とも言えなくなっています。</p>
<hr />
<p>ややこしい話題になってしまいましたが、オートデスクにとって、今後も .NET の重要性は変わりません。現在進行形のテクノロジとして、今後も注視すべきテクノロジです。</p>
<p>By Toshiaki Isezaki</p>
