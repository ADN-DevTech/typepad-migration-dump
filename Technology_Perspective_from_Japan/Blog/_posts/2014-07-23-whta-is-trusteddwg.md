---
layout: "post"
title: "TrustedDWG とは?"
date: "2014-07-23 00:26:43"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/07/whta-is-trusteddwg.html "
typepad_basename: "whta-is-trusteddwg"
typepad_status: "Publish"
---

<p>AutoCAD や AutoCAD LT で図面ファイルを開こうとすると、次のような警告メッセージが表示されることがあります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73df06a4c970d-pi" style="display: inline;"><img alt="Ask-Opening-Untrusted-Drawing" class="asset  asset-image at-xid-6a0167607c2431970b01a73df06a4c970d img-responsive" src="/assets/image_14916.jpg" style="width: 400px;" title="Ask-Opening-Untrusted-Drawing" /></a></p>
<p style="text-align: left;">この警告を無視して図面ファイルを開くことが出来ますが、AutoCAD ウィンドウ右下のステータスバー上では、「Autodesk TrustedDWG ファイルではない」ということを示すアイコンが表示された状態となります。このような図面ファイルを、非 TrustedDWG ファイル、または、非TrustedDWG と呼んでいます。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a73df069c4970d-pi" style="display: inline;"><img alt="Un-TrustedDWG" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a73df069c4970d img-responsive" src="/assets/image_409857.jpg" title="Un-TrustedDWG" /></a></p>
<p>非TrustedDWG とは、オートデスク製品やオートデスクのテクノロジを利用した製品やサービス以外で保存された図面ファイルを意味します。逆に、オートデスク製品やオートデスクのテクノロジを利用した製品が保存した図面ファイルは、TrustedDWG ということになります。</p>
<p><strong>TrustedDWG と 非TrustedDWG</strong></p>
<p>AutoCAD の普及とともに、そのファイル形式である DWG<sup>TM</sup>&#0160;が図面ファイルの主流となっていきました。同時に、クローン CAD と呼ばれる CAD 製品やテクノロジーが登場して、それらが保存する DWG ファイルも流通するようになってきました。これが、非TrustedDWG ファイルです。TrustedDWG ファイルも 非TrustedDWG ファイルも、拡張子が同じ .dwg であるため、一見しただけでは違いがわかりません。そこで 、最近の AutoCAD や AutoCAD LT では、図面を開く際の動作として、明示的に DWG の違いを区別できるようになっています。</p>
<p>非TrustedDWG ファイルの問題は、稀に、図面の情報が欠落するなど、破損を含んでいる可能性がある点です。非TrustedDWG ファイルが、必ず図面破損しているわけではありませんので注意してください。あくまで、その可能性が高いということです。</p>
<p>ご存じのように、AutoCAD は毎年バージョンアップして新機能を搭載しています。そして、新機能で利用される新しい情報を、図面ファイルに保存します。ところが、オートデスクの関知しないところで作成されたクローン CAD 製品やテクノロジが、そのような新しい情報が適切に扱う保証はありません。なぜなら、クローン CAD が独自のテクノロジで DWG ファイルを保存しているためです。つまり、クローン CAD はオートデスクのテクノロジを利用しているわけではないのです。</p>
<p>オートデスクが、外部に DWG テクノロジを提供するのは、<a href="http://www.autodesk.co.jp/developoem" rel="noopener" target="_blank">AutoCAD OEM</a> と <a href="http://www.autodesk.co.jp/realdwg" rel="noopener" target="_blank">RealDWG</a> という開発者ツールキットの形態でのみです。AutoCAD OEM や RealDWG を用いた製品には、このようなロゴが貼付されて、オートデスクの TrustedDWG を保存する機能を有していることが示されます。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511e59d1c970c-pi" style="display: inline;"><img alt="Logos" class="asset  asset-image at-xid-6a0167607c2431970b01a511e59d1c970c img-responsive" src="/assets/image_986161.jpg" title="Logos" /></a>&#0160;</p>
<p><strong>図面破損とは</strong></p>
<p>具体的な図面破損の状態をご紹介します。破損といっても、その深刻度はさまざまです。もっとも深刻なのは、図面を開くことが出来ない状態です。この場合、AutoCAD の <strong><a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-78F5B1AB-583F-410F-85DA-6D03768832C8" rel="noopener" target="_blank">RECOVER[修復] コマンド</a></strong>で、破損個所を修復して、図面を開ける状態に出来る可能があります。ただ、修復といっても、完全にオリジナルの状態を復元するのではなく、データ欠落によって図面上のオブジェクト間の参照関係に不整合が発生している場合には、それらをすべて削除してしまう可能性もあります。この場合、図面を開くことが出来るようになるものの、図面から必要な情報が消えてしまうので注意が必要です。</p>
<p>図面を無事に開くことが出来た場合でも、潜在的に図面が破損している可能性もあります。致命的ではないものの、図面内のオブジェクトの参照関係が崩れている場合です。開いた図面が破損しているか否かを調べるために、AutoCAD には <a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-62DDB935-61B1-49DA-8238-3EF1CC45259B" rel="noopener" target="_blank"><strong>AUDIT[監査] コマンド</strong></a>が用意されています。このコマンドを実行すると、稀に、図面破損個所を示すエラーを表示することがあります。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd35fae0970b-pi" style="display: inline;"><img alt="Audit_errors" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd35fae0970b image-full img-responsive" src="/assets/image_760795.jpg" title="Audit_errors" /></a></p>
<p>上記のエラーの場合では、ハッチング境界を指定して<a href="http://help.autodesk.com/view/ACD/2015/JPN/?guid=GUID-6FF62998-46E5-4649-9F68-3CFC6C4BFC46" rel="noopener" target="_blank">自動調整ハッチング</a>として作図したハッチング オブジェクトについて、境界図形との関係が崩れていることが示されています。もちろん、この状態では、境界図形の形状を変更しても、ハッチングは自動的に形状変更に追従することが出来ません。AUDIT[監査] コマンドは図面修復の機能も持っているので、エラーを修復することも出来ますが、残念ながら、処理内容は RECOVER[修復] コマンドと同様です。場合によっては不整合のあるデータを削除してしまいますので注意が必要です。</p>
<p>なお、オブジェクト間の参照が崩れていると、AutoCAD のクラッシュやハングアップを誘発する危険性もあります。簡単な例では、ある図形を選択して [オブジェクト プロパティ管理] パレットで情報を表示することを考えてみます。図形には、さまざまな情報が含まれますは、図形は、必ず参照している画層を持ち、かつ、その画層は必ず図面内に存在している必要があります。もし、参照先の画層が図面から欠落していると、[オブジェクト プロパティ管理] パレットに情報を表示させるために、図形を選択した瞬間に AutoCAD がクラッシュしてしまいます。</p>
<p>AUDIT[監査] コマンドがエラーを表示しない場合でも、図面に問題がある場合もあります。例えば、図面ファイルにダイナミック ブロックを使っているような場合、クローン CAD で編集されていまうと、ダイナミックに変化する機能を失った普通のブロック（スタティック ブロック）になってしまうことがあります。せっかく生産性を向上させるためにダイナミック ブロックを定義しても、これだと、意味がありません。</p>
<p>図面破損は、図面を運用する上で修正や再作成などの手戻りを余儀なくさせ、生産性を大幅に低下させてしまいます。関連会社も含めて、リスクが高い 非TrustedDWG の運用は出来るだけ避けていただくのがベストです。&#0160;</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fd35feff970b-pi" style="display: inline;"><img alt="TrustedDWG" class="asset  asset-image at-xid-6a0167607c2431970b01a3fd35feff970b img-responsive" src="/assets/image_614278.jpg" title="TrustedDWG" /></a></p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
