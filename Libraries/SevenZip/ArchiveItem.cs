﻿/* ------------------------------------------------------------------------- */
///
/// Copyright (c) 2010 CubeSoft, Inc.
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU Lesser General Public License as
/// published by the Free Software Foundation, either version 3 of the
/// License, or (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU Lesser General Public License for more details.
///
/// You should have received a copy of the GNU Lesser General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
///
/* ------------------------------------------------------------------------- */
using System;

namespace Cube.FileSystem.SevenZip
{
    /* --------------------------------------------------------------------- */
    ///
    /// ArchiveItem
    /// 
    /// <summary>
    /// 圧縮ファイルの 1 項目を表すクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class ArchiveItem : IArchiveItem
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// ArchiveItem
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="obj">生データ</param>
        ///
        /* ----------------------------------------------------------------- */
        public ArchiveItem(object obj, int index, string password)
        {
            if (obj is IInArchive raw) _raw = raw;
            else throw new ArgumentException("invalid object");

            Index = index;
            Password = password;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Index
        ///
        /// <summary>
        /// 圧縮ファイル中のインデックスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Index { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Path
        ///
        /// <summary>
        /// 圧縮ファイル中の相対パスを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Path
        {
            get
            {
                var dest = new PropVariant();
                _raw.GetProperty((uint)Index, ItemPropId.Path, ref dest);
                return dest.Object as string;
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
        public string Password { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// IsDirectory
        ///
        /// <summary>
        /// ディレクトリかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsDirectory
        {
            get
            {
                var dest = new PropVariant();
                _raw.GetProperty((uint)Index, ItemPropId.IsDirectory, ref dest);
                return (bool)dest.Object;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Size
        ///
        /// <summary>
        /// 展開後のファイルサイズを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public long Size
        {
            get
            {
                var dest = new PropVariant();
                _raw.GetProperty((uint)Index, ItemPropId.Size, ref dest);
                return (long)dest.Object;
            }
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Save
        ///
        /// <summary>
        /// 展開した内容を保存します。
        /// </summary>
        /// 
        /// <param name="directory">保存するディレクトリ</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Save(string directory)
        {
            var dest = System.IO.Path.Combine(directory, Path);
            if (IsDirectory)
            {
                if (!System.IO.Directory.Exists(dest)) System.IO.Directory.CreateDirectory(dest);
                return;
            }

            using(var stream = new ArchiveStreamWriter(System.IO.File.Create(dest)))
            {
                var callback = new ArchiveExtractCallback(this, stream);
                _raw.Extract(new[] { (uint)Index }, 1, 0, callback);
            }
        }

        #endregion

        #region Fields
        private IInArchive _raw;
        #endregion
    }
}
