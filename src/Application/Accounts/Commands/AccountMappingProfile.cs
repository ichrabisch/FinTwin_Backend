using Application.Accounts.Commands.CreateAccount;
using Application.Accounts.Commands.CreateTransaction;
using Application.Accounts.Commands.RetriveTransaction;
using AutoMapper;
using Domain.Accounts.Model;
using Domain.Accounts.ValueObjects;
using Domain.Common.ValueObjects;
using Domain.Members.Model;
using Domain.Members.Repository;
using Microsoft.VisualBasic;

namespace Application.Accounts.Commands;

public class AccountMappingProfile : Profile
{
    public AccountMappingProfile()
    {
        CreateMap<CreateAccountCommand, Account>()
            .ConstructUsing((cmd, ctx) =>
            {
                Member member = null!;
                if (ctx.Items.TryGetValue("Member", out var memberObj) && memberObj is Member)
                {
                    member = (Member)memberObj;
                }
                
                return Account.Create(
                    id: Guid.NewGuid(),
                    accountName: cmd.AccountName,
                    isPersonal: cmd.IsPersonal,
                    member: member
                );
            });

        CreateMap<ReceiptDto, Receipt>()
            .ConstructUsing((dto, ctx) =>
            {
                var merchant = ctx.Mapper.Map<Merchant>(dto.Merchant);
                var paymentMethod = ctx.Mapper.Map<PaymentMethodInfo>(dto.PaymentMethod);
                var recipt = Receipt.Create(
                    id: Guid.NewGuid(),
                    merchant: merchant,
                    paymentMethod: paymentMethod
                );
                foreach(var item in dto.Items)
                {
                    var receiptItem = ctx.Mapper.Map<ReceiptItem>(item);
                    recipt.AddReceiptItem(receiptItem);
                }
                return recipt;
            }).ReverseMap();

        CreateMap<MerchantDto, Merchant>()
            .ConstructUsing((dto, ctx) =>
            {
                var address = new Address(
                    country: dto.Country,
                    city: dto.City,
                    street: dto.Street
                );
                return Merchant.Create(
                    name: dto.Name,
                    address: address,
                    phoneNumber: dto.PhoneNumber
                );
            });
        CreateMap<PaymentMethodDto, PaymentMethodInfo>()
            .ConstructUsing((dto, ctx) =>
            {
                return PaymentMethodInfo.Create(
                    type: dto.Type,
                    last4: dto.Last4
                );
            });
        CreateMap<ItemDto, ReceiptItem>()
            .ConstructUsing((dto, ctx) =>
            {
                return new ReceiptItem(
                    itemName: dto.ItemName,
                    itemDescription: dto.ItemDescription,
                    quantity: dto.Quantity,
                    unit: dto.Unit,
                    unitPrice: dto.UnitPrice,
                    totalPrice: dto.TotalPrice,
                    taxRate: dto.TaxRate,
                    category: dto.Category,
                    generalCategory: dto.GeneralCategory
                    );
            });

        CreateMap<Receipt, ReceiptDto>()
            .ConstructUsing((src, ctx) => new ReceiptDto(
                src.CreatedAt,
                src.Total,
                ctx.Mapper.Map<PaymentMethodDto>(src.PaymentMethod),
                ctx.Mapper.Map<MerchantDto>(src.Merchant),
                ctx.Mapper.Map<List<ItemDto>>(src.Items)
            ));

        CreateMap<PaymentMethodInfo, PaymentMethodDto>()
            .ConstructUsing((src, ctx) => new PaymentMethodDto(
                src.Type,
                src.Last4
            ));

        CreateMap<Merchant, MerchantDto>()
            .ConstructUsing((src, ctx) => new MerchantDto(
                src.Name,
                src.Address.Country,
                src.Address.City,
                src.Address.Street,
                src.PhoneNumber
            ));

        CreateMap<ReceiptItem, ItemDto>()
            .ConstructUsing((src, ctx) => new ItemDto(
                src.ItemName,
                src.ItemDescription,
                src.Category,
                src.GeneralCategory,
                src.Unit,
                src.Quantity,
                src.UnitPrice,
                src.TotalPrice,
                src.TaxRate
            ));

        CreateMap<Transaction, RetriveTransactionDto>()
         .ConstructUsing((src, ctx) => new RetriveTransactionDto(
             src.Id,
             src.AccountId,
             src.Amount,
             src.Description,
             src.TransactionType,
             src.CreatedAt,
             src.UpdatedAt,
             src.Receipt != null ? ctx.Mapper.Map<ReceiptDto>(src.Receipt) : null
         ));

        CreateMap<Receipt, ReceiptDto>()
            .ConstructUsing((src, ctx) => new ReceiptDto(
                src.CreatedAt,
                src.Total,
                ctx.Mapper.Map<PaymentMethodDto>(src.PaymentMethod),
                ctx.Mapper.Map<MerchantDto>(src.Merchant),
                ctx.Mapper.Map<List<ItemDto>>(src.Items)
            ));

        CreateMap<Account, RetriveAccountDto>()
            .ConstructUsing((src, ctx) =>
            {
                return new RetriveAccountDto(
                    AccountId: src.Id,
                    AccountName : src.AccountName,
                    Balance: src.Balance,
                    IsPersonal: src.IsPersonal,
                    CreatedAt: src.CreatedAt
                );
            });
    }
}
