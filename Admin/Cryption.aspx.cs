using System;

public partial class Admin_Cryption : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BAL bal = new BAL();
        //  Response.Write(Cryptography.Decrypt(Server.UrlDecode("1W%2ft%2f4To47hEO9aaD7%2bE6g%3d%3d")));
        // Response.Write(Cryptography.Decrypt("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"));
        // Response.Write(Cryptography.Decrypt("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"));
        //Response.Write(Cryptography.Encrypt("shomaail"));
        string s = @"سعادة الدكتور/محمود محمد محمد الحنطور
إن كلية الدراسات المساندة والتطبيقية في جامعة الملك فهد للبترول والمعادن بمدينة الظهران بالمملكة العربية السعودية تنظر حاليا في الطلب المقدم من أحد أعضاء هيئة التدريس بها وهو سعادة الدكتور/Khalid Hassan Alabri, 
من أجل الحصول على الترقية إلى درجة 
Professor 
بقسم الدراسات الإسلامية و العربية، وتخصص سعادته هو
 Islamic and Arabic Studies Dawaa and Hesba الدعوة والاحتساب. هذا وتستلزم السياسات المتبعة بالجامعة الحصول على تقييم خارجي للإسهامات البحثية و المهنية لكل مرشح للترقية تجريه هيئات معتبرة من خارج الجامعة من أجل استكمال السجل المؤسسي وإمداد لجنة التقييم برأي مستقل. ونشكر لكم تأكيدكم إذا رغبتم في تقديم تقييمكم للمرشح خلال أربعة أسابيع من استلام المواد العلمية، حيث أن ذلك سيساعد اللجنة كثيرا في سرعة الانتهاء من عملها.
هذا ووفقا للوائح الجامعة يتم منحكم مكافأة فخرية تبلغ 400 دولارا أمريكيا عقب تسليمكم لتقرير التقييم المعد من قبلكم، لذا نرجو من سعادتكم إبداء رغبتكم بالمشاركة في التقييم وذلك عن طريق النقر على زر القبول الموجود بالصفحة الإلكترونية التالية:
http://www.kfupm.edu.sa/FacultyPromotionSystem/ExtForms/ShowWillingnessExtReviewer.aspx?refreeID=
وبمجرد استلام تأكيدكم سنزود سعادتكم باسم مستخدم وكلمة سر تستطيع من خلالهما الدخول إلى نظام ترقية أعضاء هيئة التدريس الإلكتروني لإبداء الرأي والتقييم. كما أننا سنزود سعادتكم عن طريق البريد الإلكتروني بإرشادات ترقية أعضاء هيئة التدريس بالجامعة، ونموذج تقييم الأداء البحثي، ونسخ من القائمة الكاملة للأبحاث المنشورة والأبحاث التي اختار سعادة الدكتور/ Khalid Hassan Alabriالمرشح للترقية تقديمها.


وتفضلوا بقبول وافر التحية والتقدير



                                                                           الدكتور ناصر العقيلي
وكيل الجامعة للدراسات العلیا  والبحث العلمي
                                                                          جامعة الملك فهد للبترول والمعادن
