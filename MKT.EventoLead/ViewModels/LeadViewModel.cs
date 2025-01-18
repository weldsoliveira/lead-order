namespace MKT.EventoLead.WebApp.ViewModels;

public class LeadViewModel
{
    public bool? B2B { get; set; }
    public string? BuyerName { get; set; } = null;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? ZIPCode { get; set; }
    public string? Company { get; set; }
    public string? TAXId { get; set; }
    public string? City { get; set; }
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
    public string? State { get; set; }
    public string? DeliveryState { get; set; }
}

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
