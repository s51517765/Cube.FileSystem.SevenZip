﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Cube.FileSystem.SevenZip.App.Ice
{
    /* --------------------------------------------------------------------- */
    ///
    /// ArchiveForm
    ///
    /// <summary>
    /// 圧縮処理の詳細画面を表示するクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public partial class ArchiveForm : Form
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveForm
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ArchiveForm()
        {
            InitializeComponent();

            UpdateThreadCount();
            UpdateFormat();
            UpdateCompressionLevel();
            UpdateCompressionMethod();
            UpdateEncryptionMethod();

            EncryptionMethodComboBox.Enabled = false;
            ExecuteButton.Enabled = false;

            ExecuteButton.Click += (s, e) => Close();
            ExitButton.Click    += (s, e) => Close();

            OutputButton.Click                             += WhenPathRequested;
            OutputTextBox.TextChanged                      += WhenPathChanged;
            FormatComboBox.SelectedValueChanged            += WhenFormatChanged;
            CompressionMethodComboBox.SelectedValueChanged += WhenCompressionMethodChanged;
            EncryptionCheckBox.CheckedChanged              += WhenEncryptionChanged;
            PasswordTextBox.TextChanged                    += WhenPasswordChanged;
            ConfirmTextBox.TextChanged                     += WhenConfirmChanged;
            ConfirmTextBox.EnabledChanged                  += WhenConfirmEnabledChanged;
            ShowPasswordCheckBox.CheckedChanged            += WhenShowPasswordChanged;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Format
        /// 
        /// <summary>
        /// 圧縮ファイル形式を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Format Format => (Format)FormatComboBox.SelectedValue;

        /* ----------------------------------------------------------------- */
        ///
        /// CompressionLevel
        /// 
        /// <summary>
        /// 圧縮レベルを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public CompressionLevel CompressionLevel
            => (CompressionLevel)CompressionLevelComboBox.SelectedValue;

        /* ----------------------------------------------------------------- */
        ///
        /// CompressionMethod
        /// 
        /// <summary>
        /// 圧縮方法を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public CompressionMethod CompressionMethod
            => CompressionMethodComboBox.Enabled ?
               (CompressionMethod)CompressionMethodComboBox.SelectedValue :
               CompressionMethod.Default;

        /* ----------------------------------------------------------------- */
        ///
        /// EncryptionMethod
        /// 
        /// <summary>
        /// 暗号化方法を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public EncryptionMethod EncryptionMethod
            => EncryptionMethodComboBox.Enabled ?
               (EncryptionMethod)EncryptionMethodComboBox.SelectedValue :
               EncryptionMethod.Default;

        /* ----------------------------------------------------------------- */
        ///
        /// ThreadCount
        /// 
        /// <summary>
        /// 圧縮方法を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int ThreadCount => (int)ThreadNumericUpDown.Value;

        /* ----------------------------------------------------------------- */
        ///
        /// Path
        /// 
        /// <summary>
        /// 保存先パスを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Path
        {
            get => OutputTextBox.Text;
            set
            {
                if (OutputTextBox.Text == value) return;
                OutputTextBox.Text = value;
                OutputTextBox.SelectionStart = Math.Max(value.Length - 1, 0);
                OutputTextBox.SelectionLength = 0;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Password
        /// 
        /// <summary>
        /// パスワードを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Password
            => EncryptionCheckBox.Checked ?
               PasswordTextBox.Text :
               null;

        /* ----------------------------------------------------------------- */
        ///
        /// PathIsValid
        /// 
        /// <summary>
        /// パス設定が正しいかどうかを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool PathIsValid
        {
            get => _path;
            set
            {
                if (_path == value) return;
                _path = value;
                ExecuteButton.Enabled = value & EncryptionIsValid;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// EncryptionIsValid
        /// 
        /// <summary>
        /// 暗号化設定が正しいかどうかを取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private bool EncryptionIsValid
        {
            get => _encryption;
            set
            {
                if (_encryption == value) return;
                _encryption = value;
                ExecuteButton.Enabled = value & PathIsValid;
            }
        }

        #endregion

        #region Implementations

        #region Update

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateFormat
        /// 
        /// <summary>
        /// Format を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateFormat()
        {
            Update(FormatComboBox, ViewResource.Formats);
            if (!FormatComboBox.Enabled) return;
            FormatComboBox.SelectedValue = Format.Zip;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateCompressionLevel
        /// 
        /// <summary>
        /// CompressionLevel を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateCompressionLevel()
        {
            Update(CompressionLevelComboBox, ViewResource.CompressionLevels);
            if (!CompressionLevelComboBox.Enabled) return;
            CompressionLevelComboBox.SelectedValue = CompressionLevel.Ultra;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateCompressionMethod
        /// 
        /// <summary>
        /// CompressionMethod を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateCompressionMethod()
        {
            Update(CompressionMethodComboBox, ViewResource.GetCompressionMethod(Format));
            if (!CompressionMethodComboBox.Enabled) return;
            CompressionMethodComboBox.SelectedIndex = 0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateEncryptionMethod
        /// 
        /// <summary>
        /// EncryptionMethod を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateEncryptionMethod()
        {
            Update(EncryptionMethodComboBox, ViewResource.EncryptionMethods);
            if (!EncryptionMethodComboBox.Enabled) return;
            EncryptionMethodComboBox.SelectedIndex = 0;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateThreadCount
        /// 
        /// <summary>
        /// スレッド数に関する設定を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void UpdateThreadCount()
        {
            var count = Environment.ProcessorCount;
            ThreadNumericUpDown.Maximum =
            ThreadNumericUpDown.Value   = count;
            ThreadNumericUpDown.Enabled = count > 1;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Update
        /// 
        /// <summary>
        /// ComboBox.DataSource オブジェクトを更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Update<T>(ComboBox src, IList<KeyValuePair<string, T>> data)
        {
            src.Enabled       = (data != null);
            src.DataSource    = data;
            src.DisplayMember = "Key";
            src.ValueMember   = "Value";
        }

        #endregion

        #region Password gimmick

        /* ----------------------------------------------------------------- */
        ///
        /// WhenPasswordChanged
        ///
        /// <summary>
        /// パスワード入力が変更された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenPasswordChanged(object sender, EventArgs e)
        {
            if (ShowPasswordCheckBox.Checked) EncryptionIsValid = PasswordTextBox.TextLength > 0;
            else ConfirmTextBox.Text = string.Empty;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenConfirmChanged
        ///
        /// <summary>
        /// 確認項目のテキストが変更された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenConfirmChanged(object sender, EventArgs e)
        {
            if (!ConfirmTextBox.Enabled) return;

            var eq = PasswordTextBox.Text.Equals(ConfirmTextBox.Text);
            EncryptionIsValid = eq && PasswordTextBox.TextLength > 0;
            ConfirmTextBox.BackColor = eq || ConfirmTextBox.TextLength <= 0 ?
                                       SystemColors.Window :
                                       Color.FromArgb(255, 102, 102);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenConfirmEnabledChanged
        ///
        /// <summary>
        /// 確認項目の Enabled が変更された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenConfirmEnabledChanged(object sender, EventArgs e)
            => ConfirmTextBox.BackColor = ConfirmTextBox.Enabled ?
                                          SystemColors.Window :
                                          SystemColors.Control;

        /* ----------------------------------------------------------------- */
        ///
        /// WhenShowPasswordChanged
        ///
        /// <summary>
        /// パスワードを表示の状態が変更された時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenShowPasswordChanged(object sender, EventArgs e)
        {
            var show = ShowPasswordCheckBox.Checked;

            PasswordTextBox.UseSystemPasswordChar = !show;
            ConfirmTextBox.Enabled = !show;
            ConfirmTextBox.Text = string.Empty;
            EncryptionIsValid = show & (PasswordTextBox.TextLength > 0);
        }

        #endregion

        /* ----------------------------------------------------------------- */
        ///
        /// WhenPathRequested
        /// 
        /// <summary>
        /// 保存パスの要求時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenPathRequested(object sender, EventArgs e)
        {
            var cvt  = new PathConverter(Path, Format, CompressionMethod);
            var args = new PathQueryEventArgs(cvt.Result.FullName, cvt.ResultFormat, true);

            Views.ShowSaveView(args);
            if (!args.Cancel) Path = args.Result;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenPathChanged
        /// 
        /// <summary>
        /// 保存パスの変更時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenPathChanged(object sender, EventArgs e)
            => PathIsValid = OutputTextBox.TextLength > 0;

        /* ----------------------------------------------------------------- */
        ///
        /// WhenFormatChanged
        /// 
        /// <summary>
        /// Format 変更時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenFormatChanged(object sender, EventArgs e)
        {
            UpdateCompressionMethod();
            EncryptionMethodComboBox.Enabled &= (Format == Format.Zip);
            EncryptionGroupBox.Enabled = ViewResource.IsEncryptionSupported(Format);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// WhenCompressionMethodChanged
        /// 
        /// <summary>
        /// CompressionMethod 変更時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenCompressionMethodChanged(object sender, EventArgs e)
            => Path = new PathConverter(Path, Format, CompressionMethod).Result.FullName;

        /* ----------------------------------------------------------------- */
        ///
        /// WhenEncryptionChanged
        /// 
        /// <summary>
        /// 暗号化の有効/無効状態が変化した時に実行されるハンドラです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void WhenEncryptionChanged(object sender, EventArgs e)
        {
            var enabled = EncryptionCheckBox.Checked;

            PasswordTextBox.Enabled          =
            ConfirmTextBox.Enabled           =
            ShowPasswordCheckBox.Enabled     = enabled;
            EncryptionMethodComboBox.Enabled = enabled & (Format == Format.Zip);

            if (!enabled) ExecuteButton.Enabled = true;
            else WhenShowPasswordChanged(sender, e);
        }

        #region Fields
        private bool _path = false;
        private bool _encryption = true;
        #endregion

        #endregion
    }
}
