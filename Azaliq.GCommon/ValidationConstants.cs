namespace Azaliq.GCommon
{
    public static class ValidationConstants
    {
        public static class Product
        {
            // Product Name
            public const int NameMaxLength = 100;
            public const int NameMinLength = 3;
            public const string NameRegex = @"^[A-Za-z0-9\s\-(),]+$";
            public const string NameRegexErrorMessage = "Product name can only contain letters, numbers, spaces, hyphens, commas, and parentheses.";

            // Product Description
            public const int DescriptionMaxLength = 1000;
            public const int DescriptionMinLength = 10;
            public const string DescriptionLengthErrorMessage = "Description must be between 10 and 1000 characters.";
            public const string DescriptionRegex = @"^[^<>]*$";
            public const string DescriptionRegexErrorMessage = "Description cannot contain angle brackets (< or >).";

            // Product Category
            public const string CategoryNameDisplay = "Category Name";

            // Product Image
            public const string ImageUrlDisplay = "Image URL";
            public const int ImageUrlMaxLength = 255;
            public const string ImageUrlRegex = @"^https?:\/\/.*\.(jpg|jpeg|png|gif|bmp|webp)$";
            public const string ImageUrlRegexErrorMessage = "Only valid image URLs ending in .jpg, .png, etc. are allowed.";

            // Product Price
            public const string PriceErrorMessage = "Price must be between 0.01 and 100,000.";
            public const string PriceRegex = @"^\d{1,6}(\.\d{1,2})?$";
            public const string PriceRegexErrorMessage = "Price must be a valid decimal number with up to 2 decimal places.";

            // Product Quantity
            public const string QuantityRequiredError = "Quantity is required.";
            public const int QuantityMinValue = 1;
            public const int QuantityMaxValue = 1000;
            public const string QuantityErrorMessage = "Quantity must be between 1 and 1000.";

            // Same-Day Delivery
            public const string SameDayDeliveryRequiredDisplay = "Is same day delivery available";
            
            //Product Tag
            public const string TagsDisplayName = "Tags";
            public const string AllTagsDisplayName = "All Tags";

            // Product Availability
            public const string IsAvailableDisplay = "Available";

        }

        public static class Review
        {
            public const int TextMaxLength = 500;
            public const string TextErrorMessage = "Comment can't exceed 500 characters.";
            
            public const int RatingMinValue = 1;
            public const int RatingMaxValue = 5;
            
            public const string RatingRequiredErrorMessage = "Rating must be between 1 and 5.";
            public const string RatingRequired = "Rating is required.";

            public const string CommentRequired = "Comment is required.";
        }

        public static class ProductTag
        {
            public const string TagRequiredErrorMessage = "Tag name is required.";
            public const int NameMaxLength = 50;
            public const int NameMinLength = 2;
            public const string TagErrorMessage = "Tag name must be between 2 and 50 characters.";
        }
        
        public static class Archives
        {
            public const int EmailMaxLength = 256;
            public const int UserNameMaxLength = 256;
        }

        public static class StoreLocation
        {
            public const int NameMaxLength = 100;
            public const int GoogleMapsUrlLength = 500;
            public const int AddressMaxLength = 300;
            public const int PhoneMaxLength = 13;
        }

        public static class Category
        {
            public const int NameMaxLength = 50;
            public const string CategoryNameRegex = @"^[A-Za-z\s\-]{2,50}$";
            public const string CategoryNameRegexErrorMessage = "Category name must contain only letters, spaces, or hyphens (2–50 characters).";
        }

        public static class Store
        {

            public const string NameRequired = "Name is required.";


            public const int NameMaxLength = 50;
            public const string StoreNameRegex = @"^[a-zA-Z\s]*$";
            public const string StoreNameRegexErrorMessage = "Name is invalid.";
            
            public const string NameLengthErrorMessage = "Name can't exceed 50 characters.";

            public const string GoogleMapsErrorMessage = "Google Maps URL is required.";
            public const string GoogleMapsUrlErrorMessage = "Please enter a valid URL.";
            public const string GoogleMapsUrlLengthErrorMessage = "URL can't exceed 50 characters.";
            public const string GoogleMapsDisplay = "Google Maps URL";

            public const int AddressMaxLength = 300;
            public const string AddressErrorMessage = "Address is required.";
            public const string AddressErrorMessageErrorMessage = $"Address can't exceed 300 characters.";


            public const string PhoneNumberRegex = @"^\+?(\d[\d\s\-().]{7,}\d)$";
            public const string PhoneErrorMessage = "Phone number is required.";
            
            
            public const int GoogleMapsUrlLength = 1000;
        }
        
        public static class Cart
        {
            public const int FullNameMaxLength = 100;
            public const int EmailMaxLength = 255;
            public const int PhoneMaxLength = 9;
            public const int AddressMaxLength = 200;
            public const int CityMaxLength = 100;
            public const int ZipCodeMaxLength = 20;

        }

        public static class General
        {
            public const string NoImageUrl = "no-image.jpg";

            
            public const string FullNameRegex = @"^[a-zA-Z\s]*$";
            public const string FullNameDisplay = "Full Name";

            public const string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            public const string EmailRegexErrorMessage = "Email is invalid.";

            public const string PhoneRegex = @"^[1-9][0-9]{8}$";
            public const string PhoneDisplay = "Phone Number";
            public const string PhoneRegexErrorMessage = "Phone number must be 9 digits and not start with 0.";
            
            public const string CountryCodeErrorMessage = "Please select a country code.";
            public const string CountryCodeDisplay = "Country Code";

            public const string AddressRegex = @"^[a-zA-Z0-9\s,'\-\.#\/]{5,100}$";
            public const string AddressRegexErrorMessage = "Address contains invalid characters.";

            public const string DeliveryOptionRequiredErrorMessage = "Please select a delivery option.";
            public const string DeliveryOptionDisplayName = "Delivery Options";

            public const string CityRegex = @"^[a-zA-Z\s'\-]{2,50}$";
            public const string CityRegexErrorMessage = "City name contains invalid characters.";

            public const string ZipCodeDisplay = "Zip Code";
            public const string ZipCodeRegex = @"^\d{5}$";
            public const string ZipCodeRegexErrorMessage = "Zip code must be exactly 5 digits.";

        }

    }
}
