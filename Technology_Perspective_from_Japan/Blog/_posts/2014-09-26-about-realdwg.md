---
layout: "post"
title: "RealDWG について"
date: "2014-09-26 00:12:29"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/09/about-realdwg.html "
typepad_basename: "about-realdwg"
typepad_status: "Publish"
---

<p style="text-align: center;"><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4bdb2e9200c-pi" style="display: inline;"><img alt="Realdwg" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0240a4bdb2e9200c image-full img-responsive" src="/assets/image_313655.jpg" title="Realdwg" /></a></p>
<p style="text-align: left;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6e6420c970b-pi" style="display: inline;"></a>前回の AutoCAD OEM に続いて、<a href="http://adndevblog.typepad.com/technology_perspective/2014/07/whta-is-trusteddwg.html" rel="noopener" target="_blank"><strong>TrustedDWG</strong></a>&#0160;を扱い、保存することが出来る開発者用 SDK である RealDWG をご紹介しましょう。</p>
<p>RealDWG は、開発者向けに用意された SDK（Software Development Kit）です。インストールしても、AutoCAD OEM のようにショートカット アイコンがデスクトップに登録されて、ソフトウェアを直接起動するようなことは出来ません。どちらかというと、AutoCAD や AutoCAD OEM が DWG ファイルや DXF ファイルにアクセスするための機能のみを提供します。</p>
<p>RealDWG SDK を開発に利用する 3rd Party デベロッパは、AutoCAD や AutoCAD OEM に変わるホスト アプリケーションと呼ばれるウィンドウを持つソフトウェアを開発して、その内部で RealDWG を用いて&#0160;DWG/DXF ファイルにアクセスするようなイメージです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb078b6b1a970d-pi" style="display: inline;"><img alt="Realdwg_image" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb078b6b1a970d image-full img-responsive" src="/assets/image_635237.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Realdwg_image" /></a></p>
<p>AutoCAD や AutoCAD OEM のカスタマイズでは、ObjectARX や AutoCAD .NET API、AutoLISP やCOM といった AutoCAD API を駆使して、さまざまな独自機能やコマンドを実装することが出来ました。このような API や コマンドという概念は、「AutoCAD」というソフトウェア（ホスト アプリケーション）が持つ概念です。このため、RealDWG を利用する 3rd Party 製のホスト アプリケーションには適用されません。分かり易くいうと、RealDWG では、AutoCAD API と利用してコマンドを作成したり、拡張するようなことは出来ません。同様に、DWG/DXF ファイルを画面上に表示するような機能も提供していません。インタフェースへの図面表示は、ホスト アプリケーションに依存します。</p>
<p>RealDWG で唯一可能なのは、ファイルとして保存されている DWG/DXF ファイルをメモリ上に論理的なつながりとともに展開し、操作する点です。もちろん、メモリ上で操作した図面のイメージをDWG/DXF ファイルとして保存することも可能です。メモリ上に新規に図面イメージを構築して、DWG/DXF&#0160;ファイル保存することも出来ます（新規図面の作成）。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c6e65d07970b-pi" style="display: inline;"><img alt="Realdwg_image2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c6e65d07970b image-full img-responsive" src="/assets/image_429302.jpg" title="Realdwg_image2" /></a></p>
<p>ここでいうメモリ上の論理的な図面イメージとは、以前のブログ記事&#0160;<a href="http://adndevblog.typepad.com/technology_perspective/2014/07/drawing-structure-and-corruption-from-autocad-api-perspective.html" rel="noopener" target="_blank"><strong>AutoCAD API から見た図面構造と破損</strong></a> でご案内した&#0160;<strong>図面データベース</strong>&#0160;に一致します。つまり、AutoCAD API そのもを利用することは出来なくても、扱う対象は同じメモリ上に展開されたオブジェクトと、そのつながりである オーナーシップ接続 です。</p>
<p>提供される API は、ObjectARX に準ずるアンマネージ C++ 環境と、.NET Framework ベースで AutoCAD .NET に準じたマネージ言語環境（C#、VB,NET、C++）です。もし、AutoCAD API で ObjectARX と AutoCAD .NET API の使用経験をお持ちなら、ホストアプリケーション組み込みの部分を除いて、違和感なくプログラミングすることが出来るはずです。次の比較表は、AutoCAD、AutoCAD OEM と RealDWG のカスタマイズで実装可能な機能を簡単にまとめたものです。厳密には異なりますが、RealDWG でも前述の API 環境を ObjectARX と .NET API と表現している点に注意してください。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0704f5f970c-pi" style="display: inline;"><img alt="Comparison" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d0704f5f970c image-full img-responsive" src="/assets/image_495518.jpg" title="Comparison" /></a></p>
<p>この表で注目していただきたいのは、図面の表示機能に加えて、RealDWG には印刷機能すらないという点です。AutoCAD の印刷機能には、印刷スタイルという AutoCAD 固有の機能も利用されています。もちろん、印刷スタイルは、ホストアプリケーションとして AutoCAD が提供する機能です。これら機能は、残念ながら、RealDWG では提供されていません。</p>
<p>さて、RealDWG の内容を調査したい、というご要望をいただきます。ここまでの説明で推測出来るかと思いますが、RealDWG の SDK の内容は、ほぼ、ObjectARX や AutoCAD .NET API で共通です。このため、ドキュメントも 90% ObjectARX SDK に含まれるものと共通になっています。ObjectARX SDK を <a href="http://www.autodesk.com/objectarx" rel="noopener" target="_blank">http://www.autodesk.com/objectarx</a>&#0160;からダウンロードして、ObjectARX Reference（arxref.chm）や&#0160;Managed Class Reference（arxmgd.chm）を除いてみてください。&quot;ReadDWG&quot; で検索すると、RealDWG 固有の関数、メソッド、クラスなどを参照することが出来ます。<a href="http://www.autodesk.co.jp/realdwg" rel="noopener" target="_blank">http://www.autodesk.co.jp/realdwg</a> にも、簡単な紹介が公開されていますので、併せてご参照ください。</p>
<p>もし、価格も含めて RealDWG に興味を持たれた場合には、Tech Soft 3D 社にコンタクトしてみてください。AutoCAD OEM 同様、RealDWG は <a href="https://www.techsoft3d.com/jp/" rel="noopener" target="_blank"><strong>Tech Soft 3D</strong></a> 社のみが扱う開発者用 SDK 製品です。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
