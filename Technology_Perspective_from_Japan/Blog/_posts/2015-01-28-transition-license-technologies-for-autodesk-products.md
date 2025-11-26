---
layout: "post"
title: "オートデスク製品のライセンス テクノロジの遷移"
date: "2015-01-28 01:42:21"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/01/transition-license-technologies-for-autodesk-products.html "
typepad_basename: "transition-license-technologies-for-autodesk-products"
typepad_status: "Publish"
---

<p>比較的古い OS で古いバージョンの AutoCAD をお使い方もいらっしゃるかと思いますが、ハードウェアの更新などで新しい OS を採用するよう場面では、AutoCAD 本体だけでなく、セキュリティ システム自体が OS に対応していない場合も存在します。今回は、AutoCAD の<span style="text-decoration: underline;">スタンドアロン ライセンスが採用してきたライセンス システム</span>について、まとめておきたいと思います。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0c8399d970c-pi" style="display: inline;"><img alt="License_transition" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0c8399d970c image-full img-responsive" src="/assets/image_596236.jpg" title="License_transition" /><br />&lt;ライセンス システム遷移アニメーション&gt;</a></p>
<p>&#0160;</p>
<p><strong>ハードウェアロックの時代 －&#0160;<strong>AutoCAD EX-2 ～ AutoCAD 2000</strong></strong></p>
<p style="padding-left: 30px;">当初、商用販売されていた AutoCAD は、<strong>ハードウェア ロック&#0160;</strong>と呼ばれるライセンス保護システムを採用していました。具体的には、AutoCAD パッケージ内に、フロッピー ディスクや CD-ROM といったインストールメディアと共に&#0160;<a href="http://ja.wikipedia.org/wiki/%E3%83%89%E3%83%B3%E3%82%B0%E3%83%AB" target="_blank"><strong>ドングル</strong></a>&#0160;と呼ばれるハードウェアが同梱されていて、AutoCAD を実行するコンピュータのプリンタ ポートに接続して利用することが要求されていました。<strong>&#0160;ドングル</strong>&#0160;が接続されていないと、AutoCAD を起動することが出来ない仕組みです。</p>
<p style="padding-left: 30px;">ドングルの形状は、当時主流だった NEC PC-9801&#0160;アークテクチャと、黒船的に登場した DOS/V アーキテクチャで異なっていました。前者がパラレル インタフェース用、後者は RS-232C で利用する D-Sub 25 ピンのシリアル インタフェース用に分かれていたため、AutoCAD ソフトウェアと同様に、NEC 版と DOS/V 版でドングルの互換性もない状態でした。</p>
<p style="text-align: center;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0c9f68a970c-pi"><img alt="Hardware_lock" src="/assets/image_17120.jpg" title="Hardware_lock" /><br /></a>&lt;DOS/V 用のドングル&gt;</p>
<p style="padding-left: 30px;">ハードウェア ロックのライセンス保護システムでは、ドングルがないと AutoCAD を利用することが出来ないため、ライセンス保護の観点では非常に分かり易いライセンス システムと言えます。ただ、プリンタ ポートへの接続が必要だったため、コンピュータの電源が入った状態でドングルの抜き差しをしてしまうと、稀に物理的な破損が発生してしまいます。また、プリンタポートとの内部通信には、OS レベルのドライバを介す必要があったため、一部のコンピュータで正しくドングルを認識しない、という問題もあったようです。</p>
<p style="padding-left: 30px;">※ 当然、ドングルはバージョン識別を持っているので、異なるバージョン間で利用することが利用するようなことは出来ません。&#0160;</p>
<p><strong>ソフトウェア ロックの第一世代 －&#0160;AutoCAD 2000i ～ AutoCAD 2004</strong></p>
<p style="padding-left: 30px;">ハードウェア アーキテクチャ差を吸収したり、破損ドングル発生時のリカバリにかかる時間の低減を目的に、ドングルを利用しないライセンス保護システムが、AutoCAD 2000i で初めて導入されました。ここで採用されたのは旧Macrovision 社（現 Flexera Software 社）の&#0160;<strong>C-Dilla</strong>&#0160;システムです。このライセンス システムは、、<strong>ソフトウェア ロック</strong>&#0160;と呼ばれることがあります。</p>
<p style="padding-left: 30px;">ソフトウェア ロックを採用する AutoCAD 2000i からは、ドングルの替りに&#0160;<strong>アクティベーション</strong>&#0160;という作業が必要になります。アクティベーションは、AutoCAD をインストールしたコンピュータのみで製品を利用できるようにするための処理です。ライセンス情報はハードディスクに書き込まれるので、ハードディスクの全フォーマットはライセンスの失効を意味しますが、ドングル破損 &gt;&gt; &#0160;郵送による交換 の間、AutoCAD が利用できなくなる期間を短縮することが出来ました。</p>
<p style="padding-left: 30px;">一方、C-Dilla システムは、ハードウェアの不正改ざんによるライセンスの利用を抑止するため、ハードドライブの接続状況の維持が必要でした。不用意にハードディスクを増設してしまうと、アクティベーションが無効になってしまい、再度、アクティベーションを求められる事象も発生してしまいます。特に、AutoCAD 2002 のリリース直後に Windows XP が発売された際には、Windows XP 上で C-Dilla が正しく動作せず、特別なパッチを適用するよう、ご案内していたこともあります。</p>
<p style="padding-left: 60px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tzKo.html" target="_blank">起動時のエラー： 「エラー：cdilla dllのロード !!」</a></strong></p>
<p><strong>ソフトウェア ロックの第二世代 －&#0160;AutoCAD 2005 ～ AutoCAD 2009</strong></p>
<p style="padding-left: 30px;">その後、C-Dilla が登場した頃に、メジャーではなかったデバイスが登場し始めます。USB ドライブです。USB デバイスを不正と認識しないよう、ライセンス保護システムも時代に合わせてアップデートされています。AutoCAD 2005 で採用された&#0160;旧Macrovision 社（現 Flexera Software 社）の&#0160;<strong>SafeCast</strong> システムです。SafeCast もソフトウェア ロックなので、製品を利用するためにアクティベーションが必要です。</p>
<p style="padding-left: 30px;">C-Dilla 世代からの更新で利点として使用できるようになったのは<strong>、ライセンス ポータブル ユーティリティ</strong>、通称、<strong>PLU</strong>&#0160;です。PLU は、一度アクティベーションしてしまったコンピュータから、ライセンス情報をフロッピー ディスクに書き出して（抜き出して）、別のコンピュータに移動することを可能にする仕組みです。もちろん、ライセンスを書き出してしまったコンピュータ上では、AutoCAD を起動できなくなります。これによって、ソフトウェア使用許諾の範囲内で、ライセンスを移動しながら AutoCAD を運用することが出来るようになりました。</p>
<p style="padding-left: 30px;">ただし、PLU を使ったライセンスの移動操作が少し煩雑なためか、ライセンスの移動中にライセンスが失効してしまうような「事故」もあったようです。</p>
<p style="padding-left: 60px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u0P3.html" target="_blank">AutoCAD でライセンスがインポートできない</a></strong>&#0160;</p>
<p><strong>ソフトウェア ロックの第三世代 －&#0160;AutoCAD 2010 ～ AutoCAD 2015（現在）</strong></p>
<p style="padding-left: 30px;">更に時代は進み、ハードディスク ドライブ（HDD）に加え、ソリッド ステート ドライブ（SSD）が搭載されることが増え、ライセンス保護システムも更新されます。Flexera Software 社の&#0160;<strong>FlexNet Publisher</strong> です。FlexNet Publisher は、現在ではネットワーク ライセンスでも利用されるテクノロジですが、スタンドアロン ライセンスでは、従来と同じくソフトウェア ロックを使用するので、アクティベーション作業は必須です。</p>
<p style="padding-left: 30px;">また、PLU に代わって、物理的なメディアを介在させない、より簡単な&#0160;<strong>オンライン転送ユーティリティ</strong>（Online Transfer Utility）、通常、<strong>OLT</strong>&#0160;が利用できるようにもなっています。</p>
<p style="padding-left: 60px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tzyu.html" target="_blank">OLT: ライセンス転送ユーティリティ(LTU)の使用方法について</a></strong></p>
<p>さて、このように、OS やOS 内で利用するドライバ、一般的なポータブル メディア（フロッピー &gt;&gt; USB）などの環境変化によって、スタンドアロン ライセンスでも採用するテクノロジが変化している点を理解いただけたと思います。また、ライセンス保護システムについては、オートデスク純正ではなく、広く世界で利用されているシステムを採用しています。結果として、その時々の OS と AutoCAD バージョンあった組み合わせで、製品を運用していただくことを推奨せざるおえない、といういのも事実です。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;&#0160;</p>
