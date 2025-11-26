---
layout: "post"
title: "Avoid Gimbal Lock for Rotation/Direction Maya Manipulators"
date: "2012-08-10 00:00:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "Python"
  - "UI"
original_url: "https://around-the-corner.typepad.com/adn/2012/08/avoid-gimbal-lock-for-rotationdirection-maya-manipulators.html "
typepad_basename: "avoid-gimbal-lock-for-rotationdirection-maya-manipulators"
typepad_status: "Publish"
---

<p>In the past few weeks, my colleague Naiqi Weng had to work on an issue where someone had a Maya manipulator that contains both a rotateManip and a directionManip, which controlled one single “rotate” attribute. The manipulator was working fine excepted under some specific rotations where the directional manipulator &quot;pops&quot; the object&#39;s rotation. She noticed that when this was happening there was two axes on top of each other when looking at the rotateManip and the rotateMode was in &quot;Gimbal&quot; mode, which would mean that this was happening due to gimbal lock.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017743e0b7ab970d-pi" style="display: inline;"><img alt="Gimbal" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017743e0b7ab970d" src="/assets/image_d26ec1.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Gimbal" /></a></p>
<p>Having more than one manipulators control one single attribute is not a recommended way to work with manipulators.</p>
<p>Gimbal lock is the loss of one&#0160;<a href="http://en.wikipedia.org/wiki/Degree_of_freedom_(mechanics)">degree of freedom</a>&#0160;in a three-dimensional space that occurs when the axes of two of the three&#0160;<a href="http://en.wikipedia.org/wiki/Gimbal">gimbals</a>&#0160;are driven into a parallel configuration, &quot;locking&quot; the system into&#0160;<a href="http://en.wikipedia.org/wiki/Rotation">rotation</a>&#0160;in a degenerate two-dimensional space. The only way to avoid gimbal lock is to use quaternion instead of euler to represent rotations.</p>
<p>In this specific situation, unless both rotate manip and direction manip use quaternion, the gimbal lock behavior can NOT be avoided. Quaternion can uniquely identify a rotation, but when it is converted into euler rotation, it loses one degree of freedom information. Let’s say you have rotated your object with your rotate manipulator, and you used the quaternion rotation. Now you want your direction manip to follow the rotate manipulator rotates, you convert the quaternion to euler rotation and get an angle of what the current direction manip should be, however you will never tell whether this direction manip should rotation from original direction to this angle from clock wise, or counter clock wise, because either way, you get the same end result, which is the current direction manip location.</p>
<p><a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017743e0b713970d-pi" style="display: inline;"><img alt="Gimbal_locks" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017743e0b713970d image-full" src="/assets/image_e99a4b.jpg" title="Gimbal_locks" /></a><br />Quaternion is a 4D representation which represents 3D rotation, that’s why it is sufficient to avoid any ambiguities, while euler is 3D representation and the gimbal lock ambiguity cannot be removed in this representation.&#0160;Quaternion is a data type suitable for defining object orientation and rotations. Quaternions are easier to work with than matrices and using quaternions helps to avoid gimbal lock problem like in case of Euler angles usage.&#0160;</p>
<p>Tasks like smooth interpolation between three-dimensional rotations and building rotation by vector are fairly simpler to solve with quaternions than with Euler angles or matrices. Industrial grade inertial trackers and many other orientation sensors can return rotational data in quaternion form, also to avoid gimbal lock problem, and make such values easier to filter by interpolation.</p>
<p>Unfortunately the Maya manipulator API are not exposed for quaternions yet. For now, the only workaround would be to track the rotation direction of the objects manually.</p>
