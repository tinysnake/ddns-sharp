#### DDnsSharp是一个基于dnspod服务,面向个人易用的 DDNS 软件, 适用于Windows 7, Windows Server 2008 R2 及以上的操作系统.

#### 本软件需要Microsft .NET Framework 4.5 才能运行.

当前推荐下载地址: [百度云网盘](http://pan.baidu.com/share/link?shareid=233987&uk=1040525562)

.NET4.5框架下载地址: [在线安装包](http://go.microsoft.com/fwlink/?LinkId=225704), [离线安装包](http://go.microsoft.com/fwlink/?LinkId=225702)

当前管理器版本号: `0.1.4778`

当前服务版本号: `0.1.4778`

## DDnsSharp简介

DDnsSharp是一个基于dnspod服务,面向个人易用的 Windows 7+ 平台下的 DDNS 软件,它的界面简洁,不占用您的CPU资源,管理器占用内存35M(我会未来尽量缩小内存占用的..),同步服务占用内存8M.

## DDnsSharp的优势

1. DDnsSharp的天然优势就是它站在了巨人的肩膀上.拥有强大的dnspod系统作为后盾,DDnsSharp能够轻易的击倒通常使用的某壳(免费的某壳服务不支持顶级域名),某3某2网,和远在大洋以外的dyndns服务(距离优势).

2. 微软的傻瓜化至今为止是做的最好的公司之一,虽然它的运行库安装包比较庞大(40+M),但是只要一路"下一步"到底就能完成安装,点击exe文件就能运行,这样不必为了安装运行环境而伤脑筋了.

3. 服务和管理软件分开运行,每次开机就不用见到烦人的系统托盘图标了..

4. 30秒更新一次,相当于IP一换,立马就同步,某壳免费的三分钟一同步弱爆了!

## DDnsSharp的劣势

1. DDnsSharp仅仅是我个人的小作品而已,而且当初的目的就是用来练手的..但是为了让大家能够正常的使用,我尽量将它写得足够稳定(特别是服务组件).

2. 它是基于微软.NET框架开发的.有一点软件开发知识的朋友就知道它很臃肿.虽然它的运行速度绝对很快,但是吃起内存起来和C++和python写的软件还是多非常多的.

## DDnsPod的源代码需要以下第三方库支持才能成功编译:

建议使用[Nuget](http://nuget.org/)来管理这些库.

 - [Json.NET (4.5.1+)](http://json.codeplex.com/)
 - [MVVM Light Toolkit (4.1.26+)](http://mvvmlight.codeplex.com/)
 - [Ninject (3.0+)](https://github.com/ninject/ninject)
 - [Nlog (2.0+)](http://nlog-project.org/)

##FAQs

#### Q:这个软件怎么使用?

A: 首先您得安装.NET4.5框架(注意,该框架只支持Windows 7及以上版本的操作系统),然后再下载DDnsSharp的推荐版本至您的硬盘上, 将压缩包解压开以后即可运行,不需要任何安装步骤. 本软件需要管理员权限才能运行,请确保您当前的Windows账户有管理员权限.

#### Q: 在哪里添加域名?

A: DDnsSharp仅提供DDNS服务,如您需要管理域名或管理域名记录,请到官方网站设置好以后再使用DDnsSharp的服务.

#### Q: 能不能不让DDnsSharp每次都开机启动,好烦!

A: 可以.只要您安装并开启了DDnsSharp的服务, DDnsSharp的管理软件就不需要再开启了. 只要您的电脑处于运行状态, DDnsSharp就能自动帮您同步好您当前的IP地址.

#### Q: 软件出错了怎么办!

A:在DDnsSharp目录下目录下有一个名为ddnssharp.log的文件,该文件里记录了大部分程序运行时发生的错误.用记事本将其打开,并把里面的内容发表到[工单页面](https://gitcafe.com/snake/DDnsSharp/tickets)下,大家会帮您解决这个问题的.

#### Q: 为什么服务不能安装? 为什么服务不能卸载? 为什么无法启动服务?

A: 通常情况下无法安装和卸载服务是因为您的Windows权限不足导致的,也有可能是您DDnsSharp的目录下的文件不完整导致的.您可以在DDnsSharp目录下找到InstallUtil.InstallLog和DDnsSharp.Service.InstallLog文件,用记事本将其打开,并把里面的内容发表到[工单页面](https://gitcafe.com/snake/DDnsSharp/tickets)下,大家会帮您解决这个问题的,或者重新下载一遍DDnsSharp再试试看.

#### Q: 为什么DDnsSharp只支持Windows 7以上的操作系统? 为什么要放弃广大的XP用户?

A: 本人坚决不建议周围的人继续使用Windows XP(电脑配置很烂的除外), 所以DDnsSharp不支持Windows 7以下的操作系统也是情有可原的. 当然还有主要一个原因是.NET 4.5只支持Windows 7以上的操作系统.. 至于为什么我要这样做,我就不解释,也不辩论.

#### Q:你的DDnsSharp如何保证我的账号安全!
A: 我坚决按照dnspod的开发要求所制作的软件,本地保存的密码通过AES加密,一切请求全部通过https,并且在软件中不包含任何收集用户账户或密码的代码,如果您不相信并且有能力,请自行查看代码,并且自行编译使用.

## 软件界面截图:

![](https://gitcafe.com/snake/DDnsSharp/raw/master/misc/screenshot.jpg)

## Changelogs:

#### 2013/1/30 Monitor.exe v0.1.4778.3939

- 修复了无法获取服务器状态的bug,修改了点击关闭按钮却是最小化的用户体验行为.

#### 2013/1/30 Monitor.exe v0.1.4778.19870

- 添加新纪录以后记录不再是禁用状态,而是默认启用状态.

#### 2013/1/30 Service.exe v0.1.4778.19870

- 修复了配置不同步导致服务执行同步任务的时候用老配置把新配置覆盖掉的问题.
