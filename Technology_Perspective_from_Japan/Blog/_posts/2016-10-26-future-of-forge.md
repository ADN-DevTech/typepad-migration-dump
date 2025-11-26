---
layout: "post"
title: "Forge の未来"
date: "2016-10-26 23:03:27"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/10/future-of-forge.html "
typepad_basename: "future-of-forge"
typepad_status: "Publish"
---

オートデスクが提供しているクラウド サービス（SaaS）の要素技術を、Web サービス API として公開しているのが Autodesk Forge です。以前の&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/09/autodesk-forge-overview.html" target="_blank">ブログ記事</a></strong>&#0160;でご案内しているとおり、API &#0160;は &#0160;1 つだけでなく、複数用意されています。クラウドやモバイルの利用が一般的になった現在では、多様なテクノロジを持つ企業が同じように Web サービス API を公開しています。
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d231e5dd970c-pi" style="display: inline;"><img alt="Api_economy" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d231e5dd970c image-full img-responsive" src="/assets/image_194535.jpg" title="Api_economy" /></a></p>
<p>言い換えれば、専門外で自社内に成熟したテクノロジが存在していなくとも、公開された Web サービス API を流用・活用することで、1 つのサービスとして新しいソリューションを構築出来る環境が整っています。つまり、<strong><a href="https://ja.wikipedia.org/wiki/%E3%83%9E%E3%83%83%E3%82%B7%E3%83%A5%E3%82%A2%E3%83%83%E3%83%97_(Web%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0)" target="_blank">マッシュアップ</a>&#0160;</strong>&#0160;次第で上手にビジネス出来る時代です。こういった背景から、最近では <strong><a href="http://lmgtfy.com/?q=API+%E3%82%A8%E3%82%B3%E3%83%8E%E3%83%9F%E3%83%BC" target="_blank">API エコノミー</a></strong> という言葉が経済誌などでも散見されるようになっています。</p>
<p>さて、Forge に話を戻しましょう。現在、Forge が持っているコンセプトは次の項目で挙げることが出来ます。要約すると、「データはクラウドに集約して、クラウドに接続したクライアントから、必要に応じてデータを読み書きする仕組みを提供する」と捉えることが出来るはずです。もちろん、データとはデザイン データを指しています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb094af658970d-pi" style="display: inline;"><img alt="Forge_concept" class="asset  asset-image at-xid-6a0167607c2431970b01bb094af658970d img-responsive" src="/assets/image_923161.jpg" title="Forge_concept" /></a></p>
<p>クラウドの利用が普遍的になると、いままでとは違ったアプローチで設計や製造、施工といったタスクやワークフローを変えることが出来る可能性が出てきます。具体的には、デザイン データの扱いです。</p>
<hr />
<p><strong>現在のデザイン データの扱い</strong></p>
<p style="padding-left: 30px;">いままのでソフトウェアでは、「ファイル」という単位がデザイン データの形態になっています。ファイルは、CAD ソフトウェアを使った編集中には、デザイン データの情報はメモリ中に展開され、アーカイブされる時点で「ファイル」となります。ファイルになったデザインデータは、ファイル単位でバージョン管理されているはずです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb094af7fb970d-pi" style="display: inline;"><img alt="Low_frequency_data" class="asset  asset-image at-xid-6a0167607c2431970b01bb094af7fb970d img-responsive" src="/assets/image_178922.jpg" title="Low_frequency_data" /></a></p>
<p style="padding-left: 30px;">編集 &gt;&gt; ファイル化 &gt;&gt; ファイル単位のデータ管理 の手順を踏んでいくので、バージョン管理は容易で、1 時間、2 時間といった短時間のうちにファイルが幾度も更新されてバージョンが頻繁に変更されることはないはずです。このことから、「 （更新）頻度が低いデータ」から転じて「 Low Frequency Data」と呼ぶことにします。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb094af6e3970d-pi" style="display: inline;"><img alt="Low_frequency" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb094af6e3970d image-full img-responsive" src="/assets/image_873438.jpg" title="Low_frequency" /></a></p>
<p style="padding-left: 30px;">「ファイル」を使う場合、利用する設計ツールによって異なるファイル形式のファイルが作成されてしまうので、中間ファイル形式を除いて、相互に互換性がないことが問題視されています。</p>
<hr />
<p>&#0160;<strong>クラウド時代のデザイン データの可能性</strong></p>
<p style="padding-left: 30px;">ファイルをクラウド上のストレージに保存する、といった考え方では、クラウドを有効活用しているとは言えなくなりつつあります。</p>
<p style="padding-left: 30px;"><strong><a href="https://ja.wikipedia.org/wiki/%E3%82%B9%E3%83%88%E3%83%AA%E3%83%BC%E3%83%9F%E3%83%B3%E3%82%B0" target="_blank">ストリーミング</a>&#0160;</strong>や <a href="https://ja.wikipedia.org/wiki/WebSocket" target="_blank"><strong>WebSocket</strong> </a>などを使った双方向通信は、もはや珍しくはありません。これらを利用することで、クラウド上のデザイン データにリアルタイムに読み書きすることが出来るようになります。この方法を使った場合、何が出来るでしょう。</p>
<p style="padding-left: 30px;">クライアントからのデザインの編集操作をリアルタイムにデータに反映することが出来るだけではなく、そのデータを参照している別の目的を持ったクライアントにも、タイムラグなく最新の情報を伝えることが出来るはです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb094af811970d-pi" style="display: inline;"><img alt="High_frequency_data" class="asset  asset-image at-xid-6a0167607c2431970b01bb094af811970d img-responsive" src="/assets/image_226865.jpg" title="High_frequency_data" /></a></p>
<p style="padding-left: 30px;">ある意味、ファイル時代にクライアント コンピュータのメモリを使っておこなっていた編集操作を、クラウドを相手に実行出来ます。このことから、「 （更新）頻度が高いデータ」から転じて「 High Frequency Data」と呼ぶことにします。バージョン管理の視点では、編集過程の各スナップショットが、ファイル単位でのバージョンに相当するようになるはずです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d231ece1970c-pi" style="display: inline;"><img alt="High_fequency" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d231ece1970c image-full img-responsive" src="/assets/image_921620.jpg" title="High_fequency" /></a></p>
<p style="padding-left: 30px;">また、もし、クライアントが持つ目的によって、クラウド上にあるデザイン データの「見せ方」を変えていくことが出来れば、もはや、「ファイル」という単位に固執する必要はないのかも知れません。</p>
<hr />
<p>現在、Forge が提供しているのは、「ファイル」を主眼に置いた Low Frequency なデザイン データの活用方法と考えることが出来ます、この方法は、従来の設計手法を踏襲する意味で、将来も決してなくなってしまうものではありません。ただ、まったく別の潮流として、クラウドをフルに活用する High Frequency なデザイン データの扱いと活用方法も検討され始めています。</p>
<p>今後の Autodesk Forge に引き続きご注目ください。</p>
<p>By Toshiaki Isezaki</p>
