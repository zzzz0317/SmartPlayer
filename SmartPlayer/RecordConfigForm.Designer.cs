namespace SmartPlayer
{
    partial class RecordConfigForm
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
            this.btn_record_dir = new System.Windows.Forms.Button();
            this.text_rec_dir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_rec_name_file_prefix = new System.Windows.Forms.TextBox();
            this.text_rec_max_file_size = new System.Windows.Forms.TextBox();
            this.checkbox_append_date = new System.Windows.Forms.CheckBox();
            this.checkbox_append_time = new System.Windows.Forms.CheckBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.check_rec_audio_transcode_aac = new System.Windows.Forms.CheckBox();
            this.checkbox_rec_video = new System.Windows.Forms.CheckBox();
            this.checkbox_rec_audio = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btn_record_dir
            // 
            this.btn_record_dir.Location = new System.Drawing.Point(16, 59);
            this.btn_record_dir.Margin = new System.Windows.Forms.Padding(4);
            this.btn_record_dir.Name = "btn_record_dir";
            this.btn_record_dir.Size = new System.Drawing.Size(127, 29);
            this.btn_record_dir.TabIndex = 0;
            this.btn_record_dir.Text = "选择录像目录";
            this.btn_record_dir.UseVisualStyleBackColor = true;
            this.btn_record_dir.Click += new System.EventHandler(this.btn_record_dir_Click);
            // 
            // text_rec_dir
            // 
            this.text_rec_dir.Location = new System.Drawing.Point(177, 62);
            this.text_rec_dir.Margin = new System.Windows.Forms.Padding(4);
            this.text_rec_dir.Name = "text_rec_dir";
            this.text_rec_dir.Size = new System.Drawing.Size(504, 25);
            this.text_rec_dir.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 116);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "录像文件名前缀(必须是纯英文):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 172);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(285, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "单个文件大小(单位:MB, 范围:5-800MB):";
            // 
            // text_rec_name_file_prefix
            // 
            this.text_rec_name_file_prefix.Location = new System.Drawing.Point(265, 109);
            this.text_rec_name_file_prefix.Margin = new System.Windows.Forms.Padding(4);
            this.text_rec_name_file_prefix.Name = "text_rec_name_file_prefix";
            this.text_rec_name_file_prefix.Size = new System.Drawing.Size(200, 25);
            this.text_rec_name_file_prefix.TabIndex = 4;
            // 
            // text_rec_max_file_size
            // 
            this.text_rec_max_file_size.Location = new System.Drawing.Point(317, 163);
            this.text_rec_max_file_size.Margin = new System.Windows.Forms.Padding(4);
            this.text_rec_max_file_size.Name = "text_rec_max_file_size";
            this.text_rec_max_file_size.Size = new System.Drawing.Size(148, 25);
            this.text_rec_max_file_size.TabIndex = 5;
            // 
            // checkbox_append_date
            // 
            this.checkbox_append_date.AutoSize = true;
            this.checkbox_append_date.Location = new System.Drawing.Point(20, 232);
            this.checkbox_append_date.Margin = new System.Windows.Forms.Padding(4);
            this.checkbox_append_date.Name = "checkbox_append_date";
            this.checkbox_append_date.Size = new System.Drawing.Size(134, 19);
            this.checkbox_append_date.TabIndex = 6;
            this.checkbox_append_date.Text = "文件名增加日期";
            this.checkbox_append_date.UseVisualStyleBackColor = true;
            // 
            // checkbox_append_time
            // 
            this.checkbox_append_time.AutoSize = true;
            this.checkbox_append_time.Location = new System.Drawing.Point(177, 232);
            this.checkbox_append_time.Margin = new System.Windows.Forms.Padding(4);
            this.checkbox_append_time.Name = "checkbox_append_time";
            this.checkbox_append_time.Size = new System.Drawing.Size(134, 19);
            this.checkbox_append_time.TabIndex = 7;
            this.checkbox_append_time.Text = "文件名增加时间";
            this.checkbox_append_time.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(507, 275);
            this.btn_ok.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(97, 29);
            this.btn_ok.TabIndex = 8;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(662, 275);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(97, 29);
            this.btn_cancel.TabIndex = 9;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // check_rec_audio_transcode_aac
            // 
            this.check_rec_audio_transcode_aac.AutoSize = true;
            this.check_rec_audio_transcode_aac.Checked = true;
            this.check_rec_audio_transcode_aac.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_rec_audio_transcode_aac.Location = new System.Drawing.Point(349, 232);
            this.check_rec_audio_transcode_aac.Margin = new System.Windows.Forms.Padding(4);
            this.check_rec_audio_transcode_aac.Name = "check_rec_audio_transcode_aac";
            this.check_rec_audio_transcode_aac.Size = new System.Drawing.Size(173, 19);
            this.check_rec_audio_transcode_aac.TabIndex = 10;
            this.check_rec_audio_transcode_aac.Text = "音频自动转AAC后存储";
            this.check_rec_audio_transcode_aac.UseVisualStyleBackColor = true;
            // 
            // checkbox_rec_video
            // 
            this.checkbox_rec_video.AutoSize = true;
            this.checkbox_rec_video.Location = new System.Drawing.Point(20, 18);
            this.checkbox_rec_video.Margin = new System.Windows.Forms.Padding(4);
            this.checkbox_rec_video.Name = "checkbox_rec_video";
            this.checkbox_rec_video.Size = new System.Drawing.Size(89, 19);
            this.checkbox_rec_video.TabIndex = 11;
            this.checkbox_rec_video.Text = "录制视频";
            this.checkbox_rec_video.UseVisualStyleBackColor = true;
            // 
            // checkbox_rec_audio
            // 
            this.checkbox_rec_audio.AutoSize = true;
            this.checkbox_rec_audio.Location = new System.Drawing.Point(212, 18);
            this.checkbox_rec_audio.Margin = new System.Windows.Forms.Padding(4);
            this.checkbox_rec_audio.Name = "checkbox_rec_audio";
            this.checkbox_rec_audio.Size = new System.Drawing.Size(89, 19);
            this.checkbox_rec_audio.TabIndex = 12;
            this.checkbox_rec_audio.Text = "录制音频";
            this.checkbox_rec_audio.UseVisualStyleBackColor = true;
            // 
            // RecordConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 310);
            this.Controls.Add(this.checkbox_rec_audio);
            this.Controls.Add(this.checkbox_rec_video);
            this.Controls.Add(this.check_rec_audio_transcode_aac);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.checkbox_append_time);
            this.Controls.Add(this.checkbox_append_date);
            this.Controls.Add(this.text_rec_max_file_size);
            this.Controls.Add(this.text_rec_name_file_prefix);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_rec_dir);
            this.Controls.Add(this.btn_record_dir);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RecordConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "录像配置, ,注意：录像目录和文件名前缀必须是英文";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_record_dir;
        private System.Windows.Forms.TextBox text_rec_dir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_rec_name_file_prefix;
        private System.Windows.Forms.TextBox text_rec_max_file_size;
        private System.Windows.Forms.CheckBox checkbox_append_date;
        private System.Windows.Forms.CheckBox checkbox_append_time;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.CheckBox check_rec_audio_transcode_aac;
        private System.Windows.Forms.CheckBox checkbox_rec_video;
        private System.Windows.Forms.CheckBox checkbox_rec_audio;
    }
}