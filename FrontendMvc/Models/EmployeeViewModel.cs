namespace FrontendMvc.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Email { get; set; }

        public decimal Salary { get; set; }

        public string DepartmentName { get; set; }
        public int  DepartmentId { get; set; }
    }
}
