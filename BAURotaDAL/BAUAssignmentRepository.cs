using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAURotaDAL
{
    public class BAUAssignmentRepository : IBAUAssignmentRepository
    {
        public bool Create(DateTime date, bool morning, int employeeNumber)
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BAURotaDatabase"].ConnectionString))
            {
                int rowsAffected = sqlConnection.Execute(
                    SqlQueries.InsertBAUAssignment,
                    new { Date = date, Morning = morning , EmployeeNumber = employeeNumber}
                    );

                return rowsAffected > 0;
            }
        }

        public BAUAssignment Get(DateTime date, bool morning)
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BAURotaDatabase"].ConnectionString))
            {
                BAUAssignment bauAssignment = sqlConnection.Query<BAUAssignment>(
                    SqlQueries.SelectBAUAssignmentByDateMorning,
                    new { Date = date, Morning = morning }
                    ).SingleOrDefault();

                return bauAssignment;
            }
        }

        public List<BAUAssignment> ListFrom(DateTime date)
        {
            using (var sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["BAURotaDatabase"].ConnectionString))
            {
                IEnumerable<BAUAssignment> bauAssignments = sqlConnection.Query<BAUAssignment>(
                    SqlQueries.SelectBAUAssignmentLaterThanDate,
                    new { date } );

                return bauAssignments.ToList();
            }
        }
    }
}
