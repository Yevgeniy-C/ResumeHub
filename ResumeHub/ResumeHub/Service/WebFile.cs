﻿using System.Text;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using System.Security.Cryptography;


namespace ResumeHub.Service
{

    public class ImageFileNames {
        public string FullFilename { get; set; } = "";
        public string ThumbFilename { get; set; } = "";
        public string SmallFilename { get; set; } = "";
    }

	public class WebFile
	{
        const string FOLDER_PREFIX = "./wwwroot";

        public WebFile()
		{
		}


        public string GetWebFilename(string filename, string folder = "images") {
            string dir = GetWebFileFolder(filename, folder);

            CreateFolder(FOLDER_PREFIX + dir);

            return dir + "/" + Path.GetFileNameWithoutExtension(filename) + ".jpg";
        }

        public string GetWebFileFolder(string filename, string folder = "images") {
            MD5 md5hash = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(filename);
            byte[] hashBytes = md5hash.ComputeHash(inputBytes);

            string hash = Convert.ToHexString(hashBytes);

            return "/" + folder + "/" + hash.Substring(0, 2) + "/" +
                   hash.Substring(0, 4);
        }

        public void CreateFolder(string dir) {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public async Task UploadAndResizeImage(Stream fileStream, string filename, int newWidth, int newHeight) {
            using (Image image = await Image.LoadAsync(fileStream))
            {
                int aspectWidth = newWidth;
                int aspectHeight = newHeight;

                if (image.Width / (image.Height / (float)newHeight) > newWidth)
                    aspectHeight = (int)(image.Height / (image.Width / (float)newWidth));
                else
                    aspectWidth = (int)(image.Width / (image.Height / (float)newHeight));

                int height = image.Height / 2;
                image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));

                await image.SaveAsJpegAsync(FOLDER_PREFIX + filename, new JpegEncoder() { Quality = 75 });
            }
        }
    }
}

