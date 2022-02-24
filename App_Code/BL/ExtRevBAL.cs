using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using BL.Data;

/// <summary>
/// Summary description for ExtRevBAL
/// </summary>
public class ExtRevBAL
{
	public ExtRevBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region search Ext Rev
    public List<ExternalReviewer> SearchExternalReviewer(string MainSearchString)
    {
        
        string[] srchStrArr = new string[5];
        char[] separator = new char[2];
        string srchStr;
        List<ExternalReviewer> extRevList = GetAllExtRev();
        List<ExternalReviewer> searchResultList = new List<ExternalReviewer>();
        if (MainSearchString == null || MainSearchString == "")
        {
            return searchResultList;
        }
        bool isDuplicateRow = false;
        if (MainSearchString.Contains("::"))
        {
            string deptSN = MainSearchString.Remove(0, MainSearchString.IndexOf("@")+1);
            if (MainSearchString.Contains("college"))
            {
                foreach (var item in GetForm_ExtRev().Where(a => a.Application.Employee.Department1.ShortName == deptSN && a.Type == (int)ViewType.College))
                {
                    searchResultList.Add(item.ExternalReviewer);
                }
                return searchResultList;
            }
            else if (MainSearchString.Contains("department"))
            {
                foreach (var item in GetForm_ExtRev().Where(a => a.Application.Employee.Department1.ShortName.ToLower() == deptSN && a.Type == (int)ViewType.Department))
                {
                    searchResultList.Add(item.ExternalReviewer);
                }
                return searchResultList;
            }
        }
        foreach (ExternalReviewer extRev in extRevList)
        {
            try
            {
                if (extRev.Name.ToLower().Contains(MainSearchString.ToLower())
                    || extRev.Email.ToLower().Equals(MainSearchString.ToLower())
                    )

                {
                    foreach (ExternalReviewer extRevOld in searchResultList)
                    {
                        if (isDuplicateRow = extRev.ExternalReviewerID == extRevOld.ExternalReviewerID) break;
                    }
                    if (!isDuplicateRow)
                    {
                        searchResultList.Add(extRev);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        srchStrArr = MainSearchString.Split(separator);
        for (int i = 0; i < srchStrArr.Length; i++)
        {
            srchStr = srchStrArr[i];
            foreach (ExternalReviewer extRev in extRevList)
            {
                try
                {
                    if (extRev.Name.ToLower().Contains(srchStr.ToLower()))
                    {

                        foreach (ExternalReviewer extRevOld in searchResultList)
                        {
                            if (isDuplicateRow = extRev.ExternalReviewerID == extRevOld.ExternalReviewerID)
                            {
                                break;
                            }
                        }
                        if (!isDuplicateRow)
                        {
                            searchResultList.Add(extRev);
                        }


                    }
                }
                catch (Exception)
                {

                }
            }
        }
        return searchResultList;
    }
    #endregion 
    #region ExternalReviewer
    public List<ExternalReviewer> GetAllExtRev()
    {
        var context = new FPSDBEntities();
        var data = (from er in context.ExternalReviewers
                    select er).ToList();
        return data;
    }
    public List<ExternalReviewer> GetExtRevByID(int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from er in context.ExternalReviewers
                    where er.ExternalReviewerID == ExternalReviewerID
                    select er).ToList();
        return data;                  
    }   
    public List<ExternalReviewer> GetExtRevByEmail(string Email)
    {
        var context = new FPSDBEntities();
        var data = (from er in context.ExternalReviewers
                    where er.Email== Email
                    select er).ToList();
        return data;
    }
    public List<ExternalReviewer> GetExtRevByEmailPwd(string email, string password)
    {
        string encryptedPassword = Cryptography.Encrypt(password);

        var context = new FPSDBEntities();
        var data = (from er in context.ExternalReviewers
                    where
                    (
                    er.Email == email && (er.Password == password || er.Password == encryptedPassword)&& er.Status == RecordStatus.Active.ToString()
                    )
                    select er).ToList();
        return data;
    }
    public void UpdateExtRev(int ExternalReviewerID, string Name, string Rank, string MailingAddress, string Email, string Major, string Specialty, string PhoneAndFax, string ActiveAreaOfResearch,
            string PrevAreaOfResearch, string Webpage, string Comments, int? TotalPublications, int? NoOfJournals, int? HIndex, int? Citations,
            string Status, string Password, string IBAN, string PassportNo, string NameString, string Description, string Name_1,
            string Name_2, string Name_3, string Name_4, string Salt)
    {
        var context = new FPSDBEntities();
        var query = from er in context.ExternalReviewers
                    where
                    er.ExternalReviewerID == ExternalReviewerID
                    select er;
        foreach (var row in query)
        {
            row.Name = Name;
            row.Rank = Rank;
            row.MailingAddress = MailingAddress;
            row.Email = Email;
            row.Major = Major;
            row.Specialty = Specialty;
            row.PhoneAndFax = PhoneAndFax;
            row.ActiveAreaOfResearch = ActiveAreaOfResearch;
            row.PrevAreaOfResearch = PrevAreaOfResearch;
            row.Webpage = Webpage;
            row.Comments = Comments;
            row.TotalPublications = TotalPublications;
            row.NoOfJournals = NoOfJournals;
            row.HIndex = HIndex;
            row.Citations = Citations;
            row.IBAN = IBAN;
            row.PassportNo = PassportNo;
            row.Status = Status;
            row.Password = Password;
            row.NameString = NameString; row.UpdateDate = DateTime.Now;
            row.Description = Description;
            row.Name_1 = Name_1;
            row.Name_2 = Name_2;
            row.Name_3 = Name_3;
            row.Name_4 = Name_4;
            row.Salt = Salt;
        }
        context.SaveChanges();
    }
    public int InsertExtRev(string Name, string Rank, string MailingAddress, string Email, string Major, string Specialty, string PhoneAndFax, string ActiveAreaOfResearch,
        string PrevAreaOfResearch, string Webpage, string Comments, int? TotalPublications, int? NoOfJournals, int? HIndex, int? Citations, string Status, string Password, string IBAN,
        string PassportNo, string NameString, string Description, string Name_1,
        string Name_2, string Name_3, string Name_4, string Salt)
    {
        var context = new FPSDBEntities();
        Name = Regex.Replace(Name, "<[^>]*(>|$)", string.Empty);
        ExternalReviewer er = new ExternalReviewer
        {
            Name = Name,
            Rank = Rank,
            MailingAddress = MailingAddress,
            Email = Email,
            Major = Major,
            Specialty = Specialty,
            PhoneAndFax = PhoneAndFax,
            ActiveAreaOfResearch = ActiveAreaOfResearch,
            PrevAreaOfResearch = PrevAreaOfResearch,
            Webpage = Webpage,
            Comments = Comments,
            TotalPublications = TotalPublications,
            NoOfJournals = NoOfJournals,
            HIndex = HIndex,
            Citations = Citations,
            PassportNo = PassportNo,
            IBAN = IBAN,
            NameString = NameString,
            Status = Status,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now,
            Description = Description,
            Name_1 = Name_1,
            Name_2 = Name_2,
            Name_3 = Name_3,
            Name_4 = Name_4,
            Salt = Salt,
        };
        context.ExternalReviewers.Add(er);
        context.SaveChanges();
        return er.ExternalReviewerID;
    }
    //public int InsertExtRev(string Name,string Rank,string MailingAddress,string Email,string Major,string Speciality,string PhoneAndFax,string ActiveAreaOfResearch,
    //    string PrevAreaOfResearch,string Webpage,string Comments,int? TotalPublications,int? NoOfJournals,int? HIndex,int? Citations,string SWIFTCode,string IBAN)
    //{
    //    var context = new FPSDBEntities();
    //    Name = Regex.Replace(Name, "<[^>]*(>|$)", string.Empty);
    //    ExternalReviewer er = new ExternalReviewer
    //    {
    //        Name = Name,
    //        Rank = Rank,
    //        MailingAddress = MailingAddress,
    //        Email = Email,
    //        Major = Major,
    //        Speciality = Speciality,
    //        PhoneAndFax = PhoneAndFax,
    //        ActiveAreaOfResearch = ActiveAreaOfResearch,
    //        PrevAreaOfResearch = PrevAreaOfResearch,
    //        Webpage = Webpage,
    //        Comments = Comments,
    //        TotalPublications = TotalPublications,
    //        NoOfJournals = NoOfJournals,
    //        HIndex = HIndex,
    //        Citations = Citations,
    //        SWIFTCode = SWIFTCode,
    //        IBAN = IBAN
    //    };
    //    context.ExternalReviewers.Add(er);
    //    context.SaveChanges();
    //    return er.ExternalReviewerID;
    //}
    public void DeleteExtRev(int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var query = from er in context.ExternalReviewers
                    where er.ExternalReviewerID == ExternalReviewerID
                    select er;
        foreach (var row in query)
        {
            context.ExternalReviewers.Remove(row);
        }
        context.SaveChanges();

    }
    #endregion
    #region Form_ExtRev
    public List<Form_ExtRev> GetForm_ExtRev(int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from fer in context.Form_ExtRev
                    where
                    (
                    fer.ExternalReviewerID == ExternalReviewerID
                    )
                    select fer).ToList();
        return data;
    }
    public List<Form_ExtRev> GetForm_ExtRevByAppID(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fer in context.Form_ExtRev
                    where
                    (
                    fer.ApplicationID == ApplicationID

                    )
                    select fer).ToList();
        return data;
    }
    public List<Form_ExtRev> GetForm_ExtRev(int ApplicationID, int Type)
    {
        var context = new FPSDBEntities();
        var data = (from fer in context.Form_ExtRev
                    where
                    (
                    fer.ApplicationID == ApplicationID &&
                    fer.Type == Type
                    )
                    select fer).ToList();
        return data;
    }
    public List<Form_ExtRev> GetForm_ExtRevByPK(int ApplicationID, int Type, int Serial)
    {
        var context = new FPSDBEntities();
        var data = (from fer in context.Form_ExtRev
                    where
                    (
                    fer.ApplicationID == ApplicationID &&
                    fer.Type == Type &&
                    fer.Serial == Serial
                    )
                    select fer).ToList();
        return data;
    }
  
    public void InsertFormExtRev(int ApplicationID, int Type, int Serial, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        context.Form_ExtRev.Add(new Form_ExtRev
        {
            ApplicationID = ApplicationID,
            ExternalReviewerID = ExternalReviewerID,
            Serial = Serial,
            Type = Type,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeleteFormExtRev(int ApplicationID, int Type, int Serial)
    {
        var context = new FPSDBEntities();
        var query = from fer in context.Form_ExtRev
                    where fer.ApplicationID == ApplicationID &&
                    fer.Type == Type &&
                    fer.Serial == Serial
                    select fer;
        foreach (var row in query)
        {
            context.Form_ExtRev.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteFormExtRev(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from fer in context.Form_ExtRev
                    where fer.ApplicationID == ApplicationID
                    select fer;
        foreach (var row in query)
        {
            context.Form_ExtRev.Remove(row);
        }
        context.SaveChanges();

    }
    
    
    public List<Form_ExtRev> GetForm_ExtRev()
    {
        var context = new FPSDBEntities();
        var data = (from fer in context.Form_ExtRev
                    select fer).ToList();
        return data;
    }
    //public List<Form_ExtRev> GetForm_ExtRev(int ExternalReviewerID)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from fer in context.Form_ExtRev
    //                where
    //                (
    //                fer.ExternalReviewerID == ExternalReviewerID
    //                )
    //                select fer).ToList();
    //    return data;
    //}
    public List<Form_ExtRev> GetFormExtRevByAppID(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fer in context.Form_ExtRev

                    where
                    (
                    fer.ApplicationID == ApplicationID

                    )
                    select fer).ToList();
        return data;
    }
  
    public List<Form_ExtRev> GetFormExtRevByAppIDType(int ApplicationID, int Type)
    {
        var context = new FPSDBEntities();
        var data = (from fer in context.Form_ExtRev
                    where
                    (
                    fer.ApplicationID == ApplicationID &&
                    fer.Type == Type
                    )
                    select fer).ToList();
        return data;
    }
    //public List<Form_Refree1> GetFormExtRevByAppIDTypeOld(int ApplicationID, byte Type)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from fer in context.Form_Refrees                   
    //                where
    //                (
    //                fer.ApplicationID == ApplicationID &&
    //                fer.Type == Type
    //                )
    //                select new Form_Refree1
    //                {
    //                    ApplicationID = ApplicationID,

    //                    Serial = fer.Serial,
    //                    Type = fer.Type,
    //                    Name = fer.Name,
    //                    Rank = fer.Rank,
    //                    MailingAddress = fer.MailingAddress,
    //                    Email = fer.Email,
    //                    Major = fer.Major,
    //                    Speciality = fer.Speciality,
    //                    PhoneAndFax = fer.PhoneAndFax,
    //                    ActiveAreaOfResearch = fer.ActiveAreaOfResearch,
    //                    PrevAreaOfResearch = fer.PrevAreaOfResearch,
    //                    Webpage = fer.Webpage,
    //                    Comments = fer.Comments,
    //                    TotalPublications = fer.TotalPublications,
    //                    NoOfJournals = fer.NoOfJournals,
    //                    HIndex = fer.HIndex,
    //                    Citations = fer.Citations,
    //                    //SWIFTCode = er.SWIFTCode,
    //                    //IBAN = fer.IBAN
    //                }).ToList();
    //    return data;
    //}
    public List<Form_ExtRev> GetFormExtRevByPK(int ApplicationID, int Type, int Serial)
    {
        var context = new FPSDBEntities();
        var data = (from fer in context.Form_ExtRev
                    where
                    (
                    fer.ApplicationID == ApplicationID &&
                    fer.Type == Type &&
                    fer.Serial == Serial
                    )
                    select fer).ToList();
        return data;
    }
 
    //public void InsertFormExtRev(int ApplicationID, int Type, int Serial, int ExternalReviewrID)
    //{
    //    var context = new FPSDBEntities();
    //    context.Form_ExtRev.Add(new Form_ExtRev
    //    {
    //        ApplicationID = ApplicationID,
    //        Serial = Serial,
    //        Type = Type,
    //        ExternalReviewerID = ExternalReviewrID            
    //    });
    //    context.SaveChanges();
    //}

    //public void DeleteFormExtRev(int ApplicationID, int Type, int Serial)
    //{
    //    var context = new FPSDBEntities();
    //    var query = from fer in context.Form_ExtRev
    //                where fer.ApplicationID == ApplicationID &&
    //                fer.Type == Type &&
    //                fer.Serial == Serial
    //                select fer;
    //    foreach (var row in query)
    //    {
    //        context.Form_ExtRev.Remove(row);
    //    }
    //    context.SaveChanges();

    //}
    public void DeleteForm_ExtRev(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from fer in context.Form_ExtRev
                    where fer.ApplicationID == ApplicationID                    
                    select fer;
        foreach (var row in query)
        {
            context.Form_ExtRev.Remove(row);
        }
        context.SaveChanges();

    }
    #endregion
    #region Form_FinalExtRev
    public List<Form_FinalExtRev> GetForm_FinalExtRev()
    {
        var context = new FPSDBEntities();
        var data = (from ffer in context.Form_FinalExtRev
                    select ffer).OrderBy(n => n.Serial).ToList();
        return data;
    }
    public List<Form_FinalExtRev> GetForm_FinalExtRev(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from ffer in context.Form_FinalExtRev
                    where
                    (
                         ffer.ApplicationID == ApplicationID
                    )
                    select ffer).OrderBy(n => n.Serial).ToList();
        return data;
    }
    public List<Form_FinalExtRev> GetForm_FinalExtRev(int ApplicationID, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from ffer in context.Form_FinalExtRev
                    where
                    (
                    ffer.ApplicationID == ApplicationID &&
                    ffer.ExternalReviewerID == ExternalReviewerID
                    )
                    select ffer).ToList();
        return data;
    }
    public List<Form_FinalExtRev> GetForm_FinalExtRevByAppIDSer(int ApplicationID, int Serial)
    {
        var context = new FPSDBEntities();
        var data = (from ffer in context.Form_FinalExtRev
                    join er in context.ExternalReviewers on ffer.ExternalReviewerID equals er.ExternalReviewerID
                    where
                    (
                    ffer.ApplicationID == ApplicationID &&
                    ffer.Serial == Serial
                    )
                    select ffer).ToList();
        return data;
    }
    public void UpdateFormFinalExtRev(int ApplicationID, int ExternalReviewerID, int? Serial, string WLStatus, DateTime? WLDate, string CommentsWithWL,
        string MaterialSentStatus, DateTime? MaterialSentDate, string EvaluationStatus, DateTime? EvaluationDate, string CommentsWithEval,
        bool? ShowExtRev2PC, bool? ShowEval2PC, string UserName, string Password
        , string Source, bool? PromotionRecom, string Reasons, int? Score)
    {
        var context = new FPSDBEntities();
        var query = from ffer in context.Form_FinalExtRev
                    where
                    ffer.ApplicationID == ApplicationID &&
                    ffer.ExternalReviewerID == ExternalReviewerID
                    select ffer;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.ExternalReviewerID = ExternalReviewerID;
            row.Serial = Serial;
            row.WLStatus = WLStatus;
            row.WLDate = WLDate;
            row.CommentsWithWL = CommentsWithWL;
            row.MaterialSentStatus = MaterialSentStatus;
            row.MaterialSentDate = MaterialSentDate;
            row.EvaluationStatus = EvaluationStatus;
            row.EvaluationDate = EvaluationDate;
            row.CommentsWithEval = CommentsWithEval;
            row.ShowExtRev2PC = ShowExtRev2PC;
            row.ShowEval2PC = ShowEval2PC;
            row.UserName = UserName;
            row.Password = Password;
            row.Source = Source;
            row.UpdateDate = DateTime.Now;
            row.PromotionRecom = PromotionRecom;
            row.Reasons = Reasons;
            row.Score = Score;
        }
        context.SaveChanges();
    }
    public void UpdateFormFinalExtRevShowExtRev2PC(bool? ShowExtRev2PC, int ApplicationID, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var query = from ffer in context.Form_FinalExtRev
                    where
                    ffer.ApplicationID == ApplicationID &&
                    ffer.ExternalReviewerID == ExternalReviewerID
                    select ffer;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.ExternalReviewerID = ExternalReviewerID;
            row.Serial = row.Serial;
            row.WLStatus = row.WLStatus;
            row.WLDate = row.WLDate;
            row.CommentsWithWL = row.CommentsWithWL;
            row.MaterialSentStatus = row.MaterialSentStatus;
            row.MaterialSentDate = row.MaterialSentDate;
            row.EvaluationStatus = row.EvaluationStatus;
            row.EvaluationDate = row.EvaluationDate;
            row.CommentsWithEval = row.CommentsWithEval;
            row.ShowExtRev2PC = ShowExtRev2PC;
            row.ShowEval2PC = row.ShowEval2PC;
            row.UserName = row.UserName;
            row.Password = row.Password;
            row.Source = row.Source; row.UpdateDate = DateTime.Now;

        }
        context.SaveChanges();
    }
    public void UpdateFormFinalExtRevShowEval2PC(bool? ShowEval2PC, int ApplicationID, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var query = from ffer in context.Form_FinalExtRev
                    where
                    ffer.ApplicationID == ApplicationID &&
                    ffer.ExternalReviewerID == ExternalReviewerID
                    select ffer;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.ExternalReviewerID = ExternalReviewerID;
            row.Serial = row.Serial;
            row.WLStatus = row.WLStatus;
            row.WLDate = row.WLDate;
            row.CommentsWithWL = row.CommentsWithWL;
            row.MaterialSentStatus = row.MaterialSentStatus;
            row.MaterialSentDate = row.MaterialSentDate;
            row.EvaluationStatus = row.EvaluationStatus;
            row.EvaluationDate = row.EvaluationDate;
            row.CommentsWithEval = row.CommentsWithEval;
            row.ShowExtRev2PC = row.ShowExtRev2PC;
            row.ShowEval2PC = ShowEval2PC;
            row.UserName = row.UserName;
            row.Password = row.Password;
            row.Source = row.Source; row.UpdateDate = DateTime.Now;

        }
        context.SaveChanges();
    }
    public void InsertFormFinalExtRev(int ApplicationID, int ExternalReviewerID, int? Serial, string WLStatus, DateTime? WLDate, string CommentsWithWL,
        string MaterialSentStatus, DateTime? MaterialSentDate, string EvaluationStatus, DateTime? EvaluationDate, string CommentsWithEval,
        bool? ShowExtRev2PC, bool? ShowEval2PC, string UserName, string Password
        , string Source, bool? PromotionRecommendation, string Reasons, int? Score)
    {
        var context = new FPSDBEntities();
        context.Form_FinalExtRev.Add(new Form_FinalExtRev
        {
            ApplicationID = ApplicationID,
            ExternalReviewerID = ExternalReviewerID,
            Serial = Serial,
            WLStatus = WLStatus,
            WLDate = WLDate,
            CommentsWithWL = CommentsWithWL,
            MaterialSentStatus = MaterialSentStatus,
            MaterialSentDate = MaterialSentDate,
            EvaluationStatus = EvaluationStatus,
            EvaluationDate = EvaluationDate,
            CommentsWithEval = CommentsWithEval,
            ShowExtRev2PC = ShowExtRev2PC,
            ShowEval2PC = ShowEval2PC,
            UserName = UserName,
            Password = Password,
            Source = Source,
            PromotionRecom = PromotionRecommendation,
            Reasons = Reasons,
            Score = Score,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now

        });
        context.SaveChanges();
    }

