---
layout: "post"
title: "Inventor製品のプロジェクトファイルについて"
date: "2015-01-19 01:00:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/01/inventor_projectfile.html "
typepad_basename: "inventor_projectfile"
typepad_status: "Publish"
---

<p>Inventor製品を使って設計作業をする場合、あまりどのブログでも取り上げられておりませんが Inventorはプロジェクト有きの製品である事を軽視しがちであり、今回は、Inventor製品内で一番地味な存在である「プロジェクトファイル .ipj」について、知られているようで知られていない重要な働きを取り上げたいと思います。</p>

<p>まず、プロジェクトファイルは、.ipjという拡張子のファイルで内部には設計データファイルとライブラリへのアクセスやファイル参照を維持するためのパスが設定されているものです。</p>

<p>通常考えられるプロジェクトは、たとえばプロジェクト固有のパーツやアセンブリ、社内の独自の標準コンポーネント、既製のコンポーネント(締結部品、継ぎ手、電子コンポーネントなど)といったもので構成されて利用されており、一般的には設計データは複数のファイルに分散しており、パーツ、アセンブリ、図面、プレゼンテーションはそれぞれ固有のファイルに保存されますが、これらの各ファイルから 1 つまたは複数のコンポーネントや他のアセンブリを参照していますので、プロジェクトはファイル間参照の従属情報を保持することにより、プロジェクトを定義するファイル セットの移動、Pack and Goなどのアーカイブ、再構築を可能にするために用いられる設計の重要なベース基盤の環境が設定されています。</p>

<p>これらの説明は 製品ヘルプの <a href="http://help.autodesk.com/view/INVNTOR/2015/JPN/?guid=GUID-081D84B0-8D33-4202-97F6-B47893E12719">「デザイン データを整理するためのプロジェクト」</a> 内の録画を参照してください。</p>

<p>製品ヘルプ内のこのレコード再生です。<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0c129ba970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0c129ba970c img-responsive" alt="ProjectInfoInHelp" title="ProjectInfoInHelp" src="/assets/image_612193.jpg" /></a><br /></p>

<p>無論、Inventor APIでもプロジェクトの制御は可能ですので、<a href="https://knowledge.autodesk.com/community/article/79281">Inventorプロジェクト.ipjファイルを変更する方法</a> を参考にしてください。</p>

<p>By Shigekazu Saito<br />
</p>
