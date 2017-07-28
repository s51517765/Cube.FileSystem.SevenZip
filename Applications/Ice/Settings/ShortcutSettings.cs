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
using System.Runtime.Serialization;

namespace Cube.FileSystem.Ice
{
    /* --------------------------------------------------------------------- */
    ///
    /// ShortcutSettings
    /// 
    /// <summary>
    /// デスクトップに作成するショートカットに関するユーザ設定を保持する
    /// ためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataContract]
    public class ShortcutSettings : ObservableProperty
    {
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Preset
        /// 
        /// <summary>
        /// 予め定義されたショートカットメニューを示す値を取得または
        /// 設定します。
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        [DataMember]
        public PresetMenu Preset
        {
            get { return _preset; }
            set { SetProperty(ref _preset, value); }
        }

        #endregion

        #region Fields
        private PresetMenu _preset = PresetMenu.DefaultDesktop;
        #endregion
    }
}