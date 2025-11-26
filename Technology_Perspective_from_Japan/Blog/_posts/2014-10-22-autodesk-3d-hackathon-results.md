---
layout: "post"
title: "オートデスク 3D ハッカソン - 結果発表"
date: "2014-10-22 00:28:52"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/10/autodesk-3d-hackathon-results.html "
typepad_basename: "autodesk-3d-hackathon-results"
typepad_status: "Publish"
---

<p>今日は 10 月 4 日、18 日の 2 日間に渡って開催した <strong>オートデスク 3D ハッカソン</strong> の成果をご紹介しましょう。このイベントで提供したテクノロジ（API）は、View &amp; Data Web サービス API &#0160;と ReCap Photo API の 2 つです。詳細については、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/07/technologies-on-autodesk-3d-hachathon.html" target="_blank">こちら</a>&#0160;</strong>でご案内しています。また、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/10/autodesk-3d-hackathon-day1.html" target="_blank">Day1</a></strong>、<a href="http://adndevblog.typepad.com/technology_perspective/2014/10/autodesk-3d-hackathon-day2.html" target="_blank"><strong>Day2</strong></a> の模様は、少し前のブログ記事で簡単にご案内しています。</p>
<p>オートデスクが主催する初めてのハッカソンということで、認知度も高くはないのですが、全国各地からお集まりいただいた方同士、計 4 つのチームでアイデアを出し、実現に向けた開発をしていただきました。</p>
<p>各チームの発表をダイジェスト動画と概要です。</p>
<p><strong>Castle 360</strong></p>
<p style="padding-left: 30px;">8K、膨大なデータ、といった最近のトレンドと、秘境、遺構 、という普段その場に赴くことが出来ない、あるいは経験することができない体験を、臨場感を持って、テクノロジで具現化するというアイデアです。より身近なユーザ体験として、ここではサッカースタジアムを題材に、View &amp; Data Web サービス API &#0160;と ReCap Photo API を利用した成果物を発表いただきました。</p>
<p style="padding-left: 30px;">サッカースタジアムの各箇所に配置された 8K カメラがの映像が、Web ページの右手に表示され、見たい画面をクリックすることで、すぐさま、その模様を画面中央に呼び出すことが出来ます。カメラの配置位置は、スタジアムに固定されたものから、選手のウエラブル カメラまで多彩、という仮定です。カメラからの動画を表示しているエリアは、View &amp; Data Web サービス API で実現されています。View &amp; Data Web サービス API では、直接動画ファイルの再生をサポートしていませんが、ベースになっている Three.js ライブラリを併用することで、ビューワ内でこれを実現しています。</p>
<p style="padding-left: 30px;">また、3D で目線や周囲の状況を「体験」するために、Web ページ下部には「3D 切り取り」ボタンが配置されています。このボタンをクリックすると、ReCap Photo が各カメラの画像を合成して 3D モデルが生成される、いった機能もあります。実際は、この部分までは実装出来ていませんでしたが、8K カメラの解像度とカメラ台数はそろえば、実現できるのかもしれません。</p>
<p style="padding-left: 30px;">披露されたデモでは、大人の事情を考慮して、サーカーのボードゲームを持ち込んで、撮影した動画素材が利用されていたのが印象的でした。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/STSRbVrpbXY?feature=oembed" width="500"></iframe>&#0160;</p>
<p><strong>Team Okamoto</strong></p>
<p style="padding-left: 30px;">少子高齢化を見据えたホーム オートメーションのアイデアを、View &amp; Data Web サービス API で実現しようとするアイデアです。</p>
<p style="padding-left: 30px;">軽井沢にある別荘地の管理人という想定で、地域にある管理対象の別荘を地図から指定して、その建物をコントロールしていきます。地図の部分を Google Map API で、建物の看取り図を 3D で表示する部分を&#0160;View &amp; Data Web サービス API で、それぞれ実装しています。</p>
<p style="padding-left: 30px;">表示した 3D モデル上では、照明器具のような電気設備のオン、オフをコントロール出来ます。View &amp; Data Web サービス API では、3D モデルの選択イベントを取得することが出来るので、操作を処理出来ます。ただし、残念ながら、表示した 3D モデルのプロパティの書き換えや照明器具の光源をリアルタイムに処理できないため、この部分は別画面で表現されていました。類似した要望はすでにあがっているようなので、将来、そのような動作がサポートされることを期待したいところです。</p>
<p style="padding-left: 30px;">チームには、BIM の概念をマンション設計に応用されている方が参加されていましたので、かなり具体的な内容になっていたと思います。実際には、BIM といっても CAD ベンダーによって扱うファイル形式が異なったり、また、ホーム オートメーションのインタフェースや仕組みもハウスメーカー各社で独自に作成されているそうです。このため、<a href="http://adndevblog.typepad.com/technology_perspective/2014/09/autodesk-360-and-supported-files.html" target="_blank">様々なファイル形式をサポートする</a>&#0160;View &amp; Data Web サービス API をフロントエンドのインタフェースに採用するアイデアは、とても現実的で、いますぐにでも実現できるもののように感じました。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/oM1n1BcRS90?feature=oembed" width="500"></iframe>&#0160;</p>
<p><strong>Hackshion</strong></p>
<p style="padding-left: 30px;">&#0160;「3D 革命で、人々を幸せに」のキャッチのもと、利用が進まない&#0160;e コマースを用いたアパレル販売を、テクノロジで活性化というするアイデアです。</p>
<p style="padding-left: 30px;">オンラインでジャケットなどの「服」を購入しようとする際、自身の体型や服のサイズとのミスマッチを心配して、購入を踏みとどまってしまう心理が働きます。この心理を、モバイル デバイスと ReCap Photo を組み合わせて、解消させようとする試みです。</p>
<p style="padding-left: 30px;">デモは、モバイル デバイス上に表示された Web サイトで服を購入する想定です。画面上にある [3D SIZE] ボタンをクリックすると、アプリが起動して写真撮影モードを選択出来ます。体を様々な方向から撮影した写真を複数アップロードすることで、ReCap Photo が実寸の 3D モデルを生成して、体型サイズを導き出し、ぴったりのサイズを推奨していくというストーリーでした。</p>
<p style="padding-left: 30px;">ユニークなのは、この仕組みを自社の販売のみに利用するのではなく、一連の処理を API として公開し、アパレル業界に向けてシステム全体を販売、展開するというビジネス モデルです。これによって、アパレル業界の発展にも貢献することが出来ます。チームには、オーダーメード スーツの販売を手がける会社を起業したばかりの方や、3D スキャナで 3D プリンタ ビジネスをされている方も参加されていたので、実際に切望されているようなアイデアとも受け取れました。</p>
<p style="padding-left: 30px;">残念だったのは、ReCap Photo API では、写真上の計測値を反映させる処理と、隣り合う写真同士の位置関係を関連付ける処理が自動化できない点です。これらには、どうしてもオペレータが介在した対話処理が必要になっています。また、ReCap Photo（ReCap 360） で対話操作をした場合にも、クラウドとのコミュニケーションや演算で時間がかかり、リアルタイムに応答を得ることが難しかったようです。これを受けて、<a href="https://beta.autodesk.com/callout/?callid=D69655022B4F42C880513D4E3085C7D0" target="_blank">Project Memento </a>を利用する代替策を見出したそうです。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/GYhjYaMGVBg?feature=oembed" width="500"></iframe>&#0160;</p>
<p>&#0160;<strong>ReCap利用法</strong></p>
<p style="padding-left: 30px;">チーム名がアイデアを指しているチームです。鋭角な形状生成が苦手な ReCap Photo の特性を習得された上で、食品サンプルへの応用と、SNS などのコミュニケーションに利用するアイデアを提案されていました。</p>
<p style="padding-left: 30px;">レストラン前に展示されている食品サンプルは、とても精巧で本物に近い質感を持ってます。この食品サンプルを 3D で、かつ、Web 上で表現出れば、オンライン販売での売り上げ増にもつながるのでは、という考えです。ReCap Photo の素材は実際に撮影された写真なので、照明を適切に調整することで、形状に正しくマッピングした 食材の 3D モデル生成も可能かも知れません。</p>
<p style="padding-left: 30px;">SNS 利用では、遠く離れて住んでいるおじいちゃん、おばあちゃんのもとへ、お孫さんの 3D モデルでシェアすることが出来というストーリーでした。小学校でも IT を活用した授業が進んでいますし、実際、Web にアクセスできるスマートフォンは小学生にも普及しています。写真を撮影して 3D モデルを作成するような操作も、さほど難易度は高くないのかもしれません。</p>
<p style="padding-left: 30px;">ReCap利用法チームは、Day2 当日、メンバの方が 1 名欠席されてしまったので、2 名での作業になってしまいました。少し残念でしたが、ReCap Photo API と View &amp; Data Web サービス API 用に提供されているサンプルを結合して、写真のアップロードと 3D モデル生成、3D モデルのダウンロード、3D モデルのアップロードと表示を一連の流れをデモできるところまで実現されていました。&#0160;</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/RZiXoxFh58Q?feature=oembed" width="500"></iframe>&#0160;</p>
<p><strong>投票結果</strong></p>
<p>各チームの発表後、参加メンバとスタッフで 1 票づつ投票を実施し、僅差で Hachshion チームが優秀賞となりました。アパレル業界の e コマースの利用の割合いや市場規模も含め、具体的な数字を提示できた点、ビジネスモデルまでストーリーが出来上がっている点、などが評価されたものと思います。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6f69417970b-pi" style="display: inline;"><img alt="P1000871s" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6f69417970b image-full img-responsive" src="/assets/image_818467.jpg" title="P1000871s" /></a></p>
<p>実際にマネキンに服を着せて 3D モデルの作成を試されたようですが、発表時には信楽焼のタヌキ像でデモされていたのが愉快でした。ロケハン、お疲れ様です。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d080aa35970c-pi" style="display: inline;"><img alt="Shigarakiyaki" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d080aa35970c image-full img-responsive" src="/assets/image_364301.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Shigarakiyaki" /></a></p>
<p>欧米と比べ、日本ではハッカソンというイベントが定着していないと痛感しましたが、これに懲りず、今後も同様の活動を続けていく予定です。次の機会、みんさんにお会いできることを期待しています。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb079bc132970d-pi" style="display: inline;"><img alt="Hackathon_fun" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb079bc132970d image-full img-responsive" src="/assets/image_345183.jpg" title="Hackathon_fun" /></a>&#0160;</p>
<p style="text-align: left;">さて、初めてのハッカソンの企画、開催、それに先立っての Meetup 開催と慣れないことばかりでしたが、テクノロジ企業を標榜するオートデスクとしても有意義なイベントだったと感じています。</p>
<p>ご協力いただいたすべての皆様、ご参加いただいた皆様、どうもありがとうございました !!</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
