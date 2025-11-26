---
layout: "post"
title: "Revit 2015 コンセプトデザインを建築モデルに変換する"
date: "2015-04-03 22:30:56"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/04/revit-2015-mass-conversion.html "
typepad_basename: "revit-2015-mass-conversion"
typepad_status: "Publish"
---

<p>以前、<a href="http://adndevblog.typepad.com/technology_perspective/2015/03/revit-2015-form.html" target="_blank">Revit 2015 のコンセプトデザイン環境でマスを作成してボリュームスタディを行う方法</a>についてご紹介しましたが、今回は、作成したマスを建物要素に変換して、Revitで設計するための建築モデルに変換する方法を解説いたします。</p>
<p>プロジェクト内にマスを配置・作成すると、それぞれマス毎にボリュームとして識別されるため、このままの状態では、壁や床などの建物要素として個別に編集することはできません。<br />マスはフォームやスケッチで構成されており、フォームの面を指定して、それぞれの建物要素（屋根・床・壁・カーテンシステム）に変換していきます。</p>
<p>建物要素に変換するには、[マス &amp; 外構]タブの[モデル面]パネルから、作成したい建物要素を選択します。</p>
<p>床を作成する際には、予めプロジェクトでレベルが設定され、マス床が作成されてている必要があります。</p>
<p>レベル<br /><a href="http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-075B9A47-69AB-44D2-8A05-6136EFF2694" target="_blank">http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-075B9A47-69AB-44D2-8A05-6136EFF2694</a></p>
<p>マス床を作成する<br /><a href="http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-7D7C1D92-5318-4BF9-99C2-10A45113E63F" target="_blank">http://help.autodesk.com/view/RVT/2015/JPN/?guid=GUID-7D7C1D92-5318-4BF9-99C2-10A45113E63F</a><br /><br /><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/6upwEQWZU_8?feature=oembed" width="500"></iframe>&#0160;</p>
<p>APIを利用してマスから壁を作成することもできます。次の動画では、ユーザーが選択したマスのGeometryObjectを取得し、Faceの参照とWallTypeからFaceWallオブジェクトを生成します。FaceWallクラスは傾斜のあるマス面に壁を追加するためのクラスです。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/sgcxCmlR3BQ?feature=oembed" width="500"></iframe>&#0160;</p>
<p>このように、コンセプトデザイン環境を通じてマススタディを行い、作成したマスから建築モデルに変換することで、スムースにRevitの建築モデル設計に移行することができます。</p>
<p>またサンプルコードは以下のAutodesk Technical Q&amp;A に掲載しております。<br />ご興味のある方は、ご確認ください。</p>
<p>QA-9623　傾斜するマス面から壁を作成する方法<br /><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=9623" target="_blank">http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=9623</a></p>
<p>By Ryuji Ogasawara</p>
