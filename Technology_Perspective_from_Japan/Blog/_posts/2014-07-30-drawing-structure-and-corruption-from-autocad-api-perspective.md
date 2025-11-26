---
layout: "post"
title: "AutoCAD API から見た図面構造と破損"
date: "2014-07-30 00:39:06"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html "
typepad_basename: "drawing-structure-and-corruption-from-autocad-api-perspective"
typepad_status: "Publish"
---

<p>今回は、AutoCAD API から見た図面ファイルと図面破損の状態をご紹介します。まず最初に、AutoCAD が図面を保存するために利用している DWG ファイルと DXF ファイルについてのおさらいです。</p>
<p><strong>図面ファイルの種類</strong></p>
<p>DWG ファイル形式は、オートデスクの AutoCAD が採用している図面を格納するためのファイル形式です。DWG ファイルは、<a href="http://ja.wikipedia.org/wiki/%E3%83%90%E3%82%A4%E3%83%8A%E3%83%AA" rel="noopener" target="_blank">バイナリ</a> という形式で保存されるため、Windows のメモ帳のような&#0160;<a href="http://ja.wikipedia.org/wiki/%E3%83%86%E3%82%AD%E3%82%B9%E3%83%88%E3%82%A8%E3%83%87%E3%82%A3%E3%82%BF" rel="noopener" target="_blank">テキスト エディタ</a>&#0160;では内容を把握することは出来ません。また、オートデスクは、DWG ファイル形式の仕様を公開していません。</p>
<p>一方、テキスト エディタで表示して内容を把握することが出来るのが、DXF ファイル形式です。他社製の CAD ソフトウェアとデータ交換することを目的に（他社が DXF ファイルを保存、読み込みできるようにするために）、DXF ファイル形式の仕様は<a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-235B22E0-A567-4CF6-92D3-38A2306D73F3" rel="noopener" target="_blank">オンランヘルプ上</a>で公開されています。</p>
<p><strong>図面ファイルとメモリ展開</strong></p>
<p>DWG ファイルでも、DXF ファイルでも、AutoCAD で図面を編集するには、AutoCAD 上に図面ファイル開いて表示する必要があります。この状態では、AutoCAD は図面ファイルから読み取った情報を、論理的な構造とともにメモリ上に展開して、AutoCAD 自身が操作しやすくしています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd3bc29e970b-pi" style="display: inline;"><img alt="File_from-to_database" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd3bc29e970b image-full img-responsive" src="/assets/image_205558.jpg" title="File_from-to_database" /></a></p>
<p>もちろん、この情報は設計者には見えませんし、把握する必要もありません。ただ、AutoCAD API を利用するプログラマは、メモリ上に展開された構造に注意を払う必要があります。たとえば、AutoCAD に次のような図面が表示されていると仮定します。ここでは、モデル空間に円が2つ、線分は3つ、円弧が2つ、それぞれ作図されています。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511eb85fb970c-pi" style="display: inline;"><img alt="Autocad" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511eb85fb970c image-full img-responsive" src="/assets/image_714530.jpg" title="Autocad" /></a></p>
<p>この時、AutoCAD は、メモリ上に次のような構造で図面を展開して、ユーザによる編集に備えます。 また、AutoCAD API を利用する開発者が新たな図形をモデル空間に作図する場合には、この論理構造を利用するプログラムを作成します（&quot;*Model_Space&quot; の名前が付いた BlockTableRecord にオブジェクトを紐付ける）。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511eb86e1970c-pi" style="display: inline;"><img alt="Database" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511eb86e1970c image-full img-responsive" src="/assets/image_852993.jpg" title="Database" /></a></p>
<p>ここで注目していただきたいのは、矩形で示されるオブジェクト同士が <span style="color: #0000ff;"><strong>青い線</strong></span> で結ばれている、という部分です。この&#0160;<span style="color: #0000ff;"><strong>つながり</strong></span>&#0160;によって、メモリ上の図面情報が具体的に論理構造をなしています。</p>
<p>図面ファイルには様々な情報が格納されています。メモリにも同等の情報が <span style="color: #111111;"><strong>つながり</strong></span> を持って展開されることになります。AutoCAD API では、この <span style="color: #111111;"><strong>つながり</strong></span> を <span style="color: #0000ff;"><strong>オーナーシップ接続</strong></span> と呼んでいます。</p>
<p><strong>オーナーシップ接続の原則と種類</strong></p>
<p>AutoCAD オブジェクト図面ファイルに含まれる構成要素には、このように実際に図形として画面に表示される <strong>グラフィカル オブジェクト</strong> と、直接画面には表示されないものの、図形の特性を表現する上でかかせいない、目に見えない <strong>非グラフィカル オブジェクト</strong> と呼ばれる構成要素が存在します。</p>
<p>グラフィカル オブジェクトは、線分、円、円弧、ポリライン、楕円などで、非グラフィカル オブジェクトは、画層、線種、寸法スタイル、文字スタイルなどが代表的です。これら 2 種類のオブジェクトは、メモリ上で密接に連携しています。当然、これらオブジェクトの間には、<strong>オーナーシップ接続</strong> の関係が維持されています。</p>
<p>オーナーシップ接続を別の側面から考えると、オブジェクト間の親子関係ととらえることが出来ます。そして、AutoCAD が扱うオブジェクトには、かならず、1つの親オブジェクトが存在します。親オブジェクトが存在しないのは、メモリ上の図面情報を包括する図面データベース オブジェクト（Database）のみです。</p>
<p>先に紹介したモデル空間に作図されている図形の例では、円（Circle）や線分（Line）、円弧（Arc）が、子オブジェクトとして親オブジェクトであるモデル空間 （BlockTableRecord）とオーナーシップ接続を維持しています。また、図面内に含まれる各画層テーブル レコード（LayerTableRecord）は、画層テーブル（LayerTable）を親オブジェクトとしています。親オブジェクトを複数持つオブジェクトは存在しません。</p>
<p>それでは、&quot;0&quot; 画層を参照する線分は、そのような関連性を持っているのか疑問が生じます。もちろん、線分オブジェクト（Line）の親オブジェクトが &quot;0&quot; 画層オブジェクト（LayerTableRecord）ではありません。AutoCAD では、線分を含むグラフィカル オブジェクトの親オブジェクトは、BlockTableRecord 以外許容されていないためです。これは、AutoCAD が図面を扱う上での1つの仕様です。</p>
<p>そこで、オーナーシップ接続には、オブジェクト間の関係に応じて、ハードとソフト、オーナーシップ参照とポインタ参照、の組み合わせが適用されています。簡単にまとめると、それぞれの位置づけは明確です。</p>
<p style="padding-left: 30px;"><strong>オーナーシップ接続の種類と用語</strong></p>
<p style="padding-left: 30px;"><span style="color: #0000ff;"><strong>ハード</strong></span></p>
<ul>
<ul>
<li>参照するオブジェクトにとって参照オブジェクトが必須</li>
<li>PURGE コマンド[名前削除] コマンドでの削除からの保護される</li>
</ul>
</ul>
<p style="padding-left: 30px;"><span style="color: #0000ff;"><strong>ソフト</strong></span></p>
<div>
<ul>
<ul>
<li>参照するオブジェクトにとって参照オブジェクトが必須ではない</li>
<li>PURGE コマンド[名前削除] コマンドからの保護がない</li>
</ul>
</ul>
</div>
<p style="padding-left: 30px;"><span style="color: #0000ff;"><strong>オーナーシップ参照</strong></span></p>
<div>
<ul>
<ul>
<li>相互参照</li>
</ul>
</ul>
</div>
<p style="padding-left: 30px;"><span style="color: #0000ff;"><strong>ポインタ参照</strong></span></p>
<div>
<ul>
<ul>
<li>一方向参照</li>
</ul>
</ul>
</div>
<p>次に、メモリ上に展開された図面構造から、それぞれのオーナーシップ接続の例を示します。</p>
<p style="padding-left: 30px;"><strong>ハード オーナーシップ参照の例</strong></p>
<p style="padding-left: 30px;">図面内に必ず存在しなければならない相互参照の関係です。</p>
<ul>
<ul>
<li>図面データベースとブロック テーブル</li>
<li>ブロック テーブルと &quot;モデル空間&quot; ブロック テーブル レコードの関係</li>
<li>ブロック テーブルと &quot;モデル空間&quot; ブロック テーブル レコードの関係</li>
</ul>
</ul>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd3bcc67970b-pi" style="display: inline;"><img alt="Hard_ownership" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd3bcc67970b img-responsive" src="/assets/image_78622.jpg" style="width: 450px;" title="Hard_ownership" /></a></p>
<p style="padding-left: 30px;"><strong>ソフト オーナーシップ参照の例</strong></p>
<p style="padding-left: 30px;">ユーザ定義によって発生する相互参照の関係です。</p>
<ul>
<ul>
<li>ブロック テーブルとユーザ定義のブロック定義1 や ブロック定義2 の関係</li>
<li>&quot;モデル空間&quot; ブロック テーブル レコードと線分、円弧、円などのグラフィカル オブジェクトの関係</li>
</ul>
</ul>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511eb9054970c-pi" style="display: inline;"><img alt="Soft_ownership" class="asset  asset-image at-xid-6a0167607c2431970b01a511eb9054970c img-responsive" src="/assets/image_250518.jpg" style="width: 450px;" title="Soft_ownership" /></a></p>
<p style="padding-left: 30px;"><strong>ハード ポインタ参照の例</strong></p>
<p style="padding-left: 30px;">参照元から見て、図面内に必ず存在しなければならない一方向相互参照の関係です。&#0160;</p>
<ul>
<ul>
<li>ユーザ定義のブロック定義を参照するモデル空間に挿入されているブロック参照との関係</li>
<li>STANDARD 文字スタイルを参照するモデル空間に作図されている文字との関係</li>
</ul>
</ul>
<p style="padding-left: 30px; text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd3bcd44970b-pi" style="display: inline;"><img alt="Hard_pointer" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd3bcd44970b img-responsive" src="/assets/image_627257.jpg" style="width: 450px;" title="Hard_pointer" /></a></p>
<p style="padding-left: 30px;"><strong>ソフト ポインタ参照の例</strong></p>
<p style="padding-left: 30px;">ユーザ定義にとって発生する、参照元から見て一方向参照の関係です。&#0160;</p>
<ul>
<ul>
<li>オブジェクトに付加された拡張エンティティ データとの関係</li>
<li>文字に付加された不変リアクタとの関係</li>
</ul>
</ul>
<p style="text-align: center;">&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511eb913d970c-pi" style="display: inline;"><img alt="Soft_poiner" class="asset  asset-image at-xid-6a0167607c2431970b01a511eb913d970c img-responsive" src="/assets/image_103070.jpg" style="width: 450px;" title="Soft_poiner" /></a></p>
<p><strong>図面破損の状態</strong>&#0160;</p>
<p>少し難しい内容になっていまいましたが、ここで紹介したオーナーシップ接続が崩れてしまった図面が、破損した図面と識別されることになります。破損した図面は、AutoCAD が持つ <strong><a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-62DDB935-61B1-49DA-8238-3EF1CC45259B" rel="noopener" target="_blank">AUDIT[監査] コマンド</a>&#0160;</strong>や&#0160;<strong><a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-78F5B1AB-583F-410F-85DA-6D03768832C8" rel="noopener" target="_blank">RECOVER[修復] コマンド</a>&#0160;</strong>で破損個所の削除を含む修復することが出来る可能性があります。もし、AutoCAD API でこれらの処理を自動化したい場合には、AutoCAD 2015 から API による図面の監査や修復を利用できるようになっています。</p>
<p>詳細は、下記の Autodesk Knowledge Network 記事を参照してみてください。&#0160;</p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01bb08b42d6a970d img-responsive"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/HMSn7mT0kA2fdCLqAjmpz.html" rel="noopener" target="_blank">図面監査の自動化は可能か？</a></span></strong></p>
<p style="padding-left: 30px;"><strong><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c80f6dc0970b img-responsive"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/3iE2JoirGAtxQ6sVRcmWgw.html" rel="noopener" target="_blank"> 図面修復を実装するには？</a></span></strong></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
