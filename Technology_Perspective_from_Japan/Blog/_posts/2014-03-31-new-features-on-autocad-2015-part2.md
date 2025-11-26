---
layout: "post"
title: "AutoCAD 2015 の新機能 ～ その 2"
date: "2014-03-31 00:57:57"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/03/new-features-on-autocad-2015-part2.html "
typepad_basename: "new-features-on-autocad-2015-part2"
typepad_status: "Publish"
---

<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/03/new-features-on-autocad-2015-part1.html" target="_blank">前回</a></strong>に続いて、AutoCAD 2015 の新機能についてご紹介していきます。今回は、作図領域の強化点、作図や編集時のプレビュー機能についてです。</p>
<p><strong>ハードウェア アクセラレーション ダイアログ</strong></p>
<p>ハードウェア アクセラレーション ダイアログが分かり易くシンプルなダイアログ ボックスに変更されました。AutoCAD 2015 では、新しい GRAPHICSCONFIG コマンドで 1 つの [グラフィックス パフォーマンス] ダイアログを表示するだけです。従来のバージョンでは、3DCONFIG コマンドで [レンダリングモードの最適化とパフォーマンスの調整] ダイアログと、[主導によるパフォーマンス調整] ダイアログの2つのダイアログに機能表示や設定が分割されていました。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d8c8474970d-pi" style="display: inline;"><img alt="グラフィックスパフォーマンス" class="asset  asset-image at-xid-6a0167607c2431970b01a73d8c8474970d img-responsive" src="/assets/image_355170.jpg" style="width: 400px;" title="グラフィックスパフォーマンス" /></a></p>
<p><strong>ライン スムージング</strong></p>
<p>ハードウェア アクセラレーションの有無に関係なく、ライスムージング機能を有効化することが出来るようになりました。この機能は、線分や円弧を表示した際の表示表現に、アンチエイリアス機能を用いて滑らかで明瞭な図形表示を実現します。ちょうど、Windows XP で導入された <strong><a href="http://ja.wikipedia.org/wiki/ClearType" target="_blank">ClearType フォント</a>&#0160;</strong>のような効果を図面表示に期待することが出来ます。次の画面では、AutoCAD 2014 でギザギザで表現されていた傾斜のある線分が、AutoCAD 2015 で滑らかに表現されている部分の比較です（<span style="background-color: #ffff00;">クリックして原寸で確認してみてください</span>）。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d8d7d09970d-pi" style="display: inline;"><img alt="ラインスムージング" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73d8d7d09970d image-full img-responsive" src="/assets/image_969931.jpg" title="ラインスムージング" /></a></p>
<p>表示する図面内容にもよりますが、液晶ディプレイが主流になった現在では、ラインスムージング機能を使って表示された図面を見慣れると、以前の表示との差に驚くことがあります。</p>
<p><strong>オブジェクト選択時のグラフィック フィードバック</strong></p>
<p>ハードウェア アクセラレーションが有効（オン）になっている際には、今までよりも明確にオブジェクト選択をグラフィカルにフィードバックするようになりました。 同時に、既定でオフになっていたハッチング パターンのハイライト処理も行えます。ハードウェア アクセラレーションが無効（オフ）になっている際には、いままで同じように破線表現でハイライトするようになります。</p>
<p>また、窓選択や交差選択、ポリゴン選択などの選択方法に加えて、新しく <strong>投げ縄選択</strong> が出来るようになりました。Adobe Photshop などの画像処理ソフトウェアでは一般的ですが、AutoCAD/LT でもこの選択方法を使うことで、選択が難しい場所にあるオブジェクトをマウス左ボタンのドラッグ操作で的確に選択できるようになります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51186228d970c-pi" style="display: inline;"><img alt="選択フィードバック" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a51186228d970c image-full img-responsive" src="/assets/image_230720.jpg" title="選択フィードバック" /></a></p>
<p>ライス ムージングの機能も含めて、選択処理の改良点を動画にしていますので、ぜひご覧ください。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/Ix7CPF4L-Zk?feature=oembed" width="500"></iframe>&#0160;</p>
<p><strong>コマンド プレビュー</strong></p>
<p>TRIM[トリム]、EXTEND[延長]、BREAK[部分削除]、OFFSET[オフセット]、FILLET[フィレット]、CHAMFER[面取り] のコマンド実行中に、入力した値を画面上でプレビュー表示する機能が加わりました。いままで、これらのコマンドを実行して期待した結果が得られなかった場合、一度、アンドゥしたり、図形を削除して、再度、コマンドを実行する必要がありましたが、事前のプレビューで結果が分かるので、二度手間を省いて生産性を向上させることが出来るはずです。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/oUtxa6MC_j4?feature=oembed" width="500"></iframe>&#0160;</p>
<p><strong>マルチテキスト エディタ</strong></p>
<p>マルチテキスト エディタにも生産性を向上させるための改良が加えられています。代表的な機能を 3 つ紹介しておきましょう。</p>
<p>マルチテキスト エディタで文字を入力する際に、&quot;.&quot;、”)&quot; 、&quot;&gt;&quot; 、&quot;}&quot; や &quot;]&quot;&#0160;などの特定の文字記号に続けて、 スペースかタブを入力して改行すると、自動的に箇条書きまたは番号付きのリストが作成されるようになります。</p>
<p>分数表現の認識が自動化され、斜線と水平線の切り替えも容易になりました。この点は、次の画像で具体的な動作を確認してみてください。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fce30b63970b-pi" style="display: inline;"><img alt="文字スタック" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fce30b63970b image-full img-responsive" src="/assets/image_401785.jpg" title="文字スタック" /></a></p>
<p>新しい TEXTALIGN[文字列位置合わせ] コマンドによって、複数のマルチテキストの位置合わせが簡単におこなえるようになっています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a51192c0ec970c-pi" style="display: inline;"><img alt="文字位置合わせ" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a51192c0ec970c image-full img-responsive" src="/assets/image_966821.jpg" title="文字位置合わせ" /></a></p>
<p><strong>ポリライン編集</strong></p>
<p>ポリラインの円弧セグメントをフィレットできるようになりました。円弧の描画時に [Ctrl] キーを押すことで、ポリラインの円弧を逆方向に作成できます。また、円弧の描画時に [Ctrl ]キーを押すことで、ポリラインの円弧を逆方向に作成できます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73d9dd5a2970d-pi" style="display: inline;"><img alt="ポリライン円弧の作図方向" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73d9dd5a2970d image-full img-responsive" src="/assets/image_778261.jpg" title="ポリライン円弧の作図方向" /></a></p>
<p>ポリラインの円弧セグメントが存在した場合でも、ポリラインを分解せずにフィレットを加えることが出来るようになりました。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcd5c2d2970b-pi" style="display: inline;"><img alt="ポリライン円弧のフィレット" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcd5c2d2970b image-full img-responsive" src="/assets/image_243149.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="ポリライン円弧のフィレット" /></a></p>
<p><strong>寸法記入</strong></p>
<p>寸法の記入時にインテリジェントなオブジェクト スナップ動作が提供されるようになりました。寸法を配置する際には、スナップ先のジオメトリ付近に寸法補助線がスナップされてしまうのを防ぐため、既存の寸法補助線は無視されます。寸法補助線のオブジェクト スナップ動作は、[オプション]ダイアログ ボックスの[作図補助]タブに新設されたオプションでコントロールが可能です。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fce30aa3970b-pi" style="display: inline;"><img alt="寸法補助線スナップの無視" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fce30aa3970b image-full img-responsive" src="/assets/image_420556.jpg" title="寸法補助線スナップの無視" /></a></p>
<p>次回は、地理的情報や点群との連携など、3D 機能に関係する新機能をご紹介します。</p>
<p>By Toshiaki Isezaki&#0160;</p>
<p>&#0160;</p>
