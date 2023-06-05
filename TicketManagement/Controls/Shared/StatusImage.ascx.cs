using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_StatusImage : System.Web.UI.UserControl
{
    public enum STATUS_TYPE
    {
        NEW,
        CANCEL,
        CLOSED,
        INPROGRESS,
        RESOLVED,
        ONHOLD,
        REOPEN,
        JUNK,
        UNKNOWN,
    }

    private string status;

    private STATUS_TYPE? statusType;

    public STATUS_TYPE? StatusType
    {
        get { return statusType; }
        set
        {
            statusType = value;
            string statusDescription;
            Image.ImageUrl = getStatusImage(statusType, out statusDescription);
            Image.ToolTip = statusDescription;
        }
    }

    public string Status
    {
        get { return status; }
        set
        {
            status = value;
            StatusType = getStatusType(status);
        }
    }

    public string getStatusImage(STATUS_TYPE? statusType, out string statusDescription)
    {
        try
        {
            switch (statusType)
            {
                case STATUS_TYPE.NEW:
                    statusDescription = "New";
                    return "/Images/Status/Icons/New.png";//"/Images/Status/new-icon.png";
                case STATUS_TYPE.CANCEL:
                    statusDescription = "Cancel";
                    return "/Images/Status/Icons/Cancel.png";//"/Images/Status/cancel-icon1.png";
                case STATUS_TYPE.CLOSED:
                    statusDescription = "Closed";
                    return "/Images/Status/Icons/Closed.png";//"/Images/Status/close-icon.png";
                case STATUS_TYPE.INPROGRESS:
                    statusDescription = "In-Progress";
                    return "/Images/Status/Icons/Working.png";//"/Images/Status/working-icon.png";
                case STATUS_TYPE.RESOLVED:
                    statusDescription = "Resolved";
                    return "/Images/Status/Icons/Resolved.png";//"/Images/Status/resolve-icon.png";
                case STATUS_TYPE.ONHOLD:
                    statusDescription = "On Hold";
                    return "/Images/Status/Icons/Onhold.png";//"/Images/Status/onhold-icon.png";
                case STATUS_TYPE.REOPEN:
                    statusDescription = "Re-Open";
                    return "/Images/Status/Icons/Reopen.png";//"/Images/Status/ReOpen.png";
                case STATUS_TYPE.JUNK:
                    statusDescription = "Junk";
                    return "/Images/Status/Icons/Junk.png";//"/Images/Status/ReOpen.png";
                case STATUS_TYPE.UNKNOWN:
                    statusDescription = "Unknown";
                    return "/Images/pending.jpg";
                default:
                    statusDescription = "Unknown";
                    return "/Images/pending.jpg";
            }
        }
        catch (Exception err)
        {
            statusDescription = "";
            return "";
        }
    }

    public STATUS_TYPE? getStatusType(string status)
    {
        try
        {
            switch (status.ToUpper())
            {
                case "NEW":
                    return STATUS_TYPE.NEW;
                case "CANCEL":
                    return STATUS_TYPE.CANCEL;
                case "CLOSED":
                    return STATUS_TYPE.CLOSED;
                case "INPROGRESS":
                    return STATUS_TYPE.INPROGRESS;
                case "RESOLVED":
                    return STATUS_TYPE.RESOLVED;
                case "ON HOLD":
                    return STATUS_TYPE.ONHOLD;
                case "RE-OPEN":
                    return STATUS_TYPE.REOPEN;
                case "JUNK":
                    return STATUS_TYPE.JUNK;
                default:
                    return null;
            }
        }
        catch (Exception err)
        {
            return null;
        }
    }

    public string getStatusImage(string status, out string statusDescription)
    {
        return getStatusImage(getStatusType(status), out statusDescription);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}