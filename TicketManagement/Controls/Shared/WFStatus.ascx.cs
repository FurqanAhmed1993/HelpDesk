using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
using System.Linq;

public partial class Controls_WF_WFStatus : System.Web.UI.UserControl
{
    TicketSystemEntities context = new TicketSystemEntities();

    public object MasterId
    {
        get { return Convert.ToString(ViewState["MasterId"]); }
        set { ViewState["MasterId"] = value; }
    }

    public string Type
    {
        get { return Convert.ToString(ViewState["Type"]); }
        set { ViewState["Type"] = value; }
    }

    public string Status
    {
        get { return StatusImage1.Status; }
        set { StatusImage1.Status = value; }
    }

    public bool ShowViewStatus
    {
        get { return spnViewStatus.Visible; }
        set { spnViewStatus.Visible = value; }
    }

    public string Title
    {
        get { return spnViewStatus.InnerText; }
        set { spnViewStatus.InnerText = value; }
    }

    public string ExtraText
    {
        get { return lblExtraText.Text; }
        set
        {
            lblExtraText.Text = value;
            if (value.Trim().Length > 0)
                tblExtraText.Visible = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (MasterId == null)
        {
            StatusImage1.StatusType = Controls_StatusImage.STATUS_TYPE.UNKNOWN;
            return;
        }
        BindRepeater();
    }

    private string lastStatusInTheSteps;

    protected void GridView1_DataBinding(object sender, EventArgs e)
    {
        lastStatusInTheSteps = "";
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView data = (DataRowView)e.Row.DataItem;

            // we save in status the last work flow status
            // if we find a rejected then that is the last one for sure:
            if (!data["Status"].ToString().Equals("")
                && !lastStatusInTheSteps.ToUpper().Equals("REJECTED"))
                lastStatusInTheSteps = data["Status"].ToString();


            // string temp = e.Row.Cells[2].Text.ToString();
            //  if (temp.Length > 50)
            // e.Row.Cells[2].Text = temp.Substring(0, 49) + "...";
        }
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Status))
        {
            // the status property of the control hasn't been set
            // we'll use what we got form the steps:
            Status = lastStatusInTheSteps;
        }
    }

    private void BindRepeater()
    {
        if (Type == "Ticket")
        {
            int _TicketMasterId = MasterId == "" ? 0 : Convert.ToInt32(MasterId);
            DataTable ListStatus = context.TS_TicketDetail.Where(e => e.IsActive == true && e.TicketMasterId == _TicketMasterId).Select(c => new
            {
                MasterId = c.TicketMasterId,
                DetailId = c.TicketDetailId,
                Status = c.TS_Setup_TicketStatus.Status.Trim(),
                StatusId = c.StatusId,
                Assignee = c.EmailFrom != null ? c.EmailFrom : c.UserLogin.Full_Name,
                AssigneeId = c.AssigneeId,
                CreatedDate = c.CreatedDate,
                Description = c.Description
            }).ToList().ToDataTable();
            grdWFSteps.DataSource = ListStatus;
            grdWFSteps.DataBind();
        }
        else if (Type == "Task")
        {
            int _TaskMasterId = MasterId == "" ? 0 : Convert.ToInt32(MasterId);
            DataTable ListStatus = context.TS_TaskDetail.Where(e => e.IsActive == true && e.TaskMasterId == _TaskMasterId).Select(c => new
            {
                MasterId = c.TaskMasterId,
                DetailId = c.TaskDetailId,
                Status = c.TS_Setup_TicketStatus.Status.Trim(),
                StatusId = c.StatusId,
                Assignee = c.UserLogin1.Full_Name,
                AssigneeId = c.AssigneeTo,
                CreatedDate = c.CreatedDate,
                Description = c.Description
            }).ToList().ToDataTable();
            grdWFSteps.DataSource = ListStatus;
            grdWFSteps.DataBind();
            //grdWFSteps.Columns[]



        }
    }
}
