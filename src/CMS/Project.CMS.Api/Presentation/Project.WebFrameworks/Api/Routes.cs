using System;
namespace Project.WebFrameworks.Api
{
    public static class Routes
    {
        public const string BaseRootAddress = "api/v{version:apiVersion}/[controller]";

        public static class Tests
        {
            public const string Get = "";
        }

        public static class Authentication
        {
            public const string Login = "login";
        }

        public static class Registration
        {
            public const string Register = "register";
        }

        public static class Account
        {

            public const string Logout = "logout";
            public const string IsValidUser = "isvalid";
            public const string RefreshToken = "refresh_token";
            public const string Profile = "profile";
            public const string UpdateProfile = "update_profile";
            public const string ChangePassword = "change_passwrod";
            
        }

        public static class Users
        {

            public static class Get
            {
                public const string GetAllUsers = "";
                public const string GetUserById = "{id}";
            }

            public static class Post
            {
                public const string AddUser = "";
                public const string AddOrUpdateProfile = "{user_id}/profile";
            }

            public static class Put
            {
                public const string EditUser = "{id}";
            }

            public static class Delete
            {
                public const string DeleteUser = "{id}";
            }
        }

        public static class Roles
        {

            public static class Get
            {
                public const string GetAll = "";
                public const string GetById = "{id}";

            }

            public static class Post
            {
                public const string Add = "";
            }

            public static class Put
            {
                public const string Edit = "{id}";
            }

            public static class Delete
            {
                public const string Remove = "{id}";
            }
        }

        public static class ContactUs
        {

            public static class Get
            {
                public const string GetAll = "";
                public const string GetById = "{id:guid}";
            }
            public static class Post
            {
                public const string Add = "";
            }
            public static class Delete
            {
                public const string Remove = "{id:guid}";
            }
        }

    }
}
