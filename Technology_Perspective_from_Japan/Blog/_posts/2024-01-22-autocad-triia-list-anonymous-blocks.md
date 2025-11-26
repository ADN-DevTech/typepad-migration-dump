---
layout: "post"
title: "AutoCAD 雑学：匿名ブロックの把握"
date: "2024-01-22 00:03:16"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/01/autocad-triia-list_anonymous_blocks.html "
typepad_basename: "autocad-triia-list_anonymous_blocks"
typepad_status: "Publish"
---

<p><a href="https://adndevblog.typepad.com/technology_perspective/2024/01/autocad-trivia-ownership-and-cleanup.html" rel="noopener" target="_blank">AutoCAD 雑学：図面要素のつながりと図面クリーンアップ</a> でご紹介しましたが、AutoCAD で図面に寸法を作図する際、匿名ブロック（名前のないブロック）が自動的に定義（ブロック定義）されます。そして、モデル空間などの作図領域に作図されるのは、その匿名ブロック定義を参照するブロック参照です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a70d2c200b-pi" style="display: inline;"><img alt="Anonymous_block" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a70d2c200b image-full img-responsive" src="/assets/image_680371.jpg" title="Anonymous_block" /></a></p>
<p>匿名ブロックは、<strong>*</strong>（アスタリスク）で始まる名前（ブロック名）を持ち、寸法以外にも、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-367470A6-6E6E-4181-9E53-9B0EC88F50DC" rel="noopener" target="_blank">TABLE</a> コマンドで表を作図したり、挿入後のダイナミックブロックを定義パラメーターに沿って変更したりした際などにも定義されます。</p>
<p>ユーザーが定義したブロック（ユーザー定義ブロック）と異なり、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-E3413A03-8EEF-44D5-B5C7-8B345A9257FE" rel="noopener" target="_blank">[ブロック] パレット</a>（<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-B840AB4A-91E2-4FEC-900A-33E40D1E1925" rel="noopener" target="_blank">INSERT</a> コマンド）や <a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-03B61417-F040-4EB0-AFEA-B229AD303D91" rel="noopener" target="_blank">[ブロック定義] ダイアログ</a>（<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-B03434BE-0F68-4E31-BA8D-640EEC1D7FC9" rel="noopener" target="_blank">BLOCK</a> コマンド）、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-A0477C1A-493B-4CEC-8B42-171015D47EA4" rel="noopener" target="_blank">[ブロック定義を編集] ダイアログ</a>（<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-D57D4195-72FC-4FA5-B9F4-E021291D808C" rel="noopener" target="_blank">BEDIT</a> コマンド）には匿名ブロックは表示されません。</p>
<p>もし、図面内の匿名ブロックを把握したい場合には、次の一文をコマンド プロンプトに入力して実行してみてください。</p>
<div>
<blockquote>
<div>(setq doc (vla-get-ActiveDocument (vlax-get-acad-object)))(vlax-for block (vla-get-Blocks doc)(print (vla-get-Name block)))(princ)</div>
</blockquote>
</div>
<p>寸法を作図した図面で、この「おまじない」を入力・実行すると、匿名ブロック名の一覧が表示されるはずです。寸法の場合、*Dxx（xx は数字）のブロック名が表示されるはずです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a70b50200b-pi" style="display: inline;"><img alt="Dimensions0" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a70b50200b image-full img-responsive" src="/assets/image_649751.jpg" title="Dimensions0" /></a></p>
<p>先の「おまじない」は AutoLISP 式で、いわゆる AutoCAD API の 1 つです。細かな説明は割愛しますが、次のようなカスタム コマンドの内容を 1 行で実行するものです。</p>
<blockquote>
<div>(defun C:List-Blocks()</div>
<div><strong>&#0160; (setq doc (vla-get-ActiveDocument (vlax-get-acad-object)))</strong></div>
<div><strong>&#0160; (vlax-for block (vla-get-Blocks doc) &#0160; &#0160;</strong></div>
<div><strong>&#0160; &#0160; (print (vla-get-Name block)) &#0160; &#0160;</strong></div>
<div><strong>&#0160; )</strong></div>
<div><strong>&#0160; (princ)</strong></div>
<div>)</div>
</blockquote>
<p>もし、<a href="https://www.autodesk.co.jp/support/technical/article/caas/tsarticles/tsarticles/JPN/ts/3kxk0RyvfWTfSfAIrcmsLQ.html" rel="noopener" target="_blank">VBA</a> をインストールした AutoCAD であれば、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-3B4AAAAB-AFF3-45F6-9B1D-B2AEB7DA658C" rel="noopener" target="_blank">VBASTMT</a> コマンドを利用した次の「おまじない」を入力・実行して匿名ブロックを一覧表示することも出来ます。</p>
<div>
<blockquote>
<div>VBASTMT For Each block In ThisDrawing.Blocks: ThisDrawing.Utility.Prompt vbCrLf &amp; block.Name &amp; vbCrLf: Next</div>
</blockquote>
</div>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a30731200c-pi" style="display: inline;"><img alt="Dimensions" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a30731200c image-full img-responsive" src="/assets/image_450810.jpg" title="Dimensions" /></a></p>
<p>この「おまじない」は、次のような VBA マクロの中身を 1 行で実行するものです。</p>
<div>
<blockquote>
<div>Public Sub List-Blocks()</div>
<div><strong>&#0160; For Each block In ThisDrawing.Blocks</strong></div>
<div><strong>&#0160; &#0160; ThisDrawing.Utility.Prompt vbCrLf &amp; block.Name &amp; vbCrLf</strong></div>
<div><strong>&#0160; Next</strong></div>
<div>End Sub</div>
</blockquote>
</div>
<p>匿名ブロック定義は、ブロック参照（ここでは寸法）から参照されている間、図面から削除することは出来ません。ただし、ブロック参照を削除、また、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-E98BCEF4-DED6-48A6-87EB-10FE87188083" rel="noopener" target="_blank">EXPLODE</a> コマンドで分解して匿名ブロック定義が孤立状態になると、図面から削除出来るようになります。この場合、一旦、図面を保存して開き直すと、匿名ブロックが自動的に削除されます。</p>
<p>もし、同じ図面セッションで匿名ブロックを削除したい場合には、<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-D68BA47B-A79D-4F58-9715-0569CC24BCEF" rel="noopener" target="_blank">PURGE</a> コマンドで削除することも可能です。<br /><a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-C6B62BBD-C363-4DC7-AF43-036A7B287473" rel="noopener" target="_blank">[名前削除] ダイアログ</a>の「名前削除が可能な項目(<span style="text-decoration: underline;">U</span>)」には、孤立状態になった匿名ブロックが表示されるようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a321e4200c-pi" style="display: inline;"><img alt="Purge" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a321e4200c image-full img-responsive" src="/assets/image_312319.jpg" title="Purge" /></a></p>
<p>一方、決して削除出来ない特別な匿名ブロックも存在します。先の「おまじない」で表示されていた *Model_Space（モデル空間）、*Paper_Space（ペーパー空間）です。これらは図面にとって作図領域を提供する必須な「ブロック定義」であるため、PURGE コマンドでも削除することが出来ない仕組みになっています。</p>
<p>By Toshiaki Isezaki</p>
