namespace Business.Constants
{
    public static class Messages
    {
        public static string SystemMaintenance => "System is in Maintenance";
        public static string InvalidName => "Invalid name";
        
        public static string CarAdded => "Car is successfully Added";
        public static string CarUpdated => "Car is successfully Updated";
        public static string CarDeleted => "Car is successfully Deleted";
        public static string CarInvalidModelYear => "Model Year must be bigger than 1999";
        public static string CarInvalidDailyPrice => "Daily Price must be bigger than 0";
        public static string CarsListed => "All cars are successfully listed";
        public static string CarsListedDetails => "All cars details are successfully listed";
        public static string CarsListedBrand => "All cars are successfully listed by brand";
        public static string CarsListedColor => "All cars are successfully listed by color";
        
        public static string RentalAdded => "Rental is successfully Added";
        public static string RentalUpdated => "Rental is successfully Updated";
        public static string RentalDeleted => "Rental is successfully Deleted";
        public static string RentalInvalidRentDate => "Invalid rent date";
        public static string RentalInvalidReturnDate => "Return date must be at least 2 hours more than rental date";
        public static string RentalsListed => "All rentals are successfully listed";
        public static string RentalsListedDetails => "All rentals details are successfully listed";
        public static string RentalsListedCar => "All rentals are successfully listed by car";
        public static string RentalsListedCustomer => "All rentals are successfully listed by customer";
        public static string RentalsCarInUse => "Car in use";
        
        public static string UserAdded => "User is successfully Added";
        public static string UserUpdated => "User is successfully Updated";
        public static string UserDeleted => "User is successfully Deleted";
        public static string UserInvalidPassword => "Invalid password";
        public static string UserInvalidEMail => "Invalid eMail";
        public static string UserInvalidNameDigits => "Name can not have digits";
        public static string UsersListed => "All users are successfully listed";
        public static string UsersListedFirstName => "All users are successfully listed by first name";
        public static string UsersListedLastName => "All users are successfully listed by last name";
        public static string UsersListedEMail => "All users are successfully listed by eMail";
    }
}