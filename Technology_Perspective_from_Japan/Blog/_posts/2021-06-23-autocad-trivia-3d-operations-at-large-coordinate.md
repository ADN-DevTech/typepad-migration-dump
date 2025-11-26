---
layout: "post"
title: "AutoCAD 雑学：大座標での 3D 操作"
date: "2021-06-23 00:14:10"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/06/autocad-trivia-3d-operations-at-large-coordinate.html "
typepad_basename: "autocad-trivia-3d-operations-at-large-coordinate"
typepad_status: "Publish"
---

<p class="autodesk_styles_medium">3D ソリッドやサーフェス オブジェクトを作成して AutoCAD の&#0160;<a href="http://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-C38426A3-B4CA-4788-A6B9-F132DD705CA0" rel="noopener" target="_blank">UNION[和]</a>&#0160;コマンドや&#0160;<a href="http://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-14872FC1-8827-4D3B-978E-20936F9A78E5" rel="noopener" target="_blank">SUBTRACT[差]</a> コマンドなどのブール演算や、3D オブジェクトへの編集操作をすると、次のようなエラーメッセージを表示して処理に失敗してしまうことがあります。</p>
<blockquote>
<p class="autodesk_styles_medium">ソリッドまたはサーフェス ボディのブール演算に失敗しました。<br />モデリング操作エラー:<br />Error Code Number is <em>xxxxxxx</em>&#0160;<em>（xxxxxxx は数字）</em></p>
</blockquote>
<p class="autodesk_styles_medium">モデリング操作エラーや、DWG 図面自体の破損のほか、ソリッドやサーフェスモデル自身の破損が要因となている場合があります。</p>
<p class="autodesk_styles_medium">ソリッドやサーフェスモデルが破損しているか否かは、<a href="http://help.autodesk.com/view/ACD/2022/JPN/?guid=GUID-D54C266B-2B68-4660-ACA5-0579432F149C" rel="noopener" target="_blank">SOLIDEDIT[ソリッド編集]</a>&#0160;コマンドの B オプション &gt;&gt; C オプション（[ボディ(B)]: [チェック(C)]）で確認することが出来ます。まずは、ソリッドやサーフェスモデルが健全かどうかを確認してみてください。</p>
<p class="autodesk_styles_medium">また、このエラーは、<span class="caseEventBody"><span class="caseEventRow"><span class="feeditemtext cxfeeditemtext">倍精度実数で処理される大座標系での<a href="https://ja.wikipedia.org/wiki/%E8%AA%A4%E5%B7%AE#%E6%A1%81%E8%90%BD%E3%81%A1" rel="noopener" target="_blank">桁落ち</a>にによる不<wbr />整合（破損）</span></span></span>場合もあります。AutoCAD に限らず、座標の値は<a href="https://ja.wikipedia.org/wiki/%E5%80%8D%E7%B2%BE%E5%BA%A6%E6%B5%AE%E5%8B%95%E5%B0%8F%E6%95%B0%E7%82%B9%E6%95%B0" rel="noopener" target="_blank">倍精度実数</a>で保持されていますが、操作対象のオブジェクトが原点から遠く離れた大座標にある場合、コンピュータ演算上、座標で維持できる値の小数点以下の桁数（<a href="浮動小数点数" rel="noopener" target="_blank">浮動小数点数</a>）が決まっているため、有効桁数からはずれた誤差によって演算に失敗しまうケースです。</p>
<p class="autodesk_styles_medium"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bded2c3ca200c-pi" style="display: inline;"><img alt="Large_coordinate" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bded2c3ca200c image-full img-responsive" src="/assets/image_219376.jpg" title="Large_coordinate" /></a></p>
<p class="autodesk_styles_medium">このため、ObjectARX、.NET API、AutoLISP、ActiveX オートメーション（COM API）など、AutoCAD API でブール演算処理をおこなっても、同様のエラーが発生することがあります。</p>
<p class="autodesk_styles_medium">すべてのケースで問題を回避出来る訳ではありませんが、一般に、ソリッドやサーフェスモデル内部情報の破損の可能性を低減する、または、ブール演算時のエラーを低減するには、次の操作をお勧めしています。</p>
<ol>
<li>ソリッド/ サーフェスモデルの境界ボックス座標 X、Y、Z 値の最大値が 10,000 以上（マイナス値の場合、-10,000）になる場合、</li>
<li>最大座標が 10,000 未満（あるいは、-10,000 未満）になるようにソリッド/サーフェス モデルを一時的に原点付近置くか、最大座標が 10,000 未満になるように縮小する。</li>
<li>原点付近、かつ、最大座標が 10,000 未満になる状態でソリッド/ サーフェス モデルの編集、ブール演算をおこなう。</li>
<li>ソリッド / サーフェスモデルを縮小した場合は、元の尺度に拡大処理して、元の配置位置に移動する（戻す）。</li>
</ol>
<p>モデリング操作エラーが起こってしまう場合にお試しいただければと思います。</p>
<p>By Toshiaki Isezaki</p>
