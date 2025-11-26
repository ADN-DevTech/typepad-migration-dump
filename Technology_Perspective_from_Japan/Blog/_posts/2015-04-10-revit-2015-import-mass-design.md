---
layout: "post"
title: "Revit 2015 3D デザイン ソフトウェアで作成したジオメトリからマスを作成する"
date: "2015-04-10 22:39:43"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/04/revit-2015-import-mass-design.html "
typepad_basename: "revit-2015-import-mass-design"
typepad_status: "Publish"
---

<p>Autodesk 3ds Max や AutoCAD の3D機能など、Revit 以外の3D デザイン ソフトウェアで作成したマス スタディをプロジェクトに読み込んで、Revit のホスト要素(壁、屋根など)に関連付けてモデリング設計に移行することができます。</p>
<p>普段使い慣れている3D デザイン ソフトウェアを活用しながら、Revit で建築要素に変換してファミリを適用することで、開口部や屋根の詳細などをより建築の立体的なビジュアルに沿ってレビューすることができます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c776fcfd970b-pi" style="display: inline;"><img alt="Mass_study_acad" class="asset  asset-image at-xid-6a0167607c2431970b01b7c776fcfd970b img-responsive" src="/assets/image_98896.jpg" title="Mass_study_acad" /></a></p>
<p>次のワークフローを使用すると、外部の CAD 形式からのジオメトリを Revit のマスファミリに読み込んで、 マス オブジェクトとして認識することができます。もちろん、インプレイスマスとして読み込むこともできます。</p>
<p>1. 3D デザイン ソフトウェアでサポートされるファイル形式( DWG または SAT など)に設計を書き出します。<br />2. Revit で、マス ファミリにファイルを読み込みます。<br />3. プロジェクトを開き、マス ファミリをロードし、プロジェクト内にマス ファミリのインスタンスを配置します。<br />4. これにより、Revit でジオメトリをマスとして処理できるようになり、マス コンポーネントの面を選択し、壁、床、屋根などの Revit ホスト要素に関連付けできます。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="http://www.youtube.com/embed/dbEzH2s1fhg?feature=oembed" width="500"></iframe>&#0160;</p>
<p>ただし、場合によっては、読み込まれたジオメトリがマス インスタンスに適さない場合があります。読み込まれたジオメトリがマス インスタンスに適さない場合、代わりに一般モデル カテゴリを使用できます。一般モデル カテゴリは、マス インスタンスと類似している点があり、壁、屋根、カーテン システムは、一般モデル ファミリの面から作成することができます。</p>
<p>このように、外部のアプリケーションで作成したジオメトリも、Revit で建築モデルに変換すれば、ファミリのタイプやデザインオプションを変更しながら建築要素のデザイン詳細まで検討することができます。</p>
<p>By Ryuji Ogasawara</p>
