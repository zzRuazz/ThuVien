USE [ThuVien]
GO

INSERT INTO [dbo].[Category]([Id], [Name], [Slug], [Image], [ParentId], [IsActived], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy], [DeletedAt], [DeletedBy], [IsDeleted]) VALUES
('4c8aba93-53a5-4924-9f81-df9cdd9780bd', N'Sói già phố Wall', 'soi-gia-pho-wall', '1', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('a5750039-7ad2-42e7-9d20-d9666baf455e', N'Homodeus', 'homodeus', '2', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('fc76bd70-e229-4bfe-8d93-d619f4af7592', N'Vũ điệu của Thần Chết', 'vu-dieu-cua-than-chet', '3', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('46165e06-31a5-4566-9ad1-fcb15a00f95e', N'Bố già', 'bo-gia', '5', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('44a81e4f-074b-42bc-98ff-f3de745329d1', N'Hannibal', 'hannibal', '6', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('13e24b0d-832e-4aec-8382-1f5589657123', N'Trở về Eden', 'tro-ve-eden', '7', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('64c7d30a-f17f-4775-90c5-b90e8e54ebf9', N'Những cuộc phiêu lưu của Sherlock Holmes', 'nhung-cuoc-phieu-luu-cua-sherlock-holmes', '8', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('678992b7-8f0c-4fe6-a4ca-23cab41a512c', N'Hai vạn dăm dưới biển', 'hai-van-dam-duoi-bien', '9', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('0e3df6af-7c5d-4a98-b05b-cfae141c4ef1', N'Truyện ngắn Nam Cao', 'truyen-ngan-nam-cao', '10', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('0bcfbb34-b113-438c-a2f4-afe57b625ad6', N'Bước đường cùng', 'buoc-duong-cung', '11', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('1052bd94-3039-4e28-a1f3-4e6e093fd094', N'Tiếng gọi nơi hoang dã', 'tieng-goi-noi-hoang-da', '12', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('a9dbe284-bebc-499a-9869-5b17892e2fed', N'Edragon', 'edragon', '13', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0),
('d25a58a9-5961-4d0a-a0d0-23934d85ca14', N'Ông già và biển cả', 'ong-gia-va-bien-ca', '14', NULL, 1, GETDATE(), NULL, NULL, NULL, NULL, NULL, 0);
GO