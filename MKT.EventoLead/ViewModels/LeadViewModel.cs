namespace MKT.EventoLead.WebApp.ViewModels;

public class LeadViewModel
{
    public bool? B2B { get; set; }
  //  public string? FullName { get; set; }
    public string? BuyerName { get; set; } = null;
    public string? Email { get; set; }
    //public DateTime? BirthDate { get; set; }
   // public long? IDNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
   // public string? State { get; set; }
    public string? ZIPCode { get; set; }
    public string? Company { get; set; }
    public long? TAXId { get; set; }
    public string? City { get; set; }
    //public string? Citizenship { get; set; }
    public string? Country { get; set; }
    public string? AccountingName { get; set; }
    public string? AccountingPhone { get; set; }
    public string? AccountingEmail { get; set; }

    public string? DeliveryContact  { get; set; }
    public string? DeliveryAddress { get; set; }
    public string? DeliveryZipCode { get; set; }
    public string? DeliveryPhone { get; set; }
    public string? DeliveryEmail { get; set; }
    public string? Discount { get; set; }
    public string? Terms { get; set; }
    public List<Product> Products { get; set; }

    public string? Currency {  get; set; }
    public string StaffName { get; set; }
    public string Notes { get; set; }
}

//public class LeadViewModelComoJSON
//{
//    public bool? B2B { get; set; }
//    public string? FullName { get; set; }
//    public string? Email { get; set; }
//    public DateTime? BirthDate { get; set; }
//    public long? IDNumber { get; set; }
//    public string? PhoneNumber { get; set; }
//    public string? Address { get; set; }
//    public string? State { get; set; }
//    public long? ZIPCode { get; set; }
//    public string? Company { get; set; }
//    public long? TAXId { get; set; }
//    public string? City { get; set; }
//    public string? Citizenship { get; set; }
//    public string? Country { get; set; }
//    public bool PrivacyPolicyCheck { get; set; }
//    public bool CommunicationCheck { get; set; }
//}

public class Product
{
    public long Id { get; set; }
    public string SKU { get; set; }
    public string DESCRIPTON { get; set; }
    public decimal? UNITPRICE { get; set; }
    public  decimal? unitPriceRetail { get; set; }
    public int? QTY { get; set; }
    public decimal? TOTAL { get; set; }
    public string SETDISCOUNT { get; set; }   
}

//public static class LeadViewModelExtensions
//{
//    public static LeadViewModelComoJSON ParaViewModelComoJSON(this LeadViewModel leadViewModel)
//    {
//        return new LeadViewModelComoJSON
//        {
//            B2B = leadViewModel.B2B,
//            FullName = leadViewModel.FullName,
//            Email = leadViewModel.Email,
//            BirthDate = leadViewModel.BirthDate,
//            IDNumber = leadViewModel.IDNumber,
//            PhoneNumber = leadViewModel.PhoneNumber,
//            Address = leadViewModel.Address,
//            State = leadViewModel.State,
//            ZIPCode = leadViewModel.ZIPCode,
//            Company = leadViewModel.Company,
//            TAXId = leadViewModel.TAXId,
//            City = leadViewModel.City,
//            Citizenship = leadViewModel.Citizenship,
//            Country = leadViewModel.Country,
//            PrivacyPolicyCheck = leadViewModel.PrivacyPolicyCheck,
//            CommunicationCheck = leadViewModel.CommunicationCheck
//        };
//    }
//}