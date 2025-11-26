---
layout: "post"
title: "CPU とグラフィックスカード"
date: "2013-07-17 00:07:26"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/07/cpu-and-graphics-card.html "
typepad_basename: "cpu-and-graphics-card"
typepad_status: "Publish"
---

<p>今日は少し目線を変えて、AutoCAD や AutoCAD LT の使用環境について考えてみたいと思います。ここで言う使用環境とは、みなさんが日頃お使いのコンピュータということになります。</p>
<p>オートデスク製品に限らず、CAD ソフトウェアは一般的に「重い」ソフトウェアの部類に入ると言われています。その要因になるのが、</p>
<ul>
<li>2D 図面では精緻はベクトル図形を多数表示するため&#0160;</li>
<li>3D モデルをマテリアル（材質）と共に表示するため</li>
<li>メモリ消費量が多めに遷移する</li>
</ul>
<p>になるかと思います。これらが「重い」というネガティブな反応になっているものと思います。</p>
<p>具体的には、2D 図面しか扱っていないのにマウスカーソルの動きが悪い、などが最近よく聞かれる現象です。また、3D オブジェクトやユーザインタフェースの表示に関しては、次のような表示上の崩れを指摘されることもあります。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15cc646970c-pi" style="display: inline;"><img alt="3DGraphicsFails" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15cc646970c image-full img-responsive" src="/assets/image_547553.jpg" title="3DGraphicsFails" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e416fe9970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><br /></a><br />このような問題を解決していただくためにも、次の要素について環境を整える、あるいは、既存の環境を最適化を検討したいただくことをお勧めします。</p>
<p>&#0160;</p>
<p><strong>プロセッサ（CPU）＋ Windows プラットフォーム</strong></p>
<p style="padding-left: 30px;">使用するハードウェア（コンピュータ）には、CPU と呼ばれる演算プロセッサが必ず搭載されています。CPU は、パーソナル コンピュータの発展とともに、16 ビット から 32 ビット、64 ビットの順に発展を続けていて、大きなビット数の CPU の処理能力がより高いとされています。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104377cfd970c-pi" style="display: inline;"><img alt="CPU" class="asset  asset-image at-xid-6a0167607c2431970b019104377cfd970c" src="/assets/image_857159.jpg" title="CPU" /></a></p>
<p style="padding-left: 30px; text-align: left;">Windows オペレーティング システム（OS）を使ったコンピュータ上では、複数のソフトウェアを実行することができます。そして、OS 上の実行するソフトウェアには、32 ビット版と 64 &#0160;ビット版が存在するのはご存じと思います。実は、Windows OS 自身にもビット数の差が存在します。32 ビットCPUが主流だった頃には Windows も32 ビット版でしたが、64 ビット CPU が登場したここ数年は、Windows に 64 ビット化が進んでいます。64 ビット版の Windows は、64ビット CPU を搭載したハードウェアにしかインストールすることができません。</p>
<p style="padding-left: 30px; text-align: left;">少し紛らわしいのですが、32ビット版 Windows 上では32ビット版のソフトウェアは動作しますが、64ビット版のソフトウェアは動作しません。逆に、64 ビット版Windows 上では32ビット版と64ビット版のソフトウェアともに動作させることが出来ます。特に、64 ビット版Windows 上では32ビット版のソフトウェアともに動作させる場合には、「互換モード」と呼ばれる仕組みで動作します。ただし、ソフトウェアによっては、互換モードをサポートしないものもあります。例えば、AutoCAD や AutoCAD LT は、互換モードをサポートしないので、64 ビット版Windows 上で 32ビット版の AutoCAD や AutoCAD LT のインストールや実行が出来ません。</p>
<p style="padding-left: 30px; text-align: left;">なぜでしょう？</p>
<p style="padding-left: 30px; text-align: left;">それは、CADの特性を新しいコンピュータ（CPU＋OS）で十分に生かし切るための判断に依るためです。</p>
<p style="padding-left: 30px; text-align: left;">Windows 上にインストールして実行する各種ソフトウェア、つまり、AutoCAD にも、32 ビット版と 64 ビット版があります。重要なのは、32ビット版 Windows と 64 ビット版 Windows では、ソフトウェアが利用可能なメモリ サイズに大きな違いがある点です。なお、実行スピードは、純粋に CPU に依存しますので、32ビット CPU に比べて 64 ビット CPU が必ずしも高速であるとは言えません。</p>
<p style="padding-left: 30px; text-align: left;">次の図は、32ビット版 Windows 上で動作する32ビット版ソフトウェアが利用できるメモリ量と、64ビット版 Windows 上で動作する64ビット版ソフトウェアが利用できるメモリ量を表したものです。64ビット版では、ソフトウェアは理論上 4TB まで利用できることがわかります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7d2f887970b-pi" style="display: inline;"><img alt="Memory" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7d2f887970b image-full img-responsive" src="/assets/image_983697.jpg" title="Memory" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192ac00cc8b970d-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><br /></a><br />特に、大きなラスター画像を挿入した図面や 3D オブジェクトを多用する場合には、大きなサイズのメモリ空間で作業できるよう、64 ビット版の Windows 上で 64 ビット版の AutoCAD を利用することをお勧めします。</p>
<p style="padding-left: 30px;">AutoCAD では AutoCAD 2008 以降、AutoCAD LT では AutoCAD LT 2009 以降、製品パッケージには 32 ビット版 と 64 ビット版が同梱されています。インストール時には、自動的に Windows のビット数に合わせた AutoCAD がインストールされます。32 ビット版 Windows には、64 ビット版の AutoCAD をインストールすることはできません。同様に、64 ビット版 Windows には、32 ビット版の AutoCAD をインストールすることはできません。</p>
<p style="padding-left: 30px;">API カスタマイズを利用して大規模なバッチ処理を行うような場合にも、64 ビット環境でメモリ不足に陥らないよう配慮することを忘れないでください。32&#0160;ビット環境でバッチ処理を使って大量にメモリを消費すると、場合によって AutoCAD がクラッシュしてしまうことがあります。そんなとき、よくタスクマネージャで acad.exe のメモリ量を見てメモリ不足かどうか判断される方がいらっしゃいますが、タスクマネージャに表示されるメモリ量は仮想メモリなどが記載されません。そのような場合は、次の Autodesk Kowledge Network を参照してみてください。</p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tzeV.html" target="_blank">AutoCAD 使用時のメモリ不足のメッセージについて</a></strong></p>
<p><strong>グラフィックス カード</strong></p>
<p style="padding-left: 30px;">AutoCAD で外部参照を含む大規模な&#0160;2D&#0160;図 面作成や、3D モデリングやプレゼンテーションを行う場合、より高度なグラフィックス表現が重要になります。通常、このような処理には CPU やメモリへの負荷が大きくなるので、グラフィックス表現専用のハードウェアを装備することが一般的です。このハードウェアは、コンピュータに後付けする「基板」として提供されることから、<strong>グラフィックス カード</strong> と呼ばれています。</p>
<p style="padding-left: 30px;">グラフィックス カードを導入することで、作図やマウスカーソルの動きが軽快になったり、3D オブジェクトやユーザインタフェースで発生する問題が解決することがあります。</p>
<p style="padding-left: 30px;">グラフィックスカードを導入した後には、ソフトウェア上でも正しくグラフィックスカードの能力を引き出せるよう、調整が必要になります。例えば、AutoCAD 上では 3DCONFIG コマンドで <strong>ハードウェア アクセラレーション</strong> の設定を有効にする必要があります。ハードウェア アクセラレーションを有効化すると、AutoCAD は CPU ではなく、グラフィックスカードを使って描画（内部演算）を行うようになります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15cc649970c-pi" style="display: inline;"><img alt="AutoCAD" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15cc649970c image-full img-responsive" src="/assets/image_692228.jpg" title="AutoCAD" /></a></p>
<p style="padding-left: 30px;">もちろん、グラフィックス カードの使用は任意です。ただし、もし新しいコンピュータの導入を検討されているなら、オートデスク認定グラフィックス カードの採用を強くお勧めします。オートデスクでは、AutoCAD をはじめとしたオートデスク製品にあったパフォーマンスを持つグラフィックス カードを、<strong>認定ハードウェア</strong>として公開しています。</p>
<p style="padding-left: 30px;">AutoCAD や Design Suite 製品を含む他のオートデスク製品でテストされた認定ハードウェア（グラフィックス カード）とドライバの組み合わせの一覧は、<span style="font-size: 11pt;"><strong><a href="http://www.autodesk.co.jp/graphics-hardware" target="_blank">http://www.autodesk.co.jp/graphics-hardware</a></strong></span>&#0160; で検索、参照することができます。</p>
<p style="padding-left: 30px;">2D 作図を中心にAutoCAD や AutoCAD LT を利用する場合には、通常、グラフィックスカードを特に意識する必要はありません。ただし、下記のような Windows 7 上でのさまざまな問題の発生を受けて、2D 作図を目的とした場合でも、グラフィックス カードを利用したハードウェア アクセラレーションが利用可能になっています。</p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u0lz.html" target="_blank">Windows 7でハードウェアアクセラレータがOFFの場合のカーソル表示の問題</a></strong></p>
<p style="padding-left: 30px;">この FAQ にも記載されていますが、マウスカーソルの動きが遅い、といった問題もグラフィックスカードの導入、ないし、ハードウェア アクセラレーションを有効化することで解決できる場合があります。もちろん、認定ハードウェアとしてテストされたグラフィックスカードとドライバ バージョンをお使いいただくのがベストです。AutoCAD LT 2012 以降、AutoCAD LT にもハードウェア アクセラレーションを利用する 3DCONFIG コマンドが用意されています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15cc650970c-pi" style="display: inline;"><img alt="AutoCAD LT" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15cc650970c img-responsive" src="/assets/image_742649.jpg" title="AutoCAD LT" /></a><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901e418338970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><br /></a></p>
<p>お使いのコンピュータが、何ビット版なのか、通常はあまり気にされないはずです。Windows Vista 以降の Windows であれば、システム情報として OS のビット数を確認することができます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d15cc664970c-pi" style="display: inline;"><img alt="System" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d15cc664970c image-full img-responsive" src="/assets/image_528472.jpg" title="System" /></a></p>
<p><a href="https://ja.wikipedia.org/wiki/X64" target="_blank">64 ビットCPU</a>を持つコンピュータなのに、32ビット版Wndows を搭載していたりするかも知れません（もったいない）。これを期に、一度確認してみてはいかがでしょうか？</p>
<p>By Toshiaki Isezaki</p>
