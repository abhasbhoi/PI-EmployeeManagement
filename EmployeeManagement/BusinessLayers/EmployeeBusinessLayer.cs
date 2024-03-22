using EmployeeManagement.BusinessLayers.Contracts;
using EmployeeManagement.Models;
using EmployeeManagement.Repository.Contract;

namespace EmployeeManagement.BusinessLayers
{
    public class EmployeeBusinessLayer : IEmployeeBusinessLayer
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeBusinessLayer(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Employee AddEmployee(Employee employee)
        {
            var createdEmployee = _employeeRepository.AddEmployee(employee);
            _employeeRepository.Save();
            return createdEmployee;
        }

        public void DeleteEmployee(int employeeId)
        {
            _employeeRepository.DeleteEmployee(employeeId);
            _employeeRepository.Save();
        }

        public Employee GetEmployee(int employeeId)
        {
            return _employeeRepository.GetEmployee(employeeId);
        }

        public List<Employee> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        public Employee UpdateEmployee(int employeeId, Employee employee)
        {
            var updatedEmployee = _employeeRepository.UpdateEmployee(employeeId, employee);
            _employeeRepository.Save();
            return updatedEmployee;
        }
    }
}
