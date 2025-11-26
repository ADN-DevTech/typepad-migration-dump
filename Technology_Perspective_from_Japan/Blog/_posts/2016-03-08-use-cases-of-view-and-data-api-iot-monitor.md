---
layout: "post"
title: "View and Data API の利用例 － IoT モニター"
date: "2016-03-08 00:19:00"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/03/use-cases-of-view-and-data-api-iot-monitor.html "
typepad_basename: "use-cases-of-view-and-data-api-iot-monitor"
typepad_status: "Publish"
---

<p><span style="background-color: #ffff00;">ご注意：View and Data API は2016年6月に Viewer と &#0160;Model Derivative API に分離、及び、名称変更されました。</span></p>
<p>サンフランシスコのフェリーターミナル近くには、埠頭が幾つか存在しています。オートデスクは、第9埠頭にあたる場所にワークショップを持っています。&#0160;</p>
<p><iframe allowfullscreen="" frameborder="0" height="450" src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3152.576185488322!2d-122.40019308388203!3d37.79997091848077!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x8085805e5f4555e5%3A0xa666e35a24587549!2zUGllciA5LCBTYW4gRnJhbmNpc2NvLCBDQSA5NDExMSDjgqLjg6Hjg6rjgqvlkIjooYblm70!5e0!3m2!1sja!2sjp!4v1456637098624" style="border: 0;" width="600"></iframe></p>
<p>通称、<a href="http://www.autodesk.com/pier-9" target="_blank"><strong>Pier 9</strong> </a>と呼ばれているこのワークショップは、3D プリンタや CNC 工作機械、レーザーカッターやウォータージェット カッターなど、オートデスク製品でデザインされたモノを製作する場として利用されています。いわば、<strong><a href="http://www.autodesk.co.jp/fomt" target="_blank">The&#0160;Future Of Making Things</a></strong>&#0160;を実証するための工房です。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/RcTHxE2eJ9A?feature=oembed" width="500"></iframe>&#0160;</p>
<p>折りしも、<strong><a href="https://ja.wikipedia.org/wiki/%E3%82%A4%E3%83%B3%E3%83%80%E3%82%B9%E3%83%88%E3%83%AA%E3%83%BC4.0" target="_blank">Industry 4.0/インダストリー 4.0</a></strong> に注目が集まる中、Pier 9 に設置された工作機械に<a href="http://adndevblog.typepad.com/technology_perspective/2015/08/about-internet-of-things.html" target="_blank"> IoT （Internet Of Things）</a>デバイスを設置して、監視するアイデアが実現されています。使用されているセンサーは&#0160;Texas Instruments &#0160;社の&#0160;<a href="http://www.tij.co.jp/tool/jp/cc2650stk?keyMatch=CC2650STK&amp;tisearch=Search-JP-Everything" target="_blank">CC2650STK</a>&#0160;というセンサータグで、光、デジタル・マイク、磁気センサ、湿度、圧力、加速度計、ジャイロスコープ、磁力計、物体の温度、周囲温度の検出が可能な 10 個のセンサーが搭載されていて、たったの$ 29（米ドル）です。</p>
<p>Revit デモでリングされた Pier 9 建屋の 3D モデルは、当然、社内で入手できるので、<a href="http://adndevblog.typepad.com/technology_perspective/2015/09/about-view-and-data-api.html" target="_blank">View and Data API</a> でストリーミング配信してWeb ブラウザで表示させることが可能です。また、CC2650STK というセンサーから入手したリアルタイムデータを、<a href="https://developers.google.com/chart/interactive/docs/reference" target="_blank">Google Visualization API</a> で定義したゲージを使って表示する実装が施されています。&#0160;</p>
<p>&#0160;<iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/SEuPo4_6rf4?feature=oembed" width="500"></iframe>&#0160;</p>
<p>さて、実際のサイトは <a href="http://pier9.autodesk.io/" target="_blank" title="http://pier9.autodesk.io/"><strong>Pier 9 IoT Viewer</strong>&#0160;</a>から参照いただけます。実際に選択した工作機械が稼動していれば、温度やツールパスの座標値をゲージが動いて表示されるはずです。A360 Viewer にはないツールバー ボタンも含め、View and Data API を使うと、このようなカスタマイズも可能です。ぜひ、一度、確認してみてください。</p>
<p>もちろん、WebGL 対応の Web ブラウザがあれば、スマートフォンやタブレットでも同じように工作機械の稼動状態を把握することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c81f9c4c970b-pi" style="display: inline;"><img alt="Iphone6_pier9" class="asset  asset-image at-xid-6a0167607c2431970b01b7c81f9c4c970b img-responsive" src="/assets/image_88987.jpg" title="Iphone6_pier9" /></a></p>
<p>By Toshiaki Isezaki</p>
