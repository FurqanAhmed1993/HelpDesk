<%@ WebHandler Language="C#" Class="UploadHandler" %>

using System;
using System.Web;
using System.IO;



public class UploadHandler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {

        //string FileRandom = RandomNumber.Next(0, 999999).ToString();
        string FileRandom = "";
        HttpFileCollection files = context.Request.Files;
        for (int i = 0; i < files.Count; i++)
        {

            HttpPostedFile file = files[i];
            string FileType = GetFileType(file.FileName);
            string fileName = System.IO.Path.GetFileName(file.FileName).Replace("&", "-");
            fileName = System.IO.Path.GetFileName(fileName).Replace(" ", "__");
            Random RandomNumber = new Random();
            Guid objGuid = Guid.NewGuid();
            fileName = RandomNumber.Next(0, 999999).ToString() + objGuid.ToString() + fileName;
            string fname = context.Server.MapPath("~/Uploads/" + FileRandom + fileName);
            file.SaveAs(fname);

            if (context.Request.UrlReferrer.AbsolutePath == "/pages/TicketReplyRespone.aspx")
            {
                context.Response.Write(FileRandom + fileName + "," + fileName + ",");
            }
            else if (context.Request.UrlReferrer.AbsolutePath == "/Pages/CreateTicket.aspx" || context.Request.UrlReferrer.AbsolutePath == "/pages/CreateTicket.aspx")
            {
                if (FileType != "")
                    context.Response.Write(FileRandom + fileName + "," + fileName + "," + FileType+"||");
                else
                    context.Response.Write(FileRandom + fileName + "," + fileName + "," + " " + "||");
            }
            else
            {
                context.Response.Write(FileRandom + fileName + "," + fileName + "," + FileType);
            }

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }


    public static string GetFileType(string FileName)
    {
        if (FileName.IndexOf(".gif") != -1)
        {
            return "Image";
        }
        if (FileName.IndexOf(".jpg") != -1)
        {
            return "Image";
        }
        if (FileName.IndexOf(".doc") != -1)
        {
            return "Word Document";
        }
        if (FileName.IndexOf(".pdf") != -1)
        {
            return "PDF";
        }
        if (FileName.IndexOf(".xls") != -1)
        {
            return "Excel Spreadsheet";
        }
        if (FileName.IndexOf(".txt") != -1)
        {
            return "Text File";
        }
        if (FileName.IndexOf(".rar") != -1)
        {
            return "Rar File";
        }
        if (FileName.IndexOf(".zip") != -1)
        {
            return "Zip File";
        }
        if (FileName.IndexOf(".msg") != -1)
        {
            return "Outlook File";
        }
        return "";
    }



}