using Microsoft.AspNetCore.Mvc;
using MKT.EventoLead.Domain.Entities;
using MKT.EventoLead.Domain.Interfaces.Repository;
using MKT.EventoLead.WebApp.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace MKT.EventoLead.WebApp.Controllers
{
    public class CustomerRegistrationController : Controller
    {
        private readonly ILeadRepository leadRepository;
        private readonly IProductRepository productRepository;

        public CustomerRegistrationController(ILeadRepository leadRepository, IProductRepository productRepository)
        {
            this.leadRepository = leadRepository;
            this.productRepository = productRepository;
        }

        public IActionResult B2B(string currency = "EUR")
        {
            LeadViewModel model = new LeadViewModel();
            var produtos = productRepository.GetAllProduct(currency);

            var produtosPriceList = new List<ViewModels.Product>();
            foreach (Domain.Entities.ProductPrice item in produtos)
            {
                ViewModels.Product product = new ViewModels.Product();
                product.Id = item.IdProduct;
                product.SKU = item.Product.sku;
                product.DESCRIPTON = item.Product.description;
                product.UNITPRICE = item.Price;
                product.unitPriceRetail = item.unitPriceRetail;
                product.SETDISCOUNT = item.Product.setDiscount;
                produtosPriceList.Add(product);
            }

            model.Products = produtosPriceList.OrderBy(x => x.DESCRIPTON).ToList();

            return View(model);
        }

        private void EnviarEmail(List<string> to, string body, string subject, List<string> copiaOculta)
        {
            try
            {
                var client = new GranadoPhebo.Api.Mailer.Client.Service.GranadoMailClient();
                var emailData = new GranadoPhebo.Api.Mailer.Domain.DTO.EmailDataDTO();

                emailData.Subject = subject;
                emailData.SourceApplication = "Order Placement";
                emailData.Body = body;
                emailData.To = to.ToArray();
                emailData.Bcc = copiaOculta.ToArray();
                emailData.ShouldUseTemplate = false;

                var result = client.SendEmail(emailData).Result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IActionResult UpdateProductsByCurrency(string currency)
        {
            LeadViewModel model = new LeadViewModel();
            var produtos = productRepository.GetAllProduct(currency);

            var produtosPriceList = new List<ViewModels.Product>();
            foreach (Domain.Entities.ProductPrice item in produtos)
            {
                ViewModels.Product product = new ViewModels.Product();
                product.Id = item.IdProduct;
                product.SKU = item.Product.sku;
                product.DESCRIPTON = item.Product.description;
                product.UNITPRICE = item.Price;
                product.unitPriceRetail = item.unitPriceRetail;
                product.SETDISCOUNT = item.Product.setDiscount;
                produtosPriceList.Add(product);
            }

            model.Products = produtosPriceList.OrderBy(x => x.DESCRIPTON).ToList();

            return PartialView("_PartialProduct", model);
            ;
        }

        public IActionResult Consumer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeadViewModel leadViewModel)
        {
            AttachProdutos(leadViewModel);

            var orderCreated = leadViewModel;

            string leadViewModelComoJSONString = JsonConvert.SerializeObject(leadViewModel);

            Lead lead = new()
            {
                JsonLead = leadViewModelComoJSONString,
                IdCampanha = 11,
            };

            if (leadViewModel != null && leadViewModel.Products.Any())
            {

                  leadRepository.Insert(lead);
                string email = leadViewModel.Email;
                leadViewModel = new();
                leadViewModel.Products = new();
                List<string> dest = new List<string>
{

    email ?? "granado@granado.com.br"
};

                List<string> destCC = new List<string>
{

    "labreu@granado.com.br",
    "sgiraudier@granado.fr",
    "gdemetz@granado.fr",
    "mdcruz@granado.fr",
    "larmani@granado.com.br",
    "epilatti@granado.com.br",
    "aluiz@granadophebo.com.br",
    "jfraga@granadophebo.com.br",
    "wsousa@granadophebo.com.br",
    "dparaujo@granadophebo.com.br",
    "lgrion@granadophebo.com.br"
};

                EnviarEmail(dest, GenerateEmailBody(orderCreated), "Granado: Your order is received!", destCC);
            }

            TempData["Sucesso"] = true;
            return RedirectToAction("B2B", "CustomerRegistration", new { currency = leadViewModel.Currency });
        }


        private LeadViewModel AttachProdutos(LeadViewModel leadViewModel)
        {
            var idProdutoString = Request.Form["OrderProdutoListID"].ToString();
            var valorProdutoString = Request.Form["ProdutoListQuantidade"].ToString();
            leadViewModel.Products = new List<ViewModels.Product>();

            if (!string.IsNullOrEmpty(idProdutoString) && !string.IsNullOrEmpty(valorProdutoString))
            {

                var idProduto = idProdutoString.Split(',');
                var valorProduto = valorProdutoString.Split(',');

                if (idProduto.Length == valorProduto.Length)
                {
                    for (int i = 0; i < idProduto.Length; i++)
                    {
                        if (int.TryParse(idProduto[i], out int id) && int.TryParse(valorProduto[i], out int qty))
                        {

                            var produtoPriceAtual = productRepository.GetByIdProduct(id);
                            if (produtoPriceAtual != null && qty > 0)
                            {
                                var productNew = new MKT.EventoLead.WebApp.ViewModels.Product
                                {
                                    Id = produtoPriceAtual.Product.idProduct,
                                    DESCRIPTON = produtoPriceAtual.Product.description,
                                    SETDISCOUNT = produtoPriceAtual.Product.setDiscount,
                                    SKU = produtoPriceAtual.Product.sku,
                                    UNITPRICE = produtoPriceAtual.Price,
                                    unitPriceRetail = produtoPriceAtual.unitPriceRetail,
                                    QTY = qty,
                                    TOTAL = qty * produtoPriceAtual.Price
                                };

                                if (!leadViewModel.Products.Contains(productNew))
                                {
                                    leadViewModel.Products.Add(productNew);
                                }
                            }
                        }
                    }
                }

            }


            return leadViewModel;
        }
        public string GenerateEmailBody(LeadViewModel model)
        {
            var html = new StringBuilder();

            // Cabeçalho do e-mail
            html.Append("<h1>Order Placement Details</h1>");

            html.Append("<table style='border-collapse: collapse; width: 100%;'>");
            html.Append("<thead>");
            html.Append("<tr style='background-color: #f2f2f2;'>");
            html.Append("<th style='border: 1px solid #ddd; padding: 8px;'>Details</th>");
            html.Append("<th style='border: 1px solid #ddd; padding: 8px;'>Value</th>");
            html.Append("</tr>");
            html.Append("</thead>");
            html.Append("<tbody>");

            if (!string.IsNullOrEmpty(model.Company))
                html.Append(CreateRow("Company/Store Name", model.Company));
            if (!string.IsNullOrEmpty(model.TAXId))
                html.Append(CreateRow("Tax ID (VAT)", model.TAXId));
            if (!string.IsNullOrEmpty(model.Address))
                html.Append(CreateRow("Address", model.Address));
            if (!string.IsNullOrEmpty(model.ZIPCode))
                html.Append(CreateRow("Zip Code", model.ZIPCode.ToString()));
            if (!string.IsNullOrEmpty(model.City))
                html.Append(CreateRow("City", model.City));
            if (!string.IsNullOrEmpty(model.State))
                html.Append(CreateRow("State", model.State));
            if (!string.IsNullOrEmpty(model.Country))
                html.Append(CreateRow("Country", model.Country));
            if (!string.IsNullOrEmpty(model.BuyerName))
                html.Append(CreateRow("Buyer Name", model.BuyerName));
            if (!string.IsNullOrEmpty(model.Email))
                html.Append(CreateRow("Email", model.Email));
            if (!string.IsNullOrEmpty(model.PhoneNumber))
                html.Append(CreateRow("Phone Number", model.PhoneNumber));
            if (!string.IsNullOrEmpty(model.Terms))
                html.Append(CreateRow("Terms", model.Terms));
            if (!string.IsNullOrEmpty(model.Discount))
                html.Append(CreateRow("Discount (%)", model.Discount));
            if (!string.IsNullOrEmpty(model.AccountingName))
                html.Append(CreateRow("Accounting Name", model.AccountingName));

            if (!string.IsNullOrEmpty(model.AccountingPhone))
                html.Append(CreateRow("Accounting Phone", model.AccountingPhone));
            if (!string.IsNullOrEmpty(model.DeliveryState))
                html.Append(CreateRow("Delivery State", model.DeliveryState));

            if (!string.IsNullOrEmpty(model.AccountingEmail))
                html.Append(CreateRow("Accounting E-mail", model.AccountingEmail));


            if (!string.IsNullOrEmpty(model.DeliveryContact))
                html.Append(CreateRow("Delivery Contact", model.DeliveryContact));

            if (!string.IsNullOrEmpty(model.DeliveryAddress))
                html.Append(CreateRow("Delivery Address", model.DeliveryAddress));

            if (!string.IsNullOrEmpty(model.DeliveryZipCode))
                html.Append(CreateRow("Zip Code", model.DeliveryZipCode));

            if (!string.IsNullOrEmpty(model.DeliveryZipCode))
                html.Append(CreateRow("Phone", model.DeliveryZipCode));

            if (!string.IsNullOrEmpty(model.DeliveryEmail))
                html.Append(CreateRow("E-mail", model.DeliveryEmail));

            if (!string.IsNullOrEmpty(model.StaffName))
                html.Append(CreateRow("Staff Name", model.StaffName));
            if (!string.IsNullOrEmpty(model.Notes))
                html.Append(CreateRow("Notes", model.Notes));

            html.Append("</tbody>");
            html.Append("</table>");

            // Adicionar produtos com moeda e somatório
            if (model.Products != null && model.Products.Any())
            {
                html.Append("<h2>Products</h2>");
                html.Append("<table style='border-collapse: collapse; width: 100%;'>");
                html.Append("<thead>");
                html.Append("<tr style='background-color: #f2f2f2;'>");
                html.Append("<th style='border: 1px solid #ddd; padding: 8px;'>SKU</th>");
                html.Append("<th style='border: 1px solid #ddd; padding: 8px;'>Description</th>");
                html.Append("<th style='border: 1px solid #ddd; padding: 8px;'>Unit Price</th>");
                html.Append("<th style='border: 1px solid #ddd; padding: 8px;'>Quantity</th>");
                html.Append("<th style='border: 1px solid #ddd; padding: 8px;'>Total</th>");
                html.Append("</tr>");
                html.Append("</thead>");
                html.Append("<tbody>");

                decimal grandTotal = 0;

                foreach (var product in model.Products.Where(p => p.QTY > 0))
                {
                    decimal productTotal = (product.QTY ?? 0) * (product.UNITPRICE ?? 0);
                    grandTotal += productTotal;

                    html.Append("<tr>");
                    html.AppendFormat("<td style='border: 1px solid #ddd; padding: 8px;'>{0}</td>", product.SKU);
                    html.AppendFormat("<td style='border: 1px solid #ddd; padding: 8px;'>{0}</td>", product.DESCRIPTON);
                    html.AppendFormat("<td style='border: 1px solid #ddd; padding: 8px;'>{0} {1}</td>", model.Currency, product.UNITPRICE);
                    html.AppendFormat("<td style='border: 1px solid #ddd; padding: 8px;'>{0}</td>", product.QTY);
                    html.AppendFormat("<td style='border: 1px solid #ddd; padding: 8px;'>{0} {1}</td>", model.Currency, productTotal);
                    html.Append("</tr>");
                }

                // Adicionar linha do somatório
                html.Append("<tr style='font-weight: bold; background-color: #f9f9f9;'>");
                html.Append("<td colspan='4' style='border: 1px solid #ddd; padding: 8px; text-align: right;'>Grand Total:</td>");
                html.AppendFormat("<td style='border: 1px solid #ddd; padding: 8px;'>{0} {1}</td>", model.Currency, grandTotal);
                html.Append("</tr>");

                html.Append("</tbody>");
                html.Append("</table>");
            }

            return html.ToString();
        }

        private string CreateRow(string field, string value)
        {
            return $"<tr><td style='border: 1px solid #ddd; padding: 8px;'>{field}</td><td style='border: 1px solid #ddd; padding: 8px;'>{value}</td></tr>";
        }


    }
}
