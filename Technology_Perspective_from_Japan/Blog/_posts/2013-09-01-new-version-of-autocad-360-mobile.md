---
layout: "post"
title: "AutoCAD 360 Mobile の新バージョンと 3D の再利用"
date: "2013-09-01 17:47:22"
author: "Toshiaki Isezaki"
categories:
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2013/09/new-version-of-autocad-360-mobile.html "
typepad_basename: "new-version-of-autocad-360-mobile"
typepad_status: "Publish"
---

<p>新しいバージョンの&#0160;AutoCAD 360 Mobile がリリースされました。といっても、今回の新バージョン 2.1 は、まだ iOS 用しか公開されていません。Android 版については現在作業中ですので、もうしばらくお待ちください。</p>
<p>さて、バージョン 2.1 の新機能ですが、いくつかの新機能や既存機能の改良が加えられています。</p>
<ul>
<li>Retina のフル サポートによる超高解像度の図面表示</li>
<li>PDF ファイルのサポートの向上による表示改善</li>
<li>メートル単位とフィート/インチ単位の簡単切り替え</li>
<li>アプリケーションの高速読み込みで時間節約</li>
<li>アプリケーション内通知と整理された共有機能により、コラボレーションがより便利に</li>
<li>[プロパティ]パレットの新しいプロパティ（AutoCAD 360 Mobile Pro のみ）</li>
</ul>
<p>新しいバージョンでまず感じるのは、起動時のスピードです。従来よりも起動が格段に早くなってるので、現場でイライラするようなことはないと思います。起動が遅いので、常に起動状態を維持してしまいがちの方もいらっしゃると思いますが、モバイル デバイスはまだまだメモリが少ないので、この改善はうれしいニュースになるはずです。</p>
<p>DWG ファイルを表示、簡易編集するツールである AutoCAD 360 Mobile で PDF ファイルへの機能改良が実施されることを意外に感じられるかも知れません。実は、PDF ファイルへの要望は、世界から寄せられていたものです。図面と言えば DWG ファイルや DXF ファイルを想定してしまいますが、図面を PDF ファイル化して現場に持ち出すことは、いまでも頻繁におこなわれている、ということなのだと思います。PDF ファイルの場合、AutoCAD 360 Mobile では表示するのみで、PDF ファイルに画層情報が埋め込まれていても、画層のオン、オフを制御するようなことはできませんので注意してください。<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff1f7568970c-pi" style="display: inline;"><img alt="2013-09-01 18-50-13" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff1f7568970c image-full" src="/assets/image_836272.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="2013-09-01 18-50-13" /></a></p>
<p>iOS 用の AutoCAD 360 Mobile は、<a href="https://itunes.apple.com/jp/app/autocad-360/id393149734?mt=8" target="_blank">https://itunes.apple.com/jp/app/autocad-360/id393149734?mt=8</a> から無償ダウンロードしていただくことができます。また、有償版の AutoCAD 360 Pro をお使いいただければ、無償版に加えて、さまざまな機能を利用することができます。詳しくは、<strong><a href="http://adndevblog.typepad.com/technology_perspective/2013/05/autocad-360.html" target="_blank">こちら</a></strong> をご参照ください。</p>
<p>さて、あまりよく知られていないようなのですが、AutoCAD 360 Mobile を使えば、AutoCAD で作成した 3D モデルを含む DWG ファイルを Autodesk 360 クラウドにアップロードすれば、そのまま 3D で表示させることができます。Web ブラウザを利用する AutoCAD 360 Web では、3D モデルを 3D で表示することは出来ません。</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff1faf2c970d-pi" style="display: inline;"><img alt="2013-09-01 18-51-24" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff1faf2c970d" src="/assets/image_229583.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="2013-09-01 18-51-24" /></a></p>
<p>AutoCAD 2012 以降では、モデル空間の 3D モデルをレイアウト上に投影して図面を作成する機能が存在しています。また、AutoCAD 2013 になって、断面図と詳細図の作図機能が追加されています。</p>
<p style="text-align: center;"><iframe frameborder="0" height="344" src="http://www.youtube.com/embed/q7OfTNnPZPk?feature=oembed" width="459"></iframe>&#0160;</p>
<p>AutoCAD 360 Mobile を利用すれば、この方法で作成したレイアウトを用いた 2D 図面だけでなく、部材の取り合いなどを現場で&#0160;3D 表示して確認できるようになります。モデル図面化の機能でレイアウト上に作成した各種投影ビューは、AutoCAD の EXPORTLAYOUT[レイアウトをモデルに書き出し] コマンドで変換してクラウドにアップロードしてみてください。</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff1f200b970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="2013-09-01 19-07-11" class="asset  asset-image at-xid-6a0167607c2431970b019aff1f200b970b" src="/assets/image_972007.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="2013-09-01 19-07-11" /></a><br /><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019aff1fb4cf970d-pi" style="display: inline;"><img alt="2013-09-01 19-17-45" border="0" class="asset  asset-image at-xid-6a0167607c2431970b019aff1fb4cf970d image-full" src="/assets/image_906209.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="2013-09-01 19-17-45" /></a></p>
<p>モバイルは設計のプロセスを確実に変えていける新しいデバイスです。このような 3D モデルとの共用も、ぜひお試しいただきたい機能です。</p>
<p>By Toshiaki Isezaki</p>
<p>&#0160;</p>
