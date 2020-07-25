/*
 * smart_log.cs
 * smart_log
 * 
 * Github: https://github.com/daniulive/SmarterStreaming
 * 
 * Created by DaniuLive on 2015/05/08.
 * Copyright © 2014~2017 DaniuLive. All rights reserved.
 */

using System;
using System.Runtime.InteropServices;
namespace NT
{
	/// <summary>
	/// NTSmartLog 的摘要说明。
	/// </summary>
    public class NTSmartLog
    {
        /*
		设置日志目录，Windows平台请设置宽字符，比如"D:\\xxx\gg"
		*/
        [DllImport(@"SmartLog.dll")]
        public static extern UInt32 NT_SL_SetPath([MarshalAs(UnmanagedType.LPWStr)]String path);

		/*
		设置日志等级，level请参考SL_LOG_LEVEL
		*/
        [DllImport(@"SmartLog.dll")]
        public static extern UInt32 NT_SL_SetLevel(UInt32 level);

		/*
		将缓冲全部写入
		*/
        [DllImport(@"SmartLog.dll")]
		public static extern UInt32 NT_SL_Flush();

		/*
		万能接口, 设置参数， 大多数问题， 这些接口都能解决
		*/
        [DllImport(@"SmartLog.dll")]
        public static extern UInt32 NT_SL_SetParam(UInt32 id, IntPtr pData);

		/*
		万能接口, 得到参数， 大多数问题，这些接口都能解决
		*/
        [DllImport(@"SmartLog.dll")]
        public static extern UInt32 NT_SL_GetParam(UInt32 id, IntPtr pData);

	}
}