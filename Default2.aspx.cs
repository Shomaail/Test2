using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    BAL bal = new BAL();
    protected void Page_PreRender(object sender, EventArgs e)
    {
        
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        txtCountedTotal.Attributes.Add("readonly", "readonly");
        txtAuthoshipTotal.Attributes.Add("readonly", "readonly");
        txtCountedCa.Attributes.Add("readonly", "readonly");


        if (Visible == false) return;
        //Page.Form.Attributes["onload"] = "javascript: " + this.UniqueID + "_addTxt();";
        //   Page.Form.Attributes["onload"] = "javascript: " + this.UniqueID + "_addTxt();";
        txtCountedTotal.Attributes.Add("readonly", "readonly");
        txtAuthoshipTotal.Attributes.Add("readonly", "readonly");
        txtCountedCa.Attributes.Add("readonly", "readonly");
        Page.ClientScript.RegisterStartupScript(GetType(), UniqueID + "_startup", UniqueID + "_addTxt();", true);
        /* On Blur events */
        /* Row# 1 */
        txtR1Ca.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR1Cb.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR1Cc.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR1Cd.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR1Ce.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR1Cf.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR1Cg.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR1Ch.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR1Ci.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");

        /* Row# 2 */
        txtR2Ca.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR2Cb.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR2Cc.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR2Cd.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR2Ce.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR2Cf.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR2Cg.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR2Ch.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR2Ci.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");

        /* Row# 3 */
        txtR3Ca.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR3Cb.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR3Cc.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR3Cd.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR3Ce.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR3Cf.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR3Cg.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR3Ch.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR3Ci.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");

        /* Row# 4 */
        txtR4Ca.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR4Cb.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");
        txtR4Cc.Attributes.Add("onBlur", "JavaScript:UpdateTxt(this)");


        /* Count Events */
        /* Row# 1 */
        txtR1Ca.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR1Cb.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR1Cc.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR1Cd.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR1Ce.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR1Cf.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR1Cg.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR1Ch.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR1Ci.Attributes.Add("onkeyup", UniqueID + "_addTxt()");


        /* Row# 2 */
        txtR2Ca.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR2Cb.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR2Cc.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR2Cd.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR2Ce.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR2Cf.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR2Cg.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR2Ch.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR2Ci.Attributes.Add("onkeyup", UniqueID + "_addTxt()");

        /* Row# 3 */
        txtR3Ca.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR3Cb.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR3Cc.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR3Cd.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR3Ce.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR3Cf.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR3Cg.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR3Ch.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR3Ci.Attributes.Add("onkeyup", UniqueID + "_addTxt()");

        /* Row# 4 */
        txtR4Ca.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR4Cb.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR4Cc.Attributes.Add("onkeyup", UniqueID + "_addTxt()");


        /* Row# 5 */
        txtR5Cd.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR5Ce.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR5Cf.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR5Cg.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR5Ch.Attributes.Add("onkeyup", UniqueID + "_addTxt()");
        txtR5Ci.Attributes.Add("onkeyup", UniqueID + "_addTxt()");




        Page.ClientScript.RegisterClientScriptBlock(GetType(), "Points" + UniqueID, @"

        <script language=""javascript"">
            function " + UniqueID + @"_addTxt()
            {
                /* " + UniqueID + @" */
                with(document." + Page.Form.ClientID + @")
                {

                /* Row# 1 */
                " + txtR1CaU.ClientID + @".value = isNaN(parseFloat(" + txtR1Ca.ClientID + @".value))?0:parseFloat(" + txtR1Ca.ClientID + @".value)* 1;
                " + txtR1CbU.ClientID + @".value = isNaN(parseFloat(" + txtR1Cb.ClientID + @".value))?0:parseFloat(" + txtR1Cb.ClientID + @".value)* 1;
                " + txtR1CcU.ClientID + @".value = isNaN(parseFloat(" + txtR1Cc.ClientID + @".value))?0:parseFloat(" + txtR1Cc.ClientID + @".value)* 1;
                " + txtR1CdU.ClientID + @".value = isNaN(parseFloat(" + txtR1Cd.ClientID + @".value))?0:parseFloat(" + txtR1Cd.ClientID + @".value)* 1;
                " + txtR1CeU.ClientID + @".value = isNaN(parseFloat(" + txtR1Ce.ClientID + @".value))?0:parseFloat(" + txtR1Ce.ClientID + @".value)* 1;
                " + txtR1CfU.ClientID + @".value = isNaN(parseFloat(" + txtR1Cf.ClientID + @".value))?0:parseFloat(" + txtR1Cf.ClientID + @".value)* 1;
                " + txtR1CgU.ClientID + @".value = isNaN(parseFloat(" + txtR1Cg.ClientID + @".value))?0:parseFloat(" + txtR1Cg.ClientID + @".value)* 1;
                " + txtR1ChU.ClientID + @".value = isNaN(parseFloat(" + txtR1Ch.ClientID + @".value))?0:parseFloat(" + txtR1Ch.ClientID + @".value)* 1;
                " + txtR1CiU.ClientID + @".value = isNaN(parseFloat(" + txtR1Ci.ClientID + @".value))?0:parseFloat(" + txtR1Ci.ClientID + @".value)* 1;

                /* Single Author - Units Counted */
                var x;
                x = parseFloat(" + txtR1CaU.ClientID + @".value);   " + R1CaCounted.ClientID + @".value = isNaN(x)?0:x;
                x = parseFloat(" + txtR1CbU.ClientID + @".value);   " + R1CbCounted.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);
                x = parseFloat(" + txtR1CcU.ClientID + @".value);   " + R1CcCounted.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);
                x = parseFloat(" + txtR1CdU.ClientID + @".value);   " + R1CdCounted.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);
                x = parseFloat(" + txtR1CeU.ClientID + @".value);   " + R1CeCounted.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);
                x = parseFloat(" + txtR1CfU.ClientID + @".value);   " + R1CfCounted.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);
                x = parseFloat(" + txtR1CgU.ClientID + @".value);   " + R1CgCounted.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);
                x = parseFloat(" + txtR1ChU.ClientID + @".value);   " + R1ChCounted.ClientID + @".value = isNaN(x)?0:x;
                x = parseFloat(" + txtR1CiU.ClientID + @".value);   " + R1CiCounted.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);


                /* Row# 2 */
                " + txtR2CaU.ClientID + @".value = isNaN(parseFloat(" + txtR2Ca.ClientID + @".value))?0:parseFloat(" + txtR2Ca.ClientID + @".value)* 0.5;
                " + txtR2CbU.ClientID + @".value = isNaN(parseFloat(" + txtR2Cb.ClientID + @".value))?0:parseFloat(" + txtR2Cb.ClientID + @".value)* 0.5;
                " + txtR2CcU.ClientID + @".value = isNaN(parseFloat(" + txtR2Cc.ClientID + @".value))?0:parseFloat(" + txtR2Cc.ClientID + @".value)* 0.5;
                " + txtR2CdU.ClientID + @".value = isNaN(parseFloat(" + txtR2Cd.ClientID + @".value))?0:parseFloat(" + txtR2Cd.ClientID + @".value)* 0.5;
                " + txtR2CeU.ClientID + @".value = isNaN(parseFloat(" + txtR2Ce.ClientID + @".value))?0:parseFloat(" + txtR2Ce.ClientID + @".value)* 0.5;
                " + txtR2CfU.ClientID + @".value = isNaN(parseFloat(" + txtR2Cf.ClientID + @".value))?0:parseFloat(" + txtR2Cf.ClientID + @".value)* 0.5;
                " + txtR2CgU.ClientID + @".value = isNaN(parseFloat(" + txtR2Cg.ClientID + @".value))?0:parseFloat(" + txtR2Cg.ClientID + @".value)* 0.5;
                " + txtR2ChU.ClientID + @".value = isNaN(parseFloat(" + txtR2Ch.ClientID + @".value))?0:parseFloat(" + txtR2Ch.ClientID + @".value)* 0.5;
                " + txtR2CiU.ClientID + @".value = isNaN(parseFloat(" + txtR2Ci.ClientID + @".value))?0:parseFloat(" + txtR2Ci.ClientID + @".value)* 0.5;


                /* Row# 3 */
                " + txtR3CaU.ClientID + @".value = isNaN(parseFloat(" + txtR3Ca.ClientID + @".value))?0:parseFloat(" + txtR3Ca.ClientID + @".value)* 0.5;
                " + txtR3CbU.ClientID + @".value = isNaN(parseFloat(" + txtR3Cb.ClientID + @".value))?0:parseFloat(" + txtR3Cb.ClientID + @".value)* 0.5;
                " + txtR3CcU.ClientID + @".value = isNaN(parseFloat(" + txtR3Cc.ClientID + @".value))?0:parseFloat(" + txtR3Cc.ClientID + @".value)* 0.5;
                " + txtR3CdU.ClientID + @".value = isNaN(parseFloat(" + txtR3Cd.ClientID + @".value))?0:parseFloat(" + txtR3Cd.ClientID + @".value)* 0.5;
                " + txtR3CeU.ClientID + @".value = isNaN(parseFloat(" + txtR3Ce.ClientID + @".value))?0:parseFloat(" + txtR3Ce.ClientID + @".value)* 0.5;
                " + txtR3CfU.ClientID + @".value = isNaN(parseFloat(" + txtR3Cf.ClientID + @".value))?0:parseFloat(" + txtR3Cf.ClientID + @".value)* 0.5;
                " + txtR3CgU.ClientID + @".value = isNaN(parseFloat(" + txtR3Cg.ClientID + @".value))?0:parseFloat(" + txtR3Cg.ClientID + @".value)* 0.5;
                " + txtR3ChU.ClientID + @".value = isNaN(parseFloat(" + txtR3Ch.ClientID + @".value))?0:parseFloat(" + txtR3Ch.ClientID + @".value)* 0.5;
                " + txtR3CiU.ClientID + @".value = isNaN(parseFloat(" + txtR3Ci.ClientID + @".value))?0:parseFloat(" + txtR3Ci.ClientID + @".value)* 0.5;


                /* Row# 4 */
                " + txtR4CaU.ClientID + @".value = isNaN(parseFloat(" + txtR4Ca.ClientID + @".value))?0:parseFloat(" + txtR4Ca.ClientID + @".value)* 0.25;
                " + txtR4CbU.ClientID + @".value = isNaN(parseFloat(" + txtR4Cb.ClientID + @".value))?0:parseFloat(" + txtR4Cb.ClientID + @".value)* 0.25;
                " + txtR4CcU.ClientID + @".value = isNaN(parseFloat(" + txtR4Cc.ClientID + @".value))?0:parseFloat(" + txtR4Cc.ClientID + @".value)* 0.25;


                /* Row# 5 */
                " + txtR5CdU.ClientID + @".value = isNaN(parseFloat(" + txtR5Cd.ClientID + @".value))?0:parseFloat(" + txtR5Cd.ClientID + @".value)* 0.25;
                " + txtR5CeU.ClientID + @".value = isNaN(parseFloat(" + txtR5Ce.ClientID + @".value))?0:parseFloat(" + txtR5Ce.ClientID + @".value)* 0.25;
                " + txtR5CfU.ClientID + @".value = isNaN(parseFloat(" + txtR5Cf.ClientID + @".value))?0:parseFloat(" + txtR5Cf.ClientID + @".value)* 0.25;
                " + txtR5CgU.ClientID + @".value = isNaN(parseFloat(" + txtR5Cg.ClientID + @".value))?0:parseFloat(" + txtR5Cg.ClientID + @".value)* 0.25;
                " + txtR5ChU.ClientID + @".value = isNaN(parseFloat(" + txtR5Ch.ClientID + @".value))?0:parseFloat(" + txtR5Ch.ClientID + @".value)* 0.25;
                " + txtR5CiU.ClientID + @".value = isNaN(parseFloat(" + txtR5Ci.ClientID + @".value))?0:parseFloat(" + txtR5Ci.ClientID + @".value)* 0.25;





                /* Total Units */
                x = parseFloat(" + txtR1CaU.ClientID + @".value)
                  + parseFloat(" + txtR2CaU.ClientID + @".value)
                  + parseFloat(" + txtR3CaU.ClientID + @".value)
                  + parseFloat(" + txtR4CaU.ClientID + @".value);
                " + txtTotalCa.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1CbU.ClientID + @".value)
                  + parseFloat(" + txtR2CbU.ClientID + @".value)
                  + parseFloat(" + txtR3CbU.ClientID + @".value)
                  + parseFloat(" + txtR4CbU.ClientID + @".value);
                " + txtTotalCb.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1CcU.ClientID + @".value)
                  + parseFloat(" + txtR2CcU.ClientID + @".value)
                  + parseFloat(" + txtR3CcU.ClientID + @".value)
                  + parseFloat(" + txtR4CcU.ClientID + @".value);
                " + txtTotalCc.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1CdU.ClientID + @".value)
                  + parseFloat(" + txtR2CdU.ClientID + @".value)
                  + parseFloat(" + txtR3CdU.ClientID + @".value)
                  + parseFloat(" + txtR5CdU.ClientID + @".value);
                " + txtTotalCd.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1CeU.ClientID + @".value)
                  + parseFloat(" + txtR2CeU.ClientID + @".value)
                  + parseFloat(" + txtR3CeU.ClientID + @".value)
                  + parseFloat(" + txtR5CeU.ClientID + @".value);
                " + txtTotalCe.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1CfU.ClientID + @".value)
                  + parseFloat(" + txtR2CfU.ClientID + @".value)
                  + parseFloat(" + txtR3CfU.ClientID + @".value)
                  + parseFloat(" + txtR5CfU.ClientID + @".value);
                " + txtTotalCf.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1CgU.ClientID + @".value)
                  + parseFloat(" + txtR2CgU.ClientID + @".value)
                  + parseFloat(" + txtR5CgU.ClientID + @".value);
                " + txtTotalCg.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1ChU.ClientID + @".value)
                  + parseFloat(" + txtR2ChU.ClientID + @".value)
                  + parseFloat(" + txtR3CfU.ClientID + @".value)
                  + parseFloat(" + txtR5ChU.ClientID + @".value);
                " + txtTotalCh.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1CiU.ClientID + @".value)
                  + parseFloat(" + txtR2CiU.ClientID + @".value)
                  + parseFloat(" + txtR3CiU.ClientID + @".value)
                  + parseFloat(" + txtR5CiU.ClientID + @".value);
                " + txtTotalCi.ClientID + @".value = isNaN(x)?0:x;




                /* Total Units Counted */
                x = parseFloat(" + txtR1CaU.ClientID + @".value)
                  + parseFloat(" + txtR2CaU.ClientID + @".value)
                  + parseFloat(" + txtR3CaU.ClientID + @".value)
                  + parseFloat(" + txtR4CaU.ClientID + @".value);
                " + txtCountedCa.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1CbU.ClientID + @".value)
                  + parseFloat(" + txtR2CbU.ClientID + @".value)
                  + parseFloat(" + txtR3CbU.ClientID + @".value)
                  + parseFloat(" + txtR4CbU.ClientID + @".value);
                " + txtCountedCb.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);

                x = parseFloat(" + txtR1CcU.ClientID + @".value)
                  + parseFloat(" + txtR2CcU.ClientID + @".value)
                  + parseFloat(" + txtR3CcU.ClientID + @".value)
                  + parseFloat(" + txtR4CcU.ClientID + @".value);
                " + txtCountedCc.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);

                x = parseFloat(" + txtR1CdU.ClientID + @".value)
                  + parseFloat(" + txtR2CdU.ClientID + @".value)
                  + parseFloat(" + txtR3CdU.ClientID + @".value)
                  + parseFloat(" + txtR5CdU.ClientID + @".value);
                " + txtCountedCd.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);

                x = parseFloat(" + txtR1CeU.ClientID + @".value)
                  + parseFloat(" + txtR2CeU.ClientID + @".value)
                  + parseFloat(" + txtR3CeU.ClientID + @".value)
                  + parseFloat(" + txtR5CeU.ClientID + @".value);
                " + txtCountedCe.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);

                x = parseFloat(" + txtR1CfU.ClientID + @".value)
                  + parseFloat(" + txtR2CfU.ClientID + @".value)
                  + parseFloat(" + txtR3CfU.ClientID + @".value)
                  + parseFloat(" + txtR5CfU.ClientID + @".value);
                " + txtCountedCf.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);

                x = parseFloat(" + txtR1CgU.ClientID + @".value)
                  + parseFloat(" + txtR2CgU.ClientID + @".value)
                  + parseFloat(" + txtR3CgU.ClientID + @".value)
                  + parseFloat(" + txtR5CgU.ClientID + @".value);
                " + txtCountedCg.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);

                x = parseFloat(" + txtR1ChU.ClientID + @".value)
                  + parseFloat(" + txtR2ChU.ClientID + @".value)
                  + parseFloat(" + txtR3ChU.ClientID + @".value)
                  + parseFloat(" + txtR5ChU.ClientID + @".value);
                " + txtCountedCh.ClientID + @".value = isNaN(x)?0:x;

                x = parseFloat(" + txtR1CiU.ClientID + @".value)
                  + parseFloat(" + txtR2CiU.ClientID + @".value)
                  + parseFloat(" + txtR3CiU.ClientID + @".value)
                  + parseFloat(" + txtR5CiU.ClientID + @".value);
                " + txtCountedCi.ClientID + @".value = isNaN(x)?0:(x > 1? 1 : x);








                /* Totals */
                x = parseFloat(" + R1CaCounted.ClientID + @".value)
                  + parseFloat(" + R1CbCounted.ClientID + @".value)
                  + parseFloat(" + R1CcCounted.ClientID + @".value)
                  + parseFloat(" + R1CdCounted.ClientID + @".value)
                  + parseFloat(" + R1CeCounted.ClientID + @".value)
                  + parseFloat(" + R1CfCounted.ClientID + @".value)
                  + parseFloat(" + R1CgCounted.ClientID + @".value)
                  + parseFloat(" + R1ChCounted.ClientID + @".value)
                  + parseFloat(" + R1CiCounted.ClientID + @".value);
                " + txtAuthoshipTotal.ClientID + @".value = isNaN(x)?0:x;


                x = parseFloat(" + txtCountedCa.ClientID + @".value)
                  + parseFloat(" + txtCountedCb.ClientID + @".value)
                  + parseFloat(" + txtCountedCc.ClientID + @".value)
                  + parseFloat(" + txtCountedCd.ClientID + @".value)
                  + parseFloat(" + txtCountedCe.ClientID + @".value)
                  + parseFloat(" + txtCountedCf.ClientID + @".value)
                  + parseFloat(" + txtCountedCg.ClientID + @".value)
                  + parseFloat(" + txtCountedCh.ClientID + @".value)
                  + parseFloat(" + txtCountedCi.ClientID + @".value);
                " + txtCountedTotal.ClientID + @".value = isNaN(x)?0:x;

                }
            }
        </script>
                    ");



        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "UpdateText", @"

        <script language=""javascript"">
            function UpdateTxt(obj)
            {  
                 if (obj.value.length==0)
                    obj.style.backgroundColor='yellow';
                 else
                    obj.style.backgroundColor='White';

            }
        </script>
                    ");


    }

    public void SetReadOnly(bool status)
    {
        /* Row# 1 */
        txtR1Ca.ReadOnly = status;
        txtR1Cb.ReadOnly = status;
        txtR1Cc.ReadOnly = status;
        txtR1Cd.ReadOnly = status;
        txtR1Ce.ReadOnly = status;
        txtR1Cf.ReadOnly = status;
        txtR1Cg.ReadOnly = status;
        txtR1Ch.ReadOnly = status;
        txtR1Ci.ReadOnly = status;

        /* Row# 2 */
        txtR2Ca.ReadOnly = status;
        txtR2Cb.ReadOnly = status;
        txtR2Cc.ReadOnly = status;
        txtR2Cd.ReadOnly = status;
        txtR2Ce.ReadOnly = status;
        txtR2Cf.ReadOnly = status;
        txtR2Cg.ReadOnly = status;
        txtR2Ch.ReadOnly = status;
        txtR2Ci.ReadOnly = status;

        /* Row# 3 */
        txtR3Ca.ReadOnly = status;
        txtR3Cb.ReadOnly = status;
        txtR3Cc.ReadOnly = status;
        txtR3Cd.ReadOnly = status;
        txtR3Ce.ReadOnly = status;
        txtR3Cf.ReadOnly = status;
        txtR3Cg.ReadOnly = status;
        txtR3Ch.ReadOnly = status;
        txtR3Ci.ReadOnly = status;

        /* Row# 4 */
        txtR4Ca.ReadOnly = status;
        txtR4Cb.ReadOnly = status;
        txtR4Cc.ReadOnly = status;

        /* Row# 5 */
        txtR5Cd.ReadOnly = status;
        txtR5Ce.ReadOnly = status;
        txtR5Cf.ReadOnly = status;
        txtR5Cg.ReadOnly = status;
        txtR5Ch.ReadOnly = status;
        txtR5Ci.ReadOnly = status;
    }

    public void SetTabIndex(short start)
    {
        /* Tab Indexes */
        /* Row# 1 */
        txtR1Ca.TabIndex = (short)(start + 1);
        txtR1Cb.TabIndex = (short)(start + 2);
        txtR1Cc.TabIndex = (short)(start + 3);
        txtR1Cd.TabIndex = (short)(start + 4);
        txtR1Ce.TabIndex = (short)(start + 5);
        txtR1Cf.TabIndex = (short)(start + 6);
        txtR1Cg.TabIndex = (short)(start + 7);
        txtR1Ch.TabIndex = (short)(start + 8);
        txtR1Ci.TabIndex = (short)(start + 9);

        /* Row# 2 */
        txtR2Ca.TabIndex = (short)(start + 10);
        txtR2Cb.TabIndex = (short)(start + 11);
        txtR2Cc.TabIndex = (short)(start + 12);
        txtR2Cd.TabIndex = (short)(start + 13);
        txtR2Ce.TabIndex = (short)(start + 14);
        txtR2Cf.TabIndex = (short)(start + 15);
        txtR2Cg.TabIndex = (short)(start + 16);
        txtR2Ch.TabIndex = (short)(start + 17);
        txtR2Ci.TabIndex = (short)(start + 18);

        /* Row# 3 */
        txtR3Ca.TabIndex = (short)(start + 19);
        txtR3Cb.TabIndex = (short)(start + 20);
        txtR3Cc.TabIndex = (short)(start + 21); ;
        txtR3Cd.TabIndex = (short)(start + 22);
        txtR3Ce.TabIndex = (short)(start + 23);
        txtR3Cf.TabIndex = (short)(start + 24);
        txtR3Cg.TabIndex = (short)(start + 25);
        txtR3Ch.TabIndex = (short)(start + 26);
        txtR3Ci.TabIndex = (short)(start + 27);

        /* Row# 4 */
        txtR4Ca.TabIndex = (short)(start + 28);
        txtR4Cb.TabIndex = (short)(start + 29);
        txtR4Cc.TabIndex = (short)(start + 30);


        /* Row# 5 */
        txtR5Cd.TabIndex = (short)(start + 30);
        txtR5Ce.TabIndex = (short)(start + 31);
        txtR5Cf.TabIndex = (short)(start + 32);
        txtR5Cg.TabIndex = (short)(start + 33);
        txtR5Ch.TabIndex = (short)(start + 34);
        txtR5Ci.TabIndex = (short)(start + 35);

    }

    public bool LoadPoints(int applicationID, string type)
    {
        //List<Form_Points> lfp = bal.GetPoints(applicationID, type);
        //if (lfp.Count == 0)
        //{
        //    return false;
        //}
        //Form_Points row = lfp[0];
        PromotionTableAdapters.PointsTableAdapter adapter = new PromotionTableAdapters.PointsTableAdapter();

        Promotion.PointsDataTable table = adapter.GetPointsByApplication(applicationID, type);
        if (table.Count == 0) return false;

        Promotion.PointsRow row = table[0];

        /* Row# 1 */
        txtR1Ca.Text = row.P1a.ToString();
        txtR1Cb.Text = row.P1b.ToString();
        txtR1Cc.Text = row.P1c.ToString();
        txtR1Cd.Text = row.P1d.ToString();
        txtR1Ce.Text = row.P1e.ToString();
        txtR1Cf.Text = row.P1f.ToString();
        txtR1Cg.Text = row.P1g.ToString();
        txtR1Ch.Text = row.P1h.ToString();
        txtR1Ci.Text = row.P1i.ToString();

        /* Row# 2 */
        txtR2Ca.Text = row.P2a.ToString();
        txtR2Cb.Text = row.P2b.ToString();
        txtR2Cc.Text = row.P2c.ToString();
        txtR2Cd.Text = row.P2d.ToString();
        txtR2Ce.Text = row.P2e.ToString();
        txtR2Cf.Text = row.P2f.ToString();
        txtR2Cg.Text = row.P2g.ToString();
        txtR2Ch.Text = row.P2h.ToString();
        txtR2Ci.Text = row.P2i.ToString();

        /* Row# 3 */
        txtR3Ca.Text = row.P3a.ToString();
        txtR3Cb.Text = row.P3b.ToString();
        txtR3Cc.Text = row.P3c.ToString();
        txtR3Cd.Text = row.P3d.ToString();
        txtR3Ce.Text = row.P3e.ToString();
        txtR3Cf.Text = row.P3f.ToString();
        txtR3Cg.Text = row.P3g.ToString();
        txtR3Ch.Text = row.P3h.ToString();
        txtR3Ci.Text = row.P3i.ToString();

        /* Row# 4 */
        txtR4Ca.Text = row.P4a.ToString();
        txtR4Cb.Text = row.P4b.ToString();
        txtR4Cc.Text = row.P4c.ToString();

        /* Row# 5 */
        txtR5Cd.Text = row.P5d.ToString();
        txtR5Ce.Text = row.P5e.ToString();
        txtR5Cf.Text = row.P5f.ToString();
        txtR5Cg.Text = row.P5g.ToString();
        txtR5Ch.Text = row.P5h.ToString();
        txtR5Ci.Text = row.P5i.ToString();

        return true;
    }

    public bool SavePoints(int applicationID, string type)
    {
        //bal.DeletePoints(applicationID, type);
        //bal.InsertPoints(
        //applicationID,
        //type,
        //txtR1Ca.Text.Length == 0 ? (byte)0 : byte.Parse(txtR1Ca.Text),
        //txtR1Cb.Text.Length == 0 ? (byte)0 : byte.Parse(txtR1Cb.Text),
        //txtR1Cc.Text.Length == 0 ? (byte)0 : byte.Parse(txtR1Cc.Text),
        //txtR1Cd.Text.Length == 0 ? (byte)0 : byte.Parse(txtR1Cd.Text),
        //txtR1Ce.Text.Length == 0 ? (byte)0 : byte.Parse(txtR1Ce.Text),
        //txtR1Cf.Text.Length == 0 ? (byte)0 : byte.Parse(txtR1Cf.Text),
        //txtR1Cg.Text.Length == 0 ? (byte)0 : byte.Parse(txtR1Cg.Text),
        //txtR1Ch.Text.Length == 0 ? (byte)0 : byte.Parse(txtR1Ch.Text),
        //txtR1Ci.Text.Length == 0 ? (byte)0 : byte.Parse(txtR1Ci.Text),

        //txtR2Ca.Text.Length == 0 ? (byte)0 : byte.Parse(txtR2Ca.Text),
        //txtR2Cb.Text.Length == 0 ? (byte)0 : byte.Parse(txtR2Cb.Text),
        //txtR2Cc.Text.Length == 0 ? (byte)0 : byte.Parse(txtR2Cc.Text),
        //txtR2Cd.Text.Length == 0 ? (byte)0 : byte.Parse(txtR2Cd.Text),
        //txtR2Ce.Text.Length == 0 ? (byte)0 : byte.Parse(txtR2Ce.Text),
        //txtR2Cf.Text.Length == 0 ? (byte)0 : byte.Parse(txtR2Cf.Text),
        //txtR2Cg.Text.Length == 0 ? (byte)0 : byte.Parse(txtR2Cg.Text),
        //txtR2Ch.Text.Length == 0 ? (byte)0 : byte.Parse(txtR2Ch.Text),
        //txtR2Ci.Text.Length == 0 ? (byte)0 : byte.Parse(txtR2Ci.Text),

        //txtR3Ca.Text.Length == 0 ? (byte)0 : byte.Parse(txtR3Ca.Text),
        //txtR3Cb.Text.Length == 0 ? (byte)0 : byte.Parse(txtR3Cb.Text),
        //txtR3Cc.Text.Length == 0 ? (byte)0 : byte.Parse(txtR3Cc.Text),
        //txtR3Cd.Text.Length == 0 ? (byte)0 : byte.Parse(txtR3Cd.Text),
        //txtR3Ce.Text.Length == 0 ? (byte)0 : byte.Parse(txtR3Ce.Text),
        //txtR3Cf.Text.Length == 0 ? (byte)0 : byte.Parse(txtR3Cf.Text),
        //txtR3Cg.Text.Length == 0 ? (byte)0 : byte.Parse(txtR3Cg.Text),
        //txtR3Ch.Text.Length == 0 ? (byte)0 : byte.Parse(txtR3Ch.Text),
        //txtR3Ci.Text.Length == 0 ? (byte)0 : byte.Parse(txtR3Ci.Text),

        //txtR4Ca.Text.Length == 0 ? (byte)0 : byte.Parse(txtR4Ca.Text),
        //txtR4Cb.Text.Length == 0 ? (byte)0 : byte.Parse(txtR4Cb.Text),
        //txtR4Cc.Text.Length == 0 ? (byte)0 : byte.Parse(txtR4Cc.Text),

        //txtR5Cd.Text.Length == 0 ? (byte)0 : byte.Parse(txtR5Cd.Text),
        //txtR5Ce.Text.Length == 0 ? (byte)0 : byte.Parse(txtR5Ce.Text),
        //txtR5Cf.Text.Length == 0 ? (byte)0 : byte.Parse(txtR5Cf.Text),
        //txtR5Cg.Text.Length == 0 ? (byte)0 : byte.Parse(txtR5Cg.Text),
        //txtR5Ch.Text.Length == 0 ? (byte)0 : byte.Parse(txtR5Ch.Text),
        //txtR5Ci.Text.Length == 0 ? (byte)0 : byte.Parse(txtR5Ci.Text),
        ////total
        //txtCountedTotal.Text.Length == 0 ? 0 : decimal.Parse(txtCountedTotal.Text),
        ////sigle
        //txtAuthoshipTotal.Text.Length == 0 ? 0 : decimal.Parse(txtAuthoshipTotal.Text),
        ////journal
        //txtCountedCa.Text.Length == 0 ? 0 : decimal.Parse(txtCountedCa.Text)
        //);
        //if (bal.GetPoints(applicationID, type)[0].SumTotal == 0)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}
        return true;

    }



    public void ResetBoxes()
    {
        /* Row# 1 */
        txtR1Ca.Text = "0";
        txtR1Cb.Text = "0";
        txtR1Cc.Text = "0";
        txtR1Cd.Text = "0";
        txtR1Ce.Text = "0";
        txtR1Cf.Text = "0";
        txtR1Cg.Text = "0";
        txtR1Ch.Text = "0";
        txtR1Ci.Text = "0";

        /* Row# 2 */
        txtR2Ca.Text = "0";
        txtR2Cb.Text = "0";
        txtR2Cc.Text = "0";
        txtR2Cd.Text = "0";
        txtR2Ce.Text = "0";
        txtR2Cf.Text = "0";
        txtR2Cg.Text = "0";
        txtR2Ch.Text = "0";
        txtR2Ci.Text = "0";

        /* Row# 3 */
        txtR3Ca.Text = "0";
        txtR3Cb.Text = "0";
        txtR3Cc.Text = "0";
        txtR3Cd.Text = "0";
        txtR3Ce.Text = "0";
        txtR3Cf.Text = "0";
        txtR3Cg.Text = "0";
        txtR3Ch.Text = "0";
        txtR3Ci.Text = "0";

        /* Row# 4 */
        txtR4Ca.Text = "0";
        txtR4Cb.Text = "0";
        txtR4Cc.Text = "0";

        /* Row# 5 */
        txtR5Cd.Text = "0";
        txtR5Ce.Text = "0";
        txtR5Cf.Text = "0";
        txtR5Cg.Text = "0";
        txtR5Ch.Text = "0";
        txtR5Ci.Text = "0";

    }

}