    public void DeleteFormFinalExtRev(int ApplicationID, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var query = from ffer in context.Form_FinalExtRev
                    where ffer.ApplicationID == ApplicationID &&
                    ffer.ExternalReviewerID == ExternalReviewerID
                    select ffer;
        foreach (var row in query)
        {
            context.Form_FinalExtRev.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteFormFinalExtRev(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from ffer in context.Form_FinalExtRev
                    where ffer.ApplicationID == ApplicationID
                    select ffer;
        foreach (var row in query)
        {
            context.Form_FinalExtRev.Remove(row);
        }
        context.SaveChanges();

    }
    public string GetSourceForFinalExtRev(int extRevID, int applicationID)
    {
        string source = "";
        List<Form_ExtRev> lExtRev = GetForm_ExtRevByAppID(applicationID);
        foreach (Form_ExtRev extRev in lExtRev)
        {
            if (extRev.ExternalReviewerID == extRevID)
            {
                source += GetTypeString(extRev.Type) + (extRev.Serial+1) + " ";
            }
        }
        return source == "" ? ConfigurationManager.AppSettings["TopAuthority_TitleShort"] : source;

    }

    //public List<Form_FinalExtRev> GetForm_FinalExtRev()
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from ffer in context.Form_FinalExtRev
    //                select ffer).OrderBy(n => n.Serial).ToList();
    //    return data;
    //}
    //public List<Form_FinalExtRev> GetForm_FinalExtRevByAppIDSer(int ApplicationID, int Serial)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from ffer in context.Form_FinalExtRev
    //                join er in context.ExternalReviewers on ffer.ExternalReviewerID equals er.ExternalReviewerID
    //                where
    //                (
    //                ffer.ApplicationID == ApplicationID &&
    //                ffer.Serial == Serial
    //                )
    //                select ffer).ToList();
    //    return data;
    //}
    //public List<Form_FinalExtRev> GetForm_FinalExtRev(int ApplicationID)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from ffer in context.Form_FinalExtRev
    //                join er in context.ExternalReviewers on ffer.ExternalReviewerID equals er.ExternalReviewerID
    //                where
    //                (
    //                     ffer.ApplicationID == ApplicationID
    //                )
    //                select ffer).OrderBy(n => n.Serial).ToList();
    //    return data;
    //}
    public List<Form_FinalExtRev> GetFormFinalExtRevByPK(int ApplicationID, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from ffer in context.Form_FinalExtRev
                    where
                    (
                    ffer.ApplicationID == ApplicationID &&
                    ffer.ExternalReviewerID == ExternalReviewerID
                    )
                    select ffer).ToList();
        return data;
    }
    public List<Form_FinalExtRev> GetFormFinalExtRevByAppIDSer(int ApplicationID, int Serial)
    {
        var context = new FPSDBEntities();
        var data = (from ffer in context.Form_FinalExtRev
                    where
                    (
                    ffer.ApplicationID == ApplicationID &&
                    ffer.Serial == Serial
                    )
                    select ffer).ToList();
        return data;
    }
    //public List<Form_FinalExtRev> GetForm_FinalExtRev(int ApplicationID, int ExternalReviewerID)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from ffer in context.Form_FinalExtRev
    //                join er in context.ExternalReviewers on ffer.ExternalReviewerID equals er.ExternalReviewerID
    //                where
    //                (
    //                ffer.ApplicationID == ApplicationID &&
    //                ffer.ExternalReviewerID == ExternalReviewerID
    //                )
    //                select ffer).ToList();
    //    return data;
    //}
    //public void UpdateFormFinalExtRevShowEval2PC(bool? ShowEval2PC, int ApplicationID, int ExternalReviewerID)
    //{
    //    var context = new FPSDBEntities();
    //    var query = from ffer in context.Form_FinalExtRev
    //                where
    //                ffer.ApplicationID == ApplicationID &&
    //                ffer.ExternalReviewerID == ExternalReviewerID
    //                select ffer;
    //    foreach (var row in query)
    //    {
    //        row.ApplicationID = ApplicationID;
    //        row.ExternalReviewerID = ExternalReviewerID;
    //        row.Serial = row.Serial;
    //        row.WLStatus = row.WLStatus;
    //        row.WLDate = row.WLDate;
    //        row.CommentsWithWL = row.CommentsWithWL;
    //        row.MaterialSentStatus = row.MaterialSentStatus;
    //        row.MaterialSentDate = row.MaterialSentDate;
    //        row.EvaluationStatus = row.EvaluationStatus;
    //        row.EvaluationDate = row.EvaluationDate;
    //        row.CommentsWithEval = row.CommentsWithEval;
    //        row.ShowExtRev2PC = row.ShowExtRev2PC;
    //        row.ShowEval2PC = ShowEval2PC;
    //        row.UserName = row.UserName;
    //        row.Password = row.Password;
    //        row.Source = row.Source;

    //    }
    //    context.SaveChanges();
    //}

    //public void UpdateFormFinalExtRevShowExtRev2PC(bool? ShowExtRev2PC, int ApplicationID, int ExternalReviewerID)
    //{
    //    var context = new FPSDBEntities();
    //    var query = from ffer in context.Form_FinalExtRev
    //                where
    //                ffer.ApplicationID == ApplicationID &&
    //                ffer.ExternalReviewerID == ExternalReviewerID
    //                select ffer;
    //    foreach (var row in query)
    //    {
    //        row.ApplicationID = ApplicationID;
    //        row.ExternalReviewerID = ExternalReviewerID;
    //        row.Serial = row.Serial;
    //        row.WLStatus = row.WLStatus;
    //        row.WLDate = row.WLDate;
    //        row.CommentsWithWL = row.CommentsWithWL;
    //        row.MaterialSentStatus = row.MaterialSentStatus;
    //        row.MaterialSentDate = row.MaterialSentDate;
    //        row.EvaluationStatus = row.EvaluationStatus;
    //        row.EvaluationDate = row.EvaluationDate;
    //        row.CommentsWithEval = row.CommentsWithEval;
    //        row.ShowExtRev2PC = ShowExtRev2PC;
    //        row.ShowEval2PC = row.ShowEval2PC;
    //        row.UserName = row.UserName;
    //        row.Password = row.Password;
    //        row.Source = row.Source;

    //    }
    //    context.SaveChanges();
    //}
    public void UpdateFormFinalExtRev(int ApplicationID, int ExternalReviewerID, int? Serial, string WLStatus, DateTime? WLDate, string CommentsWithWL,
        string MaterialSentStatus, DateTime? MaterialSentDate, string EvaluationStatus, DateTime? EvaluationDate, string CommentsWithEval,
        bool? ShowExtRev2PC, bool? ShowEval2PC, string UserName, string Password, string Source, int? EvaluationID)
    {
        var context = new FPSDBEntities();
        var query = from ffer in context.Form_FinalExtRev
                    where
                    ffer.ApplicationID == ApplicationID &&
                    ffer.ExternalReviewerID == ExternalReviewerID
                    select ffer;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.ExternalReviewerID = ExternalReviewerID;
            row.Serial = Serial;
            row.WLStatus = WLStatus;
            row.WLDate = WLDate;
            row.CommentsWithWL = CommentsWithWL;
            row.MaterialSentStatus = MaterialSentStatus;
            row.MaterialSentDate = MaterialSentDate;
            row.EvaluationStatus = EvaluationStatus;
            row.EvaluationDate = EvaluationDate;
            row.CommentsWithEval = CommentsWithEval;
            row.ShowExtRev2PC = ShowExtRev2PC;
            row.ShowEval2PC = ShowEval2PC;
            row.UserName = UserName;
            row.Password = Password;
            row.Source = Source;
            row.EvaluationID = EvaluationID;
        }
        context.SaveChanges();
    }
    public void InsertFormFinalExtRev(int ApplicationID, int ExternalReviewerID, int? Serial, string WLStatus, DateTime? WLDate, string CommentsWithWL,
        string MaterialSentStatus, DateTime? MaterialSentDate, string EvaluationStatus, DateTime? EvaluationDate, string CommentsWithEval,
        bool? ShowExtRev2PC, bool? ShowEval2PC, string UserName, string Password, string Source, int? EvaluationID)
    {
        var context = new FPSDBEntities();
        context.Form_FinalExtRev.Add(new Form_FinalExtRev
        {
            ApplicationID = ApplicationID,
            ExternalReviewerID = ExternalReviewerID,
            Serial = Serial,
            WLStatus = WLStatus,
            WLDate = WLDate,
            CommentsWithWL = CommentsWithWL,
            MaterialSentStatus = MaterialSentStatus,
            MaterialSentDate = MaterialSentDate,
            EvaluationStatus = EvaluationStatus,
            EvaluationDate = EvaluationDate,
            CommentsWithEval = CommentsWithEval,
            ShowExtRev2PC = ShowExtRev2PC,
            ShowEval2PC = ShowEval2PC,
            UserName = UserName,
            Password = Password,
            Source = Source,
            EvaluationID = EvaluationID
        });
        context.SaveChanges();
    }
    #endregion
    //public void DeleteFormFinalExtRev(int ApplicationID, int ExternalReviewerID)
    //{
    //    var context = new FPSDBEntities();
    //    var query = from ffer in context.Form_FinalExtRev
    //                where ffer.ApplicationID == ApplicationID &&
    //                ffer.ExternalReviewerID == ExternalReviewerID
    //                select ffer;
    //    foreach (var row in query)
    //    {
    //        context.Form_FinalExtRev.Remove(row);
    //    }
    //    context.SaveChanges();

    //}
    public void DeleteForm_FinalExtRev(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from ffer in context.Form_FinalExtRev
                    where ffer.ApplicationID == ApplicationID 
                    select ffer;
        foreach (var row in query)
        {
            context.Form_FinalExtRev.Remove(row);
        }
        context.SaveChanges();

    }
    public string GetSourceForFinalExtRev(int extRevID, int applicationID, byte CurRoleID)
    {
        string source = "";
        List<Form_ExtRev> lExtRev = GetFormExtRevByAppID(applicationID);
        foreach (Form_ExtRev extRev in lExtRev)
        {
            if (extRev.ExternalReviewerID == extRevID)
            {
                source += GetTypeString(extRev.Type) + (extRev.Serial + 1) + " ";
            }                
        }
        return source == "" ? CurRoleID == (byte) RoleID.TopAuthority? "VPRI":"SCSubComm" : source;

    }
    private string GetTypeString(int p)
    {
        switch (p)
        {
            case 0: return "A";
            case 1: return "D";
            case 2: return "C";
            case 3: return "SCSubCom";
            default: return "VPRI";
        }
    }
}