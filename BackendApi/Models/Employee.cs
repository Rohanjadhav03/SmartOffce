namespace BackendApi.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string Email { get; set; }

        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }

        public DateTime CreatedDate { get; set; }

        // Optional: Include Department info when needed (JOIN)
        public string DepartmentName { get; set; }
    }
}
