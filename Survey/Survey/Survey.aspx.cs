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
        private int currentQuestion = 0;


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
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(getInsertAnswerSQL(textBoxAnswer.Text, checkBoxOptOut.Checked), connection);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                { }
                connection.Close();
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
                questionText.Text = questions.Tables[0].Rows[currentQuestion]["QuestionText"].ToString();
                currentQuestion ++;
            }
            //catch is empty as 
            catch { }
            
        }

        protected string getQuestionSQL()
        {

            if (Session["UserID"] != null)
            {
                return "SELECT S.tblSurveyID, S.QuestionText, S.QuestionDescription, S.Active, S.Modified, S.Created " +
                       "FROM tblSurvey S " +
                       "WHERE S.tblSurveyID IN (SELECT tblSurvey_ID " +
                       "FROM tblSurveyAnswers " +
                       $"WHERE tblUser_ID != {Session["UserID"]})";
            }
            else
            {
                return "SELECT * " +
                       "FROM tblSurvey S";
            }
        }

        protected string getInsertAnswerSQL(string responds, bool OptOut)
        {
            string questionID = questions.Tables[0].Rows[currentQuestion]["tblSurveyID"].ToString();

            return "INSERT INTO tblSurveyAnswers(tblUser_ID, tblSurvey_ID, UserAnswer, OptOut) " +
                   $"VALUES({Session["UserID"].ToString()},{questionID}, {responds}, {OptOut} )";
        }
    }
}