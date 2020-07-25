namespace SmartPlayer
{
    partial class SmartPlayerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_play = new System.Windows.Forms.Button();
            this.playWnd = new System.Windows.Forms.PictureBox();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.lable_address = new System.Windows.Forms.Label();
            this.checkBox_mute = new System.Windows.Forms.CheckBox();
            this.checkBox_fast_startup = new System.Windows.Forms.CheckBox();
            this.textBox_buffer_time = new System.Windows.Forms.TextBox();
            this.label_buffer_time = new System.Windows.Forms.Label();
            this.checkBox_rtsp_tcp = new System.Windows.Forms.CheckBox();
            this.textBox_resolution = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_set_capture_image_path = new System.Windows.Forms.Button();
            this.btn_capture_image = new System.Windows.Forms.Button();
            this.btn_record_config = new System.Windows.Forms.Button();
            this.btn_record = new System.Windows.Forms.Button();
            this.label_cur_status = new System.Windows.Forms.Label();
            this.lable_cur_status_txt = new System.Windows.Forms.Label();
            this.checkBox_low_latency = new System.Windows.Forms.CheckBox();
            this.btn_rotation = new System.Windows.Forms.Button();
            this.btn_switch_url = new System.Windows.Forms.Button();
            this.btn_check_flip_horizontal = new System.Windows.Forms.CheckBox();
            this.btn_check_flip_vertical = new System.Windows.Forms.CheckBox();
            this.checkBox_hardware_decoder = new System.Windows.Forms.CheckBox();
            this.btn_check_only_decode_video_key_frame = new System.Windows.Forms.CheckBox();
            this.btn_check_render_scale_mode = new System.Windows.Forms.CheckBox();
            this.groupBoxSet = new System.Windows.Forms.GroupBox();
            this.label_custom_text = new System.Windows.Forms.Label();
            this.btn_show_custom_text = new System.Windows.Forms.Button();
            this.textBox_custom_text = new System.Windows.Forms.TextBox();
            this.btn_about = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.playWnd)).BeginInit();
            this.groupBoxSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_play
            // 
            this.btn_play.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_play.Location = new System.Drawing.Point(979, 21);
            this.btn_play.Margin = new System.Windows.Forms.Padding(4);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(87, 29);
            this.btn_play.TabIndex = 0;
            this.btn_play.Text = "播放";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // playWnd
            // 
            this.playWnd.BackColor = System.Drawing.Color.Black;
            this.playWnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playWnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playWnd.Location = new System.Drawing.Point(0, 0);
            this.playWnd.Margin = new System.Windows.Forms.Padding(4);
            this.playWnd.Name = "playWnd";
            this.playWnd.Size = new System.Drawing.Size(1192, 776);
            this.playWnd.TabIndex = 1;
            this.playWnd.TabStop = false;
            this.playWnd.Click += new System.EventHandler(this.playWnd_Click);
            this.playWnd.Resize += new System.EventHandler(this.playWnd_Resize);
            // 
            // textBox_url
            // 
            this.textBox_url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_url.Location = new System.Drawing.Point(62, 25);
            this.textBox_url.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(909, 25);
            this.textBox_url.TabIndex = 3;
            // 
            // lable_address
            // 
            this.lable_address.AutoSize = true;
            this.lable_address.Location = new System.Drawing.Point(7, 28);
            this.lable_address.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lable_address.Name = "lable_address";
            this.lable_address.Size = new System.Drawing.Size(47, 15);
            this.lable_address.TabIndex = 4;
            this.lable_address.Text = "Addr:";
            // 
            // checkBox_mute
            // 
            this.checkBox_mute.AutoSize = true;
            this.checkBox_mute.Location = new System.Drawing.Point(308, 58);
            this.checkBox_mute.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_mute.Name = "checkBox_mute";
            this.checkBox_mute.Size = new System.Drawing.Size(56, 19);
            this.checkBox_mute.TabIndex = 5;
            this.checkBox_mute.Text = "静音";
            this.checkBox_mute.UseVisualStyleBackColor = true;
            this.checkBox_mute.CheckedChanged += new System.EventHandler(this.checkBox_mute_CheckedChanged);
            // 
            // checkBox_fast_startup
            // 
            this.checkBox_fast_startup.AutoSize = true;
            this.checkBox_fast_startup.Location = new System.Drawing.Point(214, 58);
            this.checkBox_fast_startup.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_fast_startup.Name = "checkBox_fast_startup";
            this.checkBox_fast_startup.Size = new System.Drawing.Size(86, 19);
            this.checkBox_fast_startup.TabIndex = 6;
            this.checkBox_fast_startup.Text = "快速启动";
            this.checkBox_fast_startup.UseVisualStyleBackColor = true;
            // 
            // textBox_buffer_time
            // 
            this.textBox_buffer_time.Location = new System.Drawing.Point(142, 85);
            this.textBox_buffer_time.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_buffer_time.Name = "textBox_buffer_time";
            this.textBox_buffer_time.Size = new System.Drawing.Size(117, 25);
            this.textBox_buffer_time.TabIndex = 7;
            this.textBox_buffer_time.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.buffer_time_key_press);
            // 
            // label_buffer_time
            // 
            this.label_buffer_time.AutoSize = true;
            this.label_buffer_time.Location = new System.Drawing.Point(7, 88);
            this.label_buffer_time.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_buffer_time.Name = "label_buffer_time";
            this.label_buffer_time.Size = new System.Drawing.Size(127, 15);
            this.label_buffer_time.TabIndex = 8;
            this.label_buffer_time.Text = "BufferTime(ms):";
            // 
            // checkBox_rtsp_tcp
            // 
            this.checkBox_rtsp_tcp.AutoSize = true;
            this.checkBox_rtsp_tcp.Location = new System.Drawing.Point(116, 58);
            this.checkBox_rtsp_tcp.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_rtsp_tcp.Name = "checkBox_rtsp_tcp";
            this.checkBox_rtsp_tcp.Size = new System.Drawing.Size(90, 19);
            this.checkBox_rtsp_tcp.TabIndex = 9;
            this.checkBox_rtsp_tcp.Text = "RTSP-TCP";
            this.checkBox_rtsp_tcp.UseVisualStyleBackColor = true;
            // 
            // textBox_resolution
            // 
            this.textBox_resolution.Location = new System.Drawing.Point(75, 118);
            this.textBox_resolution.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_resolution.Name = "textBox_resolution";
            this.textBox_resolution.ReadOnly = true;
            this.textBox_resolution.Size = new System.Drawing.Size(117, 25);
            this.textBox_resolution.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 121);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "分辨率:";
            // 
            // btn_set_capture_image_path
            // 
            this.btn_set_capture_image_path.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_set_capture_image_path.Location = new System.Drawing.Point(1074, 95);
            this.btn_set_capture_image_path.Margin = new System.Windows.Forms.Padding(4);
            this.btn_set_capture_image_path.Name = "btn_set_capture_image_path";
            this.btn_set_capture_image_path.Size = new System.Drawing.Size(87, 29);
            this.btn_set_capture_image_path.TabIndex = 17;
            this.btn_set_capture_image_path.Text = "截图路径";
            this.btn_set_capture_image_path.UseVisualStyleBackColor = true;
            this.btn_set_capture_image_path.Click += new System.EventHandler(this.btn_set_capture_image_path_Click);
            // 
            // btn_capture_image
            // 
            this.btn_capture_image.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_capture_image.Location = new System.Drawing.Point(979, 95);
            this.btn_capture_image.Margin = new System.Windows.Forms.Padding(4);
            this.btn_capture_image.Name = "btn_capture_image";
            this.btn_capture_image.Size = new System.Drawing.Size(87, 29);
            this.btn_capture_image.TabIndex = 18;
            this.btn_capture_image.Text = "截图";
            this.btn_capture_image.UseVisualStyleBackColor = true;
            this.btn_capture_image.Click += new System.EventHandler(this.btn_capture_image_Click);
            // 
            // btn_record_config
            // 
            this.btn_record_config.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_record_config.Location = new System.Drawing.Point(1074, 58);
            this.btn_record_config.Margin = new System.Windows.Forms.Padding(4);
            this.btn_record_config.Name = "btn_record_config";
            this.btn_record_config.Size = new System.Drawing.Size(87, 29);
            this.btn_record_config.TabIndex = 19;
            this.btn_record_config.Text = "录像配置";
            this.btn_record_config.UseVisualStyleBackColor = true;
            this.btn_record_config.Click += new System.EventHandler(this.btn_record_config_Click);
            // 
            // btn_record
            // 
            this.btn_record.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_record.Location = new System.Drawing.Point(979, 58);
            this.btn_record.Margin = new System.Windows.Forms.Padding(4);
            this.btn_record.Name = "btn_record";
            this.btn_record.Size = new System.Drawing.Size(87, 29);
            this.btn_record.TabIndex = 20;
            this.btn_record.Text = "录像";
            this.btn_record.UseVisualStyleBackColor = true;
            this.btn_record.Click += new System.EventHandler(this.btn_record_Click);
            // 
            // label_cur_status
            // 
            this.label_cur_status.AutoSize = true;
            this.label_cur_status.Location = new System.Drawing.Point(200, 121);
            this.label_cur_status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_cur_status.Name = "label_cur_status";
            this.label_cur_status.Size = new System.Drawing.Size(82, 15);
            this.label_cur_status.TabIndex = 21;
            this.label_cur_status.Text = "当前状态：";
            // 
            // lable_cur_status_txt
            // 
            this.lable_cur_status_txt.AutoSize = true;
            this.lable_cur_status_txt.Location = new System.Drawing.Point(200, 139);
            this.lable_cur_status_txt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lable_cur_status_txt.Name = "lable_cur_status_txt";
            this.lable_cur_status_txt.Size = new System.Drawing.Size(82, 15);
            this.lable_cur_status_txt.TabIndex = 22;
            this.lable_cur_status_txt.Text = "无状态信息";
            // 
            // checkBox_low_latency
            // 
            this.checkBox_low_latency.AutoSize = true;
            this.checkBox_low_latency.Location = new System.Drawing.Point(7, 58);
            this.checkBox_low_latency.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_low_latency.Name = "checkBox_low_latency";
            this.checkBox_low_latency.Size = new System.Drawing.Size(101, 19);
            this.checkBox_low_latency.TabIndex = 24;
            this.checkBox_low_latency.Text = "低延迟模式";
            this.checkBox_low_latency.UseVisualStyleBackColor = true;
            // 
            // btn_rotation
            // 
            this.btn_rotation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_rotation.Location = new System.Drawing.Point(885, 95);
            this.btn_rotation.Margin = new System.Windows.Forms.Padding(4);
            this.btn_rotation.Name = "btn_rotation";
            this.btn_rotation.Size = new System.Drawing.Size(87, 29);
            this.btn_rotation.TabIndex = 25;
            this.btn_rotation.Text = "旋转";
            this.btn_rotation.UseVisualStyleBackColor = true;
            this.btn_rotation.Click += new System.EventHandler(this.btn_rotation_Click);
            // 
            // btn_switch_url
            // 
            this.btn_switch_url.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_switch_url.Location = new System.Drawing.Point(1074, 21);
            this.btn_switch_url.Margin = new System.Windows.Forms.Padding(4);
            this.btn_switch_url.Name = "btn_switch_url";
            this.btn_switch_url.Size = new System.Drawing.Size(87, 29);
            this.btn_switch_url.TabIndex = 26;
            this.btn_switch_url.Text = "切换地址";
            this.btn_switch_url.UseVisualStyleBackColor = true;
            this.btn_switch_url.Click += new System.EventHandler(this.btn_switch_url_Click);
            // 
            // btn_check_flip_horizontal
            // 
            this.btn_check_flip_horizontal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_check_flip_horizontal.AutoSize = true;
            this.btn_check_flip_horizontal.Location = new System.Drawing.Point(790, 101);
            this.btn_check_flip_horizontal.Margin = new System.Windows.Forms.Padding(4);
            this.btn_check_flip_horizontal.Name = "btn_check_flip_horizontal";
            this.btn_check_flip_horizontal.Size = new System.Drawing.Size(86, 19);
            this.btn_check_flip_horizontal.TabIndex = 35;
            this.btn_check_flip_horizontal.Text = "水平反转";
            this.btn_check_flip_horizontal.UseVisualStyleBackColor = true;
            this.btn_check_flip_horizontal.CheckedChanged += new System.EventHandler(this.OnBnClickedCheckFlipHorizontal);
            // 
            // btn_check_flip_vertical
            // 
            this.btn_check_flip_vertical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_check_flip_vertical.AutoSize = true;
            this.btn_check_flip_vertical.Location = new System.Drawing.Point(696, 101);
            this.btn_check_flip_vertical.Margin = new System.Windows.Forms.Padding(4);
            this.btn_check_flip_vertical.Name = "btn_check_flip_vertical";
            this.btn_check_flip_vertical.Size = new System.Drawing.Size(86, 19);
            this.btn_check_flip_vertical.TabIndex = 36;
            this.btn_check_flip_vertical.Text = "垂直反转";
            this.btn_check_flip_vertical.UseVisualStyleBackColor = true;
            this.btn_check_flip_vertical.CheckedChanged += new System.EventHandler(this.OnBnClickedCheckFlipVertical);
            // 
            // checkBox_hardware_decoder
            // 
            this.checkBox_hardware_decoder.AutoSize = true;
            this.checkBox_hardware_decoder.Location = new System.Drawing.Point(372, 58);
            this.checkBox_hardware_decoder.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_hardware_decoder.Name = "checkBox_hardware_decoder";
            this.checkBox_hardware_decoder.Size = new System.Drawing.Size(71, 19);
            this.checkBox_hardware_decoder.TabIndex = 37;
            this.checkBox_hardware_decoder.Text = "硬解码";
            this.checkBox_hardware_decoder.UseVisualStyleBackColor = true;
            // 
            // btn_check_only_decode_video_key_frame
            // 
            this.btn_check_only_decode_video_key_frame.AutoSize = true;
            this.btn_check_only_decode_video_key_frame.Location = new System.Drawing.Point(451, 58);
            this.btn_check_only_decode_video_key_frame.Margin = new System.Windows.Forms.Padding(4);
            this.btn_check_only_decode_video_key_frame.Name = "btn_check_only_decode_video_key_frame";
            this.btn_check_only_decode_video_key_frame.Size = new System.Drawing.Size(101, 19);
            this.btn_check_only_decode_video_key_frame.TabIndex = 38;
            this.btn_check_only_decode_video_key_frame.Text = "只播关键帧";
            this.btn_check_only_decode_video_key_frame.UseVisualStyleBackColor = true;
            this.btn_check_only_decode_video_key_frame.CheckedChanged += new System.EventHandler(this.btn_check_only_decode_video_key_frame_CheckedChanged);
            // 
            // btn_check_render_scale_mode
            // 
            this.btn_check_render_scale_mode.AutoSize = true;
            this.btn_check_render_scale_mode.Checked = true;
            this.btn_check_render_scale_mode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btn_check_render_scale_mode.Location = new System.Drawing.Point(560, 58);
            this.btn_check_render_scale_mode.Margin = new System.Windows.Forms.Padding(4);
            this.btn_check_render_scale_mode.Name = "btn_check_render_scale_mode";
            this.btn_check_render_scale_mode.Size = new System.Drawing.Size(101, 19);
            this.btn_check_render_scale_mode.TabIndex = 39;
            this.btn_check_render_scale_mode.Text = "按比例绘制";
            this.btn_check_render_scale_mode.UseVisualStyleBackColor = true;
            this.btn_check_render_scale_mode.CheckedChanged += new System.EventHandler(this.btn_check_render_scale_mode_CheckedChanged);
            // 
            // groupBoxSet
            // 
            this.groupBoxSet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSet.Controls.Add(this.btn_about);
            this.groupBoxSet.Controls.Add(this.textBox_custom_text);
            this.groupBoxSet.Controls.Add(this.btn_show_custom_text);
            this.groupBoxSet.Controls.Add(this.lable_address);
            this.groupBoxSet.Controls.Add(this.lable_cur_status_txt);
            this.groupBoxSet.Controls.Add(this.btn_rotation);
            this.groupBoxSet.Controls.Add(this.textBox_resolution);
            this.groupBoxSet.Controls.Add(this.label2);
            this.groupBoxSet.Controls.Add(this.label_cur_status);
            this.groupBoxSet.Controls.Add(this.btn_switch_url);
            this.groupBoxSet.Controls.Add(this.btn_record);
            this.groupBoxSet.Controls.Add(this.btn_check_flip_vertical);
            this.groupBoxSet.Controls.Add(this.btn_record_config);
            this.groupBoxSet.Controls.Add(this.btn_check_render_scale_mode);
            this.groupBoxSet.Controls.Add(this.btn_capture_image);
            this.groupBoxSet.Controls.Add(this.btn_check_flip_horizontal);
            this.groupBoxSet.Controls.Add(this.btn_set_capture_image_path);
            this.groupBoxSet.Controls.Add(this.textBox_url);
            this.groupBoxSet.Controls.Add(this.btn_check_only_decode_video_key_frame);
            this.groupBoxSet.Controls.Add(this.checkBox_low_latency);
            this.groupBoxSet.Controls.Add(this.checkBox_hardware_decoder);
            this.groupBoxSet.Controls.Add(this.checkBox_rtsp_tcp);
            this.groupBoxSet.Controls.Add(this.checkBox_fast_startup);
            this.groupBoxSet.Controls.Add(this.checkBox_mute);
            this.groupBoxSet.Controls.Add(this.label_buffer_time);
            this.groupBoxSet.Controls.Add(this.btn_play);
            this.groupBoxSet.Controls.Add(this.textBox_buffer_time);
            this.groupBoxSet.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSet.Name = "groupBoxSet";
            this.groupBoxSet.Size = new System.Drawing.Size(1168, 170);
            this.groupBoxSet.TabIndex = 40;
            this.groupBoxSet.TabStop = false;
            this.groupBoxSet.Text = "设置";
            // 
            // label_custom_text
            // 
            this.label_custom_text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_custom_text.BackColor = System.Drawing.Color.Transparent;
            this.label_custom_text.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_custom_text.ForeColor = System.Drawing.Color.White;
            this.label_custom_text.Location = new System.Drawing.Point(12, 707);
            this.label_custom_text.Name = "label_custom_text";
            this.label_custom_text.Size = new System.Drawing.Size(1168, 60);
            this.label_custom_text.TabIndex = 41;
            this.label_custom_text.Text = "示例文字";
            this.label_custom_text.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_custom_text.Visible = false;
            this.label_custom_text.Click += new System.EventHandler(this.label_custom_text_Click);
            // 
            // btn_show_custom_text
            // 
            this.btn_show_custom_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_show_custom_text.Location = new System.Drawing.Point(885, 58);
            this.btn_show_custom_text.Margin = new System.Windows.Forms.Padding(4);
            this.btn_show_custom_text.Name = "btn_show_custom_text";
            this.btn_show_custom_text.Size = new System.Drawing.Size(87, 29);
            this.btn_show_custom_text.TabIndex = 40;
            this.btn_show_custom_text.Text = "底部文字";
            this.btn_show_custom_text.UseVisualStyleBackColor = true;
            this.btn_show_custom_text.Click += new System.EventHandler(this.btn_show_custom_text_Click);
            // 
            // textBox_custom_text
            // 
            this.textBox_custom_text.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_custom_text.Location = new System.Drawing.Point(696, 62);
            this.textBox_custom_text.Name = "textBox_custom_text";
            this.textBox_custom_text.Size = new System.Drawing.Size(182, 25);
            this.textBox_custom_text.TabIndex = 42;
            // 
            // btn_about
            // 
            this.btn_about.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_about.Location = new System.Drawing.Point(1074, 132);
            this.btn_about.Margin = new System.Windows.Forms.Padding(4);
            this.btn_about.Name = "btn_about";
            this.btn_about.Size = new System.Drawing.Size(87, 29);
            this.btn_about.TabIndex = 43;
            this.btn_about.Text = "关于";
            this.btn_about.UseVisualStyleBackColor = true;
            this.btn_about.Click += new System.EventHandler(this.btn_about_Click);
            // 
            // SmartPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 776);
            this.Controls.Add(this.label_custom_text);
            this.Controls.Add(this.groupBoxSet);
            this.Controls.Add(this.playWnd);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SmartPlayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartPlayer RTMP/RTSP播放器(大牛直播SDK(C)2014~2020)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SmartPlayerForm_FormClosing);
            this.SizeChanged += new System.EventHandler(this.SmartPlayerForm_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SmartPlayerForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.playWnd)).EndInit();
            this.groupBoxSet.ResumeLayout(false);
            this.groupBoxSet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.PictureBox playWnd;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Label lable_address;
        private System.Windows.Forms.CheckBox checkBox_mute;
        private System.Windows.Forms.CheckBox checkBox_fast_startup;
        private System.Windows.Forms.TextBox textBox_buffer_time;
        private System.Windows.Forms.Label label_buffer_time;
        private System.Windows.Forms.CheckBox checkBox_rtsp_tcp;
        private System.Windows.Forms.TextBox textBox_resolution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_set_capture_image_path;
        private System.Windows.Forms.Button btn_capture_image;
        private System.Windows.Forms.Button btn_record_config;
        private System.Windows.Forms.Button btn_record;
        private System.Windows.Forms.Label label_cur_status;
        private System.Windows.Forms.Label lable_cur_status_txt;
        private System.Windows.Forms.CheckBox checkBox_low_latency;
        private System.Windows.Forms.Button btn_rotation;
        private System.Windows.Forms.Button btn_switch_url;
        private System.Windows.Forms.CheckBox btn_check_flip_horizontal;
        private System.Windows.Forms.CheckBox btn_check_flip_vertical;
        private System.Windows.Forms.CheckBox checkBox_hardware_decoder;
        private System.Windows.Forms.CheckBox btn_check_only_decode_video_key_frame;
        private System.Windows.Forms.CheckBox btn_check_render_scale_mode;
        private System.Windows.Forms.GroupBox groupBoxSet;
        private System.Windows.Forms.Label label_custom_text;
        private System.Windows.Forms.TextBox textBox_custom_text;
        private System.Windows.Forms.Button btn_show_custom_text;
        private System.Windows.Forms.Button btn_about;
    }
}

