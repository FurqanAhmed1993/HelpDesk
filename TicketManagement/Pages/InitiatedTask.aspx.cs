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

public partial class Pages_InitiatedTask : Base
{
    TicketSystemEntities context = new TicketSystemEntities();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ChatTypeId.Value = Convert.ToString((int)Constant.ChatType.Task_Reply_Response);
            BindDropdown();
            BindRepeater();
        } PagingHandler();
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

    private void BindDropdown()
    {
        if (IsAdmin || IsSuperAdmin)
        {
            var List = context.UserLogins.Where(x => x.Is_Active == true && (x.User_Code == UserId))
            .OrderBy(x => x.Full_Name).Select(s => new
            {
                Value = s.Full_Name,
                Id = s.User_Code
            }).ToList();
            Common.BindDropDown(ddlInitiatorSearch, List, "Value", "Id", true, false);
            ddlInitiatorSearch.SelectedValue = UserId.ToString();
        }
        else
        {
            var List = context.TS_EmployeeHirerarchy(UserId).OrderBy(x => x.Name).Select(s => new
            {
                Value = s.Name,
                Id = s.UserId
            }).ToList();
            Common.BindDropDown(ddlInitiatorSearch, List, "Value", "Id", true, false);
            ddlInitiatorSearch.SelectedValue = UserId.ToString();
        }

        var ListStatus = context.TS_Setup_TicketStatus.Where(e => e.IsActive == true).OrderBy(c => c.Status).Select(s => new
        {
            Value = s.Status,
            Id = s.StatusId
        }).ToList();
        Common.BindDropDown(ddlStatusSearch, ListStatus, "Value", "Id", true, false);


        var ListServicePriority = context.TS_Setup_Priority.Where(c => c.IsActive == true).OrderBy(c => c.PriorityName).ToList();
        Common.BindDropDown(ddlPrioritySearch, ListServicePriority, "PriorityName", "PriorityId", true, false);

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

            int InitiatorId = 0;
            int PriorityId = 0;
            int StatusId = 0;
            string TicketNo = txtTicketNo.Text.Trim();
            string Title = txtTitle.Text.Trim();
            if (ddlInitiatorSearch.SelectedIndex != -1)
                InitiatorId = Convert.ToInt32(ddlInitiatorSearch.SelectedItem.Value);
            if (ddlPrioritySearch.SelectedIndex != -1)
                PriorityId = Convert.ToInt32(ddlPrioritySearch.SelectedItem.Value);
            if (ddlStatusSearch.SelectedIndex != -1)
                StatusId = Convert.ToInt32(ddlStatusSearch.SelectedItem.Value);
            List<int?> ListUnderEmployee = context.TS_EmployeeHirerarchy(UserId).Select(c => c.UserId).ToList();
            var List = context.TS_TaskMaster.Where(e => e.IsActive == true
                  && (e.TS_TicketMaster.TicketCode.Contains(TicketNo) || TicketNo == string.Empty)
                    && (e.TaskTitle.Contains(Title) || Title == string.Empty)
                    && (e.StatusId == StatusId || StatusId == 0)
                    && (e.PriorityId == PriorityId || PriorityId == 0)
                    && (e.InitiatorId == InitiatorId || InitiatorId == 0)
                    && (e.InitiatorId == UserId || ListUnderEmployee.Contains(e.InitiatorId) || IsAdmin == true || IsSuperAdmin == true)
                ).Select(c => new
                {
                    TicketNo = c.TS_TicketMaster.TicketCode,
                    TicketMasterId = c.TicketMasterId,
                    TaskMasterId = c.TaskMasterId,
                    TicketCreationDate = c.CreatedDate,
                    InitiatorId = c.InitiatorId,
                    StatusId = c.StatusId,
                    Status = c.TS_Setup_TicketStatus.Status,
                    PriorityId = c.PriorityId,
                    Priority = c.TS_Setup_Priority.PriorityName,
                    Title = c.TaskTitle,
                    Description = c.Description,
                    AssigneeId = c.AssigneeId,
                    Initiator = c.UserLogin1.Full_Name,
                    Assignee = c.UserLogin.Full_Name,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    DepartmentId = c.DepartmentId,
                    Department = c.Setup_Department.DepartmentName,
                    ChatNotificationCount = c.TS_ChatLog.Where(b => b.IsActive == true && b.CreatedBy != UserId && b.TaskMasterId == c.TaskMasterId && b.ChatTypeId == (int)Constant.ChatType.Task_Reply_Response && !b.TS_ChatLogReadDetail.Any(f => f.ChatLogId == b.ChatLogId && f.IsActive == true && f.CreatedBy == UserId)).Count(),
                    AssignTo = c.Setup_Department.DepartmentName + (c.AssigneeId == null ? "" : "<ul> <li>" + c.UserLogin.Full_Name + "</li> </ul>"),
                }).ToList().OrderByDescending(a => a.TicketCreationDate).ThenByDescending(a => a.TaskMasterId);


            var List1 = List.OrderByDescending(a => a.TicketCreationDate).ThenByDescending(a => a.TaskMasterId).Skip(skip).Take(pageSize).ToList();
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

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindRepeater();

    }

    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        txtTicketNo.Text = "";
        txtTitle.Text = "";
        ddlInitiatorSearch.SelectedValue = "0";
        ddlStatusSearch.SelectedValue = "0";
        ddlPrioritySearch.SelectedValue = "0";
        BindRepeater();
    }

    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{

        //    string hfDepartment = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hfDepartment")).Value;
        //    string hfAssignee = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hfAssignee")).Value;
        //    (((Label)e.Item.FindControl("lblAssignee")).Text) = hfDepartment;
        //    if (hfAssignee != "")
        //    {
        //        ((Label)e.Item.FindControl("lblAssignee")).Text += "<ul  style='padding-left:20px;'>";
        //        (((Label)e.Item.FindControl("lblAssignee")).Text) += "<li>" + hfAssignee + "</li>";
        //        ((Label)e.Item.FindControl("lblAssignee")).Text += "</ul>";
        //    }
        //}
    }



}