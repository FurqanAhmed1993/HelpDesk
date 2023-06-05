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


public partial class Pages_InitiatedTicket : Base
{
    TicketSystemEntities context = new TicketSystemEntities();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Ticket_Reply_Response.Value = Convert.ToString((int)Constant.ChatType.Ticket_Reply_Response);
            Ticket_Internel_Chat.Value = Convert.ToString((int)Constant.ChatType.Ticket_Internel_Chat);
            BindDropdown();
            BindRepeater();
            if (CustomerId > 0)
            {
                btnCreateTicket.Visible = false;
            }
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

    public void BindInitiator()
    {
        var List = context.UserLogins.Where(x => x.Is_Active == true && (IsClient == true ? x.User_Code == UserId : true))
            .OrderBy(x => x.Full_Name).Select(s => new
            {
                Value = s.Full_Name,
                Id = s.User_Code
            }).ToList();
        Common.BindDropDown(ddlInitiatorSearch, List, "Value", "Id", List.Count == 1 ? false : true, false);
        if (IsClient == true)
        {
            ddlInitiatorSearch.SelectedValue = UserId.ToString();
        }
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
        BindInitiator();
        BindStatus();
       

        var ListServicePriority = context.TS_Setup_Priority.Where(c => c.IsActive == true).OrderBy(c => c.PriorityName).ToList();
        Common.BindDropDown(ddlPrioritySearch, ListServicePriority, "PriorityName", "PriorityId", true, false);

        var Department = context.Setup_Department.Where(a => a.IsActive == true)
         .Select(s => new
         {
             Value = s.DepartmentName,
             Id = s.DepartmentId
         }).Distinct().ToList().OrderBy(a => a.Value).ToList();
        Common.BindDropDown(ddlDepartmentSearch, Department, "Value", "Id", true, false);
        ddlDepartmentSearch_SelectedIndexChanged(null, null);
    }

    private void BindRepeater()
    {
        try
        {
            int? NullInt = null;
            bool? Nullbool = null;
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

            #region Parameters
            //List<int?> ListUnderEmployee = context.TS_EmployeeHirerarchy(UserId).Select(c => c.UserId).ToList();
            string PrimaryIP = txtPrimaryIP.Text.Trim() == "" ? null : txtPrimaryIP.Text.Trim();
            string CAMNo = txtCAMNo.Text.Trim() == "" ? null : txtCAMNo.Text.Trim();
            string TicketNo = txtTicket.Text.Trim() == "" ? null : txtTicket.Text.Trim();
            string Title = txtTitleSearch.Text.Trim() == "" ? null : txtTitleSearch.Text.Trim();
            int? InitiatorId = ddlInitiatorSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlInitiatorSearch.SelectedValue);
            int? DepartmentId = ddlDepartmentSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlDepartmentSearch.SelectedValue);
            int? AssigneeId = ddlAssigneeSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlAssigneeSearch.SelectedValue);
            int? PriorityId = ddlPrioritySearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlPrioritySearch.SelectedValue);
            int? CustomerTypeId = ddlCustomerTypeSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlCustomerTypeSearch.SelectedValue);
            int? CustomerId_ = ddlCustomerSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlCustomerSearch.SelectedValue);
            int? AddressId = ddlAddressSearch.SelectedValue == "0" ? NullInt : Convert.ToInt32(ddlAddressSearch.SelectedValue);
            bool? UnAssignedBoolSearch = chkbxUnAssignedTicket.Checked == false ? Nullbool : false;
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

            #endregion

            var List = context.TS_SearchTicket(UserId, InitiatorId, AssigneeId, CustomerTypeId, CustomerId_, AddressId, PriorityId,
                TicketNo, PrimaryIP, CAMNo, Title, DepartmentId, StatusIdStr, null, UnAssignedBoolSearch, IsClient).ToList();
            var List1 = List.OrderByDescending(a => a.TicketCreationDate).ThenByDescending(a => a.TicketCreationTime).Skip(skip).Take(pageSize).ToList();
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
        txtTitleSearch.Text = txtTicket.Text = txtPrimaryIP.Text = txtCAMNo.Text = "";
        ddlCustomerTypeSearch.SelectedValue = "0";
        ddlPrioritySearch.SelectedValue = "0";
        ddlDepartmentSearch.SelectedValue = "0";
        BindInitiator();
        BindStatus();
        ddlDepartmentSearch_SelectedIndexChanged(null, null);
        ddlCustomerTypeSearch_SelectedIndexChanged(null, null);
        BindRepeater();
    }

    protected void ddlCustomerTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ddlCustomerSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlDepartmentSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        int DepartmentId = Convert.ToInt32(ddlDepartmentSearch.SelectedValue == "" ? "0" : ddlDepartmentSearch.SelectedValue);
        var List = context.UserLogins.Where(x => x.Is_Active == true && x.Setup_Employee2.DepartmentId == DepartmentId)
            .OrderBy(x => x.Full_Name).Select(s => new
            {
                Value = s.Full_Name,
                Id = s.User_Code
            }).ToList();
        Common.BindDropDown(ddlAssigneeSearch, List, "Value", "Id", true, false);
    }

    protected void btn_TicketHistory_Click(object sender, ImageClickEventArgs e)
    {
        //try
        //{
        //    divError_.Visible = false;
        //    lblError_.InnerText = "";
        //    ImageButton BtnTicketHistory = (ImageButton)sender;
        //    RepeaterItem rptItem = (RepeaterItem)BtnTicketHistory.NamingContainer;
        //    int hfTicketMasterIdRpt = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfTicketMasterIdRpt")).Value);
        //    int hfProductId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfProductId")).Value);
        //    int hfManageSevicesMasterId = int.Parse(((System.Web.UI.HtmlControls.HtmlInputHidden)rptItem.FindControl("hfManageSevicesMasterId")).Value);
           
        //    var ListStatus = context.TS_TicketMaster.Where(g => g.IsActive == true
        //        && g.TicketMasterId < hfTicketMasterIdRpt
        //        && g.ProductId == hfProductId
        //        && g.ManageSevicesMasterId == hfManageSevicesMasterId
        //        ).Select(c => new
        //        {
        //            MasterId = c.TicketMasterId,
        //            Status = c.TS_Setup_TicketStatus.Status.Trim(),
        //            StatusId = c.StatusId,
        //            TicketCode = c.TicketCode,
        //            CreatedDate = c.CreatedDate,
        //            Description = c.Description,
        //            imageUrl = c.TS_Setup_TicketStatus.imageUrl,
        //        }).Take(5).OrderByDescending(f => f.MasterId).ToList();
        //    rpt_History.DataSource = ListStatus;
        //    rpt_History.DataBind();
        //    OpenPopup();
        //}
        //catch (Exception ex)
        //{
        //    divError_.Visible = true;
        //    lblError_.InnerText = "Error : " + ex.Message;
        //}
    }

    public void ClosePopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ClosePopup()", "ClosePopup();", true);
    }

    public void OpenPopup()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "OpenPopup()", "OpenPopup();", true);
    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        rpt_History.DataSource = null;
        rpt_History.DataBind();
        ClosePopup();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        TicketService obj = new TicketService();
        obj.SendTestEmail();
    }
}