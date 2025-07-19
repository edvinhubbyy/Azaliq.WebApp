namespace Azaliq.GCommon
{
    public static class ValidationConstants
    {
        public static class Product
        {
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
            public const int ImageUrlMaxLength = 255;
        }

        public static class ProductTag
        {
            public const int NameMaxLength = 50;
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
        }

        public static class Store
        {
            public const int NameMaxLength = 50;
            public const string PhoneNumberRegex = @"^\+?(\d[\d\s\-().]{7,}\d)$";
            public const int GoogleMapsUrlLength = 1000;
            public const int AddressMaxLength = 300;
            public const int PhoneMaxLength = 9;
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
            public const string IsDeletedPropertyName = "IsDeleted";
            public const string NoImageUrl = "no-image.jpg";

        }

    }
}
