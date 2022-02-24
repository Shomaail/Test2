<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.IO;
using System.Web;

public class Handler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        FileInfo[] files;
        ExtRevFormBAL erfBAL = new ExtRevFormBAL();
        BAL bal = new BAL();
        context.Response.ContentType = "text/plain";
try
        {
        int appID = int.Parse(context.Request["appID"]);
        string dataPath = "";
        int erID = -1;
        string employeeID = "";
        string fileName = "";
        if(context.Request["extRevID"] != null)
        {
            dataPath = context.Server.MapPath("~\\App_LocalResources") + "\\";
            erID = int.Parse(context.Request["extRevID"]);
            erfBAL.GetEvaluationAttachments(appID, erID, dataPath, out files);
        }
        else
        {
            dataPath = context.Server.MapPath("~\\App_LocalResources") + "\\";
            employeeID = context.Request["empID"];
            fileName = context.Request["fileName"];
            bal.GetApplicationAttachments(appID, employeeID, dataPath, fileName, out files);
        }
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
        
            context.Response.Charset = "UTF-8";
            // context.Response.AddHeader("Content-Disposition", "attachment; filename= " + s);
            context.Response.AddHeader("Content-Disposition", "inline;filename= " + files[0].Name);
            context.Response.AddHeader("Content-Type", "application/pdf");
            context.Response.AddHeader("Content-Length", "" + length);
            context.Response.WriteFile(dataPath + files[0].Name);
            context.Response.Flush();
            context.Response.Close();
        }
        catch (Exception exp)
        {

        }
    }

    public bool IsReusable {
        get {
            return false;
        }

    }



}