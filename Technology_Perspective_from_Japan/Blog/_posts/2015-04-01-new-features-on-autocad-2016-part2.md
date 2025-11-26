---
layout: "post"
title: "AutoCAD 2016 の新機能 ～ その2"
date: "2015-04-01 00:53:23"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/04/new-features-on-autocad-2016-part2.html "
typepad_basename: "new-features-on-autocad-2016-part2"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/new-features-on-autocad-2016-part1.html" target="_blank">前回</a> に引き続き、AutoCAD 2016 の機能をご紹介していきます。モダン - スマート の続きです。</p>
<p><strong>モダン - スマート ～&#0160;リアリティ コンピューティング</strong></p>
<p>AutoCAD 2016 では、レーザースキャナで計測した点群データをインポートするだけでなく、2 時的な編集に活用する機能が加わりました。なお、前バージョンと同様、AutoCAD 2016 には点群データをプロジェクト ベースで編集できるデスクトップ版の ReCap が同梱されています。AutoCAD 上で点群データを利用する前に、ReCap で不要な部分を削除するなどの操作が可能です。</p>
<p style="padding-left: 30px;"><strong>ダイナミック UCS</strong></p>
<p style="padding-left: 30px;">アタッチした点群データにセグメンテーション データが含まれている場合、マウスカーソルの動きに合わせて点群の面を自動識別する ダイナミック UCS を利用することができます。AutoCAD 2015 では、ダイナミック UCS が点群の面を識別できなかったので、3D モデリングの操作前にUCS アイコンを手動で移動させて、&#0160;ユーザ座標系を手動設定しておく必要がありました。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7656deb970b-pi" style="display: inline;"><img alt="点群_ダイナミックUCS小" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7656deb970b image-full img-responsive" src="/assets/image_115423.jpg" title="点群_ダイナミックUCS小" /></a></p>
<p style="padding-left: 30px;"><strong>クロップされた状態の保存と復元</strong></p>
<p style="padding-left: 30px;">クロップされた点群オブジェクトの状態を名前を付けて保存したり、復元することが出来るようになしました。クロップの状態は、名前のついたビューには記録されませんが、VIEW[ビュー管理] コマンドともに利用すると、矩形、円形、ポリゴンでクロップされた領域を即座に呼び出せるようになります。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb080af162970d-pi" style="display: inline;"><img alt="Crop_restore" class="asset  asset-image at-xid-6a0167607c2431970b01bb080af162970d img-responsive" src="/assets/image_678524.jpg" style="width: 350px;" title="Crop_restore" /></a></p>
<p style="padding-left: 30px;"><strong>断面オブジェクトによる断面生成</strong></p>
<p style="padding-left: 30px;">図面にアタッチされた点群データに任意に&#0160;<strong><a href="http://help.autodesk.com/view/ACD/2016/JPN/?guid=GUID-43559F01-8D66-423C-BEB4-F2BCC200DBD8" target="_blank">断面オブジェクト</a>&#0160;</strong>を設定することが出来るようになりました。また、ライブ断面に線分やポリラインを生成することが出来るので、点群データとして取り込んだ現況から、平面図や立面図を作成することが容易になりました。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0eee8db970c-pi" style="display: inline;"><img alt="Section_geometory" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0eee8db970c image-full img-responsive" src="/assets/image_440347.jpg" title="Section_geometory" /></a></p>
<p style="padding-left: 30px;">断面に線分、または、ポリラインを生成する場合には、「処理する点の最大数」として、あからじめ精度も指定することが出来ます。精度を挙げると、断面図形生成に必要な計算時間が長くなります。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb080967ef970d-pi" style="display: inline;"><img alt="Section_precision" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb080967ef970d image-full img-responsive" src="/assets/image_422564.jpg" title="Section_precision" /></a></p>
<p style="padding-left: 30px;"><strong>交差エッジや点の検出</strong></p>
<p style="padding-left: 30px;">点群データ内の複数の面を検出して、2 つの面が交差する部分に線分図形を作図したり、3 つの面の交点部分に点図形を作図することが出来ます。これらの機能を利用すると、点群の現況データに基づいてたモデリング時に有用です。</p>
<p style="padding-left: 30px;">ここまでの機能を簡単な動画にまとめているので、ご覧ください。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/vN9B3A-3puY?feature=oembed" width="500"></iframe>&#0160;</p>
<p><strong>モダン - スマート ～ 新しいレンダリング エンジン</strong></p>
<p>AutoCAD 内部で利用するレンダリング エンジンを一新しました。従来の AutoCAD は、NVIDIA mental ray® を利用していましたが、AutoCAD 2016 はオートデスク純正の RapidRT と呼ばれるレンダリング エンジンを採用しています。RapidRT では、UNITS コマンドの照明設定に関わらず、物理的に正確なフォトメトリック環境のみをサポートします。</p>
<p style="padding-left: 30px;"><strong>シンプルな設定</strong></p>
<p style="padding-left: 30px;">新いいレンダリングでは、設定しなければならない項目が劇的に少なくなっているので、細かな設定で悩むことはありません。簡単な操作と短い時間で、美しいフォトリアリスティックなレンダリング画像を手に入れることが出来ます。逆に言えば、影の設定など、シーンによってを加えたい微調整は出来なくなっています。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08096adb970d-pi" style="display: inline;"><img alt="Rendering_settings" class="asset  asset-image at-xid-6a0167607c2431970b01bb08096adb970d img-responsive" src="/assets/image_750038.jpg" title="Rendering_settings" /></a></p>
<p style="padding-left: 30px;"><strong>消費時間による新しいレンダリング プリセット</strong></p>
<p style="padding-left: 30px;">mental ray レンダリング エンジンを採用していた前バージョンまで、レンダリング品質を決定するプレセットには、[ドラフト]、[低]、[中]、[高]、[プレゼンテーション] の 5 &#0160;種類が用意されていました。いずれのプリセットも品質を基準にしていたため、どれくらいの時間でレンダリング画像が生成されるか、実際に処理しないとわからないという欠点がありました。</p>
<p style="padding-left: 30px;">AutoCAD 2016 では、品質別のプリセット [低]、[中]、[高] に加えて、利用できる時間を基準にしてレンダリング品質を決定するプリセットを用意しています。[コーヒーブレイク品質] では 10 分間、[ランチタイム品質] では 1 時間、[オーバーナイト] 品質では 12 時間で、きっかりレンダリング処理を終了することが出来ます。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7661fb8970b-pi" style="display: inline;"><img alt="Rendering_presets" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7661fb8970b img-responsive" src="/assets/image_492813.jpg" style="width: 340px;" title="Rendering_presets" /></a></p>
<p style="padding-left: 30px;"><strong>環境とイメージベースの照明</strong></p>
<p style="padding-left: 30px;">RapidRT では、フォトメトリック環境のみをサポートしますが、必ずしも光源を設定する必要はありません。環境 という新しい設定が用意されていて、IBL イメージ（画像）を指定することが出来るためです。[レンダリング環境と露出] パレットで「環境」をオンに設定することで IBL イメージを指定することが出来ます。IBL イメージは、光源情報が埋め込まれいるので、既定のプリセットを選択するだけで、レンダリング時に適切な明るさを持つレンダリング画像が生成されます。また、IBL イメージの中には、周囲 360 度の背景画像を持つものも存在します。レンダリング ビューを変えるだけで、背景画像が変化します。</p>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7663e3a970b-pi" style="display: inline;"><img alt="Rendering_exposure" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7663e3a970b img-responsive" src="/assets/image_748530.jpg" title="Rendering_exposure" /></a></p>
<p style="padding-left: 30px;">背景画像を持つ IBL イメージには、[石膏クレータ]、[乾いた湖底]、[プラザ]、[雪の雪原]、[村落] があります。次のレンダリング画像は、機動車モデルのレンダリング時に、[乾いた湖底]、[プラザ]、[雪の雪原] を設定してそれぞれ演算させたものです。IBL イメージ毎に、車両のボディに反射する周辺光の明るさや反射の度合いが異なっている点に注意してみてください。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0efc6e7970c-pi" style="display: inline;"><img alt="Ibl_rendering" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0efc6e7970c image-full img-responsive" src="/assets/image_785724.jpg" title="Ibl_rendering" /></a></p>
<p style="padding-left: 30px;">なお、従来と同様に、ANIPATH[アニメーション パス]&#0160;コマンドでレンダリング品質の動画を作成することが出来ますが、残念ながら、IBL イメージを利用することは出来ません。</p>
<p style="padding-left: 30px;"><strong>レンダリング ウィンドウ</strong></p>
<p style="padding-left: 30px;">RapidRT の演算時には、従来のようにキャンセル割り込みによる中断が容易になっていて、レンダリング ウィンドウ上でレンダリング状態を確認中に、いつでも [ESC] キーでレンダリング処理を終了することが出来ます。また、途中で中断したレンダリング結果をファイルとして保存することが出来るばかりでなく、レンダリングも演算中でも、その段階のレンダリング画像を保存出来ます。つまり、ある程度の結果が出ていたら、その状態の画像をプレゼンテーションに利用出来ます。なお、レンダリング出力は、レンダリング ウィンドウの他に、ビューポート全体、ビューポート内の指定領域を指定可能です。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0efcf15970c-pi" style="display: inline;"><img alt="Rendering_window" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0efcf15970c image-full img-responsive" src="/assets/image_742059.jpg" title="Rendering_window" /></a></p>
<p>ここまで、モダン - スマート のテーマに沿った新機能をご紹介してきましたが、次回は、生産性向上 テーマに基づいて、主に 2D 図面作図に関わる機能群をご案内します。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
