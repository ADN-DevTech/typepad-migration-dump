---
layout: "post"
title: "Setup Node.Js Enviroment on Amazon EC2 linux"
date: "2014-12-10 20:39:37"
author: "Daniel Du"
categories:
  - "Amazon Web Services"
  - "Cloud"
  - "Daniel Du"
  - "Scaling"
  - "Server"
  - "Web Server"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2014/12/setup-nodejs-enviroment-on-amazon-ec2-linux-1.html "
typepad_basename: "setup-nodejs-enviroment-on-amazon-ec2-linux-1"
typepad_status: "Publish"
---

<p><span style="font-size: small; line-height: 19.5px; background-color: #FFFFFF;">By</span> <a href="http://adndevblog.typepad.com/cloud_and_mobile/daniel-du.html" style="font-size: small; line-height: 19.5px; background-color: #FFFFFF;">Daniel Du</a><br /></p>
<p>Node.Js is very popular in web programming, you can use single language - JavaScript - to create the back end logic and the front end web page. Installing NodeJs environment on Mac or Windows is relatively easy, while if I need to put my Node.Js sample into production, I will have to set it up on Linux. Of cause I can choose Windows to host Node.Js application, but I prefer to choose the free Linux, which is a good practice for me as well.</p>
<p>I am using AWS EC2 as my host. Firstly, I need to launch an EC2 instance on AWS. I will use Amazon Linux AMI here.</p>
<p><img src="/assets/image_7e36aa.jpg" width="480" height="72" alt="201412110913.jpg" /></p>
<p>I’d like to start from t2.micro, which is free tier eligible. Once I find it not powerful enough for my application, I can scale up or scale out easily.<br />
<img src="/assets/image_f71bcd.jpg" width="480" height="97" alt="201412110914.jpg" /></p>
<p>for the 3rd step, I use the default settings:</p>
<p><img src="/assets/image_8f3528.jpg" width="480" height="286" alt="201412110921.jpg" /></p>
<p>Step4, add a Elastic Block Storage, I just use the default setting, and I can add another EBS storage latter if necessary.</p>
<p><br />
<img src="/assets/image_179d4e.jpg" width="480" height="146" alt="201412110922.jpg" /></p>
<p>Step 5, add some tags so that I can recognise this server easier, it is useful especially when you have many instances running.</p>
<p><br />
<img src="/assets/image_70b49c.jpg" width="480" height="102" alt="201412110926.jpg" /></p>
<p>Step6, Configure the security group, which is similar like firewall on AWS side, which is a whitelist, only allowed port can pass though. My server is supposed to be web server, so I add HTTP/80, and to connect to the server and do some configuration work, I also enabled SSH/22. I can remove this once the configuration is done.</p>
<p><br />
<img src="/assets/image_21bd37.jpg" width="480" height="198" alt="201412110929.jpg" /></p>
<p>Once I review the instance settings and they are fine for me, now I am ready to launch it. But before that, I need to create a key pair so that I can connect to this serve latter. Be sure to download the .pem file and save it to somewhere secure.</p>
<p><br />
<img src="/assets/image_50415e.jpg" width="480" height="306" alt="201412110932.jpg" /></p>
<p>Now, it is ready to go. After a few minutes, my Linux server is up and running.</p>
<p>Now I will connect to it and install the node.Js environment into it. To connect to the EC2 Linux instance, I need a SSH client. For Mac, the built-in terminal is good enough for me, for Windows, you may find Putty useful. Actually if I got the EC2 console, select the instance and click the “connect” button, AWS will tell me how to connect to this server:</p>
<div class="IMG" style="color: #444444; font-family: 'Helvetica Neue', Roboto, Arial, 'Droid Sans', sans-serif; font-size: 14px; line-height: 18.2000007629395px; background-color: #FFFFFF;">
  <span class="GMG" style="font-weight: bold;">To access your instance:</span>

  <ol>
    <li style="margin-top: 5px;">Open an SSH client. (find out how to <a href="https://docs.aws.amazon.com/console/ec2/instances/connect/putty" target="_blank" style="color: #146EB4; text-decoration: none; border-top-left-radius: 2px; border-top-right-radius: 2px; border-bottom-right-radius: 2px; border-bottom-left-radius: 2px; padding: 1px 2px; cursor: pointer;">connect using PuTTY</a>)</li>

    <li style="margin-top: 5px;">Locate your private key file (aws-ipg-cp-adn-devtech.pem). The wizard automatically detects the key you used to launch the instance.</li>

    <li style="margin-top: 5px;">Your key must not be publicly viewable for SSH to work. Use this command if needed:
      <pre style="font-weight: bold; padding-left: 24px;">
chmod 400 aws-ipg-cp-adn-devtech.pem
</pre>
    </li>

    <li style="margin-top: 5px;">Connect to your instance using its Public IP:
      <pre style="font-weight: bold; padding-left: 24px;">
54.67.70.195
</pre>
    </li>
  </ol>
</div>
<div style="color: #444444; font-family: 'Helvetica Neue', Roboto, Arial, 'Droid Sans', sans-serif; font-size: 14px; line-height: 18.2000007629395px; background-color: #FFFFFF;">
  <span class="GMG" style="font-weight: bold;">Example:</span>

  <div class="FMG" style="padding-left: 60px; padding-bottom: 16px;">
    <pre style="font-weight: bold;">
