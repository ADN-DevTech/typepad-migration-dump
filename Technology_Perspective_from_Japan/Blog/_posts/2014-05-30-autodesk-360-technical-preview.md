---
layout: "post"
title: "Autodesk 360 Tech Preview"
date: "2014-05-30 19:23:44"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2014/05/autodesk-360-technical-preview.html "
typepad_basename: "autodesk-360-technical-preview"
typepad_status: "Publish"
---

<p><strong>Autodesk 360 Tech Preview</strong> が公開されているのをご存じでしょうか。この Tech Preview は、現在、お使いいただいている <strong>Autodesk 360</strong>（<strong><a href="http://360.autodesk.com" target="_blank">http://360.autodesk.com</a></strong>）の将来の姿を評価していただくためのものです。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511c3a1a3970c-pi" style="display: inline;"><img alt="A360_Techical_Preview" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511c3a1a3970c img-responsive" src="/assets/image_976841.jpg" title="A360_Techical_Preview" /></a></p>
<p>公開は数か月前からおこなわれていましたが、ユーザ インタフェースが英語のままでしたので、このブログでは取り上げていませんでした。英語表記のみの提供であることは変わりありませんが、先週、Autodesk 360 Tech Preview に新たな機能が加わりましたので、ご紹介しておきたいと思います。</p>
<p>Autodesk ID をお持ちであれば、直接、<span style="font-size: 11pt;"><strong><a href="http://www.autodesk360.com" target="_blank">http://www.autodesk360.com</a></strong></span>&#0160;から&#0160;Autodesk 360 Tech Preview にアクセスすることが出来ます。</p>
<p>Autodesk 360 Tech Preview は、従来の Autodesk 360 と異なり、プロジェクト ベースでファイルやコラボレーションなどを管理します。この方法は、Fusion 360 で採用されている方法と同じです。このため、どのデータが、どのプロジェクトに相当するのか、フォルダを作成することで自身で管理する必要があった点が、大幅に改善されています。ファイルを共有する場合でも、プロジェクト単位の指定になるため、プロジェクトに関係のない人を、誤って共有先に加えてしまうこともありません。</p>
<p style="text-align: center;"><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511c3a4fb970c-pi" style="display: inline;"><img alt="Project_Based_A360" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511c3a4fb970c image-full img-responsive" src="/assets/image_471279.jpg" title="Project_Based_A360" /></a></p>
<p>さて、Autodesk 360 Tech Preview に実装された新しい機能で紹介しておきたいのは、搭載されたビューワー機能です。従来の Autodesk 360 にもアップロードした 2D 図面や 3D モデルを表示するビューワーが搭載されていましたが、透明なガラスを含むマテリアル表現や陰影、遠近感を演出する焦点レンズ長などの設定が出来ませんでした。今回、Autodesk 360 Tech Preview には新しいビューワーが搭載されて、いままで出来なかった表現方法が可能になりました。次の比較を見ていただければ、差をご理解いただけるはずです。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511c3b556970c-pi" style="display: inline;"><img alt="A360_Viewing" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01a511c3b556970c image-full img-responsive" src="/assets/image_352312.jpg" title="A360_Viewing" /></a></p>
<p>新しいビューワーの特徴をまとめると、次のようになります。&#0160;</p>
<p style="padding-left: 30px;"><strong>ゼロ クライアント</strong>：WebGL&#0160; 対応ブラウザがあれば OK</p>
<p style="padding-left: 30px;"><strong>HTML5&#0160; 実装</strong>：最新の Web テクノロジ</p>
<p style="padding-left: 30px;"><strong>ストリーミング</strong>：大規模モデルの表示が高速</p>
<p style="padding-left: 30px;"><strong>高品質表示</strong>：陰影、マテリアル等を高度に表現</p>
<p style="padding-left: 30px;"><strong>対応ファイル形式</strong>：多様なファイル形式をサポート※</p>
<p>対応するファイル形式とは、Autodesk 360 にアップロードしてビューワーで表示する目的で変換できるファイル形式を指します。 いままでの Autodesk 360 も、この変換（Translation Service）をすることで、様々なファイルを表示することが<a href="http://adndevblog.typepad.com/technology_perspective/2013/09/supported-files-on-autodesk-360.html" target="_blank">出来るようになっていました</a>。</p>
<p>新しいビューワーでも、Autodesk 360 にアップロードしたデータは Translation Service で表示可能な中間形式に変換されます。変換対象のファイル形式が約 60 種類に増加しています。サポートされるデータ形式は、3DM、3DS、AFE、AFEM、BMP、CATPART、DWF、WFx、DWG、DWT、F3D、IAM、IDW、IPT、IGES、IGS、INSTRUCTION、MOCKUP、NWD、OBJ、PRT、RCP、RVT、SAT、SIM、SIM360、SLDPRT、STE、STEP、STL、STP、VTFX、WIRE、X_B、X_T の拡張子を持つファイルです。</p>
<p>ただし、アセンブリや外部参照など、ファイル内から別のファイルを参照するファイルは、現時点ではサポートされていません。そのようなモデルをアップロードして閲覧させる場合には、アップロードする際にクライアント上の CAD で、3D DWF を出力したり、Navisworks で 1 つのファイルにまとめておくことで対応することが出来ます。なお、マテリアル表現など、一部の機能はファイル形式によってはサポートされない点にも注意してください。</p>
<p>Autodesk 360 Tech Preview のビューワー機能を動画にまとめましたので、目を通してみてください。</p>
<p style="text-align: center;"><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/xczRaLQ8zkg?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p>そして、このビューワー機能を単体で切り出して、カスタマイズやマッシュアップに利用できるようにするのが、先月、<a href="http://adndevblog.typepad.com/technology_perspective/2014/05/showing-web-services-api-at-expo.html" target="_blank">クラウド コンピューティング EXPO</a> で公開した Viewing Service API です。&#0160;</p>
<p>まずは、Autodesk 360 Tech Preview でその機能をご確認ください。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
