---
layout: "post"
title: "Inventorのアセンブリファイル内の物理的なコンポーネントファイル名の変更"
date: "2015-02-09 01:30:00"
author: "Shigelazu Saitou"
categories:
  - "API カスタマイズ"
  - "デスクトップ製品"
original_url: "https://adndevblog.typepad.com/technology_perspective/2015/02/inventor_asmfile_rename.html "
typepad_basename: "inventor_asmfile_rename"
typepad_status: "Publish"
---

<p>先回は <a href="http://adndevblog.typepad.com/technology_perspective/2015/01/inventor_AsmFile_FolderCopy.html">Inventor製品のアセンブリファイルのフォルダー間コピー操作</a> として、アセンブリファイルの完全なクローン化を説明させていただきました。</p>

<p>今回は、アセンブリファイル内にコンポーネント配置されたサブアセンブリファイルやパーツファイルの「物理的な各ファイルの名前を変更」する方法を紹介したいと思います。</p>

<p><a href="http://">Inventor製品のアセンブリファイルのフォルダー間コピー操作</a> でも説明されていますが、Inventorのアセンブリファイルは、パーツファイルや他のアセンブリファイルをサブアセンブリとして、それぞれコンポーネントとして配置しています。<br />
これがトップアセンブリより見た場合、アセンブリ構成として Inventorの画面上に表示されており、Inventor 内の ブラウザ にて　ツリー構造で表示されています。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b7c747978e970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b7c747978e970b img-responsive" alt="AsmLink" title="AsmLink" src="/assets/image_637332.jpg" /></a><br /><br />
アセンブリファイル内に注目すると、アセンブリファイル内には配置された各コンポーネントの実体は存在していません。<br />
アセンブリ内には、各コンポーネントの「参照先情報」（＝リンク情報）のみが存在してファイル内に保存されています。</p>

<p>Inventor内で、トップアセンブリを開いた場合、このテーブルに類似した情報を精査し、コンポーネント（＝参照先のファイル）群が定められたフォルダー位置に存在するか否かを確認して開くメカニズムです。</p>

<p>意図的に削除されるなどして、コンポーネントの参照先が見つからない場合は、「リンク先の解決」警告ダイアログの出現と共に、リンク先ファイルの再設定の操作が可能な製品仕様です。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d0f201970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d0f201970c img-responsive" alt="LinkError" title="LinkError" src="/assets/image_402115.jpg" /></a><br /></p>

<p>仮に、アセンブリ内に配置されているコンポーネントの物理的なファイル名を変更する事を目的として、我々が通常使用しているWindowsのエクスプローラーなどを使用して、フォルダー上の物理的なファイル名を変更した場合、Inventorのファイル管理構造より、アセンブリファイル内に保存されているテーブルに類似した各コンポーネントの「参照先情報」（＝リンク情報）がマッチせず、必ず「リンク先の解決」警告ダイアログが出現する事になります。</p>

<p>コンポーネントで使用されている物理的なファイル名を変更する場合、物理的にクローンファイルを作成しておき、コンポーネントを置き換えする方法は存在します。</p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d0f206970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d0f206970c img-responsive" alt="QA9520ReplaceBefore" title="QA9520ReplaceBefore" src="/assets/image_67299.jpg" /></a><br /></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0d0f20a970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0d0f20a970c img-responsive" alt="QA9520ReplaceAfter" title="QA9520ReplaceAfter" src="/assets/image_339219.jpg" /></a><br /><br />
必要な場合は 以下の、QA-9520 を参考にしてください。</p>

<p><a href="https://knowledge.autodesk.com/community/article/79276">アセンブリファイル内のコンポーネントファイルの物理的なファイル名を変更する方法</a></p>

<p>By Shigekazu Saito</p>
