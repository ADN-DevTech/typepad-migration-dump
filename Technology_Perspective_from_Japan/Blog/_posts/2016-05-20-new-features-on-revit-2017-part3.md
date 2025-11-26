---
layout: "post"
title: "Revit 2017 の新機能 ～ その3"
date: "2016-05-20 02:24:22"
author: "Ryuji Ogasawara"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2016/05/new-features-on-revit-2017-part3.html "
typepad_basename: "new-features-on-revit-2017-part3"
typepad_status: "Publish"
---

<div>前回は、<a href="http://adndevblog.typepad.com/technology_perspective/2016/05/new-features-on-revit-2017-part2.html">Revit 2017 の建築分野の新機能と更新内容、API の対応状況</a>について解説致しました。</div>
<div>&#0160;</div>
<div>立面図や断面図ビューでグラデーションを用いて奥行きを表現できる「デプス キューイング」機能や、ホストされた手摺の作成機能、壁接合部のオプション設定など、ぜひご活用ください。</div>
<div>&#0160;</div>
<div>今回は、構造エンジニアリング分野の新機能についてご紹介していきます。</div>
<div><strong>&#0160;</strong></div>
<div><span style="font-size: 14pt;"><strong>構造接合</strong></span></div>
<div>Revit 2017 新機能の構造接合は、梁、柱、ブレースなど複数要素の接合方法を指定する構造接合を配置することができるようになりました。これにより、接合に関する情報を保持して、要素間の関係を設定することができます。</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/3YNjNOpDrx4?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>Revit 構造接合は、接合に関する情報を、エンジニアやディテーラー、製造者間でやり取りすることを目的としています。このデータはプロジェクト内に保存されます。</div>
<div>&#0160;</div>
<div>接続に関する詳細情報には次の情報を含めることができます。</div>
<ul>
<li>接合のタイプとスタイル</li>
<li>詳細なジオメトリ</li>
<li>解析荷重とデータ</li>
<li>マテリアル</li>
<li>製造者 ID</li>
</ul>
<div><span style="font-size: 14pt;"><strong>一般接合</strong></span></div>
<div>Revit 構造接合は、標準機能として、システムファミリ「一般接合」を適用することができます。この一般接合は、鉄骨接合を記号で表現するもので、図面に表示することができます。</div>
<div>&#0160;</div>
<div>パラメータを使用して、接合に関する情報(図、接合要件へのリンクなど)を入力することができます。</div>
<div>承認/コード チェック ステータス パラメータに基づいて色分けし、確認/承認ワークフローを容易にすることができます。</div>
<div>また必要に応じて、アドイン「Steel Connections for Revit 」を使用して作成した詳細な鉄骨接合と置き換えることができます。</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c85f9908970b-popup" onclick="window.open( this.href, &#39;_blank&#39;, &#39;width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0&#39; ); return false" style="display: inline;"><img alt="StructuralConnection01" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b7c85f9908970b image-full img-responsive" src="/assets/image_645196.jpg" title="StructuralConnection01" /></a></div>
<div>&#0160;</div>
<div>結合される要素と接合される要素が円で表示されます。円からは、接合された要素まで線分セグメントが表示されます。</div>
<div>接合によっては、製造意図に合わせて正しくジオメトリを作成できるようにするために、接合内のメイン要素を円コントロールで割り当てることができます。</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>構造接合アドイン「Autodesk&#0160;</strong><strong>Steel Connections for Revit</strong> <strong>」</strong></span></div>
<div>さらに Revit 2017 では、サブスクリプションメンバーのお客様向けに、パラメトリックな構造接合のアドイン「Autodesk Steel Connections for Revit」がリリースされました。</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/JebKpiz60zU?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>このアドインを使用すると、さまざまな接合タイプをプロジェクトにロードして、一般接合を接合タイプに割り当て、詳細レベルの表示モードで構造接合のジオメトリを表示したり、モデリング環境で構造接合要素のパラメータを修正することができます。&#0160;</div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09032be2970d-pi" style="display: inline;"><img alt="StructuralConnection02" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09032be2970d image-full img-responsive" src="/assets/image_751886.jpg" title="StructuralConnection02" /></a></div>
<div>&#0160;</div>
<div><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb09032c24970d-pi" style="display: inline;"><img alt="StructuralConnection04" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01bb09032c24970d image-full img-responsive" src="/assets/image_562702.jpg" title="StructuralConnection04" /></a><br />&#0160;</div>
<div>ただし、構造接合を配置できるのは、新しい構造フレーム ファミリ(Revit 2017 で提供)のみで、現在のところ、下記ページにてリストされている認定ファミリが対象となります。</div>
<div>&#0160;</div>
<div>鉄骨の接合の認定ファミリ</div>
<div><a href="http://help.autodesk.com/view/RVT/2017/JPN/?guid=GUID-6244E741-5D14-4DAD-AE25-5069F71B69F3">http://help.autodesk.com/view/RVT/2017/JPN/?guid=GUID-6244E741-5D14-4DAD-AE25-5069F71B69F3</a></div>
<div>&#0160;</div>
<div>構造接合の API については、StructuralConnectionHandler クラス、StructuralConnectionHandlerType クラス、StructuralConnectionApprovalType クラス、StructuralConnectionSettings クラスが追加されました。</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>柱の分割</strong></span></div>
<div>ワークフローを改善して製造モデルの操作性を高めるため、[要素を分割]ツールでは垂直柱を指定するポイントで分割できるようになりました。&#0160;</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/DmqJsyGhOTc?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>柱の下部のアタッチメント</strong></span></div>
<div>[アタッチ(上部/下部)]コマンドにより、構造柱を独立基礎にアタッチできるようになりました。基礎高さを調整すると、柱の長さが適切に調整されます。</div>
<div>&#0160; <a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d1e9753c970c-pi" style="display: inline;"><img alt="Column_attach_basement" border="0" class="asset  asset-image at-xid-6a0167607c2431970b01b8d1e9753c970c image-full img-responsive" src="/assets/image_462779.jpg" title="Column_attach_basement" /></a></div>
<div><strong>&#0160;</strong></div>
<div><span style="font-size: 14pt;"><strong>鉄筋カプラー（鉄筋継手）</strong></span></div>
<div>鉄筋モデリングおよびドキュメント作成を改善するために、鉄筋継手の機能が追加されました。</div>
<div>鉄筋継手はファミリベースのためカスタマイズ可能で、各チーム メンバーはニーズに合わせて使用できます。</div>
<div>&#0160;</div>
<div>鉄筋継手は接続先の鉄筋と連携して変更管理を行います。また一意の継手番号を使用して、グループ、アセンブリ、パーティションに追加できます。さらに、継手をさまざまな形式で書き出すこともできます。</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/ig6mId_z9ik?feature=oembed" width="500"></iframe>&#0160;</div>
<div><strong>&#0160;</strong></div>
<div>API では、RebarCoupler クラスと RebarReinforcementData クラスが追加され、それに関連して列挙体やメソッドが更新されました。</div>
<div><span style="font-size: 14pt;"><strong>&#0160;</strong></span></div>
<div><span style="font-size: 14pt;"><strong>曲げメッシュ筋</strong></span></div>
<div>メッシュ筋を配置する際、ざまざまな形状のコンクリート ホストに合わせて、曲げメッシュ筋をスケッチできるようになりました。これらは変更内容に合わせて後で編集、調整できます。</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/fb0CKNGTRGk?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div><span style="font-size: 14pt;"><strong>さまざまな鉄筋形状に対応</strong></span></div>
<div>複雑なコンクリート要素形状に対して、傾斜面に沿って段階的に変化する鉄筋セットの形状を作成し、 正確な配筋を設定します。複数の鉄筋注釈、カスタマイズ可能な番号付け設定、変化する長さを表示する正確な集計表を使用することができます。形状の変わる鉄筋セットを設定できます。</div>
<div>&#0160;</div>
<div><iframe allowfullscreen="" frameborder="0" height="281" src="https://www.youtube.com/embed/aFHFYxDrhHE?feature=oembed" width="500"></iframe>&#0160;</div>
<div>&#0160;</div>
<div>
<div>次回は、MEP 分野の機能強化についてご紹介いたします。</div>
<div>&#0160;</div>
<div>By Ryuji Ogasawara</div>
</div>
