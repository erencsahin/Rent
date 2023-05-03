using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        //static'in mantıgı her yerde message kullanmak istedigimizden
        //ötürü static yaparsak sürekli instance(Messages messages= new Messages()) yapmamak adına
        //bu şekilde static diyoruz ve sadece bir adet instance oluşturarak iş yükünden kurtuluyoruz.
        public static string CarAdded = "Car has been added.";
        public static string CarNameInvalid = "Car name invalid.";
        public static string CarDeleted = "Car has been deleted.";
        public static string CarUpdated = "Car has been updated.";
        public static string MaintenanceTime = "We are in maintenance time.";
        public static string CarsListed = "Cars has been listed.";

        public static string BrandsListed = "Brands has been listed.";
        public static string AddBrand = "Brand has been added.";
        public static string DeleteBrand = "Brand has been deleted";
        public static string UpdateBrand = "Brand has been updated";

        public static string CarNoRent = "Couldn't rent a car.";
        public static string CarRented = "Car has been rented.";
        public static string CarRentDeleted = "Car rental has been deleted.";
        public static string CarsRentListed = "Rented cars has been listed.";

        public static string ColorsListed = "Colors has been listed";
        public static string DeleteColor = "Color has been deleted";
        public static string AddColor = "Color has been added";

        public static string CustomerAdded = "Customer added.";
        public static string CustomerDeleted = "Customer deleted.";
        public static string CustomerListed = "Customers listed.";
        public static string CustomerIdListed = "The customer who you want the see are shown.";
        public static string CustomerUpdated = "Customer updated.";

        public static string UserAdded = "User has been added.";
        public static string UserDeleted = "User has been deleted.";
        public static string UserUpdated = "User has been updated.";
        public static string UsersListed = "Users has been listed.";
        public static string UserListedByFiltered = "User listed by your choice.";
    }
}
