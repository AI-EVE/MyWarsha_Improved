using System;
using System.ComponentModel;
using System.Reflection.Metadata;
using MyWarsha_DTOs.CategoryDTOs;
using MyWarsha_DTOs.ServiceDTOs;
using MyWarsha_Interfaces.RepositoriesInterfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Document = QuestPDF.Fluent.Document;

namespace MyWarsha_Services.PdfServices;

public class InvoiceRenderingService
{
    public InvoiceRenderingService()
    {
        QuestPDF.Settings.License = LicenseType.Community;
    }
    public byte[] GenerateInvoicePdf(ServiceDto service, List<CategoryDto> categories)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                page.Header()
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Column(column =>
                            {

                                // column.Item().Element(container =>
                                // {
                                //     container.Width(100)  // Set the desired width
                                //             .Height(50)  // Set the desired height
                                //             .Image("https://example.com/path/to/your/image.png");
                                // });
                                column.Item().Text("AutoZone")
                                    .FontSize(20)
                                    .FontFamily("Arial")
                                    .ExtraBlack()
                                    .Bold();

                                column.Item().Text("Address")
                                    .FontSize(10)
                                    .FontFamily("Arial")
                                    .ExtraBlack()
                                    .Bold();
                            });

                        row.RelativeItem()
                            .ShowOnce()
                            .Text("بيان السعر")
                            .AlignRight()
                            .FontFamily("Arial")
                            .ExtraBlack()
                            .FontSize(30);
                    });


                page.Content()
                    .PaddingTop(50)
                    .Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .Column(column2 =>
                                {
                                    column2.Item()
                                        .Text("For: " + service.Client.Name)
                                        .FontFamily("Arial")
                                        .FontSize(15)
                                        .Bold();

                                    if (!string.IsNullOrEmpty(service.Car.PlateNumber))
                                    {
                                        column2.Item()
                                            .Text("Plate: " + service.Car.PlateNumber)
                                            .FontFamily("Arial")
                                            .FontSize(15);
                                    }

                                    if (!string.IsNullOrEmpty(service.Car.ChassisNumber))
                                    {
                                        column2.Item()
                                            .Text("Chassis: " + service.Car.ChassisNumber)
                                            .FontFamily("Arial")
                                            .FontSize(15);
                                    }

                                    if (!string.IsNullOrEmpty(service.Car.MotorNumber))
                                    {
                                        column2.Item()
                                            .Text("Model: " + service.Car.MotorNumber)
                                            .FontFamily("Arial")
                                            .FontSize(15);
                                    }
                                });

                            row.RelativeItem().Column(column2 =>
                            {
                                column2.Item()
                                    .Text($"Invoice Number: {service.Id}")
                                    .AlignRight()
                                    .Bold();
                                column2.Item()
                                    .PaddingTop(2)
                                    .Text($"Date: {service.Date}")
                                    .AlignRight();
                            });

                        });


                        column.Item().PaddingTop(30).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.RelativeColumn();
                                columns.ConstantColumn(50);
                                columns.ConstantColumn(60);
                                columns.ConstantColumn(60);
                                columns.ConstantColumn(70);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("#").Bold();
                                header.Cell().Text("Name").Bold();
                                header.Cell().Text("Qty").AlignRight().Bold();
                                header.Cell().Text("Price").AlignRight().Bold();
                                header.Cell().Text("Discount").AlignRight().Bold();
                                header.Cell().Text("Total").AlignRight().Bold();

                                header.Cell()
                                    .ColumnSpan(6)
                                    .PaddingVertical(5)
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Black);
                                for (int i = 0; i < service.ProductsToSell.Count; i++)
                                {
                                    if (service.ProductsToSell[i].IsReturned)
                                    {
                                        continue;
                                    }
                                    var backgroundColor = i % 2 == 0 ? Color.FromHex("#ffffff") : Color.FromHex("#f0f0f0");

                                    var invoiceItem = service.ProductsToSell[i];
                                    var total = invoiceItem.TotalPriceAfterDiscount.ToString();
                                    var lengthOfTotal = total.Length;

                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text((i + 1).ToString());
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(invoiceItem.Product.Name);
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(invoiceItem.Count.ToString()).AlignRight();
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(invoiceItem.PricePerUnit.ToString()).AlignRight();
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(invoiceItem.Discount.ToString()).AlignRight();
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(total.Substring(0, lengthOfTotal - 2)).AlignRight();
                                }

                                table.Cell()
                                    .ColumnSpan(6)
                                    .PaddingVertical(5)
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Black);
                                var totalOfAll = service.ProductsToSell
                                .Where(p => !p.IsReturned)
                                .Sum(p => p.TotalPriceAfterDiscount).ToString();
                                var lengthOfTotalOfAll = totalOfAll.Length;
                                table.Cell().ColumnSpan(5).Text("Total").Bold().AlignRight();
                                table.Cell().Text(totalOfAll.Substring(0, lengthOfTotalOfAll - 2)).Bold().AlignRight();
                            });

                            column.Item()
                                .AlignBottom()
                                .PaddingTop(30)
                                .Text("Thank You for Trusting Us")
                                .FontFamily("Arial")
                                .FontSize(15)
                                .Bold();


                        });
                    });

                page.Footer()
                    .Column(column =>
                    {
                        column.Item()
                            .PaddingVertical(10)
                            .Text(text =>
                            {
                                text.Span("Page ");
                                text.CurrentPageNumber();
                                text.Span(" of ");
                                text.TotalPages();
                                text.AlignStart();
                            });
                    });
            });

            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);

                page.Header()
                    .Row(row =>
                    {
                        row.RelativeItem()
                            .Column(column =>
                            {
                                column.Item().Text("AutoZone")
                                    .FontSize(20)
                                    .FontFamily("Arial")
                                    .ExtraBlack()
                                    .Bold();

                                column.Item().Text("Address")
                                    .FontSize(10)
                                    .FontFamily("Arial")
                                    .ExtraBlack()
                                    .Bold();
                            });

                        row.RelativeItem()
                            .ShowOnce()
                            .Text("Invoice")
                            .AlignRight()
                            .FontFamily("Arial")
                            .ExtraBlack()
                            .FontSize(30);
                    });


                page.Content()
                    .PaddingTop(50)
                    .Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .Column(column2 =>
                                {
                                    column2.Item()
                                        .Text("For:")
                                        .Bold();
                                    column2.Item()
                                        .Text(service.Client.Name)
                                        .FontFamily("Arial")
                                        .FontSize(15)
                                        .Bold();
                                });

                            row.RelativeItem().Column(column2 =>
                            {
                                column2.Item()
                                    .Text($"Invoice Number: {service.Id}")
                                    .AlignRight()
                                    .Bold();
                                column2.Item()
                                    .PaddingTop(2)
                                    .Text($"Date: {service.Date}")
                                    .AlignRight();
                            });

                        });


                        column.Item().PaddingTop(30).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.ConstantColumn(60);
                                columns.ConstantColumn(60);
                                columns.ConstantColumn(70);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("#").Bold();
                                header.Cell().Text("Category").Bold();
                                header.Cell().Text("Description").Bold();
                                header.Cell().Text("Price").AlignRight().Bold();
                                header.Cell().Text("Discount").AlignRight().Bold();
                                header.Cell().Text("Total").AlignRight().Bold();

                                header.Cell()
                                    .ColumnSpan(6)
                                    .PaddingVertical(5)
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Black);
                                for (int i = 0; i < service.ServiceFees.Count; i++)
                                {

                                    if (service.ServiceFees[i].IsReturned)
                                    {
                                        continue;
                                    }

                                    var backgroundColor = i % 2 == 0 ? Color.FromHex("#ffffff") : Color.FromHex("#f0f0f0");

                                    var invoiceItem = service.ServiceFees[i];

                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text((i + 1).ToString());
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(categories.FirstOrDefault(c => c.Id == invoiceItem.CategoryId)?.Name);
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(invoiceItem.Notes);
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(invoiceItem.Price.ToString()).AlignRight();
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(invoiceItem.Discount.ToString()).AlignRight();
                                    table.Cell().Background(backgroundColor).PaddingTop(4).Text(invoiceItem.TotalPriceAfterDiscount.ToString()).AlignRight();
                                }

                                table.Cell()
                                    .ColumnSpan(6)
                                    .PaddingVertical(5)
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Black);

                                table.Cell().ColumnSpan(5).Text("Total").Bold().AlignRight();
                                table.Cell().Text(service.ServiceFees.
                                Where(sf => !sf.IsReturned).
                                Sum(sf => sf.TotalPriceAfterDiscount).ToString()).Bold().AlignRight();
                            });


                            // create a spacer that pushes the content to the bottom of the page




                            column.Item()
                                .AlignBottom()
                                .PaddingTop(30)
                                .Text("Thank You for Trusting Us")
                                .FontFamily("Arial")
                                .FontSize(15)
                                .Bold();


                        });
                    });

                page.Footer()
                    .Column(column =>
                    {
                        column.Item()
                            .PaddingVertical(10)
                            .Text(text =>
                            {
                                text.Span("Page ");
                                text.CurrentPageNumber();
                                text.Span(" of ");
                                text.TotalPages();
                                text.AlignStart();
                            });
                    });
            });
        });




        return document.GeneratePdf();
    }
}
