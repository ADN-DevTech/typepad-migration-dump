---
layout: "post"
title: "AU Japan 2015：Mozilla Japan WoT セッション ご紹介"
date: "2015-09-07 00:53:47"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/09/au-japan-2015-mozilla-japan-wot-session.html "
typepad_basename: "au-japan-2015-mozilla-japan-wot-session"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb086e4a47970d-pi" style="float: right;"><img alt="Auj2015_logo" class="asset  asset-image at-xid-6a0167607c2431970b01bb086e4a47970d img-responsive" src="/assets/image_458011.jpg" style="width: 300px; margin: 0px 0px 5px 5px;" title="Auj2015_logo" /></a></p>
<p>Autodesk University Japan 2015 のカスタマイズ トラック セッションの 1 つである、<strong>H-2&#0160;<a href="https://reg34.smp.ne.jp/regist/is?SMPFORM=nerd-ogram-c914a68f7a813c785dc0540578bca65c&amp;c=H-2" target="_blank">MozOpenHard プロジェクト：コミュニティ主導で考え・作る WoT/IoT</a></strong>&#0160;をご紹介します。</p>
<p>このセッションは、無償の Web ブラウザ <a href="https://www.mozilla.org/ja/firefox/new/" target="_blank"><strong>Firefox</strong></a>&#0160;の開発元である &#0160;<a href="http://www.mozilla.jp/" target="_blank"><strong>Mozilla</strong></a> の日本法人、Mozilla Japan の方にご講演いただくセッションです。Mozilla には Firefox とは別のプロジェクトも複数存在していて、その中の 1 つが、セッションでご案内いただくことになっている <strong>MozOpenHard</strong>&#0160;プロジェクトです。Mozilla は非営利団体として活動しているため、プロジェクトはオープンなプロセスを通して開発、提供されています。</p>
<p><strong>IoT</strong> については、ブログ記事 <a href="http://adndevblog.typepad.com/technology_perspective/2015/08/about-internet-of-things.html" target="_blank"><strong>Internet Of Things とは</strong></a> でご紹介していますので、ここでは割愛しますが、オートデスク自身も先だって IoT サービスを提供する <strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/08/about-see-control.html" target="_blank">SeeControl 社の買収に合意</a>&#0160;</strong>した、非常にホットな概念であり、テクノロジです。</p>
<p>IoT &#0160;や&#0160;WoT&#0160;でキーになるのは、IP アドレスを持って外部とのコミュニケーションしながら各種機器をコントロールするハードゥエアです。MozOpenHard プロジェクトでは、このハードウェアに当たるものが、現在開発中の&#0160;<strong>CHIRIMEN ボード</strong>&#0160;です。</p>
<p>MozOpenHard のユニークな点は、Web テクノロジで CHIRIMEN ボード（電子回路）を利用しようとしている点です。MozOpenHard の Web ページ <a href="http://mozopenhard.mozillafactory.org/" target="_blank"><strong>http://mozopenhard.mozillafactory.org/</strong></a> を見てみてください。&#0160;左手にアニメーション画像、右手に HTML と JavaScript で書かれたコードが表示されるはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d153e302970c-pi" style="display: inline;"><img alt="Mozopenhard_web_page" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d153e302970c img-responsive" src="/assets/image_826292.jpg" title="Mozopenhard_web_page" /></a></p>
<p>IoT というと、C++ などの開発言語でドライバを開発するようなイメージが付きまといますが、Mizolla は Web 開発でよく利用される JavaScript で、これを行なおうとしています。上記&#0160;Web ページでは、右手にある短い JavaScript コードで左手の LED 点滅をコントロールしています。</p>
<p>この&#0160;CHIRIMEN ボードは、<span class="st">Boot to Gecko</span> を搭載した小さなコンピュータです。IoT/WoT といっても、必ずインターネットに接続しなければならない、というわけではありません。JavaScript ライブラリは、通常、インターネット接続されたサーバーにホストされていて、それを参照することでプログラミングするような仕組みですが、CHIRIMEN ボードの場合はファームウェアとして、あらかじめ <span class="st">Boot to Gecko が</span>組み込まれているので、CHIRIMEN ボードを中心にセンサーやモーターなどをつなげれば、JavaScript で、それらから情報を得たり、逆にコントロールしたりするこをが出来ます。下記の写真では、中央の小さな基板が CHIRIMEN ボードです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7cc0e03970b-pi" style="display: inline;"><img alt="写真 2015-09-11 16 00 06" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7cc0e03970b image-full img-responsive" src="/assets/image_946917.jpg" title="写真 2015-09-11 16 00 06" /></a></p>
<p>ご存じの方も多いはずですが、JavaScript は &#0160;C++ に比べて格段に習得が容易です。JavaScript なら自分にも IoT、WoT が出来るかも、と思われる方がいても当然と言えます。逆に、それが MozOpenHard の目的でもあるはずです。<a href="http://mozopenhard.mozillafactory.org/about" target="_blank"><strong>http://mozopenhard.mozillafactory.org/about</strong></a>&#0160;下部には、次のように書かれています。</p>
<blockquote>
<p><strong>ウェブブラウザはヒトがコンピュータネットワークを利用するための重要な役割を果たしてきました。さらに様々なアプリケーションを動作させるコンピューティングプラットフォームとしての役割も増しています。</strong><br /> <br /> <strong>IoT </strong><strong>や WoT といったコンセプトが示す、さまざまなモノがコンピュータネットワークで繋がる未来の社会においても、私たちヒトは重要な存在です。したがって、ヒトとモ ノがウェブを介して互いに協調しあえる環境が必要です。そして、それはウェブブラウザ核に作られていくのはないかと考えます。</strong><br /> <br /> <strong>本プロ ジェ クトは、このような環境（プラットホーム）を参加者の皆さんとオープンにデザインし、つくりあげていくことをめざします。また このような今後の新たな社会での“ウェブらしさ”とは何か、ウェブを介したよりよいヒト・モノの関係とは何かを、ディスカッション、プロトタイプ制作、利 用、改良を通して考え、提案していきます。</strong></p>
</blockquote>
<p>さて、JavaScript を利用できることで最も恩恵を受けるのが、Web デベロッパの方々でしょう。Google Maps API など、既に利用している Web サービス API があれば、MozOpenHard とのマッシュアップも容易なはずです。また、IoT で実現出来るのは、スイッチのオン、オフだけではありません。複数のモーター（アクチュエーター）を制御するような複雑なことも出来るはずです。</p>
<p>Autodesk University Japan 当日は、<span style="background-color: #ffff00;">View and Data API との連携と プロジェクションマッピングを含めた WoT の作成から成果物のデモ</span>まで、複数の利用法をご紹介してただけることになっています。展示ブースでは、その中のいくつかを実際に目にしていただけるはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d153f41f970c-pi" style="display: inline;"><img alt="Image" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d153f41f970c image-full img-responsive" src="/assets/image_167011.jpg" title="Image" /></a></p>
<p>誰にでも手の届く IoT、WoT が MozOpenHard です。Makers、Fabricators の方だけでなく、IoT の可能性や課題を把握したい方にもご参加いただきたいセッションです。</p>
<p>By Toshiaki Isezaki</p>
