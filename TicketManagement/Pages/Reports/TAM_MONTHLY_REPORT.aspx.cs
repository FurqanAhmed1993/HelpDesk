using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Transactions;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;

public partial class Pages_Reports_TAM_MONTHLY_REPORT : Base
{
    TicketSystemEntities context = new TicketSystemEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropdown();
        }
    }
    private void BindDropdown()
    {
        try
        {
            DataTable Categorydt = new DataTable();
            Categorydt.Columns.Add("CategoryId", typeof(int));
            Categorydt.Columns.Add("CategoryName", typeof(string));

            DataTable Subcategorydt = new DataTable();
            Subcategorydt.Columns.Add("SubcategoryId", typeof(int));
            Subcategorydt.Columns.Add("SubcategoryName", typeof(string));

            btn_Export.Visible = false;
            string numberOfdays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
            txtTicketDateTo.Text = txtTicketDateFrom.Text = Convert.ToDateTime(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year).ToString(Constant.DateFormat);
            var liStatus = context.TS_Setup_TicketStatus.Where(a => a.IsActive == true && a.StatusId != 2 && a.StatusId != 8).OrderBy(b => b.StatusId).ToList();
            Common.BindCheckBoxList(Chk_Status, liStatus, "Status", "StatusId", true, false);
            for (int i = 0; i < Chk_Status.Items.Count; i++)
            {
                Chk_Status.Items[i].Selected = true;
            }

            var Department = context.Setup_Department.Where(a => a.IsActive == true).OrderBy(b => b.DepartmentName).ToList();
            Common.BindDropDown(ddlDepartment, Department, "DepartmentName", "DepartmentId", true, false);

            var Priority = context.TS_Setup_Priority.Where(a => a.IsActive == true).OrderBy(b => b.PriorityName).ToList();
            Common.BindDropDown(ddlPriority, Priority, "PriorityName", "PriorityId", true, false);

            var RequestType = context.TS_Setup_RequestType.Where(a => a.IsActive == true).OrderBy(b => b.RequestTypeName).ToList();
            Common.BindDropDown(ddlRequestType, RequestType, "RequestTypeName", "RequestTypeId", true, false);

            Common.BindDropDown(ddlCategory, Categorydt, "CategoryName", "CategoryId", true, false);
            Common.BindDropDown(ddlSubcategory, Subcategorydt, "SubcategoryName", "SubcategoryId", true, false);

        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }
    }
  
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=TAM_MONTHLY_REPORT.xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            rpt.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            //divError.Visible = true;
            //lblError.InnerText = ex.Message;
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        BindDropdown();
        divError.Visible = false;
        lblError.InnerText = "";
        txtCustomer.Text = "";
        TotalRecords.Text = "0";
        btn_Export.Visible = false;
        rpt.DataSource = null;
        rpt.DataBind();

    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindRepeater();
    }
  
    private void BindRepeater()
    {
        try
        {
            divError.Visible = false;
            lblError.InnerText = "";
            TotalRecords.Text = "0";
            btn_Export.Visible = false;
            rpt.DataSource = null;
            rpt.DataBind();
            int? Nullint = null;
        
            int? departmentId_ = ddlDepartment.SelectedValue == "0" ? Nullint : Convert.ToInt32(ddlDepartment.SelectedValue);
            int? PriorityId_ = ddlPriority.SelectedValue == "0" ? Nullint : Convert.ToInt32(ddlPriority.SelectedValue);
            int? requestTypeId = ddlRequestType.SelectedValue == "0" ? Nullint : Convert.ToInt32(ddlRequestType.SelectedValue);
            int? categoryId = ddlCategory.SelectedValue == "0" ? Nullint : Convert.ToInt32(ddlCategory.SelectedValue);
            int? subcategoryId = ddlSubcategory.SelectedValue == "0" ? Nullint : Convert.ToInt32(ddlSubcategory.SelectedValue);
            string customer_ = txtCustomer.Text.Trim() == "" ? null : txtCustomer.Text.Trim();
            string StatusId = GetCommaSeparatedCBLValues(Chk_Status);

            int? IntDateFrom = Nullint;
            int? IntDateTo = Nullint;
            if (txtTicketDateFrom.Text.Trim() != "" && txtTicketDateTo.Text.Trim() != "")
            {
                try
                {
                    DateTime TicketDateFrom_ = txtTicketDateFrom.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(txtTicketDateFrom.Text);
                    DateTime TicketDateTo_ = txtTicketDateTo.Text.Trim() == "" ? DateTime.Now : Convert.ToDateTime(txtTicketDateTo.Text);
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
            TicketSystemEntities context_ = new TicketSystemEntities();
            string dbConnectionString = context_.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("TS_SearchTicketNew", con);
            da.SelectCommand.CommandTimeout = 3600;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@USERID", SqlDbType.Int).Value = Nullint;
            da.SelectCommand.Parameters.Add("@InitiatorId", SqlDbType.Int).Value = Nullint;
            da.SelectCommand.Parameters.Add("@AssigneeId", SqlDbType.Int).Value = Nullint;
            da.SelectCommand.Parameters.Add("@PriorityId", SqlDbType.Int).Value = PriorityId_;
            da.SelectCommand.Parameters.Add("@TicketCode", SqlDbType.NVarChar).Value = Nullint;
            da.SelectCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = Nullint;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = departmentId_;
            da.SelectCommand.Parameters.Add("@StatusId", SqlDbType.NVarChar).Value = StatusId;
            da.SelectCommand.Parameters.Add("@IsDeptTicket", SqlDbType.Bit).Value = null;
            da.SelectCommand.Parameters.Add("@TicketCreationFromInt", SqlDbType.Int).Value = IntDateFrom;
            da.SelectCommand.Parameters.Add("@TicketCreationToInt", SqlDbType.Int).Value = IntDateTo;
            da.SelectCommand.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = customer_;
            da.SelectCommand.Parameters.Add("@RequestTypeId", SqlDbType.Int).Value = requestTypeId;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = categoryId;
            da.SelectCommand.Parameters.Add("@SubcategoryId", SqlDbType.Int).Value = subcategoryId;
            da.Fill(dt);
            rpt.DataSource = dt;
            rpt.DataBind();

            if (dt != null && dt.Rows.Count > 0)
            {
                TotalRecords.Text = Convert.ToString(dt.Rows.Count);
                btn_Export.Visible = true;
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }
    public static string GetCommaSeparatedCBLValues(CheckBoxList cbl)
    {

        string value = "";
        foreach (ListItem li in cbl.Items)
        {
            if (li.Selected)
            {
                if (li.Value != "0")
                {
                    value += li.Value + ",";
                }
            }
        }
        return value.Length > 1 ? value : null;
    }
   


    protected void ddlRequestType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int reqId = int.Parse(ddlRequestType.SelectedValue.ToString());
        var CategoryList = context.TS_Setup_RequestTypeCategory.Where(a => a.IsActive == true && a.RequestTypeId == reqId).OrderBy(b => b.CategoryName).ToList();
        Common.BindDropDown(ddlCategory, CategoryList, "CategoryName", "CategoryId", true, false);
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int catId = int.Parse(ddlCategory.SelectedValue.ToString());
        var SubcategoryList = context.TS_Setup_RequestTypeSubcategory.Where(a => a.IsActive == true && a.CategoryId == catId).OrderBy(b => b.SubcategoryName).ToList();
        Common.BindDropDown(ddlSubcategory, SubcategoryList, "SubcategoryName", "SubcategoryId", true, false);
    }
}
