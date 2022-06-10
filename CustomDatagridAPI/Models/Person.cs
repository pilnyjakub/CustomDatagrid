namespace CustomDatagridAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public int Age { get; set; }
        public string Location { get; set; } = "";
        public string Work { get; set; } = "";
        public DateTime StartDate { get; set; }
        public int Salary { get; set; }
    }
}
