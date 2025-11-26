---
layout: "post"
title: "Fusion 360 API：開発言語の選択"
date: "2015-11-03 23:54:53"
author: "Toshiaki Isezaki"
categories:
  - "API カスタマイズ"
  - "クラウドサービス"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/11/fusion-360-api-choose-development-language.html "
typepad_basename: "fusion-360-api-choose-development-language"
typepad_status: "Publish"
---

<p>Fusion 360 API を使ったスクリプトやアドインの開発では、JavaScript、Python、C++ の 3つの開発言語を選択して、スクリプトとアドインを開発することが出来ます。</p>
<p>どの言語を使っても、基本的に実装出来る内容は同じですが、プログラムを作成する際に利用するテキスト エディタやデバッグの方法、パフォーマンスなどに違いがあります。開発作業にあたっては、それらの違いを把握した上で、どの開発言語を利用するべきか選択することをお勧めします。</p>
<p><strong>JavaScript</strong>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e2ffc9970b-pi" style="float: right;"><img alt="JavaScript" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e2ffc9970b img-responsive" src="/assets/image_1001.jpg" style="margin: 0px 0px 5px 5px;" title="JavaScript" /></a></p>
<p>JavaScript は Web ブラウザを用いたクライアント アプリケーションの開発で利用される言語です。このため、HTML ファイルと一緒に作成されるのが一般的です。HTML ファイルは、Web ページ作成時にユーザが操作するインタフェースの役割を持たせますが、Fusion 360 では、この HTML ファイルをユーザ インタフェースとして利用することは出来ません、あくまで、拡張子 .js ファイルで定義される JavaScript ファイル本体を利用するのみです。.js ファイルを Fusion 360 にロードして利用することになります。</p>
<p>Fusion 360 には&#0160;Adobe Systems 社がオープンソース化して公開している Adobe Brackets が同梱されていて、JavaScript プログラムの編集に利用できます。</p>
<p>ただし、Fusion 360 上では、JavaScript で記述されたスクリプトやアドインは外部プロセスで実行されるため、Brackets 上で直接プログラムをデバッグすることができません。代替として、Google Chrome でプログラムのデバッグをおこなえるようになっています。Brackets と Chrome は、Fusion 360 がサポートする Windows と Mac で同じものを利用することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d16cd072970c-pi" style="display: inline;"><img alt="Javascript" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d16cd072970c image-full img-responsive" src="/assets/image_517072.jpg" title="Javascript" /></a></p>
<p>外部プロセスとは、Fusion 360 の実行に使用されているプロセスとは別に、独立した<a href="https://ja.wikipedia.org/wiki/%E3%83%97%E3%83%AD%E3%82%BB%E3%82%B9" target="_self"><strong>プロセス</strong></a>で実行されることを意味します。この場合、実行に使用されるメモリ領域も独立することになり、かつ、Fusion 360 プロセスと JavaScript 実行プロセスの間で<a href="https://ja.wikipedia.org/wiki/%E3%83%97%E3%83%AD%E3%82%BB%E3%82%B9%E9%96%93%E9%80%9A%E4%BF%A1" rel="noopener noreferrer" target="_blank"><strong>プロセス間通信</strong></a>が発生することになるので、実行スピードが少し遅くなります。&#0160;</p>
<p>次の動画は、JavaScript スクリプトの作成と、作成されたスケルトン コードの実行手順、および、デバッグの様子です。アドインの場合も、原則、同じ手順になります。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/ZOonJHs2l0g?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p><strong>Python <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e303f7970b-pi" style="float: right;"><img alt="Python-logo" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e303f7970b img-responsive" src="/assets/image_557956.jpg" style="width: 200px; margin: 0px 0px 5px 5px;" title="Python-logo" /></a><br /><br /></strong></p>
<p>Python は汎用プログラミング言語として開発された言語ですが、Web 開発で利用されることも多いようです。特徴として、プログラムの構造自体を簡素化できるよう、インデントを使った構造でスコープを定義できる点を上げることができます。</p>
<p>一般的なプログラミング言語では、1つの処理をプログラムを記述するために、記述スタイルの多様な書き方が可能ですが、Python の場合は、インデントをプログラミング構造の定義に組み込んでいるので、誰がコードを記述して、ほぼ同じになるように工夫されています。</p>
<p>Fusion 360 上では、Python プログラムは内部プロセスで動作されることになります。このため、Fusion 360 に同梱されているオープン ソースの Spyder &#0160;を使って、プログラム編集とデバッグの両方の作業をおこなうことが出来ます。Spyder は、JavaScript 利用時と同様に、Windows と Mac で利用することが出来ます。</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb0886d1cc970d-pi" style="display: inline;"><img alt="Python" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb0886d1cc970d image-full img-responsive" src="/assets/image_822252.jpg" title="Python" /></a></p>
<p>内部プロセスとは、実行時に利用されるプロセスが同じことを意味します。メモリ空間も共有されてプロセス間通信のオーバーヘッドも発生しないため、高速に Python プログラムを実行することが可能です。</p>
<p>次の動画は、Python スクリプトの作成、スケルトン コードの実行、デバッグの様子です。アドインの場合も、原則、同じ手順になります。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/F5WI8R-HGw0?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p><strong>C++ <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c7e303e4970b-pi" style="float: right;"><img alt="C++" class="asset  asset-image at-xid-6a0167607c2431970b01b7c7e303e4970b img-responsive" src="/assets/image_74416.jpg" style="margin: 0px 0px 5px 5px;" title="C++" /></a></strong></p>
<p>C++ 言語は、さまざまな場面で広く利用されている開発言語です。 この C++ を利用して、Fusion 360 のスクリプトやアドインを開発することが出来ます。</p>
<p>JavaScript や Python を使用する場合と異なり、Fusion 360 で C++ を使用する場合には、オープンソースで利用可能なプログラム 編集用のテキスト エディタが同梱されていない点に注意してください。</p>
<p>また、C++ は、<a href="https://ja.wikipedia.org/wiki/%E3%82%B3%E3%83%B3%E3%83%91%E3%82%A4%E3%83%A9" rel="noopener noreferrer" target="_blank"><strong>コンパイラ</strong></a>言語であるため、記述したプログラムをそのまま実行するのではなく、一旦、コンパイルして実行する必要があります。Windows と Mac では、コンパイルや実行環境のアーキテクチャが全く異なるため、プラットフォーム毎にプログラム編集やデバッグに利用するツールを変える必要があります。具体的には、Windows 上では、Micsoroft 社の Visual Studio、Mac 上では Apple 社の Xcode になります。&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d16cd24d970c-pi" style="display: inline;"><img alt="C++" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d16cd24d970c image-full img-responsive" src="/assets/image_922559.jpg" title="C++" /></a></p>
<p>C++ でプログラムしたスクリプトやアドインを Fusion 360 で実行させるため、前述のツールでコンパイルして<a href="https://ja.wikipedia.org/wiki/%E3%83%80%E3%82%A4%E3%83%8A%E3%83%9F%E3%83%83%E3%82%AF%E3%83%AA%E3%83%B3%E3%82%AF%E3%83%A9%E3%82%A4%E3%83%96%E3%83%A9%E3%83%AA" rel="noopener noreferrer" target="_blank"><strong>ダイナミック &#0160;リンク &#0160;ライブラリ</strong></a>にします（Windows では dll ファイル、Mac では dylib ファイル）。ダイナミック &#0160;リンク ライブラリは、当然、Fusion 360 と同じ内部プロセスで動作します。コンパイルしていることもあり、同じ内部プロセスで実行する Python プログラムよりも高速に実行することができます。</p>
<p>次の動画は、C++ スクリプトの作成、実行とデバッグの様子です（Windows）。アドインの場合も、原則、同じ手順になります。</p>
<p><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/wAZwQhO49-w?feature=oembed" width="500"></iframe>&#0160;&#0160;</p>
<p><strong>実行パフォーマンスの差</strong>&#0160;</p>
<p>ここまでの説明で、選択する開発言語によって実行プロセスに違いが存在することをご理解いただけたと思います。ここでは、具体的な実行パフォーマンスの差を収録していますので、次の動画をご確認ください。ここでは、Fusion 360 に含まれている&#0160;SpurGear スクリプトサンプルで比較しています。</p>
<p>&#0160;<iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/ZDFuFtIZZSY?feature=oembed" width="500"></iframe>&#0160;</p>
<p>プログラム自体の長さや処理内容にもよりますが、この程度のコードだと、体感的にほとんど差を感じません。厳密には、C++ → Python → JavaScript の順で C++ が最も高速なはずです。</p>
<p><strong>ソースコードの隠蔽</strong></p>
<p>作成したスクリプトやアドインを他のユーザに利用してもらうことを考えると、場合によって、問題になるケースがあります。それらを販売するケースです。</p>
<p>JavaScript と Python で作成されたスクリプトとアドインは、配布先でもソースコード（プログラム自体） を参照して編集することが出来てしまいます。社内用途で利用する場合には、ソースコードが見られてしまっても問題ありませんが、有償販売した場合には、プログラム内の処理内容やノウハウを知られて他に流用されてしまう事が考えられます。</p>
<p>このような流出を抑止出来るのが、C++ 言語です。C++ では、必ずソースコードをコンパイル する必要があります。また、コンパイルされた dll ファイル（Windows）や&#0160;dylib ファイル（Mac）は、<a href="https://ja.wikipedia.org/wiki/%E3%83%90%E3%82%A4%E3%83%8A%E3%83%AA" rel="noopener noreferrer" target="_blank"><strong>バイナリ形式</strong></a>になっているので、記述したプログラムは判読できなくなっています。コンパイル済みの dll ファイルや dylib ファイルのみを配布すれば、プログラム内容を隠蔽したまま、機能を利用することが可能なわけです。</p>
<p><strong>補足：</strong></p>
<p>概要部分の内容が重複することになりますが、過去にも Fusion 360 API に触れたブログ記事をポストしていますので、参考までに、リンクをご案内しておきます。</p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/09/fusion-360-api.html" rel="noopener noreferrer" target="_blank">Fusion 360 と API Tech Preview</a></strong></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2014/11/fusion-360-update-and-fusion-360-ultimate.html" rel="noopener noreferrer" target="_blank">Fusion 360 の更新と Fusion 360 Ultimate</a></strong></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/fusion-360-addin-creation.html" rel="noopener noreferrer" target="_blank">Fusion 360 アドイン作成</a></strong></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/menu-customize-on-fusion-360-qat.html" rel="noopener noreferrer" target="_blank">Fusion 360 のメニューカスタマイズ ～ Quick Access Toolbar</a></strong></p>
<p><strong><a href="http://adndevblog.typepad.com/technology_perspective/2015/03/menu-customize-on-fusion-360-workspace.html" rel="noopener noreferrer" target="_blank">Fusion 360 のメニューカスタマイズ ～ Workspace</a></strong></p>
<p>By Toshiaki Isezaki&#0160;</p>
