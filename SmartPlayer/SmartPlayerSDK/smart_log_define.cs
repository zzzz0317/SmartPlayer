/*
 * smart_log_define.cs
 * smart_log_define
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
    public class NTSmartLogDefine
    {		
		/* 错误码 */
		public enum SL_E_ERROR_CODE : uint
		{
			NT_ERC_SL_LEVEL_ERROR = (NTBaseCodeDefine.NT_ERC_SMART_LOG | 0x1),
		}

		/* 设置参数ID, 这个目前这么写 */
		public enum SL_E_PARAM_ID : uint
		{
			SL_PARAM_ID_BASE = (NTBaseCodeDefine.NT_PARAM_ID_SMART_LOG | 0x1),
		}

        /* LOG级别 */
        public enum SL_LOG_LEVEL : uint
		{
			SL_TRACE_LEVEL = 0,
			SL_DEBUG_LEVEL,
			SL_INFO_LEVEL,
			SL_WARNING_LEVEL,
			SL_ERROR_LEVEL,
			SL_FATAL_LEVEL,
			SL_CLOSE_LEVEL,
		}
	}
}