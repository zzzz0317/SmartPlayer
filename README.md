# ZZ直播播放器

基于 Daniulive WIN-PlayerSDK-CSharp-Demo-2020-04-29

**请注意：  
大牛直播SDK可供个人学习之用，企业及商用需要经过授权 (请查阅 [daniulive/SmarterStreaming](https://github.com/daniulive/SmarterStreaming) )**



## 使用说明

* 单击 **视频画面** 可隐藏/显示 设置

* 底部文字设置为空时默认显示直播流地址
* 单击底部文字可隐藏底部文字，若需要显示请重新设置底部文字



## 命令行参数

自己看代码自己理解 =.=

```c#
// SmartPlayerForm.cs
switch (args.Length) {
	case 0:
		textBox_url.Text = "";
		break;
	case 1:
        textBox_url.Text = "rtmp://10.99.0.250/live/" + args[0].ToString();
        break;
	case 2:
        textBox_url.Text = args[0].ToString() + args[1].ToString();
        break;
	default:
        MessageBox.Show("参数错误");
        break;
}
```
P.S.  C#程序的名称不会被当作第一个命令行参数。