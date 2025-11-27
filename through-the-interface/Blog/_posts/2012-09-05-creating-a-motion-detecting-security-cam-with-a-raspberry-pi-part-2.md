---
layout: "post"
title: "Creating a motion-detecting security cam with a Raspberry Pi &ndash; Part 2"
date: "2012-09-05 10:35:00"
author: "Kean Walmsley"
categories:
  - "Raspberry Pi"
original_url: "https://www.keanw.com/2012/09/creating-a-motion-detecting-security-cam-with-a-raspberry-pi-part-2.html "
typepad_basename: "creating-a-motion-detecting-security-cam-with-a-raspberry-pi-part-2"
typepad_status: "Publish"
---

<p>In <a href="http://through-the-interface.typepad.com/through_the_interface/2012/08/creating-a-motion-detecting-security-cam-with-a-raspberry-pi-part-1.html" target="_blank">the first post in this series</a>, we took a look at some fundamentals related to building your own motion-detecting security camera using a Raspberry Pi. In this post, we’re going to look at very specific steps to get one working.</p>
<p>The first major step I took after the last post was to drop the <a href="http://www.raspbian.org" target="_blank">Raspbian</a> “wheezy” OS distribution in favour of <a href="http://archlinuxarm.org" target="_blank">Arch Linux ARM</a>. This was an important change, as it increased the stability of the system significantly, and even allowed me to increase the capture resolution of the webcam back up to 960 x 720. Where “wheezy” would frequently crash – or the webcam itself would stop working, even at really low resolutions – Arch Linux ARM has shown itself to be rock solid and just keeps on working away. I’ve now left it running for days at a time without any issues.</p>
<p>There are a few differences when working with Arch Linux ARM: it doesn’t come with the very handy raspi-config script that alows “wheezy” users to perform common start-up operations – such as resizing the root partition, which we’ll perform manually, below – and there are various other bells &amp; whistles missing, but at the end of the day most Raspberry Pi usage is likely to be to create embedded systems that clearly need leanness and a high level of stability.</p>
<p>That’s not to say that “wheezy” won’t get more stable prior to its official release (it is still in Beta, after all), but Arch Linux ARM was absolutely the way to go for this project.</p>
<p>Installing the OS to an SD card was straightforward: <a href="http://through-the-interface.typepad.com/through_the_interface/2012/08/a-slice-of-raspberry-pi.html" target="_blank">as mentioned previously</a>, I used <a href="http://www.softpedia.com/get/CD-DVD-Tools/Data-CD-DVD-Burning/Win32-Disk-Imager.shtml" target="_blank">Win32DiskImager</a> to burn it to a 16 GB Class 10 SDHC card (which has way more beef that I need for this project, but hey :-). This tool can also be used to back up your OS, which is a good thing to do from time to time.</p>
<p>It should now be a simple matter of plugging in the LAN cable via the RJ45 socket and plugging in the Micro-USB power supply to see the device boot up. I personally no longer bother with keyboards and direct display connections: I just log in using ssh (which is native on OS X and Linux, and usable via <a href="http://www.chiark.greenend.org.uk/~sgtatham/putty/download.html" target="_blank">PuTTY</a> on Windows) to access the device remotely. It’s even possible to use X11 Windowing to pipe graphics back down to a compatible system (such as OS X), but that’s beyond the scope of this post.</p>
<p>In an OS X Terminal session, then, I use this command to connect to the Pi (when on my home network, where I’ve configured my router to dedicate the IP address of 192.168.1.110 to the device):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">ssh root@192.168.1.110</span></p>
</div>
<p>The default machine name is “alarmpi” (<span style="text-decoration: underline;">A</span>rch <span style="text-decoration: underline;">L</span>inux <span style="text-decoration: underline;">ARM</span> <span style="text-decoration: underline;">Pi</span>), so when in the office I use this, instead (it’s simpler to use DNS to resolve the IP rather than hunting around for it):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">ssh root@alarmpi.ads.autodesk.com</span></p>
</div>
<p>[The default password is “root” on the standard install.]</p>
<p>Once you have an ssh session running, you can take care of some initial tasks, first of which is to increase the root OS partition to the full size of the SD card. Here is <a href="http://archlinuxarm.org/forum/viewtopic.php?f=31&amp;t=3119" target="_blank">the most elegant method I’ve found to do this</a> (while those knowing what it does may find it a little scary, it worked perfectly for me :-). It’s worth bearing in mind that expanding the size of your root partition will increase the size of the image file when you back up your OS, but then that’s the choice you make.</p>
<p>I’ve added some comments that start with the # sign – you don’t need to type those lines in:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;"># As root:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">fdisk /dev/mmcblk0</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Delete the second partition /dev/mmcblk0p2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">d</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Create a new primary partition and use default sizes prompted.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># This will then create a partition that fills the disk.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">n</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">p</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">enter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">enter</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Save and exit fdisk:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">w</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Now reboot:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">reboot</span></p>
<p style="margin: 0px;"># Once rebooted:</p>
<p style="margin: 0px;"><span style="line-height: 140%;">resize2fs /dev/mmcblk0p2</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Your main / partition should now be the full size of the disk.</span></p>
</div>
<p>As you’ve rebooted during this process, you’ll clearly need to reconnect via ssh to the device to complete it.</p>
<p>Depending on the size of the card, this resize2fs operation may take some time.</p>
<p>Now it’s worth changing the root password and setting up a new user (which I’ll name “pi”):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">passwd root</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">adduser</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Add a user named pi - accept default options</span></p>
</div>
<p>We’ll stay logged in as root, for now, as we still have some work to do to get things ready to have effective use of the device as a standard user.</p>
<p>Linux distributions have different command-line tools for installing approved component packages – much as some of you may be used to with NuGet in recent versions of Visual Studio. Raspbian has apt-get, Arch Linux ARM has pacman. To make sure pacman is installed and (along with the OS itself) up-to-date, run these operations:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">pacman -Syu</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pacman-key --init</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pacman -Syu</span></p>
</div>
<p>Now you can use pacman to install the various packages we want to use. We’ll start with sudo (which stands for “<span style="text-decoration: underline;">s</span>uper-<span style="text-decoration: underline;">u</span>ser <span style="text-decoration: underline;">do</span>” rather than being a mis-spelling of “pseudo” :-), which will allow our pi user to perform operations as root.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">pacman -S sudo</span></p>
</div>
<p>It’s one thing to have sudo installed, it’s another to be able to use it. You can edit the sudo privileges directly using this command, which unfortunately uses the vi editor to open the file (I’ve mostly switched over to the much more intuitive nano editor, at this stage):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">visudo</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Scroll down to &quot;root ALL=(ALL) ALL&quot;, i to insert:</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pi ALL=(ALL) ALL</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Esc ZZ # (to quite &amp; save)</span></p>
</div>
<p>At this stage it’s worth logging out (you can type exit or logout to do so) and logging back in via a new ssh session as the “pi” user:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">ssh pi@192.168.1.110</span></p>
</div>
<p>One last step is helpful but not strictly necessary, in that it enables command-completion via the Tab key when executing commands using sudo:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">sudo pacman -S bash-completion</span></p>
</div>
<p>Now we basically have our OS up and ready. If we were on “wheezy” we’d now be thinking about installing (or building) a webcam driver. That happens to be built into Arch Linux ARM, so we can skip on ahead to installing Motion:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">sudo pacman -S motion</span></p>
</div>
<p>At this stage we have most of the packaged components in place that we need to run our motion-detecting security cam. Now we’ll start configuring Motion.</p>
<p>We’re going to ask Motion to save it&#39;s running process ID in a file in the /var/run/motion folder. First, though, we need to create that folder:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">sudo mkdir -p /var/run/motion</span></p>
</div>
<p>[As mentioned last time, it’s helpful – but not necessary – <a href="http://chris.gg/2012/07/using-a-ps3-eyetoy-with-the-raspberry-pi" target="_blank">to set up a killmotion script that uses this ID to kill the motion process</a> (which is really only for when you’re running in daemon mode). That said, it’s also simple enough to run the “pstree –p” command to see the running process tree with the IDs at each node, and then just use “kill 1234” (assuming the process ID is 1234).]</p>
<p>Next we’re going to install Python and the GData component we need to access GMail and Google Drive.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">sudo pacman -S python2 python2-pip</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sudo pip2 install gdata</span></p>
</div>
<p>We’re going to ask Motion to save one frame per event – that we can use as an email attachment in our email, to help recipients – especially those on a mobile device – decide whether it’s worth watching the uploaded video. Both the capture frame and the video will be stored to /home/pi/motion, which we need to create:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">mkdir /home/pi/motion</span></p>
</div>
<p>We might also have kept the default location – which I believe is under /var/tmp – but that’s a folder that gets cleared between sessions (when the device is rebooted). When my device was regularly being reset – back when I was using “wheezy” – I moved across to this folder, so I could see when images/videos were captured successfully. So far I haven’t seen the need to switch back.</p>
<p>Now we can download and install the Uploader.py file, which I’ve adapted from the one in <a href="http://jeremyblythe.blogspot.co.uk/2012/06/motion-google-drive-uploader-and.html" target="_blank">this post</a> (with the help of the information in <a href="http://www.tutorialspoint.com/python/python_sending_email.htm" target="_blank">this page</a>). You might <a href="http://through-the-interface.typepad.com/files/uploader.py" target="_blank">download the file</a> and FTP it across, or you could use the following approach to download it directly using the Pi (and then make it executable):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">cd /etc/motion</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sudo wget &quot;http://through-the-interface.typepad.com/files/uploader.py&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sudo chmod +x uploader.py</span></p>
</div>
<p>You’ll also need an uploader.cfg file in the same location. The simplest approach is to run “sudo nano uploader.cfg” and paste the contents in (after having modified them with your login information, of course):</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[gmail]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># GMail account credentials</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">name = Kean Walmsley</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">user = keanrw@gmail.com</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">password = this_is_not_really_my_password</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sender = keanrw@gmail.com</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Recipient email address (could be same as from_addr)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">recipient = kean.walmsley@autodesk.com</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Subject line for email</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">subject = You have a visitor</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"># First line of email message</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">message = See attachment. Video of visitor uploaded to:</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[docs]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Folder (or collection) in Docs where you want the videos to go</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">folder = motion</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">[options]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Delete the local video file after the upload</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">delete-after-upload = true</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;"># Send an email after the upload</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">send-email = true</span></p>
</div>
<p>You should now be able to test the script, to see that it works:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">cd ~</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">wget &quot;http://through-the-interface.typepad.com/files/VidTest.avi&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">wget &quot;http://through-the-interface.typepad.com/files/VidTest.jpg&quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">/etc/motion/uploader.py /etc/motion/uploader.cfg ./VidTest.avi</span></p>
</div>
<p>[I had previously thought to post instructions to use fswebcam to capture a still and ffmpeg to capture a video, but the former involved installing an additional component and the latter didn’t actually work when called directly on my Pi (even though Motion uses it successfully to encode video, as far as I can tell)].</p>
<p>Once you’ve executed the uploader script, you should receive an email with a link to the video on Google Drive (assuming it’s enabled for your Gmail account) and the image as an attachment:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20177448672cb970d-pi" target="_blank"><img alt="Test email with attachment" border="0" height="319" src="/assets/image_668559.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border: 0px;" title="Test email with attachment" width="331" /></a>Now the various pieces are in place, it should be a simple matter of getting and adjusting the Motion configuration file. To get the one that I have working with my webcam, you can:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">cd /etc/motion</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">sudo wget &quot;http://through-the-interface.typepad.com/files/motion.conf&quot;</span></p>
</div>
<p>Motion has a lot of settings that you may want to adjust – it’s a really flexible, powerful component – but I won’t go into the details on that, here.</p>
<p>To run Motion simply type “sudo motion” at the command-line. The configuration I’ve provided will run it in daemon (i.e. as a background service) mode, but if you hit problems you may want to run it in interactive mode, or even with the –s flag. A couple of handy features provide the ability to change configuration options at runtime via a URL (in my case this is <a href="http://192.168.1.110:7071">http://192.168.1.110:7071</a>) and to view the live webcam output via another (<a href="http://192.168.1.110:7070">http://192.168.1.110:7070</a>).</p>
<p>If all is working well, you should start to get emails with detected motion events:</p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e2017c31a8b247970b-pi" target="_blank"><img alt="Email on motion detection - with attachment" border="0" height="319" src="/assets/image_514848.jpg" style="background-image: none; margin: 20px auto; padding-left: 0px; padding-right: 0px; display: block; float: none; padding-top: 0px; border: 0px;" title="Email on motion detection - with attachment" width="331" /></a></p>
<p>Once you’re ready to have it enter daemon mode on boot, you can edit the /etc/rc.conf file (using sudo nano …) to add it to the list of startup daemons on the very last line of the file.</p>
<p>One thing to note about this particular configuration: as mentioned, we save a single .jpg to accompany the .avi file that gets recorded (both files will appear in the same /home/pi/motion folder with the same name). The uploader script gets called when the .avi gets saved but assumes the .jpg will be there.</p>
<p>These files get created once motion has fully stopped (and there’s a configurable 60 delay to make sure that’s the case) which means that at some point we’ll want to change the configuration to have Motion take more regular snapshots – and use a different naming convention, as we’ll have more .jpg files per .avi – as that will allow us to run them through the facial recognition system and present the results without waiting for 60 seconds after the person has left. A change will also be needed for the uploader module, of course. But all that’s for another day and another series of posts. :-)</p>
<p><strong><em>Update:</em></strong></p>
<p>Alex Fielder reminded me that the uploader will look for a folder called &quot;motion&quot; in your Google Drive - be sure to make that folder before you try uploading. Thanks, Alex! :-)</p>
