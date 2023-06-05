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
public partial class Pages_Reports_DeletedTickets : Base
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
            btn_Export.Visible = false;
            string numberOfdays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month).ToString();
            txtTicketDateTo.Text = txtTicketDateFrom.Text = Convert.ToDateTime(DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year).ToString(Constant.DateFormat);

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
            Response.AddHeader("content-disposition", "attachment; filename=Deleted_Tickets_Report.xls");
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
            if (txtTicketDateFrom.Text.Trim() != "" && txtTicketDateTo.Text.Trim() != "")
            {

                divError.Visible = false;
                lblError.InnerText = "";
                TotalRecords.Text = "0";
                btn_Export.Visible = false;
                rpt.DataSource = null;
                rpt.DataBind();
                DateTime StartDate = Convert.ToDateTime(txtTicketDateFrom.Text.Trim());
                DateTime EndDate = Convert.ToDateTime(txtTicketDateTo.Text.Trim());
                int IntDateFrom = Convert.ToInt32(StartDate.ToString(Constant.IntDateFormat));
                int IntDateTo = Convert.ToInt32(EndDate.ToString(Constant.IntDateFormat));
                DataTable dt = new DataTable();
                TicketSystemEntities context_ = new TicketSystemEntities();
                string dbConnectionString = context_.Database.Connection.ConnectionString;
                SqlConnection con = new SqlConnection(dbConnectionString);
                SqlDataAdapter da = new SqlDataAdapter("TS_DeletedTickets", con);
                da.SelectCommand.CommandTimeout = 3600;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.Add("@TicketMasterId", SqlDbType.Int).Value = null;
                da.SelectCommand.Parameters.Add("@TicketCreationFromInt", SqlDbType.Int).Value = IntDateFrom;
                da.SelectCommand.Parameters.Add("@TicketCreationToInt", SqlDbType.Int).Value = IntDateTo;
                da.Fill(dt);
                rpt.DataSource = dt;
                rpt.DataBind();

                if (dt != null && dt.Rows.Count > 0)
                {
                    TotalRecords.Text = Convert.ToString(dt.Rows.Count);
                    btn_Export.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            divError.Visible = true;
            lblError.InnerText = ex.Message;
        }
    }

}
