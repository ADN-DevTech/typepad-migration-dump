---
layout: "post"
title: "Inventor Apperentice Server 2026 利用時の注意点"
date: "2025-07-14 01:07:24"
author: "Takehiro Kato"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2025/07/note-for-when-in-use-inventor-apperentice-server-2026.html "
typepad_basename: "note-for-when-in-use-inventor-apperentice-server-2026"
typepad_status: "Publish"
---

<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02e860eee2cd200b-pi" style="display: inline;"><img alt="Title" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02e860eee2cd200b img-responsive" src="/assets/image_463786.jpg" title="Title" /></a></p>
<p>ブログ記事：<a href="https://adndevblog.typepad.com/technology_perspective/2025/05/inventor-2026-api-update.html">Inventor 2026 API関連情報</a>にてご案内をしましたが、Inventor 2026よりInventor Apperentice Serverの配布方法に大きな変更が加えられておりますが、この変更がInventor Apperentice Serverを用いたカスタマイズを開発されている方にとって、どのような影響があるのかが判りにくいと思われます。</p>
<p>この記事では、Inventor 2026でのInventor Apperentice Serverの利用方法での留意点についてご案内をいたします。</p>
<p>&#0160;</p>
<h3>Inventor 2026での変更点</h3>
<p>では、Inventor 2026のInventor Apperentice Serverは何が変わったのでしょうか？以前の記事<a href="https://adndevblog.typepad.com/technology_perspective/2025/05/inventor-2026-api-update.html">Inventor 2026 API関連情報</a>でも記載しましたが、従来よりInventor Apprentice ServerはInventor Professionalと共にインストールがされておりましたが、Inventor 2026よりInventor Professionalと一緒にインストールされるInventor Apprentice Serverはレジストリフリー化が行われております。</p>
<p>このInventor 2026 Professionalと共にインストールされるInventor Apprentice Serverコンポーネントは、カスタムアプリケーションからは利用することが出来ません。このためInventor 2026のInventor Apperentice Serverを利用する場合は、<a href="https://www.autodesk.com/jp/support/technical/article/caas/tsarticles/tsarticles/JPN/ts/7E5yOOlf0n3RxRfXa3F1to.html?_gl=1*1lh54cz*_gcl_au*MTk2MzA2MzE5OC4xNzUwMjI0MDY4*_ga*NDI4NTQzNTk0LjE2NzY4NTQ5MTU.*_ga_NZSJ72N6RX*czE3NTI0NTk2NDEkbzU2NCRnMSR0MTc1MjQ1OTkxMiRqNTgkbDAkaDA.">ダウンロードサイト</a>からインストーラを取得し、インストールをしたコンポーネントを利用する必要があります。</p>
<p>この個別のインストーラでインストールしたApperentice Serverは従来通りの、レジストリ登録を行うCOMコンポーネントのため、VBA等からもご利用が可能です。</p>
<p>またこの変更に伴い、Inventor Professional 2026ではInventorの起動時にInventor Apperentice Serverのレジストリ登録を行わないよう動作が変更がされております。これはInventor 2026 ProfessionalがインストールするInventor Apperentice ServerのCOMコンポーネントがレジストリフリー版(＝レジストリ登録が不要であるCOMコンポーネント)であるためです。</p>
<p>&#0160;</p>
<h3>注意が必要なポイント</h3>
<p>ここまでを読まれて、「Inventor 2026からは、Apperentice Serverを別にインストールする必要があるんだ～」ぐらいの変更と思われた方もいらっしゃるかもしれませんが、実はインストール後のカスタマイズの開発、実行時にも注意が必要な点があります。</p>
<p>あまり意識をされていない方が多いかと思いますが、実はInventor 2025以前の場合、Inventorは起動時にInventor Apperentice Serverのレジストリ登録を更新しておりました。このため、複数のInventorのバージョンが混在している環境では、最後に起動したInventorバージョンのApperentice Serverが有効となっていました。</p>
<p>ところが、Inventor 2026からはInventorの起動時にApperentice Serverのレジストリ登録を更新しなくなったため、例えば以下の様な状況が発生することとなります。</p>
<h4>・インストール環境</h4>
<p>Inventor Pro 2025<br />Inventor Pro 2026<br />Standalone Apprentice Server 2026</p>
<h4>・処理手順</h4>
<p>1．Inventor 2025を起動する⇒この時点で、Apprentice Server 2025がアクティブとなる（＝レジストリ登録される）</p>
<p>2．Invenotr 2026を起動する⇒Inventor Apprentice Server 2026のレジストリ登録はされないため、Apprentice Server 2025がアクティブな状態<br /><br /></p>
<p>この状態の場合、「Visual StudioでApprentice Server 2026のタイプライブラリを参照しても、Apprentice Server 2025のタイプライブラリを参照してしまう」「Apprentice Serverを利用するカスタムアプリケーションを実行すると、Apprentice Server 2026を使いたいのに2025が使われてしまう」といった現象が発生いたします。</p>
<p>この状況でInventor Apprentice Server 2026を利用したい場合にはInventor 2026の起動ではなく、以下のコマンドを実行してApprentice Server 2026のコンポーネントのレジストリ登録を行う必要があります（インストールパスが異なる場合はパスを変更してください）。</p>
<p>&#0160;</p>
<p>&quot;C:\Program Files\Autodesk\Inventor Apprentice Server 2026\Bin\ApprenticeRegSvr.exe&quot; /install</p>
<p>&#0160;</p>
<p>これまで、Inventor Apprentice Serverを用いたカスタマイズを開発・利用されていた方の中には、経験的に「利用したいバージョンのInventorを起動してからApprentice Server を利用するカスタマイズを実行する」といった事をされていた方がいらっしゃるかもしれませんが、Inventor 2026以降では明示的にInventor Apprentice Server 2026のレジストリ登録を行う必要があることになります。</p>
<p>&#0160;</p>
<p>複数バージョンのInventorが混在する環境で、Inventor Apprentice Serverを用いたカスタマイズを開発・利用される場合は、この点にご留意ください。</p>
<p>&#0160;</p>
<p>By Takehiro Kato</p>
