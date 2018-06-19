using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SupportForm
{
    public partial class Results : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var questionnaireId = Request.QueryString["id"];
            textQuestionnaireId.InnerHtml = questionnaireId;
        }
    }
}