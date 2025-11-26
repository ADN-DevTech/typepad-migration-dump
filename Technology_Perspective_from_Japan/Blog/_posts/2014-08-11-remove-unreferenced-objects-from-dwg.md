---
layout: "post"
title: "DWG 図面からの不要なオブジェクトの削除"
date: "2014-08-11 18:17:15"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/08/remove-unreferenced-objects-from-dwg.html "
typepad_basename: "remove-unreferenced-objects-from-dwg"
typepad_status: "Publish"
---

<p>作図されている図形の数に比べて図面ファイルのサイズが非常に大きい図面ファイルが存在している場合、図面に不要な情報が残っている可能性があります。ここでいう不要な情報とは、モデル空間やペーパー空間上の図形に参照されていない画層や寸法スタイル、ブロック定義などの非グラフィカル オブジェクトを指しています。</p>
<p>AutoCAD の図面ファイルでは、その構造上、このような不要なオブジェクトが発生しがちです。目に見える図形（グラフィカル オブジェクト）であれば、不要になった際に <a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-040C580C-63A2-4C98-9964-4573EF8C9514" rel="noopener" target="_blank">ERASE[削除] コマンド</a>で削除すれば問題ありません。ただし、削除した図形がブロック参照だった場合など、ブロックの定義情報を保持するブロック定義が図面に残り続けてしまいます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511f27b5e970c-pi" style="display: inline;"><img alt="Hard_pointer" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511f27b5e970c image-full img-responsive" src="/assets/image_709853.jpg" title="Hard_pointer" /></a></p>
<p>当然、ここでブロック定義も削除してしまえば、不要な情報を図面から消し去ることが出来ます。ここで注意が必要なのは、<strong>削除しようとするブロック定義が、どのブロック参照からも参照されていない</strong>、という保証です。もし、ブロック参照から参照されているブロック定義を削除してしまうと、<strong>図面破損</strong>の状態になっていまいます。この関係は、上図のような文字と文字スタイルの他にも、寸法と寸法スタイル、図形（グラフィカル オブジェクト）と画層、などのようにいろいろなパターンで図面内に存在します。詳細は、<a href="http://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener" target="_blank"><strong>以前のブログ記事</strong></a> を参照してみてください。</p>
<p>AutoCAD を操作する上では、安全に不要なオブジェクトを削除する方法を提供しなければなりません。その方法が、<a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-D68BA47B-A79D-4F58-9715-0569CC24BCEF" rel="noopener" target="_blank">PURGE[名前削除]</a> コマンドです。PURGE コマンドを実行すると、削除対象となるブロック定義などの非グラフィカル オブジェクトをダイアログボックス上に表示します。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511f25805970c-pi" style="display: inline;"><img alt="Purge" class="asset  asset-image at-xid-6a0167607c2431970b01a511f25805970c img-responsive" src="/assets/image_860738.jpg" style="width: 380px;" title="Purge" /></a></p>
<p>AutoCAD は、 PURGE コマンドが実行されると、他から一切参照されていないオブジェクトを検出して、削除しても問題のないオブジェクトの名前を表示しています。つまり、安全に不要な情報を削除することが可能なわけです。</p>
<p>ここで、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener" target="_blank">以前のブログ記事</a>&#0160;</strong>の内容を思い出してみてください。メモリ上に展開された図面の情報、図面データベース構造では、参照の方法やつながり（オーナーシップ接続）の組み合わせが 4 つ存在していたはずです。この中で、<strong>ハード</strong> 参照の説明に「PURGE コマンド[名前削除] コマンドでの削除からの保護される」の一文が説明されていたはずです。これは、先にご紹介した安全な削除を謳っていると理解できます。</p>
<p>さて、問題は&#0160;AutoCAD API を利用してカスタマイズをしている場合です。API によるオブジェクト操作では、参照しているオブジェクトの存在を確認せずに、対象オブジェクトを削除するプログラムを容易に作成することが出来てしまいます。図面構造を正しく理解していれば、そのようなコードは作成しないはずですが、万が一ということもあり得ます。オブジェクトを削除するプログラムを作成する際には、この部分は要注意です。</p>
<p>図面から一切の不要情報を削除したい場合には、PURGE[名前削除] コマンド相当のコードを作成することが出来ます。具体的な方法は、次のリンク先のドキュメントに記載されていますので、必要に応じて参照してみてください。</p>
<p style="padding-left: 30px;"><strong><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/dtsBrnQLJmt5x0amvuRFM.html" rel="noopener" target="_blank">ObjectARX：パージ機能の実装</a></strong></p>
<p style="padding-left: 30px;"><a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/ts/614mj6i0j1pFSUZ9uYrj4.html" rel="noopener" target="_blank"><strong>AutoCAD .NET API：パージ処理の実装</strong></a></p>
<p>PURGE コマンドには、 ここまでご紹介してきた不要な情報以外にも、必要のない図形を削除する機能が備わっています。先の [名前削除] ダイアログ下部にある&#0160;[長さがゼロのジオメトリおよび空白の文字オブジェクトを名前削除] です。</p>
<p>この機能は、Database.CountEmptyObjects メソッド（.NET API）、あるいは、AcDbDatabase::countEmptyObjects() メンバ関数（ObjectARX）で、不要なオブジェクトの数を取得したり、Database.EraseEmptyObjectsメソッド（.NET API）、あるい&#0160;AcDbDatabase::eraseEmptyObjects()&#0160;メンバ関数（ObjectARX）で、不要なオブジェクトを削除する方法が提供されています。</p>
<p>AutoCAD API を利用した開発時には、図面をクリーンに保つ手法を取り入れるといいかも知れません。</p>
<p>By Toshiaki Isezaki</p>