";
        // Response.Write("the encryption of k461K: " + Cryptography.Encrypt("k461K"));
        //Response.Write("\n");
        //Response.Write(Cryptography.Decrypt("bKX31jEvC5tpBNZklUd9NA=="));

        //  Response.Write(Cryptography.Decrypt("9bWsjxCs8Fn2zNlganT54+wCANa8nauC+cXnTaKLV8kN02UkukifKZ+AEK6yujZ0SGYe8TGLB11TYerxnoQ858noN3uGY1paHlCqmAD++GmjkXnaocRWe0Ur8dudqThBH0BmvsWG97AIE3Lf/4t9BOfGNMsGdJ9rK3XQOKlNPOokYbMW+dxoihvO5cNm3ZVKYjWDswYxPB7JNuo5wyfSSwDYsSGdfQd4Bkzq1nlu5q3jBTkFluivdk4l/u0w6MNZwugThQu3mzRDyR/+4BBsOdmV/9yUJWs+gz5RVcwHXojJjkPR7LQ2qa6ofXmFTCtyl8mQvR25pob/avBJiNqoJph39vjh5h/pdgip5KhqI6D8h8nuFOFHWhFWJVSz79NqMpuAXAouJem5AA978AJd11OH50f9NOQa0Qq/LX3FWn1/tExTUXAr2WdQZUVCOWDK/5FSICdgrHD4j1/wpmc3zjN4uLOJYhz0vggv5jnAKSvx/8mAhDHHaDFoi1vVlEqT4i8vhITYDof3Ww1S5+QVDUYu3z6PgxGNRgoWg9RNNjIRQrq+Q50wzGzICyaQ7LrtRkZss1IGU6Jw4amYLhwEfQmtXHsxAF+jYzjtCcxSg5n5YM73+9aCZySST8F3tiLwHJN1oxDL17/BTG79rV6fHgJa9JIPwkW66bhmoQwsIyAR2sUnWJFHcSc6s5Qrr+DtTPOH6ERAdAqMA1Vlbo661qvPizlL5KvpekbDWOmFwfTYeWuoB6+VFq9JfL2PvQFUw7di1xL9V8PAqDGp7afBmt8qNF1FXOJhK+nuf7APzvBm+3h0+vdcf6DhDxs0CA6g2h2XHHFQrqO0TYYXGD16zV/7AEKrNMOIhl3AaqjSOSW5UCw+TcMpmq/dJSudOF3QztwqtXZ6YLdCT5vfroFsFiBehw4vUV+LfSR8Gnk3DNDRajje+r2thMbmicznDyRddzhwZvr8obiABK6lQYKrv2aLCFsxB6O+acNTAQA0++jaoMl/rARrZf1J0HTWD0EmG8Y/GsPUu3VHppPTDP2gYIAm56Ck4PckrwWxfUAiy9E7CCQIXVZpppGVQwLyN45uPSvPyM97s2KXO6pBO5JlInCpzGxwRN7b3M4zyuagYwdYj1uVZZCUrVnKfHl94Q0l"));
        /*  Response.Write(Cryptography.Encrypt(@"Dear Dr. Abdulaziz Obaid Al-Kaabi, Dean, College of Petroleum Engineering and Geosciences

  Dr. Al-Shuhail has updated the reviewers list with members from outside KFUPM only. A promotion application for Dr. Abdullatif Abdulrahman Al-Shuhail, a faculty member in Earth Sciences department has been received. The request has been discussed in the department council in its meeting #[xxxx] on [Date] and has been approved. You are kindly requested to take appropriate actions.

  Thanks
  Dr. Abdulaziz Bin Muhareb Al-Shaibani,
  Chairman,Earth Sciences Department"));*/
        //bal.UpdateFinalRefreeEncryptPassword(362);
        //  bal.UpdateFinalRefreeDecryptPassword(362);

        //bal.UpdateSentEmailEncryptBody(1702);
        //bal.UpdateSentEmailDecryptBody(1702);

        //   bal.UpdateAppTaskLgEncryptMessage(14503);
        //  bal.UpdateAppTaskLgDecryptMessage(14503);

        //    bal.UpdateAppTaskLgExtEncryptMessage(13039);
        //bal.UpdateAppTaskLgExtDecryptMessage(13039);

        //bal.UpdateFinalRefreeEncryptPassword();
        ////   bal.UpdateFinalRefreeDecryptPassword();

        //bal.UpdateSentEmailEncryptBody();
        ////bal.UpdateSentEmailDecryptBody();

        //bal.UpdateAppTaskLgEncryptMessage();
        ////bal.UpdateAppTaskLgDecryptMessage();

        //   bal.UpdateAppTaskLgExtEncryptMessage();
        //bal.UpdateAppTaskLgExtDecryptMessage();
    }

    protected void btnEncrypt_Click(object sender, EventArgs e)
    {
        tbResult.Value = Cryptography.Encrypt(tbText.Value);
    }

    protected void btnDecrypt_Click(object sender, EventArgs e)
    {
        tbResult.Value = Cryptography.Decrypt(tbText.Value);
    }
}