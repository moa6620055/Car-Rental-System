namespace Task1
{
    internal class Car
    {
        private static int nextId = 1;

        public string ID { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Condition { get; set; }
        public CarStatus Status { get; set; }
        public RentalTransaction ActiveTransaction { get; set; }

        public Car(string model, int year, string condition, CarStatus status)
        {
            ID = $"CAR-{nextId:000}";
            nextId++;

            Model = model;
            Year = year;
            Condition = condition;
            Status = status;
            ActiveTransaction = null;
        }

        public bool IsAvailable()
        {
            return Status == CarStatus.Available;
        }

        public RentalTransaction Rent(Customer customer)
        {
            if (!IsAvailable())
            {
                throw new InvalidOperationException($"Car {ID} is not available (Status: {Status}).");
            }

            DateTime rentedDate = DateTime.Now.Date;
            DateTime dueDate = rentedDate.AddDays(14);

            RentalTransaction transaction = new RentalTransaction(customer, this, rentedDate, dueDate);

            Status = CarStatus.Rented;
            ActiveTransaction = transaction;
            customer.ActiveRentals++;
            customer.Transactions.Add(transaction);

            return transaction;
        }

        public decimal Return()
        {
            if (ActiveTransaction == null)
            {
                throw new InvalidOperationException("No active transaction for this car.");
            }

            if (Status != CarStatus.Rented)
            {
                throw new InvalidOperationException($"Car {ID} is not currently rented.");
            }

            DateTime returnDate = DateTime.Now.Date;
            decimal fee = ActiveTransaction.CalculateFee(returnDate);

            ActiveTransaction.ReturnedDate = returnDate;
            ActiveTransaction.Fee = fee;
            ActiveTransaction.Customer.ActiveRentals--;

            Status = CarStatus.Available;
            ActiveTransaction = null;

            return fee;
        }
    }
}
