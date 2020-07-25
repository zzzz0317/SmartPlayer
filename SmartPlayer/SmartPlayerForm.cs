/*
 * SmartPlayerForm.cs
 * SmartPlayerForm
 * 
 * Github: https://github.com/daniulive/SmarterStreaming
 * Daniulive: http://www.daniulive.com
 * 
 * QQ交流群: 499687479 或 294891451
 * 商务合作: [QQ: 89030985 或 2679481035] [电话: 13072102209 13564529354]
 * 
 * Created by DaniuLive on 2017/04/19.
 * Copyright © 2014~2020 DaniuLive. All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using NT;
using System.Timers;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Diagnostics;


namespace SmartPlayer
{
    public partial class SmartPlayerForm : Form
    {
        private String[] progTitle = new String[4];
        private String refreshTitle()
        {
            String title = String.Join(" - ", progTitle);
            this.Text = title;
            return title;
        }

        [DllImport("kernel32", EntryPoint = "CopyMemory")]
        static extern void CopyMemory(IntPtr Destination, IntPtr Source, uint Length);

        //快照截图配置
        private String capture_image_path_ = "";

        //record配置
        private bool is_rec_video_ = true;
        private bool is_rec_audio_ = true;
        private String rec_dir_ = "";
        private String rec_name_file_prefix_ = "";
        private UInt32 max_file_size_ = 200 * 1024;    // 单位是KByte, 默认200MB
        private bool is_append_date_ = true;
        private bool is_append_time_ = true;
        private bool is_audio_transcode_aac_ = true;

        private IntPtr player_handle_;

        private Int32 width_ = 0;
        private Int32 height_ = 0;

        private bool is_sdk_init_ = false;
        private bool is_playing_ = false;
        private bool is_recording_ = false;

        private UInt32 connection_status_ = 0;
        private UInt32 buffer_status_ = 0;
        private Int32 buffer_percent_ = 0;
        private Int32 download_speed_ = -1;

        private Int32 rotate_degrees_ = 0;

        private bool is_support_h264_hardware_decoder_ = false;
        private bool is_support_h265_hardware_decoder_ = false;

        private System.Timers.Timer timer_clock_;

        SP_SDKVideoSizeCallBack video_size_call_back_;

        private NT_SP_VideoFrame cur_video_frame_ = new NT_SP_VideoFrame();

        bool is_gdi_render_ = false;

        //分辨率信息回调
        delegate void ResolutionNotifyCallback(Int32 width, Int32 height);
        ResolutionNotifyCallback resolution_notify_callback_;

        //视频数据回调
        SP_SDKVideoFrameCallBack video_frame_call_back_;
        delegate void VideoFrameCallBack(UInt32 status, NT_SP_VideoFrame frame);
        //delegate void VideoFrameCallBack(UInt32 status, Bitmap bitmap, int width, int height);
        VideoFrameCallBack set_video_frame_call_back_;

        //音频数据回调
        SP_SDKAudioPCMFrameCallBack audio_pcm_frame_call_back_;
        delegate void AudioPCMFrameCallBack(UInt32 status, IntPtr data, UInt32 size,
        Int32 sample_rate, Int32 channel, Int32 per_channel_sample_number);
        AudioPCMFrameCallBack set_audio_pcm_frame_call_back_;

        //视频时间戳回调
        //SP_SDKRenderVideoFrameTimestampCallBack video_frame_ts_callback_;
        //delegate void SetRenderVideoFrameTimestampCallBack(UInt64 timestamp, UInt64 reserve1, IntPtr reserve2);
        //SetRenderVideoFrameTimestampCallBack set_render_video_frame_timestamp_callback_;

        //快照信息回调
        SP_SDKCaptureImageCallBack capture_image_call_back_;
        delegate void SetCaptureImageCallBack(UInt32 result, String file_name);
        SetCaptureImageCallBack set_capture_image_call_back_;

        //录像信息回调
        SP_SDKRecorderCallBack record_call_back_;
        delegate void SetRecordCallBack(UInt32 status, [MarshalAs(UnmanagedType.LPStr)] String file_name);
        SetRecordCallBack set_record_call_back_;

        //event事件回调
        SP_SDKEventCallBack event_call_back_;
        delegate void SetEventCallBack(UInt32 event_id,
                Int64 param1,
                Int64 param2,
                UInt64 param3,
                [MarshalAs(UnmanagedType.LPStr)] String param4,
                [MarshalAs(UnmanagedType.LPStr)] String param5,
                IntPtr param6);
        SetEventCallBack set_event_call_back_;

        //接收到的用户数据回调
        SP_SDKUserDataCallBack user_data_call_back_;
        delegate void SetUserDataCallBack(UInt32 data_type,
            string data,
            UInt32 size,
            UInt64 timestamp,
            UInt64 reserve1,
            Int64 reserve2,
            IntPtr reserve3);

        SetUserDataCallBack set_user_data_call_back_;

        //接收到回调的SEI信息
        /*
        SP_SDKSEIDataCallBack sei_data_call_back_;
        delegate void SetSEIDataCallBack(byte[] data,
            UInt32 size,
            UInt64 timestamp,
            UInt64 reserve1,
            Int64 reserve2,
            IntPtr reserve3);

        SetSEIDataCallBack set_sei_data_call_back_;
        */

        public SmartPlayerForm(string[] args)
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.  
            SetStyle(ControlStyles.DoubleBuffer, true);         // 双缓冲

            textBox_buffer_time.Text = "100";
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

            progTitle[0] = "ZZ直播播放器";
            progTitle[1] = "未播放"; //分辨率
            progTitle[2] = "无状态信息";
            progTitle[3] = textBox_url.Text;
            refreshTitle();

            label_custom_text.Parent = this.playWnd;

            checkBox_fast_startup.Checked = true;

            checkBox_low_latency.Checked = true;
            checkBox_mute.Checked = true;
            checkBox_hardware_decoder.Checked = true;

            btn_play.Enabled = true;

            btn_capture_image.Enabled = false;
   
            is_sdk_init_ = false;
            is_playing_ = false;

            resolution_notify_callback_ = new ResolutionNotifyCallback(PlaybackWindowResized);

            set_video_frame_call_back_ = new VideoFrameCallBack(SDKVideoFrameCallBack);

            set_audio_pcm_frame_call_back_ = new AudioPCMFrameCallBack(SDKAudioPCMFrameCallBack);

            //set_render_video_frame_timestamp_callback_ = new SetRenderVideoFrameTimestampCallBack(RenderVideoFrameTsCallBack);

            set_capture_image_call_back_ = new SetCaptureImageCallBack(ImageCallBack);

            set_record_call_back_ = new SetRecordCallBack(RecordCallBack);

            set_event_call_back_ = new SetEventCallBack(EventCallBack);

            set_user_data_call_back_ = new SetUserDataCallBack(UserDataCallBack);

            //set_sei_data_call_back_ = new SetSEIDataCallBack(SEIDataCallBack);

            // 设置日志路径(请确保目录存在)
            //String log_path = "D:\\playerlog";
            //NTSmartLog.NT_SL_SetPath(log_path);

            UInt32 isInited = NT.NTSmartPlayerSDK.NT_SP_Init(0, IntPtr.Zero);
            if (isInited != 0)
            {
                MessageBox.Show("调用NT_SP_Init失败..");
                return;
            }

            is_sdk_init_ = true;

            checkBox_hardware_decoder.Enabled = false;

            is_support_h264_hardware_decoder_ = NT.NTBaseCodeDefine.NT_ERC_OK == NT.NTSmartPlayerSDK.NT_SP_IsSupportH264HardwareDecoder();
            is_support_h265_hardware_decoder_ = NT.NTBaseCodeDefine.NT_ERC_OK == NT.NTSmartPlayerSDK.NT_SP_IsSupportH265HardwareDecoder();

            if (player_handle_ == IntPtr.Zero)
            {
                player_handle_ = new IntPtr();

                UInt32 ret_open = NTSmartPlayerSDK.NT_SP_Open(out player_handle_, IntPtr.Zero, 0, IntPtr.Zero);

                if (ret_open != 0)
                {
                    player_handle_ = IntPtr.Zero;
                    MessageBox.Show("调用NT_SP_Open失败..");
                    return;
                }
            }

            if (is_support_h264_hardware_decoder_ || is_support_h265_hardware_decoder_)
            {
                checkBox_hardware_decoder.Enabled = true;
            }

            event_call_back_ = new SP_SDKEventCallBack(SDKEventCallBack);

            NTSmartPlayerSDK.NT_SP_SetEventCallBack(player_handle_, IntPtr.Zero, event_call_back_);

            timer_clock_ = new System.Timers.Timer();
            timer_clock_.Elapsed += new ElapsedEventHandler(OnTimer);
            // 1000为1秒
            timer_clock_.Interval = 1000;
            timer_clock_.Enabled = true;
        }


        private String GetHMSMsFormatStr(Int64 time_ms, bool is_append_ms, bool is_display_time_ms)
        {
	        Int64 remain_ms = time_ms % 1000;
	        Int64 t_s = time_ms / 1000;

	        Int64 h = t_s / 3600;
	        Int64 r = t_s % 3600;

	        Int64 m = r / 60;
	        Int64 s = r % 60;

	        String twss = "";
	        if (h != 0) 
            {
                twss = Convert.ToString(h) + "时";
            }

	        if (h != 0 || m != 0)
            {
                twss = twss + Convert.ToString(m) + "分";
            }
                
            if (h != 0 || m != 0 || s != 0)
            {
                twss = twss + Convert.ToString(s) + "秒";
            }

	        if ( is_append_ms )
	        {
		        if (remain_ms != 0)
                {
                    twss = twss + "." + Convert.ToString(remain_ms);
                }
	        }

	        if (is_display_time_ms)
	        {
                twss = twss + "(" + Convert.ToString(time_ms) + "ms)";
	        }

	        return twss;
        }
        
        protected void OnTimer(Object source, ElapsedEventArgs e)
        {
            if (player_handle_ != IntPtr.Zero && is_playing_)
            {
                Int64 play_pos = 0;
                if (NT.NTBaseCodeDefine.NT_ERC_OK == NTSmartPlayerSDK.NT_SP_GetPlaybackPos(player_handle_, ref play_pos))
                {
                    
                }
            }
        }

        private void PlaybackWindowResized(Int32 width, Int32 height)
        {
            width_ = width;
            height_ = height;

            int left = playWnd.Left;
            int top = playWnd.Top;

            textBox_resolution.Text = width + "*" + height;
            progTitle[1] = textBox_resolution.Text;
            refreshTitle();

            if (player_handle_ == IntPtr.Zero)
            {
                return;
            }

            NTSmartPlayerSDK.NT_SP_OnWindowSize(player_handle_, playWnd.Width, playWnd.Height);

            /*
            if (this.Width < 200 || this.Height < 200)
                return;

            int dw = 1;
            int dh = 1;
            CalRenderSize(this.Width - 60, this.Height - top - 60, width_, height_, ref dw, ref dh);

            playWnd.SetBounds(left, top, dw, dh);

            if (player_handle_ == IntPtr.Zero)
            {
                return;
            }

            NTSmartPlayerSDK.NT_SP_OnWindowSize(player_handle_, dw, dh);
             */
        }


        private void ImageCallBack(UInt32 result, String file_name)
        {
            if (file_name == null && file_name.Length == 0)
                return;

            MessageBox.Show(file_name);
        }

        private void RecordCallBack(UInt32 status, [MarshalAs(UnmanagedType.LPStr)] String file_name)
        {
            UInt32 sts = status;
            MessageBox.Show(file_name);
        }

        public void SP_SDKVideoSizeHandle(IntPtr handle, IntPtr userData, Int32 width, Int32 height)
        {
            if (playWnd.InvokeRequired)
            {
                BeginInvoke(resolution_notify_callback_, width, height);
            }
            else
            {
                resolution_notify_callback_(width, height);
            }
        }

        /*
        public void SDKVideoFrameCallBack(UInt32 status, NT_SP_VideoFrame frame)
        {
            //这里拿到回调frame，进行相关操作
            //....
            
            //release
            Marshal.FreeHGlobal(frame.plane0_);
        }
         */

        private void SmartPlayerForm_Paint(object sender, PaintEventArgs e)
        {
            if (player_handle_ == IntPtr.Zero || !is_gdi_render_ || !is_playing_)
            {
                return;
            }

            if (cur_video_frame_.plane0_ == IntPtr.Zero)
            {
                return;
            }

            Bitmap bitmap = new Bitmap(cur_video_frame_.width_, cur_video_frame_.height_, cur_video_frame_.stride0_,
                 System.Drawing.Imaging.PixelFormat.Format32bppRgb, cur_video_frame_.plane0_);

            int image_width = cur_video_frame_.width_;
            int image_height = cur_video_frame_.height_;

            Graphics g = e.Graphics;    //获取窗体画布
            g.SmoothingMode = SmoothingMode.HighSpeed;

            int limit_w = this.Width - 60;
            int limit_h = this.Height - playWnd.Top - 60;

            if (btn_check_render_scale_mode.Checked)
            {
                int d_w = 0, d_h = 0;
                int left_offset = 0;
                int top_offset = 0;

                Brush brush = new SolidBrush(Color.Black);
                g.FillRectangle(brush, playWnd.Left, playWnd.Top, limit_w, limit_h);

                GetRenderRect(limit_w, limit_h, image_width, image_height, ref left_offset, ref top_offset, ref d_w, ref d_h);
                g.DrawImage(bitmap, playWnd.Left + left_offset, playWnd.Top + top_offset, d_w, d_h);   //在窗体的画布中绘画出内存中的图像
            }
            else
            {
                g.DrawImage(bitmap, playWnd.Left, playWnd.Top, limit_w, limit_h);   //在窗体的画布中绘画出内存中的图像

                /*
                int d_w = 0, d_h = 0;

                CalRenderSize(limit_w, limit_h, image_width, image_height, ref d_w, ref d_h);

                if (d_w > 0 && d_h > 0)
                {
                    g.DrawImage(bitmap, playWnd.Left, playWnd.Top, d_w, d_h);   //在窗体的画布中绘画出内存中的图像
                }
                 */
            }
        }

        public void SDKVideoFrameCallBack(UInt32 status, NT_SP_VideoFrame frame)
        {
            if (cur_video_frame_.plane0_ != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(cur_video_frame_.plane0_);
                cur_video_frame_.plane0_ = IntPtr.Zero;
            }

            cur_video_frame_ = frame;

            this.Invalidate();
        }

        public void SDKAudioPCMFrameCallBack(UInt32 status, IntPtr data, UInt32 size,
        Int32 sample_rate, Int32 channel, Int32 per_channel_sample_number)
        {
            //这里拿到回调的PCM frame，进行相关操作（如自己播放）
            //label_debug.Text = per_channel_sample_number.ToString();

            //release
            Marshal.FreeHGlobal(data);
        }

        public void SetVideoFrameCallBack(IntPtr handle, IntPtr userData, UInt32 status, IntPtr frame)
        {
            if (frame == IntPtr.Zero)
            {
                return;
            }

            //如需直接处理RGB数据，请参考以下流程
            NT_SP_VideoFrame video_frame = (NT_SP_VideoFrame)Marshal.PtrToStructure(frame, typeof(NT_SP_VideoFrame));

            NT_SP_VideoFrame pVideoFrame = new NT_SP_VideoFrame();

            pVideoFrame.format_ = video_frame.format_;
            pVideoFrame.width_ = video_frame.width_;
            pVideoFrame.height_ = video_frame.height_;

            pVideoFrame.timestamp_ = video_frame.timestamp_;
            pVideoFrame.stride0_ = video_frame.stride0_;
            pVideoFrame.stride1_ = video_frame.stride1_;
            pVideoFrame.stride2_ = video_frame.stride2_;
            pVideoFrame.stride3_ = video_frame.stride3_;

            Int32 argb_size = video_frame.stride0_ * video_frame.height_;

            pVideoFrame.plane0_ = Marshal.AllocHGlobal(argb_size);
            CopyMemory(pVideoFrame.plane0_, video_frame.plane0_, (UInt32)argb_size);


            if (playWnd.InvokeRequired)
            {
                BeginInvoke(set_video_frame_call_back_, status, pVideoFrame);
            }
            else
            {
                set_video_frame_call_back_(status, pVideoFrame);
            }
        }

        public void SetAudioPCMFrameCallBack(IntPtr handle, IntPtr user_data,
             UInt32 status, IntPtr data, UInt32 size,
             Int32 sample_rate, Int32 channel, Int32 per_channel_sample_number)
        {
            if (data == IntPtr.Zero || size == 0)
            {
                return;
            }

            IntPtr pcmData = Marshal.AllocHGlobal((Int32)size);
            CopyMemory(pcmData, data, (UInt32)size);

            if (playWnd.InvokeRequired)
            {
                BeginInvoke(set_audio_pcm_frame_call_back_, status, pcmData, size, sample_rate, channel, per_channel_sample_number);
            }
            else
            {
                set_audio_pcm_frame_call_back_(status, pcmData, size, sample_rate, channel, per_channel_sample_number);
            }
        }

        /*
        public void SP_SDKRenderVideoFrameTimestampCallBack(IntPtr handle, IntPtr userData,
            UInt64 timestamp, UInt64 reserve1, IntPtr reserve2)
        {
            if (playWnd.InvokeRequired)
            {
                BeginInvoke(set_render_video_frame_timestamp_callback_, timestamp, reserve1, reserve2);
            }
            else
            {
                set_render_video_frame_timestamp_callback_(timestamp, reserve1, reserve2);
            }
        }
         */

        public void SDKCaptureImageCallBack(IntPtr handle, IntPtr userData, UInt32 result, IntPtr file_name)
        {
            if (file_name == IntPtr.Zero)
                return;

            int index = 0;

            while (true)
            {
                if (0 == Marshal.ReadByte(file_name, index))
                    break;

                index++;
            }

            byte[] file_name_buffer = new byte[index];

            Marshal.Copy(file_name, file_name_buffer, 0, index);

            byte[] dst_buffer = Encoding.Convert(Encoding.UTF8, Encoding.Default, file_name_buffer, 0, file_name_buffer.Length);
            String image_name = Encoding.Default.GetString(dst_buffer, 0, dst_buffer.Length);

            if (playWnd.InvokeRequired)
            {
                BeginInvoke(set_capture_image_call_back_, result, image_name);
            }
            else
            {
                set_capture_image_call_back_(result, image_name);
            }
        }

        public void SDKRecorderCallBack(IntPtr handle, IntPtr userData, UInt32 status, [MarshalAs(UnmanagedType.LPStr)] String file_name)
        {
            if (playWnd.InvokeRequired)
            {
                BeginInvoke(set_record_call_back_, status, file_name);
            }
            else
            {
                set_record_call_back_(status, file_name);
            }
        }

        public void SDKEventCallBack(IntPtr handle, IntPtr user_data,
            UInt32 event_id,
            Int64 param1,
            Int64 param2,
            UInt64 param3,
            [MarshalAs(UnmanagedType.LPStr)] String param4,
            [MarshalAs(UnmanagedType.LPStr)] String param5,
            IntPtr param6)
        {
            if (playWnd.InvokeRequired)
            {
                BeginInvoke(set_event_call_back_, event_id, param1, param2, param3, param4, param5, param6);
            }
            else
            {
                set_event_call_back_(event_id, param1, param2, param3, param4, param5, param6);
            }
        }

        public void SDKUserDataCallBack(IntPtr handle, IntPtr user_data,
            UInt32 data_type,
            IntPtr data,
            UInt32 size,
            UInt64 timestamp,
            UInt64 reserve1,
            Int64 reserve2,
            IntPtr reserve3)
        {
            byte[] bData = new byte[size];

            Marshal.Copy(data, bData, 0, (int)(size));

            string str_data = Encoding.UTF8.GetString(bData);

            if (playWnd.InvokeRequired)
            {
                BeginInvoke(set_user_data_call_back_, data_type, str_data, size, timestamp, reserve1, reserve2, reserve3);
            }
            else
            {
                set_user_data_call_back_(data_type, str_data, size, timestamp, reserve1, reserve2, reserve3);
            }
        }

        /*
        public void SDKSEIDataCallBack(IntPtr handle, IntPtr user_data,
            IntPtr data,
            UInt32 size,
            UInt64 timestamp,
            UInt64 reserve1,
            Int64 reserve2,
            IntPtr reserve3)
        {
            if (data == IntPtr.Zero || size <= 0)
                return;

            byte[] sei_data_buffer = new byte[size];

            Marshal.Copy(data, sei_data_buffer, 0, (int)size);

            if (playWnd.InvokeRequired)
            {
                BeginInvoke(set_sei_data_call_back_, sei_data_buffer, size, timestamp, reserve1, reserve2, reserve3);
            }
            else
            {
                set_sei_data_call_back_(sei_data_buffer, size, timestamp, reserve1, reserve2, reserve3);
            }
        }
         */

        private void EventCallBack(UInt32 event_id,
            Int64 param1,
            Int64 param2,
            UInt64 param3,
            [MarshalAs(UnmanagedType.LPStr)] String param4,
            [MarshalAs(UnmanagedType.LPStr)] String param5,
            IntPtr param6)
        {
            if (!is_playing_ && !is_recording_)
	        {
		        return;
	        }

            String show_str = "";

            if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_PLAYBACK_REACH_EOS == event_id)
            {
                StopPlayback();
                return;
            }
            else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_RECORDER_REACH_EOS == event_id)
            {
                StopRecorder();
                return;
            }
            else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_PULLSTREAM_REACH_EOS == event_id)
            {
                if (player_handle_ != IntPtr.Zero)
                {
                    NTSmartPlayerSDK.NT_SP_StopPullStream(player_handle_);
                }

                return;
            }
            else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_DURATION == event_id)
            {
                Int64 duration = param1;

                return;
            }
            else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_RTSP_STATUS_CODE == event_id)
            {
                int status_code = (int)param1;
                show_str = "RTSP incorrect status code received: " + status_code.ToString() + ", 请确保用户名/密码正确";

                MessageBox.Show(show_str);
            }

            if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_CONNECTING == event_id
                || (UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_CONNECTION_FAILED == event_id
                || (UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_CONNECTED == event_id
                || (UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_DISCONNECTED == event_id)
	        {
		        connection_status_ = event_id;
	        }

            if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_START_BUFFERING == event_id
                || (UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_BUFFERING == event_id
                || (UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_STOP_BUFFERING == event_id)
	        {
		        buffer_status_ = event_id;

                if ( (UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_BUFFERING == event_id )
		        {
			        buffer_percent_ = (Int32)param1;
		        }
	        }

            if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_DOWNLOAD_SPEED == event_id)
	        {
		        download_speed_ = (Int32)param1;
	        }

	        if ( connection_status_ != 0 )
	        {
		        show_str +="链接状态: ";

                if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_CONNECTING == connection_status_)
		        {
			        show_str += "链接中";
		        }
                else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_CONNECTION_FAILED == connection_status_)
		        {
			        show_str += "链接失败";
		        }
                else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_CONNECTED == connection_status_)
		        {
			        show_str += "链接成功";
		        }
                else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_DISCONNECTED == connection_status_)
		        {
			        show_str += "链接断开";
		        }
	        }

	        if (download_speed_ != -1)
	        {
                String ss = "  下载速度: " + (download_speed_ * 8 / 1000).ToString() + "kbps " +  (download_speed_ / 1024).ToString() + "KB/s";

		        show_str += ss;
	        }

	        if ( buffer_status_ != 0 )
	        {
		        show_str += "  缓冲状态: ";

                if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_START_BUFFERING == buffer_status_)
		        {
			        show_str += "开始缓冲";
		        }
                else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_BUFFERING == buffer_status_)
		        {
                    String ss = "缓冲中 " + buffer_percent_.ToString() + "%";
			        show_str += ss;
		        }
                else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_STOP_BUFFERING == buffer_status_)
		        {
			        show_str += "结束缓冲";
		        }
	        }

            if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_NEED_KEY == event_id)
            {
                show_str = "RTMP加密流，请设置播放需要的Key..";
                MessageBox.Show(show_str);
            }
            else if ((UInt32)NTSmartPlayerDefine.NT_SP_E_EVENT_ID.NT_SP_E_EVENT_ID_KEY_ERROR == event_id)
            {
                show_str = "RTMP加密流，Key错误，请重新设置..";
                MessageBox.Show(show_str);
            }

            lable_cur_status_txt.Text = show_str;
            progTitle[2] = lable_cur_status_txt.Text;
            refreshTitle();
        }

        private void UserDataCallBack(UInt32 data_type,
            string data,
            UInt32 size,
            UInt64 timestamp,
            UInt64 reserve1,
            Int64 reserve2,
            IntPtr reserve3)
        {
            //edit_player_msg_.Text = data + ", ts: " + timestamp.ToString();
        }

        /*
        private void SEIDataCallBack(byte[] data,
            UInt32 size,
            UInt64 timestamp,
            UInt64 reserve1,
            Int64 reserve2,
            IntPtr reserve3)
        {
            UInt32 sss = size;
            edit_player_msg_.Text = timestamp.ToString();
            //edit_player_msg_.Text = data + ", ts: " + timestamp.ToString();
        }
        */

        private void btn_play_Click(object sender, EventArgs e)
        {
            if (btn_play.Text == "播放")
            {
                if (!is_recording_)
                {
                    if (!InitCommonSDKParam())
                    {
                        MessageBox.Show("设置参数错误!");
                        return;
                    }
                }

                //video resolution callback
                video_size_call_back_ = new SP_SDKVideoSizeCallBack(SP_SDKVideoSizeHandle);
                NTSmartPlayerSDK.NT_SP_SetVideoSizeCallBack(player_handle_, IntPtr.Zero, video_size_call_back_);

                bool is_support_d3d_render = false;
                Int32 in_support_d3d_render = 0;

                if (NT.NTBaseCodeDefine.NT_ERC_OK == NTSmartPlayerSDK.NT_SP_IsSupportD3DRender(player_handle_, playWnd.Handle, ref in_support_d3d_render))
                {
                    if (1 == in_support_d3d_render)
                    {
                        is_support_d3d_render = true;
                    }
                }

                if (is_support_d3d_render)
                {
                    is_gdi_render_ = false;

                    // 支持d3d绘制的话，就用D3D绘制
                    NTSmartPlayerSDK.NT_SP_SetRenderWindow(player_handle_, playWnd.Handle);

                    if (btn_check_render_scale_mode.Checked)
                    {
                        NTSmartPlayerSDK.NT_SP_SetRenderScaleMode(player_handle_, 1);
                    }
                    else
                    {
                        NTSmartPlayerSDK.NT_SP_SetRenderScaleMode(player_handle_, 0);
                    }
                    
                }
                else
                {
                    is_gdi_render_ = true;

                    playWnd.Visible = false;

                    // 不支持D3D就让播放器吐出数据来，用GDI绘制

                    //video frame callback (YUV/RGB)
                    //format请参见 NT_SP_E_VIDEO_FRAME_FORMAT，如需回调YUV，请设置为 NT_SP_E_VIDEO_FRAME_FROMAT_I420
                    video_frame_call_back_ = new SP_SDKVideoFrameCallBack(SetVideoFrameCallBack);
                    NTSmartPlayerSDK.NT_SP_SetVideoFrameCallBack(player_handle_, (Int32)NT.NTSmartPlayerDefine.NT_SP_E_VIDEO_FRAME_FORMAT.NT_SP_E_VIDEO_FRAME_FORMAT_RGB32, IntPtr.Zero, video_frame_call_back_);
                }

                //audio_pcm_frame_call_back_ = new SP_SDKAudioPCMFrameCallBack(SetAudioPCMFrameCallBack);
                //NTSmartPlayerSDK.NT_SP_SetAudioPCMFrameCallBack(player_handle_, IntPtr.Zero, audio_pcm_frame_call_back_);

                //video timestamp callback
                //video_frame_ts_callback_ = new SP_SDKRenderVideoFrameTimestampCallBack(SP_SDKRenderVideoFrameTimestampCallBack);
                //NTSmartPlayerSDK.NT_SP_SetRenderVideoFrameTimestampCallBack(player_handle_, IntPtr.Zero, video_frame_ts_callback_);

                //设置是否播放出声音，如需自己播放PCM，可以设置第二个参数为0
                //NTSmartPlayerSDK.NT_SP_SetIsOutputAudioDevice(player_handle_, 0);

                user_data_call_back_ = new SP_SDKUserDataCallBack(SDKUserDataCallBack);
                NTSmartPlayerSDK.NT_SP_SetUserDataCallBack(player_handle_, IntPtr.Zero, user_data_call_back_);

                //sei_data_call_back_ = new SP_SDKSEIDataCallBack(SDKSEIDataCallBack);
                //NTSmartPlayerSDK.NT_SP_SetSEIDataCallBack(player_handle_, IntPtr.Zero, sei_data_call_back_);

                UInt32 ret_start = NTSmartPlayerSDK.NT_SP_StartPlay(player_handle_);

                if (ret_start != 0)
                {
                    MessageBox.Show("播放失败..");
                    return;
                }

                //转发相关，具体参见转发demo
                /*
                pull_stream_video_data_call_back_ = new SP_SDKPullStreamVideoDataCallBack(OnVideoDataHandle);
                pull_stream_audio_data_call_back_ = new SP_SDKPullStreamAudioDataCallBack(OnAudioDataHandle);

                NTSmartPlayerSDK.NT_SP_SetPullStreamVideoDataCallBack(pull_handle_, IntPtr.Zero, pull_stream_video_data_call_back_);
                NTSmartPlayerSDK.NT_SP_SetPullStreamAudioDataCallBack(pull_handle_, IntPtr.Zero, pull_stream_audio_data_call_back_);

                UInt32 ret = NTSmartPlayerSDK.NT_SP_StartPullStream(pull_handle_);
                 */

                btn_capture_image.Enabled = true;

                is_playing_ = true;

                btn_play.Text = "停止";
            }
            else
            {
                StopPlayback();
            }
            
        }

        private void checkBox_mute_CheckedChanged(object sender, EventArgs e)
        {
            if (  !is_playing_ )
            {
                return;
            }

            if ( checkBox_mute.Checked )
            {
                NTSmartPlayerSDK.NT_SP_SetMute(player_handle_, 1);
            }
            else
            {
                NTSmartPlayerSDK.NT_SP_SetMute(player_handle_, 0);
            }
        }

        private void buffer_time_key_press(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void SmartPlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定退出程序？", "退出程序", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
               e.Cancel = true;
            }

            timer_clock_.Enabled = false;
            
            if ( is_playing_ )
            {
                NTSmartPlayerSDK.NT_SP_StopPlay(player_handle_);
                is_playing_ = false;
            }

            if (is_recording_)
            {
                NTSmartPlayerSDK.NT_SP_StopRecorder(player_handle_);
                is_recording_ = false;
            }

            if ( player_handle_ != IntPtr.Zero)
            {
                NTSmartPlayerSDK.NT_SP_Close(player_handle_);
                player_handle_ = IntPtr.Zero;
            }
          
            if ( is_sdk_init_ )
            {
                NTSmartPlayerSDK.NT_SP_UnInit();
                is_sdk_init_ = false;
            }            
        }

        private void SmartPlayerForm_SizeChanged(object sender, EventArgs e)
        {
            int left = playWnd.Left;
            int top = playWnd.Top;

            playWnd.SetBounds(left, top, this.Width - 60, this.Height - top - 60);

            /*
            int left = playWnd.Left;
            int top = playWnd.Top;
             
            if (!is_playing_ || width_ == 0 || height_ == 0)
            {
                return;
            }

            int dw = 1;
            int dh = 1;

            int org_w = this.Width - 60;
            int org_h = this.Height - top - 20;
         
            CalRenderSize(this.Width - 60, this.Height - top - 60, width_, height_, ref dw, ref dh);

            playWnd.SetBounds(left, top, dw, dh);
             */
        }

        private void CalRenderSize(int limtWidth, int limtHeight, int videoWidth, int videoHeight, ref int dw, ref int dh)
        {
            dw = 100;
            dh = 100;

            if (videoWidth < 1)
                videoWidth = 1;

            if (videoHeight < 1)
                videoHeight = 1;

            // 先按宽度缩放
            double y = limtWidth * videoHeight * 1.0 / videoWidth;

            // 如果缩放高度还大，那就按高度继续缩放
            if (y > limtHeight)
            {
                // 尝试按高缩放一次
                double w2 = limtHeight * videoWidth * 1.0 / videoHeight;
                if (w2 <= limtWidth)
                {
                    dh = limtHeight;
                    dw = (int)w2;
                }
                else
                {
                    dh = limtHeight;
                    dw = (int)(limtWidth * limtHeight * 1.0 / y);
                }
            }
            else
            {
                dw = limtWidth;
                dh = (int)y;
            }

            if (dw < 16)
                dw = 16;

            if (dh < 16)
                dh = 16;

            if ((dw % 2) != 0)
                dw += 1;

            if ((dh % 2) != 0)
                dh += 1;
        }

        private void GetRenderRect(int limtWidth, int limtHeight, int image_w, int image_h, ref int left_offset, ref int top_offset, ref int dw, ref int dh)
        {
            if (limtWidth < 1 || limtHeight < 1)
	        {
                left_offset = 0;
                top_offset = 0;

                dw = limtWidth;
                dh = limtHeight;
                return;
	        }

	        if (image_w < 1 || image_h < 1)
	        {
                left_offset = 0;
                top_offset = 0;

                dw = limtWidth;
                dh = limtHeight;
                return;
	        }

	        // 按比例
	        double limit_ratio = limtWidth*1.0 / limtHeight;
	        double video_ratio = image_w*1.0 / image_h;

	        if (video_ratio > limit_ratio)
	        {
		        dw = limtWidth;
		        dh = (int)(dw * image_h*1.0 / image_w);

		        if (dh >limtHeight)
			        dh = limtHeight;
	        }
	        else
	        {
		        dh = limtHeight;
		        dw = (int)(dh * image_w*1.0 / image_h);

		        if (dw > limtWidth)
			        dw = limtWidth;
	        }

            left_offset = limtWidth / 2 - dw / 2;
            if (left_offset < 0)
                left_offset = 0;

            top_offset = limtHeight / 2 - dh / 2;
            if (top_offset < 0)
                top_offset = 0;
        }

        private void btn_set_capture_image_path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择截图路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                capture_image_path_ = dialog.SelectedPath.ToString();
            }
        }

        private void btn_capture_image_Click(object sender, EventArgs e)
        {
            if ( String.IsNullOrEmpty(capture_image_path_) )
	        {
		        MessageBox.Show("请先设置保存截图文件的目录! 点击截图左边的按钮设置!");
		        return;
	        }

	        if ( player_handle_ == IntPtr.Zero )
	        {
		        return;
	        }

	        if ( !is_playing_)
	        {
                MessageBox.Show("请在播放状态下截图!");
		        return;
	        }

            String name = capture_image_path_ + "\\" +  DateTime.Now.ToString("hh-mm-ss") + ".png";

            byte[] buffer1 = Encoding.Default.GetBytes(name);
            byte[] buffer2 = Encoding.Convert(Encoding.Default, Encoding.UTF8, buffer1, 0, buffer1.Length);

            byte[] buffer3 = new byte[buffer2.Length + 1];
            buffer3[buffer2.Length] = 0;

            Array.Copy(buffer2, buffer3, buffer2.Length);

            IntPtr file_name_ptr = Marshal.AllocHGlobal(buffer3.Length);
            Marshal.Copy(buffer3, 0, file_name_ptr, buffer3.Length);

            capture_image_call_back_ = new SP_SDKCaptureImageCallBack(SDKCaptureImageCallBack);

            UInt32 ret = NTSmartPlayerSDK.NT_SP_CaptureImage(player_handle_, file_name_ptr, IntPtr.Zero, capture_image_call_back_);

            Marshal.FreeHGlobal(file_name_ptr);

            if (NT.NTBaseCodeDefine.NT_ERC_OK == ret)
	        {
		        // 发送截图请求成功
	        }
            else if ((UInt32)NT.NTSmartPlayerDefine.SP_E_ERROR_CODE.NT_ERC_SP_TOO_MANY_CAPTURE_IMAGE_REQUESTS == ret)
	        {
		        // 通知用户延时
		        MessageBox.Show("Too many capture image requests!");
	        }
	        else
	        {
		        // 其他失败
	        }
        }

        private void btn_record_config_Click(object sender, EventArgs e)
        {
            RecordConfigForm record_config_dlg = new RecordConfigForm(is_rec_video_, is_rec_audio_, rec_dir_, rec_name_file_prefix_, max_file_size_, is_append_date_, is_append_time_, is_audio_transcode_aac_);

            record_config_dlg.ShowDialog();

            String rec_dir = record_config_dlg.RecDir();

            if (!String.IsNullOrEmpty(rec_dir))
            {
                rec_dir_ = rec_dir;
            }
            else
            {
                MessageBox.Show("未设置录像保存路径，默认保存到rec文件夹下..");
            }

            is_rec_video_ = record_config_dlg.IsRecVideo();
            is_rec_audio_ = record_config_dlg.IsRecAudio();
            rec_name_file_prefix_ = record_config_dlg.RecNameFilePrefix();
            max_file_size_ = record_config_dlg.MaxFileSize();
            is_append_date_ = record_config_dlg.IsAppendDate();
            is_append_time_ = record_config_dlg.IsAppendTime();
            is_audio_transcode_aac_ = record_config_dlg.IsAudioTanscodeAAC();
        }

        private void btn_record_Click(object sender, EventArgs e)
        {
            if (player_handle_ == IntPtr.Zero)
                return;

            if (btn_record.Text == "录像")
            {
                if (!is_rec_video_ && !is_rec_audio_)
                {
                    MessageBox.Show("音频录制选项和视频录制选项至少需要选择一个!");
                    return;
                }

                if (!is_playing_)
                {
                    if (!InitCommonSDKParam())
                    {
                        MessageBox.Show("设置参数错误!");
                        return;
                    }
                }

                NTSmartPlayerSDK.NT_SP_SetRecorderVideo(player_handle_, is_rec_video_ ? 1 : 0);
                NTSmartPlayerSDK.NT_SP_SetRecorderAudio(player_handle_, is_rec_audio_ ? 1 : 0);

                UInt32 ret = NTSmartPlayerSDK.NT_SP_SetRecorderDirectory(player_handle_, rec_dir_);
                if (NT.NTBaseCodeDefine.NT_ERC_OK != ret)
                {
                    MessageBox.Show("设置录像目录失败，请确保目录存在且是英文目录");
                    return;
                }

                NTSmartPlayerSDK.NT_SP_SetRecorderFileMaxSize(player_handle_, max_file_size_);

                NT_SP_RecorderFileNameRuler rec_name_ruler = new NT_SP_RecorderFileNameRuler();

                rec_name_ruler.type_ = 0;
                rec_name_ruler.file_name_prefix_ = rec_name_file_prefix_;
                rec_name_ruler.append_date_ = is_append_date_ ? 1 : 0;
                rec_name_ruler.append_time_ = is_append_time_ ? 1 : 0;

                NTSmartPlayerSDK.NT_SP_SetRecorderFileNameRuler(player_handle_, ref rec_name_ruler);

                record_call_back_ = new SP_SDKRecorderCallBack(SDKRecorderCallBack);

                NTSmartPlayerSDK.NT_SP_SetRecorderCallBack(player_handle_, IntPtr.Zero, record_call_back_);

                NTSmartPlayerSDK.NT_SP_SetRecorderAudioTranscodeAAC(player_handle_, is_audio_transcode_aac_ ? 1 : 0);

                if (NT.NTBaseCodeDefine.NT_ERC_OK != NTSmartPlayerSDK.NT_SP_StartRecorder(player_handle_))
                {
                    MessageBox.Show("录像失败!");
                    return;
                }

                btn_record.Text = "停止录像";
                is_recording_ = true;
            }
            else
            {
                StopRecorder();
            }
        }

        private bool InitCommonSDKParam()
        {
            if (is_playing_ || is_recording_)
            {
                return true;
            }

	        if ( IntPtr.Zero == player_handle_ )
		        return false;

            String url = textBox_url.Text;
            progTitle[3] = textBox_url.Text;
            refreshTitle();

            if (String.IsNullOrEmpty(url))
            {
                return false;
            }

            Int32 buffer_time = int.Parse(textBox_buffer_time.Text);

            NTSmartPlayerSDK.NT_SP_SetBuffer(player_handle_, buffer_time);

            // 设置rtsp tcp模式，rtmp不使用, 可以不设置
            if (checkBox_rtsp_tcp.Checked)
            {
                NTSmartPlayerSDK.NT_SP_SetRTSPTcpMode(player_handle_, 1);
            }
            else
            {
                NTSmartPlayerSDK.NT_SP_SetRTSPTcpMode(player_handle_, 0);
            }

            //RTSP timeout设置
            Int32 rtsp_timeout = 10;
            NTSmartPlayerSDK.NT_SP_SetRtspTimeout(player_handle_, rtsp_timeout);

            //RTSP TCP/UDP自动切换设置
            Int32 is_auto_switch_tcp_udp = 1;
            NTSmartPlayerSDK.NT_SP_SetRtspAutoSwitchTcpUdp(player_handle_, is_auto_switch_tcp_udp);

            if (checkBox_mute.Checked)
            {
                NTSmartPlayerSDK.NT_SP_SetMute(player_handle_, 1);
            }
            else
            {
                NTSmartPlayerSDK.NT_SP_SetMute(player_handle_, 0);
            }

            if (checkBox_fast_startup.Checked)
            {
                NTSmartPlayerSDK.NT_SP_SetFastStartup(player_handle_, 1);
            }
            else
            {
                NTSmartPlayerSDK.NT_SP_SetFastStartup(player_handle_, 0);
            }

            if (checkBox_hardware_decoder.Checked)
            {
                NTSmartPlayerSDK.NT_SP_SetH264HardwareDecoder(player_handle_, is_support_h264_hardware_decoder_ ? 1 : 0, 0);
                NTSmartPlayerSDK.NT_SP_SetH265HardwareDecoder(player_handle_, is_support_h265_hardware_decoder_ ? 1 : 0, 0);
            }
            else
            {
                NTSmartPlayerSDK.NT_SP_SetH264HardwareDecoder(player_handle_, 0, 0);
                NTSmartPlayerSDK.NT_SP_SetH265HardwareDecoder(player_handle_, 0, 0);
            }

            // 设置是否只解码关键帧
            if (btn_check_only_decode_video_key_frame.Checked)
            {
                NTSmartPlayerSDK.NT_SP_SetOnlyDecodeVideoKeyFrame(player_handle_, 1);
            }
            else
            {
                NTSmartPlayerSDK.NT_SP_SetOnlyDecodeVideoKeyFrame(player_handle_, 0);
            }

            // 设置低延迟模式
            if (checkBox_low_latency.Checked)
            {
                NTSmartPlayerSDK.NT_SP_SetLowLatencyMode(player_handle_, 1);
            }
            else
            {
                NTSmartPlayerSDK.NT_SP_SetLowLatencyMode(player_handle_, 0);
            }

            NTSmartPlayerSDK.NT_SP_SetRotation(player_handle_, rotate_degrees_);

            NTSmartPlayerSDK.NT_SP_SetReportDownloadSpeed(player_handle_, 1, 1);

            NTSmartPlayerSDK.NT_SP_SetURL(player_handle_, url);

            //如需播放RTMP加密流，请打开此段代码，设置Key和IV信息
            /*
            String key_str = "1234567890123456";

            UInt32 key_size = 16;

            if (key_str.Length <= 16)
            {
                key_size = 16;
            }
            else if (key_str.Length <= 24)
            {
                key_size = 24;
            }
            else
            {
                key_size = 32;
            }

            byte[] key = new byte[key_size];

            if (key_str.Length <= 32)
            {
                key = System.Text.Encoding.Default.GetBytes(key_str);
            }
            else
            {
                key = System.Text.Encoding.Default.GetBytes(key_str.Substring(0, 32));
            }

            NTSmartPlayerSDK.NT_SP_SetKey(player_handle_, key, key_size);

            String iv_str = "1234567890123456789012345678901211";

            UInt32 iv_size = 16;

            byte[] iv = new byte[iv_size];

            if (iv_str.Length <= 16)
            {
                iv = System.Text.Encoding.Default.GetBytes(iv_str);
            }
            else
            {
                iv = System.Text.Encoding.Default.GetBytes(key_str.Substring(0, 16));
            }
            
            iv = System.Text.Encoding.Default.GetBytes(iv_str);

            NTSmartPlayerSDK.NT_SP_SetDecryptionIV(player_handle_, iv, iv_size);
             */

	        return true;
        }

        private void btn_rotation_Click(object sender, EventArgs e)
        {
            rotate_degrees_ += 90;
            rotate_degrees_ = rotate_degrees_ % 360;

            if (0 == rotate_degrees_)
            {
                btn_rotation.Text = "旋转90度";
            }
            else if (90 == rotate_degrees_)
            {
                btn_rotation.Text = "旋转180度";
            }
            else if (180 == rotate_degrees_)
            {
                btn_rotation.Text = "旋转270度";
            }
            else if (270 == rotate_degrees_)
            {
                btn_rotation.Text = "不旋转";
            }

            if (player_handle_ != IntPtr.Zero)
            {
                NTSmartPlayerSDK.NT_SP_SetRotation(player_handle_, rotate_degrees_);
            }
        }

        private void StopPlayback()
        {
            if (player_handle_ == IntPtr.Zero)
            {
                return;
            }

            if (is_playing_)
            {
                //NT_SP_Stop是老接口，如使用，请配合NT_SP_Start同步使用
                //NTSmartPlayerSDK.NT_SP_Stop(player_handle_);
                NTSmartPlayerSDK.NT_SP_StopPlay(player_handle_);
                is_playing_ = false;

                //playWnd.Invalidate();   //清空最后一帧数据，如不加，默认保留最后一帧画面
            }

            if (cur_video_frame_.plane0_ != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(cur_video_frame_.plane0_);
                cur_video_frame_.plane0_ = IntPtr.Zero;
            }

            btn_capture_image.Enabled = false;
            textBox_resolution.Text = "未播放";
            progTitle[1] = textBox_resolution.Text;
            btn_play.Text = "播放";
            lable_cur_status_txt.Text = "无状态信息";
            progTitle[2] = lable_cur_status_txt.Text;
            //edit_player_msg_.Text = "";
            refreshTitle();
        }
        private void StopRecorder()
        {
            if (player_handle_ == IntPtr.Zero)
            {
                return;
            }

            NTSmartPlayerSDK.NT_SP_StopRecorder(player_handle_);

            btn_record.Text = "录像";
            is_recording_ = false;
        }

        private void btn_switch_url_Click(object sender, EventArgs e)
        {
            if (IntPtr.Zero == player_handle_)
                return;

            String url = textBox_url.Text;
            progTitle[3] = textBox_url.Text;

            if (String.IsNullOrEmpty(url))
            {
                lable_cur_status_txt.Text = "Switch url failed, url is null!";
                return;
            }

            if (NT.NTBaseCodeDefine.NT_ERC_OK == NTSmartPlayerSDK.NT_SP_SwitchURL(player_handle_, url, 0, 0))
            {
                lable_cur_status_txt.Text = "Switch url ok.";
            }
            else
            {
                lable_cur_status_txt.Text = "Switch url failed";
            }
            progTitle[2] = lable_cur_status_txt.Text;

            refreshTitle();
        }


        private void OnBnClickedCheckFlipHorizontal(object sender, EventArgs e)
        {
            if (player_handle_ != IntPtr.Zero)
            {
                if (btn_check_flip_horizontal.Checked)
                {
                    NTSmartPlayerSDK.NT_SP_SetFlipHorizontal(player_handle_, 1);
                }
                else
                {
                    NTSmartPlayerSDK.NT_SP_SetFlipHorizontal(player_handle_, 0);
                }
            }
        }

        private void OnBnClickedCheckFlipVertical(object sender, EventArgs e)
        {
            if (player_handle_ != IntPtr.Zero)
            {
                if (btn_check_flip_vertical.Checked)
                {
                    NTSmartPlayerSDK.NT_SP_SetFlipVertical(player_handle_, 1);
                }
                else
                {
                    NTSmartPlayerSDK.NT_SP_SetFlipVertical(player_handle_, 0);
                }
            }
        }

        private void playWnd_Resize(object sender, EventArgs e)
        {
            if (!is_playing_ || player_handle_ == IntPtr.Zero)
            {
                return;
            }

            NTSmartPlayerSDK.NT_SP_OnWindowSize(player_handle_, playWnd.Width, playWnd.Height);
        }

        private void btn_check_only_decode_video_key_frame_CheckedChanged(object sender, EventArgs e)
        {
            // 设置是否只解码关键帧
            if (btn_check_only_decode_video_key_frame.Checked)
            {
                NTSmartPlayerSDK.NT_SP_SetOnlyDecodeVideoKeyFrame(player_handle_, 1);
            }
            else
            {
                NTSmartPlayerSDK.NT_SP_SetOnlyDecodeVideoKeyFrame(player_handle_, 0);
            }
        }

        private void btn_check_render_scale_mode_CheckedChanged(object sender, EventArgs e)
        {
            if (player_handle_ != IntPtr.Zero)
            {
                if (!is_gdi_render_)
                {
                    if (btn_check_render_scale_mode.Checked)
                    {
                        NTSmartPlayerSDK.NT_SP_SetRenderScaleMode(player_handle_, 1);
                    }
                    else
                    {
                        NTSmartPlayerSDK.NT_SP_SetRenderScaleMode(player_handle_, 0);
                    }
                }
            }

        }

        private void playWnd_Click(object sender, EventArgs e)
        {
            groupBoxSet.Visible = !groupBoxSet.Visible;
        }

        private void btn_show_custom_text_Click(object sender, EventArgs e)
        {
            label_custom_text.Visible = true;
            if (textBox_custom_text.Text == "") {
                label_custom_text.Text = textBox_url.Text;
            } else {
                label_custom_text.Text = textBox_custom_text.Text;
            }
        }

        private void label_custom_text_Click(object sender, EventArgs e)
        {
            label_custom_text.Visible = false;
        }

        private void btn_about_Click(object sender, EventArgs e)
        {
            String sc = "关于 " + progTitle[0];
            String s = sc;
            s += "\n" + "";
            s += "\n" + "基于 Daniulive WIN-PlayerSDK-CSharp-Demo-2020-04-29";
            s += "\n" + "";
            s += "\n" + "张哲\nhome.asec01.net";
            MessageBox.Show(s,sc);
        }
    }
}
