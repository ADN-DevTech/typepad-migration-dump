---
layout: "post"
title: "Maya to be included in One LISP campaign"
date: "2016-04-01 05:09:47"
author: "Cheng Xi Li"
categories:
  - "Cheng Xi Li"
  - "Maya"
original_url: "https://around-the-corner.typepad.com/adn/2016/04/maya-to-be-included-in-one-lisp-campaign.html "
typepad_basename: "maya-to-be-included-in-one-lisp-campaign"
typepad_status: "Publish"
---

<p>We are moving towards the cloud nowadays. I've heard some news from 3ds Max guys and they are going to add JavaScript support in the near future. It is wonderful for cloud products like A360 Cloud rendering services in 3ds Max. For the desktop products, we have some a similar movement in Maya.</p>  <p>There are lots of programming languages for Autodesk developers today, like MAXScript, MEL, JavaScript, Python, C++, etc. But above all, AutoLISP should be most important, it is introduced at very beginning and thrives in the AutoCAD product line.</p>  <p>Today, to make it easier to develop plug-ins for our different products, we decide to launch a One LISP campaign to make AutoLISP API available across our whole production line.</p>  <p>The first product going to add LISP support is Maya and it is coming in our next major release. Let's take a sneak preview into it.</p>  <pre>(ns MayaLispPlugin
    (:require [clojure.core])   
    (:gen-class 
        :name MayaLispPlugin.helloWorldCmd
        :extends Autodesk.Maya.OpenMaya.MPxCommand
        :implements [Autodesk.Maya.OpenMaya.IMPxCommand]        
        :main false             
        )
    )

(defn -doIt [this args] 
    (Autodesk.Maya.OpenMaya.MGlobal/displayInfo &quot;Hello World!\n&quot;))</pre>

<p>It is a helloWorldCmd sample in LISP:</p>

<p><img alt="Brief" src="/assets/helloWorld.png" /></p>

<p>Feel excited about the new toy? Don't forget to check about the Javascript story on the <a href="http://getcoreinterface.typepad.com/">3ds Max SDK blog!</a>.</p>

<p>For more details, please check out our press release <a href="https://en.wikipedia.org/wiki/April_Fools%27_Day">Autodesk launches One LISP campaign</a>.</p>
