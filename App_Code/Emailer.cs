using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;
using System.Net.Sockets;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.SessionState;
using BL.Data;
using System.Linq;
/// <summary>
/// Summary description for Emailer
/// </summary>
public class Emailer
{
    private static string ReadFile(string filename)
    {
        if (!System.IO.File.Exists(filename))
        {
            throw new Exception("File does not exist");
        }

        StreamReader reader = new StreamReader(filename);
        string result = reader.ReadToEnd();
        reader.Close();
        return result;
    }


    private static string Replace(string data, string[] parameters, string[] values)
    {
        for (int i = 0; i < parameters.Length; i++)
        {
            data = data.Replace(parameters[i], values[i]);
        }
        return data;
    }

    public static void SendFile(Page page, ArrayList emails, string titleFile, string bodyFile)
    {
        MailMessage message = new MailMessage();

        message.BodyEncoding = System.Text.Encoding.GetEncoding(1256);
        message.SubjectEncoding = System.Text.Encoding.GetEncoding(1256);

        message.IsBodyHtml = true;

        message.From = new MailAddress(
            System.Configuration.ConfigurationManager.AppSettings["Smtp.From"]);

        message.Subject = ReadFile(page.Server.MapPath(titleFile));
        message.Body = ReadFile(page.Server.MapPath(bodyFile));


        SmtpClient client = new SmtpClient();
        foreach (string email in emails)
        {
            if (email.IndexOf("@") > 0)
            {
                message.To.Clear();
                try
                {
                    message.To.Add(new MailAddress(email));
                    client.Send(message);
                }
                catch { }
            }
        }
    }


    public static void Send(string ToEmail, string title, string body, string username, int? appID)
    {
        BAL bal = new BAL();
        MailMessage message = new MailMessage();
         
        message.IsBodyHtml      = false;
        message.From = new MailAddress(ConfigurationManager.AppSettings["Mail.From"]);
        //message.From.Address = "shomaail@gmail.com";
       // message.ReplyTo = new MailAddress(username+"@kfupm.edu.sa");
        message.Subject         = title;
        message.Body            = body;
        /**
         * 
         * Remove the following line when testing is complete
         * 
         */
        //message.Body = "\r\nPlease disregard this email as it has been sent for testing purposes. Sorry for inconvenience. Administrator Faculty Promotion System\r\n" + message.Body;

        /* add footer */
        string footer = "\r\n";
        
        footer  = "\r\n------------------------------------------";
        footer += "\r\nThis Message has been sent through:";
        footer += "\r\n[ The Faculty Promotion System ]";
        footer += "\r\nhttp://www.kfupm.edu.sa/FacultyPromotionSystem/";
        /**
         * 
         * Remove the following line when testing is complete
         * 
         */
        //footer += "\r\nPlease disregard this email as it has been sent for testing purposes. Sorry for inconvenience. Administrator Faculty Promotion System\r\n";

        message.Body += footer;
        

        SmtpClient client = new SmtpClient();
        MailAddressCollection mailAC = new MailAddressCollection();
        ToEmail = Regex.Replace(ToEmail, "\\s", "");
        string[] args = ToEmail.Split(',');
        string FinalEmailAddressString = "";
        foreach (string e in args)
        {
            if (!e.Contains('@'))
            {
                mailAC.Add(e + ConfigurationManager.AppSettings["OrganizationEmail"]);
                FinalEmailAddressString = FinalEmailAddressString + e + ConfigurationManager.AppSettings["OrganizationEmail"] + ",";
            }
            else
            {
                mailAC.Add(e);
                FinalEmailAddressString = FinalEmailAddressString + e + ",";
            }
        }
        FinalEmailAddressString = FinalEmailAddressString.Trim(',');
        if (mailAC.Count == 1)
        {
            message.To.Add(FinalEmailAddressString);
        }
        else
        {
            message.Bcc.Add(FinalEmailAddressString);
        }
        if (bal.GetWorkflowAttribute().Where(a => a.Attribute == GlobalAttribute.StopEmail.ToString()).Count() > 0
            && bal.GetWorkflowAttribute().Where(a => a.Attribute == GlobalAttribute.StopEmail.ToString()).ToList()[0].Value == "false"
            )
        {
          //   client.Send(message);
        }
        bal.InsertSentEmail(DateTime.Now, ToEmail, message.Subject, Cryptography.Encrypt(message.Body), message.From.ToString(), appID);     
    }
}
