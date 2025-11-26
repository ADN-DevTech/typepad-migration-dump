---
layout: "post"
title: "AutoCAD 2021 の新機能 ～ その2"
date: "2020-03-30 00:10:51"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2020/03/new-features-on-autocad-2021-part2.html "
typepad_basename: "new-features-on-autocad-2021-part2"
typepad_status: "Publish"
---

<p>前回、新しくリリースされた AutoCAD 2021、AutoCAD LT 2021 の「<a href="https://adndevblog.typepad.com/technology_perspective/2020/03/new-features-on-autocad-2021-part1.html" rel="noopener" target="_blank"><strong>基本的な情報</strong></a>」をお伝えしましたので、今回は「<strong>向上した生産性</strong>」についてご紹介したいと思います。</p>
<p>主に 2D 編集機能になりますが、AutoLISP の開発環境にも大きな手が加えられたのが特徴的です。なお、ここでご案内する内容の内、特に明記の無い限り、AutoCAD LT 2021 でもお使いいただける内容となります。</p>
<hr />
<p><strong>合理化されたトリムと延⻑</strong></p>
<p style="padding-left: 40px;"><strong><a href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-B1A185EF-07C6-4C53-A76F-05ADE11F5C32" rel="noopener" target="_blank">TRIM[トリム]</a> </strong>コマンドと <strong><a href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-89DD7B0F-F4F1-410D-9A3A-5847CA5F8744" rel="noopener" target="_blank">EXTEND[延長]</a></strong> コマンドに、[クイック] モードでが追加され、コマンド起動時に既定オプションとして利用出来るようになしました。[クイック] モードでは、図面内のすべての図形が切り取りエッジ、または延長エッジとみなされます。切り取り、あるいは、延長対象となるオブジェクトを指定するだけの操作で、目的を達成することが出来ます。</p>
<p style="padding-left: 40px;">下記は、AutoCAD 2021 と AutoCAD 2020 での TRIM コマンドの様子です。AutoCAD 2021 の場合、境界となるオブジェクトを、都度、指定しなければならなかった AutoCAD 2020 より、明らかに操作数が低減していることがわかります。 <a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51a490f200b-pi"></a></p>
<p style="padding-left: 40px; text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51a9b00200b-pi" style="display: inline;"><img alt="TRIM" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a51a9b00200b image-full img-responsive" src="/assets/image_945318.jpg" title="TRIM" /></a></p>
<p style="padding-left: 40px;">同じく、次の様子は、EXTEND コマンドを利用した例です。こちらも、[クイック] モードが既定値になっています。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f5de5d200d-pi" style="display: inline;"><img alt="EXTEND" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f5de5d200d image-full img-responsive" src="/assets/image_382376.jpg" title="EXTEND" /></a></p>
<p style="padding-left: 40px;">AutoCAD 2021 でも、コマンド オプションを指定することで、従来のようにエッジを指定して切り取りや延長の操作をおこなうことも出来ます。</p>
<p><strong>雲マークの機能強化</strong></p>
<p style="padding-left: 40px;">従来、<strong><a class="xref" href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-7BC6D4B1-5279-4B5F-90E0-AC87DA861E78" rel="noopener" target="_blank">REVCLOUD[雲マーク]</a></strong> コマンドで雲マークを作図で雲マークの円弧長を指定することが出来ませんでしたが、今回、この円弧長（「<strong>円弧の長さ</strong>」= <strong>弦長</strong>）を指定出来るようになりました。作図後の雲マークに対しても、[プロパティ] パレットに「<strong>円弧の長さ</strong>」が表示され、変更が可能です。AutoCAD 2020 では、残念ながら、「<strong>円弧の長さ</strong>」を指定することが出来ませんでした。</p>
<p style="padding-left: 40px; text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51a9e49200b-pi" style="display: inline;"><img alt="REV_CLOUD" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a51a9e49200b image-full img-responsive" src="/assets/image_589645.jpg" title="REV_CLOUD" /></a></p>
<p><strong>オブジェクト分割</strong></p>
<p style="padding-left: 40px;">オブジェクトを指定した 1 点指示で分割することが出来る、<strong><a href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-E0439DE0-B2C3-4233-BB4D-5A574A00694B" rel="noopener" target="_blank">BREAKATPOINT[点で部分削除]</a></strong> コマンドが新設されています。従来、1 点でオブジェクトを分割するには、<strong><a href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-36A1CDE0-3871-4B25-AC98-93235FA83863" itemprop="url" rel="noopener" target="_blank">BREAK[部分削除]</a></strong> コマンドの F オプションを指定して、同じ個所を 2 回指定しなければならなかったので少し面倒でした。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f5e4d4200d-pi" style="display: inline;"><img alt="BREAK" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f5e4d4200d image-full img-responsive" src="/assets/image_798730.jpg" title="BREAK" /></a></p>
<p><strong>クイック計測</strong></p>
<p class="p" style="padding-left: 40px;"><a href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-5D5B0EE1-DD90-47AE-8A55-642FBFF5E4E4" rel="noopener" target="_blank"><strong>MEASUREGEOM[ジオメトリ計測]</strong></a> コマンドの [クイック] オプションで、図面の平面図内のジオメトリ オブジェクトで囲まれたスペース内の面積と周長の計測がサポートされるようになりました。</p>
<p class="p" style="padding-left: 40px;">閉じた領域内をクリックするとその領域が緑でハイライト表示されて、計算された値がコマンド ウィンドウとダイナミック ツールチップに現在の単位形式で表示されます。[Shift]を 押しながら複数の領域をクリックして選択すると、累積面積と周長が計算されます。</p>
<p class="p" style="padding-left: 40px; text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a51a3eba200b-pi" style="display: inline;"><img alt="MESUREGEOM_2021" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a51a3eba200b image-full img-responsive" src="/assets/image_49621.jpg" title="MESUREGEOM_2021" /></a></p>
<p><strong>グラフィックス パフォーマンス</strong></p>
<p class="p" style="padding-left: 40px;">2D での画面移動とズームは、類似のプロパティを持つオブジェクトを利用するテクニックと、各種レベルの表示倍率で適切な詳細レベルを表示する他のテクニックによって強化されました。</p>
<p class="p" style="padding-left: 40px;">2D でリアルタイムに画面移動やズームを行うと、AutoCAD ベースの製品は必要に応じて自動的に再作図操作を実行します。通常、この操作は非常に大きい図面以外では目立ちません。非常に大きい図面の場合は、システム変数 <strong><a href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-9B52A286-BFCD-47B7-9FAC-197BBDB246D8" rel="noopener" target="_blank">RTREGENAUTO</a></strong> をオフにして、自動再作図が行われないようにすることができます。</p>
<p class="p" style="padding-left: 40px;">3D モデルを使用している場合は、ナビゲーション操作によって、状況に応じて 3D ジオメトリの忠実度の高いグラフィック表現や忠実度の低いグラフィック表現が生成されます。マルチコア プロセッサを使用することによって、3D オービット、画面移動、ズーム操作を使用したときのプログラムの応答性が大幅に向上しました。この機能強化は、曲面をレンダリングする表示スタイルを使用した複雑な 3D モデルで顕著に確認できます。</p>
<p><strong>AutoLISP</strong> <strong>の機能強化 － AutoCAD のみ</strong></p>
<p style="padding-left: 40px;">AutoLISP は、最も古い AutoCAD カスタマイズのために用意された API です。この AutoLISP をプログラミングするためのエディタが、AutoCAD R14 で導入された Visual LISP エディタです。Visual LISP エディタは、<strong><a class="xref" href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-5601ACC6-C4F6-4375-9C2C-3DBCAE2880B1">VLISP[VLISP エディタ]</a></strong> コマンドで表示することが出来ます。</p>
<p style="padding-left: 40px;">ただ、AutoCAD が AutoCAD 2007 で UNICODE に対応していますが、Visual LISP エディタは完全に UNICODE に対応していなかったため、エディタ上で一部、日本語が正しく表示出来ない問題が存在していました。</p>
<p style="padding-left: 40px;">今回、AutoLISP の編集に、Microsoft 社がオープン ソースとして無償で公開している <strong><a href="https://azure.microsoft.com/ja-jp/products/visual-studio-code/" rel="noopener" target="_blank">Visual Studio Code</a></strong>（通称、VS Code）を採用し、UNICODE に完全対応出来るようになりました。</p>
<p style="padding-left: 40px;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4f59d07200d-pi" style="display: inline;"><img alt="Vscode" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4f59d07200d image-full img-responsive" src="/assets/image_171313.jpg" title="Vscode" /></a></p>
<p style="padding-left: 40px;">VS Code には、AutoCAD とともに AutoLISP をデバッグ出来るよう、VS Code 用の <strong><a href="https://marketplace.visualstudio.com/items?itemName=Autodesk.autolispext" rel="noopener" target="_blank">AutoCAD AutoLISP Extension</a> </strong>が提供されています。これにより、Visual LISP エディタと同じようにシンボリック デバッグやウォッチ式を使いながら、プログラミングをおこなうことが出来るようになっています。</p>
<p style="padding-left: 40px;">システム変数 <strong><a class="xref" href="http://help.autodesk.com/view/ACD/2021/JPN/?guid=GUID-1853092D-6E6D-4A06-8956-AD2C3DF203A3">LISPSYS</a> </strong>によって、VLIDE コマンドで Visual LISP エディタを利用するか、VS Code を利用するかを変更することが可能です。</p>
<hr />
<p>次回は「<a href="https://adndevblog.typepad.com/technology_perspective/2020/04/new-features-on-autocad-2021-part3.html" rel="noopener" target="_blank"><strong>進化したワークフロー</strong></a>」の機能についてご案内したいと思います。</p>
<p>By Toshiaki Isezaki</p>
