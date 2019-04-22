using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace WiseThink.NTCA.Web
{
    /// <summary>
    /// Summary description for DataUploader
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DataUploader : System.Web.Services.WebService
    {
        Regex r = new Regex("^[a-zA-Z0-9]*$");
        string[] FileExtensionList = {"aif","cda","mid","midi","mp3","mpa","ogg","wav","wma","wpl","7z","arj","deb","pkg","rar",
                                         "rpm","targz","z","zip","bin","dmg","iso","toast","vcd","csv","dat","db","dbf","log","mdb",
                                         "sav","sql","tar","xml","apk","bat","bin","cgi","pl","com","exe","gadget","jar","py","wsf",
                                         "fnt","fon","otf","ttf","ai","bmp","gif","ico","jpeg","jpg","png","ps","psd","svg","tif","tiff",
                                         "asp","aspx","cer","cfm","cgi","pl","css","htm","html","js","jsp","part","php","py","rss","xhtml",
                                         "key","odp","pps","ppt","pptx","c","class","cpp","cs","h","java","sh","swift","vb","ods","xlr","xls","xlsx",
                                         "bak","cab","cfg","cpl","cur","dll","dmp","drv","icns","ico","ini","lnk","msi","sys","tmp","3g2","3gp","avi",
                                         "flv","h264","m4v","mkv","mov","mp4","mpg","mpeg","rm","swf","vob","wmv","doc","docx","odt","pdf","rtf","tex",
                                         "txt","wks","wps","wpd" };

        [WebMethod]
        public string FileUpload()
        {
            string _serverPath = Server.MapPath("~/Upload/ApoDocuments/");
            string filenname = "", msg = "";
            try
            {
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    // filenname = Request.Params.Get("name");
                    filenname = HttpContext.Current.Request.Files[i].FileName;
                    if (!Directory.Exists(_serverPath))
                    {
                        Directory.CreateDirectory(_serverPath);
                    }
                    int extensionCount = 0;
                    string[] splitFile = filenname.Split('.');
                    if (splitFile.Length <= 2)
                    {
                        if (r.IsMatch(filenname))
                        {
                            msg= "Sorry! cannot upload, file name should be only alphanumeric.";
                        }
                        else
                        {
                            HttpContext.Current.Request.Files[i].SaveAs(_serverPath + filenname);
                            foreach (string ext in splitFile)
                            {
                                if (FileExtensionList.Contains(ext.ToLower()))
                                {
                                    extensionCount++;
                                }
                            }
                            if (extensionCount <= 1)
                            {
                                Stream fs = HttpContext.Current.Request.Files[i].InputStream;
                                BinaryReader br = new BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                if (bytes[0] == 37 && bytes[1] == 80 || bytes[0] == 137 && bytes[1] == 80 || bytes[0] == 255 && bytes[1] == 216 ||
                                    bytes[0] == 66 && bytes[1] == 77 || bytes[0] == 71 && bytes[1] == 73 || bytes[0] == 123 && bytes[1] == 104 ||
                                    bytes[0] == 60 && bytes[1] == 63 || bytes[0] == 80 && bytes[1] == 75 || bytes[0] == 208 && bytes[1] == 207)
                                {
                                    fs.Close();
                                    //br.Close();
                                    //fs.Dispose();
                                    //br.Dispose();
                                    HttpContext.Current.Request.Files[i].InputStream.Close();
                                    bytes = null;
                                    msg= "File uploaded successfully";
                                }
                                else
                                {
                                    System.IO.File.Delete(_serverPath + filenname);
                                    // Show message
                                    msg= "File Error";
                                }
                            }
                            else
                            {
                                System.IO.File.Delete(_serverPath + filenname);
                                // Show message
                                msg= "File Error";
                            }
                        }
                    }
                    else
                    {
                        msg= "Sorry! cannot upload, file name contains multiple dots.";
                    }
                }

            }
            catch (Exception ex)
            {
                msg= ex.Message;
            }
            return msg;
        }
    }
}
