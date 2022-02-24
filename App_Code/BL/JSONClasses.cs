
public class Rootobject
{
    public string FormTitle { get; set; }
    public Question[] Question { get; set; }
}

public class Question
{
    public string Title { get; set; }
    public string Text { get; set; }
    public string visible { get; set; }
    public string questionWidth { get; set; }
    public string AnswerOnSameLine { get; set; }
    public string AnswerInstruction { get; set; }
    public string Lang { get; set; }
    public Answerradiobutton AnswerRadioButton { get; set; }
    public Answercheckbox AnswerCheckBox { get; set; }
    public Answerdropdownlist AnswerDropDownList { get; set; }
    public Answerta AnswerTA { get; set; }
}

public class Answerradiobutton
{
    public string visibleRB { get; set; }
    public string titleRB { get; set; }
    public Optionrb[] optionRB { get; set; }
}

public class Optionrb
{
    public string text { get; set; }
    public string value { get; set; }
    public string _checked { get; set; }
}

public class Answercheckbox
{
    public string visibleCB { get; set; }
    public string titleCB { get; set; }
    public Optioncb[] optionCB { get; set; }
}

public class Optioncb
{
    public string text { get; set; }
    public string value { get; set; }
    public string _checked { get; set; }
}

public class Answerdropdownlist
{
    public string visibleDDl { get; set; }
    public string required { get; set; }
    public string titleDDL { get; set; }
    public Optionddl[] optionDDL { get; set; }
}

public class Optionddl
{
    public string text { get; set; }
    public string value { get; set; }
}

public class Answerta
{
    public string visibleTA { get; set; }
    public string required { get; set; }
    public string titleTA { get; set; }
    public string watermark { get; set; }
    public string tooltip { get; set; }
    public string maxlength { get; set; }
    public string minlength { get; set; }
    public string height { get; set; }
    public string width { get; set; }
}
