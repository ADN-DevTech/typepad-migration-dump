---
layout: "post"
title: "AutoCAD Mechanical のアクティブ トランザクション"
date: "2023-02-27 00:06:52"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2023/02/active_transaction_on_acm.html "
typepad_basename: "active_transaction_on_acm"
typepad_status: "Publish"
---

<p>AutoCAD .NET API で開いた図面上で新規にオブジェクトを作成したり、既存のオブジェクトを編集したりする際、トランザクション マネージャを利用して対象オブジェクトをオープン（既存オブジェクト編集時のみ）、編集、コミットの手順を踏んで処理します。作成/編集されたオブジェクトは、コミットの時点で編集内容が図面上に反映されます。</p>
<div class="zd-indent">
<p dir="auto">一方、AutoCAD Mechanical 上で同様の処理をおこなう場合、AutoCAD Mechanical 固有の <strong>アクティブ トランザクション</strong>（Active Transaction）という機構が有効になり、標準のトランザクションをラップする動作をします。言い方を変えるなら、AutoCAD Mechanical 側で最上位となるトランザクションを持つため、アドインが対象オブジェクトを編集するために用意したトランザクションは、この時点でネストされた状態になってしまいます。</p>
<p dir="auto"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b75197731f200c-pi" style="display: inline;"><img alt="Differences" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b75197731f200c image-full img-responsive" src="/assets/image_77713.jpg" title="Differences" /></a></p>
<p dir="auto">この状態は、TransactionManager.TopTransaction プロパティで知ることが出来ます。</p>
<div>
<blockquote>
<div>&#0160; &#0160; &#0160; &#0160; [CommandMethod(&quot;MyCommand&quot;, CommandFlags.Modal)]</div>
<div>&#0160; &#0160; &#0160; &#0160; public void MyCommand() // This method can have any name</div>
<div>&#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; Database db = Application.DocumentManager.MdiActiveDocument.Database;</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; DEditor ed = Application.DocumentManager.MdiActiveDocument.Editor;</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; if (db.TransactionManager.TopTransaction != null)</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(&quot;\nTopTransaction あり!&quot;);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; else</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; {</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; ed.WriteMessage(&quot;\nTopTransaction なし&quot;);</div>
<div>&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; }</div>
<div>&#0160; &#0160; &#0160; &#0160; }</div>
</blockquote>
</div>
</div>
<div class="zd-indent">
<p dir="auto">上記 MyCommand コマンドを実装したアドイン アプリを AutoCAD 単体にロードして実行すると、次のように表示されます。</p>
<p dir="auto"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751976d27200c-pi" style="display: inline;"><img alt="Acad" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751976d27200c image-full img-responsive" src="/assets/image_435495.jpg" title="Acad" /></a></p>
<p dir="auto">同じコマンドを AutoCAD Mechanical で実行すると、AutoCAD 単体時とは異なるメッセージが表示されるはずです。</p>
<p dir="auto"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02b751731a90200b-pi" style="display: inline;"><img alt="Acm" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02b751731a90200b image-full img-responsive" src="/assets/image_476342.jpg" title="Acm" /></a></p>
<p dir="auto">アクティブ トランザクションは 、既に販売を停止した AutoCAD Mechanical Desktop から継承した内部機構です。常にオブジェクトのオープン状態を維持しておくことで、頻繁にオープン、コミット（クローズ）を繰り返しても、製品パフォーマンスが低下しないよう意図して用意されています。</p>
</div>
<div class="zd-indent">
<p dir="auto">オンラインヘルプ「<a href="https://help.autodesk.com/view/OARX/2023/JPN/?guid=GUID-8D8B9EE8-9D85-4C29-93A0-0BDC90F66EA7" rel="noopener" target="_blank">トランザクションをネストする(.NET)</a>」で説明されているように、トランザクションがネストしていると、一番外側のトランザクションが内側のトランザクションに影響を与えることになります。場合によっては図面に追加したオブジェクトや既存オブジェクトへの編集が、画面上に反映されない現象が<span style="text-decoration: underline;">発生してしまう場合</span>があります。</p>
</div>
<p>AutoCAD Mechanical 上でアクティブ トランザクション機構を無効化することは出来ないので、即座の画面更新が要求される場合には、次のような対応を考える必要が出てきます。（アドインの実装にも依るので、期待した効果が得られない場合も考えられます。）</p>
<ul>
<li>標準トランザクションの起動を <a href="https://help.autodesk.com/view/OARX/2023/ENU/?guid=OARX-ManagedRefGuide-Autodesk_AutoCAD_DatabaseServices_TransactionManager_StartTransaction" rel="noopener" target="_blank">TransactionManager.StartTransaction</a> メソッドから&#0160;<a href="https://help.autodesk.com/view/OARX/2023/ENU/?guid=OARX-ManagedRefGuide-Autodesk_AutoCAD_DatabaseServices_TransactionManager_StartOpenCloseTransaction" rel="noopener" target="_blank">TransactionManager.StartOpenCloseTransaction</a>&#0160;メソッドに置き換えてみる。<br />参考：<a href="https://help.autodesk.com/view/OARX/2023/JPN/?guid=GUID-50FD6118-B2D1-4313-A7D6-830794DFDEFA" rel="noopener" target="_blank">トランザクションを使用してオブジェクトにアクセスする、オブジェクトを作成する(.NET)</a></li>
<li>アドイン側のトランザクションを使用して、次のように、対象オブジェクトのグラフィックスを強制的に更新してみる。
<blockquote>doc.TransactionManager.QueueForGraphicsFlush();<br />doc.TransactionManager.EnableGraphicsFlush(true);<br />doc.TransactionManager.FlushGraphics();</blockquote>
</li>
<li>対象オブジェクトにグラフィックスの更新を伴うメソッドがあれば、編集結果がグラフィックスに反映されるか試してみる。<br />例）寸法の場合には <a href="https://help.autodesk.com/view/OARX/2023/ENU/?guid=OARX-ManagedRefGuide-Autodesk_AutoCAD_DatabaseServices_Dimension_RecomputeDimensionBlock__MarshalAsUnmanagedType_U1__bool" rel="noopener" target="_blank">Dimension.RecomputeDimensionBlock</a> メソッド、表の場合には <a href="https://help.autodesk.com/view/OARX/2023/ENU/?guid=OARX-ManagedRefGuide-Autodesk_AutoCAD_DatabaseServices_Table_RecomputeTableBlock__MarshalAsUnmanagedType_U1__bool" rel="noopener" target="_blank">Table.RecomputeTableBlock</a> メソッドなど</li>
</ul>
<p>もし、AutoCAD Mechanical 上でのみ、アドイン実装で予期しない表示上の<span style="text-decoration: underline;">問題が発生する場合には</span>、ぜひお試しください。</p>
<p>By Toshiaki Isezaki</p>
