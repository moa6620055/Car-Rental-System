namespace Task1
{
    internal class BranchInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Hours { get; set; }
        public Manager Manager { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Car> Cars { get; set; }

        public BranchInfo(string id, string name, string phone, string address, string hours, Manager manager)
        {
            ID = id;
            Name = name;
            Phone = phone;
            Address = address;
            Hours = hours;
            Manager = manager;
            Customers = new List<Customer>();
            Cars = new List<Car>();
        }

        public void DisplayInfo()
        {
            Console.WriteLine("RENTAL BRANCH INFO");
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"ID : {ID}");
            Console.WriteLine($"Name : {Name}");
            Console.WriteLine($"Address : {Address}");
            Console.WriteLine($"Phone : {Phone}");
            Console.WriteLine($"Hours : {Hours}");
            Console.WriteLine($"Manager : {Manager.Name}");
            Console.WriteLine($"Total Customers : {Customers.Count}");
            Console.WriteLine($"Total Vehicles : {Cars.Count}");
        }

        public Customer FindCustomer(string customerId)
        {
            for (int i = 0; i < Customers.Count; i++)
            {
                if (Customers[i].ID.ToLower() == customerId.ToLower())
                {
                    return Customers[i];
                }
            }

            throw new InvalidOperationException("Customer not found.");
        }

        public Car FindCar(string carId)
        {
            for (int i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].ID.ToLower() == carId.ToLower())
                {
                    return Cars[i];
                }
            }

            throw new InvalidOperationException("Car not found.");
        }
    }
}
