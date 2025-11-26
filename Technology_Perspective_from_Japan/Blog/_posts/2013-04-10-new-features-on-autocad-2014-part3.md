---
layout: "post"
title: "AutoCAD 2014 の新機能 ～ その 3"
date: "2013-04-10 00:11:37"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/04/new-features-on-autocad-2014-part3.html "
typepad_basename: "new-features-on-autocad-2014-part3"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2013/03/new-features-on-autocad-2014-part1.html" rel="noopener" target="_blank"><strong>前回</strong></a>、<a href="http://adndevblog.typepad.com/technology_perspective/2013/04/new-features-on-autocad-2014-part2.html" rel="noopener" target="_blank"><strong>前々回</strong></a> に続いて、今日は AutoCAD 2014 で導入されたカスタマイズ関係の機能や改善、改良された機能について紹介します。</p>
<p><strong>アドオン アプリケーションの互換性 と DWG ファイル形式</strong></p>
<p style="padding-left: 30px;">最初に、気になるアプリケーションの互換性について触れておきましょう。<strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/03/migrate-autocad-api-addon-apps.html" rel="noopener" target="_blank">以前紹介</a></strong>&#0160;したとおり、AutoCAD は 3 世代毎に DWG ファイル形式とAPI を利用したアドオン アプリケーションの互換性を維持するコンセプトを採用しています。</p>
<p style="padding-left: 30px;">今回、登場した AutoCAD 2014 は、AutoCAD 2013 から始まった世代の 2 世代目ということになりますので、DWG ファイル形式は、2013 DWG 形式を継続採用し、AutoCAD 2013 用に開発されたアドオン アプリケーションのバイナリ互換を保つことになります。このため、原則、そのまま再コンパイルなしでロードして実行することができるはずです。&#0160;もし、AutoCAD 2013 用のアドオン アプリケーションをお持ちであれば、まずは、AutoCAD 2014 で期待した動作をするか試してみてください。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9de4a71970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="2014互換性" class="asset  asset-image at-xid-6a0167607c2431970b017ee9de4a71970d" src="/assets/image_569808.jpg" title="2014互換性" /></a></p>
<p style="padding-left: 30px;">ObjectARX と .NET API の開発環境として利用する Microsoft Visual Studio のバージョンも、従来と同じ、Visual Studio 2010 SP1 です。ただし、プロジェクト設定を変更することで、Visual Studio 2012 を利用することもできます。ObjectARX プロジェクトの場合には、全般 の プラットフォーム ツールセットを &quot;Visual Studio 2012 (v110)&quot; ではなく、Visual Studio 2010 相当の &quot;Visual Studio 2010 (v100)&quot; に変更してください。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3862826a970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VS2012_VC_Settings" class="asset  asset-image at-xid-6a0167607c2431970b017c3862826a970b" src="/assets/image_504009.jpg" title="VS2012_VC_Settings" /></a></p>
<p style="padding-left: 30px;">.NET API プロジェクトの場合には、アプリケーション の 対象のフレームワーク&#0160;を &quot;.NET Framework 4.5&quot; ではなく、Visual Studio 2010 相当の &quot;.NET Framework 4&quot; に変更してください。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c38628579970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VS2012_dotNET_Settings" class="asset  asset-image at-xid-6a0167607c2431970b017c38628579970b" src="/assets/image_784773.jpg" title="VS2012_dotNET_Settings" /></a></p>
<p style="padding-left: 30px;">AutoCAD 2014 用の ObjectARX SDK は、<a href="http://www.autodesk.com/objectarx">http://www.autodesk.com/objectarx</a> から、Visual Studio 2010/2012 共用の&#0160;ObjectARX 2014 Wizard と AutoCAD 2014 DotNet Wizards は、<a href="http://www.autodesk.com/developautocad">http://www.autodesk.com/developautocad</a> から、それぞれ無償ダウンロードが可能です。</p>
<p><strong>JavaScript API</strong></p>
<p style="padding-left: 30px;">AutoCAD に登場した 5 つめとなる全く新しい API で、今年から数バージョンかけて実装をしていく予定です。<strong><a href="http://ja.wikipedia.org/wiki/JavaScript" rel="noopener" target="_blank">JavaScript</a></strong> は、現在、知られている限り、異なるプラットフォーム間で利用可能な唯一の API 言語です。JavaScript API を使用することで、AutoCAD 2014 で利用できるようになった<strong> <a href="http://adndevblog.typepad.com/technology_perspective/2013/03/autocad-2014-%E3%81%AE%E6%96%B0%E6%A9%9F%E8%83%BD-%E3%81%9D%E3%81%AE-1.html" target="_self">設計フィード</a></strong> のような機能を、AutoCAD だけでなく、さまざまなプラットフォーム上でも利用できるようになります。設計フィードが、AutoCAD WS 上で Web ブラウザ&#0160;や 専用アプリから同じように使えることで、その意味を把握していただけると思います。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee9debb17970d-pi" style="display: inline;"><img alt="JavaScriptAPI" class="asset  asset-image at-xid-6a0167607c2431970b017ee9debb17970d" src="/assets/image_735926.jpg" title="JavaScriptAPI" /></a></p>
<p style="padding-left: 30px;">もう1つ、JavaScript で実現できるのが、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/02/autocad-%E3%82%AB%E3%82%B9%E3%82%BF%E3%83%9E%E3%82%A4%E3%82%BA%E3%82%92%E6%89%8B%E5%8A%A9%E3%81%91%E3%81%99%E3%82%8B-autocad-api.html" target="_self">いままでの AutoCAD API</a></strong>&#0160;では実現することが出来なかったサーバー中心のカスタマイズ資産管理です。</p>
<p style="padding-left: 30px;">JavaScript は、HTML コンテンツ内に記述することで、HTML 上のボタンやメニューといったインタフェースへのユーザアクションに応じた動的なコントロールに使用されてきました。HTML コンテンツは、Web ブラウザで表示することになりますが、コンテンツ自体は Web サーバーに保存されているはずです。同じ方法で、JavaScript API で記述したプログラムを含む HTML コンテンツを AutoCAD で直接参照することで、AutoCAD 上でプログラムの内容を実行することが出来るようになります。これを実現するための用意されたのが、AutoCAD 2014 の WEBLOAD コマンドです。</p>
<p style="padding-left: 30px;">いままで、AutoLISP、VBA、ObjectARX、.NET API で作成したカスタマイズ モジュールは、AutoCAD がインストールされているクライアント コンピュータにコピー/インストールしてロードさせるのが一般的です。共有サーバーを使って、カスタマイズ モジュールをサーバーからロードさせることもできますが、ネットワーク負荷によっては、ロードまでに時間がかかることもあります。また、特に、.NET API で作成したアセンブリ ファイルは、既定ではネットワーク越しのロードが出来ません（方法はありますが）。</p>
<p style="padding-left: 30px;">カスタマイズ モジュール自体に不具合があった場合にも、ObjectARX や .NET API では、Visual Studio でのプログラム改修とコンパイル作業、サーバーへポストする作業が必要です。JavaScript API の場合には、改修したプログラムを Web サーバーに保存すれば（HTML コンテンツを公開すれば）、そのまま AutoCAD から参照してロードさせることが出来るので、非常に便利です。 &#0160;</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c383b77f7970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="JavaScriptの利点" class="asset  asset-image at-xid-6a0167607c2431970b017c383b77f7970b" src="/assets/image_590268.jpg" title="JavaScriptの利点" /></a></p>
<p style="padding-left: 30px;">さて、それでは、JavaScript API で何が実現できるのでしょう。JavaScript API の最初のリリースになる AutoCAD 2014 では、次の機能を利用できるようになっています。</p>
<ul>
<li>プロンプト表示&amp;操作（座標、数値、文字などの入力）</li>
<li>エンティティの操作（図形選択など）</li>
<li>一時グラフィックス表示や操作</li>
<li>エンティティのドラッグ実装</li>
<li>ビュー操作</li>
<li>コマンド（定義、実行）</li>
<li>ウィンドウ操作</li>
<li>図面データベースのイベント処理</li>
<li>Bindable Object Layer (BOL)</li>
<li>Application オブジェクト処理（さまざまな機能を提供）</li>
</ul>
<p style="padding-left: 30px;">一見して何を指しているか分かりにくいものありますが、既存の AutoCAD API とほぼ同じような処理を実現できることを感じていただけると思います。どのような機能を提供するかは、下記のリファレンスで参照することができます。</p>
<p style="padding-left: 30px;"><strong>JavaScript API Reference : </strong><a href="http://www.autocadws.com/jsapi/v1/docs/index.html" target="_self">http://www.autocadws.com/jsapi/v1/docs/index.html</a></p>
<p style="padding-left: 30px;">開発者へのライブラリ提供は、<a href="http://www.autocadws.com/jsapi/v1/Autodesk.AutoCAD.js">http://www.autocadws.com/jsapi/v1/Autodesk.AutoCAD.js</a>&#0160;から行われています。</p>
<p style="padding-left: 30px;">ここでは サーバーや Web サーバーという表現を使いましたが、最近では<strong> クラウド</strong> と表現したほうがいいのかもしれません。AutoCAD 2014 では、クラウドを使ったカスタマイズとそのデリバリも検討いただけるようになった、と理解していただいて結構です。</p>
<p style="padding-left: 30px;">具体的な利用方法は、別の機会にご紹介しましょう。&#0160;</p>
<p><strong>VBA 7.1</strong></p>
<p style="padding-left: 30px;">Microsoft 社により、新しいバージョンの VBA コンポーネントが提供されるようになりました。AutoCAD 2013 で利用できた VBA 6.3 は 32 ビットコンポーネントのみが提供されていました。このため、64 ビット版の AutoCAD 上では、プログラム互換性や実行時のパフォーマンスに制限が存在していました。</p>
<p style="padding-left: 30px;">この原因は、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/03/autocad-api-%E3%82%92%E4%BD%BF%E3%81%A3%E3%81%9F%E3%82%A2%E3%83%89%E3%82%AA%E3%83%B3-%E3%82%A2%E3%83%97%E3%83%AA%E3%82%B1%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E3%81%AE%E7%A7%BB%E6%A4%8D%E6%80%A7.html" rel="noopener" target="_blank">過去のポスト</a></strong> でもご紹介した、64 ビット版 AutoCAD で 32 ビット版 VBA を動作させる仕組みによるものです（AutoCAD と VBA の内部的な通信が発生）。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea066d78970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VBA通信" class="asset  asset-image at-xid-6a0167607c2431970b017eea066d78970d" src="/assets/image_434604.jpg" title="VBA通信" /></a></p>
<p style="padding-left: 30px;">今回提供される VBA 7.1 は、32 ビット版コンポーネントに加え、64 ビット版コンポーネントが含まれます。 64 ビット版 AutoCAD には 64 ビット版 VBA を利用することで、同じプロセス内（メモリ空間）で動作させることができるようになるため、もう、プロセス間通信は必要なくなります。当然、パフォーマンス上の問題は発生しなくなります。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017d42922d16970c-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="VBA7_1" class="asset  asset-image at-xid-6a0167607c2431970b017d42922d16970c" src="/assets/image_365578.jpg" title="VBA7_1" /></a></p>
<p style="padding-left: 30px;">ただし、VBA の開発言語である Visual Basic の言語規約は古く、現在の Visual Studio で利用されている VB.NET とは一部異なります。オートデスクのスタンスは、どちらかというと過去の VBA 資産の保護を視野にいれた救済策と言えます。可能であれば、.NET API への移植作業をご検討いただきたいと思います。そんな意味合いもあって、VBA 7.1 は <a href="http://www.autodesk.com/vba-download">http://www.autodesk.com/vba-download</a> からのダウンロードのみの提供となります。また、残念ながら、旧バージョンでの VBA 7.1 の利用はできません。&#0160;</p>
<p><strong>セキュア ロード メカニズム</strong></p>
<p style="padding-left: 30px;">JavaScript API をはじめ、クラウドやインターネットとの融合を強く意識した AutoCAD 2014&#0160;では、セキュリティに関する機能強化も盛り込まれています。AutoCAD 2014 にカスタマイズしたアプリケーション モジュールをロードすると、次のような警告ダイアログが表示されます。</p>
<p style="text-align: center; padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3862a200970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="QA-7977_Q1" class="asset  asset-image at-xid-6a0167607c2431970b017c3862a200970b" src="/assets/image_145854.jpg" title="QA-7977_Q1" /></a></p>
<p style="padding-left: 30px;">セキュア ロードの処理では、お客様が許容しない場所からアプリケーション ロードが発生した場合、このダイアログで警告を促すようになっています。この処理によって、AutoCAD のカスタマイズ機構を悪用した悪意のあるコードの実行を事前に警告して、お客様の環境を保護することができます。詳細に関しては、AutoCAD 2014 の <strong><a href="http://docs.autodesk.com/ACD/2014/JPN/index.html?url=files/GUID-9C7E997D-28F8-4605-8583-09606610F26D.htm,topicNumber=d30e105757" rel="noopener" target="_blank">オンラインヘルプ</a></strong> を参照してみてください。<br />&#0160;<br />と言っても、意図したカスタマイズの実行に毎回、ダイアログで警告されるのも困ります。警告ダイアログの表示を抑止するには、下記のいずれかの方法をとっていただく必要があります。&#0160;</p>
<ul>
<li>システム変数 TRUSTEDPATHS またはTRUSTEDDOMAINS に、ロードモジュールが存在するパスを追加する方法。前者は、OPTION コマンドで表示される [オプション] ダイアログの [ファイル] タブにて、[信頼する場所] として複数を登録することができます。ここで指定するパスは、読み込み専用に設定されていることが推奨されます。</li>
</ul>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3862bc5c970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="QA-7977_A1" class="asset  asset-image at-xid-6a0167607c2431970b017c3862bc5c970b" src="/assets/image_113302.jpg" title="QA-7977_A1" /></a></p>
<ul>
<li>AutoCAD のインストールフォルダ配下のプラグイン用フォルダ %ProgramData%\Autodesk\ApplicationPlugins にロードモジュールを配置する方法。このフォルダと配下のサブフォルダは自動的に信頼するフォルダと認識されます。</li>
<li>アプリケーションモジュールのロードに自動ローダーを使用する方法。自動ローダーのフォルダは自動的に信頼するフォルダと認識されます。自動ローダーについては、 <strong><a href="http://docs.autodesk.com/ACD/2014/JPN/index.html?url=files/GUID-5E50A846-C80B-4FFD-8DD3-C20B22098008.htm,topicNumber=d30e492458" rel="noopener" target="_blank">こちら</a></strong> をご参照ください。</li>
<li>バイナリ モジュールについては、電子署名を施す方法。署名されたモジュールは、自動的に「信頼する」モジュールと認識されます。電子署名につきましては、<strong><a href="http://msdn.microsoft.com/ja-jp/library/ms537361(v=vs.85).aspx" rel="noopener" target="_blank">こちら（英語）</a></strong> をご参照ください。</li>
<li>システム変数 SECURELOAD を 0 に設定する方法。この設定は、OPTION コマンドで表示される [オプション] ダイアログの [システム] タブにて、[実行可能ファイルの設定] ボタンからも変更していただけます。ただし、新しいセキュリティ機構を抑止することになるため、お勧めできません。</li>
</ul>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017eea061400970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="QA-7977_A2" class="asset  asset-image at-xid-6a0167607c2431970b017eea061400970d" src="/assets/image_565460.jpg" title="QA-7977_A2" /></a>&#0160;</p>
<p>最も容易に設定でき、かつ、安全な環境を提供できるのが、1.による方法かと思います。[オプション]&#0160; ダイアログの設定は、プロファイルとしてファイル化して、最終的にシステムレジストリに反映することができます。なお、システム変数 TRUSTEDDOMAINS は、JavaScript API の実行が安全におこなえるように用意されたものです。このため、ファイルパスではなく、外部の Web URL のドメインの設定を想定しています。</p>
<p>次回は、プラットフォーム差による VBA&#0160;のパフォーマンス差とプログラム互換性について、具体的にご紹介します。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
