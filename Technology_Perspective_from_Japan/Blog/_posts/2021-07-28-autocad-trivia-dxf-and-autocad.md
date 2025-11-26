---
layout: "post"
title: "AutoCAD 雑学：DXF と AutoCAD API"
date: "2021-07-28 00:08:16"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/07/autocad-trivia-dxf-and-autocad.html "
typepad_basename: "autocad-trivia-dxf-and-autocad"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdee3833a200c-pi" style="display: inline;"><img alt="Dxf" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdee3833a200c image-full img-responsive" src="/assets/image_574686.jpg" title="Dxf" /></a><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdee0faeb200c-pi" style="display: inline;"></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e111575b200b-pi" style="float: right;"><img alt="Dwg_icon" class="asset  asset-image at-xid-6a0167607c2431970b0282e111575b200b img-responsive" src="/assets/image_257868.jpg" style="width: 60px; margin: 0px 0px 5px 5px;" title="Dwg_icon" /></a>AutoCAD が作図した 2D 図面やモデリングした 3D モデルを保存する際、利用されるのは DWG ファイルです。DWG ファイル形式の仕様は非公開となっているので、外部のソフトウェアが TrustedDWG を保存するには、オートデスク製の <a href="https://adndevblog.typepad.com/technology_perspective/2014/09/about-realdwg.html" rel="noopener" target="_blank"><strong>RealDWG SDK</strong></a> を利用した開発が必要になっています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e111576c200b-pi" style="float: right;"><img alt="Dxf_icon" class="asset  asset-image at-xid-6a0167607c2431970b0282e111576c200b img-responsive" src="/assets/image_301844.jpg" style="width: 60px; margin: 0px 0px 5px 5px;" title="Dxf_icon" /></a>一方、その仕様を公開しているファイル形式も存在します。データ交換用に用意された <strong>D</strong>ata e<strong>X</strong>change <strong>F</strong>ormat、DXF™ ファイル形式です。DXF はオンラインヘルプで詳細な情報が<strong><a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-235B22E0-A567-4CF6-92D3-38A2306D73F3" rel="noopener" target="_blank">公開</a></strong>されていて、その仕様に沿ってファイルに<a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-0946C3A3-6739-4512-AEF6-1E6020109987" rel="noopener" target="_blank"><strong>エクポート（書き出し）</strong></a>して AutoCAD に読み込ませたり、AutoCAD が保存した DXF ファイルを独自に作成したソフトウェアに<a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-0946C3A3-6739-4512-AEF6-1E6020109987" rel="noopener" target="_blank"><strong>インポート（読み込み）</strong></a>させて、内容を把握することが出来るようになります。</p>
<p>AutoCAD からエクスポートされた DXF ファイルは、<a href="https://ja.wikipedia.org/wiki/ASCII" rel="noopener" target="_blank"><strong>ASCII</strong> </a>コードを使って書き込まれるので、メモ帳などのテキスト エディタで開くと、その内容を直接確認することも出来ます。DXF ファイルには図面全体の情報が書き出されているので、すべてを理解しようとすると少し大変ですが、図形の情報だけに特化すると、比較的把握は容易です。</p>
<p>例えば、中心点が 100,100,0 の半径 50 の円を DXF ファイルに書き出してテキストエディタで開いてみると、図形情報が書き込まれている ENTITIES セクションと呼ばれる部分に、次のような情報が見つかるはずです。</p>
<blockquote>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;：</p>
<p><span style="color: #0000ff;">0</span><br /><span style="color: #0000ff;">CIRCLE</span><br />5<br />2A2<br />330<br />1F<br />100<br />AcDbEntity<br />8<br />0<br />100<br />AcDbCircle<br /><span style="color: #0000ff;">10</span><br /><span style="color: #0000ff;">100.0</span><br /><span style="color: #0000ff;">20</span><br /><span style="color: #0000ff;">100.0</span><br /><span style="color: #0000ff;">30</span><br /><span style="color: #0000ff;">0.0</span><br /><span style="color: #0000ff;">40</span><br /><span style="color: #0000ff;">50.0</span></p>
<p>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;：</p>
</blockquote>
<p>&#0160;</p>
<p>このように、DXF ファイルは、情報の種類を示す番号である <strong>DXFグループコード</strong>(改行) と、実際の情報となる&#0160;<strong>DXF データ</strong>(改行) がペアになって情報を維持しています。円の DXF 仕様を<strong><a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-8663262B-222C-414D-B133-4A8506A27C18" rel="noopener" target="_blank">オンラインヘルプ</a></strong>で見てみると、下記のようになっていることがわかります。</p>
<p>※ DXF には DXF ファイル上の表記（DXF）とアプリ上の表記（APP）の 2 通りの<strong><a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-E50DB779-69AE-43C6-B004-85653A983AC0" rel="noopener" target="_blank">表記規約</a></strong>があります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1115112200b-pi" style="display: inline;"><img alt="Circle_dxf" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1115112200b image-full img-responsive" src="/assets/image_180390.jpg" title="Circle_dxf" /></a></p>
<p>図形には、色や画層、線種、といった共通する情報も持つので、こちらの<strong><a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-3610039E-27D1-4E23-B6D3-7E60B22BB5BD" rel="noopener" target="_blank">オンラインヘルプ</a></strong>の内容も併せると、次のようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e111516d200b-pi" style="display: inline;"><img alt="Common_dxf1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e111516d200b image-full img-responsive" src="/assets/image_379208.jpg" title="Common_dxf1" /></a></p>
<p style="text-align: center;">&lt;中略&gt;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0282e1115177200b-pi" style="display: inline;"><img alt="Common_dxf2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0282e1115177200b image-full img-responsive" src="/assets/image_793157.jpg" title="Common_dxf2" /></a></p>
<p style="text-align: center;">&lt;以下省略&gt;</p>
<p>先の DXF ファイルの抜粋を見てみると、<span style="color: #0000ff;">図形タイプ（DXF グループコード 0）が CIRCLE<span style="color: #111111;">、</span>中心点座標（DXF グループコード 10、20、30）が座標の X 値：100.0、Y 値：100.0、Z 値：0.0<span style="color: #111111;">、</span>半径（DXF グループコード 40）が 50.0</span> になっていることが読み取れます。</p>
<p>DXF ファイルを直接参照して内容を確認することはあまりないと思います。ただ、AutoCAD API を利用する開発者なら、もっと身近に DXF を感じる場面があります。もっとも分かり易いのは、AutoLISP でエンティティ リストを取得したり、操作したりする際です。</p>
<p>例えば、先の円を <strong><a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-9D4CF74D-8B8B-4D66-A952-564AFBA254E7" rel="noopener" target="_blank">(entsel)</a></strong> で選択して、<strong><a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-2DD1AF33-415C-4C1A-9631-DA958134C53A" rel="noopener" target="_blank">(car)</a></strong> でエンティティ名を抽出、<a href="https://help.autodesk.com/view/OARX/2022/JPN/?guid=GUID-12540DAE-C84B-4BDB-AEEC-DDFE5BE3C42A" rel="noopener" target="_blank"><strong>(entget)</strong></a> でエンティティリストを取得すると、下記のようになります。（APP 情報のため、DXF グループコード 10 で座標の XYZ 値すべてを保持）</p>
<blockquote>
<p>コマンド: (entget (car (entsel)))<br />オブジェクトを選択: ((-1 . &lt;図形名: 1dd1c0f84a0&gt;) <span style="color: #0000ff;">(0 . &quot;CIRCLE&quot;)</span> (330 . &lt;図形名: 1dd1c0fa1f0&gt;) (5 . &quot;2A2&quot;) (100 . &quot;AcDbEntity&quot;) (67 . 0) (410 . &quot;Model&quot;) (8 . &quot;0&quot;) (100 . &quot;AcDbCircle&quot;)<span style="color: #0000ff;"> (10 100.0 100.0 0.0)</span> <span style="color: #0000ff;">(40 . 50.0)</span> (210 0.0 0.0 1.0))</p>
</blockquote>
<p>AutoLISP では、DXF グループコードと対応する情報が、ドットペアと呼ばれる組み合わせで ( ) カッコ内に保持されていることがわかります。</p>
<p>他の AutoCAD API でも、DXF を強く意識する場面があります。ObjrctARX を使ってカスタム オブジェクトを定義する場合です。</p>
<p>カスタム オブジェクトとは、ObjectARX が使用する C++ 言語のクラス継承を用いて、ObjectARX アプリ独自に用意するオブジェクトです。カスタム オブジェクトは、グラフィカル オブジェクトと非グラフィカル オブジェクトの 2 種類に分けることができます が、いずれの場合も図面と一緒に保存することになるので、どの DXF グループコードを使って情報を書き込むか、開発者として決定、オブジェクトを定義する必要があります。</p>
<p>次のエンティティ リストは、ObjectARX で 3 頂点で構成される ASDKMYENTITY カスタム オブジェクトを定義、モデル空間に任意に作図した ASDKMYENTITY 図形のエンティティ リストを AutoLISP で取得した例です。ここでは、<span style="color: #0000ff;">DXF グループコード 10、11、12</span> のドットペアで 3 つの頂点座標が保持されていることがわかります。</p>
<blockquote>
<p>コマンド: (entget (car (entsel)))<br />オブジェクトを選択: ((-1 . &lt;図形名: 1dd1c0f8590&gt;) (0 . &quot;ASDKMYENTITY&quot;) (330 . &lt;図形名: 1dd1c0fa1f0&gt;) (5 . &quot;2B1&quot;) (100 . &quot;AcDbEntity&quot;) (67 . 0) (410 . &quot;Model&quot;) (8 . &quot;0&quot;) (100 . &quot;ASDKMyEntity&quot;) (90 . 1) <span style="color: #0000ff;">(<strong>10</strong> 87.9545 117.169 0.0) (<strong>11</strong> 75.4586 90.7618 0.0) (<strong>12</strong> 115.419 94.6643 0.0)</span>)</p>
</blockquote>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdee10545200c-pi" style="display: inline;"><img alt="Custom_entity" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdee10545200c image-full img-responsive" src="/assets/image_957082.jpg" title="Custom_entity" /></a></p>
<p>ちなみに、ObjectARX で ASDKMYENTITY カスタム オブジェクトを定義する際、DXF ファイルに書き込む情報はこんな感じで定義がおこなわれています。</p>
<blockquote>
<p>//- Dxf Filing protocol<br />Acad::ErrorStatus ASDKMyEntity::dxfOutFields (AcDbDxfFiler *pFiler) const {<br />&#0160; &#0160; assertReadEnabled () ;<br /><br />&#0160; &#0160; //----- Save parent class information first.<br />&#0160; &#0160; Acad::ErrorStatus es =AcDbEntity::dxfOutFields (pFiler) ;<br />&#0160; &#0160; if ( es != Acad::eOk )<br />&#0160; &#0160; &#0160; &#0160; return (es) ;<br /><br />&#0160; &#0160; es =pFiler-&gt;writeItem (AcDb::kDxfSubclass, _RXST(&quot;ASDKMyEntity&quot;)) ;&#0160;<br />&#0160; &#0160; if ( es != Acad::eOk )<br />&#0160; &#0160; &#0160; &#0160; return (es) ;<br /><br />&#0160; &#0160; //----- Object version number needs to be saved first<br />&#0160; &#0160; if ( (es =pFiler-&gt;writeUInt32 (kDxfInt32, ASDKMyEntity::kCurrentVersionNumber)) != Acad::eOk )<br />&#0160; &#0160; &#0160; &#0160; return (es) ;<br /><br />&#0160; &#0160; //----- Output params<br />&#0160; &#0160; //.....<br /><span style="color: #0000ff;">&#0160; &#0160; pFiler-&gt;writeItem(<strong>10</strong>, m_Vertex1);</span><br /><span style="color: #0000ff;">&#0160; &#0160; pFiler-&gt;writeItem(<strong>11</strong>, m_Vertex2);</span><br /><span style="color: #0000ff;">&#0160; &#0160; pFiler-&gt;writeItem(<strong>12</strong>, m_Vertex3);</span></p>
<p>&#0160; &#0160; return (pFiler-&gt;filerStatus ()) ;<br />}</p>
</blockquote>
<p>もちろん、カスタム オブジェクトを定義した ObjrctARX アプリは、DXF ファイルに書き出された情報を DXF グループコード毎に正しく読み込んで AutoCAD 上にカスタム オブジェクトを再現するのが一般的です。</p>
<p>ただし、カスタム オブジェクトの機能向上によって扱う情報が増減してしまうと、DXF グループコードも含めたバージョン管理がかなり煩雑になりがちです。こういった煩雑さに起因する問題を低減するために、一部の製品では、DXF ファイルの出力時にカスタム オブジェクトを分解して、AutoCAD の標準オブジェクトにしてからファイルを保存する仕様になっているものも存在します。</p>
<p>AutoCAD Architecture はその一例です。次のオンラインヘルプ記事を参照してみてください。</p>
<p style="padding-left: 40px;"><strong><a href="https://knowledge.autodesk.com/ja/support/autocad-architecture/learn-explore/caas/CloudHelp/cloudhelp/2020/JPN/AutoCAD-Architecture/files/GUID-E640D193-F6D9-497E-BEC2-D846D720B50B-htm.html" rel="noopener" target="_blank">図面を DXF ファイルに書き出すには</a></strong></p>
<p>DXF はファイル単位のみで考えるものではなく、AutoCAD API と密接に連携したデータ表現であることがわかります。ただ、AutoCAD .NET API や ActiveX オートメーションでは、API の<strong><a href="https://adndevblog.typepad.com/technology_perspective/2013/11/texhnologies-for-apis-on-autodesk-products.html" rel="noopener" target="_blank">登場時期</a></strong>や機能もあり、オブジェクト情報が隠ぺいされているので、DXF グループコードを意識することはないはずです。</p>
<p>By Toshiaki Isezaki</p>
