using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BL.Data;

public partial class Admin_ResourceEditor  : System.Web.UI.Page
{
    BAL bal = new BAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        //ResourceManager rm = new ResourceManager("WebApplication.App_GlobalResources.Resource",
        //    Assembly.GetExecutingAssembly());

        //String strWebsite = rm.GetString("ActionM1", CultureInfo.CurrentCulture);
        //String strName = rm.GetString("ActionM10");
        //form1.InnerText = rm.BaseName[0].ToString()  + " Website: " + strWebsite + "--Name: " + strName;
        //IResourceWriter writer = new ResourceWriter("WebApplication.App_GlobalResources.Resource");
        //writer.AddResource("test", "testvalue");
        //writer.Close();
        //strName = rm.GetString("test");
        //form1.InnerText = "--new: " + strName;
        //ResourceReader rr = new ResourceReader("WebApplication.App_GlobalResources.Resource");
        //string rType;
        //byte[] rData;
        //rr.GetResourceData("test", out rType, out rData);
        //form1.InnerText = "--new: " + rType + "sdf " + rData;

        //XmlDocument loResource = new XmlDocument();
        //loResource.Load(Server.MapPath("/App_Data/Resource.resx"));


        //XmlNode loRoot = loResource.SelectSingleNode("root/data[@name='Website']/value");


        //if (loRoot != null)
        //{
        //    loRoot.InnerText = "test";
        //    loResource.Save(Server.MapPath("/App_Data/Resource.resx"));
        //}

        //string resFileName = "Resource.resx";
        //string dataPath = Server.MapPath("~\\App_GlobalResources") + "\\";            //using (ResXResourceWriter writer = new ResXResourceWriter(resFileName))
        ////{
        ////    writer.AddResource("name1", "value11");
        ////    writer.AddResource("name2", "value21");
        ////    writer.AddResource("name3", "value31");
        ////    writer.AddResource("name4", "value4");
        ////    writer.AddResource("name5", "value5");
        ////    writer.AddResource("name6", "value6");
        ////}
        ////RemoveResEntry(@".\\App_GlobalResources\\Resource.resx", "name1");
        //AddOrUpdateResource("Website", "Website");

        //// form1.InnerHtml = GetGlobalResourceObject("Resource", "ActionM1").ToString();
        //ResXResourceReader rsxr = new ResXResourceReader(dataPath + resFileName);
        //List<ResourceFile> resObj = new List<ResourceFile>();
        //foreach (DictionaryEntry d in rsxr)
        //{
        //    resObj.Add(new ResourceFile() { Key = d.Key.ToString(), Value = d.Value.ToString()});
        // //   Response.Write(d.Key.ToString() + ": \t" + d.Value.ToString()+" \n");
        //}        
        if (IsPostBack)
        {
            return;
        }
        DataBindControls();
    }
    private void DataBindControls()
    {
        string resFileName = "Resource.resx";
        string dataPath = Server.MapPath("~\\App_GlobalResources") + "\\";            //using (ResXResourceWriter writer = new ResXResourceWriter(resFileName))

        ResXResourceReader rsxr = new ResXResourceReader(dataPath + resFileName);
        List<ResourceFile> resObj = new List<ResourceFile>();
        foreach (DictionaryEntry d in rsxr)
        {
            resObj.Add(new ResourceFile() { Key = d.Key.ToString(), Value = d.Value.ToString() });
            //   Response.Write(d.Key.ToString() + ": \t" + d.Value.ToString()+" \n");
        }
        rptResource.DataSource = resObj;
        rptResource.DataBind();
    }
    protected void hideModalPopupViaServer0_Click(object sender, EventArgs e)
    {
        programmaticModalPopup0.Hide();
    }

    public void AddOrUpdateResource(string key, string value)
        {

            string resFileName = "Resource.resx";
            string dataPath = Server.MapPath("~\\App_GlobalResources") + "\\";
            var resx = new List<DictionaryEntry>();
            using (var reader = new ResXResourceReader(dataPath + resFileName))
            {
                resx = reader.Cast<DictionaryEntry>().ToList();
                var existingResource = resx.Where(r => r.Key.ToString() == key).FirstOrDefault();
                if (existingResource.Key == null && existingResource.Value == null) // NEW!
                {
                    resx.Add(new DictionaryEntry() { Key = key, Value = value });
                }
                else // MODIFIED RESOURCE!
                {
                    var modifiedResx = new DictionaryEntry()
                    { Key = existingResource.Key, Value = value };
                    resx.Remove(existingResource);  // REMOVING RESOURCE!
                    resx.Add(modifiedResx);  // AND THEN ADDING RESOURCE!
                }
            }
            using (var writer = new ResXResourceWriter(dataPath + resFileName))
            {
                resx.ForEach(r =>
                {
                    // Again Adding all resource to generate with final items
                    writer.AddResource(r.Key.ToString(), r.Value.ToString());
                });
                writer.Generate();
            }
        }


    protected void rptResource_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //if (e.CommandName == "Delete")
        //{
        //    int attributeID = int.Parse(e.CommandArgument.ToString());
        //    if (bal.GetWorkflowAttribute().Count() == 1)
        //    {
        //        labelProgrammaticPopup0.Text = "There has to be atleast one attribute. You cannot delete this attribute";
        //        programmaticModalPopup0.Show();
        //        return;
        //    }
        //    //labelProgrammaticPopup0.Text = "Go to the GlobalAttribute enum specifier in the App_Code\\AllClasses.cs and delete this attribute as well in order to avoid error.";
        //    //programmaticModalPopup0.Show();
        //    bal.DeleteWorkflowAttribute(attributeID);
        //    DataBindControls();
        //}
        //else 
        if (e.CommandName == "Edit")
        {
            //labelProgrammaticPopup0.Text = "If you Edit the Attribute then do the same modification in GlobalAttribute enum specifier in the App_Code\\AllClasses.cs in order to avoid error. While Editing the value will not cause any error.";
            //programmaticModalPopup0.Show();
            ToggleElements(e.Item, true);
        }
        else if (e.CommandName == "Update")
        {
            string key = e.CommandArgument.ToString();
            string value = (e.Item.FindControl("tbValueEdit") as HtmlInputControl).Value;
            AddOrUpdateResource(key, value);
            DataBindControls();
        }
        else if (e.CommandName == "Cancel")
        {
            DataBindControls();
        }

    }
    private void ToggleElements(RepeaterItem item, bool isEdit)
    {
        //Toggle Buttons.
        item.FindControl("lbtnEdit").Visible = !isEdit;
        item.FindControl("lbtnUpdate").Visible = isEdit;
        item.FindControl("lbtnCancel").Visible = isEdit;
        //item.FindControl("lbtnDelete").Visible = !isEdit;

        //Toggle Labels.
        //item.FindControl("lblContactName").Visible = !isEdit;
        //item.FindControl("lblCountry").Visible = !isEdit;

        //Toggle TextBoxes.
        //item.FindControl("tbAttributeEdit").Visible = isEdit;
        item.FindControl("tbValueEdit").Visible = isEdit;

    }
}
