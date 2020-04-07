using System;
using System.Data.SqlClient;
using Cw3.DTOs.Requests;
using Cw3.DTOs.Responses;

namespace Cw3.Services {
    public interface IStudentsDbService {
        EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        EnrollStudentResponse EnrollPromotions(EnrollPromotionsRequest request);
    }
}