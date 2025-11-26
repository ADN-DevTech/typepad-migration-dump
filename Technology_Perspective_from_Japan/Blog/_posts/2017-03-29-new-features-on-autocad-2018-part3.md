---
layout: "post"
title: "AutoCAD 2018 の新機能 ～ その3"
date: "2017-03-29 00:06:00"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/03/new-features-on-autocad-2018-part3.html "
typepad_basename: "new-features-on-autocad-2018-part3"
typepad_status: "Publish"
---

<p>前回ご紹介した <strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/03/new-features-on-autocad-2018-part2.html" rel="noopener noreferrer" target="_blank">図面化</a></strong> 機能に引き続き、AutoCAD 2018 の新機能から&#0160;<strong>コラボレーション</strong> 機能をご紹介します。ここでご案内する機能は、AutoCAD LT 2018 でも利用することが出来ます。</p>
<p><strong>外部参照</strong></p>
<p style="padding-left: 30px;">今回のリリースでは、外部参照で発生しがちな参照パスの破損、つまり、参照ファイルが見つけられない状況について、破損を起こりにくくするための機能と、破損パスが発生してしまった際の修復に焦点をあてた機能が複数盛り込まれています。</p>
<p style="padding-left: 30px;">AutoCAD 2018 では、新規に図面ファイルを外部参照としてアタッチする際に、保存するパスの種類の既定値が「絶対パス」から「相対パス」に変更されています。これによって、外部参照作成時とは異なるコンピュータで親ファイルを開いた際に、参照パスが見つからなくなってしまう問題を低減します。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2680558970c-pi" style="display: inline;"><img alt="Relative_path_default" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2680558970c image-full img-responsive" src="/assets/image_402918.jpg" title="Relative_path_default" /></a></p>
<p style="padding-left: 30px;">もし、既定値のパスを変更したい場合には、新設された<strong>システム変数&#0160;REFPATHTYPE</strong> の値を変更して既定値を変えることも出来ます。指定可能は整数値は、次のとおりです。</p>
<p style="padding-left: 60px;"><span style="font-family: &#39;arial black&#39;, &#39;avant garde&#39;;">0</span>：パスなし<br /><span style="font-family: &#39;arial black&#39;, &#39;avant garde&#39;;">1</span>：相対パス<br /><span style="font-family: &#39;arial black&#39;, &#39;avant garde&#39;;">2</span>：絶対パス</p>
<p style="padding-left: 30px;">外部参照の参照パスに破損が発生してしまった場合にも、以前のリリースに比べて簡単に修復するオプションが追加されています。</p>
<p style="padding-left: 30px;">[外部参照] パレットの参照ファイル一覧で、参照ファイルに破損パスを示す<strong>！</strong>アイコンと「見つかりません」状態が表示された場合、そのファイル上で右クリックをすると、[新しいパスを選択] &#0160;と [検索と置換] メニューが表示されるようになりました。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0980dded970d-pi" style="display: inline;"><img alt="Resolve_missed_paths" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0980dded970d image-full img-responsive" src="/assets/image_675168.jpg" title="Resolve_missed_paths" /></a></p>
<p style="padding-left: 30px;"><strong>[新しいパスを選択] &#0160;を選択した場合</strong></p>
<p style="padding-left: 30px;">[新しいパスを選択] &#0160;を選択すると、ファイル ダイアログを表示して当該ファイルの新しいパスを更新するために参照ファイルの選択を促します。ファイルを選択して新しいパスが更新される際には、システム変数&#0160;REFPATHTYPE&#0160;で設定された値によって、参照パスの種類が選択されます（<span style="font-family: &#39;arial black&#39;, &#39;avant garde&#39;;">0</span>：パスなしに設定されている場合は絶対パスで認識されます）。</p>
<p style="padding-left: 30px;">もし、親ファイルが破損パスを持つ参照ファイルを複数持っていて、どれか 1 つの破損パスを [新しいパスを選択] &#0160;で解決すると、次のようなメッセージを表示して、同じパスで他の破損パスを修復するか確認を求められます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb098142d5970d-pi" style="display: inline;"><img alt="Apply_other_refiles" class="asset  asset-image at-xid-6a0167607c2431970b01bb098142d5970d img-responsive" src="/assets/image_436770.jpg" style="width: 380px;" title="Apply_other_refiles" /></a></p>
<p style="padding-left: 30px;">[新しいパスを選択] &#0160;は、ファイル毎に 1 つづつ新しいパスを選択してパスを解決するものですが、破損パスがすべて同じパスで解決されることが明らかな場合は、ここで &#0160;[はい] をクリックすると、一括して破損パスを解決することが出来ます。</p>
<p style="padding-left: 30px;"><strong>[検索と置換] を選択した場合</strong>&#0160;</p>
<p style="padding-left: 30px;">[検索と置換] &#0160;を選択した場合には 次の [選択パスの検索と置換] ダイアログが表示されるので、ここでは「置換後のパス」に新しい参照パスを指定します。[新しいパスを選択] &#0160;とは異なり、指定するのはファイルではなくパスです。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2686e90970c-pi" style="display: inline;"><img alt="Replace_path" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2686e90970c img-responsive" src="/assets/image_625996.jpg" style="width: 450px;" title="Replace_path" /></a></p>
<p style="padding-left: 30px;">この機能も破損パスを持つ参照ファイル毎に指定可能ですが、同時に複数の参照ファイルを選択して右クリック メニューから新しいパスを指定することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb098142bf970d-pi" style="display: inline;"><img alt="Select_multiple_files" class="asset  asset-image at-xid-6a0167607c2431970b01bb098142bf970d img-responsive" src="/assets/image_376298.jpg" style="width: 400px;" title="Select_multiple_files" /></a></p>
<p style="padding-left: 30px;">パスの置換が完了すると、置換結果がメッセージ ダイアログで表示されます。もし、結果に問題がある場合には、UNDO[元に戻す] コマンドでアンドゥすることも可能です。<br /> <br /> <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8de0e35970b-pi" style="display: inline;"><img alt="Replace_path_completion" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8de0e35970b img-responsive" src="/assets/image_685150.jpg" style="width: 380px;" title="Replace_path_completion" /></a></p>
<p style="padding-left: 30px;">AutoCAD 2018 では、外部参照ファイルがネスト（参照ファイルがさらに別の参照ファイルを参照している）していて、かつ、破損パスの場合の表現も変更されています。</p>
<p style="padding-left: 30px;">以前のリリースでは、AutoCAD で表示中の親ファイルが参照している子ファイルへのパスが破損していて、更に、子ファイルが孫ファイルを外部参照していると、[外部参照] パレットの表示を一覧表示で表示した場合、子ファイルは「ロードされていません」、孫ファイルは「参照されていません」と表示されていました。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d26870e3970c-pi" style="display: inline;"><img alt="2017_list" class="asset  asset-image at-xid-6a0167607c2431970b01b8d26870e3970c img-responsive" src="/assets/image_968717.jpg" style="width: 400px;" title="2017_list" /></a></p>
<p style="padding-left: 30px;">AutoCAD 2018 では、破損パスを持つ子ファイルの孫ファイルを「孤立」と表現するように変更されています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09814491970d-pi" style="display: inline;"><img alt="2018_list" class="asset  asset-image at-xid-6a0167607c2431970b01bb09814491970d img-responsive" src="/assets/image_685852.jpg" style="width: 400px;" title="2018_list" /></a></p>
<p style="padding-left: 30px;">また、以前のリリースでは、[外部参照] パレットの表示をツリー表示にした際に子ファイルも孫ファイルも一律の階層に表示されてしまい、相関関係を把握しにくい状態でした。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8de1012970b-pi" style="display: inline;"><img alt="2017_hierarchy" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8de1012970b img-responsive" src="/assets/image_454873.jpg" style="width: 400px;" title="2017_hierarchy" /></a></p>
<p style="padding-left: 30px;">AutoCAD 2018 では、「孤立」した孫ファイルが子ファイル下に階層的に表示されるようになったため、ファイル間の参照関係を正確に把握することが出来るようになっています。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8de102a970b-pi" style="display: inline;"><img alt="2018_hierarchy" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8de102a970b img-responsive" src="/assets/image_302551.jpg" style="width: 490px;" title="2018_hierarchy" /></a></p>
<p style="padding-left: 30px;">ここでご紹介した以外にも、外部参照の機能に細かい変更が加えられています。</p>
<p><strong>デザイン ビュー</strong></p>
<p style="padding-left: 30px;">AutoCAD 2017 で導入されたコラボレーション機能が、引き続き強化されています。関係者と 2D や 3D デザインを共有、閲覧してもらう「デザイン ビューを共有」機能です。この機能には、アプリケーションメニューの &#0160;[パブリッシュ] &gt;&gt; [デザイン ビュー] メニューか、[A360] リボンタブの [デザイン ビューを共有] メニューからアクセスすることが出来ます。どちらのメニューを使用した場合でも、実際に起動されるのは &#0160;<strong>ONLINEDESIGNSHARE[オンライン設計共有] &#0160;コマンド</strong> です。</p>
<p style="padding-left: 30px;">デザイン ビューは、誰でも利用出来るオンラインの A360 ビューアを使って、関係者とデザインを共有する機能を提供します。A360 ビューアは、AutoCAD 図面以外にも、2D、3D を問わず、多様な CAD のデザインファイルをストリーミング配信で Web ブラウザに表示する、オートデスクの Forge テクノロジを利用したオンライン ビューアです。</p>
<p style="padding-left: 30px;">AutoCAD 内から&#0160;ONLINEDESIGNSHARE[オンライン設計共有] &#0160;コマンドを実行すると、オートデスク アカウント（AutoCAD ID）でのサインインを求められ、AutoCAD で表示中の図面ファイル（2D 図面、または、3D モデル）がクラウドにアップロードされ、Web ブラウザにデザインを表示出来るように変換されます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09814847970d-pi" style="display: inline;"><img alt="Design_view_mechanism" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09814847970d image-full img-responsive" src="/assets/image_984304.jpg" title="Design_view_mechanism" /></a></p>
<p style="padding-left: 30px;">Web ブラウザがあれば、特に何もインストールする必要はありません。デバイスの種類に関係なくストリーミング配信されたデザイン ファイルを閲覧してジオメトリを計測したり、ジオメトリが持つプロパティを確認したりすることが出来ます。</p>
<p style="padding-left: 30px;">配信される 2D/3D デザインはクラウド上でいったん変換されて読み込み専用の様態で A360 ビューア上に表示されるため、オリジナルのファイルを誤って編集するようなことはありません。AutoCAD で作成したデザインを関係者とオンラインでコラボレーションするための機能です。</p>
<p style="padding-left: 30px;">A360 ビューアにデザインが表示されたら、ビューア上部の Comments ボタンをクリックして表示中の図面に対するコメントを投稿することが出来ます。投稿したコメントは、ビューアで同じ図面を参照した際に Comments ボタンでいつでも表示出来るので、共有先の相手にメッセージを残すようなことも可能になります。</p>
<p style="padding-left: 30px;">ビューア上部の Get link ボタンをクリックすると、関係者と図面を共有するための URL が生成されます。この URL を電子メールなどで相手に通知すれば、相手の環境でも同じ図面をオンラインで閲覧してもらうことが出来ます。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d2687417970c-pi" style="display: inline;"><img alt="Comments" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d2687417970c image-full img-responsive" src="/assets/image_942777.jpg" title="Comments" /></a></p>
<p style="padding-left: 30px;">A360 ビューアには、もともと複数の 関係者とリアルタイムに画面表示を同期させてコラボレーションをサポートする <strong>ライブ レビュー</strong>&#0160;が用意されているので、この機能を使って、表示するビューを互いに同期させながらコミュニケーションすることも出来ます。</p>
<p style="padding-left: 30px;">アップロードしたデザイン ファイルは、自身で削除しない限り、 30 日間保持されます。この期間内であれば、いつでも A360 ビューアで図面の内容を閲覧することが可能です。A360 ビューアへアクセスする場合には、オートデスクのホームページ <strong><a href="http://www.autodesk.co.jp" rel="noopener noreferrer" target="_blank">http://www.autodesk.co.jp</a></strong>&#0160;のページ下部にある「<strong>オンライン ビューアでファイルを見る</strong>」リンクか、URL に<strong><a href=" https://a360.autodesk.com/viewer/" rel="noopener noreferrer" target="_blank"> https://a360.autodesk.com/viewer/</a></strong>&#0160;を指定して直接 A360 ビューア ページへジャンプしてください。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d26872c7970c-pi" style="display: inline;"><img alt="Access_a360_viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d26872c7970c image-full img-responsive" src="/assets/image_181918.jpg" title="Access_a360_viewer" /></a></p>
<p style="padding-left: 30px;">ページ右上に [<strong>Sign In</strong>] ボタン、または、「<strong>Start Viewing</strong>」が表示されるので、AutoCAD からデザインをアップロードした際にサインインしていたオートデスク アカウント（ Autodesk ID）でサインインしてください。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d26873ec970c-pi" style="display: inline;"><img alt="Signin_to_a360_viewer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d26873ec970c image-full img-responsive" src="/assets/image_918355.jpg" title="Signin_to_a360_viewer" /></a></p>
<p style="padding-left: 30px;">サインインすると、同じアカウントで最近アップロードしたファイルが一覧表示されます。閲覧したいファイルをクリックすれば、A360 ビューアが起動してファイルの閲覧が出来るようになります。前述のとおり、一覧されたファイルは 30 日を経過した図面は自動的に削除されて閲覧が出来なくなりますが、一覧から <strong>(Extend)</strong>&#0160;をクリックすると、最大 30 日（当日を含め 31 日）まで保存期間を延長可能です。各ファイル枠右上の <strong>･･･</strong> をクリックすれば、Delete リンクを表示します。このリンクでファイルを明示的に削除することも出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb098145da970d-pi" style="display: inline;"><img alt="Recent_uploads" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb098145da970d image-full img-responsive" src="/assets/image_599480.jpg" title="Recent_uploads" /></a></p>
<p style="padding-left: 30px;">一連の内容をまとめていますので、次の動画をご確認してみてください。&#0160;</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/eiBoRbWboec?feature=oembed" width="500"></iframe><strong>&#0160;</strong></p>
<p>次回は、AutoCAD 2018 で導入された新しい <strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/03/new-features-on-autocad-2018-part4.html" rel="noopener noreferrer" target="_blank">テクノロジとパフォーマンス強化</a></strong>&#0160;についてご紹介します。</p>
<p>By Toshiaki Isezaki</p>