ssh -i aws-ipg-cp-adn-devtech.pem ec2-user@54.67.70.195
</pre>
  </div>
</div>
<p>As indicated, I connected to my Linux server from my Mac terminal with SSH:</p>
<p><br />
<img src="/assets/image_0c1e23.jpg" width="480" height="193" alt="201412110950.jpg" /></p>
<p>Now I am ready to install Node.Js environment. Firstly run ‘yum update’:</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ sudo yum update</p>
<p>Next, I will go ahead to install node.js from source code:</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ sudo yum install gcc-c++ make</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ sudo yum install openssl-devel</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ sudo yum install git</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ git clone git://github.com/joyent/node.git</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ cd node</p>
<p>The source code of Node.Js is downloaded to my linux server.</p>
<p><br />
<img src="/assets/image_d7fe77.jpg" width="480" height="81" alt="201412111006.jpg" /></p>
<p>To check out one version of Node, I view all available Node tags with following command:</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ git tag -l</p>
<p>I’d like to use Node 0.10.32, so I check it out with following command, you can choose your favourite version. And then I start installing it from source code, it may take a few minutes to complete.</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ git checkout v0.10.32</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ ./configure</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ make</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ sudo make install</p>
<p>Now my Node is installed on my Linux server :</p>
<p><img src="/assets/image_acbdac.jpg" width="480" height="66" alt="201412111023.jpg" /></p>
<p>I also need to install some useful NPM packages, but when I do that, it reminds me that the command is not found.-</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ sudo npm install express</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">sudo: npm: command not found</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">[ec2-user@ip-172-31-25-230 node]$</p>
<p>so I need to add it to sudo’s path so that I can install more packages.</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">$ sudo vi /etc/sudoers</p>
<p>Append the /usr/local/bin to the secure_path as below:</p>
<p><img src="/assets/image_c57d1b.jpg" width="480" height="103" alt="201412111030.jpg" /></p>
<p>After saving this file and exit to the command line, I can install express and others:</p>
<p><img src="/assets/image_a67a03.jpg" width="480" height="179" alt="201412111037.jpg" /></p>
<p>It seems the environment if one, but to confirm,I need to create a very sample Node.Js application to verify the environment works fine.</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">[ec2-user@ip-172-31-25-230 ~]$ mkdir firstnodeapp</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">[ec2-user@ip-172-31-25-230 ~]$ cd firstnodeapp/</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">[ec2-user@ip-172-31-25-230 firstnodeapp]$ ls</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;"></p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;"><br /></p>
<p>I created the Node.Js sample on my local machine and then upload the source code to Linux server. BTW, I use CyberDuck to upload files into EC2 Linux, which is very helpful. Be sure to check “use public key authentication”, it will ask you the *.pem file, which is downloaded from AWS when creating the instance. Once I connect to the EC2 instance from CyberDuck, I can drag and drop files into it to upload them.</p>
<p><img src="/assets/image_f87c83.jpg" width="480" height="421" alt="201412111156.jpg" /></p>
<p>A few seconds latter, my files are uploaded, let’s go back to the terminal of EC2 instance to start the Node.Js application.</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;"><br /></p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">[ec2-user@ip-172-31-25-230 testnodeapp]$ ls</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">app.js <span style="color: #4c7aff">bin</span> package.json <span style="color: #4c7aff">public</span> <span style="color: #4c7aff">routes</span> <span style="color: #4c7aff">views</span></p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;"></p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">[ec2-user@ip-172-31-25-230 testnodeapp]$ npm install</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">[ec2-user@ip-172-31-25-230 testnodeapp]$ chmod -R +x .</p>
<p style="margin-top: 0px; margin-bottom: 0px; font-size: 18px; line-height: normal; font-family: 'Andale Mono'; color: #29F914; background-color: #000000;">[ec2-user@ip-172-31-25-230 testnodeapp]$ sudo DEBUG=testnodeapp ./bin/www</p>
<p>Now the Node.Js application is running (I am using 80 port when I created the Node.Js sample) , I did not use the default 3000 port because my security group in AWS does not allow it. Yes, I can edit the security group to add 3000 port, but HTTP/80 makes more sense to me for a web server. Now I can now access it from browser. It is just a samplest express site, but the Node.Js environment looks fine now.</p>
<p><img src="/assets/image_f82574.jpg" width="480" height="259" alt="201412111202.jpg" /></p>
<p>Next step, I will create a new AMI based on this instance, I do not want to repeat all these steps every time when I need a NodeJs Linux instance :) Go to the AWS Console, right click the instance and choose Image -&gt; Create Image:</p>
<p><br />
<img src="/assets/image_b910b5.jpg" width="480" height="187" alt="201412111208.jpg" /></p>
<p>Give it a name:</p>
<p><img src="/assets/image_2e5157.jpg" width="480" height="251" alt="201412111206.jpg" /></p>
<p>Now the AMI is being created. if go to Images -&gt; AMIs from left panel, it shows my custom AMIs.</p>
<p><br />
<img src="/assets/image_a407f8.jpg" width="480" height="178" alt="201412111211.jpg" /></p>
<p>OK, I wil stop here, next I can start up a new instance with Node.Js environment ready by launching an instance from my customised AMI, and more important, I can launch more instances like this to do auto scaling latter.</p>
