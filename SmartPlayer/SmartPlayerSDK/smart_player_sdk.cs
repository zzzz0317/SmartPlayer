/*
 * smart_player_sdk.cs
 * smart_player_sdk
 * 
 * Github: https://github.com/daniulive/SmarterStreaming
 * 
 * Created by DaniuLive on 2017/04/19.
 * Copyright © 2014~2017 DaniuLive. All rights reserved.
 */

using System;
using System.Runtime.InteropServices;

namespace NT
{
    public class NTSmartPlayerSDK
    {
		/* 
         * Init
         * 
         * flag目前传0，后面扩展用
         * pReserve传NULL,扩展用
         * 
         * 成功返回 NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_Init(UInt32 flag, IntPtr pReserve);
		
		/*
         * Uninit
         * 
         * 这个是最后一个调用的接口
         * 
         * 成功返回 NT_ERC_OK
		 */  
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_UnInit();

        /*
         * Open
         * 
         * flag目前传0，后面扩展用
         * pReserve传NULL,扩展用
         * hwnd 绘制画面用的窗口
         * 
         * 获取Handle
         * 
         * 成功返回 NT_ERC_OK
         */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_Open(out IntPtr pHandle, IntPtr hwnd, UInt32 flag, IntPtr pReserve);
		
		/*
         * Close
         * 
         * 调用这个接口之后handle失效
         * 
         * 成功返回 NT_ERC_OK
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_Close(IntPtr handle);
		
        
		/*
         * 设置事件回调，如果想监听事件的话，建议调用Open成功后，就调用这个接口
         */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetEventCallBack(IntPtr handle, IntPtr call_back_data, SP_SDKEventCallBack call_back);

		/*
         * 设置视频大小回调接口
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetVideoSizeCallBack(IntPtr handle, IntPtr callbackdata, SP_SDKVideoSizeCallBack call_back);
		
		/*
         * 设置视频回调, 吐视频数据出来
         * frame_format: 只能是NT_SP_E_VIDEO_FRAME_FORMAT_RGB32, NT_SP_E_VIDEO_FRAME_FROMAT_I420
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetVideoFrameCallBack(IntPtr handle, Int32 frame_format,
            IntPtr callbackdata, SP_SDKVideoFrameCallBack call_back);

        /*
         * 设置视频回调, 吐视频数据出来, 可以指定吐出来的视频宽高
         * handle: 播放句柄
         * scale_width：缩放宽度（必须是偶数，建议是 16 的倍数)
         * scale_height：缩放高度（必须是偶数)
         * scale_filter_mode: 缩放质量, 0 的话 SDK 将使用默认值, 目前可设置范围为[1, 3], 值越大 缩放质量越好，但越耗性能
         * frame_format: 只能是NT_SP_E_VIDEO_FRAME_FORMAT_RGB32, NT_SP_E_VIDEO_FRAME_FROMAT_I420
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetVideoFrameCallBackV2(IntPtr handle,
            Int32 scale_width, Int32 scale_height,
            Int32 scale_filter_mode, Int32 frame_format,
            IntPtr call_back_data, SP_SDKVideoFrameCallBack call_back);

       /*
		* 设置绘制视频帧时，视频帧时间戳回调
		* 注意如果当前播放流是纯音频，那么将不会回调，这个仅在有视频的情况下才有效
		*/
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRenderVideoFrameTimestampCallBack(IntPtr handle,
            IntPtr callbackdata, SP_SDKRenderVideoFrameTimestampCallBack call_back);

