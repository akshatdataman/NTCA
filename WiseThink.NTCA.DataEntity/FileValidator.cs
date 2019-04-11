using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace WiseThink.NTCA.DataEntity
{
    public class FileValidator
    {
        
        static List<byte[]> INVALID_SIGNATURE = new List<byte[]> { new byte[] { 77, 90 }, new byte[] { 17, 224 }, new byte[] { 208, 207 } };

        public static bool IsExe(HttpPostedFile h)
        {
            byte[] b = ReadTwoBytes(h);
            return IsValidBytes(b);
        }
        public static bool IsExe(string path)
        {
            byte[] b = ReadTwoBytes(path);
            return IsValidBytes(b);
        }

        private static string GetFirstTwoByteOfFile(HttpPostedFile h)
        {
            byte[] bytes = new byte[2];
            int n = h.InputStream.Read(bytes, 0, 2);
            string strbytes = string.Empty;
            foreach (byte b in bytes)
            {
                strbytes += b.ToString() + ",";
            }
            strbytes = strbytes.TrimEnd(',');
            return strbytes;
        }
        private static string GetFirstTwoByteOfFile(string path)
        {
            byte[] bytes = new byte[2];
            using (var fileStream = File.Open(path, FileMode.Open))
            {
                fileStream.Read(bytes, 0, 2);
            }
            string strbytes = string.Empty;
            foreach (byte b in bytes)
            {
                strbytes += b.ToString() + ",";
            }
            strbytes = strbytes.TrimEnd(',');
            return strbytes;
        }
        private static byte[] ReadTwoBytes(HttpPostedFile h)
        {
            try
            {
                {
                    byte[] bytes = new byte[2];
                    int n = h.InputStream.Read(bytes, 0, 2);
                    return bytes;
                }
            }
            catch
            {
                return (byte[])null;
            }
        }
        private static byte[] ReadTwoBytes(string path)
        {
            var twoBytes = new byte[2];
            using (var fileStream = File.Open(path, FileMode.Open))
            {
                fileStream.Read(twoBytes, 0, 2);
            }
            return twoBytes;
        }
        private static bool IsValidBytes(byte[] b)
        {
            bool status = false;
            foreach (var item in INVALID_SIGNATURE)
            {
                if (b.SequenceEqual(item))
                {
                    status = true;
                    break;
                }
            }
            return status;
        }
    }
}
