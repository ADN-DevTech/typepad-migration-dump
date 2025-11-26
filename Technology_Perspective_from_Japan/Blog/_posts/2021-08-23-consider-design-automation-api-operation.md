---
layout: "post"
title: "Design Automation API for AutoCAD 運用の考察"
date: "2021-08-23 00:02:18"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2021/08/consider-design-automation-api-operation.html "
typepad_basename: "consider-design-automation-api-operation"
typepad_status: "Publish"
---

<p>今回は Design Automation API for AutoCAD の運用に焦点を当てたご案内をしたいと思います。</p>
<p>Design Automation API は、クラウド上でユーザ インタフェースのない軽量なコアエンジン（AutoCAD、Revit、Inventor、3ds Max）を実行させて、アドインをロード、実行することで、ファイルなどの成果物を得る、といった API です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b027880433f9d200d-pi" style="display: inline;"><img alt="Da" border="0" class="asset  asset-image at-xid-6a0167607c2431970b027880433f9d200d image-full img-responsive" src="/assets/image_369776.jpg" title="Da" /></a></p>
<p>それでは、Design Automation API for AutoCAD を利用する利点とは何でしょう？</p>
<p>Web・クラウド時代に生産性向上や自動化を目指すにあたり、オンプレミス環境（社内サーバー）の AutoCAD 利用から、クラウドで稼働する Design Automation API 利用に切り替えただけでは、デジタル トランスフォーメーションの利点は、あまり感じられないかもしれません。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788041fa8a200d-pi" style="display: inline;"><img alt="Da1" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788041fa8a200d image-full img-responsive" src="/assets/image_233836.jpg" title="Da1" /></a></p>
<p>一律に論じることは出来ませんが、デジタル トランスフォーメーションの成果は、デジタル技術を利用したワークフローの見直しを目標としたほうが、新しい価値を見出せるように感じます。例えば、Design Automation API&#0160; で得られた成果物をダウンロードして紙に印刷・出図手続きを経て社外とコミュニケーションする、といった従来のものではなく、デジタルに配信、コラボレーションするような発展的思想を同時に導入する、といった具合です。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02788041faa1200d-pi" style="display: inline;"><img alt="Da2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02788041faa1200d image-full img-responsive" src="/assets/image_29549.jpg" title="Da2" /></a></p>
<p>さて、この Design Automation API 環境は、他の Forge API と同じくオートデスクが AWS に構築している開発プラットフォームです。Forge を利用するアプリケーション（Forge アプリ）は、通常、Web サーバーにホストされる Web アプリとして動作することになります。この場合、Forge アプリを利用するユーザは、クライアントとなるデバイスから Web ブラウザを使って様々なリクエストを送信し、Forge アプリからのレスポンスを得ながら特定のタスクを実行していくことになります。</p>
<p>この際、伝送路として使用するのは、言うまでもなく、公衆回線を使ったインターネットです。このため、「今日はページの表示が早い」、「昨日より遅い」、など、アクセスする時間帯や状況によって通信に時間がかかってしまう可能性が考えられます。このようなインターネット通信によるレスポンスの差は、結果として、Design Automation API の Web アプリ部分でパフォーマンス差として現れることになってしまいます。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b026bdeea132f200c-pi" style="display: inline;"><img alt="Internet" border="0" class="asset  asset-image at-xid-6a0167607c2431970b026bdeea132f200c image-full img-responsive" src="/assets/image_110669.jpg" title="Internet" /></a></p>
<p>もう一つ、アドイン部分の実行環境にも、ご承知いただきたい点があります。</p>
<p>Design Automation API のアドイン処理も含め、CPU リソースを利用する演算サービスの場合、クラウドの自動伸張機能（elastic computing）の特性も理解していただきたい点です。具体的には、Design Automation API は、リクエスト数に応じて、実行環境となる仮想マシンを動的に増減します。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab9ec5200b-pi" style="display: inline;"><img alt="Elastic2" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ab9ec5200b image-full img-responsive" src="/assets/image_293238.jpg" title="Elastic2" /></a></p>
<p>もし、非常に短い間に数多くにリクエストが集中した場合（スパイク）、必要数の仮想マシン展開に時間がかかり、処理完了までに「通常」より時間を要してしまう場合もあります。オートデスクは、もちろん、このような遅延を低減するための投資を続けています。</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b02c8d3ab9ec7200b-pi" style="display: inline;"><img alt="Elastic3" border="0" class="asset  asset-image at-xid-6a0167607c2431970b02c8d3ab9ec7200b image-full img-responsive" src="/assets/image_791883.jpg" title="Elastic3" /></a></p>
<p>パブリック クラウドのインフラ（AWS）上にマルチテナント システムとして構築されている Forge は、世界中の Forge デベロッパとの共有リソースであるとの認識も必要かと思います。（もちろん、ストレージ内容が他者から勝手に見られてしまう、という意味ではありません。）</p>
<p>デジタルトランスフォーメーション成功のカギは、ワークフローの見直しから生まれる新しい価値の創出・発見にあるはずです。そんな観点で運用を捉えていただければと思います。</p>
<p>By Toshiaki Isezaki</p>
