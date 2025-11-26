---
layout: "post"
title: "ネットワーク ライセンスのサーバー構成について"
date: "2013-08-14 01:13:22"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/08/server-models-for-network-license.html "
typepad_basename: "server-models-for-network-license"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2013/07/license-types-for-desktop-products.html" target="_blank">前回</a>、 デスクトップ製品のライセンス タイプについて、基本的な考え方をご紹介しました。実はネットワーク ライセンスを選択いただいた場合には、複数あるライセンス サーバーの構成も指定する必要があります。この部分は、まだ詳しく紹介していませんので、ここでは、ネットワーク ライセンス利用時のライセンス タイプについて説明したいと思います。</p>
<p>ネットワーク ライセンスのライセンス サーバー構成&#0160;には、<strong>シングル サーバー構成</strong>、<strong>分散サーバー構成</strong>、<strong>冗長サーバー構成&#0160;</strong>の3タイプです。</p>
<p>&#0160;</p>
<p style="padding-left: 30px;"><strong>シングルサーバー構成</strong></p>
<p style="padding-left: 30px;">推奨設定です。最もシンプルなライセンスサーバ環境設定で、ほとんどの企業がこの設定から導入し始めます。この設定では、ライセンスサーバに必要なハードウェアとソフトウェアの要件はかなり低めです。メモリと CPU の使用量は小さく（サーバを使用するクライアントの数によって異なります）、必要なディスク容量は FLEXnet ユーティリティとログファイル（かなり大きくなる場合があります）が要求するサイズのみです。</p>
<p style="padding-left: 30px;">主な要件は、AutoCAD ライセンスへのアクセスが必要なすべてのクライアント コンピュータが、待ち時間を少なく（低遅延）、高い有効性を備えたライセンスサーバにアクセスできる必要があることです。サーバとクライアント間の通信が不通になった場合、一定の時間が経つとAutoCAD ライセンスはタイムアウトし、AutoCAD が動作しなくなります。ライセンス ソフトウェアは、短時間のダウンタイム（通常 15 分未満）には対応するようになっていますが、通信が時間内に回復しない場合は、ライセンス サーバとの接続が失われたことを AutoCAD がユーザに警告します。通信の復旧を複数回試みた後、作業の保存をユーザに指示してから、AutoCAD は終了します。ライセンス サーバとの通信が復旧し、ライセンスを再取得できるようになるまで、AutoCAD は起動できません。</p>
<p style="padding-left: 30px;">ユーザ グループ間でライセンスを共有するのが望ましくない場合は、複数台のシングルライセンス サーバを、グループごとにセットアップできます。この方式の主な短所は、サーバ間でライセンスを共有できない点です。未使用のライセンスがサーバ A にあっても、サーバ B のユーザはそのライセンスにアクセスできません（ユーザグループが別予算で運用されている場合など、それで問題のない場合もあります）。長所は 1 台のサーバに障害が発生しても、他のサーバのライセンスには影響しないことです。サーバ間でライセンスを共有する場合に最適な環境設定は、次に説明する分散型ライセンスサーバ環境設定です。&#0160;</p>
<p style="text-align: center;"><iframe frameborder="0" height="344" src="http://www.youtube.com/embed/J9dUaP4MYdU?feature=oembed" width="459"></iframe>&#0160;</p>
<p style="text-align: center;">&#0160;</p>
<p style="padding-left: 30px;"><strong>分散サーバー構成</strong></p>
<p style="padding-left: 30px;">この環境設定では、ライセンスを複数台のサーバに分散することができます。クライアント コンピュータがライセンスの取得を試みる場合、AutoCAD ライセンスを取得できるまで、すべてのサーバを対象にライセンス取得を試行することが可能です。各ユーザのクライアント コンピュータには、使用可能なライセンスサーバのすべて（または管理者の設定によっては一部）へのパスが設定されています。これは、インストール時に配置ウィザードを使用して定義された内容です。リストにある最初のサーバでライセンスを拒否されると、自動的にリストの 2 番目のサーバ、3 番目のサーバへと、順番に問い合わせます。</p>
<p style="padding-left: 30px;">例えば、設計オフィスが東京本社と大阪支社にそれぞれあると仮定します。各オフィスにあるシングルライセンスサーバが高速 LAN でライセンスを配布しています。本社と支社の間は高速通信で接続されていますが、オフィス間の接続（WAN）は一般的に各オフィス内の LAN よりも信頼性が低い状態です。</p>
<p style="padding-left: 30px;">会社のソフトウェア ライセンスを最大限に活用するために、すべてのライセンスを本社の 1 つのライセンスサーバにまとめることを検討していますが、通信環境の障害で接続できなくなると、支社でAutoCAD を使えなくなることが懸念されています。そこで、解決策として、複数のライセンスサーバを分散されたライセンス サーバとして環境設定することを検討します。オフィス間の通信が正常なときは、各オフィスはライセンス サーバ全体にアクセスします。オフィス接続の 1 つで障害が発生した場合、その支社は最低でもローカル サーバのライセンスにはアクセスできます。東京本社のサーバが故障した場合にも、大阪支社のライセンスを使用することが可能になります（そのように設定した場合）。</p>
<p style="text-align: center;"><iframe frameborder="0" height="344" src="http://www.youtube.com/embed/Z3yfXLlWUg8?feature=oembed" width="459"></iframe>&#0160;</p>
<p style="text-align: center;">&#0160;</p>
<p style="padding-left: 30px;"><strong>冗長サーバー構成</strong></p>
<p style="padding-left: 30px;">このサーバ環境設定はきわめて高い有効性が要求される場合に適していますが、管理が最も煩雑です。このため、オートデスクでは、可能な限り分散型システムの選択をおすすめしています。また、後述するように、現在、ライセンス サーバーを仮想化することも出来るので、冗長サーバー構成と同じような&#0160;<strong><a href="http://ja.wikipedia.org/wiki/%E3%83%95%E3%82%A9%E3%83%BC%E3%83%AB%E3%83%88%E3%83%88%E3%83%AC%E3%83%A9%E3%83%B3%E3%83%88%E3%82%B7%E3%82%B9%E3%83%86%E3%83%A0" target="_blank">フォルトトレラント</a></strong>&#0160;な環境を構築することも可能です。</p>
<p style="padding-left: 30px;">冗長型環境設定では、相互に常時通信状態にある 3 台のサーバ（3 台でなければなりません）を使って、1 つのライセンスプールを共有します。そのため、1 台のサーバが障害を起こしたり、保守のためにシャットダウンされたりする場合は、残りのサーバがライセンスプール全体をバックアップし、ライセンスの提供には悪影響が及びません。</p>
<p style="padding-left: 30px;">ただし、3 台のサーバすべてが同じサブネット上に存在し、信頼できるネットワーク通信を行う（WAN での連携が可能な分散型サーバとの相違点）必要があります。分散型サーバと異なり、この環境設定ではネットワーク障害時の対策はなく、他にも管理を難しくする要素が多数あります。</p>
<p style="text-align: center;"><iframe frameborder="0" height="344" src="http://www.youtube.com/embed/s9pH55GPcEY?feature=oembed" width="459"></iframe>&#0160;</p>
<p>&#0160;</p>
<p><strong>ネットワーク ライセンスで出来ること</strong>&#0160;</p>
<p>ネットワーク ライセンスでは、使用するサーバー構成に関係なく、さまざまなオプション設定を追加することもできます。この動作を司っているのが、ライセンス マネージャが参照することになる<strong>オプション ファイル</strong>です。次に、オプション ファイルの指定で実現できる機能をご紹介します。</p>
<p style="padding-left: 30px;"><strong>ライセンス借用</strong></p>
<p style="padding-left: 30px;">AutoCAD の機能で、ユーザが一定の期間ライセンスサーバからネットワークライセンスを「チェックアウト」し、使用が済んだら、そのライセンスをサーバプールに「チェックイン」することができます。借用されたライセンスは、ユーザのコンピュータに限定され、借用期間中はライセンスサーバとの通信は必要ありません。</p>
<p style="padding-left: 30px;">AutoCAD の実行セッション中に[ツール]メニューから[ライセンス借用]を選択すると、ライセンスを借用できます。その際に表示されるダイアログに利用可能な最長借用期間が表示されるので、ユーザはライセンスを借用したい期間を最長期間以内で指定できます。管理者はオプションファイルにより、借用可能なライセンスの数や、借用を許可するユーザを制御できます。メニュー選択で、ユーザが期限より早くライセンスを返却することも可能です（ユーザの出張が予定よりも早く終わった場合など）。AutoCAD のステータストレイにある「借用」アイコンは、借用ライセンスで作業していることをユーザに通知し、残りの借用期間を表示します。&#0160;</p>
<p style="padding-left: 30px;">例えば、サーバに 10 ライセンスあり、そのうち 5 ライセンスを借用可能にすることを検討しています。また、最長借用期間を 2 週間以内に制限し、借用機能を特定のユーザグループに限定することも検討しています。このような場合、特定のオプションファイルとシステム変数を使って、これらの設定を制御できます。</p>
<p style="text-align: center;"><iframe frameborder="0" height="344" src="http://www.youtube.com/embed/n34AccDvlj0?feature=oembed" width="459"></iframe>&#0160;</p>
<p style="padding-left: 30px;"><strong>タイムアウト</strong></p>
<p style="padding-left: 30px;">この機能を利用すると、管理者は、AutoCAD セッションをアイドル状態にしておける最大時間を設定できます。ユーザがライセンスをライセンスサーバからチェックアウトして AutoCAD を起動しているのに、実際には AutoCAD を使用していない（ソフトウェアが遊んでいる）場合などに、最大アイドル時間を設定し、ライセンスをプールに自動返却するタイミングを制御します。制限時間になると、サーバがライセンスを回収するため、ワークステーションはライセンスを失ってセッションが終了します。管理者はオプションファイルに、AutoCAD セッションの最大許容アイドル時間を設定することができます。</p>
<p style="padding-left: 30px;">例えば、ネットワーク ライセンスを有効に利用していないユーザが 2 人いるとします。2 人とも作業をすすめるのにAutoCAD が必要ですが、一方のユーザは AutoCAD を終了するのを忘れて、5 時間の会議に出かけてしまいました。もう一方のユーザは「もしかしたら使用するかもしれない」ので、AutoCAD を開いたままにしています。どちらのケースも、ライセンスを拘束してしまっているため、結果として他のユーザがライセンスを使用することを妨害しています。このようなユーザには、アイドル時間の上限を 2 時間に設定しておけば、彼らが 2 時間以上 AutoCAD を使用しない場合に、他のユーザが使用するためにサーバが自動的にライセンスを回収します。彼らが再度 AutoCAD を使用しようとする場合、使用可能なライセンスがある限り AutoCAD セッションは中断されずに続行します。使用できるライセンスがない場合、AutoCAD はライセンスサーバとの接続が失われたケースと同じように動作し、作業中の図面を保存することができます。</p>
<p>なお、特定の製品はバージョンによっては、タイムアウトをサポートしないものもありますので、注意してください。オプション ファイルの詳細については、次の Autodesk Knowledge Network が参考になると思います。</p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000u03A.html" target="_blank">AutoCAD 2014のネットワークライセンスに関する資料はありますか？</a></strong></p>
<p style="padding-left: 30px;"><strong>&#0160;<a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tzJu.html" target="_blank">(TS67304) ライセンス タイムアウト について</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tzpE.html" target="_blank">(TS83137) タイムアウト機能を設定する</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/inventor-products/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tzwx.html" target="_blank">Inventor 2009 でライセンスのタイムアウトが有効ですか？</a></strong></p>
<p style="padding-left: 30px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad/troubleshooting/caas/sfdcarticles/sfdcarticles/kA230000000tznt.html" target="_blank">ネットワーク ライセンスのオプション ファイルにて GROUP や HOST_GROUP により指定したユーザ名やホスト名の大文字、小文字の区別をしたくない</a></strong></p>
<p>&#0160;</p>
<p><strong>ライセンス マネージャの仮想化</strong>&#0160;</p>
<p>ライセンスマネージャをインストールするOSを仮想化することが出来ます。この方法でシングルサーバー構成のライセンス マネージャが動作する環境を仮想化すれば、冗長サーバー構成の必要はないのかも知れません。</p>
<p>AutoCAD 2014 サポートする仮想化ツールは、<a href="http://docs.autodesk.com/INSTALL_LICENSE/2014/JPN/Autodesk%20Installation%20Help/files/GUID-15D26625-0C9D-41FC-A293-D14935294EA5.htm" target="_blank">こちら</a>に記載されている&#0160;VMWare ESX 4.0 と 5.0 です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019104a9d0db970c-pi" style="display: inline;"><img alt="LicenseManagerVirtualizaion" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019104a9d0db970c image-full" src="/assets/image_537093.jpg" title="LicenseManagerVirtualizaion" /></a><br /><br /></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
