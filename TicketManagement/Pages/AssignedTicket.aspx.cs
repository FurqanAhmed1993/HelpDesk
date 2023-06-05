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


public partial class Pages_AssignedTicket : Base
{
    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ChatTypeId.Value = Convert.ToString((int)Constant.ChatType.Ticket_Reply_Response);
            BindDropdown();
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

    public void BindAssigneee()
    {
        
            List<int?> ListUnderEmployee = context.TS_EmployeeHirerarchy(UserId).Select(c => c.UserId).ToList();
            var obj = context.TS_TicketMaster.Where(e => e.IsActive == true && e.IsAssigned == true && e.AssigneeId.HasValue == true
            && (e.AssigneeId != UserId)
            && ((e.DepartmentId == DepartmentId || IsAdmin == true || IsSuperAdmin == true)
            && (ListUnderEmployee.Contains(e.AssigneeId) || e.AssigneeId == UserId || (e.TS_Setup_TicketLevel.Sort <= LevelSortNo && LevelSortNo != 0) || IsSuperAdmin == true || IsAdmin == true ))
             ).Select(c => new
             {
                 EmployeeId = c.AssigneeId,
                 Name = c.UserLogin.Full_Name,
             }).Distinct().ToList().ToDataTable();
            obj.Rows.Add(UserId, FullName);
            Common.BindDropDown(ddlAssigneeSearch, obj, "Name", "EmployeeId", true, false);
            ddlAssigneeSearch.SelectedValue = UserId.ToString();
       



    }

    public void BindStatus()
    {
        var ListStatus = context.TS_Setup_TicketStatus.Where(e => e.IsActive == true).OrderBy(c => c.Status).Select(s => new
        {
            Value = s.Status,
            Id = s.StatusId
        }).ToList();

        Common.BindCheckBoxList(chkbxlstStatus, ListStatus, "Value", "Id", true, false);

        if (chkbxlstStatus.Items.Count > 0)
        {
            foreach (ListItem item in chkbxlstStatus.Items)
            {
                item.Selected = true;
            }
        }
    }

    private void BindDropdown()
    {
        BindAssigneee();
        BindStatus();

        

        var ListServicePriority = context.TS_Setup_Priority.Where(c => c.IsActive == true).OrderBy(c => c.PriorityName).ToList();
        Common.BindDropDown(ddlPrioritySearch, ListServicePriority, "PriorityName", "PriorityId", true, false);

    }

