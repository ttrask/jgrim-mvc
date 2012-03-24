using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Net;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Management;
using System.Management.Instrumentation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Hosting;
using JGrim.MVC.Models;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Security.Principal;


namespace JGrim.MVC.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AjaxService" in code, svc and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AjaxService : IAjaxService
    {

        private static string m_dsn = ConfigurationManager.AppSettings["ConnectionString"];
        private string photoUrlPath = ConfigurationManager.AppSettings["PhotoPath"].ToString();
        private JGrimEntities1 db;

        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        public AjaxService()
        {
            db = new JGrimEntities1();
        }


        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            ResponseFormat = WebMessageFormat.Json)]
        public int LoadImagesToDB()
        {


            try
            {
                int photosUpdated = 0;

                List<List<String>> photoDirectories = new List<List<string>>();

                string photoPath = HostingEnvironment.MapPath(photoUrlPath);

                DirectoryInfo masterDirectoryInfo = new DirectoryInfo(photoPath);

                DirectoryInfo[] folders = masterDirectoryInfo.GetDirectories();

                using (SqlConnection cn = new SqlConnection(m_dsn))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SavePhoto", cn))
                    {

                        List<String> users = new List<String>();

                        foreach (DirectoryInfo di in folders)
                        {
                            try
                            {
                                //Add PhotoAlbum if Album isn't present in Photoset
                                try
                                {
                                    if ((from album in db.PhotoAlbums where album.Name == di.Name select album).Count<PhotoAlbum>() == 0)
                                    {
                                        PhotoAlbum album = new PhotoAlbum()
                                        {
                                            Name = di.Name,
                                            Description = "",
                                            AlbumFileLocation = Path.Combine(photoUrlPath, di.Name),
                                            AlbumCoverUrl = ConfigurationManager.AppSettings["DefaultAlbumPhoto"].ToString(),
                                            IsActive=true
                                        };
                                        db.AddToPhotoAlbums(album);
                                        db.SaveChanges();
                                    };
                                }
                                catch (Exception ex)
                                {
                                    Log.LogError("There was an error adding the following album to the database:" + di.Name + System.Environment.NewLine + ex.Message);
                                }

                                //Updates File set in folder
                                string[] extensions = "*.jpg;*.gif;*.png".Split(new char[] { ';' });

                                List<FileInfo> myfileinfos = new List<FileInfo>();

                                foreach (string ext in extensions)
                                {
                                    myfileinfos.AddRange(di.GetFiles(ext));
                                }

                                FileInfo[] files = myfileinfos.ToArray<FileInfo>();

                                if (files.Count() > 0)
                                {
                                    List<string> photoUrls = new List<string>();

                                    foreach (FileInfo fi in files)
                                    {
                                        cmd.Parameters.Clear();
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        using (SqlDataAdapter adaptor = new SqlDataAdapter(cmd))
                                        {
                                            cmd.Parameters.Add(new SqlParameter("ImageName", fi.Name));
                                            cmd.Parameters.Add(new SqlParameter("ImageType", fi.Extension));
                                            cmd.Parameters.Add(new SqlParameter("ImageAlbumName", di.Name));
                                            cmd.Parameters.Add(new SqlParameter("FileName", fi.Name));
                                            cmd.Parameters.Add(new SqlParameter("Description", ""));
                                            adaptor.SelectCommand = cmd;
                                            cmd.ExecuteNonQuery();
                                        }
                                        photosUpdated++;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.LogError("There was an error adding the following album to the database:" + di.Name + System.Environment.NewLine + ex.Message);
                            }


                            PhotoAlbum dbAlbum = (from album in db.PhotoAlbums where album.Name == di.Name select album).FirstOrDefault<PhotoAlbum>();

                            //clears out files no longer present in said folder from the database.
                            if (dbAlbum != null)
                            {
                                dbAlbum.Photos.Load();
                                dbAlbum.Photos.ToList<Photo>().ForEach(delegate(Photo photo)
                                {
                                    CheckForDelete<Photo>(di.FullName, photo);
                                });
                            }

                            try
                            {
                                //WindowsIdentity user = WindowsIdentity.GetCurrent(); 
                                //DirectorySecurity myDS = di.GetAccessControl();
                                //myDS.AddAccessRule(new FileSystemAccessRule(user.User, FileSystemRights.Read, AccessControlType.Allow));
                                //myDS.AddAccessRule(new FileSystemAccessRule(user.User, FileSystemRights.Write, AccessControlType.Allow));
                                //di.SetAccessControl(myDS);
                            }
                            catch (Exception ex)
                            {
                                StringBuilder sb = new StringBuilder("Error Creating file permissions for current user on directory");
                                sb.AppendLine("<br>");
                                sb.AppendLine("Current User: " + WindowsIdentity.GetCurrent().Name);
                                sb.AppendLine("<br>");
                                sb.AppendLine("Directory: " + di.Name);
                                sb.AppendLine("<br>");
                                sb.AppendLine(ex.Message);

                                Log.LogError(sb.ToString());
                            }
                        }
                    }
                }
                Log.LogError("Photo Album refresh completed successfully:  " + photosUpdated + " photos updated.");
                return photosUpdated;
            }
            catch (IOException ex)
            {
                Log.LogError("There was an IO error while getting information for photo album:" + ex.Message + System.Environment.NewLine + (ex.InnerException != null ? ex.InnerException.ToString() : ""));
                throw ex;
            }
            catch (SecurityException ex)
            {
                Log.LogError("There was an Security Exception while getting information for photo album:" + ex.Message + "<br>Permission Demanded: " + ex.Demanded == null ? "unknown" : ex.Demanded.ToString() + "<br>Assembly at fault: " + ex.FailedAssemblyInfo == null ? "unknown" : ex.FailedAssemblyInfo.ToString());
                throw ex;
            }
            catch (Exception ex)
            {
                Log.LogError("There was a error while saving photo information to the database: " + ex.Message + System.Environment.NewLine + (ex.InnerException != null ? ex.InnerException.ToString() : ""));
                throw ex;
            }


        }


        public List<String> GetUsers()
        {
            // This query will query for all user account names in our current Domain

            try
            {
                SelectQuery sQuery = new SelectQuery("Win32_UserAccount", "Domain='"
                               + System.Environment.UserDomainName.ToString() + "'");
                List<String> currentUsers = new List<String>();

                // Searching for available Users
                ManagementObjectSearcher mSearcher = new
                                                  ManagementObjectSearcher(sQuery);


                foreach (ManagementObject mObject in mSearcher.Get())
                {
                    // Adding all user names in our combobox
                    currentUsers.Add(mObject["Name"].ToString());
                }
                return currentUsers;
            }
            catch (Exception ex)
            {
                Log.LogError("There was an error retrieving the user account information from the server:" + ex.Message + (ex.InnerException != null ? ex.InnerException.ToString() : ""));
                return null;
            }

        }


        protected bool CheckForDelete<T>(string filePath, T obj)
        {
            try
            {
                if (typeof(T) == typeof(Photo))
                {
                    Photo photo = obj as Photo;

                    if (!File.Exists(filePath + "\\" + photo.FileName))
                    {
                        db.DeleteObject(photo);
                        db.SaveChanges();

                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                Log.LogError("There was an error deleting the following album directory from its parent directory:" + filePath + System.Environment.NewLine + ex.Message);
                return false;
            }

        }

        //protected void CreateThumbnails(FileInfo[] files)
        //{
        //    try
        //    {

        //        if (files.Count() > 0)
        //        {
        //            List<string> photoUrls = new List<string>();

        //            if (files.Count() > 0)
        //            {
        //                int thumbsCreated = 0;
        //                foreach (FileInfo fi in files)
        //                {

        //                    thumbsCreated += PhotoService.GenerateThumbnail(fi.Directory.FullName, fi.Name);

        //                    if (fi.Length > 500000)
        //                    {
        //                        PhotoService.ResizeImage(fi.FullName, 640);
        //                    }
        //                }
        //                Log.LogError("Successfully created " + thumbsCreated + " thumbnails for Album " + files.First().Directory.Name );
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogError("There was an error creating a set of thumbnails for one of the existing albums" + System.Environment.NewLine + ex.Message);
        //    }
        //}



        public static HttpWebRequest GetWebRequest(string formattedUri)
        {

            // Create the request’s URI.

            Uri serviceUri = new Uri(formattedUri, UriKind.Absolute);



            // Return the HttpWebReques.t


            return (HttpWebRequest)System.Net.WebRequest.Create(serviceUri);

        }

        // Add more operations here and mark them with [OperationContract]
    }
}
