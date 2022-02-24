using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Controls_WeekSchedule : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    public bool LoadControl(int MeetingID)
    {
        ApplicationID = int.Parse(Request.Params.Get("applicationID").ToString());
        SqlDataSourcePC.SelectParameters["ApplicationID"].DefaultValue = ApplicationID.ToString();
        SqlDataSourcePC.SelectParameters["MeetingID"].DefaultValue = MeetingID.ToString();
        MeetingsTableAdapters.Form_MeetingsTableAdapter adapterFrmMeeting = new MeetingsTableAdapters.Form_MeetingsTableAdapter();
        Meetings.Form_MeetingsDataTable table = adapterFrmMeeting.GetDataByPK(MeetingID);
        if (table.Count < 1)
        {
            return false;
        }
        Meetings.Form_MeetingsRow row = table[0];
        LabelWeek.Text = getStartEndString(row.WeekNo, row.Year);
        Button1.Text = row.TimeSlot1.ToString();
        Button1.Enabled = (row.TimeSlot1 > 0) ? false : true;
        if (Button1.Enabled)
        {
            Button1.ForeColor = System.Drawing.Color.Green;
            Button1.ToolTip = "";
            Button1.BorderStyle = BorderStyle.Solid;
            Button1.Text = "Call for Meeting";
            Button1.Font.Size = FontUnit.XXSmall;
        }

        Button2.Text = row.TimeSlot2.ToString();
        Button2.Enabled = (row.TimeSlot2 > 0) ? false : true;
        if (Button2.Enabled)
        {
            Button2.ForeColor = System.Drawing.Color.Green;
            Button2.ToolTip = "";
            Button2.BorderStyle = BorderStyle.Solid;
            Button2.Text = "Call for Meeting";
            Button2.Font.Size = FontUnit.XXSmall;

        }

        Button3.Text = row.TimeSlot3.ToString();
        Button3.Enabled = (row.TimeSlot3 > 0) ? false : true;
        if (Button3.Enabled)
        {
            Button3.ForeColor = System.Drawing.Color.Green;
            Button3.ToolTip = "";
            Button3.BorderStyle = BorderStyle.Solid;
            Button3.Text = "Call for Meeting";
            Button3.Font.Size = FontUnit.XXSmall;

        }

        Button4.Text = row.TimeSlot4.ToString();
        Button4.Enabled = (row.TimeSlot4 > 0) ? false : true;
        if (Button4.Enabled)
        {
            Button4.ForeColor = System.Drawing.Color.Green;
            Button4.ToolTip = "";
            Button4.BorderStyle = BorderStyle.Solid;
            Button4.Text = "Call for Meeting";
            Button4.Font.Size = FontUnit.XXSmall;

        }

        Button5.Text = row.TimeSlot5.ToString();
        Button5.Enabled = (row.TimeSlot5 > 0) ? false : true;
        if (Button5.Enabled)
        {
            Button5.ForeColor = System.Drawing.Color.Green;
            Button5.ToolTip = "";
            Button5.BorderStyle = BorderStyle.Solid;
            Button5.Text = "Call for Meeting";
            Button5.Font.Size = FontUnit.XXSmall;

        }

        Button6.Text = row.TimeSlot6.ToString();
        Button6.Enabled = (row.TimeSlot6 > 0) ? false : true;
        if (Button6.Enabled)
        {
            Button6.ForeColor = System.Drawing.Color.Green;
            Button6.ToolTip = "";
            Button6.BorderStyle = BorderStyle.Solid;
            Button6.Text = "Call for Meeting";
            Button6.Font.Size = FontUnit.XXSmall;

        }

        Button7.Text = row.TimeSlot7.ToString();
        Button7.Enabled = (row.TimeSlot7 > 0) ? false : true;
        if (Button7.Enabled)
        {
            Button7.ForeColor = System.Drawing.Color.Green;
            Button7.ToolTip = "";
            Button7.BorderStyle = BorderStyle.Solid;
            Button7.Text = "Call for Meeting";
            Button7.Font.Size = FontUnit.XXSmall;

        }

        Button8.Text = row.TimeSlot8.ToString();
        Button8.Enabled = (row.TimeSlot8 > 0) ? false : true;
        if (Button8.Enabled)
        {
            Button8.ForeColor = System.Drawing.Color.Green;
            Button8.ToolTip = "";
            Button8.BorderStyle = BorderStyle.Solid;
            Button8.Text = "Call for Meeting";
            Button8.Font.Size = FontUnit.XXSmall;

        }

        Button9.Text = row.TimeSlot9.ToString();
        Button9.Enabled = (row.TimeSlot9 > 0) ? false : true;
        if (Button9.Enabled)
        {
            Button9.ForeColor = System.Drawing.Color.Green;
            Button9.ToolTip = "";
            Button9.BorderStyle = BorderStyle.Solid;
            Button9.Text = "Call for Meeting";
            Button9.Font.Size = FontUnit.XXSmall;

        }

        Button10.Text = row.TimeSlot10.ToString();
        Button10.Enabled = (row.TimeSlot10 > 0) ? false : true;
        if (Button10.Enabled)
        {
            Button10.ForeColor = System.Drawing.Color.Green;
            Button10.ToolTip = "";
            Button10.BorderStyle = BorderStyle.Solid;
            Button10.Text = "Call for Meeting";
            Button10.Font.Size = FontUnit.XXSmall;

        }

        Button11.Text = row.TimeSlot11.ToString();
        Button11.Enabled = (row.TimeSlot11 > 0) ? false : true;
        if (Button11.Enabled)
        {
            Button11.ForeColor = System.Drawing.Color.Green;
            Button11.ToolTip = "";
            Button11.BorderStyle = BorderStyle.Solid;
            Button11.Text = "Call for Meeting";
            Button11.Font.Size = FontUnit.XXSmall;

        }

        Button12.Text = row.TimeSlot12.ToString();
        Button12.Enabled = (row.TimeSlot12 > 0) ? false : true;
        if (Button12.Enabled)
        {
            Button12.ForeColor = System.Drawing.Color.Green;
            Button12.ToolTip = "";
            Button12.BorderStyle = BorderStyle.Solid;
            Button12.Text = "Call for Meeting";
            Button12.Font.Size = FontUnit.XXSmall;

        }

        Button13.Text = row.TimeSlot13.ToString();
        Button13.Enabled = (row.TimeSlot13 > 0) ? false : true;
        if (Button13.Enabled)
        {
            Button13.ForeColor = System.Drawing.Color.Green;
            Button13.ToolTip = "";
            Button13.BorderStyle = BorderStyle.Solid;
            Button13.Text = "Call for Meeting";
            Button13.Font.Size = FontUnit.XXSmall;

        }

        Button14.Text = row.TimeSlot14.ToString();
        Button14.Enabled = (row.TimeSlot14 > 0) ? false : true;
        if (Button14.Enabled)
        {
            Button14.ForeColor = System.Drawing.Color.Green;
            Button14.ToolTip = "";
            Button14.BorderStyle = BorderStyle.Solid;
            Button14.Text = "Call for Meeting";
            Button14.Font.Size = FontUnit.XXSmall;

        }

        Button15.Text = row.TimeSlot15.ToString();
        Button15.Enabled = (row.TimeSlot15 > 0) ? false : true;
        if (Button15.Enabled)
        {
            Button15.ForeColor = System.Drawing.Color.Green;
            Button15.ToolTip = "";
            Button15.BorderStyle = BorderStyle.Solid;
            Button15.Text = "Call for Meeting";
            Button15.Font.Size = FontUnit.XXSmall;

        }

        Button16.Text = row.TimeSlot16.ToString();
        Button16.Enabled = (row.TimeSlot16 > 0) ? false : true;
        if (Button16.Enabled)
        {
            Button16.ForeColor = System.Drawing.Color.Green;
            Button16.ToolTip = "";
            Button16.BorderStyle = BorderStyle.Solid;
            Button16.Text = "Call for Meeting";
            Button16.Font.Size = FontUnit.XXSmall;

        }

        Button17.Text = row.TimeSlot17.ToString();
        Button17.Enabled = (row.TimeSlot17 > 0) ? false : true;
        if (Button17.Enabled)
        {
            Button17.ForeColor = System.Drawing.Color.Green;
            Button17.ToolTip = "";
            Button17.BorderStyle = BorderStyle.Solid;
            Button17.Text = "Call for Meeting";
            Button17.Font.Size = FontUnit.XXSmall;
        }
        Button18.Text = row.TimeSlot18.ToString();
        Button18.Enabled = (row.TimeSlot18 > 0) ? false : true;
        if (Button18.Enabled)
        {
            Button18.ForeColor = System.Drawing.Color.Green;
            Button18.ToolTip = "";
            Button18.BorderStyle = BorderStyle.Solid;
            Button18.Text = "Call for Meeting";
            Button18.Font.Size = FontUnit.XXSmall;
        }
        Button19.Text = row.TimeSlot19.ToString();
        Button19.Enabled = (row.TimeSlot19 > 0) ? false : true;
        if (Button19.Enabled)
        {
            Button19.ForeColor = System.Drawing.Color.Green;
            Button19.ToolTip = "";
            Button19.BorderStyle = BorderStyle.Solid;
            Button19.Text = "Call for Meeting";
            Button19.Font.Size = FontUnit.XXSmall;
        }

        Button20.Text = row.TimeSlot20.ToString();
        Button20.Enabled = (row.TimeSlot20 > 0) ? false : true;
        if (Button20.Enabled)
        {
            Button20.ForeColor = System.Drawing.Color.Green;
            Button20.ToolTip = "";
            Button20.BorderStyle = BorderStyle.Solid;
            Button20.Text = "Call for Meeting";
            Button20.Font.Size = FontUnit.XXSmall;
        }

        Button21.Text = row.TimeSlot21.ToString();
        Button21.Enabled = (row.TimeSlot21 > 0) ? false : true;
        if (Button21.Enabled)
        {
            Button21.ForeColor = System.Drawing.Color.Green;
            Button21.ToolTip = "";
            Button21.BorderStyle = BorderStyle.Solid;
            Button21.Text = "Call for Meeting";
            Button21.Font.Size = FontUnit.XXSmall;
        }

        Button22.Text = row.TimeSlot22.ToString();
        Button22.Enabled = (row.TimeSlot22 > 0) ? false : true;
        if (Button22.Enabled)
        {
            Button22.ForeColor = System.Drawing.Color.Green;
            Button22.ToolTip = "";
            Button22.BorderStyle = BorderStyle.Solid;
            Button22.Text = "Call for Meeting";
            Button22.Font.Size = FontUnit.XXSmall;
        }

        Button23.Text = row.TimeSlot23.ToString();
        Button23.Enabled = (row.TimeSlot23 > 0) ? false : true;
        if (Button23.Enabled)
        {
            Button23.ForeColor = System.Drawing.Color.Green;
            Button23.ToolTip = "";
            Button23.BorderStyle = BorderStyle.Solid;
            Button23.Text = "Call for Meeting";
            Button23.Font.Size = FontUnit.XXSmall;
        }

        Button24.Text = row.TimeSlot24.ToString();
        Button24.Enabled = (row.TimeSlot24 > 0) ? false : true;
        if (Button24.Enabled)
        {
            Button24.ForeColor = System.Drawing.Color.Green;
            Button24.ToolTip = "";
            Button24.BorderStyle = BorderStyle.Solid;
            Button24.Text = "Call for Meeting";
            Button24.Font.Size = FontUnit.XXSmall;
        }

        Button25.Text = row.TimeSlot25.ToString();
        Button25.Enabled = (row.TimeSlot25 > 0) ? false : true;
        if (Button25.Enabled)
        {
            Button25.ForeColor = System.Drawing.Color.Green;
            Button25.ToolTip = "";
            Button25.BorderStyle = BorderStyle.Solid;
            Button25.Text = "Call for Meeting";
            Button25.Font.Size = FontUnit.XXSmall;
        }

        Button26.Text = row.TimeSlot26.ToString();
        Button26.Enabled = (row.TimeSlot26 > 0) ? false : true;
        if (Button26.Enabled)
        {
            Button26.ForeColor = System.Drawing.Color.Green;
            Button26.ToolTip = "";
            Button26.BorderStyle = BorderStyle.Solid;
            Button26.Text = "Call for Meeting";
            Button26.Font.Size = FontUnit.XXSmall;
        }

        Button27.Text = row.TimeSlot27.ToString();
        Button27.Enabled = (row.TimeSlot27 > 0) ? false : true;
        if (Button27.Enabled)
        {
            Button27.ForeColor = System.Drawing.Color.Green;
            Button27.ToolTip = "";
            Button27.BorderStyle = BorderStyle.Solid;
            Button27.Text = "Call for Meeting";
            Button27.Font.Size = FontUnit.XXSmall;
        }

        Button28.Text = row.TimeSlot28.ToString();
        Button28.Enabled = (row.TimeSlot28 > 0) ? false : true;
        if (Button28.Enabled)
        {
            Button28.ForeColor = System.Drawing.Color.Green;
            Button28.ToolTip = "";
            Button28.BorderStyle = BorderStyle.Solid;
            Button28.Text = "Call for Meeting";
            Button28.Font.Size = FontUnit.XXSmall;
        }

        Button29.Text = row.TimeSlot29.ToString();
        Button29.Enabled = (row.TimeSlot29 > 0) ? false : true;
        if (Button29.Enabled)
        {
            Button29.ForeColor = System.Drawing.Color.Green;
            Button29.ToolTip = "";
            Button29.BorderStyle = BorderStyle.Solid;
            Button29.Text = "Call for Meeting";
            Button29.Font.Size = FontUnit.XXSmall;
        }

        Button30.Text = row.TimeSlot30.ToString();
        Button30.Enabled = (row.TimeSlot30 > 0) ? false : true;
        if (Button30.Enabled)
        {
            Button30.ForeColor = System.Drawing.Color.Green;
            Button30.ToolTip = "";
            Button30.BorderStyle = BorderStyle.Solid;
            Button30.Text = "Call for Meeting";
            Button30.Font.Size = FontUnit.XXSmall;

        }

        Button31.Text = row.TimeSlot31.ToString();
        Button31.Enabled = (row.TimeSlot31 > 0) ? false : true;
        if (Button31.Enabled)
        {
            Button31.ForeColor = System.Drawing.Color.Green;
            Button31.ToolTip = "";
            Button31.BorderStyle = BorderStyle.Solid;
            Button31.Text = "Call for Meeting";
            Button31.Font.Size = FontUnit.XXSmall;

        }

        Button32.Text = row.TimeSlot32.ToString();
        Button32.Enabled = (row.TimeSlot32 > 0) ? false : true;
        if (Button32.Enabled)
        {
            Button32.ForeColor = System.Drawing.Color.Green;
            Button32.ToolTip = "";
            Button32.BorderStyle = BorderStyle.Solid;
            Button32.Text = "Call for Meeting";
            Button32.Font.Size = FontUnit.XXSmall;

        }

        Button33.Text = row.TimeSlot33.ToString();
        Button33.Enabled = (row.TimeSlot33 > 0) ? false : true;
        if (Button33.Enabled)
        {
            Button33.ForeColor = System.Drawing.Color.Green;
            Button33.ToolTip = "";
            Button33.BorderStyle = BorderStyle.Solid;
            Button33.Text = "Call for Meeting";
            Button33.Font.Size = FontUnit.XXSmall;

        }

        Button34.Text = row.TimeSlot34.ToString();
        Button34.Enabled = (row.TimeSlot34 > 0) ? false : true;
        if (Button34.Enabled)
        {
            Button34.ForeColor = System.Drawing.Color.Green;
            Button34.ToolTip = "";
            Button34.BorderStyle = BorderStyle.Solid;
            Button34.Text = "Call for Meeting";
            Button34.Font.Size = FontUnit.XXSmall;

        }

        Button35.Text = row.TimeSlot35.ToString();
        Button35.Enabled = (row.TimeSlot35 > 0) ? false : true;
        if (Button35.Enabled)
        {
            Button35.ForeColor = System.Drawing.Color.Green;
            Button35.ToolTip = "";
            Button35.BorderStyle = BorderStyle.Solid;
            Button35.Text = "Call for Meeting";
            Button35.Font.Size = FontUnit.XXSmall;

        }

        Button36.Text = row.TimeSlot36.ToString();
        Button36.Enabled = (row.TimeSlot36 > 0) ? false : true;
        if (Button36.Enabled)
        {
            Button36.ForeColor = System.Drawing.Color.Green;
            Button36.ToolTip = "";
            Button36.BorderStyle = BorderStyle.Solid;
            Button36.Text = "Call for Meeting";
            Button36.Font.Size = FontUnit.XXSmall;

        }

        Button37.Text = row.TimeSlot37.ToString();
        Button37.Enabled = (row.TimeSlot37 > 0) ? false : true;
        if (Button37.Enabled)
        {
            Button37.ForeColor = System.Drawing.Color.Green;
            Button37.ToolTip = "";
            Button37.BorderStyle = BorderStyle.Solid;
            Button37.Text = "Call for Meeting";
            Button37.Font.Size = FontUnit.XXSmall;

        }

        Button38.Text = row.TimeSlot38.ToString();
        Button38.Enabled = (row.TimeSlot38 > 0) ? false : true;
        if (Button38.Enabled)
        {
            Button38.ForeColor = System.Drawing.Color.Green;
            Button38.ToolTip = "";
            Button38.BorderStyle = BorderStyle.Solid;
            Button38.Text = "Call for Meeting";
            Button38.Font.Size = FontUnit.XXSmall;

        }

        Button39.Text = row.TimeSlot39.ToString();
        Button39.Enabled = (row.TimeSlot39 > 0) ? false : true;
        if (Button39.Enabled)
        {
            Button39.ForeColor = System.Drawing.Color.Green;
            Button39.ToolTip = "";
            Button39.BorderStyle = BorderStyle.Solid;
            Button39.Text = "Call for Meeting";
            Button39.Font.Size = FontUnit.XXSmall;

        }

        Button40.Text = row.TimeSlot40.ToString();
        Button40.Enabled = (row.TimeSlot40 > 0) ? false : true;
        if (Button40.Enabled)
        {
            Button40.ForeColor = System.Drawing.Color.Green;
            Button40.ToolTip = "";
            Button40.BorderStyle = BorderStyle.Solid;
            Button40.Text = "Call for Meeting";
            Button40.Font.Size = FontUnit.XXSmall;

        }
        Button41.Text = row.TimeSlot41.ToString();
        Button41.Enabled = (row.TimeSlot41 > 0) ? false : true;
        if (Button41.Enabled)
        {
            Button41.ForeColor = System.Drawing.Color.Green;
            Button41.ToolTip = "";
            Button41.BorderStyle = BorderStyle.Solid;
            Button41.Text = "Call for Meeting";
            Button41.Font.Size = FontUnit.XXSmall;

        }

        Button42.Text = row.TimeSlot42.ToString();
        Button42.Enabled = (row.TimeSlot42 > 0) ? false : true;
        if (Button42.Enabled)
        {
            Button42.ForeColor = System.Drawing.Color.Green;
            Button42.ToolTip = "";
            Button42.BorderStyle = BorderStyle.Solid;
            Button42.Text = "Call for Meeting";
            Button42.Font.Size = FontUnit.XXSmall;

        }
        Button43.Text = row.TimeSlot43.ToString();
        Button43.Enabled = (row.TimeSlot43 > 0) ? false : true;
        if (Button43.Enabled)
        {
            Button43.ForeColor = System.Drawing.Color.Green;
            Button43.ToolTip = "";
            Button43.BorderStyle = BorderStyle.Solid;
            Button43.Text = "Call for Meeting";
            Button43.Font.Size = FontUnit.XXSmall;

        }

        Button44.Text = row.TimeSlot44.ToString();
        Button44.Enabled = (row.TimeSlot44 > 0) ? false : true;
        if (Button44.Enabled)
        {
            Button44.ForeColor = System.Drawing.Color.Green;
            Button44.ToolTip = "";
            Button44.BorderStyle = BorderStyle.Solid;
            Button44.Text = "Call for Meeting";
            Button44.Font.Size = FontUnit.XXSmall;

        }

        Button45.Text = row.TimeSlot45.ToString();
        Button45.Enabled = (row.TimeSlot45 > 0) ? false : true;
        if (Button45.Enabled)
        {
            Button45.ForeColor = System.Drawing.Color.Green;
            Button45.ToolTip = "";
            Button45.BorderStyle = BorderStyle.Solid;
            Button45.Text = "Call for Meeting";
            Button45.Font.Size = FontUnit.XXSmall;

        }
        return true;
    }
    public string getStartEndString(int WeekNumber, string Year)
    {
        DateTime firstDate = DateTime.Parse("1/1/" + Year);
        int compensation = 0;

        switch (firstDate.DayOfWeek)
        {

            case DayOfWeek.Sunday:
                compensation = 0;
                break;
            case DayOfWeek.Monday:
                compensation = 1;
                break;
            case DayOfWeek.Tuesday:
                compensation = 2;
                break;
            case DayOfWeek.Wednesday:
                compensation = 3;
                break;
            case DayOfWeek.Thursday:
                compensation = 4;
                break;
            case DayOfWeek.Friday:
                compensation = 5;
                break;
            case DayOfWeek.Saturday:
                compensation = 6;
                break;
        }


        DateTime startDateOfWeek = (firstDate.AddDays((7 * (WeekNumber - 1)) - compensation));
        DateTime endDateOfWeek = startDateOfWeek.AddDays(4);
        return "Starting Date: " + startDateOfWeek.ToString("D") + ",     Ending Date: " + endDateOfWeek.ToString("D");

    }
    public int ApplicationID
    {
        get
        {
            if (hdnApplicationID.Value.Length == 0)
                return -1;
            return int.Parse(hdnApplicationID.Value);
        }
        set
        {
            hdnApplicationID.Value = value.ToString();
        }
    }
}
