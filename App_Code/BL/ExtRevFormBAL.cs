using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using BL.Data;

/// <summary>
/// Summary description for RevFormBAL
/// </summary>
public class ExtRevFormBAL
{
	public ExtRevFormBAL()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    #region Evaluation Form 
    #region PublicationsEvaluation
    public List<PublicationsEvaluation> GetPublicationsEvaluation()
    {
        var context = new FPSDBEntities();
        var data = (from pe in context.PublicationsEvaluations
                    select pe).ToList();
        return data;
    }
    public List<PublicationsEvaluation> GetPublicationsEvaluationByForm_AttID(int Form_AttachmentID)
    {
        var context = new FPSDBEntities();
        var data = (from pe in context.PublicationsEvaluations
                    where pe.Form_AttachmentID == Form_AttachmentID
                    select pe).ToList();
        return data;
    }
    public List<PublicationsEvaluation> GetPublicationsEvaluation(int ApplicationID, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from pe in context.PublicationsEvaluations
                    where pe.ApplicationID == ApplicationID
                    && pe.ExternalReviewerID == ExternalReviewerID
                    select pe).ToList();
        return data;
    }
    public List<PublicationsEvaluation> GetPublicationsEvaluation(int ApplicationID, int ExternalReviewerID, int form_AttachmentID)
    {
        var context = new FPSDBEntities();
        var data = (from pe in context.PublicationsEvaluations
                    where pe.ApplicationID == ApplicationID
                    && pe.ExternalReviewerID == ExternalReviewerID
                    && pe.Form_AttachmentID == form_AttachmentID
                    select pe).ToList();
        return data;
    }
    public void DeletePublicationsEvaluation(int ApplicationID, int ExternalReviewerID, int Form_AttachmentID)
    {
        var context = new FPSDBEntities();
        var query = from pe in context.PublicationsEvaluations
                    where
                      pe.ApplicationID == ApplicationID &&
                      pe.ExternalReviewerID == ExternalReviewerID &&
                      pe.Form_AttachmentID == Form_AttachmentID
                    select pe;
        foreach (var row in query)
        {
            context.PublicationsEvaluations.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeletePublicationsEvaluation(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from pe in context.PublicationsEvaluations
                    where
                      pe.ApplicationID == ApplicationID
                    select pe;
        foreach (var row in query)
        {
            context.PublicationsEvaluations.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeletePublicationsEvaluation(int ApplicationID, int Form_AttachmentID)
    {
        var context = new FPSDBEntities();
        var query = from pe in context.PublicationsEvaluations
                    where
                      pe.ApplicationID == ApplicationID &&
                      pe.Form_AttachmentID == Form_AttachmentID
                    select pe;
        foreach (var row in query)
        {
            context.PublicationsEvaluations.Remove(row);
        }
        context.SaveChanges();

    }
    public void UpdatePublicationsEvaluation(int ApplicationID, int ExternalReviewerID, int Form_AttachmentID, int? EvaluationMark, string Remarks)
    {
        var context = new FPSDBEntities();
        var query = from pe in context.PublicationsEvaluations
                    where pe.ApplicationID == ApplicationID &&
                      pe.ExternalReviewerID == ExternalReviewerID &&
                      pe.Form_AttachmentID == Form_AttachmentID
                    select pe;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.EvaluationMark = EvaluationMark;
            row.Remarks = Remarks;
        }
        context.SaveChanges();
    }
    public void InsertPublicationsEvaluation(int ApplicationID, int ExternalReviewerID, int Form_AttachmentID, int? EvaluationMark, string Remarks)
    {
        var context = new FPSDBEntities();
        context.PublicationsEvaluations.Add(new PublicationsEvaluation
        {

            ApplicationID = ApplicationID,
            ExternalReviewerID = ExternalReviewerID,
            Form_AttachmentID = Form_AttachmentID,
            EvaluationMark = EvaluationMark,
            Remarks = Remarks,
        });
        context.SaveChanges();

    }
    #endregion
    #region Evaluation
    public List<Evaluation> GetEvaluation(int ApplicationID)
    {

        var context = new FPSDBEntities();
        var data = (from e in context.Evaluations
                    where e.ApplicationID == ApplicationID                    
                    select e).ToList();
        return data;

    }
    public List<Evaluation> GetEvaluation(int ApplicationID, int ExternalReviewerID, string Language)
    {

        var context = new FPSDBEntities();
        var data = (from e in context.Evaluations
                    where e.ApplicationID == ApplicationID
                    && e.ExternalReviewerID == ExternalReviewerID
                    && e.Lang == Language
                    select e).ToList();
        return data;

    }
    public List<Evaluation> GetEvaluation(int ApplicationID,string EmployeeID, int ExternalEmployeeID, int ExternalReviewerID, string Language)
    {

        var context = new FPSDBEntities();
        var data = (from e in context.Evaluations
                    where e.ApplicationID == ApplicationID
                    && e.ExternalReviewerID == ExternalReviewerID
                    && e.EmployeeID == EmployeeID
                    && e.ExternalEmployeeID == ExternalEmployeeID
                    && e.Lang == Language
                    select e).ToList();
        return data;

    }
    public List<Evaluation> GetEvaluation(int ApplicationID, string EmployeeID, int ExternalEmployeeID, int ExternalReviewerID, string Language, string SourceJSON)
    {

        var context = new FPSDBEntities();
        var data = (from e in context.Evaluations
                    where e.ApplicationID == ApplicationID
                    && e.ExternalReviewerID == ExternalReviewerID
                    && e.EmployeeID == EmployeeID
                    && e.ExternalEmployeeID == ExternalEmployeeID
                    && e.Lang == Language
                    && e.SourceJSON == SourceJSON
                    select e).ToList();
        return data;

    }
    public void GetEvaluationAttachments(int appID, int extRevID, string dataPath, out FileInfo[] files)
    {
        DirectoryInfo folder = new DirectoryInfo(dataPath);
        files = folder.GetFiles(appID + "_" + extRevID + "_*", SearchOption.AllDirectories);
    }
    public void GetPassportAttachments(int extRevID, string dataPath, out FileInfo[] files)
    {
        DirectoryInfo folder = new DirectoryInfo(dataPath);
        files = folder.GetFiles(extRevID + "_*", SearchOption.AllDirectories);
    }
    public void DeleteEvaluation(int ApplicationID, int ExternalReviewerID, string Language)
    {
        var context = new FPSDBEntities();
        var query = from e in context.Evaluations
                    where
                      e.ApplicationID == ApplicationID
                       && e.ExternalReviewerID == ExternalReviewerID
                    && e.Lang == Language
                    select e;
        foreach (var row in query)
        {
            context.Evaluations.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteEvaluation(int ApplicationID, string EmployeeID, int ExternalEmployeeID, int ExternalReviewerID, string Language, string SourceJSON)
    {
        var context = new FPSDBEntities();
        var query = from e in context.Evaluations
                    where
                      e.ApplicationID == ApplicationID
                    && e.ExternalReviewerID == ExternalReviewerID
                    && e.EmployeeID == EmployeeID
                    && e.ExternalEmployeeID == ExternalEmployeeID
                    && e.Lang == Language
                    && e.SourceJSON == SourceJSON
                    select e;
        foreach (var row in query)
        {
            context.Evaluations.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteEvaluation(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from e in context.Evaluations
                    where
                      e.ApplicationID == ApplicationID
                    select e;
        foreach (var row in query)
        {
            context.Evaluations.Remove(row);
        }
        context.SaveChanges();

    }

    public void InsertEvaluation(int? QNo,string Title,string QText,bool Visible, string Lang, bool ARBVisible,string ARBTitle,string ARBOption,string ARBValue
        ,bool ACBVisible,string ACBTitle,string ACBOption,string ACBValue,bool ADDLVisible,string ADDLTitle,string ADDLOption,bool ADDLRequired
        ,string ADDLValue,bool ATAVisible,string ATATitle,string ATAAttribute,string ATAStyle,bool ATARequired,string ATAValue
        ,int? ApplicationID,string EmployeeID,int? ExternalReviewerID, string AnswerInstruction, bool AnswerOnSameLine,string QuestionWidth
        ,int? ExternalEmployeeID,string SourceJSON, string FormTitle)
    {
        var context = new FPSDBEntities();
        context.Evaluations.Add(new Evaluation
        {
QNo	=	QNo	,
Title	=	Title	,
QText	=	QText	,
Visible	=	Visible	,
ARBVisible	=	ARBVisible	,
ARBTitle	=	ARBTitle	,
ARBOption	=	ARBOption	,
ARBValue	=	ARBValue	,
ACBVisible	=	ACBVisible	,
ACBTitle	=	ACBTitle	,
ACBOption	=	ACBOption	,
ACBValue	=	ACBValue	,
ADDLVisible	=	ADDLVisible	,
ADDLTitle	=	ADDLTitle	,
ADDLOption	=	ADDLOption	,
ADDLRequired	=	ADDLRequired	,
ADDLValue	=	ADDLValue	,
ATAVisible	=	ATAVisible	,
ATATitle	=	ATATitle	,
ATAAttribute	=	ATAAttribute	,
ATAStyle	=	ATAStyle	,
ATARequired	=	ATARequired	,
ATAValue	=	ATAValue	,
ApplicationID	=	ApplicationID	,
EmployeeID	=	EmployeeID	,
ExternalReviewerID	=	ExternalReviewerID	,
AnswerOnSameLine	=	AnswerOnSameLine	,
AnswerInstruction	=	AnswerInstruction	,
QuestionWidth	=	QuestionWidth	,
Lang	=	Lang	,
ExternalEmployeeID	=	ExternalEmployeeID	,
SourceJSON	=	SourceJSON	    ,
FormTitle = FormTitle

        });
        context.SaveChanges();

    }
    #endregion
    #endregion
    #region External Reviewers Form Summary 
    //public List<Evaluation1> GetEvaluation(int ApplicationID)
    //{

    //    var context = new FPSDB_newEntities();
    //    var data = (from e in context.Evaluations
    //                where e.ApplicationID == ApplicationID
    //                select new Evaluation1
    //                {
    //                    QNo = e.QNo.Value,
    //                    ARBValue = e.ARBValue,
    //                    ATAValue = e.ATAValue,
    //                    ExternalReviewerID = e.ExternalReviewerID.Value
    //                }).ToList<Evaluation1>();
    //    return data;

    //}
    #endregion

}