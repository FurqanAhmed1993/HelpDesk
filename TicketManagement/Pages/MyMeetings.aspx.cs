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



public partial class Pages_MyMeetings : Base
{
    TicketSystemEntities context = new TicketSystemEntities();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
            var objMeetingDetail = context.TS_TicketMeetingDetail.Where(c => c.IsActive == true && c.IsRead == false && c.AttendeeId == UserId).ToList();
            objMeetingDetail.ForEach(a => a.IsRead = true);
            context.SaveChanges();

            int TicketDate = txtMeetingDate.Text.Trim() == "" ? 0 : Convert.ToInt32(Convert.ToDateTime(txtMeetingDate.Text.Trim()).ToString(Constant.IntDateFormat));
            string Agenda = txtMeetingAgenda.Text.Trim();
            string TicketNo = txtTicketNo.Text.Trim();
            var List = context.TS_TicketMeeting.Where(c => c.IsActive == true
             && (c.TS_TicketMeetingDetail.Any(e => e.AttendeeId == UserId) || IsAdmin == true || IsSuperAdmin == true)
             && (c.MeetingAgenda.Contains(Agenda) || Agenda == string.Empty)
             && (c.CreatedDateInt == TicketDate || TicketDate == 0)
             && (c.TS_TicketMaster.TicketCode == TicketNo || TicketNo == string.Empty)).ToList().Select(c => new
                {
                    TicketMasterId = c.TicketMasterId,
                    TicketCode = c.TS_TicketMaster.TicketCode,
                    TicketMeetingId = c.TicketMeetingId,
                    StartTime = c.MeetingStartTime,
                    EndTime = c.MeetingEndTime,
                    MeetingDate = c.MeetingDate,
                    MeetingAgenda = c.MeetingAgenda,
                    MeetingDetail = c.MeetingDetail,
                    Description = c.Description,
                    Location = c.Location,
                    Attendee = string.Join(",", context.TS_TicketMeetingDetail.Where(a => a.IsActive == true && a.TicketMeetingId == c.TicketMeetingId).Select(g => g.UserLogin.Full_Name)),
                }).ToList().OrderByDescending(a => a.MeetingDate).ThenByDescending(a => a.TicketMeetingId);

            var List1 = List.OrderByDescending(a => a.MeetingDate).ThenByDescending(a => a.TicketMeetingId).Skip(skip).Take(pageSize).ToList();
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
        txtMeetingDate.Text = "";
        txtMeetingAgenda.Text = "";
        txtTicketNo.Text = "";
        BindRepeater();
    }

    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string hfAttendee = ((System.Web.UI.HtmlControls.HtmlInputHidden)e.Item.FindControl("hfAttendee")).Value;
            if (hfAttendee != "")
            {
                ((Label)e.Item.FindControl("lblAttendee")).Text += "<ul  style='padding-left:20px;'>";

                string[] Split = hfAttendee.Split(',');
                for (int i = 0; i < Split.Length; i++)
                {
                    (((Label)e.Item.FindControl("lblAttendee")).Text) += "<li>" + Split[i] + "</li>";
                }
                ((Label)e.Item.FindControl("lblAttendee")).Text += "</ul>";
            }
        }
    }


}