---
layout: "post"
title: "AutoCAD API で扱うオブジェクトの実際とオブジェクト識別子"
date: "2013-10-23 00:31:38"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/10/object-identifer-of-dautocad-api.html "
typepad_basename: "object-identifer-of-dautocad-api"
typepad_status: "Publish"
---

<p>ご存じのように、AutoCAD で扱う図面は、DWG ファイル形式や DXF ファイル形式でハードディスクなどの保存媒体に保存されます。 また、AutoCAD で図面を編集する際には、AutoCADの作図ウィンドウ上に図面を開いたり、新規に図面を作成したりして、図面上に図形（ここでは オブジェクトと表現）を作図、編集していきます。このとき、編集中の情報は、逐次、図面ファイルである DWG ファイルや DXF ファイルに保存されているわけではありません。図面編集中に AutoCAD が情報を更新しているのは、メモリ上に展開された図面イメージとなる 図面データベース と呼ばれるものです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff8d4040970b-pi" style="display: inline;"><img alt="DrawingDatabase" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff8d4040970b image-full" src="/assets/image_556824.jpg" title="DrawingDatabase" /></a><br />図面データベースには、DWG ファイルや DXF ファイルに含まれる多くのオブジェクトが展開されています。AutoCAD API を学習する際には、図面ウィンドウ上で実際に目に見える図形と目に見えない情報の2つを区別して説明することが多くあります、前者が <strong>グラフィカル オブジェクト</strong>、後者が <strong>非グラフィカル オブジェクト</strong> です。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff8d42c3970b-pi" style="display: inline;"><img alt="ObjectType" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff8d42c3970b image-full" src="/assets/image_113483.jpg" title="ObjectType" /></a></p>
<p>メモリ上の図面データベースに展開されるグラフィカル オブジェクトや非グラフィカル オブジェクトは、データベースの呼び名が示すとおり、然るべきオーナー オブジェクトに関連付けられて展開されています。たとえば、AutoCAD .NET API の場合、新規図面に必ず設定されている &quot;0&quot; 画層や、ユーザが作成した &quot;寸法&quot; 画層は個々に LayerTableRecord オブジェクトとして表現されて、オーナー オブジェクトとなる LayerTable オブジェクトに関連付けられています。同様に、モデル空間に作図した円や線分、円弧といったグラフィカル オブジェクトは、モデル空間を表現する BloctTableRecord オブジェクトに関連付けられています。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff8dfede970d-pi" style="display: inline;"><img alt="ObjectHierarchyOnMemory" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff8dfede970d image-full" src="/assets/image_269616.jpg" title="ObjectHierarchyOnMemory" /></a><br />これらのオブジェクトには一定のルールも存在します。例えば、AutoCAD の仕様によって、図面内に &quot;寸法&quot; の名前を付けて複数の画層を持つことはできません。一方、モデル空間やペーパー空間などの作図領域には、円や線分、円弧など、特に名前の付いていないグラフィカル オブジェクトを多数作図することが可能です。</p>
<p>AutoCAD API で特定のオブジェクトを操作しょうとする場合には、まず、対象となるオブジェクトを特定しなければなりません。ここまでの説明では、オブジェクトには名前の付いていないものも存在していたはずです。そこで、図面データベース上のオブジェクトを一意に特定するために、AutoCAD のすべてのオブジェクトには&#0160;<strong><a href="http://ja.wikipedia.org/wiki/%E8%AD%98%E5%88%A5%E5%AD%90" rel="noopener" target="_blank">識別子</a></strong>&#0160;が用意されています。図面の中のオブジェクトは、必ず一意な識別子を持っていて、図面内の他のオブジェクトと重複しないようになっています。</p>
<p>AutoCAD には、オブジェクトを特定するために複数の識別子が用意されています。それが、エンティティ名、ハンドル番号、オブジェクト ID の3つです。これらは、AutoCAD API の歴史とも深く関係しています。次の表は、それぞれの違いを明確するものです。</p>
<table border="0" cellspacing="3" id="table1" width="100%">
<tbody>
<tr>
<td align="left" bgcolor="#ccffff" valign="top" width="220"><span style="font-size: 10pt;"><strong><span style="font-family: Arial;">エンティティ名</span></strong></span></td>
<td align="left" bgcolor="#ccffcc" valign="top" width="1000"><span style="font-family: Arial; font-size: 10pt;">AutoCAD API で最も古いくから使われてきたオブジェクト(エンティティ) 識別子です。現在でも、AutoLISP や ObjectARX で頻繁に使用します。エンティティ名は、図面データベース内で重複することはありませんが、AutoCAD のセッション毎に更新されてしまうので、同じ図面のまったく同じ図形であっても、図面を開くたびに異なるエンティティ名が割り当てられることになります。DXF&#0160; グループ コードでは -1 で表現されています。</span></td>
</tr>
<tr>
<td align="left" bgcolor="#ccffff" valign="top" width="220"><span style="font-size: 10pt;"><strong><span style="font-family: Arial;">ハンドル番号</span></strong></span></td>
<td align="left" bgcolor="#ccffcc" valign="top" width="1000"><span style="font-family: Arial; font-size: 10pt;">エンティティ名とは異なり、AutoCAD のセッションが変わっても、オブジェクトに割り当てられるハンドル番号は常に同じになります。このため、ある図面内の特定のエンティティと、データベース上のレコードとを関連付けて使用したりするようなことができます。ただし、ハンドル番号は 1 つの図面内で重複することはありませんが、複数の図面内では 重複してしまう可能性があります。現在の AutoCAD のように同時に複数の図面を開くことができる環境では注意が必要です。DXF グループ コードでは 5 で表現されています。</span></td>
</tr>
<tr>
<td align="left" bgcolor="#ccffff" valign="top" width="220"><span style="font-size: 10pt;"><strong><span style="font-family: Arial;">オブジェクト ID</span></strong></span></td>
<td align="left" bgcolor="#ccffcc" valign="top" width="1000"><span style="font-family: Arial; font-size: 10pt;">エンティティ名と同じようにセッション毎に更新されてしまいますが、オブジェクト ID は複数の図面を扱うセッションで重複することはありません。この識別子は、ObjectARX と AutoCAD .NET API で頻繁に使用する識別子で、.NET API では ObjectId クラスで定義されています。DXF コードとして現れることはありません。</span></td>
</tr>
</tbody>
</table>
<p>発売当初の AutoCAD は、Windows ではなく、MS-DOS で動作するように作られていて、当時の AutoCAD には、同時に複数の図面を表示する能力はありませんでした。AutoCAD が同時に複数の図面を開いて表示できるようになったのは、AutoCAD 2000 からなのです。AutoCAD R14 までの AutoCAD では、エンティティ名やハンドル番号でも、同時に複数の図面データベースを意識してオブジェクト識別する必要性はありませんが、AutoCAD 2000 以降は逆に、その必要が発生したわけです。そこで考え出されたのが、オブジェクト ID です。（実際には、ObjectARX が登場した AutoCAD R13J 時に、将来を見越して複数の図面ウィンドウを搭載できるよう、オブジェクト ID が考案されて実装されていました）。</p>
<p>比較的最近のバージョンから AutoCAD をカスタマイズされる方には不可解な仕様もあるのかと思います。AutoCAD は非常に歴史の古いソフトウェアであるため、オブジェクト識別子のように、歴史的背景の影響を強く受けているのも事実なのです。</p>
<p>By Toshiaki Isezaki&#0160;</p>
