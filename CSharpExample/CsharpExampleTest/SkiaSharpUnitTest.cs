using Xunit;
using SkiaSharp;
using System.IO;
using System.Net.Http;
using OpenCvSharp;
using System;
using System.Collections.Generic;

namespace CsharpExampleTest
{
    public class SkiaSharpUnitTest
    {
        public SkiaSharpUnitTest()
        {

        }

        private List<SKRect> DetectBoundAuto(byte[] bytes)
        {
            SKBitmap bitmap = SKBitmap.Decode(bytes);

            List<SKRect> lstRect = new();

            int pos_top = default; // 상
            int pos_bottom = default; // 하
            int pos_left = default; // 좌
            int pos_right = bitmap.Width; // 우
            int posY = default;

            while (posY < bitmap.Height)
            {
                #region Top
                bool found_top = default;

                for (int y = posY; y < bitmap.Height; y++)
                {
                    if (found_top)
                    {
                        break;
                    }
                    else
                    {
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            SKColor color = bitmap.GetPixel(x, y);

                            if (color.Red > 90 &&
                                color.Green > 90 &&
                                color.Blue > 90)
                            {
                                pos_top = y;
                                found_top = true;

                                break;
                            }
                        }
                    }
                }
                #endregion

                #region Bottom
                // 폭의 60% 이상부터 폭의 120%까지 뒤져서 경계선이 없어질때까지 뒤진다.
                // 빈 곳을 못찾으면 120% 지점이 자르는 지점이다.

                int scanStartY = posY + Convert.ToInt32(bitmap.Width * 0.6);
                int scanEndY = posY + Convert.ToInt32(bitmap.Width * 1.1);
                int bottomColorVal = 30;

                // 마지막인지 판별
                if (scanEndY > bitmap.Height)
                {
                    SKRect rect = new(pos_left, pos_bottom, pos_right, bitmap.Height);

                    if (bitmap.Height - pos_bottom > 250)
                    {
                        lstRect.Add(rect);
                    }

                    posY = bitmap.Height + 1;
                }
                else
                {
                    bool found_bottom = false;

                    //수정 알고리즘. 위에서 아래로
                    for (int y = scanEndY; y >= scanStartY; y--)
                    {
                        if (found_bottom)
                        {
                            break;
                        }
                        else
                        {
                            //가로 픽셀을 검사한다.
                            //만약 가로 픽셀 중 하나라도 색상값이 발견되면 배경이 아니다
                            //따라서 가로가 다 블랙이어야 한다.
                            bool bk = true;

                            for (int x = 0; x < bitmap.Width; x++)
                            {
                                SKColor color = bitmap.GetPixel(x, y);

                                if (color.Red > bottomColorVal &&
                                    color.Green > bottomColorVal &&
                                    color.Blue > bottomColorVal)
                                {
                                    bk = false;

                                    break;
                                }
                            }

                            if (bk)
                            {
                                found_bottom = true;
                                pos_bottom = y;
                            }
                        }
                    }

                    if (!found_bottom)
                    {
                        scanStartY = posY + Convert.ToInt32(bitmap.Width * 0.6);
                        scanEndY = posY + Convert.ToInt32(bitmap.Width * 2);

                        for (int y = scanStartY; y < scanEndY; y++)
                        {
                            if (found_bottom)
                            {
                                break;
                            }
                            else
                            {
                                //가로 픽셀을 검사한다.
                                //만약 가로 픽셀 중 하나라도 색상값이 발견되면 배경이 아니다
                                //따라서 가로가 다 블랙이어야 한다.
                                bool bk = true;

                                for (int x = 0; x < bitmap.Width; x++)
                                {
                                    if (y > bitmap.Height) break;

                                    SKColor color = bitmap.GetPixel(x, y);

                                    if (color.Red > bottomColorVal &&
                                        color.Green > bottomColorVal &&
                                        color.Blue > bottomColorVal)
                                    {
                                        bk = false;

                                        break;
                                    }
                                }

                                if (bk)
                                {
                                    found_bottom = true;
                                    pos_bottom = y;
                                }
                            }
                        }
                    }

                    if (!found_bottom)
                    {
                        pos_bottom = scanEndY;
                    }

                    //create graphic variable
                    //Graphics g = Graphics.FromImage(_img);
                    //int new_height = pos_bottom - pos_top;
                    //int new_width = pos_right - pos_left;
                    //SKRect rect = new(pos_left, pos_top, pos_right - pos_left, pos_bottom - pos_top);
                    SKRect rect = new(pos_left, pos_top, pos_right, pos_bottom);

                    lstRect.Add(rect);
                    posY = Convert.ToInt32(rect.Bottom);
                }
                #endregion
            }

