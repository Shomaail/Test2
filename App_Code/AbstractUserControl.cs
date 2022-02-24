  using System;
  using System.Data;
  using System.Configuration;
  using System.Web;
  using System.Web.Security;
  using System.Web.UI;
  using System.Web.UI.HtmlControls;
  using System.Web.UI.WebControls;
  using System.Web.UI.WebControls.WebParts;
  
  
  /// <summary>
  /// Summary description for AbstractUserControl
  /// </summary>
  public abstract  class AbstractUserControl : System.Web.UI.UserControl 
  {
      public AbstractUserControl()
      {
          //
          // TODO: Add constructor logic here
          //
      }
  
      // DO concrete implementation in the user control
      public abstract void HideCallForMeetingControl();
    
  }