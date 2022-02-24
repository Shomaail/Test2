<%@ Import Namespace="System.Diagnostics" %>
<%@ Application Language="C#" %>

<script runat="server">


    #region RemindersAutoTask

    private const string DummyCacheItemKey = "GagaGuguGigi";

    private bool RegisterCacheEntry()
    {

        if (null != HttpRuntime.Cache[DummyCacheItemKey]) return false;

        HttpRuntime.Cache.Add(DummyCacheItemKey, "Test", null,
            DateTime.MaxValue, TimeSpan.FromDays(2),
            CacheItemPriority.Normal,
            new CacheItemRemovedCallback(CacheItemRemovedCallback));


        return true;
    }
    public void CacheItemRemovedCallback(string key,
                object value, CacheItemRemovedReason reason)
    {

        RegisterCacheEntry();

        // Do the service works
        DoWork();
    }



    public void DoWork()
    {
        //        MeetingsTableAdapters.Form_MeetingMembersTableAdapter frmMMAdapter = new MeetingsTableAdapters.Form_MeetingMembersTableAdapter();
        //        MeetingsTableAdapters.Form_MeetingsTableAdapter frmMAdapter = new MeetingsTableAdapters.Form_MeetingsTableAdapter();
        //        ExternalRemindersTableAdapters.Application_TaskLogExtTableAdapter appTskLgExtAdapter = new ExternalRemindersTableAdapters.Application_TaskLogExtTableAdapter();
        //        ExternalRemindersTableAdapters.Task_ExtTableAdapter tskExtAdapter = new ExternalRemindersTableAdapters.Task_ExtTableAdapter();
        //        PromotionTableAdapters.PromotionCommitteeTempTableAdapter fnlPCAdapter = new PromotionTableAdapters.PromotionCommitteeTempTableAdapter();


        //        foreach (Meetings.Form_MeetingsRow row in frmMAdapter.GetData())
        //        {
        //            if (DateTime.Today.CompareTo(getEndDate(row.WeekNo, row.Year.ToString())) >= 0 && row.MeetingStatus == true)
        //            {
        //                frmMAdapter.CancelMeetingsByApplicationID(row.ApplicationID);
        //                Promotion.PromotionCommitteeTempDataTable finalPCTable = fnlPCAdapter.GetMembersAgainstAppID(row.ApplicationID);
        //                foreach (Promotion.PromotionCommitteeTempRow pcRow in finalPCTable)
        //                {
        //                    appTskLgExtAdapter.UpdateExtTskComplete(true
        //                        , row.ApplicationID
        //                        , tskExtAdapter.GetDataByTitle("Post Busy Schedule")[0].TaskID
        //                        , pcRow.AutoNumberPK);

        //                }
        //                Emailer.Send(finalPCTable[0].Email
        //                    , "Cancelation Of Launched Busy Schedule"
        //                    , @"
        //Dear Dr. " + finalPCTable[0].Name +@",

        //The Busy Schedule you have recently launched for the week #" + row.WeekNo +
        //@" of the Year " + row.Year + "(Ending on " + getEndDate(row.WeekNo, row.Year.ToString()).ToString("D") +
        //@") has been automatically Cancelled as the ending date has passed the current date.

        //However, you can view the posted schedules of members in the Previous Meetings page after opening the promotion application. Also you can launch a new busy schedule for any future week.

        //Adminstration,

        //Online Faculty Promotion System", "AutoEmailer", row.ApplicationID);
        //            }
        //        }
        WorkFlow.SendReminders();
    }
    public DateTime getEndDate(int WeekNumber, string Year)
    {
        DateTime firstDate = DateTime.Parse("1/1/" + Year);
        int compensation = 0;

        switch (firstDate.DayOfWeek)
        {
            case DayOfWeek.Saturday:
                compensation = 0;
                break;
            case DayOfWeek.Sunday:
                compensation = 1;
                break;
            case DayOfWeek.Monday:
                compensation = 2;
                break;
            case DayOfWeek.Tuesday:
                compensation = 3;
                break;
            case DayOfWeek.Wednesday:
                compensation = 4;
                break;
            case DayOfWeek.Thursday:
                compensation = 5;
                break;
            case DayOfWeek.Friday:
                compensation = 6;
                break;

        }


        DateTime startDateOfWeek = (firstDate.AddDays((7 * (WeekNumber - 1)) - compensation));
        return startDateOfWeek.AddDays(4);

    }
    #endregion

    void Application_Start(object sender, EventArgs e)
    {

        // Code that runs on application startup
        RegisterCacheEntry();

    }
    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }


    void Application_Error(object sender, EventArgs e)
    {
        if(Request == null)
        {
            return;
        }
        // Code that runs when an unhandled error occurs
        if (Request.Url.ToString().Contains("localhost") && Request.QueryString["spaccess"] == null )
        {
            return;
        }
        string err = "";
        Exception objErr = new Exception();
        objErr = Server.GetLastError().GetBaseException();



        if (Session["user"] != null)
        {
            BL.Data.Employee emp = new BL.Data.Employee();
            emp = Session["user"] as BL.Data.Employee;
            objErr = Server.GetLastError().GetBaseException();
            err = "Error Caught in Application_Error event\n" +
                     "\nError in: " + Request.Url.ToString() +
                     "\nProblem for the user: " + emp.Title + " " + emp.Name + " (" + emp.Email + " )" +
                    "\nError Message:" + objErr.Message.ToString() +
                    "\nStack Trace:" + objErr.StackTrace.ToString();
            Emailer.Send("facpromote@kfupm.edu.sa", "Faculty Promotion System Error", err, "AutoEmailer", null);
        }
        else if (Session["refree"] != null)
        {
            Form.Form_FinalRefreeRow refree = Session["refree"] as Form.Form_FinalRefreeRow;
            objErr = Server.GetLastError().GetBaseException();
            err = "Error Caught in Application_Error event\n" +
                     "\nError in: " + Request.Url.ToString() +
                     "\nProblem for the user: Dr. " + refree.Name + " (" + refree.RefreeID + " )" +
                    "\nError Message:" + objErr.Message.ToString() +
                    "\nStack Trace:" + objErr.StackTrace.ToString();
            Emailer.Send("facpromote@kfupm.edu.sa", "Faculty Promotion System Error", err, "AutoEmailer", null);
        }
        else
        {
            Server.ClearError();
        }

        //try
        //{
        //    HR.EmployeeRow user = Session["user"] as HR.EmployeeRow;
        //    if (Session["refree"] != null)
        //    {


        //    }
        //    else
        //    {


        //    }
        //    Emailer.Send("facpromote@kfupm.edu.sa", "Faculty Promotion System Error in App", "Error caught in Application: \nError details: " + err
        //            , "AutoEmailer", -1);
        //    Se    rver.ClearError();
        //    return;
        //}
        //catch (Exception ex1)
        //{
        //    Emailer.Send("facpromote@kfupm.edu.sa", "Faculty Promotion System Error in Global.asax", "Error caught in Global.asax. \nError details: " + ex1.Message, "AutoEmailer", -1);
        //    return;
        //}

        //Exception exc = Server.GetLastError();
        //if (exc.GetType() == typeof(HttpException))
        //{
        //    // The Complete Error Handling Example generates
        //    // some errors using URLs with "NoCatch" in them;
        //    // ignore these here to simulate what would happen
        //    // if a global.asax handler were not implemented.
        //    if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
        //        return;
        //    Emailer.Send("fpromot@kfupm.edu.sa", "Faculty Promotion System Undefined Error", exc.Message, "AutoEmailer", -1);
        //}
        //Server.ClearError();

    }

    void Session_Start(object sender, EventArgs e)
    {


        // Code that runs when a new session is started
        //Response.Write("Seesion is started ");
    }

    void Session_End(object sender, EventArgs e)
    {
        // Response.Write("Session is ENDED ");
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }


    void Application_BeginRequest(object sender, EventArgs e)
    {
        //if( HttpContext.Current.Request.Url.ToString().Contains("Reserved") == false)
        //{
        //      	HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //      	HttpContext.Current.Response.Cache.SetAllowResponseInBrowserHistory(false);
        //      }
    }

</script>
