using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Transactions;
using Newtonsoft.Json;
using System.Collections;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.EntityClient;
using System.Threading;
using System.Data.Objects.SqlClient;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Script.Serialization;

[ServiceContract(Namespace = "CyberTicketService")]

[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

public class TicketService
{
    DAL.TicketSystemEntities context = new DAL.TicketSystemEntities();
    Base baseclass = new Base();



    [OperationContract]
    public string getDepartments()
    {
        var List = context.Setup_Department.Where(x => x.IsActive == true).OrderBy(x => x.DepartmentName).Select(s => new
        {
            Value = s.DepartmentName,
            Id = s.DepartmentId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getInitiator()
    {
        var JSON = "";

        var List = context.UserLogins.Where(x => x.Is_Active == true && (x.User_Code == baseclass.UserId))
        .OrderBy(x => x.Full_Name).Select(s => new
        {
            Value = s.Full_Name,
            Id = s.User_Code
        }).ToList();
        JSON = JsonConvert.SerializeObject(List);

        return JSON;
    }

    [OperationContract]
    public string getAssigneeByDepartmentId(int DepartmentId)
    {
        var JSON = "";

        var List = context.UserLogins.Where(x => x.Is_Active == true && x.Setup_Employee2.DepartmentId == DepartmentId)
        .OrderBy(x => x.Full_Name).Select(s => new
        {
            Value = s.Full_Name,
            Id = s.User_Code
        }).ToList();
        JSON = JsonConvert.SerializeObject(List);

        return JSON;
    }

    [OperationContract]
    public string getCategory(int RequestTypeId)
    {
        var List = context.TS_Setup_RequestTypeCategory.Where(x => x.IsActive == true).OrderBy(x => x.CategoryName).Select(s => new
        {
            Value = s.CategoryName,
            Id = s.CategoryId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getSubcategory(int CategoryId)
    {
        var List = context.TS_Setup_RequestTypeSubcategory.Where(x => x.IsActive == true && x.CategoryId == CategoryId).OrderBy(x => x.SubcategoryName).Select(s => new
        {
            Value = s.SubcategoryName,
            Id = s.SubcategoryId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }


    [OperationContract]
    public string getRegularityAuthority()
    {
        //var List = context.Setup_RegularityAuthority.Where(x => x.IsActive == true).OrderBy(x => x.RegularityAuthorityId).Select(s => new
        //{
        //    Value = s.RegularityAuthorityName,
        //    Id = s.RegularityAuthorityId
        //}).ToList();
        //var JSON = JsonConvert.SerializeObject(List);
        //return JSON;
        return "";
    }


    [OperationContract]
    public string getPriority()
    {
        var List = context.TS_Setup_Priority.Where(c => c.IsActive == true
          && c.IsActive == true)
        .OrderBy(x => x.PriorityName).Select(s => new
        {
            Value = s.PriorityName,
            Id = s.PriorityId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getCity()
    {
        var List = context.TS_Setup_City.Where(c => c.IsActive == true
          && c.IsActive == true)
        .OrderBy(x => x.CityName).Select(s => new
        {
            Value = s.CityName,
            Id = s.CityId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getReportedBy()
    {
        var List = context.Setup_TicketPreRequisite_Values.Where(c => c.IsActive == true
          && c.FormType == Constant.TS_Setup_TicketPreRequisiteValuesTypeID.ReportedBy)
        .OrderBy(x => x.Id).Select(s => new
        {
            Value = s.Name,
            Id = s.Id
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getRequestType()
    {
        var List = context.TS_Setup_RequestType.Where(x => x.IsActive == true).OrderBy(x => x.RequestTypeName).Select(s => new
        {
            Value = s.RequestTypeName,
            Id = s.RequestTypeId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }



    [OperationContract]
    public string getSubcategoryEdit(int? CategoryId)
    {
        var List = context.TS_Setup_RequestTypeSubcategory.Where(x => x.IsActive == true && x.CategoryId == CategoryId).OrderBy(x => x.SubcategoryName).Select(s => new
        {
            Value = s.SubcategoryName,
            Id = s.SubcategoryId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string GetTypeOfIssue()
    {
        var List = context.TS_Setup_TypeOfIssue.Where(x => x.IsActive == true).Select(s => new
        {
            Value = s.TypeOfIssue,
            Id = s.TypeOfIssueId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }
    [OperationContract]
    public string GetTypeOfComplaint()
    {
        var List = context.TS_Setup_TypeOfComplaint.Where(x => x.IsActive == true).Select(s => new
        {
            Value = s.TypeOfComplaint,
            Id = s.TypeOfComplaintId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }
    public string getCategoryInitiate()
    {
        var List = context.TS_Setup_RequestTypeCategory.Where(x => x.IsActive == true).OrderBy(x => x.CategoryName).Select(s => new
        {
            Value = s.CategoryName,
            Id = s.CategoryId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }


    [OperationContract]
    public string getRequestMode()
    {
        var List = context.TS_Setup_RequestMode.Where(x => x.IsActive == true).OrderBy(x => x.RequestMode).Select(s => new
        {
            Value = s.RequestMode,
            Id = s.RequestModeId
        }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getSourceEmployeeforCC(string Destination, int DepartmentId)
    {
        var JSON = "";

        List<int> Destinations_ids = null;
        DataTable DtDestination = null;
        List<DAL.UserLogin> UserLoginDestination = (List<DAL.UserLogin>)Common.Deserialize(Destination, typeof(List<DAL.UserLogin>));
        DtDestination = UserLoginDestination.ToDataTable();
        Destinations_ids = (from row in DtDestination.AsEnumerable() select Convert.ToInt32(row["User_Code"])).ToList();
        var List = context.UserLogins.Where(c => c.Setup_Employee2.DepartmentId == DepartmentId && c.Is_Active == true && (!Destinations_ids.Contains(c.User_Code))).OrderBy(x => x.Full_Name).Select(s => new
        {
            Value = s.Full_Name,
            Id = s.User_Code
        }).ToList();
        JSON = JsonConvert.SerializeObject(List);

        return JSON;
    }

    public string getAssignee()
    {
        var JSON = "";

        var List = context.UserLogins.Where(x => x.Is_Active == true)
        .OrderBy(x => x.Full_Name).Select(s => new
        {
            Value = s.Full_Name,
            Id = s.User_Code
        }).ToList();
        JSON = JsonConvert.SerializeObject(List);

        return JSON;
    }

    [OperationContract]
    public string getSelectedEmployeeforCC(string selectedSource, string Destination, int DepartmentId)
    {
        List<DAL.UserLogin> UserLoginselectedSource = (List<DAL.UserLogin>)Common.Deserialize(selectedSource, typeof(List<DAL.UserLogin>));
        DataTable DtSource = UserLoginselectedSource.ToDataTable();
        List<int> Source_ids = (from row in DtSource.AsEnumerable() select Convert.ToInt32(row["User_Code"])).ToList();

        List<DAL.UserLogin> UserLoginDestination = (List<DAL.UserLogin>)Common.Deserialize(Destination, typeof(List<DAL.UserLogin>));
        DataTable DtDestination = UserLoginDestination.ToDataTable();
        List<int> Destinations_ids = (from row in DtDestination.AsEnumerable() select Convert.ToInt32(row["User_Code"])).ToList();
        var Merge_UserIds = Source_ids.Concat(Destinations_ids);
        var List = context.UserLogins.Where(c =>
            c.Is_Active == true
            && (Merge_UserIds.Contains(c.User_Code))
            ).OrderBy(x => x.Full_Name).Select(s => new
            {
                Value = s.Full_Name,
                Id = s.User_Code
            }).ToList();
        var JSON = JsonConvert.SerializeObject(List);

        return JSON;
    }

    [OperationContract]
    public string getADDCCEmails(string Source, string Destination)
    {
        List<DAL.UserLogin> UserLoginDestination = (List<DAL.UserLogin>)Common.Deserialize(Destination, typeof(List<DAL.UserLogin>));
        DataTable DtDestination = UserLoginDestination.ToDataTable();
        List<int> Destinations_ids = (from row in DtDestination.AsEnumerable() select Convert.ToInt32(row["User_Code"])).ToList();
        var List = context.UserLogins.Where(c => c.Is_Active == true && (Destinations_ids.Contains(c.User_Code))).OrderBy(x => x.Full_Name).Select(s => new
        {
            Value = s.Full_Name,
            Id = s.User_Code,
            Email = s.Login_ID,
        }).ToList();

        for (int i = 0; i < List.Count; i++)
        {
            Source = Source + List[i].Email.ToString() + ";";
        }

        return Source;
    }

    [OperationContract]
    public string GetSearchCustomer(string ContactSearch, string EmailSearch)
    {
        DataTable List = context.Customers.Where(x => x.IsActive == true
        && (x.ContactNo == ContactSearch || ContactSearch == String.Empty)
        && (x.EmailAddress == EmailSearch || EmailSearch == String.Empty))
        .Select(s => new
        {
            CustomerId = s.CustomerId,
            CustomerName = s.CustomerName,
            ContactNo = s.ContactNo,
            EmailAddress = s.EmailAddress,
            CityId = s.CityId,
            Address = s.Address,
            AlternativeNumber = s.AlternativeNumber
        }).OrderBy(a => a.CustomerId).ToList().ToDataTable();
        if (List != null && List.Rows.Count == 0)
        {
            List.Rows.Add(0, "", "", "");
        }
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string GetCustomerContact(string Contact)
    {
        DataTable List = context.Customers.Where(x => x.IsActive == true && x.ContactNo == Contact).Select(s => new
        {
            ContactNo = s.ContactNo,
        }).ToList().ToDataTable();
        if (List != null && List.Rows.Count == 0)
        {
            List.Rows.Add("");
        }
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }
    [OperationContract]
    public string GetCustomerEmail(string Email)
    {
        DataTable List = context.Customers.Where(x => x.IsActive == true && x.EmailAddress == Email).Select(s => new
        {
            EmailAddress = s.EmailAddress,
        }).ToList().ToDataTable();
        if (List != null && List.Rows.Count == 0)
        {
            List.Rows.Add("");
        }
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    //[OperationContract]
    //public string ChangeCustomer(int TicketMasterId, int customerId)
    //{
    //    string _return = "";
    //    DateTime dt = DateTime.Now;
    //    int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
    //    DAL.TS_TicketMaster updateTicketMaster = context.TS_TicketMaster.FirstOrDefault(x => x.TicketMasterId == TicketMasterId);
    //    if (updateTicketMaster != null)
    //    {
    //        updateTicketMaster.CustomerId = customerId;
    //        updateTicketMaster.ModifiedDate = dt;
    //        updateTicketMaster.ModifiedDateInt = IntDate;
    //        updateTicketMaster.ModifiedBy = baseclass.UserId;
    //        updateTicketMaster.IsActive = true;
    //        updateTicketMaster.UserIP = baseclass.UserIP;
    //        context.SaveChanges();

    //        #region Ticket Detail Table

    //        DAL.TS_TicketDetail objTicketDetail = new DAL.TS_TicketDetail();
    //        objTicketDetail.TicketMasterId = TicketMasterId;
    //        objTicketDetail.AssigneeId = updateTicketMaster.AssigneeId;
    //        objTicketDetail.StatusId = (int)Constant.TicketStatus.NotAssigned;
    //        objTicketDetail.DepartmentId = updateTicketMaster.DepartmentId;
    //        objTicketDetail.Description = updateTicketMaster.Description;
    //        objTicketDetail.Description_html = updateTicketMaster.Description_html;
    //        objTicketDetail.CreatedBy = baseclass.UserId;
    //        objTicketDetail.CreatedDateInt = IntDate;
    //        objTicketDetail.CreatedDate = dt;
    //        objTicketDetail.IsActive = true;
    //        objTicketDetail.UserIP = baseclass.UserIP;
    //        context.TS_TicketDetail.Add(objTicketDetail);
    //        context.SaveChanges();
    //        #endregion


    //        _return = "updated";
    //    }

    //    return _return;
    //}


    //SaveTicket

    [OperationContract]
    public string SaveTicket(string hfCustomerId, int TicketMasterId, string Customer, string TicketMaster, string Ticket_EmailTo, string Ticket_EmailCC, string FilePath, string TicketDetailData)
    {
        int CustomerId;
        string _return = "";
        #region

        #region Customer Table
        if (hfCustomerId == "0")
        {
            DAL.Customer obj_Customer = (DAL.Customer)Common.Deserialize(Customer, typeof(DAL.Customer));

            obj_Customer.CreatedDate = DateTime.Now;
            obj_Customer.CreatedBy = baseclass.UserId;
            obj_Customer.IsActive = true;
            obj_Customer.UserIp = baseclass.UserIP;
            context.Customers.Add(obj_Customer);
            context.SaveChanges();
            CustomerId = obj_Customer.CustomerId;
        }
        else
        {
            CustomerId = int.Parse(hfCustomerId);

            DAL.Customer updateCustomer = context.Customers.FirstOrDefault(x => x.CustomerId == CustomerId);
            DAL.Customer obj_Customer = (DAL.Customer)Common.Deserialize(Customer, typeof(DAL.Customer));
            updateCustomer.ModifiedDate = DateTime.Now;
            updateCustomer.ModifiedBy = baseclass.UserId;
            updateCustomer.IsActive = true;
            updateCustomer.UserIp = baseclass.UserIP;
            updateCustomer.CustomerName = obj_Customer.CustomerName;
            updateCustomer.Address = obj_Customer.Address;
            updateCustomer.EmailAddress = obj_Customer.EmailAddress;
            updateCustomer.ContactNo = obj_Customer.ContactNo;
            updateCustomer.CityId = obj_Customer.CityId;
            updateCustomer.AlternativeNumber = obj_Customer.AlternativeNumber;
            context.SaveChanges();
        }
        #endregion

        DAL.TS_TicketMaster obj_TicketMaster = (DAL.TS_TicketMaster)Common.Deserialize(TicketMaster, typeof(DAL.TS_TicketMaster));
        if (obj_TicketMaster != null)
        {
            #region Variables
            DateTime dt = DateTime.Now;
            bool Status = false;
            int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
            int? NULLint = null;
            string TicketCode = context.TS_Populate_Ticket_code().FirstOrDefault().ToString();
            int? InitiatorId = obj_TicketMaster.InitiatorId;
            int DepartmentId = obj_TicketMaster.DepartmentId;
            int RequestTypeId = obj_TicketMaster.RequestTypeId;
            int PriorityId = obj_TicketMaster.PriorityId;
            int? RequestModeId = obj_TicketMaster.RequestModeId;
            int? AssigneeId = obj_TicketMaster.AssigneeId;
            int StatusId = Convert.ToInt32(obj_TicketMaster.StatusId);

            string Description = "", Description_html = "";
            if (!String.IsNullOrEmpty(obj_TicketMaster.Description))
            {
                Description = obj_TicketMaster.Description;
                Description_html = obj_TicketMaster.Description;
            }

            TimeSpan time = DateTime.Now.TimeOfDay;
            #endregion

            #region Insert Ticket Master Table


            obj_TicketMaster.CustomerId = CustomerId;
            obj_TicketMaster.Description = Description;
            obj_TicketMaster.Description_html = Description;
            obj_TicketMaster.TicketTypeId = (int)Constant.TicketType.Internal;
            obj_TicketMaster.TicketCode = TicketCode;
            obj_TicketMaster.TicketCreationDate = dt;
            obj_TicketMaster.TicketCreationDateInt = IntDate;
            obj_TicketMaster.TicketCreationTime = time;
            obj_TicketMaster.CreatedDate = dt;
            obj_TicketMaster.CreatedDateInt = IntDate;
            obj_TicketMaster.CreatedBy = baseclass.UserId;
            obj_TicketMaster.IsActive = true;
            obj_TicketMaster.UserIP = baseclass.UserIP;
            context.TS_TicketMaster.Add(obj_TicketMaster);
            context.SaveChanges();
            TicketMasterId = obj_TicketMaster.TicketMasterId;

            //if (FilePath != "")
            //{
            //   GetSaveAtachment(TicketMasterId, 0, FilePath);
            //}

            int Id = Convert.ToInt32(baseclass.UserId + "00" + baseclass.UserId);
            var _ObjTicketAttachment = context.TS_TicketAttachments.Where(j => j.TargetId == Id).ToList();
            _ObjTicketAttachment.ForEach(a => { a.TargetId = TicketMasterId; });
            context.SaveChanges();

            #endregion


            if (TicketMasterId > 0)
            {
                #region Ticket Detail Table

                DAL.TS_TicketDetail objTicketDetail = new DAL.TS_TicketDetail();
                objTicketDetail.TicketMasterId = TicketMasterId;
                objTicketDetail.AssigneeId = obj_TicketMaster.AssigneeId;
                objTicketDetail.StatusId = StatusId;
                objTicketDetail.DepartmentId = DepartmentId;
                objTicketDetail.Description = Description;
                objTicketDetail.Description_html = Description_html;
                objTicketDetail.CreatedBy = baseclass.UserId;
                objTicketDetail.CreatedDateInt = IntDate;
                objTicketDetail.CreatedDate = dt;
                objTicketDetail.IsActive = true;
                objTicketDetail.UserIP = baseclass.UserIP;
                context.TS_TicketDetail.Add(objTicketDetail);
                context.SaveChanges();
                #endregion

                #region Ticket EmailTo Table

                var _ObjTicketTo = context.TS_TicketTo.Where(c => c.IsActive == true && c.TicketMasterId == TicketMasterId).ToList();
                _ObjTicketTo.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                context.SaveChanges();

                if (Ticket_EmailTo != "")
                {
                    string[] EmailTo = Ticket_EmailTo.Split(';');
                    for (int i = 0; i < EmailTo.Length; i++)
                    {
                        string EmailAddress = EmailTo[i].ToString();
                        if (EmailAddress != "")
                        {
                            DAL.TS_TicketTo objTicketTo = new DAL.TS_TicketTo();
                            objTicketTo.TicketMasterId = TicketMasterId;
                            objTicketTo.EmailId = EmailAddress;
                            objTicketTo.CreatedBy = baseclass.UserId;
                            objTicketTo.CreatedDate = dt;
                            objTicketTo.IsActive = true;
                            objTicketTo.UserIP = baseclass.UserIP;
                            context.TS_TicketTo.Add(objTicketTo);
                            context.SaveChanges();
                        }
                    }
                }
                #endregion

                #region Ticket Email CC Table
                var _ObjTicketCc = context.TS_TicketCC.Where(c => c.IsActive == true && c.TicketMasterId == TicketMasterId).ToList();
                _ObjTicketCc.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                context.SaveChanges();
                if (Ticket_EmailCC != "")
                {
                    string[] EmailCC = Ticket_EmailCC.Split(';');
                    for (int i = 0; i < EmailCC.Length; i++)
                    {
                        string EmailAddress = EmailCC[i].ToString();
                        if (EmailAddress != "")
                        {
                            var User = context.UserLogins.FirstOrDefault(a => a.Login_ID == EmailAddress);
                            DAL.TS_TicketCC objTicketCC = new DAL.TS_TicketCC();
                            objTicketCC.TicketMasterId = TicketMasterId;
                            objTicketCC.AssigneeId = User == null ? NULLint : User.User_Code;
                            objTicketCC.EmailId = EmailAddress;
                            objTicketCC.CreatedBy = baseclass.UserId;
                            objTicketCC.CreatedDate = dt;
                            objTicketCC.IsActive = true;
                            objTicketCC.UserIP = baseclass.UserIP;
                            context.TS_TicketCC.Add(objTicketCC);
                            context.SaveChanges();
                        }
                    }
                }
                #endregion



                JavaScriptSerializer objJavascriptSerializer = new JavaScriptSerializer();
                Dictionary<string, object> objResponse = new Dictionary<string, object>();
                var dataTicket = objJavascriptSerializer.DeserializeObject(TicketDetailData);
                List<Constant.TicketDetail> cM_TicketDetails = JsonConvert.DeserializeObject<List<Constant.TicketDetail>>(TicketDetailData);
                DataTable dtTicketDetail = Common.ToDataTable(cM_TicketDetails);
                //   string dbConnectionString = context.Database.Connection.ConnectionString;
                // SqlConnection con = new SqlConnection(dbConnectionString);
                DataTable dtSubcategoryFieldValue = new DataTable();


                DataTable ds = new DataTable();
                DAL.TicketSystemEntities context1 = new DAL.TicketSystemEntities();
                string dbConnectionString = context1.Database.Connection.ConnectionString;
                SqlConnection con = new SqlConnection(dbConnectionString);
                SqlDataAdapter da = new SqlDataAdapter("Setup_RequestTypeSubcategoryFieldValue_Insert", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@TicketMasterId", SqlDbType.Int).Value = TicketMasterId;
                da.SelectCommand.Parameters.Add("@TicketSubCategoryDetailData", SqlDbType.Structured).Value = dtTicketDetail;
                da.SelectCommand.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = false;
                da.SelectCommand.Parameters.Add("@userid", SqlDbType.Int).Value = baseclass.UserId;
                da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.NVarChar).Value = baseclass.UserIP;
                da.Fill(ds);




                Status = true;
                _return = obj_TicketMaster.TicketCode;

            }


            if (Status == true)
            {
                // FOR EMAIL DEPARTMENT
                SendEmailToDepartment(TicketMasterId);
                GenerateEmailForCustomer(TicketMasterId, Ticket_EmailTo, Ticket_EmailCC);
                // FOR EMAIL Assignee
                if (Convert.ToInt32(AssigneeId) > 0)
                    GenerateEmailforAssignee(TicketMasterId);
            }

        }

        #endregion

        return _return;
    }
    [OperationContract]
    public string UpdateAssignee_(int MasterId, int DepartmentId, int? AssigneeId, int StatusId, int TypeOfIssueId, string Description, int TypeOfComplaintId, string Ticket_EmailTo, string Ticket_EmailCC, int ProductSubCategoryId)
    {
        int? nullint = null;
        string ReturnStatus = "0";
        if (MasterId > 0 && DepartmentId > 0)
        {
            DAL.TS_TicketMaster objTicketMaster = context.TS_TicketMaster.FirstOrDefault(j => j.TicketMasterId == MasterId);
            if (objTicketMaster != null)
            {
                DateTime dt = DateTime.Now;
                int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
                int _DepartmentId = objTicketMaster.DepartmentId;
                int? _AssigneeId = objTicketMaster.AssigneeId == null ? nullint : objTicketMaster.AssigneeId;

                #region Update TicketMaster
                objTicketMaster.StatusId = Convert.ToInt32(StatusId);
                objTicketMaster.DepartmentId = DepartmentId;
                objTicketMaster.Description = Description;
                objTicketMaster.TypeOfIssueId = TypeOfIssueId == 0 ? nullint : TypeOfIssueId;
                objTicketMaster.ProductSubCategoryId = ProductSubCategoryId == 0 ? nullint : ProductSubCategoryId;
                objTicketMaster.AssigneeId = AssigneeId == 0 ? nullint : AssigneeId;
                objTicketMaster.IsAssigned = AssigneeId == null ? false : true;
                objTicketMaster.ModifiedBy = baseclass.UserId;
                objTicketMaster.ModifiedDate = dt;
                objTicketMaster.ModifiedDateInt = IntDate;
                objTicketMaster.IsActive = true;
                objTicketMaster.UserIP = baseclass.UserIP;
                objTicketMaster.TypeOfComplaintId = TypeOfComplaintId == 0 ? nullint : TypeOfComplaintId;
                //context.SaveChanges();
                #endregion

                #region Insert TicketDetail
                DAL.TS_TicketDetail objTicketdetail = new DAL.TS_TicketDetail();
                objTicketdetail.TicketMasterId = MasterId;
                objTicketdetail.AssigneeId = objTicketMaster.AssigneeId;
                objTicketdetail.StatusId = Convert.ToInt32(StatusId);
                objTicketdetail.TypeOfIssueId = TypeOfIssueId == 0 ? nullint : TypeOfIssueId;
                objTicketdetail.ProductSubCategoryId = ProductSubCategoryId == 0 ? nullint : ProductSubCategoryId;
                objTicketdetail.DepartmentId = DepartmentId;
                objTicketdetail.Description = Description;
                objTicketdetail.Description_html = objTicketMaster.Description_html;
                objTicketdetail.CreatedBy = baseclass.UserId;
                objTicketdetail.CreatedDateInt = IntDate;
                objTicketdetail.CreatedDate = dt;
                objTicketdetail.IsActive = true;
                objTicketdetail.UserIP = baseclass.UserIP;
                objTicketdetail.TypeOfComplaintId = TypeOfComplaintId == 0 ? nullint : TypeOfComplaintId;
                context.TS_TicketDetail.Add(objTicketdetail);
                context.SaveChanges();
                ReturnStatus = "1";
                #endregion
                if (objTicketdetail.TicketDetailId > 0)
                {
                    if (ReturnStatus == "1")
                    {
                        if (AssigneeId > 0 && _AssigneeId != AssigneeId)
                        {
                            GenerateEmailforAssignee(MasterId);
                        }

                        if (_DepartmentId != DepartmentId)
                        {
                            SendEmailToDepartment(MasterId);
                        }

                        int TicketDetailId = objTicketdetail.TicketDetailId;
                        if (StatusId == (int)Constant.TicketStatus.Escalated)
                        {

                            if (!IsEscalatedExist(MasterId, TicketDetailId))
                            {
                                #region Ticket EmailTo Table

                                var _ObjTicketTo = context.TS_TicketTo.Where(c => c.IsActive == true && c.TicketMasterId == MasterId).ToList();
                                _ObjTicketTo.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                                context.SaveChanges();

                                if (Ticket_EmailTo != "")
                                {
                                    string[] EmailTo = Ticket_EmailTo.Split(';');
                                    for (int i = 0; i < EmailTo.Length; i++)
                                    {
                                        string EmailAddress = EmailTo[i].ToString();
                                        if (EmailAddress != "")
                                        {
                                            DAL.TS_TicketTo objTicketTo = new DAL.TS_TicketTo();
                                            objTicketTo.TicketMasterId = MasterId;
                                            objTicketTo.EmailId = EmailAddress;
                                            objTicketTo.CreatedBy = baseclass.UserId;
                                            objTicketTo.CreatedDate = dt;
                                            objTicketTo.IsActive = true;
                                            objTicketTo.UserIP = baseclass.UserIP;
                                            context.TS_TicketTo.Add(objTicketTo);
                                            context.SaveChanges();
                                        }
                                    }
                                }
                                #endregion

                                #region Ticket Email CC Table
                                var _ObjTicketCc = context.TS_TicketCC.Where(c => c.IsActive == true && c.TicketMasterId == MasterId).ToList();
                                _ObjTicketCc.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                                context.SaveChanges();
                                if (Ticket_EmailCC != "")
                                {
                                    string[] EmailCC = Ticket_EmailCC.Split(';');
                                    for (int i = 0; i < EmailCC.Length; i++)
                                    {
                                        string EmailAddress = EmailCC[i].ToString();
                                        if (EmailAddress != "")
                                        {
                                            var User = context.UserLogins.FirstOrDefault(a => a.Login_ID == EmailAddress);
                                            DAL.TS_TicketCC objTicketCC = new DAL.TS_TicketCC();
                                            objTicketCC.TicketMasterId = MasterId;
                                            objTicketCC.AssigneeId = User == null ? nullint : User.User_Code;
                                            objTicketCC.EmailId = EmailAddress;
                                            objTicketCC.CreatedBy = baseclass.UserId;
                                            objTicketCC.CreatedDate = dt;
                                            objTicketCC.IsActive = true;
                                            objTicketCC.UserIP = baseclass.UserIP;
                                            context.TS_TicketCC.Add(objTicketCC);
                                            context.SaveChanges();
                                        }
                                    }
                                }
                                #endregion

                                GenerateEscalatedEmailForCustomer(MasterId, TicketDetailId, Ticket_EmailTo, Ticket_EmailCC);
                            }

                        }
                        else if (StatusId == (int)Constant.TicketStatus.Closed)
                        {
                            #region Ticket EmailTo Table

                            var _ObjTicketTo = context.TS_TicketTo.Where(c => c.IsActive == true && c.TicketMasterId == MasterId).ToList();
                            _ObjTicketTo.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                            context.SaveChanges();

                            if (Ticket_EmailTo != "")
                            {
                                string[] EmailTo = Ticket_EmailTo.Split(';');
                                for (int i = 0; i < EmailTo.Length; i++)
                                {
                                    string EmailAddress = EmailTo[i].ToString();
                                    if (EmailAddress != "")
                                    {
                                        DAL.TS_TicketTo objTicketTo = new DAL.TS_TicketTo();
                                        objTicketTo.TicketMasterId = MasterId;
                                        objTicketTo.EmailId = EmailAddress;
                                        objTicketTo.CreatedBy = baseclass.UserId;
                                        objTicketTo.CreatedDate = dt;
                                        objTicketTo.IsActive = true;
                                        objTicketTo.UserIP = baseclass.UserIP;
                                        context.TS_TicketTo.Add(objTicketTo);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Ticket Email CC Table
                            var _ObjTicketCc = context.TS_TicketCC.Where(c => c.IsActive == true && c.TicketMasterId == MasterId).ToList();
                            _ObjTicketCc.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                            context.SaveChanges();
                            if (Ticket_EmailCC != "")
                            {
                                string[] EmailCC = Ticket_EmailCC.Split(';');
                                for (int i = 0; i < EmailCC.Length; i++)
                                {
                                    string EmailAddress = EmailCC[i].ToString();
                                    if (EmailAddress != "")
                                    {
                                        var User = context.UserLogins.FirstOrDefault(a => a.Login_ID == EmailAddress);
                                        DAL.TS_TicketCC objTicketCC = new DAL.TS_TicketCC();
                                        objTicketCC.TicketMasterId = MasterId;
                                        objTicketCC.AssigneeId = User == null ? nullint : User.User_Code;
                                        objTicketCC.EmailId = EmailAddress;
                                        objTicketCC.CreatedBy = baseclass.UserId;
                                        objTicketCC.CreatedDate = dt;
                                        objTicketCC.IsActive = true;
                                        objTicketCC.UserIP = baseclass.UserIP;
                                        context.TS_TicketCC.Add(objTicketCC);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                            ////GenerateClosedEmailForCustomer(MasterId, TicketDetailId, Ticket_EmailTo, Ticket_EmailCC);
                        }

                        else if (StatusId == (int)Constant.TicketStatus.InProgress)
                        {
                            #region Ticket EmailTo Table

                            var _ObjTicketTo = context.TS_TicketTo.Where(c => c.IsActive == true && c.TicketMasterId == MasterId).ToList();
                            _ObjTicketTo.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                            context.SaveChanges();

                            if (Ticket_EmailTo != "")
                            {
                                string[] EmailTo = Ticket_EmailTo.Split(';');
                                for (int i = 0; i < EmailTo.Length; i++)
                                {
                                    string EmailAddress = EmailTo[i].ToString();
                                    if (EmailAddress != "")
                                    {
                                        DAL.TS_TicketTo objTicketTo = new DAL.TS_TicketTo();
                                        objTicketTo.TicketMasterId = MasterId;
                                        objTicketTo.EmailId = EmailAddress;
                                        objTicketTo.CreatedBy = baseclass.UserId;
                                        objTicketTo.CreatedDate = dt;
                                        objTicketTo.IsActive = true;
                                        objTicketTo.UserIP = baseclass.UserIP;
                                        context.TS_TicketTo.Add(objTicketTo);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                            #region Ticket Email CC Table
                            var _ObjTicketCc = context.TS_TicketCC.Where(c => c.IsActive == true && c.TicketMasterId == MasterId).ToList();
                            _ObjTicketCc.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                            context.SaveChanges();
                            if (Ticket_EmailCC != "")
                            {
                                string[] EmailCC = Ticket_EmailCC.Split(';');
                                for (int i = 0; i < EmailCC.Length; i++)
                                {
                                    string EmailAddress = EmailCC[i].ToString();
                                    if (EmailAddress != "")
                                    {
                                        var User = context.UserLogins.FirstOrDefault(a => a.Login_ID == EmailAddress);
                                        DAL.TS_TicketCC objTicketCC = new DAL.TS_TicketCC();
                                        objTicketCC.TicketMasterId = MasterId;
                                        objTicketCC.AssigneeId = User == null ? nullint : User.User_Code;
                                        objTicketCC.EmailId = EmailAddress;
                                        objTicketCC.CreatedBy = baseclass.UserId;
                                        objTicketCC.CreatedDate = dt;
                                        objTicketCC.IsActive = true;
                                        objTicketCC.UserIP = baseclass.UserIP;
                                        context.TS_TicketCC.Add(objTicketCC);
                                        context.SaveChanges();
                                    }
                                }
                            }
                            #endregion

                        }

                        #region Initial Findings email
                        if (StatusId == (int)Constant.TicketStatus.Escalated && DepartmentId == (int)Constant.DepartmentId.L2)
                        {
                            GenerateInitialFindingsEmail(MasterId, Description);
                        }
                        #endregion

                    }

                }

            }
        }

        return ReturnStatus;
    }

    public bool IsEscalatedExist(int MasterId, int DetailId)
    {
        int EscalatedId = (int)Constant.TicketStatus.Escalated;
        var list = context.TS_TicketDetail.Where(x => x.IsActive == true && x.TicketMasterId == MasterId && x.TicketDetailId != DetailId && x.StatusId == EscalatedId).FirstOrDefault();
        if (list == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private void GenerateEscalatedEmailForCustomer(int TicketMasterId, int TicketDetailId, string To, string CC)
    {
        try
        {
            var ListSelectedTicket = context.TS_TicketMaster.FirstOrDefault(c => c.IsActive == true && c.TicketMasterId == TicketMasterId);
            var Category = context.TS_Setup_RequestTypeCategory.FirstOrDefault(x => x.IsActive == true && x.CategoryId == ListSelectedTicket.RequestTypeCategoryId);
            var Subcategory = context.TS_Setup_RequestTypeSubcategory.FirstOrDefault(x => x.IsActive == true && x.SubcategoryId == ListSelectedTicket.RequestTypeSubCategoryId);
            if (ListSelectedTicket != null)
            {
                string Subject = "RE: Ticket No [" + ListSelectedTicket.TicketCode + "] : " + Category.CategoryName + " : " + Subcategory.SubcategoryName;
                string Description = "Dear Valued Customer,"
                               + "<br /><br />"
                               + "In continuation of our previous e-mail, we have escalated the subject matter to our Technical Team. We will keep you posted on any updates accordingly.";

                string SenderEmailAddress = ConfigurationManager.AppSettings["SenderEmailAddress"];
                string EmailHeader = "<p class=MsoNormal> <b><span style='font-size:11.0pt;font-family:'Calibri',sans-serif'>From:</span></b><span style='font-size:11.0pt;font-family:'Calibri',sans-serif'> " + SenderEmailAddress + " <br> <b>Sent:</b> " + string.Format("{0:f}", DateTime.Now) + "<br> <b>To:</b> " + To + "<br> <b>Subject:</b> " + Subject + "<o:p></o:p></span></p> ";

                var TicketDetail = context.TS_TicketDetail.Where(x => x.IsActive == true && x.TicketMasterId == TicketMasterId && x.TicketDetailId != TicketDetailId).OrderByDescending(x => x.TicketDetailId).ToList();

                string LastHTML_Binding = "";
                for (int i = 0; i < TicketDetail.Count; i++)
                {
                    LastHTML_Binding += "<br /><br />" + TicketDetail[i].Description_html;
                }
                string Msg = Description + "<br /><br /> <p class='MsoNormal'><b><span style='font-size:9.0pt;color:gray'>Thanks & Regards,</span></b></p> " + (string)Constant.EmailFooter + "" + "<br /> <hr />" + LastHTML_Binding;

                ListSelectedTicket.Description_html = EmailHeader + Description;
                ListSelectedTicket.TS_TicketDetail.FirstOrDefault().Description_html = EmailHeader + Description;
                context.SaveChanges();
                Email.SendMail(To, Subject, Msg, CC, "");
            }
        }
        catch
        { }
    }
    private void GenerateClosedEmailForCustomer(int TicketMasterId, int TicketDetailId, string To, string CC)
    {
        try
        {
            var ListSelectedTicket = context.TS_TicketMaster.FirstOrDefault(c => c.IsActive == true && c.TicketMasterId == TicketMasterId);
            var Category = context.TS_Setup_RequestTypeCategory.FirstOrDefault(x => x.IsActive == true && x.CategoryId == ListSelectedTicket.RequestTypeCategoryId);
            var Subcategory = context.TS_Setup_RequestTypeSubcategory.FirstOrDefault(x => x.IsActive == true && x.SubcategoryId == ListSelectedTicket.RequestTypeSubCategoryId);
            if (ListSelectedTicket != null)
            {
                string Subject = "RE: Ticket No [" + ListSelectedTicket.TicketCode + "] : " + Category.CategoryName + " : " + Subcategory.SubcategoryName;
                string Description = "Dear Valued Customer,"
                               + "<br /><br />"
                               + "With reference to your feedback on call, the issue was resolved. Therefore, we are going to close the ticket. Our Enterprise Support is available 24 hours a day "
                               + "at support@psw.gov.pk. So, please feel free to contact us in case of any required assistance.";

                string SenderEmailAddress = ConfigurationManager.AppSettings["SenderEmailAddress"];
                string EmailHeader = "<p class=MsoNormal> <b><span style='font-size:11.0pt;font-family:'Calibri',sans-serif'>From:</span></b><span style='font-size:11.0pt;font-family:'Calibri',sans-serif'> " + SenderEmailAddress + " <br> <b>Sent:</b> " + string.Format("{0:f}", DateTime.Now) + "<br> <b>To:</b> " + To + "<br> <b>Subject:</b> " + Subject + "<o:p></o:p></span></p> ";

                var TicketDetail = context.TS_TicketDetail.Where(x => x.IsActive == true && x.TicketMasterId == TicketMasterId && x.TicketDetailId != TicketDetailId).OrderByDescending(x => x.TicketDetailId).ToList();
                string LastHTML_Binding = "";
                for (int i = 0; i < TicketDetail.Count; i++)
                {
                    LastHTML_Binding += "<br /><br />" + TicketDetail[i].Description_html;
                }

                string Msg = Description + "<br /><br /> <p class='MsoNormal'><b><span style='font-size:9.0pt;color:gray'>Thanks & Regards,</span></b></p> " + (string)Constant.EmailFooter + "" + "<br /> <hr />" + LastHTML_Binding;

                ListSelectedTicket.Description_html = EmailHeader + Description;
                ListSelectedTicket.TS_TicketDetail.FirstOrDefault().Description_html = EmailHeader + Description;
                context.SaveChanges();
                Email.SendMail(To, Subject, Msg, CC, "");
            }
        }
        catch
        { }
    }

    [OperationContract]
    public string UpdateTicket(int TicketMasterId, string TicketMaster, string Ticket_EmailTo, string Ticket_EmailCC, string FilePath)
    {
        string _return = "";
        #region
        /*
        DAL.TS_TicketMaster obj_TicketMaster = (DAL.TS_TicketMaster)Common.Deserialize(TicketMaster, typeof(DAL.TS_TicketMaster));
        if (obj_TicketMaster != null)
        {
            #region Variables
            DateTime dt = DateTime.Now;
            bool Status = false;
            int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
            int? NULLint = null;
            int? Category = obj_TicketMaster.RequestTypeCategoryId;
            int? SubCategory = obj_TicketMaster.RequestTypeSubCategoryId;
            int? InitiatorId = obj_TicketMaster.InitiatorId;
            int DepartmentId = obj_TicketMaster.DepartmentId;
            int RequestTypeId = obj_TicketMaster.RequestTypeId;
            int PriorityId = obj_TicketMaster.PriorityId;
            int RequestModeId = obj_TicketMaster.RequestModeId;
            int? AssigneeId = obj_TicketMaster.AssigneeId;
            string Description = "", Description_html = "";

            Description = obj_TicketMaster.Description == null ? "" : obj_TicketMaster.Description;
            Description_html = obj_TicketMaster.Description == null ? "" : obj_TicketMaster.Description;
            TimeSpan time = DateTime.Now.TimeOfDay;
            #endregion

            #region Update TicketMaster Table
            DAL.TS_TicketMaster updateTicketMaster = context.TS_TicketMaster.FirstOrDefault(x => x.TicketMasterId == TicketMasterId);

            updateTicketMaster.InitiatorId = InitiatorId;
            updateTicketMaster.DepartmentId = DepartmentId;
            updateTicketMaster.RequestTypeId = RequestTypeId;
            updateTicketMaster.RequestTypeCategoryId = Category;
            updateTicketMaster.RequestTypeSubCategoryId = SubCategory;
            updateTicketMaster.PriorityId = PriorityId;
            updateTicketMaster.RequestModeId = RequestModeId;
            updateTicketMaster.AssigneeId = AssigneeId;
            updateTicketMaster.Description = Description;
            updateTicketMaster.Description_html = Description_html;
            updateTicketMaster.ModifiedDate = dt;
            updateTicketMaster.ModifiedDateInt = IntDate;
            updateTicketMaster.ModifiedBy = baseclass.UserId;
            updateTicketMaster.IsActive = true;
            updateTicketMaster.UserIP = baseclass.UserIP;
            context.SaveChanges();
            if (FilePath != "")
                GetSaveAtachment(TicketMasterId, 0, FilePath);

            #endregion


            if (TicketMasterId > 0)
            {
                #region Ticket Detail Table

                DAL.TS_TicketDetail objTicketDetail = new DAL.TS_TicketDetail();
                objTicketDetail.TicketMasterId = TicketMasterId;
                objTicketDetail.AssigneeId = obj_TicketMaster.AssigneeId;
                objTicketDetail.StatusId = obj_TicketMaster.StatusId;
                objTicketDetail.DepartmentId = DepartmentId;
                objTicketDetail.Description = Description;
                objTicketDetail.Description_html = Description_html;
                objTicketDetail.CreatedBy = baseclass.UserId;
                objTicketDetail.CreatedDateInt = IntDate;
                objTicketDetail.CreatedDate = dt;
                objTicketDetail.IsActive = true;
                objTicketDetail.UserIP = baseclass.UserIP;
                context.TS_TicketDetail.Add(objTicketDetail);
                context.SaveChanges();
                #endregion

                #region Ticket EmailTo Table

                var _ObjTicketTo = context.TS_TicketTo.Where(c => c.IsActive == true && c.TicketMasterId == TicketMasterId).ToList();
                _ObjTicketTo.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                context.SaveChanges();

                if (Ticket_EmailTo != "")
                {
                    string[] EmailTo = Ticket_EmailTo.Split(';');
                    for (int i = 0; i < EmailTo.Length; i++)
                    {
                        string EmailAddress = EmailTo[i].ToString();
                        if (EmailAddress != "")
                        {
                            DAL.TS_TicketTo objTicketTo = new DAL.TS_TicketTo();
                            objTicketTo.TicketMasterId = TicketMasterId;
                            objTicketTo.EmailId = EmailAddress;
                            objTicketTo.CreatedBy = baseclass.UserId;
                            objTicketTo.CreatedDate = dt;
                            objTicketTo.IsActive = true;
                            objTicketTo.UserIP = baseclass.UserIP;
                            context.TS_TicketTo.Add(objTicketTo);
                            context.SaveChanges();
                        }
                    }
                }
                #endregion

                #region Ticket Email CC Table
                var _ObjTicketCc = context.TS_TicketCC.Where(c => c.IsActive == true && c.TicketMasterId == TicketMasterId).ToList();
                _ObjTicketCc.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                context.SaveChanges();
                if (Ticket_EmailCC != "")
                {
                    string[] EmailCC = Ticket_EmailCC.Split(';');
                    for (int i = 0; i < EmailCC.Length; i++)
                    {
                        string EmailAddress = EmailCC[i].ToString();
                        if (EmailAddress != "")
                        {
                            var User = context.UserLogins.FirstOrDefault(a => a.Login_ID == EmailAddress);
                            DAL.TS_TicketCC objTicketCC = new DAL.TS_TicketCC();
                            objTicketCC.TicketMasterId = TicketMasterId;
                            objTicketCC.AssigneeId = User == null ? NULLint : User.User_Code;
                            objTicketCC.EmailId = EmailAddress;
                            objTicketCC.CreatedBy = baseclass.UserId;
                            objTicketCC.CreatedDate = dt;
                            objTicketCC.IsActive = true;
                            objTicketCC.UserIP = baseclass.UserIP;
                            context.TS_TicketCC.Add(objTicketCC);
                            context.SaveChanges();
                        }
                    }
                }
                #endregion

                Status = true;
                _return = updateTicketMaster.TicketCode;
            }


            if (Status == true)
            {
                //_return = "updated";
                // FOR EMAIL DEPARTMENT
                // SendEmailToDepartment(TicketMasterId);
                //GenerateEmailForCustomer(TicketMasterId, Ticket_EmailTo, Ticket_EmailCC);
                // FOR EMAIL Assignee
                //if (Convert.ToInt32(AssigneeId) > 0)
                //    GenerateEmailforAssignee(TicketMasterId);
            }

        }
        */
        #endregion

        return _return;
    }

    [OperationContract]
    public string UpdateTicketAndCreateCustomer(string hfCustomerId, int TicketMasterId, string Customer, string TicketMaster, string Ticket_EmailTo, string Ticket_EmailCC, string FilePath, int hfStatusId, string TicketDetailData)
    {
        List<string> _return = new List<string>();
        if (CheckIsStatusNotChanged(TicketMasterId, hfStatusId))
        {
            int CustomerId;
            //string _return = "";

            #region Customer Table
            if (hfCustomerId == "0")
            {
                DAL.Customer obj_Customer = (DAL.Customer)Common.Deserialize(Customer, typeof(DAL.Customer));
                obj_Customer.CreatedDate = DateTime.Now;
                obj_Customer.CreatedBy = baseclass.UserId;
                obj_Customer.IsActive = true;
                obj_Customer.UserIp = baseclass.UserIP;
                context.Customers.Add(obj_Customer);
                context.SaveChanges();
                CustomerId = obj_Customer.CustomerId;
            }
            else
            {
     

                CustomerId = int.Parse(hfCustomerId);
                DAL.Customer updateCustomer = context.Customers.FirstOrDefault(x => x.CustomerId == CustomerId);
                DAL.Customer obj_Customer = (DAL.Customer)Common.Deserialize(Customer, typeof(DAL.Customer));
                updateCustomer.ModifiedDate = DateTime.Now;
                updateCustomer.ModifiedBy = baseclass.UserId;
                updateCustomer.IsActive = true;
                updateCustomer.UserIp = baseclass.UserIP;
                updateCustomer.CustomerName = obj_Customer.CustomerName;
                updateCustomer.Address = obj_Customer.Address;
                updateCustomer.EmailAddress = obj_Customer.EmailAddress;
                updateCustomer.CityId = obj_Customer.CityId;
                updateCustomer.ContactNo = obj_Customer.ContactNo;
                updateCustomer.AlternativeNumber = obj_Customer.AlternativeNumber;
                context.SaveChanges();

            }
            #endregion
            #region

            DAL.TS_TicketMaster obj_TicketMaster = (DAL.TS_TicketMaster)Common.Deserialize(TicketMaster, typeof(DAL.TS_TicketMaster));
            if (obj_TicketMaster != null)
            {
                #region Variables
                DateTime dt = DateTime.Now;
                bool Status = false;
                int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
                int? NULLint = null;
                int? Category = obj_TicketMaster.RequestTypeCategoryId;
                int? SubCategory = obj_TicketMaster.RequestTypeSubCategoryId;
                int? InitiatorId = obj_TicketMaster.InitiatorId;
                int DepartmentId = obj_TicketMaster.DepartmentId;
                int RequestTypeId = obj_TicketMaster.RequestTypeId;
                int PriorityId = obj_TicketMaster.PriorityId;
                int RequestModeId = obj_TicketMaster.RequestModeId;
                int? AssigneeId = obj_TicketMaster.AssigneeId;
                int StatusId = Convert.ToInt32(obj_TicketMaster.StatusId);
                string Description = "", Description_html = "",Title = "";
                Description = obj_TicketMaster.Description;
                Description_html = obj_TicketMaster.Description;
                TimeSpan time = DateTime.Now.TimeOfDay;
                Title = obj_TicketMaster.Tittle;
                #endregion

                #region Update TicketMaster Table
                DAL.TS_TicketMaster updateTicketMaster = context.TS_TicketMaster.FirstOrDefault(x => x.TicketMasterId == TicketMasterId);

                updateTicketMaster.InitiatorId = InitiatorId;
                updateTicketMaster.DepartmentId = DepartmentId;
                updateTicketMaster.RequestTypeId = RequestTypeId;
                updateTicketMaster.CustomerId = CustomerId;
                updateTicketMaster.RequestTypeCategoryId = Category;
                updateTicketMaster.RequestTypeSubCategoryId = SubCategory;
                updateTicketMaster.Tittle = Title;
                updateTicketMaster.PriorityId = PriorityId;
                updateTicketMaster.RequestModeId = RequestModeId;
                updateTicketMaster.AssigneeId = AssigneeId;
                updateTicketMaster.Description = Description == null ? "" : Description;
                updateTicketMaster.Description_html = Description_html == null ? "" : Description_html;
                updateTicketMaster.ModifiedDate = dt;
                updateTicketMaster.ModifiedDateInt = IntDate;
                updateTicketMaster.ModifiedBy = baseclass.UserId;
                updateTicketMaster.IsActive = true;
                updateTicketMaster.StatusId = StatusId;
                updateTicketMaster.UserIP = baseclass.UserIP;
                context.SaveChanges();
                if (FilePath != "")
                    GetSaveAtachment(TicketMasterId, 0, FilePath);



                #endregion


                if (TicketMasterId > 0)
                {
                    #region Ticket Detail Table

                    DAL.TS_TicketDetail objTicketDetail = new DAL.TS_TicketDetail();
                    objTicketDetail.TicketMasterId = TicketMasterId;
                    objTicketDetail.AssigneeId = obj_TicketMaster.AssigneeId;
                    objTicketDetail.StatusId = obj_TicketMaster.StatusId;
                    objTicketDetail.DepartmentId = DepartmentId;
                    objTicketDetail.Description = Description;
                    objTicketDetail.Description_html = Description_html;
                    objTicketDetail.CreatedBy = baseclass.UserId;
                    objTicketDetail.CreatedDateInt = IntDate;
                    objTicketDetail.CreatedDate = dt;
                    objTicketDetail.IsActive = true;
                    objTicketDetail.UserIP = baseclass.UserIP;
                    context.TS_TicketDetail.Add(objTicketDetail);
                    context.SaveChanges();
                    #endregion

                    #region Ticket EmailTo Table

                    var _ObjTicketTo = context.TS_TicketTo.Where(c => c.IsActive == true && c.TicketMasterId == TicketMasterId).ToList();
                    _ObjTicketTo.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                    context.SaveChanges();

                    if (Ticket_EmailTo != "")
                    {
                        string[] EmailTo = Ticket_EmailTo.Split(';');
                        for (int i = 0; i < EmailTo.Length; i++)
                        {
                            string EmailAddress = EmailTo[i].ToString();
                            if (EmailAddress != "")
                            {
                                DAL.TS_TicketTo objTicketTo = new DAL.TS_TicketTo();
                                objTicketTo.TicketMasterId = TicketMasterId;
                                objTicketTo.EmailId = EmailAddress;
                                objTicketTo.CreatedBy = baseclass.UserId;
                                objTicketTo.CreatedDate = dt;
                                objTicketTo.IsActive = true;
                                objTicketTo.UserIP = baseclass.UserIP;
                                context.TS_TicketTo.Add(objTicketTo);
                                context.SaveChanges();
                            }
                        }
                    }
                    #endregion

                    #region Ticket Email CC Table
                    var _ObjTicketCc = context.TS_TicketCC.Where(c => c.IsActive == true && c.TicketMasterId == TicketMasterId).ToList();
                    _ObjTicketCc.ForEach(a => { a.IsActive = false; a.ModifiedDate = dt; a.ModifiedBy = baseclass.UserId; });
                    context.SaveChanges();
                    if (Ticket_EmailCC != "")
                    {
                        string[] EmailCC = Ticket_EmailCC.Split(';');
                        for (int i = 0; i < EmailCC.Length; i++)
                        {
                            string EmailAddress = EmailCC[i].ToString();
                            if (EmailAddress != "")
                            {
                                var User = context.UserLogins.FirstOrDefault(a => a.Login_ID == EmailAddress);
                                DAL.TS_TicketCC objTicketCC = new DAL.TS_TicketCC();
                                objTicketCC.TicketMasterId = TicketMasterId;
                                objTicketCC.AssigneeId = User == null ? NULLint : User.User_Code;
                                objTicketCC.EmailId = EmailAddress;
                                objTicketCC.CreatedBy = baseclass.UserId;
                                objTicketCC.CreatedDate = dt;
                                objTicketCC.IsActive = true;
                                objTicketCC.UserIP = baseclass.UserIP;
                                context.TS_TicketCC.Add(objTicketCC);
                                context.SaveChanges();
                            }
                        }
                    }
                    #endregion



                    JavaScriptSerializer objJavascriptSerializer = new JavaScriptSerializer();
                    Dictionary<string, object> objResponse = new Dictionary<string, object>();
                    var dataTicket = objJavascriptSerializer.DeserializeObject(TicketDetailData);
                    List<Constant.TicketDetail> cM_TicketDetails = JsonConvert.DeserializeObject<List<Constant.TicketDetail>>(TicketDetailData);
                    DataTable dtTicketDetail = Common.ToDataTable(cM_TicketDetails);
                    //   string dbConnectionString = context.Database.Connection.ConnectionString;
                    // SqlConnection con = new SqlConnection(dbConnectionString);
                    DataTable dtSubcategoryFieldValue = new DataTable();


                    DataTable ds = new DataTable();
                    DAL.TicketSystemEntities context1 = new DAL.TicketSystemEntities();
                    string dbConnectionString = context1.Database.Connection.ConnectionString;
                    SqlConnection con = new SqlConnection(dbConnectionString);
                    SqlDataAdapter da = new SqlDataAdapter("Setup_RequestTypeSubcategoryFieldValue_Insert", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@TicketMasterId", SqlDbType.Int).Value = TicketMasterId;
                    da.SelectCommand.Parameters.Add("@TicketSubCategoryDetailData", SqlDbType.Structured).Value = dtTicketDetail;
                    da.SelectCommand.Parameters.Add("@IsUpdate", SqlDbType.Bit).Value = true;
                    da.SelectCommand.Parameters.Add("@userid", SqlDbType.Int).Value = baseclass.UserId;
                    da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.NVarChar).Value = baseclass.UserIP;
                    da.Fill(ds);

                    Status = true;
                    _return.Add(updateTicketMaster.TicketCode);
                }


                if (Status == true)
                {
                    //if (hfCustomerId == "0")
                    if (hfStatusId == (int)Constant.TicketStatus.New)
                    {
                        // FOR EMAIL DEPARTMENT
                        SendEmailToDepartment(TicketMasterId);
                        GenerateEmailForCustomer(TicketMasterId, Ticket_EmailTo, Ticket_EmailCC);
                        // FOR EMAIL Assignee
                        if (Convert.ToInt32(AssigneeId) > 0)
                            GenerateEmailforAssignee(TicketMasterId);
                    }
                }

            }

            #endregion
            var JSON = JsonConvert.SerializeObject(_return);
            return JSON;
        }
        else
        {
            var data = context.TS_TicketMaster.FirstOrDefault(x => x.IsActive == true && x.TicketMasterId == TicketMasterId);
            _return.Add(data.TicketCode);
            _return.Add(data.TS_Setup_TicketStatus.Status);
            var JSON = JsonConvert.SerializeObject(_return);
            return JSON;
        }


    }

    private bool CheckIsStatusNotChanged(int TicketMasterId, int hfStatusId)
    {
        try
        {
            var data = context.TS_TicketMaster.Where(x => x.TicketMasterId == TicketMasterId && x.StatusId == hfStatusId && x.IsActive == true).FirstOrDefault();

            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    private void GenerateEmailforAssignee(int TicketMasterId)
    {
        try
        {
            var ListSelectedTicket = context.TS_TicketMaster.FirstOrDefault(c => c.IsActive == true && c.TicketMasterId == TicketMasterId);
            if (ListSelectedTicket != null)
            {
                if (ListSelectedTicket.AssigneeId != null)
                {
                    string To = "", CC = "", Subject = "", Msg = "";
                    int InitiatorId = Convert.ToInt32(ListSelectedTicket.InitiatorId);
                    int? AssigneeId = ListSelectedTicket.AssigneeId;
                    string InitiatorName = context.UserLogins.FirstOrDefault(a => a.User_Code == InitiatorId).Full_Name;
                    string AssigneeName = context.UserLogins.FirstOrDefault(a => a.User_Code == AssigneeId).Full_Name;
                    To = context.UserLogins.FirstOrDefault(a => a.User_Code == AssigneeId).Login_ID;
                    Subject = "Ticket No [" + ListSelectedTicket.TicketCode + "] " + ListSelectedTicket.Tittle;
                    Msg = "Dear " + AssigneeName + ","
                        + "<br /><br />"
                        + "A ticket has been initiated by " + InitiatorName + " on " + ListSelectedTicket.CreatedDate.ToString(Constant.DateFormat) + ". Kindly review and respond accordingly. "
                       + "<br /><br /> <p class='MsoNormal'><b><span style='font-size:9.0pt;color:gray'>Thanks & Regards,</span></b></p> " + (string)Constant.EmailFooter + " ";
                    Email.SendMail(To, Subject, Msg, CC, "");
                }
            }
        }
        catch
        { }
    }

    //GenerateEmailForCustomer
    private void GenerateEmailForCustomer(int TicketMasterId, string To, string CC)
    {
        try
        {
            var ListSelectedTicket = context.TS_TicketMaster.FirstOrDefault(c => c.IsActive == true && c.TicketMasterId == TicketMasterId);
            var Category = context.TS_Setup_RequestTypeCategory.FirstOrDefault(x => x.IsActive == true && x.CategoryId == ListSelectedTicket.RequestTypeCategoryId);
            var Subcategory = context.TS_Setup_RequestTypeSubcategory.FirstOrDefault(x => x.IsActive == true && x.SubcategoryId == ListSelectedTicket.RequestTypeSubCategoryId);
            if (ListSelectedTicket != null)
            {
                string Subject = "Ticket No [" + ListSelectedTicket.TicketCode + "] : " + Category.CategoryName + " : " + Subcategory.SubcategoryName;
                Subject = Subject.Replace('\r', ' ');
                Subject = Subject.Replace('\n', ' ');
                string Msg = "Dear Valued Customer,"
                     + "<br /><br />"
                     + "Thank you for contacting Pakistan Single Window - PSW. Please note that the reported complaint regarding " + Subcategory.SubcategoryName + " in " + Category.CategoryName + " has been logged, reference ticket number is provided in"
                     + " subject. We apologize for any inconvenience and appreciate your patience while we are investigating the issue and will get back to you shortly."
                     + "<br /><br /> <p class='MsoNormal'><b><span style='font-size:9.0pt;color:gray'>Thanks & Regards,</span></b></p> " + (string)Constant.EmailFooter + " ";

                // Addition of Junk email context starts
                var Li_TS_TicketDetail_Junk = context.TS_TicketDetail.FirstOrDefault(a => a.IsActive == true && a.TicketMasterId == TicketMasterId && a.StatusId == (int)Constant.TicketStatus.New);
                if (Li_TS_TicketDetail_Junk != null)
                {
                    var li_TS_TicketTo = context.TS_TicketTo.Where(a => a.IsActive == false && a.TicketMasterId == TicketMasterId).ToList();
                    var li_TS_TicketCC = context.TS_TicketCC.Where(a => a.IsActive == false && a.TicketMasterId == TicketMasterId).ToList();
                    string To_Junk = "";
                    for (int i = 0; i < li_TS_TicketTo.Count; i++)
                    {
                        if (i == li_TS_TicketTo.Count - 1)
                        {
                            To_Junk += li_TS_TicketTo[i].EmailId;
                        }
                        else
                        {
                            To_Junk += li_TS_TicketTo[i].EmailId + "; ";
                        }
                    }
                    string CC_Junk = "";
                    for (int i = 0; i < li_TS_TicketCC.Count; i++)
                    {
                        if (i == li_TS_TicketCC.Count - 1)
                        {
                            CC_Junk += li_TS_TicketCC[i].EmailId;
                        }
                        else
                        {
                            CC_Junk += li_TS_TicketCC[i].EmailId + "; ";
                        }
                    }

                    string EmailHeader_ = "<p class=MsoNormal> <b><span style='font-size:11.0pt;font-family:'Calibri',sans-serif'>From:</span></b><span style='font-size:11.0pt;font-family:'Calibri',sans-serif'> " + Li_TS_TicketDetail_Junk.EmailFrom + " <br> <b>Sent:</b> " + string.Format("{0:f}", Li_TS_TicketDetail_Junk.CreatedDate) + "<br> <b>To:</b> " + To_Junk + "<br> " + (CC_Junk != "" ? "<b>Cc:</b> " + CC_Junk + "<br>" : "") + " <b>Subject:</b> " + Subject + "<o:p></o:p></span></p> ";
                    Msg += "<br /> <hr />" + EmailHeader_ + Li_TS_TicketDetail_Junk.Description;
                }
                // Addition of Junk email context ends

                string SenderEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SenderEmailAddress"];
                string EmailHeader = "<p class=MsoNormal> <b><span style='font-size:11.0pt;font-family:'Calibri',sans-serif'>From:</span></b><span style='font-size:11.0pt;font-family:'Calibri',sans-serif'> " + SenderEmailAddress + " <br> <b>Sent:</b> " + string.Format("{0:f}", DateTime.Now) + "<br> <b>To:</b> " + To + "<br> <b>Subject:</b> " + Subject + "<o:p></o:p></span></p> ";
                ListSelectedTicket.Description_html = EmailHeader + Msg;
                ListSelectedTicket.TS_TicketDetail.FirstOrDefault().Description_html = EmailHeader + Msg;
                context.SaveChanges();
                Email.SendMail(To, Subject, Msg, CC, "");
            }
        }
        catch
        { }
    }

    private void SendEmailToDepartment(int TicketMasterId)
    {

        var ListSelectedTicket = context.TS_TicketMaster.FirstOrDefault(c => c.IsActive == true && c.TicketMasterId == TicketMasterId);
        if (ListSelectedTicket != null)
        {
            int DepartmentId = ListSelectedTicket.DepartmentId;
            if (DepartmentId > 0)
            {
                var ListDpt = context.Setup_Department.FirstOrDefault(a => a.DepartmentId == DepartmentId);
                if (ListDpt != null)
                {
                    string DepartmentEmailAddress = ListDpt.Email == null ? "" : ListDpt.Email;
                    if (DepartmentEmailAddress != "")
                    {
                        string subject = "Ticket No [" + ListSelectedTicket.TicketCode + "] " + ListSelectedTicket.Tittle;
                        string emailtemplatebody = Email.GetTemplateString(Convert.ToInt32(Constant.EmailTemplates.Department));
                        Dictionary<string, string> urlParameter = new Dictionary<string, string>();
                        DataTable DTRequesterInfo = Email.GetRequesterInfo(baseclass.UserId);
                        DataTable DTRequestDetailInfo = Email.GetTicketInfo(TicketMasterId);
                        string RequesterInfoHTML = Email.GetRequesterInfoHtml(DTRequesterInfo);
                        string RequestDetailInfoHTML = Email.GetRequestDetailHtml(DTRequestDetailInfo);
                        string LinkHTML = "";
                        emailtemplatebody = Email.ReplaceEmailBodySection(emailtemplatebody, "PSW Helpdesk", RequesterInfoHTML, RequestDetailInfoHTML, LinkHTML);
                        emailtemplatebody = emailtemplatebody
                        + "<br /><br /> <p class='MsoNormal'><b><span style='font-size:9.0pt;color:gray'>Thanks & Regards,</span></b></p> " + (string)Constant.EmailFooter + " ";
                        Email.SendMail(DepartmentEmailAddress, subject, emailtemplatebody, "", "");
                    }
                }
            }
        }
    }

    private void GenerateInitialFindingsEmail(int TicketMasterId, string Description)
    {
        try
        {
            string TicketNo = "";
            var ListSelectedTicket = context.TS_TicketMaster.FirstOrDefault(c => c.IsActive == true && c.TicketMasterId == TicketMasterId);
            if (ListSelectedTicket != null)
            {
                TicketNo = ListSelectedTicket.TicketCode;
            }
            string Subject = "Ticket Code: " + TicketNo + " Initial Finding";
            string Msg = "<h4>Initial Findings:</h4>"+Description;
            string To = "l2support@psw.gov.pk";
            string CC = "";

            Email.SendMail(To, Subject, Msg, CC, "");
        }
        catch
        { }

    }

    [OperationContract]
    public string TestEmail(int TicketMasterId, string Ticket_EmailTo, string Ticket_EmailCC)
    {
        GenerateEmailForCustomer(TicketMasterId, Ticket_EmailTo, Ticket_EmailCC);
        SendEmailToDepartment(TicketMasterId);
        GenerateEmailforAssignee(TicketMasterId);
        return "";
    }

    [OperationContract]
    public string getTicketMaster(int MasterId)
    {
        var List = context.TS_TicketMaster.Where(x => x.IsActive == true && x.TicketMasterId == MasterId).Select(s => new
        {
            TicketMasterId = s.TicketMasterId,
            TicketCode = s.TicketCode,
            ParentTicketMasterId = s.ParentTicketMasterId,
            InitiatorId = s.InitiatorId,
            AssigneeId = s.AssigneeId,
            DepartmentId = s.DepartmentId,
            PriorityId = s.PriorityId,
            StatusId = s.StatusId,
            RequestModeId = s.RequestModeId,
            RequestTypeId = s.RequestTypeId,
            IsAssigned = s.IsAssigned,
            Tittle = s.Tittle,
            Description = s.Description,
            LevelId = s.LevelId,
            TicketTypeId = s.TicketTypeId,
            IsFlagMark = s.IsFlagMark,

        }).ToList();

        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getTicketTo(int MasterId)
    {
        string Return = "";
        var List = context.TS_TicketTo.Where(x => x.IsActive == true && x.TicketMasterId == MasterId).Select(s => new
        {
            TicketMasterId = s.TicketMasterId,
            Email = s.EmailId,
            TicketToId = s.TicketToId,
        }).OrderBy(a => a.TicketToId).ToList();

        if (List != null && List.Count > 0)
        {
            for (int i = 0; i < List.Count; i++)
            {
                Return += List[i].Email.Trim() + ";";
            }
        }

        var JSON = JsonConvert.SerializeObject(Return);
        return JSON;
    }

    [OperationContract]
    public string getTicketCC(int MasterId, int Current_StatusId = 0, bool OnEdit = false)
    {
        string Return = "";
        bool Has_SupportEmailAddress = false;
        string SenderEmailAddress = ConfigurationManager.AppSettings["SenderEmailAddress"];

        var List = context.TS_TicketCC.Where(x => x.IsActive == true && x.TicketMasterId == MasterId).Select(s => new
        {
            TicketMasterId = s.TicketMasterId,
            Email = s.EmailId,
            TicketToId = s.TicketToId,
        }).OrderBy(a => a.TicketToId).ToList();
        if (List != null && List.Count > 0)
        {
            for (int i = 0; i < List.Count; i++)
            {
                Return += List[i].Email.Trim() + ";";

                if (Current_StatusId == (int)Constant.TicketStatus.New && OnEdit == true && Has_SupportEmailAddress == false)
                {
                    if (List[i].Email != null)
                    {
                        if (List[i].Email.Trim() != "")
                        {
                            if (List[i].Email.Trim() == SenderEmailAddress)
                            {
                                Has_SupportEmailAddress = true;
                            }
                        }
                    }
                }
            }
        }

        if (Current_StatusId == (int)Constant.TicketStatus.New && OnEdit == true && Has_SupportEmailAddress == false)
        {
            if (SenderEmailAddress != "")
            {
                Return += SenderEmailAddress + ";";
            }
        }

        var JSON = JsonConvert.SerializeObject(Return);
        return JSON;
    }

    [OperationContract]
    public string getTaskMaster(int MasterId)
    {
        DataTable dtList = context.TS_TaskMaster.Where(x => x.IsActive == true && x.TaskMasterId == MasterId).Select(s => new
        {
            TicketMasterId = s.TicketMasterId,

            MasterID = s.TaskMasterId,
            InitiatorId = s.InitiatorId,
            AssigneeId = s.AssigneeId,
            DepartmentId = s.DepartmentId,
            PriorityId = s.PriorityId,
            StatusId = s.StatusId,
            Initiator = s.UserLogin1.Full_Name,
            Date = s.CreatedDate,
            Time = s.CreatedDate,
            Date_ = "",
            Time_ = "",
            Status = s.TS_Setup_TicketStatus.Status,
            Priority = s.TS_Setup_Priority.PriorityName,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            Assignee = s.UserLogin.Full_Name,
            Tittle = s.TaskTitle,
            Description = s.Description,
            TicketCode = s.TS_TicketMaster.TicketCode,
            Department = s.Setup_Department.DepartmentName,
            StartDate_ = "",
            EndDate_ = "",

        }).ToList().ToDataTable();
        if (dtList != null && dtList.Rows.Count > 0)
        {

            dtList.Rows[0]["Date_"] = Convert.ToDateTime(dtList.Rows[0]["Date"].ToString()).ToString(Constant.DateFormat1);
            dtList.Rows[0]["Time_"] = Convert.ToDateTime(dtList.Rows[0]["Time"].ToString()).ToString(Constant.TimeFormatAMPM);
            dtList.Rows[0]["StartDate_"] = Convert.ToDateTime(dtList.Rows[0]["StartDate"].ToString()).ToString(Constant.DateFormat1);
            dtList.Rows[0]["EndDate_"] = Convert.ToDateTime(dtList.Rows[0]["EndDate"].ToString()).ToString(Constant.DateFormat1);
        }


        var JSON = JsonConvert.SerializeObject(dtList);
        return JSON;
    }

    [OperationContract]
    public string IsRead_Update(int MasterId, string ReplyResponse_, int ChatTypeId, int AssigneeId, int InitiatorId)
    {
        var JSON = "False";
        int? NullInt = null;
        int? TicketMasterId = ChatTypeId == (int)Constant.ChatType.Ticket_Reply_Response ? MasterId : NullInt;
        int? TaskMasterId = ChatTypeId == (int)Constant.ChatType.Task_Reply_Response ? MasterId : NullInt;
        if (AssigneeId == baseclass.UserId || InitiatorId == baseclass.UserId)
        {
            bool ReplyResponse = Convert.ToBoolean(ReplyResponse_);
            var objTicketLog = context.TS_ChatLog.Where(c => c.IsActive == true && c.IsRead == false && c.IsReply == (ReplyResponse == true ? false : true) && (c.TicketMasterId == TicketMasterId || c.TaskMasterId == TaskMasterId)).ToList();
            objTicketLog.ForEach(a => { a.IsRead = true; });
            context.SaveChanges();
            JSON = "True";
        }
        return JSON;
    }

    private string SendEmail_On_First_ReplyToCustomer(int TicketMasterId, int ChatLogId)
    {
        string emailtemplatebody = "";
        int Count = context.TS_ChatLog.Where(a => a.IsActive == true && a.ChatLogId != ChatLogId && a.TicketMasterId == TicketMasterId && a.ChatTypeId == (int)Constant.ChatType.Ticket_Reply_Response && a.CreatedBy != null).Count();
        if (Count == 0)
        {
            var ListSelectedTicket = context.TS_TicketMaster.FirstOrDefault(c => c.IsActive == true && c.TicketMasterId == TicketMasterId);
            if (ListSelectedTicket != null)
            {

                emailtemplatebody = Email.GetTemplateString(Convert.ToInt32(Constant.EmailTemplates.Customer));
                DataTable DTRequestDetailInfo = Email.GetTicketInfoForCustomerFirstReply(TicketMasterId);
                string RequestDetailInfoHTML = Email.GetRequestDetailHtml(DTRequestDetailInfo);
                string LinkHTML = "";
                emailtemplatebody = Email.ReplaceEmailBodySection(emailtemplatebody, "PSW Helpdesk", "", RequestDetailInfoHTML, LinkHTML);
            }
        }
        return emailtemplatebody;
    }

    [OperationContract]
    public string Get_Reply_Response(int MasterId, int ChatTypeId)
    {
        var ListResponse = context.TS_GET_CHAT_HISTORYDETAIL(baseclass.UserId, MasterId, ChatTypeId).ToList();
        if (ListResponse.Count > 0)
        {
            var JSON = JsonConvert.SerializeObject(ListResponse);
            return JSON;
        }
        else
        {
            return "";
        }
    }

    //IsSaveReply_Response
    [OperationContract]
    public string IsSaveReply_Response(int MasterId, string Description, int ChatTypeId, string FilePath)
    {

        string Return = "";
        int? IntNull = null;
        DateTime dt = DateTime.Now;
        int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
        int? StatusId = IntNull;
        int? AssigneeId = IntNull;
        int? TicketMasterId = IntNull;
        int? TaskMasterId = IntNull;
        int? RequestmodeId = IntNull;
        string Title = "";
        string FileServerPath = "";
        string FilePathLatest = "";

        if (FilePath != "")
        {
            string[] Files = FilePath.Split(',');
            //FilePath = "/Uploads/" + Files[0];
            //FileServerPath = HttpContext.Current.Server.MapPath(FilePath);

            for (int i = 0; i < Files.Length; i++)
            {
                FilePathLatest += "/Uploads/" + Files[i] + ",";
                FileServerPath += HttpContext.Current.Server.MapPath("/Uploads/" + Files[i]) + ",";
            }

            FilePathLatest = FilePathLatest.Remove(FilePathLatest.Length - 1, 1);
            FileServerPath = FileServerPath.Remove(FileServerPath.Length - 1, 1);
        }


        if (ChatTypeId == (int)Constant.ChatType.Ticket_Reply_Response || ChatTypeId == (int)Constant.ChatType.Ticket_Internel_Chat)
        {
            TicketMasterId = MasterId;
            var ListTicketMaster = context.TS_TicketMaster.FirstOrDefault(c => c.IsActive == true && c.TicketMasterId == TicketMasterId);
            StatusId = ListTicketMaster == null ? IntNull : ListTicketMaster.StatusId;
            AssigneeId = ListTicketMaster == null ? IntNull : ListTicketMaster.AssigneeId;
            Title = "Ticket No [" + ListTicketMaster.TicketCode + "] " + ListTicketMaster.Tittle;
        }
        else if (ChatTypeId == (int)Constant.ChatType.Task_Reply_Response)
        {
            TaskMasterId = MasterId;
            var ListTicketMaster = context.TS_TaskMaster.FirstOrDefault(c => c.IsActive == true && c.TaskMasterId == TaskMasterId);
            StatusId = ListTicketMaster == null ? IntNull : ListTicketMaster.StatusId;
            AssigneeId = ListTicketMaster == null ? IntNull : ListTicketMaster.AssigneeId;
        }
        DAL.TS_ChatLog objlog = new DAL.TS_ChatLog();
        objlog.ChatTypeId = ChatTypeId;
        objlog.FilePath = FilePathLatest;
        objlog.Description = Description;
        objlog.Description_html = Description;
        objlog.EmailId = baseclass.EmailAddress;
        objlog.TicketMasterId = TicketMasterId;
        objlog.TaskMasterId = TaskMasterId;
        objlog.StatusId = StatusId;
        objlog.AssigneeId = AssigneeId;
        objlog.CreatedBy = baseclass.UserId;
        objlog.CreatedDateInt = IntDate;
        objlog.CreatedDate = dt;
        objlog.IsActive = true;
        objlog.UserIP = baseclass.UserIP;
        context.TS_ChatLog.Add(objlog);
        context.SaveChanges();
        if (objlog.ChatLogId > 0)
        {
            int Last_ChatLogId = objlog.ChatLogId;

            Return = Get_Reply_Response(MasterId, ChatTypeId);
            if (ChatTypeId == (int)Constant.ChatType.Ticket_Reply_Response)
            {
                string To = "";
                var List = context.TS_TicketTo.Where(x => x.IsActive == true && x.TicketMasterId == MasterId).Select(s => new
                {
                    TicketMasterId = s.TicketMasterId,
                    Email = s.EmailId,
                    TicketToId = s.TicketToId,
                }).OrderBy(a => a.TicketToId).ToList();

                if (List != null && List.Count > 0)
                {
                    for (int i = 0; i < List.Count; i++)
                    {
                        To += List[i].Email.Trim() + ";";
                    }
                }
                string CC = "";
                var ListCC = context.TS_TicketCC.Where(x => x.IsActive == true && x.TicketMasterId == MasterId).Select(s => new
                {
                    TicketMasterId = s.TicketMasterId,
                    Email = s.EmailId,
                    TicketToId = s.TicketToId,
                }).OrderBy(a => a.TicketToId).ToList();
                if (ListCC != null && ListCC.Count > 0)
                {
                    for (int i = 0; i < ListCC.Count; i++)
                    {
                        CC += ListCC[i].Email.Trim() + ";";
                    }
                }

                if (To != "")
                {
                    string LastHTML_Binding = "";

                    var List_TS_ChatLog = context.TS_ChatLog.Where(a => a.IsActive == true && a.TicketMasterId == MasterId && a.ChatLogId != Last_ChatLogId).OrderByDescending(a => a.ChatLogId).ToList();
                    if (List_TS_ChatLog != null && List_TS_ChatLog.Count > 0)
                    {
                        LastHTML_Binding = List_TS_ChatLog[0].Description_html;
                    }
                    else
                    {
                        var ListTicketMaster = context.TS_TicketMaster.FirstOrDefault(c => c.IsActive == true && c.TicketMasterId == TicketMasterId);
                        LastHTML_Binding = ListTicketMaster.Description_html;
                    }

                    string Regards = (string)Constant.EmailFooter;
                    string EmailTemplete = "";
                    if (baseclass.CustomerId == 0)
                    {
                        EmailTemplete = SendEmail_On_First_ReplyToCustomer(MasterId, objlog.ChatLogId);
                        EmailTemplete = EmailTemplete == "" ? "" : EmailTemplete + "<br /><br />";
                    }
                    else if (baseclass.CustomerId > 0)
                    {
                        Regards = baseclass.FullName;
                    }
                    string Subject = Title;
                    string Msg = EmailTemplete
                    + Description
                    + "<br /><br /> <p class='MsoNormal'><b><span style='font-size:9.0pt;color:gray'>Thanks & Regards,</span></b></p> " + Regards + "" + "<br /> <hr />" + LastHTML_Binding;


                    var li = context.TS_ChatLog.Where(a => a.ChatLogId == Last_ChatLogId).ToList();
                    li[0].Description_html = Msg;
                    context.SaveChanges();

                    Email.SendMail(To, "RE: " + Subject, Msg, CC, FileServerPath);
                }
            }
        }
        return Return;
    }

    [OperationContract]
    public string GetSaveAtachment(int MasterId, int TempId, string filename)
    {
        int Id = MasterId > 0 ? MasterId : TempId;

        if (filename != "")
        {
            string[] File = filename.Split(',');
            DAL.TS_TicketAttachments objTS_TicketAttachments = new DAL.TS_TicketAttachments();
            objTS_TicketAttachments.TargetId = Id;
            objTS_TicketAttachments.Filename = File[0].ToString();
            objTS_TicketAttachments.FileOriginalName = File[1].ToString();
            objTS_TicketAttachments.Filetype = File[2].ToString();
            objTS_TicketAttachments.IsActive = true;
            context.TS_TicketAttachments.Add(objTS_TicketAttachments);
            context.SaveChanges();
        }

        var List = context.TS_TicketAttachments.Where(a => a.IsActive == true && a.TargetId == Id).Select(s => new
        {
            FileId = s.FileId,
            TicketMasterId = s.TargetId,
            FileOriginalName = s.FileOriginalName,
            Filename = s.Filename,
            Filetype = s.Filetype,
            FilePath = s.Filename.Contains("Uploads") ? s.Filename : "/Uploads/" + s.Filename,
        }).ToList();

        var JSON = JsonConvert.SerializeObject(List);

        return JSON;
    }


    [OperationContract]
    public string DeleteAtachment(int FileId)
    {
        var List = context.TS_TicketAttachments.Where(c => c.IsActive == true && c.FileId == FileId).ToList();
        List.ForEach(a => { a.IsActive = false; });
        context.SaveChanges();
        var JSON = "1";
        return JSON;
    }

    [OperationContract]
    public string DeleteAtachmentByTargetID(int TargetId)
    {
        var List = context.TS_TicketAttachments.Where(c => c.IsActive == true && c.TargetId == TargetId).ToList();
        List.ForEach(a => { a.IsActive = false; });
        context.SaveChanges();
        var JSON = "1";
        return JSON;
    }

    [OperationContract]
    public string DeleteTaskAttachments(int FileId)
    {
        var List = context.TS_TaskAttachments.Where(c => c.IsActive == true && c.FileId == FileId).ToList();
        List.ForEach(a => { a.IsActive = false; });
        context.SaveChanges();
        var JSON = "1";
        return JSON;
    }

    [OperationContract]
    public string GetStatus(int StatusId, bool? IsInitiator)
    {
        var JSON = "";
        #region OldCode
        //if (IsInitiator != null)
        //{
        //    var List = context.TS_StatusDetail.Where(e => e.IsActive == true
        //               && (e.IsInitiator == IsInitiator || IsInitiator == null)
        //                && (e.ParentId == StatusId)
        //           ).Select(c => new
        //           {
        //               Id = c.ChildId,
        //               Value = c.TS_Setup_TicketStatus.Status
        //           }).ToList();
        //    JSON = JsonConvert.SerializeObject(List);
        //}
        //else
        //{
        //    if (StatusId == (int)Constant.TicketStatus.Junk)
        //    {
        //        var List = context.TS_StatusDetail.Where(e => e.IsActive == true && e.TS_Setup_TicketStatus.StatusId == (int)Constant.TicketStatus.Cancel
        //               ).Select(c => new
        //               {
        //                   Id = c.ChildId,
        //                   Value = c.TS_Setup_TicketStatus.Status
        //               }).Distinct().ToList();

        //        JSON = JsonConvert.SerializeObject(List);
        //    }
        //    else if (StatusId == (int)Constant.TicketStatus.Resolved)
        //    {
        //        var List = context.TS_StatusDetail.Where(e => e.IsActive == true && e.TS_Setup_TicketStatus.StatusId == (int)Constant.TicketStatus.Closed
        //               ).Select(c => new
        //               {
        //                   Id = c.ChildId,
        //                   Value = c.TS_Setup_TicketStatus.Status
        //               }).Distinct().ToList();

        //        JSON = JsonConvert.SerializeObject(List);
        //    }
        //    else
        //    {
        //        var List = context.TS_StatusDetail.Where(e => e.IsActive == true && e.TS_Setup_TicketStatus.StatusId != (int)Constant.TicketStatus.New
        //              ).Select(c => new
        //              {
        //                  Id = c.ChildId,
        //                  Value = c.TS_Setup_TicketStatus.Status
        //              }).Distinct().ToList();

        //        JSON = JsonConvert.SerializeObject(List);
        //    }
        //} 
        #endregion

        var List = context.TS_Setup_TicketStatus.Where(e => e.IsActive == true)
            .Select(c => new
            {
                Id = c.StatusId,
                Value = c.Status
            }).ToList();
        JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string GetStatusByRole(int RoleCode, bool? IsInitiator)
    {
        var JSON = "";
        var List = (from roleStatus in context.RoleStatusMappings
                    join Stat in context.TS_Setup_TicketStatus on roleStatus.StatusId equals Stat.StatusId
                    where (roleStatus.IsActive == true && roleStatus.Role_Code == RoleCode)
                    orderby Stat.Status
                    select new
                    {
                        Id = Stat.StatusId,
                        Value = Stat.Status,
                    }).ToList();

        JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string GetStatusCancel()
    {
        var JSON = "";

        var List = context.TS_Setup_TicketStatus.Where(e => e.IsActive == true && e.StatusId == (int)Constant.TicketStatus.Cancel)
            .Select(c => new
            {
                Id = c.StatusId,
                Value = c.Status
            }).ToList();
        JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string GetStatusInProgress()
    {
        var JSON = "";

        var List = context.TS_Setup_TicketStatus.Where(e => e.IsActive == true && e.StatusId == (int)Constant.TicketStatus.InProgress)
            .Select(c => new
            {
                Id = c.StatusId,
                Value = c.Status
            }).ToList();
        JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }
    [OperationContract]
    public string UpdateTicketStatusByinitiator(int MasterId, int StatusId, string Desc)
    {
        string ReturnStatus = "0";

        if (MasterId > 0 && StatusId > 0)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DateTime dt = DateTime.Now;
                int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
                TimeSpan time = DateTime.Now.TimeOfDay;
                DAL.TS_TicketMaster objTicketMaster = context.TS_TicketMaster.FirstOrDefault(j => j.TicketMasterId == MasterId);
                objTicketMaster.StatusId = StatusId;
                objTicketMaster.ModifiedBy = baseclass.UserId;
                objTicketMaster.ModifiedDate = dt;
                objTicketMaster.ModifiedDateInt = IntDate;
                objTicketMaster.IsActive = true;
                objTicketMaster.UserIP = baseclass.UserIP;
                context.SaveChanges();
                DAL.TS_TicketDetail objTicketdetail = new DAL.TS_TicketDetail();
                objTicketdetail.Description = Desc.Trim();
                objTicketdetail.Description_html = Desc.Trim();
                objTicketdetail.TicketMasterId = MasterId;
                objTicketdetail.StatusId = StatusId;
                objTicketdetail.CreatedBy = baseclass.UserId;
                objTicketdetail.CreatedDateInt = IntDate;
                objTicketdetail.CreatedDate = dt;
                objTicketdetail.IsActive = true;
                objTicketdetail.UserIP = baseclass.UserIP;
                objTicketdetail.AssigneeId = objTicketMaster.AssigneeId;
                context.TS_TicketDetail.Add(objTicketdetail);
                context.SaveChanges();
                scope.Complete();
                ReturnStatus = "1";
            }
        }

        return ReturnStatus;
    }

    [OperationContract]
    public string getTicketMasterDetails(int MasterId)
    {
        var JSON = "";
        DateTime? NullDatetime = null;

        string connstr = context.Database.Connection.ConnectionString;
        DataSet ds = new DataSet();
        SqlConnection con = new SqlConnection(connstr);
        SqlDataAdapter da = new SqlDataAdapter("sp_RequestTypeSubcategoryFieldValue_ByTicketMasterId", con);
        da.SelectCommand.Parameters.Add("@TicketMasterId", SqlDbType.Int).Value = MasterId;
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.Fill(ds);

        DataTable DynamicControls = ds.Tables[0];
        DataTable DynamicControlsValues = ds.Tables[1];

        int DefaultDepartmentId = context.Setup_Department.FirstOrDefault(a => a.IsActive == true && a.IsDefault == true) == null ? 0 : context.Setup_Department.FirstOrDefault(a => a.IsActive == true && a.IsDefault == true).DepartmentId;
        DataTable dtList = context.TS_TicketMaster.Where(x => x.IsActive == true && x.TicketMasterId == MasterId).Select(s => new
        {
            TicketMasterId = s.TicketMasterId,
            ParentTicketMasterId = s.ParentTicketMasterId,
            InitiatorId = s.InitiatorId,
            InitiatorDepartmentId = s.UserLogin1.Setup_Employee2.DepartmentId,
            AssigneeId = s.AssigneeId,
            DepartmentId = s.DepartmentId,
            PriorityId = s.PriorityId,
            StatusId = s.StatusId,
            RequestModeId = s.RequestModeId,
            RequestTypeId = s.RequestTypeId,
            CategoryId = s.RequestTypeCategoryId,
            SubcategoryId = s.RequestTypeSubCategoryId,
            TicketTypeId = s.TicketTypeId,
            TicketCreationDate = s.TicketCreationDate,
            TicketCreationDate_ = "",
            TicketCreationTime = s.TicketCreationTime,
            TicketCreationTime_ = "",
            TicketCode = s.TicketCode,

            Assignee = s.UserLogin.Full_Name,
            Initiator = s.UserLogin1.Full_Name,
            Department = s.Setup_Department.DepartmentName,
            Priority = s.TS_Setup_Priority.PriorityName,
            Status = s.TS_Setup_TicketStatus.Status,
            RequestMode = s.TS_Setup_RequestMode.RequestMode,
            RequestTypeName = s.TS_Setup_RequestType.RequestTypeName,
            CategoryName = s.TS_Setup_RequestTypeCategory.CategoryName,
            Subcategory = s.TS_Setup_RequestTypeSubcategory.SubcategoryName,
            IsAssigned = s.IsAssigned,
            Tittle = s.Tittle,
            Description = s.Description,
            TypeOfIssueId = s.TypeOfIssueId,
            ProductSubCategoryId = s.ProductSubCategoryId,
            CustomerName = s.Customer.CustomerName,
            CustomerEmail = s.Customer.EmailAddress,
            CustomerCity = s.Customer.CityId != null ? s.Customer.TS_Setup_City.CityName : "",
            CustomerContact = s.Customer.ContactNo,
            CustomerAddress = s.Customer.Address,
            CustomerAlternativeNumber = s.Customer.AlternativeNumber,
            IsFlagMark = s.IsFlagMark,
            CreatedDate = s.CreatedDate,
            FirstReplyToCustomer = context.TS_ChatLog.FirstOrDefault(a => a.IsActive == true && a.ChatTypeId == (int)Constant.ChatType.Ticket_Reply_Response) == null ? NullDatetime : context.TS_ChatLog.FirstOrDefault(a => a.IsActive == true && a.ChatTypeId == (int)Constant.ChatType.Ticket_Reply_Response).CreatedDate,
            FirstReplyToCustomer_ = "",
            TypeOfIssue = s.TypeOfIssueId > 0 ? (context.TS_Setup_TypeOfIssue.FirstOrDefault(a => a.TypeOfIssueId == s.TypeOfIssueId).TypeOfIssue) : "",
            TypeOfComplaint = s.TypeOfComplaintId > 0 ? (context.TS_Setup_TypeOfComplaint.FirstOrDefault(a => a.TypeOfComplaintId == s.TypeOfComplaintId).TypeOfComplaint) : "",
            ProductSubCategory = s.ProductSubCategoryId > 0 ? (context.TS_Setup_ProductSubCategory.FirstOrDefault(a => a.ProductSubCategoryId == s.ProductSubCategoryId).ProductSubCategory) : "",

        }).ToList().ToDataTable();

        if (dtList != null && dtList.Rows.Count > 0)
        {
            var Department = getDepartments();
            int DepartmentId = Convert.ToInt32(dtList.Rows[0]["DepartmentId"].ToString() == "" ? "0" : dtList.Rows[0]["DepartmentId"].ToString());
            var TicketTO = getTicketTo(MasterId);
            var TicketCC = getTicketCC(MasterId);
            var Assignee = getAssignee();
            var RequestType = getRequestType();
            var Category = getCategory(int.Parse(dtList.Rows[0]["RequestTypeId"].ToString()));
            var Subcategory = getSubcategoryEdit(int.Parse(dtList.Rows[0]["SubcategoryId"].ToString() == "" ? "0" : dtList.Rows[0]["SubcategoryId"].ToString()));
            dtList.Rows[0]["FirstReplyToCustomer_"] = dtList.Rows[0]["FirstReplyToCustomer"].ToString() == "" ? "" : String.Format("{0:F}", Convert.ToDateTime(dtList.Rows[0]["FirstReplyToCustomer"].ToString()));

            //var Response = getTicketSubCategoryDetal_DataOnEdit(MasterId);

            JSON = JsonConvert.SerializeObject(dtList) + "Split_" + TicketTO + "Split_" + TicketCC + "Split_" + Department + "Split_" + Assignee + "Split_" + RequestType + "Split_" + Category + "Split_" + Subcategory + "Split_" + JsonConvert.SerializeObject(DynamicControls) + "Split_" + JsonConvert.SerializeObject(DynamicControlsValues);
        }

        return JSON;
    }


    [OperationContract]
    public string Get_Comments(int MasterId)
    {
        var List = context.TS_GET_COMMENTS_HISTORY(MasterId).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string SaveComments(int MasterId, string Description)
    {
        string Return = "0";
        DateTime dt = DateTime.Now;
        int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
        DAL.TS_TicketComment objTicketComment = new DAL.TS_TicketComment();
        objTicketComment.Description = Description;
        objTicketComment.TicketMasterId = MasterId;
        objTicketComment.CreatedDateInt = IntDate;
        objTicketComment.CreatedDate = dt;
        objTicketComment.IsActive = true;
        objTicketComment.UserIP = baseclass.UserIP;
        objTicketComment.CreatedBy = baseclass.UserId;
        context.TS_TicketComment.Add(objTicketComment);
        context.SaveChanges();
        if (objTicketComment.TicketCommentId > 0)
        {
            Return = "1";
        }
        return Return;
    }

    [OperationContract]
    public string getUsers()
    {
        var JSON = "";
        var List = context.UserLogins.Where(x => x.Is_Active == true)
           .OrderBy(x => x.Full_Name).Select(s => new
           {
               Value = s.Full_Name,
               Id = s.User_Code
           }).ToList();
        JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string SaveMeeting(int TicketMasterId, string MeetingMaster, string MeetingDetail, string FilePath, string StartTime, string EndTime)
    {

        string Status = "0";
        if (MeetingDetail != "")
        {
            DateTime dt = DateTime.Now;
            int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
            DAL.TS_TicketMeeting obj_TS_TicketMeeting = (DAL.TS_TicketMeeting)Common.Deserialize(MeetingMaster, typeof(DAL.TS_TicketMeeting));
            TimeSpan time = DateTime.Now.TimeOfDay;
            using (TransactionScope scope = new TransactionScope())
            {
                obj_TS_TicketMeeting.TicketMasterId = TicketMasterId;
                obj_TS_TicketMeeting.MeetingStartTime = Convert.ToDateTime(StartTime).TimeOfDay;
                obj_TS_TicketMeeting.MeetingEndTime = Convert.ToDateTime(EndTime).TimeOfDay;
                obj_TS_TicketMeeting.CreatedDate = dt;
                obj_TS_TicketMeeting.CreatedDateInt = IntDate;
                obj_TS_TicketMeeting.CreatedBy = baseclass.UserId;
                obj_TS_TicketMeeting.IsActive = true;
                obj_TS_TicketMeeting.UserIP = baseclass.UserIP;
                context.TS_TicketMeeting.Add(obj_TS_TicketMeeting);
                context.SaveChanges();

                DAL.TS_TicketMeetingDetail objMeetingDetail = new DAL.TS_TicketMeetingDetail();
                objMeetingDetail.TicketMeetingId = obj_TS_TicketMeeting.TicketMeetingId;
                objMeetingDetail.AttendeeId = baseclass.UserId;
                objMeetingDetail.CreatedBy = baseclass.UserId;
                objMeetingDetail.CreatedDate = dt;
                objMeetingDetail.IsActive = true;
                context.TS_TicketMeetingDetail.Add(objMeetingDetail);
                context.SaveChanges();

                string[] Detail = MeetingDetail.Split(',');
                for (int i = 0; i < Detail.Length; i++)
                {
                    if (Detail[i].ToString() != "")
                    {
                        int AttendeeId = Convert.ToInt32(Detail[i].ToString());
                        if (AttendeeId != baseclass.UserId)
                        {
                            DAL.TS_TicketMeetingDetail _objMeetingDetail = new DAL.TS_TicketMeetingDetail();
                            _objMeetingDetail.TicketMeetingId = obj_TS_TicketMeeting.TicketMeetingId;
                            _objMeetingDetail.AttendeeId = AttendeeId;
                            _objMeetingDetail.CreatedBy = baseclass.UserId;
                            _objMeetingDetail.CreatedDate = dt;
                            _objMeetingDetail.IsActive = true;
                            context.TS_TicketMeetingDetail.Add(_objMeetingDetail);
                            context.SaveChanges();
                        }
                    }
                }


                if (FilePath != "")
                {
                    string[] File = FilePath.Split(',');
                    DAL.TS_MeetingAttachments objTS_MeetingAttachments = new DAL.TS_MeetingAttachments();
                    objTS_MeetingAttachments.TargetId = obj_TS_TicketMeeting.TicketMeetingId;
                    objTS_MeetingAttachments.Filename = File[0].ToString();
                    objTS_MeetingAttachments.FileOriginalName = File[1].ToString();
                    objTS_MeetingAttachments.Filetype = File[2].ToString();
                    context.TS_MeetingAttachments.Add(objTS_MeetingAttachments);
                    context.SaveChanges();
                }
                scope.Complete();
                Status = "1";
            }
        }
        return Status;
    }

    [OperationContract]
    public string GetMeeting(int TicketMasterId, int TicketMeetingId)
    {
        var List = context.TS_TicketMeeting.Where(x => x.IsActive == true && x.TicketMasterId == TicketMasterId && x.TicketMeetingId == TicketMeetingId)
           .Select(s => new
           {
               TicketMasterId = s.TicketMasterId,
               TicketMeetingId = s.TicketMeetingId,
               MeetingStartTime = s.MeetingStartTime,
               MeetingEndTime = s.MeetingEndTime,
               MeetingDate = s.MeetingDate,
               MeetingAgenda = s.MeetingAgenda,
               MeetingDetail = s.MeetingDetail,
               Location = s.Location,
               Description = s.Description,
               TicketCode = s.TS_TicketMaster.TicketCode,
               HasAttachment = context.TS_MeetingAttachments.Where(a => a.TargetId == s.TicketMeetingId).Count(),
           }).ToList();

        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string ViewMeetingAttendee(int TicketMeetingId)
    {
        var List = context.TS_TicketMeetingDetail.Where(x => x.IsActive == true && x.TicketMeetingId == TicketMeetingId)
           .Select(s => new
           {
               Attendee = s.UserLogin.Full_Name,
           }).ToList();
        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string getMeetingAttachment(int TicketMeetingId)
    {
        var List = context.TS_MeetingAttachments.Where(a => a.TargetId == TicketMeetingId).Select(s => new
        {
            FileId = s.FileId,
            TicketMasterId = s.TargetId,
            FileOriginalName = s.FileOriginalName,
            Filename = s.Filename,
            Filetype = s.Filetype,
            FilePath = "/Uploads/" + s.Filename,
        }).ToList();

        var JSON = JsonConvert.SerializeObject(List);
        return JSON;
    }

    [OperationContract]
    public string SaveMinutesOfMeeting(int TicketMeetingId, string MinutesOfMeeting)
    {

        string Status = "";
        if (MinutesOfMeeting != "" && TicketMeetingId > 0)
        {
            DateTime dt = DateTime.Now;
            DAL.TS_TicketMeeting obj = context.TS_TicketMeeting.FirstOrDefault(j => j.TicketMeetingId == TicketMeetingId);
            obj.MeetingDetail = MinutesOfMeeting.Trim();
            obj.ModifiedBy = baseclass.UserId;
            obj.ModifiedDate = dt;
            context.SaveChanges();
            Status = obj.MeetingDetail;
        }
        return Status;
    }

    [OperationContract]
    public string SaveTask(int TicketMasterId, string TaskMaster, string TaskDetail, string FilePath)
    {

        string Status = "0";
        if (TicketMasterId > 0 && TaskMaster != "" && TaskDetail != "")
        {
            DateTime dt = DateTime.Now;
            DataTable dtdd = new DataTable();
            int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
            using (TransactionScope scope = new TransactionScope())
            {
                DAL.TS_TaskMaster obj_TS_TaskMaster = (DAL.TS_TaskMaster)Common.Deserialize(TaskMaster, typeof(DAL.TS_TaskMaster));
                obj_TS_TaskMaster.CreatedDate = dt;
                obj_TS_TaskMaster.CreatedBy = baseclass.UserId;
                obj_TS_TaskMaster.IsActive = true;
                obj_TS_TaskMaster.UserIP = baseclass.UserIP;
                context.TS_TaskMaster.Add(obj_TS_TaskMaster);
                context.SaveChanges();
                DAL.TS_TaskDetail obj_TS_TaskDetail = (DAL.TS_TaskDetail)Common.Deserialize(TaskDetail, typeof(DAL.TS_TaskDetail));
                obj_TS_TaskDetail.TaskMasterId = obj_TS_TaskMaster.TaskMasterId;
                obj_TS_TaskDetail.CreatedBy = baseclass.UserId;
                obj_TS_TaskDetail.CreatedDate = dt;
                obj_TS_TaskDetail.IsActive = true;
                context.TS_TaskDetail.Add(obj_TS_TaskDetail);
                context.SaveChanges();

                if (FilePath != "")
                {
                    string[] File = FilePath.Split(',');
                    DAL.TS_TaskAttachments objTS_TaskAttachments = new DAL.TS_TaskAttachments();
                    objTS_TaskAttachments.TargetId = obj_TS_TaskMaster.TaskMasterId;
                    objTS_TaskAttachments.Filename = File[0].ToString();
                    objTS_TaskAttachments.FileOriginalName = File[1].ToString();
                    objTS_TaskAttachments.Filetype = File[2].ToString();
                    objTS_TaskAttachments.IsActive = true;
                    context.TS_TaskAttachments.Add(objTS_TaskAttachments);
                    context.SaveChanges();
                }
                scope.Complete();
                Status = "1";
            }
        }
        return Status;
    }

    [OperationContract]
    public string GetSaveTaskAtachment(int MasterId, int TempId, string filename)
    {
        int Id = MasterId > 0 ? MasterId : TempId;

        if (filename != "")
        {
            string[] File = filename.Split(',');
            DAL.TS_TaskAttachments objTS_TicketAttachments = new DAL.TS_TaskAttachments();
            objTS_TicketAttachments.TargetId = Id;
            objTS_TicketAttachments.Filename = File[0].ToString();
            objTS_TicketAttachments.FileOriginalName = File[1].ToString();
            objTS_TicketAttachments.Filetype = File[2].ToString();
            objTS_TicketAttachments.IsActive = true;
            context.TS_TaskAttachments.Add(objTS_TicketAttachments);
            context.SaveChanges();
        }

        var List = context.TS_TaskAttachments.Where(a => a.IsActive == true && a.TargetId == Id).Select(s => new
        {
            FileId = s.FileId,
            TicketMasterId = s.TargetId,
            FileOriginalName = s.FileOriginalName,
            Filename = s.Filename,
            Filetype = s.Filetype,
            FilePath = "/Uploads/" + s.Filename,
        }).ToList();

        var JSON = JsonConvert.SerializeObject(List);

        return JSON;
    }

    [OperationContract]
    public string UpdateTaskStatus(int MasterId, int StatusId, string Desc)
    {
        string ReturnStatus = "0";

        if (MasterId > 0 && StatusId > 0)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DateTime dt = DateTime.Now;
                int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
                DAL.TS_TaskMaster objTaskMaster = context.TS_TaskMaster.FirstOrDefault(j => j.TaskMasterId == MasterId);
                objTaskMaster.StatusId = StatusId;
                objTaskMaster.ModifiedBy = baseclass.UserId;
                objTaskMaster.ModifiedDate = dt;
                objTaskMaster.IsActive = true;
                objTaskMaster.UserIP = baseclass.UserIP;
                context.SaveChanges();
                DAL.TS_TaskDetail objTaskdetail = new DAL.TS_TaskDetail();
                objTaskdetail.TaskMasterId = MasterId;
                objTaskdetail.Description = Desc.Trim();
                objTaskdetail.StatusId = StatusId;
                objTaskdetail.CreatedBy = baseclass.UserId;
                objTaskdetail.CreatedDate = dt;
                objTaskdetail.AssigneeTo = objTaskMaster.AssigneeId;
                objTaskdetail.IsActive = true;
                objTaskdetail.UserIP = baseclass.UserIP;
                context.TS_TaskDetail.Add(objTaskdetail);
                context.SaveChanges();
                scope.Complete();
                ReturnStatus = "1";
            }
        }

        return ReturnStatus;
    }

    [OperationContract]
    public string UpdateTaskAssignee_(int MasterId, int AssigneeId)
    {
        string ReturnStatus = "0";

        if (MasterId > 0 && AssigneeId > 0)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                DateTime dt = DateTime.Now;
                int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
                DAL.TS_TaskMaster objTaskMaster = context.TS_TaskMaster.FirstOrDefault(j => j.TaskMasterId == MasterId);
                objTaskMaster.AssigneeId = AssigneeId;
                objTaskMaster.ModifiedBy = baseclass.UserId;
                objTaskMaster.ModifiedDate = dt;
                objTaskMaster.IsActive = true;
                objTaskMaster.UserIP = baseclass.UserIP;
                context.SaveChanges();
                scope.Complete();
                ReturnStatus = "1";
            }
        }

        return ReturnStatus;
    }

    [OperationContract]
    public void SleepMethod(int Time)
    {
        Thread.Sleep(Time);
    }

    [OperationContract]
    public string getInitiator_getDepartments_getPriority__getRequestType_getRequestMode_getCity()
    {
        var Department = getDepartments();
        var Initiator = getInitiator();
        var Priority = getPriority();
        var RequestType = getRequestType();
        var RequestMode = getRequestMode();
        var City = getCity();
        string DefaultIdsofControls = getDefaultIdsofControls();
        string[] split = DefaultIdsofControls.Split('%');
        int DepartmentId_ = Convert.ToInt32(split[0].ToString());
        var Subcategory = getSubcategory(0);
        //var Assignee = getAssigneeByDepartmentId(DepartmentId_);
        var JSON = Initiator + "Split_" + Priority + "Split_" + RequestType + "Split_" + RequestMode + "Split_" + Department + "Split_" + DefaultIdsofControls + "Split_" + Subcategory + "Split_" + City;
        return JSON;
    }

    [OperationContract]
    public string getTicketDataOnEdit(int MasterId)
    {
        var JSON = "";
        var List = context.TS_TicketMaster.Where(x => x.IsActive == true && x.TicketMasterId == MasterId).Select(s => new
        {
            TicketMasterId = s.TicketMasterId,
            TicketCode = s.TicketCode,
            CustomerId = s.CustomerId == 1 ? 0 : s.CustomerId,
            ParentTicketMasterId = s.ParentTicketMasterId,
            InitiatorId = s.InitiatorId,
            AssigneeId = s.AssigneeId,
            DepartmentId = s.DepartmentId,
            PriorityId = s.PriorityId,
            CityId = s.Customer.CityId,
            StatusId = s.StatusId,
            RequestModeId = s.RequestModeId,
            RequestTypeId = s.RequestTypeId,
            IsAssigned = s.IsAssigned,
            Tittle = s.Tittle,
            Description = s.Description,
            LevelId = s.LevelId,
            TicketTypeId = s.TicketTypeId,
            IsFlagMark = s.IsFlagMark,
            RequestTypeCategoryId = s.RequestTypeCategoryId,
            RequestTypeSubcategoryId = s.RequestTypeSubCategoryId,

        }).ToList();


        if (List != null && List.Count > 0)
        {


            int DepartmentId = List[0].DepartmentId == null ? 0 : Convert.ToInt32(List[0].DepartmentId);
            int LevelId = List[0].LevelId == null ? 0 : Convert.ToInt32(List[0].LevelId);

            JSON = JsonConvert.SerializeObject(List);

            var Initiator = getInitiator();
            var Priority = getPriority();
            var RequestType = getRequestType();
            var MethodOfContact = getRequestMode();
            var Department = getDepartments();
            var Assignee = getAssignee();
            var City = getCity();
            var Category = getCategory(List[0].RequestTypeId);
            var Subcategory = getSubcategoryEdit(List[0].RequestTypeCategoryId);
            var TicketTO = getTicketTo(MasterId);
            var TicketCC = getTicketCC(MasterId, List[0].StatusId, true);
            var TicketAtachment = GetSaveAtachment(MasterId, 0, "");
            var StatusId = List[0].StatusId;


            JSON = JSON + "Split_" + Initiator + "Split_" + Priority + "Split_" + RequestType + "Split_" + MethodOfContact + "Split_" + Department +
                    "Split_" + Assignee + "Split_" + TicketTO + "Split_" + TicketCC + "Split_" + TicketAtachment + "Split_" + Category + "Split_" + Subcategory + "Split_" + StatusId + "Split_" + City;
        }
        return JSON;
    }

    public string getDefaultIdsofControls()
    {
        string DefaultDepartmentId = Convert.ToString(context.Setup_Department.FirstOrDefault(a => a.IsActive == true && a.IsDefault == true) == null ? 4 : context.Setup_Department.FirstOrDefault(a => a.IsActive == true && a.IsDefault == true).DepartmentId);
        string DefaultPriorityId = Convert.ToString(context.TS_Setup_Priority.FirstOrDefault(a => a.IsActive == true && a.IsDefault == true) == null ? 0 : context.TS_Setup_Priority.FirstOrDefault(a => a.IsActive == true && a.IsDefault == true).PriorityId);
        string DefaultRequestTypeId = Convert.ToString(context.TS_Setup_RequestType.FirstOrDefault(a => a.IsActive == true && a.IsDefault == true) == null ? 0 : context.TS_Setup_RequestType.FirstOrDefault(a => a.IsActive == true && a.IsDefault == true).RequestTypeId);
        string Return = DefaultDepartmentId + "%" + DefaultPriorityId + "%" + DefaultRequestTypeId;
        return Return;
    }

    [OperationContract]
    public string ChkIsRunningTicket(int TicketMasterId, int ManageSevicesMasterId, int DetailId)
    {
        string JSON = "";
        //if (ManageSevicesMasterId > 0 && DetailId == 0)
        //{
        //    var List = context.TS_TicketMaster.Where(a => a.IsActive == true && a.TicketMasterId != TicketMasterId && a.ManageSevicesMasterId == ManageSevicesMasterId && a.DetailId == null &&
        //        (a.StatusId == (int)Constant.TicketStatus.New || a.StatusId == (int)Constant.TicketStatus.OnHold || a.StatusId == (int)Constant.TicketStatus.ReOpen || a.StatusId == (int)Constant.TicketStatus.Working)).ToList();
        //    if (List != null && List.Count > 0)
        //    {
        //        string TicketCode = List[0].TicketCode;
        //        JSON = "0," + TicketCode;
        //    }
        //    else
        //    {
        //        List = context.TS_TicketMaster.Where(a => a.IsActive == true && a.TicketMasterId != TicketMasterId &&
        //        a.ManageSevicesMasterId == ManageSevicesMasterId && a.DetailId > 0 &&
        //       (a.StatusId == (int)Constant.TicketStatus.New || a.StatusId == (int)Constant.TicketStatus.OnHold || a.StatusId == (int)Constant.TicketStatus.ReOpen || a.StatusId == (int)Constant.TicketStatus.Working)).ToList();

        //        if (List != null && List.Count > 0)
        //        {
        //            JSON = "1";
        //        }
        //    }
        //}
        //else if (ManageSevicesMasterId > 0 && DetailId > 0)
        //{
        //    var List = context.TS_TicketMaster.Where(a => a.IsActive == true && a.TicketMasterId != TicketMasterId && a.ManageSevicesMasterId == ManageSevicesMasterId && a.DetailId == DetailId &&
        //   (a.StatusId == (int)Constant.TicketStatus.New || a.StatusId == (int)Constant.TicketStatus.OnHold || a.StatusId == (int)Constant.TicketStatus.ReOpen || a.StatusId == (int)Constant.TicketStatus.Working)).ToList();
        //    if (List != null && List.Count > 0)
        //    {

        //        string TicketCode = List[0].TicketCode;
        //        JSON = "2," + TicketCode;
        //    }
        //}

        return JSON;
    }

    [OperationContract]
    public string UpdateTicketToEmails(int TicketMasterId, string ToEmails)
    {
        string ReturnStatus = "0";
        try
        {
            if (TicketMasterId > 0 && ToEmails != "")
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DateTime dt = DateTime.Now;
                    var objTS_TicketTo = context.TS_TicketTo.Where(j => j.IsActive == true && j.TicketMasterId == TicketMasterId).ToList();
                    objTS_TicketTo.ForEach(a => { a.IsActive = false; a.ModifiedBy = baseclass.UserId; a.ModifiedDate = dt; });
                    context.SaveChanges();
                    string[] Email = ToEmails.Split(';');
                    for (int i = 0; i < Email.Length; i++)
                    {
                        string EmailAddress = Email[i].ToString();
                        if (EmailAddress != "")
                        {
                            DAL.TS_TicketTo obj = new DAL.TS_TicketTo();
                            obj.TicketMasterId = TicketMasterId;
                            obj.EmailId = EmailAddress;
                            obj.CreatedBy = baseclass.UserId;
                            obj.CreatedDate = dt;
                            obj.IsActive = true;
                            obj.UserIP = baseclass.UserIP;
                            context.TS_TicketTo.Add(obj);
                            context.SaveChanges();
                        }
                    }
                    scope.Complete();
                    ReturnStatus = "1";
                }
            }
        }
        catch
        {
        }
        return ReturnStatus;
    }

    [OperationContract]
    public string UpdateTicketCCEmails(int TicketMasterId, string CCEmails)
    {
        string ReturnStatus = "0";
        int? NULLint = null;
        try
        {
            if (TicketMasterId > 0 && CCEmails != "")
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DateTime dt = DateTime.Now;
                    var objTS_TicketCC = context.TS_TicketCC.Where(j => j.IsActive == true && j.TicketMasterId == TicketMasterId).ToList();
                    objTS_TicketCC.ForEach(a => { a.IsActive = false; a.ModifiedBy = baseclass.UserId; a.ModifiedDate = dt; });
                    context.SaveChanges();
                    string[] Email = CCEmails.Split(';');
                    for (int i = 0; i < Email.Length; i++)
                    {
                        string EmailAddress = Email[i].ToString();
                        if (EmailAddress != "")
                        {
                            var User = context.UserLogins.FirstOrDefault(a => a.Login_ID == EmailAddress);
                            DAL.TS_TicketCC obj = new DAL.TS_TicketCC();
                            obj.TicketMasterId = TicketMasterId;
                            obj.AssigneeId = User == null ? NULLint : User.User_Code;
                            obj.EmailId = EmailAddress;
                            obj.CreatedBy = baseclass.UserId;
                            obj.CreatedDate = dt;
                            obj.IsActive = true;
                            obj.UserIP = baseclass.UserIP;
                            context.TS_TicketCC.Add(obj);
                            context.SaveChanges();
                        }
                    }
                    scope.Complete();
                    ReturnStatus = "1";
                }
            }
        }
        catch
        {
        }
        return ReturnStatus;
    }

    [OperationContract]
    public string GetWallBoardData(int UserId, string TicketDateFrom, string TicketDateTo)
    {
        var JSON = "";
        try
        {
            DataSet ds = new DataSet();
            DataTable dt0 = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();

            int? Nullint = null;
            int? IntDateFrom = Nullint;
            int? IntDateTo = Nullint;
            if (TicketDateFrom != "" && TicketDateTo != "")
            {
                try
                {
                    DateTime TicketDateFrom_ = TicketDateFrom == "" ? DateTime.Now : Convert.ToDateTime(TicketDateFrom);
                    DateTime TicketDateTo_ = TicketDateTo == "" ? DateTime.Now : Convert.ToDateTime(TicketDateTo);
                    IntDateFrom = Convert.ToInt32(TicketDateFrom_.ToString(Constant.IntDateFormat));
                    IntDateTo = Convert.ToInt32(TicketDateTo_.ToString(Constant.IntDateFormat));
                }
                catch
                {
                    DateTime TicketDateFrom_ = DateTime.Now;
                    DateTime TicketDateTo_ = DateTime.Now;
                    IntDateFrom = Convert.ToInt32(TicketDateFrom_.ToString(Constant.IntDateFormat));
                    IntDateTo = Convert.ToInt32(TicketDateTo_.ToString(Constant.IntDateFormat));
                }
            }
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("SP_Wallboard", con);
            da.SelectCommand.CommandTimeout = 3600;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@USERID", SqlDbType.Int).Value = UserId;
            da.SelectCommand.Parameters.Add("@TicketCreationFromInt", SqlDbType.Int).Value = IntDateFrom;
            da.SelectCommand.Parameters.Add("@TicketCreationToInt", SqlDbType.Int).Value = IntDateTo;
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                //dt0 = ds.Tables[0];
                //dt1 = ds.Tables[1];
                //dt2 = ds.Tables[2];
                //dt3 = ds.Tables[3];
                //JSON = JsonConvert.SerializeObject(dt0) + "Split_" + JsonConvert.SerializeObject(dt1) + "Split_" + JsonConvert.SerializeObject(dt2) + "Split_" + JsonConvert.SerializeObject(dt3);
                JSON = JsonConvert.SerializeObject(ds);
            }
        }
        catch (Exception ex)
        {
            JSON = "";
        }
        return JSON;
    }

    [OperationContract]
    public string Dashboard_GetUserList()
    {
        var JSON = "";
        try
        {
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Dashboard_GetUserList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = baseclass.UserId;
            da.Fill(dt);
            JSON = JsonConvert.SerializeObject(dt);
        }
        catch
        {
            JSON = "";
        }
        return JSON;
    }

    [OperationContract]
    public string Dashboard_InsertChatIntoDB(string Msg, int UserId_To)
    {
        var JSON = "";
        DateTime dt = DateTime.Now;
        int IntDate = Convert.ToInt32(dt.ToString(Constant.IntDateFormat));
        DAL.TS_ChatLog objlog = new DAL.TS_ChatLog();
        objlog.ChatTypeId = (int)Constant.ChatType.Internel_Chat;
        objlog.Description = Msg;
        objlog.AssigneeId = UserId_To;
        objlog.CreatedBy = baseclass.UserId;
        objlog.CreatedDateInt = IntDate;
        objlog.CreatedDate = dt;
        objlog.IsActive = true;
        objlog.IsRead = false;
        objlog.UserIP = baseclass.UserIP;
        context.TS_ChatLog.Add(objlog);
        context.SaveChanges();
        return JSON;
    }

    [OperationContract]
    public string Dashboard_GetUnreadChat(int From)
    {
        var JSON = "";
        try
        {
            DataSet ds = new DataSet();
            DataTable dt0 = new DataTable();
            DataTable dt1 = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Dashboard_GetUnreadChat", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FromUser", SqlDbType.Int).Value = From;
            da.SelectCommand.Parameters.Add("@ToUser", SqlDbType.Int).Value = baseclass.UserId;
            da.Fill(ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt0 = ds.Tables[0];
                dt1 = ds.Tables[1];
                JSON = JsonConvert.SerializeObject(dt0) + "Split_" + JsonConvert.SerializeObject(dt1);
            }
        }
        catch
        {
            JSON = "";
        }
        return JSON;
    }

    [OperationContract]
    public string Dashboard_GetAllChat(int From)
    {
        var JSON = "";
        try
        {
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Dashboard_GetAllChat", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FromUser", SqlDbType.Int).Value = From;
            da.SelectCommand.Parameters.Add("@ToUser", SqlDbType.Int).Value = baseclass.UserId;
            da.Fill(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                JSON = JsonConvert.SerializeObject(dt);
            }
        }
        catch
        {
            JSON = "";
        }
        return JSON;
    }

    [OperationContract]
    public string BarChart(string symbol)
    {
        if (symbol == "pr")
        {
            var BarSeries = context.Dashboard_TopFive_ProductTicketsCount().ToList();
            string JSON = JsonConvert.SerializeObject(BarSeries);
            return JSON;
        }
        else if (symbol == "cit")
        {
            var BarSeries = context.Dashboard_TopFive_CityWise_TicketsCount().ToList();
            string JSON = JsonConvert.SerializeObject(BarSeries);
            return JSON;
        }
        else if (symbol == "us")
        {
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Dashboard_TopFive_UserWise_Closed_TicketsCount", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            string JSON = JsonConvert.SerializeObject(dt);

            //var BarSeries = context.Dashboard_TopFive_UserWise_Relolved_TicketsCount().ToList();
            //string JSON = JsonConvert.SerializeObject(BarSeries);
            return JSON;
        }
        else if (symbol == "cus")
        {
            //var BarSeries = context.Dashboard_TopFive_CustomerWiseTicketsCount().ToList();
            //string JSON = JsonConvert.SerializeObject(BarSeries);
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Dashboard_TopFive_CustomerWiseTicketsCount", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            string JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        else
        {
            return "";
        }


    }
    [OperationContract]
    public string PieChart(string symbol)
    {
        if (symbol == "pri")
        {
            var BarSeries = context.Dashboard_PriorityWiseTicketsCount().ToList();
            string JSON = JsonConvert.SerializeObject(BarSeries);
            return JSON;
        }

        else if (symbol == "cus")
        {
            var BarSeries = context.Dashboard_TopFive_CustomerWiseTicketsCount().ToList();
            string JSON = JsonConvert.SerializeObject(BarSeries);
            return JSON;
        }
        else if (symbol == "pr")
        {
            var BarSeries = context.Dashboard_TopFive_ProductTicketsCount().ToList();
            string JSON = JsonConvert.SerializeObject(BarSeries);
            return JSON;
        }
        else
        {
            return "";
        }
    }
    [OperationContract]
    public string polarAreaChart(string symbol)
    {
        if (symbol == "pr")
        {
            var Series = context.Dashboard_TopFive_ProductTicketsCount().ToList();
            string JSON = JsonConvert.SerializeObject(Series);
            return JSON;
        }
        else
        {
            return "";
        }
    }
    [OperationContract]
    public string doughnutChart(string symbol)
    {
        if (symbol == "cus")
        {
            var BarSeries = context.Dashboard_TopFive_CustomerWiseTicketsCount().ToList();
            string JSON = JsonConvert.SerializeObject(BarSeries);
            return JSON;
        }
        else if (symbol == "cit")
        {
            var BarSeries = context.Dashboard_TopFive_CityWise_TicketsCount().ToList();
            string JSON = JsonConvert.SerializeObject(BarSeries);
            return JSON;
        }
        else if (symbol == "radarProductSubCategory")
        {
            DataTable dt = new DataTable();
            string dbConnectionString = context.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("Dashboard_ProductSubCategoryWise_TicketsCount", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(dt);
            string JSON = JsonConvert.SerializeObject(dt);
            return JSON;
        }
        else
        {
            return "";
        }
    }
    [OperationContract]
    public string Line(string symbol)
    {
        if (symbol == "us")
        {
            var BarSeries = context.Dashboard_TopFive_UserWise_Relolved_TicketsCount().ToList();
            string JSON = JsonConvert.SerializeObject(BarSeries);
            return JSON;
        }
        else
        {
            return "";
        }
    }
    [OperationContract]
    public string LineChart()
    {
        var LineChart = context.Dashboard_Trend_SevenDays().ToList();
        string JSON = JsonConvert.SerializeObject(LineChart);
        return JSON;

    }

    [OperationContract]
    public string DashboardCharts(string TicketDateFrom, string TicketDateTo)
    {
        DateTime TicketDateFrom_ = TicketDateFrom == "" || TicketDateFrom == null ? DateTime.Now : Convert.ToDateTime(TicketDateFrom);
        DateTime TicketDateTo_ = TicketDateTo == "" || TicketDateTo == null ? DateTime.Now : Convert.ToDateTime(TicketDateTo);

        DataSet ds = new DataSet();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("Dashboard_GetAllGraphs", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@StartDate", SqlDbType.Date).Value = TicketDateFrom_;
        da.SelectCommand.Parameters.Add("@EndDate", SqlDbType.Date).Value = TicketDateTo_;
        da.Fill(ds);
        string JSON = JsonConvert.SerializeObject(ds);
        return JSON;
    }


    public string SendTestEmail()
    {
        string Msg = "<div dir='ltr'><img src='http://175.107.196.78:54002/Uploads/14011757_10154487635284380_682787331_n.jpg' alt='Inline image 1' width='324' height='100'><div class='gmail_extra'><br></div></div>";
        Email.SendMail("fahad1988iqbal@gmail.com", "ss", Msg, "", "");
        return "";
    }

    [OperationContract]
    public string SearchTickets(int? InitiatorId_, int? DepartmentId_, int? AssigneeId_, int? PriorityId_, int? CityId_,
        string Ticket_, string Title_, string StatusId_, bool IsResponseOverTickets,
        bool IsInternalChatOverTickets, string TicketDateFrom, string TicketDateTo, string CustomerName, string ContactNo, string AltContactNo , string EmailAddress, string ReportedBy, string Operation)
    {
        int? HfDepartmentId_ = null;
        int _CustomerId = baseclass.CustomerId;
        if (_CustomerId > 0)
        {
            InitiatorId_ = baseclass.UserId;
        }


        if (!baseclass.IsAdmin && !baseclass.IsSuperAdmin && !baseclass.IsLevel_1)
        {
            HfDepartmentId_ = baseclass.DepartmentId;
        }

        bool? Nullbool = null;
        int? Nullint = null;
        int? IntDateFrom = Nullint;
        int? IntDateTo = Nullint;
        if (TicketDateFrom != "" && TicketDateTo != "")
        {
            try
            {
                DateTime TicketDateFrom_ = TicketDateFrom == "" ? DateTime.Now : Convert.ToDateTime(TicketDateFrom);
                DateTime TicketDateTo_ = TicketDateTo == "" ? DateTime.Now : Convert.ToDateTime(TicketDateTo);
                IntDateFrom = Convert.ToInt32(TicketDateFrom_.ToString(Constant.IntDateFormat));
                IntDateTo = Convert.ToInt32(TicketDateTo_.ToString(Constant.IntDateFormat));
            }
            catch
            {
                DateTime TicketDateFrom_ = DateTime.Now;
                DateTime TicketDateTo_ = DateTime.Now;
                IntDateFrom = Convert.ToInt32(TicketDateFrom_.ToString(Constant.IntDateFormat));
                IntDateTo = Convert.ToInt32(TicketDateTo_.ToString(Constant.IntDateFormat));
            }
        }

        DataTable dt = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("TS_SearchTicketNew", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.CommandTimeout = 3600;
        da.SelectCommand.Parameters.Add("@USERID", SqlDbType.Int).Value = baseclass.UserId;
        da.SelectCommand.Parameters.Add("@InitiatorId", SqlDbType.Int).Value = InitiatorId_;
        da.SelectCommand.Parameters.Add("@AssigneeId", SqlDbType.Int).Value = AssigneeId_;
        da.SelectCommand.Parameters.Add("@PriorityId", SqlDbType.Int).Value = PriorityId_;
        da.SelectCommand.Parameters.Add("@CityId", SqlDbType.Int).Value = CityId_;
        da.SelectCommand.Parameters.Add("@TicketCode", SqlDbType.NVarChar).Value = Ticket_;
        da.SelectCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Title_;
        da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = DepartmentId_;
        da.SelectCommand.Parameters.Add("@StatusId", SqlDbType.NVarChar).Value = StatusId_;
        da.SelectCommand.Parameters.Add("@IsDeptTicket", SqlDbType.Bit).Value = Nullbool;
        da.SelectCommand.Parameters.Add("@TicketCreationFromInt", SqlDbType.Int).Value = IntDateFrom;
        da.SelectCommand.Parameters.Add("@TicketCreationToInt", SqlDbType.Int).Value = IntDateTo;
        da.SelectCommand.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = CustomerName;
        da.SelectCommand.Parameters.Add("@RequestTypeId", SqlDbType.Int).Value = Nullint;
        da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = Nullint;
        da.SelectCommand.Parameters.Add("@SubcategoryId", SqlDbType.Int).Value = Nullint;
        da.SelectCommand.Parameters.Add("@HfDepartmentId", SqlDbType.Int).Value = HfDepartmentId_;
        da.SelectCommand.Parameters.Add("@ContactNo", SqlDbType.NVarChar).Value = ContactNo;
        da.SelectCommand.Parameters.Add("@AltContactNo", SqlDbType.NVarChar).Value = AltContactNo;
        da.SelectCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = EmailAddress;
        da.SelectCommand.Parameters.Add("@ReportedBy", SqlDbType.NVarChar).Value = ReportedBy;
        da.SelectCommand.Parameters.Add("@RequestModeId", SqlDbType.Int).Value = 0;
        da.SelectCommand.Parameters.Add("@Operation", SqlDbType.VarChar).Value = Operation;
        da.Fill(dt);


        var JSON = JsonConvert.SerializeObject(dt);
        return JSON;
    }

    [OperationContract]
    public string getInitiator_getDepartments_getPriority()
    {
        var Initiator_ = context.UserLogins.Where(x => x.Is_Active == true && (baseclass.IsClient == true ? x.User_Code == baseclass.UserId : true))
           .OrderBy(x => x.Full_Name).Select(s => new
           {
               Value = s.Full_Name,
               Id = s.User_Code
           }).ToList();

        DataTable ListStatus = context.TS_Setup_TicketStatus.Where(e => e.IsActive == true).OrderBy(c => c.Status).Select(s => new
        {
            Value = s.Status,
            Id = s.StatusId
        }).ToList().ToDataTable();
        var Initiator = JsonConvert.SerializeObject(Initiator_);
        var Priority = getPriority();
        var Department = getDepartments();
        var Status = JsonConvert.SerializeObject(ListStatus);
        var City = getCity();
        var ReportedBy = getReportedBy();
        var JSON = Initiator + "Split_" + Department + "Split_" + Priority + "Split_" + Status + "Split_" + City+ "Split_"+ ReportedBy;
        return JSON;
    }

    [OperationContract]
    public string GetStatusHistory(int TicketMasterId)
    {
        var JSON = "";
        //DataTable ListStatus = context.TS_TicketDetail.Where(e => e.IsActive == true && e.TicketMasterId == TicketMasterId).Select(c => new
        //{
        //    SNo = "",
        //    MasterId = c.TicketMasterId,
        //    DetailId = c.TicketDetailId,
        //    Status = c.TS_Setup_TicketStatus.Status.Trim(),
        //    StatusId = c.StatusId,
        //    //Assignee = c.EmailFrom != null ? c.EmailFrom : c.UserLogin.Full_Name,
        //    //Assignee = c.UserLogin.Full_Name,
        //    //AssigneeId = c.AssigneeId,
        //    CreatedById = c.CreatedBy,
        //    CreatedBy = c.UserLogin.Full_Name,
        //    imageUrl = c.TS_Setup_TicketStatus.imageUrl,
        //    CreatedDate = c.CreatedDate,
        //    CreatedDate_ = "",
        //    Description = c.Description
        //}).ToList().OrderBy(f => f.CreatedDate).ToList().ToDataTable();

        var ListStatus = (from ticketDetail in context.TS_TicketDetail
                          join userlogin in context.UserLogins on ticketDetail.CreatedBy equals userlogin.User_Code into gj
                          from subpet in gj.DefaultIfEmpty()
                          where (ticketDetail.IsActive == true && ticketDetail.TicketMasterId == TicketMasterId)
                          orderby ticketDetail.CreatedDate
                          select new
                          {
                              SNo = "",
                              MasterId = ticketDetail.TicketMasterId,
                              DetailId = ticketDetail.TicketDetailId,
                              Status = ticketDetail.TS_Setup_TicketStatus.Status.Trim(),
                              StatusId = ticketDetail.StatusId,
                              CreatedById = ticketDetail.CreatedBy,
                              CreatedBy = ticketDetail.CreatedBy > 0 ? subpet.Full_Name : ticketDetail.EmailFrom, // userlogin.Full_Name,
                              imageUrl = ticketDetail.TS_Setup_TicketStatus.imageUrl,
                              CreatedDate = ticketDetail.CreatedDate,
                              CreatedDate_ = "",
                              Description = ticketDetail.Description,
                              TypeOfIssue = ticketDetail.TypeOfIssueId > 0 ? (context.TS_Setup_TypeOfIssue.FirstOrDefault(a => a.TypeOfIssueId == ticketDetail.TypeOfIssueId).TypeOfIssue) : "",
                              TypeOfComplaint = ticketDetail.TypeOfComplaintId > 0 ? (context.TS_Setup_TypeOfComplaint.FirstOrDefault(a => a.TypeOfComplaintId == ticketDetail.TypeOfComplaintId).TypeOfComplaint) : "",
                              ProductSubCategory = ticketDetail.ProductSubCategoryId > 0 ? (context.TS_Setup_ProductSubCategory.FirstOrDefault(a => a.ProductSubCategoryId == ticketDetail.ProductSubCategoryId).ProductSubCategory) : "",

                          }).ToList().OrderByDescending(f => f.CreatedDate).ToList().ToDataTable();

        if (ListStatus != null && ListStatus.Rows.Count > 0)
        {
            for (int i = 0; i < ListStatus.Rows.Count; i++)
            {
                ListStatus.Rows[i]["CreatedDate_"] = Convert.ToString(ListStatus.Rows[i]["CreatedDate"].ToString());
                ListStatus.Rows[i]["SNo"] = Convert.ToString(i + 1);
            }
            JSON = JsonConvert.SerializeObject(ListStatus);
        }
        return JSON;
    }

    [OperationContract]
    public string Sleep()
    {
        var JSON = "1";
        Thread.Sleep(1000);
        return JSON;
    }

    [OperationContract]
    public string DeleteTicket(int TicketMasterId, string remarks)
    {
        var JSON = "";
        var li = context.TS_TicketMaster.FirstOrDefault(a => a.IsActive == true && a.TicketMasterId == TicketMasterId);
        if (li != null)
        {
            try
            {
                DataTable ds = new DataTable();
                DAL.TicketSystemEntities context1 = new DAL.TicketSystemEntities();
                string dbConnectionString = context1.Database.Connection.ConnectionString;
                SqlConnection con = new SqlConnection(dbConnectionString);
                SqlDataAdapter da = new SqlDataAdapter("TS_DeletedTickets", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@TicketMasterId", SqlDbType.Int).Value = TicketMasterId;
                da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = li.DepartmentId;
                da.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = baseclass.UserId;
                da.SelectCommand.Parameters.Add("@Remarks", SqlDbType.NVarChar).Value = remarks;
                da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.NVarChar).Value = baseclass.UserIP;

                da.Fill(ds);
                if (ds != null && ds.Rows.Count > 0)
                {
                    JSON = li.TicketCode;
                }
            }
            catch (Exception ex)
            {
                Email.WriteFile("Delete Ticket Email : " + ex.ToString());
            }
            //}
        }
        return JSON;
    }

    [OperationContract]
    public string ChkIsRunningTicketOnSave(int TicketMasterId, int ManageSevicesMasterId, int DetailId, int ProductId)
    {
        string JSON = "";
        //if (ProductId != (int)Constant.Product.Email && ManageSevicesMasterId > 0)
        //{
        //    var List = context.TS_TicketMaster.Where(a => a.IsActive == true && a.TicketMasterId != TicketMasterId && a.ManageSevicesMasterId == ManageSevicesMasterId &&
        //        (a.StatusId == (int)Constant.TicketStatus.New || a.StatusId == (int)Constant.TicketStatus.OnHold || a.StatusId == (int)Constant.TicketStatus.ReOpen || a.StatusId == (int)Constant.TicketStatus.Working)).ToList();
        //    if (List != null && List.Count > 0)
        //    {
        //        JSON = "0" + "," + List[0].TicketCode;
        //        var List_Details = context.TS_TicketMaster.Where(a => a.IsActive == true && a.TicketMasterId != TicketMasterId && a.ManageSevicesMasterId == ManageSevicesMasterId && a.DetailId > 0 &&
        //            (a.StatusId == (int)Constant.TicketStatus.New || a.StatusId == (int)Constant.TicketStatus.OnHold || a.StatusId == (int)Constant.TicketStatus.ReOpen || a.StatusId == (int)Constant.TicketStatus.Working)).ToList();
        //        if (List_Details != null && List_Details.Count > 0)
        //        {
        //            JSON = "0" + "," + "0";
        //            var List_DetailId = context.TS_TicketMaster.Where(a => a.IsActive == true && a.TicketMasterId != TicketMasterId && a.ManageSevicesMasterId == ManageSevicesMasterId && a.DetailId == DetailId &&
        //           (a.StatusId == (int)Constant.TicketStatus.New || a.StatusId == (int)Constant.TicketStatus.OnHold || a.StatusId == (int)Constant.TicketStatus.ReOpen || a.StatusId == (int)Constant.TicketStatus.Working)).ToList();
        //            if (List_DetailId != null && List_DetailId.Count > 0)
        //            {
        //                JSON = "0" + "," + List_DetailId[0].TicketCode;
        //            }
        //        }
        //    }
        //}
        return JSON;
    }

    [OperationContract]
    public string getProductSubCategoryByProductId(int ProductId)
    {
        DataTable dt = new DataTable();
        DataTable dtFiltered = new DataTable();
        string dbConnectionString = context.Database.Connection.ConnectionString;
        SqlConnection con = new SqlConnection(dbConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("GetProductSubCategoryByProductId", con);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductId;
        da.Fill(dt);
        var JSON = JsonConvert.SerializeObject(dt);
        return JSON;
    }


    [OperationContract]
    public Stream SetDynamicFields(int TicketSubCategoryId)
    {
        var JSON = "";
        Dictionary<string, object> objResponse = new Dictionary<string, object>();

        try
        {
            string connstr = context.Database.Connection.ConnectionString;
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connstr);
            SqlDataAdapter da = new SqlDataAdapter("CM_GetDynamicControls_By_TicketSubCategoryId", con);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@TicketSubCategoryId", SqlDbType.Int).Value = TicketSubCategoryId;
            da.Fill(ds);
            objResponse.Add(Constant.ResponseKeys.Response, Constant.ErrorMessages.Success);
            objResponse.Add(Constant.ResponseKeys.ResponseCode, Constant.ErrorCodes.Success);
            objResponse.Add("Data", ds);

        }
        catch (Exception ex)
        {
            objResponse.Add(Constant.ResponseKeys.Response, Constant.ErrorMessages.Failure);
            objResponse.Add(Constant.ResponseKeys.ResponseCode, Constant.ErrorCodes.Exception);
            objResponse.Add(Constant.ResponseKeys.ErrorMessage, ex.InnerException.Message);
        }

        return MakeJSONResponse(objResponse);
    }

    private Stream MakeJSONResponse(dynamic ResponseObject)
    {
        var list = JsonConvert.SerializeObject(ResponseObject,
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });

        System.ServiceModel.Web.WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
        return new System.IO.MemoryStream(Encoding.UTF8.GetBytes(list));
    }
    
    [OperationContract]
    public Stream getTicketSubCategoryDetal_DataOnEdit(int TicketMasterId)
    {
        JavaScriptSerializer objJavascriptSerializer = new JavaScriptSerializer();
        Dictionary<string, object> objResponse = new Dictionary<string, object>();
        try
        {
            string connstr = context.Database.Connection.ConnectionString;
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(connstr);
            SqlDataAdapter da = new SqlDataAdapter("sp_RequestTypeSubcategoryFieldValue_ByTicketMasterId", con);
            da.SelectCommand.Parameters.Add("@TicketMasterId", SqlDbType.Int).Value = TicketMasterId;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(ds);

            objResponse.Add(Constant.ResponseKeys.Response, Constant.ErrorMessages.Success);
            objResponse.Add(Constant.ResponseKeys.ResponseCode, Constant.ErrorCodes.Success);
            objResponse.Add("Data", ds);
        }
        catch (Exception ex)
        {
            objResponse.Add(Constant.ResponseKeys.Response, Constant.ErrorMessages.Failure);
            objResponse.Add(Constant.ResponseKeys.ResponseCode, Constant.ErrorCodes.Exception);
            objResponse.Add(Constant.ResponseKeys.ErrorMessage, ex.InnerException.Message);
        }
        return MakeJSONResponse(objResponse);
    }
}
