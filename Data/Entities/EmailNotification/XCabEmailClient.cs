namespace Data.Entities.EmailNotification
{
    public class XCabEmailClient
    {
        public int Id { get; set; }
        public int LoginId { get; set; }
        public string AccountCode { get; set; }
        public int StateId { get; set; }
        public string EmailRecipientList { get; set; }
        public string CCList { get; set; }

        public string InternalEmailListWithoutAttachments { get; set; }
        public string EmailTitlePrefix { get; set; }
        public string WebApiKey { get; set; }
        public string Sha1HashValue { get; set; }
        public int BusinessBrand { get; set; }

        public string BrandLogoFilename { get; set; }

        public string ClientName { get; set; }
        public string ClientAddress { get; set; }

        public string BrandUrl { get; set; }

        public string BrandPhoneNumber { get; set; }

        public string BrandEmailSenderAddress { get; set; }

        public string BrandName { get; set; }
    }
}
