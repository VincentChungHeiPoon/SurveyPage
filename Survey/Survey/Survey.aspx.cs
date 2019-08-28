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
                    string sql = getInsertAnswerSQL(textBoxAnswer.Text, checkBoxOptOut.Checked);
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch
                    { }
                    connection.Close();
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
                    adapter.SelectCommand = new SqlCommand(getQuestionSQL(), connection);
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

        protected string getQuestionSQL()
        {

            if (Session["UserID"] != null)
            {
                return "SELECT S.tblSurveyID, S.QuestionText, S.QuestionDescription, S.Active, S.Modified, S.Created " +
                       "FROM tblSurvey S " +
                       "WHERE S.tblSurveyID NOT IN (SELECT tblSurvey_ID " +
                       "FROM tblSurveyAnswers " +
                       $"WHERE tblUser_ID = {Session["UserID"].ToString()})";
            }
            else
            {
                return "SELECT * " +
                       "FROM tblSurvey S";
            }
        }

        protected string getInsertAnswerSQL(string responds, bool OptOut)
        {
            //nextQuestion - 1 to get current question
            string questionID = questions.Tables[0].Rows[nextQuestion - 1]["tblSurveyID"].ToString();

            if (!OptOut)
            {
                return "INSERT INTO tblSurveyAnswers(tblUser_ID, tblSurvey_ID, UserAnswer, OptOut) " +
                       $"VALUES({Session["UserID"].ToString()},{questionID},{responds}, 0 )";
            }
            else
            {
                return "INSERT INTO tblSurveyAnswers(tblUser_ID, tblSurvey_ID, UserAnswer, OptOut) " +
                      $"VALUES({Session["UserID"].ToString()},{questionID}, null, 1 )";   
            }
        }
    }
}