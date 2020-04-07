using System;
using System.Data.SqlClient;
using Cw3.DTOs.Requests;
using Cw3.DTOs.Responses;

namespace Cw3.Services {
    public class SqlServerDbService : IStudentsDbService {
        private const string ConString = "Data Source=db-mssql;Initial Catalog=s19101;Integrated Security=True";

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request) {
            EnrollStudentResponse response = null;
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand()) {
                com.Connection = con;
                con.Open();
                try {
                    com.Transaction = con.BeginTransaction();
                    com.CommandText = "select Studies.IdStudy from dbo.Studies where Studies.Name = @study";
                    com.Parameters.AddWithValue("study", request.Studies);
                    var dr = com.ExecuteReader();
                    int idStudy;
                    if (dr.Read()) {
                        idStudy = int.Parse(dr["IdStudy"].ToString());
                    } else {
                        com.Transaction.Rollback();
                        throw new Exception("Studia nie istnieją");
                    }
                    dr.Close();

                    com.CommandText =
                        "select Enrollment.IdEnrollment, Enrollment.Semester, Studies.Name, format(Enrollment.StartDate, 'dd-MM-yyyy') as StartDate from dbo.Enrollment inner join Studies on Studies.IdStudy = Enrollment.IdStudy where Enrollment.Semester = 1 and Enrollment.IdStudy = @IdStudy";
                    com.Parameters.AddWithValue("IdStudy", idStudy);
                    dr = com.ExecuteReader();
                    int idEnrollment;
                    if (dr.Read()) {
                        idEnrollment = int.Parse(dr["IdEnrollment"].ToString());
                    } else {
                        dr.Close();
                        com.CommandText =
                            "insert into dbo.Enrollment (IdEnrollment, Semester, IdStudy, StartDate) values ((select MAX(Enrollment.IdEnrollment) + 1 from dbo.Enrollment), 1, @studies, @StartDate); select Enrollment.IdEnrollment, Enrollment.Semester, Studies.Name, format(Enrollment.StartDate, 'dd-MM-yyyy') as StartDate from dbo.Enrollment inner join Studies on Studies.IdStudy = Enrollment.IdStudy where Enrollment.IdEnrollment = (select max(IdEnrollment) from dbo.Enrollment)";
                        com.Parameters.AddWithValue("studies", idStudy);
                        com.Parameters.AddWithValue("StartDate", DateTime.Now.ToString("yyyy-MM-dd"));
                        dr = com.ExecuteReader();
                        dr.Read();
                        idEnrollment = int.Parse(dr["IdEnrollment"].ToString());
                    }
                    response = new EnrollStudentResponse {
                        IdEnrollment = idEnrollment,
                        Semester = int.Parse(dr["Semester"].ToString()),
                        Study = dr["Name"].ToString(),
                        StartDate = dr["StartDate"].ToString()
                    };
                    dr.Close();

                    com.CommandText =
                        "insert into dbo.Student (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (@IndexNumber, @FirstName, @LastName, @BirthDate, @IdEnrollment)";
                    com.Parameters.AddWithValue("IndexNumber", request.IndexNumber);
                    com.Parameters.AddWithValue("FirstName", request.FirstName);
                    com.Parameters.AddWithValue("LastName", request.LastName);
                    com.Parameters.AddWithValue("BirthDate", request.BirthDate);
                    com.Parameters.AddWithValue("IdEnrollment", idEnrollment);
                    com.ExecuteNonQuery();

                    com.Transaction.Commit();
                } catch (SqlException) {
                    com.Transaction.Rollback();
                    throw;
                }
            }
            return response;
        }

        public EnrollStudentResponse EnrollPromotions(EnrollPromotionsRequest request) {
            EnrollStudentResponse response = null;
            using (var con = new SqlConnection(ConString))
            using (var com = new SqlCommand()) {
                com.Connection = con;
                con.Open();
                com.CommandText = "exec enrollpromotions @studies, @semester";
                com.Parameters.AddWithValue("studies", request.Studies);
                com.Parameters.AddWithValue("semester", request.Semester);
                var dr = com.ExecuteReader();
                if (dr.Read()) {
                    response = new EnrollStudentResponse {
                        IdEnrollment = int.Parse(dr["IdEnrollment"].ToString()),
                        Semester = int.Parse(dr["Semester"].ToString()),
                        Study = dr["Name"].ToString(),
                        StartDate = dr["StartDate"].ToString()
                    };
                }
            }
            return response;
        }
    }
}