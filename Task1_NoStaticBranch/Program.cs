namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the manager and branch
            Manager manager = new Manager(
                "MGR-01", "Sara Ahmed", "01012345678", 9000,
                new DateTime(2019, 3, 15));

            BranchInfo branch = new BranchInfo(
                "BR-01",
                "Elite Auto Rental - Nasr City Branch",
                "01099887766",
                "45 Abbas El Akkad St, Nasr City, Cairo",
                "Sat-Thu: 08:00 AM - 10:00 PM",
                manager);

            AddInitialData(branch);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("CAR RENTAL SYSTEM - MAIN MENU");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("1. Branch Information");
                Console.WriteLine("2. Show All Users");
                Console.WriteLine("3. Show Available Cars");
                Console.WriteLine("4. Show All Fleet");
                Console.WriteLine("5. Rent a Car");
                Console.WriteLine("6. Return a Car");
                Console.WriteLine("7. Customer Rental History");
                Console.WriteLine("8. Register New Customer");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    if (choice == "1")
                        branch.DisplayInfo();
                    else if (choice == "2")
                        ShowAllUsers(branch);
                    else if (choice == "3")
                        ShowAvailableCars(branch);
                    else if (choice == "4")
                        ShowAllFleet(branch);
                    else if (choice == "5")
                        RentCar(branch);
                    else if (choice == "6")
                        ReturnCar(branch);
                    else if (choice == "7")
                        ShowRentalHistory(branch);
                    else if (choice == "8")
                        RegisterCustomer(branch);
                    else if (choice == "0")
                    {
                        Console.WriteLine("Goodbye!");
                        break;
                    }
                    else
                        Console.WriteLine("Invalid choice. Please choose from 0 to 8.");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void AddInitialData(BranchInfo branch)
        {
            Customer customer1 = new Customer("CUST-001", "Ahmed Kamal", "01098765432", "ahmed@example.com", new DateTime(2023, 1, 28));
            Customer customer2 = new Customer("CUST-002", "Nour Hassan", "01155556677", "", new DateTime(2024, 3, 5));
            Customer customer3 = new Customer("CUST-003", "Omar Ali", "01211112222", "omar@example.com", new DateTime(2025, 6, 10));

            branch.Customers.Add(customer1);
            branch.Customers.Add(customer2);
            branch.Customers.Add(customer3);

            Car car1 = new Car("Toyota Corolla", 2024, "Excellent", CarStatus.Available);
            Car car2 = new Car("Toyota Corolla", 2024, "Fair", CarStatus.Rented);
            Car car3 = new Car("Hyundai Elantra", 2023, "Excellent", CarStatus.Available);
            Car car4 = new Car("Kia Cerato", 2022, "Poor", CarStatus.Maintenance);

            branch.Cars.Add(car1);
            branch.Cars.Add(car2);
            branch.Cars.Add(car3);
            branch.Cars.Add(car4);

            RentalTransaction oldTransaction = new RentalTransaction(customer1, car4, new DateTime(2026, 8, 1), new DateTime(2026, 8, 15));
            oldTransaction.ReturnedDate = new DateTime(2026, 8, 15);
            oldTransaction.Fee = 0;
            customer1.Transactions.Add(oldTransaction);

            RentalTransaction activeTransaction = new RentalTransaction(customer1, car1, new DateTime(2026, 9, 1), new DateTime(2026, 9, 15));
            customer1.Transactions.Add(activeTransaction);
            customer1.ActiveRentals = 1;
            car1.Status = CarStatus.Rented;
            car1.ActiveTransaction = activeTransaction;
        }

        static void ShowAllUsers(BranchInfo branch)
        {
            Console.WriteLine("All Registered Users");
            Console.WriteLine("----------------------------------");
            branch.Manager.DisplayInfo();

            for (int i = 0; i < branch.Customers.Count; i++)
                branch.Customers[i].DisplayInfo();
        }

        static void ShowAvailableCars(BranchInfo branch)
        {
            Console.WriteLine("Available Fleet:");
            Console.WriteLine("----------------------------------");
            int count = 0;

            for (int i = 0; i < branch.Cars.Count; i++)
            {
                if (branch.Cars[i].Status == CarStatus.Available)
                {
                    PrintCar(branch.Cars[i]);
                    count++;
                }
            }

            if (count == 0)
                Console.WriteLine("No available cars found.");
        }

        static void ShowAllFleet(BranchInfo branch)
        {
            Console.WriteLine("All Fleet");
            Console.WriteLine("----------------------------------");

            if (branch.Cars.Count == 0)
            {
                Console.WriteLine("No cars found in system.");
                return;
            }

            for (int i = 0; i < branch.Cars.Count; i++)
                PrintCar(branch.Cars[i]);
        }

        static void PrintCar(Car car)
        {
            Console.WriteLine($"Car [{car.ID}] - {car.Model} {car.Year} | Condition: {car.Condition} | {car.Status}");
        }

        static void RentCar(BranchInfo branch)
        {
            Console.Write("Enter Customer ID: ");
            string customerId = Console.ReadLine();
            Customer customer = branch.FindCustomer(customerId);

            ShowAvailableCars(branch);

            Console.Write("Enter Car ID to rent: ");
            string carId = Console.ReadLine();
            Car car = branch.FindCar(carId);

            RentalTransaction transaction = car.Rent(customer);
            Console.WriteLine($"Car [{car.ID}] \"{car.Model} {car.Year}\" rented by {customer.Name}.");
            Console.WriteLine($"Due date: {transaction.DueDate:dd/MM/yyyy}");
        }

        static void ReturnCar(BranchInfo branch)
        {
            Console.Write("Enter Car ID: ");
            string carId = Console.ReadLine();
            Car car = branch.FindCar(carId);
            decimal fee = car.Return();

            Console.WriteLine($"Car [{car.ID}]: {car.Model} {car.Year} returned.");

            if (fee == 0)
                Console.WriteLine("Returned on time. No late fee.");
            else
                Console.WriteLine($"Late return fee: {fee:F2} EGP");
        }

        static void ShowRentalHistory(BranchInfo branch)
        {
            Console.Write("Enter Customer ID: ");
            string customerId = Console.ReadLine();
            Customer customer = branch.FindCustomer(customerId);

            if (customer.Transactions.Count == 0)
            {
                Console.WriteLine("No rental history found.");
                return;
            }

            for (int i = 0; i < customer.Transactions.Count; i++)
                customer.Transactions[i].DisplayInfo();
        }

        static void RegisterCustomer(BranchInfo branch)
        {
            Console.Write("Enter Full Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phone = Console.ReadLine();
            Console.Write("Enter Email Address: ");
            string email = Console.ReadLine();

            if (!ContainsDigit(phone))
                throw new InvalidOperationException("Phone number must contain at least one digit.");

            if (!IsValidEmail(email))
                throw new InvalidOperationException("Invalid email format. Must contain '@' and '.'.");

            Customer customer = new Customer(name, phone, email);
            branch.Customers.Add(customer);
            Console.WriteLine($"Customer: {customer.Name} - [{customer.ID}] registered.");
        }

        static bool ContainsDigit(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                    return true;
            }
            return false;
        }

        static bool IsValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".");
        }
    }
}
