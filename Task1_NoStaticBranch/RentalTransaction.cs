namespace Task1
{
    internal class RentalTransaction
    {
        private static int nextId = 1000;
        public const decimal FeePerDay = 150m;

        public int TransactionId { get; set; }
        public Customer Customer { get; set; }
        public Car Car { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnedDate { get; set; }
        public decimal Fee { get; set; }

        public RentalTransaction(Customer customer, Car car, DateTime rentedDate, DateTime dueDate)
        {
            TransactionId = ++nextId;
            Customer = customer;
            Car = car;
            RentedDate = rentedDate;
            DueDate = dueDate;
            ReturnedDate = DateTime.MinValue;
            Fee = 0;
        }

        public decimal CalculateFee(DateTime returnDate)
        {
            int overdueDays = (returnDate - DueDate).Days;

            if (overdueDays <= 0)
            {
                return 0;
            }

            return overdueDays * FeePerDay;
        }

        public string GetStatus()
        {
            if (ReturnedDate == DateTime.MinValue)
            {
                return "Active";
            }

            return "Returned";
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"--- Transaction #{TransactionId} ------------------");
            Console.WriteLine($"Car : {Car.Model} {Car.Year}");
            Console.WriteLine($"Car ID : {Car.ID}");
            Console.WriteLine($"Rented : {RentedDate:dd/MM/yyyy}");
            Console.WriteLine($"Due : {DueDate:dd/MM/yyyy}");

            if (ReturnedDate == DateTime.MinValue)
            {
                Console.WriteLine("Returned : Not returned yet");
            }
            else
            {
                Console.WriteLine($"Returned : {ReturnedDate:dd/MM/yyyy}");
            }

            Console.WriteLine($"Status : {GetStatus()}");

            if (Fee == 0)
            {
                Console.WriteLine("Fee : None");
            }
            else
            {
                Console.WriteLine($"Fee : {Fee:F2} EGP");
            }
        }
    }
}
