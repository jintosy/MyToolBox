using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyToolBox
{
    public class ImgAnalyze
    {
        // 画像種別の定義
        private Dictionary<string, string> _ImgTypes = new Dictionary<string, string>()
        {
            // bmp:先頭2byteがフォーマット種類を表す
            {"424D", "bmp"},
            // jpg:先頭2byteがフォーマット種類を表す
            {"FFD8", "jpg"},
            // png:先頭8byteがファイルヘッダを表す
            {"89504E470D0A1A0A", "png"},
            // gif:先頭3byteがシグネチャ(ASCIIコードで"GIF")を表す
            {"474946", "gif"},
            // tiff:先頭2バイトがフォーマット種類を表す
            {"49492A", "tiff"},
            {"4D4D2A", "tiff"},
            // BigTIFF: tiff
            {"49492B", "bigtiff" },
            {"4D4D2B", "bigtiff" }
        };

        private int _height;
        private int _width;

        /// <summary>
        /// コンストラクタ
        /// 画像縦横変数初期化
        /// </summary>
        public ImgAnalyze()
        {
            this._height = 0;
            this._width = 0;
        }

        /// <summary>
        /// 画像の拡張子を判定して返す
        /// </summary>
        /// <param name="_filePath">画像フルパス</param>
        /// <returns>バイナリから判定した画像拡張子</returns>
        public string GetImgExtension(string _filePath)
        {
            StringBuilder bytesCombine = new StringBuilder();
            try
            {
                // 引数チェック
                if (_filePath is null || _filePath.Length == 0)
                {
                    return "Please input correct filepath!";
                }
                else if(!File.Exists(_filePath))
                {
                    return "Don't exists file";
                }
                

                using (BinaryReader br = new BinaryReader(new FileStream(_filePath, FileMode.Open, FileAccess.Read)))
                {
                    // ファイルの先頭8byteを取得
                    for (int i = 0; i < 8; i++)
                    {
                        // 1byte取得後、16進数に変換して結合していく
                        string val = br.ReadByte().ToString("X2");
                        bytesCombine.Append(val);
                    }

                    // 上で定義した画像種別を使って判定する
                    string fileExtension = null;
                    foreach (KeyValuePair<string, string> type in _ImgTypes)
                    {
                        if (bytesCombine.ToString() == type.Key)
                        {
                            fileExtension = type.Value;
                            break;
                        }
                    }

                    return fileExtension;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
