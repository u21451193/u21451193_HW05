using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;


namespace u21451193_HW05.Models
{
    public class DataService
    {
        private String ConnectionString;

        public DataService()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public List<Books> getAllBooks()
        {
            List<Books> books = new List<Books>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT b.bookId, b.name, a.surname AS aSurname, t.name AS tName, b.pagecount, b.point FROM authors a INNER JOIN books b ON a.authorId = b.authorId INNER JOIN types t ON b.typeId = t.typeId LEFT JOIN borrows bb ON bb.bookId = b.bookId GROUP BY b.bookId, b.name, a.surname, t.name, b.pagecount, b.point ORDER BY b.bookId", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Books book = new Books
                            {
                                BookID = Convert.ToInt32(reader["bookId"]),
                                BookName = Convert.ToString(reader["name"]),
                                BookPageCount = Convert.ToInt32(reader["pagecount"]),
                                BookPoint = Convert.ToInt32(reader["point"]),
                                AuthorName = Convert.ToString(reader["aSurname"]),
                                TypeName = Convert.ToString(reader["tName"]),
                                BookStatusTaken = null,
                                BookStatusBrought = null,
                                BookStatus = ""
                            };
                            // DBNull error because system cant convert DB null values to date time
                            

                            books.Add(book);
                        }
                    }
                }
                con.Close();
            }
            return books;
        }
        public List<Author> getAllAuthors()
        {
            List<Author> authors = new List<Author>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM authors", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Author author = new Author
                            {
                                AuthorId = Convert.ToInt32(reader["authorId"]),
                                AuthorName = Convert.ToString(reader["name"]),
                                AuthorSurname = Convert.ToString(reader["surname"])
                            };

                            authors.Add(author);
                        }
                    }
                }
                con.Close();
            }
            return authors;
        }
        public List<Types> getAllTypes()
        {
            List<Types> types = new List<Types>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM books", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Types type = new Types
                            {
                                TypeID = Convert.ToInt32(reader["typeId"]),
                                typeName = Convert.ToString(reader["name"]),
                            };

                            types.Add(type);
                        }
                    }
                }
                con.Close();
            }
            return types;
        }
        public List<Students> getAllStudents()
        {
            List<Students> students = new List<Students>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select * from students", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Students stu = new Students
                            {
                                StudentID = Convert.ToInt32(reader["studentId"]),
                                StudentName = Convert.ToString(reader["name"]),
                                StudentSurname = Convert.ToString(reader["surname"]),
                                StudentBirthDate = Convert.ToDateTime(reader["birthDate"]),
                                StudentGender = Convert.ToString(reader["gender"]),
                                StudentClass = Convert.ToString(reader["class"]),
                                StudentPoint = Convert.ToInt32(reader["point"])
                            };
                            students.Add(stu);
                        }
                    }
                }
                con.Close();
            }
            return students;
        }
        public List<Borrows> getAllBorrows()
        {

            List<Borrows> borrows = new List<Borrows>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT b.borrowId, b.takenDate, b.broughtDate, CONCAT(s.name, ' ' , s.surname) AS studentName, bb.name AS bookName, bb.bookId AS bookID FROM students AS s INNER JOIN borrows AS b ON b.studentId = s.studentId  INNER JOIN books AS bb ON b.bookId = bb.bookId", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Borrows borrow = new Borrows
                            {
                                BorrowsID = Convert.ToInt32(reader["borrowId"]),
                                StudentName = Convert.ToString(reader["studentName"]),
                                BookName = Convert.ToString(reader["bookName"]),
                                BorrowsTakenDate = Convert.ToDateTime(reader["takenDate"]),
                                BorrowsBroughtDate = Convert.ToDateTime(reader["broughtDate"])
                            };
                            borrows.Add(borrow);
                        }
                    }
                }
                con.Close();
            }
            return borrows;

        }


        // Parameters required
        public List<Borrows> getBookdByID(int ID)
        {
            List<Borrows> books = new List<Borrows>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT bb.name AS bName, b.borrowId, b.bookId, b.takenDate, b.broughtDate, CONCAT(s.name, ' ', s.surname) AS sName FROM borrows b INNER JOIN students s   ON b.studentId = s.studentId   INNER JOIN books bb   ON b.bookId = bb.bookId  WHERE bb.bookId =" +  ID + "ORDER BY b.borrowId DESC", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Borrows book = new Borrows
                            {
                                BorrowsID = Convert.ToInt32(reader["borrowId"]),
                                BookName = Convert.ToString(reader["bName"]),
                                BorrowsTakenDate = Convert.ToDateTime(reader["takenDate"]),
                                BorrowsBroughtDate = Convert.ToDateTime(reader["broughtDate"]),
                                StudentName = Convert.ToString(reader["sName"])
                            };
                            books.Add(book);
                        }
                    }
                }
                connection.Close();
            }
            return books;
        }

        public void BorrowBook(Borrows borrows, int id)
        {
            // SQL INSERT STATEMENT
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                var query = "Insert Into borrows(borrowId, studentId, bookId, takenDate, broughtDate) Values('" + borrows.BorrowsID + "', '" + id + "', '" + id + "', '" + System.DateTime.Now + "', ' Book Out ')";
                SqlCommand sqlCommand = new SqlCommand(query, con);
                con.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }

        }

        public void ReturnBook()
        {
            //SQL UPDATE STATEMENT
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                var query = "Update borrows SET broughtDate " + System.DateTime.Now;
                SqlCommand sqlCommand = new SqlCommand(query, con);
                con.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }

        }
    }
}
