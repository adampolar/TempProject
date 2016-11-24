using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using BAURotaDAL;
using EmployeeService;

namespace BAURotaService
{
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
            if((morning && _bauAssignmentRepository.Get(DateTime.Now.Date, false) != null) || 
                _bauAssignmentRepository.ListFrom(DateTime.Now.Date).Count() > 0)
            {
                throw new FaultException<AssignmentFault>(new AssignmentFault("Cannot assign before another assignement"));
            }
            return _bauAssignmentRepository.Create(DateTime.Now.Date, morning, employeeNumber);
        }

        public List<int> GetEligibleEmployees(bool morning)
        {
            List<BAUAssignment> bauAssignmentsForAtLeastPastTwoWeeks = 
                _bauAssignmentRepository.ListFrom(DateTime.Now - new TimeSpan(15, 0 , 0, 0)).Take(20).ToList();

            List<int> allEmployees;

            using (var employeeService = new Util.WebServiceProxy<IEmployeeService>("EmployeeService"))
            {
                allEmployees = employeeService.Service.GetAllEmployees().Select(e => e.StaffNumber).ToList();
            }

            return _bauAssignmentEligibilitySelector.ListEligibleEmployees(DateTime.Now.Date, morning, bauAssignmentsForAtLeastPastTwoWeeks, allEmployees);
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
