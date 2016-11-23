using BAURotaDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

/// <summary>
/// Service allows user to get BAU rota details, assign employees for todays rota, and get eligible employees
/// </summary>
namespace BAURotaService
{
    [ServiceContract]
    public interface IBAURotaService
    {
        [OperationContract]
        List<int?> GetTodaysRota();

        [OperationContract]
        List<int> GetEligibleEmployees(bool morning);

        [OperationContract]
        bool AssignEmployee(int employeeNumber, bool morning);
    }

    [DataContract]
    public class AssignmentFault
    {
        public AssignmentFault(string message)
        {
            Message = message;
        }

        [DataMember]
        public string Message { get; private set; }
    }
}
