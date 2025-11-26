---
layout: "post"
title: "Fedora Core 20 gcc 4.8.2 Installation instructions"
date: "2015-04-22 00:00:00"
author: "Cyrille Fauvel"
categories:
  - "Autodesk"
  - "C++"
  - "Cyrille Fauvel"
  - "GCC"
  - "Linux"
  - "Maya"
  - "Tools"
original_url: "https://around-the-corner.typepad.com/adn/2015/04/fedora-core-20-gcc-482-installation-instructions.html "
typepad_basename: "fedora-core-20-gcc-482-installation-instructions"
typepad_status: "Publish"
---

<p>If you are trying to build and install gcc 4.8.2 from source code on Fedora Core 20 box, you will find lot of challenges to fix the dependencies. To fix that issue and to reduce the time here I have listed out the steps to be followed to get the successful build.</p>
<h2>Install build pre-requisites:</h2>
<p>Before installing gcc 4.8.2, install the build pre-requisites<br />1. yum groupinstall &quot;Development tools&quot;<br />2. yum install glibc-devel.i686 glibc-i686<br />3. yum install gcc-c++<br />4. yum install m4<br />5. yum install wget<br />6. Yum install zlib zlib-devel</p>
<h2>Download, build and install gcc pre-requisites</h2>
<p>1. wget -c https://gmplib.org/download/gmp/gmp-5.1.3.tar.bz2<br /> $ tar -jxf gmp-5.1.3.tar.bz2<br /> $ cd gmp-5.1.3/<br /> $ ./configure<br /> $ make<br /> $ make install</p>
<p>2. wget -c http://www.mpfr.org/mpfr-current/mpfr-3.1.2.tar.bz2<br /> $ tar -jxf mpfr-3.1.2.tar.bz2<br /> $ cd mpfr-3.1.2<br /> $ ./configure --with-gmp=~/gcc482/gmp-5.1.3<br /> $ make<br /> $ make install</p>
<p>3. wget -c http://www.multiprecision.org/mpc/download/mpc-1.0.1.tar.gz<br /> $ tar -zxf mpc-1.0.1.tar.gz<br /> $ cd mpc-1.0.1/<br /> $ ./configure --with-gmp=~/gcc482/gmp-5.1.3 --with-mpfr=~/gcc482/mpfr-3.1.2<br /> $ make<br /> $ make install</p>
<h2>Build gcc 4.8.2</h2>
<p>1. wget -c http://mirrors.ispros.com.bd/gnu/gcc/gcc-4.8.2/gcc-4.8.2.tar.bz2<br /> $ tar -jxf gcc-4.8.2.tar.bz2<br /> $ cd gcc-4.8.2<br /> $ ./configure --prefix=/opt/gcc482 --with-gmp=~/gcc482/gmp-5.1.3 --with-mpfr=~/gcc482/mpfr-3.1.2 --with-mpc=~/gcc482/mpc-1.0.1<br /> $ make<br /> $ su root<br /> $ make install</p>
<h2>Mapping gcc on the system</h2>
<p>$ cd /usr/bin<br /> $ ln -s /opt/gcc482/bin/gcc gcc482 <br /> $ ln -s /opt/gcc482/bin/g++ g++482</p>
<p>&#0160;</p>
<p>This article is a contribute from Vijay Prakash from the ADN team, Vijay is located in Bangalore, India and is one of our Maya expert.</p>
<p>&#0160;</p>
