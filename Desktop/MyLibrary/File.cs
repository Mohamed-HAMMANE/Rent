using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Class for manipulating files
    /// </summary>
    public static class _file
    {
        #region Files

        /// <summary>
        /// Method to convert a byte array to file
        /// </summary>
        /// <param name="path">The path of file</param>
        /// <param name="byteArray">The byte array</param>
        public static string ToFile(this byte[] byteArray, string path)
        {
            using var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite,FileShare.ReadWrite);
            fs.Write(byteArray, 0, byteArray.Length);
            return path;
        }
        /// <summary>
        /// Convert a Stream to byte array
        /// </summary>
        /// <param name="input">the stream to convert</param>
        /// <returns>the byte array</returns>
        public static byte[] ToBytes(this Stream input)
        {
            var buffer = new byte[16 * 1024];
            using var ms = new MemoryStream();
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0) ms.Write(buffer, 0, read);
            return ms.ToArray();
        }
        /// <summary>
        /// Convert a file to byte array
        /// </summary>
        /// <param name="path">path of file</param>
        /// <returns></returns>
        public static byte[] ToBytes(string path)
        {
            using var fs = new FileStream(path, FileMode.Open,FileAccess.ReadWrite,FileShare.ReadWrite);
            return fs.ToBytes();
        }

        /// <summary>
        /// File attribute of a class manipulation
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ClassFile(string path) => !File.Exists(path) ? null : ToBytes(path);
        /// <summary>
        /// File attribute of a class manipulation
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="extension"></param>
        /// <returns>Path</returns>
        public static string ClassFile(this byte[] bytes, string extension = "xml") => bytes.IsNull() || bytes.Length <= 0 ? null : bytes.ToFile(Temp(extension));

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap Resize(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using var graphics = Graphics.FromImage(destImage);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

            return destImage;
        }

        /// <summary>
        /// Resize image with percent
        /// </summary>
        /// <param name="image"></param>
        /// <param name="size"></param>
        /// <returns>Bitmap</returns>
        public static Bitmap Resize(this Image image,int size)
        {
            double percent;
            if (image.Width > image.Height) percent = size / (double)image.Width;
            else percent = size / (double)image.Height;
            return image.Resize(Math.Round(percent * image.Width).ToInt(), Math.Round(percent * image.Height).ToInt());
        }

        #endregion

        #region Folders

        /// <summary>
        /// Create a folder if not exist
        /// </summary>
        /// <param name="path">the path</param>
        public static string CreateFolder(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path.TrimEnd('\\').TrimEnd('/');
        }

        #endregion

        #region Others

        /// <summary>
        /// Get the absolute path of a relative path
        /// </summary>
        /// <param name="path">the relative path</param>
        /// <returns>string path, null if doesn't exists</returns>
        public static string GetAbsolutePathIfExitsOf(string path)
        {
            try
            {
                var tmp = GetAbsolutePathOf(path);
                return File.Exists(tmp) || Directory.Exists(tmp) ? tmp : null;
            }
            catch (Exception) { return null; }
        }
        /// <summary>
        /// Get absolute path of a path
        /// </summary>
        /// <param name="path">the path</param>
        /// <returns>the absolute path</returns>
        public static string GetAbsolutePathOf(string path) => Path.GetFullPath(new Uri(path).LocalPath);

        /// <summary>
        /// Application folder (Web content folder, desktop executable folder)
        /// </summary>
        /// <returns></returns>
        public static string AppFolder() => _app.CurrentDirectoryPath;
        /// <summary>
        /// Get the temp folder of current app
        /// </summary>
        /// <returns>string</returns>
        public static string TempFolder() => CreateFolder(AppFolder() + "\\Temp");
        /// <summary>
        /// Get the temp file of current app
        /// </summary>
        /// <param name="extension">extension</param>
        /// <returns>string</returns>
        public static string Temp(string extension = "pdf") => $"{TempFolder()}\\{_str.UniqueId()}.{extension}";
        /// <summary>
        /// Log file
        /// </summary>
        /// <returns></returns>
        public static string Log() => AppFolder() + $"\\{_app.Name}Log.txt";
        /// <summary>
        /// Empty the temp folder from temp files
        /// </summary>
        public static void EmptyTempFolder()
        {
            var di = new DirectoryInfo(TempFolder());
            foreach (var file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// Layout unique name
        /// </summary>
        public static string[] LayoutUniqueName
        {
            get => _session.Get("LayoutUniqueName") as string[] ;
            set => _session.Set("LayoutUniqueName", value);
        }
        /// <summary>
        /// Layout path
        /// </summary>
        /// <returns></returns>
        public static string LayoutPath(int index) => $"{CreateFolder($"{_app.CurrentDirectoryPath}/Layouts")}/{LayoutUniqueName[index]}Layout.xml";
        /// <summary>
        /// check layout exists
        /// </summary>
        /// <returns></returns>
        public static bool LayoutExists(int index)
        {
            try
            {
                return File.Exists(LayoutPath(index));
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
