---
layout: "post"
title: "AutoCAD 2024 新機能 ～ その４"
date: "2023-04-12 00:10:48"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/04/new-features-on-autocad-2024-part4.html "
typepad_basename: "new-features-on-autocad-2024-part4"
typepad_status: "Publish"
---

<p>AutoCAD 2024 には、他にも多彩な新機能や機能改良が施されています。</p>
<p><strong>アクティビティ インサイト（AutoCAD Plus のみ）</strong></p>
<p>アクティビティ インサイトは、AutoCAD で図面ファイルを開いて作業するたびに、イベントを追跡、記録する機能です。Windows エクスプローラ上での図面ファイル名前の変更や、図面ファイルのコピーなど、AutoCAD 外部のイベントも一部、追跡・記録することが出来ます。</p>
<p>[オプション] ダイアログ ボックスの [ファイル] タブには、 [アクティビティ インサイト イベントの場所] が追加されていて、イベント情報を書き込む場所（パス）を指定する仕組みなので、共有パスを指定しておくと、誰が作業しているかに関係なく、すべての作図アクティビティが 1 つの場所に記録されます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b7519bbf1b200c-pi" style="display: inline;"><img alt="Activity_insight1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b7519bbf1b200c image-full img-responsive" src="/assets/image_700542.jpg" title="Activity_insight1" /></a></p>
<p>図面を開くと、その図面で過去に実行されたイベントが読み込まれ、[アクティビティ インサイト] パレットに時系列で表示されます。図面を開いて作業中の状態でも、編集イベントが書き込まれ、パレットの表示内容が最新の状態を保ちます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751776f4c200b-pi" style="display: inline;"><img alt="Activity_insight2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751776f4c200b image-full img-responsive" src="/assets/image_843531.jpg" title="Activity_insight2" /></a></p>
<p>アクティビティ インサイトによって、図面に関して自分や他のユーザが実行した過去のアクションを理解することができます。</p>
<p><strong>スタート タブ</strong></p>
<p>最近使用した図面のリストに小さいサムネイルを使用することで、より多くの図面が表示できるようになりました。また、グリッド表示とリスト表示の両方で、一覧表示される図面を並べ替えたり、検索したりすることが出来るようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6852e97c9200d-pi" style="display: inline;"><img alt="スタートタブx800" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6852e97c9200d image-full img-responsive" src="/assets/image_846457.jpg" title="スタートタブx800" /></a></p>
<p><strong>ファイル タブとレイアウト タブ</strong></p>
<p>図面を表示する子ウィンドウ内のタブにも、利便性を考慮したユーザ インタフェースが追加されています。</p>
<p>子ウィンドウ上部のファイル タブ メニューを使用すると、図面を切り替えたり、図面の作成や、図面を開く、すべての図面を保存する、すべての図面を閉じる、といった動作を指定することが出来ます。</p>
<p>子ウィンドウ下部のレイアウト タブ メニューを使用すると、レイアウトを切り替えたり、テンプレートからレイアウトを作成したり、レイアウトをパブリッシュしたりすることが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6852e98c1200d-pi" style="display: inline;"><img alt="File_layout_tab" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6852e98c1200d image-full img-responsive" src="/assets/image_257148.jpg" title="File_layout_tab" /></a></p>
<p>ファイル タブ メニューからレイアウトにカーソルを合わせてもレイアウト一覧が一時的に表示されるので、印刷およびパブリッシュ用のアイコンから印刷・パブリッシュ動作を指定することも可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75177706f200b-pi" style="display: inline;"><img alt="File_tab_layout_plot" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75177706f200b image-full img-responsive" src="/assets/image_767492.jpg" title="File_tab_layout_plot" /></a></p>
<p><strong>AutoCAD Web の AutoLISP 機能（AutoCAD Plus のみ）</strong></p>
<p>AutoCAD Plus ライセンスでは、AutoCAD Web（<a href="https://web.autocad.com/" rel="noopener" target="_blank">https://web.autocad.com/</a>）の使用時に AutoLISP を利用することが出来ます。AutoCAD Web にはスマートブロック置換の機能はありませんが、事前に AutoLISP プログラムを用意すれば、指定したブロックを別のブロックに置き換えたり、さまざまな処理機能を持つカスタム コマンドを実行することが可能です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6852e97bb200d-pi" style="display: inline;"><img alt="AutoLISP_Webx800" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6852e97bb200d image-full img-responsive" src="/assets/image_288567.jpg" title="AutoLISP_Webx800" /></a></p>
<p><strong>AutoCAD</strong> <strong>LT</strong><strong> の </strong><strong>AutoLISP</strong><strong> 機能</strong></p>
<p>日本では既に AutoCAD LT の新規販売を終了していますが、AutoCAD LT 2024 でも AutoLISP をロードして実行することが出来るようになっています。<a href="https://help.autodesk.com/view/ACDLT/2024/JPN/?guid=GUID-037BF4D4-755E-4A5C-8136-80E85CCEDF3E" rel="noopener" target="_blank">一部制限</a>があるものの、カスタム コマンドの作成と運用を実現することが出来るようになりました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b6852e9b6b200d-pi" style="display: inline;"><img alt="Lt_autolisp" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b6852e9b6b200d image-full img-responsive" src="/assets/image_951234.jpg" title="Lt_autolisp" /></a></p>
<p>&#0160;</p>
<p>この他にも、2D/3D グラフィックスへの継続したパフォーマンス改善や、同じくパフォーマンスを主眼とした Autodesk Docs へのアクセス改良などが実施されています。</p>
<p>By Toshiaki Isezaki</p>
