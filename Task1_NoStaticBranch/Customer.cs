namespace Task1
{
    internal class Customer : Iuser
    {
        private static int nextId = 4;

        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Joined { get; set; }
        public int ActiveRentals { get; set; }
        public List<RentalTransaction> Transactions { get; set; }

        public Customer(string name, string phone, string email)
        {
            ID = $"CUST-{nextId:000}";
            nextId++;

            Name = name;
            Phone = phone;
            Email = email;
            Joined = DateTime.Now.Date;
            ActiveRentals = 0;
            Transactions = new List<RentalTransaction>();
        }

        // Used only for the sample customers in the system.
        public Customer(string id, string name, string phone, string email, DateTime joined)
        {
            ID = id;
            Name = name;
            Phone = phone;
            Email = email;
            Joined = joined;
            ActiveRentals = 0;
            Transactions = new List<RentalTransaction>();
        }

        public void DisplayInfo()
        {
            Console.WriteLine("--- CUSTOMER PROFILE ---");
            Console.WriteLine($"ID : {ID} | Name : {Name} | Joined : {Joined:dd/MM/yyyy}");
            Console.WriteLine($"Phone : {Phone} | Email : {(string.IsNullOrEmpty(Email) ? "N/A" : Email)} | Active Rentals : {ActiveRentals}");
        }
    }
}
