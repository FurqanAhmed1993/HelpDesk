using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;
using System.Collections.Generic;
using DAL;
using System.Linq;
using System.Threading;
using System.Data.Objects.SqlClient;
using System.Net;
/// <summary>
/// Summary description for Email
/// </summary>
public class Email
{
    public static TicketSystemEntities context = new TicketSystemEntities();
    //public static string EmailErrorFilePath = ConfigurationManager.AppSettings["EmailErrorFilePath"];
    public static string SenderEmailAddress = ConfigurationManager.AppSettings["SenderEmailAddress"];
    public static string SenderEmailPassword = ConfigurationManager.AppSettings["SenderEmailPassword"];
    public static string SenderSMTPServer = ConfigurationManager.AppSettings["SenderSMTPServer"];
    public static string Port = ConfigurationManager.AppSettings["Port"];
    public static string clientEnableSsl = ConfigurationManager.AppSettings["clientEnableSsl"];

    public static void SendMail(string to, string subject, string msg, string cc, string Attachment)
    {
        try
        {
          //  to = "fahad.iqbal@sybrid.com";            
            cc = "";
            Thread email = new Thread(delegate ()
         {
             SendMail(to, subject, msg, cc, Attachment, "");
         });

            email.IsBackground = true;
            email.Start();
        }
        catch (Exception ex)
        {
            WriteFile(ex.ToString());
            return;
        }
    }
    private static void SendMail(string to, string subject, string msg, string cc, string Attachment, string var)
    {
        try
        {
            to = "jahangir.siddiqui@sybrid.com";
            cc = "";
            if (SenderSMTPServer != "" && SenderEmailAddress != "" && SenderEmailPassword != "")
            {
                string displayName = "PSW Support";
                MailMessage message = new MailMessage();
                string[] addresses = to.Split(';');
                foreach (string address in addresses)
                {
                    if (address != "")
                    {
                        message.To.Add(new MailAddress(address));
                    }
                }

                string[] cc_address = cc.Split(';');
                foreach (string address in cc_address)
                {
                    if (address != "")
                    {
                        message.CC.Add(new MailAddress(address));
                    }
                }

                if (Attachment != string.Empty)
                {
                    Attachment attach = new Attachment(Attachment);
                    message.Attachments.Add(attach);
                }
              
                message.From = new MailAddress(SenderEmailAddress, displayName);
                message.Subject = subject;
                message.Body = msg;
                message.IsBodyHtml = true;

                NetworkCredential objNetworkCredential = new NetworkCredential(SenderEmailAddress, SenderEmailPassword);
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = false;
                client.Credentials = objNetworkCredential;
                client.Port = Convert.ToInt32(Port);
                client.Host = SenderSMTPServer;
                client.EnableSsl = Convert.ToBoolean(clientEnableSsl);
                client.Send(message);
                WriteFile("<<subject: " + subject + " >> " + "Successfully Email Send To : " + to + " and CC : " + cc + " from " + SenderEmailAddress + ">>");
            }
        }
        catch (Exception ex)
        {
            WriteFile("<<subject: " + subject + " >> " + "Exception raised while Email Send To : " + to + " and CC : " + cc + " from " + SenderEmailAddress + " >> Exception Message : " + ex.ToString() + "");
            return;
        }
    }
    public static string GetTemplateString(int templateCode)
    {
        StreamReader objStreamReader;
        string emailText = "";
        switch (templateCode)
        {
            case (int)Constant.EmailTemplates.Customer:
                objStreamReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"Templates\Email\CustomerFirstReplyFromCybernet.htm");
                emailText = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                objStreamReader = null;
                break;
            case (int)Constant.EmailTemplates.Department:
                objStreamReader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @"Templates\Email\EmployeeProfileEmail.htm");
                emailText = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                objStreamReader = null;
                break;
        }
        return emailText;
    }
    public static string ReplaceEmailBodySection(string emailTemplate, string heading, string requesterInfoHtml, string requestDetailHtml, string linksHtml)
    {
        string emailBody = emailTemplate;
        emailBody = emailBody.Replace("[Heading]", heading);
        emailBody = emailBody.Replace("[RequesterInfo]", requesterInfoHtml);
        emailBody = emailBody.Replace("[RequestDetail]", requestDetailHtml);
        emailBody = emailBody.Replace("[Year]", "© " + Convert.ToString(DateTime.Now.Year) + ", Sybrid Pvt Ltd. All rights reserved.");
        return emailBody;
    }
    public static DataTable GetTicketInfo(int TicketMasterId)
    {
        DataTable Ticket = context.TS_TicketMaster.Where(c => c.TicketMasterId == TicketMasterId

            ).Select(c => new
            {
                TicketCode = c.TicketCode,
                Category = c.TS_Setup_RequestTypeCategory.CategoryName,
                Title = c.Tittle,
                Priority = c.TS_Setup_Priority.PriorityName,
            }).ToList().ToDataTable();
        return Ticket;
    }
    public static DataTable GetTicketInfoForCustomerFirstReply(int TicketMasterId)
    {
        DataTable Ticket = new DataTable();
        var List = context.TS_TicketMaster.FirstOrDefault(c => c.TicketMasterId == TicketMasterId && c.IsActive == true);
        if (List != null)
        {
            Ticket.Columns.Add("Ticket Code");
            Ticket.Columns.Add("Category");
            Ticket.Columns.Add("Customer");
            Ticket.Columns.Add("Address");

            Ticket.Rows.Add(Convert.ToString(List.TicketCode), Convert.ToString(List.TS_Setup_RequestTypeCategory.CategoryName), Convert.ToString(List.Customer.CustomerName), Convert.ToString(List.Customer.Address));
        }
        return Ticket;
    }
    public static string GetRequesterInfoHtml(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string html =

    @"<td style='color: #eeeeee; font-family: Calibri; font-size: 12px;'>
            &nbsp; Requester Name:
        </td>
        <td  style='color: #ffffff; font-family: Calibri; font-size: 12px;'>
            " + Convert.ToString(dt.Rows[0]["requesterName"]) + "" +
    @"</td>
        <td style='color: #eeeeee; font-family: Calibri; font-size: 12px;' align='center'>
            Designation:
        </td>
        <td  style='color: #ffffff; font-family: Calibri; font-size: 12px;' align='left'>
            " + Convert.ToString(dt.Rows[0]["RequesterDesignation"]) + "" +
    @"</td>
        <td  style='color: #eeeeee; font-family: Calibri; font-size: 12px;' align='center'>
            Department:
        </td>
        <td style='color: #ffffff; font-family: Calibri; font-size: 12px;'>
            &nbsp; " + Convert.ToString(dt.Rows[0]["requesterDeptName"]) + "" +
    @"</td>";

            return html;
        }
        return "";
    }
    public static string GetRequestDetailHtml(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string detailHTML = "<table border='0' bordercolor='#CCCCCC' cellpadding='0' cellspacing='2' align='center' width='620px' style='color:#0d6e8c; font-family:Calibri; font-size:12px;border-top:thick #FFFFFF solid;'>";
            detailHTML += "<tr valign='bottom'>";
            int columnCount = 0;

            foreach (DataColumn dc in dt.Columns)
            {
                detailHTML += "<td width='100' style='color:#222222;' bgcolor='#f7f7f7'>&nbsp;" + dc.ColumnName.Replace("?", "") + "</td>";
                detailHTML += "<td width='190' colspan='" + (dc.ColumnName.EndsWith("?") ? "3" : "1") + "'style='color:#0d6e8c;' bgcolor='#dfebf4'>&nbsp;" + dt.Rows[0][dc.ColumnName] + "</td>";

                columnCount++;

                if (columnCount % 2 == 0)
                {
                    detailHTML += "</tr><tr>";
                }
            }

            detailHTML += "</tr></table>";
            return detailHTML;
        }
        return "";
    }
    public static DataTable GetRequesterInfo(int RequesterId)
    {
        DataTable Employees = context.UserLogins.Where(c => c.User_Code == RequesterId

            ).Select(c => new
            {
                requesterName = c.Full_Name,
                RequesterDesignation = c.Setup_Employee2.DesignationId == null ? "" : c.Setup_Employee2.Setup_Designation.DesignationName,
                requesterDeptName = c.Setup_Employee2.DesignationId == null ? "" : c.Setup_Employee2.Setup_Department.DepartmentName,
            }).ToList().ToDataTable();
        return Employees;
    }
    public static string ComposeEmailLinks(string pageURL, Dictionary<string, string> urlParameter, bool includeWFParameter, int workflowDataId = 0)
    {
        // the DBHelper send approval email changes this XXX for the next WFId
        string WFId = workflowDataId.ToString();

        string queryString = ""; //"?" + ParameterName + "=" + ParameterValue;

        if (urlParameter.Count > 0)
        {
            queryString += "?";
            foreach (KeyValuePair<string, string> keyValuePair in urlParameter)
            {
                queryString += keyValuePair.Key + "=" + keyValuePair.Value + "&";
            }

            queryString = queryString.Substring(0, queryString.Length - 1);
        }

        if (includeWFParameter)
            queryString += "&WF=" + WFId;


        string getLinksHTML =
     "<table height='62' border='0' bgcolor='#f7f7f7' cellpadding='0' cellspacing='0' align='left' width='360px' style='color:#0d6e8c; font-family:Calibri; font-size:12px; margin-left:5px; border:#CCCCCC dotted thin;'>"
   + "<tr>"
   + "<td width='100' style='color:#222222;'>&nbsp;I am in Sybrid</td>"
   + "<td width='75' style='color:#0d6e8c; text-align:right'><img src='#$4/Images/arrow1.png#$4'/>&nbsp;</td>"
   + "<td width='75' style='color:#222222; text-align:center'><a href='#1' style='color:#000000; text-decoration:none'>Click Here</a>&nbsp;</td>"
   + "</tr>"

   + "</table>";

        getLinksHTML = getLinksHTML.Replace("#1", Constant.Urls.RootInSybrid + pageURL + queryString);


        return getLinksHTML;
    }
    public static void WriteFile(string error)
    {
        DateTime dateTime = DateTime.Now;
        string EmailLog = AppDomain.CurrentDomain.BaseDirectory + "/"+ "EmailLog";
        if (!Directory.Exists(EmailLog))
            Directory.CreateDirectory(EmailLog);
        string Year = EmailLog + "/" + dateTime.ToString("yyyy");
        if (!Directory.Exists(Year))
            Directory.CreateDirectory(Year);
        string Month = Year + "/" + dateTime.ToString("MMM");
        if (!Directory.Exists(Month))
            Directory.CreateDirectory(Month);
        string Date = dateTime.ToString(Constant.DateFormatDDMMYYYY);
        string LogFileName = Month + "/" + Date + ".txt";

        if (!System.IO.File.Exists(LogFileName))
        {
            // Create a file to write to. 
            using (System.IO.StreamWriter sw = System.IO.File.CreateText(LogFileName))
            {

            }
        }
        // This text is always added, making the file longer over time 
        // if it is not deleted. 
        using (System.IO.StreamWriter sw = System.IO.File.AppendText(LogFileName))
        {
            sw.WriteLine(DateTime.Now.ToString() + ": " + error);
        }
    }


}
