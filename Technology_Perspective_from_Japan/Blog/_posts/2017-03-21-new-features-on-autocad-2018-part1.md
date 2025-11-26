---
layout: "post"
title: "AutoCAD 2018 の新機能 ～ その1"
date: "2017-03-21 00:49:31"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/03/new-features-on-autocad-2018-part1.html "
typepad_basename: "new-features-on-autocad-2018-part1"
typepad_status: "Publish"
---

<p>AutoCAD の誕生から、ちょうど、35 年の節目を迎えた今年も、新バージョンとなる AutoCAD 2018 が発表されました。もちろん、昨年から始まった Update（更新モジュール）で導入された機能も継承しています。このため、ここでは <strong><a href="http://adndevblog.typepad.com/technology_perspective/2016/10/autocad-2017-update-1.html" rel="noopener noreferrer" target="_blank">AutoCAD 2017.1 Update</a></strong> で導入された機能や強化された機能も、AutoCAD 2018 の新機能としてご紹介していきます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0980cabf970d-pi" style="display: inline;"><img alt="35th_anniversary" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0980cabf970d image-full img-responsive" src="/assets/image_863926.jpg" title="35th_anniversary" /></a></p>
<p><strong>サポート OS についての注意</strong></p>
<p style="padding-left: 30px;">AutoCAD 2018 / AutoCAD LT 2018 がサポートする Windows OS には少し注意が必要です。前バージョンの AutoCAD 2017 と同様の Windows をサポートすることになりますが、Windows 10 のみ 64 ビット版のみのサポートになる点です。Windows 10 32 ビット版には、AutoCAD 2018、AutoCAD LT 2018 をインストールすることは出来ません。</p>
<ul>
<li><strong>Windows 7 SP1</strong>
<ul>
<li>下記エディションの <strong>32 ビット、および 、64 ビット版</strong></li>
<li>Enterprise、Ultimate、Professional、Home Premium</li>
</ul>
</li>
<li><strong>Windows 8.1 </strong>＋<a href="https://www.microsoft.com/ja-jp/download/details.aspx?id=42327">KB2919355</a> <a href="https://www.microsoft.com/ja-jp/download/details.aspx?id=42327">Update</a>
<ul>
<li>下記エディションの<strong> 32 ビット、および 、64 ビット版</strong></li>
<li>（標準）、Pro、Enterprise</li>
</ul>
</li>
<li><strong>Windows 10</strong>
<ul>
<li>下記エディションの <strong>64 ビット版</strong></li>
<li>Home、Pro、Enterprise、Education</li>
</ul>
</li>
</ul>
<p style="padding-left: 30px;">新規に購入するコンピュータには、最新の 64 ビット CPU と 64 ビット版 Windows 10 が搭載されているので、32 ビット版 Windows 10 の未サポートは特に問題にはならないはずです。古いコンピュータをお使いで、もともと搭載された &#0160;Windows OS をアップグレードして Windows 10 にしている場合には、 32 ビット版 &#0160;Windows 10 になっている可能性が高いので、再確認していただくことをお勧めします。コンピュータに搭載されている CPUと Windows の関係については、過去のブログ記事 <strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/07/cpu-and-graphics-card.html" rel="noopener noreferrer" target="_blank">CPU とグラフィックスカード</a></strong>&#0160;から <strong>プロセッサ（CPU）＋ Windows プラットフォーム</strong>&#0160;の部分をご確認ください。</p>
<p>それでは、<strong>ユーザ インタフェース</strong> についてご案内していきましょう。ここでご紹介する内容は、AutoCAD &#0160;2018 だけでなく、AutoCAD LT 2018 にも適用することが出来ます。</p>
<p><strong>ファイル ダイアログ</strong>&#0160;</p>
<p style="padding-left: 30px;">図面を開いたり保存したり、あるいは、外部ファイルをアタッチする場合には、必ず ファイルを選択するためいわゆるファイル ダイアログが表示されます。この際、フォルダ一覧に Windows エクスプローラと同様にカテゴリが表示されます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8dd3d47970b-pi" style="display: inline;"><img alt="File_dialog" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8dd3d47970b image-full img-responsive" src="/assets/image_478743.jpg" title="File_dialog" /></a></p>
<p style="padding-left: 30px;">このカテゴリ欄は、マウスで右クリックすると表示順序（ソート順序）を変えることが出来ます。以前のリリースでは、変更したソート順序を記憶出来ませんでした。AutoCAD 2018 は、指定したソート順序を記憶出来るようになったため、ダイアログを開く度に順序を変える必要がなくなりました。</p>
<p><strong>[作図補助設定] ダイアログ</strong></p>
<p style="padding-left: 30px;">作図時のさまざまな設定を集約した [作図補助設定] ダイアログがサイズ変更出来るようになりました。このダイアログには沢山のタブがあり、その切り替えが面倒でした。AutoCAD 2018 では、ダイアログのサイズを変更して使用することで、各タブへのアクセスが容易になります。 &#0160;スピン コントロールで画面外のタブを呼び出す必要はありません。<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d264dc12970c-pi" style="display: inline;"><img alt="2018_作図設定ダイアログ_small" class="asset  asset-image at-xid-6a0167607c2431970b01b8d264dc12970c img-responsive" src="/assets/image_17374.jpg" style="width: 700px;" title="2018_作図設定ダイアログ_small" /></a></p>
<p style="padding-left: 30px;">もちろん、変更したダイアログ サイズは記憶されるので、毎回、サイズ変更する必要はありません。</p>
<p><strong>[色選択] ダイアログ</strong></p>
<p style="padding-left: 30px;">以前のリリースでは、RGB 値をカンマ区切りで入力した際、リターンキーで入力を確定しようとすると、入力時に値が変動してしまい、正しく色を設定することが出来ませんでした。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0980ccba970d-pi" style="display: inline;"><img alt="2017_色設定ダイアログ" class="asset  asset-image at-xid-6a0167607c2431970b01bb0980ccba970d img-responsive" src="/assets/image_509339.jpg" style="width: 450px;" title="2017_色設定ダイアログ" /></a></p>
<p style="padding-left: 30px;">AutoCAD 2018 では、この挙動を改善して、入力した RGB 値で色を設定することが可能になりました。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0980ccc7970d-pi" style="display: inline;"><img alt="2018_色設定ダイアログ" class="asset  asset-image at-xid-6a0167607c2431970b01bb0980ccc7970d img-responsive" src="/assets/image_484450.jpg" style="width: 450px;" title="2018_色設定ダイアログ" /></a></p>
<p><strong>クイック アクセス ツールバー</strong></p>
<p style="padding-left: 30px;">AutoCAD のアプリケーション ボタン横に常に表示されるクイック アクセス ツールバーに、<strong>画層一覧コンボボックス&#0160;</strong>を表示出来るようになりました。表示中のリボン タブに関係なく、すばやく画層状態を切り替えることが出来るようになしました。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb097da245970d-pi" style="display: inline;"><img alt="2018_QAT_画像コンボボックス_small" class="asset  asset-image at-xid-6a0167607c2431970b01bb097da245970d img-responsive" src="/assets/image_175334.jpg" style="width: 800px;" title="2018_QAT_画像コンボボックス_small" /></a></p>
<p style="padding-left: 30px;">従来、クイック アクセス ツールバーには、 [画層プロパティ管理] パレットを表示する目的で、LAYER[画層管理] コマンドを起動するボタンしか配置することが出来ませんでした。</p>
<p><strong>&#0160;ステータス バー</strong></p>
<p style="padding-left: 30px;">AutoCAD 2016 で導入された SYSVARMONITOR[システム変数モニタ] &#0160;コマンドを使用すると、意図しないシステム変数を監視して、変更時に通知を受けることが出来ます。AutoCAD 2017までは、通知後にシステム変数の値を値セットするために、通知バルーンから [システム変数をモニタ]ダイアログを開く必要がありましたが、AutoCAD 2018 では、ステータスバーに表示された通知アイコンから右クリック メニューでシステム変数をリセット出来るようになりました。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8dd9827970b-pi" style="display: inline;"><img alt="Statusbar" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8dd9827970b image-full img-responsive" src="/assets/image_760435.jpg" title="Statusbar" /></a></p>
<p><strong>ラバーバンド色&#0160;</strong></p>
<p style="padding-left: 30px;">オブジェクト作図や編集時に表示されるラバーバンドの色が指定可能になりました。これによって、作図ウィンドウの背景色にあわせて目立つ色に変更が出来るので、オブジェクトが多数作図されて込み入った状態でも、正確にクロスヘア カーソルの位置を判別しやすくなります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d267f64e970c-pi" style="display: inline;"><img alt="Rubberband_color" class="asset  asset-image at-xid-6a0167607c2431970b01b8d267f64e970c img-responsive" src="/assets/image_782121.jpg" style="width: 600px;" title="Rubberband_color" /></a></p>
<p><strong><a id="offscreen"></a>オフスクリーン選択</strong></p>
<p style="padding-left: 30px;">AutoCAD 2017.1 Update で導入された機能です。従来、交差選択や窓選択をしながらズームや画面移動（パン）などの画面操作をした場合、選択したオブジェクトが画面の表示範囲からはずれてしまうと、それらの選択状態が解除されてしまいました。</p>
<p style="padding-left: 30px;">AutoCAD 2018（AutoCAD 2017.1 Update）では、画面新設された SELECTIONOFFSCREEN システム変数を 1 に設定することで、従来、事前選択中に画面外に移動されたオブジェクトの選択状態も、選択セットに保持されるようになります。&#0160;</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/vev5rqSbYvs?feature=oembed" width="500"></iframe></p>
<p class="asset-video"><strong>線種ギャップ選択</strong></p>
<p style="padding-left: 30px;">AutoCAD 2017.1 Update で導入された機能です。 以前のリリースでは、破線や一点鎖線など、線種の空白部分（スペース部分）でオブジェクトを選択したり、オブジェクト スナップ（O スナップ）することが出来ませんでしたが、AutoCAD 2018（AutoCAD 2017.1 Update）で、この部分が改善されています。</p>
<p style="padding-left: 30px;">DGN インポートした DGN 線種と幅を持つポリラインやスプラインなどでも、線種の空白部分でオブジェクトを選択したり、オブジェクト スナップさせて作図することが出来るようになりました。</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/kLgUOD2KeoQ?feature=oembed" width="500"></iframe><strong><br /></strong></p>
<p>次回は、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/03/new-features-on-autocad-2018-part2.html" rel="noopener noreferrer" target="_blank">図面化</a>&#0160;</strong>に関連する新機能をご紹介します。</p>
<p>By Toshiaki Isezaki</p>
