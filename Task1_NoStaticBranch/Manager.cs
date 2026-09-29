namespace Task1
{
    internal class Manager : Iuser
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public decimal Salary { get; set; }
        public DateTime Hired { get; set; }

        public Manager(string id, string name, string phone, decimal salary, DateTime hired)
        {
            ID = id;
            Name = name;
            Phone = phone;
            Salary = salary;
            Hired = hired;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("--- MANAGER PROFILE ---");
            Console.WriteLine($"ID : {ID} | Name : {Name} | Phone : {Phone}");
            Console.WriteLine($"Salary : ${Salary:N2} | Hired : {Hired:dd/MM/yyyy}");
        }
    }
}
