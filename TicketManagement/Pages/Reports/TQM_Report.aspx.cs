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

public partial class Pages_Reports_TQM_Report : Base
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

            txtTicket.Text = txtCustomerName.Text = txtContactNo.Text = txtEmail.Text = "";

            btn_Export.Visible = false;
            string numberOfdays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
            txtTicketDateTo.Text = txtTicketDateFrom.Text = Convert.ToDateTime(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year).ToString(Constant.DateFormat);
            var liStatus = context.TS_Setup_TicketStatus.Where(a => a.IsActive == true && a.StatusId != (int)Constant.TicketStatus.New).OrderBy(b => b.StatusId).ToList();
            Common.BindCheckBoxList(Chk_Status, liStatus, "Status", "StatusId", true, false);
            for (int i = 0; i < Chk_Status.Items.Count; i++)
            {
                Chk_Status.Items[i].Selected = true;
            }

            var priority = context.TS_Setup_Priority.Where(c => c.IsActive == true && c.IsActive == true).ToList();
            var requestType = context.TS_Setup_RequestType.Where(x => x.IsActive == true).OrderBy(x => x.RequestTypeName).ToList();
            var requestMode = context.TS_Setup_RequestMode.Where(x => x.IsActive == true).OrderBy(x => x.RequestMode).ToList();

            Common.BindDropDown(ddlPriority, priority, "PriorityName", "PriorityId", priority.Count == 1 ? false : true, false);
            Common.BindDropDown(ddlRequestType, requestType, "RequestTypeName", "RequestTypeId", requestType.Count == 1 ? false : true, false);
            Common.BindDropDown(ddlRequestMode, requestMode, "RequestMode", "RequestModeId", requestMode.Count == 1 ? false : true, false);
            var category = context.TS_Setup_RequestTypeCategory.Where(x => x.IsActive == true).ToList();
            Common.BindDropDown(ddlCategory, category, "CategoryName", "CategoryId", true, false);
            ddlCategory_SelectedIndexChanged(null, null);


        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message.ToString();
        }

    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int categoryId = int.Parse(ddlCategory.SelectedValue);
            var subCategory = context.TS_Setup_RequestTypeSubcategory.Where(x => x.IsActive == true && x.CategoryId == categoryId).ToList();
            Common.BindDropDown(ddlSubcategory, subCategory, "SubcategoryName", "SubcategoryId", true, false);
        }
        catch (Exception ex)
        {

        }
    }
    protected void btn_Export_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=TQM Report.xls");
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
            DateTime StartDate = Convert.ToDateTime(txtTicketDateFrom.Text.Trim());
            DateTime EndDate = Convert.ToDateTime(txtTicketDateTo.Text.Trim());
            int IntDateFrom = Convert.ToInt32(StartDate.ToString(Constant.IntDateFormat));
            int IntDateTo = Convert.ToInt32(EndDate.ToString(Constant.IntDateFormat));
            int PriorityId_ = ddlPriority.SelectedValue == "0" ? 0 : int.Parse(ddlPriority.SelectedValue);
            int RequestTypeId_ = ddlRequestType.SelectedValue == "0" ? 0 : int.Parse(ddlRequestType.SelectedValue);
            int RequestTypeCategoryId_ = ddlCategory.SelectedValue == "0" ? 0 : int.Parse(ddlCategory.SelectedValue);
            int RequestTypeSubcategoryId_ = ddlSubcategory.SelectedValue == "0" ? 0 : int.Parse(ddlSubcategory.SelectedValue);
            int RequestModeId_ = ddlRequestMode.SelectedValue == "0" ? 0 : int.Parse(ddlRequestMode.SelectedValue);
            string TicketNo = txtTicket.Text.Trim();
            string CustomerName = txtCustomerName.Text.Trim();
            string ContactNo = txtContactNo.Text.Trim();
            string EmailAddress = txtEmail.Text.Trim();
            string StatusId = GetCommaSeparatedCBLValues(Chk_Status);
            DataTable dt = new DataTable();
            TicketSystemEntities context_ = new TicketSystemEntities();
            string dbConnectionString = context_.Database.Connection.ConnectionString;
            SqlConnection con = new SqlConnection(dbConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("TS_SearchTicketNew", con); //RPT_TQM_Report
            da.SelectCommand.CommandTimeout = 3600;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@USERID", SqlDbType.Int).Value = null;
            da.SelectCommand.Parameters.Add("@InitiatorId", SqlDbType.Int).Value = null;
            da.SelectCommand.Parameters.Add("@AssigneeId", SqlDbType.Int).Value = null;
            if (PriorityId_ == 0)
                da.SelectCommand.Parameters.Add("@PriorityId", SqlDbType.Int).Value = null;
            else
                da.SelectCommand.Parameters.Add("@PriorityId", SqlDbType.Int).Value = PriorityId_;

            da.SelectCommand.Parameters.Add("@TicketCode", SqlDbType.NVarChar).Value = TicketNo;
            da.SelectCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = null;
            da.SelectCommand.Parameters.Add("@DepartmentId", SqlDbType.Int).Value = null;
            da.SelectCommand.Parameters.Add("@StatusId", SqlDbType.VarChar).Value = StatusId;
            da.SelectCommand.Parameters.Add("@IsDeptTicket", SqlDbType.Bit).Value = null;
            da.SelectCommand.Parameters.Add("@TicketCreationFromInt", SqlDbType.Int).Value = IntDateFrom;
            da.SelectCommand.Parameters.Add("@TicketCreationToInt", SqlDbType.Int).Value = IntDateTo;
            da.SelectCommand.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = CustomerName;
            if (RequestTypeId_ == 0)
                da.SelectCommand.Parameters.Add("@RequestTypeId", SqlDbType.Int).Value = null;
            else
                da.SelectCommand.Parameters.Add("@RequestTypeId", SqlDbType.Int).Value = RequestTypeId_;

            if (RequestTypeCategoryId_ == 0)
                da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = null;
            else
                da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = RequestTypeCategoryId_;

            if (RequestTypeSubcategoryId_ == 0)
                da.SelectCommand.Parameters.Add("@SubcategoryId", SqlDbType.Int).Value = null;
            else
                da.SelectCommand.Parameters.Add("@SubcategoryId", SqlDbType.Int).Value = RequestTypeSubcategoryId_;

            da.SelectCommand.Parameters.Add("@HfDepartmentId", SqlDbType.Int).Value = null;
            da.SelectCommand.Parameters.Add("@ContactNo", SqlDbType.NVarChar).Value = ContactNo;
            da.SelectCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar).Value = EmailAddress;
            da.SelectCommand.Parameters.Add("@RequestModeId", SqlDbType.Int).Value = RequestModeId_;
            da.SelectCommand.Parameters.Add("@Operation", SqlDbType.VarChar).Value = "TQM";

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


}
