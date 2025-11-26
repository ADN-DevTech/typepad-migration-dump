---
layout: "post"
title: "仮想化環境に対応したライセンスについて"
date: "2013-12-11 01:13:01"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/12/license-type-for-vitualization.html "
typepad_basename: "license-type-for-vitualization"
typepad_status: "Publish"
---

<p>Windows XP のサポート切れにともなって、利用されている&#0160;Windows OS やハードウェアの更新を検討されている方も多いと思います。そんな中、一気に仮想化環境の導入しようと、調査されている方も多数いらっしゃると聞いています。</p>
<p>CAD の利用環境として仮想環境、別の言い方をすれ「シンクライアント」や「プライベート クラウド化」に対応するのは、なかなかハードルが高いのも事実です。<a href="http://adndevblog.typepad.com/technology_perspective/2013/07/license-types-for-desktop-products.html" rel="noopener noreferrer" target="_blank"><strong>以前も少しご紹介したのですが</strong></a>、今日は、このような環境をサポートするオートデスク製品とライセンス タイプをご紹介します。</p>
<p><strong>さまざまな仮想化</strong></p>
<p>最初に、仮想化のお話しです。仮想化と一口に言っても、異なる形態の「仮想化」が存在します。ここでは、CAD を導入するという視点で、さまざまな仮想化のタイプを概略のみご案内します（厳密な定義は除きます）。なぜ、この話題から入るかというと、個人的に「仮想化」の印象や概念が人によって異なる、という印象を持つことが多いためです。</p>
<p>最初にホスト型仮想化の例です。数年前に Windows 7 Professional に搭載されていた <a href="http://technet.microsoft.com/ja-jp/windows/hh415001.aspx" rel="noopener noreferrer" target="_blank">Windows XP モード</a>&#0160;が話題になった時期がありました。これは、Windows 7 にアップグレードしたユーザが、Windows XP 上でしか動作しないアプリケーションを利用する手段として提供されたものです。XP モードは、Windows 7 上に、Microsoft Virtual PC&#0160;という仮想化製品を使って Windows XP の実行環境を実現しています。 1つの OS 上で別の OS を仮想化して提供するこのようなタイプを、ホスト型とします。もちろん、先の例では、ホスト OS が Windows 7、ゲスト OS が Windows XP となり、1 台のコンピュータ上で 2 つの OS が利用できるようになります。類似した仮想化製品には、VMWare Workstation&#0160;などが存在します。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0227a5e8970b-pi" style="display: inline;"><img alt="Host_virtualization" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b0227a5e8970b image-full img-responsive" src="/assets/image_786004.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Host_virtualization" /></a>別のタイプのハイパーバイザ型の仮想化も存在します。ホスト型と違って、1つのハードウェア上に複数のゲスト OS を仮想化して稼働させる用途で利用されます。CAD を直接操作するコンピュータ一にインストールされるものではないため、一般ユーザの方の目に触れる機会はほとんどないように思います。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b02282ae4970d-pi" style="display: inline;"><img alt="HypervisorVirtualization" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b02282ae4970d image-full img-responsive" src="/assets/image_604514.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="HypervisorVirtualization" /></a></p>
<p>ハイパーバイザー方は、一定規模以上のサーバーや、クラウド インフラなどで利用されています。イメージとしては、下図のような感じになるかと思います。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b022d14e5970c-pi" style="display: inline;"><img alt="Clould" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b022d14e5970c image-full img-responsive" src="/assets/image_558988.jpg" title="Clould" /></a></p>
<p>このような形態の中で、仮想化する対象を OS（つまり、デスクトップ）にするか、OS 上のアプリケーションにするか、といった利用形態も存在します。同時に仮想化された OS（デスクトップ）やアプリケーションを、仮想化しているコンピュータを直接操作するのではなく、遠隔地のコンピュータから操作する、といった方法が存在します。少々強引な説明ですが、この形態が、<strong><a href="http://ja.wikipedia.org/wiki/%E3%82%B7%E3%83%B3%E3%82%AF%E3%83%A9%E3%82%A4%E3%82%A2%E3%83%B3%E3%83%88" rel="noopener noreferrer" target="_blank">シンクライアント</a>&#0160;</strong>です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b022d28a8970c-pi" style="display: inline;"><img alt="App_Desktop" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019b022d28a8970c image-full img-responsive" src="/assets/image_123700.jpg" title="App_Desktop" /></a></p>
<p>OS（デスクトップ） をサーバー コンピュータ上で仮想化してクライアントに配信する形態を「<a href="http://ja.wikipedia.org/wiki/%E3%83%87%E3%82%B9%E3%82%AF%E3%83%88%E3%83%83%E3%83%97%E4%BB%AE%E6%83%B3%E5%8C%96" rel="noopener noreferrer" target="_blank"><strong>デスクトップ仮想化</strong></a>」、アプリケーション単体をサーバー コンピュータ上で仮想化してクライアントに配信する形態を「<a href="http://ja.wikipedia.org/wiki/%E3%82%A2%E3%83%97%E3%83%AA%E3%82%B1%E3%83%BC%E3%82%B7%E3%83%A7%E3%83%B3%E4%BB%AE%E6%83%B3%E5%8C%96" rel="noopener noreferrer" target="_blank"><strong>アプリケーション仮想化</strong></a>」、と呼ばれています。「配信」という言葉を使って説明をしましたが、サーバー上で動作するデスクトップやアプリケーションを、当然、ネットワーク回線を使って間接的に操作させるといった仕組みです。&#0160;</p>
<p>&#0160;</p>
<p><strong>オートデスクがサポートするシンクライアント仮想化</strong></p>
<p>すべての製品ではありませんが、オートデスク はいくつかの製品でアプリケーション仮想化とデスクトップ仮想化をサポートしています。この時利用していただく仮想化製品には、アプリケーション仮想化に <strong>Citrix XenApp</strong>、デスクトップ仮想化に <strong>Citrix XenDesktop</strong> をサポートしています。現在、英語のページでしか紹介していませんが、サポートされている製品一覧は、<strong><a href="http://www.autodesk.com/citrix" rel="noopener noreferrer" target="_blank">http://www.autodesk.com/citrix</a></strong> でご確認いただくことが出来ます。</p>
<p>&#0160;</p>
<p><strong>技術的な側面でのライセンス タイプ</strong></p>
<p>Citrix 社の仮想化製品を使ってオートデスク製品を利用いただく場合には、<strong>技術的な側面からは</strong>、XenApp 用に「<strong>セッション用ネットワーク ライセンス</strong>」、XenDesktop 用に「<strong>ネットワーク ライセンス</strong>」を利用することが可能です。残念ながら、スタンドアロン ライセンスはサポートされません。また、セッション用ネットワーク ライセンスもネットワーク ライセンスも、ライセンス管理にライセンス マネージャが必要になります。</p>
<p>両者とも、サーバー上で複数のオートデスク製品がインストールされ、実行されるのは変わりません。ただし、起動中の製品のライセンスカウントの仕方で違いがあります。たとえば、ネットワーク ライセンスやスタンドアロン ライセンスの AutoCAD を 1 台のコンピュータ上で実行させた場合、使用しているライセンス数は1つとカウントされてしまいます。サーバー上で起動している AutoCAD 1つ 1つを別々のクライアントに配信するので、本来であれば、起動している AutoCAD の数（インスタンス）を個別にカウントすべきところです。これを実現しているのが、「セッション用ネットワーク ライセンス」です。クライアント コンピュータには、配信された画面を表示したり、マウスやキーボード入力をサーバー上で実行されている製品にフィードバックするための Citrix Receiver をインストールだけで済みます。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="344" src="http://www.youtube.com/embed/4z1Qg2RzifU?feature=oembed" width="459"></iframe>&#0160;</p>
<p>一方、デスクトップ仮想化の場合には、サーバー上でクライアントに配信するデスクトップ OS が個別に起動されることになります。ライセンス マネージャも 1 台のコンピュータ上で実行されることになるので、通常のネットワーク ライセンスが利用される環境が、サーバー上で完結することになります。なお、クライアント コンピュータに Citrix Receiver をインストールのは、アプリケーション配信と同じです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="344" src="http://www.youtube.com/embed/oHLithy7K_8?feature=oembed" width="459"></iframe>&#0160;</p>
<p>&#0160;</p>
<p><strong>使用許諾の側面でのライセンス タイプ</strong></p>
<p>ところが、オートデスク使用許諾（Software License Agreements、別名 SLA）では、デスクトップ仮想化でも「セッション用ネットワーク ライセンス」を使用することになります。<a href="http://download.autodesk.com/us/FY14/Suites/LSA/ja-JP/lsa.html" rel="noopener noreferrer" target="_blank"><strong>SLA</strong></a> の <strong>別紙 B ライセンス タイプ</strong>&#0160;のネットワーク ライセンス の項目に仮想化の想定が記載されていないためです。</p>
<p>技術的には、XenDesktop を使ったデスクトップ仮想化のために、セッション用ネットワーク ライセンスを使っても問題ありませんので、ここで説明した <strong>XenApp と XenDesktop 用にはセッション用ネットワーク ライセンスを利用する</strong>ものと思っていただいて結構です。 <span style="background-color: #ffff00;">後日追記：2015 年以降、XenApp、XenDesktop とも、通常のネットワーク ライセンスで利用できるように統一されました。</span></p>
<p>&#0160;</p>
<p><strong>シンクライアントの利点</strong></p>
<p>ありきたりですが、XenApp や XenDesktop を利用してオートデスク製品を利用した場合、次のような利点をあげることが出来ます。</p>
<ul>
<li>サーバー上で CAD 製品のメンテナンスが実施できるので、Service Pack や修正モジュールがリリースされた場合でも、サーバー上の製品に適用すればよい。つまり、スタンドアロン ライセンスやネットワーク ライセンス環境のように、クライアント コンピュータ 1 台 1 台にそれらを適用しなくてもいいので、メンテナンス性が向上します。</li>
<li>セキュリティ上扱うデータはサーバー上に一元管理されることになるので、クライアントにデータは保存されず、データ漏えいやウィルス感染を最小限に抑えることが出来る。また、クライアント コンピュータにノート型コンピュータを利用していて、出先で盗難にあった場合でも、サーバー上のコントロールでアカウントを無効にできる。</li>
<li>CAD 製品はサーバーコンピュータ上で動作するため、クライアント コンピュータに高スペックなものは要求されない。</li>
</ul>
<p>&#0160;</p>
<p><strong>パフォーマンス評価</strong></p>
<p>利点だけを見ると、テクノロジの進歩によって実現した、昔では考えられない夢の仕組みに見えてしまいます。ただ、オートデスクがサポートする画面転送方式では、サーバー上で動作させた CAD 製品の画面をクライアントに転送して、マウスやキーボード操作をサーバーに送り返すことで、間接操作をしていることになります。</p>
<p>ここでは、サーバーコンピュータの演算能力やグラフィックス処理能力、また、ネットワークの帯域など、いままでの CAD 利用では想像できないハードルが存在するのが事実です。実際、クライアント コンピュータに CAD 製品をインストールして実行する場合と比較すると、画面の更新やマウスの追従性に遅延が発生します。環境によって結果は異なるので、導入前に、CAD を利用されている設計者が参加しての十分な評価をお勧めします。次の動画は、Citrix XenApp 上と、ローカル コンピュータ上にインストールした AutoCAD の様子を録画したものです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/h7QruduinrM?feature=oembed" width="500"></iframe>&#0160;</p>
<p style="text-align: left;">この Citrix XenApp 上の AutoCAD インスタンスは、他のユーザが利用していない環境で録画したものなので、サーバーはこのユーザが占有している状態です。従って、比較的高いパフォーマンスを維持しているように見えますが、他のユーザが別のクライアント コンピュータから同時にアクセスした場合や、ネットワーク帯域が狭い、あるいはトラフィック量が高い場合には、当然、反応が遅くなることになります。</p>
<p>下記に指標を記した Autodesk Knowledge Network がありますので、ご参照ください。</p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tzgg.html" rel="noopener noreferrer" target="_blank">Citrix XenApp対応版AutoCADの推奨ハードウエア及びネットワーク環境について</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u0kQ.html" rel="noopener noreferrer" target="_blank">Citrix XenApp対応版Autodesk製品はハイパーバイザー環境をサポートしていますか？</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u0kD.html" rel="noopener noreferrer" target="_blank">Citrix XenApp対応版AutoCADのパフォーマンスを改善するための推奨設定について</a></strong></p>
<p>事前評価や導入時には、仮想化ベンダーや販売店に相談されることを強くお勧めします。ローカル コンピュータにインストールした製品をお使いの設計者の方の中には、ちょっとした遅延に違和感を感じられる方もいらっしゃいます。もし、評価過程で満足のいく操作感や感触が得らる場合には、仮想化環境でのCAD製品の運用に支障がないと言えるのかも知れません。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
