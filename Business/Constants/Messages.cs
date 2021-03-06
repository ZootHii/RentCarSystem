﻿namespace Business.Constants
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
        public static string UserExistsWithTheSameEMail => "There is a user already exists with the same eMail";
        public static string InvalidFileExtension => "File extension must be JPG, JPEG or PNG";
        public static string NullFileError => "Image File must not be null";
        public static string CarHasNoImage => "There is no image for this car";
        public static string CarReachedMaxImageCount => "Car has reached max image (5)";
        public static string AuthorizationDenied => "Authorization Denied not an authorized operation";
        public static string UserRegistered => "Successfully Registered";
        public static string UserLoggedIn => "Successfully Logged in";
        public static string CarImagesListedByCarId => "Car image/s listed by car id";
        public static string UserNotFound => "User not found";
        public static string PasswordNotTrue => "User password is not true";
        public static string AccessTokenCreated => "Access token created";

        public static string BrandAdded => "Brand is successfully Added";
        public static string BrandUpdated => "Brand is successfully Updated";
        public static string BrandDeleted => "Brand is successfully Deleted";
        
        public static string ColorAdded => "Color is successfully Added";
        public static string ColorUpdated => "Color is successfully Updated";
        public static string ColorDeleted => "Color is successfully Deleted";
        public static string CustomerFindeksScoreIsNotEnough => "Findeks Score is not enough for this car.";
    }
}