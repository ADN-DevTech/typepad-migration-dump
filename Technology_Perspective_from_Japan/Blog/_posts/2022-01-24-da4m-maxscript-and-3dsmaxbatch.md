---
layout: "post"
title: "Design Automation API for 3ds Max：MAXScript と 3ds Max Batch"
date: "2022-01-24 00:02:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2022/01/da4m-maxscript-and-3dsmaxbatch.html "
typepad_basename: "da4m-maxscript-and-3dsmaxbatch"
typepad_status: "Publish"
---

<p>Forge の &#0160;<a href="https://adndevblog.typepad.com/technology_perspective/2020/07/forge-online-design-automation-basics.html" rel="noopener" target="_blank"><strong>Design Automation API</strong>&#0160;</a>に は、クラウド上で稼働させる「コアエンジン」が 4 タイプ用意されています。AutoCAD、Revit、Inventor、3ds Max です。これらコアエンジンはデスクトップ製品である AutoCAD、Revit、Inventor、3ds Max と異なり、ユーザ インタフェースを持たない「エンジン」です。遠隔で操作画面を見ながら対話作業することは出来ません。Design Automation API が提供するのは、コアエンジンにアドイン/プラグイン アプリをロードさせて実行する自動化の手段です。</p>
<p>いままで Design Automation API for AutoCAD、Revit、Inventor についてご紹介してきましたが、3ds Max についてはしていませんでした。今日以降、Design Automation API for 3ds Max コアエンジンを使った基本的な実装を数回にわたってご案内したいと思います。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880653cb1200d-pi" style="display: inline;"><img alt="Da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880653cb1200d image-full img-responsive" src="/assets/image_132242.jpg" title="Da" /></a></p>
<p><strong>3ds Max？</strong></p>
<p style="padding-left: 40px;">3ds Max は、設計ビジュアライゼーション、ゲーム、アニメーションに最適な 3D モデリングおよびレンダリング機能を提供するソフトウェアです。</p>
<p style="padding-left: 40px;">意外に感じる方もいらっしゃるかと思いますが、オートデスクの歴史の中では、AutoCAD に次いで古い歴史があります（旧名 3D Studio Max）。古い話ですが、日本では自動車の TV CM で <a href="https://en.wikipedia.org/wiki/Dancing_baby" rel="noopener" target="_blank">Dancing Baby</a>（ダンシング・ベイビー）が使われたことで、作成に使用された 3D Studio Max が話題になりました。 <a href="https://adndevblog.typepad.com/files/3ds_maxd_autocad_2014_3d_handbook.pdf"></a></p>
<p style="padding-left: 40px;">販売戦略の一環で、一時期 3D Studio VIZ や <a href="https://adndevblog.typepad.com/files/3ds_maxd_autocad_2014_3d_handbook.pdf" rel="noopener" target="_blank">3ds Max Design</a> の製品ラインで販売された時期がありましたが、現在は 3ds Max に統合されています。</p>
<p><strong>3ds Max のカスタマイズ</strong></p>
<p style="padding-left: 40px;">そんな 3ds Max には、プラグイン アプリケーションを作成手段として、大きく 3ds Max SDK を使用する C++ と、ネイティブ スクリプト言語である MAXScript の 2 つの方法が用意されています。</p>
<p style="padding-left: 40px;">MAXScript には pymxs Python モジュールによるラッパーが用意されているので、Python 言語を使ったスクリプトの作成も可能です（<a href="https://help.autodesk.com/view/MAXDEV/2021/ENU/?guid=Max_Python_API_using_pymxs_pymxs_differences_html" rel="noopener" target="_blank">一部制限あり</a>）。また、あまり一般的ではないように思いますが、<a href="https://help.autodesk.com/view/3DSMAX/2020/ENU/?guid=__developer_3ds_max_sdk___the_learning_path_lesson_7_writing__net_plug_ins_html" rel="noopener" target="_blank">.NET アセンブリ</a>を使った .NET プラグインの作成も可能になっています。</p>
<p style="padding-left: 40px;">カスタマイズに&#0160;<a href="https://help.autodesk.com/view/3DSMAX/2020/ENU/?guid=__developer_about_the_3ds_max_sdk_html" rel="noopener" target="_blank">3ds Max SDK</a> を使った C++、あるいは<a href="https://help.autodesk.com/view/3DSMAX/2020/JPN/?guid=GUID-CA4091CE-3656-4190-99DC-B2419214ED84" rel="noopener" target="_blank"> MAXScript</a> か、どちらの言語を選択するかは、作業の形態とプラグインで実行する目的によって異なります。両方の言語には長所と制約がありますが、どちらを使用しても複雑なアプリケーションを開発できます。</p>
<p style="padding-left: 40px;">一般に、MAXScript プラグインは、C++ で書かれた同等のプラグインよりも実行速度が遅くなります。したがって、パフォーマンスが重要になる場合は、SDK を使用した方がおそらく適しています。OLE/ActiveX/.NET コントロールを介して 3ds Max 機能をパブリッシュする場合、SDK を使用するよりも MAXScript でコード化した方が簡単です。MAXScript は、プラグインのプロトタイプ作成、比較的小さな機能の開発、およびテスト項目の作成に役立ちます。&#0160;</p>
<p style="padding-left: 40px;">今回は、この MAXScript を Design Automation API for 3ds Max で使用してみたいと思います。</p>
<p><strong>MAXScript の編集と実行</strong></p>
<p style="padding-left: 40px;">MAXScript は拡張子 .ms を持つ ASCII テキストファイルです。3ds Max の [スクリプト(S)] プルダウンから [<a href="https://help.autodesk.com/view/3DSMAX/2022/JPN/?guid=GUID-9AE37152-B6D3-473E-9490-DFFC62BDB005" rel="noopener" target="_blank">スクリプトを起動</a>(R)...] メニューをクリックして、.ms ファイルを指定して実行することが出来ます。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0278806603b5200d-pi" style="display: inline;"><img alt="Execute_script" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0278806603b5200d image-full img-responsive" src="/assets/image_282235.jpg" title="Execute_script" /></a></p>
<p style="padding-left: 40px;">MAXScript の編集は、[MAXScript エディタ(E)...]&#0160; メニューで<a href="https://help.autodesk.com/view/3DSMAX/2022/JPN/?guid=GUID-AA6E11BE-4EA8-42A7-8ED1-5C61E774485F" rel="noopener" target="_blank">エディタ</a>を起動しておこなうことが出来ます。実行はインタプリタで逐次実行されるため、エラーが発生すると、その時点で処理が中断してしまいます。</p>
<p style="padding-left: 40px;">MAXScript エディタに馴染みのない場合は、VS Code にオープンソースのエクステンションをインストールして利用することも可能です、例えば、<a href="https://marketplace.visualstudio.com/items?itemName=atelierbump.language-maxscript" rel="noopener" target="_blank">Language MaxScript</a> などがあります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f93be3c200c-pi" style="display: inline;"><img alt="Vs_extension" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f93be3c200c image-full img-responsive" src="/assets/image_373333.jpg" title="Vs_extension" /></a></p>
<p style="padding-left: 40px;"><a href="https://help.autodesk.com/view/3DSMAX/2022/JPN/?guid=GUID-C8019A8A-207F-48A0-985E-18D47FAD8F36" rel="noopener" target="_blank">MAXScript リスナー</a>を使うと、スクリプトを対話的に評価することが出来ます。MAXScript エディタと同じように独立したウィンドウを持つ MAXScript リスナーでは、スクリプトを１行づつ入力して実行したり、変数の内容を表示させてデバッグ作業に役立てることが出来ます。</p>
<p><strong>Design Automation API for 3ds Max のコアエンジン</strong></p>
<p style="padding-left: 40px;">Design Automation API for 3ds Max がコアエンジンとして使用するのは、<a href="https://knowledge.autodesk.com/ja/support/3ds-max/learn-explore/caas/CloudHelp/cloudhelp/2020/JPN/3DSMax-Batch/files/GUID-48A78515-C24B-4E46-AC5F-884FBCF40D59-htm.html" rel="noopener" target="_blank">3ds Max Batch</a> と呼ばれる 3dsmaxbatch.exe です。当然、MAXScript プラグインをロードして実行することが出来るようになっています。</p>
<p style="padding-left: 40px;">3dsmaxbatch.exe は 3ds Max のインストール フォルダに同梱されているので、コマンド プロンプトから起動して作成したスクリプトをテストすることが出来ます。 MAXScript の指定には、3dsmaxbatch.exe の起動オプションを使用します。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f93c12a200c-pi" style="display: inline;"><img alt="3dsmaxbatch" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02942f93c12a200c image-full img-responsive" src="/assets/image_827178.jpg" title="3dsmaxbatch" /></a></p>
<p><strong>ゴールの考察と最初のスクリプト</strong></p>
<p style="padding-left: 40px;">ここでは、AU 2021 でご紹介した <a href="https://www.autodesk.com/autodesk-university/ja/class/Forge-ViewerDesign-Automation-AutoCAD-tezuorukonfuikiyureta-2021" rel="noopener" target="_blank">Forge Viewer＋Design Automation AutoCAD で作るコンフィギュレータ</a> の素材を流用して、卓上扇風機モデル Table Fan.dwg を 3ds Max に読み込み、AutoCAD でモデルにアッタッチした <a href="https://knowledge.autodesk.com/ja/support/autocad/learn-explore/caas/sfdcarticles/sfdcarticles/JPN/How-to-install-the-Autodesk-Medium-Resolution-Materials-Image-Library.html" rel="noopener" target="_blank">Autodesk Material</a> と互換性を持つ <a href="https://help.autodesk.com/view/3DSMAX/2022/JPN/?guid=GUID-173D043E-0006-46E6-B8EC-35F3DB6BACBD" rel="noopener" target="_blank">ART レンダラー</a>を使ってレンダリング画像を作成する単純な例を考察してみます。</p>
<p style="padding-left: 40px;">具体的には、コンフィギュレーター機能の一部として、「羽根」の色や「葉っぱ」の有無を指定、その値を Design Automation に渡し、同内容のレンダリング画像を生成します。ただ、今回は、指定内容のパラメータ渡し部分は一旦割愛するものとします。</p>
<p style="padding-left: 40px;">Table Fan.dwg には、Color1 から Color6 に色別の画層があり、「羽根」ジオメトリがモデリングされているものとします。同様に「葉っぱ」ジオメトリは Leaf 画層にモデリングされています。また、すべての画層は「オン」/「フリーズなし」の状態で保存されています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e13ea341200b-pi" style="display: inline;"><img alt="Dwg_conditions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e13ea341200b image-full img-responsive" src="/assets/image_997872.jpg" title="Dwg_conditions" /></a></p>
<p style="padding-left: 40px;">下記は、「羽根」の色を赤 - 色番号 1、「葉っぱ」有りの状態を画層オン/オフで作り出し、<a href="https://help.autodesk.com/view/3DSMAX/2017/JPN/?guid=__files_GUID_9175301C_13E6_488B_ABA6_D27CD804B205_htm" rel="noopener" target="_blank">レンダリング</a>する MAXScript です。</p>
<div>
<blockquote>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* シーンリセット &amp; DWG 読み込み */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">resetMaxFile #noprompt</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">importFile &quot;C:/<em>&lt;your path&gt;</em>/Table Fan.dwg&quot; #noprompt</span></div>
<br />
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* 想定既定値（JSON 読み込み予定） */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">col = 1</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">leaf = true</span></div>
<br />
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* ColorX 画層オフ */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">global lay</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">for i = 1 to 6 do</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">(</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; if col != i then (</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; lay = LayerManager.getLayerFromName ( &quot;Color&quot; + i as string )</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; &#0160; &#0160; lay.on = false</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">&#0160; &#0160; )</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">)</span></div>
<br />
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* Leaf 画層オン/オフ */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">lay = LayerManager.getLayerFromName ( &quot;Leaf&quot; )</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">lay.on = leaf</span></div>
<br />
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* 既定パース ビューポート */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">viewport.activeviewport = 4</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">viewport.SetRenderLevel #smoothhighlights</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">viewport.setLayout #layout_4</span></div>
<br />
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* ART レンダラー設定 */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">global art = ART_Renderer()</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.quality_db = 20</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.render_method = 1</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.anti_aliasing_filter_diameter = 2.0</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.enable_noise_filter = true</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">art.noise_filter_strength = 1</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">renderers.current = art</span></div>
<br />
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">/* レンダリング &amp; 生成画像保存 */</span></div>
<div><span style="font-family: arial, helvetica, sans-serif; font-size: 10pt;">undisplay ( render outputfile:&quot;C:/<em>&lt;your path&gt;</em>/result.jpg&quot; )</span></div>
</blockquote>
</div>
<p style="padding-left: 40px;">このスクリプトで生成されたレンダリング画像は次のようになります。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1411bad200b-pi" style="display: inline;"><img alt="Result2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1411bad200b image-full img-responsive" src="/assets/image_650113.jpg" title="Result2" /></a></p>
<ul>
<li><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788066062c200d-pi" style="float: right;"><img alt="Dwg_import_settings" class="asset  asset-image at-xid-6a0167607c2431970b02788066062c200d img-responsive" src="/assets/image_32982.jpg" style="width: 250px; margin: 0px 0px 5px 5px;" title="Dwg_import_settings" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02942f93c04f200c-pi" style="float: right;"></a>AutoCAD DWG の読み込み時、形状をなめらかにするために「3D ソリッドの最大サーフェス偏差」設定を調整します。この設定値は、残念ながら、MAXScript からアクセスすることが出来ません。<br /><br />同設定値は、C:\Users\<em>&lt;user name&gt;</em>\AppData\Local\Autodesk\3dsMax\2022 - 64bit\JPN\ja-JP\plugcfg フォルダにある&#0160; dwg_dxf_import.ini ファイルの DWG_ACISDeviation 項に保存されますが、Design Automation API for 3ds Max の WorkItem 実行時、事前に用意した dwg_dxf_import.ini ファイルをコアエンジンに認識させる方法は出来ません。<br /><br />このため、先のスクリプトで読み込みとレンダリングを自動化した場合、既定値 1.0 が適用されてしまい、形状によっては、ポリゴンが目立つ状態になってしまう場合があります。<br /><br />対応策として一般的なのは、あらかじめ（手動で）「3D ソリッドの最大サーフェス偏差」を調整して読み込み、保存したシーン ファイル（.max）を用意して、Design Automation API for 3ds Max で使用する方法です。</li>
<li>ここでの目的は、MAXScript を Design Automation API for 3ds Max で実行させることなので、レンダリング設定や内容は最小限にとどめています。ART レンダラーの利用は、AutoCAD 上で DWG 内のジオメトリに適用した Autodesk Material の利用を意図したもので、それ以上の理由は特にありません。既定の Arnold レンダラーでは、 Autodesk Material のレンダリングで警告が多数表示されてしまい、Design Automation API のレポートログが見にくくなってしまうのを避けています。</li>
</ul>
<p>今後、MAXScript から JSON ファイルを読み込んで、「羽根」の色や「葉っぱ」の有無を動的に変化させる方法を考えてみます。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
