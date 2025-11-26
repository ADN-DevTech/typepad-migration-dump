---
layout: "post"
title: "Autodesk ReMake 登場"
date: "2016-05-30 00:01:55"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/05/autodesk-remake-released.html "
typepad_basename: "autodesk-remake-released"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c864aca1970b-pi" style="display: inline;"><img alt="Remake" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c864aca1970b image-full img-responsive" src="/assets/image_632087.jpg" title="Remake" /></a></p>
<p>複数の写真やレーザー スキャン データから 3D メッシュを生成する Autodesk Memento が、Autodesk ReMake として生まれ変わりました。Autodesk Memento は、比較的長い間、技術評価や使い勝手等のフィードバックを得る目的で Beta 版の扱いで公開されてきましたが、精度の向上や用途の多様化を受けて、正式にリリースされました。クライアント コンピュータにインストールするデスクトップ製品として利用することになりますが、同じく、写真から 3D メッシュを生成する <strong><a href="https://recap360.autodesk.com/" target="_blank">ReCap 360 クラウド サービス</a></strong>&#0160;とシームレスに連携することが出来るハイブリッドな製品になっています。</p>
<p>ReMake の大きなと特徴は、インストールしたローカル コンピュータを使って、写真から 3D メッシュを演算で生成できる点です。この演算には、ReCap 360 クラウド サービスのようにクラウド クレジットは不要なため、高性能なハードウェアが手元にあれば、いつでも 3D メッシュを生成することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1ef677a970c-pi" style="display: inline;"><img alt="Remake local compute" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1ef677a970c image-full img-responsive" src="/assets/image_312032.jpg" title="Remake local compute" /></a></p>
<p>ただ、ローカル コンピュータ上で演算させる場合は、かなり高いハードウェアが必要になります。残念ながら、CPU 自身に GPUを内蔵する Core i プロセッサなどを搭載するコンピュータでは、ローカル演算しようとすると、次のようなエラーを表示して演算を継続することは出来ません。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1eee9e5970c-pi"><img alt="System requirement warning" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1eee9e5970c img-responsive" src="/assets/image_410609.jpg" title="System requirement warning" /></a></p>
<p>ちなみに、ローカル演算に推奨される動作環境は次のとおりです。</p>
<ul>
<li>Microsoft Windows 7 以降の 64 ビット版 Winows OS&#0160;</li>
<li>128GB RAM</li>
<li>100GB の空き領域がある NVMe PCIe SSD</li>
<li>NVIDIA GFX グラフィックス カード:
<ul>
<li>1 つ以上のQuadro M6000 cards、<br />または、<br />1 つ以上の&#0160;12 GB 専用ビデオ メモリを持つ GeForce Titan X</li>
</ul>
</li>
<li>マルチ Xeon プロセッサ</li>
</ul>
<p>一方、クラウド演算で推奨される動作環境は、かなり、一般的なコンピュータに近くなります。</p>
<ul>
<li>Microsoft Windows 7 以降の 64 ビット版 Winows OS</li>
<li>12GB 以上の RAM</li>
<li>マルチ コア プロセッサ</li>
<li>3GB 以上の専用ビデオ メモリを持つ&#0160;ディスクリート グラフィックス カード</li>
<li>SSD ストレージ</li>
</ul>
<p>このように、ローカル演算には高価な上級ワークステーションが必要です。同時に、ReMake の実行時に多くのメモリ空間を必要するため、 64 ビット版 でのみでお使いいただけます。残念ながら、デスクトップ版 ReCap 360 と同様、32 ビット版では利用できません。なお、現在、Windows 用製品のみが利用可能になっていますが、まもなく、Mac 版が利用可能になる予定です。また、用意されているユーザインタフェースは、英語のみになっています。</p>
<p>やはり、写真から 3D メッシュを生成する現実的な選択肢には、ReMake 上からReCap 360 クラウド サービスを利用する方法が主流になるものと考えられます。この場合、生成する品質やエクスポートするファイル形式によっては、クラウド演算にクラウド クレジットが必要になりますのでご注意ください。ReMake 上から ReCap 360 クラウド サービスにプロジェクトを作成して 3D メッシュを生成する手順は、次の動画を参照してみてください。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/wRWo3r-woMI?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>生成された 3D メッシュ データは、各種用途に合わせて、クリーンアップ、修正、編集、尺度変更、計測、再トポロジ化、データの間引き、位置合わせ、比較、最適化などの加工が可能です。もちろん、メッシュの破れを修復して STL ファイルで出力すれば、3D プリントも容易です。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/XLYqdCubedA?feature=oembed" width="500"></iframe>&#0160;</p>
<p>さまざまなワークフローに対応するという意味では、他のオートデスク製品との連携で更に用途が広がるはずです。例えば、デスクトップ版の&#0160;Autodesk ReCap 360 と連携することで、レーザー スキャナ データから生成されたメッシュのクリーンアップ、修正、編集、最適化を行って、後工程で利用しやすい状態を作り出すことも出来ます。&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/fZkJ4kGHRdc?feature=oembed" width="500"></iframe>&#0160;</p>
<p>生成した 3D メッシュモデルを Fusion 360 で T-Spline モデルに変換すれば、工業デザインに有機的な形状を取り入れた再利用も簡単です。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/7F01N6zZOdo?feature=oembed" width="500"></iframe>&#0160;</p>
<p>さて、Autodesk ReMake には、無償版と有償版の Pro の2 タイプが用意されています。それぞれの機能差は次の比較表とおりです。無償版は <strong><a href="https://memento.autodesk.com/try-remake" target="_blank">https://memento.autodesk.com/try-remake</a></strong>&#0160;からダウンロードしてお使いいただけます。製品利用時には Autodesk ID アカウントで識別されるので、<strong><a href="http://store.autodesk.co.jp/store/adskjp/ja_JP/pd/ThemeID.29255400/productID.2307138600" target="_blank">Autodesk ストア</a></strong>&#0160;で Pro 版のサブスクリプションを購入いただければ、Pro 版の機能が利用出来るようになります。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c865135f970b-pi" style="display: inline;"><img alt="Remake_comparison" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c865135f970b image-full img-responsive" src="/assets/image_430563.jpg" title="Remake_comparison" /></a></p>
<p>Autodesk ReMake は、15日間の無償体験期間が設定されています。ローカル演算も含め、その機能をご確認ください。</p>
<p>By Toshiaki Isezaki</p>
<p>（三浦さん、写真使わせていただきました。ありがとうございます！）</p>
