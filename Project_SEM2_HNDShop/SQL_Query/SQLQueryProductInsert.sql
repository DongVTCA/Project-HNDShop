USE [HNDShoesShop]
GO

INSERT INTO [dbo].[Products]
           ([PromoId]
           ,[SubBrandId]
           ,[CateId]
           ,[ProCode]
           ,[ProName]
           ,[Quantity]
           ,[Size]
           ,[Color]
           ,[OriginPrice]
           ,[SellPrice]
           ,[StatusCode])
     VALUES
           (2,1,1,'Hello','Ultra Boost',2,24,'blue',2000000,2500000,1),
		    (2,1,1,'Hello','Ultra Boost',2,24,'blue',2000000,2500000,1),
			 (2,1,1,'Hello','Ultra Boost',2,24,'blue',2000000,2500000,1)
GO


