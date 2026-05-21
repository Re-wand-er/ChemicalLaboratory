using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ChemicalLaboratory.Application.Interfaces;
using ChemicalLaboratory.Application.UseCases.DTOs;

namespace ChemicalLaboratory.Application.UseCases.Services
{
  	public class GeneratePDFService
    {
		private readonly IUnitOfWork _unitOfWork;

		public GeneratePDFService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

    	// Метод в твоем сервисе
    	public Task<string> GenerateLowStockInvoicePdfAsync(List<ReagentDTO> reagentDTOs)
		{
	    	if (reagentDTOs == null || !reagentDTOs.Any())
	    	    return Task.FromResult(string.Empty);
	
	    	var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "invoices");
	    	if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
	
	    	// Фиктивный, но реалистичный номер накладной на основе текущей даты
	    	var invoiceNumber = $"НЗ-{DateTime.Now:yyMMdd}/{new Random().Next(100, 999)}";
	    	var fileName = $"invoice_low_stock_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
	    	var filePath = Path.Combine(folderPath, fileName);
	
	    	Document.Create(container =>
			{
			    container.Page(page =>
			    {
			        page.Size(PageSizes.A4);
			        page.Margin(1.5f, Unit.Centimetre); 
			        page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(10).FontColor(Colors.Grey.Darken4)); 
	
			        // ==================== ХЕДЕР ДОКУМЕНТА ====================
			        page.Header().Column(col =>
			        {
			            col.Item().Row(row =>
			            {
			                row.RelativeItem().Column(c =>
			                {
			                    c.Item().Text("ООО \"ЦЕНТРАЛЬНАЯ ХИМИЧЕСКАЯ ЛАБОРАТОРИЯ\"").Bold().FontSize(8);
    							c.Item().Text("УНП: 190002026").FontSize(8).FontColor(Colors.Grey.Medium); 
    							c.Item().Text("Адрес: Республика Беларусь, 220072, г. Минск, ул. Академическая, д. 12, корп. 2")
    							    .FontSize(8).FontColor(Colors.Grey.Medium); 
			                });
	
			                row.ConstantItem(45).Height(45).Background(Colors.Grey.Lighten2).AlignCenter().AlignMiddle()
			                   .Text("QR").FontSize(10).Bold().FontColor(Colors.Grey.Darken1); 
			            });
	
			            col.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten1); 
	
			            col.Item().PaddingTop(15).Row(row =>
			            {
			                row.RelativeItem().Column(c =>
			                {
			                    c.Item().Text($"НАКЛАДНАЯ-ТРЕБОВАНИЕ № {invoiceNumber}")
			                        .ExtraBold().FontSize(15).FontColor(Colors.Blue.Darken4); 
			                    c.Item().Text($"Дата формирования: {DateTime.Now:dd.MM.yyyy} г. Время: {DateTime.Now:HH:mm}")
			                        .FontSize(9).FontColor(Colors.Grey.Darken1); 
			                });
			            });
			        });
	
			        // ==================== ОСНОВНОЙ КОНТЕНТ ====================
			        page.Content().PaddingTop(15).Column(col =>
			        {
			            col.Item().Text("Основание: Нарушение неснижаемого остатка (Дефицит)").Italic().FontSize(9).FontColor(Colors.Red.Darken2); 
			            col.Item().PaddingTop(5);
	
			            // ИСПРАВЛЕНО: Дубликат удален, таблица инициализируется корректно
			            col.Item().Table(table =>
			            {
			                table.ColumnsDefinition(columns =>
			                {
			                    columns.ConstantColumn(30);  // № п/п
			                    columns.RelativeColumn(4);   // Название
			                    columns.RelativeColumn(2);   // Формула
			                    columns.RelativeColumn(1.5f); // Остаток
			                    columns.RelativeColumn(1.5f); // Минимум
			                    columns.RelativeColumn(1.5f); // Требуется
			                    columns.RelativeColumn(1.5f); // Ед. изм.
			                });
	
			                // Шапка таблицы
			                table.Header(header =>
			                {
			                    // ИСПРАВЛЕНО: Перешли с .Color() на .FontColor() для стиля
			                    var headerStyle = TextStyle.Default.Bold().FontColor(Colors.White).FontSize(9);
			                    var headerBg = Colors.Blue.Darken3;
	
			                    // ИСПРАВЛЕНО: Применение стиля переведено на современный формат цепочки методов .Style()
			                    header.Cell().Background(headerBg).Padding(5).AlignCenter().Text("№").Style(headerStyle);
			                    header.Cell().Background(headerBg).Padding(5).Text("Наименование реактива").Style(headerStyle);
			                    header.Cell().Background(headerBg).Padding(5).Text("Формула").Style(headerStyle);
			                    header.Cell().Background(headerBg).Padding(5).AlignRight().Text("Остаток").Style(headerStyle);
			                    header.Cell().Background(headerBg).Padding(5).AlignRight().Text("Мин.").Style(headerStyle);
			                    header.Cell().Background(headerBg).Padding(5).AlignRight().Text("Заказ").Style(headerStyle);
			                    header.Cell().Background(headerBg).Padding(5).AlignCenter().Text("Ед.").Style(headerStyle);
			                });
	
			                // Строки таблицы
			                int index = 1;
			                foreach (var reagent in reagentDTOs)
			                {
			                    decimal requiredQty = reagent.MinQuantity - reagent.CurrentQuantity;
			                    if (requiredQty < 0) requiredQty = 0;
	
			                    var rowBg = (index % 2 == 0) ? Colors.Grey.Lighten4 : Colors.White;
	
			                    table.Cell().Background(rowBg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text(index.ToString());
			                    table.Cell().Background(rowBg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(reagent.Name).Bold();
			                    table.Cell().Background(rowBg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten2).Padding(5).Text(reagent.ChemicalFormula ?? "-");
			                    table.Cell().Background(rowBg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignRight().Text(reagent.CurrentQuantity.ToString("F2"));
			                    table.Cell().Background(rowBg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignRight().Text(reagent.MinQuantity.ToString("F2"));
	
			                    table.Cell().Background(rowBg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignRight().Text(requiredQty.ToString("F2")).Bold().FontColor(Colors.Red.Darken3); 
	
			                    table.Cell().Background(rowBg).BorderBottom(0.5f, Unit.Point).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text(reagent.Unit);
	
			                    index++;
			                }
			            });
	
			            col.Item().PaddingTop(10).AlignRight().Text($"Всего наименований к пополнению: {reagentDTOs.Count}").Bold().FontSize(10);
	
			            // Блок подписей
			            col.Item().PaddingTop(40).Row(row =>
			            {
			                row.RelativeItem(5).Column(c =>
			                {
			                    c.Item().Text("Ответственный лаборант:").Bold().FontSize(9);
			                    c.Item().PaddingTop(15).Text("_____________________ / _____________________").FontSize(10);
			                    c.Item().Text("(Подпись)                        (Ф.И.О.)").FontSize(8).FontColor(Colors.Grey.Medium); 
			                });
	
			                row.RelativeItem(2); 
	
			                row.RelativeItem(5).Column(c =>
			                {
			                    c.Item().Text("Заведующий лабораторией (Утвердил):").Bold().FontSize(9);
			                    c.Item().PaddingTop(15).Text("_____________________ / _____________________").FontSize(10);
			                    c.Item().Text("(Подпись)                        (Ф.И.О.)").FontSize(8).FontColor(Colors.Grey.Medium); 
			                });
			            });
			        });
	
			        // ==================== ПОДВАЛ СТРАНИЦЫ ====================
			        page.Footer().Column(col =>
			        {
			            col.Item().LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten2); 
			            col.Item().PaddingTop(5).Row(row =>
			            {
			                row.RelativeItem().Text("Документ сформирован автоматически АИС 'Учет реагентов v2.0'").FontSize(8).FontColor(Colors.Grey.Medium); 
			                row.RelativeItem().AlignRight().Text(x =>
			                {
			                    x.CurrentPageNumber();
			                    x.Span(" / "); 
			                    x.TotalPages();
			                });
			            });
			        });
			    });
			}).GeneratePdf(filePath);


	    	return Task.FromResult($"/invoices/{fileName}");
		}
	}
}