    private void BindRepeater()
    {
        try
        {
            int? NullInt = null;
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

            List<int?> ListUnderEmployee = context.TS_EmployeeHirerarchy(UserId).Select(c => c.UserId).ToList();
            string PrimaryIP = txtPrimaryIP.Text.Trim() == "" ? null : txtPrimaryIP.Text.Trim();
            string CAMNo = txtCAMNo.Text.Trim() == "" ? null : txtCAMNo.Text.Trim();
            string TicketNo = txtTicket.Text.Trim() == "" ? null : txtTicket.Text.Trim();
            string Title = txtTitleSearch.Text.Trim() == "" ? null : txtTitleSearch.Text.Trim();
            int? AssigneeId = ddlAssigneeSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlAssigneeSearch.SelectedValue);
            int? PriorityId = ddlPrioritySearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlPrioritySearch.SelectedValue);
            int? CustomerTypeId = ddlCustomerTypeSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlCustomerTypeSearch.SelectedValue);
            int? CustomerId_ = ddlCustomerSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlCustomerSearch.SelectedValue);
            int? AddressId = ddlAddressSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlAddressSearch.SelectedValue);
            bool UnAssignedBoolSearch = chkbxUnAssignedTicket.Checked;
            int?[] ArrStatusId = null;
            string StatusIdStr = "";
            foreach (ListItem chkbx in chkbxlstStatus.Items)
            {
                if (chkbx.Text != "All")
                {
                    if (chkbx.Selected == true)
                    {
                        StatusIdStr += chkbx.Value + ",";
                    }
                }
            }
            if (StatusIdStr.Length > 1)
            {
                StatusIdStr = StatusIdStr.Substring(0, StatusIdStr.Length - 1);
                ArrStatusId = StatusIdStr.Split(',')
                    .Select(x =>
                    {
                        int value;
                        return int.TryParse(x, out value) ? value : (int?)null;
                    })
                    .ToArray();
            }
            else
            {
                StatusIdStr = "0";
                StatusIdStr = StatusIdStr.Substring(0, StatusIdStr.Length - 1);
                ArrStatusId = StatusIdStr.Split(',')
                    .Select(x =>
                    {
                        int value;
                        return int.TryParse(x, out value) ? value : (int?)null;
                    })
                    .ToArray();
            }
            var List = context.TS_TicketMaster.Where(e => e.IsActive == true && e.IsAssigned == true
            
            && (e.PriorityId == PriorityId || PriorityId == null)
            && (e.TicketCode == TicketNo || TicketNo == null)
           
            && (e.Tittle.Contains(Title) || Title == null)
            && (ArrStatusId.Contains(e.StatusId))
            //&& (e.AssigneeId == AssigneeId || AssigneeId == null)
            && ((e.DepartmentId == DepartmentId && e.AssigneeId.HasValue == false) || UnAssignedBoolSearch == false)
             && (ListUnderEmployee.Contains(e.AssigneeId) || e.AssigneeId == UserId || (e.LevelId == null || e.TS_Setup_TicketLevel.Sort <= LevelSortNo && LevelSortNo != 0))
            )
             .Select(c => new
                {
                   
                    TicketMasterId = c.TicketMasterId,
                    TicketCode = c.TicketCode,
                    TicketCreationDate = c.CreatedDate,
                    TicketTime = c.TicketCreationTime,
                    TicketInitiatorId = c.InitiatorId,
                    TicketInitiatorName = c.UserLogin1.Full_Name,
                    TicketStatusId = c.StatusId,
                    TicketStatus = c.TS_Setup_TicketStatus.Status,
                    TicketRequestModeId = c.RequestModeId,
                    TicketRequestMode = c.TS_Setup_RequestMode.RequestMode,
                    TicketPriorityId = c.PriorityId,
                    TicketPriority = c.TS_Setup_Priority.PriorityName,
                    TicketTitle = c.Tittle,
                    TicketDescription = c.Description,
                    TicketDepartmentId = c.DepartmentId,
                    TicketAssigneeId = c.AssigneeId,
                    Assignee = c.UserLogin.Full_Name,
                    AssignTo = c.Setup_Department.DepartmentName + (c.AssigneeId == null ? "" : "<ul> <li>" + c.UserLogin.Full_Name + "</li> </ul>"),
                    Department = c.Setup_Department.DepartmentName,
                    LevelName = c.TS_Setup_TicketLevel.LevelName,
                    BackgroundColor = c.TS_Setup_TicketStatus.BackgroundColor,
                   
                    TicketType = c.TS_Setup_TicketType == null ? "" : c.TS_Setup_TicketType.TicketTypeName,
                    RequestType = c.TS_Setup_RequestType == null ? "" : c.TS_Setup_RequestType.RequestTypeName,
                    LevelId = c.LevelId,
                    IsFlagMark = c.IsFlagMark,
                    IsFlagMarkVisible = c.AssigneeId == UserId ? true : false,
                    
                    MeetingCount = c.TS_TicketMeeting.Where(f => f.IsActive == true && f.TicketMasterId == c.TicketMasterId).Count(),
                    TaskCount = c.TS_TaskMaster.Where(f => f.IsActive == true && f.TicketMasterId == c.TicketMasterId).Count(),
                    IsEdit = (c.StatusId == (int)Constant.TicketStatus.New && c.InitiatorId == UserId) ? "visible" : "hidden",
                    ResponseVisible = (c.InitiatorId == UserId) ? "visible" : "hidden",
                }).OrderByDescending(a => a.TicketCreationDate).ThenByDescending(a => a.TicketTime).ToList();
            var List1 = List.OrderByDescending(a => a.TicketCreationDate).ThenByDescending(a => a.TicketTime).Skip(skip).Take(pageSize).ToList();
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
        chkbxUnAssignedTicket.Checked = false;
        txtTicket.Text = txtPrimaryIP.Text = txtCAMNo.Text = "";
        ddlCustomerTypeSearch.SelectedValue = "0";
        ddlPrioritySearch.SelectedValue = "0";
        BindAssigneee();
        BindStatus();
        ddlCustomerTypeSearch_SelectedIndexChanged(null, null);
        BindRepeater();
    }

    protected void ddlCustomerTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ddlCustomerSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }




}