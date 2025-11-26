---
layout: "post"
title: "AutoCAD 雑学：図形選択の変化"
date: "2024-09-18 00:07:43"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/09/autocad-trivia-entity-selection.html "
typepad_basename: "autocad-trivia-entity-selection"
typepad_status: "Publish"
---

<p>パーソ ナル コンピュータ（Personal Computer、PC）の登場から Windows の誕生までの期間、搭載されていたオペレーティング システム（Operating System、OS）は、文字入力と出力が標準たっだ <a href="https://ja.wikipedia.org/wiki/MS-DOS" rel="noopener" target="_blank">MS-DOS</a> でした。そんな PC の利用を加速させたのが、OS 上で動作する 3rd party ソフトウエア（アプリケーション、またはアプリ）の存在です。AutoCAD はそんな時期にアプリの 1 つとして誕生しました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0d05dd7200d-pi" style="display: inline;"><img alt="Autocad_history_1172x660" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0d05dd7200d image-full img-responsive" src="/assets/image_737461.jpg" title="Autocad_history_1172x660" /></a></p>
<p>当時、他のアプリと比べて AutoCAD がユニークだったのは、主に図形を扱うアプリだった点です。ただ、当時のディスプレイ解像度は非常に低く、PC/AT 互換機（DOS/V 搭載機）で &#0160;640×480（VGA） 、PC-9800 シリーズ機で 640×400（ノーマルモード）でした。発色も 8 色 や 16 色が普通だたため、初期の AutoCAD で図形で表現出来たのは、色番号 1 ～ 7 の ごく限られた色しかありませんでした。</p>
<p>忘れてはならないのは、図形を作図・編集する上で重要なポインティング デバイスの存在です。座標の指示や図形の選択で使用するのは現在と同じですが、グラフィカル ユーザー インターフェース（Graphical User Interface、GUI）の実現が難しかった時代、主流になっていたのは、マウスではなく、コマンドを選択しやすい「タブレット」と呼ばれていたディジタイザーです。</p>
<p>当時のマウスは、マウス底部に物理的に埋め込まれたボールを保持、その転がりから移動量を得ていたため、より高精細に移動量を検出出来る電磁誘導式のタブレットが好まれていたとも考えられます。</p>
<p>最近は見かける機会も少なくなりましたが、タブレットの名残りは今も AutoCAD の存在しています。メニュー用紙を印刷してタブレットに貼り付けて使うタブレットメニュー用紙（<a href="https://download.autodesk.com/us/samplefiles/acad/tablet.dwg" rel="noopener" target="_blank">tablet.dwg</a>） が <a href="https://www.autodesk.com/jp/support/technical/article/caas/tsarticles/tsarticles/JPN/ts/6XGQklp3ZcBFqljLPjrnQ9.html" rel="noopener" target="_blank">AutoCAD サンプルファイル</a>&#0160;に記載されていたり、貼り付けたタブレットメニューの位置合わせをする <a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-6ED485A6-EAF0-47EF-84FC-4975BBAE3DA3" rel="noopener" target="_blank">TABLET[タブレット設定]</a> コマンドなどです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bfed97200b-pi" style="display: inline;"><img alt="Dawn_period" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bfed97200b image-full img-responsive" src="/assets/image_389079.jpg" title="Dawn_period" /></a></p>
<p>前置きが長くなりましたが、低い解像度と乏しい発色数しかない環境で、当時、どう図形が選択されたことを操作者に伝えていたか、というお話です。</p>
<p>ご存じのように、AutoCAD は図形選択を知らせる目的で、図形を「ハイライト」表示する方法をとっています。MS-DOS 版の AutoCAD では、ハイライトの表現方法が現在とは大きく異なり、選択した図形を破線表示することで選択状態を伝えていました。破線表現のハイライト表示は、いまでもシステム変数 <a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-E3D7841C-87EB-4F5C-86C7-213FF9469BA8" rel="noopener" target="_blank">SELECTIONEFFECT</a> を 0 に変更することで再現することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0d02b33200d-pi" style="display: inline;"><img alt="Highlight" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0d02b33200d image-full img-responsive" src="/assets/image_954375.jpg" title="Highlight" /></a></p>
<p>現在のハイライト表現は、グラフィックス カードを含む PC 自体の性能向上を反映して、ハードウェア アクセラレーションを使ったブレンド色を使い、よりグラフィカルなものになっています（システム変数 <a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-E3D7841C-87EB-4F5C-86C7-213FF9469BA8" rel="noopener" target="_blank">SELECTIONEFFECT</a> が 1）。また、図形選択と同時に編集用の「グリップ」も表示するので、破線表現に比べ、高解像度モニター使用時の視認性が向上しています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bc6028200c-pi" style="display: inline;"><img alt="Highlight_today" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bc6028200c image-full img-responsive" src="/assets/image_971929.jpg" title="Highlight_today" /></a></p>
<p>Windows 上での AutoCAD 利用が進んでくると、それまでの窓選択や交差選択時の表現も分かり易く改良されてきました。AutoCAD は、2 点の座標を指示して矩形領域の内側に入る図形を選択する「窓選択」と、矩形領域の境界に交差するか、矩形領域の内側入る図形を選択する「交差選択」を、1 点目から 2 点目の座標指示の左右の順番で判定します。左手から右手の点を指示すると、実線のラバーバンドが矩形表現されて「窓選択」に、逆に、右手から左手の点を指示すると、破線のラバーバンドが矩形表現されて「交差選択」になります。</p>
<p>当初、窓選択と交差選択時の矩形領域は無着色でしたが、現在では窓選択時の矩形領域は半透明の青系色、交差択時の矩形領域は半透明の緑系色に着色されて、ラバーバンドの線種と共に高解像度モニターでも選択モードが明瞭になっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0d03011200d-pi" style="display: inline;"><img alt="Wc_selection" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0d03011200d image-full img-responsive" src="/assets/image_987041.jpg" title="Wc_selection" /></a></p>
<p>視覚的効果を利用して確実に図形を選択させようとする試みも導入されています。「選択のプレビュー」です。マウス操作でクロスヘア カーソルを図形の上に移動（ホバー）すると、カーソル下の選択対象をハイライト表現でプレビュー表示します。そのまま、マウスの左クリックで図形を選択することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0d03f6b200d-pi" style="display: inline;"><img alt="Selection_preview" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0d03f6b200d image-full img-responsive" src="/assets/image_603208.jpg" title="Selection_preview" /></a></p>
<p>選択が難しい重なり合った図形の選択方法も改良が続けられて、AutoCAD 2011 以降、「選択の循環」ツールによる選択が実現されています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0d041fe200d-pi" style="display: inline;"><img alt="Selection_cycling" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0d041fe200d image-full img-responsive" src="/assets/image_11574.jpg" title="Selection_cycling" /></a></p>
<p>AutoCAD 2015 になると、新しく「投げ縄選択」が登場します。画像処理ソフトウェアでは一般的ですが、AutoCAD/LT でもこの選択方法を使って、選択が難しい入り組んだ場所にあるオブジェクトを的確に選択出来るようになっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02dad0d030c4200d-pi" style="display: inline;"><img alt="Wc2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02dad0d030c4200d image-full img-responsive" src="/assets/image_71806.jpg" title="Wc2" /></a></p>
<p>問題だったのは、ームや画面移動（パン）を交えた際の図形選択です。当初、交差選択や窓選択をしながらズームやパンなどの画面操作をしてしまうと、選択したオブジェクトが画面の表示範囲からはずれた段階で、それらの選択状態が解除されてしまいました。</p>
<p>AutoCAD 2018（AutoCAD 2017.1 Update）では、新設されたシステム変数 <a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-2609B540-EF39-45B5-8682-FEC7F16B77DE" rel="noopener" target="_blank">SELECTIONOFFSCREEN</a> を 1 に設定することで、従来、事前選択中に画面外に出てしまったオブジェクトの選択状態も保持されるようになっていす。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bc6357200c-pi" style="display: inline;"><img alt="Off_screen_selection" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bc6357200c image-full img-responsive" src="/assets/image_825744.jpg" title="Off_screen_selection" /></a></p>
<p>図形選択のタイミングも、MS-DOS 版を含む初期の AutoCAD と Windows 版で変化しています。当初、コマンドを実行してから図形の選択を求める方法が編集コマンドの基本動作でした。MS-DOS 版に加えて Windows 版も導入された AutoCAD R12 では、システム変数 <a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-258E01D5-83E8-40EB-9662-78C626A60844" rel="noopener" target="_blank">PICKFIRST</a> を追加して、コマンド実行前に図形を選択する方法が導入されました。この「事前選択」の導入で、図形選択の柔軟性が向上しています。</p>
<p>ここまでご紹介してきた図形選択の振る舞いや視覚効果のオプションは、<a href="https://help.autodesk.com/view/ACD/2025/JPN/?guid=GUID-DD2A36B6-1196-4205-9A07-32056292F0B5" rel="noopener" target="_blank">OPTIONS[オプション] ダイアログの [選択] タブ</a>で変更することが出来ます。&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3bfc0d1200b-pi" style="display: inline;"><img alt="Options" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3bfc0d1200b image-full img-responsive" src="/assets/image_407360.jpg" title="Options" /></a></p>
<p>また、API を利用すると、目線を変えたカスタマイズに図形選択を応用することも出来ます。</p>
<ul>
<li><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/50cXUNbSGm44JPZkA5VO2H.html" rel="noopener" target="_blank">AutoCAD .NET API ：選択セットのセッション間の維持</a></li>
<li><a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/autocad-net-api-komando-shi-xing-shino-shi-qian-xuan-zeobujekutono-qu-deto-xuan-ze-jie-chuno-yi-zhi/ta-p/13010848" rel="noopener" target="_blank">AutoCAD .NET API：コマンド実行時の事前選択オブジェクトの取得と選択解除の抑止</a></li>
<li><a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/autocad-net-api-komando-zhong-le-hounoobujekuto-xuan-ze-zhuang-taino-wei-chi/ta-p/13010785" rel="noopener" target="_blank">AutoCAD .NET API：コマンド終了後のオブジェクト選択状態の維持</a></li>
<li><a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/autocad-net-api-zuo-biao-yao-suwo-li-yongshita-xuan-zesetto-firutaringu/ta-p/12059873" rel="noopener" target="_blank">AutoCAD .NET API ：座標要素を利用した選択セット フィルタリング</a></li>
</ul>
<p>By Toshiaki Isezaki</p>
