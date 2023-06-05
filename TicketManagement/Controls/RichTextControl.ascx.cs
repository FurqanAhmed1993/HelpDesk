using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_RichTextControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string txtEditorText
    {
        get { return txtEditor.Content.Trim(); }
        set
        {

            txtEditor.Content = value;

        }
    }
    //public bool SetText(string content)
    //{
    //    try
    //    {
    //        txtEditor.Content = content;
    //        return true;
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }
    //}
}