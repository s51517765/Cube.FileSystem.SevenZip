﻿/* ------------------------------------------------------------------------- */
///
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System.ComponentModel;
using Cube.FileSystem.Ice;

namespace Cube.FileSystem.App.Ice.Settings
{
    /* --------------------------------------------------------------------- */
    ///
    /// ContextSettingsViewModel
    /// 
    /// <summary>
    /// ContextSettings の ViewModel を表すクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ContextSettingsViewModel : ObservableProperty
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ContextSettingsViewModel
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="model">Model オブジェクト</param>
        /// 
        /* ----------------------------------------------------------------- */
        public ContextSettingsViewModel(ContextSettings model)
        {
            _model = model;
            _model.PropertyChanged += (s, e) => OnPropertyChanged(e);
        }

        #endregion

        #region Properties

        #region Archive

        /* ----------------------------------------------------------------- */
        ///
        /// Archive
        /// 
        /// <summary>
        /// 圧縮の項目が有効かどうかを示す値を取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Archive
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.Archive); }
            set { Set(PresetMenu.Archive, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveZip
        /// 
        /// <summary>
        /// Zip で圧縮の項目が有効かどうかを示す値を取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ArchiveZip
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ArchiveZip); }
            set { Set(PresetMenu.ArchiveZip, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveZipPassword
        /// 
        /// <summary>
        /// パスワード付 Zip で圧縮の項目が有効かどうかを示す値を取得
        /// または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ArchiveZipPassword
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ArchiveZipPassword); }
            set { Set(PresetMenu.ArchiveZipPassword, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveSevenZip
        /// 
        /// <summary>
        /// 7z で圧縮の項目が有効かどうかを示す値を取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ArchiveSevenZip
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ArchiveSevenZip); }
            set { Set(PresetMenu.ArchiveSevenZip, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveGZip
        /// 
        /// <summary>
        /// GZip で圧縮の項目が有効かどうかを示す値を取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ArchiveGZip
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ArchiveGZip); }
            set { Set(PresetMenu.ArchiveGZip, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveBZip2
        /// 
        /// <summary>
        /// BZip2 で圧縮の項目が有効かどうかを示す値を取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ArchiveBZip2
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ArchiveBZip2); }
            set { Set(PresetMenu.ArchiveBZip2, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveSfx
        /// 
        /// <summary>
        /// 自己解凍形式で圧縮の項目が有効かどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ArchiveSfx
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ArchiveSfx); }
            set { Set(PresetMenu.ArchiveSfx, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveDetail
        /// 
        /// <summary>
        /// 詳細を設定して圧縮の項目が有効かどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ArchiveDetail
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ArchiveDetail); }
            set { Set(PresetMenu.ArchiveDetail, value); }
        }

        #endregion

        #region Extract

        /* ----------------------------------------------------------------- */
        ///
        /// Extract
        /// 
        /// <summary>
        /// 解凍の項目が有効かどうかを示す値を取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Extract
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.Extract); }
            set { Set(PresetMenu.Extract, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExtractSource
        /// 
        /// <summary>
        /// ここに解凍の項目が有効かどうかを示す値を取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ExtractSource
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ExtractSource); }
            set { Set(PresetMenu.ExtractSource, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExtractDesktop
        /// 
        /// <summary>
        /// デスクトップに解凍の項目が有効かどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ExtractDesktop
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ExtractDesktop); }
            set { Set(PresetMenu.ExtractDesktop, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExtractMyDocuments
        /// 
        /// <summary>
        /// マイドキュメントに解凍の項目が有効かどうかを示す値を取得
        /// または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ExtractMyDocuments
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ExtractMyDocuments); }
            set { Set(PresetMenu.ExtractMyDocuments, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ExtractRuntime
        /// 
        /// <summary>
        /// 場所を指定して解凍の項目が有効かどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool ExtractRuntime
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.ExtractRuntime); }
            set { Set(PresetMenu.ExtractRuntime, value); }
        }

        #endregion

        #region Mail

        /* ----------------------------------------------------------------- */
        ///
        /// Mail
        /// 
        /// <summary>
        /// 圧縮してメール送信の項目が有効かどうかを示す値を取得または
        /// 設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool Mail
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.Mail); }
            set { Set(PresetMenu.Mail, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MailZip
        /// 
        /// <summary>
        /// Zip で圧縮してメール送信の項目が有効かどうかを示す値を取得
        /// または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool MailZip
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.MailZip); }
            set { Set(PresetMenu.MailZip, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MailZipPassword
        /// 
        /// <summary>
        /// パスワード付 Zip で圧縮してメール送信の項目が有効かどうかを
        /// 示す値を取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool MailZipPassword
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.MailZipPassword); }
            set { Set(PresetMenu.MailZipPassword, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MailSevenZip
        /// 
        /// <summary>
        /// 7z で圧縮してメール送信の項目が有効かどうかを示す値を取得
        /// または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool MailSevenZip
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.MailSevenZip); }
            set { Set(PresetMenu.MailSevenZip, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MailGZip
        /// 
        /// <summary>
        /// GZip で圧縮してメール送信の項目が有効かどうかを示す値を取得
        /// または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool MailGZip
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.MailGZip); }
            set { Set(PresetMenu.MailGZip, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MailBZip2
        /// 
        /// <summary>
        /// BZip2 で圧縮してメール送信の項目が有効かどうかを示す値を取得
        /// または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool MailBZip2
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.MailBZip2); }
            set { Set(PresetMenu.MailBZip2, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MailSfx
        /// 
        /// <summary>
        /// 自己解凍形式で圧縮してメール送信の項目が有効かどうかを示す値を
        /// 取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool MailSfx
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.MailSfx); }
            set { Set(PresetMenu.MailSfx, value); }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MailDetail
        /// 
        /// <summary>
        /// 詳細を設定して圧縮し、メール送信の項目が有効かどうかを示す値を
        /// 取得または設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public bool MailDetail
        {
            get { return _model.PresetMenu.HasFlag(PresetMenu.MailDetail); }
            set { Set(PresetMenu.MailDetail, value); }
        }

        #endregion

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Reset
        /// 
        /// <summary>
        /// コンテキストメニューを規定値に再設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void Reset()
        {
            _model.IsExtended = false;
            _model.PresetMenu = PresetMenu.DefaultContext;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Set
        /// 
        /// <summary>
        /// PresetMenu に値を設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void Set(PresetMenu value, bool check)
        {
            if (check) _model.PresetMenu |= value;
            else _model.PresetMenu &= ~value;
        }

        #region Fields
        private ContextSettings _model;
        #endregion

        #endregion
    }
}
