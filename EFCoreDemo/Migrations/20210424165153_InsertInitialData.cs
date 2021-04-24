using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCoreDemo.Migrations
{
    public partial class InsertInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO ProductCategory(Name, CreatedDate)
                                    SELECT c, GETDATE()
                                    FROM (VALUES('Skirt'),('Dress'),('Trousers'),('Blazer'),('Suit')) as category(c)");

            migrationBuilder.Sql(@"INSERT INTO Product(Name, CategoryId) VALUES('DOUBLE-BREASTED LINEN BLEND BLAZER',(SELECT ProductCategoryId FROM ProductCategory WHERE Name = 'Blazer'))
                                   INSERT INTO Product(Name, CategoryId) VALUES('TEXTURED DOUBLE-BREASTED BLAZER', (SELECT ProductCategoryId FROM ProductCategory WHERE Name = 'Blazer'))
                                   INSERT INTO Product(Name, CategoryId) VALUES('TEXTURED FITTED BLAZER', (SELECT ProductCategoryId FROM ProductCategory WHERE Name = 'Blazer'))
                                   INSERT INTO Product(Name, CategoryId) VALUES('LINEN BLEND FLARED DRESS', (SELECT ProductCategoryId FROM ProductCategory WHERE Name = 'Blazer'))
                                   INSERT INTO Product(Name, CategoryId) VALUES('DRESS WITH CUTWORK EMBROIDERY', (SELECT ProductCategoryId FROM ProductCategory WHERE Name = 'Blazer'))
                                   INSERT INTO Product(Name, CategoryId) VALUES('LONG STRIPED DRESS', (SELECT ProductCategoryId FROM ProductCategory WHERE Name = 'Dress'))
                                   INSERT INTO Product(Name, CategoryId) VALUES('TIE-DYE DRESS', (SELECT ProductCategoryId FROM ProductCategory WHERE Name = 'Dress'))
                                   INSERT INTO Product(Name, CategoryId) VALUES('FADED-EFFECT LONG DRESS', (SELECT ProductCategoryId FROM ProductCategory WHERE Name = 'Dress'))
                                   INSERT INTO Product(Name, CategoryId) VALUES('FADED-EFFECT DRESS WITH POCKETS', (SELECT ProductCategoryId FROM ProductCategory WHERE Name = 'Dress'))");

            migrationBuilder.Sql(@"INSERT INTO Company(Name, Revenue, FoundationDate) VALUES('TEST COMPANY 1', 10, CAST('2020-09-09' as date))
                                   INSERT INTO Company(Name, Revenue, FoundationDate) VALUES('TEST COMPANY 2', 10, CAST('2020-10-09' as date))
                                   INSERT INTO Company(Name, Revenue, FoundationDate) VALUES('TEST COMPANY 3', 10, CAST('2020-07-09' as date))");

            migrationBuilder.Sql(@"INSERT INTO Supply(CompanyId, ProductId) VALUES((SELECT CompanyId FROM Company WHERE Name = 'TEST COMPANY 1'), (SELECT ProductId FROM Product WHERE Name = 'TIE-DYE DRESS'))
                                   INSERT INTO Supply(CompanyId, ProductId) VALUES((SELECT CompanyId FROM Company WHERE Name = 'TEST COMPANY 1'), (SELECT ProductId FROM Product WHERE Name = 'FADED-EFFECT LONG DRESS'))
                                   INSERT INTO Supply(CompanyId, ProductId) VALUES((SELECT CompanyId FROM Company WHERE Name = 'TEST COMPANY 3'), (SELECT ProductId FROM Product WHERE Name = 'TEXTURED FITTED BLAZER'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Supply");
            migrationBuilder.Sql("DELETE FROM Company");
            migrationBuilder.Sql("DELETE FROM Product");
            migrationBuilder.Sql("DELETE FROM ProductCategory");
        }
    }
}
