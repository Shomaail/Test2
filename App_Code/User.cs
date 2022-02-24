using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>


    public class User
    {
        public Inner_data[] data { get; set; }
    }

    public class Inner_data
    {
        public Inner_Array0 Array0 { get; set; }
    }

    public class Inner_Array0
    {
        public string username { get; set; }
        public string kfupm_id { get; set; }
        public object title { get; set; }
        public string name_en { get; set; }
        public string name_ar { get; set; }
        public string active_status { get; set; }
        public string department_id { get; set; }
        public string email { get; set; }
        public object mobile { get; set; }
        public string nationality { get; set; }
        public string type { get; set; }
        public object rank { get; set; }
    }
