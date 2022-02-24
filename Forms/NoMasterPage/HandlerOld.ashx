<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.IO;
using System.Web;

public class Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        FileInfo[] files;
        BAL bal = new BAL();
       // context.Response.ContentType = "text/plain";

        int appID = int.Parse(context.Request["appID"]);
        string dataPath = "";
        int erID = -1;
        //string employeeID = "";
        //string fileName = "";
        if(context.Request["refreeID"] != null)
        {
            dataPath = context.Server.MapPath("~\\App_Data\\EvaluationAttachments") + "\\";
            erID = int.Parse(context.Request["refreeID"]);
            GetEvaluationAttachments(appID, erID, dataPath, out files);
        }
        else {
            return;}
        //else
        //{
        //    dataPath = context.Server.MapPath("~\\App_Data\\ApplicationAttachments") + "\\";
        //    employeeID = context.Request["empID"];
        //    fileName = context.Request["fileName"];
        //    GetApplicationAttachments(appID, employeeID, dataPath, fileName, out files);
        //}
        if (files.Length == 0)
        {
            context.Response.Write("File Not Found");
            return;
        }
        string filePath = dataPath + files[0].Name;
        if (!File.Exists(filePath))
        {
            context.Response.Write("File Not Found");
            return;
        }

        FileStream stream = File.OpenRead(filePath);
        long length = stream.Length;
        stream.Close();
        string s = files[0].Name;
        int index = s.IndexOf('_', s.IndexOf('_') + 1);
        s = s.Remove(0, index+1);
        context.Response.Clear();
        try
        {
                //            context.Response.Charset = "UTF-8";
            //context.Response.AddHeader("Content-Disposition", "attachment; filename= " + s);
            context.Response.AddHeader("Content-Disposition", "inline;filename= " + files[0].Name);
            //context.Response.AddHeader("Content-Type", "application/pdf");
           // context.Response.AddHeader("Content-Length", "" + length);
            context.Response.WriteFile(dataPath + files[0].Name);
            context.Response.Flush();
            context.Response.Close();
        }
        catch (Exception exp)
        {
        }



        //context.Response.ContentType = "text/plain";
        //string dataPath = context.Server.MapPath("~\\App_LocalResources") + "\\";
        //int refreeID = int.Parse(context.Request["refreeID"]); 
        //ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter adapterRevForm = new ReviewerFormTableAdapters.Form_ReviewersFormTableAdapter();
        //string fileName = adapterRevForm.GetDataByPK(refreeID)[0].SupDocument;
        //string filePath = dataPath + fileName;

        //if (!File.Exists(filePath))
        //{
        //    context.Response.Write("File Not Found");
        //    return;
        //}

        //FileStream stream = File.OpenRead(filePath);
        //long length = stream.Length;
        //stream.Close();

        //context.Response.Clear();
        //try
        //{
        //    context.Response.Charset = "UTF-8";
        //    context.Response.AddHeader("Content-Disposition", "attachment; filename= " + fileName);
        //    context.Response.AddHeader("Content-Length", "" + length);
        //    context.Response.WriteFile(dataPath + fileName);
        //    context.Response.Flush();
        //    context.Response.Close();
        //}
        //catch
        //{
        //}        
    }
    public void GetEvaluationAttachments(int appID, int refreeID, string dataPath, out FileInfo[] files)
    {
        DirectoryInfo folder = new DirectoryInfo(dataPath);
        files = folder.GetFiles(appID + "_" + refreeID + "_*", SearchOption.AllDirectories);
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}