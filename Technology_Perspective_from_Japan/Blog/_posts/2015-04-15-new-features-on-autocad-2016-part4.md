---
layout: "post"
title: "AutoCAD 2016 の新機能 ～ その4"
date: "2015-04-15 00:18:11"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/04/new-features-on-autocad-2016-part4.html "
typepad_basename: "new-features-on-autocad-2016-part4"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/new-features-on-autocad-2016-part1.html" target="_blank">その1</a>、<a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-features-on-autocad-2016-part2.html" target="_blank">その2</a>、<a href="http://adndevblog.typepad.com/technology_perspective/2015/04/new-features-on-autocad-2016-part3.html" target="_blank">その3</a> と続けた AutoCAD 2016 の新機能紹介の最後に、接続性に関する機能をご紹介します。接続性とは、AutoCAD を使った図面作成や 3D モデリングの前後の工程で、他の製品やサービス、あるいは、他のデータ形式と強調しながら作業するワークフロー関連の機能を指しています。</p>
<p><strong>接続性 ～ PDF ファイルを使ったワークフロー</strong></p>
<p>従来から AutoCAD に搭載されていた PDF 関連の機能が大きな改良を受けました。主に、出力時のパフォーマンスと図面を PDF ファイルに出力する際の改良ですが、出力した PDF ファイル図面の利便性を高める機能も盛り込まれています。</p>
<p style="padding-left: 30px;"><strong>出力品質と新しいプリセット</strong></p>
<p style="padding-left: 30px;">従来の AutoCAD では、PDF ファイルで図面を出力する際に、"DWG To PDF.pc3" という単一のプロッタ環境設定を選択することが出来ました。AutoCAD 2016 では、さらに出力品質別に 4 つの新しいプリセットが追加されています。新しいプレセットは、EXPORTPDF[PDF 書き出し] コマンドや、PLOT[印刷] コマンドなどのダイアログ設定で指定することが出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb080b11a8970d-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb080b11a8970d image-full img-responsive" style="display: block; margin-left: auto; margin-right: auto;" title="Pdf_output_presets" src="/assets/image_264489.jpg" alt="Pdf_output_presets" border="0" /></a></p>
<p style="padding-left: 30px;">PDF プリセットの内容は、PDF オプションとしてアクセスすることが出来ます。各プリセットの違いは、ベクトルとラスター毎に指定できる解像度です。例えば、AutoCAD PDF (High Quality Print).pc3 ではベクトル品質の解像度が 2400 dpi なのに対して、AutoCAD PDF (Smallest File).pc3 では 400 dpi に設定されています。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7671bc9970b-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7671bc9970b image-full img-responsive" title="Pdf_options" src="/assets/image_983419.jpg" alt="Pdf_options" border="0" /></a></p>
<p style="padding-left: 30px;">解像度の高いプレセットを選択することで、より高品質な PDF ファイルを出力することが出来ますが、ファイルサイズは大きくなります。4 つのプリセットで、用途に合わせた出力が可能になります。</p>
<p style="padding-left: 30px;"><strong>TrueType 文字の利便性向上</strong><strong><br /></strong></p>
<p style="padding-left: 30px;">従来、出力された PDF ファイル内の TrueType フォントを利用した文字の検索が出来ませんでしたが、AutoCAD 2016 から出力された PDF ファイルでは、図面上の文字を PDF ファイル ビューアで検索出来るようになっています。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb080b12ff970d-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb080b12ff970d image-full img-responsive" title="Pdf_text_search" src="/assets/image_11870.jpg" alt="Pdf_text_search" border="0" /></a></p>
<p style="padding-left: 30px;">同様に、PDF 図面ファイル上の TrueType を使った日本語文字も、文字化けすることなくクリップボード経由でコピーして他のドキュメントに貼り付けることが出来ます。</p>
<p style="padding-left: 30px;"><strong>シェイプ フォント文字の注釈化</strong></p>
<p style="padding-left: 30px;">図面ファイルに記入された文字がシェイプ フォントであった場合、PDF 出力された際に自動的に注釈リストにリストアップされるようになり、リストから注釈をハイライトさせたり、必要に応じてテキスト ボックスでマークアップを表示することが出来ます。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f0adae970c-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f0adae970c image-full img-responsive" title="Pdf_markup" src="/assets/image_161690.jpg" alt="Pdf_markup" border="0" /></a></p>
<p style="padding-left: 30px;"><strong>ハイパーリンクとマルチシート PDF</strong></p>
<p style="padding-left: 30px;">図面ファイル内のハイパーリンクを、そのまま PDF ファイル内でも維持するようになりました。これによって、PDF 図面からハイパーリンクされた Web ページにジャンプしたりすることが出来ます。得に、シートセット マネージャーからプロジェクト全体のシートを PDF 出力した場合には、シート一覧表の各シートへのリンクで、目的のシートにジャンプして表示することも出来ます。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7671e72970b-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c7671e72970b image-full img-responsive" title="Pdf_hyperlink" src="/assets/image_37782.jpg" alt="Pdf_hyperlink" border="0" /></a></p>
<p><strong>接続性 ～ BIM ワークフロー</strong><a name="bim_underlay"></a></p>
<p>AutoCAD 2016 では、BIM（Building Information Modeling）を意識した機能も新たに搭載されています。AutoCAD 自体は BIM を体現する CAD ソフトウェアではありませんが、Navisworks を介して Revit プロジェクトを再利用したり、BIM 360 Glue プロジェクトをアタッチするなどの機能が盛り込まれています。</p>
<p style="padding-left: 30px;"><strong>BIM アンダーレイ</strong></p>
<p style="padding-left: 30px;">BIM を利用する建設業界では、建設前フェーズや建設フェーズを通じて、さまざまな業種の仮想調整に使用されるモデルを、コーディネーション モデルと呼んでいます。BIM アンダーレイは、コーディネーションモデルの検討や調整で利用されている <a href="http://www.autodesk.co.jp/navisworks" target="_blank"><strong>Navisworks</strong></a> ファイル（.nwc、.nwd）を&nbsp;AutoCAD 内にアタッチして、利用する機能を提供します。</p>
<p style="padding-left: 30px;">いままでの PDF アンダーレイ、DGN アンダーレイと同様に、外部参照としてアタッチすることになるため、実際にモデル自身を修正することは出来ませんが、AutoCAD のコンセプトモデルとの調整が可能です。現在の機能には、Navisworks が持つ干渉チェックやタイムライナーといった機能を継承することは残念ながら出来ませんが、BIM 専用 CAD や Navisworks が手元になくとも、AutoCAD で BIM ワークフローに参加することが出来るようになります。&nbsp;</p>
<p style="padding-left: 30px; text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/7ORsYQrdJtQ?feature=oembed" width="500"></iframe>&nbsp;</p>
<p style="padding-left: 30px;"><strong>BIM 360 プラグイン</strong></p>
<p style="padding-left: 30px;"><a href="http://www.autodesk.com/products/bim-360-glue" target="_blank"><strong>BIM 360 Glue</strong></a> は、クラウドを利用した BIM コラボレーションを実現するサービスです。まだ、日本語化されていませんが、ちょうど、コーディネーション モデルの調整、検討をおこなう Navisworks のクラウド版といった機能群を備えています。</p>
<p style="padding-left: 30px;">今回の AutoCAD 2016 では、新たに BIM 360 Glue プロジェクトのマージモデル、あるいは、一部のモデルを&nbsp;BIM アンダーレイと同じように AutoCAD 上にアタッチできるようになっています。BIM 360 Glue プロジェクトから、設備モデルと構造モデルだけをアタッチ後、AutoCAD で作成した 3D モデルを用いて、改修工事に伴う意匠設計の評価を AutoCAD 内で実施することが可能になります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f0c156970c-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f0c156970c image-full img-responsive" style="display: block; margin-left: auto; margin-right: auto;" title="Glue_attach" src="/assets/image_961084.jpg" alt="Glue_attach" border="0" /></a></p>
<p style="padding-left: 30px;">BIM 360 アドインを用いて AutoCAD 内でモデリングした 3D モデルを BIM 360 Glue プロジェクトにアップロード、マージするこで、更に高度なコーディネーション評価を実行することも出来ます。すなわち、代表的なコーディネーション作業となる干渉チェックです。前述の BIM アンダーレイでは、Navisworks 上でチェックした干渉チェックの結果を AutoCAD にフィードバックすることは出来ませんが、BIM 360 Glue との間では、この相互コミュニケーションをプロジェクトに参加するメンバ間でおこなうことが出来ます。</p>
<p style="padding-left: 30px; text-align: center;">&nbsp;<iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/6yxvhEeMZYA?feature=oembed" width="500"></iframe>&nbsp;</p>
<p style="padding-left: 30px;">&nbsp;</p>
<p><strong>AutoCAD 環境</strong></p>
<p>AutoCAD 2016 には、この他にも紹介すべき新機能が搭載されています。 最後に、AutoCAD の運用時に有用な 2 つの機能をご案内しておきます。</p>
<p style="padding-left: 30px;"><strong>システム変数モニタ</strong></p>
<p style="padding-left: 30px;">複数の設計者で 1 のプロジェクトを担当する際、重要になるのは図面の一貫性です。つまり、同じ画層や線種、寸法スタイル、文字スタイルを用いた図面の標準化が必要となります。AutoCAD は、図面の標準化をサポートするために&nbsp;<strong><a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-02979F86-385F-4A53-A3FB-7202F1225CDA" target="_blank">図面テンプレート</a></strong>&nbsp;の概念や、<a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-D64F8076-4978-44B7-B056-D921C77FEA88" target="_blank"><strong>CAD 標準仕様</strong></a> の機能を提供してきました。</p>
<p style="padding-left: 30px;">ただ、実際の作図環境は各種コマンドで簡単に設定を変えることが出来てしまいます。CAD 標準仕様を設定した環境で作図していても、仕様を逸脱した場合の警告は受け取れますが、いつ設定を変更してしまったのか、場合によっては把握できないことがあります。</p>
<p style="padding-left: 30px;">システム変数モニタは、あらかじめ監視しておきたいシステム変数を登録しておくことで、その値が変更された際に警告する機能を提供します。AutoCAD の作図環境は、ほとんどがシステム変数で設定、管理されいるので、かなり詳細に環境を管理することが出来ます。どちらかというと、CAD 管理者向けの機能と言えます。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0f0c6b1970c-pi" style="display: inline;"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0f0c6b1970c image-full img-responsive" title="System_variable_monitor" src="/assets/image_911913.jpg" alt="System_variable_monitor" border="0" /></a></p>
<p style="padding-left: 30px;"><strong>統一されたサービスパック</strong></p>
<p style="padding-left: 30px;">AutoCAD と AutoCAD Mechanical、AutoCAD Map 3D、AutoCAD Civil 3D などの AutoCAD ベースのバーチカル製品では、従来、同じ内容の修正が含まれる場合でも、サービスパックのリリース時期や名称が異なっていました。今回のバージョンかた、基準となっている AutoCAD のサービスパックは、そのまま業種別製品にも適用することが出来るようになります。もちろん、業種別製品固有の修正モジュールは、個別にリリースされるはずです。CAD 管理者は、製品毎に内容を把握する必要がなくなるので、サービスパックの適用が容易になるはずです。</p>
<p>より詳細な情報は、<span class="asset  asset-generic at-xid-6a0167607c2431970b01bb080b245c970d img-responsive"><a href="http://adndevblog.typepad.com/files/autocad-2016-%E3%83%97%E3%83%AC%E3%83%93%E3%83%A5%E3%83%BC%E3%82%AC%E3%82%A4%E3%83%89.pdf">AutoCAD 2016 プレビューガイド</a>&nbsp;</span>を参照してみてください。</p>
<p>By Toshiaki Isezaki</p>
<p>&nbsp;</p>
