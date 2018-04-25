using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            var isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("What do you want to do:");
                Console.WriteLine("1) Add Professor?");
                Console.WriteLine("2) Add Class?");
                Console.WriteLine("3) View Classes?");
                Console.WriteLine("0) exit");

                var action = Console.ReadLine();

                if (action == "1")
                {
                    Console.WriteLine("Name?");
                    var name = Console.ReadLine();

                    Console.WriteLine("Title?");
                    var title = Console.ReadLine();

                    InsertProfessor(name, title);
                }
                else if (action == "2")
                {
                    Console.WriteLine("Number?");
                    var number = Console.ReadLine();
                    var courseNumber = Convert.ToInt32(number);

                    Console.WriteLine("Class Name?");
                    var name = Console.ReadLine();

                    Console.WriteLine("Room?");
                    var room = Console.ReadLine();
                    var roomNumber = Convert.ToInt32(room);

                    Console.WriteLine("Start time?");
                    var time = Console.ReadLine();
                    var startTime = Convert.ToDateTime(time);

                    InsertCourses(courseNumber, name, roomNumber, startTime);
                }
                else if (action == "3")
                {
                    var courseName = Console.ReadLine();
                    ViewEnrollment(courseName); 
                }
                else if (action == "0")
                {
                    isRunning = false;
                }
            }

            Console.WriteLine("Bye!");

        }

        public static string GetConnectionString()
        {
            return @"Data Source=localhost\SQLEXPRESS;Initial Catalog=college;Integrated Security=True";

        }

        private static void InsertProfessor(string name, string title)
        {

            SqlConnection conn = new SqlConnection(GetConnectionString());
            string sql = "INSERT INTO Professors (name,title ) VALUES (@name,@title)";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                conn.Close();
            }
        }

        private static void InsertCourses(int number, string name, int room, DateTime starttime)
        {

            SqlConnection conn = new SqlConnection(GetConnectionString());
            string sql = "INSERT INTO Courses (number, name, room, starttime ) VALUES (@number,@name,@room,@starttime)";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@number", number);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@room", room);
                cmd.Parameters.AddWithValue("@starttime", starttime);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                conn.Close();
            }
        }

        private static void ViewEnrollment(string coursename)
        {

            SqlConnection conn = new SqlConnection(GetConnectionString());
            string sql = "SELECT FullName from Courses JOIN Enrollment on courses.CourseID = Enrollment.CourseID JOIN Students on students.StudentID = Enrollment.StudentID WHERE courses.name = @coursename";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@coursename", coursename);
                cmd.CommandType = CommandType.Text;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Console.WriteLine(dr["FullName"]);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Insert Error:";
                msg += ex.Message;
                throw new Exception(msg);

            }
            finally
            {
                conn.Close();
            }
        }
    }
}
