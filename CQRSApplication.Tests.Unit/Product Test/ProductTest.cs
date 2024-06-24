using CQRSApplication.Command.ProductCommand;
using CQRSApplication.Command.ProductCommandHandler;
using CQRSApplication.Context;
using CQRSApplication.Model;
using CQRSApplication.Query.ProductQuery;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CQRSApplication.Tests.Unit.Product_Test
{
    public class ProductTest
    {
        CQRSDbContext context;
        private readonly ITestOutputHelper output;

        public ProductTest(ITestOutputHelper output)
        {
            DbContextOptionsBuilder<CQRSDbContext> optionsBuilder = new();
            optionsBuilder.UseInMemoryDatabase(MethodBase.GetCurrentMethod().Name);
            context = new(optionsBuilder.Options);
            this.output = output;
        }

        [Fact]
        public async void Queries_ProductQuery_ReturnsAll()
        {
            output.WriteLine(context.Products.Count().ToString());
            var item1 = new Product
            {
                Id = new Guid("a1cc8ef7-3ff3-4bec-884a-ef033b2acfa3"),
                Name = "Product 1",
                VendorId = new Guid("8b83afdf-ea34-44e5-84df-44f547cc53d0"),
                Price = 120,
                Stock = 100,
                Category = "asdasd",
                ImageUrl = "asd",
                Description = "asdasd"
            };

            await context.Products.AddAsync(item1);

            var item2 = new Product
            {
                Id = new Guid("315af8e9-7ca2-48db-881d-c667df2b6519"),
                Name = "Product 2",
                VendorId = new Guid("2e294730-26f4-4f59-aac5-678f6b65c379"),
                Price = 120,
                Stock = 100,
                Category = "asdasd",
                ImageUrl = "asd",
                Description = "asdasd"
            };

            await context.Products.AddAsync(item2);
            await context.SaveChangesAsync();

            var handler = new GetAllProductQueryHandler(context);
            var result = await handler.Handle(new GetAllProductQuery(), CancellationToken.None);
            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async void Queries_ProductQuery_ReturnById()
        {
            var item2 = new Product
            {
                Id = new Guid("215af8e9-7ca2-48db-881d-c667df2b6519"),
                Name = "Product 2",
                VendorId = new Guid("2e294730-26f4-4f59-aac5-678f6b65c379"),
                Price = 120,
                Stock = 100,
                Category = "asdasd",
                ImageUrl = "asd",
                Description = "asdasd"
            };
            await context.Products.AddAsync(item2);
            context.SaveChanges();

            var handler = new GetProductQueryHandler(context);

            var result = await handler.Handle(new GetProductQuery
            {
                Id = new Guid("215af8e9-7ca2-48db-881d-c667df2b6519")
            }, CancellationToken.None);

            result.Name.Should().Be("Product 2");
        }

        [Fact]
        public async void Command_Product_Create()
        {
            var mediatorMock = new Mock<IMediator>();

            var User1 = new User
            {
                Id = new Guid("07593bc7-a76f-4150-9839-de656a799dd7"),
                FirstName = "User1",
                LastName = "string",
                PhoneNumber = "19239123123",
                Address = "Shantinagar",
                ImageUrl = "",
                UserCredentialsId = new Guid("10cf5ce7-9b74-4a90-b328-43a821090a0a")

            };

            var user1Credentials = new UserCredentials
            {
                Id = new Guid("10cf5ce7-9b74-4a90-b328-43a821090a0a"),
                Email = "test@test.com",
                UserName = "User1",
                Password = "Papa",
                Role = RoleType.Vendor,
                IsActive = false
            };
            await context.UserCredentials.AddAsync(user1Credentials);
            await context.Users.AddAsync(User1);

            var vendor1 = new Vendor
            {
                Id = new Guid("da0af3df-021f-4eae-ae5e-0cc50bd3635c"),
                ShopAddress = "Shantinagar",
                ShopName = "New Fancy Store",
                PanNo = "9123912301923",
                UserId = new Guid("07593bc7-a76f-4150-9839-de656a799dd7")
            };

            await context.Vendors.AddAsync(vendor1);

            await context.SaveChangesAsync();
            var handler = new CreateProductCommandHandler(context, mediatorMock.Object);

            var item = new CreateProductCommand
            {
                Name = "Product 1",
                VendorId = new Guid("da0af3df-021f-4eae-ae5e-0cc50bd3635c"),
                Price = 120,
                Stock = 100,
                Category = "asdasd",
                Description = "asdasd",
                Image = null
            };

            var result = await handler.Handle(item, CancellationToken.None);
            var item2 = new CreateProductCommand
            {
                Name = "Product 2",
                VendorId = new Guid("da0af3df-021f-4eae-ae5e-0cc50bd3635c"),
                Price = 120,
                Stock = 100,
                Category = "asdasd",
                Description = "asdasd",
                Image = null
            };
            var result2 = await handler.Handle(item2, CancellationToken.None);

            result.Name.Should().Be("Product 1");
            result2.Name.Should().Be("Product 2");
        }

        [Fact]
        public async void Command_Product_Update()
        {
            var mediatorMock = new Mock<IMediator>();

            var user1 = new User
            {
                Id = new Guid("3e81cf93-d29e-4f55-addd-0d5e78431c91"),
                FirstName = "User1",
                LastName = "string",
                PhoneNumber = "19239123123",
                Address = "Shantinagar",
                ImageUrl = "",
                UserCredentialsId = new Guid("d3c16ddb-e79c-4ee0-89bd-c51bc43d46ef")

            };

            var user1Credentials = new UserCredentials
            {
                Id = new Guid("d3c16ddb-e79c-4ee0-89bd-c51bc43d46ef"),
                Email = "test@test.com",
                UserName = "User1",
                Password = "Papa",
                Role = RoleType.Vendor,
                IsActive = false
            };
            await context.UserCredentials.AddAsync(user1Credentials);
            await context.Users.AddAsync(user1);

            await context.SaveChangesAsync();
            var vendor2 = new Vendor
            {
                Id = new Guid("5e8cae4c-b807-4376-a755-51f14ebe188d"),
                ShopAddress = "Shantinagar",
                ShopName = "New Fancy Store",
                PanNo = "9123912301923",
                UserId = new Guid("3e81cf93-d29e-4f55-addd-0d5e78431c91"),
            };

            await context.Vendors.AddAsync(vendor2);
            await context.SaveChangesAsync();
            var item = context.Products.FirstOrDefault();
            var command = new UpdateProductCommand
            {
                ProductId = item.Id.ToString(),
                Name = item.Name,
                Description = item.Description,
                Price = item.Price - 20F,
                Category = item.Category,
                Stock = item.Stock - 20,
                VendorId = new Guid("5e8cae4c-b807-4376-a755-51f14ebe188d"),
            };
            var handler = new UpdateProductCommandHandler(context, mediatorMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            result.Name.Should().Be(item.Name);
            result.Id.Should().Be(item.Id);
        }

        [Fact]
        public async void Command_ProductCommand_Delete()
        {
            var mediatorMock = new Mock<IMediator>();

            var User1 = new User
            {
                Id = new Guid("cb92efb3-ca01-48f6-91fa-6b2bf278771f"),
                FirstName = "User1",
                LastName = "string",
                PhoneNumber = "19239123123",
                Address = "Shantinagar",
                ImageUrl = "",
                UserCredentialsId = new Guid("42b6a1c9-4bc5-43cc-adc9-19ccadb999ee")

            };

            var user1Credentials = new UserCredentials
            {
                Id = new Guid("42b6a1c9-4bc5-43cc-adc9-19ccadb999ee"),
                Email = "test@test.com",
                UserName = "User1",
                Password = "Papa",
                Role = RoleType.Vendor,
                IsActive = false
            };
            await context.UserCredentials.AddAsync(user1Credentials);
            await context.Users.AddAsync(User1);

            var vendor1 = new Vendor
            {
                Id = new Guid("fb93af2a-feb3-4df4-a7dc-ce0f18c67064"),
                ShopAddress = "Shantinagar",
                ShopName = "New Fancy Store",
                PanNo = "9123912301923",
                UserId = new Guid("cb92efb3-ca01-48f6-91fa-6b2bf278771f")
            };

            await context.Vendors.AddAsync(vendor1);
            var item1 = new CreateProductCommand
            {
                Name = "Product 1",
                VendorId = new Guid("fb93af2a-feb3-4df4-a7dc-ce0f18c67064"),
                Price = 120,
                Stock = 100,
                Category = "asdasd",
                Description = "asdasd",
                Image = null
            };
            var handler1 = new CreateProductCommandHandler(context, mediatorMock.Object);

            var result1 = await handler1.Handle(item1, CancellationToken.None);

            var item = context.Products.FirstOrDefault();

            var command = new DeleteProductCommand
            {
                ProductId = item.Id,
                VendorId = item.VendorId
            };

            var handler = new DeleteProductCommandHandler(context, mediatorMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Name.Should().Be(item.Name);
        }
    }
}
