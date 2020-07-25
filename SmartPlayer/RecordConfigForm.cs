using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPlayer
{
    public partial class RecordConfigForm : Form
    {
        private bool is_rec_video_ = true;
        private bool is_rec_audio_ = true;
        private String rec_dir_ = "";
        private String rec_name_file_prefix_ = "";
        private UInt32 max_file_size_ = 200 * 1024; // 单位是KByte, 默认200MB
        private bool is_append_date_ = true;
        private bool is_append_time_ = true;
        private bool is_audio_transcode_aac_ = true;

        public bool IsRecVideo() { return is_rec_video_; }
        public bool IsRecAudio() { return is_rec_audio_; }
        public String RecDir() { return rec_dir_; }
        public String RecNameFilePrefix() { return rec_name_file_prefix_; }
        public UInt32 MaxFileSize() { return max_file_size_; }
        public bool IsAppendDate() { return is_append_date_; }
        public bool IsAppendTime() { return is_append_time_; }
        public bool IsAudioTanscodeAAC() { return is_audio_transcode_aac_; }

        public RecordConfigForm(bool is_rec_video, bool is_rec_audio, String rec_dir, String rec_name_file_prefix, UInt32 max_file_size, bool is_append_date, bool is_append_time, bool is_audio_transcode_aac)
        {
            InitializeComponent();
            text_rec_max_file_size.Text = "200";

            if (is_rec_video)
            {
                checkbox_rec_video.CheckState = CheckState.Checked;
            }
            else
            {
                checkbox_rec_video.CheckState = CheckState.Unchecked;
            }

            if (is_rec_audio)
            {
                checkbox_rec_audio.CheckState = CheckState.Checked;
            }
            else
            {
                checkbox_rec_audio.CheckState = CheckState.Unchecked;
            }

            text_rec_dir.Text = rec_dir;
            text_rec_name_file_prefix.Text = rec_name_file_prefix;
            text_rec_max_file_size.Text = (max_file_size/1024).ToString();

            if (is_append_date)
            {
                checkbox_append_date.CheckState = CheckState.Checked;
            }
            else
            {
                checkbox_append_date.CheckState = CheckState.Unchecked;
            }

            if (is_append_time)
            {
                checkbox_append_time.CheckState = CheckState.Checked;
            }
            else
            {
                checkbox_append_time.CheckState = CheckState.Unchecked;
            }

            if (is_audio_transcode_aac)
            {
                check_rec_audio_transcode_aac.CheckState = CheckState.Checked;
            }
            else
            {
                check_rec_audio_transcode_aac.CheckState = CheckState.Unchecked;
            }
        }

        private void btn_record_dir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                text_rec_dir.Text = dlg.SelectedPath.ToString();
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            is_rec_video_ = checkbox_rec_video.Checked;
            is_rec_audio_ = checkbox_rec_audio.Checked;
            rec_dir_ = text_rec_dir.Text;
            rec_name_file_prefix_ = text_rec_name_file_prefix.Text;
            max_file_size_ = UInt32.Parse(text_rec_max_file_size.Text)*1024;
            is_append_date_ = checkbox_append_date.Checked;
            is_append_time_ = checkbox_append_time.Checked;
            is_audio_transcode_aac_ = check_rec_audio_transcode_aac.Checked;
            this.Close();
            this.Dispose();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
