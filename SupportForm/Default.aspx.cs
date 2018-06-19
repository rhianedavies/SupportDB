using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.DynamicData;

namespace SupportForm
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SupportDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");
            SqlDataAdapter daQ = new SqlDataAdapter("select * from questions order by questionno", conn);
            SqlDataAdapter daAns = new SqlDataAdapter("SELECT * from MultipleChoiceAnswers", conn);
            DataSet ds = new DataSet();
            daQ.Fill(ds.Tables.Add("questions"));
            daAns.Fill(ds.Tables.Add("answers"));
            questions.InnerHtml += "<div class='content'>";
            foreach (DataRow dr in ds.Tables["questions"].Rows)
            {
                questions.InnerHtml += "<div class='row'>";
                questions.InnerHtml += "<div class='col-sm'>" + dr["questionno"] + ") " + dr["question"] + "</div>\n";
                DataView dvA = ds.Tables["answers"].DefaultView;
                dvA.RowFilter = "questionid = '" + dr["questionid"] + "'";
                DataTable dtMCA = dvA.ToTable();
                if (dtMCA.Rows.Count > 0)
                {
                    questions.InnerHtml += "<div class='col-sm'><select id='" + dr["questionid"] + "' name='" + dr["questionid"] + "' runat='server'>\n";
                    foreach (DataRow drMCA in dtMCA.Rows)
                    {
                        questions.InnerHtml += "<option value='" + drMCA["multiplechoiceanswersid"] + "'>" + drMCA["answer"] + "</option>\n";
                    }

                    questions.InnerHtml += "<option selected>--SELECT--</option>\n";
                    questions.InnerHtml += "</select></div>";
                }
                else
                {
                    questions.InnerHtml += "<div class='col-sm'><input type='text' id='" + dr["questionid"] + "' name='" + dr["questionid"] + "' runat='server'></div>\n";
                }

                questions.InnerHtml += "</div>\n";
            }

            questions.InnerHtml += "</div>\n";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SupportDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");
            SqlDataAdapter daQ = new SqlDataAdapter("select * from questions order by questionno", conn);
            DataSet ds = new DataSet();
            daQ.Fill(ds);
            SqlCommand cmdQuestionnaire = new SqlCommand("insert into questionnaire (questionnaireid) values (@questionnaireid)", conn);
            var questionnaireId = Guid.NewGuid();
            cmdQuestionnaire.Parameters.Add(new SqlParameter("@questionnaireid", questionnaireId));
            conn.Open();
            cmdQuestionnaire.ExecuteNonQuery();
            conn.Close();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                var answer = Request.Form[dr["questionid"].ToString()];
                if (Guid.TryParse(answer, out Guid aGuid) == false && answer != "--SELECT--")
                {
                    var freeTextAnswersId = Guid.NewGuid();
                    SqlCommand cmdFreeTextAnswer = new SqlCommand("insert into freetextanswers (freetextanswersid, " +
                                                                  "questionid, answer) values (@freetextanswersid, " +
                                                                  "@questionid, @answer)", conn);
                    cmdFreeTextAnswer.Parameters.Add(new SqlParameter("@freetextanswersid", freeTextAnswersId));
                    cmdFreeTextAnswer.Parameters.Add(new SqlParameter("@questionid", dr["questionid"].ToString()));
                    cmdFreeTextAnswer.Parameters.Add(new SqlParameter("@answer", answer));
                    SqlCommand cmdAns = new SqlCommand("insert into answers (questionnaireid, questionid, freetextanswersid) " +
                                                       "values (@questionnaireid, @questionid, @freetextanswersid)", conn);
                    cmdAns.Parameters.Add(new SqlParameter("@questionnaireid", questionnaireId));
                    cmdAns.Parameters.Add(new SqlParameter("@questionid", dr["questionid"].ToString()));
                    cmdAns.Parameters.Add(new SqlParameter("@freetextanswersid", @freeTextAnswersId));
                    conn.Open();
                    cmdFreeTextAnswer.ExecuteNonQuery();
                    cmdAns.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    SqlCommand cmdAns = new SqlCommand("insert into answers (questionnaireid, questionid, multiplechoiceanswersid) " +
                                                       "values (@questionnaireid, @questionid, @multiplechoiceanswersid)", conn);
                    cmdAns.Parameters.Add(new SqlParameter("@questionnaireid", questionnaireId));
                    cmdAns.Parameters.Add(new SqlParameter("@questionid", dr["questionid"].ToString()));
                    cmdAns.Parameters.Add(new SqlParameter("@multiplechoiceanswersid", answer));
                    conn.Open();
                    cmdAns.ExecuteNonQuery();
                    conn.Close();
                }
            }
            Response.Redirect("Results.aspx?id=" + questionnaireId);
        }
    }
}