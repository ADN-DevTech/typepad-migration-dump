---
layout: "post"
title: "AutoCAD 雑学：図面要素のつながりと図面クリーンアップ"
date: "2024-01-10 00:01:49"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2024/01/autocad-trivia-ownership-and-cleanup.html "
typepad_basename: "autocad-trivia-ownership-and-cleanup"
typepad_status: "Publish"
---

<p>AutoCAD で図面を編集していると、削除出来るはずなのに削除出来ない要素（オブジェクト）に遭遇するときがあります。結論から言ってしまうと、これは、図面破損を防ぐ AutoCAD の仕組みです。</p>
<p><strong>それは、どのような場面でしょうか？</strong></p>
<p><strong>どうすれば、削除出来るようになるでしょう？</strong></p>
<p>少し長くなりますが、順を追ってご紹介してみたいと思います。</p>
<hr />
<p>図面を編集する際、コマンド操作毎に更新されるのは、ファイルとして保存されている DWG ファイルではなく、「図面データベース」と呼ばれるメモリ上に展開された状態の図面です。言い換えれば、「図面」はファイルとしてアーカイブされている状態と、編集時にメモリに展開される図面データベースの状態の 2 つの形態が存在することになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a67611200b-pi" style="display: inline;"><img alt="Drawing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a67611200b image-full img-responsive" src="/assets/image_113559.jpg" title="Drawing" /></a></p>
<p>この「図面」、さまざまな要素（オブジェクト）が含まれています。そして、個々の要素は他の要素と一方向や双方向に参照しあって正しい図面情報が成り立っています。</p>
<p>一方向参照の例では、図形を挙げることが出来ます。例えば、LINE コマンドで線分を作図すると、線分は暗黙のうちに現在の「画層」や「線種」といった要素（情報）を参照することになります。</p>
<p>双方向参照の例では、AutoCAD の図面にもともと存在することが前提になっている非図形情報を挙げることが出来ます。例えば、作図する場所を提供している「モデル空間」は AutoCAD 図面には必須の存在であり、 AutoCAD 図面にとっても「モデル空間」がないと作図が出来ないため、その存在が必須です。イメージしやすい要素では、「寸法スタイル」と &quot;STANDARD&quot; と名前のついた「寸法スタイル」の関係も相互に必須な参照関係にあたります。</p>
<p>つまり、図面に何かしらの要素を追加すると、要素間に「つながり」が生まれる、ということになります。「つながり」の参照先の要素が無くなってしまうと、正しい図面表現が出来ず、「破損」していると判定されてしまいます。</p>
<ul>
<li>この「つながり」を「オーナーシップ接続」と呼ぶことがあります。アドイン開発者寄りの内容になってしまいますが、オーナシップ接続については、<a href="https://nam11.safelinks.protection.outlook.com/?url=https%3A%2F%2Fadndevblog.typepad.com%2Ftechnology_perspective%2F2014%2F07%2Fdrawing-structure-and-corruption-from-autocad-api-perspective.html&amp;data=05%7C01%7Ctoshiaki.isezaki%40autodesk.com%7C8bdf5a391fb44e234be308dbf7107b97%7C67bff79e7f914433a8e5c9252d2ddc1d%7C0%7C0%7C638375422402525236%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&amp;sdata=YfkP6HPXM%2FwVz6wbbatCgv0C25e94Vdzkj6kUuQiUoI%3D&amp;reserved=0" rel="noopener" target="_blank"> AutoCAD API から見た図面構造と破損</a>&#0160;の記事でご紹介したこともあります。</li>
</ul>
<p>図面に追加した要素は、他の要素から参照されていない孤立した状態のときのみ、図面から削除することが出来ます。例えば、独自に追加した &quot;スタイル1&quot; 文字スタイルは、その文字スタイルを参照する TEXT、あるいは MTEXT 図形が図面にない場合、STYLE コマンドで表示可能な [文字スタイル管理] ダイアログの [削除] ボタンで削除することが出来ます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a6e207200d-pi" style="display: inline;"><img alt="Text_style_manager1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a6e207200d image-full img-responsive" src="/assets/image_973040.jpg" title="Text_style_manager1" /></a></p>
<p>もし、作図済の TEXT、あるいは MTEXT 図形から &quot;スタイル1&quot; 文字スタイルが参照されていると、エラーになって削除出来ません。逆に、作図済の TEXT、あるいは MTEXT 図形が、&quot;スタイル1&quot; 文字スタイルへの参照を止めると（他の文字スタイルを参照すると）、再び [削除] ボタンで削除することが出来るようになります。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a688ed200b-pi" style="display: inline;"><img alt="Text_style_manager2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a688ed200b image-full img-responsive" src="/assets/image_317927.jpg" title="Text_style_manager2" /></a></p>
<p>なお、&quot;スタイル1&quot; 文字スタイルが現在の文字スタイルに設定されている場合は、[削除] ボタンはマスクされてクリック出来ません。この削除の可否は、PURGE コマンドの [名前削除] ダイアログでも同様です。</p>
<p>まとめると、 &quot;スタイル1&quot; 文字スタイルの例では、作図済の TEXT、あるいは MTEXT 図形が &quot;スタイル1&quot; 文字スタイルを参照しなくなったり、TEXT、あるいは MTEXT 図形自体が削除されてしまうと、&quot;スタイル1&quot; 文字スタイル要素が孤立した状態になるので、図面から削除出来るようになります。</p>
<hr />
<p>ただし、図面の編集中には、前述の「つながり」が原因で要素を削除出来なくなるケースが存在します。ブロック定義の中の図形がその要素を参照している場合です。</p>
<p>もし、ご自身で定義したブロック定義であれば、ブロックエディタで参照を止めて参照先の要素が孤立状態にすれば、その要素を削除出来るようになります。</p>
<p>問題は、寸法図形が &quot;スタイル1&quot; 文字スタイルを参照する場合です。厳密には、寸法図形が参照する寸法スタイルが &quot;スタイル1&quot; 文字スタイルを参照している場合です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a29d22200c-pi" style="display: inline;"><img alt="Dimesion_style" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a29d22200c image-full img-responsive" src="/assets/image_681020.jpg" title="Dimesion_style" /></a></p>
<p>このケースでは、もちろん &quot;スタイル1&quot; 文字スタイルを削除しようとするとエラーになってしまいます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a29cf2200c-pi" style="display: inline;"><img alt="Text_style_manager3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a29cf2200c image-full img-responsive" src="/assets/image_349825.jpg" title="Text_style_manager3" /></a></p>
<p><strong>それでは、寸法図形を ERASE コマンドで削除して、かつ、寸法図形が参照していた寸法スタイルから &quot;スタイル1&quot; 文字スタイルへの参照を止めた場合はどうでしょう？</strong></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a29d0f200c-pi" style="display: inline;"><img alt="Dimension_style3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a29d0f200c image-full img-responsive" src="/assets/image_429673.jpg" title="Dimension_style3" /></a></p>
<p>ここまでの内容から考えると、参照されなくなった要素（ここでは &quot;スタイル1&quot; 文字スタイル）は孤立した状態になるので、このタイミングで削除が可能なはずです。</p>
<p><strong>そこで実際に、&quot;スタイル1&quot; 文字スタイルを削除しようとすると、前述と同じ「文字スタイルは現在使用中です。削除できません。」エラーが発生して削除が出来ません。なぜでしょう？</strong></p>
<p>ご存じの方もいるものと思いますが、寸法図形を作図すると、暗黙のうちにブロック定義が作成されます。一般的な長さ寸法の場合、寸法図形は、寸法線（<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-E8C1190C-A26C-484C-ADDD-DDF81666F69F" rel="noopener" target="_blank">LINE</a>）、寸法補助線（<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-E8C1190C-A26C-484C-ADDD-DDF81666F69F" rel="noopener" target="_blank">LINE</a>）、矢印（<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-0998E0EE-7829-4AA4-9282-4FC703F9B1F4" rel="noopener" target="_blank">SOLID</a>）、寸法値（<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-E6BCE05D-B9E3-4875-BBBC-29134EA6FD51" rel="noopener" target="_blank">MTEXT</a>）、寸法計測点（<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-3F5861A1-9A63-42A6-8F12-3395771BAA6D" rel="noopener" target="_blank">POINT</a>）で構成されます、そして、運用上、寸法図形として 1 つにまとめて扱う必要があります。</p>
<p>これを実現するために、AutoCAD は<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-F81D7F1E-1F0A-45AD-AC7E-891A85A0033A" rel="noopener" target="_blank">ブロック定義</a>と<a href="https://help.autodesk.com/view/ACD/2024/JPN/?guid=GUID-BC0FD3C1-3BFC-4C5D-AB9A-BF480D5084BE" rel="noopener" target="_blank">ブロック参照</a>の仕組みを利用しています。作図した寸法図形はブロック参照であり、自動的に作成されたブロック定義を参照します。寸法のブロック定義は、ユーザー定義のブロックと区別するため、*D で始まるブロック名が自動的に割り当てられて、INSERT コマンドや BLOCK コマンド、BEDIT コマンドのブロック エディタから隠ぺいされます。このようなブロックを「Anonymous Block」、日本語名で「匿名ブロック」、または「名前のないブロック」と呼んでいます。</p>
<p>ご参考：</p>
<ul>
<li><a href="https://help.autodesk.com/view/ACD/2024/JPN/?caas=caas/sfdcarticles/sfdcarticles/kA93g000000H1Wb.html" rel="noopener" target="_blank">AutoCAD 2024 ヘルプ | 長さ寸法などを作成すると、*D28 などのブロックが作成される | Autodesk</a></li>
<li><a href="https://help.autodesk.com/view/ACD/2024/JPN/?caas=caas/sfdcarticles/sfdcarticles/kA93g0000000Eok.html" rel="noopener" target="_blank">AutoCAD 2024 ヘルプ | 匿名ブロックを編集できない | Autodesk</a></li>
</ul>
<p>前置きが長くなりましたが、&quot;スタイル1&quot; 文字スタイルを削除出来ないのは、寸法図形用に用意された匿名ブロック（ブロック定義）が、まだ &quot;スタイル1&quot; 文字スタイルを参照しているためです。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3a6f564200d-pi" style="display: inline;"><img alt="Anonymous_block" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3a6f564200d image-full img-responsive" src="/assets/image_23587.jpg" title="Anonymous_block" /></a></p>
<p><strong> &quot;スタイル1&quot; 文字スタイルや寸法図形が参照していた匿名ブロックは図面から削除出来ないのでしょうか？</strong></p>
<hr />
<p>AutoCAD は、図面を開いたタイミングで図面データベース内を検査して、参照されていない匿名ブロックとネストされた外部参照ブロックを名前削除（パージ）する仕様を持っています。</p>
<p><strong>ここまでの例では、図面をファイルに一旦保存して、再度開くと &quot;スタイル1&quot; 文字スタイルを [文字スタイル管理] ダイアログの [削除] ボタンで削除出来るようになります。</strong></p>
<p>同じ現象は、画層を作成して寸法図形に同画層を指定した後で、その寸法図形を削除した場合にも発生します。この場合は寸法図形が参照していた画層が削除出来ない状態になります。これを解決するのは、図面を一旦保存して、開き直せばいいわけです。寸法図形の匿名ブロックは、図面を開いたタイミングで自動的に削除されます。</p>
<hr />
<p>この仕組みは、図面要素間の「つながり」を維持して、図面破損を防ぐ仕様です。もし、図面を開き直さずに同じ図面セッションで参照されていた孤立要素（ここではAPI で &quot;スタイル1&quot; 文字スタイル）を削除したい場合には、AutoCAD .NET API か ObjectARX でのみ可能です。</p>
<p style="padding-left: 40px;">例：<a href="https://forums.autodesk.com/t5/autodesk-community-tips-adnopun/autocad-net-api-cun-fa-xue-chu-zhi-houno-cun-fa-can-zhao-hua-cengno-xue-chu/ta-p/12462307" rel="noopener" target="_blank">AutoCAD .NET API：寸法削除直後の寸法参照画層の削除 - Autodesk Community</a></p>
<hr />
<p>原因がわからず、うやむやになっている現象かと思いますが、不具合ではなく、こんな理由があります。</p>
<p>By Toshiaki Isezaki</p>
