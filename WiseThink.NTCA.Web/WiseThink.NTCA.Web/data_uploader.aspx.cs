using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using log4net;
using System.Web.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace WiseThink.NTCA.Web
{
    public partial class data_uploader : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            string _serverPath = Server.MapPath("~/Upload/ApoDocuments/");
            string filenname = "";
            try
            {
                for (int x = 0; x < Request.Files.Count; x++)
                {
                    // filenname = Request.Params.Get("name");
                    filenname = Request.Files[x].FileName;
                    if (!Directory.Exists(_serverPath))
                    {
                        Directory.CreateDirectory(_serverPath);
                    }
                    int extensionCount = 0;
                    string[] splitFile = filenname.Split('.');
                    if(splitFile.Length<=2)
                    {
                        if(r.IsMatch(filenname))
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Sorry! cannot upload, file name should be only alphanumeric.", "ShowAlertFileExtension()", true);
                            return;
                        }
                        else
                        {
                            Request.Files[x].SaveAs(_serverPath + filenname);
                            foreach (string ext in splitFile)
                            {
                                if (FileExtensionList.Contains(ext.ToLower()))
                                {
                                    extensionCount++;
                                }
                            }
                            if (extensionCount <= 1)
                            {
                                Stream fs = Request.Files[x].InputStream;
                                BinaryReader br = new BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                                if (bytes[0] == 37 && bytes[1] == 80 || bytes[0] == 137 && bytes[1] == 80 || bytes[0] == 255 && bytes[1] == 216 ||
                                    bytes[0] == 66 && bytes[1] == 77 || bytes[0] == 71 && bytes[1] == 73 || bytes[0] == 123 && bytes[1] == 104 ||
                                    bytes[0] == 60 && bytes[1] == 63 || bytes[0] == 80 && bytes[1] == 75 || bytes[0] == 208 && bytes[1] == 207)
                                {
                                    fs.Close();
                                    br.Close();
                                    fs.Dispose();
                                    br.Dispose();
                                    Request.Files[x].InputStream.Close();
                                    bytes = null;
                                }
                                else
                                {
                                    System.IO.File.Delete(_serverPath + filenname);
                                    // Show message
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "ShowAlertFile()", true);
                                }
                            }
                            else
                            {
                                System.IO.File.Delete(_serverPath + filenname);
                                // Show message
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "File Error", "ShowAlertFileExtension()", true);
                            }
                        }
                    }
                    else
                    {
                        //Response.Write("<script>alert('Hello');</script>");
                        //throw new System.FormatException("Sorry! cannot upload, file name contains multiple dots.");
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "showalert", "alert('Sorry! cannot upload, file name contains multiple dots.');", true);
                    }
                }

            }
            catch
            {
                // logger.Fatal("exception" + ex.Message);
            }
        }
    }
}