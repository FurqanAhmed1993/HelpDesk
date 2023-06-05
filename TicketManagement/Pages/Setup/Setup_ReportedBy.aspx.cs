using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Setup_Setup_ReportedBy : Base
{
    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRepeater();
        }
        PagingHandler();
    }


    #region PAGING
    private void PagingHandler()
    {
        PagingAndSorting.ImgNext.Click += ImgNext_Click;
        PagingAndSorting.ImgPrevious.Click += ImgPrevious_Click;
        PagingAndSorting.DdlPage.SelectedIndexChanged += DdlPage_SelectedIndexChanged;
        PagingAndSorting.DdlPageSize.SelectedIndexChanged += DdlPageSize_SelectedIndexChanged;
    }

    void DdlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    void DdlPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindRepeater();
    }
    void ImgNext_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    void ImgPrevious_Click(object sender, ImageClickEventArgs e)
    {
        BindRepeater();
    }
    #endregion

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txtReportedBySearch.Text = "";
        ResetControls();
    }


    private void BindRepeater()
    {
        try
        {
            //int pageSize = 50;
            //int pageNumber = 1;
            //if (PagingAndSorting.DdlPageSize.SelectedValue.toInt() > 0)
            //{
            //    pageSize = PagingAndSorting.DdlPageSize.SelectedValue.toInt();
            //}
            //if (PagingAndSorting.DdlPage.Items.Count > 0)
            //{
            //    pageNumber = PagingAndSorting.DdlPage.SelectedValue.toInt();
            //}

            //int skip = pageNumber * pageSize - pageSize;

            //string Name = txtReportedBySearch.Text.ToString();
            //var List = context.TS_Setup_ReportedBy.Where(a => a.IsActive == true
            //     && (a.ReportedByName.Contains(Name) || Name == String.Empty)
            //    )
            //     .Select(c => new
            //     {
            //         ReportedById = c.ReportedById,
            //         ReportedByName = c.ReportedByName,
            //     }).OrderBy(a => a.ReportedByName)
            //    .ToList();
            //var List1 = List.OrderBy(a => a.ReportedByName).Skip(skip).Take(pageSize).ToList();
            //rpt.DataSource = List1;
            //rpt.DataBind();
            //PagingAndSorting.setPagingOptions(List.Count());


            int pageSize = 50;
            int pageNumber = 1;
            if (PagingAndSorting.DdlPageSize.SelectedValue.toInt() > 0)
            {
                pageSize = PagingAndSorting.DdlPageSize.SelectedValue.toInt();
            }
            if (PagingAndSorting.DdlPage.Items.Count > 0)
            {
                pageNumber = PagingAndSorting.DdlPage.SelectedValue.toInt();
            }

            int skip = pageNumber * pageSize - pageSize;


            string Bank = txtReportedBySearch.Text.ToString();
            var List = context.Setup_TicketPreRequisite_Values.Where(a => a.IsActive == true
                 && (a.Name.Contains(Bank) || Bank == String.Empty) && a.FormType == Constant.TS_Setup_TicketPreRequisiteValuesTypeID.ReportedBy)
                 .Select(c => new
                 {
                     ReportedById = c.Id,
                     ReportedByName = c.Name,

                 }).OrderByDescending(a => a.ReportedById)
                .ToList();
            var List1 = List.OrderByDescending(a => a.ReportedById).Skip(skip).Take(pageSize).ToList();
            rpt.DataSource = List1;
            rpt.DataBind();
            PagingAndSorting.setPagingOptions(List.Count());

        }
        catch (Exception ex)
        {
        }
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    LinkButton btnEdit = (LinkButton)sender;
        //    RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
        //    int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
        //    var List = context.TS_Setup_ReportedBy.FirstOrDefault(j => j.ReportedById == hfRId);
        //    if (List != null)
        //    {
        //        txtReportedByName.Text = List.ReportedByName.Trim();
        //        hfId.Value = hfRId.ToString();
        //        OpenPopup();
        //    }
        //}
        //catch (Exception ex)
        //{
        //}

        try
        {
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
            var List = context.Setup_TicketPreRequisite_Values.FirstOrDefault(j => j.Id == hfRId);
            if (List != null)
            {
                hfId.Value = hfRId.ToString();
                txtReportedByName.Text = List.Name.Trim();
                OpenPopup();
            }
        }
        catch (Exception ex)
        {
            
        }
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    LinkButton btnDelete = (LinkButton)sender;
        //    RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
        //    int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
        //    var List = context.TS_Setup_ReportedBy.FirstOrDefault(j => j.ReportedById == hfRId);
        //    if (List != null)
        //    {
        //        string msg = "";//IsTransectionExist(hfRId);
        //        if (msg == "")
        //        {
        //            DateTime dt = DateTime.Now;
        //            List.IsActive = false;
        //            List.ModifiedBy = UserId;
        //            List.ModifiedAt = dt;
        //            List.UserIP = UserIP;
        //            context.SaveChanges();
        //            Success("Deleted successfully");
        //            ResetControls();
        //        }
        //        else
        //        {
        //            Error(msg);
        //            ResetControls();
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //}

        try
        {
            LinkButton btnDelete = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnDelete.NamingContainer;
            int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);

            using (TransactionScope scope = new TransactionScope())
            {
                var List = context.Setup_TicketPreRequisite_Values.FirstOrDefault(j => j.Id == hfRId);
                DateTime dt = DateTime.Now;
                List.IsActive = false;
                List.ModifiedBy = UserId;
                List.ModifiedDate = dt;
                List.UserIP = UserIP;
                context.SaveChanges();

                #region for Ticket Subcategory PreRequestie

                var List2 = context.Setup_RequestTypeSubcategoryFieldTypeDetail.Where(j => j.FieldValueID == hfRId && j.FieldValueTypeID == Constant.TS_Setup_TicketPreRequisiteValuesTypeID.ReportedBy).Select(s => new
                {
                    RequestTypeSubcategoryFieldTypeId = s.RequestTypeSubcategoryFieldTypeId,

                }).OrderBy(a => a.RequestTypeSubcategoryFieldTypeId).ToList();

                if (List2 != null && List2.Count > 0)
                {
                    DataTable ds = new DataTable();
                    DAL.TicketSystemEntities context1 = new DAL.TicketSystemEntities();
                    string dbConnectionString = context1.Database.Connection.ConnectionString;
                    SqlConnection con = new SqlConnection(dbConnectionString);
                    SqlDataAdapter da = new SqlDataAdapter("sp_Setup_RequestTypeSubcategoryFieldTypeDetail_Insert", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.Add("@RequestTypeSubcategoryFieldTypeDetailData", SqlDbType.Structured).Value = null;
                    da.SelectCommand.Parameters.Add("@userid", SqlDbType.Int).Value = UserId;
                    da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.NVarChar).Value = UserIP;
                    da.SelectCommand.Parameters.Add("@FieldValue", SqlDbType.NVarChar).Value = "";
                    da.SelectCommand.Parameters.Add("@FieldValueID", SqlDbType.Int).Value = hfRId;
                    da.SelectCommand.Parameters.Add("@OperationTypeId", SqlDbType.Int).Value = Constant.OperationTypeID.Delete;
                    da.Fill(ds);
                }
                #endregion

                Success("Deleted successfully");
                ResetControls();
                scope.Complete();
            }
        }
        catch (Exception ex)
        {
        }
    }

    private string CheckNameIfExist(String Name, int Id)
    {
        string Msg = "";
        try
        {
            int Count = context.TS_Setup_ReportedBy.Where(x => x.IsActive == true && x.ReportedById != Id && x.ReportedByName == Name).Count();
            if (Count > 0)
            {
                Msg = "ReportedBy Name already exist";
            }
            return Msg;
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        try
        {
            //int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);
            //string Msg = CheckNameIfExist(txtReportedByName.Text.Trim(), Id);
            //if (Msg == "")
            //{
            //    if (Id == 0)
            //    {
            //        Add();
            //    }
            //    else
            //    {
            //        Update();
            //    }
            //}
            //else
            //{
            //    Error(Msg);
            //}

            int Id = hfId.Value == string.Empty ? 0 : Convert.ToInt32(hfId.Value);

            using (TransactionScope scope = new TransactionScope())
            {
                if (Id == 0)
                {
                    Add();
                }
                else
                {
                    Update();
                }
                scope.Complete();
            }
        }

        catch (Exception ex)
        {
        }
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        ResetControls();
        ClosePopup();
    }

    private void Add()
    {
        //TS_Setup_ReportedBy Obj = new TS_Setup_ReportedBy();
        //Obj.ReportedByName = txtReportedByName.Text.Trim();
        //Obj.IsActive = true;
        //Obj.CreatedBy = UserId;
        //Obj.CreatedAt = DateTime.Now;
        //Obj.UserIP = UserIP;
        //context.TS_Setup_ReportedBy.Add(Obj);
        //context.SaveChanges();
        //ClosePopup();
        //ResetControls();
        //Success("Added Successfully");

        DateTime dt = DateTime.Now;
        Setup_TicketPreRequisite_Values Obj = new Setup_TicketPreRequisite_Values();
        Obj.Name = txtReportedByName.Text.Trim();
        Obj.FormType = Constant.TS_Setup_TicketPreRequisiteValuesTypeID.ReportedBy;
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIP = UserIP;
        context.Setup_TicketPreRequisite_Values.Add(Obj);
        context.SaveChanges();

        DataTable dtPreReqValues = new DataTable();
        dtPreReqValues.Columns.Add("RequestTypeSubcategoryFieldTypeId", typeof(int));
        dtPreReqValues.Columns.Add("FieldValue", typeof(string));
        dtPreReqValues.Columns.Add("FieldValueID", typeof(int));
        dtPreReqValues.Columns.Add("FieldValueTypeID", typeof(int));

        var List = context.Setup_RequestTypeSubcategoryFieldType.Where(j => j.FieldTypeId == 2 && j.InputClass == "ReportedBy").Select(s => new
        {
            RequestTypeSubcategoryFieldTypeId = s.RequestTypeSubcategoryFieldTypeId,

        }).OrderBy(a => a.RequestTypeSubcategoryFieldTypeId).ToList();


        if (List != null && List.Count > 0)
        {
            bool Status = false;
            DataTable dtValues = new DataTable();
            dtValues.Columns.Add("RequestTypeSubcategoryFieldTypeId", typeof(int));
            dtValues.Columns.Add("FieldValue", typeof(string));
            dtValues.Columns.Add("FieldValueID", typeof(int));
            dtValues.Columns.Add("FieldValueTypeID", typeof(int));

            for (int i = 0; i < List.Count; i++)
            {
                #region for Ticket Subcategory PreRequestie

                int RequestTypeSubcategoryFieldTypeID = Convert.ToInt32(List[i].RequestTypeSubcategoryFieldTypeId.ToString());

                dtValues.Rows.Add(RequestTypeSubcategoryFieldTypeID, txtReportedByName.Text.Trim(), Convert.ToInt32(Obj.Id), Constant.TS_Setup_TicketPreRequisiteValuesTypeID.ReportedBy);

                #endregion
            }

            if (dtValues != null && dtValues.Rows.Count > 0)
            {
                DataTable ds = new DataTable();
                DAL.TicketSystemEntities context1 = new DAL.TicketSystemEntities();
                string dbConnectionString = context1.Database.Connection.ConnectionString;
                SqlConnection con = new SqlConnection(dbConnectionString);
                SqlDataAdapter da = new SqlDataAdapter("sp_Setup_RequestTypeSubcategoryFieldTypeDetail_Insert", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@RequestTypeSubcategoryFieldTypeDetailData", SqlDbType.Structured).Value = dtValues;
                da.SelectCommand.Parameters.Add("@userid", SqlDbType.Int).Value = UserId;
                da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.NVarChar).Value = UserIP;
                da.SelectCommand.Parameters.Add("@FieldValue", SqlDbType.NVarChar).Value = "";
                da.SelectCommand.Parameters.Add("@FieldValueID", SqlDbType.Int).Value = 0;
                da.SelectCommand.Parameters.Add("@OperationTypeId", SqlDbType.Int).Value = Constant.OperationTypeID.Insert;
                da.Fill(ds);

                Status = true;
            }
        }

        ClosePopup();
        ResetControls();
        Success("Added Successfully");
    }

    private void Update()
    {
        //int Id = Convert.ToInt32(hfId.Value);
        //TS_Setup_ReportedBy Obj = context.TS_Setup_ReportedBy.FirstOrDefault(j => j.ReportedById == Id);
        //Obj.ReportedByName = txtReportedByName.Text.Trim();
        //Obj.IsActive = true;
        //Obj.ModifiedBy = UserId;
        //Obj.ModifiedAt = DateTime.Now;
        //Obj.UserIP = UserIP;
        //context.SaveChanges();
        //ClosePopup();
        //ResetControls();
        //Success("Updated Successfully");

        bool Status = false;
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfId.Value);
        Setup_TicketPreRequisite_Values Obj = context.Setup_TicketPreRequisite_Values.FirstOrDefault(j => j.Id == Id);
        Obj.Name = txtReportedByName.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIP = UserIP;
        context.SaveChanges();

        #region for Ticket Subcategory PreRequestie


        var List2 = context.Setup_RequestTypeSubcategoryFieldTypeDetail.Where(j => j.FieldValueID == Id && j.FieldValueTypeID == Constant.TS_Setup_TicketPreRequisiteValuesTypeID.ReportedBy).Select(s => new
        {
            RequestTypeSubcategoryFieldTypeId = s.RequestTypeSubcategoryFieldTypeId,

        }).OrderBy(a => a.RequestTypeSubcategoryFieldTypeId).ToList();

        if (List2 != null && List2.Count > 0)
        {

            DataTable ds = new DataTable();
            DAL.TicketSystemEntities context1 = new DAL.TicketSystemEntities();
            string dbConnectionString = context1.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("sp_Setup_RequestTypeSubcategoryFieldTypeDetail_Insert", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@RequestTypeSubcategoryFieldTypeDetailData", SqlDbType.Structured).Value = null;
            da.SelectCommand.Parameters.Add("@userid", SqlDbType.Int).Value = UserId;
            da.SelectCommand.Parameters.Add("@UserIP", SqlDbType.NVarChar).Value = UserIP;
            da.SelectCommand.Parameters.Add("@FieldValue", SqlDbType.NVarChar).Value = txtReportedByName.Text.Trim();
            da.SelectCommand.Parameters.Add("@FieldValueID", SqlDbType.Int).Value = Id;
            da.SelectCommand.Parameters.Add("@OperationTypeId", SqlDbType.Int).Value = Constant.OperationTypeID.Update;
            da.Fill(ds);

            Status = true;
        }

        #endregion

        ClosePopup();
        ResetControls();
        Success("Updated Successfully");
    }

    public void Success(string message)
    {
        message = "AlertBox('Success!','" + message + "','success');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }

    public void Error(string message)
    {
        message = "AlertBox('Error!','" + message + "','error');";
        ScriptManager.RegisterStartupScript(this, GetType(), message, message, true);
    }

    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
    }

    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
    }

    private void ResetControls()
    {
        hfId.Value = "";
        txtReportedByName.Text = "";
        BindRepeater();
    }
}