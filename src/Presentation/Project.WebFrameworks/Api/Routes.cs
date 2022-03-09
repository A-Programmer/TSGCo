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

        public static class Login
        {
            public const string SignIn = "";
        }
        public static class Register
        {
            public const string SignUp = "";
        }
        

        // TODO: Delete Later
        public static class Account
        {

            public const string Login = "login";
            public const string RefreshToken = "refresh_token";
            public const string Logout = "logout";
            public const string Register = "register";
            public const string IsValidUser = "isvalid";
            public const string Profile = "profile";
            public const string UpdateProfile = "update_profile";
            public const string ChangePassword = "change_passwrod";
            public const string Dashboard = "dashboard";
            public const string UserWallets = "wallets";
            public const string CreateWalletsIfNotExist = "create_wallets_if_not_exist";

            //Verifications:
            public const string CheckMobileVerificationCode = "verifications/confirmation/mobile";
            public const string CheckPhoneVerificationCode = "verifications/confirmation/phone";
            public static class AddOrUpdateVerifications
            {
                public const string AddMobileVerification = "verifications/mobile";
                public const string AddPhoneVerification = "verifications/phone";
                public const string AddIdVerification = "verifications/id";
                public const string AddCreditCardVerification = "verifications/credit_card";
                public const string AddAgreementVerification = "verifications/agreement";
            }

            public static class GetVerifications
            {
                public const string GetAllVerifications = "verifications";
                public const string GetById = "verifications/{id}";
                public const string GetMobileVerification = "verifications/mobile";
                public const string GetPhoneVerification = "verifications/phone";
                public const string GetIdVerification = "verifications/id";
                public const string GetCreditCardVerification = "verifications/credit_card";
                public const string GetAgreementVerification = "verifications/agreement";
            }

            public static class DeleteVerifications
            {

                public const string DeleteById = "verifications/{id}";
                public const string DeleteMobileVerification = "verifications/mobile";
                public const string DeletePhoneVerification = "verifications/phone";
                public const string DeleteIdVerification = "verifications/id";
                public const string DeleteCreditCardVerification = "verifications/credit_card";
                public const string DeleteAgreementVerification = "verifications/agreement";
            }

        }

        public static class Users
        {

            public static class Get
            {
                public const string GetAllUsers = "";
                public const string GetUserById = "{id}";

                public static class Verifications
                {
                    public const string GetAll = "{user_id}/verifications";
                    public const string GetById = "{user_id}/verifications/{id}";
                }

                public static class Transactions
                {
                    public const string GetUserTransactions = "{user_id}/transactions";
                }

                public static class Wallets
                {
                    public const string GetUserWallets = "{user_id}/wallets";
                }

            }

            public static class Post
            {
                public const string AddUser = "";
                public const string AddOrUpdateProfile = "{user_id}/profile";

                public static class Verifications
                {
                    public const string Add = "{user_id}/verifications";
                    public const string AddMobileVerification = "{user_id}/verifications/mobile";
                    public const string AddPhoneVerification = "{user_id}/verifications/phone";
                    public const string AddIdVerification = "{user_id}/verifications/id";
                    public const string AddCreditCardVerification = "{user_id}/verifications/credit_card";
                    public const string AddAgreementVerification = "{user_id}/verifications/agreement";
                }
            }

            public static class Put
            {
                public const string EditUser = "{id}";

                public static class Verifications
                {
                    public const string Update = "{user_id}/verifications";
                    public const string ConfirmMobileVerification = "{user_id}/verifications/mobile";
                    public const string ConfirmPhoneVerification = "{user_id}/verifications/phone";
                    public const string ConfirmIdVerification = "{user_id}/verifications/id";
                    public const string ConfirmCreditCardVerification = "{user_id}/verifications/credit_card";
                    public const string ConfirmAgreementVerification = "{user_id}/verifications/agreement";

                }
            }

            public static class Delete
            {
                public const string DeleteUser = "{id}";

                public static class Verifications
                {
                    public const string Delete = "{user_id}/verifications";
                }
            }
        }

        public static class Verifications
        {
            public static class Get
            {
                public const string GetAll = "";
                public const string GetById = "{id}";
            }

            public static class Post
            {
                public const string AddMobileVerification = "mobile";
                public const string AddPhoneVerification = "phone";
                public const string AddIdVerification = "id";
                public const string AddCreditCardVerification = "credit_card";
                public const string AddAgreementVerification = "agreement";
            }

            public static class Put
            {
                public const string UpdateMobileVerification = "{id}/mobile";
                public const string UpdatePhoneVerification = "{id}/phone";
                public const string UpdateIdVerification = "{id}/id";
                public const string UpdateCreditCardVerification = "{id}/credit_card";
                public const string UpdateAgreementVerification = "{id}/agreement";
            }

            public static class Delete
            {
                public const string DeleteVerification = "{id}";
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


        public static class CategoryPosts
        {
            public static class Get
            {
                public const string GetAll = "";
                //public const string GetById = "{id:guid}";
                public const string GetBySlug = "{slug}";

                public static class PostCategoryPosts
                {
                    public static class Get
                    {
                        public const string GetPostCategoryPosts = "{categoryId:guid}/posts";
                    }
                }
            }

            public static class Post
            {
                public const string AddPostCategory = "";
            }
            public static class Put
            {
                public const string EditPostCategory = "{id:guid}";
            }
            public static class Delete
            {
                public const string DeleteCategory = "{id:guid}";
            }
        }

        public static class Posts
        {
            public static class Get
            {
                public const string GetAll = "";
                //public const string GetById = "{id:guid}";
                public const string GetBySlug = "{slug}";

                public static class PostComments
                {
                    public const string GetPostComments = "{slug}/comments";
                    public const string GetPostCommentById = "{slug}/comments/{id}";

                }
            }
            public static class Post
            {
                public const string AddPost = "";


                public static class PostComments
                {
                    public const string AddComment = "{post_id}/comments";
                }
            }
            public static class Put
            {
                public const string EditPost = "{id:guid}";

                public static class PostComments
                {
                    public const string Put = "{slug}/comments/{id}";
                }
            }
            public static class Delete
            {
                public const string DeletePost = "{id:guid}";

                public static class PostComments
                {
                    public const string DeleteComment = "{post_id}/comments/{id}";
                }
            }
        }

        public static class PostComments
        {
            public static class Get
            {
                public const string GetAll = "";
                public const string GetById = "{id:guid}";
                public const string Slides = "slides";
            }
            public const string AddComment = "";
            public const string UpdateComment = "";
            public const string DeleteComment = "{id:guid}";
        }

        public static class PostKeywords
        {
            public static class Get
            {
                public const string GetAll = "";
                public const string GetBySlug = "{slug}";
            }
            public const string AddKeyword = "";
            public const string UpdateKeyword = "";
            public const string DeleteKeyword = "{id:guid";
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

        public static class Announcements
        {

            public static class Get
            {
                public const string GetAll = "";
                //public const string GetById = "{id:guid}";
                public const string GetBySlug = "{slug}";

            }
            public static class Post
            {
                public const string Add = "";
            }
            public static class Put
            {
                public const string Edit = "{id:guid}";
            }
            public static class Delete
            {
                public const string Remove = "{id:guid}";
            }
        }

        public static class Certificates
        {

            public static class Get
            {
                public const string GetAll = "";
                public const string GetById = "{id:guid}";
            }
            public static class Post
            {
                public const string AddCertificate = "";
            }
            public static class Put
            {
                public const string EditCertificate = "{id:guid}";
            }
            public static class Delete
            {
                public const string DeleteCertificate = "{id:guid}";
            }
        }

        public static class Agencies
        {

            public static class Get
            {
                public const string GetAll = "";
                public const string GetById = "{id:guid}";
            }
            public static class Post
            {
                public const string AddAgency = "";
            }
            public static class Put
            {
                public const string EditAgency = "{id:guid}";
            }
            public static class Delete
            {
                public const string DeleteAgency = "{id:guid}";
            }
        }

        public static class Menus
        {

            public static class Get
            {
                public const string GetAll = "";
                public const string GetById = "{id:guid}";
            }
            public static class Post
            {
                public const string AddMenu = "";
            }
            public static class Put
            {
                public const string EditMenu = "{id:guid}";
            }
            public static class Delete
            {
                public const string DeleteMenu = "{id:guid}";
            }
        }

        public static class DynamicPages
        {

            public static class Get
            {
                public const string GetAll = "";
                //public const string GetById = "{id:guid}";
                public const string GetBySlug = "{slug}";

            }
            public static class Post
            {
                public const string Add = "";
            }
            public static class Put
            {
                public const string Edit = "{id:guid}";
            }
            public static class Delete
            {
                public const string Remove = "{id:guid}";
            }
        }

        public static class Slides
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
            public static class Put
            {
                public const string Edit = "{id:guid}";
            }
        }


        public static class Tickets
        {

            public static class Get
            {
                public const string GetAll = "";
                public const string GetByIdOrGetByTrackingCode = "{id}";

                public static class Replies
                {
                    public const string GetReplies = "{ticket_id}/replies";
                    public const string GetReplyById = "{ticket_id}/replies/{id:guid}";
                }

                public static class Attachments
                {
                    public const string GetAttachments = "{ticket_id}/attachments";
                    public const string GetAttachmentById = "{ticket_id}/attachments/{id:guid}";

                }
            }
            public static class Post
            {
                public const string Add = "";

                public static class Replies
                {
                    public const string PostReplies = "{ticket_id}/replies";
                }

                public static class Attachments
                {
                    public const string PostAttachments = "{ticket_id}/attachments";
                }
            }

            public static class Put
            {
                public const string Edit = "{id:guid}";
            }

            public static class Delete
            {
                public const string Remove = "{id:guid}";

                public static class Replies
                {
                    public const string RemoveReplies = "{ticket_id}/replies/{id:guid}";
                }

                public static class Attachments
                {
                    public const string RemoveAttachments = "{ticket_id}/attachments/{id:guid}";
                }
            }
        }

        public static class TicketCategories
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
            public static class Put
            {
                public const string Edit = "{id:guid}";
            }
        }

        public static class StaticContents
        {

            public static class Get
            {
                public const string GetAll = "";
                public const string GetByTitle = "{title}";

            }
            public static class Post
            {
                public const string Add = "";
            }
            public static class Put
            {
                public const string Edit = "{id:guid}";
            }
            public static class Delete
            {
                public const string Remove = "{id:guid}";
            }
        }

        public static class Transactions
        {
            public static class Get
            {
                public const string GetAll = "";
                public const string GetByTrackingCode = "{tracking_code}";
            }

            public static class Post
            {
                public const string AddTransaction = "";
            }
        }

        public static class Wallets
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
        }

        public static class Currencies
        {
            public static class Get
            {
                public const string GetAll = "";
                public const string GetBySymbol = "{symbol}";
            }
            public static class Post
            {
                public const string Add = "";
            }

            public static class Put
            {
                public const string Update = "{id}";
            }

            public static class Delete
            {
                public const string Remove = "{id}";
            }
        }


        public static class Orders
        {

            public static class Get
            {
                public const string GetAll = "";
                public const string GetById = "{id}";
            }
            public static class Post
            {
                public const string AddBuyPerfectMoneyOrder = "add_buy_order_with_perfect_money";
                public const string AddSellPerfectMoneyOrder = "add_sell_order_to_perfect_money";
                public const string AddBuyTomanOrder = "add_buy_order_with_toman";
                public const string AddSellTomanOrder = "add_sell_order_to_toman";
                public const string AddBuyCryptoWalletOrder = "add_buy_order_with_crypto_wallet";
                public const string AddSellCryptoWalletOrder = "add_sell_order_to_crypto_wallet";
                public const string Convert = "convert";
            }

            public static class Put
            {
                public const string Edit = "{id:guid}";
            }

            public static class Delete
            {
                public const string Remove = "{id:guid}";
            }
        }
    }
}
