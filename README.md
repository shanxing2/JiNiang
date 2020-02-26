# JiNiang(中文名：`姬娘`，下面都用`姬娘`表示)
[bilibili](https://www.bilibili.com/)直播姬、弹幕姬、danmuku、各种姬，VB(新VB.NET)实现，***史上结构最乱项目(官方吐槽)***

## Release
[安装包(installer package)](https://github.com/shanxing2/JiNiang/releases)

## 框架版本 
.NET 4.6.1

## 引用
### NuGet  
1.Install-Package System.ValueTuple -Version 4.5.0  
2.Install-Package Microsoft.AspNet.WebApi.Client -Version 5.2.7  

注：此Package依赖于Newtonsoft.Json，但是我们这个项目用不到，所以不需要也可以，可以在NuGet管理器中勾选 ‘强制卸载，即使有依赖项’，然后强制卸载依赖包Newtonsoft.Json。  
### 第三方库 
*  1.[shanxinglib.dll](https://github.com/shanxing2/shanxinglib) 
*  2.[ShanXingTech.IO2.Database](https://github.com/shanxing2/ShanXingTech.IO2.Database) 
*  3.`姬娘`插件 

## 错误 
1.无法获取密钥文件“ShanXingTech.pfx”的 MD5 校验和。未能找到文件“*\ShanXingTech.pfx”。  
解决方法：  
右键项目——属性——签名，去掉勾选 “为程序集签名(A)”，或者选择（或创建）你自己的强名称密钥文件。  


## 已有功能（主要）

*  自动更新在线人数、粉丝人数（更新间隔跟随发心跳包间隔）；
*  新粉丝关注提示；
*  送礼提示&感谢、上船提示&感谢、欢迎老爷进直播间；
*  发弹幕；
*  退出时自动缓存最多100条记录，下次启动时如果没有新弹幕会再加载缓存的弹幕；
*  一键快速开播（已有）以及OBS自动推流（暂未实现）；
*  ***勋章升级提示***

## TODO
*  添加插件系统；
*  可自定义部分弹幕展示格式（送礼、上船、老爷进直播间、新粉丝关注等，通过插件形式）；
*  弹幕只展示最新100条，减少资源占用（目前展示接收到的所有弹幕）；
*  发弹幕模块可分离，自由吸附到其他软件上，比如官方的弹幕姬；
*  自动打开直播宝箱(可选)；
*  自动赠送包裹中的免费道具给主播(可选)；
*  听直播，只有声音没有视频，会不会省辣么一点点牛量呢???（当前B站用的协议，应该实现不了这个功能）
*  礼物榜单/统计;
* 注：插件系统做好之后，界面会大改！

## BUG反馈、建议、编程交流群
Q群：[797522112](https://jq.qq.com/?_wv=1027&k=5MuFkkR) 胖头鱼煲汤好好次

## 小破站直播间
[4236342](https://live.bilibili.com/4236342)，[关注他](https://space.bilibili.com/52155851)