            return lstRect;
        }

        private byte[] GetSpanByteFromOpenCVCannyEdgeSpan(byte[] imageArr)
        {
            try
            {
                using Mat matData = Mat.FromImageData(imageArr, ImreadModes.Grayscale);
                double minThreshold = 100.0;
                double maxThreshold = 200.0;
                using Mat cannySrc = matData;
                using Mat cannyDst = new();
                Cv2.Canny(
                    src: cannySrc,
                    edges: cannyDst,
                    threshold1: minThreshold,
                    threshold2: maxThreshold,
                    apertureSize: 3,
                    L2gradient: true);

                return cannyDst.ImEncode(".jpg");
            }
            catch (TypeInitializationException ex) when (ex.InnerException is DllNotFoundException) // OpenCVSharp4 Occur
            {
                return default;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        [Theory]
        [InlineData("")] // image 파일 경로
        public void ImageResizeTest(string path)
        {
            using FileStream outputFs = File.OpenWrite($"{path}/수정_세로.JPG");
            using MemoryStream ms = new(File.ReadAllBytes($"{path}/캡처_세로.JPG"));
            using SKManagedStream skiaStream = new(ms);
            using SKBitmap bitmap = SKBitmap.Decode(skiaStream);

            SKImageInfo info;
            SKImageInfo sourceInfo = new(bitmap.Width, bitmap.Height);
            SKPoint point;

            if (bitmap.Height < 650 && bitmap.Width > 650) // Height만 조정
            {
                info = new(bitmap.Width + 10, 650); // the desired image size
                point = new(5, (650 - bitmap.Height) / 2);
            }
            else if (bitmap.Height > 650 && bitmap.Width < 650) // Width만 조정
            {
                info = new(650, bitmap.Height + 10); // the desired image size
                point = new((650 - bitmap.Width) / 2, 5);
            }
            else // 둘다 조정
            {
                info = new(650, 650); // the desired image size
                point = new((650 - bitmap.Width) / 2, (650 - bitmap.Height) / 2);
            }

            using SKSurface surface = SKSurface.Create(info); // create the surface
            using SKCanvas canvas = surface.Canvas; // get the canvas for drawing
            canvas.Clear(SKColors.Transparent); // draw the transparent background
            using SKPaint paint = new() // create a paint object so that drawing can happen at a higher resolution
            {
                IsAntialias = true,
                FilterQuality = SKFilterQuality.High
            };
            canvas.DrawBitmap(bitmap, point, paint); // 지정된 point에 bitmap 원본 사이즈만큼 paint 설정대로 그림
            canvas.Flush(); // create an image for saving/drawing
            using SKImage finalImage = surface.Snapshot();
            finalImage.Encode(SKEncodedImageFormat.Jpeg, 100).SaveTo(outputFs);
        }

        [Theory]
        [InlineData("")] // image Uri
        public async void ByteArrToSKImage(string uri)
        {
            HttpClient httpClient = new();
            byte[] imagebytes = await httpClient.GetByteArrayAsync(uri);
            MemoryStream ms = new(imagebytes);
            SKManagedStream skStream = new(ms, false);
            SKData data = SKData.Create(skStream);
            SKCodec codec = SKCodec.Create(data);
            SKBitmap bitmap = SKBitmap.Decode(codec);
            SKImage originImage = SKImage.FromBitmap(bitmap);
        }
    }
}
