using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey.Data
{

    public partial class Survey : System.Web.UI.Page
    {
        private string conString = @"Data Source=DESKTOP-UDRQBUG\SQLEXPRESS;Initial Catalog=CASASurvey;Integrated Security=True";

        //List used to store all unaswered question for that client
        private DataSet questions = new DataSet();
        private int nextQuestion = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                labelUserID.Text = Session["UserID"].ToString();
            }

            getQuestionSet();
            //fill the questionText label with the first question
            getNextQuestion();
            
        }

        protected void onBtnSubmitClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        SqlCommand cmd = new SqlCommand();
                        if (Session["UserID"].ToString() != null)
                        {
                            string questionID = questions.Tables[0].Rows[nextQuestion - 1]["tblSurveyID"].ToString();
                            
                            if(checkBoxOptOut.Checked)
                            {
                                cmd = new SqlCommand("procedureInsertAnswerOptOut", connection);
                                cmd.Parameters.Add(new SqlParameter("@OptOut", '0'));
                            }
                            else
                            {
                                cmd = new SqlCommand("procedureInsertAnswer", connection);
                                cmd.Parameters.Add(new SqlParameter("@OptOut", '1'));
                            }
                            cmd.Parameters.Add(new SqlParameter("@UserID", Session["UserID"].ToString()));
                            cmd.Parameters.Add(new SqlParameter("@SurveyID", questionID));
                            cmd.Parameters.Add(new SqlParameter("@Responds", textBoxAnswer.Text));
                            cmd.CommandType = CommandType.StoredProcedure;
                        }

                        
                            cmd.ExecuteNonQuery();
                        
                    }
                }
                getNextQuestion();
                textBoxAnswer.Text = "";
            }
        }

        protected void isAnswerRequired(object sender, ServerValidateEventArgs e)
        {
            if(checkBoxOptOut.Checked)
            {
                e.IsValid = true;
            }
            else
            {
                if(textBoxAnswer.Text!= "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                }
            }
        }

        protected void getQuestionSet()
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    SqlCommand cmd = new SqlCommand();
                    if (Session["UserID"].ToString() != null)
                    {                        
                        cmd = new SqlCommand("procedureSelectQuestion", connection);
                        cmd.Parameters.Add(new SqlParameter("@UserID", Session["UserID"].ToString()));
                    }
                    else
                    {
                        cmd = new SqlCommand("procedureSelectAllQuestion", connection);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand = cmd;
                    adapter.Fill(questions);
                }
            }
        }

        protected void getNextQuestion()
        {
            try
            {
                questionText.Text = questions.Tables[0].Rows[nextQuestion]["QuestionText"].ToString();
                nextQuestion ++;
            } 
            catch
            {
                questionText.Text = "No new question avaliable";
            }
            
        }
    }
}