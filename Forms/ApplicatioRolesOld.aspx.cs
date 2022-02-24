using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_ApplicatioRoles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Instruction1.Text = Master.CurrentFormInstruction;
    }
}