---
layout: "post"
title: "Accessing \"unsubdivide\" functionality from a Mudbox plug-in"
date: "2013-06-04 05:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Mudbox"
  - "Plug-in"
  - "Qt"
original_url: "https://around-the-corner.typepad.com/adn/2013/06/accessing-unsubdivide-functionality-from-a-mudbox-plug-in.html "
typepad_basename: "accessing-unsubdivide-functionality-from-a-mudbox-plug-in"
typepad_status: "Publish"
---

<p>Unfortunately, the rebuild subdivision levels functionality isn’t directly exposed through the SDK, but it is possible to call any menu item in Mudbox (including this one) via Qt.</p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d0192aa6d5250970d-pi" style="display: inline;"><img alt="Mudbox-subdivide" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d0192aa6d5250970d" src="/assets/image_e2ae1f.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Mudbox-subdivide" /></a>
<p>So what you will want to do is:</p>
<pre class="brush: cpp; toolbar: false;">// build a list of geometries you wish to operate on.
QList&lt;mudbox::Geometry *&gt; aGeos ;  // you populate this list.

// get a pointer to the QMenu widget for the mesh menu
QMenu *pQ =Kernel ()-&gt;Interface ()-&gt;DropDownMenu (Interface::ddmMesh) ;

// Enumerate over the menu items. 
// Find the item that corresponds to the rebuild subd levels item
QList&lt;QAction *&gt; pL =pQ-&gt;actions () ;
QAction *pA =NULL ;
for ( int i =0 ; i &lt; pL.size () ; ++i ) {
    Kernel ()-&gt;Log (QString (&quot;action %1&quot;).arg (pL [i]-&gt;text ())) ;
    if ( pL [i]-&gt;text () == &quot;Rebuild Subdivision Levels&quot; ) {
        pA =pL [i] ;
        break ;
    }
}
if ( pA ) {
    for ( int i =0 ; i &lt; aGeos.size () ; ++i ) {
        // Set the active geometry. This indicates which geometry the rebuild command should operate on.
        Kernel ()-&gt;Scene ()-&gt;SetActiveGeometry (aGeos [i]) ;

        // trigger the rebuild subd menu item.
        pA-&gt;activate (QAction::Trigger) ;
    }
}
</pre>
<p>However, the DropDownMenu() method is only available in Mudbox 2014, so for previous releases you need to go with pure QT programming. Something like this:</p>
<pre class="brush: cpp; toolbar: false;">// build a list of geometries you wish to operate on.
QList&lt;mudbox::Geometry *&gt; aGeos ;  // you populate this list.

QWidget *pWidget =Kernel ()-&gt;Interface ()-&gt;MainWindow () ;
QMainWindow *pMW =dynamic_cast&lt;QMainWindow *&gt;(pWidget) ;
QAction *pA =NULL ;
if ( pMW ) {
    // get the menu bar of the main widget, and find the mesh menu.
    // QMenuBar doesn’t seem to expose an API for querying the menus
    // so inspect all of its children.
    QMenuBar *pMB =pMW-&gt;menuBar () ;
    QList&lt;QMenu *&gt; pMenus =pMB-&gt;findChildren&lt;QMenu *&gt;() ;//QString (&quot;Mesh&quot;)) ;
    for ( int i =0 ; i &lt; pMenus.size () &amp;&amp; pA == NULL ; ++i ) {
        if ( pMenus [i]-&gt;title ().contains (QString (&quot;Mesh&quot;), Qt::CaseInsensitive) ) {
            QMenu *pMenu1 =pMenus [i] ;
            QList&lt;QAction *&gt; pActions =pMenu1-&gt;findChildren&lt;QAction *&gt;() ;//QString (&quot;Rebuild Subdivision Levels&quot;)) ;
            for ( int j =0 ; j &lt; pActions.size () ; ++j ) {
                Kernel ()-&gt;Log (QString (&quot;action %1, %2&quot;).arg (i).arg (pActions [j]-&gt;text ())) ;
                if ( pActions [j]-&gt;text ().contains (QString (&quot;Rebuild Subdivision Levels&quot;), Qt::CaseInsensitive) ) {
                    pA =pActions [j] ;
                    break ;
                }
            }
        }
    }
}
if ( pA ) {
    for ( int i =0 ; i &lt; aGeos.size () ; ++i ) {
        // Set the active geometry. This indicates which geometry the rebuild command should operate on.
        Kernel ()-&gt;Scene ()-&gt;SetActiveGeometry (aGeos [i]) ;

        // trigger the rebuild subd menu item.
        pA-&gt;activate (QAction::Trigger) ;
    }
}
</pre>
<p>&#0160;</p>
