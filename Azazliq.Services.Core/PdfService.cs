using Azaliq.Data.Models.Models;
using Azaliq.Services.Core.Contracts;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;
using Document = iTextSharp.text.Document;
using PageSize = iTextSharp.text.PageSize;

namespace Azaliq.Services.Core
{
    public class PdfService : IPdfService
    {
        private const decimal EuroToBgnRate = 1.95583m;

        public async Task<byte[]> GenerateOrderPdfWithQrAsync(Order order)
        {
            using var ms = new MemoryStream();
            Document doc = new Document(PageSize.A4, 40, 40, 80, 50);
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            // Colors
            var darkGreen = new BaseColor(0, 100, 0);
            var softPink = new BaseColor(255, 192, 203);
            var deepPurple = new BaseColor(102, 0, 102);
            var lightGray = new BaseColor(230, 230, 230);

            // Fonts
            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 22, darkGreen);
            var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, deepPurple);
            var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.BLACK);
            var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, BaseColor.BLACK);
            var smallItalicFont = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9, BaseColor.GRAY);

            // Flower Shop Title with underline
            var title = new Paragraph("🌸 Azaliq Flower Shop 🌸", titleFont)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 15f
            };
            doc.Add(title);

            // Invoice header with soft pink background
            var headerTable = new PdfPTable(1) { WidthPercentage = 100 };
            var headerCell = new PdfPCell(new Phrase("INVOICE", headerFont))
            {
                BackgroundColor = softPink,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 10,
                Border = Rectangle.NO_BORDER
            };
            headerTable.AddCell(headerCell);
            doc.Add(headerTable);

            doc.Add(new Paragraph($"Invoice Number: #{order.Id}", normalFont));
            doc.Add(new Paragraph($"Date Issued: {order.OrderDate.ToLocalTime():dd.MM.yyyy}", normalFont));
            doc.Add(new Paragraph(" "));

            // Seller Info in a colored box
            var sellerTable = new PdfPTable(1) { WidthPercentage = 50, HorizontalAlignment = Element.ALIGN_LEFT };
            var sellerCell = new PdfPCell
            {
                BackgroundColor = lightGray,
                Padding = 8,
                Border = Rectangle.BOX
            };
            sellerCell.AddElement(new Phrase("Seller:", boldFont));
            sellerCell.AddElement(new Phrase("Azaliq Flower Shop", normalFont));
            sellerCell.AddElement(new Phrase("12 Bulgaria Blvd, Blagoevgrad", normalFont));
            sellerCell.AddElement(new Phrase("VAT ID: 123456789", normalFont));
            sellerCell.AddElement(new Phrase("Owner: Ivan Djambazki", normalFont));
            sellerTable.AddCell(sellerCell);
            doc.Add(sellerTable);

            doc.Add(new Paragraph(" "));

            // Buyer Info box with border and padding
            var buyerTable = new PdfPTable(1) { WidthPercentage = 50, HorizontalAlignment = Element.ALIGN_LEFT };
            var buyerCell = new PdfPCell
            {
                BackgroundColor = lightGray,
                Padding = 8,
                Border = Rectangle.BOX
            };
            buyerCell.AddElement(new Phrase("Buyer:", boldFont));
            buyerCell.AddElement(new Phrase(order.FullName, normalFont));
            buyerCell.AddElement(new Phrase($"Email: {order.Email}", normalFont));
            buyerCell.AddElement(new Phrase($"Phone: +359 {order.Phone}", normalFont));
            buyerTable.AddCell(buyerCell);
            doc.Add(buyerTable);

            doc.Add(new Paragraph(" "));

            // Products Table with colorful headers
            PdfPTable productTable = new PdfPTable(5)
            {
                WidthPercentage = 100,
                SpacingBefore = 10f,
                SpacingAfter = 20f
            };
            productTable.SetWidths(new float[] { 40f, 15f, 15f, 15f, 15f });

            void AddHeaderCell(string text)
            {
                var cell = new PdfPCell(new Phrase(text, boldFont))
                {
                    BackgroundColor = softPink,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 6
                };
                productTable.AddCell(cell);
            }

            AddHeaderCell("Product");
            AddHeaderCell("Quantity");
            AddHeaderCell("Price (BGN)");
            AddHeaderCell("Total (BGN)");
            AddHeaderCell("Price (EUR)");

            decimal total = 0m;

            foreach (var item in order.Products)
            {
                var productTotal = item.Product.Price * item.Quantity;
                total += productTotal;

                productTable.AddCell(new PdfPCell(new Phrase(item.Product.Name, normalFont)) { Padding = 5 });
                productTable.AddCell(new PdfPCell(new Phrase(item.Quantity.ToString(), normalFont)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 5 });
                productTable.AddCell(new PdfPCell(new Phrase(item.Product.Price.ToString("F2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5 });
                productTable.AddCell(new PdfPCell(new Phrase(productTotal.ToString("F2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5 });
                productTable.AddCell(new PdfPCell(new Phrase((productTotal / EuroToBgnRate).ToString("F2"), normalFont)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 5 });
            }

            // Total row with different background color
            var totalBgColor = new BaseColor(200, 255, 200); // Light green background
            var totalLabelCell = new PdfPCell(new Phrase("Total:", boldFont))
            {
                Colspan = 3,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                BackgroundColor = totalBgColor,
                Padding = 6
            };
            productTable.AddCell(totalLabelCell);

            productTable.AddCell(new PdfPCell(new Phrase($"{total:F2} BGN", boldFont))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                BackgroundColor = totalBgColor,
                Padding = 6
            });
            productTable.AddCell(new PdfPCell(new Phrase($"{(total / EuroToBgnRate):F2} EUR", boldFont))
            {
                HorizontalAlignment = Element.ALIGN_RIGHT,
                BackgroundColor = totalBgColor,
                Padding = 6
            });

            doc.Add(productTable);

            // Delivery details section
            var deliveryType = order.DeliveryOption == Data.Models.Models.Enum.DeliveryOptions.Courier
                ? "Courier Delivery"
                : "Pickup from Store";

            var deliveryInfo = new Paragraph($"Delivery method: {deliveryType}", boldFont)
            {
                SpacingBefore = 10f,
                SpacingAfter = 5f
            };
            doc.Add(deliveryInfo);

            if (order.PickupStore != null)
            {
                doc.Add(new Paragraph($"Pickup Store: {order.PickupStore.Name}", normalFont));
            }

            if (order.PickupTime.HasValue)
            {
                doc.Add(new Paragraph($"Pickup Date & Time: {order.PickupTime.Value.ToLocalTime():dd.MM.yyyy HH:mm}", normalFont));
            }

            // Footer
            var thankYou = new Paragraph("Thank you for choosing Azaliq! 🌷", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLDITALIC, darkGreen))
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingBefore = 30f
            };
            doc.Add(thankYou);

            doc.Close();
            return ms.ToArray();
        }
    }
}