---
layout: "post"
title: "AutoCAD 雑学：R12 DXF"
date: "2024-01-29 00:40:42"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/01/autocad-trivia-r12-dxf.html "
typepad_basename: "autocad-trivia-r12-dxf"
typepad_status: "Publish"
---

<p>ご存じのとおり、AutoCAD の図面ファイルは、使用する AutoCAD バージョンによって既定で保存する DWG・DXF のファイル形式が異なっています。なぜ図面ファイル形式を定期的に更新するのかは、過去に <a href="https://adndevblog.typepad.com/technology_perspective/2014/08/reason-for-updating-drawing-format.html" rel="noopener" target="_blank">図面ファイル形式の更新について</a> の記事で触れたこともありました。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a973c3200d-pi" style="display: inline;"><img alt="Intercompatibilities" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a973c3200d image-full img-responsive" src="/assets/image_196179.jpg" title="Intercompatibilities" /></a></p>
<p>最近では、1 つの図面ファイル形式を複数のバージョンで既定として扱うようになっています。Revit のようにバージョン毎にファイル形式が更新されてしまい、より新しいバージョンで保存した RVT ファイルを旧バージョンで開けない、といった製品も存在する中、広範なバージョン間で同じ DWG ファイルを開ける利点をもたらしています。</p>
<p>加えて、より新しいバージョンでも、古いバージョンが採用していたファイル形式で保存する機能も提供しています。この際に使用するのが <a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-1FF801F9-7FEE-4494-854D-4704A7784232" rel="noopener" target="_blank">SAVEAS[名前を付けて保存]</a> コマンドです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a52486200c-pi" style="display: inline;"><img alt="Saveas" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a52486200c image-full img-responsive" src="/assets/image_759184.jpg" title="Saveas" /></a></p>
<p>ここで気になるのが &quot;AutoCAD R12/LT2 DXF&quot; 形式の存在です。言うまでもなく、この形式は、30年以上前 ‼ の1992年当時最新だった AutoCAD R12（日本では AutoCAD R12J と呼称）が採用していた DXF ファイル形式です。</p>
<p>まず、「<strong>なぜ、R12 なのか？</strong>」ですが、端的に言えば、「<strong>AutoCAD R12 が扱い、保存する図形の構造が最もシンプルだったから</strong>」です。</p>
<p>一般にはあまり認知されていませんが、AutoCAD の歴史上、この次の AutoCAD R13 で内部的に加えられた変更がかなり大がかりなものでした。</p>
<p>言い換えると、それまで AutoCAD を開発するために継承・使用していたプログラム（ソースコード）を一新したのが AutoCAD R13 です。開発者寄りの難しめな表現を使うなら、AutoCAD R13 では C++ 言語を用いた「<a href="https://ja.wikipedia.org/wiki/%E3%82%AA%E3%83%96%E3%82%B8%E3%82%A7%E3%82%AF%E3%83%88%E6%8C%87%E5%90%91%E3%83%97%E3%83%AD%E3%82%B0%E3%83%A9%E3%83%9F%E3%83%B3%E3%82%B0" rel="noopener" target="_blank">オブジェクト指向プログラミング</a>」が採用したことで、製品のアーキテクチャが大きく変化しています。</p>
<p>この大きな変更によって、AutoCAD 機能のコンポーネント化（モジュール化）が可能になりました。この点は、以前、<a href="https://adndevblog.typepad.com/technology_perspective/2022/03/autocad-trivia-components.html">AutoCAD 雑学：AutoCAD コンポーネント</a> でも触れていますが、AutoCAD ユーザーの視点では、図面内で多様な図形を扱える利点を生むことになりました。</p>
<p>例を挙げると、「自由曲線」と表現される「スプライン」があります。AutoCAD R12 までは、スプライン（SPLINE）図形は定義されていなかったため、ポリラインの作図後、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-0C422AA9-23DD-4650-AD66-68E9D7989E3F" rel="noopener" target="_blank">PEDIT[ポリライン編集]</a> コマンドで近似スプラインを得るしかありませんでした（下図では下側の図形）。一方、AuroCAD R13 以降、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-5E7D51E2-1595-4E0C-85F8-2D7CBD166A08" rel="noopener" target="_blank">SPLINE[スプライン]</a> コマンドが新設されて、「近似」ではないスプラインを作図出来るようになっています（下図では上側の図形）。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a525da200c-pi" style="display: inline;"><img alt="Splines" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a525da200c image-full img-responsive" src="/assets/image_217032.jpg" title="Splines" /></a></p>
<p>同じように AutoCAD R13 で導入されたな図形には楕円（ELLIPSE）や、AutoCAD R14 で導入された最適化ポリライン（LWPOLYLINE）もあります。図形のデータ構造という点では、AutoCAD R12 までは、旧ポリラインを使って様々な形状を表現していたことがわかります。</p>
<ul>
<li>
<p><a href="https://adndevblog.typepad.com/technology_perspective/2021/07/autocad-trivia-polyline.html" rel="noopener" target="_blank">AutoCAD 雑学：ポリライン</a></p>
</li>
</ul>
<p>次に「<strong>なぜ、R12 DWG でく R12 DXF なのか？</strong>」という疑問が出てくると思います。DXF は他の CAD 製品などとデータ交換する目的でオートデスクが用意した図面ファイル形式です。DWG が<a href="https://ja.wikipedia.org/wiki/%E3%83%90%E3%82%A4%E3%83%8A%E3%83%AA" rel="noopener" target="_blank">バイナリ</a>データを保存するのに対し、DXF はメモ帳でも開くことが可能な ASCII データを保存します。DWG と比べるとファイルサイズが大きくなりがちですが、内容を確認しやすいという利点があります。</p>
<p>ここで図面の運用を考えてみると、設計データがオートデスク製品だけで利用されるものではないことは明らかです。R12 DXF が残されているのは、「<strong>図形表現がオートデスク以外の CAD あるいは製品が理解しやすいシンプルな内容で、データ交換を主目的としているため</strong>」です。</p>
<p>今から十数年前、旧バージョンの図面ファイル形式の保存に、DWG、DXF とも一律 2 バージョン前までに統一する計画があり、実際、実施した AutoCAD バージョンがありましたが、CNC（工作機械のコンピュータ数値制御）との運用で問題が出ることがわかったため、機能を元に戻した経緯もあります。日本では jw_cad とのデータ交換で R12 DXF が利用されている方もいらっしゃると思います。</p>
<p>R12 DXF で保存することで問題が起こる場合もあります。図面内により新しいバージョンで導入された図形が含まれる場合です。それらは AutoCAD R12 で理解出来るシンプルなデータ構造に変換されてしまうので、明確な理由がない限り、R12 DXF での保存は避けたほうが無難です。R12 DXF で図面を保存した際の制限は、<a href="https://www.autodesk.co.jp/support/technical/article/caas/sfdcarticles/sfdcarticles/JPN/Save-as-R12-DXF-format-from-AutoCAD.html" rel="noopener" target="_blank">AutoCADからR12 DXFフォーマットとして保存 (autodesk.co.jp)</a> や <a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-C36C0AF2-6925-4E7F-BDAD-F57897D837B2" rel="noopener" target="_blank">概要 - 旧図面ファイル形式で図面を保存する</a>&#0160;をチェックしてみてください。</p>
<p>By Toshiaki Isezaki</p>
