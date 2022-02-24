using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using BL.Data;
using System.IO;

/// <summary>
/// Summary description for BAL
/// </summary>

public class BAL
{
    public BAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string GetTypeString(int p)
    {
        switch (p)
        {
            case 0: return "A";
            case 1: return "D";
            case 2: return "C";
            case 3: return "VPRI";
            default: return "SCSubCom";
        }
    }
    #region WorkflowStatic
    #region Action_logTable
    public void InsertActionLog(string Detail, DateTime? TimeStamp)
    {
        var context = new FPSDBEntities();
        context.Action_Log.Add(new Action_Log
        {
            Detail = Detail,
            TimeStamp = TimeStamp
        });
        context.SaveChanges();

    }
    #endregion
    #region TaskForm
    public List<TaskForm> GetTaskFormByTask(int TaskID, bool TaskExternal)
    {
        var context = new FPSDBEntities();
        var data = (from tf in context.TaskForms
                    join f in context.Forms on tf.FormID equals f.FormID
                    where tf.TaskID == TaskID
                    && tf.TaskExternal == TaskExternal
                    select tf).OrderBy(tff => tff.Rank).ToList();
        return data;
    }
    public List<TaskForm> GetTaskForm(int TaskID, byte FormID, bool TaskExternal)
    {
        var context = new FPSDBEntities();
        var data = (from tf in context.TaskForms
                    join f in context.Forms on tf.FormID equals f.FormID
                    where tf.TaskID == TaskID && tf.FormID == FormID
                    && tf.TaskExternal == TaskExternal
                    select tf).OrderBy(tff => tff.Rank).ToList();
        return data;
    }
    public List<TaskForm> GetTaskForm()
    {
        var context = new FPSDBEntities();
        var data = (from tf in context.TaskForms
                    select tf).OrderBy(tff => tff.Rank).ToList();
        return data;


    }
    public void UpdateTaskForm(int taskID, byte formID, bool taskExternal, byte? rank, byte? level, string instruction, bool checkable)
    {
        var context = new FPSDBEntities();
        var query = from a in context.TaskForms
                    where
                        a.TaskID == taskID && a.FormID == formID && a.TaskExternal == taskExternal
                    select a;
        foreach (var row in query)
        {
            row.TaskID = taskID;
            row.FormID = formID;
            row.TaskExternal = taskExternal;
            row.Rank = rank;
            row.Level = level;
            row.Instruction = instruction;
            row.Checkable = checkable;
        }
        context.SaveChanges();
    }
    public void InsertTaskForm(int taskID, byte formID, bool taskExternal, byte rank, byte level, string instruction, bool checkable)
    {
        var context = new FPSDBEntities();
        context.TaskForms.Add(new TaskForm
        {
            TaskID = taskID,
            FormID = formID,
            TaskExternal = taskExternal,
            Rank = rank,
            Level = level,
            Instruction = instruction,
            Checkable = checkable
        });
        context.SaveChanges();
    }
    public void DeleteTaskForm(int taskID, byte formID, bool taskExternal)
    {
        var context = new FPSDBEntities();
        var query = from a in context.TaskForms
                    where
                     a.TaskID == taskID && a.FormID == formID && a.TaskExternal == taskExternal
                    select a;
        foreach (var row in query)
        {
            context.TaskForms.Remove(row);
        }
        context.SaveChanges();
    }
    public List<TaskFormDTO> GetTaskFormByAppIDTask(int ApplicationID, int TaskID, bool TaskExternal, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from tf in context.TaskForms
                    join atf in context.Application_TaskForm on new { v1 = tf.TaskID, v2 = tf.FormID, v3 = ApplicationID }
                    equals new { v1 = atf.TaskID, v2 = atf.FormID, v3 = atf.ApplicationID }
                    into atftf
                    from atf in atftf.DefaultIfEmpty()
                    where tf.TaskID == TaskID && tf.Checkable && tf.TaskExternal == TaskExternal
                    select new TaskFormDTO
                    {
                        TaskID = tf.TaskID,
                        FormID = tf.FormID,
                        Rank = tf.Rank,
                        Level = tf.Level,
                        Instruction = tf.Instruction,
                        Checkable = tf.Checkable,
                        TaskExternal = tf.TaskExternal,
                        ExternalReviewerID = ExternalReviewerID,
                        ApplicationID = ApplicationID,
                        Completed = atf.Completed,
                        Message = atf.Message

                    }).OrderBy(tff => tff.Rank).ToList();
        return data;

    }
    #endregion
    #region Task_Ext
    public List<Task_Ext> GetTaskExtByTitle(string Title)
    {
        var context = new FPSDBEntities();
        var data = (from te in context.Task_Ext
                    where
                    (
                    te.Title == Title
                    )
                    select te).ToList();
        return data;
    }
    public List<Task_Ext> GetTaskExt()
    {
        var context = new FPSDBEntities();
        var data = (from te in context.Task_Ext
                    select te).ToList();
        return data;
    }
    public void InsertTask_Ext(string Title, byte RemindAfter, byte RemindAgainAfter, byte RoleID)
    {
        var context = new FPSDBEntities();
        context.Task_Ext.Add(new Task_Ext
        {
            RemindAfter = RemindAfter,
            Title = Title,
            RoleID = RoleID,
            RemindAgainAfter = RemindAgainAfter,

        });
        context.SaveChanges();

    }
    public void UpdateTask_Ext(int TaskID, string Title, byte RemindAfter, byte RemindAgainAfter, byte RoleID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Task_Ext
                    where
                        a.TaskID == TaskID
                    select a;
        foreach (var row in query)
        {
            row.TaskID = TaskID;
            row.Title = Title;
            row.RemindAfter = RemindAfter;
            row.RemindAgainAfter = RemindAgainAfter;
            row.RoleID = RoleID;

        }
        context.SaveChanges();
    }

    public Phase GetPhaseFromNextTask(vw_NextTaskDTO nextTask)
    {
        foreach (var phase in GetPhase(true))
        {
            if (nextTask.NextRole.Contains(phase.Title))
            {
                return phase;
            }

        }
        return null;

    }

    public void DeleteTask_Ext(int TaskID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Task_Ext
                    where
                      a.TaskID == TaskID
                    select a;
        foreach (var row in query)
        {
            context.Task_Ext.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region TaskExtMessage
    public List<Task_ExtMessages> GetExtTaskMessage(int TaskID)
    {
        var context = new FPSDBEntities();
        var data = (from tem in context.Task_ExtMessages
                    where
                    (
                    tem.TaskID == TaskID
                    )
                    select tem).ToList();
        return data;
    }
    public List<Task_ExtMessages> GetExtTaskMessage()
    {
        var context = new FPSDBEntities();
        var data = (from tem in context.Task_ExtMessages
                    select tem).ToList();
        return data;
    }
    public List<Task_ExtMessages> GetExtTaskMessage(string Subject)
    {
        var context = new FPSDBEntities();
        var data = (from tem in context.Task_ExtMessages
                    where
                    (
                    tem.Subject == Subject
                    )
                    select tem).ToList();
        return data;
    }
    public void InsertTask_ExtMessages(int TaskID, string Subject, string Message)
    {
        var context = new FPSDBEntities();
        context.Task_ExtMessages.Add(new Task_ExtMessages
        {
            Message = Message,
            Subject = Subject,
            TaskID = TaskID

        });
        context.SaveChanges();

    }
    public void UpdateTask_ExtMessages(int TaskID, string Subject, string Message)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Task_ExtMessages
                    where a.TaskID == TaskID
                    && a.Subject == Subject
                    select a;
        foreach (var row in query)
        {
            row.TaskID = TaskID;
            row.Subject = Subject;
            row.Message = Message.Trim();
        }
        context.SaveChanges();

    }
    public void DeleteExtTaskMessage(int TaskID, string Subject)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Task_ExtMessages
                    where
                      a.TaskID == TaskID &&
                      a.Subject == Subject
                    select a;
        foreach (var row in query)
        {
            context.Task_ExtMessages.Remove(row);
        }
        context.SaveChanges();
    }

    #endregion
    #region TaskExt
    public List<Task_ExtMessages> GetTaskExtMessage(int TaskID)
    {
        var context = new FPSDBEntities();
        var data = (from tem in context.Task_ExtMessages
                    where
                    (
                    tem.TaskID == TaskID
                    )
                    select tem).ToList();
        return data;
    }
    #endregion
    #region Phase
    public List<Phase> GetPhase()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Phases
                    select a).ToList();
        return data;
    }
    public List<Phase> GetPhase(byte PhaseID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Phases
                    where
                    (
                    a.PhaseID == PhaseID
                    )
                    select a).ToList();
        return data;
    }
    public List<Phase> GetPhase(bool? IsCommittee)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Phases
                    where
                    (
                    a.IsCommittee == IsCommittee
                    )
                    select a).ToList();
        return data;
    }
    public void UpdatePhase(byte PhaseID, string title, bool? IsCommittee, bool? HasChair, int? Min, int? Max, bool? IsPermanentCommittee)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Phases
                    where
                        a.PhaseID == PhaseID
                    select a;
        foreach (var row in query)
        {
            row.PhaseID = PhaseID;
            row.Title = title;
            row.IsCommittee = IsCommittee;
            row.HasChair = HasChair;
            row.Min = Min;
            row.Max = Max;
            row.IsPermanentCommittee = IsPermanentCommittee;

            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public void InsertPhase(string title, bool? IsCommittee, bool? HasChair, int? Min, int? Max, bool? IsPermanentCommittee)
    {
        var context = new FPSDBEntities();
        context.Phases.Add(new Phase
        {

            Title = title,
            IsCommittee = IsCommittee,
            HasChair = HasChair,
            Min = Min,
            Max = Max,
            IsPermanentCommittee = IsPermanentCommittee,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeletePhase(byte PhaseID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Phases
                    where
                      a.PhaseID == PhaseID
                    select a;
        foreach (var row in query)
        {
            context.Phases.Remove(row);
        }
        context.SaveChanges();
    }

    #endregion
    #region Forms
    #region Form_ChairmanInput
    public List<Form_ChairmanInput> GetChairmanInput(int applicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fci in context.Form_ChairmanInput
                    where
                    (
                       fci.ApplicationID == applicationID
                    )
                    select fci).ToList();
        return data;
    }
    public List<Form_ChairmanInput> GetForm_ChairmanInput(int applicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fci in context.Form_ChairmanInput
                    where
                    (
                       fci.ApplicationID == applicationID
                    )
                    select fci).ToList();
        return data;
    }
    public void DeleteForm_ChairmanInput(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from ci in context.Form_ChairmanInput
                    where
                      ci.ApplicationID == ApplicationID
                    select ci;
        foreach (var row in query)
        {
            context.Form_ChairmanInput.Remove(row);
        }
        context.SaveChanges();

    }
    public void InsertChairmanInput(int ApplicationID, string Teaching, string Research, string CommunityService, string CommentsAndRecommendation, string MeetingNo, DateTime? MeetingDate)

    {
        var context = new FPSDBEntities();
        context.Form_ChairmanInput.Add(new Form_ChairmanInput
        {
            ApplicationID = ApplicationID,
            Teaching = Teaching,
            Research = Research,
            CommunityService = CommunityService,
            CommentsAndRecommendation = CommentsAndRecommendation,
            MeetingNo = MeetingNo,
            MeetingDate = MeetingDate
        });
        context.SaveChanges();

    }
    public void InsertForm_ChairmanInput(int ApplicationID, string F1, string F2, string F3, string F4, string F5, string F6
           , string F7, string F8, string F9, string F10, string F11, string F12, string F13, string F14, string F15, string F16
        , string F17, string F18, string F19, string meetingNo, DateTime? meetingDate)

    {
        var context = new FPSDBEntities();
        context.Form_ChairmanInput.Add(new Form_ChairmanInput
        {
            ApplicationID = ApplicationID,
            F1 = F1,
            F2 = F2,
            F3 = F3,
            F4 = F4,
            F5 = F5,
            F6 = F6,
            F7 = F7,
            F8 = F8,
            F9 = F9,
            F10 = F10,
            F11 = F11,
            F12 = F12,
            F13 = F13,
            F14 = F14,
            F15 = F15,
            F16 = F16,
            F17 = F17,
            F18 = F18,
            F19 = F19,
            MeetingNo = meetingNo,
            MeetingDate = meetingDate
        });
        context.SaveChanges();
    }
    public void UpdateForm_ChairmanInput(int ApplicationID, string F1, string F2, string F3, string F4, string F5, string F6
        , string F7, string F8, string F9, string F10, string F11, string F12, string F13, string F14, string F15, string F16
        , string F17, string F18, string F19, string meetingNo, DateTime? meetingDate)
    {
        var context = new FPSDBEntities();
        var query = from fmf in context.Form_ChairmanInput
                    where
                    fmf.ApplicationID == ApplicationID
                    select fmf;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.F1 = F1;
            row.F2 = F2;
            row.F3 = F3;
            row.F4 = F4;
            row.F5 = F5;
            row.F6 = F6;
            row.F7 = F7;
            row.F8 = F8;
            row.F9 = F9;
            row.F10 = F10;
            row.F11 = F11;
            row.F12 = F12;
            row.F13 = F13;
            row.F14 = F14;
            row.F15 = F15;
            row.F16 = F16;
            row.F17 = F17;
            row.F18 = F18;
            row.F19 = F19;
            row.MeetingNo = meetingNo;
            row.MeetingDate = meetingDate;
        }
        context.SaveChanges();
    }
    public void UpdateChairmanInput(int ApplicationID, string Teaching, string Research, string CommunityService, string CommentsAndRecommendation, string MeetingNo, DateTime? MeetingDate)
    {
        var context = new FPSDBEntities();
        var query = from fmf in context.Form_ChairmanInput
                    where
                    fmf.ApplicationID == ApplicationID
                    select fmf;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.Teaching = Teaching;
            row.Research = Research;
            row.CommunityService = CommunityService;
            row.CommentsAndRecommendation = CommentsAndRecommendation;
            row.MeetingNo = MeetingNo;
            row.MeetingDate = MeetingDate;
        }
        context.SaveChanges();
    }
    #endregion
    #region Form_ChairmanInput
    public List<Form_CollegeInput> GetCollegeInput(int applicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fci in context.Form_CollegeInput
                    where
                    (
                       fci.ApplicationID == applicationID
                    )
                    select fci).ToList();
        return data;
    }
    public List<Form_CollegeInput> GetForm_CollegeInput(int applicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fci in context.Form_CollegeInput
                    where
                    (
                       fci.ApplicationID == applicationID
                    )
                    select fci).ToList();
        return data;
    }
    public void DeleteForm_CollegeInput(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from ci in context.Form_CollegeInput
                    where
                      ci.ApplicationID == ApplicationID
                    select ci;
        foreach (var row in query)
        {
            context.Form_CollegeInput.Remove(row);
        }
        context.SaveChanges();

    }
    public void InsertForm_CollegeInput(int ApplicationID, string Teaching, string Research, string CommunityService, string CommentsAndRecommendation, string MeetingNo, DateTime? MeetingDate)

    {
        var context = new FPSDBEntities();
        context.Form_CollegeInput.Add(new Form_CollegeInput
        {
            ApplicationID = ApplicationID,
            MeetingNo = MeetingNo,
            MeetingDate = MeetingDate
        });
        context.SaveChanges();

    }
    public void InsertForm_CollegeInput(int ApplicationID, string F1, string F2, string F3, string F4, string F5, string F6
           , string F7, string F8, string F9, string F10, string F11, string F12, string F13, string F14, string F15, string F16
        , string F17, string F18, string F19, string meetingNo, DateTime? meetingDate)

    {
        var context = new FPSDBEntities();
        context.Form_CollegeInput.Add(new Form_CollegeInput
        {
            ApplicationID = ApplicationID,
            F1 = F1,
            F2 = F2,
            F3 = F3,
            F4 = F4,
            F5 = F5,
            F6 = F6,
            F7 = F7,
            F8 = F8,
            F9 = F9,
            MeetingNo = meetingNo,
            MeetingDate = meetingDate
        });
        context.SaveChanges();
    }
    public void UpdateForm_CollegeInput(int ApplicationID, string F1, string F2, string F3, string F4, string F5, string F6
        , string F7, string F8, string F9, string F10, string F11, string F12, string F13, string F14, string F15, string F16
        , string F17, string F18, string F19, string meetingNo, DateTime? meetingDate)
    {
        var context = new FPSDBEntities();
        var query = from fmf in context.Form_CollegeInput
                    where
                    fmf.ApplicationID == ApplicationID
                    select fmf;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.F1 = F1;
            row.F2 = F2;
            row.F3 = F3;
            row.F4 = F4;
            row.F5 = F5;
            row.F6 = F6;
            row.F7 = F7;
            row.F8 = F8;
            row.F9 = F9;
            row.MeetingNo = meetingNo;
            row.MeetingDate = meetingDate;
        }
        context.SaveChanges();
    }
    public void UpdateForm_CollegeInput(int ApplicationID, string Teaching, string Research, string CommunityService, string CommentsAndRecommendation, string MeetingNo, DateTime? MeetingDate)
    {
        var context = new FPSDBEntities();
        var query = from fmf in context.Form_CollegeInput
                    where
                    fmf.ApplicationID == ApplicationID
                    select fmf;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.MeetingNo = MeetingNo;
            row.MeetingDate = MeetingDate;
        }
        context.SaveChanges();
    }
    #endregion
    #region Form_Points
    public List<Form_Points> GetPoints()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Form_Points
                    select a).ToList();
        return data;
    }
    public List<Form_Points> GetPoints(int ApplicationID, string Type)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Form_Points
                    where
                    (
                    a.ApplicationID == ApplicationID
                    && a.Type == Type
                    )
                    select a).ToList();
        return data;
    }
    public void UpdatePoints(int ApplicationID, string Type, byte P1a, byte P1b, byte P1c, byte P1d, byte P1e, byte P1f, byte P1g, byte P1h, byte P1i, byte P2a
        , byte P2b, byte P2c, byte P2d, byte P2e, byte P2f, byte P2g, byte P2h, byte P2i, byte P3a, byte P3b, byte P3c, byte P3d, byte P3e, byte P3f, byte P3g
        , byte P3h, byte P3i, byte P4a, byte P4b, byte P4c, byte P5d, byte P5e, byte P5f, byte P5g, byte P5h, byte P5i, decimal SumTotal, decimal SumSigle
        , decimal SumJournal)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_Points
                    where
                        a.ApplicationID == ApplicationID
                    && a.Type == Type
                    select a;
        foreach (var row in query)
        {
            row.P1a = P1a;
            row.P1b = P1b;
            row.P1c = P1c;
            row.P1d = P1d;
            row.P1e = P1e;
            row.P1f = P1f;
            row.P1g = P1g;
            row.P1h = P1h;
            row.P1i = P1i;
            row.P2a = P2a;
            row.P2b = P2b;
            row.P2c = P2c;
            row.P2d = P2d;
            row.P2e = P2e;
            row.P2f = P2f;
            row.P2g = P2g;
            row.P2h = P2h;
            row.P2i = P2i;
            row.P3a = P3a;
            row.P3b = P3b;
            row.P3c = P3c;
            row.P3d = P3d;
            row.P3e = P3e;
            row.P3f = P3f;
            row.P3g = P3g;
            row.P3h = P3h;
            row.P3i = P3i;
            row.P4a = P4a;
            row.P4b = P4b;
            row.P4c = P4c;
            row.P5d = P5d;
            row.P5e = P5e;
            row.P5f = P5f;
            row.P5g = P5g;
            row.P5h = P5h;
            row.P5i = P5i;
            row.SumJournal = SumJournal;
            row.SumSigle = SumSigle;
            row.SumTotal = SumTotal;
        }
        context.SaveChanges();
    }
    public void InsertPoints(int ApplicationID, string Type, byte P1a, byte P1b, byte P1c, byte P1d, byte P1e, byte P1f, byte P1g, byte P1h, byte P1i, byte P2a
        , byte P2b, byte P2c, byte P2d, byte P2e, byte P2f, byte P2g, byte P2h, byte P2i, byte P3a, byte P3b, byte P3c, byte P3d, byte P3e, byte P3f, byte P3g
        , byte P3h, byte P3i, byte P4a, byte P4b, byte P4c, byte P5d, byte P5e, byte P5f, byte P5g, byte P5h, byte P5i, decimal SumTotal, decimal SumSigle
        , decimal SumJournal)
    {
        var context = new FPSDBEntities();
        context.Form_Points.Add(new Form_Points
        {
            ApplicationID = ApplicationID,
            Type = Type,
            P1a = P1a,
            P1b = P1b,
            P1c = P1c,
            P1d = P1d,
            P1e = P1e,
            P1f = P1f,
            P1g = P1g,
            P1h = P1h,
            P1i = P1i,
            P2a = P2a,
            P2b = P2b,
            P2c = P2c,
            P2d = P2d,
            P2e = P2e,
            P2f = P2f,
            P2g = P2g,
            P2h = P2h,
            P2i = P2i,
            P3a = P3a,
            P3b = P3b,
            P3c = P3c,
            P3d = P3d,
            P3e = P3e,
            P3f = P3f,
            P3g = P3g,
            P3h = P3h,
            P3i = P3i,
            P4a = P4a,
            P4b = P4b,
            P4c = P4c,
            P5d = P5d,
            P5e = P5e,
            P5f = P5f,
            P5g = P5g,
            P5h = P5h,
            P5i = P5i,
            SumJournal = SumJournal,
            SumSigle = SumSigle,
            SumTotal = SumTotal
        });
        context.SaveChanges();
    }
    public void DeleteForm_Points(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_Points
                    where
                       a.ApplicationID == ApplicationID
                    select a;
        foreach (var row in query)
        {
            context.Form_Points.Remove(row);
        }
        context.SaveChanges();
    }
    public void DeletePoints(int ApplicationID, string Type)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_Points
                    where
                       a.ApplicationID == ApplicationID
                    && a.Type == Type

                    select a;
        foreach (var row in query)
        {
            context.Form_Points.Remove(row);
        }
        context.SaveChanges();
    }

    public void DeleteForm_PointChanges(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_PointChanges
                    where
                       a.ApplicationID == ApplicationID
                    select a;
        foreach (var row in query)
        {
            context.Form_PointChanges.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion

    #region Form_AreaOfSp
    public List<Form_AreaOfSp> GetForm_AreaOfSp()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Form_AreaOfSp
                    select a).ToList();
        return data;
    }
    public List<Form_AreaOfSp> GetForm_AreaOfSp(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Form_AreaOfSp
                    where
                    (
                    a.ApplicationID == ApplicationID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateForm_AreaOfSp(int ApplicationID, string CR, string CU_PS, string GenRmrk, string IR, string Orig
       , string Prod, string QualC, string QualJ, string Signi, string SS, string TP)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_AreaOfSp
                    where
                         a.ApplicationID == ApplicationID
                    select a;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;


        }
        context.SaveChanges();
    }
    public void InsertForm_AreaOfSp(int ApplicationID, string CR, string CU_PS, string GenRmrk, string IR, string Orig
       , string Prod, string QualC, string QualJ, string Signi, string SS, string TP)
    {
        var context = new FPSDBEntities();
        context.Form_AreaOfSp.Add(new Form_AreaOfSp
        {
            ApplicationID = ApplicationID,

        });
        context.SaveChanges();
    }
    public void DeleteForm_AreaOfSp(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_AreaOfSp
                    where
                      a.ApplicationID == ApplicationID

                    select a;
        foreach (var row in query)
        {
            context.Form_AreaOfSp.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region Form_PCFeedback
    public List<Form_PCFeedback> GetForm_PCFeedback()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Form_PCFeedback
                    select a).ToList();
        return data;
    }
    public List<Form_PCFeedback> GetForm_PCFeedback(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Form_PCFeedback
                    where
                    (
                    a.ApplicationID == ApplicationID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateForm_PCFeedback(int ApplicationID, string CR, string CU_PS, string GenRmrk, string IR, string Orig
       , string Prod, string QualC, string QualJ, string Signi, string SS, string TP)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_PCFeedback
                    where
                         a.ApplicationID == ApplicationID
                    select a;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.CR = CR;
            row.CU_PS = CU_PS;
            row.GenRmrk = GenRmrk;
            row.IR = IR;
            row.Orig = Orig;
            row.Prod = Prod;
            row.QualC = QualC;
            row.QualJ = QualJ;
            row.Signi = Signi;
            row.QualC = QualC;
            row.QualJ = QualJ;
            row.Signi = Signi;
            row.SS = SS;
            row.TP = TP;

        }
        context.SaveChanges();
    }
    public void InsertForm_PCFeedback(int ApplicationID, string CR, string CU_PS, string GenRmrk, string IR, string Orig
       , string Prod, string QualC, string QualJ, string Signi, string SS, string TP)
    {
        var context = new FPSDBEntities();
        context.Form_PCFeedback.Add(new Form_PCFeedback
        {
            ApplicationID = ApplicationID,
            CR = CR,
            CU_PS = CU_PS,
            GenRmrk = GenRmrk,
            IR = IR,
            Orig = Orig,
            Prod = Prod,
            QualC = QualC,
            QualJ = QualJ,
            Signi = Signi,
            SS = SS,
            TP = TP
        });
        context.SaveChanges();
    }
    public void DeleteForm_PCFeedback(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_PCFeedback
                    where
                      a.ApplicationID == ApplicationID

                    select a;
        foreach (var row in query)
        {
            context.Form_PCFeedback.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region Form_AttachmentAd
    public List<Form_AttachmentAd> GetForm_AttachmentAd(int Form_AttachmentAdID)
    {
        using (var context = new FPSDBEntities())
        {
            var data = (from f in context.Form_AttachmentAd
                        where
                        (
                            f.Form_AttachmentAdID == Form_AttachmentAdID
                        )
                        select f).ToList();
            return data;
        }
    }

    public List<Form_AttachmentAd> GetForm_AttachmentAdByAppID(int applicationID)
    {
        var context = new FPSDBEntities();
        var data = (from f in context.Form_AttachmentAd
                    where
                    (
                        f.ApplicationID == applicationID
                    )
                    select f).ToList();
        return data;
    }
    //public List<pr_ApplicationAttachments_Result> GetApplication_Attachments(int applicationID, byte? roleID)
    //{
    //    var context = new FPSDB_newEntities();
    //    List<pr_ApplicationAttachments_Result> prAA;
    //    if (roleID == (byte) RoleID.External_Reviewer)
    //    {
    //        prAA = context.pr_ApplicationAttachments(applicationID, (byte) RoleID.Applicant).Where(aa=>aa.SelectionForExtRev ==true).ToList();
    //    }
    //    else
    //    {
    //        prAA = context.pr_ApplicationAttachments(applicationID,roleID).ToList();
    //    }

    //    return prAA;
    //}
    public void UpdateForm_AttachmentAd(int Form_AttachmentAdID, int? AttachmentCategoryID, int ApplicationID, string EmployeeID, string DocumentName, string DocumentSize, byte? RoleID,
        string Description, bool? SelectionForExtRev, DateTime? DateOfPublication, string Journal, int NoOfAuthors, bool FirstAuthor, decimal Points, string Remarks, bool Accept)
    {
        var context = new FPSDBEntities();
        var query = from f in context.Form_AttachmentAd
                    where
                    f.Form_AttachmentAdID == Form_AttachmentAdID
                    select f;
        foreach (var row in query)
        {
            row.Form_AttachmentAdID = Form_AttachmentAdID;
            row.AttachmentCategoryID = AttachmentCategoryID;
            row.ApplicationID = ApplicationID;
            row.EmployeeID = EmployeeID;
            row.DocumentName = DocumentName;
            row.DocumentSize = DocumentSize;
            row.RoleID = RoleID;
            row.Description = Description;
            row.SelectionForExtRev = SelectionForExtRev;
            row.DateOfPublication = DateOfPublication;
            row.Journal = Journal;
            row.NoOfAuthors = NoOfAuthors;
            row.FirstAuthor = FirstAuthor;
            row.Points = Points;
            row.Remarks = Remarks;
            row.Accept = Accept;
            row.UpdateDate = DateTime.Now;

        }
        context.SaveChanges();
    }
    public int InsertForm_AttachmentAd(int AttachmentCategoryID, int ApplicationID, string EmployeeID, string DocumentName, string DocumentSize, byte? RoleID,
        string Description, bool SelectionForExtRev, DateTime? DateOfPublication, string Journal, int NoOfAuthors, bool FirstAuthor, decimal Points, string Remarks, bool Accept)
    {
        var context = new FPSDBEntities();
        Form_AttachmentAd fa = new Form_AttachmentAd
        {
            AttachmentCategoryID = AttachmentCategoryID,
            ApplicationID = ApplicationID,
            EmployeeID = EmployeeID,
            DocumentName = DocumentName,
            DocumentSize = DocumentSize,
            RoleID = RoleID,
            Description = Description,
            SelectionForExtRev = SelectionForExtRev,
            DateOfPublication = DateOfPublication,
            Journal = Journal,
            NoOfAuthors = NoOfAuthors,
            FirstAuthor = FirstAuthor,
            Points = Points,
            Remarks = Remarks,
            Accept = Accept,
            InsertDate = DateTime.Now,
            UpdateDate = DateTime.Now

        };
        context.Form_AttachmentAd.Add(fa);
        context.SaveChanges();
        return fa.Form_AttachmentAdID;
    }

    public void DeleteForm_AttachmentAd(int Form_AttachmentAdID)
    {
        var context = new FPSDBEntities();
        var query = from f in context.Form_AttachmentAd
                    where
                      f.Form_AttachmentAdID == Form_AttachmentAdID
                    select f;
        foreach (var row in query)
        {
            context.Form_AttachmentAd.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteForm_AttachmentAdByAppID(int appID)
    {
        var context = new FPSDBEntities();
        var query = from f in context.Form_AttachmentAd
                    where
                      f.ApplicationID == appID
                    select f;
        foreach (var row in query)
        {
            context.Form_AttachmentAd.Remove(row);
        }
        context.SaveChanges();

    }
    public void GetApplicationAttachments(int appID, string employeeID, string dataPath, string fileName, out FileInfo[] files)
    {
        DirectoryInfo folder = new DirectoryInfo(dataPath);
        files = folder.GetFiles(appID + "_" + employeeID + "_" + fileName, SearchOption.AllDirectories);
    }

    #endregion    
    #region Form_FinalPC
    public List<Form_FinalPC> GetForm_FinalPCByPK(int ApplicationID, string EmployeeID, int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from fpc in context.Form_FinalPC
                    where
                    (
                      fpc.ApplicationID == ApplicationID &&
                      fpc.EmployeeID == EmployeeID &&
                      fpc.ExternalEmployeeID == ExternalEmployeeID
                    )
                    select fpc).ToList();
        return data;
    }

    public List<Form_FinalPC> GetForm_FinalPC(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fpc in context.Form_FinalPC
                    where
                    (
                      fpc.ApplicationID == ApplicationID
                    )
                    select fpc).OrderBy(fpc => fpc.Position).ToList();

        return data;
    }
    public void DeleteForm_FinalPC(int ApplicationID, string EmployeeID, int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        var query = from fpc in context.Form_FinalPC
                    where
                      fpc.ApplicationID == ApplicationID &&
                      fpc.EmployeeID == EmployeeID &&
                      fpc.ExternalEmployeeID == ExternalEmployeeID
                    select fpc;
        foreach (var row in query)
        {
            context.Form_FinalPC.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteForm_FinalPC(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from fpc in context.Form_FinalPC
                    where
                      fpc.ApplicationID == ApplicationID
                    select fpc;
        foreach (var row in query)
        {
            context.Form_FinalPC.Remove(row);
        }
        context.SaveChanges();

    }
    public void UpdateForm_FinalPC(int ApplicationID, string EmployeeID, int ExternalEmployeeID,
   int Position, string WLStatus, bool? DigitalSignature, DateTime? DSDate, string Comments, string Source, DateTime? WLDate,
        string CommentsWithWL, string Status)
    {
        var context = new FPSDBEntities();
        var query = from fpc in context.Form_FinalPC
                    where fpc.ApplicationID == ApplicationID
                     && fpc.EmployeeID == EmployeeID
                     && fpc.ExternalEmployeeID == ExternalEmployeeID
                    select fpc;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.Position = Position;
            row.EmployeeID = EmployeeID;
            row.ExternalEmployeeID = ExternalEmployeeID;
            row.WLStatus = WLStatus;
            row.DigitalSignature = DigitalSignature;
            row.DSDate = DSDate;
            row.Comments = Comments;
            row.Source = Source;
            row.WLDate = WLDate;
            row.CommentsWithWL = CommentsWithWL;
            row.Status = Status;
        }
        context.SaveChanges();
    }
    public void InsertForm_FinalPC(int ApplicationID, string EmployeeID, int ExternalEmployeeID,
   int Position, string WLStatus, bool? DigitalSignature, DateTime? DSDate, string Comments, string Status, string Source)
    {
        var context = new FPSDBEntities();
        context.Form_FinalPC.Add(new Form_FinalPC
        {
            ApplicationID = ApplicationID,
            Position = Position,
            EmployeeID = EmployeeID,
            WLStatus = WLStatus,
            DigitalSignature = DigitalSignature,
            DSDate = DSDate,
            Comments = Comments,
            Status = Status,
            Source = Source,
            ExternalEmployeeID = ExternalEmployeeID

        });
        context.SaveChanges();

    }
    #endregion
    #region FormMaterialFlag
    public List<Form_MaterialFlag> GetFormMaterialFlag(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fmf in context.Form_MaterialFlag
                    where
                    (
                    fmf.ApplicationID == ApplicationID
                    )
                    select fmf).ToList();
        return data;
    }
    public void DeleteFormMaterialFlag(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from ci in context.Form_MaterialFlag
                    where
                      ci.ApplicationID == ApplicationID
                    select ci;
        foreach (var row in query)
        {
            context.Form_MaterialFlag.Remove(row);
        }
        context.SaveChanges();

    }
    public void UpdateFormMaterialFlag(int ApplicationID, bool? PCMaterialReady4ExtRevFlag, string MaterialOKEmail, string MaterialNotOKEmail, string EmailFromTopAuthority)
    {
        var context = new FPSDBEntities();
        var query = from fmf in context.Form_MaterialFlag
                    where
                    fmf.ApplicationID == ApplicationID
                    select fmf;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.PCMaterialReady4ExtRevFlag = PCMaterialReady4ExtRevFlag;
            row.MaterialOKEmail = MaterialOKEmail;
            row.MaterialNotOKEmail = MaterialNotOKEmail;
            row.EmailFromTopAuthority = EmailFromTopAuthority;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public void InsertFormMaterialFlag(int ApplicationID, bool? PCMaterialReady4ExtRevFlag, string MaterialOKEmail, string MaterialNotOKEmail, string EmailFromTopAuthority)
    {
        var context = new FPSDBEntities();
        context.Form_MaterialFlag.Add(new Form_MaterialFlag
        {
            ApplicationID = ApplicationID,
            PCMaterialReady4ExtRevFlag = PCMaterialReady4ExtRevFlag,
            MaterialOKEmail = MaterialOKEmail,
            MaterialNotOKEmail = MaterialNotOKEmail,
            EmailFromTopAuthority = EmailFromTopAuthority,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    #endregion
    #region FormVoting
    public List<Form_Voting> GetFormVoting(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fmf in context.Form_Voting
                    where
                    (
                    fmf.ApplicationID == ApplicationID
                    )
                    select fmf).ToList();
        return data;
    }
    public void DeleteFormVoting(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from ci in context.Form_Voting
                    where
                      ci.ApplicationID == ApplicationID
                    select ci;
        foreach (var row in query)
        {
            context.Form_Voting.Remove(row);
        }
        context.SaveChanges();

    }
    public void UpdateFormVoting(int ApplicationID, bool? PCMaterialReady4ExtRevFlag, string MaterialOKEmail, string MaterialNotOKEmail, string EmailFromTopAuthority)
    {
        var context = new FPSDBEntities();
        var query = from fmf in context.Form_Voting
                    where
                    fmf.ApplicationID == ApplicationID
                    select fmf;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            //row.PCMaterialReady4ExtRevFlag = PCMaterialReady4ExtRevFlag;
            //row.MaterialOKEmail = MaterialOKEmail;
            //row.MaterialNotOKEmail = MaterialNotOKEmail;
            //row.EmailFromTopAuthority = EmailFromTopAuthority;
            //row.UpdateDate = DateTime.Now;
            //if (row.InsertDate == null)
            //{
            //    row.InsertDate = DateTime.Now;
            //}
        }
        context.SaveChanges();
    }
    public void InsertFormVoting(int ApplicationID, bool? PCMaterialReady4ExtRevFlag, string MaterialOKEmail, string MaterialNotOKEmail, string EmailFromTopAuthority)
    {
        var context = new FPSDBEntities();
        context.Form_Voting.Add(new Form_Voting
        {
            ApplicationID = ApplicationID,
            //PCMaterialReady4ExtRevFlag = PCMaterialReady4ExtRevFlag,
            //MaterialOKEmail = MaterialOKEmail,
            //MaterialNotOKEmail = MaterialNotOKEmail,
            //EmailFromTopAuthority = EmailFromTopAuthority,
            //UpdateDate = DateTime.Now,
            //InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    #endregion
    #region Form_AppProperties
    public List<Form_AppProperties> GetForm_AppProperties(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from faos in context.Form_AppProperties
                    where
                    (
                    faos.ApplicationID == ApplicationID
                    )
                    select faos).ToList();
        return data;
    }
    public void UpdateForm_AppProperties(int ApplicationID, string AreaOfSp, string PlaceOfGraduation, string Degree, DateTime? DateOfGraduation,
        DateTime? DateOfAppointment)
    {
        var context = new FPSDBEntities();
        var query = from faos in context.Form_AppProperties
                    where
                    faos.ApplicationID == ApplicationID
                    select faos;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.AreaOfSpecialization = AreaOfSp;
            row.PlaceOfGraduation = PlaceOfGraduation;
            row.Degree = Degree;
            row.DateOfGraduation = DateOfGraduation;
            row.DateOfAppointment = DateOfAppointment;

        }
        context.SaveChanges();
    }
    public void InsertForm_AppProperties(int ApplicationID, string AreaOfSp, string PlaceOfGraduation, string Degree, DateTime? DateOfGraduation,
        DateTime? DateOfAppointment)
    {
        var context = new FPSDBEntities();
        context.Form_AppProperties.Add(new Form_AppProperties
        {
            ApplicationID = ApplicationID,
            AreaOfSpecialization = AreaOfSp,
            PlaceOfGraduation = PlaceOfGraduation,
            Degree = Degree,
            DateOfGraduation = DateOfGraduation,
            DateOfAppointment = DateOfAppointment,

        });
        context.SaveChanges();
    }
    public void DeleteForm_AppProperties(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from faos in context.Form_AppProperties
                    where
                    faos.ApplicationID == ApplicationID

                    select faos;
        foreach (var row in query)
        {
            context.Form_AppProperties.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    //#region Form_PC
    //public List<Form_PC> GetForm_PCByPK(int ApplicationID, byte Position)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from fpc in context.Form_PC
    //                where
    //                (
    //                  fpc.ApplicationID == ApplicationID &&
    //                  fpc.Position == Position
    //                )
    //                select fpc).ToList();
    //    return data;
    //}
    //public List<Form_PC> GetForm_PC(int ApplicationID)
    //{

    //    var context = new FPSDBEntities();
    //    var data = (from fpc in context.Form_PC
    //                where
    //                (
    //                  fpc.ApplicationID == ApplicationID
    //                  )
    //                select fpc).ToList();
    //    return data;
    //}
    //public void DeleteForm_PC(int ApplicationID)
    //{
    //    var context = new FPSDBEntities();
    //    var query = from fpc in context.Form_PC
    //                where
    //                  fpc.ApplicationID == ApplicationID
    //                select fpc;
    //    foreach (var row in query)
    //    {
    //        context.Form_PC.Remove(row);
    //    }
    //    context.SaveChanges();

    //}
    //public void DeleteForm_PC(int ApplicationID,
    //   byte Position)
    //{
    //    var context = new FPSDBEntities();
    //    var query = from fpc in context.Form_PC
    //                where
    //                  fpc.ApplicationID == ApplicationID &&
    //                  fpc.Position == Position
    //                select fpc;
    //    foreach (var row in query)
    //    {
    //        context.Form_PC.Remove(row);
    //    }
    //    context.SaveChanges();

    //}
    //public void UpdateForm_PC(
    //   int ApplicationID,

    //   byte Position,
    //   string EmployeeID,
    //   int ExternalEmployeeID)
    //{
    //    var context = new FPSDBEntities();
    //    var query = from fpc in context.Form_PC
    //                where fpc.ApplicationID == ApplicationID

    //                && fpc.Position == Position
    //                select fpc;
    //    foreach (var row in query)
    //    {
    //        row.ApplicationID = ApplicationID;

    //        row.Position = Position;
    //        row.EmployeeID = EmployeeID;
    //        row.ExternalEmployeeID = ExternalEmployeeID;
    //    }
    //    context.SaveChanges();
    //}
    //public void InsertForm_PC(
    //    int ApplicationID,

    //    byte Position,
    //    string EmployeeID,
    //    int ExternalEmployeeID)
    //{
    //    var context = new FPSDBEntities();
    //    context.Form_PC.Add(new Form_PC
    //    {

    //        ApplicationID = ApplicationID,

    //        Position = Position,
    //        EmployeeID = EmployeeID,
    //        ExternalEmployeeID = ExternalEmployeeID
    //    });
    //    context.SaveChanges();Form_ServiceYears

    //}
    //#endregion
    #region FormPromotionCommittee
    public List<Form_PromotionCommittee> GetForm_PromotionCommitteeByPK(int ApplicationID, string Type, byte Position)
    {
        var context = new FPSDBEntities();
        var data = (from fpc in context.Form_PromotionCommittee
                    where
                    (
                      fpc.ApplicationID == ApplicationID &&
                      fpc.Type == Type &&
                      fpc.Position == Position
                    )
                    select fpc).ToList();
        return data;
    }
    public List<Form_PromotionCommittee> GetForm_PromotionCommittee(int ApplicationID, string Type)
    {
        var context = new FPSDBEntities();
        var data = (from fpc in context.Form_PromotionCommittee
                    where
                    (
                      fpc.ApplicationID == ApplicationID &&
                      fpc.Type == Type
                    )
                    select fpc).ToList();
        return data;
    }
    public void DeleteFormPromotionCommittee(int ApplicationID,
       string Type,
       byte Position)
    {
        var context = new FPSDBEntities();
        var query = from fpc in context.Form_PromotionCommittee
                    where
                      fpc.ApplicationID == ApplicationID &&
                      fpc.Type == Type &&
                      fpc.Position == Position
                    select fpc;
        foreach (var row in query)
        {
            context.Form_PromotionCommittee.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteForm_PromotionCommittee(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from fpc in context.Form_PromotionCommittee
                    where
                      fpc.ApplicationID == ApplicationID
                    select fpc;
        foreach (var row in query)
        {
            context.Form_PromotionCommittee.Remove(row);
        }
        context.SaveChanges();

    }
    public void UpdateFormPromotionCommittee(
       int ApplicationID,
       string Type,
       byte Position,
       bool IsKFUPM,
       string EmployeeID,
       string Name,
       string Department,
       string Rank,
       string Email,
       string MailingAddress,
       string Major,
       string Phone,
       int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        var query = from fpc in context.Form_PromotionCommittee
                    where fpc.ApplicationID == ApplicationID
                    && fpc.Type == Type
                    && fpc.Position == Position
                    select fpc;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.Type = Type;
            row.Position = Position;
            row.IsKFUPM = IsKFUPM;
            row.EmployeeID = EmployeeID;
            row.Name = Name;
            row.Department = Department;
            row.Rank = Rank;
            row.Email = Email;
            row.MailingAddress = MailingAddress;
            row.Major = Major;
            row.Phone = Phone;
            row.ExternalEmployeeID = ExternalEmployeeID;
        }
        context.SaveChanges();
    }
    public void InsertFormPromotionCommittee(
        int ApplicationID,
        string Type,
        byte Position,
        bool IsKFUPM,
        string EmployeeID,
        string Name,
        string Department,
        string Rank,
        string Email,
        string MailingAddress,
        string Major,
        string Phone,
        int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        context.Form_PromotionCommittee.Add(new Form_PromotionCommittee
        {

            ApplicationID = ApplicationID,
            Type = Type,
            Position = Position,
            IsKFUPM = IsKFUPM,
            EmployeeID = EmployeeID,
            Name = Name,
            Department = Department,
            Rank = Rank,
            Email = Email,
            MailingAddress = MailingAddress,
            Major = Major,
            Phone = Phone,
            ExternalEmployeeID = ExternalEmployeeID

        });
        context.SaveChanges();

    }
    #endregion
    #region FormDepartmentCommittee
    public List<Form_DepartmentCommittee> GetForm_DepartmentCommitteeByPK(int ApplicationID, string EmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from fdc in context.Form_DepartmentCommittee
                    where
                    (
                      fdc.ApplicationID == ApplicationID &&
                      fdc.EmployeeID == EmployeeID
                    )
                    select fdc).ToList();
        return data;
    }
    public List<Form_DepartmentCommittee> GetForm_DepartmentCommittee(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from fdc in context.Form_DepartmentCommittee
                    where
                    (
                      fdc.ApplicationID == ApplicationID
                    )
                    select fdc).OrderBy(x => x.Position).ToList();
        return data;
    }
    public void DeleteFormDepartmentCommittee(int ApplicationID, byte Position)
    {
        var context = new FPSDBEntities();
        var query = from fdc in context.Form_DepartmentCommittee
                    where
                      fdc.ApplicationID == ApplicationID &&
                      fdc.Position == Position
                    select fdc;
        foreach (var row in query)
        {
            context.Form_DepartmentCommittee.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteForm_DepartmentCommittee(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from fdc in context.Form_DepartmentCommittee
                    where
                      fdc.ApplicationID == ApplicationID

                    select fdc;
        foreach (var row in query)
        {
            context.Form_DepartmentCommittee.Remove(row);
        }
        context.SaveChanges();

    }
    public void UpdateFormDepartmentCommittee(
       int ApplicationID,
       byte Position,
       string EmployeeID)
    {
        var context = new FPSDBEntities();
        var query = from fdc in context.Form_DepartmentCommittee
                    where fdc.ApplicationID == ApplicationID
                    && fdc.EmployeeID == EmployeeID
                    select fdc;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.Position = Position;
            row.EmployeeID = EmployeeID;
        }
        context.SaveChanges();
    }
    public void InsertFormDepartmentCommittee(
        int ApplicationID,
        byte Position,
        string EmployeeID)
    {
        var context = new FPSDBEntities();
        context.Form_DepartmentCommittee.Add(new Form_DepartmentCommittee
        {

            ApplicationID = ApplicationID,
            Position = Position,
            EmployeeID = EmployeeID
        });
        context.SaveChanges();

    }
    #endregion
    #endregion
    #region WFAction
    public void DeleteAction(int ActionID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.WFActions
                    where
                      a.ActionID == ActionID
                    select a;
        foreach (var row in query)
        {
            context.WFActions.Remove(row);
        }
        context.SaveChanges();
    }
    public void UpdateAction(int ActionID, string Type, string Title, string Status)
    {
        var context = new FPSDBEntities();
        var query = from a in context.WFActions
                    where
                        a.ActionID == ActionID
                    select a;
        foreach (var row in query)
        {
            row.Type = Type;
            row.Title = Title;
            row.Status = Status;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public List<WFAction> GetActions(string Status)
    {
        var context = new FPSDBEntities();
        var data = (from
                    a in context.WFActions
                    where a.Status == Status
                    select a).ToList();
        return data;
    }
    public List<WFAction> GetActions(string Status, bool HasAttachedActionID)
    {
        var context = new FPSDBEntities();
        var data = (from
                    a in context.WFActions
                    where a.Status == Status && a.AttActionID.HasValue == HasAttachedActionID
                    select a).ToList();
        return data;
    }
    public List<BL.Data.WFAction> GetActionByPK(int ActionID)
    {
        var context = new FPSDBEntities();
        var data = (from
                    a in context.WFActions
                    where a.ActionID == ActionID
                    select a).ToList();
        return data;
    }
    public List<BL.Data.WFAction> GetActionsByTask(int TaskID, string Status)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.WFActions
                    where a.TaskID == TaskID
                    // && a.Status == Status
                    select a).ToList();
        return data;
    }
    public void InsertActions(string Type, string Title, string Status)
    {
        var context = new FPSDBEntities();
        context.WFActions.Add(new BL.Data.WFAction
        {
            Title = Title,
            //  Status = Status,
            Type = Type
            //,
            //     UpdateDate = DateTime.Now,
            //     InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    #endregion
    #region ActionMessage
    public List<ActionMessage> GetActionMessageByPK(int ActionID, int TaskID)
    {
        var context = new FPSDBEntities();
        var data = (from
                    am in context.ActionMessages
                    where am.ActionID == ActionID && am.NextTaskID == TaskID
                    select am).ToList();
        return data;
    }
    public List<ActionMessage> GetActionMessage()
    {
        var context = new FPSDBEntities();
        var data = (from
                    am in context.ActionMessages
                    select am).ToList();
        return data;
    }
    public void InsertActionMessage(int ActionID, int NextTaskID, string Message)
    {
        var context = new FPSDBEntities();
        context.ActionMessages.Add(new ActionMessage
        {
            Message = Message.Trim(),
            NextTaskID = NextTaskID,
            ActionID = ActionID,

        });
        context.SaveChanges();
    }
    public void UpdateActionMessage(int ActionID, int NextTaskID, string Message)
    {
        var context = new FPSDBEntities();
        var query = from a in context.ActionMessages
                    where
                        a.ActionID == ActionID && a.NextTaskID == NextTaskID
                    select a;
        foreach (var row in query)
        {
            row.Message = Message.Trim();
            row.NextTaskID = NextTaskID;
            row.ActionID = ActionID;
        }
        context.SaveChanges();
    }
    public void DeleteActionMessage(int ActionID, int NextTaskID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.ActionMessages
                    where
                      a.ActionID == ActionID &&
                      a.NextTaskID == NextTaskID
                    select a;
        foreach (var row in query)
        {
            context.ActionMessages.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region RolesReplacement
    public List<RolesReplacement> GetRolesReplacement()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.RolesReplacements
                    select a).ToList();
        return data;
    }
    public List<RolesReplacement> GetRolesReplacement(int CaseID, byte RoleID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.RolesReplacements
                    where
                    (
                    a.CaseID == CaseID &&
                    a.RoleID == RoleID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateRolesReplacement(int CaseID, byte RoleID, byte ReplacementRoleID, string Description)
    {
        var context = new FPSDBEntities();
        var query = from a in context.RolesReplacements
                    where
                        a.CaseID == CaseID &&
                        a.RoleID == RoleID
                    select a;
        foreach (var row in query)
        {
            row.ReplacementRoleID = ReplacementRoleID;
            row.Description = Description;
        }
        context.SaveChanges();
    }
    public void InsertRolesReplacement(int CaseID, byte RoleID, byte ReplacementRoleID, string Description)
    {
        var context = new FPSDBEntities();
        context.RolesReplacements.Add(new RolesReplacement
        {
            CaseID = CaseID,
            RoleID = RoleID,
            ReplacementRoleID = ReplacementRoleID,
            Description = Description
        });
        context.SaveChanges();
    }
    public void DeleteRolesReplacement(int CaseID, byte RoleID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.RolesReplacements
                    where
                        a.CaseID == CaseID &&
                        a.RoleID == RoleID

                    select a;
        foreach (var row in query)
        {
            context.RolesReplacements.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region SpecialCases
    public List<SpecialCase> GetSpecialCases()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.SpecialCases
                    where a.Status == RecordStatus.Active.ToString()
                    select a).ToList();
        return data;
    }
    public List<SpecialCase> GetSpecialCases(int CaseID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.SpecialCases
                    where
                    (
                    a.CaseID == CaseID &&
                    a.Status == RecordStatus.Active.ToString()
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateSpecialCases(int CaseID, byte RoleID, byte EquivalentRoleID, string Description, string Status)
    {
        var context = new FPSDBEntities();
        var query = from a in context.SpecialCases
                    where
                        a.CaseID == CaseID
                    select a;
        foreach (var row in query)
        {
            row.Description = Description;
            row.Status = Status;
            row.UpdateDate = DateTime.Now;
        }
        context.SaveChanges();
    }
    public void InsertSpecialCases(int CaseID, byte RoleID, byte EquivalentRoleID, string Description, string Status)
    {
        var context = new FPSDBEntities();
        context.SpecialCases.Add(new SpecialCase
        {
            RoleID = RoleID,
            EquivalentRoleID = EquivalentRoleID,
            Description = Description,
            Status = Status,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeleteSpecialCases(int CaseID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.SpecialCases
                    where
                      a.CaseID == CaseID

                    select a;
        foreach (var row in query)
        {
            context.SpecialCases.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region Role 
    public List<Role> GetRole()
    {
        var context = new FPSDBEntities();
        var data = (from
                    r in context.Roles
                    select r).ToList();
        return data.ToList();
    }
    public List<Role> GetRole(string Title)
    {
        var context = new FPSDBEntities();
        var data = (from
                    r in context.Roles
                    where r.Title == Title
                    select r).ToList();
        return data.ToList();
    }
    public List<Role> GetRoleByPK(byte RoleID)
    {
        var context = new FPSDBEntities();
        var data = (from
                    r in context.Roles
                    where r.RoleID == RoleID
                    select r).ToList();
        return data.ToList();
    }
    public void UpdateRole(int RoleID, string Title, byte Type)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Roles
                    where
                        a.RoleID == RoleID
                    select a;
        foreach (var row in query)
        {
            row.Title = Title;
            row.Type = Type;
        }
        context.SaveChanges();
    }
    public void InsertRole(string Title, byte Type)
    {
        var context = new FPSDBEntities();
        context.Roles.Add(new Role
        {
            Title = Title,
            Type = Type
        });
        context.SaveChanges();
    }
    public void DeleteRole(int RoleID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Roles
                    where
                      a.RoleID == RoleID
                    select a;
        foreach (var row in query)
        {
            context.Roles.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region Task
    public List<Task> GetTask()
    {
        var context = new FPSDBEntities();
        var data = (from t in context.Tasks
                    select t).ToList();
        return data;
    }

    public List<Task> GetTask(int TaskID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Tasks
                    where
                    (
                    a.TaskID == TaskID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateTask(int TaskID, string title, byte phaseID, byte roleID, byte remindAfter, byte remindAgainAfter, bool actionRequired)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Tasks
                    where
                        a.TaskID == TaskID
                    select a;
        foreach (var row in query)
        {
            row.ActionRequired = actionRequired;
            row.RemindAfter = remindAfter;
            row.RemindAgainAfter = remindAgainAfter;
            row.PhaseID = phaseID;
            row.RoleID = roleID;
            row.TaskID = TaskID;
            row.Title = title;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public void InsertTask(string title, byte phaseID, byte roleID, byte remindAfter, byte remindAgainAfter, bool actionRequired)
    {
        var context = new FPSDBEntities();
        context.Tasks.Add(new Task
        {
            RoleID = roleID,
            PhaseID = phaseID,
            RemindAfter = remindAfter,
            RemindAgainAfter = remindAgainAfter,
            ActionRequired = actionRequired,
            Title = title
        ,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeleteTask(int TaskID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Tasks
                    where
                      a.TaskID == TaskID
                    select a;
        foreach (var row in query)
        {
            context.Tasks.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region Task_Dependency
    public List<Task_Dependencies> GetTaskDependency(int TaskDependencyID)
    {
        var context = new FPSDBEntities();
        var data = (from td in context.Task_Dependencies
                    where td.TaskDependencyID == TaskDependencyID
                    select td).ToList();
        return data;
    }
    public List<Task_Dependencies> GetTaskDependency()
    {
        var context = new FPSDBEntities();
        var data = (from td in context.Task_Dependencies
                    select td).ToList();
        return data;
    }
    #endregion
    #region NameExclusion
    public List<NameExclusion> GetNameExclusion()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.NameExclusions
                    select a).ToList();
        return data;
    }
    public List<NameExclusion> GetNameExclusion(byte RoleID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.NameExclusions
                    where
                    (
                    a.RoleID == RoleID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateNameExclusion(byte RoleID, byte ExcludedRoleID, byte NewRoleID, byte NewExcludedRoleID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.NameExclusions
                    where
                        a.RoleID == RoleID &&
                        a.ExcludedRoleID == ExcludedRoleID
                    select a;
        foreach (var row in query)
        {
            row.RoleID = NewRoleID;
            row.ExcludedRoleID = NewExcludedRoleID;
        }
        context.SaveChanges();
    }
    public void InsertNameExclusion(byte RoleID, byte ExcludedRoleID, byte NewRoleID, byte NewExcludedRoleID)
    {
        var context = new FPSDBEntities();
        context.NameExclusions.Add(new NameExclusion
        {
            RoleID = NewRoleID,
            ExcludedRoleID = NewExcludedRoleID
        });
        context.SaveChanges();
    }
    public void DeleteNameExclusion(byte RoleID, byte ExcludedRoleID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.NameExclusions
                    where
                      a.RoleID == RoleID &&
                        a.ExcludedRoleID == ExcludedRoleID

                    select a;
        foreach (var row in query)
        {
            context.NameExclusions.Remove(row);
        }
        context.SaveChanges();
    }
    public void DeleteNameExclusion(byte RoleID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.NameExclusions
                    where
                      a.RoleID == RoleID

                    select a;
        foreach (var row in query)
        {
            context.NameExclusions.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region WorkflowAttribute
    public List<WorkflowAttribute> GetWorkflowAttribute()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.WorkflowAttributes
                    select a).ToList();
        return data;
    }
    public List<WorkflowAttribute> GetWorkflowAttribute(int AttributeID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.WorkflowAttributes
                    where
                    (
                    a.AttributeID == AttributeID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateWorkflowAttribute(int AttributeID, string Attribute, string Value)
    {
        var context = new FPSDBEntities();
        var query = from a in context.WorkflowAttributes
                    where
                        a.AttributeID == AttributeID
                    select a;
        foreach (var row in query)
        {
            row.Attribute = Attribute;
            row.Value = Value;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }

        }
        context.SaveChanges();
    }
    public void InsertWorkflowAttribute(string Attribute, string Value)
    {
        var context = new FPSDBEntities();
        context.WorkflowAttributes.Add(new WorkflowAttribute
        {
            Attribute = Attribute,
            Value = Value,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeleteWorkflowAttribute(int AttributeID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.WorkflowAttributes
                    where
                      a.AttributeID == AttributeID
                    select a;
        foreach (var row in query)
        {
            context.WorkflowAttributes.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region Admin
    public List<Admin> GetAdmin()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Admins
                    select a).ToList();
        return data;
    }
    public List<Admin> GetAdmin(string EmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Admins
                    where
                    (
                    a.EmployeeID == EmployeeID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateAdmin(int AdminID, string EmployeeID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Admins
                    where
                        a.AdminID == AdminID
                    select a;
        foreach (var row in query)
        {
            row.EmployeeID = EmployeeID;
        }
        context.SaveChanges();
    }
    public void InsertAdmin(string EmployeeID)
    {
        var context = new FPSDBEntities();
        context.Admins.Add(new Admin
        {
            EmployeeID = EmployeeID
        });
        context.SaveChanges();
    }
    public void DeleteAdmin(int AdminID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Admins
                    where
                      a.AdminID == AdminID

                    select a;
        foreach (var row in query)
        {
            context.Admins.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region  Form
    public List<BL.Data.Form> GetFormByPage(string Page)
    {
        var context = new FPSDBEntities();
        var data = (from f in context.Forms
                    where
                    (
                       f.Page == Page
                    )
                    select f).ToList();
        return data;
    }
    public List<BL.Data.Form> GetForm()
    {
        var context = new FPSDBEntities();
        var data = (from f in context.Forms

                    select f).ToList();
        return data;
    }
    public void UpdateForm(byte FormID, string title, string page, bool? allowFAAction)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Forms
                    where
                        a.FormID == FormID
                    select a;
        foreach (var row in query)
        {
            row.FormID = FormID;
            row.Title = title;
            row.AllowFAAction = allowFAAction;
        }
        context.SaveChanges();
    }
    public void InsertForm(string title, string page, bool? allowFAAction)
    {
        var context = new FPSDBEntities();
        context.Forms.Add(new BL.Data.Form
        {
            Title = title,
            Page = page,
            AllowFAAction = allowFAAction
        });
        context.SaveChanges();
    }
    public void DeleteForm(byte FormID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Forms
                    where
                      a.FormID == FormID
                    select a;
        foreach (var row in query)
        {
            context.Forms.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region  FormCategory
    public List<FormCategory> GetFormCategoryByTitle(string Title)
    {
        var context = new FPSDBEntities();
        var data = (from f in context.FormCategories
                    where
                    (
                       f.Title == Title
                    )
                    select f).ToList();
        return data;
    }
    public List<FormCategory> GetFormCategory()
    {
        var context = new FPSDBEntities();
        var data = (from f in context.FormCategories
                    select f).ToList();
        return data;
    }
    public void UpdateFormCategory(byte FormCategoryID, string title, string status)
    {
        var context = new FPSDBEntities();
        var query = from a in context.FormCategories
                    where
                        a.FormCategoryID == FormCategoryID
                    select a;
        foreach (var row in query)
        {
            row.FormCategoryID = FormCategoryID;
            row.Title = title;
            row.Status = status;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public void InsertFormCategory(string title, string status)
    {
        var context = new FPSDBEntities();
        context.FormCategories.Add(new FormCategory
        {
            Title = title,
            Status = status,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeleteFormCategory(byte FormCategoryID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.FormCategories
                    where
                      a.FormCategoryID == FormCategoryID
                    select a;
        foreach (var row in query)
        {
            context.FormCategories.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region  FormVariable
    public List<FormVariable> GetFormVariableByTitle(string Variable)
    {
        var context = new FPSDBEntities();
        var data = (from f in context.FormVariables
                    where
                    (
                       f.Variable == Variable
                    )
                    select f).ToList();
        return data;
    }
    public List<FormVariable> GetFormVariableByTitle(byte FormID)
    {
        var context = new FPSDBEntities();
        var data = (from f in context.FormVariables
                    where
                    (
                       f.FormID == FormID
                    )
                    select f).ToList();
        return data;
    }
    public List<FormVariable> GetFormVariable()
    {
        var context = new FPSDBEntities();
        var data = (from f in context.FormVariables
                    where f.Status == RecordStatus.Active.ToString()
                    select f).ToList();
        return data;
    }
    public void UpdateFormVariable(int FormVariableID, byte FormID, string Variable, string Status, string ValueString, string FormTable)
    {
        var context = new FPSDBEntities();
        var query = from a in context.FormVariables
                    where
                        a.FormVariableID == FormVariableID
                    select a;
        foreach (var row in query)
        {
            row.FormVariableID = FormVariableID;
            row.FormID = FormID;
            row.Variable = Variable;
            row.Status = Status;
            row.UpdateDate = DateTime.Now;
            row.ValueString = ValueString;
            row.FormTable = FormTable;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public void InsertFormVariable(byte FormID, string Variable, string Status, string ValueString, string FormTable)
    {
        var context = new FPSDBEntities();
        context.FormVariables.Add(new FormVariable
        {
            FormID = FormID,
            Variable = Variable,
            Status = Status,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now,
            ValueString = ValueString,
            FormTable = FormTable
        });
        context.SaveChanges();
    }
    public void DeleteFormVariable(byte FormVariableID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.FormVariables
                    where
                      a.FormVariableID == FormVariableID
                    select a;
        foreach (var row in query)
        {
            context.FormVariables.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion

    #region ExtFormInstruction 
    public List<ExtFormInstruction> ExtFormInstructionByPK(byte FormID)
    {
        var context = new FPSDBEntities();
        var data = (from efi in context.ExtFormInstructions
                    where
                    (
                       efi.FormID == FormID
                    )
                    select efi).ToList();
        return data;
    }
    public List<ExtFormInstruction> ExtFormInstructionByType(string Type)
    {
        var context = new FPSDBEntities();
        var data = (from efi in context.ExtFormInstructions
                    where
                    (
                       efi.Type == Type
                    )
                    select efi).ToList();
        return data;
    }

    #endregion
    #endregion
    #region WorkflowDynamic
    #region Application Related






    #region Application_Role

    //    public void AssignManager(string Name, string ShortName, int ParentDeptID, string NewEmployeeID, string Phone, string Website, string Fax,
    //byte HeadRoleID, string Type, string Status, string DeputyEmailID, int DepartmentID, string HeadEmpID)
    //    {
    //        UpdateDepartment(Name, ShortName, ParentDeptID, NewEmployeeID, Phone, Website, Fax, HeadRoleID, Type, Status,
    //            DeputyEmailID, DepartmentID);
    //        UpdateApplicationRoles(HeadEmpID, HeadRoleID, NewEmployeeID, 0);
    //    }
    public List<Application_Role> GetApplicationRole(int applicationID, byte roleID, string employeeID, int externalEmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    where
                    (
                    ar.ApplicationID == applicationID &&
                    ar.RoleID == roleID &&
                    ar.EmployeeID == employeeID &&
                    ar.ExternalEmployeeID == externalEmployeeID
                    )
                    select ar).ToList();
        return data;
    }
    public List<Application_Role> GetApplicationRole(int applicationID, byte roleID)
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    where
                    (
                    ar.ApplicationID == applicationID &&
                    ar.RoleID == roleID
                    )
                    select ar).ToList();
        return data;
    }
    public List<Application_Role> GetApplicationRole(int applicationID)
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    where
                    (
                    ar.ApplicationID == applicationID
                    )
                    select ar).ToList();
        return data;
    }
    public List<Application_Role> GetApplicationRole(byte roleID)
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    where
                    (
                    ar.RoleID == roleID
                    )
                    select ar).ToList();
        return data;
    }
    public List<Application_Role> GetApplicationRole()
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    select ar).ToList();
        return data;
    }
    public List<Application_Role> GetApplicationRoleForActiveApp(byte StartRoleID, byte EndRoleID)
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    where !ar.Application.ApplicationClosed
                    && ar.RoleID >= StartRoleID
                    && ar.RoleID <= EndRoleID
                    select ar).ToList();
        return data;
    }
    public List<Application_Role> GetApplicationRole(int ApplicationID, byte StartRoleID, byte EndRoleID)
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    where
                    ar.ApplicationID == ApplicationID
                    && ar.RoleID >= StartRoleID
                    && ar.RoleID <= EndRoleID
                    select ar).ToList();
        return data;
    }

    public List<byte> GetApplicationRoleID(int ApplicationID, byte StartRoleID, byte EndRoleID)
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    where
                    ar.ApplicationID == ApplicationID
                    && ar.RoleID >= StartRoleID
                    && ar.RoleID <= EndRoleID
                    select ar.RoleID).ToList();
        return data;
    }
    public List<Application_Role> GetApplicationRole(string EmployeeID, int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    where
                    (
                    ar.EmployeeID == EmployeeID &&
                    ar.ExternalEmployeeID == ExternalEmployeeID
                    )
                    select ar).ToList();
        return data;
    }
    public void DeleteApplicationRoles(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Application_Role
                    where
                      ar.ApplicationID == ApplicationID

                    select ar;
        foreach (var row in query)
        {
            context.Application_Role.Remove(row);
        }
        context.SaveChanges();
    }
    //public List<Application_Role> GetApplicationRole(int applicationID)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from ar in context.Application_Role
    //                            where
    //                            (
    //                            ar.ApplicationID == applicationID
    //                            )
    //                            select ar).ToList();
    //    return data;
    //}
    //public List<Application_Role> GetApplicationRole(int applicationID, byte roleID)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from ar in context.Application_Role
    //                where
    //                (
    //                ar.ApplicationID == applicationID &&
    //                ar.RoleID == roleID
    //                )
    //                select ar).ToList();
    //    return data;
    //}
    public void UpdateApplicationRoles(int ApplicationID, byte RoleID, string NewEmployeeID, int ExternalEmployeeID, string IsActing)
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Application_Role
                    where ar.ApplicationID == ApplicationID
                    && ar.RoleID == RoleID
                    select ar;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.EmployeeID = NewEmployeeID;
            row.RoleID = RoleID;
            row.IsActing = IsActing;
            row.RoleID = RoleID;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public void UpdateAppRoles(int ApplicationID, byte RoleID, string EmployeeID, string NewEmployeeID)/*Incomplete*/
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Application_Role
                    where ar.ApplicationID == ApplicationID
                    && ar.EmployeeID == EmployeeID
                    && ar.RoleID == RoleID
                    select ar;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.EmployeeID = NewEmployeeID;
            row.RoleID = RoleID;
        }
        context.SaveChanges();

    }
    public void InsertApplicationRoles(int applicationID)
    {
        Application a = GetApplication(applicationID)[0];
        if (a.Employee.DepartmentID == null || a.Employee.DepartmentID == 0)
        {
            return;
        }
        if (a.Employee.Department1.ParentDeptID == null || a.Employee.Department1.ParentDeptID == 0)
        {
            return;
        }
        //string a.EmployeeID = GetApplication(applicationID)[0].EmployeeID;        
        //Department applicantDept = GetDepartmentByPK(GetEmployeeByPK(a.EmployeeID)[0].DepartmentID);
        //Department applicantParentDept = GetDepartmentByPK(applicantDept.ParentDeptID.Value);
        var context = new FPSDBEntities();
        if (GetApplicationRole(applicationID, (byte)RoleID.Applicant).Count == 0)
        {
            context.Application_Role.Add(new Application_Role
            {
                ApplicationID = applicationID,
                RoleID = (byte)RoleID.Applicant,
                EmployeeID = a.EmployeeID,
                ExternalEmployeeID = 0,
                UpdateDate = DateTime.Now,
                InsertDate = DateTime.Now

            });
            context.SaveChanges();
        }
        if (GetApplicationRole(applicationID, (byte)RoleID.DepartmentChairman).Count == 0)
        {
            context.Application_Role.Add(new Application_Role
            {
                ApplicationID = applicationID,
                RoleID = (byte)RoleID.DepartmentChairman,
                EmployeeID = a.Employee.Department1.HeadEmpID,
                ExternalEmployeeID = 0,
                UpdateDate = DateTime.Now,
                InsertDate = DateTime.Now
            });
            context.SaveChanges();
        }
        if (GetApplicationRole(applicationID, (byte)RoleID.College_Dean).Count == 0)
        {
            context.Application_Role.Add(new Application_Role
            {
                ApplicationID = applicationID,
                EmployeeID = a.Employee.Department1.Department1.HeadEmpID,
                RoleID = (byte)RoleID.College_Dean,
                ExternalEmployeeID = 0,
                UpdateDate = DateTime.Now,
                InsertDate = DateTime.Now
            });
            context.SaveChanges();
        }
        if (GetApplicationRole(applicationID, (byte)RoleID.DeanofFacultyAffairs).Count == 0)
        {
            context.Application_Role.Add(new Application_Role
            {
                ApplicationID = applicationID,
                EmployeeID = GetDepartmentByPK((int)DepartmentID.Deanship_Of_Faculty_Affairs).HeadEmpID,
                RoleID = (byte)RoleID.DeanofFacultyAffairs,
                ExternalEmployeeID = 0,
                UpdateDate = DateTime.Now,
                InsertDate = DateTime.Now
            });
            context.SaveChanges();
        }
        if (GetApplicationRole(applicationID, (byte)RoleID.TopLowAuthority).Count == 0)
        {
            context.Application_Role.Add(new Application_Role
            {
                ApplicationID = applicationID,
                EmployeeID = GetDepartmentByPK((int)DepartmentID.VPAA).HeadEmpID,
                RoleID = (byte)RoleID.TopLowAuthority,
                ExternalEmployeeID = 0,
                UpdateDate = DateTime.Now,
                InsertDate = DateTime.Now
            });
            context.SaveChanges();
        }
        if (GetApplicationRole(applicationID, (byte)RoleID.TopAuthority).Count == 0)
        {
            context.Application_Role.Add(new Application_Role
            {
                ApplicationID = applicationID,
                EmployeeID = GetDepartmentByPK((int)DepartmentID.VPRI).HeadEmpID,
                RoleID = (byte)RoleID.TopAuthority,
                ExternalEmployeeID = 0,
                UpdateDate = DateTime.Now,
                InsertDate = DateTime.Now
            });
            context.SaveChanges();
        }
        if (GetApplicationRole(applicationID, (byte)RoleID.President).Count == 0)
        {
            context.Application_Role.Add(new Application_Role
            {
                ApplicationID = applicationID,
                EmployeeID = GetDepartmentByPK((int)DepartmentID.President).HeadEmpID,
                RoleID = (byte)RoleID.President,
                ExternalEmployeeID = 0,
                UpdateDate = DateTime.Now,
                InsertDate = DateTime.Now
            });
            context.SaveChanges();
        }
        //if (GetApplicationRole(applicationID, (byte)RoleID.TopMostAuthority).Count == 0)
        //{
        //    context.Application_Role.Add(new Application_Role
        //    {
        //        ApplicationID = applicationID,
        //        EmployeeID = GetDepartmentByPK((int)DepartmentID.TopMostAuthority).HeadEmpID,
        //        RoleID = (byte)RoleID.TopMostAuthority,
        //        ExternalEmployeeID = 0
        //    });

        //    context.SaveChanges();
        //}

        CheckAndCompletePermanentCommitteeInAppRole(applicationID);

        foreach (Application_Role ar in GetApplicationRole(applicationID))
        {
            foreach (SpecialCase spCase in GetSpecialCases())
            {
                if (a.EmployeeID == ar.EmployeeID)
                {
                    if (spCase.RoleID == (byte)RoleID.Applicant
                        && spCase.EquivalentRoleID.Value == ar.RoleID)
                    {
                        ar.EmployeeID = GetApplicationRole(applicationID, GetRolesReplacement(spCase.CaseID, spCase.EquivalentRoleID.Value)[0].ReplacementRoleID)[0].EmployeeID;
                        UpdateApplicationRoles(applicationID, ar.RoleID, ar.EmployeeID, ar.ExternalEmployeeID, "Acting");
                    }
                }
            }
        }

        foreach (Application_Role ar in GetApplicationRole(applicationID))
        {
            if (ar.EmployeeID == a.EmployeeID && ar.RoleID != (byte)RoleID.Applicant)
            {
                DeleteApplicationRoles(ar.ApplicationID, ar.RoleID);
            }
        }
    }
    public void InsertApplicationRoles(int applicationID, byte RoleID, string EmployeeID, int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        context.Application_Role.Add(new Application_Role
        {
            ApplicationID = applicationID,
            RoleID = RoleID,
            EmployeeID = EmployeeID,
            ExternalEmployeeID = ExternalEmployeeID,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void InsertApplicationRoles(int applicationID, string EmployeeID, byte RoleID)
    {
        var context = new FPSDBEntities();
        context.Application_Role.Add(new Application_Role
        {
            ApplicationID = applicationID,
            EmployeeID = EmployeeID,
            RoleID = RoleID
        });
        context.SaveChanges();
    }
    public void InsertMemberInPermanentCommitteeOfApp(int applicationID, string employeeID, int extEmployeeID, PermanentCommitteeTypeTitle permanentCommittee)
    {
        if (permanentCommittee == PermanentCommitteeTypeTitle.Scientific_Council)
        {
            byte i = ReorderMembersInPermanentCommitteeofApp(applicationID, permanentCommittee);
            InsertApplicationRoles(applicationID, i, employeeID, extEmployeeID);
        }
        else if (permanentCommittee == PermanentCommitteeTypeTitle.SC_Subcommittee)
        {
            byte i = ReorderMembersInPermanentCommitteeofApp(applicationID, permanentCommittee);
            InsertApplicationRoles(applicationID, i, employeeID, extEmployeeID);
        }
        else if (permanentCommittee == PermanentCommitteeTypeTitle.Scientific_Council_Coordinator)
        {
            InsertApplicationRoles(applicationID, (byte)RoleID.Scientific_Council_Coordinator, employeeID, extEmployeeID);
        }
    }
    public void DeleteApplicationRoles(int ApplicationID, byte RoleID)
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Application_Role
                    where
                      ar.ApplicationID == ApplicationID &&
                      ar.RoleID == RoleID
                    select ar;
        foreach (var row in query)
        {
            context.Application_Role.Remove(row);
        }
        context.SaveChanges();
    }
    public void DeleteApplicationRoles(int ApplicationID, byte StartRoleID, byte EndRoleID)
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Application_Role
                    where
                      ar.ApplicationID == ApplicationID &&
                      ar.RoleID >= StartRoleID && ar.RoleID <= EndRoleID
                    select ar;
        foreach (var row in query)
        {
            context.Application_Role.Remove(row);
        }
        context.SaveChanges();
    }
    public byte ReorderMembersInPermanentCommitteeofApp(int applicationID, PermanentCommitteeTypeTitle permanentCommittee)
    {
        if (permanentCommittee == PermanentCommitteeTypeTitle.Scientific_Council)
        {
            byte i = 0;
            List<Application_Role> larSC = GetApplicationRole(applicationID)
                .Where(a => a.RoleID >= (byte)RoleID.Scientific_Council_Member_1 &&
                a.RoleID <= (byte)RoleID.Scientific_Council_Coordinator).ToList();
            List<Application_Role> larSCCopy = larSC;

            foreach (Application_Role ar in larSC)
            {
                DeleteApplicationRoles(applicationID, ar.RoleID);
            }
            foreach (Application_Role ar in larSCCopy)
            {
                InsertApplicationRoles(applicationID, (byte)((byte)RoleID.Scientific_Council_Member_1 + i), ar.EmployeeID, ar.ExternalEmployeeID);
                i++;
            }
            return (byte)(i + (byte)RoleID.Scientific_Council_Member_1);
        }
        else if (permanentCommittee == PermanentCommitteeTypeTitle.SC_Subcommittee)
        {
            byte i = 0;
            List<Application_Role> larSCSubComm = GetApplicationRole(applicationID)
                .Where(a => a.RoleID >= (byte)RoleID.SC_Subcommittee_Chair &&
                a.RoleID <= (byte)RoleID.SC_Subcommittee_Member_4).ToList();
            List<Application_Role> larSCSubCommCopy = larSCSubComm;

            foreach (Application_Role ar in larSCSubComm)
            {
                DeleteApplicationRoles(applicationID, ar.RoleID);
            }
            foreach (Application_Role ar in larSCSubCommCopy)
            {
                InsertApplicationRoles(applicationID, (byte)((byte)RoleID.SC_Subcommittee_Chair + i), ar.EmployeeID, ar.ExternalEmployeeID);
                i++;
            }
            return (byte)(i + (byte)RoleID.SC_Subcommittee_Chair);
        }
        else
        {
            return 0;
        }

    }
    public void CheckAndCompletePermanentCommitteeInAppRole(int applicationID)
    {
        var context = new FPSDBEntities();
        List<PermanentCommittee> subc = GetPermanentCommitteeByTypeID((int)PermanentCommitteeTypeTitle.SC_Subcommittee);
        if (subc.Count != 0 && GetApplicationRole(applicationID, (byte)RoleID.SC_Subcommittee_Chair).Count == 0)
        {
            context.Application_Role.Add(new Application_Role
            {
                ApplicationID = applicationID,
                EmployeeID = subc[0].EmployeeID,
                RoleID = (byte)RoleID.SC_Subcommittee_Chair,
                ExternalEmployeeID = 0,
                UpdateDate = DateTime.Now,
                InsertDate = DateTime.Now
            });
            context.SaveChanges();
        }
        for (int i = 0; i < subc.Count() - 1; i++)
        {
            if (GetApplicationRole(applicationID, GetRole("SC Subcommittee Member " + (i + 1)).First().RoleID).Count == 0)
            {
                context.Application_Role.Add(new Application_Role
                {
                    ApplicationID = applicationID,
                    EmployeeID = subc[i + 1].EmployeeID,
                    RoleID = GetRole("SC Subcommittee Member " + (i + 1)).First().RoleID,
                    ExternalEmployeeID = 0
                });
                context.SaveChanges();
            }
        }
        List<PermanentCommittee> sc = GetPermanentCommitteeByTypeID((int)PermanentCommitteeTypeTitle.Scientific_Council);
        for (int i = 0; i < sc.Count(); i++)
        {
            if (GetRole("Scientific Council Member " + (i + 1)).Count == 0)
            {
                continue;
            }
            if (GetApplicationRole(applicationID, GetRole("Scientific Council Member " + (i + 1))[0].RoleID).Count == 0)
            {
                context.Application_Role.Add(new Application_Role
                {
                    ApplicationID = applicationID,
                    EmployeeID = sc[i].EmployeeID,
                    RoleID = GetRole("Scientific Council Member " + (i + 1))[0].RoleID,
                    ExternalEmployeeID = 0,
                    UpdateDate = DateTime.Now,
                    InsertDate = DateTime.Now
                });
                context.SaveChanges();
            }
        }
        List<PermanentCommittee> scC = GetPermanentCommitteeByTypeID((int)PermanentCommitteeTypeTitle.Scientific_Council_Coordinator);
        if (scC.Count != 0 && GetApplicationRole(applicationID, (byte)RoleID.Scientific_Council_Coordinator).Count == 0)
        {
            context.Application_Role.Add(new Application_Role
            {
                ApplicationID = applicationID,
                EmployeeID = subc[0].EmployeeID,
                RoleID = (byte)RoleID.Scientific_Council_Coordinator,
                ExternalEmployeeID = 0,
                UpdateDate = DateTime.Now,
                InsertDate = DateTime.Now
            });
            context.SaveChanges();
        }
    }

    #endregion
    #region Application   
    public int InsertApplication(string EmployeeID, string ForRank)
    {
        var context = new FPSDBEntities();
        Application a = new Application
        {
            ApplicationClosed = false,
            EmployeeID = EmployeeID,
            ForRank = ForRank,
            StartDate = DateTime.Now,
            Comment = "",
            FinalDeicision = false,
            SCDecision = false,
            SCComments = "",
            RectorComments = "",
            RectorDecision = false,
            ExclusionList = false
        };
        context.Applications.Add(a);
        context.SaveChanges();
        return a.ApplicationID;
    }

    public List<Application> GetApplication()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Applications
                    select a).ToList();
        return data;
    }
    public List<Application> GetApplicationWithApplicationClosed(bool ApplicationClosed)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Applications
                    where a.ApplicationClosed == ApplicationClosed
                    select a).ToList();
        return data;
    }
    public List<int> GetApplicationIDWithApplicationClosed(bool ApplicationClosed)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Applications
                    where a.ApplicationClosed == ApplicationClosed
                    select a.ApplicationID).ToList();
        return data;
    }
    public List<Application> GetApplication(string EmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Applications
                    where a.EmployeeID == EmployeeID
                    select a).ToList();
        return data;
    }
    public List<Application> GetApplication(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Applications
                    where
                    (
                    a.ApplicationID == ApplicationID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateApplication(int ApplicationID, bool ApplicationClosed, string EmployeeID, string ForRank, DateTime? StartDate
        , bool? ExclusionList, string Comment, bool? HardCopy, string SCComments, bool RectorDecision, bool FinalDeicision, bool SCDecision, string RectorComments)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Applications
                    where
                    a.ApplicationID == ApplicationID
                    select a;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.ApplicationID = ApplicationID;
            row.ApplicationClosed = ApplicationClosed;
            row.EmployeeID = EmployeeID;
            row.ForRank = ForRank;
            row.StartDate = StartDate;
            row.ExclusionList = ExclusionList;
            row.Comment = Comment;
            row.HardCopy = HardCopy;
            row.SCComments = SCComments;
            row.RectorDecision = RectorDecision;
            row.FinalDeicision = FinalDeicision;
            row.SCDecision = SCDecision;
            row.RectorComments = RectorComments;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }


    public void DeleteApplication(int appID)
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Applications
                    where
                      ar.ApplicationID == appID
                    select ar;
        foreach (var row in query)
        {
            context.Applications.Remove(row);
        }
        context.SaveChanges();

    }
    #endregion 
    #region ApplicationChecklist
    public void InsertApplicationChecklist(int applicationID, string f1c, string f2c, string f3c, string f4c, string f5c, string f6c,
        string f7c, string f8c, bool? f1, bool? f2, bool? f3, bool? f4, bool? f5, bool? f6, bool? f7, bool? f8)
    {
        var context = new FPSDBEntities();
        context.ApplicationChecklists.Add(new ApplicationChecklist
        {
            ApplicationID = applicationID,
            Field1Comments = f1c,
            Field2Comments = f2c,
            Field3Comments = f3c,
            Field4Comments = f4c,
            Field5Comments = f5c,
            Field6Comments = f6c,
            Field7Comments = f7c,
            Field8Comments = f8c,
            Field1Status = f1,
            Field2Status = f2,
            Field3Status = f3,
            Field4Status = f4,
            Field5Status = f5,
            Field6Status = f6,
            Field7Status = f7,
            Field8Status = f8

        });
        context.SaveChanges();

    }
    #endregion
    #region ApplicationLog 

    public void InsertApplication_Log(int ApplicationID, int? ActionID, DateTime? ActionTime, string Message, string IsActingRole, string Details, string Actor)
    {
        var context = new FPSDBEntities();
        context.Application_Log.Add(new Application_Log
        {
            ApplicationID = ApplicationID,
            ActionID = ActionID,
            ActionTime = ActionTime,
            Message = Message,
            Comments = IsActingRole,
            Details = Details,
            Actor = Actor,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeleteApplication_Log(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Application_Log
                    where
                      ar.ApplicationID == ApplicationID
                    select ar;
        foreach (var row in query)
        {
            context.Application_Log.Remove(row);
        }
        context.SaveChanges();
    }
    public List<Application_Log> GetApplicationLog()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Application_Log
                    select a).ToList();
        return data;
    }


    #endregion
    #region Application_LogExt
    public void DeleteApplication_LogExtByAppID(int appID)
    {
        var context = new FPSDBEntities();
        var query = from atle in context.Application_LogExt
                    where
                      atle.ApplicationID == appID
                    select atle;
        foreach (var row in query)
        {
            context.Application_LogExt.Remove(row);
        }
        context.SaveChanges();

    }
    public void InsertAppLgExt(int ApplicationID, string ExtActionTitle, int ID, string Name, string Message, DateTime? ActionTime)
    {
        var context = new FPSDBEntities();
        context.Application_LogExt.Add(new Application_LogExt
        {
            ApplicationID = ApplicationID,
            ExtActionTitle = ExtActionTitle,
            ID = ID,
            Name = Name,
            Message = Message,
            ActionTime = ActionTime
        });
        context.SaveChanges();
    }

    #endregion
    #region Application_TaskLog

    public List<Application_TaskLog> GetAppTaskLogByAppIDTskIDActID(int ApplicationID, int? TaskID, int ActionID)
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    where
                    (
                    atl.ApplicationID == ApplicationID &&
                    atl.TaskID == TaskID &&
                    atl.ActionID == ActionID
                    )
                    select atl).ToList();
        return data;
    }
    public List<Application_TaskLog> GetAppTaskLogByAppIDCompleted(int ApplicationID, bool Completed)
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    where
                    (
                    atl.ApplicationID == ApplicationID &&
                    atl.Completed == Completed
                    )
                    select atl).ToList();
        return data;
    }
    public List<Application_TaskLog> GetAppTaskLogByAppIDCompletedActionID(int ApplicationID, bool Completed, int ActionID1, int ActionID2)
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    where
                    (
                    atl.ApplicationID == ApplicationID &&
                    atl.Completed == Completed &&
                    atl.ActionID == ActionID1 || atl.ActionID == ActionID2
                    )
                    select atl).ToList();
        return data;
    }
    public List<Application_TaskLog> GetAppTaskLogByAppIDNotTaskID(int ApplicationID, int TaskID)
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    where
                    (
                    atl.ApplicationID == ApplicationID &&
                    atl.Completed == false &&
                    atl.TaskID != TaskID
                    )
                    select atl).ToList();
        return data;
    }
    public List<Application_TaskLog> GetAppTaskLogByAppIDTaskIDCompleted(int ApplicationID, int TaskID, bool Completed)
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    where
                    (
                    atl.ApplicationID == ApplicationID &&
                    atl.Completed == Completed &&
                    atl.TaskID == TaskID
                    )
                    select atl).ToList();
        return data;
    }
    public List<Application_TaskLog> GetAppTaskLogByAppIDTaskID(int ApplicationID, int TaskID)
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    where
                    (
                    atl.ApplicationID == ApplicationID &&
                    atl.TaskID == TaskID
                    )
                    select atl).ToList();
        return data;
    }
    public List<Application_TaskLog> GetAppTaskLog(int appID)
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    where atl.ApplicationID == appID
                    select atl).ToList();
        return data;
    }
    //public List<Application_TaskLog> GetAppTaskLog()
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from atl in context.Application_TaskLog
    //                select atl).ToList();
    //    return data;
    //}

    public void InsertAppTskLg(int applicationID, int? taskID, int? actionID, DateTime? sentDate, bool completed
        , byte reminders, string message)
    {
        var context = new FPSDBEntities();
        context.Application_TaskLog.Add(new Application_TaskLog
        {
            ApplicationID = applicationID,
            TaskID = taskID.Value,
            ActionID = actionID,
            SentDate = sentDate,
            Completed = completed,
            Reminders = reminders,
            Message = message,

        });
        context.SaveChanges();
    }

    public void UpdateAppTaskLogStatusByTaskID(bool completed, int applicationID, int? taskID)
    {
        var context = new FPSDBEntities();
        var query = from aptl in context.Application_TaskLog
                    where
                    aptl.ApplicationID == applicationID &&
                    aptl.TaskID == taskID
                    select aptl;
        foreach (var row in query)
        {
            row.ApplicationID = applicationID;
            row.TaskID = taskID.Value;
            row.Completed = completed;
        }
        context.SaveChanges();
    }
    public void UpdateAppTaskLogReminders(int TaskLogID)
    {
        var context = new FPSDBEntities();
        var query = from aptl in context.Application_TaskLog
                    where
                    aptl.TaskLogID == TaskLogID
                    select aptl;
        foreach (var row in query)
        {
            row.Reminders = Convert.ToByte(row.Reminders + 1);
        }
        context.SaveChanges();
    }
    public void UpdateApplication_TaskLog(string OldName, string NewName, int TaskLogID)
    {
        var context = new FPSDBEntities();
        var query = from aptl in context.Application_TaskLog
                    where
                    aptl.TaskLogID == TaskLogID
                    select aptl;
        foreach (var row in query)
        {
            row.Message = Cryptography.Encrypt(Cryptography.Decrypt(row.Message).Replace(OldName, NewName));
        }
        context.SaveChanges();
    }
    public void DeleteAppTskLg(int AppLicationID)
    {
        var context = new FPSDBEntities();
        var query = from atle in context.Application_TaskLog
                    where

                      atle.ApplicationID == AppLicationID
                    select atle;
        foreach (var row in query)
        {
            context.Application_TaskLog.Remove(row);
        }
        context.SaveChanges();

    }

    public int GetNoOfActiveTask(int appID)
    {
        return GetAppTaskLog(appID)
                    .Where(atl => !atl.Completed)
                    .GroupBy(atl => atl.TaskID)
                    .Select(group => group.First()).Count();
    }
    public List<Application_TaskLog> GetAppTaskLogByAppIDTskIDActID(int ApplicationID, int TaskID, int ActionID)
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    where
                    (
                    atl.ApplicationID == ApplicationID &&
                    atl.TaskID == TaskID &&
                    atl.ActionID == ActionID
                    )
                    select atl).ToList();
        return data;
    }
    //public List<Application_TaskLog> GetAppTaskLog(int appID)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from atl in context.Application_TaskLog
    //                where atl.ApplicationID == appID
    //                select atl).ToList();
    //    return data;
    //}
    public List<Application_TaskLog> GetAppTaskLog()
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    select atl).ToList();
        return data;
    }
    public List<Application_TaskLog> GetAppTaskLogClosedApp(int TaskID, bool Complete)
    {
        var context = new FPSDBEntities();
        var data = (from atl in context.Application_TaskLog
                    where !atl.Application.ApplicationClosed
                    && atl.TaskID == TaskID
                    && atl.Completed == Complete
                    select atl).ToList();
        return data;
    }

    public void UpdateAppTaskLgEncryptMessage(int TaskLogID)
    {
        var context = new FPSDBEntities();
        var query = from c in context.Application_TaskLog
                    where
                    c.TaskLogID == TaskLogID
                    select c;

        foreach (var row in query)
        {
            row.Message = Cryptography.Encrypt(row.Message);
        }
        context.SaveChanges();
    }
    public void UpdateAppTaskLgDecryptMessage(int TaskLogID)
    {
        var context = new FPSDBEntities();
        var query = from c in context.Application_TaskLog
                    where
                    c.TaskLogID == TaskLogID
                    select c;

        foreach (var row in query)
        {
            row.Message = Cryptography.Decrypt(row.Message);
        }
        context.SaveChanges();
    }
    public void UpdateAppTaskLgEncryptMessage()
    {
        var context = new FPSDBEntities();
        var query = from c in context.Application_TaskLog

                    select c;

        foreach (var row in query)
        {
            row.Message = Cryptography.Encrypt(row.Message);
        }
        context.SaveChanges();
    }
    public void UpdateAppTaskLgDecryptMessage()
    {
        var context = new FPSDBEntities();
        var query = from c in context.Application_TaskLog

                    select c;

        foreach (var row in query)
        {
            row.Message = Cryptography.Decrypt(row.Message);
        }
        context.SaveChanges();
    }
    public void DeleteApplication_TaskLog(int AppLicationID)
    {
        var context = new FPSDBEntities();
        var query = from atle in context.Application_TaskLog
                    where

                      atle.ApplicationID == AppLicationID
                    select atle;
        foreach (var row in query)
        {
            context.Application_TaskLog.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteApplication_TaskLog(int AppLicationID, byte roleID)
    {
        var context = new FPSDBEntities();
        var query = from atle in context.Application_TaskLog
                    where
                        atle.ApplicationID == AppLicationID &&
                        atle.Task.RoleID == roleID
                    select atle;
        foreach (var row in query)
        {
            context.Application_TaskLog.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region Application_TaskLogExt
    public List<Application_TaskLogExt> GetAppTaskLogExt(int TaskLogID)
    {
        var context = new FPSDBEntities();
        var data = (from aptle in context.Application_TaskLogExt
                    where
                    (
                    aptle.TaskLogID == TaskLogID
                    )
                    select aptle).ToList();
        return data;
    }
    public List<Application_TaskLogExt> GetAppTaskLogExt(int ApplicationID, int TaskID, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from aptle in context.Application_TaskLogExt
                    where
                    (
                    aptle.ApplicationID == ApplicationID &&
                    aptle.TaskID == TaskID &&
                    aptle.ExternalReviewerID == ExternalReviewerID
                    )
                    select aptle).ToList();
        return data;
    }
    public List<Application_TaskLogExt> GetAppTaskLogExt(int ApplicationID, int TaskID, string EmployeeID, int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from aptle in context.Application_TaskLogExt
                    where
                    (
                    aptle.ApplicationID == ApplicationID &&
                    aptle.TaskID == TaskID &&
                    aptle.EmployeeID == EmployeeID &&
                    aptle.ExternalEmployeeID == ExternalEmployeeID
                    )
                    select aptle).ToList();
        return data;
    }
    public List<Application_TaskLogExt> GetAppTaskLogExt()
    {
        var context = new FPSDBEntities();
        var data = (from aptle in context.Application_TaskLogExt

                    select aptle).ToList();
        return data;
    }
    public void InsertAppTskLgExt(int ApplicationID, int TaskID, int? ActionID, DateTime? SentDate, bool Completed, int Reminders, string EmailAddress, int ExternalReviewerID, string Message, string EmployeeID, int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        context.Application_TaskLogExt.Add(new Application_TaskLogExt
        {
            ApplicationID = ApplicationID,
            TaskID = TaskID,
            ActionID = ActionID,
            SentDate = SentDate,
            Completed = Completed,
            Reminders = Reminders,
            EmailAddress = EmailAddress,
            ExternalReviewerID = ExternalReviewerID,
            ExternalEmployeeID = ExternalEmployeeID,
            EmployeeID = EmployeeID,
            Message = Message,
        });
        context.SaveChanges();
    }
    public void UpdateAppTskLgExt(int ApplicationID, int TaskID, int? ActionID, DateTime? SentDate, bool Completed, int Reminders, string EmailAddress, int ExternalReviewerID, string Message, string EmployeeID, int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        var query = from aptle in context.Application_TaskLogExt
                    where
                    aptle.ApplicationID == ApplicationID &&
                    aptle.TaskID == TaskID &&
                    aptle.ExternalReviewerID == ExternalReviewerID &&
                    aptle.ExternalEmployeeID == ExternalEmployeeID &&
                    aptle.EmployeeID == EmployeeID
                    select aptle;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.TaskID = TaskID;
            row.ActionID = ActionID;
            row.SentDate = SentDate;
            row.Completed = Completed;
            row.Reminders = Reminders;
            row.EmailAddress = EmailAddress;
            row.ExternalReviewerID = ExternalReviewerID;
            row.ExternalEmployeeID = ExternalEmployeeID;
            row.EmployeeID = EmployeeID;
            row.Message = Message.Trim();
            row.EmployeeID = EmployeeID;
        }
        context.SaveChanges();
    }
    public void DeleteApplication_TaskLogExt(int TaskLogID)
    {
        var context = new FPSDBEntities();
        var query = from atle in context.Application_TaskLogExt
                    where

                      atle.TaskLogID == TaskLogID
                    select atle;
        foreach (var row in query)
        {
            context.Application_TaskLogExt.Remove(row);
        }
        context.SaveChanges();

    }
    public void DeleteApplication_TaskLogExtByAppID(int appID)
    {
        var context = new FPSDBEntities();
        var query = from atle in context.Application_TaskLogExt
                    where

                      atle.ApplicationID == appID
                    select atle;
        foreach (var row in query)
        {
            context.Application_TaskLogExt.Remove(row);
        }
        context.SaveChanges();

    }
    #endregion
    #region Application Task Form 
    public List<Application_TaskForm> GetApplicationTaskForm(int ApplicationID, int TaskID, bool TaskExternal, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from atf in context.Application_TaskForm
                    where atf.TaskID == TaskID && atf.Task.TaskForms.Where(tf => tf.FormID == atf.FormID && tf.Checkable).Count() > 0
                    && atf.TaskExternal == TaskExternal && atf.ExternalReviewerID == ExternalReviewerID
                    && atf.ApplicationID == ApplicationID
                    select atf).ToList();
        return data;

    }
    public List<Application_TaskForm> GetApplicationTaskForm(int ApplicationID, int TaskID, byte FormID, bool TaskExternal, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from atf in context.Application_TaskForm
                    where
                    (
                       atf.ApplicationID == ApplicationID
                       && atf.TaskID == TaskID
                       && atf.FormID == FormID
                       && atf.TaskExternal == TaskExternal
                       && atf.ExternalReviewerID == ExternalReviewerID

                    )
                    select atf).ToList();
        return data;
    }

    public List<Application_TaskForm> GetAppTaskFormByAppID(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from atf in context.Application_TaskForm
                    where atf.ApplicationID == ApplicationID

                    select atf).ToList();
        return data;

    }
    public List<Application_TaskForm> GetAppTaskFormByAppID(int ApplicationID, int TaskID)
    {
        var context = new FPSDBEntities();
        var data = (from atf in context.Application_TaskForm
                    where atf.TaskID == TaskID && atf.Task.TaskForms.Where(tf => tf.FormID == atf.FormID && tf.Checkable).Count() > 0
                    select atf).ToList();
        return data;

    }

    public List<Application_TaskForm> GetAppTaskFormByAppID(int ApplicationID, int TaskID, bool TaskExternal, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var data = (from atf in context.Application_TaskForm
                    where atf.TaskID == TaskID && atf.Task.TaskForms.Where(tf => tf.FormID == atf.FormID && tf.Checkable).Count() > 0
                    && atf.TaskExternal == TaskExternal && atf.ExternalReviewerID == ExternalReviewerID
                    && atf.ApplicationID == ApplicationID
                    select atf).ToList();
        return data;

    }
    public void UpdateApplicationTaskForm(int ApplicationID, int TaskID, byte FormID, bool Completed, string Message, bool TaskExternal, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var query = from atf in context.Application_TaskForm
                    where
                     atf.ApplicationID == ApplicationID &&
                     atf.FormID == FormID &&
                     atf.TaskID == TaskID &&
                     atf.TaskExternal == TaskExternal &&
                     atf.ExternalReviewerID == ExternalReviewerID
                    select atf;
        foreach (var row in query)
        {
            row.Completed = Completed;
            row.Message = Message;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();

    }

    public void UpdateAppTskFrm(int ApplicationID, int TaskID, byte FormID, bool Completed, string Message, bool TaskExternal, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var query = from atf in context.Application_TaskForm
                    where
                    atf.ApplicationID == ApplicationID &&
                    atf.TaskID == TaskID &&
                    atf.FormID == FormID &&
                    atf.TaskExternal == TaskExternal &&
                    atf.ExternalReviewerID == ExternalReviewerID
                    select atf;
        foreach (var row in query)
        {
            row.Completed = Completed;
            row.Message = Message;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public void InsertAppTskFrm(int ApplicationID, int TaskID, byte FormID, bool Completed, string Message, bool TaskExternal, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        context.Application_TaskForm.Add(new Application_TaskForm
        {
            ApplicationID = ApplicationID,
            TaskID = TaskID,
            FormID = FormID,

            Completed = Completed,
            Message = Message,
            TaskExternal = TaskExternal,
            ExternalReviewerID = ExternalReviewerID,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now

        });
        context.SaveChanges();
    }
    public void DeletetAppTskFrm(int ApplicationID, int TaskID, byte FormID, bool TaskExternal, int ExternalReviewerID)
    {
        var context = new FPSDBEntities();
        var query = from atf in context.Application_TaskForm
                    where
                      atf.ApplicationID == ApplicationID &&
                      atf.FormID == FormID &&
                      atf.TaskID == TaskID &&
                      atf.TaskExternal == TaskExternal &&
                      atf.ExternalReviewerID == ExternalReviewerID
                    select atf;
        foreach (var row in query)
        {
            context.Application_TaskForm.Remove(row);
        }
        context.SaveChanges();
    }
    public void DeletetAppTskFrm(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from atf in context.Application_TaskForm
                    where
                      atf.ApplicationID == ApplicationID
                    select atf;
        foreach (var row in query)
        {
            context.Application_TaskForm.Remove(row);
        }
        context.SaveChanges();
    }
    public void DeletetAppTskFrmByExtRevID(int ExtRevID)
    {
        var context = new FPSDBEntities();
        var query = from atf in context.Application_TaskForm
                    where
                      atf.ExternalReviewerID == ExtRevID
                    select atf;
        foreach (var row in query)
        {
            context.Application_TaskForm.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion   
    #region applicationTaskLoGExt
    public void UpdateAppTaskLgExtEncryptMessage(int TaskLogID)
    {
        var context = new FPSDBEntities();
        var query = from c in context.Application_TaskLogExt
                    where
                    c.TaskLogID == TaskLogID
                    select c;

        foreach (var row in query)
        {
            row.Message = Cryptography.Encrypt(row.Message);
        }
        context.SaveChanges();
    }
    public void UpdateAppTaskLgExtDecryptMessage(int TaskLogID)
    {
        var context = new FPSDBEntities();
        var query = from c in context.Application_TaskLogExt
                    where
                    c.TaskLogID == TaskLogID
                    select c;

        foreach (var row in query)
        {
            row.Message = Cryptography.Decrypt(row.Message);
        }
        context.SaveChanges();
    }
    public void UpdateAppTaskLgExtEncryptMessage()
    {
        var context = new FPSDBEntities();
        var query = from c in context.Application_TaskLogExt

                    select c;

        foreach (var row in query)
        {
            row.Message = Cryptography.Encrypt(row.Message);
        }
        context.SaveChanges();
    }
    public void UpdateAppTaskLgExtDecryptMessage()
    {
        var context = new FPSDBEntities();
        var query = from c in context.Application_TaskLogExt

                    select c;

        foreach (var row in query)
        {
            row.Message = Cryptography.Decrypt(row.Message);
        }
        context.SaveChanges();
    }
    #endregion
    #region Application_ActionStatus
    public List<Application_ActionStatus> GetApplicationActionStatus()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Application_ActionStatus
                    select a).ToList();
        return data;
    }
    public List<Application_ActionStatus> GetApplicationActionStatus(int applicationID, int actionID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Application_ActionStatus
                    where
                    (
                    a.ApplicationID == applicationID &&
                    a.ActionID == actionID
                    )
                    select a).ToList();
        return data;
    }
    public List<Application_ActionStatus> GetApplicationActionStatus(int applicationID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Application_ActionStatus
                    where
                    (
                    a.ApplicationID == applicationID

                    )
                    select a).ToList();
        return data;
    }
    public void UpdateApplicationActionStatus(int ApplicationID, int ActionID, string Status)
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Application_ActionStatus
                    where ar.ApplicationID == ApplicationID
                    && ar.ActionID == ActionID
                    select ar;
        foreach (var row in query)
        {
            row.ApplicationID = ApplicationID;
            row.ActionID = ActionID;
            row.Status = Status;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();

    }
    public void InsertApplicationActionStatus(int ApplicationID, int ActionID, string Status)
    {
        var context = new FPSDBEntities();
        context.Application_ActionStatus.Add(new Application_ActionStatus
        {
            ApplicationID = ApplicationID,
            ActionID = ActionID,
            Status = Status,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeleteApplicationActionStatus(int ApplicationID, int actionID)
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Application_ActionStatus
                    where
                      ar.ApplicationID == ApplicationID &&
                      ar.ActionID == actionID
                    select ar;
        foreach (var row in query)
        {
            context.Application_ActionStatus.Remove(row);
        }
        context.SaveChanges();
    }
    public void DeleteApplicationActionStatus(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from ar in context.Application_ActionStatus
                    where
                      ar.ApplicationID == ApplicationID
                    select ar;
        foreach (var row in query)
        {
            context.Application_ActionStatus.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region Application List
    public List<ApplicationList> GetApplicationList(int ApplicationID, string EmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Applications
                    join ar in context.Application_Role on a.ApplicationID equals ar.ApplicationID
                    join e in context.Employees on a.EmployeeID equals e.EmployeeID
                    join r in context.Roles on ar.RoleID equals r.RoleID
                    join d in context.Departments on e.DepartmentID equals d.DepartmentID

                    where
                    (
                    ar.ApplicationID == ApplicationID && ar.EmployeeID == EmployeeID
                    )
                    select new ApplicationList
                    {
                        ApplicationID = a.ApplicationID,
                        Role = r.Title,
                        Employee = e.Email,
                        Name = e.Title + e.Name,
                        Department = d.Name,
                        Rank = e.Rank,
                        ToRank = a.ForRank,
                        Status = !a.ApplicationClosed ? RecordStatus.Active.ToString() : "Ended",
                        AppDate = a.StartDate,
                        RoleID = r.RoleID

                    }).ToList<ApplicationList>();
        return data;
    }
    //public List<pr_ActiveApplicationList_Result> GetActiveMyApplicationList(string EmployeeID)
    //{
    //    var context = new Entities();
    //    List<pr_ActiveApplicationList_Result> prAAL = context.pr_ActiveApplicationList(EmployeeID).Where(p => p.RoleID == (byte)RoleID.Applicant).ToList();

    //    return prAAL;

    //}
    //public List<pr_ActiveApplicationList_Result> GetActiveApplicationList(string EmployeeID)
    //{
    //    var context = new Entities();
    //    List<pr_ActiveApplicationList_Result> prAAL = context.pr_ActiveApplicationList(EmployeeID).ToList();

    //    return prAAL;

    //}
    //public List<pr_ActiveApplicationList4EE_Result> GetActiveApplicationList4EE(int eEmployeeID)
    //{
    //    var context = new Entities();
    //    List<pr_ActiveApplicationList4EE_Result> prAAL = context.pr_ActiveApplicationList4EE(eEmployeeID).ToList();

    //    return prAAL;

    //}
    //public List<pr_QuickJumpDDL_Result> GetQuickJumpDDL(string EmployeeID)
    //{
    //    var context = new Entities();
    //    List<pr_QuickJumpDDL_Result> lal = context.pr_QuickJumpDDL(EmployeeID).ToList();

    //    return lal;

    //}

    #endregion
    #endregion
    #region Forms 
    #region Form_ServiceYears
    public List<Form_ServiceYears> GetForm_ServiceYears()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Form_ServiceYears
                    select a).ToList();
        return data;
    }
    public List<Form_ServiceYears> GetForm_ServiceYears(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Form_ServiceYears
                    where
                    (
                    a.ApplicationID == ApplicationID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateForm_ServiceYears(int ApplicationID, byte? App_Service_NumYears, byte? App_Service_NumMonths, byte? App_Saudi_NumYears, byte? App_Saudi_NumMonths
        , byte? Service_NumYears, byte? Service_NumMonths, byte? Saudi_NumYears, byte? Saudi_NumMonths, string Evaluations
        , float? tsy1, float? rsy1, float? cssy1, float? tsy2, float? rsy2, float? cssy2, int? Year1, int? Year2)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_ServiceYears
                    where
                         a.ApplicationID == ApplicationID
                    select a;
        foreach (var row in query)
        {
            row.App_Service_NumYears = App_Service_NumYears;
            row.App_Service_NumMonths = App_Service_NumMonths;
            row.App_Saudi_NumYears = App_Saudi_NumYears;
            row.App_Saudi_NumMonths = App_Saudi_NumMonths;
            row.Service_NumYears = Service_NumYears;
            row.Service_NumMonths = Service_NumMonths;
            row.Saudi_NumYears = Saudi_NumYears;
            row.Saudi_NumMonths = Saudi_NumMonths;
            row.Evaluations = Evaluations;
            row.TeachingScoreYear1 = tsy1;
            row.ResearchScoreYear1 = rsy1;
            row.ComServiceScoreYear1 = cssy1;
            row.TeachingScoreYear2 = tsy2;
            row.ResearchScoreYear2 = rsy2;
            row.ComServiceScoreYear2 = cssy2; row.Year1 = Year1; row.Year2 = Year2;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }

        }
        context.SaveChanges();
    }
    public void InsertForm_ServiceYears(int ApplicationID, byte? App_Service_NumYears, byte? App_Service_NumMonths, byte? App_Saudi_NumYears, byte? App_Saudi_NumMonths
        , byte? Service_NumYears, byte? Service_NumMonths, byte? Saudi_NumYears, byte? Saudi_NumMonths, string Evaluations
        , float? tsy1, float? rsy1, float? cssy1, float? tsy2, float? rsy2, float? cssy2, int? Year1, int? Year2)
    {
        var context = new FPSDBEntities();
        context.Form_ServiceYears.Add(new Form_ServiceYears
        {
            ApplicationID = ApplicationID,
            App_Service_NumYears = App_Service_NumYears,
            App_Service_NumMonths = App_Service_NumMonths,
            App_Saudi_NumYears = App_Saudi_NumYears,
            App_Saudi_NumMonths = App_Saudi_NumMonths,
            Service_NumYears = Service_NumYears,
            Service_NumMonths = Service_NumMonths,
            Saudi_NumYears = Saudi_NumYears,
            Saudi_NumMonths = Saudi_NumMonths,
            Evaluations = Evaluations,
            TeachingScoreYear1 = tsy1,
            ResearchScoreYear1 = rsy1,
            ComServiceScoreYear1 = cssy1,
            TeachingScoreYear2 = tsy2,
            ResearchScoreYear2 = rsy2,
            ComServiceScoreYear2 = cssy2,
            Year1 = Year1,
            Year2 = Year2
        ,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeleteForm_ServiceYears(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_ServiceYears
                    where
                      a.ApplicationID == ApplicationID

                    select a;
        foreach (var row in query)
        {
            context.Form_ServiceYears.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #endregion
    #endregion
    #region  View Next Task
    public List<vw_NextTaskDTO> GetNextTask()
    {
        return GetNextTaskNextTaskIDNotNull().Concat(GetNextTaskNextTaskIDNull()).ToList();
    }
    public List<vw_NextTaskDTO> GetNextTaskNextTaskIDNull()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.WFActions
                    join t in context.Tasks on a.TaskID equals t.TaskID
                    join nextT in context.Tasks on a.NextPhaseID equals nextT.PhaseID
                    join nextR in context.Roles on nextT.RoleID equals nextR.RoleID
                    join am in context.ActionMessages on new { v1 = a.ActionID, v2 = nextT.TaskID } equals new { v1 = am.ActionID, v2 = am.NextTaskID }
                    into amanexT
                    from am in amanexT.DefaultIfEmpty()
                    where
                    (
                        a.NextTaskID == null
                    )
                    select new vw_NextTaskDTO
                    {
                        TaskID = a.TaskID,
                        Task = t.Title,
                        ActionID = a.ActionID,
                        Action = a.Title,
                        NextPhaseID = a.NextPhaseID,
                        NextTaskID = nextT.TaskID,
                        NextTask = nextT.Title,
                        NextRoleID = nextR.RoleID,
                        NextRole = nextR.Title,
                        Message = am.Message,
                        HasCommitteeInfo = am.HasCommitteeInfo,
                        PhaseID = am.PhaseID,
                        Type = a.Type,

                    }).ToList();
        return data;
    }


    public void DeleteAttachmentByDocumentID(int documentID)
    {
        throw new NotImplementedException();
    }

    public List<vw_NextTaskDTO> GetNextTaskNextTaskIDNotNull()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.WFActions
                    join t in context.Tasks on a.TaskID equals t.TaskID
                    join nextT in context.Tasks on a.NextTaskID equals nextT.TaskID
                    join nextR in context.Roles on nextT.RoleID equals nextR.RoleID
                    join am in context.ActionMessages on new { v1 = a.ActionID, v2 = nextT.TaskID } equals new { v1 = am.ActionID, v2 = am.NextTaskID }
                    into amanexT
                    from am in amanexT.DefaultIfEmpty()
                    where
                    (
                        a.NextTaskID != null && a.TaskID != nextT.TaskID
                    )
                    select new vw_NextTaskDTO
                    {
                        TaskID = a.TaskID,
                        Task = a.Task.Title,
                        ActionID = a.ActionID,
                        Action = a.Title,
                        NextPhaseID = a.NextPhaseID,
                        NextTaskID = nextT.TaskID,
                        NextTask = nextT.Title,
                        NextRoleID = nextR.RoleID,
                        NextRole = nextR.Title,
                        Message = am.Message,
                        HasCommitteeInfo = am.HasCommitteeInfo,
                        PhaseID = am.PhaseID,
                        Type = a.Type,
                    }).ToList();
        return data;
    }

    public List<vw_NextTaskDTO> GetNextTask(int TaskID, int ActionID, int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from nt in GetNextTask()
                    join ar in context.Application_Role on nt.NextRoleID equals ar.RoleID
                    where
                    (
                        ar.ApplicationID == ApplicationID &&
                        nt.TaskID == TaskID &&
                        nt.ActionID == ActionID
                    )
                    select new vw_NextTaskDTO
                    {
                        TaskID = nt.TaskID,
                        Task = nt.Task,
                        ActionID = nt.ActionID,
                        Action = nt.Action,
                        NextPhaseID = nt.NextPhaseID,
                        NextTaskID = nt.NextTaskID,
                        NextTask = nt.NextTask,
                        NextRole = nt.NextRole,
                        Message = nt.Message,
                        HasCommitteeInfo = nt.HasCommitteeInfo,
                        PhaseID = nt.PhaseID,
                        Type = nt.Type,
                        NextRoleID = nt.NextRoleID,
                    }).ToList();
        return data;
    }


    #endregion
    #region Permanent Committee 
    public List<PermanentCommittee> GetPermanentCommitteeByTypeID(int PromotionCommitteeTypeID)
    {
        var context = new FPSDBEntities();
        var data = (from pc in context.PermanentCommittees
                    where pc.PermanentCommitteeTypeID == PromotionCommitteeTypeID
                    select pc).ToList();
        return data;

    }
    public List<PermanentCommittee> GetPermanentCommittee()
    {
        var context = new FPSDBEntities();
        var data = (from pc in context.PermanentCommittees
                    select pc).ToList();
        return data;
    }
    public List<PermanentCommittee> GetPermanentCommittee(int PermanentCommitteeTypeID, string EmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from pc in context.PermanentCommittees
                    where pc.PermanentCommitteeTypeID == PermanentCommitteeTypeID
                    && pc.EmployeeID == EmployeeID
                    select pc).ToList();
        return data;
    }
    public List<PermanentCommittee> GetPermanentCommittee(int PermanentCommitteeID)
    {
        var context = new FPSDBEntities();
        var data = (from pc in context.PermanentCommittees
                    where pc.PermanentCommitteeID == PermanentCommitteeID
                    select pc).ToList();
        return data;
    }
    public void InsertPermanentCommittee(int PermanentCommitteeTypeID, string EmployeeID, int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        context.PermanentCommittees.Add(new PermanentCommittee
        {
            PermanentCommitteeTypeID = PermanentCommitteeTypeID,
            EmployeeID = EmployeeID,
            ExternalEmployeeID = ExternalEmployeeID
        });
        context.SaveChanges();
    }
    public void UpdatePermanentCommittee(int PermanentCommitteeID, int PermanentCommitteeTypeID, string EmployeeID, int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        var query = from pc in context.PermanentCommittees
                    where
                    pc.PermanentCommitteeID == PermanentCommitteeID
                    select pc;
        foreach (var row in query)
        {
            row.PermanentCommitteeID = PermanentCommitteeID;
            row.ExternalEmployeeID = ExternalEmployeeID;
            row.EmployeeID = EmployeeID;
            row.PermanentCommitteeTypeID = PermanentCommitteeTypeID;

        }
        context.SaveChanges();
    }
    public void DeletetPermanentCommittee(int PermanentCommitteeID)
    {
        var context = new FPSDBEntities();
        var query = from pc in context.PermanentCommittees
                    where
                      pc.PermanentCommitteeID == PermanentCommitteeID
                    select pc;
        foreach (var row in query)
        {
            context.PermanentCommittees.Remove(row);
        }
        context.SaveChanges();
    }
    public void DeletetPermanentCommitteeByType(int PermanentCommitteeTypeID)
    {
        var context = new FPSDBEntities();
        var query = from pc in context.PermanentCommittees
                    where
                      pc.PermanentCommitteeTypeID == PermanentCommitteeTypeID
                    select pc;
        foreach (var row in query)
        {
            context.PermanentCommittees.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region Permanent Committee Type
    public List<PermanentCommitteeType> GetPermanentCommitteeTypeByTypeID(int PromotionCommitteeTypeID)
    {
        var context = new FPSDBEntities();
        var data = (from pc in context.PermanentCommitteeTypes
                    where pc.PermanentCommitteeTypeID == PromotionCommitteeTypeID
                    select pc).ToList();
        return data;

    }
    public List<PermanentCommitteeType> GetPermanentCommitteeType()
    {
        var context = new FPSDBEntities();
        var data = (from pc in context.PermanentCommitteeTypes
                    select pc).ToList();
        return data;
    }
    public void InsertPermanentCommitteeType(string Title)
    {
        var context = new FPSDBEntities();
        context.PermanentCommitteeTypes.Add(new PermanentCommitteeType
        {
            Title = Title
        });
        context.SaveChanges();
    }
    public void DeletetPermanentCommitteeType(int PermanentCommitteeTypeID)
    {
        var context = new FPSDBEntities();
        var query = from pc in context.PermanentCommitteeTypes
                    where
                      pc.PermanentCommitteeTypeID == PermanentCommitteeTypeID
                    select pc;
        foreach (var row in query)
        {
            context.PermanentCommitteeTypes.Remove(row);
        }
        context.SaveChanges();
    }

    #endregion

    #region SentEmail 
    public void UpdateSentEmailEncryptBody(int ID)
    {
        var context = new FPSDBEntities();
        var query = from c in context.SentEmails
                    where
                    c.ID == ID
                    select c;

        foreach (var row in query)
        {
            row.Body = Cryptography.Encrypt(row.Body);
        }
        context.SaveChanges();
    }
    public void UpdateSentEmailDecryptBody(int ID)
    {
        var context = new FPSDBEntities();
        var query = from c in context.SentEmails
                    where
                    c.ID == ID
                    select c;

        foreach (var row in query)
        {
            row.Body = Cryptography.Decrypt(row.Body);
        }
        context.SaveChanges();
    }
    public void UpdateSentEmailEncryptBody()
    {
        var context = new FPSDBEntities();
        var query = from c in context.SentEmails


                    select c;

        foreach (var row in query)
        {
            row.Body = Cryptography.Encrypt(row.Body);
        }
        context.SaveChanges();
    }
    public void UpdateSentEmailDecryptBody()
    {
        var context = new FPSDBEntities();
        var query = from c in context.SentEmails

                    select c;

        foreach (var row in query)
        {
            row.Body = Cryptography.Decrypt(row.Body);
        }
        context.SaveChanges();
    }

    public List<SentEmail> GetSentEmail()
    {
        var context = new FPSDBEntities();
        var data = (from a in context.SentEmails
                    select a).ToList();
        return data;
    }
    public List<SentEmail> GetSentEmailByID(int ID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.SentEmails
                    where a.ID == ID
                    select a).ToList();
        return data;
    }
    public List<SentEmail> GetSentEmail(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.SentEmails
                    where
                    (
                    a.ApplicationID == ApplicationID
                    )
                    select a).ToList();
        return data;
    }
    public void UpdateSentEmail(int ID, DateTime SentDate, string ToEmail, string Subject, string Body, string FromEmail, int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.SentEmails
                    where
                         a.ID == ID
                    select a;
        foreach (var row in query)
        {
            row.SentDate = SentDate;
            row.ToEmail = ToEmail;
            row.Subject = Subject;
            row.Body = Body;
            row.FromEmail = FromEmail;
            row.ApplicationID = ApplicationID;

        }
        context.SaveChanges();
    }
    public void InsertSentEmail(DateTime SentDate, string ToEmail, string Subject, string Body, string FromEmail, int? ApplicationID)
    {
        var context = new FPSDBEntities();
        context.SentEmails.Add(new SentEmail
        {
            SentDate = SentDate,
            ToEmail = ToEmail,
            Subject = Subject,
            Body = Body,
            FromEmail = FromEmail,
            ApplicationID = ApplicationID,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    public void DeleteSentEmail(int ID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.SentEmails
                    where
                      a.ID == ID

                    select a;
        foreach (var row in query)
        {
            context.SentEmails.Remove(row);
        }
        context.SaveChanges();
    }
    public void DeleteSentEmailByAppID(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.SentEmails
                    where
                      a.ApplicationID == ApplicationID

                    select a;
        foreach (var row in query)
        {
            context.SentEmails.Remove(row);
        }
        context.SaveChanges();
    }

    #endregion

    #region HR
    #region ExternalEmployee
    public List<ExternalEmployee> GetExternalEmployeeByPK(int ExternalEmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from e in context.ExternalEmployees
                    where
                    (
                       e.ExternalEmployeeID == ExternalEmployeeID
                    )
                    select e).ToList();
        return data;
    }
    public List<ExternalEmployee> GetExternalEmployeeByEmail(string email)
    {
        var context = new FPSDBEntities();
        var data = (from ee in context.ExternalEmployees
                    where
                    (
                       ee.Email == email || ee.Email2 == email
                    )
                    select ee).ToList();
        return data;
    }
    public List<ExternalEmployee> GetExternalEmployee()
    {
        var context = new FPSDBEntities();
        var data = (from ee in context.ExternalEmployees
                    select ee).ToList();
        return data;
    }
    public void DeleteExternalEmployee(int eEmpID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.ExternalEmployees
                    where
                      a.ExternalEmployeeID == eEmpID

                    select a;
        foreach (var row in query)
        {
            context.ExternalEmployees.Remove(row);
        }
        context.SaveChanges();
    }
    public void UpdateExternalEmployee(
        int eEmpID, string Title, string Name, string Rank, string Department, string Organization, string Address, string Email,
        string Email2, string Phone, string Mobile, string Country, string NameString, string Status, string Password, string Major,
        string Website)
    {
        var context = new FPSDBEntities();
        var query = from a in context.ExternalEmployees
                    where
                         a.ExternalEmployeeID == eEmpID
                    select a;
        foreach (var row in query)
        {
            row.Address = Address;
            row.Country = Country;
            row.Department = Department;
            row.Email = Email;
            row.Email2 = Email2;
            row.Major = Major;
            row.Mobile = Mobile;
            row.Title = PromotionApplication.SetTitle(Title);
            row.Name = Name;
            row.NameString = row.Title + " " + row.Name;
            row.Organization = Organization;
            row.Password = Password;
            row.Phone = Phone;
            row.Rank = Rank;
            row.Status = Status;


        }
        context.SaveChanges();
    }

    public void InsertExternalEmployee(string Title, string Name, string Rank, string Department, string Organization, string Address, string Email,
        string Email2, string Phone, string Mobile, string Country, string NameString, string Status, string Password, string Major,
        string Website)
    {
        var context = new FPSDBEntities();
        context.ExternalEmployees.Add(new ExternalEmployee
        {
            Address = Address,
            Country = Country,
            Department = Department,
            Email = Email,
            Email2 = Email2,
            Major = Major,
            Mobile = Mobile,
            Title = PromotionApplication.SetTitle(Title),
            Name = Name,
            NameString = Title + " " + Name,
            Organization = Organization,
            Password = Password,
            Phone = Phone,
            Rank = Rank,
            Status = Status
        ,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now
        });
        context.SaveChanges();
    }
    #endregion
    #region Employee
    public List<Employee> GetEmployeeByPK(string EmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from e in context.Employees
                    where
                    (
                       e.EmployeeID == EmployeeID &&
                       e.Status == RecordStatus.Active.ToString()
                    )
                    select e).ToList();
        return data;
    }
    public List<Employee> GetEmployeeInactiveByPK(string EmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from e in context.Employees
                    where
                    (
                       e.EmployeeID == EmployeeID &&
                       e.Status == RecordStatus.Inactive.ToString()
                    )
                    select e).ToList();
        return data;
    }
    //public List<Employee> GetEmployeeByEmail(string Email)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from e in context.Employees
    //                where
    //                (
    //                   e.Email == Email && 
    //                   e.Status == RecordStatus.Active.ToString()
    //                )
    //                select e).ToList();
    //    return data;
    //}
    //public List<Employee> GetEmployeeByDeputyEmail(string Email)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from e in context.Employees
    //                join d in context.Departments on e.EmployeeID equals d.HeadEmpID
    //                where
    //                (
    //                   d.DeputyEmail == Email &&
    //                   e.Status == RecordStatus.Active.ToString()
    //                )
    //                select e).ToList();
    //    return data;
    //}
    public List<Employee> GetApplicant(int ApplicationID)
    {
        var context = new FPSDBEntities();
        var data = (from a in context.Applications
                    join e in context.Employees on a.EmployeeID equals e.EmployeeID
                    where
                    (
                       a.ApplicationID == ApplicationID &&
                       e.Status == RecordStatus.Active.ToString()
                    )
                    select e).ToList();
        return data;
    }
    public List<Employee> GetEmployees()
    {
        var context = new FPSDBEntities();
        var data = (from emp in context.Employees
                    where
                    (
                       emp.Status == RecordStatus.Active.ToString()
                    )
                    select emp).ToList();
        return data;
    }
    public List<Employee> GetEmployeesAll()
    {
        var context = new FPSDBEntities();
        var data = (from emp in context.Employees
                    select emp).ToList();
        return data;
    }
    public bool isEmployeeSynchNeeded(DateTime oldDate)
    {
        var context = new FPSDBEntities();
        var data = (from emp in context.Employees
                    where
                    (
                        emp.UpdateDate == null || emp.UpdateDate < oldDate
                    )
                    select emp).ToList();
        return data.Count > 0;
    }
    public List<Employee> GetEmployeesInactive()
    {
        var context = new FPSDBEntities();
        var data = (from emp in context.Employees
                    where
                    (
                       emp.Status == RecordStatus.Inactive.ToString()
                    )
                    select emp).ToList();
        return data;
    }


    public List<Employee> GetEmployeeByEmail(string Email)
    {
        var context = new FPSDBEntities();
        var data = (from e in context.Employees
                    where
                    (
                       e.Email.ToLower() == Email.ToLower() &&
                       e.Status == RecordStatus.Active.ToString()
                    )
                    select e).ToList();
        return data;
    }
    //public List<Employee> GetEmployeeByEmailPwd(string Email, string Password)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from e in context.Employees
    //                where
    //                (
    //                   e.Email == Email && e.Password == Password
    //                )
    //                select e).ToList();
    //    return data;
    //}

    public List<Employee> GetEmployeeByAppTsk(int ApplicationID, int TaskID)
    {
        var context = new FPSDBEntities();
        var data = (from t in context.Tasks
                    join r in context.Roles on t.RoleID equals r.RoleID
                    join ar in context.Application_Role on r.RoleID equals ar.RoleID
                    join e in context.Employees on ar.EmployeeID equals e.EmployeeID
                    where
                    (
                       ar.ApplicationID == ApplicationID && t.TaskID == TaskID &&
                       e.Status == RecordStatus.Active.ToString()
                    )
                    select e).ToList();
        return data;
    }
    //public List<ExternalEmployee> GetEEmployeeByAppTsk(int ApplicationID, int TaskID)
    //{
    //    var context = new FPSDBEntities();
    //    var data = (from t in context.Tasks
    //                join r in context.Roles on t.RoleID equals r.RoleID
    //                join ar in context.Application_Role on r.RoleID equals ar.RoleID
    //                join ee in context.ExternalEmployees on ar.ExternalEmployeeID equals ee.ExternalEmployeeID
    //                where
    //                (
    //                   ar.ApplicationID == ApplicationID && t.TaskID == TaskID
    //                )
    //                select ee).ToList();
    //    return data;
    //}
    public List<Employee> GetEmployeeByAppRole(int ApplicationID, byte RoleID)
    {
        var context = new FPSDBEntities();
        var data = (from ar in context.Application_Role
                    join e in context.Employees on ar.EmployeeID equals e.EmployeeID
                    where
                    (
                       ar.ApplicationID == ApplicationID && ar.RoleID == RoleID &&
                       e.Status == RecordStatus.Active.ToString()
                    )
                    select e).ToList();
        return data;
    }
    public List<Employee> GetEmployeeByDeputyEmail(string Email)
    {
        var context = new FPSDBEntities();
        var data = (from e in context.Employees
                    join d in context.Departments on e.EmployeeID equals d.HeadEmpID
                    where
                    (
                       d.DeputyEmail.ToLower() == Email.ToLower() || d.Deputy2Email.ToLower() == Email.ToLower() &&
                       e.Status == RecordStatus.Active.ToString()
                    )
                    select e).ToList();
        return data;
    }
    public void UpdateEmployeeRank(string Rank, string EmployeeID)
    {
        var context = new FPSDBEntities();
        var query = from e in context.Employees
                    where
                    e.EmployeeID == EmployeeID
                    select e;
        foreach (var row in query)
        {
            row.Rank = Rank;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public void UpdateEmployee(string EmployeeID, string SecondEmail, string Email, string Title, string Name,
       string Rank, string POBox, string Phone, DateTime? JoinDate, string Status,
       int? DepartmentID, string NameString, string Department, string ParentDept, bool? Sex,
       string Password, string Major, string DeptShort, string Name_1, string Name_2,
       string Name_3, string Name_4, DateTime? ContractDate, DateTime? DateOfBirth, string ContractType, string Mobile
        , string Specialty, string Nationality, string Pager)
    {
        var context = new FPSDBEntities();
        var query = from e in context.Employees
                    where
                    e.EmployeeID == EmployeeID
                    select e;
        foreach (var row in query)
        {
            row.EmployeeID = EmployeeID;
            row.SecondEmail = SecondEmail;
            row.Email = Email;
            row.Title = Title;
            row.Name = Name;
            row.Rank = Rank;
            row.POBox = POBox;
            row.Phone = Phone;
            row.JoinDate = JoinDate;
            row.Status = Status;
            row.DepartmentID = DepartmentID;
            row.NameString = NameString;
            row.Department = Department;
            row.ParentDept = ParentDept;
            row.Sex = Sex;
            row.Password = Password;

            row.Major = Major;
            row.DeptShort = DeptShort;
            row.Name_1 = Name_1;
            row.Name_2 = Name_2;
            row.Name_3 = Name_3;
            row.Name_4 = Name_4;
            row.ContractDate = ContractDate;
            row.DateOfBirth = DateOfBirth;
            row.ContractType = ContractType;
            row.Mobile = Mobile;
            row.Specialty = Specialty;
            row.Nationality = Nationality;
            row.Pager = Pager;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public string InsertEmployee(string EmployeeID, string SecondEmail, string Email, string Title, string Name, string Rank,
          string POBox, string Phone, DateTime? JoinDate, string Status, int? DepartmentID, string NameString, string Department,
          string ParentDept, bool? Sex, string Password, string Major, string DeptShort, string Name_1,
          string Name_2, string Name_3, string Name_4, DateTime? ContractDate, DateTime? DateOfBirth, string ContractType,
          string Mobile, string Specialty, string Nationality, string Pager
           )
    {
        if (GetEmployeeByPK(EmployeeID).Count > 0)
        {
            return "";
        }
        var context = new FPSDBEntities();
        Employee e = new Employee
        {
            EmployeeID = EmployeeID,
            SecondEmail = SecondEmail,
            Email = Email,
            Title = Title,
            Name = Name,
            Rank = Rank,
            POBox = POBox,
            Phone = Phone,
            JoinDate = JoinDate,
            Status = Status,
            DepartmentID = DepartmentID,
            NameString = NameString,
            Department = Department,
            ParentDept = ParentDept,
            Sex = Sex,
            Password = Password,
            Major = Major,
            DeptShort = DeptShort,
            Name_1 = Name_1,
            Name_2 = Name_2,
            Name_3 = Name_3,
            Name_4 = Name_4,
            ContractDate = ContractDate,
            DateOfBirth = DateOfBirth,
            ContractType = ContractType,
            Mobile = Mobile,
            Specialty = Specialty,
            Nationality = Nationality,
            Pager = Pager,
            UpdateDate = DateTime.Now,
            InsertDate = DateTime.Now

        };

        context.Employees.Add(e);
        context.SaveChanges();
        return e.EmployeeID;
    }
    //public string InsertEmployee(string EmployeeID, string SecondEmail, string Email, string Title, string Name, string Rank,
    //   string POBox, string Phone, string JoinDate, string Status, int DepartmentID, string NameString, string Department,
    //   string ParentDept, bool Sex, string Password, string Major, string DeptShort, string Name_1,
    //   string Name_2, string Name_3, string Name_4, DateTime? ContractDate, DateTime? DateOfBirth, string ContractType,
    //   string Mobile
    //    )
    //{
    //    if (GetEmployeeByPK(EmployeeID).Count > 0)
    //    {
    //        return "";
    //    }
    //    var context = new FPSDBEntities();
    //    Employee e = new Employee
    //    {
    //        EmployeeID = EmployeeID,
    //        SecondEmail = SecondEmail,
    //        Email= Email,
    //        Title = Title,
    //        Name = Name,
    //        Rank = Rank,
    //        POBox = POBox,
    //        Phone = Phone,
    //        JoinDate = JoinDate,
    //        Status = Status,
    //        DepartmentID = DepartmentID,
    //        NameString = NameString,
    //        Department = Department,

    //    };

    //    context.Employees.Add(e);
    //    context.SaveChanges();
    //    return e.EmployeeID;
    //}
    public void DeleteEmployee(string empID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Employees
                    where
                      a.EmployeeID == empID

                    select a;
        foreach (var row in query)
        {
            context.Employees.Remove(row);
        }
        context.SaveChanges();
    }
    #endregion
    #region department
    public void DeleteDepartment(int DepartmentID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Departments
                    where
                      a.DepartmentID == DepartmentID
                    select a;
        foreach (var row in query)
        {
            context.Departments.Remove(row);
        }
        context.SaveChanges();
    }
    public Department GetDepartmentByPK(int? departmentID)
    {
        var context = new FPSDBEntities();
        var data = (from d in context.Departments
                    where
                    (
                       d.DepartmentID == departmentID
                    )
                    select d).ToList()[0];
        return data;
    }
    public List<Department> GetDepartmentBySN(string Shortname)
    {
        var context = new FPSDBEntities();
        var data = (from d in context.Departments
                    where
                    (
                       d.ShortName.ToLower() == Shortname.ToLower()
                    )
                    select d).ToList();
        return data;
    }
    public List<Department> GetDepartmentByName(string Name)
    {
        var context = new FPSDBEntities();
        var data = (from d in context.Departments
                    where
                    (
                       d.Name.ToLower() == Name.ToLower()
                    )
                    select d).ToList();
        return data;
    }

    public List<Department> GetDepartmentByAppTsk(int ApplicationID, int TaskID)
    {
        var context = new FPSDBEntities();
        var data = (from t in context.Tasks
                    join r in context.Roles on t.RoleID equals r.RoleID
                    join ar in context.Application_Role on r.RoleID equals ar.RoleID
                    join d in context.Departments on new { v1 = ar.EmployeeID, v2 = ar.RoleID } equals new { v1 = d.HeadEmpID, v2 = d.HeadRoleID.Value }
                    join e in context.Employees on d.HeadEmpID equals e.EmployeeID
                    where
                    (
                       ar.ApplicationID == ApplicationID && t.TaskID == TaskID
                    )
                    select d).ToList();
        return data;
    }
    public List<Department> GetDepartmentByEmp(string EmployeeID)
    {
        var context = new FPSDBEntities();
        var data = (from d in context.Departments
                    where
                    (
                       d.HeadEmpID == EmployeeID
                    )
                    select d).ToList();
        return data;
    }
    public List<Department> GetDepartments(string Status)
    {
        var context = new FPSDBEntities();
        var data = (from d in context.Departments
                    where
                    (
                       d.Status == Status
                    )
                    select d).ToList();
        return data;
    }

    public List<Department> GetDepartmentByHeadString(string HeadString)
    {
        var context = new FPSDBEntities();
        var data = (from d in context.Departments
                    where
                        (
                        d.HeadString == HeadString
                        )
                    select d).ToList();
        return data;
    }
    public void UpdateDepartmentDeputyEmail(string DeputyEmail, string Deputy2Email, int DepartmentID)
    {
        var context = new FPSDBEntities();
        var query = from d in context.Departments
                    where
                    d.DepartmentID == DepartmentID && d.Status != RecordStatus.Inactive.ToString()
                    select d;

        foreach (var row in query)
        {
            row.DeputyEmail = DeputyEmail;
            row.Deputy2Email = Deputy2Email;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    public void UpdateDepartment(string Name, string ShortName, int ParentDeptID, string HeadEmpID, string Phone, string Website, string Fax,
      byte HeadRoleID, string Type, string Status, string DeputyEmail, string Deputy2Email, int DepartmentID)
    {
        var context = new FPSDBEntities();
        var query = from d in context.Departments
                    where
                    d.DepartmentID == DepartmentID && d.Status != RecordStatus.Inactive.ToString()
                    select d;

        foreach (var row in query)
        {
            row.Name = Name;
            row.ShortName = ShortName;
            row.ParentDeptID = ParentDeptID;
            row.ParentDeptString = GetDeptNameString(ParentDeptID);
            row.HeadEmpID = HeadEmpID;
            row.Phone = Phone;
            row.HeadString = GetDeptHeadString(HeadEmpID, Type, Name);
            row.HeadEmail = GetEmployeeByPK(HeadEmpID)[0].Email;
            row.NameString = GetDeptNameString(Type, Name);
            row.Fax = Fax;
            row.HeadRoleID = HeadRoleID;
            row.Type = Type;
            row.Status = Status;
            row.DeputyEmail = DeputyEmail;
            row.Deputy2Email = Deputy2Email;
            row.UpdateDate = DateTime.Now;
            if (row.InsertDate == null)
            {
                row.InsertDate = DateTime.Now;
            }
        }
        context.SaveChanges();
    }
    private string GetDeptNameString(string Type, string Name)
    {
        if (Type == DepartmentType.Department.ToString())
        {
            return "Department of " + Name;
        }
        else if (Type == DepartmentType.College.ToString())
        {
            if (Name == "KFUPM Business School")
            {
                return Name;
            }
            else
            {
                return "College of " + Name;
            }
        }
        else
        {
            return Name;
        }
    }
    private string GetDeptNameString(int ID)
    {
        string Name = GetDepartmentByPK(ID).Name;
        string Type = GetDepartmentByPK(ID).Type;
        if (Type == DepartmentType.Department.ToString())
        {
            return "Department of " + Name;
        }
        else if (Type == DepartmentType.College.ToString())
        {
            if (Name == "KFUPM Business School")
            {
                return Name;
            }
            else
            {
                return "College of " + Name;
            }
        }
        else
        {
            return Name;
        }
    }
    private string GetDeptHeadString(string HeadEmpID, string Type, string Name)
    {
        if (Type == DepartmentType.Department.ToString())
        {
            return GetEmployeeByPK(HeadEmpID)[0].NameString + " Chairman, Department of " + Name;
        }
        else if (Type == DepartmentType.College.ToString())
        {
            if (Name == "KFUPM Business School")
            {
                return GetEmployeeByPK(HeadEmpID)[0].NameString + " Dean, " + Name; ;
            }
            else
            {
                return GetEmployeeByPK(HeadEmpID)[0].NameString + " Dean, College of " + Name;
            }
        }
        else if (Name == "Faculty Affairs")
        {
            return GetEmployeeByPK(HeadEmpID)[0].NameString + ", Dean, " + Name;
        }
        else
        {
            return GetEmployeeByPK(HeadEmpID)[0].NameString + ", " + Name;
        }

    }

    public int InsertDepartment(string Name, string ShortName, int ParentDeptID, string HeadEmpID, string Phone, string Website, string Fax,
        byte HeadRoleID, string Type, string Status, string DeputyEmail, string Deputy2Email)
    {
        var context = new FPSDBEntities();
        Department d = new Department
        {
            Name = Name,
            ShortName = ShortName,
            ParentDeptID = ParentDeptID,
            HeadEmpID = HeadEmpID,
            Phone = Phone,
            Fax = Fax,
            HeadRoleID = HeadRoleID,
            Type = Type,
            Status = Status,
            DeputyEmail = DeputyEmail,
            Deputy2Email = Deputy2Email
        };

        context.Departments.Add(d);
        context.SaveChanges();
        return d.DepartmentID;
    }
    #endregion
    #endregion
    /**
     *      
     *   Old System 
     */

    public void DeleteForm_Checklist(int appID)
    {
        var context = new FPSDBEntities();
        var query = from a in context.Form_Checklist
                    where
                      a.ApplicationID == appID

                    select a;
        foreach (var row in query)
        {
            context.Form_Checklist.Remove(row);
        }
        context.SaveChanges();
    }



}

