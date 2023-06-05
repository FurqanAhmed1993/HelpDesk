using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO.Compression;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Web.UI;
using System.Data;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Common
/// </summary>
public static class Common
{
    //public Common()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}

    public static string Serialize(object obj)
    {
        string result = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        return result;
    }

    public static object Deserialize(string json, Type type)
    {
        return JsonConvert.DeserializeObject(json, type);
    }
    public static void BindCheckBoxList(CheckBoxList cbl, Object dataSource, string dataTextField, string dataValueField, bool hasAllItem = false, bool hasAllItemsSelected = false)
    {
        cbl.Items.Clear();
        cbl.DataSource = dataSource;
        cbl.DataTextField = dataTextField;
        cbl.DataValueField = dataValueField;
        cbl.DataBind();

        if (hasAllItem == true)
        {
            cbl.Items.Insert(0, new ListItem("All", "0"));
            //cbl.Items[0].Selected = true;
        }
        if (hasAllItemsSelected == true)
        {
            foreach (ListItem li in cbl.Items)
            {
                li.Selected = true;
            }
        }
    }

    public static string GetHash(string value)
    {

        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "md5");
    }

    public static void BindRadioButtonList(RadioButtonList rdbtnLstGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
    {
        rdbtnLstGeneral.DataSource = dataSource;
        rdbtnLstGeneral.DataTextField = dataTextField;
        rdbtnLstGeneral.DataValueField = dataValueField;
        rdbtnLstGeneral.DataBind();

        if (hasSelectItem == true)
        {
            rdbtnLstGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        if (hasOtherItem == true)
        {
            rdbtnLstGeneral.Items.Add(new ListItem("Other", "-100"));
        }
    }

    public static void BindDropDown(DropDownList ddlGeneral, Object dataSource, string dataTextField, string dataValueField, bool hasSelectItem, bool hasOtherItem)
    {
        ddlGeneral.DataSource = dataSource;
        ddlGeneral.DataTextField = dataTextField;
        ddlGeneral.DataValueField = dataValueField;
        ddlGeneral.DataBind();

        if (hasSelectItem == true)
        {
            ddlGeneral.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        if (hasOtherItem == true)
        {
            ddlGeneral.Items.Add(new ListItem("-OTHER-", "-100"));
        }
    }
    public static string getSubstring(string content, int length)
    {
        string substr = content;
        if (content.Length > length)
        {
            substr = content.Substring(0, length) + "...";
        }

        return substr;
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

    public static DataTable ToDataTable<TSource>(this IList<TSource> data)
    {
        DataTable dataTable = new DataTable();
        PropertyInfo[] props = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in props)
        {

            dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ??
                  prop.PropertyType);

        }

        foreach (TSource item in data)
        {
            var values = new object[props.Length];
            for (int i = 0; i < props.Length; i++)
            {

                values[i] = props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }

    public static bool ResetBaseClass()
    {
        Base baseClass = new Base();
        baseClass.EmployeeId = 0;
        baseClass.UserId = 0;
        baseClass.FullName = "";
        baseClass.EmailAddress = "";
        baseClass.Regionid = 0;
        baseClass.RoleCode = Convert.ToString(0);
        baseClass.IsSuperAdmin = false;
        baseClass.IsAdmin = false;
        baseClass.IsEmployee = false;
        baseClass.IsIncharge = false;
        baseClass.IsClient = false;
        baseClass.DesignationId = 0;
        baseClass.DepartmentId = 0;
        baseClass.LevelId = 0;
        baseClass.LevelSortNo = 0;
        baseClass.CustomerId = 0;
        return true;
    }

    public static string Decrypt(string EncryptedData)
    {
        AES_SHA2 security = new AES_SHA2();
        return security.Decrypt(Constant.SecurityKey, EncryptedData);
    }
   
    public static string DecryptByIdAES(string EncryptedData, string Id)
    {
        string DecryptedQueryString = DecryptStringAES(EncryptedData);

        DecryptedQueryString = DecryptedQueryString.After(Id + "=");
        int EndIndex = DecryptedQueryString.IndexOf("&");

        if (EndIndex == -1)
        {
            return DecryptedQueryString;
        }
        else
        {
            return DecryptedQueryString.Substring(0, EndIndex);
        }
    }
    public static string DecryptById(string EncryptedData, string Id)
    {
        string DecryptedQueryString = Decrypt(EncryptedData);

        DecryptedQueryString = DecryptedQueryString.After(Id + "=");
        int EndIndex = DecryptedQueryString.IndexOf("&");

        if (EndIndex == -1)
        {
            return DecryptedQueryString;
        }
        else
        {
            return DecryptedQueryString.Substring(0, EndIndex);
        }
    }
    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Check arguments.  
        if (cipherText == null || cipherText.Length <= 0)
        {
            throw new ArgumentNullException("cipherText");
        }
        if (key == null || key.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }
        if (iv == null || iv.Length <= 0)
        {
            throw new ArgumentNullException("key");
        }

        // Declare the string used to hold  
        // the decrypted text.  
        string plaintext = null;

        // Create an RijndaelManaged object  
        // with the specified key and IV.  
        using (var rijAlg = new RijndaelManaged())
        {
            //Settings  
            rijAlg.Mode = CipherMode.CBC;
            rijAlg.Padding = PaddingMode.PKCS7;
            rijAlg.FeedbackSize = 128;

            rijAlg.Key = key;
            rijAlg.IV = iv;

            // Create a decrytor to perform the stream transform.  
            var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            try
            {
                // Create the streams used for decryption.  
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {

                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream  
                            // and place them in a string.  
                            plaintext = srDecrypt.ReadToEnd();

                        }

                    }
                }
            }
            catch
            {
                plaintext = "keyError";
            }
        }

        return plaintext;
    }
    public static string DecryptStringAES(string cipherText)
    {
        var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
        var iv = Encoding.UTF8.GetBytes("8080808080808080");

        var encrypted = Convert.FromBase64String(cipherText);
        var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
        return string.Format(decriptedFromJavascript);
    }

    public static DataTable GetData(SqlCommand pObjCommand)
    {
        try
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter objAdapter = new SqlDataAdapter(pObjCommand);
            objAdapter.Fill(dataTable);
            objAdapter.Dispose();
            return dataTable;
        }
        catch (SqlException exception)
        {
            throw exception;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
}