        /*
         * 设置音频PCM帧回调, 吐PCM数据出来，目前每帧大小是10ms.
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetAudioPCMFrameCallBack(IntPtr handle,
            IntPtr call_back_data, SP_SDKAudioPCMFrameCallBack call_back);

        /*
		 * 设置用户数据回调
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetUserDataCallBack(IntPtr handle,
			IntPtr call_back_data, SP_SDKUserDataCallBack call_back);
        
		/*
		 * 设置视频sei数据回调
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetSEIDataCallBack(IntPtr handle,
            IntPtr call_back_data, SP_SDKSEIDataCallBack call_back);

		/*
         * 开始播放,传URL进去
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_Start(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]String url,
            IntPtr callbackdata, SP_SDKStartPlayCallBack call_back);
		
		/*
         * 停止播放
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_Stop(IntPtr handle);
		
        //新接口++

        /*
		 * 设置URL
		 * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetURL(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]String url);

        /*
         * 设置解密key，目前只用来解密rtmp加密流
		 * key： 解密密钥
		 * size: 密钥长度
		 * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetKey(IntPtr handle, byte[] key, UInt32 size);

		/*
		 * 设置解密向量，目前只用来解密rtmp加密流
		 * iv：  解密向量
		 * size: 向量长度
		 * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetDecryptionIV(IntPtr handle, byte[] iv, UInt32 size);

		/*
         * handle: 播放句柄
         * hwnd: 这个要传入真正用来绘制的窗口句柄
         * is_support: 如果支持的话 *is_support 为1， 不支持的话为0
         * 接口调用成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_IsSupportD3DRender(IntPtr handle, IntPtr hwnd, ref Int32 is_support);

		/*
         * 设置绘制窗口句柄，如果在调用Open时设置过，那这个接口可以不调用
         * 如果在调用Open时设置为NULL，那么这里可以设置一个绘制窗口句柄给播放器
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRenderWindow(IntPtr handle, IntPtr hwnd);

        /*
         * 设置视频画面的填充模式，如填充整个绘制窗口、等比例填充绘制窗口，如不设置，默认填充整个绘制窗口
         * handle: 播放句柄
         * mode： 0: 填充整个绘制窗口; 1: 等比例填充绘制窗口, 默认值是0
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRenderScaleMode(IntPtr handle, Int32 mode);

		/*
		 * 设置是否播放出声音，这个和静音接口是有区别的
		 * 这个接口的主要目的是为了用户设置了外部PCM回调接口后，又不想让SDK播放出声音时使用
		 * is_output_auido_device: 1: 表示允许输出到音频设备，默认是1， 0：表示不允许输出. 其他值接口返回失败
		 * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetIsOutputAudioDevice(IntPtr handle, Int32 is_output_auido_device);

        /*
		 * 开始播放, 注意NT_SP_StartPlay和NT_SP_Start不能混用，要么使用NT_SP_StartPlay, 要么使用NT_SP_Start.
		 * NT_SP_Start和NT_SP_Stop是老接口，不推荐用。请使用NT_SP_StartPlay和NT_SP_StopPlay新接口
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_StartPlay(IntPtr handle);

		/*
		 * 停止播放
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_StopPlay(IntPtr handle);

        /*
		 * 设置是否录视频，默认的话，如果视频源有视频就录，没有就没得录, 但有些场景下可能不想录制视频，只想录音频，所以增加个开关
         * 
		 * is_record_video: 1 表示录制视频, 0 表示不录制视频, 默认是1
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRecorderVideo(IntPtr handle, Int32 is_record_video);


		/*
		 * 设置是否录音频，默认的话，如果视频源有音频就录，没有就没得录, 但有些场景下可能不想录制音频，只想录视频，所以增加个开关
		 *
         * is_record_audio: 1 表示录制音频, 0 表示不录制音频, 默认是1
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRecorderAudio(IntPtr handle, Int32 is_record_audio);

		/*
		 * 设置本地录像目录, 必须是英文目录，否则会失败
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRecorderDirectory(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)] String dir);

		/*
		 * 设置单个录像文件最大大小, 当超过这个值的时候，将切割成第二个文件
		 * size: 单位是KB(1024Byte), 当前范围是 [5MB-800MB], 超出将被设置到范围内
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRecorderFileMaxSize(IntPtr handle, UInt32 size);

		/*
		 * 设置录像文件名生成规则
		 */
        [DllImport(@"SmartPlayerSDK.dll", EntryPoint = "NT_SP_SetRecorderFileNameRuler", CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 NT_SP_SetRecorderFileNameRuler(IntPtr handle, ref NT_SP_RecorderFileNameRuler ruler);

		/*
		 * 设置录像回调接口
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRecorderCallBack(IntPtr handle,
            IntPtr call_back_data, SP_SDKRecorderCallBack call_back);

        /*
         * 设置录像时音频转AAC编码的开关, aac比较通用，sdk增加其他音频编码(比如speex, pcmu, pcma等)转aac的功能.
         * is_transcode: 设置为1的话，如果音频编码不是aac，则转成aac, 如果是aac，则不做转换. 设置为0的话，则不做任何转换. 默认是0.
         * 注意: 转码会增加性能消耗
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRecorderAudioTranscodeAAC(IntPtr handle, Int32 is_transcode);

		/*
		 * 启动录像
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_StartRecorder(IntPtr handle);

		/*
		 * 停止录像
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_StopRecorder(IntPtr handle);

       /*
		* 设置拉流时，吐视频数据的回调
		*/
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetPullStreamVideoDataCallBack(IntPtr handle, IntPtr call_back_data, SP_SDKPullStreamVideoDataCallBack call_back);

       /*
		* 设置拉流时，吐音频数据的回调
		*/
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetPullStreamAudioDataCallBack(IntPtr handle, IntPtr call_back_data, SP_SDKPullStreamAudioDataCallBack call_back);

        /*
         * 设置拉流时音频转AAC编码的开关, aac比较通用，sdk增加其他音频编码(比如speex, pcmu, pcma等)转aac的功能.
         * is_transcode: 设置为1的话，如果音频编码不是aac，则转成aac, 如果是aac，则不做转换. 设置为0的话，则不做任何转换. 默认是0.
         * 注意: 转码会增加性能消耗
		*/
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetPullStreamAudioTranscodeAAC(IntPtr handle, Int32 is_transcode);

		/*
		 * 启动拉流
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_StartPullStream(IntPtr handle);

 		/*
		 * 停止拉流
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_StopPullStream(IntPtr handle);

		/*
         * 绘制窗口大小改变时，必须调用
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_OnWindowSize(IntPtr handle, Int32 cx, Int32 cy);
		
		/*
         * 万能接口, 设置参数， 大多数问题， 这些接口都能解决
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetParam(IntPtr handle, UInt32 id, IntPtr pData);
		
		/*
         * 万能接口, 得到参数， 大多数问题，这些接口都能解决
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_GetParam(IntPtr handle, UInt32 id, IntPtr pData);	

		/*
         * 设置buffer,最小0ms
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetBuffer(IntPtr handle, Int32 buffer);	
		
		/*
         * 静音接口，1为静音，0为不静音
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetMute(IntPtr handle, Int32 is_mute);

		/*
         * 设置RTSP TCP 模式, 1为TCP, 0为UDP, 仅RTSP有效
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRTSPTcpMode(IntPtr handle, Int32 isUsingTCP);

        /*
         * 设置RTSP超时时间, timeout单位为秒，必须大于0
         */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRtspTimeout(IntPtr handle, Int32 timeout);

        /*
         * [NOTE] 对于RTSP来说，有些可能支持rtp over udp方式，有些可能支持使用rtp over tcp方式. 
         * 为了方便使用，有些场景下可以开启自动尝试切换开关, 打开后如果udp无法播放，sdk会自动尝试tcp, 如果tcp方式播放不了,sdk会自动尝试udp.
         * is_auto_switch_tcp_udp： 如果设置1的话, sdk将在tcp和udp之间尝试切换播放，如果设置为0，则不尝试切换.
         */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRtspAutoSwitchTcpUdp(IntPtr handle, Int32 is_auto_switch_tcp_udp);

		/*
         * 设置秒开, 1为秒开, 0为不秒开
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetFastStartup(IntPtr handle, Int32 isFastStartup);

        /*
         * 设置低延时播放模式，默认是正常播放模式
         * mode: 1为低延时模式， 0为正常模式，其他只无效
         * 接口调用成功返回NT_ERC_OK
         */
        [DllImport(@"SmartPlayerSDK.dll")]
		public static extern UInt32 NT_SP_SetLowLatencyMode(IntPtr handle, Int32 mode);

		/*
         * 检查是否支持H264硬解码 
         * 如果支持的话返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
		public static extern UInt32 NT_SP_IsSupportH264HardwareDecoder();

		/*
         * 检查是否支持H265硬解码
         * 如果支持的话返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
		public static extern UInt32 NT_SP_IsSupportH265HardwareDecoder();

		/*
         * 设置H264硬解
         * is_hardware_decoder: 1:表示硬解, 0:表示不用硬解
         * reserve: 保留参数, 当前传0就好
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetH264HardwareDecoder(IntPtr handle, Int32 is_hardware_decoder, Int32 reserve);

		/*
         * 设置H265硬解
         * is_hardware_decoder: 1:表示硬解, 0:表示不用硬解
         * reserve: 保留参数, 当前传0就好
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetH265HardwareDecoder(IntPtr handle, Int32 is_hardware_decoder, Int32 reserve);

        /*
         * 设置只解码视频关键帧
         * is_only_dec_key_frame: 1:表示只解码关键帧, 0:表示都解码, 默认是0
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetOnlyDecodeVideoKeyFrame(IntPtr handle, Int32 is_only_dec_key_frame);

        /*
		 *上下反转(垂直反转)
		 *is_flip: 1:表示反转, 0:表示不反转
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
		public static extern UInt32 NT_SP_SetFlipVertical(IntPtr handle, Int32 is_flip);

		/*
		 *水平反转
		 *is_flip: 1:表示反转, 0:表示不反转
		 */
		[DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetFlipHorizontal(IntPtr handle, Int32 is_flip);

		/*
         * 设置旋转，顺时针旋转
         * degress： 设置0， 90， 180， 270度有效，其他值无效
         * 注意：除了0度，其他角度播放会耗费更多CPU
         * 接口调用成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRotation(IntPtr handle, Int32 degress);

        /*
		 * 在使用D3D绘制的情况下，给绘制窗口上画一个logo, logo的绘制由视频帧驱动, 必须传入argb图像
		 * argb_data: argb图像数据, 如果传null的话,将清除之前设置的logo
		 * argb_stride: argb图像每行的步长(一般都是image_width*4)
		 * image_width: argb图像宽度
		 * image_height: argb图像高度
		 * left: 绘制位置的左边x
		 * top： 绘制位置的顶部y
		 * render_width: 绘制的宽度
		 * render_height: 绘制的高度
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetRenderARGBLogo(IntPtr handle,
            IntPtr argb_data, Int32 argb_stride,
            Int32 image_width, Int32 image_height,
            Int32 left, Int32 top,
            Int32 render_width, Int32 render_height);

        /*
         * 设置下载速度上报, 默认不上报下载速度
         * is_report: 上报开关, 1: 表上报. 0: 表示不上报. 其他值无效.
         * report_interval： 上报时间间隔（上报频率），单位是秒，最小值是1秒1次. 如果小于1且设置了上报，将调用失败
         * 注意：如果设置上报的话，请设置SetEventCallBack, 然后在回调函数里面处理这个事件.
         * 上报事件是：NT_SP_E_EVENT_ID_DOWNLOAD_SPEED
         * 这个接口必须在StartXXX之前调用
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetReportDownloadSpeed(IntPtr handle, Int32 is_report, Int32 report_interval);

		/*
         * 主动获取下载速度
         * speed： 返回下载速度，单位是Byte/s
         * （注意：这个接口必须在startXXX之后调用，否则会失败）
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_GetDownloadSpeed(IntPtr handle, ref Int32 speed);

        /*
         * 获取视频时长
         * 对于直播的话，没有时长，调用结果未定义
         * 点播的话，如果获取成功返回NT_ERC_OK， 如果SDK还在解析中，则返回NT_ERC_SP_NEED_RETRY
		 */
         [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_GetDuration(IntPtr handle, ref Int64 duration);

        /*
         * 获取当前播放时间戳, 单位是毫秒(ms)
         * 注意:这个时间戳是视频源的时间戳，只支持点播. 直播不支持.
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_GetPlaybackPos(IntPtr handle, ref Int64 pos);

        /*
         * 获取当前拉流时间戳, 单位是毫秒(ms)
         * 注意:这个时间戳是视频源的时间戳，只支持点播. 直播不支持.
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_GetPullStreamPos(IntPtr handle, ref Int64 pos);

        /*
         * 设置播放位置,单位是毫秒(ms)
         * 注意:直播不支持,这个接口用于点播
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetPos(IntPtr handle, Int64 pos);

        /*
         * 暂停播放
         * isPause: 1表示暂停, 0表示恢复播放, 其他错误
         * 注意:直播不存在暂停的概念，所以直播不支持,这个接口用于点播
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_Pause(IntPtr handle, Int32 isPause);

        /*
         * 切换URL
         * url:要切换的url
         * switch_pos: 切换到新url以后，设置的播放位置, 默认请填0, 这个只对设置播放位置的点播url有效, 直播url无效
         * reserve: 保留参数
         * 注意: 1. 如果切换的url和正在播放的url相同,sdk则不做任何处理
         * 调用前置条件: 已经成功调用了 StartPlay, StartRecorder, StartPullStream 三个中的任意一个接口
         * 成功返回NT_ERC_OK
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SwitchURL(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]String url, Int64 switch_pos, Int32 reserve);

        /*
		 * 捕获图片
		 * file_name_utf8: 文件名称，utf8编码
		 * call_back_data: 回调时用户自定义数据
		 * call_back: 回调函数，用来通知用户截图已经完成或者失败
		 * 成功返回 NT_ERC_OK
		 * 只有在播放时调用才可能成功，其他情况下调用，返回错误.
		 * 因为生成PNG文件比较耗时，一般需要几百毫秒,为防止CPU过高，SDK会限制截图请求数量,当超过一定数量时，
		 * 调用这个接口会返回NT_ERC_SP_TOO_MANY_CAPTURE_IMAGE_REQUESTS. 这种情况下, 请延时一段时间，等SDK处理掉一些请求后，再尝试.
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_CaptureImage(IntPtr handle, IntPtr file_name_utf8,
            IntPtr call_back_data, SP_SDKCaptureImageCallBack call_back);

        /*
		 * 使用GDI绘制RGB32数据
		 * 32位的rgb格式, r, g, b各占8, 另外一个字节保留, 内存字节格式为: bb gg rr xx, 主要是和windows位图匹配, 在小端模式下，按DWORD类型操作，最高位是xx, 依次是rr, gg, bb
		 * 为了保持和windows位图兼容，步长(image_stride)必须是width_*4
		 * handle: 播放器句柄
		 * hdc: 绘制dc
		 * x_dst: 绘制面左上角x坐标
		 * y_dst: 绘制面左上角y坐标
		 * dst_width: 要绘制的宽度
		 * dst_height： 要绘制的高度
		 * x_src: 源图像x位置
		 * y_src: 原图像y位置
		 * rgb32_data: rgb32数据，格式参见前面的注释说明
		 * rgb32_data_size: 数据大小
		 * image_width： 图像实际宽度
		 * image_height： 图像实际高度
		 * image_stride： 图像步长
		 */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_GDIDrawRGB32(IntPtr handle, IntPtr hdc,
            Int32 x_dst, Int32 y_dst,
			Int32 dst_width, Int32 dst_height,
			Int32 x_src, Int32 y_src,
			Int32 src_width, Int32 src_height,
			IntPtr rgb32_data, UInt32 rgb32_data_size,
			Int32 image_width, Int32 image_height,
			Int32 image_stride);

        /*
         * 设置授权Key
         * 
         * reserve1: 请传0
         * reserve2: 请传NULL
         * 成功返回: NT_ERC_OK
         */
        [DllImport(@"SmartPlayerSDK.dll")]
        public static extern UInt32 NT_SP_SetSDKClientKey([MarshalAs(UnmanagedType.LPStr)]String cid, [MarshalAs(UnmanagedType.LPStr)]String key, Int32 reserve1, IntPtr reserve2);
	}
}