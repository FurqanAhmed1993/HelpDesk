using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;


/// <summary>
/// Summary description for Constant
/// </summary>
public class Constant
{
    public Constant()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public const string ConnectionStringKey = "TicketSystemEntities";
    public const string DateTimeFormat1 = "dd-MMM-yyyy HH:mm:ss";
    public const string DateFormatLong = "dd/MM/yyyy HH:mm:ss";
    public const string DateFormatLong_ = "MM/dd/yyyy HH:mm:ss";
    public const string DateFormat = "MMM dd,yyyy";
    public const string DateFormatDDMMYYYY = "dd-MM-yyyy"; // "MMM dd,yyyy";
    public const string DateFormat1 = "dd-MMM-yyyy ddd";
    public const string DateFormatInt = "yyyyMMdd";
    public const string TimeFormatAMPM = "HH:mm:ss tt";
    public const string TimeFormatHHMMSS = "HH:mm:ss";
    public const string IntDateFormat = "yyyyMMdd";
    public const string SecurityKey = "PsW-HeLpDesK";
    public const string DefaultPassword = "psw@098";
    public static string EmailFooter = "<b style='font-size: 15px'>PSW Support Team<br />  <b>";


    public enum Role
    {
        SuperAdmin = 18,
        Admin = 19,
        Level_1 = 20,
        Level_2_3 = 28,
        Client = 30,
    }

    public enum RequestType
    {
        Complaint = 1,
    }
    public enum Application
    {
        CDMS = 0,
        Ticket = 5,
    }

    public enum TicketType
    {
        External = 1,
        Internal = 2,
    }

    public enum EmailTemplates
    {
        Customer = 1,
        Department = 2
    }


    public enum TicketStatus
    {
        InProgress = 1,
        Cancel = 2,
        Closed = 3,
        Escalated = 4,
        Resolved = 5,
        OnHold = 6,
        Re_Open = 7,
        New = 8
    }


    public enum RequestMode
    {
        Email = 1,
        Fax = 2,
        Phone = 3,
        System = 4,
    }



    public enum ChatType
    {
        Ticket_Reply_Response = 1,
        Task_Reply_Response = 2,
        Ticket_Internel_Chat = 3,
        Internel_Chat = 4,
    }


    public static class Urls
    {
        public static string RootInSybrid { get { return Convert.ToString(WebConfigurationManager.AppSettings["Root"]); } }

    }


    public enum Product
    {
        ION = 1,
        Metro = 22,
        Radio = 4,
        WIMAX = 5,
        Qubee = 6,
        Telenor = 7,
        IDirect = 8,
        DS = 9,
        PRI = 10,
        Zong = 11,
        EthernetServices = 12,
        DXX = 13,
        DarkCore = 14,
        Training = 15,
        InternalMeeting = 16,
        ExternalMeeting = 17,
        Survey = 18,
        Fiber = 25,
        Satellite = 21,
        MRTG = 23,
        Terminated = 24,
        Three_G = 26,
        MetroFiber = 3,
        SmartWifi = 28,
        VendorRadio = 27,
        Email = 29,
    }

    public class ResponseKeys
    {
        public static string Response = "Response";
        public static string ResponseCode = "ResponseCode";
        public static string ErrorMessage = "ErrorMessage";
        public static string TicketData = "TicketData";
        public static string TicketStatuses = "TicketStatuses";
        public static string Priorities = "Priorities";
        public static string ServiceCategories = "ServiceCategories";
        public static string NayaPayId = "NayaPayId";
        public static string TicketNo = "TicketCode";
        public static string ResponseMessage = "ResponseMessage";
        public static string CustomerAccountId = "CustomerAccountId";
        public static string AuthToken = "AuthToken";
        public static string UploadedImageIds = "UploadedImageIds";
        public static string Departments = "Departments";
        public static string TotalRecords = "TotalRecords";
    }

    public class ErrorMessages
    {
        public static string Success = "Success";
        public static string TokenExpired = "Token Expired";
        public static string NotGeneratedAgainstThisUser = "Token is not valid for this user";
        public static string InvalidToken = "Invalid Token";
        public static string InvalidCredentials = "Invalid Credentials";
        public static string InvalidErrorCode = "Invalid Error Code";
        public static string Failure = "Failure";
        public static string Exception = "An Exception has been occured";
        public static string TicketException = "An error occured while generating ticket";
        public static string TicketGenerated = "Your ticket has been generated";
        public static string TicketUpdated = "Your ticket has been updated";
        public static string InvalidTicket = "Please provide a valid ticket code";
        public static string AtleastOneFileRequired = "Please provide atleast 1 file to upload";
        public static string MaxFilesCountExceed = "Maximum 25 files allowed";
    }

    public class ErrorCodes
    {
        public static string Success = "00";
        public static string TokenExpired = "01";
        public static string NotGeneratedAgainstThisUser = "02";
        public static string InvalidToken = "03";
        public static string InvalidCredentials = "04";
        public static string Exception = "05";
        public static string TicketException = "06";
        public static string ValidationError = "07";
        public static string InvalidTicket = "08";
        public static string MaxFilesCountExceed = "09";
        public static string AtleastOneFileRequired = "10";
    }

    //public enum DefaultDepartment
    //{
    //    Ticket_Reply_Response = 1,
    //    Task_Reply_Response = 2,
    //    Ticket_Internel_Chat = 3,
    //}
    public class TicketDetail
    {
        public int RequestTypeSubcategoryFieldTypeId { get; set; }
        public string FieldValue { get; set; }
        public int? RequestTypeSubcategoryFieldTypeDetailId { get; set; }
    }

    public enum DepartmentId
    {
        L1 = 4,
        L2 = 5,
        l3 = 11,

    }

    public enum OperationTypeID
    {
        Insert = 1,
        Update = 2,
        Delete = 3,

    }


    public struct TS_Setup_TicketPreRequisiteValuesTypeID
    {
        public const int Bank = 1;
        public const int OGA = 2;
        public const int OGA2 = 3;
        public const int ReportedBy = 4;
    }

}