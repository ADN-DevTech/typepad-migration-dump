---
layout: "post"
title: "AutoCAD 2018 の新機能 ～ その2"
date: "2017-03-23 00:08:08"
author: "Toshiaki Isezaki"
categories:
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2017/03/new-features-on-autocad-2018-part2.html "
typepad_basename: "new-features-on-autocad-2018-part2"
typepad_status: "Publish"
---

<p>前回ご案内した<a href="http://adndevblog.typepad.com/technology_perspective/2017/03/new-features-on-autocad-2018-part1.html" rel="noopener noreferrer" target="_blank">&#0160;<strong>ユーザ インタフェース機能</strong></a>&#0160;に続いて、AutoCAD 2018 の新機能や強化された <strong>図面化</strong> の機能についてご紹介していきます。今回ご案内する図面化の機能も、AutoCAD 2017.1 Update で導入された機能です。また、AutoCAD LT 2018 でも利用することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8dda43c970b-pi" style="display: inline;"><img alt="Acad_acadlt" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8dda43c970b image-full img-responsive" src="/assets/image_91729.jpg" title="Acad_acadlt" /></a></p>
<p><strong>PDF インポートの強化</strong></p>
<p style="padding-left: 30px;">AutoCAD 2017 では、PDF 図面に記入されている文字が TrueType フォントで作図されていると、そのフォントを PDF ファイルの読み込み時に認識して、AutoCAD のテキストエディタで編集できる文字オブジェクトに変換することが出来ました。ただ、PDF 図面内の文字がシェイプ フォント（SHX フォント）で作図されていた場合、文字認識がされず、円や円弧、ポリラインなどのジオメトリの集合としてしかインポート出来ませんでした。こうなってしまうと、もちろん、テキストエディタで記述内容を編集することが出来ません。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d267f8aa970c-pi" style="display: inline;"><img alt="Imported_geometries" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d267f8aa970c image-full img-responsive" src="/assets/image_309349.jpg" title="Imported_geometries" /></a></p>
<p style="padding-left: 30px;">AutoCAD 2018（AutoCAD 2017.1 Update）では、PDF 図面内のシェイプ フォント文字が英語フォントである場合、PDF ファイルの<span style="text-decoration: underline;">インポート後</span>に<strong>&#0160;</strong>PDFSHXTEXT[PDF SHX 文字変換]コマンドを使って、マルチテキスト オブジェクトに変換することが出来るようになりました。 つまり、円や円弧、ポリラインなど、バラバラになってしまったジオメトリの集合を、文字オブジェクトとして再認識させることが可能になりました。<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0980cfc8970d-pi" style="display: inline;"><img alt="Converted_text" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0980cfc8970d image-full img-responsive" src="/assets/image_584928.jpg" title="Converted_text" /></a></p>
<p style="padding-left: 30px;">PDFSHXTEXT[PDF SHX 文字変換] コマンドの SE（設定） オプションを利用すると、変換対象となるシェイプ フォントの種類や変換時に採用するフォントの優先順位を変更することも出来ます。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0980d616970d-pi" style="display: inline;"><img alt="Pdf_text_recoginition" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0980d616970d image-full img-responsive" src="/assets/image_938851.jpg" title="Pdf_text_recoginition" /></a></p>
<p style="padding-left: 30px;">残念ながら、文字認識は、英語のシェイプ フォントに限定されているため、bigfont.shx や extfont2.shx を指定するとエラーメッセージが表示されますのでご注意ください。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0980d63e970d-pi" style="display: inline;"><img alt="Pdf_text_recoginition_err" class="asset  asset-image at-xid-6a0167607c2431970b01bb0980d63e970d img-responsive" src="/assets/image_247289.jpg" style="width: 350px;" title="Pdf_text_recoginition_err" /></a></p>
<p style="padding-left: 30px;">ここまでの内容を動画にまとめていますので、ご確認ください。</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/aS9p-Ruspzk?feature=oembed" width="500"></iframe><strong><br /></strong></p>
<p><strong>テキスト結合（文字結合）</strong></p>
<p style="padding-left: 30px;">複数の文字オブジェクト(&#0160;TEXT) やマルチテキスト オブジェクト(MTEXT) を 1 つに結合する <strong>TXT2MTXT[文字を結合] コマンド</strong>が新設されました。前述の&#0160;PDFSHXTEXT[PDF SHX 文字変換] コマンドでは、認識・変換されるマルチテキストは 1 行単位になってしまうため、それらを 1 つに結合するために利用することが出来ます。&#0160;</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8dda392970b-pi" style="display: inline;"><img alt="Make_single_mtext" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8dda392970b image-full img-responsive" src="/assets/image_779542.jpg" title="Make_single_mtext" /></a></p>
<p style="padding-left: 30px;">結合時に<strong> TXT2MTXT[文字を結合] コマンド</strong>の SE（設定） オプションでは、マルチテキスト領域での文字の折り返しや行間などを設定することが可能です。</p>
<p style="padding-left: 30px;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c8dda3ed970b-pi" style="display: inline;"><img alt="Txt2mtext_settings" class="asset  asset-image at-xid-6a0167607c2431970b01b7c8dda3ed970b img-responsive" src="/assets/image_459849.jpg" style="width: 350px;" title="Txt2mtext_settings" /></a></p>
<p style="padding-left: 30px;">この処理では、もともと別々の文字オブジェクトを 1 つに結合することになるので、結合されたマルチテキストが結合前のレイアウトを維持できないことも考えられます。その場合には、マルチテキスト エディタっで行間やインデントを調整してみてください。</p>
<p style="padding-left: 30px;">なお、AutoCAD 2017.1 Update では、Express Tools の扱いになっていたため、コマンド プロンプト等に表示されるガイド メッセージが英語になっていましたが、AutoCAD 2018 ではすべて日本語化されています。</p>
<p style="padding-left: 30px;">ここまでの内容を動画にまとめていますのでご確認ください。</p>
<p class="asset-video" style="padding-left: 30px;"><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/L9izRNm4jsc?feature=oembed" width="500"></iframe><strong><br /></strong></p>
<p>次回は、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2017/03/new-features-on-autocad-2018-part3.html" rel="noopener noreferrer" target="_blank">コラボレーション機能</a></strong>&#0160;についてご紹介します。</p>
<p>By Toshiaki Isezaki</p>
