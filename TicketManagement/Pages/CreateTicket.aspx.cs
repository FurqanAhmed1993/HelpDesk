using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_CreateTicket : Base
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hfPhoneNumber.Value = "";
                hfEmailProductId.Value = Convert.ToString((int)Constant.Product.Email);
                //hfCustomerId.Value = Convert.ToString(CustomerId);
                hf_UserId.Value = Convert.ToString(UserId);

                if (!IsLevel_1 && !IsAdmin && !IsSuperAdmin)
                {
                    pnlHideShow.Visible = false;
                }

                if (!string.IsNullOrEmpty(Request.Url.Query.TrimStart('?')))
                {
                    string EncryptedQueryString = Request.Url.Query.TrimStart('?');
                    string CheckTMID = Common.DecryptByIdAES(EncryptedQueryString, "TMID");
                    //bool CheckTMID = Request.QueryString.ToString().Contains("TMID");

                    if (!string.IsNullOrEmpty(CheckTMID))
                    {
                        int TMID = Convert.ToInt32(CheckTMID);
                        hf_TicketMasterId.Value = TMID.ToString();
                        DAL.TicketSystemEntities context = new DAL.TicketSystemEntities();
                        var list = context.TS_TicketMaster.FirstOrDefault(a => a.IsActive == true && a.TicketMasterId == TMID && a.CustomerId != 1);
                        if (list != null)
                        {
                            if (list.CustomerId != null)
                            {
                                int customerId = int.Parse(list.CustomerId.ToString());
                                var customer = context.Customers.FirstOrDefault(x => x.IsActive == true && x.CustomerId == customerId);
                                txtName.Text = customer.CustomerName;
                                txtContact.Text = customer.ContactNo;
                                txtEmail.Text = customer.EmailAddress;
                                txtAddress.Text = customer.Address;
                                txtAlternativeNumber.Text = customer.AlternativeNumber;
                                hfTicketCustomerId.Value = Convert.ToString(customer.CustomerId);

                            }

                            hfStatusId.Value = list.StatusId.ToString();
                            txtDescription.Content = list.Description == null ? "" : list.Description.ToString().Trim();
                        }
                    }

                }

            }
        }
        catch (Exception ex)
        {

        }
    }

}