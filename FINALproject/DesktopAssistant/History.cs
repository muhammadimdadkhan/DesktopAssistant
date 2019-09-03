using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopAssistant
{
    class History
    {
        SqlDataAdapter adp;
        SqlCommand cmd;
        string exception = null;
        public string ExceptionShow()
        {
            return exception;
        }
        Connection conn = new Connection();

        public DataTable AllBooks()
        {
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("select Books.Books_ID as [Book IDs],Books.Author_Name,Books.Books_Name as [Book Names],Books.Condition_Discription,Books.Edition,Books.price,(select top 1 Subjects.Name from ClassSubjects join Subjects on Subjects.Subjects_ID=ch.Subjects_ID ) as SubjectName,(select top 1 Classes.Classes_ID from ClassSubjects join Classes on ch.Classes_ID=Classes.Classes_ID) as ClassGrade from Books join ClassSubjects ch on Books.ClassSubjects_ID=ch.ClassSubjects_ID", conn.Connect());
                adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

            }
            catch (Exception ex)
            {
                exception = ex.Message + " Sorry data not found";
            }
            conn.Close();
            return dt;
        }
    }
}
