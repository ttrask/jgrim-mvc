using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using JGrim.MVC.Models;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace JGrim.MVC.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PhotoService
    {
        //
        // GET: /PhotoService/

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="imagePath">The absolute (on the disk) image path.</param>
        /// <param name="maxSize">Size of the max.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        [OperationContract]
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public static int ResizeImageFile(string imagePath, int maxSize = 0)
        {
            try
            {
                string sSize = ConfigurationManager.AppSettings["PhotoSize"];

                int imageSize = 0;

                if (maxSize == 0)
                {
                    if (sSize != null)
                        imageSize = Int32.Parse(sSize);
                }
                else
                {
                    imageSize = maxSize;
                }


                MemoryStream ms = new MemoryStream();

                try
                {
                    using (Bitmap bmp = ResizeImage(imagePath, imageSize, imageSize))
                    {

                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);                        
                    }

                    File.Delete(imagePath);
                    FileStream fs = new FileStream(imagePath, FileMode.CreateNew);
                    ms.WriteTo(fs);
                    fs.Close();
                    return 1;
                }
                catch
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        


        [OperationContract]
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public static int GenerateThumbnail(string imagePath, string imageName)
        {
            try
            {
                if (imagePath == null)
                {
                    
                    return 0;
                }

                string path = imagePath;


                try
                {
                    if (!Directory.Exists(imagePath + "\\thumbs"))
                    {
                        Directory.CreateDirectory(imagePath + "\\thumbs");
                    }
                }
                catch
                {
                    Log.LogError("Error Creating Directory for thumbnail " + imagePath);
                }
                string outputFileName = null;

                try
                {
                    outputFileName = Path.Combine(imagePath + "\\thumbs", imageName);
                }
                catch (Exception ex)
                {
                    throw new Exception("Incorrect File Path: " + ex.ToString());
                }


                if (!File.Exists(outputFileName))
                {
                    string sSize = ConfigurationManager.AppSettings["PhotoSize"];
                    int Size = 120;
                    if (sSize != null)
                        Size = Int32.Parse(sSize);

                    Bitmap bmp = ResizeImage(Path.Combine(imagePath, imageName),  Size, Size);

                    if (bmp == null)
                    {

                        return 0;
                    }

                    if (outputFileName != null)
                    {
                        using (FileStream imageStream = new FileStream(outputFileName, FileMode.CreateNew))
                        {
                            try
                            {
                                bmp.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                            catch (Exception ex)
                            {
                                StringBuilder sw = new StringBuilder();

                                sw.AppendLine("Error Saving bitmap image: " + outputFileName);
                                sw.AppendLine("<br>");
                                sw.AppendLine(ex.Message);
                                sw.AppendLine("<br>");
                                sw.AppendLine("Currnent User: " + WindowsIdentity.GetCurrent().Name);
                                Log.LogError(sw.ToString());
                            }
                        }
                    }

                    // Put user code to initialize the page here

                    return 1;
                }
            }
            catch (Exception ex)
            {
                StringBuilder sw = new StringBuilder();

                sw.AppendLine("There was an error creating a thumbnail for the following image:" + imageName + "<BR>" + ex.Message);
                sw.AppendLine("<br>");
                sw.AppendLine(ex.StackTrace);
                sw.AppendLine("<br>");
                sw.AppendLine("Currnent User: " + WindowsIdentity.GetCurrent().Name);
                Log.LogError(sw.ToString());
            }

            return 0;
        }

        public static Bitmap ResizeImage(string src)
        {
            return ResizeImage(src, Int32.Parse(ConfigurationManager.AppSettings["PhotoMaxWidth"]), Int32.Parse(ConfigurationManager.AppSettings["PhotoMaxHeight"]));
        }


        ///
        [OperationContract]
        [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
        public static Bitmap ResizeImage(string src, int lnWidth=0, int lnHeight=0)
        {



            try
            {
                FileStream fs = File.OpenRead(src);

                try
                {
                    using (Image loImg = Image.FromStream(fs))
                    {
                        ImageFormat loFormat = loImg.RawFormat;

                        decimal lnRatio;
                        int lnNewWidth = 0;
                        int lnNewHeight = 0;


                        if (lnWidth == 0)
                        {
                            lnWidth = Int32.Parse(ConfigurationManager.AppSettings["photoMaxWidth"]);
                        }

                        if (lnHeight == 0)
                        {
                            lnHeight = Int32.Parse(ConfigurationManager.AppSettings["photoMaxHeight"]);
                        }

                        //*** If the image is smaller than a thumbnail just return it
                        if (loImg.Width < lnWidth && loImg.Height < lnHeight)
                            return new Bitmap(loImg);

                        if (loImg.Width > loImg.Height)
                        {
                            lnRatio = (decimal)lnWidth / loImg.Width;
                            lnNewWidth = lnWidth;
                            decimal lnTemp = loImg.Height * lnRatio;
                            lnNewHeight = (int)lnTemp;
                        }
                        else
                        {
                            lnRatio = (decimal)lnHeight / loImg.Height;
                            lnNewHeight = lnHeight;
                            decimal lnTemp = loImg.Width * lnRatio;
                            lnNewWidth = (int)lnTemp;
                        }

                        try
                        {
                            System.Drawing.Image FullsizeImage = loImg;

                            // Prevent using images internal thumbnail
                            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
                            FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

                            System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(lnNewWidth, lnNewHeight, null, IntPtr.Zero);

                            // Clear handle to original file so that we can overwrite it if necessary
                            FullsizeImage.Dispose();
                            loImg.Dispose();
                            return new Bitmap(NewImage);
                        }
                        catch (Exception ex)
                        {
                            StringBuilder sw = new StringBuilder();
                            sw.AppendLine("There was an error creating a thumbnail for the following image:" + src + "<BR>" + ex.Message);
                            sw.AppendLine("<br>");
                            sw.AppendLine(ex.StackTrace);
                            Log.LogError(sw.ToString());
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    StringBuilder sw = new StringBuilder();
                    sw.AppendLine("There was an error creating a thumbnail for the following image:" + src + "<BR>" + ex.Message);
                    sw.AppendLine("<br>");
                    sw.AppendLine(ex.StackTrace);
                    Log.LogError(sw.ToString());
                    return null;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogError("There was an error resizing the image '" + src + "':<br>" + ex.Message);
                return null;
            }
        }
    }
}
