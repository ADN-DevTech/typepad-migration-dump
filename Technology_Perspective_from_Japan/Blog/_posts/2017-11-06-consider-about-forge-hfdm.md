---
layout: "post"
title: "Forge HFDM の考え方"
date: "2017-11-06 00:05:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/11/consider-about-forge-hfdm.html "
typepad_basename: "consider-about-forge-hfdm"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2b82e85970c-pi" style="display: inline;"><img alt="Hfdm" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2b82e85970c image-full img-responsive" src="/assets/image_463668.jpg" title="Hfdm" /></a></p>
<p>過去のブログ記事&#0160;<strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/10/future-of-forge.html" rel="noopener noreferrer" target="_blank">Forge の未来</a></strong> でも触れているファイルを超えた新しいデータ管理の方法に、クラウドを利用した&#0160;High Frequency Data の利用があります。オートデスクは、この&#0160;High Frequency Data を利用する仕組みを&#0160;<strong>High Frequency Data Management（HFDM）</strong> と呼んでいて、ファイルに替わるオートデスクの次世代データプラットフォームに位置付けています。</p>
<p>HFDM は、Web ブラウザで動作する Fusion 360、<strong>Fusion 360 Web</strong>、Web ブラウザで動作する次世代の BIM コラボレーション CAD、<strong>Project Quantum</strong>、<strong><a href="https://www.tinkercad.com" rel="noopener noreferrer" target="_blank">TinkerCAD</a></strong> などのクラウドサービスで既に採用、または、採用されることが決まっています。</p>
<p>今回は、この HFDM を理解する上で基本となる考え方を背景とともにご案内していきたいと思います。</p>
<hr />
<p><strong>HFDM の必要性</strong></p>
<p style="padding-left: 30px;">クラウドの登場から大分時間が経過していますが、日本でもデザインや設計の分野でクラウドが活用されつつあります。もはや、なぜ、クラウドが必要なのか？、セキュリティに問題があるのでは？という疑問についての解説は避けますがが、クラウドに関する議論には枚挙に暇がありません。改めて、クラウドの利点や利用すべき理由をお知りになりたい方は、&quot;クラウド 利点&quot; をキーワードに <strong><a href="http://lmgtfy.com/?q=&quot;クラウド%20利点&quot;" rel="noopener noreferrer" target="_blank">Web 検索</a></strong> をしてみてください。</p>
<p style="padding-left: 30px;">さて、HFDM の解説の前に、少し違った視点でクラウドの利用を考えてみたいと思います。昨今、あたりまえのように語られているテクノロジに<a href="http://adndevblog.typepad.com/technology_perspective/2015/08/about-internet-of-things.html" rel="noopener noreferrer" target="_blank"><strong> IoT</strong></a> があります。IoT センサーからは、日々、検出・測定されたデータが生み出されていきます。もし、それらのデータを自社内に設置したサーバーに保存、蓄積しようとすれば、ストレージをどんどん追加していかなければなりません。また、蓄積された膨大なデータ、いわゆる、<strong><a href="https://ja.wikipedia.org/wiki/%E3%83%93%E3%83%83%E3%82%B0%E3%83%87%E3%83%BC%E3%82%BF" rel="noopener noreferrer" target="_blank">ビックデータ</a></strong>&#0160;を解析して一定の傾向や、何らかの特異点を見出そうとするのには、膨大な時間を費やす必要があるのは想像に難くありません。もちろん、データ解析をおこなうソフトウェアを開発したり、購入したりする必要もあるかもしれません。仮に、それらを自社内でまかなえたとしても、データ解析に CPU パワーを使い切ってしまい、他の作業の支障が出るようでは本末転倒です。</p>
<p style="padding-left: 30px;">そこで、クラウドの登場です。クラウド ストレージは比較的安価で、自社内に物理的なデータ サーバーを用意するよりも簡単にストレージ量を増やすことが可能です。また、解析に必要となる CPU パワーも、必要に応じて伸長的に追加できるのは周知のとおりです。</p>
<p style="padding-left: 30px;">ファイルをバックアップするストレージとして利用され出したクラウドですが、現在では、クラウドを複合的にどう使っていくかが問われる時代になっています。IoT に話を戻すなら、IoT がもてはやされだしたのは、IoT センサーが生み出すデータの受け皿として、コスト面で優れたクラウドが選択され出したため、と捉えることが出来るはずです。つまり、クラウドの利用が進んだうえの必然的な結果なのです。</p>
<p style="padding-left: 30px;">さて、設計やデザインの領域ではどうでしょう。一般的には、デザイン データ（ファイル）をクラウド ストレージへバックアップしたり、関係者間で共有する場としてクラウドが利用されているのではないでしょうか。事実、CAD ソフトウェア内ですべての情報を付加してしまう設計手法、例えば、<strong>デジタル プロトタイプ&#0160;</strong>や <strong>BIM・CIM</strong>&#0160;では、デザイン データ ファイル内に様々な情報を包含するようになり、ファイルサイズが益々肥大化する傾向にあります。メールに添付するには大きすぎるはずです。</p>
<p style="padding-left: 30px;">他にも問題があります。設計から製造、施工までには、多くの関係者/関係会社が携わるという点です。設計段階につきまとう設計変更やデザインフィードバックの度に、すべてのデザイン情報が含まれる大きなサイズのファイルを関係者の間でやり取りするのは、たとえ、クラウド ストレージを利用したとしても面倒です。同時に、沢山生じることになるデザイン ファイルから、個別の変更点を共有する方法も煩雑になってしまいます。やりとりされるデザイン データは、あくまでファイル単位で、クライアントでファイルを開き、修正、ファイル保存後に再度クラウドストレージへアップロードして共有といった手順です。</p>
<p style="padding-left: 30px;">なお、オートデスクは、このようなファイル単位のデータを、更新頻度が低い <strong>Low Frequency Data</strong> と呼んでいます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2ba48cd970c-pi" style="display: inline;"><img alt="Low_frequency" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2ba48cd970c image-full img-responsive" src="/assets/image_806414.jpg" title="Low_frequency" /></a></p>
<p style="padding-left: 30px;">ファイル単位のデザインが無事、共有出来たとしても、設計変更の履歴や変更にともなう他社担当デザインへの関連性を見出すのは困難でしょう。ちょうど、IoT 例でのビックデータ解析に近い状態です。</p>
<p style="padding-left: 30px;">そこで、HFDM の登場です。HFDM では、CAD ソフトウェア毎に異なるファイル単位でのデータ処理に替わって、クラウドにあらゆるデータを蓄積し、履歴や関連性を自動的に記録しながら、多くの関係者間で個別、あるいは、リアルタイムに変更履歴の確認やデザイン評価などを効率的におこなう仕組みを提供します。クライアント側のアプリケーションやサービスは、一元的にクラウドで管理されたマスター データへ、設計変更が生じた部分のみを送信して更新したり、自社に必要な部分のデータのみを抽出、利用することが出来るようになります。これら変更は、リアルタイムにクラウド上のデザイン データに反映されていきます。</p>
<p style="padding-left: 30px;">オートデスクは、このようなデータを、更新頻度が高い <strong>High Frequency Data</strong> と呼んでいるわけです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09d30b61970d-pi" style="display: inline;"><img alt="High_fequency" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09d30b61970d image-full img-responsive" src="/assets/image_884880.jpg" title="High_fequency" /></a></p>
<p style="padding-left: 30px;">それでは、HFDM の考え方を詳細に見ていきましょう。</p>
<p><strong>差分データだけの送信</strong></p>
<p style="padding-left: 30px;">HFDM では、クラウド上に一元管理されたデザイン マスター データがあり、あらゆる情報が蓄積されています。あるクライアントから CAD アプリケーション、または、クラウド サービスを使って新規のデザイン プロジェクトを立ち上げた状態を考えてみましょう。設計者が見ている作図領域（下図左手）は何も作図されていません。当然、クラウドへ保存されるべきデータもありません（下図右手）。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2b9f520970c-pi" style="display: inline;"><img alt="Hfdm1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2b9f520970c image-full img-responsive" src="/assets/image_24452.jpg" title="Hfdm1" /></a></p>
<p style="padding-left: 30px;">次に、設計者が作図領域に半径 2、中心座標 (2, 2) で青緑色の円を追加したとします。この時、円の追加は、即座にクラウドに反映されていきます。ご注意いただきたいのは、クラウドに送信される情報は、追加された円についての情報のみという点です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09d2b0c1970d-pi" style="display: inline;"><img alt="Hfdm2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09d2b0c1970d image-full img-responsive" src="/assets/image_336685.jpg" title="Hfdm2" /></a></p>
<p style="padding-left: 30px;">続いて、中心座標 (4, 5) の位置に、幅 2、高さ 3 の青い矩形を追加します。この矩形の追加も即座にクラウドへ反映されますが、送信されるデータは矩形に関するものだけで、作図領域にある円の情報は送信されません。すでにお気づきのように、ファイル単位でデザイン データを扱う場合には、一旦すべての情報をファイルに保存し、クラウドにアップロードしてデータを更新しなければなりません。この方法は、ネットワーク負荷の観点からも無駄が生じていることがわかります。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c92f86c4970b-pi" style="display: inline;"><img alt="Hfdm3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c92f86c4970b image-full img-responsive" src="/assets/image_652558.jpg" title="Hfdm3" /></a></p>
<p style="padding-left: 30px;">作図済みの矩形の色を赤に変更した場合も同様です。データ更新でクラウドに送信されるのは、矩形の色に関する部分のみです（矩形を識別する名前や識別子を除く）。矩形の幅や高さ、位置は送信されません。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2b9f52f970c-pi" style="display: inline;"><img alt="Hfdm4" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2b9f52f970c image-full img-responsive" src="/assets/image_786456.jpg" title="Hfdm4" /></a></p>
<p style="padding-left: 30px;">矩形の幅を変更した場合には、その情報のみが更新データとしてクラウドに送信されることになります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c92f86d0970b-pi" style="display: inline;"><img alt="Hfdm5" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c92f86d0970b image-full img-responsive" src="/assets/image_752154.jpg" title="Hfdm5" /></a></p>
<p style="padding-left: 30px;">&#0160;矩形の位置を変更した場合も同様です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09d2b0d9970d-pi" style="display: inline;"><img alt="Hfdm6" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09d2b0d9970d image-full img-responsive" src="/assets/image_332052.jpg" title="Hfdm6" /></a></p>
<p style="padding-left: 30px;">このように HFDM では、クラウドにデータの集合体としてでデザインがあり、差分データの送信のみでデザイン全体が更新されていきます。</p>
<p><strong>自動的な履歴管理</strong></p>
<p style="padding-left: 30px;">デザイン データがクラウドで一元管理され、クライアントからのデザイン変更の度に更新データだけが送信されてくるなら、変更が発生した時間、変更の内容、更新データを送信してきたクライアントの特定等の記録は容易です。HFDM では、このような履歴管理機能が最初から備わっているので、過去に遡ってデザイン データの状態を把握することが可能です。</p>
<p style="padding-left: 30px;">前述の例では、図形の追加や編集内容がすべて記録されていることになります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2b9f54e970c-pi" style="display: inline;"><img alt="Hfdm_history1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2b9f54e970c image-full img-responsive" src="/assets/image_652658.jpg" title="Hfdm_history1" /></a></p>
<p style="padding-left: 30px;">&#0160;もちろん、特定のデザイン データの状態を簡単に復元することも出来るわけです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c92f86e1970b-pi" style="display: inline;"><img alt="Hfdm_history2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c92f86e1970b image-full img-responsive" src="/assets/image_696302.jpg" title="Hfdm_history2" /></a></p>
<p><strong>ブランチ（Branch）とマージ（Merge）ー 非同期コラボレーション</strong></p>
<p style="padding-left: 30px;">HFDM では、デザインに関わる各関係者が、個別にデザイン評価や検討をおこなえる仕組みを提供します。この仕組みには、<strong>ブランチ（分岐）</strong>と<strong>マージ（結合）</strong>という概念を利用します。</p>
<p style="padding-left: 30px;"><strong>ブランチ</strong></p>
<p style="padding-left: 30px;">簡単に説明するなら、独自にデザイン評価/検討をおこなう際にブランチを作成し、クラウドで一元管理されたデザインのマスター データに影響を与えずに、つまり、設計変更等の差分データをマスター データに反映せずに、設計変更をおこなうことができる仕組みがブランチです。</p>
<p style="padding-left: 30px;">次の例では、青い矩形を作図してクラウドのマスター データに反映した後にブランチを作成すると、この設計者独自に三角形を追加、色を変更する設計評価をした状態となります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c92f86f7970b-pi" style="display: inline;"><img alt="Branch1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c92f86f7970b image-full img-responsive" src="/assets/image_383396.jpg" title="Branch1" /></a></p>
<p style="padding-left: 30px;">各ブランチでおこなった設計変更の検討が関係者間で合意された場合には、ブランチの内容をグローバル ブランチとも呼ばれるマスター データにマージすることが可能になります。マージが完了すると、関係者全員が参照出来るマスター データに、ブランチの内容が反映されることになります。&#0160;</p>
<p style="padding-left: 30px;">ブランチによる独自の設計変更は、複数の関係者が同時進行でおこなえる点に注意してください。設計のプロセスでは、他社の設計変更を待たなければならい時があり、いたずらに時間を消費してしまうことがあります。もちろん、必要なプロセスと考えられますが、事前に打ち合わせに応じた考えられるデザイン パターンを用意できれば、全体の時間短縮に繋げることが出来るかもしれません。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c92f870e970b-pi" style="display: inline;"><img alt="Branch2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c92f870e970b image-full img-responsive" src="/assets/image_778095.jpg" title="Branch2" /></a></p>
<p style="padding-left: 30px;">あるブランチのデザインをマスター データにマージした場合でも、マスターへのマージ前後の履歴は自動的記録されているので、いつでもマージ前の状態に戻ることが出来ます。もし、複数ブランチからのデザイン マージで競合・矛盾が発生した場合には、ユーザが介在してそれらを解決出来るだけでなく、事前に決めたルールや標準的なルールを適用して競合を解決することも可能です</p>
<p style="padding-left: 30px;">余談ではありますが、でに Forge サンプルの利用も含め、プログラム開発で <strong><a href="https://ja.wikipedia.org/wiki/GitHub" rel="noopener noreferrer" target="_blank">GitHub</a></strong> をお使いの方は、<strong><a href="https://ja.wikipedia.org/wiki/Git" rel="noopener noreferrer" target="_blank">Git</a></strong>&#0160;で利用されているブランチとマージとほぼ同じ考え方であることにお気づきかと思います。また、Fusion 360 でもブランチとマージの考え方が<strong><a href="https://knowledge.autodesk.com/ja/support/fusion-360/learn-explore/caas/CloudHelp/cloudhelp/JPN/Fusion-Collaborate/files/GUID-67792291-4519-4D87-BAC1-0135B863CD4E-htm.html" rel="noopener noreferrer" target="_blank">導入</a></strong>されているのはご存じのとおりです。</p>
<p style="padding-left: 30px;">このように、ブランチとマージは設計者に関係者との非同期コラボレーションの場を提供します。</p>
<p><strong>リアルタイム コラボレーション ー 同期コラボレーション</strong></p>
<p style="padding-left: 30px;">ブランチとマージで実現可能な非同期コラボレーションですが、場面によっては、関係者間で同時にデザイン評価・変更を検討したいときもあります。よく、リアルタイム コラボレーションと呼ぶこの方法は、関係者全員が参照可能なグローバル ブランチ（マスター データ）を使って、各クライアントからの変更をリアルタイムにマージしていくことで実現可能です。</p>
<p style="padding-left: 30px;">このリアルタイム コラボレーションは、ちょうど、過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/11/high-frequency-data-on-tinkercad-beta.html" rel="noopener noreferrer" target="_blank">TinkerCAD Beta で見る High Frequency Data</a></strong> でご紹介した動画でも把握していただけるはずです。なお、現在 TinkerCAD は<a href="http://adndevblog.typepad.com/technology_perspective/2017/01/release-japanese-version-of-tonkercad.html" rel="noopener noreferrer" target="_blank">日本語化</a>されて Beta ではなくなっています。&#0160;</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" gesture="media" height="281" src="https://www.youtube.com/embed/BO5QlZxUEi8?feature=oembed" width="500"></iframe>&#0160;</p>
<p class="asset-video"><strong>アクセス管理</strong></p>
<p class="asset-video" style="padding-left: 30px;">すべての設計者、関係者が一律にマスター データにアクセスしては混乱が生じてしまいます。HFDM でも、アカウント管理によってアクセス可能なデータや書き込み／読み込み等のアクセス権限を設定することが出来ます。</p>
<div id="AppleMailSignature"><hr />ここまでの説明を理解いただくと、HFDM データは永続的にアクセスすることが保証されているのか？、既存のデザイン ファイルをインポートして HFDM データ化出来るのか？、また、HFDM データを特定のデザイン ファイル形式でエクスポート出来るのか？など、多くの疑問を持たれるかもしれません。</div>
<div>&#0160;</div>
<div>ただ、現時点では、オートデスクは&#0160; HFDM を、今後、開発者に公開しようとしている段階です。残念ながら、まだ、それらの疑問に答える時期ではありません。詳細が決まってきた段階で、別途、疑問にお答えすることにしたいと思います。少なくとも、<strong>HFDM がクラウドを真に利用するデザイン データ管理、運用の方法であること、オートデスクは HFDM を次世代のアプリケーションやクラウド サービスのデータ プラットフォームとして位置づけている</strong>ことを、その考え方とともにご理解いただけたらと思います。</div>
<p>By Toshiaki Isezaki</p>
