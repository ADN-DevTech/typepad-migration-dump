---
layout: "post"
title: "Get Roll value of [Edit Current Viewpoint]"
date: "2019-07-28 01:19:09"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2019/07/get-roll-value-of-edit-current-viewpoint.html "
typepad_basename: "get-roll-value-of-edit-current-viewpoint"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>There is a parameter roll in UI [Edit Current Viewpoint]. It means rotating the camera around its front-to-back axis. A positive value rotates the camera counterclockwise, and a negative value rotates it clockwise.</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4713d16200c-pi"><img alt="Screen Shot 2019-07-28 at 3.45.34 PM" class="asset  asset-image at-xid-6a0167607c2431970b0240a4713d16200c img-responsive" src="/assets/image_139850.jpg" style="display: block; margin-left: auto; margin-right: auto; border: 1px  #000000;" title="Screen Shot 2019-07-28 at 3.45.34 PM" /></a></p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a4bf1e41200b-pi"><img alt="Screen Shot 2019-07-28 at 3.45.53 PM" class="asset  asset-image at-xid-6a0167607c2431970b0240a4bf1e41200b img-responsive" src="/assets/image_320954.jpg" style="display: block; margin-left: auto; margin-right: auto; border: 1px  #000000;" title="Screen Shot 2019-07-28 at 3.45.53 PM" /></a></p>
<p>In API perspective, a rotation around world axes (WCS) is configured by Viewpoint.Rotation (Rotation3D) which is in 3D space defined as a quaternion. From quaternion, it can also tell something like roll, yaw, pitch. One post kindly provides the mathematical equations:</p>
<p><a href="https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html">https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html</a></p>
<p>These are defined in <a href="https://www.grc.nasa.gov/www/k-12/airplane/rotations.html">aircraft principal axes</a>. In Navisworks space, &#0160;when the up vector is Y+, right vector is X+, and view direction is Z-, the roll can be calculated from quaternion (Viewpoint.Rotation) by the equations above. However, in other cases when the up vector is different, roll in UI means what it indicates: rotating the camera around its front-to-back axis. Unfortunately, I do not find any API which tells roll in UI.</p>
<p>While in math, once we know the base up vector, current up vector, we can calculate the roll ourselves.</p>
<ul>
<li>Viewpoint.WorldUpVector: initial base up vector when user [Set Viewpoint Up]</li>
<li>Viewpoint.GetCamera(): a json string which contains many information such as
<ul>
<li>current up vector</li>
<li>current view direction (reversed vector of forward vector)</li>
</ul>
</li>
</ul>
<p>since view direction will keep the same when setting roll, the roll value will be the angle from current right to the base right (aligned with initial up vector).</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49a7611200d-pi"><img alt="Screen Shot 2019-07-28 at 4.15.44 PM" class="asset  asset-image at-xid-6a0167607c2431970b0240a49a7611200d img-responsive" src="/assets/image_552296.jpg" style="display: block; margin-left: auto; margin-right: auto; border: 1px  #000000;" title="Screen Shot 2019-07-28 at 4.15.44 PM" /></a></p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="https://adndevblog.typepad.com/.a/6a0167607c2431970b0240a49a761c200d-pi"><img alt="Screen Shot 2019-07-28 at 4.06.25 PM" class="asset  asset-image at-xid-6a0167607c2431970b0240a49a761c200d img-responsive" src="/assets/image_951336.jpg" style="display: block; margin-left: auto; margin-right: auto; border: 1px  #000000;" title="Screen Shot 2019-07-28 at 4.06.25 PM" /></a></p>
<p>The code below prints out the roll in <a href="https://www.grc.nasa.gov/www/k-12/airplane/rotations.html">aircraft principal axes</a> and the roll in Navisworks UI.</p>
<p>&#0160;</p>
<p>&#0160;</p>
<pre><code>
[Serializable]
    public class cameraInfoClass
    {
        public double[] UpDirection { get; set; }
        public double[] WorldRightDirection { get; set; }
        public double[] ViewDirection { get; set; }
    }

 public override int Execute(params string[] parameters)
        {
            //current viewpoint
            Viewpoint oViewpoint = 
Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentViewpoint;

            //current world up vector. The vector defined by user. 
           //Same to UI &gt;&gt; [Edit Viewpoint] &gt;&gt; 
            UnitVector3D worldUpVec = null;
            if (oViewpoint.HasWorldUpVector)
                worldUpVec = oViewpoint.WorldUpVector;
            else
                return 0;

            //A rotation in 3D space defined as a quaternion. 
            Rotation3D rotation = oViewpoint.Rotation;

            //***** 
            //Aircraft principal axes: X+: right, Y+: up, Z-: view direction
            // roll: around Z-
            // pitch: around X+
            // yaw: around X+
            //from https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html
            //roll  = Mathf.Atan2(2*y*w - 2*x*z, 1 - 2*y*y - 2*z*z);
            //pitch = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            //yaw = Mathf.Asin(2 * x * y + 2 * z * w);

            double aircraft_roll = Math.Atan2((2 * rotation.B * rotation.D) 
                                               - (2 * rotation.A * rotation.C),
                                               1 - (2 * rotation.B * rotation.B) 
                                               - (2 * rotation.C * rotation.C));
            double aircraft_roll_degree = aircraft_roll * 180 / Math.PI;

            double aircraft_pitch = Math.Atan2((2 * rotation.A * rotation.D) 
                                                - (2 * rotation.B * rotation.C), 
                                                1 - (2 * rotation.A * rotation.A) 
                                                - (2 * rotation.C * rotation.C));
            double aircraft_pitch_degree = aircraft_pitch * 180 / Math.PI;

            double aircraft_yaw = Math.Asin((2 * rotation.A * rotation.B) 
                                            + (2 * rotation.C * rotation.D));
            double aircraft_yaw_degree = aircraft_yaw * 180 / Math.PI;

            //*****
            //get camera parameters which contains more data we need to calculate roll of Navisworks UI
            string cameraStr = oViewpoint.GetCamera();
            cameraInfoClass cameraStrJson = JsonConvert.DeserializeObject(cameraStr);

            //current up vector
            Vector3D currentUpVec = new Vector3D(cameraStrJson.UpDirection[0],
                                                 cameraStrJson.UpDirection[1],
                                                 cameraStrJson.UpDirection[2]);

            Vector3D currentViewDir = new Vector3D(-cameraStrJson.ViewDirection[0],
                                                   -cameraStrJson.ViewDirection[1],
                                                   -cameraStrJson.ViewDirection[2]);
            
            //current right vector
            Vector3D currentRightVec = currentUpVec.Cross(currentViewDir);
            //current world right vector is when the viewpoint 
            //is aligned with an intial up vector.Initially, roll of UI is 0
            Vector3D currentWorldRightVec = worldUpVec.Cross(currentViewDir);

            //get roll of UI in degree
            double UI_roll_degree = currentRightVec.Angle(currentWorldRightVec) * 180 / Math.PI;


            MessageBox.Show(&quot;aircraft_roll:&quot; + aircraft_roll 
                            + &quot;\naircraft_pitch:&quot; + aircraft_pitch_degree 
                            + &quot;\naircraft_yaw:&quot; + aircraft_yaw_degree
                            + &quot;\nUI roll:&quot; + UI_roll_degree); 


            return 0;
}
</code></pre>
