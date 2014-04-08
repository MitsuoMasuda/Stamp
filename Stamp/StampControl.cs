using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Stamp
{
	public class StampControl
	{
		//--定数定義--//
		private const int ImageSizeX = 70;
		private const int ImageSizeY = 70;

		//--変数宣言--//
		private string _name;
		private string _departmentName;
		private string _date;

		//--プロパティ--//
		public string Name { set { this._name = value; } }
		public string Department { set { this._departmentName = value; } }
		public string Date { set { this._date = value; } }

		public StampControl()
		{
		}

		/// <summary>
		/// 印鑑画像の生成
		/// </summary>
		/// <param name="path">画像パス（フルパスで指定）</param>
		public void CreateImage(string path)
		{
			try
			{
				Bitmap bitmap = new Bitmap(ImageSizeX, ImageSizeY);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.FillRectangle(Brushes.White, graphics.VisibleClipBounds);
				graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

				Font fnt = new Font("ＭＳ 明朝", this.GetFontSize(this._departmentName.Trim()));
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Near;

				RectangleF rec1 = new RectangleF(2, 10, ImageSizeX - 1, ImageSizeY - 1);
				graphics.DrawString(this._departmentName.Trim(), fnt, Brushes.Red, rec1, sf);

				fnt = new Font("ＭＳ 明朝", this.GetFontSize(this._name.Trim()));
				sf.LineAlignment = StringAlignment.Far;
				RectangleF rec2 = new RectangleF(2, -6, ImageSizeX - 1, ImageSizeY - 1);
				graphics.DrawString(this._name.Trim(), fnt, Brushes.Red, rec2, sf);

				fnt = new Font("ＭＳ 明朝", 10);
				sf.LineAlignment = StringAlignment.Center;
				graphics.DrawString(this._date.Trim(), fnt, Brushes.Red, graphics.VisibleClipBounds, sf);

				// 円の表示
				Pen pen = new Pen(Color.Red);
				graphics.DrawEllipse(pen, 0, 0, ImageSizeX - 1, ImageSizeY - 1);

				// 横線の表示
				graphics.DrawLine(pen, 1, 25, ImageSizeX - 2, 25);
				graphics.DrawLine(pen, 1, 45, ImageSizeX - 2, 45);

				// ファイルの保存
				bitmap.Save(path);

				// 解放
				fnt.Dispose();
				sf.Dispose();
				graphics.Dispose();
				bitmap.Dispose();
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// フォントサイズの取得
		/// </summary>
		/// <param name="data">対象文字列</param>
		/// <returns>文字列に合うサイズを返す</returns>
		private float GetFontSize(string data)
		{
			Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
			int byteCount = sjisEnc.GetByteCount(data);

			if (byteCount >= 1 && byteCount <= 4)
			{
				return 10;
			}
			else if (byteCount >= 5 && byteCount <= 8)
			{
				return 8;
			}
			else
			{
				return 6;
			}
		}
	}
}
