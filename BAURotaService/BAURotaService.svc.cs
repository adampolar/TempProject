using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BAURotaDAL;
using EmployeeService;

namespace BAURotaService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class BAURotaService : IBAURotaService
    {
        private IBAUAssignmentRepository _bauAssignmentRepository;
        public IBAUAssignmentEligibilitySelector _bauAssignmentEligibilitySelector;

        public BAURotaService(IBAUAssignmentRepository bauAssignmentRepository, IBAUAssignmentEligibilitySelector bauAssignmentEligibilitySelector)
        {
            _bauAssignmentRepository = bauAssignmentRepository;
            _bauAssignmentEligibilitySelector = bauAssignmentEligibilitySelector;
        }

        public bool AssignEmployee(int employeeNumber, bool morning)
        {
            return _bauAssignmentRepository.Create(DateTime.Now.Date, morning, employeeNumber);
        }

        public List<int> GetEligibleEmployees(bool morning)
        {
            List<BAUAssignment> bauAssignmentsForPastTwoWeeks = _bauAssignmentRepository.ListFrom(DateTime.Now - new TimeSpan(14, 0 , 0, 0));

            List<int> allEmployees;

            using (var employeeService = new Util.WebServiceProxy<IEmployeeService>("EmployeeService"))
            {
                allEmployees = employeeService.Service.GetAllEmployees().Select(e => e.StaffNumber).ToList();
            }

            return _bauAssignmentEligibilitySelector.ListEligibleEmployees(DateTime.Now.Date, morning, bauAssignmentsForPastTwoWeeks, allEmployees);
        }

        public List<int?> GetTodaysRota()
        {
            return new List<int?>()
            {
                _bauAssignmentRepository.Get(DateTime.Now.Date, true)?.EmployeeNumber,
                _bauAssignmentRepository.Get(DateTime.Now.Date, false)?.EmployeeNumber
        };
        }
    }
}
