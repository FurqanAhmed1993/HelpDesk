using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;

public partial class Pages_Setup_Setup_OGA : Base
{

    TicketSystemEntities context = new TicketSystemEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRepeater();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtOGASearch.Text = "";
        ResetControls();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
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
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }

    }

    private void Update()
    {
        DateTime dt = DateTime.Now;
        int Id = Convert.ToInt32(hfId.Value);
        Setup_TicketPreRequisite_Values Obj = context.Setup_TicketPreRequisite_Values.FirstOrDefault(j => j.Id == Id);
        Obj.Name = txtOGA.Text.Trim();
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIP = UserIP;
        context.SaveChanges();

        bool Status = false;

        #region for Ticket Subcategory PreRequestie

        var List2 = context.Setup_RequestTypeSubcategoryFieldTypeDetail.Where(j => j.FieldValueID == Id && j.FieldValueTypeID == Constant.TS_Setup_TicketPreRequisiteValuesTypeID.OGA).Select(s => new
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
            da.SelectCommand.Parameters.Add("@FieldValue", SqlDbType.NVarChar).Value = txtOGA.Text.Trim();
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

    private void Add()
    {
        DateTime dt = DateTime.Now;
        Setup_TicketPreRequisite_Values Obj = new Setup_TicketPreRequisite_Values();
        Obj.Name = txtOGA.Text.Trim();
        Obj.FormType = Constant.TS_Setup_TicketPreRequisiteValuesTypeID.OGA;
        Obj.IsActive = true;
        Obj.CreatedBy = UserId;
        Obj.CreatedDate = dt;
        Obj.UserIP = UserIP;
        context.Setup_TicketPreRequisite_Values.Add(Obj);
        context.SaveChanges();

        bool Status = false;
        DataTable dtPreReqValues = new DataTable();
        dtPreReqValues.Columns.Add("RequestTypeSubcategoryFieldTypeId", typeof(int));
        dtPreReqValues.Columns.Add("FieldValue", typeof(string));
        dtPreReqValues.Columns.Add("FieldValueID", typeof(int));
        dtPreReqValues.Columns.Add("FieldValueTypeID", typeof(int));

        var List = context.Setup_RequestTypeSubcategoryFieldType.Where(j => j.FieldTypeId == Constant.TS_Setup_TicketPreRequisiteValuesTypeID.OGA && (j.InputClass == "OGA" || j.InputClass == "OGA2")).Select(s => new
        {
            RequestTypeSubcategoryFieldTypeId = s.RequestTypeSubcategoryFieldTypeId,

        }).OrderBy(a => a.RequestTypeSubcategoryFieldTypeId).ToList();


        var AuthorityList = context.Setup_TicketPreRequisite_Values.Where(j => j.FormType == 3 && j.IsActive == true).Select(s => new
        {
            Id = s.Id,
            Name = s.Name,

        }).OrderBy(a => a.Name).ToList();


        if (List != null && List.Count > 0)
        {

            DataTable dtValues = new DataTable();
            dtValues.Columns.Add("RequestTypeSubcategoryFieldTypeId" , typeof(int));
            dtValues.Columns.Add("FieldValue", typeof(string));
            dtValues.Columns.Add("FieldValueID", typeof(int));
            dtValues.Columns.Add("FieldValueTypeID", typeof(int));

            for (int i = 0; i < List.Count; i++)
            {
                #region for Ticket Subcategory PreRequestie

                int RequestTypeSubcategoryFieldTypeID = Convert.ToInt32(List[i].RequestTypeSubcategoryFieldTypeId.ToString());


                dtValues.Rows.Add(RequestTypeSubcategoryFieldTypeID , txtOGA.Text , Convert.ToInt32(Obj.Id), Constant.TS_Setup_TicketPreRequisiteValuesTypeID.OGA);

       
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

        if (Status == true)
        {
            ClosePopup();
            ResetControls();
            Success("Added Successfully");
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        ResetControls();
        ClosePopup();
    }

    protected void lbEdit_Click(object sender, EventArgs e)
    {
        try
        {
            divError_.Visible = false;
            lblError_.InnerText = "";
            LinkButton btnEdit = (LinkButton)sender;
            RepeaterItem rptItem = (RepeaterItem)btnEdit.NamingContainer;
            int hfRId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfRId")).Value);
            var List = context.Setup_TicketPreRequisite_Values.FirstOrDefault(j => j.Id == hfRId);
            if (List != null)
            {
                hfId.Value = hfRId.ToString();
                txtOGA.Text = List.Name.Trim();
                OpenPopup();
            }
        }

        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }

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
    private void ResetControls()
    {
        hfId.Value = "";
        txtOGA.Text = "";
        //isDefault.Checked = false;
        BindRepeater();
    }

    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
    }

    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
    }

    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            divError_.Visible = false;
            lblError_.InnerText = "";
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

                bool Status = false ;
                #region for Ticket Subcategory PreRequestie

                var List2 = context.Setup_RequestTypeSubcategoryFieldTypeDetail.Where(j => j.FieldValueID == hfRId && j.FieldValueTypeID == Constant.TS_Setup_TicketPreRequisiteValuesTypeID.OGA).Select(s => new
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

                    Status = true;

                }

                #endregion

                Success("Deleted successfully");
                ResetControls();
                scope.Complete();
            }

        }

        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }

    }

    private void BindRepeater()
    {
        try
        {
            divError_.Visible = false;
            lblError_.InnerText = "";

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


            string Authority = txtOGASearch.Text.ToString();
            var List = context.Setup_TicketPreRequisite_Values.Where(a => a.IsActive == true
                 && (a.Name.Contains(Authority) || Authority == String.Empty) && a.FormType == Constant.TS_Setup_TicketPreRequisiteValuesTypeID.OGA)
                 .Select(c => new
                 {
                     Id = c.Id,
                     Name = c.Name,

                 }).OrderBy(a => a.Id)
                .ToList();
            var List1 = List.OrderByDescending(a => a.Id).Skip(skip).Take(pageSize).ToList();
            rpt.DataSource = List1;
            rpt.DataBind();
            PagingAndSorting.setPagingOptions(List.Count());


        }
        catch (Exception ex)
        {
            divError_.Visible = true;
            lblError_.InnerText = "Error : " + ex.Message;
        }
    }
}