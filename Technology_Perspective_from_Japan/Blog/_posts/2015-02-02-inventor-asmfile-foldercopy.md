---
layout: "post"
title: "Inventor製品のアセンブリファイルのフォルダー間コピー操作"
date: "2015-02-02 01:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "その他カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/02/inventor_asmfile_foldercopy.html "
typepad_basename: "inventor_asmfile_foldercopy"
typepad_status: "Publish"
---

<p>今回は、Inventor製品のファイルをフォルダー間でコピーした後にハンドリングする際の話題を取り上げさせていただきます。</p>

<p>普段私たちはWindowsのエクスプローラ内でテキストファイルやEXCELファイルなどのクローン作成を目的として「コピー(C)」->「貼り付け(P)」の操作をしてクローン化しますが、Inventor製品ではファイルのクローン化は同様の手順では作成する事ができません。</p>

<p>特に気を付けなければならないのは、アセンブリファイルをコピーしようとした時です。</p>

<p>Inventor製品のアセンブリファイルは、「コンポーネント配置」の操作を経て「パーツファイル」や「他のアセンブリファイルをサブアセンブリ」としてトップのアセンブリファイルに配置された関連性を持って構成されています。</p>

<p>これは、トップアセンブリファイル内から見た場合、コンポーネントとして配置した「実際のパーツファイル内部のオブジェクト群」や「他のアセンブリファイルをサブアセンブリとしたオブジェクト群」そのものがトップアセンブリファイル内に複写されているものではありません。<br />
（仮に、実際のモデルのオブジェクト群がトップアセンブリ内にクローンとして存在させるようなメカニズムであれば、1つの工場の全てといった大規模プロジェクトをInventor製品で設計した場合、トップアセンブリのファイルの大きさは想像できませんし、トップのアセンブリファイルを開こうとしてInventor製品内部に読み込むだけでも時間が掛かり、おそらく仕事にはならないでしょう）</p>

<p>アセンブリファイル内に配置されたコンポーネントの関連性を制御しているのは、以前の記事 <a href="http://adndevblog.typepad.com/technology_perspective/2015/01/inventor_ProjectFile.htmlhttp://">Inventor製品のプロジェクトファイルについて</a> と <a href="http://adndevblog.typepad.com/technology_perspective/2015/01/inventor_file_search.html">Inventor製品のファイル検索順序について</a> で紹介しました Inventorのプロジェクトファイル .ipt ファイル が大きな働きをしており、手動操作はもとより Inventor API を使ってアセンブリのハンドリングをする場合にも無視してはならず、APIによるカスタマイズの場合でも意識した状態で Inventor API を使わなければ、設計者が意図に反したフォルダー先の構成ファイルが更新される結果になりますので、注意する必要があります。</p>

<p>以下の説明ビデオは、単純に複写元からファイル群を複写先にコピーして複写先のトップアセンブリを開いて修正し保存した時の振る舞いと、複写元からプロジェクトファイルを含む全てのファイル群を複写先にコピーして複写先のトップアセンブリを開いて修正し保存した時の振る舞いの違いを説明したものです。</p>

<p><br />
YouTube再生はこちら ->  <a href="http://youtu.be/MsGvXasVJjk">Inventor製品のアセンブリファイルのフォルダー間コピー操作</a></p>

<p>YouTubeイメージ<br />
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07e4ed88970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07e4ed88970d img-responsive" alt="Blog-2015-02-02-No1" title="Blog-2015-02-02-No1" src="/assets/image_747179.jpg" /></a><br /></p>

<p>前者は意図しない振る舞いの結果となり、複写先のトップアセンブリファイルのみ修正され、含まれるコンポーネントファイルは複写元のファイル群が更新されるのに対し、後者は意図した振る舞いの結果であり、複写先のトップアセンブリファイル・コンポーネントファイル群全てが更新される事を説明したものです。</p>

<p>フォルダー内の全てのファイルを新しいフォルダーに全て複写するようなクローン化してハンドリングするようなAPIのカスタマイズを模索される場合は、QA-9485 を参考に くれぐれも必要に応じ適切に切り替えるような考慮を忘れないようにしてください。</p>

<p><a href="http://tech.autodesk.jp/faq/faq/adsk_result_dd.asp?QA_ID=9485">QA-9485　Inventorプロジェクト.ipjファイルを変更する方法</a></p>

<p>By Shigekazu Saito</p>
